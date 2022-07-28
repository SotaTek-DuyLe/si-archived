using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class TasksBulkUpdatePage : BasePage
    {
        private readonly By title = By.XPath("//label[contains(string(), 'Task Bulk Update')]");
        private readonly By completedDateInput = By.CssSelector("div[data-bind='with: fields']>div:nth-child(1) input[id='completedDate.id']");
        private readonly By endDateInput = By.CssSelector("div[data-bind='with: fields']>div:nth-child(2) input[id='endDate.id']");
        private readonly By notesInput = By.CssSelector("div[data-bind='with: fields']>div:nth-child(3) textarea[id='notes.id']");
        private readonly By useBackgroundTransactionCheckbox = By.XPath("//label[contains(string(), 'Use Background Transaction')]/following-sibling::input");
        private readonly By firstToggleArrow = By.XPath("(//*[@id='toggle-arrow']/parent::div)[1]");
        private readonly By secondToggleArrow = By.XPath("(//*[@id='toggle-arrow']/parent::div)[2]");
        private readonly By firstToggleArrowExpanded = By.XPath("(//*[@id='toggle-arrow']/parent::div[@aria-expanded='true'])[1]");
        private readonly By secondToggleArrowExpanded = By.XPath("(//*[@id='toggle-arrow']/parent::div[@aria-expanded='true'])[2]");
        private readonly By helpBtn = By.CssSelector("button[title='Help']");
        private readonly By saveBtn = By.CssSelector("button[title='Save']");
        private readonly By saveAndCloseBtn = By.CssSelector("button[title='Save and Close']");
        private readonly By closeWithoutSavingBtn = By.CssSelector("button[title='Close Without Saving']");

        private const string toggleStandardCommercialTransaction = "//label[text()='Standard - {0}']";
        private const string detailTitle = "//label[contains(string(), 'Update {0} Selected Tasks')]";

        //Toggle arrow
        private const string anyTaskStateSelect = "(div[@id='task-type-content-tab']//select[@id='taskStates.id'])[{0}]";
        private const string anyResolutionCodeSelect = "(//label[contains(string(), 'Resolution Code')]/following-sibling::echo-select/select)[{0}]";
        private const string anyCompletedDateToggleInput = "(div[@id='task-type-content-tab']//input[@id='completedDate.id'])[{0}]";
        private const string anyEndDateToggleInput = "(//div[@id='task-type-content-tab']//input[@id='endDate.id'])[{0}]";
        private const string anyNotesToggleInput = "(//div[@id='task-type-content-tab']//textarea[@id='notes.id'])[{0}]";

        public TasksBulkUpdatePage IsTaskBulkUpdatePage(string toggleTitle, string numberOfTask)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(title));
            Assert.IsTrue(IsControlDisplayed(detailTitle, numberOfTask));
            Assert.IsTrue(IsControlDisplayed(completedDateInput));
            Assert.IsTrue(IsControlDisplayed(endDateInput));
            Assert.IsTrue(IsControlDisplayed(notesInput));
            Assert.IsTrue(IsControlDisplayed(useBackgroundTransactionCheckbox));
            Assert.IsTrue(IsControlDisplayed(toggleStandardCommercialTransaction, toggleTitle));

            return this;
        }

        public TasksBulkUpdatePage VerifyTaskTypeSecondTask(string taskType)
        {
            Assert.IsTrue(IsControlDisplayed(toggleStandardCommercialTransaction, taskType));
            return this;
        }

        public TasksBulkUpdatePage ClickFirstToggleArrow()
        {
            if (IsControlUnDisplayed(firstToggleArrowExpanded))
            {
                ClickOnElement(firstToggleArrow);
                WaitUtil.WaitForElementVisible(firstToggleArrowExpanded);
            }
            return this;
        }

        public TasksBulkUpdatePage ClickSecondToggleArrow()
        {
            if (IsControlUnDisplayed(secondToggleArrowExpanded))
            {
                ClickOnElement(secondToggleArrow);
                WaitUtil.WaitForElementVisible(secondToggleArrowExpanded);
            }
            return this;
        }

        public TasksBulkUpdatePage VerifyValueAfterClickAnyToggle(string numberOfToggle)
        {
            //verify
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(anyTaskStateSelect, numberOfToggle);
            Assert.IsTrue(IsControlDisplayed(anyTaskStateSelect, numberOfToggle), "task states is not displayed");
            Assert.IsTrue(IsControlDisplayed(anyResolutionCodeSelect, numberOfToggle), "resolutionCodeSelect is not displayed");
            WaitUtil.WaitForElementVisible(anyCompletedDateToggleInput, numberOfToggle);
            Assert.IsTrue(IsControlDisplayed(anyCompletedDateToggleInput, numberOfToggle), "completedDateToggleInput is not displayed");
            WaitUtil.WaitForElementVisible(anyEndDateToggleInput, numberOfToggle);
            Assert.IsTrue(IsControlDisplayed(anyEndDateToggleInput, numberOfToggle), "endDateToggleInput is not displayed");
            WaitUtil.WaitForElementVisible(anyNotesToggleInput, numberOfToggle);
            Assert.IsTrue(IsControlDisplayed(anyNotesToggleInput, numberOfToggle), "notesToggleInput is not displayed");
            return this;
        }

        //Click [Help] btn
        public TasksBulkUpdatePage ClickHelpBtnAndVerify()
        {
            ClickOnElement(helpBtn);
            //Verify
            SwitchToLastWindow();
            Assert.AreEqual(WebUrl.MainPageUrl + "web/help", GetCurrentUrl());
            CloseCurrentWindow();
            return this;
        }

        //Verify [Task Bulk Update] form
        public TasksBulkUpdatePage VerifyTaskBulkUpdateForm()
        {
            Assert.IsTrue(IsControlDisplayed(helpBtn));
            Assert.IsTrue(IsControlDisplayed(saveBtn));
            Assert.IsTrue(IsControlDisplayed(saveAndCloseBtn));
            Assert.IsTrue(IsControlDisplayed(closeWithoutSavingBtn));
            return this;
        }

        //Click [Close without saving] btn
        public TasksBulkUpdatePage ClickCloseWithoutSavingBtn()
        {
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }

        //Send value in [Note]
        public TasksBulkUpdatePage SendKeyInNoteInput(string noteValue)
        {
            SendKeys(notesInput, noteValue);
            return this;
        }

        //Click [Use Background Transaction] checkbox
        public TasksBulkUpdatePage ClickUserBackgroundTransactionCheckbox()
        {
            ClickOnElement(useBackgroundTransactionCheckbox);
            return this;
        }

        //Verify [Task State]
        public TasksBulkUpdatePage VerifyTaskStatePopulated(string stateExp, string numberOfToggle)
        {
            Assert.AreEqual(stateExp, GetFirstSelectedItemInDropdown(string.Format(anyTaskStateSelect, numberOfToggle)), "Task state is not populated");
            return this;
        }
    }
}
