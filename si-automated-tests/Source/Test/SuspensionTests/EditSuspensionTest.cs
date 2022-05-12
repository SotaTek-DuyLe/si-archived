using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySuspension;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar;
using si_automated_tests.Source.Main.Finders;


namespace si_automated_tests.Source.Test.SuspensionTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class EditSuspensionTest : BaseTest
    {
        private CommonFinder finder;
        public override void Setup()
        {
            base.Setup();
            //finder = new CommonFinder(DatabaseContext);
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

        [Category("Edit Suspension")]
        [Test(Description = "Edit suspension")]
        public void TC_090_Edit_Suspension()
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
            PageFactoryManager.Get<PartySuspensionPage>().DoubleClickNewSuspension();
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
                .ClickSellectAllSites()
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
    }
}
