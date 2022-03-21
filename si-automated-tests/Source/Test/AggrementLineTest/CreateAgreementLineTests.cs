using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using si_automated_tests.Source.Main.Pages.Task;


namespace si_automated_tests.Source.Test
{
    public class CreateAgreementLineTests : BaseTest
    {
        [Test]
        public void TC_012_Create_Agreement_Line_With_Start_Date_In_The_Past()
        {
            string agreementStartDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -5);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser5.UserName, AutoUser5.Password)
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(73)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            string partyStartDate = PageFactoryManager.Get<DetailPartyPage>()
                .GetPartyStartDate();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .IsOnPartyAgreementPage()
                .VerifyStartDateIsCurrentDate()
                .VerifyEndDateIsPredefined()
                .EnterStartDate(agreementStartDate + Keys.Enter)
                .VerifyStartDateIs(agreementStartDate)
                .ClickSaveBtn();
            PageFactoryManager.Get<BasePage>().VerifyToastMessage("Agreement Type is required");
            PageFactoryManager.Get<PartyAgreementPage>()
               .IsOnPartyAgreementPage()
               .SelectAgreementType("Commercial Collections")
               .ClickSaveBtn();
            PageFactoryManager.Get<BasePage>().VerifyToastMessage("Successfully saved agreement");
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("New")
                .VerifyNewOptionsAvailable()
                .ClosePartyAgreementPage();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            int newAgreementId = PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .VerifyFirstAgreementInfo("Greggs", agreementStartDate, "01/01/2050", "Commercial Collections", "New")
                .GetAgreementId();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCloseBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(newAgreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .IsOnPartyAgreementPage()
                .ClickAddService()
                .IsOnAddServicePage();
            string siteAdd = PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .ChooseService("Commercial")
                .ChooseServicedSite(1)
                .GetSiteAddress();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .ChooseAssetType("660L")
                .InputAssetQuantity(2)
                .ChooseTenure("Rental")
                .ChooseProduct("General Recycling")
                .ChooseEwcCode("150106")
                .InputProductQuantity(600)
                .VerifyTotalProductQuantity(2 * 600)
                .ClickDoneBtn()
                .VerifyAssetSummary(2, "660L", "Rental", 600, "General Recycling")
                .ClickBack();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .VerifyAssetSummary(2, "660L", "Rental", 600, "General Recycling")
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .VerifyAssetSummary(2, "660L", 600, "General Recycling")
                .ClickAddScheduleRequirement()
                .SelectFrequencyOption("Weekly")
                .UntickAnyDayOption()
                .SelectDayOfWeek("Thu")
                .ClickDoneRequirementBtn()
                .VerifyScheduleSummary("Once Every week on any Thursday")
                .ClickNext();
            PageFactoryManager.Get<PriceTab>()
                .ClosePriceRecords()
                .ClickNext();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .VerifyInvoiceOptions("Use Agreement")
                .ClickFinish();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServicePanelPresent()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(5000);
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServiceStartDate(agreementStartDate)
                .VerifyServiceSiteAddres(siteAdd)
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyAllStartDate(agreementStartDate)
                .VerifyCreateAdhocButtonsAreDisabled()
                .ClickApproveAgreement()
                .ConfirmApproveBtn()
                .VerifyAgreementStatus("Active")
                .ClosePartyAgreementPage()
                .SwitchToLastWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(73)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .VerifyFirstAgreementInfo("Greggs", agreementStartDate, "01/01/2050", "Commercial Collections", "Active")
                .OpenFirstAgreement()
                .SwitchToLastWindow();

            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("Active")
                .OpenTaskTab()
                .VerifyFirstTaskType("Deliver Commercial Bin")
                .VerifyFirstTaskDueDate(CommonUtil.GetLocalTimeFromDate(agreementStartDate, "dd/MM/yyyy", 7))
                .OpenFirstTask()
                .SwitchToLastWindow()
                .SwitchToTab("Task Lines");
            PageFactoryManager.Get<TaskLineTab>()
                .VerifyFirstTaskInfo("Deliver", "660L", "General Recycling", "Kilograms", "Unallocated");
        }
        [Test]
        public void TC_013_Create_Agreement_Line_With_Start_Date_In_The_Future()
        {
            string agreementStartDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 7);
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser5.UserName, AutoUser5.Password)
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(73)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            string partyStartDate = PageFactoryManager.Get<DetailPartyPage>()
                .GetPartyStartDate();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .IsOnPartyAgreementPage()
                .VerifyStartDateIsCurrentDate()
                .VerifyEndDateIsPredefined()
                .EnterStartDate(agreementStartDate + Keys.Enter)
                .VerifyStartDateIs(agreementStartDate)
                .SelectAgreementType("Commercial Collections")
                .ClickSaveBtn();
            PageFactoryManager.Get<BasePage>().VerifyToastMessage("Successfully saved agreement");
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("New")
                .VerifyNewOptionsAvailable()
                .ClosePartyAgreementPage();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            int newAgreementId = PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .VerifyFirstAgreementInfo("Greggs", agreementStartDate, "01/01/2050", "Commercial Collections", "New")
                .GetAgreementId();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCloseBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(newAgreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .IsOnPartyAgreementPage()
                .ClickAddService()
                .IsOnAddServicePage();
            string siteAdd = PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .ChooseService("Commercial")
                .ChooseServicedSite(1)
                .GetSiteAddress();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .ChooseAssetType("660L")
                .InputAssetQuantity(2)
                .ChooseTenure("Rental")
                .ChooseProduct("General Recycling")
                .ChooseEwcCode("150106")
                .InputProductQuantity(600)
                .VerifyTotalProductQuantity(2 * 600)
                .ClickDoneBtn()
                .VerifyAssetSummary(2, "660L", "Rental", 600, "General Recycling")
                .ClickBack();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .VerifyAssetSummary(2, "660L", "Rental", 600, "General Recycling")
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .VerifyAssetSummary(2, "660L", 600, "General Recycling")
                .ClickAddScheduleRequirement()
                .SelectFrequencyOption("Weekly")
                .UntickAnyDayOption()
                .SelectDayOfWeek("Tue")
                //.InputStartDate(serviceStartDate)
                .ClickDoneRequirementBtn()
                .VerifyScheduleSummary("Once Every week on any Tuesday")
                .ClickNext();
            PageFactoryManager.Get<PriceTab>()
                .IsOnPriceTab()
                .ClosePriceRecords()
                .ClickNext();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .VerifyInvoiceOptions("Use Agreement")
                .ClickFinish();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServicePanelPresent()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(5000);
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServiceStartDate(agreementStartDate)
                .VerifyServiceSiteAddres(siteAdd)
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyAllStartDate(agreementStartDate)
                .VerifyCreateAdhocButtonsAreDisabled()
                .ClickApproveAgreement()
                .ConfirmApproveBtn()
                .VerifyAgreementStatus("Approved")
                .ClosePartyAgreementPage()
                .SwitchToLastWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(73)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .VerifyFirstAgreementInfo("Greggs", agreementStartDate, "01/01/2050", "Commercial Collections", "Approved")
                .OpenFirstAgreement()
                .SwitchToLastWindow();

            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("Approved")
                .OpenTaskTab()
                .VerifyFirstTaskType("Deliver Commercial Bin")
                .VerifyFirstTaskDueDate(CommonUtil.GetLocalTimeFromDate(agreementStartDate, "dd/MM/yyyy", 7))
                .OpenFirstTask()
                .SwitchToLastWindow()
                .VerifyToastMessageNotAppear("Invalid Required Delivery Date")
                .SwitchToTab("Task Lines");
            PageFactoryManager.Get<TaskLineTab>()
                .VerifyFirstTaskInfo("Deliver", "660L", "General Recycling", "Kilograms", "Unallocated")
                .ClickCloseBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("Approved")
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .VerifySecondTaskType("Deliver Commercial Bin")
                .VerifySecondTaskDueDate(CommonUtil.GetLocalTimeFromDate(agreementStartDate, "dd/MM/yyyy", 7))
                .OpenSecondTask()
                .SwitchToLastWindow()
                .VerifyToastMessageNotAppear("Invalid Required Delivery Date")
                .SwitchToTab("Task Lines");
            PageFactoryManager.Get<TaskLineTab>()
                .VerifyFirstTaskInfo("Deliver", "660L", "General Recycling", "Kilograms", "Unallocated");
        }
        [Test]
        public void TC_014_Create_Agreement_Line_With_Start_Date_Is_Current_Date()
        {
            string agreementStartDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser5.UserName, AutoUser5.Password)
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(73)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            string partyStartDate = PageFactoryManager.Get<DetailPartyPage>()
                .GetPartyStartDate();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .IsOnPartyAgreementPage()
                .VerifyStartDateIsCurrentDate()
                .VerifyEndDateIsPredefined()
                //.EnterStartDate(agreementStartDate + Keys.Enter)
                .VerifyStartDateIs(agreementStartDate)
                .SelectAgreementType("Commercial Collections")
                .ClickSaveBtn();
            PageFactoryManager.Get<BasePage>().VerifyToastMessage("Successfully saved agreement");
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("New")
                .VerifyNewOptionsAvailable()
                .ClosePartyAgreementPage();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            int newAgreementId = PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .VerifyFirstAgreementInfo("Greggs", agreementStartDate, "01/01/2050", "Commercial Collections", "New")
                .GetAgreementId();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCloseBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(newAgreementId)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .IsOnPartyAgreementPage()
                .ClickAddService()
                .IsOnAddServicePage();
            string siteAdd = PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .ChooseService("Commercial")
                .ChooseServicedSite(2)
                .GetSiteAddress();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .ClickAddAsset()
                .ChooseAssetType("1100L")
                .InputAssetQuantity(3)
                .ChooseTenure("Rental")
                .ChooseProduct("Plastic")
                .ChooseEwcCode("200139")
                .InputProductQuantity(1000)
                .VerifyTotalProductQuantity(3 * 1000)
                .ClickDoneBtn()
                .VerifyAssetSummary(3, "1100L", "Rental", 1000, "Plastic")
                .ClickBack();
            PageFactoryManager.Get<SiteAndServiceTab>()
                .IsOnSiteServiceTab()
                .ClickNext();
            PageFactoryManager.Get<AssetAndProducTab>()
                .IsOnAssetTab()
                .VerifyAssetSummary(3, "1100L", "Rental", 1000, "Plastic")
                .ClickNext();
            PageFactoryManager.Get<ScheduleServiceTab>()
                .IsOnScheduleTab()
                .ClickAddService()
                .ClickDoneScheduleBtn()
                .VerifyAssetSummary(3, "1100L", 1000, "Plastic")
                .ClickAddScheduleRequirement()
                .SelectFrequencyOption("Weekly")
                .UntickAnyDayOption()
                .SelectDayOfWeek("Mon")
                //.InputStartDate(serviceStartDate)
                .ClickDoneRequirementBtn()
                .VerifyScheduleSummary("Once Every week on any Monday")
                .ClickNext();
            PageFactoryManager.Get<PriceTab>()
                .IsOnPriceTab()
                .ClosePriceRecords()
                .ClickNext();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .VerifyInvoiceOptions("Use Agreement")
                .ClickFinish();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServicePanelPresent()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(5000);
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServiceStartDate(agreementStartDate)
                .VerifyServiceSiteAddres(siteAdd)
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyAllStartDate(agreementStartDate)
                .VerifyCreateAdhocButtonsAreDisabled()
                .ClickApproveAgreement()
                .ConfirmApproveBtn()
                .VerifyAgreementStatus("Active")
                .ClosePartyAgreementPage()
                .SwitchToLastWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser5);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(73)
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<DetailPartyPage>()
                .OpenAgreementTab()
                .VerifyFirstAgreementInfo("Greggs", agreementStartDate, "01/01/2050", "Commercial Collections", "Active")
                .OpenFirstAgreement()
                .SwitchToLastWindow();

            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("Active")
                .OpenTaskTab()
                .VerifyFirstTaskType("Deliver Commercial Bin")
                .VerifyFirstTaskDueDate(CommonUtil.GetLocalTimeFromDate(agreementStartDate, "dd/MM/yyyy", 7))
                .OpenFirstTask()
                .SwitchToLastWindow()
                .VerifyToastMessageNotAppear("Invalid Required Delivery Date")
                .SwitchToTab("Task Lines");
            PageFactoryManager.Get<TaskLineTab>()
                .VerifyFirstTaskInfo("Deliver", "1100L", "Plastic", "Kilograms", "Unallocated")
                .ClickCloseBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyAgreementStatus("Active")
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<TaskTab>()
                .VerifySecondTaskType("Deliver Commercial Bin")
                .VerifySecondTaskDueDate(CommonUtil.GetLocalTimeFromDate(agreementStartDate, "dd/MM/yyyy", 7))
                .OpenSecondTask()
                .SwitchToLastWindow()
                .VerifyToastMessageNotAppear("Invalid Required Delivery Date")
                .SwitchToTab("Task Lines");
            PageFactoryManager.Get<TaskLineTab>()
                .VerifyFirstTaskInfo("Deliver", "1100L", "Plastic", "Kilograms", "Unallocated");
        }

    }
}
