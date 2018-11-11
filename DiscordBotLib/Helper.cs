using Discord;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using HassBotUtils;
using HassBotDTOs;
using HassBotData;

using Discord.WebSocket;
using Discord.Commands;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DiscordBotLib
{
    public class Helper
    {

        private static readonly string YAML_START = @"```yaml";
        private static readonly string YAML_END = @"```";
        private static readonly string JSON_START = @"```json";
        private static readonly string JSON_END = @"```";
        private static readonly string GOOD_EMOJI = "✅";
        private static readonly string BAD_EMOJI = "❌";

        private static readonly log4net.ILog logger =
                    log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            Color[] colors = { Color.Blue, Color.DarkBlue, Color.DarkerGrey,
                               Color.DarkGreen, Color.DarkGrey, Color.DarkMagenta,
                               Color.DarkOrange, Color.DarkPurple, Color.DarkRed,
                               Color.DarkTeal, Color.Gold, Color.Green,
                               Color.LighterGrey, Color.LightGrey,
                               Color.LightOrange, Color.Magenta, Color.Orange,
                               Color.Purple, Color.Red, Color.Teal };

            int r = rnd.Next(colors.Count());
            return colors[r];
        }

        public static Task LogMessage(LogMessage message)
        {
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                    logger.Fatal(message.Message);
                    break;
                case LogSeverity.Error:
                    logger.Error(message.Message);
                    break;
                case LogSeverity.Warning:
                    logger.Warn(message.Message);
                    break;
                case LogSeverity.Info:
                    logger.Info(message.Message);
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    logger.Debug(message.Message);
                    break;
            }
            return Task.FromResult(0);
        }

        public static async Task HandleHoundCIMessages(SocketUserMessage message,
                                                       SocketCommandContext context,
                                                       SocketGuildChannel channel)
        {

            if (null == channel || channel.Name != "github" || message.Embeds.Count <= 0)
                return;

            bool purgeHoundBotMsgs = AppSettingsUtil.AppSettingsBool("deleteHoundBotMsgs",
                                                                     false, false);
            if (!purgeHoundBotMsgs)
                return;

            // #github channel contains messages from many different sources. 
            // check if the sender is 'houndci-bot' before deleting.
            foreach (Embed e in message.Embeds)
            {
                EmbedAuthor author = (EmbedAuthor)e.Author;
                if (author.ToString() == "houndci-bot")
                {
                    //logger.InfoFormat("Deleting the houndci-bot message: {0} => {1}: {2}",
                    //                   e.Url, e.Title, e.Description);
                    await context.Message.DeleteAsync();
                }
            }
        }

        public static async Task ProcessSubscriptions(SocketUserMessage message,
                                               SocketCommandContext context,
                                               SocketGuildChannel channel)
        {

            // only interested in processing incoming messages to the #github channel
            if (null == channel || channel.Name != "github" || message.Embeds.Count <= 0)
                return;

            // #github channel contains messages from many different sources. 
            foreach (Embed e in message.Embeds)
            {
                List<string> uniqueTags = SubscriptionManager.TheSubscriptionManager.GetDistinctTags();
                List<SubscribeDTO> uniqueUsers = new List<SubscribeDTO>(8);
                List<string> matchedTags = new List<string>(32);
                List<string> matchedLocations = new List<string>(32);

                // first get all the unique tags across all users and check if any of the tags match
                // if there is a match, keep a list of those tags
                foreach (string tag in uniqueTags)
                {
                    if (e.Description.ToLower().Contains(tag))
                    {
                        matchedLocations.Add("Description");
                        matchedTags.Add(tag);
                    }
                    if (e.Url.ToLower().Contains(tag))
                    {
                        matchedLocations.Add("Url");
                        matchedTags.Add(tag);
                    }
                    if (e.Author.ToString().ToLower().Contains(tag))
                    {
                        matchedLocations.Add("Author");
                        matchedTags.Add(tag);
                    }
                    if (e.Title.ToLower().Contains(tag))
                    {
                        matchedLocations.Add("Title");
                        matchedTags.Add(tag);
                    }
                }

                // filter out any duplicates
                matchedTags = matchedTags.Distinct().ToList();
                matchedLocations = matchedLocations.Distinct().ToList();

                // if there are matched tags, get unique list of users that are interested in those tags
                if (matchedTags.Count > 0)
                {
                    foreach (string tag in matchedTags)
                    {
                        uniqueUsers.AddRange(SubscriptionManager.TheSubscriptionManager.GetSubscribersByTag(tag));
                    }

                    // get the server guild from config file
                    ulong serverGuild = (ulong)AppSettingsUtil.AppSettingsLong("serverGuild", true, 330944238910963714);
                    var guild = context.Client.GetGuild(serverGuild);
                    if (null == guild)
                        return;

                    // now that we have list of users that are interested in the tag, and the tags match in description/author name/title
                    // send the url of the message to each of the subscriber using a DM
                    foreach (SubscribeDTO usr in uniqueUsers.Distinct().ToList())
                    {
                        SocketUser user = guild.GetUser(usr.Id);
                        var dmChannel = await user.GetOrCreateDMChannelAsync();

                        string msg = string.Format("Subscription Alert: Found '{0}' in the '{1}' of the PR/Issue: {2}",
                                                    GetListAsCommaSeparated(matchedTags),
                                                    GetListAsCommaSeparated(matchedLocations),
                                                    e.Url);

                        await dmChannel.SendMessageAsync(msg);
                    }
                }
            }
        }

        private static string GetListAsCommaSeparated(List<string> items)
        {
            if (items == null || items.Count == 0)
                return "none!";

            StringBuilder buffer = new StringBuilder();

            for (int i = 0; i < items.Count; i++)
            {
                if (i == 0) buffer.Append("[ ");
                buffer.Append(items[i]);
                if (i + 1 == items.Count)
                    buffer.Append(" ]");
                else
                    buffer.Append(", ");
            }

            return buffer.ToString();
        }

        public static async Task ReactToYaml(string content, SocketCommandContext context)
        {
            if (!(content.Contains(YAML_START) || content.Contains(YAML_END)))
                return;

            int start = content.IndexOf(YAML_START);
            int end = content.IndexOf(YAML_END, start + 3);

            if (start == -1 || end == -1 || end == start)
                return;

            string errMsg = string.Empty;
            string substring = content.Substring(start, (end - start));
            bool yamlCheck = YamlValidator.ValidateYaml(substring, out errMsg);
            if (yamlCheck)
            {
                var okEmoji = new Emoji(GOOD_EMOJI);
                await context.Message.AddReactionAsync(okEmoji);
            }
            else
            {
                var errorEmoji = new Emoji(BAD_EMOJI);
                await context.Message.AddReactionAsync(errorEmoji);
            }
        }

        internal static async Task ReactToJson(string content, SocketCommandContext context)
        {
            if (!(content.Contains(JSON_START) || content.Contains(JSON_END)))
                return;

            int start = content.IndexOf(JSON_START);
            int end = content.IndexOf(JSON_END, start + 3);

            if (start == -1 || end == -1 || end == start)
                return;

            string errMsg = string.Empty;
            string substring = content.Substring(start, (end - start));
            bool jsonCheck = JSONValidator.ValidateJson(substring, out errMsg);
            if (jsonCheck)
            {
                var okEmoji = new Emoji(GOOD_EMOJI);
                await context.Message.AddReactionAsync(okEmoji);
            }
            else
            {
                var errorEmoji = new Emoji(BAD_EMOJI);
                await context.Message.AddReactionAsync(errorEmoji);
            }
        }

        public static async Task ChangeNickName(DiscordSocketClient client,
                                                SocketCommandContext context)
        {
            // Change Nick Name 💎
            // Get the Home Assistant Server Guild
            ulong serverGuild = (ulong)AppSettingsUtil.AppSettingsLong("serverGuild", true, 330944238910963714);
            var guild = client.GetGuild(serverGuild);
            if (null == guild)
                return;

            var user = guild.GetUser(context.User.Id);
            if (user.Nickname.Contains("🔹"))
            {
                await user.ModifyAsync(
                    x => {
                        string newNick = user.Nickname.Replace("🔹", string.Empty);
                        x.Nickname = newNick;
                    }
                );
            }
        }

        public static string LookupString(string searchString)
        {
            string[] searchWords = null;
            StringBuilder sb = new StringBuilder();
            XmlDocument doc = Sitemap.SiteMapXmlDocument;

            searchString = searchString.Replace('.', ' ').Replace('_', ' ').Replace('-', ' ').ToLower();
            if (searchString.Contains(" "))
                searchWords = searchString.Split(' ');
            else
                searchWords = new string[] { searchString };

            if (null == searchWords)
                return string.Empty;

            Array.Sort(searchWords);

            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                string location = string.Empty;
                string[] sitemapWords = null;

                string loc = item.FirstChild.InnerText;
                if (loc.EndsWith("/"))
                {
                    int index = loc.LastIndexOf("/", (loc.Length - 2));
                    location = loc.Substring((index) + 1, ((loc.Length - index) - 2));
                }
                else
                {
                    int index = loc.LastIndexOf("/", loc.Length);
                    location = loc.Substring((index) + 1, ((loc.Length - index) - 1));
                }

                location = location.Replace('.', ' ').Replace('_', ' ').Replace('-', ' ').ToLower();
                if (location.Contains(" "))
                    sitemapWords = location.Split(' ');
                else
                    sitemapWords = new string[] { location };

                if (null == sitemapWords)
                    continue;

                Array.Sort(sitemapWords);
                if (string.Join("", searchWords) == string.Join("", sitemapWords))
                {
                    sb.Append(item.FirstChild.InnerText);
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }
    }
}