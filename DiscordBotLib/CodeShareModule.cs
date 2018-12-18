///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 04/21/2018
//  FILE            : CodeShareModule.cs
//  DESCRIPTION     : A class that implements ~codeshare command
///////////////////////////////////////////////////////////////////////////////
using System.Threading.Tasks;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Linq;
using Discord;

namespace DiscordBotLib
{

    public class CodeShareModule : BaseModule {

        [Command("codeshare")]
        public async Task CodeShareAsync() {
            await CodeShareCommand();
        }

        [Command("codeshare")]
        public async Task CodeShareAsync([Remainder]string cmd) {
            await CodeShareCommand();
        }

        private async Task CodeShareCommand() {
            var embed = new EmbedBuilder();
            string codeshareUrl = HassBotUtils.Utils.GetCodeShareURL(Constants.CODESHARE_URL);
            string message = string.Format(Constants.CODESHARE_MESSAGE, codeshareUrl);

            // mention users if any
            string mentionedUsers = base.MentionedUsers();
            if (string.Empty != mentionedUsers)
                message = mentionedUsers + " " + message;

            await ReplyAsync(message, false, null);
        }
    }
}