using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties;
using static si_automated_tests.Source.Main.Models.UserRegistry;

using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Test
{
    public class PartySiteTest : BaseTest
    {
        [Test]
        public void TC007()
        {
            BasePage basePage = new BasePage();
            LoginPage login = new LoginPage();
            HomePage homePage = new HomePage();
            PartyPage partyPage = new PartyPage();
            PartySiteAddressPage partySiteAddressPage = new PartySiteAddressPage();
            CreateEditSiteAddressPage createEditSiteAddressPage = new CreateEditSiteAddressPage();
            PartyCommonPage partyCommonPage = new PartyCommonPage();
            CreatePartyPage createPartyPage = new CreatePartyPage();
            DetailPartyPage detailPartyPage = new DetailPartyPage();
            string PartyName = "AutoPartyy " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, "North Star Commercial", CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", -1));
            string address = "Twickenham";
            string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
            //login
            login.GoToURL(Url.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser15.UserName)
                .SendKeyToPassword(AutoUser15.Password)
                .ClickOnSignIn();
            homePage
                .IsOnHomePage(AutoUser15.UserName)
                .GoToThePatiesSubSubMenu();
            //create new party 
            partyCommonPage
                .ClickAddNewItem()
                .SwitchToChildWindow();
            createPartyPage
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage();
            //Test path for TC 007
            partyPage.ClickOnDetailsTab()
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            createEditSiteAddressPage.IsOnCreateEditSiteAddressPage();
            string addressAdded = createEditSiteAddressPage.SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickCreateBtn()
                .SwitchToChildWindow();
            partyPage.VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded)
                .SelectCreatedAddress(addressAdded)
                .VerifySelectedAddressOnInvoicePage(address)
                .ClickOnSitesTab()
                .VerifyAddressAppearAtSitesTab(addressSite1)
                .ClickOnDetailsTab()
                .ClickSaveBtn()
                .VerifyDisplaySuccessfullyMessage();
        }
    }
}
