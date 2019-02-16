using HassBotDTOs;
using HassBotUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HassBotData
{
    public class BlockedDomains
    {
        private static readonly Lazy<BlockedDomains> lazy = new Lazy<BlockedDomains>(() => new BlockedDomains());
        private static List<BlockedDomainDTO> _domains = null;
        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private BlockedDomains()
        {
            // private .ctor
        }

        static BlockedDomains()
        {
            ReloadData();
        }

        public static BlockedDomains Instance
        {
            get { return lazy.Value; }
        }

        public static void ReloadData()
        {
            try
            {
                if (null != _domains)
                    _domains.Clear();

                _domains = Persistence.LoadBlockedDomains();
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ERR_BLOCKED_DOMAINS_FILE, e);
            }
        }

        public List<BlockedDomainDTO> Domains()
        {
            if (_domains == null)
                return null;
            return _domains;
        }
    }
}