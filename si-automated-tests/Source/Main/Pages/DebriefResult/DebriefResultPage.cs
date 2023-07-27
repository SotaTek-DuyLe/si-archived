using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.DebriefResult
{
    public class DebriefResultPage : BasePageCommonActions
    {
        public readonly By ActivityHeader = By.XPath("//div[@class='header-node']//label//span[text()='ACTIVITY']");
        public readonly By ConfirmationHeader = By.XPath("//div[@class='children-container']//span[text()='CONFIRMATIONS']");

        public readonly By BinLiftSecondRow = By.XPath("(//div[@class='gps-event-block'])[2]");
        public readonly By BinLiftFirstRow = By.XPath("(//div[@class='gps-event-block'])[1]");
        public readonly By FirstService1100L = By.XPath("(//div[contains(@data-bind, 'groupedTaskLines')]//div[contains(@data-bind, 'parentTaskLine.assetType') and text()='1100L'])[1]");
        public readonly By UnmatchButton = By.XPath("//td[@class='actions-list']//button[contains(text(), 'Unmatch')]");
        public readonly By MatchButton = By.XPath("//td[@class='actions-list']//button[contains(text(), 'Match')]");
        public readonly By UnknownButton = By.XPath("//td[@class='actions-list']//button[contains(text(), 'Unknown')]");
        public readonly By FirstTaskRatio = By.XPath("(//div[@id='unmatched-tasks']//div[@data-bind='text: taskRatio'])[1]");
        private readonly By roundBtn = By.XPath("//span[text()='ROUND']/parent::label");
        private readonly By activityBtn = By.XPath("//span[text()='ACTIVITY']/parent::label");
        private readonly By worksheetBtn = By.XPath("//span[text()='WORKSHEET']/parent::label");
        private readonly By activityTab = By.XPath("//span[text()='ACTIVITY']/parent::label");
        private readonly By eventTab = By.XPath("//span[text()='EVENTS']/parent::h4");
        private readonly By roundInstanceEventImg = By.XPath("//img[contains(@src, '../content/images/round-instance-event.svg')]/parent::td");
        private readonly By inspectionTab = By.XPath("//span[contains(string(), 'INSPECTIONS')]/parent::h4");
        private readonly By selectResolutionCodes = By.CssSelector("select[id='resolutionCode.id']");
        private readonly By reasonTakenLabel = By.XPath("//label[contains(string(), 'REASON/ACTION TAKEN')]");
        private readonly By postBtn = By.CssSelector("button[title='Post']");
        private readonly By unPostBtn = By.CssSelector("button[title='Unpost']");

        //DYNAMIC
        private readonly string resolutionCodeOption = "//select[@id='resolutionCode.id']/option[text()='{0}']";

        [AllureStep]
        public DebriefResultPage WaitForDebriefLoaded()
        {
            try
            {
                WaitUtil.WaitForAllElementsInvisible60("//div[contains(@data-bind,'shield: gpsEventMatchForm.isLoading')]");
                WaitUtil.WaitForPageLoaded();
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Loading icon doesn't disappear after 60 seconds");
            }
            return this;
        }

        [AllureStep]
        public DebriefResultPage DragSecondBinLiftToTaskLine()
        {
            SleepTimeInMiliseconds(2000);
            IWebElement row = GetElement(BinLiftSecondRow);
            IWebElement service = GetElement(FirstService1100L);
            DragAndDropByJS(row, service);
            return this;
        }

        [AllureStep]
        public DebriefResultPage DragFirstBinLiftToTaskLine()
        {
            SleepTimeInMiliseconds(2000);
            IWebElement row = GetElement(BinLiftFirstRow);
            IWebElement service = GetElement(FirstService1100L);
            DragAndDropByJS(row, service);
            return this;
        }

        [AllureStep]
        public DebriefResultPage VerifyTaskLineStateIsCompleted()
        {
            IWebElement img = GetElement(By.XPath("(//td[@id='taskLines']//img[contains(@data-bind, 'taskStateIcon')])[1]"));
            string src = img.GetAttribute("src");
            Assert.IsTrue(src.Contains("coretaskstate/s3.svg"), "Expected " + src + " to contain s3.svg");
            return this;
        }

        [AllureStep]
        public DebriefResultPage VerifyBinLiftStateIsNotCompleted()
        {
            IWebElement img = GetElement(By.XPath("(//div[@class='gps-event-block'])[1]//img"));
            string src = img.GetAttribute("src");
            Assert.IsTrue(src.Contains("gpseventmatch/red-cross.svg"));
            return this;
        }

        [AllureStep]
        public DebriefResultPage VerifyBinLiftStateIsCompleted()
        {
            IWebElement img = GetElement(By.XPath("(//div[@class='gps-event-block'])[1]//img"));
            string src = img.GetAttribute("src");
            Assert.IsTrue(src.Contains("gpseventmatch/green-tick.svg"));
            return this;
        }

        [AllureStep]
        public DebriefResultPage VerifyFirstTaskRatio()
        {
            Assert.AreEqual(GetElementText(FirstTaskRatio), "0 / 1");
            return this;
        }

        [AllureStep]
        public DebriefResultPage VerifyCurrentUrlOfDebriefDetailPage(string id)
        {
            Assert.AreEqual(WebUrl.MainPageUrl + "web/debrief-results/" + id, GetCurrentUrl());
            return this;
        }

        [AllureStep]
        public DebriefResultPage ClickOnRoundBtnAndNoErrorMessageDisplayed()
        {
            ClickOnElement(roundBtn);
            VerifyNotDisplayErrorMessage();
            return this;
        }

        [AllureStep]
        public DebriefResultPage ClickOnActivityBtnAndNoErrorMessageDisplayed()
        {
            ClickOnElement(activityBtn);
            VerifyNotDisplayErrorMessage();
            return this;
        }

        [AllureStep]
        public DebriefResultPage ClickOnWorksheetBtnAndNoErrorMessageDisplayed()
        {
            ClickOnElement(worksheetBtn);
            VerifyNotDisplayErrorMessage();
            return this;
        }

        [AllureStep]
        public DebriefResultPage ClickOnActivityTab()
        {
            ClickOnElement(activityTab);
            return this;
        }

        [AllureStep]
        public DebriefResultPage ClickOnEventsTab()
        {
            ClickOnElement(eventTab);
            return this;
        }

        [AllureStep]
        public DebriefResultPage ClickOnImgInEventTab()
        {
            ClickOnElement(roundInstanceEventImg);
            return this;
        }

        [AllureStep]
        public DebriefResultPage ClickOnInspectionsTab()
        {
            ClickOnElement(inspectionTab);
            return this;
        }

        [AllureStep]
        public DebriefResultPage SelectReasonTaken(string reasonName)
        {
            WaitUtil.WaitForElementVisible(reasonTakenLabel);
            ClickOnElement(selectResolutionCodes);
            ClickOnElement(resolutionCodeOption, reasonName);
            SleepTimeInSeconds(1);
            return this;
        }

        [AllureStep]
        public DebriefResultPage CLickOnPostBtn()
        {
            ClickOnElement(postBtn);
            return this;
        }

        [AllureStep]
        public DebriefResultPage CLickOnUnPostBtn()
        {
            ClickOnElement(unPostBtn);
            return this;
        }
    }
}
