using System;
using System.Threading.Tasks;
using NUnit.Framework;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Core
{
    public class BaseTest
    {
        protected DatabaseContext DatabaseContext { get; private set; }
        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            SetupAsync().Wait();
            DatabaseContext = new DatabaseContext();
        }

        public async Task SetupAsync()
        {
            var chromeDriverInstaller = new ChromeDriverInstaller();
            // not necessary, but added for logging purposes
            var chromeVersion = await chromeDriverInstaller.GetChromeVersion();
            Console.WriteLine($"Chrome version {chromeVersion} detected");
            await chromeDriverInstaller.Install(chromeVersion);
            Console.WriteLine("ChromeDriver installed");
        }

        [SetUp]
        public virtual void Setup()
        {
            OnSetup();
            //DatabaseContext = new DatabaseContext();
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
            //DatabaseContext?.Dispose();
        }

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            //DatabaseContext?.Dispose();
        }

        protected void OnTearDown()
        {
            CustomTestListener.OnTestFinished();
            IWebDriverManager.GetDriver().Quit();
        }
    }
}
