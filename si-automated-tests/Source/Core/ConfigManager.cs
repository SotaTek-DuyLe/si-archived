using NUnit.Framework;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Core
{
    class ConfigManager
    {
        public static WebPlatform GetCurrentPlatform()
        {
            var browser = IWebDriverManager.Brw.Value;
            if (browser.Equals(WebPlatform.Chrome.ToString(), StringComparison.OrdinalIgnoreCase) || browser.Equals(WebPlatform.Hchrome.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return WebPlatform.Chrome;
            }
            else if (browser.Equals(WebPlatform.IE.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return WebPlatform.IE;
            }
            else if (browser.Equals(WebPlatform.Firefox.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return WebPlatform.Firefox;
            }
            else
            {
                return WebPlatform.Chrome;
            }
        }
    }
}
