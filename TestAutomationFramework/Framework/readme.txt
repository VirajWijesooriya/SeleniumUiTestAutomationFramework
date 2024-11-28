[TestFixture("Broker")]
    public class ActionSingleUser_ExpectedResult : TestDriver
    {
        public ActionSingleUser_ExpectedResult(string userRole) : base(new List<string> { userRole }) { }
        [Test, Category("AppConditionTests")]
        public void Test_ActionSingleUser_ExpectedResult()
        {
            // Landing Page
            
            // User actions
        }
    }

    public class ActionMultipleUser_ExpectedResult : TestDriver
    {
        public ActionMultipleUser_ExpectedResult() : base(new List<string>() { "Broker", "Credit", "Settlement" }) { }
        [Test, Category("AppConditionTests")]
        public void Test_ActionMultipleUser_ExpectedResult()
        {
            // Get user Credentials
            var brokerUserIndex = testUsers.IndexOf(testUsers.Single(x => x.UserType == "Broker"));
            var creditUserIndex = testUsers.IndexOf(testUsers.Single(x => x.UserType == "Credit"));
            var settlementUserIndex = testUsers.IndexOf(testUsers.Single(x => x.UserType == "Settlement"));

            var brokerUserName = testUsers[brokerUserIndex].UserName;
            var brokerUserPassword = testUsers[brokerUserIndex].UserPwd;

            var creditUserName = testUsers[creditUserIndex].UserName;
            var creditUserPassword = testUsers[creditUserIndex].UserPwd;

            var settlementUserName = testUsers[settlementUserIndex].UserName;
            var settlementUserPassword = testUsers[settlementUserIndex].UserPwd;

            // Broker Login
            LoginPage loginPage = new LoginPage(testBrowser);
            loginPage.Login(brokerUserName, brokerUserPassword);

            // Broker logs out and Credit user Logs in
            BrokerLandingPage brokerLandingPage = new BrokerLandingPage(testBrowser);
            brokerLandingPage.UserLogout();
            loginPage.WaitForLoginPage();
            loginPage.Login(creditUserName, creditUserPassword);
        }
    }

    [TestFixture(null)]
    public class ActionNoUser_ExpectedResult : TestDriver
    {
        public ActionNoUser_ExpectedResult(string userRole) : base(new List<string> { userRole }) { }
        [Test, Category("AppConditionTests")]
        public void Test_ActionNoUser_ExpectedResult()
        {
            LoanDataHelper loanDataHelper = new LoanDataHelper();
            var envLoans = loanDataHelper.GetLoanDataForEnvironment(testBrowser.environementName);
        }
    }

    [TestFixture("Settlement")]
    [TestFixture("SettlementAdmin")]
    [TestFixture("Admin")]
    public class ActionSameTestMultipleUsers_ExpectedResult : TestDriver
    {
        public ActionSameTestMultipleUsers_ExpectedResult(string userRole) : base(new List<string> { userRole }) { }
        [Test, Category("PPSR_Tests")]
        public void Test_ActionSameTestMultipleUsers_ExpectedResult()
        {
            if (testUser.UserType == "SettlementAdmin" || testUser.UserType == "Admin" || )
            {
                // Actions for these users
            }
            else if (testUser.UserType == "Settlement")
            {
                // Actions for other users
            }

        }
    }