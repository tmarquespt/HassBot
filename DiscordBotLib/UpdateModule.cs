///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : UpdateModule.cs
//  DESCRIPTION     : A class that implements ~update command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using HassBotData;

namespace DiscordBotLib
{
    public class UpdateModule : BaseModule {

        [Command("update")]
        public async Task UpdateAsync() {
            var embed = new EmbedBuilder();
            try {
                Sitemap.ReloadData();
            }
            catch{
                embed.WithColor(Color.Red);
                embed.AddInlineField(Constants.EMOJI_FAIL, Constants.UPDATE_FAILED);
                await ReplyAsync(string.Empty, false, embed);
                return;
            }

            embed.WithColor(Helper.GetRandomColor());
            embed.AddInlineField(Constants.EMOJI_THUMBSUP, Constants.UPDATE_SUCCESSFUL);
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}