using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RegionDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Region']");

        //DYNAMIC
        private readonly string regionName = "//h5[text()='{0}']";

        public RegionDetailPage IsRegionDetailPage(string regionValue)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(regionName, regionValue);
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Region?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public RegionDetailPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectRegion)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public RegionDetailPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public RegionDetailPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public RegionDetailPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
