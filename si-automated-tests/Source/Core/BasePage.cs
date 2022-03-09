using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework.Internal;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace si_automated_tests.Source.Core
{
    public class BasePage
    {
        protected IWebDriver driver;
        private IJavaScriptExecutor javascriptExecutor;
        public BasePage()
        {
            Thread.Sleep(750);
            this.driver = IWebDriverManager.GetDriver();
        }

        public void GoToURL(string url)
        {
            driver.Url = url;
            WaitUtil.WaitForPageLoaded();
        }

        //GET ELEMENT
        public IWebElement GetElement(By by)
        {
            return WaitUtil.WaitForElementVisible(by);
        }
        public List<IWebElement> GetAllElements(string locator)
        {
            Thread.Sleep(5000);
            return driver.FindElements(By.XPath(locator)).ToList();
        }

        //SEND KEYS
        public void SendKeys(string locator, string value)
        {
            IWebElement element = WaitUtil.WaitForElementVisible(locator);
            element.Clear();
            element.SendKeys(value);
        }
        public void SendKeys(By by, string value)
        {
            IWebElement element = WaitUtil.WaitForElementVisible(by);
            element.Clear();
            element.SendKeys(value);
        }

        //CLICK ON ELEMENT
        public void ClickOnElement(By by)
        {
            WaitUtil
                .WaitForElementClickable(by)
                .Click();
        }
        public void ClickOnElement(IWebElement element)
        {
            WaitUtil.WaitForElementClickable(element).Click();
        }

        public void ClickOnElement(string xpath)
        {
            WaitUtil
                .WaitForElementVisible(xpath);
            WaitUtil
                .WaitForElementClickable(xpath)
                .Click();
        }
        public void ClickOnElement(string xpath, string value)
        {
            xpath = string.Format(xpath, value);
            WaitUtil
                .WaitForElementClickable(xpath)
                .Click();
        }
        public void ClickToElementByAction(string xpath)
        {
            IWebElement element = this.driver.FindElement(By.XPath(xpath));
            this.javascriptExecutor.ExecuteScript("arguments[0].scrollIntoViewIfNeeded(true);", new Object[] { element });
            Actions actions = new Actions(driver);
            WaitUtil.WaitForElementVisible(xpath);
            actions.Click(element).Perform();
        }
        public void ClickToElementByAction(string xpath, string value)
        {
            xpath = string.Format(xpath, value);
            IWebElement element = this.driver.FindElement(By.XPath(xpath));
            this.javascriptExecutor.ExecuteScript("arguments[0].scrollIntoViewIfNeeded(true);", new Object[] { element });
            Actions actions = new Actions(driver);
            WaitUtil.WaitForElementVisible(xpath);
            actions.Click(element).Perform();
        }
        public void ClickToElementByJavascript(string xpath)
        {
            IWebElement element = this.driver.FindElement(By.XPath(xpath));
            this.javascriptExecutor = (IJavaScriptExecutor)this.driver;
            this.javascriptExecutor.ExecuteScript("arguments[0].click();", new Object[] { element });
        }

        public void ClickToElementByJavascript(string xpath, string value)
        {
            xpath = string.Format(xpath, value);
            IWebElement element = this.driver.FindElement(By.XPath(xpath));
            this.javascriptExecutor = (IJavaScriptExecutor)this.driver;
            this.javascriptExecutor.ExecuteScript("arguments[0].click();", new Object[] { element });
        }
        public void DoubleClickOnElement(By by)
        {
            Actions act = new Actions(IWebDriverManager.GetDriver());
            IWebElement element = WaitUtil.WaitForElementVisible(by);
            act.DoubleClick(element).Perform();
        }
        public void DoubleClickOnElement(string xpath)
        {
            Actions act = new Actions(IWebDriverManager.GetDriver());
            IWebElement element = WaitUtil.WaitForElementVisible(xpath);
            act.DoubleClick(element).Perform();
        }
        public bool IsControlDisplayed(string xpath)
        {
            WaitUtil.WaitForElementVisible(xpath);
            return this.driver.FindElement(By.XPath(xpath)).Displayed;
        }

        public bool IsControlDisplayed(string xpath, string value)
        {
            xpath = string.Format(xpath, value);
            return this.driver.FindElement(By.XPath(xpath)).Displayed;
        }

        public bool IsControlDisplayedNotThrowEx(string xpath)
        {
            return this.driver.FindElements(By.XPath(xpath)).Count != 0;
        }

        public bool IsControlDisplayed(By by)
        {
            return this.driver.FindElement(by).Displayed;
        }

        public bool IsControlEnabled(By by)
        {
            return this.driver.FindElement(by).Enabled;
        }

        //RETURN ELEMENT'S TEXT
        public string GetElementText(string xpath)
        {
            return WaitUtil.WaitForElementVisible(xpath).Text;
        }
        public string GetElementText(By by)
        {
            return WaitUtil.WaitForElementVisible(by).Text;
        }
        public string GetElementText(IWebElement element)
        {
            return element.Text;
        }

        //SWITCH FRAME
        public void SwitchToFrame(By by)
        {
            IWebElement e = WaitUtil.WaitForElementVisible(by);
            IWebDriverManager.GetDriver().SwitchTo().Frame(e);

        }
        public void SwitchToDefaultContent()
        {
            IWebDriverManager.GetDriver().SwitchTo().DefaultContent();

        }
        public void SwitchNewIFrame()
        {
            IWebElement iframe = WaitUtil.WaitForElementVisible(By.TagName("iframe"));
            driver.SwitchTo().Frame(iframe);
            Thread.Sleep(5000);
        }

        //SWITCH WINDOW
        public void SwitchToFirstWindow()
        {
            IWebDriverManager.GetDriver().SwitchTo().Window(IWebDriverManager.GetDriver().WindowHandles.First());
        }
        public void SwitchToLastWindow()
        {
            IWebDriverManager.GetDriver().SwitchTo().Window(IWebDriverManager.GetDriver().WindowHandles.Last());
        }
        public void SwitchToChildWindow()
        {
            WaitUntilNewWindowIsOpened(2);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            MaximumWindow();
        }
        public void WaitUntilNewWindowIsOpened(int expectedNumberOfWindows, int maxRetryCount = 10)
        {
            int returnValue;
            bool boolReturnValue;
            for (var i = 0; i < maxRetryCount; Thread.Sleep(100), i++)
            {
                returnValue = driver.WindowHandles.Count;
                boolReturnValue = (returnValue == expectedNumberOfWindows ? true : false);
                if (boolReturnValue)
                {
                    return;
                }
            }
            //try one last time to check for window
            returnValue = driver.WindowHandles.Count;
            boolReturnValue = (returnValue == expectedNumberOfWindows ? true : false);
            if (!boolReturnValue)
            {
                throw new ApplicationException("New window did not open.");
            }
        }

        //ALERT
        public String GetAlertText()
        {
            WaitUtil.WaitForAlert();
            return IWebDriverManager.GetDriver().SwitchTo().Alert().Text;
        }
        public BasePage VerifyAlertText(string expected)
        {
            Assert.AreEqual(expected, GetAlertText());
            return this;
        }
        public BasePage AcceptAlert()
        {
            WaitUtil.WaitForAlert();
            IWebDriverManager.GetDriver().SwitchTo().Alert().Accept();
            return this;
        }

        //REFRESH
        public BasePage Refresh()
        {
            IWebDriverManager.GetDriver().Navigate().Refresh();
            return this;
        }
        //SCROLLING

        public BasePage Scroll(int pixel)
        {
            WaitUtil.WaitForPageLoaded();
            Thread.Sleep(2000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            js.ExecuteScript(String.Format("window.scrollTo(0, {0})", pixel.ToString()));

            return this;
        }
        public BasePage ScrollDownInElement(string elementId)
        {
            WaitUtil.WaitForPageLoaded();
            Thread.Sleep(2000);
            string scriptText = String.Format("var objDiv = document.getElementById(\"{0}\");objDiv.scrollTop = objDiv.scrollHeight;", elementId);
            IJavaScriptExecutor js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            js.ExecuteScript(scriptText);
            return this;
        }

        public BasePage ScrollDownToElement(By by)
        {
            WaitUtil.WaitForPageLoaded();
            Thread.Sleep(2000);
            IWebElement e = GetElement(by);
            IJavaScriptExecutor js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            js.ExecuteScript("arguments[0].scrollIntoView(true);", e);

            return this;
        }

        public BasePage ScrollToBottomOfPage()
        {
            WaitUtil.WaitForPageLoaded();
            Thread.Sleep(2000);
            var js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

            return this;
        }
        //GET FIRST SELECTED ITEM IN DROPDOWN
        public string GetFirstSelectedItemInDropdown(string xpath)
        {
            IWebElement comboBox = driver.FindElement(By.XPath(xpath));
            SelectElement selectedValue = new SelectElement(comboBox);
            return selectedValue.SelectedOption.Text;
        }

        //GET ATTRIBUTE VALUE
        public string GetAttributeValue(string xpath, string attributeName)
        {
            IWebElement element = WaitUtil.WaitForElementVisible(xpath);
            return element.GetAttribute(attributeName);
        }
        public string GetAttributeValue(By by, string attributeName)
        {
            IWebElement element = WaitUtil.WaitForElementVisible(by);
            return element.GetAttribute(attributeName);
        }

        //MAXIMUM WINDOW
        public void MaximumWindow()
        {
            this.driver.Manage().Window.Maximize();
        }

    }
}
