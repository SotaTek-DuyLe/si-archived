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
using TaskAllocationPage = si_automated_tests.Source.Main.Pages.Applications.TaskAllocationPage;

namespace si_automated_tests.Source.Test.ApplicationTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ServiceDataManagamentTest : BaseTest
    {
        [Category("TC_122_1 Verify that the layout of the new Service Data Management screen appears correctly")]
        [Category("Chang")]
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
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
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
        [Category("Chang")]
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
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame();

            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                 .WaitForLoadingIconToDisappear();
            List<(int rowCount, string selectPoint)> testCases = new List<(int rowCount, string selectPoint)>()
            {
                (10, "Address"),
                (8, "Segment"),
                (5, "Area"),
                (6, "Node"),
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
                    .ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
                serviceDataManagementPage.WaitForLoadingIconToDisappear();
                Thread.Sleep(500);
            }
        }

        [Category("TC_122_3 Verify that only max. 300 Segments are displayed in the Grid ")]
        [Category("Chang")]
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
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
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
            serviceDataManagementPage
                .VerifyTheDisplayOfPopupOver300Point()
                .ClickOnOkBtn();

            serviceDataManagementPage.WaitForLoadingIconToDisappear();
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
            serviceDataManagementPage
                .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = 300", true);
        }

        [Category("TC_122_4 Verify that only max. 300 Addresses/Nodes are displayed in the Grid")]
        [Category("Chang")]
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
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame();

            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                 .WaitForLoadingIconToDisappear();

            List<(int rowCount, string selectPoint)> testCases = new List<(int rowCount, string selectPoint)>()
            {
                (305, "Address"),
                //(305, "Node"),
            };
            foreach (var testCase in testCases)
            {
                serviceDataManagementPage
                    .SelectTextFromDropDown(serviceDataManagementPage.ServiceLocationTypeSelect, testCase.selectPoint)
                    .WaitForLoadingIconToDisappear();
                Thread.Sleep(1000);
                Dictionary<int, List<object>> rowDatas = serviceDataManagementPage.ClickMultiPointAddress(testCase.rowCount).Take(300).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
                serviceDataManagementPage
                    .VerifyTheDisplayOfPopupOver300Point()
                    .ClickOnOkBtn();
                serviceDataManagementPage.WaitForLoadingIconToDisappear();

                serviceDataManagementPage
                    .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = 300", true);
                foreach (var item in rowDatas)
                {
                    serviceDataManagementPage.VerifyDescriptionLayout(item.Value, item.Key);
                }
                serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
                serviceDataManagementPage
                    .WaitForLoadingIconToDisappear()
                    .ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
                serviceDataManagementPage.WaitForLoadingIconToDisappear();
                Thread.Sleep(500);
            }
        }

        [Category("TC_122_5 Verify that the icons, displaying the status of the Point records, are appear correctly")]
        [Category("Chang")]
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
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
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
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
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
                .ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            Thread.Sleep(500);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ClearFilterBtn);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            //Retired
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.StatusExpandButton);
            serviceDataManagementPage
                .SelectByDisplayValueOnUlElement(serviceDataManagementPage.StatusSelect, "Verified")
                .ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            Thread.Sleep(1000);
            Dictionary<int, List<object>> rowVerifiedDatas = serviceDataManagementPage.ClickMultiPointAddress(4);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = {rowVerifiedDatas.Count}", true);
            foreach (var item in rowVerifiedDatas)
            {
                serviceDataManagementPage.VerifyDescriptionLayout(item.Value, "green", item.Key);
            }
        }

        [Category("TC_122_6 Verify that the Points with Status 'Retired' are appear correctly")]
        [Category("Chang")]
        [Test]
        public void TC_122_6_Verify_that_the_Points_with_Status_Retired_are_appear_correctly()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password)
                .IsOnHomePage(AutoUser38);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame();

            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage
                 .WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                    .SelectTextFromDropDown(serviceDataManagementPage.ServiceLocationTypeSelect, "Address")
                    .ClickOnElement(serviceDataManagementPage.StatusExpandButton);
            serviceDataManagementPage
                .SelectByDisplayValueOnUlElement(serviceDataManagementPage.StatusSelect, "Retired")
                .ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            Thread.Sleep(1000);
            Dictionary<int, List<object>> rowDatas = serviceDataManagementPage.ClickMultiPointAddress(20);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage
                .VerifyElementText(serviceDataManagementPage.TotalSpan, $"Total = {rowDatas.Count}", true);
            foreach (var item in rowDatas)
            {
                serviceDataManagementPage.VerifyDescriptionLayout(item.Value, "red", item.Key);
            }
        }

        [Category("ServiceDataManagement")]
        [Category("Chang")]
        [Test]
        public void TC_245_Display_assured_flag_according_to_effective_date()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120913");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser38.UserName, AutoUser38.Password);
           
            Main.Pages.Services.ServicesTaskPage servicesTaskPage = PageFactoryManager.Get<Main.Pages.Services.ServicesTaskPage>();
            servicesTaskPage.WaitForLoadingIconToDisappear();
            //TASK 1
            //Assured task is ticked Assured from is set for the future, assured until is blank
            DateTime startDateTask1 = DateTime.Now.AddDays(15);
            servicesTaskPage.SetCheckboxValue(servicesTaskPage.AssuredTaskCheckbox, true);
            servicesTaskPage.SetInputValue(servicesTaskPage.StartDateInput, startDateTask1.ToString("dd/MM/yyyy"));
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredFromInput, startDateTask1.ToString("dd/MM/yyyy"));
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredToInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string descriptionTask1 = servicesTaskPage.GetElementText(servicesTaskPage.OpenServiceUnitLink);
            string taskType1 = servicesTaskPage.GetTaskType();

            //TASK 2
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120718");
            servicesTaskPage.WaitForLoadingIconToDisappear();
            DateTime startDateTask2 = DateTime.Now.AddDays(15);
            DateTime endDateTask2 = startDateTask2.AddDays(7);
            //Assured from is blank, assured until is set for the future
            servicesTaskPage.SetCheckboxValue(servicesTaskPage.AssuredTaskCheckbox, true);
            servicesTaskPage.SetInputValue(servicesTaskPage.StartDateInput, startDateTask2.ToString("dd/MM/yyyy"));
            servicesTaskPage.SetInputValue(servicesTaskPage.EndDateInput, endDateTask2.ToString("dd/MM/yyyy"));
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredFromInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string descriptionTask2 = servicesTaskPage.GetElementText(servicesTaskPage.OpenServiceUnitLink);
            string taskType2 = servicesTaskPage.GetTaskType();

            //TASK 3
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120717");
            servicesTaskPage.WaitForLoadingIconToDisappear();
            //Assured from and until are blank (will always be set)
            servicesTaskPage.SetCheckboxValue(servicesTaskPage.AssuredTaskCheckbox, true);
            servicesTaskPage.SetInputValue(servicesTaskPage.StartDateInput, DateTime.Now.ToString("dd/MM/yyyy"));
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredFromInput, "");
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredToInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string descriptionTask3 = servicesTaskPage.GetElementText(servicesTaskPage.OpenServiceUnitLink);
            string taskType3 = servicesTaskPage.GetTaskType();

            //TASK 4
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120716");
            DateTime startDateTask4 = DateTime.Now.AddDays(15);
            DateTime endDateTask4 = startDateTask4.AddDays(7);
            servicesTaskPage.WaitForLoadingIconToDisappear();
            servicesTaskPage.SetInputValue(servicesTaskPage.StartDateInput, startDateTask4.ToString("dd/MM/yyyy"));
            servicesTaskPage.SetInputValue(servicesTaskPage.EndDateInput, endDateTask4.ToString("dd/MM/yyyy"));
            //Assured from and until are blank (will always be set)
            servicesTaskPage.SetCheckboxValue(servicesTaskPage.AssuredTaskCheckbox, true);
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredFromInput, "");
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredToInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string descriptionTask4 = servicesTaskPage.GetElementText(servicesTaskPage.OpenServiceUnitLink);
            string taskType4 = servicesTaskPage.GetTaskType();

            //TASK 5
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-tasks/120715");
            DateTime startDateTask5 = DateTime.Now.AddDays(15);
            DateTime endDateTask5 = startDateTask5.AddDays(7);
            servicesTaskPage.WaitForLoadingIconToDisappear();
            servicesTaskPage.SetInputValue(servicesTaskPage.StartDateInput, startDateTask5.ToString("dd/MM/yyyy"));
            servicesTaskPage.SetInputValue(servicesTaskPage.EndDateInput, endDateTask5.ToString("dd/MM/yyyy"));
            //Assured from and until are blank (will always be set)
            servicesTaskPage.SetCheckboxValue(servicesTaskPage.AssuredTaskCheckbox, false);
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredFromInput, "");
            servicesTaskPage.SetInputValue(servicesTaskPage.AssuredToInput, "");
            servicesTaskPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string descriptionTask5 = servicesTaskPage.GetElementText(servicesTaskPage.OpenServiceUnitLink);
            string taskType5 = servicesTaskPage.GetTaskType();

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption(SubOption.ServiceDataManagement)
                .SwitchNewIFrame();
            ServiceDataManagementPage serviceDataManagementPage = PageFactoryManager.Get<ServiceDataManagementPage>();
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            //Applications -> Service data management -> Use the filter to display the data you set above 
            //Change the effective date to before start date for 1 -> Click OK 
            //Verify task 1
            serviceDataManagementPage
                    .SelectTextFromDropDown(serviceDataManagementPage.ServiceLocationTypeSelect, "Address");
            serviceDataManagementPage
                .ClickInputService()
                .ExpandServiceNode(Contract.Municipal)
                .ExpandServiceNode("Ancillary")
                .SelectServiceNode("Clinical Waste");
            serviceDataManagementPage.InputDescription("17 LONSDALE ROAD, BARNES, LONDON, SW13 9ED");
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.SelectRow(0);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), false);
            //Change the effective date to after start date for 1 -> Click OK 
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, startDateTask1.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);

            //Verify task 3
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            serviceDataManagementPage.InputDescription("9 THAMESGATE CLOSE, HAM, RICHMOND, TW10 7YS");
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.SelectRow(0);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);
            //Change the effective date to after start date for 1 -> Click OK 
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, startDateTask1.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);

            //go back and verify task 2
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            serviceDataManagementPage.InputDescription("13 THAMESGATE CLOSE, HAM, RICHMOND, TW10 7YS");
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.SelectRow(0);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            //Change the effective date to after end date for 2 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, endDateTask2.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), false);
            //Change the effective date to before end date for 2 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, endDateTask2.AddDays(-2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);

            //go back and verify taks 3
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            serviceDataManagementPage.InputDescription("9 THAMESGATE CLOSE, HAM, RICHMOND, TW10 7YS");
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.SelectRow(0);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            //Change the effective date to after end date for 2 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, endDateTask2.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);
            //Change the effective date to before end date for 2 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, endDateTask2.AddDays(-2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);

            //go back and verify task 4
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            serviceDataManagementPage.InputDescription("46 TANGIER ROAD, RICHMOND, TW10 5DW");
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.SelectRow(0);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            //Change the effective date to before start date for 4 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, startDateTask4.AddDays(-2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), false);

            //Change the effective date to between start and end date for 4 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, startDateTask4.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);

            //Change the effective date to after end date for 4 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, endDateTask4.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), false);

            //go back and verify task 3
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            serviceDataManagementPage.InputDescription("9 THAMESGATE CLOSE, HAM, RICHMOND, TW10 7YS");
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.SelectRow(0);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            //Change the effective date to before start date for 4 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, startDateTask4.AddDays(-2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);

            //Change the effective date to between start and end date for 4 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, startDateTask4.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);

            //Change the effective date to after end date for 4 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, endDateTask4.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), true);

            //go back and verify task 5
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.PreviousButton);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInLeavePopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();

            serviceDataManagementPage.InputDescription("38 TANGIER ROAD, RICHMOND, TW10 5DW");
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.ApplyFilterBtn);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.okBtnInWarningFilterPopup);
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.SelectRow(0);
            serviceDataManagementPage.ClickOnElement(serviceDataManagementPage.NextButton);
            //Change the effective date to before start date for 5 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, startDateTask5.AddDays(-2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), false);

            //Change the effective date to between start and end date for 5 -> Click OK
            serviceDataManagementPage.InputCalendarDate(serviceDataManagementPage.EffectiveDateInput, startDateTask5.AddDays(2).ToString("dd/MM/yyyy"));
            serviceDataManagementPage.WaitForLoadingIconToDisappear();
            serviceDataManagementPage.ClickTextServiceDataManagement()
                .AcceptAlert()
                .WaitForLoadingIconToDisappear();
            serviceDataManagementPage.VerifyAssuredTaskDisplay("COLLECT CLINICAL WASTE".ToLower(), false);

            //Navigate to task allocation -> Filter the round for service task 5 and set effective date between start and end date of data set -> Go 
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();

            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Municipal);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Municipal)
                .ExpandRoundNode("Ancillary")
                .SelectRoundNode("Clinical Waste");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.SleepTimeInMiliseconds(1000);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.FromInput, startDateTask5.AddDays(-1).ToString("dd/MM/yyyy"));
            taskAllocationPage.SleepTimeInMiliseconds(3000);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.ToInput, endDateTask5.ToString("dd/MM/yyyy"));
            taskAllocationPage.ClickOnElement(taskAllocationPage.ContractSelect);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.DragRoundInstanceToUnlocattedGrid("CLINICAL1", "Friday");
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.SendKeys(taskAllocationPage.DescriptionFilterInput, "38 TANGIER ROAD, RICHMOND, TW10 5DW");
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.UnallocatedHorizontalScrollToElement(taskAllocationPage.ServiceUnitGroupFilterInput, true);
            taskAllocationPage.UnallocatedHorizontalScrollToElement(taskAllocationPage.OriginalRoundFilterInput, true);
            taskAllocationPage.VerifyTaskAssured(false);
        }
    }
}
