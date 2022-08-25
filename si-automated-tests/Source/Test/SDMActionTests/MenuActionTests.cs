using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

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


    }
}
