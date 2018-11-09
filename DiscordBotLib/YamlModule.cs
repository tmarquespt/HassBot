///////////////////////////////////////////////////////////////////////////////
//  AUTHOR          : Suresh Kalavala
//  DATE            : 02/02/2018
//  FILE            : YamlModule.cs
//  DESCRIPTION     : A class that implements ~yaml? command
///////////////////////////////////////////////////////////////////////////////
using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

using HassBotUtils;
namespace DiscordBotLib
{
    public class YamlModule : BaseModule {

        private static readonly string THE_COMMAND = "yaml?";
        private static readonly string YAML_START = @"```yaml";
        private static readonly string YAML_END = @"```";

        [Command("yaml?")]
        public async Task YamlAsync() {
            await base.DisplayUsage(Constants.USAGE_YAML);
        }

        [Command("yaml?")]
        public async Task YamlAsync([Remainder]string cmd) {
            await YamlCommand(cmd);
        }

        private async Task YamlCommand(string cmd) {
            int start = cmd.IndexOf(YAML_START);
            int end = cmd.IndexOf(YAML_END, start + 3);

            if (start == -1 || end == -1 || end == start)
                return;

            string errMsg = string.Empty;
            string substring = cmd.Substring(start, (end - start));

            string errorMessage = string.Empty;
            bool result = YamlValidator.ValidateYaml(substring, out errorMessage);

            // mentioned users
            string mentionedUsers = base.MentionedUsers();
            var embed = new EmbedBuilder();
            if (result == true) {
                embed.WithTitle(Constants.EMOJI_THUMBSUP);
                embed.WithColor(Helper.GetRandomColor());
                embed.AddField(THE_COMMAND, mentionedUsers + Constants.GOOD_YAML);
            }
            else {
                embed.WithTitle(Constants.EMOJI_THUMBSDOWN);
                embed.WithColor(Color.DarkRed);
                embed.AddField(THE_COMMAND, mentionedUsers + 
                                string.Format(Constants.INVALID_YAML, errorMessage));
            }
            await ReplyAsync(string.Empty, false, embed);
        }
    }
}