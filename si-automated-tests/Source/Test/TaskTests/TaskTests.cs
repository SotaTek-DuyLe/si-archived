using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Round.RoundInstance;
using si_automated_tests.Source.Main.Pages.Services.ServiceTask;
using si_automated_tests.Source.Main.Pages.Services.ServiceUnit;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.TaskTests
{
    public class TaskTests : BaseTest
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
                .Login(AutoUser58.UserName, AutoUser58.Password)
                .IsOnHomePage(AutoUser58);
        }

        [Category("Tasks/tasklines")]
        [Test(Description = "Tasks/tasklines - Detail Tasks - Source - sevice task")]
        public void TC_125_Detail_tasks()
        {
            string taskIDWithSourceServiceTask = "3964";
            //Run query to get task's information
            List<TaskDBModel> taskInfoById = finder.GetTask(int.Parse(taskIDWithSourceServiceTask));
            List<ServiceUnitDBModel> serviceUnitDBModels = finder.GetServiceUnit(taskInfoById[0].serviceunitID);
            List<TaskLineDBModel> taskLineDBModels = finder.GetTaskLine(int.Parse(taskIDWithSourceServiceTask));
            List<TaskLineTypeDBModel> taskLineTypeDBModels = finder.GetTaskLineType(taskLineDBModels[0].tasklinetypeID);
            List<AssetTypeDBModel> assetTypeDBModels = finder.GetAssetType(taskLineDBModels[0].assettypeID);
            List<ProductDBModel> productDBModels = finder.GetProduct(taskLineDBModels[0].productID);

            //Login ECHO and check the detail of the task
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Tasks")
                .OpenOption("North Star Commercial")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId(taskIDWithSourceServiceTask)
                .ClickOnFirstRecord()
                .SwitchToLastWindow();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage()
                .VerifyCurrentUrlOfDetailTaskPage(taskIDWithSourceServiceTask)
                //Verify response returned from DB
                .VerifyTaskWithDB(taskInfoById[0], serviceUnitDBModels[0])
                //Line 9: Click on the hyperlink next to Task
                .ClickHyperlinkNextToTask()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            ServiceTaskDetailPage serviceTaskDetailPage = PageFactoryManager.Get<ServiceTaskDetailPage>();
            serviceTaskDetailPage
                .WaitForServiceTaskDetailPageDisplayed()
                .VerifyCurrentUrlServiceTaskDetail(taskInfoById[0].servicetaskID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Line 12: Click on hyperlink in a description
            detailTaskPage
                .ClickOnHyperlinkInADesciption()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForServiceUnitDetailPageDisplayed()
                .VerifyCurrentUrlServiceTaskDetail(taskInfoById[0].serviceunitID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Line 13: only party, site and round have hyperlink in the header
            detailTaskPage
                .VerifyFieldInHeaderWithLink("Party")
                .VerifyFieldInHeaderWithLink("Site")
                .VerifyFieldInHeaderWithLink("Round")
                .VerifyFieldInHeaderReadOnly("Contract")
                .VerifyFieldInHeaderReadOnly("Service Group")
                .VerifyFieldInHeaderReadOnly("Service")
                .VerifyFieldInHeaderReadOnly("Round Group")
                //Line 14: Click party hyperlink
                .ClickPartyHyperLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("Lidl")
                .VerifyCurrentUrlPartyDetailPage(taskInfoById[0].partyID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Line 15: Click site hyperlink
            detailTaskPage
                .ClickSiteHyperLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            DetailSitePage detailSitePage = PageFactoryManager.Get<DetailSitePage>();
            detailSitePage
                .WaitForSiteDetailPageDisplayed()
                .VerifyCurrentUrlSitePage(taskInfoById[0].siteID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Line 16: Click round hyperlink
            detailTaskPage
                .ClickRoundHyperLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            RoundInstancesDetailPage roundInstancesDetailPage = PageFactoryManager.Get<RoundInstancesDetailPage>();
            roundInstancesDetailPage
                .WaitForRoundInstanceDetailPageDisplayed()
                .VerifyCurrentUrlRoundPage(taskInfoById[0].roundinstanceID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Line 17 - Details tab
            detailTaskPage
                .VerifyDetailTabWithDataInDB(taskInfoById[0])
                //Line 19 - Click Data tab and verify no error displayed
                .ClickDataTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessageNotAppear();
            //Line 20 - Click Source Data and verify no error displayed
            detailTaskPage
                .ClickSourceDataTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessageNotAppear();
            //Line 21 - Click Task line and verify no error displayed
            detailTaskPage
                .ClickTasklinesTab()
                .WaitForLoadingIconToDisappear()
                .VerifyToastMessageNotAppear();
            //Line 22: Verify data in task line tab
            List<TaskLineModel> allTaskLines = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyDataInTaskLinesTab(taskLineDBModels[0], allTaskLines[0], taskLineTypeDBModels[0], assetTypeDBModels[0], productDBModels[0]);
            //Line 23: CLick Add new Item
            detailTaskPage
                .ClickAddNewItemTaskLineBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.AssetTypeOrProductMustBeSelected)
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.TaskLineTypeRequired);
            //Line 24: Select type
            detailTaskPage
                .SelectType(2)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.AssetTypeOrProductMustBeSelected);
            //Line 25: Select assetType and add scheduled qty = 1 -> Save
            detailTaskPage
                .SelectAssetType(2)
                .InputScheduledAssetQty(2, "1")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            List<TaskLineModel> allTaskLinesAfter = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyTaskLineCreated(allTaskLinesAfter[1], "Service", "1100L", "1");
            //Line 26: Click on add new item -> Select type, product and enter scheduled qty = 80 -> Save
            detailTaskPage
                .ClickAddNewItemTaskLineBtn()
                .SelectType(3);

        }
    }
}
