///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : HelpModule.cs
//  DESCRIPTION     : A class that implements ~help command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public class HelpModule : BaseModule {
        [Command("help")]
        public async Task HelpAsync() {
            await HelpCommand();
        }

        [Command("help")]
        public async Task HelpAsync([Remainder]string cmd) {
            await HelpCommand();
        }

        private async Task HelpCommand() {
            StringBuilder sb = new StringBuilder();
            GetCommaSeparatedCommandList(sb);
            // mention users if any
            string mentionedUsers = base.MentionedUsers();

            var embed = new EmbedBuilder();
            embed.WithTitle("💁");
            embed.WithColor(Helper.GetRandomColor());
            embed.AddField("Available commands: ", sb.ToString());
            if (mentionedUsers != string.Empty)
              embed.AddField("fyi", mentionedUsers);

            await ReplyAsync(string.Empty, false, embed);
        }

        private static void GetCommaSeparatedCommandList(StringBuilder buffer) {
            if (buffer == null)
                return;

            string[] commands = {
                "~help", "~about", "~8ball", "~list", "~command", "~lookup", "~deepsearch",
                "~format", "~share", "~lmgtfy", "~ping", "~pong", "~update", "~yaml",
                "~welcome", "~json2yaml", "~yaml2json", "~base64_encode", "~base64_decode", "~codeshare",
                "c2f", "f2c", "hex2dec", "dec2hex", "bin2dec", "dec2bin"
            };

            buffer.Append(string.Format("Run any of the following {0} commands: ", commands.Length));
            for (int i = 0; i < commands.Length; i++ ) {
                if (i == 0) buffer.Append("[ ");
                buffer.Append(commands[i]);
                if (i + 1 == commands.Length)
                    buffer.Append(" ]");
                else
                    buffer.Append(", ");
            }
        }
    }
}