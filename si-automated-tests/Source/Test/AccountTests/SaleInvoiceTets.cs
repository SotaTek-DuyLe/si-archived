using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.Common;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.AccountTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class SaleInvoiceTests : BaseTest
    {

        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO DEFAULT ALLOCATION
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser24.UserName, AutoUser24.Password)
                .IsOnHomePage(AutoUser24);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Accounts")
                .ExpandOption("North Star Commercial")
                .OpenOption("Sales Invoices");
        }

        [Category("Account")]
        [Test]
        public void TC_84()
        {
            string partyName = "Greggs";
            string lineType = "Commercial Line Type";
            string site = "Greggs - 35 THE QUADRANT, RICHMOND, TW9 1DN";
            string product = "General Refuse";
            string priceElement = "Revenue";
            string quantity = "1";
            string price = "100.00";

            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create");
            PageFactoryManager.Get<CreateInvoicePage>()
                .IsOnCreateInvoicePage()
                .SearchForParty(partyName)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Sales Invoice");
            PageFactoryManager.Get<CreateInvoicePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SaleInvoiceLinePage>()
                .IsOnSaleInvoiceLinePage()
                .InputInfo(lineType, site, product, priceElement, quantity, price)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Sales Invoice Line")
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .VerifyLineInfo(partyName, product, product, quantity, price)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickFirstItem()
                .ClickButton("Post");
            PageFactoryManager.Get<PostConfirmationPage>()
                .ClickConfirm()
                .VerifyToastMessage("Success")
                .SleepTimeInMiliseconds(1500);
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Status", "POSTED");
        }
    }
}
