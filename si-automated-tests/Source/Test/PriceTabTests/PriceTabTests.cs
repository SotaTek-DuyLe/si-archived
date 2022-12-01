using System.Collections.Generic;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.IE_Configuration;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccount;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccountStatement;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyHistory;
using si_automated_tests.Source.Main.Pages.Prices;
using si_automated_tests.Source.Main.Pages.UserAndRole;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.PriceTabTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class PriceTabTests : BaseTest
    {
        [Category("Create price record")]
        [Category("Huong")]
        [Test(Description = "Verify whether the Minimum price value saved on newly created prices from Party")]
        public void TC_171_1_Verify_whether_the_Minimum_price_value_saved_on_newly_created_prices_from_Party()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser58.UserName)
                .SendKeyToPassword(AutoUser58.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser58);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(1115)
                .OpenFirstResult();
            PageFactoryManager.Get<BasePage>()
                .SwitchToLastWindow();
            var detailPartyPage = PageFactoryManager.Get<DetailPartyPage>();
            detailPartyPage.WaitForLoadingIconToDisappear();
            detailPartyPage.ClickTabDropDown();
            detailPartyPage.ClickOnElement(detailPartyPage.pricesTab);
            detailPartyPage.WaitForLoadingIconToDisappear();
            PricesTab pricesTab = PageFactoryManager.Get<PricesTab>();
            detailPartyPage.SwitchToFrame(pricesTab.PricesIFrame);
            pricesTab.ClickAddNewPrice(0)
                .EditPriceRecord(0, "Parties Collection", "10", "0")
                .ClickOnElement(pricesTab.ApplyChangesButton);
            pricesTab.WaitForLoadingIconToDisappear()
                .VerifyToastMessage("PriceBook Saved.")
                .ClickRefreshBtn();
            pricesTab.WaitForLoadingIconToDisappear();
            pricesTab.VerifyPriceRecord(0, "Parties Collection", "10", "0");
        }

        [Category("Create price record")]
        [Category("Huong")]
        [Test(Description = "Verify whether the Minimum price value saved on newly created prices from Agreement")]
        public void TC_171_2_Verify_whether_the_Minimum_price_value_saved_on_newly_created_prices_from_Agreement()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser58.UserName)
                .SendKeyToPassword(AutoUser58.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser58);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.Commercial)
                .OpenOption("Agreements")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(163)
                .OpenFirstResult()
                .SwitchToLastWindow();
            var partyAgreementPage = PageFactoryManager.Get<PartyAgreementPage>();
            partyAgreementPage.WaitForLoadingIconToDisappear();
            partyAgreementPage.ClickOnElement(partyAgreementPage.PricesTab);
            partyAgreementPage.WaitForLoadingIconToDisappear();
            PricesTab pricesTab = PageFactoryManager.Get<PricesTab>();
            partyAgreementPage.SwitchToFrame(pricesTab.PricesIFrame);
            pricesTab.EditPriceRecord(0, "Selco Ham Re1", "13", "0")
                .ClickOnElement(pricesTab.ApplyChangesButton);
            pricesTab.WaitForLoadingIconToDisappear()
                .VerifyToastMessage("PriceBook Saved.")
                .ClickRefreshBtn();
            pricesTab.WaitForLoadingIconToDisappear();
            pricesTab.VerifyPriceRecord(3, "Selco Ham Re1", "13", "0");
        }

        [Category("Create price record")]
        [Category("Huong")]
        [Test(Description = "Verify whether the Minimum price value saved on newly created prices from Contracts")]
        public void TC_171_3_Verify_whether_the_Minimum_price_value_saved_on_newly_created_prices_from_Contracts()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser58.UserName)
                .SendKeyToPassword(AutoUser58.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser58);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .OpenOption(Contract.Commercial)
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .WaitForLoadingIconToDisappear();
            PricesTab pricesTab = PageFactoryManager.Get<PricesTab>();
            pricesTab.ClickOnElement(pricesTab.PriceTab);
            pricesTab.WaitForLoadingIconToDisappear();
            pricesTab.SwitchToFrame(pricesTab.PricesIFrame);
            pricesTab.ClickAddNewPrice(0)
                .EditPriceRecord(0, "RMC Collection", "10", "0")
                .ClickOnElement(pricesTab.ApplyChangesButton);
            pricesTab.WaitForLoadingIconToDisappear()
                .VerifyToastMessage("PriceBook Saved.")
                .ClickRefreshBtn();
            pricesTab.WaitForLoadingIconToDisappear();
            pricesTab.VerifyPriceRecord(0, "RMC Collection", "10", "0");
        }
    }
}
