using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotData
{
    public sealed class Constants
    {
        public static readonly string ERR_AFK_FILE =
               "Failed to load away.json file";
        public static readonly string ERR_COMMANDS_FILE =
                "Failed to load commands.json file";
        public static readonly string ERR_SUBSCRIPTION_FILE =
                "Failed to load subscriptions.json file";
        public static readonly string ERR_VIOLATIONS_FILE =
                "Failed to load violations.json file";
        public static readonly string ERR_DOWNLOADING_FILE =
                "Error downloading {0} file.";
        public static readonly string ERR_BLOCKED_DOMAINS_FILE =
                "Failed to load blocked_domains.json file";
    }
}