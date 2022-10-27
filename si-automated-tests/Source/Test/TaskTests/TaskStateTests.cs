using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
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
        [TestCase(new string[] { "1", "3", "2", "5", "4" }, new string[] { "Unallocated", "Completed", "In Progress", "Cancelled", "Not Completed" }, TestName = "Scenario 1 - Sort order for ALL states")]
        [TestCase(new string[] { "1", "0", "2", "0", "3" }, new string[] { "Unallocated", "Completed", "Cancelled", "In Progress", "Not Completed" }, TestName = "Scenario 2 - Sort order for SOME states")]
        [TestCase(new string[] { "0", "0", "0", "0", "0" }, new string[] { "Unallocated", "In Progress", "Completed", "Not Completed", "Cancelled" }, TestName = "Scenario 3 - NO order for states")]
        public void TC_99_task_state_sort_web_sort_order(string[] orderNumber, string[] orderStateValues)
        {
            string taskId = "14337";
            string descAtTaskConfirmation = "Kennedy Roofing, UNIT B, TWICKENHAM TRADING ESTATE, RUGBY ROAD, TWICKENHAM, TW1 1DQ";
            string mapObjectName = "COM2 NST";
            string taskIdInWorksheetMap = "19448";

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
                .ClickOnTaskStateDd()
                .VerifyOrderInTaskStateDd(orderStateValues)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Verify order in [Task Bulk Update] - Task State detail form
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
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
                .SelectContract(Contract.RMC)
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
                .SendKeyInDesc(descAtTaskConfirmation)
                .VerifyDisplayResultAfterSearchWithDesc(descAtTaskConfirmation)
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
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .WaitForMapsTabDisplayed()
                .ClickOnAnyMapObject(mapObjectName)
                .ClickOnRoundTab()
                .ClickOnFirstShowRoundInstanceBtnRoundTab()
                .ClickOnWorksheetTab()
                .SwitchToWorksheetTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .FilterWorksheetById(taskIdInWorksheetMap)
                .VerifyTheDisplayOfTheWorksheetIdAfterFiltering(taskIdInWorksheetMap)
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
        [TestCase(new string[] { "1", "2", "3", "4", "5" }, 1)]
        public void TC_99_task_state_sort_web_set_on_stop_settings_on_Task_type(string[] orderNumber, int a)
        {
            string taskId = "14339";
            string partyName = "Tesco PLC";
            string[] orderStateValues = { "Cancelled", "In Progress" };
            string[] onlyCancelledStatus = { "Cancelled" };
            string roundName = "REF1-AM";
            string dayName = "Wednesday";
            string descWithPartyTescoPLC = "Tesco Superstore, 20-28 BROAD STREET, TEDDINGTON, TW11 8RF";
            string mapObjectName = "COM2 NST";

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
                .ClickOnTaskStateDd()
                .VerifyOrderInTaskStateDd(orderStateValues)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            //Verify order in [Task Bulk Update] - Task State detail form
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInId(taskId)
                .ClickOnStatusFirstRow()
                .VerifyOrderInTaskStateDd(onlyCancelledStatus)
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
                .SelectContract(Contract.RMC)
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
                .OpenOption(Contract.RMC)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .WaitForMapsTabDisplayed()
                .ClickOnAnyMapObject(mapObjectName)
                .ClickOnRoundTab()
                .ClickOnFirstShowRoundInstanceBtnRoundTab()
                .ClickOnWorksheetTab()
                .SwitchToWorksheetTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MapListingPage>()
                .FilterWorksheetByPartyName(partyName)
                .VerifyTheDisplayOfTheWorksheetIdAfterFilteringParty(partyName)
                .ClickOnStatusInFirstRow()
                .VerifyOrderInTaskStateDd(onlyCancelledStatus)
                //Verify order in [Bulk update] - Map tab
                .ClickOnBulkUpdateBtn()
                .ClickOnStatusDdInBulkUpdatePopup()
                .VerifyOrderInTaskStateDdInBulkUpdate(orderStateValues);
        }

    }


}
