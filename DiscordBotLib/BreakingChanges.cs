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

            StringBuilder sb = new StringBuilder();
            string data = HassBotData.Helper.DownloadBreakingChanges(url);

            string bc = GetValue(data, "</a> Breaking Changes</h2>\n\n<ul>\n  ", "</ul>");
            string[] lines = bc.Split('\n');
            if (lines.Count() > 0)
                sb.Append("Here are the breaking changes in version: ." + version + ".0\n```");

            foreach (string line in lines)
            {
                string tmp = line.Trim();
                if (tmp == string.Empty)
                    continue;

                string pr = GetValue(tmp, "home-assistant/home-assistant/pull/", "\"");
                string component = GetValue(tmp, "(<a href=\"/components/", "/\">");
                string description = GetValue(tmp, "<li>", "(<a ");
                //string docLink = string.Format("https://www.home-assistant.io/components/{0}", component);

                sb.Append(string.Format("PR# {0}- [{1}] {2}", pr, component.ToUpper(), description));
                sb.Append("\n");
            }
            sb.Append("```");
            await ReplyAsync(sb.ToString());
        }

        private string GetValue(string line, string start_tag, string end_tag)
        {
            int startIndex = line.IndexOf(start_tag);
            if (startIndex == -1)
                return string.Empty;

            startIndex = startIndex + start_tag.Length;
            int endIndex = line.IndexOf(end_tag, startIndex);
            if (endIndex == -1)
                return string.Empty;

            string value = line.Substring(startIndex, (endIndex - startIndex));
            return value;
        }
    }
}