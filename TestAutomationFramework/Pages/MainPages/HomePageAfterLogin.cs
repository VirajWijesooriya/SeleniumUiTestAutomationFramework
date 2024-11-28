using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationFramework.Framework.TestInit;

namespace TestAutomationFramework.Pages.MainPages
{
    internal class HomePageAfterLogin : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = ".logged-in")]
        private IList<IWebElement> txtWelcomeBanner;

        public HomePageAfterLogin(TestBrowser browser)
        {
            Browser = browser;
            PageFactory.InitElements(browser.driver, this);
        }


        internal bool IsWelcomeBannerVisible()
        {
            return IsElementActive(txtWelcomeBanner.FirstOrDefault());
        }
    }
}
