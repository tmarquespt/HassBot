using HassBotDTOs;
using HassBotUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HassBotData
{
    public class Helper
    {

        private static readonly log4net.ILog logger =
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void DownloadFile(string targetUrl, string localFilePath)
        {
            try
            {
                if (File.Exists(localFilePath))
                    File.Delete(localFilePath);

                // The Home Assistant web site has stopped support for TLS 1.0 - which is used bydefault.
                // Let's force it to use TLS 1.2 - otherwise it will throw the following error:
                // The underlying connection was closed: An unexpected error occurred on a send.
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                WebClient wc = new WebClient();

                wc.DownloadFile(new Uri(targetUrl), localFilePath);
            }
            catch (Exception e)
            {
                logger.Error(string.Format(Constants.ERR_DOWNLOADING_FILE, targetUrl), e);
            }
        }
    }
}