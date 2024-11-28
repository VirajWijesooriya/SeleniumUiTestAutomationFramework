using TestAutomationFramework.Framework;
using TestAutomationFramework.Framework.TestInit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Util.UrlHelpers
{
    class UrlHelper
    {
        public static string GetLoginUrl(TestBrowser testBrowser)
        {
            return $"{testBrowser.BaseUrl}{testBrowser.LoginPageUrl}";
        }
    }
}
