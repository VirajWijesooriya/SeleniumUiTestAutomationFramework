using TestAutomationFramework.Framework.TestInit;
using OpenQA.Selenium;
using System.Threading;
using SeleniumExtras.PageObjects;
using System;

namespace TestAutomationFramework.Pages.MainPages
{
    class LoginPage : BasePage
    {
        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement txtEmail;

        [FindsBy(How = How.Id, Using = "pass")]
        private IWebElement txtPassword;

        [FindsBy(How = How.Id, Using = "send2")]
        private IWebElement btnLogin;

        [FindsBy(How = How.CssSelector, Using = "div[ng-if='loginErrorMessage']")]
        private IWebElement eleErrorLogin;


        public LoginPage(TestBrowser browser)
        {
            Browser = browser;
            PageFactory.InitElements(browser.driver, this);
        }

        public void WaitForLoginPage()
        {
            WaitForElementToClickable_Short(txtEmail);
            WaitForElementToClickable_Short(txtPassword);
            WaitForElementToClickable_Short(btnLogin);
        }


        public void EnterUserName(string userName)
        {
            WaitForElementToClickable_Short(txtEmail);
            txtEmail.Clear();
            txtEmail.SendKeys(userName);
        }

        public void EnterPassword(string password)
        {
            txtPassword.Clear();
            txtPassword.SendKeys(password);
        }

        public void PressLogin()
        {
            WaitForElementToClickable(btnLogin);
            btnLogin.Click();
        }

        public void Login(string userName, string password)
        {
            EnterUserName(userName);
            EnterPassword(password);

            Thread.Sleep(3000);
            PressLogin();
        }


    }
}
