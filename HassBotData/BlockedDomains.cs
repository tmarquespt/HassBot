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

        public static void ReloadData()
        {
            try
            {
                if (null != _domains)
                    _domains.Clear();

                string remoteurl = AppSettingsUtil.AppSettingsString("hassbotBlockedDomainsUrl", true, string.Empty);
                string localPath = AppSettingsUtil.AppSettingsString("hassbotBlockedDomainsLocalPath", true, string.Empty);

                if (File.Exists(localPath))
                    File.Delete(localPath);

                Helper.DownloadFile(remoteurl, localPath);

                _domains = Persistence.LoadBlockedDomains(localPath);
            }
            catch (Exception e)
            {
                throw new Exception(Constants.ERR_COMMANDS_FILE, e);
            }
        }

        public static BlockedDomains Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public string Lookup(string cmd)
        {
            BlockedDomainDTO retDomain = _domains.Find(delegate (BlockedDomainDTO dto)
            {
                return dto.Url.Trim().ToLower() == cmd.Trim().ToLower();
            });

            if (null != retDomain)
                return retDomain.Url;
            else
                return string.Empty;
        }

        public List<BlockedDomainDTO> Domains()
        {
            if (_domains == null)
                return null;
            return _domains;
        }
    }
}