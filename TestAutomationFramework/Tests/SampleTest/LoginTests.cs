using TestAutomationFramework.Framework.TestInit;
using NUnit.Framework;
using System.Collections.Generic;
using TestAutomationFramework.Pages.MainPages;

namespace TestAutomationFramework.Tests.LoginTests
{
    [TestFixture("staff")]
    public class UserTryLogin_UserCanLoginSuccessfully: TestDriver
    {
        public UserTryLogin_UserCanLoginSuccessfully(string userRole) : base(new List<string> { userRole }) { }
        [Test, Category("SampleTest")]
        public void LoginTest_UserTryLogin_UserCanLoginSuccessfully()
        {
            // Landing Page
            LandingPage landingPage = new LandingPage(testBrowser);
            landingPage.ClickOnSignIn();

            // Login with user
            LoginPage loginPage = new LoginPage(testBrowser);
            loginPage.WaitForPageLoad();
            var homePageAfterLogin = loginPage.Login(testUsers[0].UserName, testUsers[0].UserPwd);

            Assert.IsTrue(homePageAfterLogin.IsWelcomeBannerVisible(), "Welcome banner wasn't visible");
        }
    }
}
