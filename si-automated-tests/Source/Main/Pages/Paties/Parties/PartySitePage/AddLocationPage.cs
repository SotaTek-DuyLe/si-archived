using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage
{
    public class AddLocationPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Weighbridge Site Location']");
        private readonly By nameInput = By.CssSelector("input#location-name");
        private readonly By activeCheckbox = By.XPath("//label[text()='Active']/following-sibling::div/input");
        private readonly By clientRefInput = By.CssSelector("input#client-ref");

        public AddLocationPage WaitForAddLocationPageLoaded()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        public AddLocationPage VerifyDisplayPartySitePage()
        {
            Assert.IsTrue(IsControlDisplayed(nameInput));
            Assert.IsTrue(IsControlDisplayed(activeCheckbox));
            Assert.IsTrue(IsControlDisplayed(clientRefInput));
            //Mandatory field
            Assert.AreEqual(GetCssValue(nameInput, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }

        public AddLocationPage InputName(string nameValue)
        {
            SendKeys(nameInput, nameValue);
            return this;
        }

        public AddLocationPage SelectActiveCheckbox()
        {
            ClickOnElement(activeCheckbox);
            return this;
        }

        public AddLocationPage VerifyActiveCheckboxSelected()
        {
            Assert.IsTrue(IsElementSelected(activeCheckbox));
            return this;
        }

        public AddLocationPage InputClientName(string client)
        {
            SendKeys(clientRefInput, client);
            return this;
        }
    }
}
