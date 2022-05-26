using System;
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
            new WebUrl();
            try
            {
                string db = TestContext.Parameters.Get("dbname");
                string userId = TestContext.Parameters.Get("dbusername");
                string password = TestContext.Parameters.Get("dbpassword");
                var useIntegratedSecurity = TestContext.Parameters.Get("useIntegratedSecurity");
                if (useIntegratedSecurity.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    DatabaseContext = new DatabaseContext(db);
                }
                else
                {
                    DatabaseContext = new DatabaseContext(db, userId, password);
                }
            }
            catch(Exception e)
            {
                Logger.Get().Info(e.StackTrace);
                Logger.Get().Info("SQL details not specified, using default details");
                DatabaseContext = new DatabaseContext();
            }
            Logger.Get().Info("Using Connection string: " + DatabaseContext.Conection.ConnectionString);
            
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
