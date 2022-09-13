using System;
using System.Collections.Generic;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccountStatement;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.AccountStatementTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class TakePaymentTests : BaseTest
    {
        [Category("Billing rule")]
        [Category("Chang")]
        [Test(Description = "Billing rule in agreement line")]
        public void TC_155_account_statement_take_payment()
        {
            string partyId = "1131";
            string partyName = "Ham Food Centre";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .SendKeyToUsername(AutoUser48.UserName)
                .SendKeyToPassword(AutoUser48.Password)
                .ClickOnSignIn();
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser48);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Parties)
                .ExpandOption(Contract.RMC)
                .OpenOption(MainOption.Parties)
                .SwitchNewIFrame();
            List<PartyModel> partyModels = PageFactoryManager.Get<PartyCommonPage>()
                .FilterPartyById(partyId)
                .GetAllPartyListing();
            PageFactoryManager.Get<PartyCommonPage>()
                .OpenFirstResult()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                .ClickTabDropDown()
                .ClickOnAccountStatement()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AccountStatementPage>()
                //Line: Click [Take payment] btn
                .ClickTakePayment()
                .SwitchToLastWindow();
            string timeNow = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT);
            PageFactoryManager.Get<SalesReceiptPage>()
                .VerifyInfoInSaleReceiptScreen(partyModels[0], timeNow);

        }
    }
}
