using TestAutomationFramework.Pages.MainPages;
using TestAutomationFramework.Util.FileHelpers;
using TestAutomationFramework.Util.UrlHelpers;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TestAutomationFramework.Util.Enums.Enums;

namespace TestAutomationFramework.Framework.TestInit
{
    public class TestDriver : IDisposable
    {
        protected TestBrowser testBrowser;
        protected TestUser testUser;
        protected IList<TestUser> testUsers;
        protected string loginUrl;
        protected string apiToken;

        protected TestDriver(List<string> userRoles, string targetEnv = "test")
        {
            testBrowser = new TestBrowser();
            testUsers = new List<TestUser>();

            // Read the environement details file and find the required environment in file data
            JObject envDetails = JsonFileHelper.GetFileData("Environments.json");
            JArray environments = (JArray)envDetails["environments"];
            JObject targetEnvironmentData = JsonFileHelper.GetEntityFromFileData(environments, "environmentName", targetEnv);

            if (targetEnvironmentData != null)
            {
                testBrowser.environementName = (string)targetEnvironmentData["environmentName"];
                testBrowser.BaseUrl = (string)targetEnvironmentData["baseUrl"];
                testBrowser.LoginPageUrl = (string)targetEnvironmentData["loginPageUrl"];

                // Connection Strings
                testBrowser.ConnectionString = (string)targetEnvironmentData["connectionString"];

                // API Auth Details
                testBrowser.ApiAuthUserName = (string)targetEnvironmentData["apiAuthUserName"];
                testBrowser.ApiAuthPassword = (string)targetEnvironmentData["apiAuthPassword"];
                testBrowser.ApiAuthClientApplicationClientId = (string)targetEnvironmentData["apiAuthClientApplicationClientId"];
                testBrowser.ApiAuthClientApplicationSecret = (string)targetEnvironmentData["apiAuthClientApplicationSecret"];

                loginUrl = UrlHelper.GetLoginUrl(testBrowser);
                apiToken = GetApiAuthenticationToken();
            }
            else
            {
                throw new ApplicationException($"Couldn't find target environment {targetEnv} in the Environments.json file");
            }

            // Read the user details file and find the required user in file data
            JObject userDetails = JsonFileHelper.GetFileData("Users.json");
            JArray users = (JArray)userDetails["Users"];

            if (userRoles[0] != null)
            {
                for (int i = 0; i < userRoles.Count; i++)
                {
                    testUsers.Add(new TestUser());
                }

                for (int i = 0; i < userRoles.Count; i++)
                {
                    JObject user = JsonFileHelper.GetEntityFromFileData(users, "userRole", userRoles[i]);

                    if (user != null)
                    {
                        testUsers[i].UserType = (string)user["userRole"];
                        testUsers[i].UserName = (string)user["userId"];
                        testUsers[i].UserPwd = (string)user["password"];
                    }
                    else
                    {
                        throw new ApplicationException($"Couldn't find the given user {targetEnv} in the Users.json file");
                    }
                }

                for (int i = 0; i < 15; i++)
                {
                    try
                    {
                        StartBrowserAndVisitBaseUrl();
                        if (testBrowser.driver.Url.Contains(testBrowser.BaseUrl))
                        {
                            break;
                        }
                    }
                    catch (WebDriverTimeoutException)
                    {
                        testBrowser.driver.Close();
                        testBrowser.driver.Quit();
                    }
                }

                WebDriverWait wait = new WebDriverWait(testBrowser.driver, TimeSpan.FromSeconds(50));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(loginUrl));
            }
        }

        public string GetApiAuthenticationToken()
        {

            var accessToken = string.Empty;
            string apiTokenUrl = "api/oauth/token";
            string authenticationUrl = $"{this.testBrowser.BaseUrl}{apiTokenUrl}";

            var urlParams = new Dictionary<string, string>()
            {
                {"grant_type", "password"},
                {"username", testBrowser.ApiAuthUserName},
                {"password", testBrowser.ApiAuthPassword },
                {"client_id", testBrowser.ApiAuthClientApplicationClientId},
                {"client_secret", testBrowser.ApiAuthClientApplicationSecret}
            };

            HttpContent httpUrlContent = new FormUrlEncodedContent(urlParams.ToList());

            using (var tokenClient = new HttpClient())
            {
                try
                {
                    var response = tokenClient.PostAsync(authenticationUrl, httpUrlContent);
                    var tokenResult = response.Result.Content.ReadAsStringAsync().Result;

                    if (response.Result.IsSuccessStatusCode)
                    {
                        JObject resultObject = JObject.Parse(tokenResult);

                        if (resultObject != null && resultObject.HasValues)
                        {
                            var tokenData = resultObject.SelectToken("access_token");

                            if (tokenData != null)
                                accessToken = tokenData.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    accessToken = string.Empty;
                }
            }

            return accessToken;
        }

        [TearDown]
        public void TearDown()
        {
            // Take a screenshot upon test failure
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed && testBrowser.driver != null )
            {
                Screenshot screenShot = ((ITakesScreenshot)testBrowser.driver).GetScreenshot();

                var imageName = $"{TestContext.CurrentContext.Test.MethodName}_{Convert.ToString(DateTime.Now.Ticks)}.png";
                var folderName = "ScreenShots";

                WindowsExplorerHelper.CreateFolder(folderName);
                var currentPath = $"{Path.GetDirectoryName(WindowsExplorerHelper.GetExecutionFolderPath())}\\{folderName}\\{imageName}";

                screenShot.SaveAsFile(currentPath, ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(currentPath, TestContext.CurrentContext.Test.FullName);
            }
        }

        private void StartBrowserAndVisitBaseUrl()
        {
            testBrowser.Initialise(BrowserVersion.Chrome);

            // Setting the Implecit wait
            testBrowser.driver.Manage().Window.Maximize();
            testBrowser.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            testBrowser.driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(50);
            Thread.Sleep(1000);

            // Open the site
            testBrowser.driver.Navigate().GoToUrl(loginUrl);
            Thread.Sleep(200);
        }

        // Browser instance disposal
        public void Dispose()
        {
            if (testBrowser.driver != null)
            {
                testBrowser.driver.Close();
                testBrowser.driver.Quit();
                GC.SuppressFinalize(this);
            }
        }  
    }
}
