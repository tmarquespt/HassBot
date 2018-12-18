using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string CommandAuthor { get; set; }
        public DateTime CommandCreatedDate { get; set; }
        public int CommandCount { get; set; }
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
}