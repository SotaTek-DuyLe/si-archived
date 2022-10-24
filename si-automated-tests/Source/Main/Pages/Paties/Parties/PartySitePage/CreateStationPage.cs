using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage
{
    public class CreateStationPage : BasePage
    {
        private readonly By nameTitle = By.XPath("//label[text()='Name']");
        private readonly By nameInput = By.CssSelector("input#location-name");
        private readonly By defaultTicketTypeDd = By.CssSelector("select#ticket-type");
        private readonly By referenceInput = By.CssSelector("input#station-reference");
        private readonly By prefixInput = By.CssSelector("input#station-prefix");
        private readonly By partyInput = By.CssSelector("input#station-party");
        private readonly By pinInput = By.CssSelector("input#station-pin");
        private readonly By timeZoneTitle = By.XPath("//label[text()='Time Zone']");
        private readonly By timeZoneDd = By.CssSelector("select#time-zone");
        private readonly By allowAddNewVehicleCheckbox = By.XPath("//label[text()='Allow Add New Vehicle']/following-sibling::div/input");
        private readonly By allowAddNewCustomerCheckbox = By.XPath("//label[text()='Allow Add New Customer']/following-sibling::div/input");
        private readonly By activeCheckbox = By.XPath("//label[@for='is-active']/following-sibling::div/input");
        private readonly By allowStotedTareCheckbox = By.CssSelector("input#allowStoredTare");
        private readonly By useAssetTypeCheckbox = By.CssSelector("input#useAssetType");
        private readonly By tarePromtDayInput = By.CssSelector("input#tarePromptDays");

        //DYNAMIC
        private const string title = "//h4[text()='{0}']";
        private const string timezoneOption = "//select[@id='time-zone']/option[text()='{0}']";
        private const string defaultTicketOption = "//select[@id='ticket-type']/option[text()='{0}']";

        [AllureStep]
        public CreateStationPage WaitForCreateStationPageLoaded(string siteName)
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(title, siteName);
            return this;
        }
        [AllureStep]
        public CreateStationPage IsCreateStationPage()
        {
            Assert.IsTrue(IsControlDisplayed(nameInput));
            Assert.IsTrue(IsControlDisplayed(defaultTicketTypeDd));
            Assert.IsTrue(IsControlDisplayed(referenceInput));
            Assert.IsTrue(IsControlDisplayed(prefixInput));
            Assert.IsTrue(IsControlDisplayed(partyInput));
            Assert.IsTrue(IsControlDisplayed(pinInput));
            Assert.IsTrue(IsControlDisplayed(timeZoneDd));
            Assert.IsTrue(IsControlDisplayed(allowAddNewVehicleCheckbox));
            Assert.IsTrue(IsControlDisplayed(allowAddNewCustomerCheckbox));
            Assert.IsTrue(IsControlDisplayed(allowStotedTareCheckbox));
            Assert.IsTrue(IsControlDisplayed(activeCheckbox));
            Assert.IsTrue(IsControlDisplayed(useAssetTypeCheckbox));
            Assert.IsTrue(IsControlDisplayed(tarePromtDayInput));
            //Party - readonly field
            Assert.AreEqual(GetAttributeValue(partyInput, "readonly"), "true");
            //Active - checked
            Assert.IsTrue(IsCheckboxChecked(activeCheckbox));
            //Promp
            Assert.AreEqual(GetAttributeValue(tarePromtDayInput, "value"), "0");
            //Default timezone
            Assert.AreEqual(GetFirstSelectedItemInDropdown(timeZoneDd), "Select...");
            //Defaul ticket
            Assert.AreEqual(GetFirstSelectedItemInDropdown(defaultTicketTypeDd), "Select...");
            //Mandatory field
            Assert.AreEqual(GetCssValue(nameInput, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(nameTitle, "color"), CommonConstants.BoderColorMandatory_Rbga);
            Assert.AreEqual(GetCssValue(timeZoneTitle, "color"), CommonConstants.BoderColorMandatory_Rbga);
            Assert.AreEqual(GetCssValue(timeZoneDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }
        [AllureStep]
        public CreateStationPage VerifyDisplayErrorMesMissingTimezone()
        {
            VerifyDisplayToastMessage(MessageRequiredFieldConstants.MissingTimezoneMessage);
            //Verify color
            //Assert.AreEqual(GetCssValue(errorMesRequiredTimeZone, "color"), "rgba(159, 139, 64, 1)");
            return this;
        }
        [AllureStep]
        public CreateStationPage SelectTimezone(string timezoneInput)
        {
            ClickOnElement(timeZoneDd);
            ClickOnElement(timezoneOption, timezoneInput);
            //Verify value is Selected
            Assert.AreEqual(GetFirstSelectedItemInDropdown(timeZoneDd), timezoneInput);
            return this;
        }
        [AllureStep]
        public CreateStationPage InputName(string stationName)
        {
            SendKeys(nameInput, stationName);
            return this;
        }
        [AllureStep]
        public CreateStationPage SelectDefaultTicket(string ticketOption)
        {
            ClickOnElement(defaultTicketTypeDd);
            //Verify
            Assert.IsTrue(IsControlDisplayed(defaultTicketOption, "Select..."));
            Assert.IsTrue(IsControlDisplayed(defaultTicketOption, "Incoming"));
            Assert.IsTrue(IsControlDisplayed(defaultTicketOption, "Neutral"));
            Assert.IsTrue(IsControlDisplayed(defaultTicketOption, "Outbound"));
            //Click any option
            ClickOnElement(defaultTicketOption, ticketOption);
            return this;
        }
    }
}
