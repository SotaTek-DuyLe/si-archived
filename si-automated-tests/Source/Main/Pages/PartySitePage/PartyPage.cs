﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages
{
    public class PartyPage : BasePage
    {
        private const string DetailsTab = "//a[text()='Details']";
        private const string SitesTab = "//a[text()='Sites']";
        private const string CorrespondenceAddressAddBtn = "//label[text()='Correspondence Address']/following-sibling::div//span[text()='Add']";
        private const string SuccessfullyToastMessage = "//div[@class='notifyjs-corner']//div[text()='Successfully saved party.']";
        private readonly By InvoiceAddressButton = By.Id("party-invoice-address");
        private readonly By SaveBtn = By.XPath("//button[@title='Save']");

        private string SiteAddressValue = "//label[text()='Correspondence Address']/following-sibling::div//option[text()='{0}']";
        private string InvoiceAddressValue = "//select[@id='party-invoice-address']/option[text()='{0}']";
        private string InvoiceAddressOnPage = "//div[contains(@data-bind,'invoiceAddress')]/p[text()='{0}']";
        private string AddressTitle = "//div[text()='{0}']"; 
        public PartyPage ClickAddCorrespondenceAddress()
        {
            WaitUtil.WaitForElementVisible(CorrespondenceAddressAddBtn);
            ClickOnElement(CorrespondenceAddressAddBtn);
            return this;
        }

        public PartyPage ClickOnDetailsTab()
        {
            ClickOnElement(DetailsTab);
            return this;
        }

        public PartyPage ClickOnSitesTab()
        {
            ClickOnElement(SitesTab);
            return this;
        }

        public PartyPage VerifyAddressAppearAtSitesTab(string title)
        {
            WaitUtil.WaitForElementVisible(AddressTitle, title);
            Assert.IsTrue(IsControlDisplayed(AddressTitle, title));
            return this;
        }
        public PartyPage VerifyCreatedSiteAddressAppearAtAddress(string address)
        {
            WaitUtil.WaitForElementVisible(SiteAddressValue, address);
            Assert.IsTrue(IsControlDisplayed(SiteAddressValue, address));
            return this;
        }

        public PartyPage ClickOnInvoiceAddressButton()
        {
            ClickOnElement(InvoiceAddressButton);
            return this;
        }

        public PartyPage VerifyCreatedAddressAppearAtInvoiceAddress(string address)
        {
            WaitUtil.WaitForElementVisible(InvoiceAddressValue, address);
            Assert.IsTrue(IsControlDisplayed(InvoiceAddressValue, address));
            return this;
        }

        public PartyPage SelectCreatedAddress(string address)
        {
            ClickOnElement(InvoiceAddressValue, address);
            return this;
        }

        public PartyPage ClickSaveBtn()
        {
            ClickOnElement(SaveBtn);
            return this;
        }

        public PartyPage VerifyDisplaySuccessfullyMessage()
        {

            Assert.IsTrue(IsControlDisplayed(SuccessfullyToastMessage));
            WaitUtil.WaitForElementInvisible(SuccessfullyToastMessage);
            return this;
        }

        public PartyPage VerifySelectedAddressOnInvoicePage(String address)
        {
            WaitUtil.WaitForElementVisible(InvoiceAddressOnPage, address);
            Assert.IsTrue(IsControlDisplayed(InvoiceAddressOnPage, address));
            return this;
        }

    }
}
