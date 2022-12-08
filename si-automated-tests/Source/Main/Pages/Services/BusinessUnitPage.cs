﻿using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class BusinessUnitPage : BasePage
    {
        private By businessUnitInput = By.Id("businessUnit");
        private readonly By title = By.XPath("//span[contains(string(), 'Business Unit:')]");

        public BusinessUnitPage()
        {
            SwitchToLastWindow();
        }
        [AllureStep]
        public BusinessUnitPage InputBusinessName(string name)
        {
            SendKeys(businessUnitInput, name);
            return this;
        }

        [AllureStep]
        public BusinessUnitPage IsBusinessUnitPage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Business Unit?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public BusinessUnitPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectBusinessUnits)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public BusinessUnitPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public BusinessUnitPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public BusinessUnitPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
