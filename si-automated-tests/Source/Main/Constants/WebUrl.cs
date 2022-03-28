using System;
using NUnit.Framework;

namespace si_automated_tests.Source.Main.Constants
{
    public class WebUrl
    {
        public static string MainPageUrl = "";
        public static string MainPageUrlIE = "";

        public WebUrl()
        {
            try
            {
                var env = TestContext.Parameters.Get("env");
                if (env.Equals("demo", StringComparison.OrdinalIgnoreCase)){
                    MainPageUrl = "https://demo.echoweb.co.uk/";
                    MainPageUrlIE = "https://demo.echoweb.co.uk/echo2/echo2extra/mainframe.aspx";
                }
                else if (env.Equals("prod", StringComparison.OrdinalIgnoreCase))
                {
                    MainPageUrl = "https://www.google.com/";
                    MainPageUrlIE = "https://echoweb.co.uk/echo2/echo2extra/mainframe.aspx";
                }
                else
                {
                    Console.WriteLine("Environment incorrect. Using demo environment");
                    MainPageUrl = "https://demo.echoweb.co.uk/";
                    MainPageUrlIE = "https://demo.echoweb.co.uk/echo2/echo2extra/mainframe.aspx";
                }
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("Environment not specified. Using demo environment");
                MainPageUrl = "https://demo.echoweb.co.uk/";
                MainPageUrlIE = "https://demo.echoweb.co.uk/echo2/echo2extra/mainframe.aspx";
            }
        }
    }
}
