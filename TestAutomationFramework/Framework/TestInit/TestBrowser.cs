using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static TestAutomationFramework.Util.Enums.Enums;

namespace TestAutomationFramework.Framework.TestInit
{
    public class TestBrowser : IDisposable
    {
        public IWebDriver driver { get; private set; }
        public string environementName { get; set; }
        public string BaseUrl { get; set; }
        public string LoginPageUrl { get; set; }
        public string ConnectionString { get; set; }
        public string ApiAuthUserName { get; set; }
        public string ApiAuthPassword { get; set; }
        public string ApiAuthClientApplicationClientId { get; set; }
        public string ApiAuthClientApplicationSecret { get; set; }

        public void Initialise(BrowserVersion browserVersion)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("start-maximized");
            chromeOptions.AddArguments("enable-automation");
            chromeOptions.AddArguments("--disable-infobars");
            chromeOptions.AddArguments("--disable-dev-shm-usage");
            chromeOptions.AddArguments("--disable-browser-side-navigation");
            chromeOptions.AddArguments("--headless");
            chromeOptions.AddArguments("--disable-gpu");
            chromeOptions.AddArguments("--no-sandbox");
            chromeOptions.AddArguments("--allow-insecure-localhost");
            chromeOptions.AddArguments("--window-size=1920,1080");
          
            switch (browserVersion)
            {
                case BrowserVersion.Chrome:
                    var test = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;
                case BrowserVersion.ChromeHeadless:
                    var test1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
                    break;
                case BrowserVersion.Edge:
                    driver = new EdgeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;
                default:
                    driver = new ChromeDriver();
                    break;
            }
        }

        #region IDisposable Support
        // To detect redundant calls
        private bool _disposedValue = false;


        private void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                driver.Close();
                driver.Quit();
                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
