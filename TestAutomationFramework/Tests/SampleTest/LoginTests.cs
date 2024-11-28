using TestAutomationFramework.Framework.TestInit;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestAutomationFramework.Tests.LoginTests
{
    [TestFixture("staff")]
    public class UserTryLogin_UserCanLoginSuccessfully: TestDriver
    {
        public UserTryLogin_UserCanLoginSuccessfully(string userRole) : base(new List<string> { userRole }) { }
        [Test, Category("SampleTest")]
        public void LoginTest_UserTryLogin_UserCanLoginSuccessfully()
        {

        
        }
    }
}
