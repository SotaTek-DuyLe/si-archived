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
            HomePage homePage = new HomePage();
            PartyCommonPage partyPage = new PartyCommonPage();
            CreatePartyPage createPartyPage = new CreatePartyPage();
            DetailPartyPage detailPartyPage = new DetailPartyPage();
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(4), "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

            //login
            login.GoToURL(Url.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            homePage
                .IsOnHomePage(AutoUser6.UserName)
                .GoToThePatiesSubSubMenu();
            //create new party 
            partyPage
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
                .VerifyDisplaySuccessfullyMessage()
                .MergeAllTabInDetailPartyAndVerify()
                .ClickAllTabAndVerify()
                .ClickAllTabInDropdownAndVerify();

        }

        [Test]
        public void TC_005()
        {
            LoginPage login = new LoginPage();
            HomePage homePage = new HomePage();
            CreatePartyPage createPartyPage = new CreatePartyPage();
            DetailPartyPage detailPartyPage = new DetailPartyPage();
            PartyModel partyModel = new PartyModel("AutoParty" + CommonUtil.GetRandomNumber(5), "North Star Commercial", CommonUtil.GetLocalTimeMinusDay(PartyTabConstant.DATE_DD_MM_YYYY_FORMAT, -1));

            //login
            login.GoToURL(Url.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            homePage
                .IsOnHomePage(AutoUser6.UserName)
                .ClickCreateEventDropdownAndVerify()
                .GoToThePatiesByCreateEvenDropdown()
                .SwitchToChildWindow();
            createPartyPage
                .IsCreatePartiesPopup("North Star")
                .VerifyContractDropdownVlues()
                .VerifyAllPartyTypes()
                .SendKeyToThePartyInput(partyModel.PartyName)
                .SelectContract(3)
                .SelectStartDate(-1)
                .SelectPartyType(1)
                .ClickSaveBtn();
            //Verify all tab display correctly
            detailPartyPage
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
            HomePage homePage = new HomePage();
            PartyCommonPage partyPage = new PartyCommonPage();
            CreatePartyPage createPartyPage = new CreatePartyPage();
            //login
            login.GoToURL(Url.MainPageUrl);
            login
                .SendKeyToUsername(AutoUser6.UserName)
                .SendKeyToPassword(AutoUser6.Password)
                .ClickOnSignIn();
            homePage
                .IsOnHomePage(AutoUser6.UserName)
                .GoToThePatiesSubSubMenu();
            //create new party 
            partyPage
                .ClickAddNewItem()
                .SwitchToChildWindow();
            createPartyPage
                .IsCreatePartiesPopup("North Star Commercial")
                .SendKeyToThePartyInput("Auto" + CommonUtil.GetRandomString(2))
                .SelectStartDate(1)
                .SelectPartyType(1)
                .ClickSaveBtn()
                .VerifyDisplayErrorMessage(errorMessage);
        }
    }
}
