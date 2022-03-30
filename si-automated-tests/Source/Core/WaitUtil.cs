﻿using System;
using System.Collections.Generic;
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
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
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
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
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
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(ExpectedConditions.AlertIsPresent());
        }

        public static void WaitForElementInvisible(string xpath)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xpath)));
        }
        public static void WaitForElementInvisible(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
        }
        public static void WaitForElementInvisible(string xpath, string value)
        {
            xpath = String.Format(xpath, value);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xpath)));
        }
        public static void WaitForElementToBeSelected(string xpath)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(ExpectedConditions.ElementToBeSelected(By.XPath(xpath)));
        }
        public static void WaitForElementToBeSelected(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(ExpectedConditions.ElementToBeSelected(by));
        }
        public static IList<IWebElement> WaitForAllElementsVisible(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            return driverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }
        public static IList<IWebElement> WaitForAllElementsVisible(string xpath)
        {
            By by = By.XPath(xpath);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            return driverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }
        public static IList<IWebElement> WaitForAllElementsVisible(string xpath, string value)
        {
            xpath = String.Format(xpath, value);
            Console.WriteLine(xpath);
            By by = By.XPath(xpath);
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            return driverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }
        public static void WaitForTextVisibleInElement(By by, string text)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, text));
        }

        public static void WaitForValueInputDisplayed(By by)
        {
            var driverWait = new WebDriverWait(IWebDriverManager.GetDriver(), TimeSpan.FromSeconds(30));
            driverWait.Until(driver => driver.FindElement(by).GetAttribute("value").Length != 0);
        }
    }
}
