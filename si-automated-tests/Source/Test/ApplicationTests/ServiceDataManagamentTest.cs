using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using System;
using System.Collections.Generic;
using System.Text;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ApplicationTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ServiceDataManagamentTest : BaseTest
    {
        [Category("TC_122_1 Verify that the layout of the new Service Data Management screen appears correctly")]
        [Test]
        public void TC_122_1_Verify_Layout_Service_Data_Management()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Applications")
                .OpenOption("Service Data Management")
                .SwitchNewIFrame();

            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                 .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .SelectTextFromDropDown(serviceDataManagementPage.ServiceLocationTypeSelect, "Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickPointAddress("5")
                .ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .VerifyElementVisibility(serviceDataManagementPage.PreviousButton, true)
                .VerifyElementVisibility(serviceDataManagementPage.ApplyButton, true)
                .VerifyElementText(serviceDataManagementPage.TotalSpan, "Total = 1", true);
        }

        [Category("TC_122_2 Verify that every selected Points appears in Service Data Management screen ")]
        [Test]
        public void TC_122_2_Verify_Every_Selected_Points_Appears_In_Service_Data_Management()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Applications")
                .OpenOption("Service Data Management")
                .SwitchNewIFrame();

            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            int rowCount = 10;
            serviceDataManagementPage
                 .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .SelectTextFromDropDown(serviceDataManagementPage.ServiceLocationTypeSelect, "Address")
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .ClickMultiPointAddress(rowCount);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = {rowCount}", true);
        }
    }
}
