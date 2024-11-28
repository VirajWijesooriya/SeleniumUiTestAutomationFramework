using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.ApiHelpers
{
    public static class ApiHelper
    {
        public static T DeserializeContent<T>(string ContentStr)
        {
            T content;
            var settings = new JsonSerializerSettings();
            settings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;

            content = JsonConvert.DeserializeObject<T>(ContentStr, settings);
            return content;
        }
    }
}
