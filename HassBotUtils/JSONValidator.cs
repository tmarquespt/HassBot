using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HassBotUtils
{
    public sealed class JSONValidator
    {
        private static readonly string JSON_START = @"```json";
        private static readonly string JSON_END = @"```";
        public static bool ValidateJson(string stringValue, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return false;
            }

            var value = stringValue.Replace(JSON_START, string.Empty).Replace(JSON_END, string.Empty).Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) || // For object
                (value.StartsWith("[") && value.EndsWith("]")))   // For array
            {
                try
                {
                    var obj = JToken.Parse(value);
                    return true;
                }
                catch (JsonReaderException e)
                {
                    errorMessage = e.ToString();
                    return false;
                }
            }
            return false;
        }
    }
}
