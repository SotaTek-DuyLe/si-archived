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

namespace si_automated_tests.Source.Main.Pages.Paties.Sites
{
    public class SitePage : BasePageCommonActions
    {
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By LockCheckbox = By.XPath("//input[@id='locked']");
        public readonly By LockReferenceInput = By.XPath("//input[@id='lockReference']");
        public readonly By LockHelpButton = By.XPath("//span[contains(@class, 'lock-help')]");
        public readonly By LockHelpContent = By.XPath("//div[contains(@class, 'popover-content')]");
        public readonly By SiteAbvInput = By.XPath("//input[@id='site-abv']");
        public readonly By SiteAddressTitle = By.XPath("//p[@class='object-name']");
    }
}
