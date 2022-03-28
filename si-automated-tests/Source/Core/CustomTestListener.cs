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
        public static void GetScreenShot(string testName)
        {
            IWebDriverManager.GetDriver().SwitchTo().Window(IWebDriverManager.GetDriver().WindowHandles.Last());
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../si-automated-tests/TestResults/Screenshots/");
            var newPath = new Uri(path).LocalPath;
            Screenshot screenshot = ((ITakesScreenshot)IWebDriverManager.GetDriver()).GetScreenshot();
            screenshot.SaveAsFile(newPath + "Evidence_" + testName + ".png");
        }
        public static void OnTestStarted()
        {
            var test = TestContext.CurrentContext;
            Console.WriteLine("------TEST STARTED: " + test.Test.MethodName + "------");
        }
        public static void OnTestFinished()
        {
            var test = TestContext.CurrentContext;
            var testOutCome = TestContext.CurrentContext.Result.Outcome;
            if (testOutCome.Equals(ResultState.Success))
            {
                Console.WriteLine("TEST PASSED: " + test.Test.MethodName);
            }
            else
            {
                Console.WriteLine("TEST STATUS: " + testOutCome.ToString() + " " + test.Test.MethodName);
                Console.WriteLine("TEST MESSAGE: " + test.Result.Message);
                GetScreenShot(test.Test.MethodName);
            }
            Console.WriteLine("------END OF TEST------");
        }
    }
}
