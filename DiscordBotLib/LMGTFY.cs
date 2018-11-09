///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : HassBot.cs
//  DESCRIPTION     : A class that implements ~lmgtfy command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System.Web;

namespace DiscordBotLib
{

    public class LMGTFY : BaseModule {

        [Command("lmgtfy")]
        public async Task LetMeGoogleThatForYouAsync() {
            await base.DisplayUsage(Constants.USAGE_LMGTFY);
        }

        [Command("lmgtfy")]
        public async Task LetMeGoogleThatForYouAsync([Remainder]string cmd) {
            var embed = new EmbedBuilder();
            embed.WithTitle(Constants.EMOJI_POINT_UP);
            embed.WithColor(Helper.GetRandomColor());

            // mention users if any
            string mentionedUsers = base.MentionedUsers();
            if (string.Empty != mentionedUsers) {
                foreach (string user in mentionedUsers.Split(' '))
                    if (string.Empty != user) {
                        string userHandle = user.Replace("!", string.Empty);
                        cmd = cmd.Replace(userHandle.Trim(), string.Empty);
                    }
            }

            string encoded = HttpUtility.UrlEncode(cmd.Trim());
            embed.AddInlineField(Constants.LET_ME_GOOGLE,
                string.Format("Here, try this {0} => <http://lmgtfy.com/?q={1}>", mentionedUsers, encoded));
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}