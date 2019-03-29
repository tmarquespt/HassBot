///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : LookupModule.cs
//  DESCRIPTION     : A class that implements ~lookup command
//                    It uses sitemap data to lookup
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using HassBotData;
using System;
using System.Linq;

namespace DiscordBotLib
{
    public class LookupModule : BaseModule {

        [Command("lookup")]
        public async Task LookupAsync() {
            await base.DisplayUsage(Constants.USAGE_LOOKUP);
        }

        [Command("deepsearch")]
        public async Task DeepSearchAsync() {
            await base.DisplayUsage(Constants.USAGE_DEEPSEARCH);
        }

        [Command("lookup")]
        public async Task LookupAsync([Remainder]string input) {
            await LookupCommand(input);
        }

        private async Task LookupCommand(string input) {
            string result = Helper.SitemapLookup(input);
            result = result.Trim();

            // mention users if any
            string mentionedUsers = base.MentionedUsers();

            var embed = new EmbedBuilder();
            if (result == string.Empty) {
                embed.WithTitle(string.Format("Pesquisada por '{0}': ", input));
                embed.WithColor(Helper.GetRandomColor());
                string msg = string.Format("You may try `~deepsearch {0}`.", input);
                embed.AddInlineField("Não encontrei isso! :frowning:", msg);
            }
            else {
                embed.WithColor(Helper.GetRandomColor());
                embed.AddInlineField("Aqui está o que encontrei: :smile:", mentionedUsers + result);
            }
            await ReplyAsync(string.Empty, false, embed);
        }

        [Command("deepsearch")]
        public async Task DeepSearchAsync([Remainder]string input) {
            XmlDocument doc = Sitemap.SiteMapXmlDocument;

            StringBuilder sb = new StringBuilder();
            foreach (XmlNode item in doc.DocumentElement.ChildNodes) {
                if (item.InnerText.Contains(input.Split(' ')[0])) {
                    sb.Append("<" + item.FirstChild.InnerText + ">\n");
                }
            }

            string result = sb.ToString();

            if (result.Length > 1900) {
                result = result.Substring(0, 1850);
                result += "...\n\nA mensagem foi bloqueada porque é muito longa. Tens que  alterar os critérios de pesquisa.";
            }

            // Send a Direct Message to the User with search information
            var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
            await dmChannel.SendMessageAsync(result);
        }
    }
}
