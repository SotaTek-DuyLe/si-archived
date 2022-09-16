using System;
using NUnit.Allure.Core;
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
    [AllureNUnit]
    public class MenuActionTests : BaseTest
    {
        [Category("SDM Actions")]
        [Category("Chang")]
        [Test(Description = "Verify that a menu of Actions is displayed by clicking the right mouse button")]
        public void TC_132_Verify_that_a_menu_of_actions_is_displayed_by_clicking_the_right_mouse_button()
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
            

        }
    }
}
