using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace si_automated_tests.Source.Core
{
    public class BasePage
    {
        protected IWebDriver driver;
        private IJavaScriptExecutor javascriptExecutor;

        private readonly By closeBtn = By.XPath("//button[@title='Close Without Saving']");
        private readonly By refreshBtn = By.XPath("//button[@title='Refresh']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By saveAndCloseBtn = By.XPath("//button[@title='Save and Close']");
        private readonly string tab = "//a[@data-toggle='tab' and contains(text(),'{0}')]";
        private readonly string tabs = "//a[@data-toggle='tab']";
        private readonly string frameMessage = "//div[@class='notifyjs-corner']/div";


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
        public IWebElement GetElement(string locator)
        {
            return WaitUtil.WaitForElementVisible(locator);
        }
        public List<IWebElement> GetAllElements(string locator)
        {
            WaitUtil.WaitForAllElementsVisible(locator);
            return driver.FindElements(By.XPath(locator)).ToList();
        }
        public List<IWebElement> GetAllElements(string locator, string value)
        {
            WaitUtil.WaitForAllElementsVisible(string.Format(locator, value));
            return driver.FindElements(By.XPath(string.Format(locator, value))).ToList();
        }
        public List<IWebElement> GetAllElements(By by)
        {
            WaitUtil.WaitForAllElementsVisible(by);
            return driver.FindElements(by).ToList();
        }
        public List<IWebElement> GetAllElementsNotWait(By by)
        {
            return driver.FindElements(by).ToList();
        }

        //SEND KEYS
        public void SendKeys(IWebElement element, string value)
        {
            element.Clear();
            element.SendKeys(value);
        }
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

        public void SendKeysWithoutClear(By by, string value)
        {
            IWebElement element = WaitUtil.WaitForElementVisible(by);
            element.SendKeys(value);
        }

        public void ClearInputValue(By by)
        {
            IWebElement element = WaitUtil.WaitForElementVisible(by);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        public void EditSendKeys(By by, string value)
        {
            IWebElement element = WaitUtil.WaitForElementClickable(by);
            element.SendKeys(OpenQA.Selenium.Keys.LeftShift + OpenQA.Selenium.Keys.Home);
            element.SendKeys(value);
        }
        public void EditSendKeys(string xpath, string value)
        {
            IWebElement element = WaitUtil.WaitForElementClickable(xpath);
            element.SendKeys(OpenQA.Selenium.Keys.LeftShift + OpenQA.Selenium.Keys.Home);
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
        public void DoubleClickOnElement(string xpath, string value)
        {
            xpath = String.Format(xpath, value);
            Actions act = new Actions(IWebDriverManager.GetDriver());
            IWebElement element = WaitUtil.WaitForElementVisible(xpath);
            act.DoubleClick(element).Perform();
        }
        public void DoubleClickOnElement(string xpath)
        {
            Actions act = new Actions(IWebDriverManager.GetDriver());
            IWebElement element = WaitUtil.WaitForElementVisible(xpath);
            act.DoubleClick(element).Perform();
        }
        public void DoubleClickOnElement(IWebElement element)
        {
            Actions act = new Actions(IWebDriverManager.GetDriver());
            WaitUtil.WaitForElementClickable(element);
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
            WaitUtil.WaitForElementVisible(xpath);
            return this.driver.FindElement(By.XPath(xpath)).Displayed;
        }

        public bool IsControlDisplayedNotThrowEx(string xpath)
        {
            return this.driver.FindElements(By.XPath(xpath)).Count != 0;
        }

        public bool IsControlDisplayedNotThrowEx(By by)
        {
            return this.driver.FindElements(by).Count != 0;
        }

        public bool IsControlDisplayed(By by)
        {
            return this.driver.FindElement(by).Displayed;
        }

        public bool IsControlUnDisplayed(string xpath)
        {
            int num = this.driver.FindElements(By.XPath(xpath)).Count;
            if (num == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool IsControlUnDisplayed(By by)
        {
            int num = this.driver.FindElements(by).Count;
            if (num == 0)
            {
                return true;
            }
            else
            {
                return !this.driver.FindElements(by).FirstOrDefault()?.Displayed ?? false;
            }
        }
        public bool IsControlUnDisplayed(string xpath, string value)
        {
            xpath = String.Format(xpath, value);
            int num = this.driver.FindElements(By.XPath(xpath)).Count;
            if (num == 0)
            {
                return true;
            }
            else
            {
                return !this.driver.FindElements(By.XPath(xpath)).FirstOrDefault()?.Displayed ?? false;
            }

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
        public string GetElementText(string xpath, string value)
        {
            return WaitUtil.WaitForElementVisible(xpath, value).Text;
        }
        public string GetElementText(By by)
        {
            return WaitUtil.WaitForElementVisible(by).Text;
        }

        public string GetInputValue(By by)
        {
            return WaitUtil.WaitForElementVisible(by).GetAttribute("value");
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
        public BasePage SwitchNewIFrame()
        {
            IWebElement iframe = WaitUtil.WaitForElementVisible(By.TagName("iframe"));
            driver.SwitchTo().Frame(iframe);
            Thread.Sleep(1000);
            return this;
        }

        //SWITCH WINDOW
        public BasePage SwitchToFirstWindow()
        {
            IWebDriverManager.GetDriver().SwitchTo().Window(IWebDriverManager.GetDriver().WindowHandles.First());
            return this;
        }
        public BasePage SwitchToLastWindow()
        {
            Thread.Sleep(500);
            IWebDriverManager.GetDriver().SwitchTo().Window(IWebDriverManager.GetDriver().WindowHandles.Last());
            return this;
        }
        public BasePage SwitchToChildWindow(int numberOfWindow, int maxRetryCount = 50)
        {
            WaitUntilNewWindowIsOpened(numberOfWindow, maxRetryCount);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            MaximumWindow();
            return this;
        }
        public void WaitUntilNewWindowIsOpened(int expectedNumberOfWindows, int maxRetryCount = 50)
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
        public BasePage ScrollDownInElement(By by)
        {
            WaitUtil.WaitForPageLoaded();
            Thread.Sleep(2000);
            IWebElement e = GetElement(by);
            IJavaScriptExecutor js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", e);
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
        public BasePage ScrollDownToElement(IWebElement e)
        {
            WaitUtil.WaitForPageLoaded();
            Thread.Sleep(2000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            js.ExecuteScript("arguments[0].scrollIntoView(true);", e);
            return this;
        }
        public BasePage ScrollLeftt(By by)
        {
            WaitUtil.WaitForPageLoaded();
            Thread.Sleep(2000);
            IWebElement e = GetElement(by);
            IJavaScriptExecutor js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            js.ExecuteScript("arguments[0].scrollLeft += 250", e);

            return this;
        }
        public BasePage ScrollDownToElement(string locator, string value)
        {
            WaitUtil.WaitForPageLoaded();
            Thread.Sleep(2000);
            string xpath = String.Format(locator, value);
            IWebElement e = driver.FindElement(By.XPath(xpath));
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

        public string GetFirstSelectedItemInDropdown(IWebElement comboBox)
        {
            SelectElement selectedValue = new SelectElement(comboBox);
            return selectedValue.SelectedOption.Text;
        }

        public string GetFirstSelectedItemInDropdown(By by)
        {
            IWebElement comboBox = driver.FindElement(by);
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

        public string GetAttributeValue(IWebElement element, string attributeName)
        {
            return element.GetAttribute(attributeName);
        }

        //MAXIMUM WINDOW
        public void MaximumWindow()
        {
            this.driver.Manage().Window.Maximize();
        }

        //SELECT VALUE FROM SELECT ELEMENT
        public BasePage SelectTextFromDropDown(By by, string _text)
        {
            Thread.Sleep(1000);
            IWebElement comboBox = WaitUtil.WaitForElementClickable(by);
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByText(_text);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public BasePage SelectTextFromDropDown(IWebElement webElement, string _text)
        {
            Thread.Sleep(1000);
            WaitUtil.WaitForElementClickable(webElement);
            SelectElement selectedValue = new SelectElement(webElement);
            selectedValue.SelectByText(_text);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public BasePage SelectValueFromDropDown(By by, string _value)
        {
            IWebElement comboBox = WaitUtil.WaitForElementVisible(by);
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByValue(_value);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public BasePage SelectValueFromDropDown(IWebElement comboBox, string _value)
        {
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByValue(_value);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public BasePage SelectIndexFromDropDown(By by, int index)
        {
            IWebElement comboBox = WaitUtil.WaitForElementVisible(by);
            SelectElement selectedValue = new SelectElement(comboBox);
            selectedValue.SelectByIndex(index);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public BasePage SelectIndexFromDropDown(IWebElement webElement, int index)
        {
            SelectElement selectedValue = new SelectElement(webElement);
            selectedValue.SelectByIndex(index);
            WaitForLoadingIconToDisappear();
            return this;
        }

        //GET WARNING TEXT
        public string GetToastMessage()
        {
            string text = WaitUtil.WaitForElementVisible("//div[@data-notify-html='title']").Text;
            return text;
        }
        public BasePage VerifyToastMessage(string message)
        {
            Assert.AreEqual(message, GetToastMessage());
            return this;
        }
        public BasePage VerifyDisplayToastMessage(string message)
        {
            Assert.IsTrue(IsControlDisplayed("//*[contains(text(),'{0}')]", message));
            return this;
        }
        public BasePage VerifyToastMessages(List<string> messages)
        {
            WaitUtil.WaitForElementVisible("//div[@data-notify-html='title']");
            var notifyMsgs = GetAllElements(By.XPath("//div[@data-notify-html='title']")).Select(x => x.Text).ToList();
            Assert.AreEqual(messages, notifyMsgs);
            return this;
        }

        public BasePage VerifyToastMessagesIsUnDisplayed()
        {
            IsControlUnDisplayed(By.XPath("//div[@data-notify-html='title']"));
            return this;
        }

        public BasePage WaitUntilToastMessageInvisible(string toastMessage)
        {
            WaitUtil.WaitForElementInvisibleWithText("//div[@data-notify-html='title']", toastMessage);
            return this;
        }
        public BasePage ClickOnSuccessLink()
        {
            ClickOnElement("//a[@id='echo-notify-success-link' or @id='echo-notify-Success-link']");
            return this;
        }
        public bool IsElementSelected(By by)
        {
            return WaitUtil.WaitForElementVisible(by).Selected;
        }
        public bool IsElementSelected(string xpath, string value)
        {
            xpath = String.Format(xpath, value);
            return WaitUtil.WaitForElementVisible(xpath).Selected;
        }
        public BasePage WaitForLoadingIconToDisappear(bool implicitSleep = true)
        {
            try
            {
                if (implicitSleep) Thread.Sleep(750);
                WaitUtil.WaitForAllElementsInvisible60("//*[contains(@data-bind,'shield: isLoading')]");
                WaitUtil.WaitForAllElementsInvisible60("//div[@id='loading-shield']");
                WaitUtil.WaitForAllElementsInvisible60("//div[@class='loading-data' and contains(@data-bind,'loadingDefinition')]");
                WaitUtil.WaitForAllElementsInvisible60("//div[contains(@data-bind,'loadingDefinition')]");
                WaitUtil.WaitForAllElementsInvisible60("//div[@class='ui-widget-overlay shield' and contains(@data-bind,'shield: $root.isLoading')]");
                WaitUtil.WaitForPageLoaded();
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Loading icon doesn't disappear after 60 seconds");
            }
            return this;
        }

        public BasePage VerifyToastMessageNotAppear(string message)
        {
            string xpath = "//div[@data-notify-html='title' and text()='{0}']";
            Assert.IsTrue(IsControlUnDisplayed(String.Format(xpath, message)));
            return this;
        }
        public BasePage ClickCloseBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
        public BasePage ClickSaveBtn()
        {
            ClickOnElement(saveBtn);
            return this;
        }
        public string ClickSaveBtnGetUTCTime()
        {
            ClickOnElement(saveBtn);
            return CommonUtil.GetUtcTimeNow("dd/MM/yyyy hh:mm");
        }
        public BasePage ClickRefreshBtn()
        {
            ClickOnElement(refreshBtn);
            WaitForLoadingIconToDisappear();
            SleepTimeInMiliseconds(2000);
            return this;
        }
        public BasePage ClickSaveAndCloseBtn()
        {
            ClickOnElement(saveAndCloseBtn);
            return this;
        }
        public BasePage SetElementAttribute(string id, string _attribute, string _value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            string script = String.Format("document.getElementById('{0}').setAttribute('{1}', '{2}')", id, _attribute, _value);
            js.ExecuteScript(script);
            return this;
        }
        //SWITCH TAB
        public BasePage SwitchToTab(string tabName)
        {
            WaitForLoadingIconToDisappear();
            ClickOnElement(String.Format(tab, tabName));
            WaitForLoadingIconToDisappear();
            return this;
        }
        public int GetNumberOfWindowHandle()
        {
            return driver.WindowHandles.Count;
        }
        //GET CSS VALUE
        public string GetCssValue(By by, string propertyName)
        {
            IWebElement e = WaitUtil.WaitForElementVisible(by);
            return e.GetCssValue(propertyName);
        }

        public BasePage VerifyWindowClosed(int numberCurrentWindow)
        {
            Assert.AreEqual(GetNumberOfWindowHandle(), numberCurrentWindow);
            return this;
        }

        //SLEEP TIME IN MILISECONDS
        public BasePage SleepTimeInMiliseconds(int num)
        {
            Thread.Sleep(num);
            return this;
        }

        public BasePage DragAndDrop(IWebElement sourceElement, IWebElement targetElement)
        {
            var builder = new Actions(IWebDriverManager.GetDriver());
            var dragAndDrop = builder.ClickAndHold(sourceElement).MoveToElement(targetElement).Release(targetElement).Build();
            dragAndDrop.Perform();
            return this;
        }
        public BasePage AlternativeDragAndDrop(IWebElement sourceElement, IWebElement targetElement)
        {
            var builder = new Actions(IWebDriverManager.GetDriver());
            builder.ClickAndHold(sourceElement).MoveToElement(targetElement, 5, 5).Click(targetElement).Click(targetElement).Build().Perform();
            return this;
        }

        public BasePage VerifyFocusElement(By by)
        {
            Assert.AreEqual(GetElement(by), driver.SwitchTo().ActiveElement());
            return this;
        }

        public string GetCurrentUrl()
        {
            return IWebDriverManager.GetDriver().Url;
        }

        public BasePage CloseCurrentWindow()
        {
            IWebDriverManager.GetDriver().Close();
            return this;
        }

        public string GetCurrentTitle()
        {
            return IWebDriverManager.GetDriver().Title;
        }

        public bool IsCheckboxChecked(By by)
        {
            return GetElement(by).Selected;
        }

        public BasePage GoToAllTabAndConfirmNoError()
        {
            IList<IWebElement> elements = WaitUtil.WaitForAllElementsVisible(tabs);
            foreach (IWebElement element in elements)
            {
                Thread.Sleep(1000);
                ClickOnElement(element);
                WaitForLoadingIconToDisappear();
                Assert.IsFalse(IsControlDisplayedNotThrowEx(frameMessage));
            }
            return this;
        }
    }
}
