using System;
using System.Collections.Generic;
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
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);
            string taskId = "14337";
            string mapObjectName = "COM2 NST";
            string fromDateMap = "29/10/2022 00:00";
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
                .ClickOnStatusAtFirstColumn()
                .VerifyTheDisplayOfTheOrderStatus(orderStateValues);
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
                .ClickOnStatusInFirstRow()
                .VerifyOrderInTaskStateDd(orderStateValues)
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
            string[] orderStateValues = { "Cancelled", "In Progress" };
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
                .ClickOnStatusAtFirstColumn()
                .VerifyTheDisplayOfTheOrderStatus(onlyCancelledStatus)
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
                .ClickOnStatusInFirstRow()
                .VerifyOrderInTaskStateDd(onlyCancelledStatus);
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

    }
}
