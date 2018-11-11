using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotLib
{
    public sealed class Constants
    {

        /// <summary>
        /// Usage Specific Messages
        /// </summary>
        public static readonly string USAGE_TITLE =
                "Usage";
        public static readonly string USAGE_AFK =
                "`~afk <message>` or `~away <message>` or `~seen <username>`";
        public static readonly string USAGE_SEEN =
                "`~seen <username>`";
        public static readonly string USAGE_EIGHTBALL =
                "~8ball <your question>";
        public static readonly string USAGE_AS =
                "~as @user #channel <your message>";
        public static readonly string USAGE_BASE64 =
                "~base64_encode <string to encode> or ~base64_decode <string to decode>";
        public static readonly string USAGE_BREAKINGCHANGES =
                "Example: `~breaking_changes 82` gives you breaking changes for release 82.0 and it's minor releases";
        public static readonly string USAGE_COMMAND =
                "~command <name> <description>";
        public static readonly string USAGE_C2F =
                "c2f <numeric value of temperature in celsius>";
        public static readonly string USAGE_F2C =
                "f2c <numeric value of temperature in fahrenheit>";
        public static readonly string USAGE_HEX2DEC =
                "hex2dec <decimal value>";
        public static readonly string USAGE_DEC2HEX =
                "dec2hex <hex value>";
        public static readonly string USAGE_BIN2DEC =
                "bin2dec <binary value>";
        public static readonly string USAGE_DEC2BIN =
                "dec2bin <decimal value>";
        public static readonly string USAGE_LOOKUP =
                "~lookup <keyword> <@ optional user>";
        public static readonly string USAGE_DEEPSEARCH =
                "~deepsearch <keyword>";
        public static readonly string USAGE_LMGTFY =
                "~lmgtfy <google search string>";
        public static readonly string USAGE_YAML2JSON =
                "~yaml2json <yaml code>";
        public static readonly string USAGE_JSON2YAML =
                "~json2yaml <json code>";
        public static readonly string USAGE_SUBSCRIBE =
                "`~subscribe <tag>` to subscribe\n`~subscribe list` to see all subscribed tags.";
        public static readonly string USAGE_UNSUBSCRIBE =
                "`~unsubscribe <tag>` to unsubscribe\n`~unsubscribe all` to clear everything!";
        public static readonly string USAGE_YAML =
                "Try the following:\n~yaml? \\`\\`\\`yaml\ncode\n\\`\\`\\`";

        /// <summary>
        /// Command Specific Messages
        /// </summary>
        public static readonly string SEEN_MESSAGE_FORMAT =
                "**{0} is away** for {1} with a message :point_right: {2}";
        public static readonly string AWAY_MESSAGE_FORMAT =
                "{0} is away! {1} :wave:";
        public static readonly string ACCESS_DENIED =
                "Access Denied";
        public static readonly string ACCESS_DENIED_MESSAGE =
                "You do not have permissions to run this command. Please contact @skalavala to get access.";
        public static readonly string COMMAND_TOTAL =
                "There are `{0}` custom command(s) available. ";
        public static readonly string CODESHARE_URL =
                "https://codeshare.io/new";
        public static readonly string CODESHARE_MESSAGE =
                "Click on the link {0} and paste your code there. It makes it easy to collaborate and make changes in real-time.";
        public static readonly string SUCCESS =
                "Success!";
        public static readonly string COMMAND_SUCCESS_MESSAGE =
                "Go ahead and run the command using `~{0}`";
        public static readonly string TITLE_SUBSCRIBE =
                "Subscribe";
        public static readonly string TITLE_UNSUBSCRIBE =
                "Unsubscribe";
        public static readonly string ERROR_NO_SUBSCRIPTIONS =
                "You do not have any subscriptions currently.";
        public static readonly string INFO_TAG_EXISTS =
                "The tag '{0}' is already in your subscription list.";
        public static readonly string INFO_SUBSCRIPTION_SUCCESS =
                "Subscribed to tag '{0}' successfully.";
        public static readonly string INFO_CURRENT_SUBSCRIPTIONS =
                "Your current subscriptions are: {0}";
        public static readonly string INFO_UNSUBSCRIBE_ALL_SUCCESS =
                "Successfully unsubscribed to all tags.";
        public static readonly string INFO_NOT_SUBSCRIBED =
                "You have not subscribed to '{0}'.";
        public static readonly string INFO_UNSUBSCRIBE_SUCCESS =
                "Successfully unsubscribed to '{0}'.";
        public static readonly string GOOD_YAML =
                "That is a perfectly valid YAML!";
        public static readonly string INVALID_YAML =
                "Invalid YAML! Error: {1}";
        public static readonly string WELCOME_TITLE =
                "Welcome";
        public static readonly string WELCOME_MESSAGE =
                "Welcome to {0} Discord Channel!";
        public static readonly string WELCOME_RULES_MESSAGE =
                "Please read {0} \n";
        public static readonly string CODE_SHARING_MESSAGE =
                "For sharing code, please use <https://www.hastebin.com>\nIf it is less than 10 lines of code, **make sure** it is formatted using below format:\n\\`\\`\\`yaml\ncode\n\\`\\`\\`\n";
        public static readonly string SHARE_MESSAGE =
                "Please use https://www.hastebin.com/ or https://paste.ubuntu.com/ to share code.";
        public static readonly string FORMAT_CODE =
                "Format Code";
        public static readonly string UPDATE_SUCCESSFUL =
                "Refreshed lookup data successfully!";
        public static readonly string UPDATE_FAILED =
                "Failed to refresh lookup data! contact @skalavala at https://www.github.com/skalavala";
        public static readonly string LET_ME_GOOGLE =
                "Let me Google that for you...";

        /// <summary>
        /// Emojis
        /// </summary>
        public static readonly string EMOJI_INFORMATION =
                ":information_source:";
        public static readonly string EMOJI_STOPSIGN =
                ":octagonal_sign:";
        public static readonly string EMOJI_THUMBSUP =
                ":thumbsup:";
        public static readonly string EMOJI_THUMBSDOWN =
                ":thumbsdown:";
        public static readonly string EMOJI_THERMOMETER =
                ":thermometer:";
        public static readonly string EMOJI_NAMASTE =
                ":pray:";
        public static readonly string EMOJI_POINT_UP =
                ":point_up:";
        public static readonly string EMOJI_POINT_DOWN =
                ":point_down:";
        public static readonly string EMOJI_FAIL =
                ":cold_sweat:";
        public static readonly string EMOJI_GO =
                ":checkered_flag:";
        public static readonly string EMOJI_PING_PONG =
                ":ping_pong:";
    }
}