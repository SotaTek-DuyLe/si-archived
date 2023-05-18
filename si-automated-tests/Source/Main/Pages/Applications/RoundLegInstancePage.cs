using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Pages.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundLegInstancePage : BasePageCommonActions
    {
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By StatusSelect = By.XPath("//select[@id='status']");
        public readonly By ResolutionCodeSelect = By.XPath("//select[@id='resolution-code-id']");
    }
}
