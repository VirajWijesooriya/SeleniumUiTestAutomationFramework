using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Util.FileHelpers
{
    class JsonFileHelper
    {
        internal static JObject GetFileData(string fileName)
        {
            try
            {
                var filePath = $@"{Path.GetDirectoryName(WindowsExplorerHelper.GetExecutionFolderPath())}\DataFiles\{fileName}";
                var settings = JObject.Parse(File.ReadAllText(filePath));
                return settings;
            }
            catch (Exception)
            {
                throw new FileNotFoundException($"{fileName} file could not be found in the execution folder.");
            }
        }

        internal static JObject GetEntityFromFileData(JArray fileData, string field, string value)
        {
            JObject _myEntity = null;

            foreach (JObject item in fileData)
            {
                if (string.Equals((string)item[field], value, StringComparison.OrdinalIgnoreCase))
                {
                    _myEntity = item;
                    break;
                }
            }
            return _myEntity;
        }
    }
}
