using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages
{
    public class PartyDetailsTab : BasePage
    {
        private const string DetailsTab = "//a[text()='Details']";
        private const string SitesTab = "//a[text()='Sites']";
        private const string CorrespondenceAddressAddBtn = "//label[text()='Correspondence Address']/following-sibling::div//span[text()='Add']";
        private const string SuccessfullyToastMessage = "//div[@class='notifyjs-corner']//div[text()='Success']";
        private readonly By InvoiceAddressButton = By.Id("party-invoice-address");

        //SITES TAB
        private readonly By siteIdInput = By.XPath("//div[@id='sites-tab']//div[contains(@class, 'l1')]//input");
        private readonly By applyBtnSiteTab = By.XPath("//div[@id='sites-tab']//button[@title='Apply Filters']");
        private readonly By clearBtnSiteTab = By.XPath("//div[@id='sites-tab']//button[@title='Clear Filters']");
        private readonly By firstCheckboxAtRow = By.XPath("//div[@id='sites-tab']//div[@class='grid-canvas']//div[contains(@class, 'l0')]");
        private readonly By firstSiteRow = By.XPath("//div[@id='sites-tab']//div[@class='grid-canvas']/div[1]");
        private readonly By addNewItemInSiteTab = By.XPath("//div[@id='sites-tab']//button[text()='Add New Item']");

        //DYNAMIC
        private const string InvoiceAddressOnPage = "//div[contains(@data-bind,'invoiceAddress')]/p[text()='{0}']";
        private const string SiteAddressValue = "//label[text()='Correspondence Address']/following-sibling::div//option[text()='{0}']";
        private const string AddressTitle = "//div[text()='{0}']";
        private const string AccountingAtRow = "//div[@class='grid-canvas']/div[{0}]/div[contains(@class, 'l8')]";
        private const string InvoiceAddressValue = "//select[@id='party-invoice-address']/option[text()='{0}']";

        [AllureStep]
        public PartyDetailsTab ClickAddCorrespondenceAddress()
        {
            WaitUtil.WaitForElementVisible(CorrespondenceAddressAddBtn);
            ClickOnElement(CorrespondenceAddressAddBtn);
            return this;
        }
        [AllureStep]
        public PartyDetailsTab ClickOnDetailsTab()
        {
            ClickOnElement(DetailsTab);
            return this;
        }
        [AllureStep]
        public PartyDetailsTab ClickOnSitesTab()
        {
            ClickOnElement(SitesTab);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(addNewItemInSiteTab);
            return this;
        }
        [AllureStep]
        public PartyDetailsTab VerifyAddressAppearAtSitesTab(string title)
        {
            WaitUtil.WaitForElementVisible(AddressTitle, title);
            Assert.IsTrue(IsControlDisplayed(AddressTitle, title));
            return this;
        }
        [AllureStep]
        public PartyDetailsTab VerifyCreatedSiteAddressAppearAtAddress(string address)
        {
            WaitUtil.WaitForElementVisible(SiteAddressValue, address);
            Assert.IsTrue(IsControlDisplayed(SiteAddressValue, address));
            return this;
        }
        [AllureStep]
        public PartyDetailsTab ClickOnInvoiceAddressButton()
        {
            ClickOnElement(InvoiceAddressButton);
            return this;
        }
        [AllureStep]
        public PartyDetailsTab VerifyCreatedAddressAppearAtInvoiceAddress(string address)
        {
            WaitUtil.WaitForElementVisible(InvoiceAddressValue, address);
            Assert.IsTrue(IsControlDisplayed(InvoiceAddressValue, address));
            return this;
        }
        [AllureStep]
        public PartyDetailsTab SelectCreatedAddress(string address)
        {
            ClickOnElement(InvoiceAddressValue, address);
            return this;
        }
        [AllureStep]
        public PartyDetailsTab VerifyDisplaySuccessfullyMessage()
        {

            Assert.IsTrue(IsControlDisplayed(SuccessfullyToastMessage));
            WaitUtil.WaitForElementInvisible(SuccessfullyToastMessage);
            return this;
        }
        [AllureStep]
        public PartyDetailsTab VerifySelectedAddressOnInvoicePage(String address)
        {
            WaitUtil.WaitForElementVisible(InvoiceAddressOnPage, address);
            Assert.IsTrue(IsControlDisplayed(InvoiceAddressOnPage, address));
            return this;
        }

        //SITE TAB
        [AllureStep]
        public PartyDetailsTab FilterBySiteId(string siteIdValue)
        {
            SendKeys(siteIdInput, siteIdValue);
            ClickOnElement(applyBtnSiteTab);
            WaitForLoadingIconToDisappear();
            ClickOnElement(firstCheckboxAtRow);
            DoubleClickOnElement(firstSiteRow);
            return this;
        }

        [AllureStep]
        public PartyDetailsTab ClickOnClearBtn()
        {
            ClickOnElement(clearBtnSiteTab);
            return this;
        }

        [AllureStep]
        public PartyDetailsTab VerifyAccountingRefAnyRow(string rowNumber, string accountingValue)
        {
            Assert.AreEqual(accountingValue, GetElementText(AccountingAtRow, rowNumber), "Accounting Ref at row " + rowNumber + " is incorrect");
            return this;
        }

    }
}
