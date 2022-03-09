using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages
{
    public class PartySiteAddressPage : BasePage
    {
        private readonly By SearchInput = By.Id("search");
        private readonly By SearchBtn = By.XPath("//button[@type='submit']");
        private readonly By CancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By NextBtn = By.XPath("//button[text()='Next']");
        private readonly By ResultField = By.XPath("//div[contains(@data-bind,'geocodedAddresses')]");
        private readonly By MapContainer = By.Id("map-container");
        private readonly By CreateManuallyBtn = By.XPath("//button[text()='Create Manually ']");
        private readonly By CreateNonGeographicalAddress = By.XPath("//button[text()='Create Non Geographical Address']");

        //CHECK ADDRESS DETAIL
        private readonly By SiteNameInput = By.Id("site-name");
        private readonly By SiteNameLabel = By.XPath("//label[text()='Site Name']");
        private readonly By SiteAbvInput = By.Id("site-abv");
        private readonly By SiteTypeDropdown = By.Id("site-type");
        private readonly By PropertyInput = By.Id("property");
        private readonly By PropertyLabel = By.XPath("//label[text()='Property']");
        private readonly By SubPropertyInput = By.Id("sub-property");
        private readonly By StreetInput = By.Id("street");
        private readonly By StreetLabel = By.XPath("//label[text()='Street']");
        private readonly By TownInput = By.Id("town");
        private readonly By TownLabel = By.XPath("//label[text()='Town']");
        private readonly By PostcodeInput = By.Id("postcode");
        private readonly By PostcodeLabel = By.XPath("//label[text()='Postcode']");
        private readonly By CountryInput = By.Id("country");
        private readonly By CountryLabel = By.XPath("//label[text()='Country']");
        private readonly By BackBtn = By.XPath("//button[text()='Back']");
        private readonly By CreateBtn = By.XPath("//button[text()='Create']");
        private const string LoadingData = "//div[@class='loading-data']";


        //DYNAMIC LOCATOR
        private const string AddressSite = "//div[contains(text(),'{0}')]";
        private const string SiteTypeOption = "//label[text()='Site Type']/following-sibling::select/option[text()='{0}']";

        public PartySiteAddressPage IsOnPartySiteAddressPage()
        {
            Assert.IsTrue(IsControlDisplayed(SearchInput));
            Assert.IsTrue(IsControlDisplayed(SearchBtn));
            Assert.IsTrue(IsControlDisplayed(ResultField));
            Assert.IsTrue(IsControlDisplayed(MapContainer));
            Assert.IsTrue(IsControlDisplayed(CancelBtn));
            Assert.False(IsControlEnabled(NextBtn));
            Assert.IsTrue(IsControlDisplayed(CreateManuallyBtn));
            Assert.IsTrue(IsControlDisplayed(CreateNonGeographicalAddress));
            return this;
        }
        public PartySiteAddressPage InputTextToSearchBar(String value)
        {
            WaitUtil.WaitForElementVisible(SearchInput);
            ClickOnElement(SearchInput);
            SendKeys(SearchInput, value);
            return this;
        }

        public PartySiteAddressPage ClickSearchBtn()
        {
            WaitUtil.WaitForElementVisible(SearchBtn);
            ClickOnElement(SearchBtn);
            return this;
        }

        public PartySiteAddressPage VerifySearchedAddressAppear(String add)
        {
            WaitUtil.WaitForElementVisible(AddressSite, add);
            Assert.IsTrue(IsControlDisplayed(AddressSite, add));
            return this;
        }

        public PartySiteAddressPage ClickOnSearchedAddress(string add)
        {
            WaitUtil.WaitForElementClickable(AddressSite, add);
            ClickOnElement(AddressSite, add);
            return this;
        }

        public PartySiteAddressPage VerifyNextButtonAvalable()
        {
            WaitUtil.WaitForElementClickable(NextBtn);
            Assert.True(IsControlEnabled(NextBtn));
            return this;
        }

        public PartySiteAddressPage ClickOnNextButton()
        {
            WaitUtil.WaitForElementClickable(NextBtn);
            ClickOnElement(NextBtn);
            return this;
        }

        public PartySiteAddressPage ClickOnCreateManuallyBtn()
        {
            ClickOnElement(CreateManuallyBtn);
            return this;
        }

        public PartySiteAddressPage IsCheckAddressDetailScreen()
        {
            WaitUtil.WaitForElementVisible(SiteNameLabel);
            Assert.IsTrue(IsControlDisplayed(SiteNameInput));
            Assert.IsTrue(IsControlDisplayed(SiteAbvInput));
            Assert.IsTrue(IsControlDisplayed(SiteTypeDropdown));
            Assert.IsTrue(IsControlDisplayed(PropertyInput));
            Assert.IsTrue(IsControlDisplayed(SubPropertyInput));
            Assert.IsTrue(IsControlDisplayed(StreetInput));
            Assert.IsTrue(IsControlDisplayed(TownInput));
            ScrollDownToElement(CountryInput);
            WaitUtil.WaitForElementVisible(CountryInput);
            Assert.IsTrue(IsControlDisplayed(PostcodeInput));
            Assert.IsTrue(IsControlDisplayed(CountryInput));
            Assert.IsTrue(IsControlDisplayed(BackBtn));
            WaitUtil.WaitForElementVisible(CreateBtn);
            Assert.IsTrue(IsControlDisplayed(CreateBtn));
            //Verify madatory field
            Assert.AreEqual(GetAttributeValue(SiteNameLabel, "class"), "control-label");
            Assert.AreEqual(GetAttributeValue(SiteNameInput, "class"), "form-control");
            Assert.AreEqual(GetAttributeValue(PropertyLabel, "class"), "control-label");
            Assert.AreEqual(GetAttributeValue(PropertyInput, "class"), "form-control");
            Assert.AreEqual(GetAttributeValue(StreetLabel, "class"), "control-label");
            Assert.AreEqual(GetAttributeValue(StreetInput, "class"), "form-control");
            Assert.AreEqual(GetAttributeValue(TownLabel, "class"), "control-label");
            Assert.AreEqual(GetAttributeValue(TownInput, "class"), "form-control");
            Assert.AreEqual(GetAttributeValue(PostcodeLabel, "class"), "control-label");
            Assert.AreEqual(GetAttributeValue(PostcodeInput, "class"), "form-control");
            Assert.AreEqual(GetAttributeValue(CountryLabel, "class"), "control-label");
            Assert.AreEqual(GetAttributeValue(CountryInput, "class"), "form-control");
            return this;
        }

        public PartySiteAddressPage SendKeyInSiteNameInput(string siteName)
        {
            SendKeys(SiteNameInput, siteName);
            return this;
        }

        public PartySiteAddressPage VerifyCreateBtnDisabled()
        {
            Assert.AreEqual(GetAttributeValue(CreateBtn, "disabled"), "true");
            return this;
        }

        public PartySiteAddressPage InputAllDataInCheckAddressDetailScreen(AddressDetailModel addressDetail)
        {
            SendKeys(PropertyInput, addressDetail.Property.ToString());
            SendKeys(StreetInput, addressDetail.Street);
            SendKeys(TownInput, addressDetail.Town);
            SendKeys(PostcodeInput, addressDetail.PostCode);
            SendKeys(CountryInput, addressDetail.Country);
            return this;
        }

        public PartySiteAddressPage ClickCreateBtn()
        {
            ClickOnElement(CreateBtn);
            return this;
        }

        public PartySiteAddressPage WaitForLoadingIconInvisiable()
        {
            WaitUtil.WaitForElementInvisible(LoadingData);
            return this;
        }
        
    }


}