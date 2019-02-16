///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : WelcomeModule.cs
//  DESCRIPTION     : A class tha implements ~welcome command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HassBotUtils;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public class WelcomeModule : BaseModule {

        [Command("welcome")]
        public async Task WelcomeAsync() {
            await WelcomeCommand();
        }

        [Command("welcome")]
        public async Task WelcomeAsync([Remainder]string cmd) {
            await WelcomeCommand();
        }

        private async Task WelcomeCommand() {
            StringBuilder sb = new StringBuilder();

            string serverName = AppSettingsUtil.AppSettingsString("discordServerName", true, string.Empty); 
            string welcomerulesChannel = AppSettingsUtil.AppSettingsString("welcomerulesChannel", false, string.Empty);

            sb.Append(string.Format(Constants.WELCOME_MESSAGE, serverName));

            if (string.Empty != welcomerulesChannel) {
                sb.Append(string.Format(Constants.WELCOME_RULES_MESSAGE, "<#" + welcomerulesChannel + ">" ));
            }
            sb.Append(Constants.CODE_SHARING_MESSAGE);

            // mentioned users
            string mentionedUsers = base.MentionedUsers();
            var embed = new EmbedBuilder();
            embed.WithTitle(Constants.EMOJI_NAMASTE);
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField(Constants.WELCOME_TITLE, 
                                 mentionedUsers + sb.ToString());
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}