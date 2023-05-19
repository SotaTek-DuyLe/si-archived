using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundSchedulePage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//span[text()='Round Schedule']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By ScheduleTab = By.XPath("//a[@aria-controls='schedule-tab']");
        public readonly By StartDateInput = By.XPath("//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//input[@id='endDate.id']");
        public readonly By SeasonSelect = By.XPath("//select[@id='season.id']");
        private readonly By periodTimeButtons = By.XPath("//div[@id='schedule-tab']//div[@role='group']//button");
        public readonly By weeklyFrequencySelect = By.XPath("//div[@id='schedule-tab']//select[@id='weekly-frequency']");
        public readonly By RoundScheduleTitle = By.XPath("//h5[@data-bind='text: roundSchedule']");
        public readonly By RoundScheduleStatus = By.XPath("//h5[@id='header-status']//span[contains(@data-bind, \"visible: displayedStatusType() === 'status'\")]");
        public readonly By RetireButton = By.XPath("//button[@title='Retire']");
        public readonly By OKButton = By.XPath("//div[@class='modal-dialog']//button[contains(string(), 'OK')]");
        public readonly By RetireConfirmTitle = By.XPath("//div[@class='modal-dialog']//h4");

        [AllureStep]
        public RoundSchedulePage ClickPeriodTimeButton(string period)
        {
            IWebElement webElement = GetAllElements(periodTimeButtons).FirstOrDefault(x => x.Text.Contains(period));
            ClickOnElement(webElement);
            return this;
        }
        [AllureStep]
        public RoundSchedulePage ClickDayButtonOnWeekly(string day)
        {
            string xpath = $"//div[@id='schedule-tab']//div[contains(@data-bind, 'foreach: dayButtons')]//button[contains(string(), '{day}')]";
            ClickOnElement(By.XPath(xpath));
            Thread.Sleep(200);
            return this;
        }

        [AllureStep]
        public RoundSchedulePage IsRoundSchedulePage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Round Schedule?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public RoundSchedulePage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectRoundSchedule)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public RoundSchedulePage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public RoundSchedulePage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public RoundSchedulePage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }
    }
}
