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
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAdHoc;

namespace si_automated_tests.Source.Test.AdHocTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateAdHocTest : BaseTest
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

        [Category("Create ad-hoc task")]
        [Test(Description = "Create ad-hoc task")]
        public void TC_091_CreateAdHocTask()
        {
            int partyId = 73;
            string partyName = "Greggs";
            string inputPO = "PO ad hoc task 1";
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
                .ClickAdHocTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AdhocPage>()
                .ClickCreateAdHocTask("Repair Commercial Bin");
            Thread.Sleep(200);
            PageFactoryManager.Get<CreateAdHocTaskPage>()
                .VerifyTitle("PO Number Required for Party")
                .InputPoNumber(inputPO)
                .ClickDone()
                .IsCreateAdhocTaskInVisible();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<AdhocTaskDetailPage>()
                .VerifyPoNumber()
                .VerifyPurchaseOrderField(inputPO)
                .ClickTaskLinesTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskLinesPage>()
                .VerifyTaskLine();
        }
    }
}
