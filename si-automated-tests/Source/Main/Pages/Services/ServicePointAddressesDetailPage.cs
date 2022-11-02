using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Task;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServicePointAddressesDetailPage : BasePageCommonActions
    {
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
    }
}
