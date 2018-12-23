using HassBotDTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotData
{
    public class Persistence
    {

        public static List<CommandDTO> LoadCommands(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<CommandDTO>>(json);
        }

        public static void SaveCommands(List<CommandDTO> commands, string filePath)
        {
            string json = JsonConvert.SerializeObject(commands, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static List<AFKDTO> LoadAFKUsers(string filePath)
        {
            if (!File.Exists(filePath)) return null;
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
            if (!File.Exists(filePath)) return null;
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
            if (!File.Exists(filePath)) return null;
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