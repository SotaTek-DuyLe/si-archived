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
        public readonly By TimeBandInput = By.XPath("//div[@id='details-tab']//select[@id='timeband.id']");
        public readonly By UseRoundScheduleRadio = By.XPath("//div[@id='details-tab']//input[@id='rd-round']");
        public readonly By RoundSelect = By.XPath("//div[@id='div-round']//echo-select[contains(@params, 'round')]//select");
        public readonly By RoundLegSelect = By.XPath("//div[@id='div-roundLeg']//select[@id='roundLeg.id']");
        private readonly By title = By.XPath("//span[text()='Service Task Schedule']");
        private readonly By serviceTaskLink = By.XPath("//span[text()='Service Task Schedule']/following-sibling::span");
        private readonly By roundDd = By.XPath("//div[@id='div-round']//label[contains(string(), 'Round')]/following-sibling::echo-select/select");

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
            Assert.AreEqual(CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), GetAttributeValue(StartDateInput, "value"));
            Assert.AreEqual("01/01/2050", GetAttributeValue(EndDateInput, "value"));
            return this;
        }


    }
}
