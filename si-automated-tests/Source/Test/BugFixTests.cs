using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.IE_Configuration;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Tasks;
using si_automated_tests.Source.Main.Pages.UserAndRole;
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
                .ExpandOption(Contract.RMC)
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
                .OpenOption(Contract.RM)
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
                .OpenOption(Contract.RMC)
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
                .OpenOption(Contract.RMC)
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
                .ClickOnAutoPrintTickedCheckbox()
                //Change [purchase order number required] - Before: UnTicked
                .ClickOnPurchaseOrderNumberRequiredCheckbox()
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
                .VerifyInfoInHistoryTab(CommonConstants.HistoryTitleAfterUpdateWBTicketTab, valueChangedExp, AutoUser46.DisplayName);
            //API to verify
            List<PartyActionDBModel> list= commonFinder.GetPartyActionByPartyIdAndUserId(partyId, userId);
            PartyActionDBModel partyActionDBModel = list[0];
            Assert.AreEqual(licenceNumber, partyActionDBModel.wb_licencenumber, "Licence number is incorrect");
            Assert.IsFalse(partyActionDBModel.wb_autoprint, "Auto-print is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_driverrequired, "Driver Name Required is incorrect");
            Assert.IsTrue(partyActionDBModel.wb_driverrequired, "Purchase Order Number Required is incorrect");
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
                .VerifyFirstResultValue("Name",resoName);
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
                .OpenOption(Contract.RMC)
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

    }
}
