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

namespace si_automated_tests.Source.Test.AggrementLineTest
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class EditAgreementLineTest : BaseTest
    {
        [Category("EditAgreement")]
        [Test]
        public void TC_016()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(27)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .IsOnPartyAgreementPage()
                .ClickEditAgreementBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .ClickOnEditAsset()
                .EditAssetQuantity(3)
                .ClickOnTenureText()
                .EditAssertClickDoneBtn()
                .VerifySummaryOfStep("3 x 1100L(Rental), 100kg Paper & Cardboard")
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
                .RemoveAllRedundantPrice()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .IsOnInvoiceDetailsTab()
                .ClickFinish()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();

            //Step 18 Go to task tab to verify editition
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn();
            PageFactoryManager.Get<TaskTab>()
                .ClickTaskTabBtn()
                .VerifyTwoNewTaskAppear();

            //Verity 1 Task Line on the Task 1 created
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .GoToFirstTask()
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
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .ClickCloseWithoutSaving()
                .SwitchToChildWindow(2);

            //Verity 1 Task Line on the Task 2 created
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .GoToSecondTask()
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
                .VerifyToastMessage("Success")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTaskDetailsPage>()
                .ClickCloseWithoutSaving()
                .SwitchToChildWindow(2);

            //Verify date in expand is tomorrow 
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1).Replace('-', '/');
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickOnDetailsTab()
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyAssetAndProductAssetTypeStartDate(tommorowDate)
                .VerifyRegularAssetTypeStartDate(tommorowDate)
                .VerifyTaskLineTypeStartDates(tommorowDate)
                .CloseWithoutSaving()
                .SwitchToChildWindow(1);

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Site Services")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SiteServicesCommonPage>()
                .FilterAgreementId(27)
                .VerifyFirstLineAgreementResult(54, 27)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<AgreementLinePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementLinePage>()
                .GoToAllTabAndConfirmNoError();
        }

        [Category("EditAgreement")]
        [Test]
        public void TC_017()
        {
            string date = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

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
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(64)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .IsOnPartyAgreementPage()
               .SelectAgreementType("Commercial Collections")
               .ClickSaveBtn();
            PageFactoryManager.Get<BasePage>()
                .VerifyToastMessage("Successfully saved agreement");

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
                .ClickAssetType()
                .SelectAssetType("660L")
                .InputAssetQuantity(3)
                .ChooseTenure("Rental")
                .ChooseProduct("General Recycling")
                .ChooseEwcCode("150106")
                .InputProductQuantity(600)
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
               .RemoveAllRedundantPrice17()
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
               .VerifyToastMessage("Successfully saved agreement")
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
            //finish step 19
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .VerifyRetiredTask(3)//3x Mobilization tasks display in Italics and grey font
                .VerifyNewTaskAppearWithNum(3, "Completed", "Deliver Commercial Bin", date, todayDate);
            PageFactoryManager.Get<PartyAgreementPage>()
                .SwitchToFirstWindow();

            //Go to Services and verify 
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .OpenOption("Active Service Tasks")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .OpenTribleYarnsWithDate(todayDate)
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskLineStartDate(todayDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(todayDate)
                .verifyScheduleEndDate("01/01/2050");

            PageFactoryManager.Get<BasePage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(3);

            //step 25
            PageFactoryManager.Get<PartyAgreementPage>()
                .WaitForLoadingIconToDisappear();
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
                .RemoveAllRedundantPrice17()
                .ClickNext()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .IsOnInvoiceDetailsTab()
                .ClickFinish()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
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
                .VerifyAdhocInfo(allAdhoc, adhocListInput);

            //Go to task tab and verify 
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickTaskTabBtn()
               .WaitForLoadingIconToDisappear();

            List<IWebElement> availableRow = PageFactoryManager.Get<TaskTab>()
               .VerifyNewTaskAppearWithNum(2, "Unallocated", "Remove Commercial Bin", date, "");
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
                    .VerifyTaskLine1("Remove", "660L", "1", "General Recycling", "600", "Kilograms", "Unallocated")
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(3);
            }

            PageFactoryManager.Get<PartyAgreementPage>()
               .SwitchToFirstWindow();
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame();
            //Go to Services again to verify 
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .OpenTribleYarnsWithDate(todayDate)
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceTaskLineTab>()
                .verifyTaskLineStartDate(todayDate)
                .verifyTaskLineEndDate("01/01/2050");
        }

        [Category("EditAgreement")]
        [Test]
        public void TC_018()
        {
            string date = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);
            string tmrDatePlus7day = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 8);
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

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
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
                .ClickOnDetailsTab()
                .IsOnPartyAgreementPage()
                .ClickEditAgreementBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<EditAgreementServicePage>()
                .ClickOnNextBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AssetAndProducTab>()
                .ClickRemoveAsset()
                .ClickAddAsset()
                .ClickAssetType()
                .SelectAssetType("1100L")
                .InputAssetQuantity(2)
                .ChooseTenure("Rental")
                .ChooseProduct("Paper & Cardboard")
                .ChooseEwcCode("150101")
                .InputProductQuantity(100)
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
               .RemoveAllRedundantPrices(4)
               .ClickNext()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<InvoiceDetailTab>()
               .IsOnInvoiceDetailsTab()
               .ClickFinish()
               .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickSaveBtn()
               .VerifyToastMessage("Successfully saved agreement")
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
                .VerifyAdhocInfo(allAdhoc, adhocListInput);

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
                    .VerifyTaskLine1("Remove", "660L", "1", "General Refuse", "60", "Kilograms", "Unallocated")
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
                    .SwitchToChildWindow(2);
            }
            PageFactoryManager.Get<PartyAgreementPage>()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .OpenOption("Site Services")
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
               .ClickMainOption("Services")
               .ExpandOption("Regions")
               .ExpandOption("London")
               .ExpandOption("North Star Commercial")
               .ExpandOption("Collections")
               .ExpandOption("Commercial Collections")
               .OpenOption("Active Service Tasks")
               .SwitchNewIFrame();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .OpenSidraTeddingtonStartDate(tommorowDate)
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<ServicesTaskPage>()
            //    .ClickOnTaskLineTab();
            //PageFactoryManager.Get<ServiceTaskLineTab>()
            //    .WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<ServiceTaskLineTab>()
            //    .verifyTaskLineStartDate(tommorowDate);
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnScheduleTask();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServiceScheduleTab>()
                .verifyScheduleStartDate(tommorowDate)
                .SwitchToFirstWindow();
            //verify last step
            //PageFactoryManager.Get<CommonActiveServicesTaskPage>()
            //    .WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<CommonActiveServicesTaskPage>()
            //    .OpenSidraTeddingtonEndDate(tommorowDate)
            //    .SwitchToLastWindow();
            //PageFactoryManager.Get<ServicesTaskPage>()
            //    .WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<ServicesTaskPage>()
            //    .ClickOnTaskLineTab();
            //PageFactoryManager.Get<ServiceTaskLineTab>()
            //    .WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<ServiceTaskLineTab>()
            //    .verifyTaskLineEndDate(tommorowDate);
            //PageFactoryManager.Get<ServicesTaskPage>()
            //    .ClickOnScheduleTask();
            //PageFactoryManager.Get<ServiceScheduleTab>()
            //    .WaitForLoadingIconToDisappear();
            //PageFactoryManager.Get<ServiceScheduleTab>()
            //    .verifyScheduleEndDate(tommorowDate);
        }

        [Category("EditAgreement")]
        [Test]
        public void TC_019()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            AsserAndProductModel assetAndProductInput = new AsserAndProductModel("Mini (1.53m3)", "1", "Wood", "", "3", "Kilograms", "Owned", new string[1], new string[1], tommorowDate, "");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
                .ChooseTenure("Owned")
                .ChooseProduct("Wood")
                .InputProductQuantity(3)
                .ClickDoneBtn()
                .VerifySummaryOfStep("1 x Mini (1.53m3)(Owned), 3kg Wood")
                .ClickNext();
            PageFactoryManager.Get<PriceTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PriceTab>()
               .IsOnPriceTab()
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
               .VerifyToastMessage("Successfully saved agreement")
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
                .ClickMainOption("Parties")
                .OpenOption("Site Services")
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
        [Test]
        public void TC_020_A()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
               .VerifyToastMessage("Successfully saved agreement")
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
               .ClickMainOption("Services")
               .ExpandOption("Regions")
               .ExpandOption("London")
               .ExpandOption("North Star Commercial")
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
                .SwitchToFirstWindow()
                .SwitchNewIFrame();

            //open task with enddate is tommorow and verify 
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .OpenTaskWithPartyNameAndDate("La Plata Steakhouse", tommorowDate, "ENDDATE")
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
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
        }
    }
}
