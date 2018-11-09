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
            @"Attention!: Please use https://paste.ubuntu.com to share code or message that is more than 10-15 lines. You have been warned, {0}!\n
              Please read rule #6 here <#331130181102206976>";

        private static readonly string HASTEBIN_MESSAGE =
            "{0} being {1}, posted a message that is more than 15 lines. It is now available at: {2}";

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
            logger.Warn("The @HassBot was disconnected... will try to connect in 5 seconds.");

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

            if (!Utils.LineCountCheck(message.Content))
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
                        string msxLimitMsg = AppSettingsUtil.AppSettingsString("maxLineLimitMessage", false, MAX_LINE_LIMIT);
                        await message.Channel.SendMessageAsync(string.Format(msxLimitMsg, context.User.Mention));
                        await context.Message.AddReactionAsync(poopEmoji);
                        return;
                    }
                }

                // publish the URL link
                string adjective = HassBotUtils.Utils.GetFlippinAdjective();
                string response = string.Format(HASTEBIN_MESSAGE, context.User.Mention, adjective, url);
                await message.Channel.SendMessageAsync(response);

                // and, delete the original message!
                await context.Message.DeleteAsync();
            }
        }

        private static async Task HandleCustomCommand(string command, SocketUserMessage message, string mentionedUsers, IResult result)
        {
            CommandDTO cmd = CommandManager.TheCommandManager.GetCommandByName(command);
            if (cmd != null && cmd.CommandData != string.Empty)
            {
                cmd.CommandCount += 1;
                CommandManager.TheCommandManager.UpdateCommand(cmd);
                await message.Channel.SendMessageAsync(mentionedUsers + cmd.CommandData);
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
                    string msg = "**{0} is away** for {1}with a message :point_right: {2}";
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