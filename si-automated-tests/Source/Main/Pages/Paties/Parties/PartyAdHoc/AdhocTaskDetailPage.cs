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
    public class AdhocTaskDetailPage : BasePage
    {
        private readonly By title = By.XPath("//div[@class='popup-container']//h4[@class='id']");
        private readonly By purchaseOrderValue = By.XPath("//div[@class='popup-container']//div[@class='header-block-name' and text()='Purchase Order #']//following-sibling::div");
        private readonly By tasklineTab = By.XPath("//ul[contains(@class,'nav-tabs')]//a[@aria-controls='taskLines-tab']");

        public AdhocTaskDetailPage VerifyPoNumber()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(GetElementText(title)));
            return this;
        }

        public AdhocTaskDetailPage VerifyPurchaseOrderField(string expectedStr)
        {
            Assert.IsTrue(GetElementText(purchaseOrderValue) == expectedStr);
            return this;
        }

        public AdhocTaskDetailPage ClickTaskLinesTab()
        {
            ClickOnElement(tasklineTab);
            return this;
        }

    }
}
