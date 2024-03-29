﻿using System;
using NUnit.Allure.Attributes;
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

        [AllureStep]
        public AddLocationPage WaitForAddLocationPageLoaded()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(title);
            return this;
        }
        [AllureStep]
        public AddLocationPage VerifyDisplayPartySitePage()
        {
            WaitUtil.WaitForElementVisible(nameInput);
            Assert.IsTrue(IsControlDisplayed(nameInput));
            Assert.IsTrue(IsControlDisplayed(activeCheckbox));
            Assert.IsTrue(IsControlDisplayed(clientRefInput));
            //Mandatory field
            Assert.AreEqual(GetCssValue(nameInput, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }
        [AllureStep]
        public AddLocationPage InputName(string nameValue)
        {
            SendKeys(nameInput, nameValue);
            return this;
        }
        [AllureStep]
        public AddLocationPage SelectActiveCheckbox()
        {
            ClickOnElement(activeCheckbox);
            return this;
        }
        [AllureStep]
        public AddLocationPage VerifyActiveCheckboxSelected()
        {
            Assert.IsTrue(IsElementSelected(activeCheckbox));
            return this;
        }
        [AllureStep]
        public AddLocationPage InputClientName(string client)
        {
            SendKeys(clientRefInput, client);
            return this;
        }
    }
}
