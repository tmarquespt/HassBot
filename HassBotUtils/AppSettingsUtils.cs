using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotUtils
{
    public sealed class AppSettingsUtil
    {
        private static readonly string ELEMENT_NOT_FOUND =
            "ASU001: Element '{0}' not found, or contains invalid data in the configuration file under AppSettings section.";

        private AppSettingsUtil()
        {
            // .ctor
        }

        public static string AppSettingsString(string name, bool required, string defaultValue)
        {
            ArgumentValidation.CheckForEmptyString(name, "config item name");

            string configValue = ConfigurationManager.AppSettings[name];
            configValue = (null == configValue ? string.Empty : configValue.Trim());

            if (string.Empty == configValue)
            {
                if (!required) return defaultValue;
                throw new Exception(SafeFormatter.Format(ELEMENT_NOT_FOUND, name));
            }
            else
            {
                return configValue;
            }
        }

        public static int AppSettingsInt(string name, bool required, int defaultVal)
        {

            string configValue = AppSettingsString(name, required, defaultVal.ToString());
            int retVal = defaultVal;
            try
            {
                retVal = int.Parse(configValue);
            }
            catch (Exception e)
            {
                if (!required)
                    retVal = defaultVal;
                else
                    throw new Exception(SafeFormatter.Format(ELEMENT_NOT_FOUND, name), e);
            }
            return retVal;
        }

        public static long AppSettingsLong(string name, bool required, long defaultVal)
        {

            string configValue = AppSettingsString(name, required, defaultVal.ToString());
            long retVal = defaultVal;
            try
            {
                retVal = long.Parse(configValue);
            }
            catch (Exception e)
            {
                if (!required)
                    retVal = defaultVal;
                else
                    throw new Exception(SafeFormatter.Format(ELEMENT_NOT_FOUND, name), e);
            }
            return retVal;
        }

        public static bool AppSettingsBool(string name, bool required, bool defaultVal)
        {

            string configValue = AppSettingsString(name, required, defaultVal.ToString());
            bool retVal = defaultVal;
            try
            {
                retVal = bool.Parse(configValue);
            }
            catch (Exception e)
            {
                if (!required)
                    retVal = defaultVal;
                else
                    throw new Exception(SafeFormatter.Format(ELEMENT_NOT_FOUND, name), e);
            }
            return retVal;
        }

        public static double AppSettingsDouble(string name, bool required, double defaultVal)
        {

            string configValue = AppSettingsString(name, required, defaultVal.ToString());
            double retVal = defaultVal;
            try
            {
                retVal = double.Parse(configValue);
            }
            catch (Exception e)
            {
                if (!required)
                    retVal = defaultVal;
                else
                    throw new Exception(SafeFormatter.Format(ELEMENT_NOT_FOUND, name), e);
            }
            return retVal;
        }
    }
}