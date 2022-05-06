using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Suspension;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.Inspections;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Suspension;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.SuspensionTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateSuspensionTest : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
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
        [Test(Description = "Add new suspension")]
        public void TC_089_Add_New_Suspension()
        {
            int partyId = 73;
            string partyName = "Greggs";
            SuspensionInputData inputData = new SuspensionInputData();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
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
                .ClickTabDropDown()
                .ClickSuspensionTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>().ClickAddNewSuspension();
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .WaitServiceSuspensionVisible()
                .VerifySuspensionTitle(partyName + " - Add Service Suspension");
            inputData.Sites = PageFactoryManager.Get<AddNewSuspensionPage>().GetSiteNames();
            string query = $"select * from sites where partyID={partyId};";
            SqlCommand commandSites = new SqlCommand(query, DatabaseContext.Conection);
            SqlDataReader readerSites = commandSites.ExecuteReader();
            List<SiteDBModel> sites = ObjectExtention.DataReaderMapToList<SiteDBModel>(readerSites);
            readerSites.Close();
            Assert.AreEqual(inputData.Sites, sites.Select(x => x.site).ToList(), "The data from the database does not match all of the sites configured for the Party.");
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .VerifyNextButtonIsDisable()
                .ClickSelectAllSiteCheckbox()
                .VerifyNextButtonIsEnable()
                .ClickNextButton();
            inputData.Services = PageFactoryManager.Get<AddNewSuspensionPage>().GetServiceNames();
            string queryService = $"select * from servicetypes;";
            SqlCommand commandServices = new SqlCommand(queryService, DatabaseContext.Conection);
            SqlDataReader readerServices = commandServices.ExecuteReader();
            List<ServiceDBModel> services = ObjectExtention.DataReaderMapToList<ServiceDBModel>(readerServices);
            readerServices.Close();
            Assert.AreEqual(inputData.Services, services.Select(x => x.servicetype).ToList(), "The data from the database does not match all of the services configured for the Party.");
            PageFactoryManager.Get<AddNewSuspensionPage>()
                .VerifyNextButtonIsDisable()
                .ClickSelectAllServiceCheckbox()
                .VerifyNextButtonIsEnable()
                .ClickNextButton();

            inputData.FromDate = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            inputData.LastDate = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
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
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            SuspensionModel suspension = PageFactoryManager.Get<DetailPartyPage>().GetNewSuspension();
            var suspensions = PageFactoryManager.Get<DetailPartyPage>().GetAllSuspension();
            Assert.IsTrue(string.Join(", ", inputData.Sites.ToArray()) == suspension.Sites);
            Assert.IsTrue(string.Join(", ", inputData.Services.ToArray()) == suspension.Services);
            Assert.IsTrue(inputData.FromDate == suspension.FromDate);
            Assert.IsTrue(inputData.LastDate == suspension.LastDate);
            Assert.IsTrue(inputData.SuspensedDay == suspension.SuspensedDay);
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCalendarTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickSiteCombobox()
                .ClickSellectAllSites()
                .ClickServiceCombobox()
                .ClickSellectAllServices()
                .ClickApplyCalendarButton()
                .WaitForLoadingIconToDisappear();
            DateTime fromDateTime = DateTime.ParseExact(inputData.FromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime toDateTime = DateTime.ParseExact(inputData.LastDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var serviceTasks = PageFactoryManager.Get<DetailPartyPage>().GetAllDataInMonth(fromDateTime, toDateTime).Where(x => x.ImagePath.AsString().Contains("service-suspension.svg")).ToList();
            Assert.IsTrue(serviceTasks.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() != 0);
            var serviceTasksNotInRange = serviceTasks.ToList();
            foreach (var item in suspensions)
            {
                serviceTasksNotInRange = serviceTasksNotInRange.Where(x => x.DateTime < fromDateTime && x.DateTime > toDateTime).ToList();
            }
            Assert.IsTrue(serviceTasksNotInRange.Count == 0);
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
            Assert.IsTrue(serviceTasksInSitePage.Where(x => fromDateTime <= x.DateTime && x.DateTime <= toDateTime).Count() != 0);
        }
    }
}
