using System;
using NUnit.Framework;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Core
{
    public class BaseTest
    {
        [SetUp]
        public void Setup()
        {
            new WebUrl();
            CustomTestListener.OnTestStarted();
            IWebDriverManager.SetDriver();
            new UserRegistry();
        }

        [TearDown]
        public void TearDown()
        {
            CustomTestListener.OnTestFinished();
            IWebDriverManager.GetDriver().Quit();
        }
    }
}
