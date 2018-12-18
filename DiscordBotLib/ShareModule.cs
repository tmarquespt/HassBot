///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : Share.cs
//  DESCRIPTION     : A class that implements ~share command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public class ShareModule : BaseModule {

        [Command("share")]
        public async Task ShareAsync() {
            await ShareCommand();
        }

        [Command("share")]
        public async Task ShareAsync([Remainder]string cmd) {
            await ShareCommand();
        }

        private async Task ShareCommand() {
            // mentioned users
            string mentionedUsers = base.MentionedUsers();
            var embed = new EmbedBuilder();
            embed.WithTitle(Constants.EMOJI_POINT_DOWN);
            embed.WithColor(Color.DarkRed);
            embed.AddInlineField(Constants.FORMAT_CODE, mentionedUsers + Constants.SHARE_MESSAGE);

            await ReplyAsync(string.Empty, false, embed);
        }
    }
}