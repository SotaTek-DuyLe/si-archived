using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using TaskAllocationPage = si_automated_tests.Source.Main.Pages.Applications.TaskAllocationPage;

namespace si_automated_tests.Source.Test.SDMActionTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class MenuActionTests : BaseTest
    {
        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Verify that a menu of Actions is displayed by clicking the right mouse button - Richmond Commercial - point address")]
        public void TC_132_Test_1_RMC_Verify_that_a_menu_of_actions_is_displayed_by_clicking_the_right_mouse_button_point_address()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.SERVICE_DATA_MANAGEMENT)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.RMC)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            //Step 1: Line 8: Right-click on any of the cells with ServiceTask Schedule
            string[] addServiceTaskScheduleAction = { CommonConstants.ActionMenuSDM[0] };
            serviceDataManagementPage
                .RightClickOnFirstRowWithServiceTaskSchedule()
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(addServiceTaskScheduleAction);
            //Step 2: Line 9: Right-click on any of the cells with no ServiceTask Schedule
            string[] otherAction = { CommonConstants.ActionMenuSDM[1], CommonConstants.ActionMenuSDM[2], CommonConstants.ActionMenuSDM[3], CommonConstants.ActionMenuSDM[4]};
            serviceDataManagementPage
                .RightClickOnFirstRowWithoutServiceTaskSchedule()
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(otherAction);
            //Step 3: Line 10: Select MULTIPLE cells containing Service Task Schedule
            serviceDataManagementPage
                .ClickOnMultipleRowsWithServiceTaskSchedule()
                .SelectMultipleRowsWithServiceTaskSchedule()
                .RightClickOnMultipleRowWithServiceTaskSchedule()
                .VerifyActionInActionMenuDisabled(addServiceTaskScheduleAction);
            //Step 4: Line 11: Select MULTIPLE cells not containing Service Task Schedule
            serviceDataManagementPage
                .SelectMultipleRowsWithoutServiceTaskSchedule()
                .RightClickOnMultipleRowWithoutServiceTaskSchedule()
                .VerifyActionInActionMenuDisabled(otherAction);
            //Step 5: Line 12: Right click on any of the cells with Service Unit
            string[] actionNotAvailable = { CommonConstants.ActionMenuSU[0] };
            string[] actionEnabled = { CommonConstants.ActionMenuSU[1] };
            serviceDataManagementPage
                .RightClickOnFirstCellWithServiceUnit()
                .VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuDisabled(actionNotAvailable)
                .VerifyActionInActionMenuEnabled(actionEnabled);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Verify that a menu of Actions is displayed by clicking the right mouse button - Richmond - point address")]
        public void TC_132_Test_1_RM_Verify_that_a_menu_of_actions_is_displayed_by_clicking_the_right_mouse_button_point_address()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.SERVICE_DATA_MANAGEMENT)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.RM)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            //Step 1: Line 13: Right-click on any of the cells with Service Unit containing icon of Service Unit with multiple Service Unit Points
            string[] actionEnabled = { CommonConstants.ActionMenuSU[0] };
            serviceDataManagementPage
                .RightClickOnCellWithMutipleServiceUnitPoint()
                //.VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuEnabled(actionEnabled);

        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Verify that a menu of Actions is displayed by clicking the right mouse button - point segment")]
        public void TC_132_Test_1_RMC_Verify_that_a_menu_of_actions_is_displayed_by_clicking_the_right_mouse_button_point_segment()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.SERVICE_DATA_MANAGEMENT)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Segment")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.RM)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            //Step 1: Line 8: Right-click on any of the cells with ServiceTask Schedule
            string[] addServiceTaskScheduleAction = { CommonConstants.ActionMenuSDM[0] };
            serviceDataManagementPage
                .RightClickOnFirstRowWithServiceTaskSchedule()
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(addServiceTaskScheduleAction);
            //Step 2: Line 9: Right-click on any of the cells with no ServiceTask Schedule
            string[] otherAction = { CommonConstants.ActionMenuSDM[1], CommonConstants.ActionMenuSDM[2], CommonConstants.ActionMenuSDM[3], CommonConstants.ActionMenuSDM[4] };
            serviceDataManagementPage
                .RightClickOnFirstRowWithoutServiceTaskSchedule()
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(otherAction);
           
            //Step 4: Line 11: Select MULTIPLE cells not containing Service Task Schedule
            serviceDataManagementPage
                .RightClickOnMultipleServiceTaskScheduleSegment()
                .RightClickOnMultipleRowWithoutServiceTaskSchedule()
                .VerifyActionInActionMenuDisabled(otherAction);
            //Step 5: Line 12: Right click on any of the cells with Service Unit
            string[] actionNotAvailable = { CommonConstants.ActionMenuSU[0] };
            //string[] actionEnabled = { CommonConstants.ActionMenuSU[1] };
            serviceDataManagementPage
                .RightClickOnFirstCellWithServiceUnit()
                //.VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuDisabled(actionNotAvailable);
                //.VerifyActionInActionMenuEnabled(actionEnabled);
            //Step 1: Line 13: Right-click on any of the cells with Service Unit containing icon of Service Unit with multiple Service Unit Points
            string[] actionEnabled = { CommonConstants.ActionMenuSU[0] };
            serviceDataManagementPage
                .RightClickOnCellWithMutipleServiceUnitPoint()
                //.VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuEnabled(actionEnabled);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Verify that a menu of Actions is displayed by clicking the right mouse button - point node")]
        public void TC_132_Test_1_RM_Verify_that_a_menu_of_actions_is_displayed_by_clicking_the_right_mouse_button_point_node()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.SERVICE_DATA_MANAGEMENT)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Node")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.RM)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            //Step 1: Line 8: Right-click on any of the cells with ServiceTask Schedule
            string[] addServiceTaskScheduleAction = { CommonConstants.ActionMenuSDM[0] };
            serviceDataManagementPage
                .RightClickOnFirstRowWithServiceTaskSchedule()
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(addServiceTaskScheduleAction)
                .ClickOutOfAction();
            //Step 2: Line 9: Right-click on any of the cells with no ServiceTask Schedule
            string[] otherAction = { CommonConstants.ActionMenuSDM[1], CommonConstants.ActionMenuSDM[2], CommonConstants.ActionMenuSDM[3], CommonConstants.ActionMenuSDM[4] };
            serviceDataManagementPage
                .RightClickOnFirstRowWithoutServiceTaskSchedule()
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(otherAction)
                .ClickOutOfAction();
            //Step 3: Line 10: Select MULTIPLE cells containing Service Task Schedule => No data to test
            //Step 4: Line 11: Select MULTIPLE cells not containing Service Task Schedule
            serviceDataManagementPage
                .SelectMultipleRowsWithoutServiceTaskSchedule()
                .RightClickOnMultipleRowWithoutServiceTaskSchedule()
                .VerifyActionInActionMenuDisabled(otherAction)
                .ClickOutOfAction();
            //Step 5: Line 12: Right click on any of the cells with Service Unit
            string[] actionNotAvailable = { CommonConstants.ActionMenuSU[0] };
            string[] actionEnabled = { CommonConstants.ActionMenuSU[1] };
            serviceDataManagementPage
                .RightClickOnFirstCellWithServiceUnit()
                //.VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuDisabled(actionNotAvailable);
                //.VerifyActionInActionMenuEnabled(actionEnabled);
            //Step 1: Line 13: Right-click on any of the cells with Service Unit containing icon of Service Unit with multiple Service Unit Points
            // No data to test
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Verify that a menu of Actions is displayed by clicking the right mouse button - point area")]
        public void TC_132_Test_1_RM_Verify_that_a_menu_of_actions_is_displayed_by_clicking_the_right_mouse_button_point_area()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.SERVICE_DATA_MANAGEMENT)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Area")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.RM)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            //Step 1: Line 8: Right-click on any of the cells with ServiceTask Schedule
            string[] addServiceTaskScheduleAction = { CommonConstants.ActionMenuSDM[0] };
            serviceDataManagementPage
                .RightClickOnFirstRowWithServiceTaskSchedule()
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(addServiceTaskScheduleAction)
                .ClickOutOfAction();
            //Step 2: Line 9: Right-click on any of the cells with no ServiceTask Schedule => No data to test
            //Step 3: Line 10: Select MULTIPLE cells containing Service Task Schedule => No data to test
            //Step 4: Line 11: Select MULTIPLE cells not containing Service Task Schedule => No data to test
            //Step 5: Line 12: Right click on any of the cells with Service Unit
            string[] actionNotAvailable = { CommonConstants.ActionMenuSU[0] };
            string[] actionEnabled = { CommonConstants.ActionMenuSU[1] };
            serviceDataManagementPage
                .RightClickOnFirstCellWithServiceUnit()
                //.VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuDisabled(actionNotAvailable);
            //.VerifyActionInActionMenuEnabled(actionEnabled);
            //Step 1: Line 13: Right-click on any of the cells with Service Unit containing icon of Service Unit with multiple Service Unit Points
            // No data to test
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Add Service Task' on Point with Service Unit and existing Service Task - Verify that only Service Task Schedule is add into existing Service Task under the same Task Type")]
        public void TC_132_Test_6_Action_Add_Service_task_schedule_on_point_with_service_unit_and_existing_service_task()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.SERVICE_DATA_MANAGEMENT)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.RMC)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            //Step 1: Line 8: Right-click on any of the cells with ServiceTask Schedule

        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Set Assured' - ONE cell")]
        public void TC_132_Test_7_Action_Set_Assured_one_cell()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.SERVICE_DATA_MANAGEMENT)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.RM)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            //Line 90: Right click on ONE cell with Service task schedule in column Round Schedule/Round
            string tomorrowDate = CommonUtil.GetLocalDayMinusDay(1, CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            string datePlus5Days = CommonUtil.GetLocalDayMinusDay(5, CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            string descRedRow = serviceDataManagementPage
                .GetFirstDescWithRedColor();
            serviceDataManagementPage
                .RightClickOnFirstRowWithServiceTaskSchedule(descRedRow)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[1])
                .VerifySetAssuredAfterClick()
                .InputDateInSetEndDate(datePlus5Days)
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Line 90: Double click on cell to display Service Task Schedule with Service Task
            serviceDataManagementPage
                .DoubleClickOnFirstRowWithServiceTaskSchedule()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsServiceTaskSchedule()
                .ClickOnServiceTaskLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
                
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyAssuredTaskChecked()
                .VerifyAsseredFromAndAssuredUntil(tomorrowDate, datePlus5Days);

            //Get service group
            string serviceGroupName = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceGroupName();
            string serviceName = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceName();
            string roundAtFirstRow = PageFactoryManager.Get<ServicesTaskPage>()
                .GetRoundName();
            string roundName = roundAtFirstRow.Split(" ")[0];
            string dayName = roundAtFirstRow.Split(" ")[1];
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            string nowPlus2Days = CommonUtil.GetLocalDayMinusDay(1, CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            //Line 92: Go to [Task Confirmation screen]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TASK_CONFIRMATION)
                .AcceptAlert()
                .AcceptAlert()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.RM)
                .ClickServicesAndSelectServiceInTree(serviceGroupName, serviceName, roundName, dayName)
                .SendDateInScheduledDate(nowPlus2Days)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(descRedRow)
                .VerifyDisplayResultAfterSearchWithDesc(descRedRow);
            string datePlus7Days = CommonUtil.GetLocalDayMinusDay(5, CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendDateInScheduledDate(datePlus7Days)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .VerifyDisplayResultAfterSearchWithDesc(descRedRow)
                .SwitchToDefaultContent();
            //Go to [Task Allocation] to check
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TASK_ALLOCATION)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()

        }

    }
}
