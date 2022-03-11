using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.PartyAgreement.AddService;
using si_automated_tests.Source.Main.Pages.Paties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test
{
    public class CreateAgreementLineTests : BaseTest
    {
        [Test]
        public void TC_012()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser5.UserName, AutoUser5.Password)
                .IsOnHomePage(AutoUser5)
                .GoToThePatiesSubSubMenu()
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
                .EnterStartDate(partyStartDate + Keys.Enter)
                .VerifyStartDateIs(partyStartDate)
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
                .VerifyFirstAgreementInfo("Greggs", partyStartDate, "01/01/2050", "Commercial Collections", "New")
                .GetAgreementId();
            PageFactoryManager.Get<DetailPartyPage>()
                .ClickCloseBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser5)
                .GotoAgreementPage();
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
            //Error? Skip for now
            PageFactoryManager.Get<PriceTab>()
                .ClickNext();
            PageFactoryManager.Get<InvoiceDetailTab>()
                .VerifyInvoiceOptions("Use Agreement")
                .ClickFinish();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServicePanelPresent()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyAgreementPage>()
                .VerifyServiceStartDate(partyStartDate)
                .VerifyServiceSiteAddres(siteAdd)
                .ExpandAgreementLine()
                .ExpandAllAgreementFields()
                .VerifyAllStartDate(partyStartDate)
                .ClickApproveAgreement()
                .ConfirmApproveBtn()
                .VerifyAgreementStatus("Active");


            Thread.Sleep(5555);
        }


    }
}
