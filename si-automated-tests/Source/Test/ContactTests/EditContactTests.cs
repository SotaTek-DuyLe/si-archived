using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties.PartyAgreement;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ContactTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class EditContactTests : BaseTest
    {
        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Verify user can edit contact for a party")]
        public void TC_038_01_verify_that_user_can_edit_contact_for_a_party()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser57.UserName, AutoUser57.Password)
                .IsOnHomePage(AutoUser57);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PartyCommonPage partyCommonPage = PageFactoryManager.Get<PartyCommonPage>();
            partyCommonPage
                .FilterPartyById(1)
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("Twisted Fish Limited")
                .WaitForLoadingIconToDisappear();
            //Contact tab
            detailPartyPage
                .ClickOnContactTab()
                .WaitForLoadingIconToDisappear();
            ContactModel contactModelEdit = new ContactModel(false, CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2));
            List<ContactModel> contactModelList = detailPartyPage
               .GetAllContact();
            detailPartyPage
                .ClickFirstContact()
                .SwitchToLastWindow();
            EditPartyContactPage editPartyContactPage = PageFactoryManager.Get<EditPartyContactPage>();
            editPartyContactPage
                .NavigateToDetailsTab()
                .IsEditPartyContactPage(contactModelList[0])
                .EnterAllValueFields(contactModelEdit)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Contact");
            string completedDateDisplayed = CommonUtil.GetUtcTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            string timeNow = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);

            editPartyContactPage
                .NavigateToNotesTab()
                .IsNotesTab()
                .EnterTitleAndNoteField("Edit Contact", "New Edit")
                .WaitForLoadingIconToDisappear();
            editPartyContactPage
                .VerifyTitleAndNoteAfter()
                .GetAndVerifyNoteAfterAdding("Edit Contact: ", "New Edit", AutoUser57.UserName, completedDateDisplayed, timeNow)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickRefreshBtn();
            List<ContactModel> getAllContactAfter = detailPartyPage
                .GetAllContact();
            detailPartyPage
                .VerifyContactCreated(contactModelEdit, getAllContactAfter[0]);
        }


        [Category("CreateInspection")]
        [Category("Chang")]
        [Test(Description = "Verify contact with start date more than current date will not display in primary contact and invoice contact")]
        public void TC_038_02_verify_contact_with_start_date_more_than_current_date_will_not_display_in_primary_contact_and_invoice_contact()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser57.UserName, AutoUser57.Password)
                .IsOnHomePage(AutoUser57);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PartyCommonPage partyCommonPage = PageFactoryManager.Get<PartyCommonPage>();
            partyCommonPage
                .FilterPartyById(51)
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("The Angel & Crown")
                .WaitForLoadingIconToDisappear();
            string[] valueContactParty = new string[1] { "Select..." };
            detailPartyPage
                .ClickOnDetailsTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickOnPrimaryContactDd()
                .VerifyValueInPrimaryContactDd(valueContactParty)
                .ClickInvoiceContactDd()
                .VerifyValueInInvoiceContactDd(valueContactParty)
                .OpenAgreementTab();
            AgreementTab agreementTab = PageFactoryManager.Get<AgreementTab>();
            agreementTab
                .OpenFirstAgreementRow()
                .SwitchToLastWindow();
            AgreementDetailPage agreementDetailPage = PageFactoryManager.Get<AgreementDetailPage>();
            string[] valueContactAgreement = new string[1] { "Use Customer" };
            agreementDetailPage
                .WaitForDetailAgreementLoaded("COMMERCIAL COLLECTIONS", "THE ANGEL & CROWN")
                .ClickPrimaryContactDd()
                .VerifyValueInPrimaryContactDd(valueContactAgreement)
                .ClickInvoiceContactDd()
                .VerifyValueInInvoiceContactDd(valueContactAgreement)
                .ScrollToBottomOfPage();
            string[] valueContactAgreementLine = new string[1] { "Use Agreement" };
            agreementDetailPage
                .ClickInvoiceContactDdAtServiceTable()
                .VerifyNumberOfContact(1)
                .VerifyValueInInvoiceContactServiceTable(valueContactAgreementLine)
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            string[] valueContactPrimary = new string[1] { "Select..." };
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailPageLoaded("The Angel & Crown / THE ANGEL AND CROWN, 5 CHURCH COURT, RICHMOND, TW9 1JL", "THE ANGEL & CROWN")
                .ClickPrimaryContactDd()
                .VerifyNumberOfContact(1)
                .VerifyValueInPrimaryContactDd(valueContactPrimary);
        }

    }
}
