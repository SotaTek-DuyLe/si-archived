using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundInstanceEventPage : BasePageCommonActions
    {
        public readonly By DetailsTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By RoundEventTypeSelect = By.XPath("//div[@id='details-tab']//select[@id='event-type']");
        public readonly By ResourceSelect = By.XPath("//div[@id='details-tab']//select[@id='resource']");
    }
}
