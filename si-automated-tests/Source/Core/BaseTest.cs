using System;
using NUnit.Framework;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Core
{
    public class BaseTest
    {
        [SetUp]
        public void Setup()
        {
            IWebDriverManager.SetDriver();
            new UserRegistry();
        }

        [TearDown]
        public void TearDown()
        {
            IWebDriverManager.GetDriver().Quit();
        }
    }
}
