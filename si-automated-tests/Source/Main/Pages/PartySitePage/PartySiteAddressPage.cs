using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

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

        //DYNAMIC LOCATOR
        private const string AddressSite = "//div[contains(text(),'{0}')]";

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
    }
}
