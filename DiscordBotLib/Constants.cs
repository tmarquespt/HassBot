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
        public static readonly string TITLE_HASSBOT =
                "HassBot";
        public static readonly string USAGE_TITLE =
                "Usage";
        public static readonly string USAGE_AFK =
                "`~afk <message>` or `~away <message>` or `~seen <username>`";
        public static readonly string USAGE_SEEN =
                "`~seen <username>`";
        public static readonly string USAGE_EIGHTBALL =
                "`~8ball <your question>`";
        public static readonly string USAGE_AS =
                "`~as @user #channel <your message>`";
        public static readonly string USAGE_BASE64 =
                "`~base64_encode <string to encode>` or `~base64_decode <string to decode>`";
        public static readonly string USAGE_BREAKINGCHANGES =
                "`~breaking_changes 82` gives you breaking changes for release 82.0 and it's minor releases";
        public static readonly string USAGE_COMMAND =
                "`~command add` or `~command refresh` to refresh commands";
        public static readonly string USAGE_C2F =
                "`~c2f <numeric value of temperature in celsius>`";
        public static readonly string USAGE_F2C =
                "`~f2c <numeric value of temperature in fahrenheit>`";
        public static readonly string USAGE_VIOLATION =
                "`~violation pardon @username` to pardon a user's violations.\n`~violation add @user <description of violation>` to add a new violation";
        public static readonly string USAGE_HEX2DEC =
                "`~hex2dec <decimal value>`";
        public static readonly string USAGE_DEC2HEX =
                "`~dec2hex <hex value>`";
        public static readonly string USAGE_BIN2DEC =
                "`~bin2dec <binary value>`";
        public static readonly string USAGE_DEC2BIN =
                "`~dec2bin <decimal value>`";
        public static readonly string USAGE_LOOKUP =
                "`~lookup <keyword> <@ optional user>`";
        public static readonly string USAGE_DEEPSEARCH =
                "`~deepsearch <keyword>`";
        public static readonly string USAGE_LMGTFY =
                "`~lmgtfy <google search string>`";
        public static readonly string USAGE_YAML2JSON =
                "`~yaml2json <yaml code>`";
        public static readonly string USAGE_JSON2YAML =
                "`~json2yaml <json code>`";
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
        public static readonly string SUCCESS =
                "Successo!";
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

        public static readonly string MAXLINELIMITMESSAGE =
            "Attention!: Please use <https://www.hastebin.com> to share code that is more than 10-15 lines. You have been warned, {0}!;";

        public static readonly string COMMAND_REFRESH_SUCCESSFUL =
                "Commands, Sitemap and Blocked Domains are reloaded and ready to go!";
        public static readonly string COMMAND_REFRESH_FAILED =
                "Failed to refresh lookup data! contact @skalavala at https://www.github.com/skalavala";

        public static readonly string COMMAND_MESSAGE =
                "Manage Hassbot data online at <https://github.com/awesome-automations/hassbot-data>. Make sure you run `~command refresh` after updating data online.";
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
        public static readonly string FORMAT_CODE =
                "Format Code";
        public static readonly string LET_ME_GOOGLE =
                "Let me Google that for you...";
        public static readonly string BAN_MESSAGE =
                "User **{0}** got permanently banned for posting {1}.";
        public static readonly string ERROR_BLOCKED_URL =
                "{0} Your message has been deleted as it contains a link or a domain name '{1}' that is on the blocked list because of: '**{2}**'.\nPlease re-post by removing/changing the domain name/link. Your original message has been DM'ed to you.";
        public static readonly string USER_MESSAGE_BLOCKED_URL =
                "Here is your original message you posted earlier that was blocked. You can re-post by removing/changing the domain name/link: {0}\nReason for deletion: **{1}**\nYour message: {2}";

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
