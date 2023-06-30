using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    public class CreatePartyPage : BasePage
    {
        private const string partyTitle = "//h4[text()='Party']";
        private const string PartyNameInput = "//label[text()='Party Name']/following-sibling::div/input";
        private const string ContractDropdown = "//label[text()='Contract']/following-sibling::div/select";
        private const string StartDateInput = "//label[text()='Start Date']/following-sibling::div//input";
        private const string AllPartyTypes = "//label[text()='All Party Types']";
        private const string SaveBtn = "//button[@title='Save']";
        private const string SaveAndCloseBtn = "//button[@title='Save and Close']";
        private const string CloseWithoutSaveingBtn = "//button[@title='Close Without Saving']";
        private const string CalenderBtn = "//span[contains(@class, 'glyphicon-calendar')]";

        //DYNAMIC LOCATOR
        private const string ContractOption = "//label[text()='Contract']/following-sibling::div/select/option[{0}]";
        private const string PartyTypeValue = "//label[text()='All Party Types']/following-sibling::div/div[{0}]//span";
        private const string AnyDay = "//td[contains(@class, 'day') and text()='{0}']";
        private const string AnyMessage = "//div[text()='{0}']";

        public By selectContract = By.Id("party-contract");

        [AllureStep]
        public CreatePartyPage IsCreatePartiesPopup(string contractDefault)
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(partyTitle);
            Assert.IsTrue(IsControlDisplayed(PartyNameInput));
            Assert.IsTrue(IsControlDisplayed(ContractDropdown));
            Assert.IsTrue(IsControlDisplayed(StartDateInput));
            Assert.IsTrue(IsControlDisplayed(AllPartyTypes));
            Assert.IsTrue(IsControlDisplayed(SaveBtn));
            Assert.IsTrue(IsControlDisplayed(SaveAndCloseBtn));
            Assert.IsTrue(IsControlDisplayed(CloseWithoutSaveingBtn));
            //Verify defaul value
            Assert.AreEqual(GetFirstSelectedItemInDropdown(ContractDropdown), contractDefault);
            string dateNow = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT).Replace('-','/');
            Assert.AreEqual(GetAttributeValue(StartDateInput, "value"), dateNow);
            return this;
        }
        [AllureStep]
        public CreatePartyPage ClickContractDropdown()
        {
            ClickOnElement(ContractDropdown);
            return this;
        }
        [AllureStep]
        public CreatePartyPage VerifyContractDropdownVlues()
        {
            IList<IWebElement> optionList = GetSelectElement(selectContract).Options;
            List<string> optionListText = new List<string>();
            foreach(IWebElement option in optionList)
            {
                optionListText.Add(option.Text);
            }
            List<string> contracts = new List<string> { "Select...", Constants.Contract.Commercial, Constants.Contract.Municipal, Constants.Contract.Gold, Constants.Contract.Brist };
            CollectionAssert.IsSubsetOf(contracts, optionListText);
            return this;
        }
        [AllureStep]
        public CreatePartyPage VerifyAllPartyTypes()
        {
            Assert.AreEqual(GetElementText(String.Format(PartyTypeValue, 1)), "Customer");
            Assert.AreEqual(GetElementText(String.Format(PartyTypeValue, 2)), "Haulier");
            Assert.AreEqual(GetElementText(String.Format(PartyTypeValue, 3)), "Supplier");
            return this;
        }
        [AllureStep]
        public CreatePartyPage SendKeyToThePartyInput(string partyName)
        {
            SendKeys(PartyNameInput, partyName);
            return this;
        }
        [AllureStep]
        public CreatePartyPage SelectPartyType(int number)
        {
            ClickOnElement(string.Format(PartyTypeValue, number));
            return this;
        }
        [AllureStep]
        public CreatePartyPage SelectContract(int optionNumber)
        {
            ClickOnElement(ContractDropdown);
            ClickOnElement(String.Format(ContractOption, optionNumber));
            return this;
        }
        [AllureStep]
        public CreatePartyPage SelectStartDate(int day)
        {
            ClickOnElement(CalenderBtn);
            string nowSubDay = CommonUtil.GetLocalDayMinusDay(day);
            ClickOnElement(String.Format(AnyDay, nowSubDay));
            return this;
        }
        [AllureStep]
        public CreatePartyPage SelectStartDatePlusOneDay()
        {
            string nowPlusOneDay = CommonUtil.GetUtcTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            SendKeys(StartDateInput, nowPlusOneDay);
            return this;
        }
        [AllureStep]
        public CreatePartyPage VerifyDisplayErrorMessage(string errorMessage)
        {
            Assert.IsTrue(IsControlDisplayed(String.Format(AnyMessage, errorMessage)));
            return this;
        }
    }
}
