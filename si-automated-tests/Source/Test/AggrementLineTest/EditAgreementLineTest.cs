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
    public class EditAgreementLineTest : BaseTest
    {
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
                .ClickParties()
                .ExpandNSC();
            PageFactoryManager.Get<PartyNavigation>()
                .ClickAgreementSubMenu()
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
                .ClickParties()
                .ExpandNSC();
            PageFactoryManager.Get<PartyNavigation>()
                .ClickPartySubMenu()
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

        [Test]
        public void TC_017()
        {
            string date = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
                PageFactoryManager.Get<NavigationBase>()
                .ClickParties()
                .ExpandNSC();
            PageFactoryManager.Get<PartyNavigation>()
                .ClickPartySubMenu()
                .SwitchNewIFrame();
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
               .IsOnScheduleTab()
               .ClickAddService()
               .ClickDoneScheduleBtn()
               .ClickOnNotSetLink()
               .ClickDoneRequirementBtn()
               .VerifyScheduleOnceEveryDay()
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
            
            //Go to Services
            PageFactoryManager.Get<HomePage>()
                .ClickServices()
                .GoToActiveServiceTask()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .OpenTribleYarnsWithDate(todayDate)
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab()
                .ClickOnScheduleTask()
                .CloseWithoutSaving()
                .SwitchToChildWindow(3);

            //step 27 
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
               .VerifyAssetAndProductAssetTypeStartDate(tommorowDate)
               .VerifyTaskLineTypeStartDates(tommorowDate)
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
                    .ClickCloseWithoutSaving()
                    .SwitchToChildWindow(3);
            }

            PageFactoryManager.Get<PartyAgreementPage>()
               .SwitchToFirstWindow();

            //Go to Services again to verify 
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonActiveServicesTaskPage>()
                .OpenTribleYarnsWithDate(todayDate)
                .SwitchToLastWindow();
            PageFactoryManager.Get<ServicesTaskPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ServicesTaskPage>()
                .ClickOnTaskLineTab()
                .ClickOnScheduleTask();
        }

        [Test]
        public void TC_018()
        {
            string date = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);
            string tmrDatePlus7day = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 8);
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
  
            AsserAndProductModel assetAndProductInput = new AsserAndProductModel("1100L", "2", "Paper & Cardboard", "", "100", "Kilograms", "Rental", new string [1], new string[1], tommorowDate, "");
            MobilizationModel mobilizationInput = new MobilizationModel ("Deliver", "1100L", "2", "Paper & Cardboard", "100", "Kilograms", tommorowDate);
            RegularModel regularInput = new RegularModel("Service", "1100L", "2", "Paper & Cardboard", "100", "Kilograms", tommorowDate);
            MobilizationModel deMobilizationInput = new MobilizationModel("Remove", "1100L", "2", "Paper & Cardboard", "100", "Kilograms", tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", "1100L", "2", "Paper & Cardboard", "100", "Kilograms", tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> {adhoc, adhoc};

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser14.UserName, AutoUser14.Password)
                .IsOnHomePage(AutoUser14);
            PageFactoryManager.Get<NavigationBase>()
                .ClickParties()
                .ExpandNSC();
            PageFactoryManager.Get<PartyNavigation>()
                .ClickAgreementSubMenu()
                .SwitchNewIFrame();
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
            PageFactoryManager.Get < BasePage>()
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
               .VerifyNewTaskAppearWithNum(1, "Unallocated", "Remove Commercial Bin", tommorowDate, "");
            List<IWebElement> mobilizationRows = PageFactoryManager.Get<TaskTab>()
               .VerifyNewTaskAppearWithNum(2, "Unallocated", "Deliver Commercial Bin", tmrDatePlus7day, "");
            PageFactoryManager.Get<PartyAgreementPage>()
               .ClickTaskTabBtn()
               .WaitForLoadingIconToDisappear();
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

        }
    }
}
