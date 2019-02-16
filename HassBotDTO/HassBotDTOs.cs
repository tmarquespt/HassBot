using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HassBotDTOs
{
    public class AFKDTO
    {
        public ulong Id { get; set; }
        public string AwayUser { get; set; }
        public string AwayMessage { get; set; }
        public DateTime AwayTime { get; set; }
    }

    public class AFKDTOComparer : IEqualityComparer<AFKDTO>
    {
        public bool Equals(AFKDTO x, AFKDTO y)
        {
            return (x.AwayUser == y.AwayUser) ? true : false;
        }

        public int GetHashCode(AFKDTO obj)
        {
            return base.GetHashCode();
        }
    }

    public class CommandDTO
    {
        public string CommandName { get; set; }
        public string CommandData { get; set; }
    }

    public class BlockedDomainDTO
    {
        public string Url { get; set; }
        public string Reason { get; set; }
        public bool Ban { get; set; }
    }

    public class CommandDTOComparer : IEqualityComparer<CommandDTO>
    {
        public bool Equals(CommandDTO x, CommandDTO y)
        {
            return (x.CommandName == y.CommandName) ? true : false;
        }

        public int GetHashCode(CommandDTO obj)
        {
            return base.GetHashCode();
        }
    }

    public class SubscribeDTO
    {
        public ulong Id { get; set; }
        public string User { get; set; }
        public List<string> Tags { get; set; }
    }

    public class SubscribeDTOComparer : IEqualityComparer<SubscribeDTO>
    {
        public bool Equals(SubscribeDTO x, SubscribeDTO y)
        {
            return (x.Id == y.Id) ? true : false;
        }

        public int GetHashCode(SubscribeDTO obj)
        {
            return base.GetHashCode();
        }
    }

    public enum HassioRelease
    {
        Beta,
        Stable
    }

    public class HassIOVersion
    {
        public string HomeAssistant { get; set; }
        public string Supervisor { get; set; }
        public string HassOS { get; set; }
    }

    public class HomeAssistantVersion
    {
        public string Stable { get; set; }
        public string Beta { get; set; }
    }

    public enum CommonViolationTypes
    {
        Codewall,
        Troll,
        Spam,
        Custom
    }

    public class Violation
    {
        [JsonProperty(PropertyName = "Description")]
        public string ViolationDescription { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public DateTime ViolationDateTime { get; set; }

        [JsonProperty(PropertyName = "Channel")]
        public string ViolationChannel { get; set; }

        [JsonProperty(PropertyName = "ExtraData")]
        public string ViolationExtraData { get; set; }
    }

    public class ViolatorDTO
    {
        private List<Violation> _violations = new List<Violation>(10);

        [JsonProperty(PropertyName = "ID")]
        public ulong ViolatorId { get; set; }

        [JsonProperty(PropertyName = "User")]
        public string ViolatorName { get; set; }

        [JsonProperty(PropertyName = "Violations")]
        public List<Violation> Violations {
            get
            {
                return _violations;
            }
            set
            {
                _violations = value;
            }
        }
    }
}