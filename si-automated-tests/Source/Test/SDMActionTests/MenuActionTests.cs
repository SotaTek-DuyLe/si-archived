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
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Tasks;
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
                .OpenOption(SubOption.ServiceDataManagement)
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
            else if(today.DayOfWeek == DayOfWeek.Tuesday)
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
                .SelectContract(Contract.RM)
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
                .SelectContract(Contract.RM)
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
                .ClickOnServicesAndSelectGroupInTree(Contract.RMC)
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
                .SelectContract(Contract.RM)
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
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string taskId = PageFactoryManager.Get<DetailTaskPage>()
                .GetTaskId();

            //API Check => Bug
            List<TaskDBModel> taskDBModels = finder.GetTask(int.Parse(taskId));
            Assert.AreEqual(1, taskDBModels[0].proximityalert);
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
                .ClickOnServicesAndSelectGroupInTree(Contract.RM)
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
                .SelectContract(Contract.RM)
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
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string firstTaskId = PageFactoryManager.Get<DetailTaskPage>()
                .GetTaskId();
            CommonFinder finder = new CommonFinder(DbContext);
            //API Check => Bug
            List<TaskDBModel> taskDBModels = finder.GetTask(int.Parse(firstTaskId));
            Assert.AreEqual(1, taskDBModels[0].proximityalert);

            //Second taskId
            PageFactoryManager.Get<TaskConfirmationPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .SendKeyInDesc(secondDescRedRow)
                .ClickOnExpandRoundsBtn()
                .ClickOnExpandRoundLegsBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskConfirmationPage>()
                .VerifyDisplayResultAfterSearchWithDesc(secondDescRedRow)
                .ClickOnSelectAndDeselectBtn()
                .DoubleClickOnFirstTask()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string secondTaskId = PageFactoryManager.Get<DetailTaskPage>()
                .GetTaskId();
            //API Check => Bug
            List<TaskDBModel> secondtaskDBModels = finder.GetTask(int.Parse(secondTaskId));
            Assert.AreEqual(1, secondtaskDBModels[0].proximityalert);

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
                .ClickOnServicesAndSelectGroupInTree(Contract.RMC)
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
                .SelectContract(Contract.RM)
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
                .SelectContract(Contract.RM)
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
                .ClickOnServicesAndSelectGroupInTree(Contract.RMC)
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
            string contract = Contract.RMC;
            string service = "Collections";
            string filterDatePast = "";
            string filterDateFuture = "";
            //Thursday
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Monday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -4);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 3);
            }
            else if (today.DayOfWeek == DayOfWeek.Tuesday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -5);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
            }
            else if (today.DayOfWeek == DayOfWeek.Wednesday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -6);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            }
            else if (today.DayOfWeek == DayOfWeek.Thursday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -7);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 7);
            }
            else if (today.DayOfWeek == DayOfWeek.Friday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -1);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 6);
            }
            else if (today.DayOfWeek == DayOfWeek.Saturday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -2);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 5);
            }
            else if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                filterDatePast = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, -3);
                filterDateFuture = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 4);
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
                .SelectContract(Contract.RMC)
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
                .SelectContract(Contract.RMC)
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
                .ClickOnServicesAndSelectGroupInTree(Contract.RM, "Waste", "Domestic Refuse")
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

    }
}
