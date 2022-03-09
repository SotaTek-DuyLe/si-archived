using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace si_automated_tests.Source.Core
{
    public class WaitUtil
    {
        public static void WaitForPageLoaded()
        {
            var js = (IJavaScriptExecutor)IWebDriverManager.GetDriver();
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }
        public static IWebElement WaitForElementVisible(string locator)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            return (WebElement)driverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
        }

        public static IWebElement WaitForElementVisible(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            return driverWait.Until(ExpectedConditions.ElementIsVisible(by));
        }
        public static IWebElement WaitForElementVisible(string locator, string value)
        {
            locator = String.Format(locator, value);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(20));
            return (WebElement)driverWait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
        }

        public static IWebElement WaitForElementClickable(string locator)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            return driverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
        }
        public static IWebElement WaitForElementClickable(string locator, string value)
        {
            locator = String.Format(locator, value);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(20));
            return driverWait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
        }
        public static IWebElement WaitForElementClickable(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            return driverWait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
        public static IWebElement WaitForElementClickable(IWebElement webElement)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            return driverWait.Until(ExpectedConditions.ElementToBeClickable(webElement));
        }

        public static void WaitForAlert()
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(20));
            driverWait.Until(ExpectedConditions.AlertIsPresent());
        }

        public static void WaitForElementInvisible(string xpath)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(20));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xpath)));
        }
    }
}
