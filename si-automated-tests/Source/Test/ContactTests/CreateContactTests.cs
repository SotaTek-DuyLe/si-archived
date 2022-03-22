using System;
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
    public class CreateContactTests : BaseTest
    {
        [Test]
        public void TC_037_01_02_03_04_05_06_07verify_user_can_create_a_new_contact_and_set_newly_create_contact_related_on_a_party()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser8.UserName, AutoUser8.Password)
                .IsOnHomePage(AutoUser8);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PartyCommonPage partyCommonPage = PageFactoryManager.Get<PartyCommonPage>();
            partyCommonPage
                .FilterPartyById(43)
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("Jaflong Tandoori")
                .WaitForLoadingIconToDisappear();
            //Contact tab
            detailPartyPage
                .ClickOnContactTab()
                .ClickAddNewItemAtContactTab()
                .SwitchToLastWindow();

            CreatePartyContactPage createPartyContactPage = PageFactoryManager.Get<CreatePartyContactPage>();
            ContactModel contactModel = new ContactModel();
            createPartyContactPage
                .IsCreatePartyContactPage()
                //Step 1: Line 7
                .EnterFirstName(contactModel.FirstName)
                .EnterLastName(contactModel.LastName)
                .ClickSaveBtn()
                .VerifyToastMessage("Please enter at least one of the contact details.");
            //Step 1: Line 9
            createPartyContactPage
                .EnterMobileValue(contactModel.Mobile)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Contact");
            //Step 1: Line 10
            createPartyContactPage
                .EnterValueRemainingFields(contactModel)
                .ClickAnyContactGroupsAndVerify(contactModel.ContactGroups)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            List<ContactModel> contactModelList = detailPartyPage
               .GetAllContact();
            string[] primaryContact = { "Select...", contactModel.FirstName + " " + contactModel.LastName };
            detailPartyPage
                .VerifyContactCreated(contactModel, contactModelList[0])
                //Step 2: Line 11
                .ClickOnDetailsTab()
                .ClickOnPrimaryContactDd()
                .VerifyValueInPrimaryContactDd(primaryContact)
                //Step 2: Line 12
                .SelectAnyPrimaryContactAndVerify(contactModel)
                //Step 2: Line 13
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved party.");
            //Step 3: Line 14
            detailPartyPage
                .ClickInvoiceContactDd()
                .VerifyValueInInvoiceContactDd(primaryContact)
                //Step 3: Line 15
                .SelectAnyInvoiceContactAndVerify(contactModel)
                //Step 3: Line 16
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved party.");
                //Step 4: Line 17
            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconInvisiable()
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailPageLoaded()
                .ClickPrimaryContactDd()
                //Step 4: Line 18
                .SelectAnyPrimaryContactAndVerify(contactModel)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Site")
                .ClickCloseBtn()
                .SwitchToChildWindow(2);
            //Step 5: Line 19
            AgreementTab agreementTab = PageFactoryManager.Get<AgreementTab>();
            detailPartyPage
                .OpenAgreementTab();
            agreementTab
                .OpenFirstAgreementRow()
                .SwitchToLastWindow();
            AgreementDetailPage agreementDetailPage = PageFactoryManager.Get<AgreementDetailPage>();
            string[] primaryContactAgreement = { "Use Customer", contactModel.FirstName + " " + contactModel.LastName };
            agreementDetailPage
                .WaitForDetailAgreementLoaded()
                //Step 5: Line 20
                .ClickPrimaryContactDd()
                //Step 5: Line 21
                .SelectAnyPrimaryContactAndVerify(contactModel)
                //Step 5: Line 22
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear();
            //Step 6: Line 23
            agreementDetailPage
                .ClickInvoiceContactDd()
                .VerifyValueInInvoiceContactDd(primaryContactAgreement)
                //Step 6: Line 24
                .SelectAnyInvoiceContactAndVerify(contactModel)
                //Step 5: Line 25
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear()
                .ScrollToBottomOfPage();
            //Step 7: Line 26
            agreementDetailPage
                .ClickExpandBtn()
                .ScrollToAdhoc()
                .ClickInvoiceContactDdAtServiceTable()
                //Step 7: Line 28
                .SelectAnyInvoiceContactServiceTableAndVerify(contactModel)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved agreement")
                .WaitForLoadingIconToDisappear()
                .ClickCloseBtn();
        }

        [Test]
        public void TC_037_08_verify_user_can_create_new_contact_using_add_button_on_party_form()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser8.UserName, AutoUser8.Password)
                .IsOnHomePage(AutoUser8);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PartyCommonPage partyCommonPage = PageFactoryManager.Get<PartyCommonPage>();
            partyCommonPage
                .FilterPartyById(43)
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("Jaflong Tandoori")
                .WaitForLoadingIconToDisappear();
            //Step 8: Line 29
            detailPartyPage
                .ClickAddPrimaryContactBtn()
                .SwitchToLastWindow();
            AddPrimaryContactPage addPrimaryContactPage = PageFactoryManager.Get<AddPrimaryContactPage>();
            ContactModel contactModelNewPrimary = new ContactModel("Joe", "Smith", "+447785434111");
            addPrimaryContactPage
                .IsCreatePrimaryContactPage()
                //Step 8: Line 30
                .EnterFirstName(contactModelNewPrimary.FirstName)
                .EnterLastName(contactModelNewPrimary.LastName)
                .EnterMobileValue(contactModelNewPrimary.Mobile)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .VerifyWindowClosed(2);
            detailPartyPage
                .ClickOnPrimaryContactDd()
                .VerifyFirstValueInPrimaryContactDd(contactModelNewPrimary)
                .ClickOnContactTab()
                .WaitForLoadingIconToDisappear();
            List<ContactModel> contactModels = detailPartyPage
                .GetAllContact();
            detailPartyPage
                .VerifyContactCreatedWithSomeFields(contactModelNewPrimary, contactModels[0]);
        }

        [Test]
        public void TC_037_09_verify_user_can_create_new_contact_using_add_button_on_Agreement_form()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser8.UserName, AutoUser8.Password)
                .IsOnHomePage(AutoUser8);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PartyCommonPage partyCommonPage = PageFactoryManager.Get<PartyCommonPage>();
            partyCommonPage
                .FilterPartyById(43)
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("Jaflong Tandoori")
                .WaitForLoadingIconToDisappear();
            //Step 8: Line 29
            detailPartyPage
            //Step 9: Line 32
                .OpenAgreementTab();
            AgreementTab agreementTab = PageFactoryManager.Get<AgreementTab>();
            agreementTab
                .OpenFirstAgreementRow()
                .SwitchToLastWindow();
            AgreementDetailPage agreementDetailPage = PageFactoryManager.Get<AgreementDetailPage>();
            agreementDetailPage
                .WaitForDetailAgreementLoaded()
                .ClickAddInvoiceContactBtn()
                .SwitchToLastWindow();
            AddInvoiceContactPage addInvoiceContactPage = PageFactoryManager.Get<AddInvoiceContactPage>();
            ContactModel contactModelNewInvoice = new ContactModel("Anna", "Joe", "+447785434111");
            addInvoiceContactPage
                .IsCreateInvoiceContactPage()
                //Step 9: Line 33
                .EnterFirstName(contactModelNewInvoice.FirstName)
                .EnterLastName(contactModelNewInvoice.LastName)
                .EnterMobileValue(contactModelNewInvoice.Mobile)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(3);
            agreementDetailPage
                .ClickInvoiceContactDd()
                .VerifyFirstValueInInvoiceContactDd(contactModelNewInvoice)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2, 200);
            detailPartyPage
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickOnContactTab();
            List<ContactModel> getAllContact = detailPartyPage
                .GetAllContact();
            detailPartyPage
                .VerifyContactCreatedWithSomeFields( contactModelNewInvoice, getAllContact[0]);
        }

        [Test]
        public void TC_037_10_verify_user_can_create_new_contact_using_add_button_on_Site_form()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser8.UserName, AutoUser8.Password)
                .IsOnHomePage(AutoUser8);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PartyCommonPage partyCommonPage = PageFactoryManager.Get<PartyCommonPage>();
            partyCommonPage
                .FilterPartyById(43)
                .OpenFirstResult()
                .SwitchToLastWindow();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .WaitForDetailPartyPageLoadedSuccessfully("Jaflong Tandoori")
                .WaitForLoadingIconToDisappear();
            //Step 10: Line 35
            detailPartyPage
                .ClickOnSitesTab()
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForSiteDetailPageLoaded()
                .ClickPrimaryContactAddBtn()
                .SwitchToLastWindow();
            AddPrimaryContactAtServicePage addPrimaryContactAtServicePage = PageFactoryManager.Get<AddPrimaryContactAtServicePage>();
            ContactModel contactModelNew = new ContactModel("Chang", "Joe", "+447785434324");
            addPrimaryContactAtServicePage
                .IsCreatePrimaryContactAtServiceTabPage()
                .EnterFirstName(contactModelNew.FirstName)
                .EnterLastName(contactModelNew.LastName)
                .EnterMobileValue(contactModelNew.Mobile)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(3);
            siteDetailPage
                .ClickPrimaryContactDd()
                .VerifyFirstValueInPrimaryContactDd(contactModelNew)
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2, 200);
            detailPartyPage
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickOnContactTab();
            List<ContactModel> getAllContact = detailPartyPage
                .GetAllContact();
            detailPartyPage
                .VerifyContactCreatedWithSomeFields(contactModelNew, getAllContact[0]);
        }

    }
}
