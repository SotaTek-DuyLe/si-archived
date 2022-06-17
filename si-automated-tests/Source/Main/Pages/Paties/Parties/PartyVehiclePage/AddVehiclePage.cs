using System;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyVehiclePage
{
    public class AddVehiclePage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='WEIGHBRIDGE VEHICLE CUSTOMER HAULIER']");
        private readonly By resourceInput = By.XPath("//label[text()='Resource']/following-sibling::div//input[@placeholder='Start typing for suggestions...']");
        private readonly By customerInput = By.XPath("//label[text()='Customer']/following-sibling::div//input[@placeholder='Start typing for suggestions...']");
        private readonly By haulierInput = By.XPath("//label[text()='Haulier']/following-sibling::div//input[@placeholder='Start typing for suggestions...']");
        private readonly By hireStartInput = By.CssSelector("input#start-date");
        private readonly By hireEndInput = By.CssSelector("input#end-date");
        private readonly By suspendedCheckbox = By.CssSelector("input#is-suspended");
        private readonly By suspendedReasonTextarea = By.CssSelector("textarea#suspended-reason");
        private readonly By defaultSiteSelect = By.CssSelector("select#default-site");
        private readonly By defaultProductSelect = By.CssSelector("select#default-product");
        private readonly By suggestionResource = By.XPath("//label[text()='Resource']/following-sibling::div//ul/li");
        private readonly By suggestionHaulier = By.XPath("//label[text()='Haulier']/following-sibling::div//ul/li");
        private readonly By messageHumanResourceRequired = By.XPath("//div[text()='Resource is required']");

        //DYNAMIC LOCATOR
        private const string DefaultCustomerAddressOption = "//select[@id='default-site']/option[text()='{0}']";
        private const string AnyLiOption = "//li[contains(text(), '{0}')]";

        public AddVehiclePage IsCreateVehicleCustomerHaulierPage()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(resourceInput);
            Assert.IsTrue(IsControlDisplayed(title));
            Assert.IsTrue(IsControlDisplayed(resourceInput));
            Assert.IsTrue(IsControlDisplayed(customerInput));
            Assert.IsTrue(IsControlDisplayed(haulierInput));
            Assert.IsTrue(IsControlDisplayed(hireStartInput));
            Assert.IsTrue(IsControlDisplayed(hireEndInput));
            Assert.IsTrue(IsControlDisplayed(suspendedCheckbox));
            Assert.IsTrue(IsControlDisplayed(suspendedReasonTextarea));
            Assert.IsTrue(IsControlDisplayed(defaultSiteSelect));
            Assert.IsTrue(IsControlDisplayed(defaultProductSelect));
            
            return this;
        }

        public AddVehiclePage VerifyDefaultMandatoryFieldAndDefaultValue(string partyName)
        {
            //Mandatory field
            Assert.AreEqual(GetCssValue(resourceInput, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(haulierInput, "border-color"), CommonConstants.BoderColorMandatory);
            //Defaul value
            Assert.AreEqual(GetAttributeValue(hireStartInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(hireEndInput, "value"), CommonConstants.EndDateAgreement);
            WaitUtil.WaitForValueInputDisplayed(customerInput);
            Regex regex = new Regex(@"\s");
            string[] splits = regex.Split(GetAttributeValue(customerInput, "value"));
            string customerInputVal = splits.FirstOrDefault();
            Assert.AreEqual(customerInputVal, partyName);
            
            return this;
        }

        public AddVehiclePage ClickDefaultCustomerAddressDropdownAndVerify(string addressSite)
        {
            ClickOnElement(defaultSiteSelect);
            //Verify
            Assert.IsTrue(IsControlDisplayed(DefaultCustomerAddressOption, "Select..."));
            Assert.IsTrue(IsControlDisplayed(DefaultCustomerAddressOption, addressSite));
            return this;
        }

        public AddVehiclePage VerifyDisplayResourceRequiredMessage()
        {
            Assert.IsTrue(IsControlDisplayed(messageHumanResourceRequired));
            WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.ResourceRequiredMessage);
            return this;
        }

        public AddVehiclePage InputResourceName(string resourceValue)
        {
            SendKeys(resourceInput, resourceValue);
            return this;
        }

        public AddVehiclePage SelectResourceName(string resourceValue)
        {
            WaitUtil.WaitForElementVisible(AnyLiOption, resourceValue);
            ClickOnElement(AnyLiOption, resourceValue);
            return this;
        }

        public AddVehiclePage InputHaulierName(string haulierName)
        { 
            SendKeys(haulierInput, haulierName);
            return this;
        }

        public AddVehiclePage SelectHaulierName(string haulierName)
        {
            WaitUtil.WaitForElementVisible(AnyLiOption, haulierName);
            ClickOnElement(AnyLiOption, haulierName);
            return this;
        }

        public AddVehiclePage VerifyNotDisplaySuggestionInResourceInput()
        {
            Assert.IsTrue(IsControlUnDisplayed(suggestionResource));
            //Verify field is highlighted in red
            return this;
        }

        public AddVehiclePage VerifyNotDisplaySuggestionInHaulierInput()
        {
            Assert.IsTrue(IsControlUnDisplayed(suggestionHaulier));
            //Verify field is highlighted in red
            return this;
        }
    }
}
