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
using NUnit.Allure.Core;

namespace si_automated_tests.Source.Test.SuspensionTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateSuspensionTest : BaseTest
    {
        private CommonFinder finder;
        public override void Setup()
        {
            base.Setup();
            finder = new CommonFinder(DbContext);
            Login();
        }

        public void Login()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser34.UserName, AutoUser34.Password)
                .IsOnHomePage(AutoUser34);
        }

        [Category("Add Suspension")]
        [Category("Huong")]
        [Test(Description = "Add new suspension")]
        [Order(1)]
        public void TC_089_Add_New_Suspension()
        {
            int partyId = 73;
            string partyName = "Greggs";
            SuspensionInputData inputData = new SuspensionInputData();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickNewVerSuspensionTab()
                .WaitForLoadingIconToDisappear();
            //Add New Suspension
            PageFactoryManager.Get<PartySuspensionPage>().ClickAddNewSuspension();
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .WaitServiceSuspensionVisible()
                .VerifySuspensionTitle(partyName + " - Add Service Suspension");
            inputData.Sites = PageFactoryManager.Get<AddNewSuspensionPage>().GetSiteNames();
            List<SiteDBModel> sites = finder.GetSitesByPartyId(partyId);
            Assert.AreEqual(inputData.Sites, sites.Select(x => x.site).ToList(), "The data from the database does not match all of the sites configured for the Party.");
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .VerifyNextButtonIsDisable()
                .ClickSelectAllSiteCheckbox()
                .VerifyNextButtonIsEnable()
                .ClickNextButton();
            inputData.Services = PageFactoryManager.Get<AddNewSuspensionPage>().GetServiceNames();
            List<ServiceDBModel> services = finder.GetServiceTypes();
            Assert.AreEqual(inputData.Services, services.Select(x => x.servicetype).ToList(), "The data from the database does not match all of the services configured for the Party.");
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .VerifyNextButtonIsDisable()
                .ClickSelectAllServiceCheckbox()
                .VerifyNextButtonIsEnable()
                .ClickNextButton();

            inputData.FromDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            inputData.LastDate = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            inputData.SuspensedDay = "Everyday";
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .IsFirstDayInputVisible()
                .IsLastDayInputVisible()
                .IsDateDiffLabelVisible()
                .IsEveryDayRadioVisible()
                .IsSelectedDayRadioVisible()
                .ClickFinish()
                .VerifyWarningMessage("First Day is required")
                .InputDaysAndVerifyDaysCalculationLbl(inputData.FromDate, inputData.LastDate)
                .ClickFinish()
                .VerifySaveMessage("Saved")
                .IsAddSuspensionModalInVisible();
            //Verify Suspension Created
            PageFactoryManager.Get<PartySuspensionPage>()
                .WaitForLoadingIconToDisappear();
            SuspensionModel suspension = PageFactoryManager.Get<PartySuspensionPage>().GetNewSuspension();
            var suspensions = PageFactoryManager.Get<PartySuspensionPage>().GetAllSuspension();
            Assert.AreEqual(inputData.Sites.Select(x => x.Trim()).ToArray(), suspension.Sites.Split(',').Select(x => x.Trim()).ToArray());
            Assert.AreEqual(inputData.Services.Select(x => x.Trim()).ToArray(), suspension.Services.Split(',').Select(x => x.Trim()).ToArray());
            Assert.IsTrue(inputData.FromDate.Replace("/", "").Replace("-", "") == suspension.FromDate.Replace("/", "").Replace("-", "").Trim());
            Assert.IsTrue(inputData.LastDate.Replace("/", "").Replace("-", "") == suspension.LastDate.Replace("/", "").Replace("-", "").Trim());
            Assert.IsTrue(inputData.SuspensedDay == suspension.SuspensedDay.Trim());
            //Go to Calendar and Verify
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCalendarPage>()
                .ClickSiteCombobox()
                .ClickSellectAllSites()
                .ClickServiceCombobox()
                .ClickSellectAllServices()
                .ClickApplyCalendarButton()
                .WaitForLoadingIconToDisappear();
            DateTime fromDateTime = DateTime.ParseExact(inputData.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime toDateTime = DateTime.ParseExact(inputData.LastDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var serviceTasks = PageFactoryManager.Get<PartyCalendarPage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("service-suspension.svg")).ToList();
            Assert.IsTrue(serviceTasks.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() != 0);
            var serviceTasksNotInRange = serviceTasks.ToList();
            foreach (var item in suspensions)
            {
                serviceTasksNotInRange = serviceTasksNotInRange.Where(x => x.DateTime < fromDateTime && x.DateTime > toDateTime).ToList();
            }
            Assert.IsTrue(serviceTasksNotInRange.Count == 0);
            //Go to Site Tab and Verify
            PageFactoryManager.Get<DetailPartyPage>()
                 .ClickSiteTab()
                 .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .DoubleClickSiteRow(97)
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailSitePage>()
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            var serviceTasksInSitePage = PageFactoryManager.Get<DetailSitePage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("service-suspension.svg")).ToList();
            Assert.IsTrue(serviceTasksInSitePage.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() != 0);
        }

        [Category("Edit Suspension")]
        [Category("Huong")]
        [Test(Description = "Edit suspension")]
        [Order(2)]
        public void TC_090_Edit_Suspension()
        {
            int partyId = 73;
            string partyName = "Greggs";
            SuspensionInputData inputData = new SuspensionInputData();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickNewVerSuspensionTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartySuspensionPage>().ClickRefreshBtn()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartySuspensionPage>().ClickNewSuspension();
            inputData.FromDate = DateTime.Now.AddDays(4).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            inputData.LastDate = DateTime.Now.AddDays(18).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            inputData.SuspensedDay = "Everyday";
            PageFactoryManager.Get<EditSuspensionPage>()
               .VerifyNextButtonIsEnable()
               .VerifyServiceCheckboxsAreSelected()
               .ClickServiceCheckbox();
            Thread.Sleep(200);
            inputData.Sites = PageFactoryManager.Get<EditSuspensionPage>().GetSiteNames();
            PageFactoryManager.Get<EditSuspensionPage>()
               .ClickNextButton()
               .VerifyServiceTypeCheckboxsAreSelected();
            inputData.Services = PageFactoryManager.Get<EditSuspensionPage>().GetServiceNames();
            PageFactoryManager.Get<EditSuspensionPage>()
               .ClickNextButton()
               .InputDaysAndVerifyDaysCalculationLbl(inputData.FromDate, inputData.LastDate)
               .ClickFinish()
               .VerifySaveMessage("Saved")
               .IsEditSuspensionModalInVisible();
            SuspensionModel suspension = PageFactoryManager.Get<PartySuspensionPage>().GetNewSuspension();
            var suspensions = PageFactoryManager.Get<PartySuspensionPage>().GetAllSuspension();
            Assert.AreEqual(inputData.Sites.Select(x => x.Trim()).ToArray(), suspension.Sites.Split(',').Select(x => x.Trim()).ToArray());
            Assert.AreEqual(inputData.Services.Select(x => x.Trim()).ToArray(), suspension.Services.Split(',').Select(x => x.Trim()).ToArray());
            Assert.IsTrue(inputData.FromDate.Replace("/", "").Replace("-", "") == suspension.FromDate.Replace("/", "").Replace("-", "").Trim());
            Assert.IsTrue(inputData.LastDate.Replace("/", "").Replace("-", "") == suspension.LastDate.Replace("/", "").Replace("-", "").Trim());
            Assert.IsTrue(inputData.SuspensedDay == suspension.SuspensedDay.Trim());
            //Go to Calendar and Verify
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCalendarPage>()
                .ClickSiteCombobox()
                .ClickSellectSite(1)
                .ClickServiceCombobox()
                .ClickSellectAllServices()
                .ClickApplyCalendarButton()
                .WaitForLoadingIconToDisappear();
            DateTime fromDateTime = DateTime.ParseExact(inputData.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime toDateTime = DateTime.ParseExact(inputData.LastDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var serviceTasks = PageFactoryManager.Get<PartyCalendarPage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("service-suspension.svg")).ToList();
            Assert.IsTrue(serviceTasks.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() == 0);
            //Go to Site Tab and Verify
            PageFactoryManager.Get<DetailPartyPage>()
                 .ClickSiteTab()
                 .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .DoubleClickSiteRow(97)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailSitePage>()
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            var serviceTasksInSitePage = PageFactoryManager.Get<DetailSitePage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("service-suspension.svg")).ToList();
            Assert.IsTrue(serviceTasksInSitePage.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() == 0);
        }

        [Category("Delete Suspension")]
        [Category("Huong")]
        [Test(Description = "Delete suspension")]
        public void TC_181_Deleting_Service_Suspensions()
        {
            int partyId = 73;
            string partyName = "Greggs";
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickNewVerSuspensionTab()
                .WaitForLoadingIconToDisappear();
            //Add New Suspension
            PageFactoryManager.Get<PartySuspensionPage>().ClickAddNewSuspension();
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .WaitServiceSuspensionVisible()
                .VerifySuspensionTitle(partyName + " - Add Service Suspension");
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .VerifyNextButtonIsDisable()
                .ClickSelectAllSiteCheckbox()
                .VerifyNextButtonIsEnable()
                .ClickNextButton();
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .VerifyServiceNames()
                .VerifyNextButtonIsDisable()
                .ClickSelectAllServiceCheckbox()
                .VerifyNextButtonIsEnable()
                .ClickNextButton();
            var fromDate = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            var lastDate = DateTime.Now.AddDays(60).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            PageFactoryManager.Get<AddNewSuspensionPage>()
               .IsFirstDayInputVisible()
               .IsLastDayInputVisible()
               .IsDateDiffLabelVisible()
               .IsEveryDayRadioVisible()
               .IsSelectedDayRadioVisible()
               .ClickFinish()
               .VerifyWarningMessage("First Day is required")
               .InputDaysAndVerifyDaysCalculationLbl(fromDate, lastDate, "31 days")
               .ClickFinish()
               .VerifySaveMessage("Saved")
               .IsAddSuspensionModalInVisible();
            PageFactoryManager.Get<PartySuspensionPage>()
                .WaitForLoadingIconToDisappear();
            //Go to Calendar and Verify
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCalendarPage>()
                .ClickSiteCombobox()
                .ClickSellectAllSites()
                .ClickServiceCombobox()
                .ClickSellectAllServices()
                .ClickApplyCalendarButton()
                .WaitForLoadingIconToDisappear();
            DateTime fromDateTime = DateTime.ParseExact(fromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime toDateTime = DateTime.ParseExact(lastDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var serviceTasks = PageFactoryManager.Get<PartyCalendarPage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("service-suspension.svg")).ToList();
            Assert.IsTrue(serviceTasks.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() != 0);
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickNewVerSuspensionTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AddNewSuspensionPage>().ClickDeleteNewSuspension()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<DetailPartyPage>().ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            //Go to Calendar and Verify
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCalendarPage>()
                .ClickSiteCombobox()
                .ClickSellectAllSites()
                .ClickServiceCombobox()
                .ClickSellectAllServices()
                .ClickApplyCalendarButton()
                .WaitForLoadingIconToDisappear();
            serviceTasks = PageFactoryManager.Get<PartyCalendarPage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("service-suspension.svg")).ToList();
            Assert.IsTrue(serviceTasks.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() == 0);
        }
    }
}
