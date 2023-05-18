using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.ServiceStatus;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.Applications.ServiceStatus;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.WB.Tickets;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using TaskAllocationPage = si_automated_tests.Source.Main.Pages.Applications.TaskAllocationPage;

namespace si_automated_tests.Source.Test.HistoryTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ErrorLoadingHistoryTests : BaseTest
    {
        [Category("Error loading history")]
        [Category("Chang")]
        [Test(Description = "Error loading History (bug fix)")]
        public void TC_158_error_loading_history()
        {
            string serviceId = "6350";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser47.UserName, AutoUser47.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser47);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(MainOption.ServiceStatus)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            //Filter any service status
            PageFactoryManager.Get<ServiceStatusPage>()
                .FilterServiceStatusById(serviceId)
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            string note = "Auto" + CommonUtil.GetRandomString(5);
            roundInstanceForm
                .IsRoundInstanceForm(serviceId)
                .ClickOnDetailTab()
                .AddNotes(note)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveRoundInstanceSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveRoundInstanceSuccessMessage);
            roundInstanceForm
                .ClickOnHistoryTab()
                .VerifyToastMessagesIsUnDisplayed();
            roundInstanceForm
                .VerifyFirstHistoryRow("Notes: " + note + ".", AutoUser47.DisplayName);
            //Click on [Events] tab
            roundInstanceForm
                .ClickOnEventsTab()
                .ClickOnAddNewItemBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .IsRoundInstanceEventDetailPage()
                .SelectRoundEventTypeAndResource("Tipping", "COM2 NST")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Click On - History tab
            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .ClickOnHistoryTab()
                .VerifyToastMessagesIsUnDisplayed();
            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .ClickOnFirstValueInDetailColumn()
                .IsHistoryPopup()
                .ClickOnCloseHistoryPopup()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Back to the [Round Instance] and change state
            roundInstanceForm
                .ClickOnDetailTab()
                .ClickOnStatusDdAndSelectValue("Complete")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveRoundInstanceSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SaveRoundInstanceSuccessMessage);
            roundInstanceForm
                .ClickOnHistoryTab()
                .VerifyToastMessagesIsUnDisplayed()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            string[] labelInHistoryTab = { "State"};
            string[] valueUpdated = { "Complete" };
            roundInstanceForm
                .VerifyFirstHistoryRow(valueUpdated, labelInHistoryTab, AutoUser47.DisplayName)
                .CloseCurrentWindow()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Line 13: Verify in [Weighbridge ticket]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Weighbridge)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Tickets")
                .SwitchNewIFrame();
            string ticketId = "7";
            TicketListingPage ticketListingPage = PageFactoryManager.Get<TicketListingPage>();
            ticketListingPage
                .FilterTicketById(ticketId)
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<WeighbridgeTicketDetailPage>()
                .IsWBTicketDetailPage(ticketId)
                .ClickOnHistoryTab()
                .VerifyToastMessagesIsUnDisplayed();
            PageFactoryManager.Get<WeighbridgeTicketDetailPage>()
                .ClickOnFirstValueInDetailColumn("1")
                .ClickOnCloseHistoryPopup()
                .ClickOnFirstValueInDetailColumn("2")
                .ClickOnCloseHistoryPopup()
                .ClickOnFirstValueInDetailColumn("3")
                .ClickOnCloseHistoryPopup();
        }

        [Category("Reallocated tasks")]
        [Category("Chang")]
        [Test(Description = "Tasks cannot be reallocated from RI worksheet (bug fix)")]
        public void TC_159_tasks_cannot_be_reallocated_from_RI_worksheet()
        {
            string serviceId = "6487";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser47.UserName, AutoUser47.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser47);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(MainOption.ServiceStatus)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //Filter any service status
            PageFactoryManager.Get<ServiceStatusPage>()
                .FilterServiceStatusById(serviceId)
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            roundInstanceForm
                .IsRoundInstanceForm(serviceId)
                .ClickOnWorksheetTab()
                //Line 8: Click on [Expand rounds] button
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            List<TaskInWorksheetModel> listTasks = roundInstanceForm
                .ClickOnExpandRoundBtn()
                .GetAllTaskInWorksheetTab(2);
            string taskName = roundInstanceForm
                .GetTaskName();
            //Line 9: Select 2 tasks in the grid and click [Reallocate] button
            roundInstanceForm
                .SelectTwoTaskInGrid(listTasks[0].checkboxLocator, listTasks[0].checkboxLocator)
                .ClickReallocateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            taskAllocationPage
                .VerifyTaskNameDisplayed(taskName)
                .VerifyTaskSelectedDisplayedInGrid(listTasks);
            string firstRoundGroupUnAllocated = taskAllocationPage
                .GetRoundGroupFirstUnAllocated();
            string firstRoundNameUnAllocated = taskAllocationPage
                .GetRoundNameFirstUnAllocated();
            //Line 10: Select tasks again and drag and drop them to another round for a next day
            taskAllocationPage
                .SelectTwoTaskAgainInGrid()
                .DragAndDropTwoTaskToFirstUnAllocatedRound()
                .IsConfirmationNeededPopup()
                //Line 11: Click on [Allocate All] btn
                .ClickOnAllocateAllBtn()
                .IsReasonNeededPopup()
                .ClickReasonDdAndSelectReason()
                .ClickOnConfirmBtn()
                .VerifyToastMessage(MessageSuccessConstants.TasksAllocatedSuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.TasksAllocatedSuccessMessage);
            taskAllocationPage
                .VerifyTaskNoLongerDisplayedInGrid()
                .HoverDateAndVerifyTaskDisplayGreenColor();
            string timeNow = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            //Line 12: Drag and drop this round to the grid
            taskAllocationPage
                .DragAndDropRoundToGrid()
                .SendKeyInId(listTasks[0].id)
                .VerifyTaskDisplayedInGrid()
                .SendKeyInId(listTasks[1].id)
                .VerifyTaskDisplayedInGrid()
                //.VerifyThirdTaskNameDisplayed(firstRoundGroupUnAllocated + " " + firstRoundNameUnAllocated + " - " + timeNow)
                //Line 13: Close Task allocation form
                .CloseCurrentWindow()
                .SwitchToChildWindow(2)
                .SwitchNewIFrame();
            roundInstanceForm
                .SendKeyInId(listTasks[0].id)
                .VerifyTaskNoLongerDisplayedInGrid()
                .SendKeyInId(listTasks[1].id)
                .VerifyTaskNoLongerDisplayedInGrid();
        }

        [Category("TaskConfirmation")]
        [Category("Huong")]
        [Test(Description = "Verify that worksheet grid is loading in round instance from")]
        public void TC_186_Task_confirmation_screen_loads_in_the_RI_worksheet_grid()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser47.UserName, AutoUser47.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser47);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(MainOption.ServiceStatus)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //Filter any service status
            PageFactoryManager.Get<ServiceStatusPage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            roundInstanceForm.ClickOnWorksheetTab()
                .WaitForLoadingIconToDisappear();
            roundInstanceForm.SwitchToFrame(roundInstanceForm.WorkSheetIFrame);
            roundInstanceForm.WaitForLoadingIconToDisappear();
            roundInstanceForm.VerifyRITableVisible()
                .VerifyNoFilterOnRITable();
        }
    }
}
