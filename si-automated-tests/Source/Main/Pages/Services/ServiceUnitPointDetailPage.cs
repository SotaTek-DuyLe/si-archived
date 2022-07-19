using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceUnitPointDetailPage : BasePageCommonActions
    {
        public readonly By RetireButton = By.XPath("//button[@title='Retire']");
        public readonly By LastUpdatedInput = By.XPath("//input[@id='lastUpdated']");
    }
}
