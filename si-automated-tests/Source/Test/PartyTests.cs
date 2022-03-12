using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Paties;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test
{
    public class PartyTests : BaseTest
    {

        [Test]
        public void TC_004()
        {
            LoginPage login = new LoginPage();
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(4), "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6)
                .ClickParties()
                .ClickNSC()
                .ClickPartySubMenu()
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
                .MergeAllTabInDetailPartyAndVerify()
                .ClickAllTabAndVerify()
                .ClickAllTabInDropdownAndVerify();

        }

        [Test]
        public void TC_005()
        {
            LoginPage login = new LoginPage();
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(5), "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6)
                .ClickCreateEventDropdownAndVerify()
                .GoToThePatiesByCreateEvenDropdown()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectContract(3)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            //Verify all tab display correctly
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
                .MergeAllTabInDetailPartyAndVerify()
                .ClickAllTabAndVerify()
                .ClickAllTabInDropdownAndVerify()
                .SwitchToFirstWindow();

        }

        [Test]
        public void TC_006()
        {
            string errorMessage = "Start Date cannot be in the future";
            LoginPage login = new LoginPage();

            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6)
                .ClickParties()
                .ClickNSC()
                .ClickPartySubMenu()
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .SendKeyToThePartyInput("Auto" + CommonUtil.GetRandomString(2))
                .SelectStartDate(1)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .VerifyDisplayErrorMessage(errorMessage);
        }

        [Test]
        public void TC_010()
        {
            LoginPage login = new LoginPage();

            string PartyName = "AutoParty " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6)
                .ClickParties()
                .ClickNSC()
                .ClickPartySubMenu()
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
            //Test path for TC 010
                .ClickOnAddInvoiceAddressBtn()
                .SwitchToChildWindow(3);
            string siteName = "SiteAuto" + CommonUtil.GetRandomNumber(3);
            string postCode = CommonUtil.GetRandomString(2) + CommonUtil.GetRandomNumber(3);
            AddressDetailModel addressDetailModel = new AddressDetailModel(siteName, postCode);
            PageFactoryManager.Get<PartySiteAddressPage>()
                .IsOnPartySiteAddressPage()
                .ClickOnCreateManuallyBtn()
                .IsCheckAddressDetailScreen(false)
                .SendKeyInSiteNameInput(siteName)
                .VerifyCreateBtnDisabled()
                .InputAllMandatoryFieldInCheckAddressDetailScreen(addressDetailModel)
                .ClickCreateBtn()
                .WaitForLoadingIconInvisiable()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyCreatedSiteAddressAppearAtAddress(addressDetailModel)
                .ClickCorresspondenAddress()
                .VerifyDisplayNewSiteAddressInCorresspondence(addressDetailModel, false)
                .SelectCorresspondenAddress(addressDetailModel)
                .ClickOnSitesTab()
                .WaitForLoadingIconInvisiable();
            List<SiteModel> allSiteModel = PageFactoryManager.Get<DetailPartyPage>()
                .GetAllSiteInList();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifySiteManualCreated(addressDetailModel, allSiteModel[0], "Serviced Site", false)
                .ClickOnDetailsTab()
                .ClickSaveBtn()
                .VerifyDisplaySuccessfullyMessage();
        }

        [Test]
        public void TC_011()
        {
            LoginPage login = new LoginPage();
            string siteName = "SiteAuto" + CommonUtil.GetRandomNumber(3);
            string postCode = CommonUtil.GetRandomString(2) + CommonUtil.GetRandomNumber(3);
            AddressDetailModel addressDetailModel = new AddressDetailModel(siteName, postCode);

            string PartyName = "AutoParty " + CommonUtil.GetRandomNumber(4);
            PartyModel partyModel = new PartyModel(PartyName, "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));
            //login
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser6)
                .ClickParties()
                .ClickNSC()
                .ClickPartySubMenu()
                .SwitchNewIFrame();
            //create new party 
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifyDisplaySuccessfullyMessage()
            //Test path for TC 011
                 .ClickOnSitesTab()
                 .WaitForLoadingIconInvisiable()
                 .ClickOnAddNewItemInSiteTabBtn()
                 .SwitchToChildWindow(3);
            PageFactoryManager.Get<PartySiteAddressPage>()
                .IsOnPartySiteAddressPage()
                .ClickOnNonGeoGraphicalAddressBtn()
                .IsCheckAddressDetailScreen(true)
                .SendKeyInSiteNameInput(siteName)
                .InputSomeMandatoryFieldInCheckAddressDetailScreen(addressDetailModel)
                .VerifyCreateBtnDisabled()
                .InputValueInCountry(addressDetailModel.Country)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            List<SiteModel> allSiteModel = PageFactoryManager.Get<DetailPartyPage>()
                .GetAllSiteInList();
            PageFactoryManager.Get<DetailPartyPage>()
                .VerifySiteManualCreated(addressDetailModel, allSiteModel[0], "Serviced Site", true)
                .ClickOnDetailsTab()
                .VerifyValueDefaultInCorresspondenAddress()
                .ClickCorresspondenAddress()
                .VerifyDisplayNewSiteAddressInCorresspondence(addressDetailModel, true)
                //Invoice Address
                .VerifyValueDefaultInInvoiceAddress()
                .ClickInvoiceAddressDd()
                .VerifyDisplayNewSiteAddressInInvoiceAddress(addressDetailModel, true);
        }
    }
}
