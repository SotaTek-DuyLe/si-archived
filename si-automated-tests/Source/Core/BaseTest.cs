using System;
using NUnit.Framework;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Core
{
    public class BaseTest
    {
        protected DatabaseContext DbContext { get; private set; }

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            new WebUrl();
            try
            {
                string host = TestContext.Parameters.Get("host");
                string useIntegratedSecurity = TestContext.Parameters.Get("useIntegratedSecurity");
                string db = TestContext.Parameters.Get("dbname");
                if (useIntegratedSecurity.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    Logger.Get().Info("Using Integrated Security");
                    DbContext = new DatabaseContext(host, db);
                }
                else
                {
                    Logger.Get().Info("Using Creds");
                    string userId = TestContext.Parameters.Get("dbusername");
                    string password = TestContext.Parameters.Get("dbpassword");
                    DbContext = new DatabaseContext(host, db, userId, password);
                }
            }
            catch (Exception e)
            {
                Logger.Get().Info("SQL details not specified correctly: " + e.Message);
                Logger.Get().Info("Using default details");
                DbContext = new DatabaseContext();
            }

        }

        [SetUp]
        public virtual void Setup()
        {
            OnSetup();
            //DatabaseContext = new DatabaseContext();
        }

        protected void OnSetup()
        {
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
