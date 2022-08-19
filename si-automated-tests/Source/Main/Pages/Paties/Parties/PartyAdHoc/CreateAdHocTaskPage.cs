using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Adhoc;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyVehiclePage;
using si_automated_tests.Source.Main.Pages.WB.Tickets;
using CanlendarServiceTask = si_automated_tests.Source.Main.Models.Suspension.ServiceTaskModel;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAdHoc
{
    public class CreateAdHocTaskPage : BasePage
    {
        private readonly By modal = By.XPath("//div[@id='po-number-requirement-editor-modal']");
        private readonly By title = By.XPath("//div[@id='po-number-requirement-editor-modal']//h4");
        private readonly By input = By.XPath("//div[@id='po-number-requirement-editor-modal']//input[@id='poNumber']");
        private readonly By doneBtn = By.XPath("//div[@id='po-number-requirement-editor-modal']//button[text()='Done']");

        public CreateAdHocTaskPage VerifyAdHocTaskIsCreated()
        {
            string url = driver.Url;
            string id = url.Split('/').LastOrDefault();
            Assert.IsTrue(id.AsInteger() > 0);
            return this;
        }

        public CreateAdHocTaskPage VerifyTitle(string expectedTitle)
        {
            Assert.IsTrue(GetElementText(title) == expectedTitle);
            return this;
        }

        public CreateAdHocTaskPage InputPoNumber(string inputStr)
        {
            IWebElement webElement = GetElement(input);
            SendKeys(webElement, inputStr);
            return this;
        }

        public CreateAdHocTaskPage ClickDone()
        {
            ClickOnElement(doneBtn);
            return this;
        }

        public CreateAdHocTaskPage IsCreateAdhocTaskInVisible()
        {
            Assert.IsTrue(IsControlUnDisplayed(modal));
            return this;
        }
    }
}
