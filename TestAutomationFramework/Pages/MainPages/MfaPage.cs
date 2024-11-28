using TestAutomationFramework.Framework.TestInit;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace TestAutomationFramework.Pages.MainPages
{
    internal class MfaPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "EnteredToken")]
        private IWebElement txtMfaToken;

        [FindsBy(How = How.Name, Using = "rememberMe")]
        private IWebElement chkRememberme;

        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        private IWebElement btnVerify;

        [FindsBy(How = How.Id, Using = "resendCode")]
        private IWebElement lnkResendcode;


        public MfaPage(TestBrowser browser)
        {
            Browser = browser;
            PageFactory.InitElements(browser.driver, this);
        }

        public void WaitForMfaPage()
        {
            WaitForElementToClickable_Short(txtMfaToken);
        }

        public Dictionary<string, bool> GetElementsVisibility_MfaPage()
        {
            Dictionary<string, bool> dict = new Dictionary<string, bool>();

            dict.Add("Textbox MFA code", IsElementPresentShort(txtMfaToken));
            dict.Add("Check box remember me", IsElementPresentShort(chkRememberme));
            dict.Add("Button verify", IsElementPresentShort(btnVerify));
            dict.Add("Link Resend code", IsElementPresentShort(lnkResendcode));

            return dict;
        }
    }
}
