using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;

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

        private const string toggleStandardCommercialTransaction = "//label[contains(string(), '{0}')]";
        private const string detailTitle = "//label[contains(string(), 'Update {0} Selected Tasks')]";

        //Toggle arrow
        private const string anyTaskStateSelect = "(//div[@id='task-type-content-tab']//select[@id='taskStates.id'])[{0}]";
        private const string anyResolutionCodeSelect = "(//label[contains(string(), 'Resolution Code')]/following-sibling::echo-select/select)[{0}]";
        private const string anyCompletedDateToggleInput = "(//div[@id='task-type-content-tab']//input[@id='completedDate.id'])[{0}]";
        private const string anyEndDateToggleInput = "(//div[@id='task-type-content-tab']//input[@id='endDate.id'])[{0}]";
        private const string anyNotesToggleInput = "(//div[@id='task-type-content-tab']//textarea[@id='notes.id'])[{0}]";
        private const string optionTaskState = "(//div[@id='task-type-content-tab']//select[@id='taskStates.id'])[{0}]//option[text()='{1}']";
        private const string anyResolutionCode = "(//label[contains(string(), 'Resolution Code')])[{0}]/following-sibling::echo-select/select";
        private const string optionResolutionCode = "(//label[contains(string(), 'Resolution Code')])[{0}]/following-sibling::echo-select/select//option[text()='{1}']";

        [AllureStep]
        public TasksBulkUpdatePage IsTaskBulkUpdatePage(string toggleTitle, string numberOfTask)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(title));
            Assert.IsTrue(IsControlDisplayed(completedDateInput));
            Assert.IsTrue(IsControlDisplayed(endDateInput));
            Assert.IsTrue(IsControlDisplayed(notesInput));
            Assert.IsTrue(IsControlDisplayed(useBackgroundTransactionCheckbox));
            Assert.IsTrue(IsControlDisplayed(detailTitle, numberOfTask));
            Assert.IsTrue(IsControlDisplayed(toggleStandardCommercialTransaction, toggleTitle));

            return this;
        }
        [AllureStep]
        public TasksBulkUpdatePage VerifyTaskTypeSecondTask(string taskType)
        {
            Assert.IsTrue(IsControlDisplayed(toggleStandardCommercialTransaction, taskType));
            return this;
        }
        [AllureStep]
        public TasksBulkUpdatePage ClickFirstToggleArrow()
        {
            if (IsControlUnDisplayed(firstToggleArrowExpanded))
            {
                ClickOnElement(firstToggleArrow);
                WaitUtil.WaitForElementVisible(firstToggleArrowExpanded);
            }
            return this;
        }
        [AllureStep]
        public TasksBulkUpdatePage ClickSecondToggleArrow()
        {
            if (IsControlUnDisplayed(secondToggleArrowExpanded))
            {
                ClickOnElement(secondToggleArrow);
                WaitUtil.WaitForElementVisible(secondToggleArrowExpanded);
            }
            return this;
        }
        [AllureStep]
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
        [AllureStep]
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
        [AllureStep]
        public TasksBulkUpdatePage VerifyTaskBulkUpdateForm()
        {
            Assert.IsTrue(IsControlDisplayed(helpBtn));
            Assert.IsTrue(IsControlDisplayed(saveBtn));
            Assert.IsTrue(IsControlDisplayed(saveAndCloseBtn));
            Assert.IsTrue(IsControlDisplayed(closeWithoutSavingBtn));
            return this;
        }

        //Click [Close without saving] btn
        [AllureStep]
        public TasksBulkUpdatePage ClickCloseWithoutSavingBtn()
        {
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }

        //Send value in [Note]
        [AllureStep]
        public TasksBulkUpdatePage SendKeyInNoteInput(string noteValue)
        {
            SendKeys(notesInput, noteValue);
            return this;
        }

        //Click [Use Background Transaction] checkbox
        [AllureStep]
        public TasksBulkUpdatePage ClickUserBackgroundTransactionCheckbox()
        {
            ClickOnElement(useBackgroundTransactionCheckbox);
            return this;
        }

        //Verify [Task State]
        [AllureStep]
        public TasksBulkUpdatePage VerifyTaskStatePopulated(string stateExp, string numberOfToggle)
        {
            Assert.AreEqual(stateExp, GetFirstSelectedItemInDropdown(string.Format(anyTaskStateSelect, numberOfToggle)), "Task state is not populated");
            return this;
        }

        [AllureStep]
        public TasksBulkUpdatePage SelectTaskState(string state, string numberOfToggle)
        {
            SelectTextFromDropDown(By.XPath(string.Format(anyTaskStateSelect, numberOfToggle)), state);
            return this;
        }

        [AllureStep]
        public TasksBulkUpdatePage SelectResolutionCode(string code, string numberOfToogle)
        {
            By xpath = By.XPath(string.Format(anyResolutionCodeSelect, numberOfToogle));
            if (code.Equals("random", StringComparison.OrdinalIgnoreCase))
            {
                var numberOfOptions = GetNumberOfOptionInSelect(By.XPath(string.Format(anyResolutionCodeSelect, numberOfToogle)));
                var random = CommonUtil.GetRandomNumberBetweenRange(1, numberOfOptions - 1);
                SelectIndexFromDropDown(xpath, random);
            }
            else if (code.Equals("last", StringComparison.OrdinalIgnoreCase))
            {
                SelectIndexFromDropDown(xpath, GetNumberOfOptionInSelect(xpath) - 1);
            }
            else
            {
                SelectTextFromDropDown(xpath, code);
            }

            //SelectTextFromDropDown(By.XPath(string.Format(anyResolutionCodeSelect, numberOfToogle)), code);
            return this;
        }

        //Click on Completed Date input
        [AllureStep]
        public TasksBulkUpdatePage ClickOnCompletedDateInput()
        {
            ClickOnElement(completedDateInput);
            return this;
        }
        [AllureStep]
        public TasksBulkUpdatePage SendKeyInCompletedDate(string value)
        {
            SendKeys(completedDateInput, value);
            return this;
        }

        //Click on End Date input
        [AllureStep]
        public TasksBulkUpdatePage ClickOnEndDateInput()
        {
            ClickOnElement(endDateInput);
            return this;
        }
        [AllureStep]
        public TasksBulkUpdatePage SendKeyInEndDate(string value)
        {
            SendKeys(endDateInput, value);
            return this;
        }

        //Verify [task] after updating
        [AllureStep]
        public TasksBulkUpdatePage VerifyTaskAfterBulkUpdating(TaskDBModel taskDBModel, string note, string taskCompletedDate, string taskEndDate)
        {
            Assert.AreEqual(note, taskDBModel.tasknotes);
            Assert.AreEqual(taskCompletedDate, taskDBModel.taskcompleteddate.ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT));
            Assert.AreEqual(taskEndDate, taskDBModel.taskenddate.ToString(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT));
            return this;
        }
        [AllureStep]
        public TasksBulkUpdatePage VerifyTaskCompletedDateAndEndDateNotUpdated(TaskDBModel taskDBModel)
        {
            Assert.AreEqual(null, taskDBModel.taskcompleteddate);
            Assert.AreEqual(null, taskDBModel.taskenddate);
            return this;
        }

        //Change [Task State]
        [AllureStep]
        public TasksBulkUpdatePage ChangeTaskStateBottomPage(string stateValue, string indexOfTask)
        {
            ClickOnElement(anyTaskStateSelect, indexOfTask);
            ClickOnElement(string.Format(optionTaskState, indexOfTask, stateValue));
            return this;
        }

        //Change [Completed Date]
        [AllureStep]
        public TasksBulkUpdatePage ChangeCompletedDateBottomPage(string completedDateValue, string indexOfTask)
        {
            SendKeys(string.Format(anyCompletedDateToggleInput, indexOfTask), completedDateValue);
            return this;
        }

        //Change [End Date]
        [AllureStep]
        public TasksBulkUpdatePage ChangeEndDateBottomPage(string endDateValue, string indexOfTask)
        {
            SendKeys(string.Format(anyEndDateToggleInput, indexOfTask), endDateValue);
            return this;
        }

        //Change [Notes]
        [AllureStep]
        public TasksBulkUpdatePage ChangeNotesBottomPage(string noteValue, string indexOfTask)
        {
            SendKeys(string.Format(anyNotesToggleInput, indexOfTask), noteValue);
            return this;
        }

        //Change [Resolution Code]
        [AllureStep]
        public TasksBulkUpdatePage ChangeResolutionCode(string resolutionCodeValue, string indexOfTask)
        {
            ClickOnElement(anyResolutionCode, indexOfTask);
            ClickOnElement(string.Format(optionResolutionCode, indexOfTask, resolutionCodeValue));
            return this;
        }
    }
}
