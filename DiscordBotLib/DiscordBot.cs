using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HassBotData;
using HassBotDTOs;
using HassBotUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DiscordBotLib
{
    public class DiscordBot
    {
        private static readonly char PREFIX_1 = '~';
        private static readonly char PREFIX_2 = '.';

        private static readonly string POOP = "💩";

        private static readonly string TOKEN = "token";
        private static readonly string MAX_LINE_LIMIT =
            @"Atenção! Por favor, usa <https://www.hastebin.com> ou similar para partilhares a tua mensagem/código se tiver mais que 15 linhas. Foste avisado, {0}!\n
              Lê as regras de utilização aqui <#regras-de-utilização>";

        private static readonly string OLD_HASTEBIN_MESSAGE =
            "Por favor, cumpre as regras, {0}! Ainda tens {1} aviso(s) antes de ser tomada uma acção punitiva. Publicaste uma mensagem/código com mais de 15 linhas. Foi movida para aqui: --> {2}";

        private static readonly string HASTEBIN_MESSAGE =
            "{0} colocou uma mensagem/código muito longa e foi movida para aqui --> {1}";

        private static readonly log4net.ILog logger =
             log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        private System.Timers.Timer siteMapRefreshTimer = null;

        public async Task StartBotAsync()
        {
            await StartInternal();
        }

        public async void Start()
        {
            await StartInternal();
        }

        private void SiteMapRefreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // reload sitemap data
            Sitemap.ReloadData();
        }

        public async void Stop()
        {
            siteMapRefreshTimer.Enabled = false;
            await _client.LogoutAsync();
        }

        private async Task StartInternal()
        {

            // when the bot starts, start hourly timer to refresh sitemap
            if (null == siteMapRefreshTimer)
            {
                siteMapRefreshTimer = new System.Timers.Timer(60 * 60 * 1000);
                siteMapRefreshTimer.Elapsed += SiteMapRefreshTimer_Elapsed;
            }
            siteMapRefreshTimer.Enabled = true;

            // create client and command objects
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            // register commands
            await RegisterCommandsAsync();

            string token = AppSettingsUtil.AppSettingsString(TOKEN, true, string.Empty);
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // wait forever and process commands! 
            await Task.Delay(Timeout.Infinite);
        }

        private async Task RegisterCommandsAsync()
        {
            _client.Log += Helper.LogMessage;
            _commands.Log += Helper.LogMessage;
            _client.UserJoined += NewUser.NewUserJoined;
            _client.MessageReceived += HandleCommandAsync;
            _client.Disconnected += _client_Disconnected;

            Assembly libAssembly = Assembly.Load("DiscordBotLib");
            await _commands.AddModulesAsync(libAssembly);
        }

        private async Task _client_Disconnected(Exception arg)
        {
            siteMapRefreshTimer.Enabled = false;
            logger.Warn("O @CPHAbot perdeu a ligação... Irá tentar de novo em 5 segundos.");

            // wait for 5 seconds
            await Task.Delay(5000);

            // start all over again!
            await StartInternal();
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            // Create a Command Context.
            var context = new SocketCommandContext(_client, message);
            var channel = message.Channel as SocketGuildChannel;

            // remove houndci-bot messages from #github channel
            await Helper.HandleHoundCIMessages(message, context, channel);

            // process subscriptions
            await Helper.ProcessSubscriptions(message, context, channel);

            // We don't want the bot to respond to itself or other bots.
            if (message.Author.Id == _client.CurrentUser.Id || message.Author.IsBot)
                return;

            // check if the user was in "away" mode. if it is, the user is no longer "away"
            AFKManager.TheAFKManager.RemoveAFKUserById(context.User.Id);

            // Block/Remove messages that contain harmful links
            await Helper.CheckBlockedDomains(message.Content, context);

            // verify URLs
            await Helper.VerifyUrls(message.Content, context);

            // YAML verification
            await Helper.ReactToYaml(message.Content, context);

            // JSON verification
            await Helper.ReactToJson(message.Content, context);

            // Line limit check
            await HandleLineCount(message, context);

            // handle mentioned users
            string mentionedUsers = await HandleMentionedUsers(message);

            // Create a number to track where the prefix ends and the command begins
            int pos = 0;
            if (!(message.HasCharPrefix(PREFIX_1, ref pos) ||
                  message.HasCharPrefix(PREFIX_2, ref pos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref pos)))
                return;

            var result = await _commands.ExecuteAsync(context, pos, _services);

            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            {
                logger.Error(result.ErrorReason);
                return;
            }

            // if you are here, it means there is no module that could handle the message/command
            // lets check if we have anything in the custom commands collection
            string key = message.Content.Substring(1);
            string command = key.Split(' ')[0];

            // handle custom command
            await HandleCustomCommand(command, message, mentionedUsers, result);
        }

        private static async Task HandleLineCount(SocketUserMessage message, SocketCommandContext context)
        {
            if (Utils.LineCountCheckPassed(message.Content))
                return;

            if (!IsMod(context.User.Username))
            {
                string url = await HassBotUtils.Utils.Paste2Ubuntu(message.Content, context.User.Username);
                if (url == string.Empty)
                {
                    // untutu paste failed... try hastebin
                    url = HassBotUtils.Utils.Paste2HasteBin(message.Content);
                    if (url == string.Empty)
                    {
                        // hastebin paste ALSO failed... just warn the user, and drop a poop emoji :)
                        var poopEmoji = new Emoji(POOP);
                        await message.Channel.SendMessageAsync(string.Format(Constants.MAXLINELIMITMESSAGE, context.User.Mention));
                        await context.Message.AddReactionAsync(poopEmoji);
                        return;
                    }
                }

                //List<Violation> violations = ViolationsManager.TheViolationsManager.GetIncidentsByUser(context.User.Id);
                //int totalViolations = 0;
                //if (null != violations)
                //    totalViolations = violations.Count;

                // publish the URL link
                string response = string.Format(HASTEBIN_MESSAGE, context.User.Mention, url);
                await message.Channel.SendMessageAsync(response);

                //// Violation Management
                //ViolationsManager.TheViolationsManager.AddIncident(context.User.Id, context.User.Username, CommonViolationTypes.Codewall.ToString(), context.Channel.Name);                
                //if (null != violations)
                //{
                //    if (violations.Count >= 3 && violations.Count <= 5 )
                //    {
                //        await KickWarningMessage(context);
                //    }
                //    else if (violations.Count > 5)
                //    {
                //        await KickMessage(message, context);
                //    }
                //}

                // and, delete the original message!
                await context.Message.DeleteAsync();
            }
        }

        private static async Task KickWarningMessage(SocketCommandContext context)
        {
            var dmChannel = await context.User.GetOrCreateDMChannelAsync();
            StringBuilder sb = new StringBuilder();
            sb.Append("\n\nCaro utilizador,");
            sb.Append("\n");
            sb.Append("Estás perto de ser expulso do nosso servidor de Discord por não cumprires as Regras. Já foste avisado por três (3) vezes, mais duas (2) e serás expulso.");
            sb.Append("\n");
            sb.Append("Apesar dos avisos violaste repetidamente as regras, as quais foram criadas para o bom funcionamento do servidor e respeito entre todos os utilizadores!");
            sb.Append("\n");
            sb.Append("Podes ler as nossas regras no canal #regras-de-utilização");
            sb.Append("\n");
            sb.Append("Por favor contacta um moderador ou administrador. Se a violação às regras continuar serás expulso do servidor.");
            sb.Append("\n");
            sb.Append("Obrigado!\n");

            await dmChannel.SendMessageAsync(sb.ToString());

            // send a message to #botspam channel as well
            await HAChannels.ModLogChannel(context).SendMessageAsync("O utilizador " + context.User.Mention + " foi avisado por violar as regras por três (3) vezes consecutivas!", false, null);
        }

        private static async Task KickMessage(SocketUserMessage message, SocketCommandContext context)
        {
            // Send a Direct Message to the User
            var dmChannel = await context.User.GetOrCreateDMChannelAsync();
            StringBuilder sb = new StringBuilder();

            sb.Append("\n\nCaro utilizador,");
            sb.Append("\n\n");
            sb.Append("Temos um problema. Existem regras que devem ser seguidas e que gostariamos **mesmo** que as seguisses.");
            sb.Append("\n");
            sb.Append("Certifica-te que **leste e compreendeste** as regras de utilização que estão descritas em #regras-de-utilização e as descrições de cada canal.");
            sb.Append("\n\n");
            sb.Append("Foste expulso do nosso servidor do Discord por publicares código com mais de 15 linhas por **MAIS DE 5 VEZES**.");
            sb.Append("\n\n");
            sb.Append("Gostariamos de te poder ajudar e dar o suporte que necessitas mas para isso as regras devem ser cumpridas");
            sb.Append("\n");
            sb.Append("Quando leres e compreenderes as regras, podes simplesmente fazer login de novo e voltares a estar connosco.");
            sb.Append("\n");
            sb.Append("Para te juntares de novo ao nosso servidor, clica no link abaixo.");
            sb.Append("\n");
            sb.Append("https://discord.gg/Mh9mTEA");
            sb.Append("\n\n");
            sb.Append("Obrigado, e esperamos ver-te de novo em breve!\n");

            await dmChannel.SendMessageAsync(sb.ToString());

            // kick the user
            await ((SocketGuildUser)message.Author).KickAsync("Publicação de mensagens/código com mais de 15 linhas por mais de cinco (5) vezes.", null);
            await message.Channel.SendMessageAsync("User " + context.User.Mention + " got kicked out because of posting too many codewalls!");

            // send a message to #botspam channel as well
            await HAChannels.ModLogChannel(context).SendMessageAsync("O utilizador " + context.User.Mention + " foi expulso por violar as regras mais de cinco (5) vezes!", false, null);

            // finally clear the violations, so that the user can start fresh
            ViolationsManager.TheViolationsManager.ClearViolationsForUser(context.User.Id);
        }

        private static bool IsMod(string user)
        {
            // get the list of mods from config file
            string mods = AppSettingsUtil.AppSettingsString("mods",
                                                             true,
                                                             string.Empty);
            string[] moderators = mods.Split(',');
            var results = Array.FindAll(moderators,
                                        s => s.Trim().Equals(user,
                                        StringComparison.OrdinalIgnoreCase));
            if (results.Length == 1)
                return true;
            else
                return false;
        }

        private static async Task HandleCustomCommand(string command, SocketUserMessage message, string mentionedUsers, IResult result)
        {
            string response = HassBotCommands.Instance.Lookup(command);
            if (string.Empty != response)
            {
                await message.Channel.SendMessageAsync(mentionedUsers + response);
            }
            else
            {
                if (result.IsSuccess)
                    return;

                // command not found, look it up and see if there are any results.
                string lookupResult = Sitemap.Lookup(command);
                if (string.Empty != lookupResult)
                {
                    await message.Channel.SendMessageAsync(mentionedUsers + lookupResult);
                }
            }
        }

        private static async Task<string> HandleMentionedUsers(SocketUserMessage message)
        {
            string mentionedUsers = string.Empty;
            foreach (var user in message.MentionedUsers)
            {
                AFKDTO afkDTO = AFKManager.TheAFKManager.GetAFKById(user.Id);
                if (afkDTO != null)
                {
                    string msg = "**{0} está ausente** por {1} com a mensagem :point_right: {2}";
                    string awayFor = string.Empty;
                    if ((DateTime.Now - afkDTO.AwayTime).Days > 0)
                    {
                        awayFor += (DateTime.Now - afkDTO.AwayTime).Days.ToString() + "d ";
                    }
                    if ((DateTime.Now - afkDTO.AwayTime).Hours > 0)
                    {
                        awayFor += (DateTime.Now - afkDTO.AwayTime).Hours.ToString() + "h ";
                    }
                    if ((DateTime.Now - afkDTO.AwayTime).Minutes > 0)
                    {
                        awayFor += (DateTime.Now - afkDTO.AwayTime).Minutes.ToString() + "m ";
                    }
                    if ((DateTime.Now - afkDTO.AwayTime).Seconds > 0)
                    {
                        awayFor += (DateTime.Now - afkDTO.AwayTime).Seconds.ToString() + "s ";
                    }

                    string awayMsg = string.Format(msg, afkDTO.AwayUser, awayFor, afkDTO.AwayMessage);
                    await message.Channel.SendMessageAsync(awayMsg);
                }
                mentionedUsers += $"{user.Mention} ";
            }
            return mentionedUsers;
        }
    }
}
