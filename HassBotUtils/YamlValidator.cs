using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace HassBotUtils
{
    public sealed class YamlValidator
    {
        private static readonly string YAML_START = @"```yaml";
        private static readonly string YAML_END = @"```";

        public static bool ValidateYaml(string yamlData, out string errorMessage)
        {
            try
            {
                yamlData = yamlData.Replace(YAML_START, string.Empty).Replace(YAML_END, string.Empty);

                // for some reason, the yaml validator fails when you have secrets or `!`
                yamlData = yamlData.Replace("!secret ", string.Empty);
                yamlData = yamlData.Replace("!include ", string.Empty);
                yamlData = yamlData.Replace("!include_dir_list ", string.Empty);
                yamlData = yamlData.Replace("!include_dir_named ", string.Empty);
                yamlData = yamlData.Replace("!include_dir_merge_list ", string.Empty);
                yamlData = yamlData.Replace("!include_dir_merge_named ", string.Empty);

                errorMessage = string.Empty;
                var input = new StringReader(yamlData);
                var deserializer = new Deserializer();
                object o = deserializer.Deserialize(input);
                if (o == null)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return false;
            }
        }
    }
}