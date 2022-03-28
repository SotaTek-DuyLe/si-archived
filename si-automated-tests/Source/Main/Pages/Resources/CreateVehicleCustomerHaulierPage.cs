using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class CreateVehicleCustomerHaulierPage : BasePage
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
        private readonly By messageHumanResourceRequired = By.XPath("//div[text()='Resource is required']");

        //DYNAMIC LOCATOR
        private const string AnyLiOption = "//li[contains(text(), '{0}')]";

        public CreateVehicleCustomerHaulierPage IsCreateVehicleCustomerHaulierPage()
        {
            WaitUtil.WaitForElementVisible(title);
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
            //Mandatory field
            Assert.AreEqual(GetCssValue(resourceInput, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(customerInput, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(haulierInput, "border-color"), CommonConstants.BoderColorMandatory);
            //Defaul value
            Assert.AreEqual(GetAttributeValue(hireStartInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(hireEndInput, "value"), CommonConstants.EndDateAgreement);

            return this;
        }

        public CreateVehicleCustomerHaulierPage InputCustomer(string customerName)
        {
            SendKeys(customerInput, customerName);
            WaitUtil.WaitForElementVisible(AnyLiOption, customerName);
            ClickOnElement(AnyLiOption, customerName);
            return this;
        }

        public CreateVehicleCustomerHaulierPage InputHaulier(string haulierName)
        {
            SendKeys(haulierInput, haulierName);
            WaitUtil.WaitForElementVisible(AnyLiOption, haulierName);
            ClickOnElement(AnyLiOption, haulierName);
            return this;
        }

        public CreateVehicleCustomerHaulierPage InputHumanResourceName(string resourceName)
        {
            SendKeys(resourceInput, resourceName);
            return this;
        }

        public CreateVehicleCustomerHaulierPage VerifyNotDisplaySuggestionInResourceInput()
        {
            Assert.IsTrue(IsControlUnDisplayed(suggestionResource));
            //Verify field is highlighted in red
            Assert.AreEqual(GetCssValue(resourceInput, "border-color"), CommonConstants.BoderColorResourceInputWB);
            return this;
        }

        public CreateVehicleCustomerHaulierPage VerifyDisplayResourceRequiredMessage()
        {
            Assert.IsTrue(IsControlDisplayed(messageHumanResourceRequired));
            return this;
        }
    }
}
