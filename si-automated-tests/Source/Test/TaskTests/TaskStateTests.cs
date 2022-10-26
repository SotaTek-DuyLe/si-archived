using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.Maps;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TaskStateTests : BaseTest
    {
        private string taskTypeName = "Standard - Commercial Collection";
        private string roundInstanceId = "6776";
        private string taskId = "14337";
        private string serviceGroupName = "Collections";
        private string serviceName = "Commercial Collections";
        private string descAtTaskConfirmation = "Kennedy Roofing, UNIT B, TWICKENHAM TRADING ESTATE, RUGBY ROAD, TWICKENHAM, TW1 1DQ";
        private string mapObjectName = "COM2 NST";
        private string taskIdInWorksheetMap = "19448";

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Task state sort order (Web) - Scenario 1 - Sort order for all states")]
        public void TC_99_task_state_sort_web_sort_order_for_all_States()
        {
            string taskTypeId = "3";

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
                .InputNumberInSortOrder("1", "1")
                .InputNumberInSortOrder("2", "2")
                .InputNumberInSortOrder("3", "3")
                .InputNumberInSortOrder("4", "4")
                .InputNumberInSortOrder("5", "5")
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
            string[] orderTask = { "Unallocated", "In Progress", "Completed", "Not Completed", "Cancelled" };
            //Verify order [Task State] in [Task] detail
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .ClickOnTaskStateDd()
                .VerifyOrderInTaskStateDd(orderTask)
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
                .VerifyOrderInTaskStateDd(orderTask)
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
                .VerifyTheDisplayOfTheOrderStatus(orderTask);
            //Verify order in [Task Confirmation] Screen - Bulk Update form
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnBulkUpdateBtn()
                .ClickOnStatusDdInBulkUpdatePopup()
                .VerifyOrderInTaskStateDd(orderTask)
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
                .VerifyOrderInTaskStateDd(orderTask)
                //Verify order in [Bulk update] - Map tab
                .ClickOnBulkUpdateBtn()
                .ClickOnStatusDdInBulkUpdatePopup()
                .VerifyOrderInTaskStateDdInBulkUpdate(orderTask);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test]
        [TestCase (new string[] { "1", "0", "2", "0", "3" }, new string[] { "Unallocated", "Completed", "Cancelled", "In Progress", "Not Completed" })]
        public void TC_99_task_state_sort_web_sort_order_for_some_of_the_States(string[] orderNumber, string[] orderStateValues)
        {
            string taskTypeId = "3";

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

    }

}
