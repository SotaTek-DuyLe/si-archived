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
using System.Threading;
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
            Thread.Sleep(1000);
            List<object> cellValues = serviceDataManagementPage.GetPointAddressCellValues("5");
            serviceDataManagementPage
                .ClickPointAddress("5")
                .ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .VerifyElementVisibility(serviceDataManagementPage.PreviousButton, true)
                .VerifyElementVisibility(serviceDataManagementPage.ApplyButton, true)
                .VerifyElementText(serviceDataManagementPage.TotalSpan, "Total = 1", true);
            serviceDataManagementPage.VerifyDescriptionLayout(cellValues);
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
            serviceDataManagementPage
                 .WaitForLoadingIconToDisappear();
            List<(int rowCount, string selectPoint)> testCases = new List<(int rowCount, string selectPoint)>()
            {
                (10, "Address"),
                (8, "Segment"),
                (5, "Area"),
                (7, "Node"),
            };
            foreach (var testCase in testCases)
            {
                serviceDataManagementPage
                    .SelectTextFromDropDown(serviceDataManagementPage.ServiceLocationTypeSelect, testCase.selectPoint)
                    .WaitForLoadingIconToDisappear();
                Thread.Sleep(1000);
                Dictionary<int, List<object>> rowDatas = serviceDataManagementPage.ClickMultiPointAddress(testCase.rowCount);
                serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
                serviceDataManagementPage.WaitForLoadingIconToDisappear();
                serviceDataManagementPage
                    .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = {rowDatas.Count}", true);
                foreach (var item in rowDatas)
                {
                    serviceDataManagementPage.VerifyDescriptionLayout(item.Value, item.Key);
                }
                serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
                serviceDataManagementPage
                    .WaitForLoadingIconToDisappear()
                    .ClickOnElement(serviceDataManagementPage.OkButton);
                serviceDataManagementPage.WaitForLoadingIconToDisappear();
                Thread.Sleep(500);
            }
        }

        [Category("TC_122_3 Verify that only max. 300 Segments are displayed in the Grid ")]
        [Test]
        public void TC_122_3_Verify_that_only_max_300_Segments_are_displayed_in_the_Grid()
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
                    .SelectTextFromDropDown(serviceDataManagementPage.ServiceLocationTypeSelect, "Segment")
                    .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
            Dictionary<int, List<object>> rowDatas = serviceDataManagementPage.ClickMultiPointAddress(305);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = {rowDatas.Count}", true);
            int i = 0;
            foreach (var item in rowDatas)
            {
                if (i >= 20)
                {
                    break;
                }
                i++;
                serviceDataManagementPage.VerifyDescriptionLayout(item.Value, item.Key);
            }
        }

        [Category("TC_122_4 Verify that only max. 300 Addresses/Nodes are displayed in the Grid")]
        [Test]
        public void TC_122_4_Verify_that_only_max_300_Addresses_Nodes_are_displayed_in_the_Grid()
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

            List<(int rowCount, string selectPoint)> testCases = new List<(int rowCount, string selectPoint)>()
            {
                (300, "Address"),
                (300, "Node"),
            };
            foreach (var testCase in testCases)
            {
                serviceDataManagementPage
                    .SelectTextFromDropDown(serviceDataManagementPage.ServiceLocationTypeSelect, testCase.selectPoint)
                    .WaitForLoadingIconToDisappear();
                Thread.Sleep(1000);
                Dictionary<int, List<object>> rowDatas = serviceDataManagementPage.ClickMultiPointAddress(testCase.rowCount);
                serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
                serviceDataManagementPage.WaitForLoadingIconToDisappear();
                serviceDataManagementPage
                    .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = {rowDatas.Count}", true);
                foreach (var item in rowDatas)
                {
                    serviceDataManagementPage.VerifyDescriptionLayout(item.Value, item.Key);
                }
                serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
                serviceDataManagementPage
                    .WaitForLoadingIconToDisappear()
                    .ClickOnElement(serviceDataManagementPage.OkButton);
                serviceDataManagementPage.WaitForLoadingIconToDisappear();
                Thread.Sleep(500);
            }
        }

        [Category("TC_122_5 Verify that the icons, displaying the status of the Point records, are appear correctly")]
        [Test]
        public void TC_122_5_Verify_that_the_icons_displaying_the_status_of_the_Point_records()
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
                    .ClickOnElement(serviceDataManagementPage.StatusExpandButton);
            serviceDataManagementPage
                .SelectByDisplayValueOnUlElement(serviceDataManagementPage.StatusSelect, "New")
                .SelectByDisplayValueOnUlElement(serviceDataManagementPage.StatusSelect, "Updated")
                .ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.OkButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            Thread.Sleep(1000);
            Dictionary<int, List<object>> rowDatas = serviceDataManagementPage.ClickMultiPointAddress(20);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = {rowDatas.Count}", true);
            foreach (var item in rowDatas)
            {
                serviceDataManagementPage.VerifyDescriptionLayout(item.Value, "yellow", item.Key);
            }

            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
            serviceDataManagementPage
                .WaitForLoadingIconToDisappear()
                .ClickOnElement(serviceDataManagementPage.OkButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            Thread.Sleep(500);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ClearFilterBtn);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            //Retired
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.StatusExpandButton);
            serviceDataManagementPage
                .SelectByDisplayValueOnUlElement(serviceDataManagementPage.StatusSelect, "Verified")
                .ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.OkButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            Thread.Sleep(1000);
            Dictionary<int, List<object>> rowVerifiedDatas = serviceDataManagementPage.ClickMultiPointAddress(20);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = {rowVerifiedDatas.Count}", true);
            foreach (var item in rowVerifiedDatas)
            {
                serviceDataManagementPage.VerifyDescriptionLayout(item.Value, "green", item.Key);
            }
        }
    }
}
