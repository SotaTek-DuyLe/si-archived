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
        public void TC_022_A()
        {
            string todayDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser15.UserName, AutoUser15.Password)
                .IsOnHomePage(AutoUser15);
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
    }
}
