using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class ResoureDetailPage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//h4[text()='RESOURCE']");

        [AllureStep]
        public ResoureDetailPage IsResourceDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }
        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Resource?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");
        private readonly By ThirdPartyCheckbox = By.XPath("//input[@id='third-party']");
        public readonly By SupplierSelect = By.XPath("//select[@id='supplier']");

        public ResoureDetailPage SelectThirdPartyCheckbox(bool isSelect)
        {
            if (isSelect)
            {
                if (!GetCheckboxValue(ThirdPartyCheckbox)) ClickOnElement(ThirdPartyCheckbox);
            }
            else
            {
                if (GetCheckboxValue(ThirdPartyCheckbox)) ClickOnElement(ThirdPartyCheckbox);
            }
            return this;
        }
        #endregion

        #region Shift Schedule tab
        public readonly By ShiftScheduleTab = By.XPath("//a[@aria-controls='shiftSchedules-tab']");
        public readonly By AddNewShiftScheduleButton = By.XPath("//div[@id='shiftSchedules-tab']//button[text()='Add New Item']");
        #endregion

        [AllureStep]
        public ResoureDetailPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectResource)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ResoureDetailPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public ResoureDetailPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public ResoureDetailPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
