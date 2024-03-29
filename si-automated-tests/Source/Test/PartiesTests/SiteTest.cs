﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Suspension;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySuspension;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar;
using si_automated_tests.Source.Main.Finders;
using System.Threading;
using si_automated_tests.Source.Main.Pages.Paties.Sites;
using TaskAllocationPage = si_automated_tests.Source.Main.Pages.Applications.TaskAllocationPage;
using RoundInstanceForm = si_automated_tests.Source.Main.Pages.Applications.RoundInstanceForm;
using OpenQA.Selenium;
using si_automated_tests.Source.Main.Models.Applications;
using NUnit.Allure.Core;

namespace si_automated_tests.Source.Test.PartiesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class SiteTest : BaseTest
    {
        [Category("140_Task_Locked Tasks")]
        [Category("Huong")]
        [Category("Huong_2")]
        [Test(Description = "Verify that Site form displays  'Lock' and 'Lock Reference' fields")]
        [Order(1)]
        public void TC_140_1_Verify_that_Site_form_displays_Lock_and_Lock_Reference_fields()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/sites/1201");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password);
            SitePage sitePage = PageFactoryManager.Get<SitePage>();
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.ClickOnElement(sitePage.DetailTab);
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyElementVisibility(sitePage.LockCheckbox, true)
                .VerifyCheckboxIsSelected(sitePage.LockCheckbox, false)
                .VerifyElementVisibility(sitePage.LockReferenceInput, true)
                .VerifyElementIsMandatory(sitePage.LockReferenceInput, false);
            sitePage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            sitePage.ScrollDownToElement(sitePage.LockHelpButton)
                .SleepTimeInMiliseconds(300);
            sitePage.ClickOnElement(sitePage.LockHelpButton);
            sitePage.VerifyElementText(sitePage.LockHelpContent, "Set to true, if a Task should be locked to a Round Instance")
                .VerifyElementContainAttributeValue(sitePage.LockReferenceInput, "maxlength", "40");
            sitePage.ClickOnElement(sitePage.SiteAbvInput);
            sitePage.ClickOnElement(sitePage.LockCheckbox);
            sitePage.SendKeys(sitePage.LockReferenceInput, "Lock test automation");
            sitePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
        }

        [Category("140_Task_Locked Tasks")]
        [Category("Huong")]
        [Category("Huong_2")]
        [Test(Description = "Verify that Service Unit form displays 'Lock' and 'Lock Reference' fields")]
        [Order(2)]
        public void TC_140_2_Verify_that_ServiceUnit_form_displays_Lock_and_Lock_Reference_fields()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/service-units/230015");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password);
            ServiceUnitPage serviceUnitPage = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.ClickOnElement(serviceUnitPage.DetailTab);
            serviceUnitPage.WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyElementVisibility(serviceUnitPage.LockCheckbox, true)
                .VerifyCheckboxIsSelected(serviceUnitPage.LockCheckbox, false)
                .VerifyElementVisibility(serviceUnitPage.LockReferenceInput, true)
                .VerifyElementIsMandatory(serviceUnitPage.LockReferenceInput, false);
            serviceUnitPage.ClickOnElement(serviceUnitPage.LockHelpButton);
            serviceUnitPage.VerifyElementText(serviceUnitPage.LockHelpContent, "Set to true, if a Task should be locked to a Round Instance")
                .VerifyElementContainAttributeValue(serviceUnitPage.LockReferenceInput, "maxlength", "40");
            serviceUnitPage.ClickOnElement(serviceUnitPage.ClientReferenceInput);
            serviceUnitPage.ClickOnElement(serviceUnitPage.LockCheckbox);
            string lockedreference = "Lock test automation";
            serviceUnitPage.SendKeys(serviceUnitPage.LockReferenceInput, lockedreference);
            serviceUnitPage.ClickOnElement(serviceUnitPage.ClientReferenceInput);
            serviceUnitPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            var serviceUnit = finder.GetServiceUnitById(230015);
            Assert.IsTrue(serviceUnit.islocked);
            Assert.IsTrue(serviceUnit.lockedreference == lockedreference);
        }

        [Category("140_Task_Locked Tasks")]
        [Category("Huong")]
        [Category("Huong_2")]
        [Test(Description = "Verify that Site's  'locked' and 'Lock Reference' values are inherited by SU(s) created for this Site")]
        [Order(3)]
        public void TC_140_3_Verify_that_Site_Locked_and_Lock_Reference_values_are_inherited_by_SU_created_for_this_Site()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/sites/25");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password);
            SitePage sitePage = PageFactoryManager.Get<SitePage>();
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.ClickOnElement(sitePage.DetailTab);
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyElementVisibility(sitePage.LockCheckbox, true)
                .VerifyCheckboxIsSelected(sitePage.LockCheckbox, false)
                .VerifyElementVisibility(sitePage.LockReferenceInput, true)
                .VerifyElementIsMandatory(sitePage.LockReferenceInput, false);
            sitePage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            sitePage.ScrollDownToElement(sitePage.LockHelpButton)
                .SleepTimeInMiliseconds(300);
            sitePage.ClickOnElement(sitePage.LockHelpButton);
            sitePage.VerifyElementText(sitePage.LockHelpContent, "Set to true, if a Task should be locked to a Round Instance")
                .VerifyElementContainAttributeValue(sitePage.LockReferenceInput, "maxlength", "40");
            sitePage.ClickOnElement(sitePage.SiteAbvInput);
            sitePage.ClickOnElement(sitePage.LockCheckbox);
            string lockedreference = "Red key ring";
            sitePage.SendKeys(sitePage.LockReferenceInput, lockedreference);
            sitePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            var siteDBModel = finder.GetSitesById("25");
            Assert.IsTrue(siteDBModel.islocked);
            Assert.IsTrue(siteDBModel.lockedreference == lockedreference);
            sitePage.ClickOnElement(sitePage.SiteAddressTitle);
            sitePage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointAddressPage pointAddressPage = PageFactoryManager.Get<PointAddressPage>();
            pointAddressPage.ClickOnElement(pointAddressPage.AllServiceTab);
            pointAddressPage.WaitForLoadingIconToDisappear();
            pointAddressPage.ClickServiceUnit("Collections:Commercial Collections")
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitPage serviceUnitPage = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnitPage.VerifyCheckboxIsSelected(serviceUnitPage.LockCheckbox, true)
                .VerifyInputValue(serviceUnitPage.LockReferenceInput, lockedreference);
            serviceUnitPage.ClickCloseBtn()
                .SwitchToFirstWindow();
            sitePage.ClickOnElement(sitePage.LockCheckbox);
            lockedreference = "";
            sitePage.SendKeys(sitePage.LockReferenceInput, lockedreference);
            sitePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToChildWindow(2);
            pointAddressPage.ClickOnElement(pointAddressPage.AllServiceTab);
            pointAddressPage.WaitForLoadingIconToDisappear();
            pointAddressPage.ClickServiceUnit("Collections:Commercial Collections")
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceUnitPage.VerifyCheckboxIsSelected(serviceUnitPage.LockCheckbox, false)
                .VerifyInputValue(serviceUnitPage.LockReferenceInput, lockedreference);
        }

        [Category("140_Task_Locked Tasks")]
        [Category("Huong")]
        [Category("Huong_2")]
        [Test(Description = "Verify that individual Service Unit (SU) can be unlocked ")]
        [Order(4)]
        public void TC_140_4_Verify_that_individual_Service_Unit_can_be_unlocked()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/sites/24");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password);
            SitePage sitePage = PageFactoryManager.Get<SitePage>();
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.ClickOnElement(sitePage.DetailTab);
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyElementVisibility(sitePage.LockCheckbox, true)
                .VerifyCheckboxIsSelected(sitePage.LockCheckbox, false)
                .VerifyElementVisibility(sitePage.LockReferenceInput, true)
                .VerifyElementIsMandatory(sitePage.LockReferenceInput, false);
            sitePage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            sitePage.ScrollDownToElement(sitePage.LockHelpButton)
                .SleepTimeInMiliseconds(300);
            sitePage.ClickOnElement(sitePage.LockHelpButton);
            sitePage.VerifyElementText(sitePage.LockHelpContent, "Set to true, if a Task should be locked to a Round Instance")
                .VerifyElementContainAttributeValue(sitePage.LockReferenceInput, "maxlength", "40");
            sitePage.ClickOnElement(sitePage.SiteAbvInput);
            sitePage.ClickOnElement(sitePage.LockCheckbox);
            string lockedreference = "yellow key";
            sitePage.SendKeys(sitePage.LockReferenceInput, lockedreference);
            sitePage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            //Verify DB
            CommonFinder finder = new CommonFinder(DbContext);
            var siteDBModel = finder.GetSitesById("24");
            Assert.IsTrue(siteDBModel.islocked);
            Assert.IsTrue(siteDBModel.lockedreference == lockedreference);
            sitePage.ClickOnElement(sitePage.SiteAddressTitle);
            sitePage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointAddressPage pointAddressPage = PageFactoryManager.Get<PointAddressPage>();
            pointAddressPage.ClickOnElement(pointAddressPage.AllServiceTab);
            pointAddressPage.WaitForLoadingIconToDisappear();
            pointAddressPage.ClickServiceUnit("Collections:Commercial Collections")
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitPage serviceUnitPage = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnitPage.VerifyCheckboxIsSelected(serviceUnitPage.LockCheckbox, true)
                .VerifyInputValue(serviceUnitPage.LockReferenceInput, lockedreference);
            serviceUnitPage.ClickOnElement(serviceUnitPage.LockCheckbox);
            serviceUnitPage.SendKeys(serviceUnitPage.LockReferenceInput, "");
            serviceUnitPage.ClickOnElement(serviceUnitPage.ClientReferenceInput);
            serviceUnitPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToFirstWindow();
            sitePage.ClickRefreshBtn();
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyCheckboxIsSelected(sitePage.LockCheckbox, true)
                .VerifyInputValue(sitePage.LockReferenceInput, lockedreference);
        }

        [Category("140_Task_Locked Tasks")]
        [Category("Huong")]
        [Category("Huong_2")]
        [Test(Description = "Verify that if user attempts to reallocate Tasks which are locked (Task -> ServiceUnits.ISLocked = TRUE) from Round Instance in Core Round State other than 'Outstanding', then warning message will display")]
        [Order(5)]
        public void TC_140_5_Verify_that_if_user_attempts_to_reallocate_Tasks()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password)
                .IsOnHomePage(AutoUser34);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear(false);
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "15/07/2022";
            string to = "15/07/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Commercial);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Commercial)
                .ExpandRoundNode("Collections")
                .SelectRoundNode("Commercial Collections");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.FromInput, from);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.DoubleClickFromCellOnRound("Saturday")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstanceForm roundInstanceForm = PageFactoryManager.Get<RoundInstanceForm>();
            roundInstanceForm.ClickOnElement(roundInstanceForm.DropDownStatusButton);
            roundInstanceForm.SelectStatus("In Progress")
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            taskAllocationPage.DragRoundInstanceToUnlocattedGrid("REF1-AM", "Friday");
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.UnallocatedHorizontalScrollToElement(taskAllocationPage.LockFilterInput, true);
            taskAllocationPage.SendKeys(taskAllocationPage.LockFilterInput, "True");
            taskAllocationPage.SleepTimeInMiliseconds(200);
            taskAllocationPage.UnallocatedHorizontalScrollToElement(taskAllocationPage.IdFilterInput, true);
            taskAllocationPage.SleepTimeInMiliseconds(200);
            taskAllocationPage.ClickUnallocatedRow();
            taskAllocationPage.SleepTimeInMiliseconds(200);
            taskAllocationPage.DragUnallocatedRowToRoundInstance("REC1-AM", "Saturday")
                .VerifyElementVisibility(taskAllocationPage.AllocatingConfirmMsg, true)
                .ClickOnElement(taskAllocationPage.AllocateAllButton);
            taskAllocationPage
                .VerifyElementVisibility(taskAllocationPage.AllocatingConfirmMsg2, true)
                .ClickOnElement(taskAllocationPage.AllocateAllButton); ;
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.AllocationReasonSelect, "Incident")
                .ClickOnElement(taskAllocationPage.AllocationConfirmReasonButton);
            taskAllocationPage.WaitForLoadingIconToDisappear();
            taskAllocationPage.VerifyTaskAllocated("REC1-AM", "Saturday");
        }

        [Category("143_1_Task Allocation_Rellocation of Round Legs and Tasks")]
        [Category("Huong")]
        //[Test(Description = "Verify that Round Legs are successfully reallocated to a new Round Instance")]
        public void TC_143_1_Verify_that_Round_Legs_are_successfully_reallocated_to_a_new_Round_Instance()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password)
                .IsOnHomePage(AutoUser34);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear(false);
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "14/07/2022";
            string to = "15/07/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Municipal);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Municipal)
                .ExpandRoundNode("Recycling")
                .SelectRoundNode("Communal Recycling");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.FromInput, from);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.DragRoundInstanceToUnlocattedGrid("ECREC1", "Thursday");
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            List<RoundInstanceModel> roundInstanceDetails = taskAllocationPage.ExpandRoundInstance(3);
            taskAllocationPage.ScrollToFirstRow();
            List<RoundInstanceModel> roundInstances = taskAllocationPage.SelectExpandedUnallocated(3);
            taskAllocationPage.DragUnallocatedRowToRoundInstance("WCREC1", "Friday")
                .VerifyElementVisibility(taskAllocationPage.GetAllocatingConfirmMsg(roundInstanceDetails.Count), true)
                .ClickOnElement(taskAllocationPage.AllocateAllButton);
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.AllocationReasonSelect, "Incident")
                .ClickOnElement(taskAllocationPage.AllocationConfirmReasonButton);
            taskAllocationPage.WaitForLoadingIconToDisappear()
                .VerifyToastMessage("Allocated 3 round leg(s)");
            taskAllocationPage.DragRoundInstanceToRoundGrid("WCREC1", "Friday", 4);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            List<RoundInstanceModel> reAllocatedRoundInstanceDetails = taskAllocationPage.ExpandRoundInstance(roundInstances);
            taskAllocationPage.VerifyReAllocatedRows(roundInstanceDetails, reAllocatedRoundInstanceDetails);
        }

        [Category("143_2_Task Allocation_Rellocation of Round Legs and Tasks")]
        [Category("Huong")]
        //[Test(Description = "Verify that Tasks are successfully reallocated to a new Round Instance")]
        public void TC_143_2_Verify_that_Tasks_are_successfully_reallocated_to_a_new_Round_Instance()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password)
                .IsOnHomePage(AutoUser34);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear(false);
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "14/07/2022";
            string to = "15/07/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Municipal);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Municipal)
                .ExpandRoundNode("Recycling")
                .SelectRoundNode("Communal Recycling");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.FromInput, from);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.DragRoundInstanceToUnlocattedGrid("ECREC1", "Thursday");
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ToggleRoundLegsButton);
            taskAllocationPage.SleepTimeInMiliseconds(300);
            List<RoundInstanceModel> roundInstanceDetails = taskAllocationPage.SelectRoundLegs(4);
            taskAllocationPage.DragRoundLegRowToRoundInstance("WCREC1", "Friday")
                .VerifyElementVisibility(taskAllocationPage.GetAllocatingConfirmMsg(roundInstanceDetails.Count), true)
                .ClickOnElement(taskAllocationPage.AllocateAllButton);
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.AllocationReasonSelect, "Incident")
                .ClickOnElement(taskAllocationPage.AllocationConfirmReasonButton);
            taskAllocationPage.WaitForLoadingIconToDisappear()
                .VerifyToastMessages(new List<string>() { "Allocated 1 round leg(s)", "Task(s) Allocated" });
            taskAllocationPage.DragRoundInstanceToRoundGrid("WCREC1", "Friday", 4);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.VerifyRoundLegIsAllocated(roundInstanceDetails);
        }

        [Category("143_3_Task Allocation_Rellocation of Round Legs and Tasks")]
        [Category("Huong")]
        //[Test(Description = "Verify that Round Leg Instance will be reallocated if ALL Tasks under this RLI are reallocated ")]
        public void TC_143_3_Verify_that_Round_Leg_Instance_will_be_reallocated_if_ALL_Tasks_under_this_RLI_are_reallocated()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password)
                .IsOnHomePage(AutoUser34);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Applications)
                .OpenOption("Task Allocation")
                .WaitForLoadingIconToDisappear(false);
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            TaskAllocationPage taskAllocationPage = PageFactoryManager.Get<TaskAllocationPage>();
            string from = "14/07/2022";
            string to = "15/07/2022";
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.ContractSelect, Contract.Municipal);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ServiceInput);
            taskAllocationPage.ExpandRoundNode(Contract.Municipal)
                .ExpandRoundNode("Recycling")
                .SelectRoundNode("Communal Recycling");
            taskAllocationPage.ClickOnElement(taskAllocationPage.FromInput);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.FromInput, from);
            taskAllocationPage.InputCalendarDate(taskAllocationPage.ToInput, to);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ButtonGo);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.DragRoundInstanceToUnlocattedGrid("ECREC1", "Thursday");
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.ClickOnElement(taskAllocationPage.ToggleRoundLegsButton);
            taskAllocationPage.SleepTimeInMiliseconds(300);
            List<RoundInstanceModel> roundInstanceDetails = taskAllocationPage.SelectRoundLegs(4);
            taskAllocationPage.DragRoundLegRowToRoundInstance("WCREC1", "Friday")
                .VerifyElementVisibility(taskAllocationPage.GetAllocatingConfirmMsg(roundInstanceDetails.Count), true)
                .ClickOnElement(taskAllocationPage.AllocateAllButton);
            taskAllocationPage.SelectTextFromDropDown(taskAllocationPage.AllocationReasonSelect, "Incident")
                .ClickOnElement(taskAllocationPage.AllocationConfirmReasonButton);
            taskAllocationPage.WaitForLoadingIconToDisappear()
                .VerifyToastMessages(new List<string>() { "Allocated 2 round leg(s)", "Task(s) Allocated" });
            taskAllocationPage.DragRoundInstanceToRoundGrid("WCREC1", "Friday", 4);
            taskAllocationPage.WaitForLoadingIconToDisappear(false);
            taskAllocationPage.VerifyRoundLegIsAllocated(roundInstanceDetails);
        }

        [Category("Sites")]
        [Category("Huong")]
        [Test()]
        public void TC_248_Qualification_Constraints_to_Sites()
        {
            //Verify whether Required Qualification Tab is in the Sites Form
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser34.UserName, AutoUser34.Password);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser34);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Sites")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            SitePage sitePage = PageFactoryManager.Get<SitePage>();
            sitePage.ClickOnElement(sitePage.RequiredQualificationTab);
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyElementVisibility(sitePage.AddQualificationButton, true)
                .VerifyElementVisibility(sitePage.QualificationTable, true);

            //Verify whether user able to click Add New Item Button
            sitePage.ClickOnElement(sitePage.AddQualificationButton);
            sitePage.SleepTimeInMiliseconds(500);
            sitePage.VerifyElementVisibility(sitePage.ToogleSelectQualificationButton, true)
                .VerifyElementVisibility(sitePage.ConfirmAddQualificationButton, true)
                .VerifyElementVisibility(sitePage.CancelAddQualificationButton, true);

            //Verify user able to select single or multiple QC from the popup Window
            sitePage.ClickOnElement(sitePage.ToogleSelectQualificationButton);
            sitePage.SelectByDisplayValueOnUlElement(sitePage.ExpandedListbox, "Site Induction")
                .SleepTimeInMiliseconds(200);
            sitePage.ClickOnElement(sitePage.ConfirmAddQualificationButton);

            //Verify when new constraint is added and is visible in the main panel
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            sitePage.VerifyNewQualification("Site Induction", londonCurrentDate.ToString("dd/MM/yyyy"), "01/01/2050");

            //Verify whether user able to Retire QC
            sitePage.ClickRetireQualification(0);
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyToastMessage("Successfully retired Required Qualification");
        }
    }
}
