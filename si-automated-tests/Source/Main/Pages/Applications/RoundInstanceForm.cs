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
    public class RoundInstanceForm : BasePageCommonActions
    {
        public readonly By DropDownStatusButton = By.XPath("//button[@data-id='status']");
        public readonly By DropDownSelect = By.XPath("//ul[@class='dropdown-menu inner']");

        public RoundInstanceForm SelectStatus(string status)
        {
            SleepTimeInMiliseconds(300);
            IWebElement webElement = this.driver.FindElements(DropDownSelect).FirstOrDefault(x => x.Displayed);
            SelectByDisplayValueOnUlElement(webElement, status);
            return this;
        }
    }
}
