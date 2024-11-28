using TestAutomationFramework.Framework.TestInit;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestAutomationFramework.Pages.MainPages
{
    public class BasePage
    {
        protected TestBrowser Browser;

        public void WaitForPageLoad()
        {
            var wait = new WebDriverWait(Browser.driver, TimeSpan.FromMilliseconds(1000));
            wait.Until(e => ((IJavaScriptExecutor)e).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public string GetPageUrl()
        {
            return Browser.driver.Url;
        }

        public bool IsElementPresent(IWebElement element)
        {
            bool isFound;
            try
            {
                WaitForElementToClickable_VeryLong(element);
                isFound = element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                isFound = false;
            }
            catch (NoSuchElementException)
            {
                isFound = false;
            }

            return isFound;
        }

        public bool IsElementPresentShort(IWebElement element)
        {
            bool isFound;
            try
            {
                WaitForElementToClickable_Short(element);
                isFound = element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                isFound = false;
            }
            catch (NoSuchElementException)
            {
                isFound = false;
            }

            return isFound;
        }

        public bool IsElementVisibleShort(IWebElement element)
        {
            bool isFound;
            try
            {
                WaitForElementsToBeVisible_Short(new List<IWebElement>() { element });
                isFound = element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                isFound = false;
            }
            catch (NoSuchElementException)
            {
                isFound = false;
            }

            return isFound;
        }

        public void SelectDropDownElement(IWebElement element, string optionText)
        {
            try
            {
                var select = new SelectElement(element);
                select.SelectByText(optionText);
            }
            catch (NullReferenceException)
            {
                throw new Exception("Option string is null or empty!!");
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                throw new Exception("Given option string is not available for the given element");
            }
        }

        public bool IsRadioButtonSelected(IWebElement element)
        {
            try
            {
                return element.Selected;
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return false;
            }
        }

        public IList<IWebElement> GetDropdownAvailbaleOptions(IWebElement element)
        {
            SelectElement select = new SelectElement(element);
            return select.Options;
        }

        public IList<string> GetDropdownAvailbaleOptionStrings(IWebElement element)
        {
            SelectElement select = new SelectElement(element);
            var elementList = select.Options;
            IList<string> optionStrings = new List<string>();

            for (int i = 0; i < elementList.Count; i++)
            {
                optionStrings.Add(elementList[i].Text);
            }
            return optionStrings;
        }

        public string GetDropDownSelectedElementText(IWebElement element)
        {
            SelectElement select = new SelectElement(element);
            return select.SelectedOption.Text;
        }

        public void MoveToTheLatestTab(TestBrowser browser)
        {
            browser.driver.SwitchTo().Window(browser.driver.WindowHandles.Last());
        }

        public void MoveFocusAwayFromElement(IWebElement element)
        {
            element.SendKeys(Keys.Tab);
        }

        public bool IsElementActive(IWebElement ele)
        {
            try
            {
                if (ele.GetAttribute("disabled") == "true")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsElementVisible(IWebElement ele)
        {
            try
            {
                WaitForElementToClickable(ele);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsElementVisible_Short(IWebElement ele)
        {
            try
            {
                WaitForElementToClickable_Short(ele);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ClearTextBoxValueText(IWebElement ele)
        {
            var eleTextLength = ele.GetAttribute("value").Length;

            for (int i = 0; i < eleTextLength; i++)
            {
                ele.SendKeys(Keys.Backspace);
            }
        }

        public void RefreshPage()
        {
            Browser.driver.Navigate().Refresh();
        }

        public bool IsAlertWindowPresent()
        {
            try
            {
                Browser.driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException Ex)
            {
                return false;
            }
        }

        public void WaitForElementTextToPopulate(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(5));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(e => (element.Text != string.Empty));
        }

        public void WaitForElementTextToPopulate_VeryLong(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(300));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(e => (element.Text != string.Empty));
        }

        public void WaitForElementTextToGetText(IWebElement element, string expectedText)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(30));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(e => (element.Text == expectedText));
        }

        public void WaitForElementToClickable(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(OpenQA.Selenium.ElementClickInterceptedException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitForElementToClickable_VeryLong(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(180));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitForElementsToBeVisible(IList<IWebElement> elements)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            elements.ToList().ForEach(x => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(elements.ToList().AsReadOnly())));
        }

        private void WaitForElementsToBeVisible_Short(List<IWebElement> elements)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(3));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            elements.ForEach(x => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(elements.AsReadOnly())));
        }

        public void WaitForElementToClickable_Short(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(3));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitForElementLocatedByToDissapear(By by)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public void WaitForElementLocatedByToAppear(By by)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }

        public void WaitForMultipleElementsToClickable(List<IWebElement> elements)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            elements.ForEach(x => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(x)));
        }

        public void WaitForElementValueTextGetsPopulated(IWebElement element)
        {
            Func<IWebDriver, bool> condition = (x) => (element.GetAttribute("value") != string.Empty);
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(59));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(NoSuchElementException));
            wait.Until(condition);
        }

        public void WaitForElementValueTextToHaveGivenText(IWebElement element, string givenText)
        {
            Func<IWebDriver, bool> condition = (x) => (element.GetAttribute("value") == givenText);
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(59));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(NoSuchElementException));
            wait.Until(condition);
        }

        public void WaitForElementToBeDisabled(IWebElement element)
        {
            Func<IWebDriver, bool> condition = (x) => (element.GetAttribute("disabled") == "true");
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(59));
            wait.PollingInterval = new TimeSpan(0, 0, 0, 0, 100);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(NoSuchElementException));
            wait.Until(condition);
        }

        public void WaitForElementToBeEnabled(IWebElement element)
        {

            Func<IWebDriver, bool> condition = (x) => (element.GetAttribute("disabled") == null);
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(90));
            wait.PollingInterval = new TimeSpan(0, 0, 0, 0, 100);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(NoSuchElementException));
            wait.Until(condition);
        }

        public void WaitForElementTextNotToBeZero(IWebElement element)
        {

            Func<IWebDriver, bool> condition = (x) => (element.Text != "$0.00");
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(59));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(NoSuchElementException));
            wait.Until(condition);
        }

        public void WaitForElementToBeVisibleByLocator(By by)
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        public void WaitForDropDownElementToHaveGivenText(IWebElement ele, string text)
        {
            Func<IWebDriver, bool> condition = (x) => (GetDropDownSelectedElementText(ele) == text);
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(59));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(NoSuchElementException));
            wait.Until(condition);
        }

        public void WaitForDropDownElementToHaveText(IWebElement ele)
        {
            Func<IWebDriver, bool> condition = (x) => (GetDropDownSelectedElementText(ele) != "Select"
                && GetDropDownSelectedElementText(ele) != null && GetDropDownSelectedElementText(ele) != string.Empty);
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(59));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(NoSuchElementException));
            wait.Until(condition);
        }

        public void WaitForAnAlertToPopUp()
        {
            WebDriverWait wait = new WebDriverWait(Browser.driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(TargetInvocationException), typeof(NoSuchElementException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }
    }
}
