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
                string host = TestContext.Parameters.Get("host");
                string useIntegratedSecurity = TestContext.Parameters.Get("useIntegratedSecurity");
                string db = TestContext.Parameters.Get("dbname");
                if (useIntegratedSecurity.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    DatabaseContext = new DatabaseContext(db);
                }
                else
                {
                    string userId = TestContext.Parameters.Get("dbusername");
                    string password = TestContext.Parameters.Get("dbpassword");
                    DatabaseContext = new DatabaseContext(db, userId, password);
                }
            }
            catch(Exception e)
            {
                Logger.Get().Info(e.StackTrace);
                Logger.Get().Info("SQL details not specified correctly, using default details");
                DatabaseContext = new DatabaseContext();
            }
            
        }

        [SetUp]
        public virtual void Setup()
        {
            OnSetup();
            Logger.Get().Info("Using Connection string: " + DatabaseContext.Conection.ConnectionString);
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
