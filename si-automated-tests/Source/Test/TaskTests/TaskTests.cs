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
                .Login(AutoUser92.UserName, AutoUser92.Password)
                .IsOnHomePage(AutoUser92);
        }

        [Category("Tasks/tasklines")]
        [Test(Description = "Tasks/tasklines - Detail Tasks - Source - sevice task")]
        public void TC_125_Detail_tasks()
        {
            //Task from Contract = Commercial
            string taskIDWithSourceServiceTask = "3964";
            //Run query to get task's information
            List<TaskDBModel> taskInfoById = finder.GetTask(int.Parse(taskIDWithSourceServiceTask));
            List<ServiceUnitDBModel> serviceUnitDBModels = finder.GetServiceUnit(taskInfoById[0].serviceunitID);
            List<TaskLineDBModel> taskLineDBModels = finder.GetTaskLine(int.Parse(taskIDWithSourceServiceTask));
            List<TaskLineTypeDBModel> taskLineTypeDBModels = finder.GetTaskLineType(taskLineDBModels[0].tasklinetypeID);
            List<AssetTypeDBModel> assetTypeDBModels = finder.GetAssetType(taskLineDBModels[0].assettypeID);
            List<ProductDBModel> productDBModels = finder.GetProduct(taskLineDBModels[0].productID);
            List<PartiesDBModel> partiesDBModels = finder.GetPartiesByPartyId(taskInfoById[0].partyID.ToString());

            //Login ECHO and check the detail of the task
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/tasks/3964");
            
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .VerifyCurrentUrlOfDetailTaskPage(taskIDWithSourceServiceTask)
                //Verify response returned from DB
                .VerifyTaskWithDB(taskInfoById[0], serviceUnitDBModels[0])
                //Line 9: Click on the hyperlink next to Task
                .ClickHyperlinkNextToTask()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceTaskDetailPage serviceTaskDetailPage = PageFactoryManager.Get<ServiceTaskDetailPage>();
            serviceTaskDetailPage
                .WaitForServiceTaskDetailPageDisplayed()
                .VerifyCurrentUrlServiceTaskDetail(taskInfoById[0].servicetaskID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 12: Click on hyperlink in a description
            detailTaskPage
                .ClickOnHyperlinkInADesciption()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForServiceUnitDetailPageDisplayed()
                .VerifyCurrentUrlServiceTaskDetail(taskInfoById[0].serviceunitID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
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
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("Lidl")
                .VerifyCurrentUrlPartyDetailPage(taskInfoById[0].partyID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 15: Click site hyperlink
            detailTaskPage
                .ClickSiteHyperLink()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailSitePage detailSitePage = PageFactoryManager.Get<DetailSitePage>();
            detailSitePage
                .WaitForSiteDetailPageDisplayed()
                .VerifyCurrentUrlSitePage(partiesDBModels[0].correspondence_siteID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Line 16: Click round hyperlink
            detailTaskPage
                .ClickRoundHyperLink()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            RoundInstancesDetailPage roundInstancesDetailPage = PageFactoryManager.Get<RoundInstancesDetailPage>();
            roundInstancesDetailPage
                .WaitForRoundInstanceDetailPageDisplayed()
                .VerifyCurrentUrlRoundPage(taskInfoById[0].roundinstanceID.ToString())
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
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
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.AssetTypeOrProductMustBeSelected)
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.TaskLineTypeRequired);
            //Line 24: Select type
            detailTaskPage
                .SelectType(2)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageRequiredFieldConstants.AssetTypeOrProductMustBeSelected)
                .WaitUntilToastMessageInvisible(MessageRequiredFieldConstants.AssetTypeOrProductMustBeSelected);
            //Line 25: Select assetType and add scheduled qty = 1 -> Save
            detailTaskPage
                .SelectAssetType(2)
                .InputScheduledAssetQty(2, "1")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string timeCreatedSecondTaskLine = "";
            detailTaskPage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            List<TaskLineModel> allTaskLinesAfter = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyTaskLineCreated(allTaskLinesAfter[1], "Service", "1100L", "1");
            //Line 26: Click on add new item -> Select type, product and enter scheduled qty = 80 -> Save
            detailTaskPage
                .ClickAddNewItemTaskLineBtn()
                .SelectType(3)
                .SelectGeneralRecyclingProductAtAnyRow(3)
                .InputSheduledProductQuantiAtAnyRow(3, "80")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            allTaskLinesAfter = detailTaskPage
                .GetAllTaskLineInTaskLineTab();
            detailTaskPage
                .VerifyTaskLineCreated(allTaskLinesAfter[2], "Service", "General Recycling", 80);
            //Line 28: Double click on first task line
            detailTaskPage
                .DoubleClickAnyTaskLine("1")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            //Line 29: Verify all fields display correctly
            DetailTaskLinePage detailTaskLinePage = PageFactoryManager.Get<DetailTaskLinePage>();
            int taskLineId = detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .GetTaskLineId();
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLines[0]);
            //Line 30: Run query to get taskline detail and verify
            List<TaskLineDBModel> taskLineDBModelsDetail = finder.GetTaskLine(taskLineId);
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLines[0], taskLineDBModelsDetail[0]);
            //Step line 31: Click on the task line hyperlink in the header
            detailTaskLinePage
                .ClickOnHyperlinkOnHeader()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .VerifyCurrentUrl(taskIDWithSourceServiceTask)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Step line 32: Run query and check
            List<TaskAndTaskTypeByTaskIdDBModel> taskAndTaskTypeByTaskIdDBModels = finder.GetTaskAndTaskTypeByTaskId(taskIDWithSourceServiceTask);

            detailTaskLinePage
                .VerifyHyperlinkOnHeader(taskAndTaskTypeByTaskIdDBModels[0], taskIDWithSourceServiceTask);
            //Step line 33: CLick on [Data tab]
            detailTaskLinePage
                .ClickOnDataTab()
                .VerifyNotDisplayErrorMessage();
            string[] expValueHistoryTab = { "0", "1100L", "0", "2", "105", "", "General Refuse", "Kilograms", "Pending", "Unticked", "0" };
            //Step line 34: Click on [History tab]
            detailTaskLinePage
                .ClickOnHistoryTab()
                .VerifyNotDisplayErrorMessage();
            string destinationSite = "Kingston Tip, 20 Chapel Mill Road, Kingston upon Thames, KT1 3GZ";
            string destinationSiteName = "Kingston Tip";
            TaskLineModel taskLineModelNew = new TaskLineModel("1", "660L", "1", "3", "1", "100", destinationSite, "5", "2", "6", "3", "Client Ref", "Completed", "GW1", "General Refuse", "Kilograms");
            detailTaskLinePage
                .VerifyActionCreateWithUserDisplay("09/06/2022 02:07", CommonConstants.ActionCreateInHistoryTaskLineDetail, expValueHistoryTab, "")
                //Step line 35: Detail tab and Update all fields that are not read only and set state to completed
                .InputAllFieldInDetailTab(taskLineModelNew)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage);
            string timeUpdated = "";
            detailTaskLinePage
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            detailTaskLinePage
                .VerifyTaskLineInfo(taskLineModelNew)
                .VeriryConfirmationAndCompletedDate(timeUpdated, "Manually Confirmaed on Web")
                //Step line 36: Click on History tab and verify all updated
                .ClickOnHistoryTab()
                .WaitForLoadingIconToDisappear();
            string[] expValueHistoryAfterUpdate = { taskLineModelNew.actualAssetQuantity, taskLineModelNew.assetType, taskLineModelNew.actualProductQuantity, taskLineModelNew.scheduledAssetQty, taskLineModelNew.scheduledProductQuantity, taskLineModelNew.state, taskLineModelNew.order, CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT), taskLineModelNew.clientRef, destinationSiteName, taskLineModelNew.siteProduct, taskLineModelNew.minAssetQty, taskLineModelNew.maxAssetQty, taskLineModelNew.minProductQty, taskLineModelNew.maxProductQty, "Manually Confirmaed on Web" };
            detailTaskLinePage
                .VerifyActionCreateWithUserDisplay("09/06/2022 02:07", CommonConstants.ActionUpdateInHistoryTaskLineDetail, expValueHistoryAfterUpdate, AutoUser92.DisplayName)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 37: Tasks > Verdict tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                .VerifyFirstTaskLineStateVerdictTab(timeUpdated, taskLineModelNew.state, "Manually Confirmaed on Web", taskLineModelNew.product, taskLineModelNew.actualProductQuantity + "kg", taskLineModelNew.assetType, taskLineModelNew.actualAssetQuantity);
            //Step line 39: Double click on the taskline you have created
            detailTaskPage
                .ClickOnTaskLineTab()
                .DoubleClickAnyTaskLine("2")
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            int taskLineIdNew = detailTaskLinePage
                .WaitForTaskLineDetailDisplayed()
                .GetTaskLineId();
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLinesAfter[1]);
            //Line 41: Run query to get new taskline detail and verify
            List<TaskLineDBModel> taskLineDBModelsDetailNew = finder.GetTaskLine(taskLineIdNew);
            detailTaskLinePage
                .VerifyTaskLineInfo(allTaskLinesAfter[1], taskLineDBModelsDetailNew[0]);
            //Step line 42: Click on data tab
            detailTaskLinePage
                .ClickOnDataTab()
                .VerifyNotDisplayErrorMessage();
            //Step line 43: Click on [History] tab
            detailTaskLinePage
                .ClickOnHistoryTab()
                .VerifyNotDisplayErrorMessage();
            string[] expValueSecondTaskLineHistotyTab = { "0", "660L", "0", "1", "0", "", "Pending", "Unticked", "0", "", "0", "0", "0", "0", "0", "Not Certified", "0" };
            detailTaskLinePage
                .VerifyActionCreateWithUserDisplay(timeCreatedSecondTaskLine, CommonConstants.ActionCreateSecondTaskLineInHistoryTaskLineDetail, expValueSecondTaskLineHistotyTab, AutoUser92.DisplayName)
                .ClickCloseBtn()
                .SwitchToChildWindow(1);
            //Step line 44: Tasks > Verdict tab -> Task lines tab
            detailTaskPage
                .ClickOnVerdictTab()
                .ClickOnTaskLineVerdictTab()
                .VerifySecondTaskLineStateVerdictTab("Pending", "660L", "", "1", "0", "0", taskLineIdNew.ToString());
            //Step line 45: Click back on details tab and change the state to Cancelled
            detailTaskPage
                .ClickOnDetailTab()
                .ClickOnTaskStateDd()
                .SelectAnyTaskStateInDd("Cancelled")
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);


        }
    }
}
