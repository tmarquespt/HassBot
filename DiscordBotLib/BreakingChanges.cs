///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 11/11/2018
//  FILE            : BreakingChanges.cs
//  DESCRIPTION     : A class that implements ~breaking_changes command
///////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBotLib
{
    public class BreakingChanges : BaseModule
    {
        [Command("breaking_changes")]
        public async Task BreakingChangesAsync()
        {
            await base.DisplayUsage(Constants.USAGE_BREAKINGCHANGES);
        }

        [Command("breaking_changes")]
        public async Task BreakingChangesAsync([Remainder]string version)
        {
            var embed = new EmbedBuilder();

            // get the release notes of the specified version number
            string url = Helper.LookupString("release-" + version);
            url = url.Replace("\n", string.Empty);

            if (url == string.Empty)
            {
                embed.WithTitle(Constants.EMOJI_THUMBSDOWN);
                embed.WithColor(Helper.GetRandomColor());
                embed.AddField("Sorry!", "No release changes found for that version!");
                await ReplyAsync(string.Empty, false, embed);
                return;
            }
            else
            {
                if ( url.EndsWith("/"))
                    url += "#breaking-changes";
                else
                    url += "/#breaking-changes";
            }

            await ReplyAsync("<" + url + ">" );
        }
    }
}