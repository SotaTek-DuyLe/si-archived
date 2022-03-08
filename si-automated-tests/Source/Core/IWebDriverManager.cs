using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace si_automated_tests.Source.Core
{
    public class IWebDriverManager
    {
        public static ThreadLocal<IWebDriver> drivers = new ThreadLocal<IWebDriver>();
        public static ThreadLocal<IWebDriver> Drivers { get => drivers; set => drivers = value; }

        public static string Browser;

        public static IWebDriver GetDriver()
        {
            return Drivers.Value;
        }
        public static void SetDriver()
        {
            try
            {
                var browser = TestContext.Parameters.Get("browser");
                if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
                {
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Drivers.Value = new ChromeDriver();
                }
                else if (browser.Equals("firefox", StringComparison.OrdinalIgnoreCase))
                {
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    Drivers.Value = new FirefoxDriver();
                }
                else if (browser.Equals("hchrome", StringComparison.OrdinalIgnoreCase))
                {
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--headless");
                    Drivers.Value = new ChromeDriver(options);
                }
                else if (browser.Equals("ie", StringComparison.OrdinalIgnoreCase))
                {
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    Drivers.Value = new InternetExplorerDriver();
                }
                else
                {
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Drivers.Value = new ChromeDriver();
                }
                System.Console.WriteLine("Setting up " + browser);
                Browser = browser;
            }
            catch (NullReferenceException)
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                Drivers.Value = new ChromeDriver();
                Browser = "chrome";
                System.Console.WriteLine("Browser not specified, setting up for Chrome");
            }
            Drivers.Value.Manage().Window.Maximize();
        }

        public static void SetDriver(string browser)
        {

            if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                Drivers.Value = new ChromeDriver();
            }
            else if (browser.Equals("firefox", StringComparison.OrdinalIgnoreCase))
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
                Drivers.Value = new FirefoxDriver();
            }
            else if (browser.Equals("hchrome", StringComparison.OrdinalIgnoreCase))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless");
                Drivers.Value = new ChromeDriver(options);
            }
            else if (browser.Equals("ie", StringComparison.OrdinalIgnoreCase))
            {
                InternetExplorerConfig config = new InternetExplorerConfig();
                new DriverManager().SetUpDriver(config,config.GetLatestVersion(),WebDriverManager.Helpers.Architecture.X32);
                InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                ieOptions.PageLoadStrategy = PageLoadStrategy.None;
                Drivers.Value = new InternetExplorerDriver(ieOptions);
            }
            else
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                Drivers.Value = new ChromeDriver();
            }
            System.Console.WriteLine("Setting up " + browser);
            Browser = browser;
            Drivers.Value.Manage().Window.Maximize();
        }
    }
}
