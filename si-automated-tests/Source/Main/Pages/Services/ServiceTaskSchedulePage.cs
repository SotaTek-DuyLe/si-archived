using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceTaskSchedulePage : BasePageCommonActions
    {
        public readonly By StartDateInput = By.XPath("//div[@id='details-tab']//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//div[@id='details-tab']//input[@id='endDate.id']");
        public readonly By TimeBandInput = By.XPath("//div[@id='details-tab']//select[@id='timeBand.id']");
        public readonly By UseRoundScheduleRadio = By.XPath("//div[@id='details-tab']//input[@id='rd-round']");
        public readonly By RoundSelect = By.XPath("//div[@id='div-round']//echo-select[contains(@params, 'round')]//select");
        public readonly By RoundLegSelect = By.XPath("//div[@id='div-roundLeg']//select[@id='roundLeg.id']");
        private readonly By title = By.XPath("//span[text()='Service Task Schedule']");
        private readonly By serviceTaskLink = By.XPath("//span[text()='Service Task Schedule']/following-sibling::span");
        private readonly By roundDd = By.XPath("//div[@id='div-round']//label[contains(string(), 'Round')]/following-sibling::echo-select/select");
        private readonly By statusText = By.XPath("//h5[@id='header-status']/span[1]");
        private readonly By firstTimeBand = By.XPath("//select[@id='timeband.id']/option[1]");

        [AllureStep]
        public string GetServiceTaskScheduleID(string url)
        {
            return GetCurrentUrl().Replace(url, "");
        }

        [AllureStep]
        public ServiceTaskSchedulePage IsServiceTaskSchedule()
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(title));
            WaitUtil.WaitForElementVisible(serviceTaskLink);
            return this;
        }

        [AllureStep]
        public ServicesTaskPage ClickOnServiceTaskLink()
        {
            ClickOnElement(serviceTaskLink);
            return PageFactoryManager.Get<ServicesTaskPage>();
        }

        //DETAIL TAB
        private readonly By detailTab = By.CssSelector("a[aria-controls=details-tab]");

        [AllureStep]
        public ServiceTaskSchedulePage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public string GetRoundNameDisplayed()
        {
            return GetFirstSelectedItemInDropdown(roundDd);
        }

        [AllureStep]
        public ServiceTaskSchedulePage VerifyStartDateAndEndDate()
        {
            Assert.AreEqual(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1), GetAttributeValue(StartDateInput, "value"));
            Assert.AreEqual("01/01/2050", GetAttributeValue(EndDateInput, "value"));
            return this;
        }

        [AllureStep]
        public ServiceTaskSchedulePage VerifyEndDateIsDisabled()
        {
            Assert.AreEqual("true", GetAttributeValue(EndDateInput, "disabled"));
            return this;
        }

        [AllureStep]
        public ServiceTaskSchedulePage VerifyRoundValue(string roundValueExp)
        {
            Assert.AreEqual(roundValueExp, GetFirstSelectedItemInDropdown(RoundSelect), "Wrong round value");
            return this;
        }

        [AllureStep]
        public ServiceTaskSchedulePage VerifyStatusOfServiceTaskSchedule(string statusValue)
        {
            Assert.AreEqual(statusValue.ToLower(), GetElementText(statusText).ToLower(), "Status is not correct");
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Service Task Schedule?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public ServiceTaskSchedulePage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectServiceTaskSchedule)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public ServiceTaskSchedulePage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public ServiceTaskSchedulePage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public ServiceTaskSchedulePage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }

        [AllureStep]
        public ServiceTaskSchedulePage SelectTimeBand()
        {
            ClickOnElement(TimeBandInput);
            ClickOnElement(firstTimeBand);
            return this;
        }


    }
}
