using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using si_automated_tests.Source.Main.Constants;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace si_automated_tests.Source.Core
{
    public class IWebDriverManager
    {
        public static ThreadLocal<IWebDriver> drivers = new ThreadLocal<IWebDriver>();
        public static ThreadLocal<IWebDriver> Drivers { get => drivers; set => drivers = value; }
        public static ThreadLocal<string> Brw { get; set; } = new ThreadLocal<string>();

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
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--incognito");
                    options.AddArgument("--disable-extensions");
                    options.AddArgument("--disable-infobars");
                    Drivers.Value = new ChromeDriver(options);
                }
                else if (browser.Equals("firefox", StringComparison.OrdinalIgnoreCase))
                {
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    FirefoxOptions options = new FirefoxOptions();
                    options.AcceptInsecureCertificates = true;
                    Drivers.Value = new FirefoxDriver(options);
                }
                else if (browser.Equals("hchrome", StringComparison.OrdinalIgnoreCase))
                {
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("--headless");
                    options.AddArgument("--incognito");
                    options.AddArgument("--disable-extensions");
                    options.AddArgument("--disable-infobars");
                    options.AddArgument("window-size=1920,1080");
                    Drivers.Value = new ChromeDriver(options);
                }
                else if (browser.Equals("ie", StringComparison.OrdinalIgnoreCase))
                {
                    InternetExplorerConfig config = new InternetExplorerConfig();
                    new DriverManager().SetUpDriver(config, config.GetLatestVersion(), WebDriverManager.Helpers.Architecture.X32);
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.PageLoadStrategy = PageLoadStrategy.None;
                    Drivers.Value = new InternetExplorerDriver(ieOptions);
                }
                else
                {
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    Drivers.Value = new ChromeDriver();
                }
                Logger.Get().Info("Setting up " + browser);
                Brw.Value = browser;
            }
            catch (NullReferenceException)
            {
                Logger.Get().Info("Browser not specified, setting up for Chrome");
                new DriverManager().SetUpDriver(new ChromeConfig());
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--incognito");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-infobars");
                Drivers.Value = new ChromeDriver(options);
                Brw.Value = "chrome";
            }
            Drivers.Value.Manage().Window.Maximize();
            Drivers.Value.Manage().Cookies.DeleteAllCookies();
        }

        public static void SetDriver(string browser)
        {

            if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--incognito");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-infobars");
                Drivers.Value = new ChromeDriver(options);
            }
            else if (browser.Equals("firefox", StringComparison.OrdinalIgnoreCase))
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
                FirefoxOptions options = new FirefoxOptions();
                options.AcceptInsecureCertificates = true;
                Drivers.Value = new FirefoxDriver(options);
            }
            else if (browser.Equals("hchrome", StringComparison.OrdinalIgnoreCase))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--incognito");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-infobars");
                options.AddArgument("window-size=1920,1080");
                Drivers.Value = new ChromeDriver(options);
            }
            else if (browser.Equals("ie", StringComparison.OrdinalIgnoreCase))
            {
                InternetExplorerConfig config = new InternetExplorerConfig();
                new DriverManager().SetUpDriver(config, config.GetLatestVersion(), WebDriverManager.Helpers.Architecture.X32);
                InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                ieOptions.PageLoadStrategy = PageLoadStrategy.None;
                Drivers.Value = new InternetExplorerDriver(ieOptions);
            }
            else
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                Drivers.Value = new ChromeDriver();
            }
            Logger.Get().Info("Setting up " + browser);
            Brw.Value = browser;
            Drivers.Value.Manage().Window.Maximize();
        }
    }
}
