using System;
using System.Collections.Generic;
using System.Text;
using si_automated_tests.Source.Core;
using OpenQA.Selenium;
using NUnit.Framework;

namespace si_automated_tests.Source.Main.Pages.PartySitePage
{
    public class CreateEditSiteAddressPage : BasePage
    {
        private readonly By CreateSiteAddressTitle = By.XPath("//h1[text()='Create/Edit Site Address']");
        private readonly By CancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By NextBtn = By.XPath("//button[text()='Next']");

        private readonly By SiteNameInput = By.XPath("//label[text()='Site Name']/following-sibling::input");
        private readonly By SelectAddressNextBtn = By.XPath("//button[text()='Next' and contains(@data-bind,'selectedExistingAddress')]");
        private readonly By CreateBtn = By.XPath("//button[text()='Create']");
        public CreateEditSiteAddressPage IsOnCreateEditSiteAddressPage()
        {
            WaitUtil.WaitForElementVisible(CreateSiteAddressTitle);
            Assert.IsTrue(IsControlDisplayed(CreateSiteAddressTitle));
            Assert.IsTrue(IsControlDisplayed(CancelBtn));
            Assert.IsTrue(IsControlDisplayed(NextBtn));
            return this;
        }

        public CreateEditSiteAddressPage VerifyCreateSiteAddressPageClosed()
        {
            Assert.False(IsControlDisplayed(CreateSiteAddressTitle));
            return this;
        }

        public string SelectRandomSiteAddress() {
            WaitUtil.WaitForElementVisible("//div[contains(@data-bind, 'existingAddresses')]/div[1]");
            List<IWebElement> AllSiteAddressRows = GetAllElements("//div[contains(@data-bind, 'existingAddresses')]/div");
            Random rnd = new Random();
            int num = rnd.Next(0, AllSiteAddressRows.Count - 1);
            IWebElement site = AllSiteAddressRows[num];
            ClickOnElement(site);
            return GetElementText(site);
        }

        public CreateEditSiteAddressPage ClickNextBtn()
        {
            ScrollDownToElement(NextBtn);
            ClickOnElement(NextBtn);
            return this;
        }

        public CreateEditSiteAddressPage SelectAddressClickNextBtn()
        {
            ScrollDownToElement(SelectAddressNextBtn);
            ClickOnElement(SelectAddressNextBtn);
            return this;
        }

        public CreateEditSiteAddressPage InsertSiteName(string name)
        {
            SendKeys(SiteNameInput, name);
            return this;
        }

        public CreateEditSiteAddressPage ClickCreateBtn()
        {
            ClickOnElement(CreateBtn);
            return this;
        }

    }
}
