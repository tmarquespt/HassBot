using HassBotDTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HassBotUtils;
namespace HassBotData
{
    public class Persistence
    {
        public static List<CommandDTO> LoadCommands()
        {
            string remoteurl = AppSettingsUtil.AppSettingsString("CommandsUrl", true, string.Empty);
            string localPath = AppSettingsUtil.AppSettingsString("CommandsLocalPath", true, string.Empty);

            Helper.DownloadFile(remoteurl, localPath);

            if (!File.Exists(localPath))
                return null;

            string json = File.ReadAllText(localPath);
            return JsonConvert.DeserializeObject<List<CommandDTO>>(json);
        }

        public static List<BlockedDomainDTO> LoadBlockedDomains()
        {
            string remoteurl = AppSettingsUtil.AppSettingsString("BlockedDomainsUrl", true, string.Empty);
            string localPath = AppSettingsUtil.AppSettingsString("BlockedDomainsLocalPath", true, string.Empty);

            Helper.DownloadFile(remoteurl, localPath);

            if (!File.Exists(localPath))
                return null;
            string json = File.ReadAllText(localPath);
            return JsonConvert.DeserializeObject<List<BlockedDomainDTO>>(json);
        }

        public static List<AFKDTO> LoadAFKUsers(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<AFKDTO>>(json);
        }

        public static void SaveAFKUsers(List<AFKDTO> afkUsers, string filePath)
        {
            string json = JsonConvert.SerializeObject(afkUsers, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static List<SubscribeDTO> LoadSubscriptions(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<SubscribeDTO>>(json);
        }

        public static void SaveSubscriptions(List<SubscribeDTO> subscriptions, string filePath)
        {
            string json = JsonConvert.SerializeObject(subscriptions, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static List<ViolatorDTO> LoadViolations(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<ViolatorDTO>>(json);
        }

        public static void SaveViolations(List<ViolatorDTO> violations, string filePath)
        {
            string json = JsonConvert.SerializeObject(violations, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}