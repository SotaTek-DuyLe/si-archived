using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.Maps;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Task;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TaskStateTests : BaseTest
    {
        private readonly string taskTypeId = "3";
        private readonly string taskTypeName = "Standard - Commercial Collection";
        private readonly string roundInstanceId = "6776";
        private readonly string serviceGroupName = "Collections";
        private readonly string serviceName = "Commercial Collections";

        [Category("Task State")]
        [Category("Chang")]
        [Test]
        [TestCase(new string[] { "1", "3", "2", "5", "4" }, new string[] { "Unallocated", "Completed", "In Progress", "Cancelled", "Not Completed" }, TestName = "TC_99 - Scenario 1 - Sort order for ALL states")]
        [TestCase(new string[] { "1", "0", "2", "0", "3" }, new string[] { "Unallocated", "Completed", "Cancelled", "In Progress", "Not Completed" }, TestName = "TC_99 - Scenario 2 - Sort order for SOME states")]
        [TestCase(new string[] { "0", "0", "0", "0", "0" }, new string[] { "Unallocated", "In Progress", "Completed", "Not Completed", "Cancelled" }, TestName = "TC_99 - Scenario 3 - NO order for states")]
        [TestCase(new string[] { "1", "1", "1", "3", "2" }, new string[] { "Unallocated", "In Progress", "Completed", "Cancelled", "Not Completed" }, TestName = "TC_99 - Scenario 4 - Sort order for states duplicated")]
        [TestCase(new string[] { "1", "0", "3", "0", "2" }, new string[] { "Unallocated", "Cancelled", "Completed", "In Progress", "Not Completed" }, TestName = "TC_99 - Scenario 5 - Verify if the sort order one or more  integers are skipped, the sort order is applied first and then based on the ID asc order is applied")]
        public void TC_99_task_state_sort_web_sort_order(string[] orderNumber, string[] orderStateValues)
        //public void TC_99_task_state_sort_web_sort_order()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string taskId = "14337";
            string mapObjectName = "COM2 NST";
            string fromDateMap = "29/10/2022 00:00";
            string[] onlyCancelledStatus = { "In Progress", "Cancelled" };
            //API: Get current task state
            List<TaskStateDBModel> taskStateDBModels = commonFinder.GetTaskStateByTaskId(taskId);
            string currentTaskState = taskStateDBModels[0].taskstate;
            //string[] orderNumber = new string[] { "1", "3", "2", "5", "4" };
            //string[] orderStateValues = new string[] { "Unallocated", "Completed", "In Progress", "Cancelled", "Not Completed" };

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);

            //Config in IE
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(string.Format(WebUrl.TaskTypeUrlIE, taskTypeId));
            PageFactoryManager.Get<TaskTypeEchoExtraPage>()
                .IsTaskTypePage(taskTypeName)
                .ClickOnStatesTab()
                .InputNumberInSortOrder("1", orderNumber[0])
                .InputNumberInSortOrder("2", orderNumber[1])
                .InputNumberInSortOrder("3", orderNumber[2])
                .InputNumberInSortOrder("4", orderNumber[3])
                .InputNumberInSortOrder("5", orderNumber[4])
                .ClickSaveBtnToUpdateTaskType()
                .SleepTimeInSeconds(3);

            PageFactoryManager.Get<TaskTypeEchoExtraPage>()
                .NavigateToRoundInstanceDetailPage(roundInstanceId)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInId(taskId)
                .ClickOnFirstRound()
                .DoubleClickOnFirstRowAfterFilteringWithDesc()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Verify order [Task State] in [Task] detail
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyCurrentTaskState(currentTaskState)
                .ClickOnTaskStateDd()
                .VerifyOrderInTaskStateDd(orderStateValues)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .WaitForLoadingIconToDisappear();
            //Verify order in [Task Bulk Update] - Task State detail form
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnFirstRound()
                .ClickOnSecondAfterClickingFirstRound()
                .ClickOnBulkUpdateBtn()
                .ClickOnStatusDdInBulkUpdatePopup()
                .VerifyOrderInTaskStateDd(orderStateValues)
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser44);
            //Verify order in [Task Confirmation] Screen
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(serviceGroupName, serviceName)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumnAndVerifyTheOrderStatus(orderStateValues, onlyCancelledStatus);
            //Verify order in [Task Confirmation] Screen - Bulk Update form
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .ClickOnStatusDdInBulkUpdatePopup()
                .VerifyOrderInTaskStateDd(orderStateValues)
                .ClickOnCloseBulkUpdateModel()
                .SwitchToDefaultContent();
            //Verify order in [Maps]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Maps)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .WaitForMapsTabDisplayed()
                .ClickOnAnyMapObject(mapObjectName)
                .SendDateInFromDateInput(fromDateMap)
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .ClickOnRoundInRightHand()
                .ClickOnRoundNameInLeftHand()
                .ClickOnWorksheetTab()
                .SwitchToWorksheetTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .ClickOnStatusInFirstRowAndVerify(orderStateValues, onlyCancelledStatus)
                //Verify order in [Bulk update] - Map tab
                .ClickOnBulkUpdateBtn()
                .ClickOnStatusDdInBulkUpdatePopup()
                .VerifyOrderInTaskStateDdInBulkUpdate(orderStateValues);
        }

        [Category("Task State")]
        [Category("Chang")]
        [Test]
        public void TC_99_task_state_sort_web_set_on_stop_settings_on_Task_type()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            //=> BUG
            string taskId = "14339";
            string partyName = "Tesco PLC";
            string[] orderStateValues = { "In Progress", "Cancelled"};
            string[] onlyCancelledStatus = { "Cancelled" };
            string roundName = "REF1-AM";
            string dayName = "Wednesday";
            string descWithPartyTescoPLC = "Tesco Superstore, 20-28 BROAD STREET, TEDDINGTON, TW11 8RF";
            string mapObjectName = "COM2 NST";
            string fromDateMap = "29/10/2022 00:00";
            string[] orderNumber = { "1", "3", "2", "5", "4" };
            //API: Get current task state
            List<TaskStateDBModel> taskStateDBModels = commonFinder.GetTaskStateByTaskId(taskId);
            string currentTaskState = taskStateDBModels[0].taskstate;

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //Config in IE
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(string.Format(WebUrl.TaskTypeUrlIE, taskTypeId));
            PageFactoryManager.Get<TaskTypeEchoExtraPage>()
                .IsTaskTypePage(taskTypeName)
                .ClickOnStatesTab()
                .InputNumberInSortOrder("1", orderNumber[0])
                .InputNumberInSortOrder("2", orderNumber[1])
                .InputNumberInSortOrder("3", orderNumber[2])
                .InputNumberInSortOrder("4", orderNumber[3])
                .InputNumberInSortOrder("5", orderNumber[4])
                .ClickSaveBtnToUpdateTaskType();
            PageFactoryManager.Get<TaskTypeEchoExtraPage>()
                .NavigateToRoundInstanceDetailPage(roundInstanceId)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInId(taskId)
                .ClickOnFirstRound()
                .DoubleClickOnFirstRowAfterFilteringWithDesc()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnPartyLink()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            //[ON STOP] Parties
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickAccountTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickOnElement(detailPartyPage.OnStopButton);
            detailPartyPage
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyElementText(detailPartyPage.PartyStatus, "On Stop");
            detailPartyPage
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Verify [Task State] in [Task Detail]
            PageFactoryManager.Get<DetailTaskPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailTaskPage>()
                .ClickOnDetailTab()
                .VerifyCurrentTaskState(currentTaskState)
                .ClickOnTaskStateDd()
                .VerifyOrderInTaskStateDd(orderStateValues)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .Refresh()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Verify order in [Task Bulk Update] - Task State detail form
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInId(taskId)
                .ClickOnStatusFirstRow()
                .VerifyOrderTaskStateInFirstRowInWorksheerDd(onlyCancelledStatus)
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser44);
            //Verify order in [Task Confirmation] Screen
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //WEDNESDAY
            string filterDate = "";
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 8);
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 7);
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 6);
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 5);
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 4);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 3);
            }
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(serviceGroupName, serviceName, roundName, dayName)
                .SendDateInScheduledDate(filterDate)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(descWithPartyTescoPLC)
                .VerifyDisplayResultAfterSearchWithDesc(descWithPartyTescoPLC)
                .ClickOnStatusAtFirstColumnAndVerifyTheOrderStatus(orderStateValues, onlyCancelledStatus)
                .SwitchToDefaultContent();
            //Verify order in [Maps]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Maps)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .WaitForMapsTabDisplayed()
                .ClickOnAnyMapObject(mapObjectName)
                .SendDateInFromDateInput(fromDateMap)
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .ClickOnRoundInRightHand()
                .ClickOnRoundNameInLeftHand()
                .ClickOnWorksheetTab()
                .SwitchToWorksheetTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .FilterWorksheetByPartyName(partyName)
                .VerifyTheDisplayOfTheWorksheetIdAfterFilteringParty(partyName)
                .ClickOnStatusInFirstRowAndVerify(onlyCancelledStatus, onlyCancelledStatus);
            //==> BUG
                ////Verify order in [Bulk update] - Map tab
                //.ClickOnBulkUpdateBtn()
                //.ClickOnStatusDdInBulkUpdatePopup()
                //.VerifyOrderInTaskStateDdInBulkUpdate(orderStateValues);
        }

        [Category("Task State")]
        [Category("Chang")]
        [Test(Description = "Task line 1) with sort order for some of the states")]
        public void TC_100_task_line_state_sort_order_for_some_of_states()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            string taskLineId = "1";
            string taskLineName = "Collections";
            string taskId = "14337";
            string[] orderNumber = { "0", "0", "0", "2", "1" };
            string[] orderStatus = { "Cancelled", "Not Completed", "Pending", "Completed" };

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //Config in IE
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(string.Format(WebUrl.TaskLineUrlIE, taskLineId));
            PageFactoryManager.Get<TaskLineEchoExtraPage>()
                .WaitForTaskLineEchoExtraPageDisplayed(taskLineName)
                .ClickOnStatesTab()
                .InputNumberInSortOrder("1", orderNumber[0])
                .InputNumberInSortOrder("2", orderNumber[1])
                .InputNumberInSortOrder("3", orderNumber[2])
                .InputNumberInSortOrder("4", orderNumber[3])
                .InputNumberInSortOrder("5", orderNumber[4])
                .ClickSaveBtnToUpdateTaskLine();
            PageFactoryManager.Get<TaskTypeEchoExtraPage>()
                .NavigateToRoundInstanceDetailPage(roundInstanceId)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInId(taskId)
                .ClickOnFirstRound()
                .DoubleClickOnFirstRowAfterFilteringWithDesc()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Verify order [State] in [Task] detail - Task lines tab
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .VerifyStateOfFirstRowInTaskLineTab("Pending")
                .ClickOnStateOnFirstTaskLineRowAndVerifyOrder(orderStatus)
                //Double click on first [Task lines]
                .DoubleClickFirstTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            TaskLineDetailPage taskLineDetailPage = PageFactoryManager.Get<TaskLineDetailPage>();
            taskLineDetailPage
                .IsTaskLineDetailPage()
                .ClickOnDetailTab();
            string taskLineIdDetail = taskLineDetailPage.GetTaskLineId();
            //API: Get task line state
            List<TaskLineDBModel> taskLineDBModel = commonFinder.GetTaskLineStateByTaskLineId(taskLineIdDetail);
            string taskLineState = taskLineDBModel[0].tasklinestate;

            taskLineDetailPage
                .VerifyStatusInStateDropdown(taskLineState)
                .ClickOnStateDdAndVerify(orderStatus);
        }

        [Category("Task State")]
        [Category("Chang")]
        [Test(Description = "Task line 2) with sort order for all states")]
        public void TC_100_task_line_state_sort_order_for_all_states()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string taskLineId = "1";
            string taskLineName = "Collections";
            string taskId = "14337";
            string[] orderNumber = { "4", "5", "2", "3", "1" };
            string[] orderStatus = { "Cancelled", "Completed", "Not Completed", "Pending" };

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //Config in IE
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(string.Format(WebUrl.TaskLineUrlIE, taskLineId));
            PageFactoryManager.Get<TaskLineEchoExtraPage>()
                .WaitForTaskLineEchoExtraPageDisplayed(taskLineName)
                .ClickOnStatesTab()
                .InputNumberInSortOrder("1", orderNumber[0])
                .InputNumberInSortOrder("2", orderNumber[1])
                .InputNumberInSortOrder("3", orderNumber[2])
                .InputNumberInSortOrder("4", orderNumber[3])
                .InputNumberInSortOrder("5", orderNumber[4])
                .ClickSaveBtnToUpdateTaskLine();
            PageFactoryManager.Get<TaskLineEchoExtraPage>()
                .SleepTimeInSeconds(3);
            PageFactoryManager.Get<TaskTypeEchoExtraPage>()
                .NavigateToRoundInstanceDetailPage(roundInstanceId)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInId(taskId)
                .ClickOnFirstRound()
                .DoubleClickOnFirstRowAfterFilteringWithDesc()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Verify order [State] in [Task] detail - Task lines tab
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .ClickOnStateOnFirstTaskLineRowAndVerifyOrder(orderStatus)
                //Double click on first [Task lines]
                .DoubleClickFirstTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            TaskLineDetailPage taskLineDetailPage = PageFactoryManager.Get<TaskLineDetailPage>();
            taskLineDetailPage
                .IsTaskLineDetailPage()
                .ClickOnDetailTab();
            string taskLineIdDetail = taskLineDetailPage.GetTaskLineId();
            //API: Get task line state
            List<TaskLineDBModel> taskLineDBModel = commonFinder.GetTaskLineStateByTaskLineId(taskLineIdDetail);
            string taskLineState = taskLineDBModel[0].tasklinestate;
            taskLineDetailPage
                .ClickOnStateDdAndVerify(orderStatus);
        }

        [Category("Task State")]
        [Category("Chang")]
        [Test(Description = "Task line 3) with sort order not set")]
        public void TC_100_task_line_state_sort_order_not_set()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string taskLineId = "1";
            string taskLineName = "Collections";
            string taskId = "14337";
            string[] orderNumber = { "0", "0", "0", "0", "0" };
            string[] orderStatus = { "Pending", "Completed", "Not Completed", "Cancelled" };

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //Config in IE
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(string.Format(WebUrl.TaskLineUrlIE, taskLineId));
            PageFactoryManager.Get<TaskLineEchoExtraPage>()
                .WaitForTaskLineEchoExtraPageDisplayed(taskLineName)
                .ClickOnStatesTab()
                .InputNumberInSortOrder("1", orderNumber[0])
                .InputNumberInSortOrder("2", orderNumber[1])
                .InputNumberInSortOrder("3", orderNumber[2])
                .InputNumberInSortOrder("4", orderNumber[3])
                .InputNumberInSortOrder("5", orderNumber[4])
                .ClickSaveBtnToUpdateTaskLine();
            PageFactoryManager.Get<TaskTypeEchoExtraPage>()
                .NavigateToRoundInstanceDetailPage(roundInstanceId)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInId(taskId)
                .ClickOnFirstRound()
                .DoubleClickOnFirstRowAfterFilteringWithDesc()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Verify order [State] in [Task] detail - Task lines tab
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .ClickOnStateOnFirstTaskLineRowAndVerifyOrder(orderStatus)
                //Double click on first [Task lines]
                .DoubleClickFirstTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            TaskLineDetailPage taskLineDetailPage = PageFactoryManager.Get<TaskLineDetailPage>();
            taskLineDetailPage
                .IsTaskLineDetailPage()
                .ClickOnDetailTab();
            string taskLineIdDetail = taskLineDetailPage.GetTaskLineId();
            //API: Get task line state
            List<TaskLineDBModel> taskLineDBModel = commonFinder.GetTaskLineStateByTaskLineId(taskLineIdDetail);
            string taskLineState = taskLineDBModel[0].tasklinestate;
            taskLineDetailPage
                .ClickOnStateDdAndVerify(orderStatus);
        }

        [Category("Task State")]
        [Category("Chang")]
        [Test(Description = "Task line 4) Verify if the sort order integer repeats itself, the order based on the ID is applied")]
        public void TC_100_task_line_state_sort_order_repeats_itself()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string taskLineId = "1";
            string taskLineName = "Collections";
            string taskId = "14337";
            string[] orderNumber = { "1", "1", "2", "1", "2" };
            string[] orderStatus = { "Pending", "Not Completed", "Completed", "Cancelled" };

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //Config in IE
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(string.Format(WebUrl.TaskLineUrlIE, taskLineId));
            PageFactoryManager.Get<TaskLineEchoExtraPage>()
                .WaitForTaskLineEchoExtraPageDisplayed(taskLineName)
                .ClickOnStatesTab()
                .InputNumberInSortOrder("1", orderNumber[0])
                .InputNumberInSortOrder("2", orderNumber[1])
                .InputNumberInSortOrder("3", orderNumber[2])
                .InputNumberInSortOrder("4", orderNumber[3])
                .InputNumberInSortOrder("5", orderNumber[4])
                .ClickSaveBtnToUpdateTaskLine();
            PageFactoryManager.Get<TaskTypeEchoExtraPage>()
                .NavigateToRoundInstanceDetailPage(roundInstanceId)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInId(taskId)
                .ClickOnFirstRound()
                .DoubleClickOnFirstRowAfterFilteringWithDesc()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Verify order [State] in [Task] detail - Task lines tab
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnTaskLineTab()
                .ClickOnStateOnFirstTaskLineRowAndVerifyOrder(orderStatus)
                //Double click on first [Task lines]
                .DoubleClickFirstTaskLine()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            TaskLineDetailPage taskLineDetailPage = PageFactoryManager.Get<TaskLineDetailPage>();
            taskLineDetailPage
                .IsTaskLineDetailPage()
                .ClickOnDetailTab();
            string taskLineIdDetail = taskLineDetailPage.GetTaskLineId();
            //API: Get task line state
            List<TaskLineDBModel> taskLineDBModel = commonFinder.GetTaskLineStateByTaskLineId(taskLineIdDetail);
            string taskLineState = taskLineDBModel[0].tasklinestate;
            taskLineDetailPage
                .ClickOnStateDdAndVerify(orderStatus);
        }

        [Category("Task state")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_314_Costs_on_Task()
        {
            //Verify that Costs tab is added to Task form
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser56.UserName, AutoUser56.Password)
                .IsOnHomePage(AutoUser56);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage.ClickOnElement(detailTaskPage.CostTab);
            detailTaskPage.WaitForLoadingIconToDisappear();
            detailTaskPage.VerifyCostLineColumnsDisplayCorrectly();

            //Verify that costs display correctly in the Costs grid on Task form: Task and Task Line cost line
            //2)In Details tab, set Subcontract = True; select Subcontract Reason, select Subcontractor = North Star Environmental Services > Save task.
            detailTaskPage.ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
                .FilterTaskState("contains any of", "In Progress")
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnDetailTab();
            detailTaskPage.WaitForLoadingIconToDisappear();
            detailTaskPage.WaitForLoadingIconToDisappear();
            detailTaskPage.ClickOnElement(detailTaskPage.SubContractCheckbox);
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.SubContractReasonSelect, "No Capacity");
            detailTaskPage.ClickOnElement(detailTaskPage.SubContractorButton);
            detailTaskPage.SleepTimeInMiliseconds(200);
            detailTaskPage.SelectByDisplayValueOnUlElement(detailTaskPage.SubContractorUl, "North Star Environmental Services");
            detailTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskPage.VerifySelectedValue(detailTaskPage.taskStateDd, "Unallocated");
            string taskId = detailTaskPage.GetCurrentUrl().Split('/').LastOrDefault();
            detailTaskPage.SwitchToFirstWindow()
                .SwitchNewIFrame();

            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Applications)
               .OpenOption("Subcontracted Tasks")
               .SwitchNewIFrame()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SubcontractedTasksListPage>()
                .IsSubcontractedTasksLoaded();
            PageFactoryManager.Get<SubcontractedTasksListPage>()
                .FilterTaskId("contains any of", taskId)
                .VerifyTaskExists();

            //3) In  details tab of this task, set Task status=Completed. Save task.
            detailTaskPage.SwitchToChildWindow(2);
            detailTaskPage.ClickOnDetailTab();
            detailTaskPage.WaitForLoadingIconToDisappear();
            detailTaskPage.SelectTextFromDropDown(detailTaskPage.taskStateDd, "Completed");
            detailTaskPage.ClickSaveBtn()
               .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            CommonFinder commonFinder = new CommonFinder(DbContext);
            Assert.IsTrue(commonFinder.GetCostLineDBModels(taskId).Count != 0);

            detailTaskPage.ClickOnElement(detailTaskPage.CostTab);
            detailTaskPage.WaitForLoadingIconToDisappear();
            detailTaskPage.VerifyCostLinesExist();
        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task_confirmation_completed_1()
        {
            string dateInPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -2);
            string dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -2);
            DateTime temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if(temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -4);
            }
            string dateInFurtherPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -5);
            dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -5);
            temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInFurtherPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -7);
            }
            string description = "Tesco Superstore, 20-28 BROAD STREET, TEDDINGTON, TW11 8RF";


            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            //Step 9
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(Contract.Commercial)
                .SendDateInScheduledDate(dateInPastInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn();
            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Completed")
                .SelectResolutionCode("random");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem();
            var expectedDate = CommonUtil.GetUtcTimeNowMinusHour(1, "dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<CommonBrowsePage>()
                .SleepTimeInSeconds(2);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(1, "Completed Date", expectedDate);



            //Step 10
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SelectContract(Contract.Commercial)
                .SendDateInScheduledDate(dateInFurtherPastInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Completed")
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClicKCompletedDateAtFirstColumn()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2));
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SleepTimeInSeconds(2);
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);
            PageFactoryManager.Get<TaskConfirmationPage>()
               .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(1, "Completed Date", expectedDate);
        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task_confirmation_completed_2()
        {
            string saveToast = "Task Saved";
            string dateNowInSchedule = CommonUtil.GetLocalTimeNow("dd");
            string dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 0);
            DateTime temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateNowInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -2);
            }

            string dateInFutreInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", 7);
            dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 0);
            temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInFutreInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", 8);
            }


            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            //Step 9
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(Contract.Commercial)
                .SendDateInScheduledDate(dateInFutreInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Completed")
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            var expectedDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(3, "Completed Date", expectedDate);

            //Step 14
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SelectContract(Contract.Commercial)
                .SendDateInScheduledDate(dateNowInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Completed")
                .ClickCompletedDateAtBulkUpdate()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2))
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(3, "Completed Date", expectedDate);
        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_service_status_completed_1()
        {
            string description = "Tesco Superstore, 20-28 BROAD STREET, TEDDINGTON, TW11 8RF";
            string tempDescription = "Teddington Station, TEDDINGTON RAILWAY STATION, VICTORIA ROAD, TEDDINGTON, TW11 0BB";



            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(SubOption.ServiceStatus)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemByField("Service", Contract.Commercial, false)
                .FilterItemByField("Round", "Monday", false)
                .FilterItemByField("Round Group", "REF1-AM", false)
                .SleepTimeInSeconds(2)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(1)
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();
            //Step 9


            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn();

            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Completed")
                .SelectResolutionCode("random");

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem()
                .SleepTimeInSeconds(2);

            var expectedDate = CommonUtil.GetUtcTimeNowMinusHour(1, "dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(1, "Completed Date", expectedDate);


            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .ClickRefreshBtn();

            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Service", Contract.Commercial, false)
               .FilterItemByField("Round", "Wednesday", false)
               .FilterItemByField("Round Group", "REF1-AM", false)
               .SleepTimeInSeconds(2)
               .WaitForLoadingIconToDisappear();


            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(1)
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn();

            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", tempDescription, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Completed")
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClicKCompletedDateAtFirstColumn()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2));
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem();
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(1, "Completed Date", expectedDate);
        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_service_status_completed_2()
        {
            string saveToast = "Task Saved";



            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(SubOption.ServiceStatus)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn();

            //Step 13

            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemByField("Service", Contract.Commercial, false)
                .FilterItemByField("Round", "Monday", false)
                .FilterItemByField("Round Group", "REF1-AM", false)
                .SleepTimeInSeconds(2)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(7)
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Completed")
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            var expectedDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(3, "Completed Date", expectedDate);

            //Step 14
            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .ClickRefreshBtn();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(8)
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Completed")
                .ClickCompletedDateAtBulkUpdate()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2))
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(3, "Completed Date", expectedDate);
        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task_confirmation_not_completed_1()
        {
            string dateInPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -2);
            string dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -2);
            DateTime temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -4);
            }

            string dateInFurtherPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -5);
            dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -5);
            temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInFurtherPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -7);
            }
            string description = "Tesco Superstore, 20-28 BROAD STREET, TEDDINGTON, TW11 8RF";


            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            //Step 9
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(Contract.Commercial)
                .SendDateInScheduledDate(dateInPastInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn();
            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Not Completed")
                .SelectResolutionCode("random");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem();
            var expectedDate = CommonUtil.GetUtcTimeNowMinusHour(1, "dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<CommonBrowsePage>()
                .SleepTimeInSeconds(2);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(1, "Completed Date", expectedDate);

            //Step 10
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SelectContract(Contract.Commercial)
                .SendDateInScheduledDate(dateInFurtherPastInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Not Completed")
                .SelectResolutionCode("random"); PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Not Completed")
                .SelectResolutionCode("random")
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClicKCompletedDateAtFirstColumn()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2));
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem();
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);
            PageFactoryManager.Get<CommonBrowsePage>()
                .SleepTimeInSeconds(2);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(1, "Completed Date", expectedDate);
        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task_confirmation_not_completed_2()
        {
            string saveToast = "Task Saved";
            string dateNowInSchedule = CommonUtil.GetLocalTimeNow("dd");
            string dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 0);
            string description = "Express";
            DateTime temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateNowInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", 1);
            }

            string dateInFutreInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", 7);
            dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);
            temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInFutreInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", 8);
            }

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            //Step 13
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(Contract.Commercial)
                .SendDateInScheduledDate(dateInFutreInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Not Completed")
                .SelectResolutionCodeInBulkUpdatePopup("random")
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            var expectedDate = CommonUtil.GetUtcTimeNowMinusHour(1, "dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(3, "Completed Date", expectedDate);

            //Step 14
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SelectContract(Contract.Commercial)
                .SendDateInScheduledDate(dateNowInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();

            PageFactoryManager.Get<CommonBrowsePage>()
              .FilterItemByField("Description", description, false);

            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Not Completed")
                .SelectResolutionCodeInBulkUpdatePopup("random")
                .ClickEndDateAtBulkUpdate()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2))
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);
            var expectedCompletedDate = CommonUtil.GetUtcTimeNowMinusHour(1, "dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(3, "Completed Date", expectedCompletedDate);
        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_service_status_not_completed_1()
        {
            string description = "Tesco Superstore, 20-28 BROAD STREET, TEDDINGTON, TW11 8RF";


            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(SubOption.ServiceStatus)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemByField("Service", Contract.Commercial, false)
                .FilterItemByField("Round", "Monday", false)
                .FilterItemByField("Round Group", "REF1-AM", false)
                .SleepTimeInSeconds(2)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(CommonUtil.GetRandomNumberBetweenRange(1,20))
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();
            //Step 40


            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn();

            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Not Completed")
                .SelectResolutionCode("random");

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem()
                .SleepTimeInSeconds(2);

            var expectedDate = CommonUtil.GetUtcTimeNowMinusHour(1, "dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(1, "Completed Date", expectedDate);


            //CANNOT EDIT COMPLETE DATE
            //PageFactoryManager.Get<BasePage>()
            //    .CloseCurrentWindow()
            //    .SwitchToLastWindow()
            //    .SwitchNewIFrame()
            //    .ClickRefreshBtn();

            //PageFactoryManager.Get<CommonBrowsePage>()
            //   .FilterItemByField("Service", Contract.Commercial, false)
            //   .FilterItemByField("Round", "Wednesday", false)
            //   .FilterItemByField("Round Group", "REF1-AM", false)
            //   .SleepTimeInSeconds(2)
            //   .WaitForLoadingIconToDisappear();


            //PageFactoryManager.Get<CommonBrowsePage>()
            //    .OpenResultNumber(2)
            //    .SwitchToLastWindow<RoundInstanceDetailPage>()
            //    .IsRoundInstancePage()
            //    .SwitchToTab("Worksheet")
            //    .SwitchNewIFrame();

            //PageFactoryManager.Get<TaskConfirmationPage>()
            //    .ClickOnExpandRoundsBtn();

            //PageFactoryManager.Get<CommonBrowsePage>()
            //   .FilterItemByField("Description", description, false);
            //PageFactoryManager.Get<TaskConfirmationPage>()
            //    .ClickOnStatusAtFirstColumn()
            //    .SelectStatus("Not Completed")
            //    .SelectResolutionCode("random")
            //    .ScrollMaxToTheRightOfGrid();
            //PageFactoryManager.Get<TaskConfirmationPage>()
            //    .ClicKCompletedDateAtFirstColumn()
            //    .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2));
            //PageFactoryManager.Get<TaskConfirmationPage>()
            //    .ScrollMaxToTheLeftOfGrid();
            //PageFactoryManager.Get<CommonBrowsePage>()
            //    .DeselectActiveItem();
            //expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);

            //PageFactoryManager.Get<TaskConfirmationPage>()
            //    .ClickOnStatusAtFirstColumn();
            //PageFactoryManager.Get<TaskConfirmationPage>()
            //    .ScrollMaxToTheRightOfGrid();
            //PageFactoryManager.Get<CommonBrowsePage>()
            //    .VerifyDateValueInActiveRow(1, "End Date", expectedDate)
            //    .VerifyDateValueInActiveRow(1, "Completed Date", expectedDate);

        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_service_status_not_completed_2()
        {
            string saveToast = "Task Saved";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(SubOption.ServiceStatus)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn();

            //Step 40

            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemByField("Service", Contract.Commercial, false)
                .FilterItemByField("Round", "Monday", false)
                .FilterItemByField("Round Group", "REF1-AM", false)
                .SleepTimeInSeconds(2)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(CommonUtil.GetRandomNumberBetweenRange(1, 20))
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Not Completed")
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            var expectedDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(3, "Completed Date", expectedDate);

            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .ClickRefreshBtn();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(CommonUtil.GetRandomNumberBetweenRange(1, 20))
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Not Completed")
                .ClickEndDateAtBulkUpdate()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2))
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);
            var now = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 0);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate)
                .VerifyDateValueInActiveRow(3, "Completed Date", now);
        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task_confirmation_cancelled_1()
        {
            string dateInPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -2);

            string dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -2);
            DateTime temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -4);
            }
            string dateInFurtherPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -5);
            dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -5);
            temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInFurtherPastInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -7);
            }
            string description = "Tesco Superstore, 20-28 BROAD STREET, TEDDINGTON, TW11 8RF";


            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            //Step 9
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(Contract.Commercial)
                .SendDateInScheduledDate(dateInPastInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn();
            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Cancelled")
                .SelectResolutionCode("last");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem();
            var expectedDate = CommonUtil.GetUtcTimeNowMinusHour(1, "dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<CommonBrowsePage>()
                .SleepTimeInSeconds(2);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate);

        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task_confirmation_cancelled_2()
        {
            string saveToast = "Task Saved";
            string dateNowInSchedule = CommonUtil.GetLocalTimeNow("dd");
            string dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 0);
            DateTime temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateNowInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", -2);
            }

            string dateInFutreInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", 7);
            dateToValidate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 0);
            temp = DateTime.ParseExact(dateToValidate, "dd/MM/yyyy", null);
            if (temp.DayOfWeek == DayOfWeek.Sunday)
            {
                dateInFutreInSchedule = CommonUtil.GetLocalTimeMinusDay("dd", 8);
            }


            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);
            //
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            //Step 9
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(Contract.Commercial)
                .SendDateInScheduledDate(dateInFutreInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Cancelled")
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            var expectedDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate);

            //Step 14
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SelectContract(Contract.Commercial)
                .SendDateInScheduledDate(dateNowInSchedule)
                .ClickGoBtn()
                .IsConfirmationNeededPopup()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Cancelled")
                .ClickEndDateAtBulkUpdate()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2))
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate);

        }

        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_service_status_cancelled_1()
        {
            string description = "Tesco Superstore, 20-28 BROAD STREET, TEDDINGTON, TW11 8RF";


            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(SubOption.ServiceStatus)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemByField("Service", Contract.Commercial, false)
                .FilterItemByField("Round", "Monday", false)
                .FilterItemByField("Round Group", "REF1-AM", false)
                .SleepTimeInSeconds(2)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(CommonUtil.GetRandomNumberBetweenRange(1, 20))
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();
            //Step 40


            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn();

            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItemByField("Description", description, false);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn()
                .SelectStatus("Cancelled")
                .SelectResolutionCode("last");

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .DeselectActiveItem()
                .SleepTimeInSeconds(2);

            var expectedDate = CommonUtil.GetUtcTimeNowMinusHour(1, "dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnStatusAtFirstColumn();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(1, "End Date", expectedDate);

        }
        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_service_status_cancelled_2()
        {

            string saveToast = "Task Saved";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .ExpandOption(SubOption.ServiceStatus)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear()
                .ClickRefreshBtn();

            //Step 40

            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemByField("Service", Contract.Commercial, false)
                .FilterItemByField("Round", "Monday", false)
                .FilterItemByField("Round Group", "REF1-AM", false)
                .SleepTimeInSeconds(2)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(CommonUtil.GetRandomNumberBetweenRange(1, 20))
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Cancelled")
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            var expectedDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy HH:mm");
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate);

            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .ClickRefreshBtn();

            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenResultNumber(CommonUtil.GetRandomNumberBetweenRange(1, 20))
                .SwitchToLastWindow<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .SwitchToTab("Worksheet")
                .SwitchNewIFrame();

            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ScrollMaxToTheLeftOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .SelectFirstNumberOfItem(3);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .SelectStatusInBulkUpdatePopup("Cancelled")
                .ClickEndDateAtBulkUpdate()
                .InsertDayInFutre(CommonUtil.GetLocalTimeMinusDay("dd", 2))
                .ClickOnConfirmBtn()
                .VerifyToastMessages(new List<string> { saveToast, saveToast, saveToast });
            expectedDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy HH:mm", 2);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ScrollMaxToTheRightOfGrid();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyDateValueInActiveRow(3, "End Date", expectedDate);

        }
        [Category("Task State")]
        [Category("Dee")]
        [TestCase(new object[] { "Completed" })]
        [TestCase(new object[] { "Not Completed" })]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task(string stateName)
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            var taskId = commonFinder.GetRandomTaskId();
            var url = WebUrl.MainPageUrl + "web/tasks/" + taskId;

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(url);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password);

            //Set task state only
            PageFactoryManager.Get<TaskDetailTab>()
                .IsOnTaskDetailTab()
                .ClickStateDetais()
                .ChooseTaskState(stateName)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyEndDate(CommonUtil.GetUtcTimeNow("dd/MM/yyyy"))
                .VerifyCompletionDate(CommonUtil.GetUtcTimeNow("dd/MM/yyyy"));

            //Set task state and completion date
            taskId = commonFinder.GetRandomTaskId();
            url = WebUrl.MainPageUrl + "web/tasks/" + taskId;

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(url);
            PageFactoryManager.Get<TaskDetailTab>()
                .IsOnTaskDetailTab()
                .ClickStateDetais()
                .ChooseTaskState(stateName)
                .SelectDateFromCalendar("Completion Date", CommonUtil.GetCustomUtcDay(2, "d")) //future day
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyEndDate(CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 2))
                .VerifyCompletionDate(CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 2));

            //Set task state and end date + completion date
            taskId = commonFinder.GetRandomTaskId();
            url = WebUrl.MainPageUrl + "web/tasks/" + taskId;

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(url);
            PageFactoryManager.Get<TaskDetailTab>()
                .IsOnTaskDetailTab()
                .ClickStateDetais()
                .ChooseTaskState(stateName)
                .SelectDateFromCalendar("Completion Date", CommonUtil.GetCustomUtcDay(2, "d")) //future day
                .SelectDateFromCalendar("End Date", CommonUtil.GetCustomUtcDay(0, "d")) //past day
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyEndDate(CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 2))
                .VerifyCompletionDate(CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 2));

            //Set task state and end date
            taskId = commonFinder.GetRandomTaskId();
            url = WebUrl.MainPageUrl + "web/tasks/" + taskId;

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(url);
            PageFactoryManager.Get<TaskDetailTab>()
                .IsOnTaskDetailTab()
                .ClickStateDetais()
                .ChooseTaskState(stateName)
                .SelectDateFromCalendar("End Date", CommonUtil.GetCustomUtcDay(3, "d")) //future day
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyEndDate(CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 3))
                .VerifyCompletionDate(CommonUtil.GetUtcTimeMinusDay("dd/MM/yyyy", 0));
        }

        [Category("Task State")]
        [Category("Dee")]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task_cancelled()
        {
            string stateName = "Cancelled";
            CommonFinder commonFinder = new CommonFinder(DbContext);
            var taskId = commonFinder.GetRandomTaskId();
            var url = WebUrl.MainPageUrl + "web/tasks/" + taskId;

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(url);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password);

            //Set task state only
            PageFactoryManager.Get<TaskDetailTab>()
                .IsOnTaskDetailTab()
                .ClickStateDetais()
                .ChooseTaskState(stateName)
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyEndDate(CommonUtil.GetUtcTimeNow("dd/MM/yyyy"));

            //Set task state and end date
            taskId = commonFinder.GetRandomTaskId();
            url = WebUrl.MainPageUrl + "web/tasks/" + taskId;
            var date = CommonUtil.GetRandomNumberBetweenRange(1, 5);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(url);
            PageFactoryManager.Get<TaskDetailTab>()
                .IsOnTaskDetailTab()
                .ClickStateDetais()
                .ChooseTaskState(stateName)
                .SelectDateFromCalendar("End Date", CommonUtil.GetCustomUtcDay(date, "d")) //any day
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyEndDate(CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", date));

            //Set task state and end date + completion date
            taskId = commonFinder.GetRandomTaskId();
            url = WebUrl.MainPageUrl + "web/tasks/" + taskId;

            date = CommonUtil.GetRandomNumberBetweenRange(1, 5);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(url);
            PageFactoryManager.Get<TaskDetailTab>()
                .IsOnTaskDetailTab()
                .ClickStateDetais()
                .ChooseTaskState(stateName)
                .SelectDateFromCalendar("Completion Date", CommonUtil.GetCustomUtcDay(CommonUtil.GetRandomNumberBetweenRange(-3, 3), "d")) //any day
                .SelectDateFromCalendar("End Date", CommonUtil.GetCustomUtcDay(date, "d")) //any day
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskDetailTab>()
                .VerifyEndDate(CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", date))
                .VerifyCompletionDate("");
        }
        [Category("Task State")]
        [Category("Dee")]
        [TestCase(new object[] { "Completed" })]
        [TestCase(new object[] { "Not Completed" })]
        [Test]
        public void TC_176_verify_task_state_date_change_in_task_bulk_update_completed(string stateName)
        {
            int numberOfTasks = 3;

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser44.UserName, AutoUser44.Password)
                .IsOnHomePage(AutoUser44);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItemByField("Task State", "In Progress")
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CommonTaskPage>()
                .SelectFirstNumberOfItem(numberOfTasks)
                .ClickBulkUpdateBtn()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksBulkUpdatePage>()
                .ClickFirstToggleArrow()
                .SelectTaskState(stateName, "1")
                .SelectResolutionCode("last", "1")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);

            var ids = PageFactoryManager.Get<BasePage>()
                .GetCurrentUrl().Replace("https://test.echoweb.co.uk/web/task-bulk-updates?ids=", "");
            var listOfIds = ids.Split(",").ToList();
            PageFactoryManager.Get<BasePage>()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClearFilters()
                .SleepTimeInSeconds(10);
            //var count = 0;

            for (int i = 0; i < listOfIds.Count; i++)
            {
                PageFactoryManager.Get<BasePage>()
                       .ClickRefreshBtn()
                       .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<CommonBrowsePage>()
                    .FilterItemByField("ID", listOfIds[i])
                    .SelectFirstNumberOfItem(1)
                    .VerifyDateValueInActiveRow(1, "Completed Date", CommonUtil.GetUtcTimeNow("dd/MM/yyyy HH:mm"))
                    .VerifyDateValueInActiveRow(1, "End Date", CommonUtil.GetUtcTimeNow("dd/MM/yyyy HH:mm"));
            }

        }
    }
}
