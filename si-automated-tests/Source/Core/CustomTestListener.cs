using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace si_automated_tests.Source.Core
{

    public class CustomTestListener
    {
        [AllureStep("Screenshot of failure")]
        public static void GetScreenShot(string testName)
        {
            IWebDriverManager.GetDriver().SwitchTo().Window(IWebDriverManager.GetDriver().WindowHandles.Last());
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../si-automated-tests/TestResults/Screenshots/");
            var newPath = new Uri(path).LocalPath;
            Screenshot screenshot = ((ITakesScreenshot)IWebDriverManager.GetDriver()).GetScreenshot();
            var screenshotPath = newPath + "Evidence_" + testName + ".png";
            screenshot.SaveAsFile(screenshotPath);
            AllureLifecycle.Instance.AddAttachment(screenshotPath);
        }
        public static void OnTestStarted()
        {
            var test = TestContext.CurrentContext;
            Logger.Get().Info("TEST STARTED: " + test.Test.MethodName);
        }
        public static void OnTestFinished()
        {
            var test = TestContext.CurrentContext;
            var testOutCome = TestContext.CurrentContext.Result.Outcome;
            if (testOutCome.Equals(ResultState.Success))
            {
                Logger.Get().Info("TEST PASSED: " + test.Test.MethodName);
            }
            else
            {
                Logger.Get().Info("TEST STATUS: " + testOutCome.ToString() + " " + test.Test.MethodName);
                Logger.Get().Info("TEST MESSAGE: " + test.Result.Message);
                GetScreenShot(test.Test.MethodName);
            }
            Logger.Get().Info("END OF TEST");
        }
    }
}
