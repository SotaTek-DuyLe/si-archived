using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties;
using System;
using System.Collections.Generic;
using System.Text;

using static si_automated_tests.Source.Main.Models.UserRegistry;
using si_automated_tests.Source.Main.Pages.Paties.SiteServices;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.NavigationPanel;

using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Task;
using System.Data.SqlClient;
using si_automated_tests.Source.Main.Models.DBModels;
using si_automated_tests.Source.Main.DBModels;

namespace si_automated_tests.Source.Test.AggrementLineTest
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class EditAgreementLineTest_022_029 : BaseTest
    {
        [Category("EditAgreement")]
        [Category("Huong")]
        [Test(Description = "022A_Create Agreement without Mobilization and Demobilization phases")]
        public void TC_022()
        {
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string partyName = "Rosie and Java";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(53)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .OpenAgreementTab();
            PageFactoryManager.Get<AgreementTab>()
                .IsOnAgreementTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .IsOnPartyAgreementPage()
               .SelectAgreementType("Commercial Collections")
               .ClickSaveBtn();
            PageFactoryManager.Get<BasePage>()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);

            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("New")
                .VerifyNewOptionsAvailable()
                .ClickAddService()
                .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
                 .IsOnSiteServiceTab()
                 .ChooseService("Commercial - Collection Only")
                 .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .ClickAssetType()
                .SelectAssetType("1100L")
                .InputAssetQuantity(2)
                .ChooseTenure("Owned")
                .ChooseProduct("General Refuse")
                .InputProductQuantity(500)
                .SelectKiloGramAsUnit()
                .ClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickOnWeeklyBtn()
               .UntickAnyDayOption()
               .SelectDayOfWeek("Mon")
               .ClickDoneRequirementBtn()
               .VerifyScheduleSummary("Once Every week on any Monday")
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .RemoveAllRedundantPrices()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitForLoadingIconToDisappear();
            // Finish Create Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(10000);
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyTaskLineTypeStartDates(todayDate)
                .VerifyAssetAndProductAssetTypeStartDate(todayDate)
                .VerifyRegularAssetTypeStartDate(todayDate)
                .VerifyCreateAdhocButtonsAreDisabled();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationPanelDisappear()
                .VerifyDeMobilizationPanelDisappear();
            //Approve Agreement 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickApproveAgreement()
                .ConfirmApproveBtn();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyTaskLineTypeStartDates(todayDate)
                .VerifyAssetAndProductAssetTypeStartDate(todayDate)
                .VerifyRegularAssetTypeStartDate(todayDate)
                .VerifyCreateAdhocButtonsAreEnabled()
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);

            PageFactoryManager.Get<AgreementTab>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            int AgreementId = PageFactoryManager.Get<AgreementTab>()
                .GetAgreementId();
            PageFactoryManager.Get<AgreementTab>()
                .VerifyFirstAgreementInfo("Rosie and Java", todayDate, "01/01/2050", "Commercial Collections", "Active")
                .OpenFirstAgreement()
                .SwitchToLastWindow();
            //Verify no new task appear 
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully("Commercial Collections", partyName);
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .VerifyNoNewTaskAppear()
                .SwitchToFirstWindow();

            //Go to services task and verify 
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(AgreementId)
                .OpenAgreementWithDate(todayDate)
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(AgreementId.ToString())
                .ClickDetailTab();
            PageFactoryManager.Get<DetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields()
                .VerifyTaskLineTypeStartDates(todayDate)
                .VerifyAssetAndProductAssetTypeStartDate(todayDate)
                .VerifyRegularAssetTypeStartDate(todayDate)
                .VerifyMobilizationPanelDisappear()
                .VerifyDeMobilizationPanelDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .GoToAllTabAndConfirmNoError()
                .SwitchToFirstWindow();

            //Go to Serivce and verify 
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .ExpandOption("Regions")
               .ExpandOption(Region.UK)
               .ExpandOption(Contract.Commercial)
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter("Rosie and Java")
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate("Rosie and Java", todayDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo("1100L", "2", "General Refuse", "Kilograms", todayDate, "01/01/2050");
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(todayDate);
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test(Description = "023A_Edit Agreement Line (Increase Asset Type Qty) on active Agreement without Mobilization and Demobilization phases")]
        public void TC_023()
        {
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string originDate = "08/03/2022";
            string agreementType = "COMMERCIAL COLLECTIONS";
            string partyName = "Rosie and Java";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(32)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName)
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNumBefore = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            //Edit Agreement 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickEditAgreementBtn()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .VerifySummaryOfStep("2 x 660L(Owned), 165 General Recycling")
                .ClickOnEditAsset()
                .EditAssetQuantity(3)
                .ClickOnTenureText()
                .EditAssertClickDoneBtn()
                .VerifySummaryOfStep("3 x 660L(Owned), 165 General Recycling")
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .ClickOnNotSetLink()
                .ClickOnWeeklyBtn()
                .ClickDoneRequirementBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .IsOnPriceTab()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .IsOnInvoiceDetailsTab()
                .ClickFinish()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyBlueBorder()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            // Finish Edit Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(10000);
            //Verify info in panel 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyTaskLineTypeStartDates(tommorowDate)
                .VerifyAssetAndProductAssetTypeStartDate(tommorowDate)
                .VerifyRegularAssetTypeStartDate(tommorowDate)
                .VerifyCreateAdhocButtonsAreEnabled();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationPanelDisappear()
                .VerifyDeMobilizationPanelDisappear()
                .SwitchToFirstWindow();

            //Back To Party and verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(53)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<AgreementTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTab>()
                .VerifyAgreementAppear("32", "Rosie and Java", originDate, "01/01/2050", "Active")
                .OpenAgreementWithId(32)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName)
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNum = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskNumUnchange(taskNum, taskNumBefore)
                .SwitchToFirstWindow();

            //Verify in site service 
            AsserAndProductModel assetAndProductInput = new AsserAndProductModel("660L", "3", "General Recycling", "", "165", "", "Owned", new string[1], new string[1], tommorowDate, "01/01/2050");
            RegularModel regularInput = new RegularModel("Service", "660L", "3", "General Recycling", "165", "", tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", "660L", "3", "General Recycling", "165", "", tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(32)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess("32")
                .ClickDetailTab();
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields()
                .VerifyAssetAndProductAssetTypeStartDate(tommorowDate)
                .VerifyRegularAssetTypeStartDate(tommorowDate)
                .VerifyMobilizationPanelDisappear()
                .VerifyDeMobilizationPanelDisappear()
                ;
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();

            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel, assetAndProductInput);

            //Regular
            RegularModel regularModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel, regularInput);

            //Ad-hoc
            List<MobilizationModel> allAdhoc = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();

            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc, adhocListInput);
            PageFactoryManager.Get<DetailTab>()
                .SwitchToFirstWindow()
                .WaitForLoadingIconToDisappear();

            //Go to service and verify 
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .ExpandOption("Regions")
               .ExpandOption(Region.UK)
               .ExpandOption(Contract.Commercial)
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter("Rosie and Java")
                .ClickApplyBtn();

            //Get service task id
            string serviceTaskId = PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .GetTaskId(partyName, tommorowDate);
            string serviceTaskIdRetired = PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .GetTaskId(partyName, originDate);

            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .OpenTaskWithPartyNameAndDate("Rosie and Java", tommorowDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo("660L", "3", "General Recycling", "", tommorowDate, "01/01/2050");
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .verifyScheduleEndDate("01/01/2050");

            //VERIFY IN DB

            string serviceTaskQuery = SQLConstants.SQL_ServiceTask + serviceTaskId;
            SqlCommand commandServiceTask = new SqlCommand(serviceTaskQuery, DbContext.Connection);
            SqlDataReader readerServiceTask = commandServiceTask.ExecuteReader();
            List<ServiceTaskLineDBModel> serviceTasks = ObjectExtention.DataReaderMapToList<ServiceTaskLineDBModel>(readerServiceTask);
            readerServiceTask.Close();

            string serviceTaskQueryRetired = SQLConstants.SQL_ServiceTask + serviceTaskIdRetired;
            SqlCommand commandServiceTaskRetired = new SqlCommand(serviceTaskQueryRetired, DbContext.Connection);
            SqlDataReader readerServiceTaskRetired = commandServiceTaskRetired.ExecuteReader();
            List<ServiceTaskLineDBModel> serviceTasksRetired = ObjectExtention.DataReaderMapToList<ServiceTaskLineDBModel>(readerServiceTaskRetired);
            readerServiceTask.Close();

            PageFactoryManager.Get<ServicesTaskPage>()
                .VerifyServiceTaskInDB(serviceTasks, 3, "660L", 165, null, "General Recycling", tommorowDate, "01/01/2050")
                .VerifyServiceTaskInDB(serviceTasksRetired, 2, "660L", 165, null, "General Recycling", originDate, tommorowDate);


        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test(Description = "Edit Agreement Line (Decrease Asset Type Qty) on active Agreement without Mobilization and Demobilization phases")]
        public void TC_024()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string originDate = "08/03/2022";
            string agreementType = "COMMERCIAL COLLECTIONS";
            string agreementName = "The White Cross ";

            AsserAndProductModel assetAndProductInput = new AsserAndProductModel("660L", "1", "General Refuse", "", "120", "", "Owned", new string[1], new string[1], tommorowDate, "01/01/2050");
            RegularModel regularInput = new RegularModel("Service", "660L", "1", "General Refuse", "120", "", tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", "660L", "1", "General Refuse", "120", "", tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };


            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(33)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, agreementName);
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNumBefore = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            //Edit Agreement 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickEditAgreementBtn()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .VerifySummaryOfStep("3 x 660L(Owned), 120 General Refuse")
                .ClickOnEditAsset()
                .EditAssetQuantity(1)
                .ClickOnTenureText()
                .EditAssertClickDoneBtn()
                .VerifySummaryOfStep("1 x 660L(Owned), 120 General Refuse")
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .ClickOnNotSetLink()
                .ClickOnWeeklyBtn()
                .ClickDoneRequirementBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .IsOnPriceTab()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .IsOnInvoiceDetailsTab()
                .ClickFinish()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyBlueBorder()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            // Finish Edit Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(10000);
            //Verify info in panel 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyTaskLineTypeStartDates(tommorowDate)
                .VerifyAssetAndProductAssetTypeStartDate(tommorowDate)
                .VerifyRegularAssetTypeStartDate(tommorowDate)
                .VerifyCreateAdhocButtonsAreEnabled();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationPanelDisappear()
                .VerifyDeMobilizationPanelDisappear();
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel, assetAndProductInput);

            //Regular
            RegularModel regularModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel, regularInput);

            //Ad-hoc
            List<MobilizationModel> allAdhoc = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc, adhocListInput);
            PageFactoryManager.Get<DetailTab>()
                .SwitchToFirstWindow();
            //Back To Party and verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(54)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(agreementName);
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<AgreementTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTab>()
                .VerifyAgreementAppear("33", "The White Cross", originDate, "01/01/2050", "Active")
                .OpenAgreementWithId(33)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, agreementName);
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNum = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskNumUnchange(taskNum, taskNumBefore)
                .SwitchToFirstWindow();

            //Verify in site service 
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(33)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess("33")
                .ClickDetailTab();
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields()
                .VerifyAssetAndProductAssetTypeStartDate(tommorowDate)
                .VerifyRegularAssetTypeStartDate(tommorowDate)
                .VerifyMobilizationPanelDisappear()
                .VerifyDeMobilizationPanelDisappear()
                ;
            //Assert and Product
            AsserAndProductModel asserAndProductModel1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel1, assetAndProductInput);

            //Regular
            RegularModel regularModel1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel1, regularInput);

            //Ad-hoc
            List<MobilizationModel> allAdhoc1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc1, adhocListInput);
            PageFactoryManager.Get<DetailTab>()
                .SwitchToFirstWindow()
                .WaitForLoadingIconToDisappear();

            //Go to service and verify 
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .ExpandOption("Regions")
               .ExpandOption(Region.UK)
               .ExpandOption(Contract.Commercial)
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter("The White Cross")
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate("The White Cross", tommorowDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo("660L", "1", "General Refuse", "", tommorowDate, "01/01/2050");
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .verifyScheduleEndDate("01/01/2050");
            //BUGS FOR VERIFY IN DB
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test(Description = "Edit Agreement Line (Remove Asset Type and Add new Asset Type) on active Agreement without Mobilization and Demobilization phases")]
        public void TC_025()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

            int agreementId = 31;
            string partyName = "Danieli On The Green";
            string agreementType = "COMMERCIAL COLLECTIONS";

            string assetType = AgreementConstants.ASSET_TYPE_660L;
            int assetQty = 2;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string tenure = AgreementConstants.TENURE_OWNED;
            int productQty = 600;
            string defautEndDate = AgreementConstants.DEFAULT_END_DATE;
            string unit = AgreementConstants.KILOGRAMS;

            AsserAndProductModel assetAndProductInput = new AsserAndProductModel(assetType, assetQty.ToString(), product, "", productQty.ToString(), unit, tenure, new string[1], new string[1], tommorowDate, defautEndDate);
            RegularModel regularInput = new RegularModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName);
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickTaskTabBtn()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNumBefore = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            //Edit Agreement 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickEditAgreementBtn()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickRemoveAsset()
                .ClickAddAsset()
                .SelectAssetType(assetType)
                .InputAssetQuantity(assetQty)
                .ChooseTenure(tenure)
                .ChooseProduct(product)
                .InputProductQuantity(productQty)
                .SelectKiloGramAsUnit()
                .ClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickOnWeeklyBtn()
               .ClickDoneRequirementBtn()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .RemoveAllRedundantPrices()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage(AgreementConstants.SUCCESSFULLY_SAVED_AGREEMENT)
               .WaitForLoadingIconToDisappear();
            // Finish Edit Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .SleepTimeInMiliseconds(10000);

            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine();
            //Verify at Details Tab
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields()
                .VerifyMobilizationPanelDisappear()
                .VerifyDeMobilizationPanelDisappear();
            //Assert and Product
            AsserAndProductModel asserAndProductModel1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel1, assetAndProductInput);
            //Regular
            RegularModel regularModel1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel1, regularInput);
            //Ad-hoc
            List<MobilizationModel> allAdhoc1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc1, adhocListInput);

            //Verify no new task appear at Task tab
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNum = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskNumUnchange(taskNum, taskNumBefore)
                .SwitchToFirstWindow();

            //Verify on Site Service
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(agreementId.ToString())
                .ClickDetailTab();
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields()
                .VerifyMobilizationPanelDisappear()
                .VerifyDeMobilizationPanelDisappear();
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel, assetAndProductInput);
            //Regular
            RegularModel regularModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel, regularInput);
            //Ad-hoc
            List<MobilizationModel> allAdhoc = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
               .VerifyAdhocInfo(allAdhoc1, adhocListInput);
            PageFactoryManager.Get<AgreementLinePage>()
                .GoToAllTabAndConfirmNoError()
                .SwitchToFirstWindow();

            //Go to active service Task and verify 
            PageFactoryManager.Get<NavigationBase>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .ExpandOption("Regions")
               .ExpandOption(Region.UK)
               .ExpandOption(Contract.Commercial)
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter(partyName)
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate(partyName, tommorowDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo(assetType, assetQty.ToString(), product, unit, tommorowDate, defautEndDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .verifyScheduleEndDate(defautEndDate);
            //BUGS FOR VERIFY IN DB
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_026_A()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string futureDate = CommonUtil.GetLocalTimeMinusMonth("dd/MM/yyyy", 6); //current date plus 6 months
            string futureDueDate = CommonUtil.GetLocalTimeFromDate(futureDate, "dd/MM/yyyy", 7);

            int partyId = 55;
            string partyName = "Kiss the Hippo Coffee";

            string assetType = AgreementConstants.ASSET_TYPE_660L;
            int assetQty = 1;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string tenure = AgreementConstants.TENURE_RENTAL;
            int productQty = 650;
            string defautEndDate = AgreementConstants.DEFAULT_END_DATE;
            string unit = AgreementConstants.KILOGRAMS;

            AsserAndProductModel assetAndProductInput = new AsserAndProductModel(assetType, assetQty.ToString(), product, "", productQty.ToString(), unit, tenure, new string[1], new string[1], tommorowDate, defautEndDate);
            RegularModel regularInput = new RegularModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            //Create new Agreement
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<AgreementTab>()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<AgreementTab>()
                .IsOnAgreementTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .IsOnPartyAgreementPage()
               .SelectAgreementType("Commercial Collections")
               .EnterStartDate(futureDate)
               .ClickSaveBtn();
            PageFactoryManager.Get<BasePage>()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            //Finish created new agreement
            PageFactoryManager.Get<PartyAgreementPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("New")
                .VerifyNewOptionsAvailable()
                .ClickAddService()
                .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
                 .IsOnSiteServiceTab()
                 .ChooseService("Commercial")
                 .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .SelectAssetType(assetType)
                .InputAssetQuantity(assetQty)
                .ChooseTenure(tenure)
                .VerifyDeliveryDate(futureDueDate)
                .ChooseProduct(product)
                .ChooseEwcCode("150106")
                .InputProductQuantity(productQty)
                .SelectKiloGramAsUnit()
                .ClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickOnWeeklyBtn()
               .ClickDoneRequirementBtn()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab();
            PageFactoryManager.Get<PriceTab>()
               .RemoveAllRedundantPrices()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage(AgreementConstants.SUCCESSFULLY_SAVED_AGREEMENT)
               .WaitForLoadingIconToDisappear();
            //Approve Agreement
            PageFactoryManager.Get<PartyAgreementPage>()
                .SleepTimeInMiliseconds(10000);
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickApproveAgreement()
                .ConfirmApproveBtn();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .SleepTimeInMiliseconds(5000);
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatusWithText("Approved")
                .VerifyAgreementStatusInRedBackground();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();

            List<IWebElement> allTasks = PageFactoryManager.Get<TaskTab>()
               .VerifyNewDeliverCommercialBin(futureDueDate, 1);
            for (int i = 0; i < allTasks.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(allTasks[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .InputActuaAssetQuantity(1)
                    .ClickOnAcualAssetQuantityText()
                    .SelectCompletedState()
                    .ClickOnAcualAssetQuantityText()
                    .CLickOnSaveBtn()
                    .VerifyToastMessage("Success")
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab()
                    .ClickStateDetais()
                    .ChooseTaskState("Completed")
                    .CLickOnSaveBtn()
                    .VerifyToastMessage("Success");
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(3);
            }
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_026_BC()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string futureDate = CommonUtil.GetLocalTimeMinusMonth("dd/MM/yyyy", 6); //current date plus 6 months
            string futureDueDate = CommonUtil.GetLocalTimeFromDate(futureDate, "dd/MM/yyyy", 7);

            int partyId = 55;
            string partyName = "Kiss the Hippo Coffee";
            string agreementType = "Commercial Collections";

            string assetType = AgreementConstants.ASSET_TYPE_660L;
            int assetQty1 = 1;
            int assetQty = 3;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string tenure = AgreementConstants.TENURE_RENTAL;
            int productQty = 650;
            string defautEndDate = AgreementConstants.DEFAULT_END_DATE;
            string unit = AgreementConstants.KILOGRAMS;

            AsserAndProductModel assetAndProductInput = new AsserAndProductModel(assetType, assetQty.ToString(), product, "", productQty.ToString(), unit, tenure, new string[1], new string[1], futureDate, defautEndDate);
            RegularModel regularInput = new RegularModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, futureDate);
            MobilizationModel adhoc = new MobilizationModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, futureDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
              .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .ExpandOption("Regions")
               .ExpandOption(Region.UK)
               .ExpandOption(Contract.Commercial)
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            //Verify at Active Service Task
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter(partyName)
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate(partyName, futureDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo(assetType, assetQty1.ToString(), product, unit, futureDate, defautEndDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(futureDate)
                .verifyScheduleEndDate(defautEndDate)
                .CloseCurrentWindow()
                .SwitchToFirstWindow();

            //Go to Agreement again and edit it 
            PageFactoryManager.Get<NavigationBase>()
              .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Parties)
               .ExpandOption(Contract.Commercial)
               .OpenOption(MainOption.Parties)
               .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            //Open new Agreement

            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            int agreementId = PageFactoryManager.Get<AgreementTab>()
                .GetAgreementId();
            PageFactoryManager.Get<AgreementTab>()
                .OpenFirstAgreement()
                .SwitchToLastWindow();

            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName);
            //Edit Agreement
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickEditAgreementBtn()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickOnEditAsset()
                .EditAssetQuantity(3)
                .ClickOnTenureText()
                .EditAssertClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickOnWeeklyBtn()
               .ClickDoneRequirementBtn()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .RemoveAllRedundantPrices()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage(AgreementConstants.SUCCESSFULLY_SAVED_AGREEMENT)
               .WaitForLoadingIconToDisappear();
            // Finish Edit Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(20000); //waiting for new task are genarated
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            List<IWebElement> allTasks = PageFactoryManager.Get<TaskTab>()
               .VerifyNewTaskAppearWithNum(2, "Unallocated", "Deliver Commercial Bin", futureDueDate, "");
            for (int i = 0; i < allTasks.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(allTasks[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .InputActuaAssetQuantity(1)
                    .ClickOnAcualAssetQuantityText()
                    .SelectCompletedState()
                    .ClickOnAcualAssetQuantityText()
                    .CLickOnSaveBtn()
                    .VerifyToastMessage("Success")
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab()
                    .ClickStateDetais()
                    .ChooseTaskState("Completed")
                    .CLickOnSaveBtn()
                    .VerifyToastMessage("Success");
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(3);
            }

            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine();
            //Verify at Details Tab
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields();
            //Assert and Product
            AsserAndProductModel asserAndProductModel1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel1, assetAndProductInput);
            //Regular
            RegularModel regularModel1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel1, regularInput);
            //Ad-hoc
            List<MobilizationModel> allAdhoc1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();

            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc1, adhoc, 3);
            PageFactoryManager.Get<DetailTab>()
                .SwitchToFirstWindow();

            //Go to Site Service and Verify 
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(agreementId.ToString())
                .GoToAllTabAndConfirmNoError()
                .ClickDetailTab();
            PageFactoryManager.Get<DetailTab>()
                .WaitForLoadingIconToDisappear();

            //Verify at Details Tab
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields();
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel, assetAndProductInput);
            //Regular
            RegularModel regularModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel, regularInput);
            //Ad-hoc
            List<MobilizationModel> allAdhoc = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc, adhoc, 3);
            PageFactoryManager.Get<DetailTab>()
                .SwitchToFirstWindow();

            //Verify Again At Active Service Task 
            PageFactoryManager.Get<NavigationBase>()
             .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            //Verify at Active Service Task
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter(partyName)
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate(partyName, futureDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo(assetType, assetQty.ToString(), product, unit, futureDate, defautEndDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(futureDate)
                .verifyScheduleEndDate(defautEndDate);
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_027_A()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string futureDate = CommonUtil.GetLocalTimeMinusMonth("dd/MM/yyyy", 7); //current date plus 7 months
            string futureDueDate = CommonUtil.GetLocalTimeFromDate(futureDate, "dd/MM/yyyy", 7);

            int partyId = 64;
            string partyName = "Tribe Yarns";

            string assetType = AgreementConstants.ASSET_TYPE_660L;
            int assetQty = 4;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string tenure = AgreementConstants.TENURE_RENTAL;
            int productQty = 650;
            string defautEndDate = AgreementConstants.DEFAULT_END_DATE;
            string unit = AgreementConstants.KILOGRAMS;

            AsserAndProductModel assetAndProductInput = new AsserAndProductModel(assetType, assetQty.ToString(), product, "", productQty.ToString(), unit, tenure, new string[1], new string[1], tommorowDate, defautEndDate);
            RegularModel regularInput = new RegularModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            //Create new Agreement
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTab>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .IsOnPartyAgreementPage()
               .SelectAgreementType("Commercial Collections")
               .EnterStartDate(futureDate)
               .ClickSaveBtn();
            PageFactoryManager.Get<BasePage>()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            //Finish created new agreement
            PageFactoryManager.Get<PartyAgreementPage>()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("New")
                .VerifyNewOptionsAvailable()
                .ClickAddService()
                .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
                 .IsOnSiteServiceTab()
                 .ChooseService("Commercial")
                 .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .SelectAssetType(assetType)
                .InputAssetQuantity(assetQty)
                .ChooseTenure(tenure)
                .VerifyDeliveryDate(futureDueDate)
                .ChooseProduct(product)
                .ChooseEwcCode("150106")
                .InputProductQuantity(productQty)
                .SelectKiloGramAsUnit()
                .ClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickOnWeeklyBtn()
               .ClickDoneRequirementBtn()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab();
            PageFactoryManager.Get<PriceTab>()
               .RemoveAllRedundantPrices()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage(AgreementConstants.SUCCESSFULLY_SAVED_AGREEMENT)
               .WaitForLoadingIconToDisappear();
            //Waiting for saved successfully 
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .SleepTimeInMiliseconds(10000);
            //Approve Agreement 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickApproveAgreement()
                .ConfirmApproveBtn();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .SleepTimeInMiliseconds(5000);
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatusWithText("Approved")
                .VerifyAgreementStatusInRedBackground()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            List<IWebElement> allTasks = PageFactoryManager.Get<TaskTab>()
               .VerifyNewDeliverCommercialBin(futureDueDate, 4);
            for (int i = 0; i < allTasks.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(allTasks[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .InputActuaAssetQuantity(1)
                    .ClickOnAcualAssetQuantityText()
                    .SelectCompletedState()
                    .ClickOnAcualAssetQuantityText()
                    .CLickOnSaveBtn()
                    .VerifyToastMessage("Success")
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab()
                    .ClickStateDetais()
                    .ChooseTaskState("Completed")
                    .CLickOnSaveBtn()
                    .VerifyToastMessage("Success");
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(3);
            }
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_027_BC()
        {
            string futureDate = CommonUtil.GetLocalTimeMinusMonth("dd/MM/yyyy", 7); //current date plus 7 months
            string futureDueDate = CommonUtil.GetLocalTimeFromDate(futureDate, "dd/MM/yyyy", 7);

            int partyId = 64;
            string partyName = "Tribe Yarns";
            string agreementType = "Commercial Collections";

            string assetType = AgreementConstants.ASSET_TYPE_660L;
            int assetQty1 = 4;
            int assetQty = 1;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string tenure = AgreementConstants.TENURE_RENTAL;
            int productQty = 650;
            string defautEndDate = AgreementConstants.DEFAULT_END_DATE;
            string unit = AgreementConstants.KILOGRAMS;

            AsserAndProductModel assetAndProductInput = new AsserAndProductModel(assetType, assetQty.ToString(), product, "", productQty.ToString(), unit, tenure, new string[1], new string[1], futureDate, defautEndDate);
            RegularModel regularInput = new RegularModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, futureDate);
            MobilizationModel adhoc = new MobilizationModel("Service", assetType, assetQty.ToString(), product, productQty.ToString(), unit, futureDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
              .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .ExpandOption("Regions")
               .ExpandOption(Region.UK)
               .ExpandOption(Contract.Commercial)
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            //Verify at Active Service Task
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter(partyName)
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate(partyName, futureDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo(assetType, assetQty1.ToString(), product, unit, futureDate, defautEndDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(futureDate)
                .verifyScheduleEndDate(defautEndDate)
                .CloseCurrentWindow()
                .SwitchToFirstWindow();

            //Go to Agreement again and edit it 
            PageFactoryManager.Get<NavigationBase>()
              .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Parties)
               .ExpandOption(Contract.Commercial)
               .OpenOption(MainOption.Parties)
               .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            //Open new Agreement
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName);
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
            int agreementId = PageFactoryManager.Get<AgreementTab>()
                .GetAgreementId();
            PageFactoryManager.Get<AgreementTab>()
                .OpenFirstAgreement()
                .SwitchToLastWindow();

            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName);
            //Edit Agreement
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickEditAgreementBtn()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickOnEditAsset()
                .EditAssetQuantity(1)
                .ClickOnTenureText()
                .EditAssertClickDoneBtn()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickOnWeeklyBtn()
               .ClickDoneRequirementBtn()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .RemoveAllRedundantPrices()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage(AgreementConstants.SUCCESSFULLY_SAVED_AGREEMENT)
               .WaitForLoadingIconToDisappear();
            // Finish Edit Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(20000); //waiting for new task are genarated
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            List<IWebElement> allTasks = PageFactoryManager.Get<TaskTab>()
              .VerifyNewTaskAppearWithNum(3, "Unallocated", "Remove Commercial Bin", futureDueDate, "");

            for (int i = 0; i < allTasks.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(allTasks[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .VerifyTaskLine("Remove", assetType, assetQty.ToString(), product, productQty.ToString(), unit, "Unallocated");
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(3);
            }

            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine();
            //Verify at Details Tab
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields();
            //Assert and Product
            AsserAndProductModel asserAndProductModel1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel1, assetAndProductInput);
            //Regular
            RegularModel regularModel1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel1, regularInput);
            //Ad-hoc
            List<MobilizationModel> allAdhoc1 = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
               .VerifyAdhocInfo(allAdhoc1, adhoc, 3);
            PageFactoryManager.Get<DetailTab>()
                .SwitchToFirstWindow();

            //Go to Site Service and Verify 
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(agreementId.ToString())
                .GoToAllTabAndConfirmNoError()
                .ClickDetailTab();
            PageFactoryManager.Get<DetailTab>()
                .WaitForLoadingIconToDisappear();

            //Verify at Details Tab
            PageFactoryManager.Get<DetailTab>()
                .ExpandAllAgreementFields();
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel, assetAndProductInput);
            //Regular
            RegularModel regularModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel, regularInput);
            //Ad-hoc
            List<MobilizationModel> allAdhoc = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc, adhoc, 3);
            PageFactoryManager.Get<DetailTab>()
                .SwitchToFirstWindow();

            //Verify Again At Active Service Task 
            PageFactoryManager.Get<NavigationBase>()
             .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            //Verify at Active Service Task
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter(partyName)
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate(partyName, futureDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo(assetType, assetQty.ToString(), product, unit, futureDate, defautEndDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(futureDate)
                .verifyScheduleEndDate(defautEndDate);
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_029A()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

            int agreementId = 43;
            string agreementType = "COMMERCIAL COLLECTIONS";
            string agreementName = "Whitton Baptist Church";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, agreementName);
            //Retire service 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementLineNum(2)
                .ClickRetireButton()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyRetirePopup()
                .RetirePopupClickOK()
                .VerifyToastMessage("Successfully retired Agreement")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyEndDate(tommorowDate)
                .VerifyAgreementLineDisappear();
        }
        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_029B()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string partyName = "Whitton Baptist Church";

            string assetType = AgreementConstants.ASSET_TYPE_1100L;
            int assetQty = 1;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string unit = AgreementConstants.KILOGRAMS;

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
               .ClickMainOption(MainOption.Services)
               .ExpandOption("Regions")
               .ExpandOption(Region.UK)
               .ExpandOption(Contract.Commercial)
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            //Verify at Active Service Task
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter(partyName)
                .ClickApplyBtn()
                .WaitForLoadingIconToDisappear();
            List<IWebElement> allTasks = PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .VerifyTaskWithPartyNameAndDate(2, partyName, tommorowDate, "ENDDATE");
            for (int i = 0; i < allTasks.Count; i++)
            {
                PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                    .OpenATask(allTasks[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<ServicesTaskPage>()
                    .ClickOnTaskLineTab();
                PageFactoryManager.Get<ServiceTaskLineTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<ServiceTaskLineTab>()
                    .verifyTaskInfo(assetType, assetQty.ToString(), product, unit, tommorowDate);
                PageFactoryManager.Get<ServicesTaskPage>()
                    .ClickOnScheduleTask();
                PageFactoryManager.Get<ServiceScheduleTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<ServiceScheduleTab>()
                    .verifyScheduleEndDate(tommorowDate)
                    .SwitchToFirstWindow()
                    .SwitchNewIFrame();
            }
        }
        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_029C()
        {
            //Verify at task tab
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

            int agreementId = 43;
            string assetType = AgreementConstants.ASSET_TYPE_1100L;
            int assetQty = 1;
            string product = AgreementConstants.GENERAL_RECYCLING;
            string unit = AgreementConstants.KILOGRAMS;

            string agreementType = "COMMERCIAL COLLECTIONS";
            string agreementName = "Whitton Baptist Church";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, agreementName);
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            List<IWebElement> allTasks = PageFactoryManager.Get<TaskTab>()
              .VerifyNewTaskAppearWithNum(2, "Unallocated", "Remove Commercial Bin", tommorowDate, "");

            for (int i = 0; i < allTasks.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(allTasks[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                if(i == 0)
                {
                    PageFactoryManager.Get<AgreementTaskDetailsPage>()
                   .VerifyTaskLine("Remove", assetType, assetQty.ToString(), product, "50", unit, "Unallocated");
                }
                else
                {
                    PageFactoryManager.Get<AgreementTaskDetailsPage>()
                   .VerifyTaskLine("Remove", assetType, assetQty.ToString(), product, "60", unit, "Unallocated");
                }
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .CloseCurrentWindow()
                    .SwitchToChildWindow(2);
            }
        }
    }
}
