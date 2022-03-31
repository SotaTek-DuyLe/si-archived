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

namespace si_automated_tests.Source.Test.AggrementLineTest
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class EditAgreementLineTest_022_029 : BaseTest
    {
        [Category("EditAgreement")]
        [Test]
        public void TC_022()
        {
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(53)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<AgreementTab>()
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
               .RemoveAllRedundantPrices(1)
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
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .VerifyNoNewTaskAppear()
                .SwitchToFirstWindow();

            //Go to services task and verify 
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
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
        [Test]
        public void TC_023()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string originDate = "08/03/2022";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser13.UserName, AutoUser13.Password)
                .IsOnHomePage(AutoUser13);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            // Finish Edit Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(5000);
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
                .ClickMainOption("Parties")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(53)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<AgreementTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTab>()
                .VerifyAgreementAppear("32", "Rosie and Java", originDate, "01/01/2050", "Active")
                .OpenAgreementWithId(32)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNum = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskNumUnchange(taskNum, 3)
                .SwitchToFirstWindow();

            //Verify in site service 
            AsserAndProductModel assetAndProductInput = new AsserAndProductModel("660L", "3", "General Recycling", "", "165", "", "Owned", new string[1], new string[1], tommorowDate, "01/01/2050");
            RegularModel regularInput = new RegularModel("Service", "660L", "3", "General Recycling", "165", "", tommorowDate);
            MobilizationModel adhoc = new MobilizationModel("Service", "660L", "3", "General Recycling", "165", "", tommorowDate);
            List<MobilizationModel> adhocListInput = new List<MobilizationModel> { adhoc, adhoc };
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
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
        }

        [Category("EditAgreement")]
        [Test]
        public void TC_024()
        {
            string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
            string originDate = "08/03/2022";
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
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
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
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            // Finish Edit Agreement Line 
            PageFactoryManager.Get<BasePage>()
                .WaitForLoadingIconToDisappear()
                .SleepTimeInMiliseconds(5000);
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
                .ClickMainOption("Parties")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(54)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab();
            PageFactoryManager.Get<AgreementTab>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AgreementTab>()
                .VerifyAgreementAppear("33", "The White Cross", originDate, "01/01/2050", "Active")
                .OpenAgreementWithId(33)
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .ClickTaskTabBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .WaitForLoadingIconToDisappear();
            int taskNum = PageFactoryManager.Get<TaskTab>()
                .GetAllTaskNum();
            PageFactoryManager.Get<TaskTab>()
                .VerifyTaskNumUnchange(taskNum, 3)
                .SwitchToFirstWindow();

            //Verify in site service 
          
            PageFactoryManager.Get<NavigationBase>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
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
        }
    }
}
