using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.IE_Configuration;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Search.PointSegment;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Tasks;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Author("Dee", "duy.le@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class BugFixTests : BaseTest
    {
        [Category("Bug fix")]
        [Category("Chang")]
        [Test(Description = "Asset form - data and trail tab (bug fix)")]
        public void TC_162_Asset_form_data_and_trail_tab()
        {
            string assetTypeValue = "1100L";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46)
                //Open link [web/service-units/223643]
                .GoToURL(WebUrl.MainPageUrl + "/web/service-units/223643");
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .WaitForLoadingIconToDisappear();
            string serviceUnitName = serviceUnitDetailPage
                .GetServiceUnitName();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                //Line 8: Click on [Asset] tab
                .ClickOnAssetTab()
                .ClickOnAddNewItemBtn()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            AssetDetailItemPage assetDetailItemPage = PageFactoryManager.Get<AssetDetailItemPage>();
            assetDetailItemPage
                .IsOnPage()
                //Line 9: Click on [Data] tab
                .ClickOnDataTab()
                .VerifyNotDisplayMessageAttributeConfig(MessageRequiredFieldConstants.WhoopsSomethingIsWrongAttributesConfig)
                //Line 10: Click on [Detail] tab
                .ClickOnDetailTab()
                .ClickAndSelectAssetType(assetTypeValue)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            assetDetailItemPage
                .VerifyCurrentUrl()
                //Line 11: Click on [Trail] tab
                .ClickOnTrailTab()
                .VerifyServiceUnitAtFirstRowTrailTab(serviceUnitName)
                .ClickOnServiceUnitLinkAtFirstRowTrailTab()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .VerifyCurrentUrl("223643")
                .VerifyToastMessagesIsUnDisplayed();

        }

        [Category("Bug fix")]
        [Category("Chang")]
        [Test(Description = "Asset - Trail - opens incorrect service unit (bug fix)")]
        public void TC_168_Asset_trail_opens_incorrect_Service_unit()
        {
            string serviceUnitId = "230016";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOption("Collections")
                .ExpandOptionLast("Commercial Collections")
                .OpenOption("Active Service Units")
                .SwitchNewIFrame();
            ServiceUnitPage serviceUnitPage = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnitPage
                .FindServiceUnitWithId(serviceUnitId)
                .DoubleClickServiceUnit()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage();

            string serviceUnitName = serviceUnitDetailPage.GetServiceUnitName();
            serviceUnitDetailPage
                .ClickOnAssetTab()
                .DoubleClickOnFirstRowAtAssetTab()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            AssetDetailItemPage assetDetailItemPage = PageFactoryManager.Get<AssetDetailItemPage>();
            assetDetailItemPage
                .IsOnPage()
                .ClickOnTrailTab()
                .VerifyServiceUnitAtFirstRowTrailTab(serviceUnitName)
                .ClickOnServiceUnitLinkAtFirstRowTrailTab()
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .VerifyCurrentUrl(serviceUnitId)
                .VerifyToastMessagesIsUnDisplayed();
        }

        [Category("Bug fix")]
        [Category("Chang")]
        [Test(Description = "Loading of tasks without partyID is failing (bug fix)")]
        public void TC_167_Loading_of_tasks_without_partyId_is_failing()
        {
            string taskIdRM = "19099";
            string taskIdRMC = "19657";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Municipal)
                .SwitchNewIFrame();
            //Double click on row where [Party] column is blank [RM]
            PageFactoryManager.Get<TasksListingPage>()
                .WaitForTaskListinPageDisplayed()
                .FilterByTaskId(taskIdRM)
                .ClickOnFirstRecord()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailTaskPage detailTaskPage = PageFactoryManager.Get<DetailTaskPage>();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyCurrentUrl(taskIdRM)
                .VerifyToastMessagesIsUnDisplayed()
                .ClickCloseBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame()
                .SwitchToDefaultContent();
            //Doube click on row where [Party] column is populated [RMC]
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<TasksListingPage>()
               .WaitForTaskListinPageDisplayed()
               .FilterByTaskId(taskIdRMC)
               .ClickOnFirstRecord()
               .SwitchToChildWindow(2)
               .WaitForLoadingIconToDisappear();
            detailTaskPage
                .IsDetailTaskPage()
                .ClickOnDetailTab()
                .WaitForLoadingIconToDisappear();
            detailTaskPage
                .VerifyCurrentUrl(taskIdRMC)
                .VerifyToastMessagesIsUnDisplayed();
        }

        //BUG
        [Category("Bug fix")]
        [Category("Chang")]
        [Test(Description = "The Weighbridge setting is not recorded in party actions (bug fix)")]
        public void TC_177_The_Weighbridge_setting_is_not_recorded_in_party_actions()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            string partyId = "1122";
            string userId = "1092";
            string partyName = "The Mitre";
            string restrictedSite = "Kingston Tip - 20 Chapel Mill Road, Kingston upon Thames, KT1 3GZ";
            string licenceNumber = CommonUtil.GetRandomNumber(5);
            string licenceNumberExp = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2);
            string dormanceDate = CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 3);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickWBSettingTab()
                .WaitForLoadingIconToDisappear();
            //Change some fields in tab
            detailPartyPage
                //Change [Auto-print Weighbridge Ticket] - Before: Ticked
                .ClickOnAutoPrintTickedCheckbox();
            //Change [purchase order number required] - Before: UnTicked
            detailPartyPage
                //.ClickOnPurchaseOrderNumberRequiredCheckbox()
                //Change [driver name required] - Before: UnTicked
                .ClickOnDriverNameRequiredCheckbox()
                //Change [use stored purchase order number] - Before: UnTicked
                .ClickOnUseStorePoNumberCheckbox()
                //Change [allow manual purchase order number,] - Before: Ticked
                .ClickOnAllowManualPoNumberCheckbox()
                //Change [external round code required] - Before: UnTicked
                .ClickOnExternalRoundCodeRequiredCheckbox()
                //Change [use stored external round code] - Before: UnTicked
                .ClickOnUseStoredExternalRoundCodeRequiredCheckbox()
                //Change [allow manual external round code] - Before: Ticked
                .ClickOnAllowManualExternalRoundCodeCheckbox()
                //Change [allow manual name entry] - Before: UnTicked
                .ClickOnAllowManualNameEntryCheckbox()
                //Change [Restrict Products] - Before: UnTicked
                .ClickOnRestrictProductsCheckbox()
                //Select [Authorise Tipping] - Before [Do Not Override On Stop]
                .SelectAnyOptionAuthoriseTipping("Never Allow Tipping")
                //Select [Restricted Sites]
                .SelectAnyOptionRestrictedSites(restrictedSite)
                //Input [Licence Number]
                .InputLicenceNumber(licenceNumber)
                //Input [Licence Number Expiry]
                .InputLienceNumberExField(licenceNumberExp)
                //Input [Domain Date]
                .InputDormantDate(dormanceDate)
                //Clear [Warning Limit £]
                .ClearTextInWarningLimit()
                .ClickSaveBtn()
                .WaitForLoadingIconToDisappear();
            //.VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
            //.WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            //Click on [History] tab and verify
            detailPartyPage
                .ClickOnHistoryTab()
                .ClickRefreshBtn();
            string[] valueChangedExp = { ".", "NO.", "YES.", "YES.", "NO.", "YES.", "YES.", "NO.", "YES.", "NO.", licenceNumber + ".", licenceNumberExp + " 00:00.", dormanceDate + " 00:00.", "YES." };
            detailPartyPage
                .VerifyInfoInHistoryTab(CommonConstants.HistoryTitleAfterUpdateWBTicketTab, valueChangedExp, AutoUser46.DisplayName)
                .VerifyRestrictedSite("Kingston Tip.");

            //API to verify
            List<PartyActionDBModel> list = commonFinder.GetPartyActionByPartyIdAndUserId(partyId, userId);
            PartyActionDBModel partyActionDBModel = list[1];
            Assert.AreEqual(licenceNumber, partyActionDBModel.wb_licencenumber, "Licence number is incorrect");
            Assert.IsFalse(partyActionDBModel.wb_autoprint, "Auto-print is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_driverrequired, "Driver Name Required is incorrect");
            //Assert.IsTrue(partyActionDBModel.wb_driverrequired, "Purchase Order Number Required is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_usestoredpo, "Use Stored Purchase Order Number is incorrect");
            Assert.IsFalse(partyActionDBModel.wb_usemanualpo, "Allow Manual Purchase Order Number is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_externalroundrequired, "External Round Code Required is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_usestoredround, "Use Stored External Round Code is incorrect");
            Assert.IsFalse(partyActionDBModel.wb_usemanualround, "Allow Manual External Round Code is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_allowmanualname, "Allow Manual Name Entry is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_restrictproducts, "Restrict Products is incorrect");
            Assert.AreEqual(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 2), partyActionDBModel.wb_licencenumberexpiry.ToString(CommonConstants.DATE_MM_DD_YYYY_FORMAT), "Licence Number Expiry is incorrect");
            Assert.AreEqual(CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_MM_DD_YYYY_FORMAT, 3), partyActionDBModel.wb_dormantdate.ToString(CommonConstants.DATE_MM_DD_YYYY_FORMAT), "Dormant Date is incorrect");
            Assert.AreEqual(null, partyActionDBModel.wb_creditlimitwarning, "Warning Limit £ is incorrect");
        }

        [Category("Bug fix")]
        [Category("Dee")]
        [Test(Description = "Unable to add a new Resolution code (bug fix)")]
        public void TC_178_verify_that_new_resolution_can_be_added()
        {
            string url = WebUrl.MainPageUrl + "web/grids/resolutioncodes";
            string resoName = "Test resolution " + CommonUtil.GetRandomNumber(5);
            string clientRef = "Test ref " + CommonUtil.GetRandomNumber(5);
            PageFactoryManager.Get<LoginPage>()
                 .GoToURL(url);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password);
            PageFactoryManager.Get<CommonGridPage>()
                .IsOnGrid()
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResolutionCodeDetailPage>()
                .IsOnResolutionCodeDetailPage()
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CommonGridPage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResolutionCodeDetailPage>()
               .IsOnResolutionCodeDetailPage()
               .VerifyNoIdIsGenerated()
               .InputResolutionCodeDetails(resoName, clientRef)
               .SaveResolutionCode()
               .WaitForLoadingIconToDisappear()
               .CloseCurrentWindow()
               .SwitchToLastWindow()
               .ClickRefreshBtn();
            PageFactoryManager.Get<CommonGridPage>()
                .IsOnGrid()
                .VerifyFirstResultValue("Name", resoName);
        }

        [Category("Bug fix")]
        [Category("Dee")]
        [Test(Description = "The AdHoc tasks don't inherit the PartyID from ServiceTask (bug fix)")]
        public void TC_179_verify_that_adhoc_task_can_inherit_partyID()
        {
            CommonFinder finder = new CommonFinder(DbContext);
            PageFactoryManager.Get<LoginPage>()
                 .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Tasks)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstServiceTaskLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string serviceTaskDescription = PageFactoryManager.Get<ServicesTaskPage>()
                .IsServiceTaskPage()
                .GetServiceTaskDescription();

            string serviceTaskSite = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceSite();

            string serviceTaskGroup = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceGroupName();

            string serviceTask = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceName();

            string serviceTaskId = PageFactoryManager.Get<ServicesTaskPage>()
                .GetServiceTaskId();

            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickCreateAdhocTaskButton()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<DetailTaskPage>()
                .IsDetailTaskPage()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            string taskDescription = PageFactoryManager.Get<DetailTaskPage>()
                .GetLocationName();

            string site = PageFactoryManager.Get<DetailTaskPage>()
                .GetSite();

            string group = PageFactoryManager.Get<DetailTaskPage>()
                .GetServiceGroup();

            string service = PageFactoryManager.Get<DetailTaskPage>()
                .GetServiceName();

            string taskId = PageFactoryManager.Get<DetailTaskPage>()
                .GetTaskId();

            Assert.AreEqual(serviceTaskDescription, taskDescription);
            Assert.AreEqual(serviceTaskSite, site);
            Assert.AreEqual(serviceTaskGroup, group);
            Assert.AreEqual(serviceTask, service);

            TaskDBModel firstTask = finder.GetTask(int.Parse(taskId))[0];
            ServiceTaskDBModel firstServiceTask = finder.GetTaskService(int.Parse(serviceTaskId))[0];

            Assert.AreEqual(firstTask.PartyId, firstServiceTask.PartyId);
            Assert.AreEqual(firstTask.AgreementId, firstServiceTask.AgreementId);
            Assert.AreEqual(firstTask.AgreementlinetasktypeId, firstServiceTask.AgreementlinetasktypeId);
            Assert.AreEqual(firstTask.ServiceTaskId, int.Parse(serviceTaskId));
        }

        [Category("ServiceUnitPoint")]
        [Category("Chang")]
        [Test(Description = "Service Unit point map showing incorrect data (bug fix) - Point Segment")]
        public void TC_205_Service_Unit_point_map_showing_incorrect_data_point_segment()
        {
            string idSegment = "32807";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOptionLast(Contract.Commercial)
                .OpenOption("Point Segments")
                .SwitchNewIFrame();
            //Step line 8: Open a point segment
            PageFactoryManager.Get<PointSegmentListingPage>()
                .WaitForPointSegmentsPageDisplayed()
                .FilterSegmentById(idSegment)
                .DoubleClickFirstPointSegmentRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<PointSegmentDetailPage>();
            string segmentDesc = pointSegmentDetailPage
                .WaitForPointSegmentDetailPageDisplayed()
                .ClickOnMapTab()
                .GetDescInMapTab();
            pointSegmentDetailPage
                .ClickOnActiveServiceTab()
                .WaitForLoadingIconToDisappear();

            //Step line 9: Open [Service unit]
            pointSegmentDetailPage
                .ClickOnFirstServiceUnit()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            //Step line 10: At [Service Unit] detail click on [Service Unit Points]
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .ClickOnServiceUnitPointsTab()
                .DoubleClickOnServiceUnitPointId(idSegment)
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();

            ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(segmentDesc)
                .ClickOnMapTab()
                .VerifyValueInMapTabSegmentType(segmentDesc);
        }

        [Category("ServiceUnitPoint")]
        [Category("Chang")]
        [Test(Description = "Service Unit point map showing incorrect data (bug fix) - Point Note")]
        public void TC_205_Service_Unit_point_map_showing_incorrect_data_point_note()
        {
            string idNode = "3";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.Commercial)
                .ExpandOptionLast(Contract.Commercial)
                .OpenOption("Point Nodes")
                .SwitchNewIFrame();
            //Step line 8: Open a point node
            PageFactoryManager.Get<PointNodeListingPage>()
                .WaitForPointNodeListingPageDisplayed()
                .FilterNodeById(idNode)
                .DoubleClickFirstPointNodeRow()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            PointNodeDetailPage pointNodeDetailPage = PageFactoryManager.Get<PointNodeDetailPage>();
            pointNodeDetailPage
                .WaitForPointNodeDetailDisplayed();
            string nodeDesc = pointNodeDetailPage
                .ClickOnMapTab()
                .GetDescInMapTab();
            pointNodeDetailPage
                .ClickOnActiveServicesTab()
                .WaitForLoadingIconToDisappear();

            //Step line 9: Open [Service unit]
            pointNodeDetailPage
                .ClickOnFirstServiceUnit()
                .SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
            //Step line 10: At [Service Unit] detail click on [Service Unit Points]
            serviceUnitDetailPage
                .IsServiceUnitDetailPage()
                .ClickOnServiceUnitPointsTab()
                .DoubleClickOnServiceUnitPointId(idNode)
                .SwitchToChildWindow(4)
                .WaitForLoadingIconToDisappear();

            ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
            serviceUnitPointDetailPage
                .IsServiceUnitPointDetailPage(nodeDesc)
                .ClickOnMapTab()
                .VerifyValueInMapTabNoteType(nodeDesc);
        }

        //[Category("ServiceUnitPoint")]
        //[Category("Chang")]
        //[Test(Description = "Service Unit point map showing incorrect data (bug fix) - Point Area")]
        //public void TC_205_Service_Unit_point_map_showing_incorrect_data_point_area()
        //{
        //    string idArea = "6";

        //    PageFactoryManager.Get<LoginPage>()
        //           .GoToURL(WebUrl.MainPageUrl);
        //    PageFactoryManager.Get<LoginPage>()
        //        .IsOnLoginPage()
        //        .Login(AutoUser46.UserName, AutoUser46.Password)
        //        .IsOnHomePage(AutoUser46);
        //    PageFactoryManager.Get<NavigationBase>()
        //        .ClickMainOption(MainOption.Services)
        //        .ExpandOption("Regions")
        //        .ExpandOption(Region.UK)
        //        .ExpandOption(Contract.RMC)
        //        .ExpandOptionLast(Contract.RMC)
        //        .OpenOption("Point Areas")
        //        .SwitchNewIFrame();
        //    //Step line 8: Open a point node
        //    PageFactoryManager.Get<PointAreaListingPage>()
        //        .WaitForPointAreaListingPageDisplayed()
        //        .FilterAreaById(idArea)
        //        .DoubleClickFirstPointAreaRow()
        //        .SwitchToChildWindow(2)
        //        .WaitForLoadingIconToDisappear();
        //    PointAreaDetailPage pointAreaDetailPage = PageFactoryManager.Get<PointAreaDetailPage>();
        //    pointAreaDetailPage
        //        .WaitForAreaDetailDisplayed();
        //    string areaDesc = pointAreaDetailPage
        //        .ClickOnMapTab()
        //        .GetDescInMapTab();
        //    pointAreaDetailPage
        //        .ClickOnActiveServicesTab()
        //        .WaitForLoadingIconToDisappear();

        //    //Step line 9: Open [Service unit]
        //    pointAreaDetailPage
        //        .ClickOnFirstServiceUnit()
        //        .SwitchToChildWindow(3)
        //        .WaitForLoadingIconToDisappear();
        //    ServiceUnitDetailPage serviceUnitDetailPage = PageFactoryManager.Get<ServiceUnitDetailPage>();
        //    //Step line 10: At [Service Unit] detail click on [Service Unit Points]
        //    serviceUnitDetailPage
        //        .IsServiceUnitDetailPage()
        //        .ClickOnServiceUnitPointsTab()
        //        .DoubleClickOnServiceUnitPointId(idArea)
        //        .SwitchToChildWindow(4)
        //        .WaitForLoadingIconToDisappear();

        //    ServiceUnitPointDetailPage serviceUnitPointDetailPage = PageFactoryManager.Get<ServiceUnitPointDetailPage>();
        //    serviceUnitPointDetailPage
        //        .IsServiceUnitPointDetailPage(areaDesc)
        //        .ClickOnMapTab()
        //        .VerifyValueInMapTabAreaType(areaDesc);

        [Category("Dee")]
        [Test(Description = "Add a hyperlink to Round form for easier access of round group form")]
        public void TC_207_hyper_link_for_round_group()
        {
            string url= WebUrl.MainPageUrl + "web/rounds/37";

            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(url);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .WaitForLoadingIconToDisappear();
            var roundName = PageFactoryManager.Get<RoundDetailPage>()
                .GetRoundName();
            PageFactoryManager.Get<RoundDetailPage>()
                .ClickRoundGroupHyperLink()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            var roundGroupName = PageFactoryManager.Get<RoundGroupPage>()
                .GetRoundGroupName();
            Assert.IsTrue(roundName.Contains(roundGroupName));
        }
        [Category("Dee")]
        [Test(Description = "Daily Allocation - Prompt user with resolution code dropdown when resolution code is mandatory for Resource State")]
        public void TC_219_daily_allocation_prompt_user_with_resolution_code()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string resourceType = "Driver";
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 5);

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser46.UserName, AutoUser46.Password)
                .IsOnHomePage(AutoUser46);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Resources)
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract(Contract.Municipal)
                .SelectBusinessUnit(Contract.Municipal)
                .SelectShift("AM")
                .InsertDate(dateInFutre + Keys.Enter)
                .ClickGo()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(2000);
            //Create driver
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectBusinessUnit(BusinessUnit.EastCollections)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Verify popup
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .SelectResourceState("SICK")
                .IsReasonPopupDisplayed()
                .VerifyConfirmButtonEnabled(false)
                .CloseReasonPopup()
                .ClickAllocatedResource(resourceName)
                .SelectResourceState("TRAINING")
                .IsReasonPopupDisplayed()
                .VerifyConfirmButtonEnabled(false)
                .SelectReason(ResourceReason.Paid)
                .VerifyConfirmButtonEnabled(true);
        }
    }
}
