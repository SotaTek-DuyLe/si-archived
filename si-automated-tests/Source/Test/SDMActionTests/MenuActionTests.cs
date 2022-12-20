using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using ServiceDataManagementPage = si_automated_tests.Source.Main.Pages.Services.ServiceDataManagementPage;
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Commercial)
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
                .ClickOnTotalRecord();
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal)
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
                .VerifyActionMenuDisplayWithServiceUnit()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Segment")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal)
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
            serviceDataManagementPage
                .RightClickOnFirstCellWithServiceUnit()
                .VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuDisabled(actionNotAvailable);
            //.VerifyActionInActionMenuEnabled(actionEnabled);
            //Step 1: Line 13: Right-click on any of the cells with Service Unit containing icon of Service Unit with multiple Service Unit Points
            string[] actionEnabled = { CommonConstants.ActionMenuSU[1] };
            serviceDataManagementPage
                .RightClickOnCellWithMutipleServiceUnitPoint()
                .VerifyActionMenuDisplayWithServiceUnit()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Node")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal)
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
                .VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuDisabled(actionNotAvailable)
                .VerifyActionInActionMenuEnabled(actionEnabled);
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Area")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal)
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
                .VerifyActionMenuDisplayWithServiceUnit()
                .VerifyActionInActionMenuDisabled(actionNotAvailable)
                .VerifyActionInActionMenuEnabled(actionEnabled);
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Commercial)
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
            string tomorrowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            string datePlus10Days = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 10);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            //Line 90: Right click on ONE cell with Service task schedule in column Round Schedule/Round

            string descRedRow = serviceDataManagementPage
                .GetFirstDescWithRedColor();
            serviceDataManagementPage
                .RightClickOnFirstRowWithServiceTaskSchedule(descRedRow)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[1])
                .VerifySetAssuredAfterClick()
                .InputDateInSetEndDate(datePlus10Days)
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Get Round
            int roundDay = serviceDataManagementPage
                .GetRoundDate(descRedRow);

            //Line 90: Double click on cell to display Service Task Schedule with Service Task
            serviceDataManagementPage
                .DoubleClickOnFirstRowWithServiceTaskSchedule(descRedRow)
                .SwitchToChildWindow(2)
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
                .VerifyAsseredFromAndAssuredUntil(tomorrowDate, datePlus10Days)
                .ClickOnSchedulesTab();

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
            string filterDate = "";
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 4);
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 3);
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 8);
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 7);
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 6);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 5);
            }
            //Line 92: Go to [Task Confirmation screen]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Municipal)
                .ClickServicesAndSelectServiceInTree(serviceGroupName, serviceName, roundName, dayName)
                .SendDateInScheduledDate(filterDate)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(descRedRow)
                .VerifyDisplayResultAfterSearchWithDesc(descRedRow);
            //FRIDAY
            string filterDayOutOfDateRange = "";
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 18);
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 17);
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 16);
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 15);
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 14);
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 13);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 12);
            }
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendDateInScheduledDate(filterDayOutOfDateRange)
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
                .OpenOption(SubOption.TaskAllocation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()
                .SelectContract(Contract.Municipal)
                .SelectServices(serviceGroupName, serviceName)
                .SendKeyInFrom(filterDate)
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()
                .DragAndDropUnAllocatedRoundToGridTask(roundName)
                .SendKeyInDescInputToSearch(descRedRow)
                .VerifyDisplayTaskWithAssuredChecked(descRedRow);
            PageFactoryManager.Get<TaskAllocationPage>()
                .SendKeyInFrom(filterDayOutOfDateRange)
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()
                .DragAndDropUnAllocatedRoundToGridTask(roundName)
                .SendKeyInDescInputToSearch(descRedRow)
                .VerifyDisplayTaskWithAssuredNotChecked(descRedRow);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Set Proximity Alert' - ONE cell")]
        public void TC_132_Test_9_Action_Set_Proximity_alert_on_cell()
        {
            CommonFinder finder = new CommonFinder(DbContext);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Commercial)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .FilterReferenceById("77755")
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            string descRedRow = serviceDataManagementPage
                .GetFirstDescWithRedColor();
            serviceDataManagementPage
                .RightClickOnFirstRowUnAllocated()
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[2])
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceDataManagementPage
                .DoubleClickOnFirstRowUnAllocated()
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
                .VerifyProximityAlertChecked()
                .ClickOnSchedulesTab();

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
            //Filter taskId and check
            //Go to [Task confirmation] to get task id
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
            //Line 92: Go to [Task Confirmation screen]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Municipal)
                .ClickServicesAndSelectServiceInTree(serviceGroupName, serviceName, roundName, dayName)
                .SendDateInScheduledDate(filterDate)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(descRedRow)
                .VerifyDisplayResultAfterSearchWithDesc(descRedRow)
                .ClickOnSelectAndDeselectBtn()
                .DoubleClickOnFirstTask()
                .SleepTimeInMiliseconds(3000);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string taskId = PageFactoryManager.Get<DetailTaskPage>()
                .GetTaskId();

            //API Check => Bug
            List<TaskDBModel> taskDBModels = finder.GetTask(int.Parse(taskId));
            Assert.IsTrue(taskDBModels[0].proximityalert, "proximityalert is not correct");
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Set Proximity Alert' - MULTIPLE cell")]
        public void TC_132_Test_10_Action_Set_Proximity_alert_on_multiple_cell()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            string firstDescRedRow = serviceDataManagementPage
                .GetFirstDescWithRedColor();
            string secondDescRedRow = serviceDataManagementPage
                .GetSecondDescWithRedColor();

            serviceDataManagementPage
                .SelectAndRightClickOnMultipleRowsUnAllocated(firstDescRedRow, secondDescRedRow)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[2])
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //First round
            serviceDataManagementPage
                .DoubleClickOnFirstRowUnAllocated(firstDescRedRow)
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
                .VerifyProximityAlertChecked()
                .ClickOnSchedulesTab();

            //Get service group for first round
            string firstServiceGroupName = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceGroupName();
            string firstServiceName = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceName();
            string firstRoundAtFirstRow = PageFactoryManager.Get<ServicesTaskPage>()
                .GetRoundName();
            string firstRoundName = firstRoundAtFirstRow.Split(" ")[0];
            string firstDayName = firstRoundAtFirstRow.Split(" ")[1];
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Second round
            serviceDataManagementPage
                .DoubleClickOnSecondRowUnAllocated(secondDescRedRow)
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
                .VerifyProximityAlertChecked()
                .ClickOnSchedulesTab();

            //Get service group for first round
            string secondServiceGroupName = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceGroupName();
            string secondServiceName = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceName();
            string secondRoundAtFirstRow = PageFactoryManager.Get<ServicesTaskPage>()
                .GetRoundName();
            string secondRoundName = secondRoundAtFirstRow.Split(" ")[0];
            string secondDayName = secondRoundAtFirstRow.Split(" ")[1];
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Filter taskId and check
            //Go to [Task confirmation] to get task id
            DateTime today = DateTime.Today;
            //FRIDAY
            string filterDayOutOfDateRange = "";
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 18);
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 17);
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 16);
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 15);
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 14);
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 13);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                filterDayOutOfDateRange = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 12);
            }
            //Line 92: Go to [Task Confirmation screen]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Municipal)
                .ClickServicesAndSelectServiceInTree(firstServiceGroupName, firstServiceName, firstRoundName, firstDayName)
                .SendDateInScheduledDate(filterDayOutOfDateRange)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(firstDescRedRow)
                .VerifyDisplayResultAfterSearchWithDesc(firstDescRedRow)
                .ClickOnSelectAndDeselectBtn()
                .DoubleClickOnFirstTask()
                .SleepTimeInMiliseconds(3000);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string firstTaskId = PageFactoryManager.Get<DetailTaskPage>()
                .GetTaskId();
            CommonFinder finder = new CommonFinder(DbContext);
            List<TaskDBModel> taskDBModels = finder.GetTask(int.Parse(firstTaskId));
            Assert.IsTrue(taskDBModels[0].proximityalert, "proximityalert is not correct");

            //Second taskId
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(secondDescRedRow)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .VerifyDisplayResultAfterSearchWithDesc(secondDescRedRow)
                .ClickOnSelectAndDeselectBtn()
                .DoubleClickOnFirstTask()
                .SleepTimeInMiliseconds(3000);
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string secondTaskId = PageFactoryManager.Get<DetailTaskPage>()
                .GetTaskId();
            List<TaskDBModel> secondtaskDBModels = finder.GetTask(int.Parse(secondTaskId));
            Assert.IsTrue(secondtaskDBModels[0].proximityalert, "proximityalert is not correct");

        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Add/Amend Crew Notes' - ONE cell with Assured Task")]
        public void TC_132_Test_11_Action_add_amend_crew_notes_one_cell_with_assured_task()
        {
            string tomorrowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            string datePlus10Days = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 10);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Commercial)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .FilterReferenceById("77755")
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            string descRedRow = serviceDataManagementPage
                .GetFirstDescWithRedColor();
            string noteValue = "Auto test note" + CommonUtil.GetRandomNumber(5);
            serviceDataManagementPage
                .RightClickOnSecondRowUnAllocated()
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[1])
                .VerifySetAssuredAfterClick();
            //Input [Set Assured]
            serviceDataManagementPage.InputDateInSetEndDate(datePlus10Days);
            //Input [Add/Amend Crew Notes]
            string roundNameWithFormat = "WDREF1:Wednesday";
            serviceDataManagementPage.RightClickOnSecondRowUnAllocated(roundNameWithFormat);
            serviceDataManagementPage.ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[3]);
            serviceDataManagementPage.InputAddAmendCrewNotes(noteValue);
            serviceDataManagementPage.ClickOnSaveBtn();
            serviceDataManagementPage.ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Get Round
            int roundDay = serviceDataManagementPage
                .GetRoundDate(descRedRow);
            //Line 90: Double click on cell to display Service Task Schedule with Service Task
            serviceDataManagementPage
                .DoubleClickOnAnyRowWithServiceTaskSchedule(roundNameWithFormat)
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
                .VerifyAsseredFromAndAssuredUntil(tomorrowDate, datePlus10Days)
                //Verify [Task Notes]
                .VerifyNoteValueInTaskNotes(noteValue)
                .ClickOnSchedulesTab();

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

            //Line 92: Go to [Task Confirmation screen]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Municipal)
                .ClickServicesAndSelectServiceInTree(serviceGroupName, serviceName, roundName, dayName)
                .SendDateInScheduledDate(filterDate)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(descRedRow)
                .VerifyDisplayResultAfterSearchWithDesc(descRedRow)
                .DoubleClickOnFirstRound()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInDesc(descRedRow)
                .VerifyDisplayNotesAfterSearchWithDesc(noteValue)
                .ClickOnSelectAndDeselectBtn()
                .DoubleClickOnFirstRowAfterFilteringWithDesc()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Verify [Task notes] in [Details] tab
            PageFactoryManager.Get<DetailTaskPage>()
                .ClickOnDetailTab()
                .VerifyTaskNotesValue(noteValue)
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Verify in [Task Allocation] page

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskAllocation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()
                .SelectContract(Contract.Municipal)
                .SelectServices(serviceGroupName, serviceName)
                .SendKeyInFrom(filterDate)
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()
                .DragAndDropUnAllocatedRoundToGridTask(dayName, roundName)
                .SendKeyInDescInputToSearch(descRedRow)
                .DoubleClickOnTaskAfterFilter(descRedRow)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .VerifyTaskNotesValue(noteValue);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Retire Service Task Schedule' -  Service Task with MULTIPLE Service Task Schedules")]
        public void TC_132_Test_13_Action_Retire_Service_task_schedule_service_task_with_multiple_service_task_schedules()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Commercial)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .SelectCheckboxByReferenceId("98558")
                .SelectCheckboxByReferenceId("104353")
                .SelectCheckboxByReferenceId("104386")
                .SelectCheckboxByReferenceId("108488")
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            string descRedRow = serviceDataManagementPage
                .GetFirstDescWithRedColor();
            string roundNameRetired = "REC1-AM:Thursday";
            string roundNameRetiredWithFormat = "REC1-AM Thursday";
            string roundNameNotRetiredWithFormat = "REC1-AM Monday";
            string serviceGroupName = "Collections";
            string serviceName = "Commercial Collections";
            serviceDataManagementPage
                .RightClickOnSecondRowUnAllocated(roundNameRetired)
                .VerifyActionMenuDisplayedWithActions()
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[4])
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify Only Service Task Schedule is retired and a white colour appears in cell (no STS is present)
            serviceDataManagementPage
                .VerifyWhiteColourAppearInCellNoSTSPresent(roundNameRetired)
                .SwitchToDefaultContent();
            //Go to [Master Round Management] - STS with retired round not displayed
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.MasterRoundManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            string contract = Contract.Commercial;
            string service = "Collections";
            string filterDatePast = "";
            string filterDateFuture = "";
            //Thursday
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -4);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 10);
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -5);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 9);
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -6);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 8);
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -7);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 14);
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -1);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 13);
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -2);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 12);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -3);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 11);
            }
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .IsOnPage()
                .InputSearchDetails(contract, service)
                .ClickOnSearchRoundBtn()
                .SendKeyInSearchRound(roundNameRetiredWithFormat)
                .DragAndDropFirstRoundToGrid();
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .SendKeyInDescInput(descRedRow)
                .VerifyNoRecordInTaskGrid()
                //Go to [Master Round Management] - STS with round displayed
                .SendKeyInSearchRound(roundNameNotRetiredWithFormat)
                .DragAndDropFirstRoundToGrid()
                .SwitchToTab(roundNameNotRetiredWithFormat);
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .SendKeyInDescInput(descRedRow, "2")
                .VerifyFirstRecordWithDescInTaskGrid(descRedRow)
                .SwitchToDefaultContent();
            //Go to [Task confirmation screen] to verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();

            string roundName = "REC1-AM";
            //=> Filter with date in the past [round Retired]
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Commercial)
                .ClickServicesAndSelectServiceInTree(serviceGroupName, serviceName, roundName)
                .SendDateInScheduledDate(filterDatePast)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(descRedRow)
                .VerifyDisplayResultAfterSearchWithDesc(descRedRow);
            //=> Filter with date in the future [round Retired]
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendDateInScheduledDate(filterDateFuture)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(descRedRow)
                .VerifyNoDisplayResultAfterSearchWithDesc()
                .SwitchToDefaultContent();
            //Go to [Task Allocation] to verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskAllocation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //=> Filter with date in the past
            PageFactoryManager.Get<TaskAllocationPage>()
                .SelectContract(Contract.Commercial)
                .SelectServices(serviceGroupName, serviceName)
                .SendKeyInFrom(filterDatePast)
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            string dayName = "Thursday";
            PageFactoryManager.Get<TaskAllocationPage>()
                .DragAndDropUnAllocatedRoundToGridTask(dayName, roundName)
                .SendKeyInDescInputToSearch(descRedRow)
                .VerifyDisplayTaskAfterFilter(descRedRow);
            //=> Filter with date in the future
            PageFactoryManager.Get<TaskAllocationPage>()
                .SendKeyInFrom(filterDateFuture)
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()
                .DragAndDropUnAllocatedRoundToGridTask(dayName, roundName)
                .SendKeyInDescInputToSearch(descRedRow)
                .VerifyNoDisplayTaskAfterFilter(descRedRow);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Verify that is possible to apply for combination of MULTIPLE Actions ")]
        public void TC_132_Test_16_Verify_that_is_possible_to_apply_for_combination_of_MULTIPLE_actions()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal, "Waste", "Domestic Refuse")
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .SelectCheckboxByReferenceId("63074")
                .SelectCheckboxByReferenceId("63075")
                .SelectCheckboxByReferenceId("63076")
                .SelectCheckboxByReferenceId("63077")
                .SelectCheckboxByReferenceId("63078")
                .SelectCheckboxByReferenceId("63080")
                .SelectCheckboxByReferenceId("63081")
                .SelectCheckboxByReferenceId("63082")
                .SelectCheckboxByReferenceId("63083")
                .SelectCheckboxByReferenceId("63084")
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            string datePlus10Days = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 10);
            string noteValue = "Note value " + CommonUtil.GetRandomString(5);
            string roundNameRetired = "WDREC1:Thursday";
            string roundNameSetAssuredAndProximity = "WDREF1:Thursday";
            string roundNameCrewNotes = "CLINICAL1:Monday";
            string tomorrowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);

            //MULTIPLE [Retire]
            serviceDataManagementPage
                //Retire multiple cell with service task schedule
                .SelectMultipleCellWithServiceTaskSchedule(roundNameRetired)
                .RightClickOnMultipleRowWithServiceTaskSchedule(roundNameRetired)
                .VerifyActionMenuDisplayedWithActions()
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[4])
                //[Add Service Task] for multiple cell
                .SelectMultipleCellWithNoServiceTaskSchedule()
                .RightClickOnMultipleCellWithMoServiceTaskSchedule()
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[0])
                //[Set Assured] for one cell with service task schedule
                .RightClickOnThirdCellWithServiceTaskSchedule(roundNameSetAssuredAndProximity)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[1])
                .VerifySetAssuredAfterClick()
                .InputDateInSetEndDate(datePlus10Days)
                .ClickOnTotalRecord()
                //[Set Proximity Alert] for one cell with service task schedule
                .RightClickOnForthCellWithServiceTaskSchedule(roundNameSetAssuredAndProximity)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[2])
                //[Add/Amend Crew Notes] for one cell with service task schedule
                .RightClickOnFifthCellWithServiceTaskSchedule(roundNameCrewNotes)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[3])
                .InputAddAmendCrewNotes(noteValue)
                .ClickOnSaveBtn()
                .InputDateInSetEndDate(datePlus10Days)
                //Click on [Apply] btn
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify each column
            serviceDataManagementPage
                //Column retired
                .VerifyWhiteColourAppearInCellNoSTSPresent(roundNameRetired)
                .VerifyWhiteColourAppearInSecondCellNoSTSPresent(roundNameRetired)
                //Column [Add Service Task]
                .DoubleClickOnFirstServiceTasSchedule(roundNameCrewNotes)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsServiceTaskSchedule()
                .VerifyStartDateAndEndDate()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            serviceDataManagementPage
                .DoubleClickOnSecondServiceTasSchedule(roundNameCrewNotes)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsServiceTaskSchedule()
                .VerifyStartDateAndEndDate()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Column [Set assured]
            serviceDataManagementPage
                .DoubleClickOnSetAssuredCell16(roundNameSetAssuredAndProximity)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsServiceTaskSchedule()
                .ClickOnServiceTaskLink()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyAssuredTaskChecked()
                .VerifyAsseredFromAndAssuredUntil(tomorrowDate, datePlus10Days)
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Column [Set Proximity Alert]
            serviceDataManagementPage
                .DoubleClickOnSetProximityAlert16(roundNameSetAssuredAndProximity)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsServiceTaskSchedule()
                .ClickOnServiceTaskLink()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyProximityAlertChecked()
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Column [Add/Amend Crew Notes]
            serviceDataManagementPage
                .DoubleClickOnAddAmendCrewNoteAndSetAssured16(roundNameCrewNotes)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskSchedulePage>()
                .IsServiceTaskSchedule()
                .ClickOnServiceTaskLink()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyAssuredTaskChecked()
                .VerifyAsseredFromAndAssuredUntil(tomorrowDate, datePlus10Days)
                .VerifyNoteValueInTaskNotes(noteValue);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Drag SU with No STS into a cell with No SU (Point B drag into Point A)")]
        public void TC_132_Test_17_Drag_SU_with_no_STS_into_a_cell_with_no_SU_point_B_drag_into_point_A()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Commercial)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();

            serviceDataManagementPage
                //.ClickOnSelectAndDeselectBtn()
                .SelectCheckboxByReferenceId("77755")
                .SelectCheckboxByReferenceId("80147")
                .SelectCheckboxByReferenceId("98558")
                .SelectCheckboxByReferenceId("103611")
                .SelectCheckboxByReferenceId("104386")
                .SelectCheckboxByReferenceId("108488")
                .SelectCheckboxByReferenceId("110048")
                .SelectCheckboxByReferenceId("111390")
                .SelectCheckboxByReferenceId("112050")
                .SelectCheckboxByReferenceId("117464")
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .DragServiceUnitPointCToServicePointA()
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            //verify database

            //Double click on Point C in the grid Click on 'All Services' tab and Click on 'Edit Service Unit' button for retired Service Unit
            serviceDataManagementPage.DoubleClickPointC()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage.ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            pointAddressDetailPage
                .ClickOnServiceUnitWithServiceNameAndActive("Recycling:Domestic Recycling", "HAM HOUSE STABLES, HAM STREET, HAM, RICHMOND, TW10 7RS")
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage.VerifyInputValue(serviceUnitDetailPage.EndDateInput, londonCurrentDate.AddDays(1).ToString("dd/MM/yyyy"));

            //Double click on Service Task ID
            serviceUnitDetailPage.ClickOnElement(serviceUnitDetailPage.ServiceTaskScheduleTab);
            serviceUnitDetailPage.WaitForLoadingIconToDisappear();
            serviceUnitDetailPage.ClickEditServiceTask(0)
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();
            ServicesTaskPage servicesTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
            servicesTaskPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            servicesTaskPage.VerifyInputValue(servicesTaskPage.EndDateInput, londonCurrentDate.AddDays(1).ToString("dd/MM/yyyy"))
                .ClickCloseBtn()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            //Go back to Service Unit window, Click on 'Service Unit Points' tab Double click on retired Service Unit Point ID
            serviceUnitDetailPage.ClickOnElement(serviceUnitDetailPage.ServiceUnitPointTab);
            serviceUnitDetailPage.WaitForLoadingIconToDisappear();
            serviceUnitDetailPage.DoubleClickServiceUnitPoint(0)
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();

            ServiceUnitPointDetailPage serviceUnitPointDetail = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetail.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            serviceUnitPointDetail.VerifyInputValue(serviceUnitPointDetail.EndDateInput, londonCurrentDate.AddDays(1).ToString("dd/MM/yyyy"))
                .ClickCloseBtn()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();

            //Go back to 'All Services' tab and Click on 'Edit Service Unit' button for a new Service Unit
            serviceUnitDetailPage.ClickCloseBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            pointAddressDetailPage.ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            pointAddressDetailPage.ClickOnServiceUnitWithServiceNameAndNonActive("Recycling:Domestic Recycling", "HAM HOUSE STABLES, HAM STREET, HAM, RICHMOND, TW10 7RS")
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage.ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage.VerifyInputValue(serviceUnitDetailPage.EndDateInput, CommonConstants.FUTURE_END_DATE);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Split Service Units'")]
        public void TC_132_Test_19_Action_split_service_units()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            string schedulePointA = "2 GLEBE WAY, HANWORTH, FELTHAM, TW13 6HH";
            string schedulePointB = "3 GLEBE WAY, HANWORTH, FELTHAM, TW13 6HH";
            string titleTaskSchedule = "Domestic Recycling";

            serviceDataManagementPage
                //.ClickOnSelectAndDeselectBtn()
                .SelectCheckboxByReferenceId("26")
                .SelectCheckboxByReferenceId("27")
                .SelectCheckboxByReferenceId("42")
                .SelectCheckboxByReferenceId("68")
                .SelectCheckboxByReferenceId("185")
                .SelectCheckboxByReferenceId("272")
                .SelectCheckboxByReferenceId("357")
                .SelectCheckboxByReferenceId("454")
                .SelectCheckboxByReferenceId("63074")
                .SelectCheckboxByReferenceId("63075")
                .SelectCheckboxByReferenceId("63076")
                .SelectCheckboxByReferenceId("76599")
                .SelectCheckboxByReferenceId("76600")
                .SelectCheckboxByReferenceId("76611")
                .SelectCheckboxByReferenceId("76615")
                .SelectCheckboxByReferenceId("76620")
                .SelectCheckboxByReferenceId("76629")
                .SelectCheckboxByReferenceId("76635")
                .SelectCheckboxByReferenceId("76640");
            serviceDataManagementPage
                .ScrollToBottomOfPage();
            serviceDataManagementPage
                .SelectCheckboxByReferenceId("76642")
                .SelectCheckboxByReferenceId("76643")
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .DragServiceUnitPointToServiceUnitStep19(schedulePointA, schedulePointB)
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify the display of the multiple service unit
            serviceDataManagementPage
                .VerifyDisplayMultipleIconInServiceUnit(schedulePointA, schedulePointB);
            //Click on [Split Service Unit]
            serviceDataManagementPage
                .RightClickOnServiceUnitPointStep19(schedulePointB)
                .VerifyActionInActionMenuEnabled(CommonConstants.ActionMenuSU)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSU[0])
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify undisplay of the multiple service unit
            serviceDataManagementPage
                .VerifyUnDisplayMultipleIconInServiceUnit(schedulePointA, schedulePointB)
                //Click on Point B to verify detail of point
                .DoubleClickAtAnyPointStep19(schedulePointB)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            string tomorrowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage
                .WaitForPointAddressDetailDisplayed()
                .ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            //Service Unit with active status
            pointAddressDetailPage
                .ClickOnServiceUnitWithServiceNameAndActive(titleTaskSchedule, schedulePointB)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                //Service Unit Points tab
                .ClickOnServiceUnitPointsTab()
                .VerifyEndDateAndDescAtAnyServiceUnitPoint(CommonConstants.FUTURE_END_DATE, schedulePointA, "1")
                .VerifyEndDateAndDescAtAnyServiceUnitPoint(tomorrowDate, schedulePointB, "2")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Service Unit with non status
            pointAddressDetailPage
                .ClickOnServiceUnitWithServiceNameAndNonActive(titleTaskSchedule, schedulePointB)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .ClickOnServiceUnitPointsTab()
                .VerifyEndDateAndDescAtAnyServiceUnitPoint(CommonConstants.FUTURE_END_DATE, schedulePointB, "1")
                //Service Task Schedules
                .ClickOnServiceTaskSchedulesTab()
                .ClickOnEditServiceTaskBtnAtFirstRow()
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();
            ServicesTaskPage servicesTaskPage = PageFactoryManager.Get<ServicesTaskPage>();
            servicesTaskPage
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyStartDateAndEndDate(tomorrowDate, CommonConstants.FUTURE_END_DATE)
                .ClickCloseBtn()
                .SwitchToChildWindow(3)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            pointAddressDetailPage
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            //Click on Point B to verify detail of point
            serviceDataManagementPage
                .DoubleClickAtAnyPointStep19(schedulePointA)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            pointAddressDetailPage
                .WaitForPointAddressDetailDisplayed()
                .ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            //Service Unit with active status
            pointAddressDetailPage
                .ClickOnServiceUnitWithServiceNameAndActive(titleTaskSchedule, schedulePointA)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                //Service Unit Points tab
                .ClickOnServiceUnitPointsTab()
                .VerifyEndDateAndDescAtAnyServiceUnitPoint(tomorrowDate, schedulePointA, "1")
                .ClickOnServiceTaskSchedulesTab()
                .VerifyEndDateAtFirstRowServiceTaskScheduleTab(tomorrowDate, "1")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            pointAddressDetailPage
                .ClickOnServiceUnitWithServiceNameAndNonActive(titleTaskSchedule, schedulePointB)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .ClickOnServiceUnitPointsTab()
                .VerifyEndDateAndDescAtAnyServiceUnitPoint(CommonConstants.FUTURE_END_DATE, schedulePointA, "1")
                .VerifyEndDateAndDescAtAnyServiceUnitPoint(tomorrowDate, schedulePointB, "2");
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Remove Point' - for Service Unit with Multiple Service Unit Points")]
        public void TC_132_Test_21_Action_remove_point_for_service_unit_with_multiple_service_unit_points()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Municipal)
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                //.ClickOnSelectAndDeselectBtn()
                .SelectCheckboxByReferenceId("26")
                .SelectCheckboxByReferenceId("27")
                .SelectCheckboxByReferenceId("42")
                .SelectCheckboxByReferenceId("68")
                .SelectCheckboxByReferenceId("185")
                .SelectCheckboxByReferenceId("272")
                .SelectCheckboxByReferenceId("357")
                .SelectCheckboxByReferenceId("454")
                .SelectCheckboxByReferenceId("63074")
                .SelectCheckboxByReferenceId("63075")
                .SelectCheckboxByReferenceId("63076")
                .SelectCheckboxByReferenceId("76598")
                .SelectCheckboxByReferenceId("76599")
                .SelectCheckboxByReferenceId("76610")
                .SelectCheckboxByReferenceId("76620")
                .SelectCheckboxByReferenceId("76626")
                .SelectCheckboxByReferenceId("76627")
                .SelectCheckboxByReferenceId("76628")
                .SelectCheckboxByReferenceId("76629")
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            string serviceUnitName = "1, BUTTS HOUSE, BUTTS CRESCENT, HANWORTH, FELTHAM, TW13 6HT";
            string serviceUnitNameContains = "BUTTS HOUSE, BUTTS CRESCENT, HANWORTH, TW13 6HT";
            string title = "Communal Refuse";

            serviceDataManagementPage
                .RightClickOnServiceUnitWithMultipleServiceUnitStep19(serviceUnitName)
                .VerifyActionInActionMenuEnabled(CommonConstants.ActionMenuSU)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSU[1])
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify the icon for Service Unit and 'Linked' icon are not present in cell with service unit
            serviceDataManagementPage
                .VerifyUnDisplayIconForServiceUnitAndLinkedIcon(serviceUnitName)
                .DoubleClickAtAnyPointStep19(serviceUnitName)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            string tomorrowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage
                .WaitForPointAddressDetailDisplayed()
                .ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            //Service Unit with active status
            pointAddressDetailPage
                .ClickOnServiceUnitWithServiceNameAndActive(title, serviceUnitNameContains)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .ClickOnServiceUnitPointsTab()
                .VerifyEndStartDateAndDescAtAnyServiceUnitPoint(tomorrowDate, serviceUnitName, "Point of Service")
                //Detail tab
                .ClickOnDetailTab()
                .VerifyStartDateEndDateInDetailTab(CommonConstants.FUTURE_END_DATE);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Reallocation of ONE new added STS from Round Schedule/Round to another Round Schedule/Round - reallocation from one cell to another and back to the first cell")]
        public void TC_132_Test_Reallocation_of_one_cell()
        {
            string initialRound = "REC1-AM:Tuesday";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser52.UserName, AutoUser52.Password)
                .IsOnHomePage(AutoUser52);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Commercial, "Collections", "Commercial Collections")
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .RightClickOnTuesdayEveryWeek()
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[0])
                .DragTuesDayToWednesdayToTestReallocate()
                .DragWednesdayToTuesDayToTestReallocate()
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            serviceDataManagementPage
                .DoubleClickOnTuesdayAfterReallocate()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceTaskSchedulePage serviceTaskSchedulePage = PageFactoryManager.Get<ServiceTaskSchedulePage>();
            serviceTaskSchedulePage
                .IsServiceTaskSchedule()
                .VerifyRoundValue(initialRound);
        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Action 'Add Service Task Schedule' on Point without Service Unit under Service - ONE cell in column Round Schedule/Round ")]
        public void TC_132_Test_2_Action_Add_Service_Task_Schedule_on_Point_without_Service_unit_under_service_one_cell_in_column_round_schedule_round()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree("Recycling")
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            string roundName = "WCREF3:Monday";
            string roundNameWithFormat = "WCREF3 Monday";
            string serviceUnitName = "3 WARWICK ROAD, HAMPTON WICK, KINGSTON UPON THAMES, KT1 4DW";
            string serviceTaskName = "Collect Communal Refuse";
            string serviceGroupName = "Waste";
            string serviceName = "Communal Refuse";
            string tomorrowDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);

            string[] otherAction = { CommonConstants.ActionMenuSDM[1], CommonConstants.ActionMenuSDM[2], CommonConstants.ActionMenuSDM[3], CommonConstants.ActionMenuSDM[4] };
            serviceDataManagementPage
                .RightClickOnFirstRowWithNoServiceUnitStep2(roundName)
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(otherAction)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[0])
                .ClickOnApplyAtBottomBtn()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Verify cell
            serviceDataManagementPage
                .VerifyDisplayServiceUnitAfterAddServiceTaskSchedule(roundName)
                .VerifyDisplayGreenBorderAfterAddServiceTaskSchedule(roundName)
                //Double Click at [Service Task Schedule]
                .DoubleClickOnFirstServiceTaskScheduleByRoundStep2(roundName)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceTaskSchedulePage serviceTaskSchedulePage = PageFactoryManager.Get<ServiceTaskSchedulePage>();
            serviceTaskSchedulePage
                .IsServiceTaskSchedule()
                .VerifyStartDateAndEndDate()
                .VerifyStatusOfServiceTaskSchedule("Inactive")
                .VerifyRoundValue(roundName)
                //Click on [Service Task] link
                .ClickOnServiceTaskLink()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .ClickOnDetailTab()
                .VerifyServiceAndServicegroupName(serviceName, serviceGroupName)
                .VerifyServiceScheduleTaskName(serviceUnitName)
                .VerifyServiceTaskName(serviceTaskName)
                .VerifyStartDateAndEndDate(tomorrowDate, CommonConstants.FUTURE_END_DATE)
                .ClickOnSchedulesTab()
                .VerifyStartDateAndEndDateFirstRow(tomorrowDate, CommonConstants.FUTURE_END_DATE);
                //Get service group
            //string serviceGroupName = PageFactoryManager.Get<ServicesTaskPage>()
            //    .GetServiceGroupName();
            //string serviceName = PageFactoryManager.Get<ServicesTaskPage>()
            //    .GetServiceName();
            string roundAtFirstRow = PageFactoryManager.Get<ServicesTaskPage>()
                .GetRoundName();
            string roundNameValue = roundAtFirstRow.Split(" ")[0];
            string dayName = roundAtFirstRow.Split(" ")[1];
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();

            //Double Click at [Service Unit] and verify
            serviceDataManagementPage
                .DoubleClickOnFirstUnitServiceByRoundStep2(roundName)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .VerifyServiceGroupAndServiceName(serviceGroupName, serviceName)
                //Detail tab
                .ClickOnDetailTab()
                .VerifyStartDateEndDateInDetailTab(CommonConstants.FUTURE_END_DATE)
                .VerifyStartDateValueIsTomorrow()
                //Service Task Schedules tab
                .ClickOnServiceTaskSchedulesTab()
                .VerifyEndDateAtFirstRowServiceTaskScheduleTab(CommonConstants.FUTURE_END_DATE, "1")
                .VerifyStartDateAtFirstRowServiceTaskScheduleTab(tomorrowDate, "1")
                //.VerifyTaskTypeAllocationAtFirstRowServiceTaskScheduleTab(serviceTaskName, roundName, "1")
                .VerifyEditScheduleEditServiceTaskBtnEnabled("1")
                //Service Unit Points tab
                .ClickOnServiceUnitPointsTab()
                .VerifyEndDateAndDescAtAnyServiceUnitPoint(CommonConstants.FUTURE_END_DATE, serviceUnitName, "1")
                .VerifyStartDateAtAnyServiceUnitPoint(tomorrowDate, "1")
                //Service Unit Points tab => Double click at first row
                .DoubleClickAtFirstRowInServiceUnitPointTab()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(serviceUnitName)
                .VerifyPointTypeStartAndEndDate("Point Address", tomorrowDate, CommonConstants.FUTURE_END_DATE)
                .ClickCloseBtn()
                .SwitchToChildWindow(2)
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Go to [Task Confirmation] to verify
            //MONDAY
            string filterDate = "";
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 7);
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 6);
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 5);
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 4);
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 3);
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                filterDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 8);
            }
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskConfirmation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .IsTaskConfirmationPage()
                .SelectContract(Contract.Municipal)
                .ClickServicesAndSelectServiceInTree(serviceGroupName, serviceName, roundNameValue, dayName)
                .SendDateInScheduledDate(filterDate)
                .ClickGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(serviceUnitName)
                .VerifyDisplayResultAfterSearchWithDesc(serviceUnitName)
                .VerifyScheduledDateAndDueDateAfterSearchWithDesc(filterDate)
                //Double Click on [Round]
                .DoubleClickOnFirstRound(roundName)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnWorksheetTab()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnMinimiseRoundsAndRoundLegsBtn()
                .SendKeyInDesc(serviceUnitName)
                .VerifyDisplayRowAfterSearchWithDesc(serviceUnitName)
                .CloseCurrentWindow()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Go to [Master Round Management] to verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.MasterRoundManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .IsOnPage()
                .InputSearchDetails(Contract.Municipal, serviceGroupName, serviceName, tomorrowDate)
                .ClickOnSearchRoundBtn()
                .SendKeyInSearchRound(roundNameWithFormat)
                .DragAndDropFirstRoundToGrid()
                .SwitchToTab(roundNameWithFormat);
            PageFactoryManager.Get<MasterRoundManagementPage>()
                .SendKeyInDescInput(serviceUnitName)
                .VerifyFirstRecordWithDescInTaskGrid(serviceUnitName)
                .SwitchToDefaultContent();
            //Go to [Task Allocation] to verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.TaskAllocation)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()
                .SelectContract(Contract.Municipal)
                .SelectServices(serviceGroupName, serviceName)
                .SendKeyInFrom(filterDate)
                .ClickOnGoBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskAllocationPage>()
                .DragAndDropUnAllocatedRoundToGridTask("WCREF3")
                .SendKeyInDescInputToSearch(serviceUnitName)
                .VerifyDisplayTaskAfterFilter(serviceUnitName);

        }

        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Verify the display of the message [You cannot create Service task under own schedule header]")]
        public void TC_132_Test_2_Verify_the_display_of_the_message_you_cannot_create_service_task_under_own_schedule_header()
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
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                .IsServiceDataManagementPage()
                .ClickServiceLocationTypeDdAndSelectOption("Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnServicesAndSelectGroupInTree(Contract.Commercial, "Collections", "Commercial Collections")
                .ClickOnApplyFiltersBtn()
                .VerifyWarningPopupDisplayed()
                .ClickOnOkBtn()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickOnSelectAndDeselectBtn()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            string roundName = "No Round";
            string[] otherAction = { CommonConstants.ActionMenuSDM[1], CommonConstants.ActionMenuSDM[2], CommonConstants.ActionMenuSDM[3], CommonConstants.ActionMenuSDM[4] };
            serviceDataManagementPage
                .RightClickOnFirstRowWithNoServiceUnitStep2(roundName)
                .VerifyActionMenuDisplayedWithActions()
                .VerifyActionInActionMenuDisabled(otherAction)
                .ClickOnAnyOptionInActions(CommonConstants.ActionMenuSDM[0])
                .VerifyToastMessage(MessageRequiredFieldConstants.YouCannotCreateServiceTaskMessage);
        }
    }
}
