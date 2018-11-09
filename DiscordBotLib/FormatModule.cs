///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : FormatModule.cs
//  DESCRIPTION     : A class that implements ~format command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;
using System;

namespace DiscordBotLib
{
    public class FormatModule : BaseModule {
        [Command("format")]
        public async Task FormatAsync() {
            await FormatCommand();
        }

        [Command("format")]
        public async Task FormatAsync([Remainder]string cmd) {
            await FormatCommand();
        }

        private async Task FormatCommand() {

            StringBuilder sb = new StringBuilder();
            sb.Append("To format your text as code, enter three backticks on the first line, press Enter for a new line, paste your code, press Enter again for another new line, and lastly three more backticks. Here's an example:\n\n");
            sb.Append("\\`\\`\\`\n");
            sb.Append("code here\n");
            sb.Append("\\`\\`\\`\n");
            sb.Append("\nClick on the link to learn how to format: <https://raw.githubusercontent.com/skalavala/HassBot/master/format.gif>\n");

            // mention users if any
            string mentionedUsers = base.MentionedUsers();

            var embed = new EmbedBuilder();
            embed.WithTitle(":information_source:");
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField("Format Code:", mentionedUsers + sb.ToString());
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}