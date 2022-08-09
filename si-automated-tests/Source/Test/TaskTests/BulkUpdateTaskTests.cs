using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class BulkUpdateTaskTests : BaseTest
    {
        //Check for one task from North Star Commercial
        [Category("Bulk update Task form")]
        [Category("Chang")]
        [Test(Description = "Verify that a bulk update form can be opened from Tasks step 1 and 2")]
        public void TC_126_Verify_bulk_update_form_can_be_opened_from_Tasks_step_1_2()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star Commercial")
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId("8866")
                .ClickCheckboxFirstTaskInList()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            TasksBulkUpdatePage tasksBulkUpdatePage = PageFactoryManager.Get<TasksBulkUpdatePage>();
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage("Commercial Collection", "1")
                .ClickFirstToggleArrow()
                .VerifyValueAfterClickAnyToggle("1")
                //Step 2 - Verify form:
                .VerifyTaskBulkUpdateForm()
                //Step 2 - 1. Untick [User Background Transaction] -> Click [Save] btn
                .ClickUserBackgroundTransactionCheckbox()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Step 2 - 2. Untick [User Background Transaction] -> Click [Save and Close] btn
            string note = "Note" + CommonUtil.GetRandomNumber(5);
            tasksBulkUpdatePage
                .SendKeyInNoteInput(note)
                .ClickUserBackgroundTransactionCheckbox()
                .ClickSaveAndCloseBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(1)
                .VerifyWindowClosed(1)
                .SwitchNewIFrame();
            //Step 2 - 3.  Untick [User Background Transaction] -> Click [Close without Saving] button and confirm leaving the page 
            PageFactoryManager.Get<TasksListingPage>()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string newNote = "New note" + CommonUtil.GetRandomNumber(5);
            Thread.Sleep(3000);
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage("Commercial Collection", "1")
                .ClickFirstToggleArrow()
                //Verify [State of the task type is auto populated]
                .VerifyTaskStatePopulated("In Progress", "1")
                .SendKeyInNoteInput(newNote)
                .ClickUserBackgroundTransactionCheckbox()
                .ClickCloseWithoutSavingBtn()
                .AcceptAlert()
                .SwitchToLastWindow()
                .SwitchToChildWindow(1)
                .VerifyWindowClosed(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage("Commercial Collection", "1")
                .ClickFirstToggleArrow()
                .VerifyValueAfterClickAnyToggle("1")
                //Verify [State of the task type is auto populated]
                .VerifyTaskStatePopulated("In Progress", "1")
                //Step 2 - 5. [Help] button
                .ClickHelpBtnAndVerify();

        }

        //Two tasks with state = Completed 
        [Category("Bulk update Task form")]
        [Category("Chang")]
        [Test(Description = "Step 4 - 1 Selecting date/time in top panel. No values in bottom panel")]
        public void TC_126_step_4_1_selecting_date_time_in_top_panel_no_values_in_bottom_panel()
        {
            CommonFinder finder = new CommonFinder(DbContext);

            string firstTaskId = "8930";
            string secondTaskId = "1988";
            string firstTaskTypeName = "Collect Communal Refuse";
            string secondTaskTypeName = "Collect Domestic Refuse";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star")
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterMultipleTaskId(firstTaskId, secondTaskId)
                .ClickCheckboxMultipleTaskInList()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            TasksBulkUpdatePage tasksBulkUpdatePage = PageFactoryManager.Get<TasksBulkUpdatePage>();
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage(firstTaskTypeName, "2")
                .VerifyTaskTypeSecondTask(secondTaskTypeName)
                //Click First toggle and Verify
                .ClickFirstToggleArrow()
                .VerifyValueAfterClickAnyToggle("1")
                .VerifyTaskStatePopulated("Completed", "1")
                //Click Second toggle and verify
                .ScrollToBottomOfPage();
            tasksBulkUpdatePage
                .ClickSecondToggleArrow()
                .VerifyValueAfterClickAnyToggle("2")
                .VerifyTaskStatePopulated("Completed", "2");
            string timeNow = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            DateTime dateTime = CommonUtil.GetLocalTimeNow();
            string note = "Note for two task";
            //Step 4: Line 44 - Update top pannel
            tasksBulkUpdatePage
                .SendKeyInCompletedDate(timeNow)
                .SendKeyInEndDate(timeNow)
                .SendKeyInNoteInput(note)
                .ClickUserBackgroundTransactionCheckbox()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            tasksBulkUpdatePage
                .SleepTimeInMiliseconds(3000);
            //API - Verify task after updated
            List<TaskDBModel> taskDBModels = finder.GetMultipleTask(int.Parse(firstTaskId), int.Parse(secondTaskId));
            TaskDBModel taskDBModelFirstTask = taskDBModels.Find(p => p.taskId == int.Parse(firstTaskId));
            TaskDBModel taskDBModelSecondTask = taskDBModels.Find(p => p.taskId == int.Parse(secondTaskId));

            tasksBulkUpdatePage
                .VerifyTaskAfterBulkUpdating(taskDBModelFirstTask, note, timeNow, timeNow)
                .VerifyTaskAfterBulkUpdating(taskDBModelSecondTask, note, timeNow, timeNow);
            //Go to detail each task and check
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(firstTaskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyFieldAfterBulkUpdate(note, timeNow, "Completed", timeNow, "")
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .ClickClearBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(secondTaskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyFieldAfterBulkUpdate(note, timeNow, "Completed", timeNow, "");
        }

        //Bug found => Failed
        //Two tasks with state = Inprogress 
        [Category("Bulk update Task form")]
        [Category("Chang")]
        [Test(Description = "Step 4 - 2 Verify that user can update tasks (status/resolution code) based on their task type")]
        public void TC_126_step_4_2_update_status_resolution_code_based_on_their_task_type()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string firstTaskId = "9496";
            string secondTaskId = "9692";
            string firstTaskTypeName = "Collect Domestic Recycling";
            string secondTaskTypeName = "Collect Communal Recycling";
            //DB - Get task
            TaskDBModel secondTaskDB = finder.GetTask(int.Parse(secondTaskId))[0];

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star")
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterMultipleTaskId(firstTaskId, secondTaskId)
                .ClickCheckboxMultipleTaskInList()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            TasksBulkUpdatePage tasksBulkUpdatePage = PageFactoryManager.Get<TasksBulkUpdatePage>();
            string timeNow = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string completedDateDisplayed = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            string noteFirst = "Note first";
            string noteSecond = "Note second";

            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage(firstTaskTypeName, "2")
                .VerifyTaskTypeSecondTask(secondTaskTypeName)
                .ClickUserBackgroundTransactionCheckbox()
                //Click First toggle - Update
                .ClickFirstToggleArrow()
                .VerifyValueAfterClickAnyToggle("1")
                .VerifyTaskStatePopulated("In Progress", "1")
                .ChangeTaskStateBottomPage("Completed", "1")
                .ChangeCompletedDateBottomPage(timeNow, "1")
                .ChangeEndDateBottomPage(timeNow, "1")
                .ChangeNotesBottomPage(noteFirst, "1")
                //Click Second toggle - Update
                .ScrollToBottomOfPage();
            tasksBulkUpdatePage
                .ClickSecondToggleArrow()
                .VerifyValueAfterClickAnyToggle("2")
                .VerifyTaskStatePopulated("In Progress", "2")
                .ChangeTaskStateBottomPage("Not Completed", "2")
                .ChangeCompletedDateBottomPage(timeNow, "2")
                .ChangeEndDateBottomPage(timeNow, "2")
                .ChangeResolutionCode("Not Out", "2")
                .ChangeNotesBottomPage(noteSecond, "2")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();

            //Wait for task type updated
            PageFactoryManager.Get<TasksListingPage>()
                .SleepTimeInMiliseconds(5000)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //Step 4: Second task - Line 52 - Verify detail
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(secondTaskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            string completionDateFirstTask = detailTaskPage.CompareDueDateWithTimeNow(secondTaskDB, timeNow);

            //Failed
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyFieldAfterBulkUpdate(noteSecond, timeNow, "Not Completed", completionDateFirstTask, "Not Out");
        }

        //Two tasks with state = Cancelled 
        [Category("Bulk update Task form")]
        [Category("Chang")]
        [Test(Description = "Step 4 - 3 Selecting Status/Resoulution code in bottom panel, but date/time in top panel")]
        public void TC_126_step_4_3_update_top_panel_in_progress_state() 
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string firstTaskId = "14";
            string secondTaskId = "466";
            string secondTaskTypeName = "Collect Domestic Recycling";
            string firstTaskTypeName = "Collect Communal Refuse";
            //DB - Get task
            TaskDBModel firstTaskDB = finder.GetTask(int.Parse(firstTaskId))[0];

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star")
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterMultipleTaskId(firstTaskId, secondTaskId)
                .ClickCheckboxMultipleTaskInList()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            TasksBulkUpdatePage tasksBulkUpdatePage = PageFactoryManager.Get<TasksBulkUpdatePage>();
            string topNote = "Auto Note " + CommonUtil.GetRandomNumber(5);
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage(firstTaskTypeName, "2")
                .VerifyTaskTypeSecondTask(secondTaskTypeName)
                //Step 4: Line 47 - Update bottom pannel
                //Click First toggle - Update
                .ClickFirstToggleArrow()
                .VerifyValueAfterClickAnyToggle("1")
                .VerifyTaskStatePopulated("Cancelled", "1")
                .ChangeTaskStateBottomPage("Completed", "1")
                //Click Second toggle - Update
                .ScrollToBottomOfPage();
            tasksBulkUpdatePage
                .ClickSecondToggleArrow()
                .VerifyValueAfterClickAnyToggle("2")
                .VerifyTaskStatePopulated("Cancelled", "2")
                .ChangeTaskStateBottomPage("Not Completed", "2")
                .ChangeResolutionCode("Not Out", "2");
            //Update top pannel
            string timeNow = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string completedDateDisplayed = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            string completedDateDDMMYYYY = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            tasksBulkUpdatePage
                .SendKeyInCompletedDate(timeNow)
                .SendKeyInEndDate(timeNow)
                .SendKeyInNoteInput(topNote)
                .ClickUserBackgroundTransactionCheckbox()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Wait for task type updated
            PageFactoryManager.Get<TasksListingPage>()
                .SleepTimeInMiliseconds(3000)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //Step 4: Second task - Line 52 - Verify detail
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(secondTaskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyFieldAfterBulkUpdate(topNote, timeNow, "Not Completed", completedDateDisplayed, "Not Out");
            //Step 4: Line 53 - History tab
            string[] valueExpInServiceUpdateSecondTask = { "Not Completed", completedDateDDMMYYYY, "Manually Confirmed on Web"};
            string[] valueExpUpdateSecondTask = { topNote, completedDateDisplayed, "Not Completed", timeNow, "Not Out" };
            detailTaskPage
                .ClickOnHistoryTab()
                .VerifyTitleTaskLineFirstServiceUpdate()
                .VerifyHistoryTabFirstAfterBulkUpdating(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.ServiceUpdateTaskLineFromCancelledToNotCompleted, valueExpInServiceUpdateSecondTask);
            detailTaskPage
                .VerifyTitleUpdate()
                .VerifyHistoryTabUpdate(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.UpdateColumnHistoryTabFirst, valueExpUpdateSecondTask);
            //Step 4: Line 53 - Verdict tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskInformation()
                .VerifyTaskInformationAfterBulkUpdating(completedDateDisplayed, "Not Completed", "Not Out", "Manually Confirmed on Web")
                .ClickOnTaskLineVerdictTab()
                .VerifyFirstTaskLineStateVerdictTab(completedDateDisplayed, "Not Completed", "Manually Confirmed on Web", "General Refuse");
            //Step 4: Line 53 - Task line tab
            detailTaskPage
                .ClickOnTaskLineTab()
                .VerifyFirstTaskLineAfterBulkUpdate("General Refuse", "Not Completed", "")
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Step 4: First task - Line 52 - Verify detail
            PageFactoryManager.Get<TasksListingPage>()
                .ClickClearBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(firstTaskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string completionDateFirstTask = detailTaskPage.CompareDueDateWithTimeNow(firstTaskDB, timeNow);

            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyFieldAfterBulkUpdate(topNote, timeNow, "Completed", completionDateFirstTask, "");
            //Step 4: Line 53 - History tab
            string[] valueExpUpdateFirstTask = { topNote, completionDateFirstTask, "Completed", timeNow };
            string[] valueExpInServiceUpdateFirstTask = { "1", "1", "Completed", "", completedDateDDMMYYYY, "Manually Confirmed on Web"};

            detailTaskPage
                .ClickOnHistoryTab()
                .VerifyTitleTaskLineFirstServiceUpdate()
                .VerifyHistoryTabFirstAfterBulkUpdating(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.ServiceUpdateColumnHistoryTab, valueExpInServiceUpdateFirstTask)
                .VerifyTitleTaskLineSecondServiceUpdate()
                .VerifyHistoryTabSecondAfterBulkUpdating(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.ServiceUpdateColumnHistoryTab, valueExpInServiceUpdateFirstTask)
                .VerifyTitleUpdate()
                .VerifyHistoryTabUpdate(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.UpdateColumnHistoryTabSecond, valueExpUpdateFirstTask);
            //Step 4: Line 53 - Verdict tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskInformation()
                .VerifyTaskInformationAfterBulkUpdating(completionDateFirstTask, "Completed", "", "Manually Confirmed on Web")
                .ClickOnTaskLineVerdictTab()
                .VerifyFirstTaskLineStateVerdictTab(completedDateDisplayed, "Completed", "Manually Confirmed on Web", "Paper & Cardboard")
                .VerifySecondTaskLineStateVerdictTab(completedDateDisplayed, "Completed", "Manually Confirmed on Web", "Plastic");
            //Step 4: Line 53 - Task line tab
            detailTaskPage
                .ClickOnTaskLineTab()
                .VerifyFirstTaskLineAfterBulkUpdate("Paper & Cardboard", "Completed", "")
                .VerifySecondTaskLineAfterBulkUpdate("Plastic", "Completed", "");

        }

        //Two tasks with state = Unallocated 
        [Category("Bulk update Task form")]
        [Category("Chang")]
        [Test(Description = "Step 4 - 4 Selecting 'Task Completed Date'/'Task End Date' /'Task Notes' in top panel and 'Completion Date'/ 'Task End Date'/'Task Notes' in bottom panel")]
        public void TC_126_step_4_4_update_top_panel_in_progress_state()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string firstTaskId = "12";
            string secondTaskId = "11";
            string firstTaskTypeName = "Collect Domestic Recycling";
            string secondTaskTypeName = "Collect Bulky";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star")
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterMultipleTaskId(firstTaskId, secondTaskId)
                .ClickCheckboxMultipleTaskInList()
                .ClickOnBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            TasksBulkUpdatePage tasksBulkUpdatePage = PageFactoryManager.Get<TasksBulkUpdatePage>();
            string topNote = "Auto Note " + CommonUtil.GetRandomNumber(5);

            string timeMinus1Day = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, -1);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            string completedDateDisplayed = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            string completedDateDDMMYYYY = CommonUtil.ParseDateTimeToFormat(londonCurrentDate, CommonConstants.DATE_DD_MM_YYYY_FORMAT);

            string completedDateEndDateSecondTask = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, 1);
            string noteFirstTask = "auto first task";

            string completedDateEndDateFirstTask = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT, 2);
            string noteSecondTask = "auto second task";
            tasksBulkUpdatePage
                .IsTaskBulkUpdatePage(firstTaskTypeName, "2")
                .VerifyTaskTypeSecondTask(secondTaskTypeName)
                //Step 4: Line 55 - Update top pannel
                .SendKeyInCompletedDate(timeMinus1Day)
                .SendKeyInEndDate(timeMinus1Day)
                .SendKeyInNoteInput(topNote)
                .ClickUserBackgroundTransactionCheckbox()
                //Step 4: Line 55 - Update bottom pannel
                //Click First toggle - Update (Completion Date', 'End Date', 'Notes')
                .ClickFirstToggleArrow()
                .VerifyValueAfterClickAnyToggle("1")
                .VerifyTaskStatePopulated("Unallocated", "1")
                .ChangeCompletedDateBottomPage(completedDateEndDateSecondTask, "1")
                .ChangeEndDateBottomPage(completedDateEndDateSecondTask, "1")
                .ChangeNotesBottomPage(noteSecondTask, "1")
                .ScrollToBottomOfPage();
            //Click Second toggle - Update (Completion Date', 'End Date', 'Notes')
            tasksBulkUpdatePage
                .ClickSecondToggleArrow()
                .VerifyValueAfterClickAnyToggle("2")
                .VerifyTaskStatePopulated("Unallocated", "2")
                .ChangeCompletedDateBottomPage(completedDateEndDateFirstTask, "2")
                .ChangeEndDateBottomPage(completedDateEndDateFirstTask, "2")
                .ChangeNotesBottomPage(noteFirstTask, "2")
                //Click [Save] btn
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Wait for task type updated
            PageFactoryManager.Get<TasksListingPage>()
                .SleepTimeInMiliseconds(3000)
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //Step 4: Second task - Line 56 - Verify detail
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(secondTaskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            string[] valueExpInServiceUpdateSecondTask = { "1", "1", "Completed", "", completedDateDDMMYYYY, "Manually Confirmed on Web" };
            string[] valueExpUpdateSecondTask = { topNote + " " + noteSecondTask, completedDateEndDateSecondTask, "Completed", completedDateEndDateSecondTask};

            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyFieldAfterBulkUpdate(topNote, noteSecondTask, completedDateEndDateSecondTask, "Completed", completedDateEndDateSecondTask, "")
                //History tab
                .ClickOnHistoryTab()
                .VerifyTitleTaskLineFirstServiceUpdate()
                .VerifyHistoryTabFirstAfterBulkUpdating(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.ServiceUpdateColumnHistoryTab, valueExpInServiceUpdateSecondTask)
                .VerifyTitleTaskLineSecondServiceUpdate()
                .VerifyHistoryTabSecondAfterBulkUpdating(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.ServiceUpdateColumnHistoryTab, valueExpInServiceUpdateSecondTask)
                .VerifyTitleUpdate()
                .VerifyHistoryTabUpdate(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.UpdateColumnHistoryTabSecond, valueExpUpdateSecondTask)
                //Step 4: Line 56 - Verdict tab
                .ClickOnVerdictTab()
                .ClickOnTaskInformation()
                .VerifyTaskInformationAfterBulkUpdating(completedDateEndDateSecondTask, "Completed", "", "Manually Confirmed on Web")
                .ClickOnTaskLineVerdictTab()
                .VerifyFirstTaskLineStateVerdictTab(completedDateDisplayed, "Completed", "Manually Confirmed on Web", "Plastic")
                .VerifySecondTaskLineStateVerdictTab(completedDateDisplayed, "Completed", "Manually Confirmed on Web", "Paper & Cardboard")
                //Step 4: Line 56 - Task line tab
                .ClickOnTaskLineTab()
                .VerifyFirstTaskLineAfterBulkUpdate("Plastic", "Completed", "")
                .VerifySecondTaskLineAfterBulkUpdate("Paper & Cardboard", "Completed", "")
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Step 4: First task - Line 56 - Verify detail
            PageFactoryManager.Get<TasksListingPage>()
                .ClickClearBtn()
                .WaitForLoadingIconToDisappear();
            string completionDateFirstTask = detailTaskPage.CompareDueDateWithTimeNow(firstTaskDB, completedDateEndDateSecondTask);
            string[] valueExpUpdateFirstTask = { topNote + " " + noteFirstTask, completionDateFirstTask, "Completed", completedDateEndDateFirstTask };
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(firstTaskId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyFieldAfterBulkUpdate(topNote, noteFirstTask, completedDateEndDateFirstTask, "Completed", completionDateFirstTask, "")
                //Step 4: Line 56 - History tab
                .ClickOnHistoryTab()
                .VerifyTitleUpdate()
                .VerifyHistoryTabUpdate(AutoUser55.DisplayName, completedDateDisplayed, CommonConstants.UpdateColumnHistoryTabSecond, valueExpUpdateFirstTask);
            //Step 4: Line 53 - Verdict tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskInformation()
                .VerifyTaskInformationAfterBulkUpdating(completionDateFirstTask, "Completed", "", "Manually Confirmed on Web");
        }

        [Category("Bulk update Task form")]
        [Category("Chang")]
        [Test(Description = "Step 6 - Verify that the task can be deleted")]
        public void TC_126_step_6_verify_the_task_can_be_deleted()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            string taskId = "8938";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star")
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId(taskId)
                .ClickCheckboxFirstTaskInList()
                .ClickDeleteBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<RemoveTaskPage>()
                .IsDeleteTaskPopup()
                //Click (x) btn
                .ClickCloseDeleteTaskPopupBtn()
                .SwitchToChildWindow(1)
                .VerifyWindowClosed(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .ClickDeleteBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<RemoveTaskPage>()
                .IsDeleteTaskPopup()
                //Click [No] btn
                .ClickOnNoDeleteTaskPopupBtn()
                .SwitchToChildWindow(1)
                .VerifyWindowClosed(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .ClickDeleteBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<RemoveTaskPage>()
                .IsDeleteTaskPopup()
                //Click [ESC] btn
                .EnterEscBtn()
                .SwitchToChildWindow(1)
                .VerifyWindowClosed(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .ClickDeleteBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<RemoveTaskPage>()
                .IsDeleteTaskPopup()
                //Click [Yes] btn
                .ClickOnYesDeleteTaskPopupBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(1)
                .VerifyWindowClosed(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksListingPage>()
                .FilterByTaskId(taskId)
                .VerifyNoRecordDisplayed()
                .ClickClearBtn()
                .WaitForLoadingIconToDisappear();
            //DB
            List<TaskDBModel> firstTaskDB = finder.GetTask(int.Parse(taskId));
            Assert.AreEqual(0, firstTaskDB.Count);
            //Step 73
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterMultipleTaskId("8298", "8297")
                .ClickCheckboxMultipleTaskInList()
                .ClickDeleteBtn()
                .VerifyToastMessage(MessageRequiredFieldConstants.TooManyTaskItemsSelectedDeletedMessage);
        }

        [Category("Bulk update Task form")]
        [Category("Chang")]
        [Test(Description = "Verify that a bulk update form can be opened from Parties")]
        public void TC_126_Verify_bulk_update_form_can_be_opened_from_Paties()
        {
            int partyId = 43;
            string partyName = "Jaflong Tandoori";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser55.UserName, AutoUser55.Password)
                .IsOnHomePage(AutoUser55);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                //Click [Task] tab
                .ClickTabDropDown()
                .ClickTasksTab()
                .WaitForLoadingIconToDisappear();
            string taskId = "26";
            PageFactoryManager.Get<DetailPartyPage>()
                .FilterTaskId(taskId)
                .ClickFirstTaskCheckbox()
                .ClickBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksBulkUpdatePage>()
                .IsTaskBulkUpdatePage("Deliver Commercial Bin", "1");
        }

    }
}
