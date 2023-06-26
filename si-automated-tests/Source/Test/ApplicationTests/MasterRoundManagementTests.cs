using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ApplicationTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class MasterRoundManagementTests : BaseTest
    {
        [Category("MasterRoundManagement")]
        [Category("Huong")]
        [Test]
        public void TC_242_Allow_User_to_save_selection_in_MRM_TA_TC()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser63.UserName, AutoUser63.Password)
                .IsOnHomePage(AutoUser63);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.MasterRoundManagement)
                .SwitchNewIFrame();

            //Verify whether the order of buttons in Master Round Management page is changed to Popup, Save,Refresh, Help
            MasterRoundManagementPage masterRoundManagementPage = PageFactoryManager.Get<MasterRoundManagementPage>();
            masterRoundManagementPage.WaitForLoadingIconToDisappear();
            string contract = Contract.Municipal;
            string service = "Waste";
            string subService = "Domestic Refuse";
            string date = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 1);
            masterRoundManagementPage.InputSearchDetails(contract, service, subService, date);
            masterRoundManagementPage.ClickPopoutButton()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            masterRoundManagementPage.IsOnPage();
            masterRoundManagementPage.CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            masterRoundManagementPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();

            masterRoundManagementPage.ClickHelp()
                .WaitForLoadingIconToDisappear()
                .IsInformationModalDisplay()
                .ClickCloseInformationModal()
                .WaitForLoadingIconToDisappear();

            //Verify whether new functionality for Save button is added so that user can save screen parameter selection and Hover over message Save Selection
            masterRoundManagementPage.ClickSaveSelectionButton()
                .VerifyToastMessage("Saved")
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.MasterRoundManagement)
                .SwitchNewIFrame();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            masterRoundManagementPage.IsSelectionCorrect(Contract.Municipal);
        }
    }
}
