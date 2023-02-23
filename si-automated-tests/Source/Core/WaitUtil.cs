using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace si_automated_tests.Source.Core
{
    public class WaitUtil
    {
        private static int shortTimeOut = 30;
        private static int longTimeOut = 60;

        [AllureStep]
        public static void WaitForPageLoaded()
        {
            var js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(longTimeOut));
            driverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }
        [AllureStep]
        public static IWebElement WaitForElementVisible(string locator)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.PollingInterval = TimeSpan.FromMilliseconds(10);
            return (WebElement)driverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
        }
        [AllureStep]
        public static IWebElement WaitForElementVisible(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        [AllureStep]
        public static IWebElement WaitForElementVisible(string locator, string value)
        {
            locator = String.Format(locator, value);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return (WebElement)driverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
        }
        [AllureStep]
        public static IWebElement WaitForElementClickable(string locator)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
        }
        [AllureStep]
        public static IWebElement WaitForElementClickable(string locator, string value)
        {
            locator = String.Format(locator, value);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
        }
        [AllureStep]
        public static IWebElement WaitForElementClickable(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
        [AllureStep]
        public static IWebElement WaitForElementClickable(IWebElement webElement)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.ElementToBeClickable(webElement));
        }
        [AllureStep]
        public static void WaitForAlert()
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.AlertIsPresent());
        }
        [AllureStep]
        public static void WaitForElementInvisible(string xpath)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xpath)));
        }
        [AllureStep]
        public static void WaitForElementInvisibleWithText(string xpath, string text)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementWithText(By.XPath(xpath),text));
        }
        [AllureStep]
        public static void WaitForElementInvisible60(string xpath)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(longTimeOut));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xpath)));
        }
        [AllureStep]
        public static void WaitForAllElementsInvisible60(string xpath)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(longTimeOut));
            driverWait.Until(webDriver => IWebDriverManager.GetDriver().FindElements(By.XPath(xpath)).Any(x => x.Displayed) == false);
        }
        [AllureStep]
        public static void WaitForElementInvisible(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }
        [AllureStep]
        public static void WaitForElementInvisible(string xpath, string value)
        {
            xpath = String.Format(xpath, value);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xpath)));
        }
        [AllureStep]
        public static void WaitForElementToBeSelected(string xpath)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.ElementToBeSelected(By.XPath(xpath)));
        }
        [AllureStep]
        public static void WaitForElementToBeSelected(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.ElementToBeSelected(by));
        }
        [AllureStep]
        public static IList<IWebElement> WaitForAllElementsVisible(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }
        [AllureStep]
        public static IList<IWebElement> WaitForAllElementsPresent(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
        }
        [AllureStep]
        public static IWebElement WaitForElementsPresent(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.ElementExists(by));
        }
        [AllureStep]
        public static IList<IWebElement> WaitForAllElementsVisible(string xpath)
        {
            By by = By.XPath(xpath);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }
        [AllureStep]
        public static IList<IWebElement> WaitForAllElementsVisible(string xpath, string value)
        {
            xpath = String.Format(xpath, value);
            By by = By.XPath(xpath);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            return driverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }
        [AllureStep]
        public static void WaitForTextVisibleInElement(By by, string text)
        {
            try
            {
                var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
                driverWait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, text));
            }
            catch (WebDriverTimeoutException)
            {
                throw (new WebDriverTimeoutException("Expected text: " + text + " not found after " + shortTimeOut + " seconds"));
            }
        }
        [AllureStep]
        public static void WaitForValueInputDisplayed(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(driver => driver.FindElement(by).GetAttribute("value").Length != 0);
        }
        [AllureStep]
        public static void TextToBePresentInElementValue(IWebElement webElement, string text)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.TextToBePresentInElementValue(webElement, text));
        }
        [AllureStep]
        public static void TextToBePresentInElementValue(By by, string text)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.TextToBePresentInElementValue(by, text));
        }
        [AllureStep]
        public static void TextToBePresentInElementLocated(By by, string text)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, text));
        }
        [AllureStep]
        public static void WaitAttributeChange(By by, string attribute, string originalValue)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until((driver) => 
            {
                IWebElement webElement = driver.FindElement(by);
                string attributeValue = webElement.GetAttribute(attribute);
                return attributeValue != originalValue;
            });
        }
        [AllureStep]
        public static void WaitForTextToDisappearInElement(By by, string textToDisappear)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until((driver) =>
            {
                var element = driver.FindElement(by);
                return element.Text != textToDisappear;
            });
        }
        [AllureStep]
        public static void WaitForElementSize(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until((driver) =>
            {
                IWebElement webElement = driver.FindElement(by);
                var elementSize = webElement.Size;
                return elementSize != new System.Drawing.Size(0,0);
            });
        }
        [AllureStep]
        public static void WaitCssAttributeChange(By by, string attribute, string originalValue)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(shortTimeOut));
            driverWait.Until((driver) =>
            {
                IWebElement webElement = driver.FindElement(by);
                string attributeValue = webElement.GetCssValue(attribute);
                return attributeValue != originalValue;
            });
        }
    }
}
