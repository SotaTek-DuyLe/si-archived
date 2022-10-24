using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyDataPage;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.PartiesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class PartyDataTest : BaseTest
    {
        [Category("PartyData")]
        [Test]
        public void TC_039_Save_Extend_Data_From_Partytype_Customer()
        {
            int partyId = 4;
            string partyName = "Mr & Mrs Smith";
            string note1 = "This is notes automation test";
            string note2 = "This is edited note for the Party Customer";

            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser30.UserName, AutoUser30.Password)
                .IsOnHomePage(AutoUser30);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickOnDetailsTab()
                .VerifyPartyTypeChecked("Customer")
                .GoToATab("Data");
            PageFactoryManager.Get<PartyDataPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyDataPage>()
                .VerifyPartyDataCustomer()
                .ClickCustomer()
                .VerifyCustomerPath()
                .InputNote(note1)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyDataPage>()
                .VerifyCustomerPathHidden()
                .ClickCustomer()
                .VerifyCustomerPathWithNote(note1)
                .InputNote(note2)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SavePartySuccessMessage)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<PartyDataPage>()
               .VerifyCustomerPathHidden()
               .ClickCustomer()
               .VerifyCustomerPathWithNote(note2);
        }
    }
}
