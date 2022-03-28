using System;
using NUnit.Framework;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Core
{
    public class BaseTest
    {
        [SetUp]
        public virtual void Setup()
        {
            OnSetup();
        }

        protected void OnSetup()
        {
            new WebUrl();
            CustomTestListener.OnTestStarted();
            IWebDriverManager.SetDriver();
            new UserRegistry();
        }

        [TearDown]
        public virtual void TearDown()
        {
            OnTearDown();
        }

        protected void OnTearDown()
        {
            CustomTestListener.OnTestFinished();
            IWebDriverManager.GetDriver().Quit();
        }
    }
}
