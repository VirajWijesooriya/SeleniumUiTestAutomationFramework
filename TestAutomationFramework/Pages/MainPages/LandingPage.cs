using TestAutomationFramework.Framework.TestInit;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAutomationFramework.Pages.MainPages
{
    internal class LandingPage : BasePage
    {
        [FindsBy(How = How.CssSelector, Using = "a[href^='https://magento.softwaretestingboard.com/customer/account/login']")]
        private IList<IWebElement> btnSignIn;



        public LandingPage(TestBrowser browser)
        {
            Browser = browser;
            PageFactory.InitElements(browser.driver, this);
        }

        internal void WaitForLandingPageLoad()
        {
            WaitForElementLocatedByToAppear(By.CssSelector(".header.content"));
        }

        internal void ClickOnSignIn()
        {
            WaitForElementToClickable_Short(btnSignIn.FirstOrDefault());
            btnSignIn.FirstOrDefault().Click(); 
        }
    }
}
