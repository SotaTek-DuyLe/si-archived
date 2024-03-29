﻿using NUnit.Framework;
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
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Models.DBModels;
namespace si_automated_tests.Source.Test.AggrementLineTest
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class EditAgreementLineTest_016_021 : BaseTest
    {
        
        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_016_edit_agreement_line_increase_accest_type_qty()
        {
            int agreementId = 65;
            string agreementType = "COMMERCIAL COLLECTIONS";
            string agreementName = "DALEMEAD CARE HOME";
            string tomorrowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(agreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, agreementName)
                .ClickOnDetailsTab()
                .IsOnPartyAgreementPage()
                .ClickEditAgreementBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .VerifySummaryOfStep("1 x 1100L(Rental), 95kg General Recycling")
                .ClickOnEditAsset()
                .EditAssetQuantity(3)
                .ClickOnTenureText();
            String deliveryDate = PageFactoryManager.Get<AssetAndProducTab>()
                .GetDeliveryDate();
            PageFactoryManager.Get<AssetAndProducTab>()
            .EditAssertClickDoneBtn()
            .VerifySummaryOfStep("3 x 1100L(Rental), 95kg General Recycling")
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
                .ClosePriceRecords()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .IsOnInvoiceDetailsTab()
                .ClickFinish()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickSaveBtn()
                //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            //Fix wating time for saved agreement
            PageFactoryManager.Get<PartyAgreementPage>()
                .SleepTimeInMiliseconds(10000);

            //Step 18 Go to task tab to verify editition

            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            List<IWebElement> newTasks = PageFactoryManager.Get<TaskTab>()
                .VerifyNewDeliverCommercialBin(deliveryDate, 2);
            foreach (IWebElement task in newTasks)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(task)
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitingForTaskDetailsPageLoadedSuccessfully();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
               .ClickToTaskLinesTab()
               .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .VerifyTaskLine("Deliver", "1100L", "1", "General Recycling", "95", "Kilograms", "Unallocated")
                    .InputActuaAssetQuantity(1)
                    .ClickOnAcualAssetQuantityText()
                    .SelectCompletedState()
                    .ClickOnAcualAssetQuantityText()
                    .CLickOnSaveBtn()
                    //.VerifyToastMessage("Success")
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(2);
            }

            //Verify date in expand is tomorrow 
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyAssetAndProductAssetTypeStartDate(tomorrowDate)
                .VerifyRegularAssetTypeStartDate(tomorrowDate)
                .VerifyTaskLineTypeStartDates(tomorrowDate)
                .CloseCurrentWindow()
                .SwitchToChildWindow(1);

            //Back to Site Service and verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.SiteServices)
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(agreementId)
                .OpenAgreementBySiteID(118)
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess(agreementId.ToString())
                .GoToAllTabAndConfirmNoError();
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test(Description = "Edit Agreement Line (Decrease Asset Type Qty)  on active Agreement with Mobilization and Demobilization phases")]
        public void TC_017_edit_agreement_line_decrease_accest_type_qty()
        {
            string date = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string agreementId017;
            string partyName = "Tribe Yarns";
            string agreementType = "COMMERCIAL COLLECTIONS";

            AsserAndProductModel assetAndProductInput = new AsserAndProductModel("660L", "1", "General Recycling", "", "600", "Kilograms", "Rental", new string[1], new string[1], tommorowDate, "");
            MobilizationModel mobilizationInput = new MobilizationModel("Deliver", "660L", "1", "General Recycling", "600", "Kilograms", tommorowDate);
            RegularModel regularInput = new RegularModel("Service", "660L", "1", "General Recycling", "600", "Kilograms", tommorowDate);
            MobilizationModel deMobilizationInput = new MobilizationModel("Remove", "660L", "1", "General Recycling", "600", "Kilograms", tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", "660L", "1", "General Recycling", "600", "Kilograms", tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(64)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForLoadingIconToDisappear();
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
                .ClickSaveBtn()
                //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName);
            agreementId017 = PageFactoryManager.Get<PartyAgreementPage>()
                .GetAgreementId();
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
                .ClickAssetType()
                .SelectAssetType("660L")
                .InputAssetQuantity(3)
                .ChooseTenure("Rental")
                .ChooseProduct("General Recycling")
                .ChooseEwcCode("150106")
                .InputProductQuantity(600)
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
               .ClickDoneRequirementBtn()
               .VerifyScheduleOnceEveryDay()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .ClosePriceRecords()
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
               //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitForLoadingIconToDisappear();
            // Finish step 15 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(10000);
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<PartyAgreementPage>()
                //.VerifyAgreementStatusWithText("New")
                .ClickApproveAgreement()
                .ConfirmApproveBtn();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName)
                //.VerifyAgreementStatusWithText("Active")
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            List<IWebElement> allTasks = PageFactoryManager.Get<TaskTab>()
               .VerifyNewTaskAppearWithNum(3, "Unallocated", "Deliver Commercial Bin", date, "");
            for (int i = 0; i < allTasks.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(allTasks[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .WaitingForTaskDetailsPageLoadedSuccessfully();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .InputActuaAssetQuantity(1)
                    .ClickOnAcualAssetQuantityText()
                    .SelectCompletedState()
                    .ClickOnAcualAssetQuantityText()
                    .CLickOnSaveBtn()
                    //.VerifyToastMessage("Success")
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab()
                    .ClickStateDetais()
                    .ChooseTaskState("Completed")
                    .CLickOnSaveBtn()
                    .WaitForLoadingIconToDisappear();
                //.VerifyToastMessage("Success");
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(3);
            }
            //finish step 19
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .VerifyRetiredTask(3)//3x Mobilization tasks display in Italics and grey font
                .VerifyNewTaskAppearWithNum(3, "Completed", "Deliver Commercial Bin", date, todayDate);
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .SwitchToFirstWindow();

            //Go to Services and verify 
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
                .OpenTribleYarnsWithDate(tommorowDate)
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskLineStartDate(tommorowDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .verifyScheduleEndDate("01/01/2050");

            PageFactoryManager.Get<BasePage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(3);

            //step 25
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, partyName)
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
                .ClickOnEditAsset()
                .EditAssetQuantity(1)
                .ClickOnTenureText()
                .EditAssertClickDoneBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .ClickOnNotSetLink()
                .ClickDoneRequirementBtn()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
                .IsOnPriceTab()
                .ClosePriceRecords()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .IsOnInvoiceDetailsTab()
                .ClickFinish()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickSaveBtn()
                //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .SleepTimeInMiliseconds(10000);
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickOnDetailsTab()
               .ExpandAgreementLine()
               .ExpandAllAgreementFields()
               .WaitForLoadingIconToDisappear();
            //step 34 Verify that changes reflect on the existing Agreement and its objects
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProduct();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel, assetAndProductInput);
            //Mobilization
            MobilizationModel mobilizationModel = PageFactoryManager.Get<DetailTab>()
               .GetAllInfoMobilization();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationInfo(mobilizationModel, mobilizationInput);
            //Regular
            RegularModel regularModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel, regularInput);
            //De-Mobilization
            MobilizationModel deMobilizationModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoDeMobilization();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationInfo(deMobilizationModel, deMobilizationInput);
            //Ad-hoc
            List<MobilizationModel> allAdhoc = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc, adhoc, 3);

            //Go to task tab and verify 
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickTaskTabBtn()
               .WaitForLoadingIconToDisappear();

            List<IWebElement> availableRow = PageFactoryManager.Get<TaskTab>()
               .VerifyNewTaskAppearWithNum(2, "Unallocated", "Remove Commercial Bin", tommorowDate, "");
            for (int i = 0; i < availableRow.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(availableRow[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .VerifyTaskLine("Remove", "660L", "1", "General Recycling", "600", "Kilograms", "Unallocated")
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(3);
            }
            PageFactoryManager.Get<TaskTab>()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter(partyName)
                .ClickApplyBtn();
            string serviceTaskId = PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .GetTaskId(partyName, tommorowDate);

            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .OpenTaskWithPartyNameAndDate(partyName, tommorowDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskLineStartDate(tommorowDate)
                .verifyTaskLineEndDate("01/01/2050");

            string serviceTaskQuery = SQLConstants.SQL_ServiceTask + serviceTaskId;
            SqlCommand commandServiceTask = new SqlCommand(serviceTaskQuery, DbContext.Connection);
            SqlDataReader readerServiceTask = commandServiceTask.ExecuteReader();
            List<ServiceTaskLineDBModel> serviceTasks = ObjectExtention.DataReaderMapToList<ServiceTaskLineDBModel>(readerServiceTask);
            readerServiceTask.Close();

            string serviceUnitAssetQuery = SQLConstants.SQL_ServiceUnitAssets + agreementId017;
            SqlCommand commandServiceUnitAsset = new SqlCommand(serviceUnitAssetQuery, DbContext.Connection);
            SqlDataReader readerServiceUnitAsset = commandServiceUnitAsset.ExecuteReader();
            List<ServiceUnitAssetsDBModel> serviceUnitAsset = ObjectExtention.DataReaderMapToList<ServiceUnitAssetsDBModel>(readerServiceUnitAsset);
            readerServiceUnitAsset.Close();

            string serviceTaskAgreementQuery = SQLConstants.SQL_ServiceTaskForAgreement + agreementId017;
            SqlCommand commandServiceTaskAgreement = new SqlCommand(serviceTaskAgreementQuery, DbContext.Connection);
            SqlDataReader readerServiceTaskAgreement = commandServiceTaskAgreement.ExecuteReader();
            List<ServiceTaskForAgreementDBModel> serviceTaskAgreement = ObjectExtention.DataReaderMapToList<ServiceTaskForAgreementDBModel>(readerServiceTaskAgreement);
            readerServiceTaskAgreement.Close();

            PageFactoryManager.Get<ServicesTaskPage>()
                .VerifyServiceTaskInDB(serviceTasks, 1, "660L", 600, "Kilograms", "General Recycling", tommorowDate, "01/01/2050")
                //.VerifyServiceTaskInDB(serviceTasks, 2, "660L", 600, "Kilograms", "General Recycling", todayDate, tommorowDate)
                .VerifyServiceUnitAssets(serviceUnitAsset, 2, todayDate, tommorowDate) //Verify 2 retired task with enddate is tommorow
                .VerifyServiceTaskAgreementNum(serviceTaskAgreement, 1, tommorowDate); //Verify No new Service Task and Service Task Schedule created 

        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test(Description = "Edit Agreement Line (Remove Asset Type and Add new Asset Type) on active Agreement with Mobilization/Demobilization phases")]
        public void TC_018_edit_agreement_line_remove_and_add_asset_type()
        {
            string date = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);
            string tmrDatePlus7day = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 8);
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

            int agreementId = 7;
            string agreementSite = "COMMERCIAL COLLECTIONS";
            string agreementName = "SIDRA - TEDDINGTON";

            AsserAndProductModel assetAndProductInput = new AsserAndProductModel("1100L", "2", "Paper & Cardboard", "", "100", "Kilograms", "Rental", new string[1], new string[1], tommorowDate, "");
            MobilizationModel mobilizationInput = new MobilizationModel("Deliver", "1100L", "2", "Paper & Cardboard", "100", "Kilograms", tommorowDate);
            RegularModel regularInput = new RegularModel("Service", "1100L", "2", "Paper & Cardboard", "100", "Kilograms", tommorowDate);
            MobilizationModel deMobilizationInput = new MobilizationModel("Remove", "1100L", "2", "Paper & Cardboard", "100", "Kilograms", tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", "1100L", "2", "Paper & Cardboard", "100", "Kilograms", tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(7)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementSite, agreementName);
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .IsOnPartyAgreementPage()
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
                .ClickAssetType()
                .SelectAssetType("1100L")
                .InputAssetQuantity(2)
                .ChooseTenure("Rental")
                .ChooseProduct("Paper & Cardboard")
                .ChooseEwcCode("150101")
                .InputProductQuantity(100)
                .SelectKiloGramAsUnit()
                .ClickDoneBtn()
                .ClickNext();
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
               .ClosePriceRecords()
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitForLoadingIconToDisappear();
            //Finish step 15
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(10000);

            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("Active")
                .ExpandAgreementLine()
               .ExpandAllAgreementFields();
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProduct();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel, assetAndProductInput);
            //Mobilization
            MobilizationModel mobilizationModel = PageFactoryManager.Get<DetailTab>()
               .GetAllInfoMobilization();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationInfo(mobilizationModel, mobilizationInput);
            //Regular
            RegularModel regularModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularInfo(regularModel, regularInput);
            //De-Mobilization
            MobilizationModel deMobilizationModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoDeMobilization();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationInfo(deMobilizationModel, deMobilizationInput);
            //Ad-hoc
            List<MobilizationModel> allAdhoc = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhoc, adhoc, 3);

            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickTaskTabBtn()
               .WaitForLoadingIconToDisappear();
            List<IWebElement> demobilizationRows = new List<IWebElement>();
            demobilizationRows = PageFactoryManager.Get<TaskTab>()
               .VerifyNewRemovedCommercialBin(tommorowDate, 1);
            for (int i = 0; i < demobilizationRows.Count; i++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(demobilizationRows[i])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .VerifyTaskLine("Remove", "660L", "1", "General Refuse", "60", "Kilograms", "Unallocated")
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(2);
            }
            List<IWebElement> mobilizationRows = PageFactoryManager.Get<TaskTab>()
               .VerifyNewDeliverCommercialBin(tmrDatePlus7day, 2);
            for (int j = 0; j < mobilizationRows.Count; j++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(mobilizationRows[j])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .VerifyTaskLine("Deliver", "1100L", "1", "Paper & Cardboard", "100", "Kilograms", "Unallocated")
                    .InputActuaAssetQuantity(1)
                    .ClickOnAcualAssetQuantityText()
                    .SelectCompletedState()
                    .ClickOnAcualAssetQuantityText()
                    .CLickOnSaveBtn()
                    //.VerifyToastMessage("Success")
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab()
                    .ClickStateDetais()
                    .ChooseTaskState("Completed")
                    .CLickOnSaveBtn()
                    .WaitForLoadingIconToDisappear();
                //.VerifyToastMessage("Success");
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(2);
            }
            PageFactoryManager.Get<PartyAgreementPage>()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.SiteServices)
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(7)
                .VerifyFirstLineAgreementResult(10, 7)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess("7")
                .GoToAllTabAndConfirmNoError()
                .SwitchToFirstWindow();

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
                .InputPartyNameToFilter("Sidra - Teddington")
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate("Sidra - Teddington", tommorowDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskLineStartDate(tommorowDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            //verify last step
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .InputPartyNameToFilter("Sidra - Teddington")
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate("Sidra - Teddington", tommorowDate, "ENDDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskLineEndDate(tommorowDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleEndDate(tommorowDate);

            //Verify in DB
            string serviceUnitAssetQuery = SQLConstants.SQL_ServiceUnitAssets + agreementId;
            SqlCommand commandServiceUnitAsset = new SqlCommand(serviceUnitAssetQuery, DbContext.Connection);
            SqlDataReader readerServiceUnitAsset = commandServiceUnitAsset.ExecuteReader();
            List<ServiceUnitAssetsDBModel> serviceUnitAsset = ObjectExtention.DataReaderMapToList<ServiceUnitAssetsDBModel>(readerServiceUnitAsset);
            readerServiceUnitAsset.Close();

            PageFactoryManager.Get<ServicesTaskPage>()
                .VerifyServiceUnitAssets(serviceUnitAsset, 1, "660L", "General Refuse", "16/12/2021", tommorowDate) //Verify 1 retired task with enddate is tommorow
                .VerifyServiceUnitAssets(serviceUnitAsset, 2, "1100L", "Paper & Cardboard", tommorowDate, "01/01/2050"); //Verify 2 new task with start date is tommorow 
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_019_edit_agreement_add_agreement_line_()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            AsserAndProductModel assetAndProductInput = new AsserAndProductModel("Mini (1.53m3)", "1", "Wood", "", "3", "Kilograms", "Rental", new string[1], new string[1], tommorowDate, "");
            string agreementType = "COMMERCIAL COLLECTIONS";
            string agreementName = "LA PLATA STEAKHOUSE";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(28)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, agreementName);
            PageFactoryManager.Get<PartyAgreementPage>()
               .OpenTaskTab();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNumBefore = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickAddService();
            PageFactoryManager.Get<AddServicePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AddServicePage>()
                .IsOnAddServicePage();
            PageFactoryManager.Get<SiteAndServiceTab>()
                 .IsOnSiteServiceTab()
                 .SelectService("Skips")
                 .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .SelectAssetType("Mini (1.53m3)")
                .InputAssetQuantity(1)
                .ChooseTenure("Rental")
                .ChooseProduct("Wood")
                .InputProductQuantity(3)
                .SelectKiloGramAsUnit()
                .ClickDoneBtn()
                .VerifySummaryOfStep("1 x Mini (1.53m3)(Rental), 3kg Wood")
                .ClickNext();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
               .ClosePriceRecords()
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
               //.WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage)
               //.VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .SleepTimeInMiliseconds(10000);
            //Verify for Asset and product 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLineByService("Skips")
                .ExpandAllAgreementFields();
            //Assert and Product
            AsserAndProductModel asserAndProductModel = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProductAgreement();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModel, assetAndProductInput);
            PageFactoryManager.Get<PartyAgreementPage>()
               .OpenTaskTab();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNumAfter = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskNumUnchange(taskNumBefore, taskNumAfter)
                .SwitchToFirstWindow();
            //Go to site service and verify
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .OpenOption(MainOption.SiteServices)
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(28)
                .VerifyAgreementResultNum(2)
                .OpenAgreementWithDate(tommorowDate)
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForWindowLoadedSuccess("28")
                .GoToAllTabAndConfirmNoError();
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_020_edit_agreement_update_existing_schedule_on_existing_service()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string agreementType = "COMMERCIAL COLLECTIONS";
            string agreementName = "LA PLATA STEAKHOUSE";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(28)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForAgreementPageLoadedSuccessfully(agreementType, agreementName);
            //Edit Agreement 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifySchedule("Once per week on any weekday");
            //Assert and Product
            AsserAndProductModel asserAndProductModelBefore = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProduct();
            //Mobilization
            MobilizationModel mobilizationModelBefore = PageFactoryManager.Get<DetailTab>()
               .GetAllInfoMobilization();
            //Regular
            RegularModel regularModelBefore = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoRegular();
            //De-Mobilization
            MobilizationModel deMobilizationModelBefore = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoDeMobilization();
            //Ad-hoc
            List<MobilizationModel> allAdhocBefore = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();

            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickEditAgreementByAddressBtn("109 SHEEN LANE, EAST SHEEN, LONDON, SW14 8AE")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .IsOnEditAgreementServicePage()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
               .IsOnScheduleTab()
               .ClickOnSchedule("Once per week on any weekday")
               .TickAnyDayOption()
               .UntickAnyDayOption()
               .SelectDayOfWeek("Wed")
               .ClickDoneRequirementBtn()
               .VerifyScheduleSummary("Once Every week on any Wednesday")
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
               //.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
               .WaitForLoadingIconToDisappear();

            //waiting for save 
            PageFactoryManager.Get<PartyAgreementPage>()
                .SleepTimeInMiliseconds(10000);
            //Verify after editing
            PageFactoryManager.Get<PartyAgreementPage>()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifySchedule("Once per week on any Wednesday");
            //Assert and Product
            AsserAndProductModel asserAndProductModelAfter = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAssetAndProduct();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAssertAndProductInfo(asserAndProductModelBefore, asserAndProductModelAfter);
            //Mobilization
            MobilizationModel mobilizationModelAfter = PageFactoryManager.Get<DetailTab>()
               .GetAllInfoMobilization();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationInfo(mobilizationModelBefore, mobilizationModelAfter);
            //Regular
            PageFactoryManager.Get<DetailTab>()
                .VerifyRegularTaskTypeDate(tommorowDate + " - 01/01/2050")
                .VerifyRegularTaskLineTypeStartDate(tommorowDate);
            //De-Mobilization
            MobilizationModel deMobilizationModelAfter = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoDeMobilization();
            PageFactoryManager.Get<DetailTab>()
                .VerifyMobilizationInfo(deMobilizationModelBefore, deMobilizationModelAfter);
            //Ad-hoc
            List<MobilizationModel> allAdhocAfter = PageFactoryManager.Get<DetailTab>()
                .GetAllInfoAdhoc();
            PageFactoryManager.Get<DetailTab>()
                .VerifyAdhocInfo(allAdhocBefore, allAdhocAfter)
                .SwitchToFirstWindow();

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
                .InputPartyNameToFilter("La Plata Steakhouse")
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate("La Plata Steakhouse", tommorowDate, "STARTDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForTaskPageLoadedSuccessfully("Commercial Collection", "La Plata Steakhouse");
            
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo("1100L", "1", "Plastic", "Kilograms", tommorowDate, "01/01/2050");
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            //open task with enddate is tommorow and verify 
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
               .InputPartyNameToFilter("La Plata Steakhouse")
               .ClickApplyBtn()
               .OpenTaskWithPartyNameAndDate("La Plata Steakhouse", tommorowDate, "ENDDATE")
               .SwitchToLastWindow()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForTaskPageLoadedSuccessfully("Commercial Collection", "La Plata Steakhouse");

            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo("1100L", "1", "Plastic", "Kilograms", "15/02/2022", tommorowDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleEndDate(tommorowDate);
            //Verify in DB
            string serviceUnitAssetQuery = SQLConstants.SQL_ServiceUnitAssets + "28" + " and s.agreementlineID = 55";
            SqlCommand commandServiceUnitAsset = new SqlCommand(serviceUnitAssetQuery, DbContext.Connection);
            SqlDataReader readerServiceUnitAsset = commandServiceUnitAsset.ExecuteReader();
            List<ServiceUnitAssetsDBModel> serviceUnitAsset = ObjectExtention.DataReaderMapToList<ServiceUnitAssetsDBModel>(readerServiceUnitAsset);
            readerServiceUnitAsset.Close();

            PageFactoryManager.Get<ServicesTaskPage>()
                .VerifyServiceUnitAssetsNum(serviceUnitAsset, 1); //Verify no new service unit asset for this agreement
               
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_021_A_edit_agreement_remove_existing_service_remove_greement_line()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(29)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickTaskTabBtn()
               .WaitForLoadingIconToDisappear();

            //Verify DeliverCommercialBin task at TaskTab
            List<IWebElement> mobilizationRows = PageFactoryManager.Get<TaskTab>()
               .VerifyNewDeliverCommercialBin("08/01/2022", 1);
            //Edit Task 
            for (int j = 0; j < mobilizationRows.Count; j++)
            {
                PageFactoryManager.Get<TaskTab>()
                    .GoToATask(mobilizationRows[j])
                    .SwitchToLastWindow();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToTaskLinesTab()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                   .VerifyTaskLine("Deliver", "1100L", "1", "General Recycling", "0", "Kilograms", "Unallocated")
                   .InputActuaAssetQuantity(1)
                   .ClickOnAcualAssetQuantityText()
                   .SelectCompletedState()
                   .ClickOnAcualAssetQuantityText()
                   .CLickOnSaveBtn()
                   //.VerifyToastMessage("Success")
                   .WaitForLoadingIconToDisappear();

                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickToDetailsTab();
                PageFactoryManager.Get<TaskDetailTab>()
                    .ClickStateDetais()
                    .ChooseTaskState("Completed");
                PageFactoryManager.Get<BasePage>()
                    .ClickSaveBtn()
                    //.VerifyToastMessage("Success")
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                    .ClickCloseWithoutSaving()
                    .SwitchToFirstWindow();
            }

            PageFactoryManager.Get<PartyAgreementPage>()
                .SwitchToFirstWindow();

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
                .InputPartyNameToFilter("JAFLONG TANDOORI")
                .ClickApplyBtn()
                .OpenTaskWithPartyNameAndDate("Jaflong Tandoori", tommorowDate, "STARTDATE", 15)
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskInfo("1100L", "1", "General Recycling", "Kilograms", tommorowDate, "01/01/2050");
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .verifyScheduleEndDate("01/01/2050")
                .CloseCurrentWindow()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickOnDetailsTab()
               .WaitForLoadingIconToDisappear();

            //Remove Agreement Line 
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickRemoveAgreementBtn()
                .VerifyDotRedBorder()
                .ClickKeepAgreementBtn()
                .VerifyDotRedBorderDisappear()
                .ClickRemoveAgreementBtn()
                .ClickSaveBtn()
                .waitForLoadingIconDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementLineDisappear();
        }

        [Category("EditAgreement")]
        [Category("Huong")]
        [Test]
        public void TC_021_B_edit_agreement_remove_existing_service_remove_greement_line()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser33.UserName, AutoUser33.Password)
                .IsOnHomePage(AutoUser33);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.SiteServices)
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(29)
                .VerifyAgreementResultNum(1);
        }

    }
}
