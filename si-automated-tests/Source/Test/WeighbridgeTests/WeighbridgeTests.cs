using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.WeighbridgeTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class WeighbridgeTests : BaseTest
    {
        [Test]
        public void TC_045_WB_Create_party_customer()
        {
            string address = "Twickenham";
            string addressSite1 = "Site Twickenham " + CommonUtil.GetRandomNumber(4);
            string siteName = CommonConstants.WBSiteName;

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser10.UserName, AutoUser10.Password)
                .IsOnHomePage(AutoUser10);
            //Create new party
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Parties")
                .ExpandOption("North Star Commercial")
                .OpenOption("Parties")
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .ClickAddNewItem()
                .SwitchToChildWindow(2);
            PageFactoryManager.Get<CreatePartyPage>()
                .IsCreatePartiesPopup("North Star Commercial")
                .SendKeyToThePartyInput("Auto" + CommonUtil.GetRandomString(2))
                .SelectPartyType(1)
                .ClickSaveBtn();
            DetailPartyPage detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage
                .VerifyDisplaySuccessfullyMessage();
            detailPartyPage
                .WaitForLoadingIconToDisappear();
            detailPartyPage
                .ClickOnDetailsTab()
                .ClickAddCorrespondenceAddress()
                .SwitchToLastWindow();
            PartySiteAddressPage partySiteAddressPage = PageFactoryManager.Get<PartySiteAddressPage>();
            partySiteAddressPage.WaitForLoadingIconToDisappear();
            partySiteAddressPage.IsOnPartySiteAddressPage()
                .InputTextToSearchBar(address)
                .ClickSearchBtn()
                .VerifySearchedAddressAppear(address)
                .ClickOnSearchedAddress(address)
                .ClickOnNextButton()
                .SwitchToLastWindow();
            CreateEditSiteAddressPage createEditSiteAddressPage = PageFactoryManager.Get<CreateEditSiteAddressPage>();
            createEditSiteAddressPage
                .WaitForLoadingIconToDisappear();
            string addressAdded = createEditSiteAddressPage
                .SelectRandomSiteAddress();
            createEditSiteAddressPage.SelectAddressClickNextBtn()
                .InsertSiteName(addressSite1)
                .ClickAnySiteInDd(siteName)
                .ClickCreateBtn()
                .SwitchToChildWindow(2);
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage
                .VerifyCreatedSiteAddressAppearAtAddress(addressAdded)
                .ClickOnInvoiceAddressButton()
                .VerifyCreatedAddressAppearAtInvoiceAddress(addressAdded)
                .SelectCreatedAddress(addressAdded)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved party.");
            //Internal flag checked
            detailPartyPage
                .ClickInternalCheckbox()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved party.");
            //Navigate to Site page

            detailPartyPage
                .ClickOnSitesTab()
                .WaitForLoadingIconInvisiable()
                .OpenFirstSiteRow()
                .SwitchToLastWindow();
            SiteDetailPage siteDetailPage = PageFactoryManager.Get<SiteDetailPage>();
            siteDetailPage
                .WaitForLoadingIconToDisappear();
            siteDetailPage
                .WaitForSiteDetailsLoaded(CommonConstants.WBSiteName, addressSite1 + " / " + addressAdded)
                .VerifyDisplayAllTab(CommonConstants.AllSiteTab)
                .ClickDetailTab()
                .ClickSomeTabAndVerifyNoErrorMessage()
                .ClickMapTabAndVerifyMessage("No Service Unit(s) associated to this Site ")
                .ClickSaveAndCloseBtn()
                .SwitchToChildWindow(2);
            detailPartyPage
                .ClickWBSettingTab()
                .WaitForLoadingIconInvisiable();
            detailPartyPage
                .VerifyWBSettingTab();
        }
    }
}
