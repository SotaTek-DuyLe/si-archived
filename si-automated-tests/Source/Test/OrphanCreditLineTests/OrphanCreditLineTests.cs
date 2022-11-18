using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccountStatement;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.OrphanCreditLineTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class OrphanCreditLineTests : BaseTest
    {
        [Category("Orphan Credit Line")]
        [Category("Chang")]
        [Test(Description = "Orphan credit lines - checkboxes (bug fix)")]
        public void TC_147_orphan_credit_lines_check_boxes()
        {
            string saleBatchId = "3";
            string status = "POSTED";
            string priceLineId = "693";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser50.UserName, AutoUser50.Password)
                .IsOnHomePage(AutoUser50);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.SalesInvoiceBatches)
                .SwitchNewIFrame();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .IsSalesInvoiceBatchesPage()
                .FilterBySaleInvoiceBatchId(saleBatchId)
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            SalesInvoiceBatchesDetailPage salesInvoiceBatchesDetailPage = PageFactoryManager.Get<SalesInvoiceBatchesDetailPage>();
            salesInvoiceBatchesDetailPage
                .IsSalesInvoiceBatchesDetailPage(status, saleBatchId)
                //Line 9: Click on [Invoices] tab and Double click on any rows
                .ClickOnInvoiceTab()
                .DoubleClickOnAnyInvoiceRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            SalesInvoiceDetailPage salesInvoiceDetailPage = PageFactoryManager.Get<SalesInvoiceDetailPage>();
            salesInvoiceDetailPage
                .IsSaleInvoiceDetailPage(status, saleBatchId)
                //Line 10: Click on [Price line] tab and Double click on price line row
                .ClickOnPriceLineTab()
                .FilterByPriceLineId(priceLineId)
                .ClickOnFirstPriceLineRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PriceLineDetailPage priceLineDetailPage = PageFactoryManager.Get<PriceLineDetailPage>();
            priceLineDetailPage
                .IsPriceLineDetailPage(priceLineId)
                //Line 11: Click on [Mark For Credit] btn
                .ClickOnMarkForCreditBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<CreditNoteLinePage>()
                .IsOnCreditNoteLinePage()
                .VerifyCurrentUrl()
                .ClickCloseBtn()
                .SwitchToChildWindow(4);
            string partyName = priceLineDetailPage
                .VerifyDisplayUnMarkFromCreditBtn()
                .GetPartyName();
            //Line 14: Click on [Party hyperlink] on the header
            priceLineDetailPage
                .ClickOnPartyHyperlinkText()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailPartyPage>()
                .WaitForDetailPartyPageLoadedSuccessfully(partyName)
                //.ClickTabDropDown()
                .ClickOnAccountStatement()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<AccountStatementPage>()
                //Line 15: Click on [Create Credit Note]
                .ClickCreateCreditNote()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            string idCredit = PageFactoryManager.Get<CreditNotePage>()
                .IsPopupCreditNote()
                .GetIdOfFirstRowInPopupCredit();
            PageFactoryManager.Get<CreditNotePage>()
                .ClickOnFirstCreditRow()
                .ClickOnConfirmBtn()
                .WaitForLoadingIconToDisappear();
            //Line 16: Verify
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyCurrenCreditNotetUrl("2", "68")
                //Line 17: Click on [SAVE] and verify
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SaveCreditNoteSuccessMessage);
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyCurrentCreditNoteUrl()
                .VerifyPartyNameUpdated(partyName)
                //Line 18: Click on [Lines] tab
                .ClickOnLinesTab()
                .VerifyRowOfNewLines("WeighbridgeTicket", idCredit, "NEW");

        }

        [Category("Orphan Credit Line")]
        [Category("Chang")]
        [Test(Description = "Crediting invoice lines error ")]
        public void TC_153_crediting_invoice_lines_error()
        {
            string saleBatchId = "5";
            string status = "POSTED";
            
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser50.UserName, AutoUser50.Password)
                .IsOnHomePage(AutoUser50);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.Commercial)
                .OpenOption(MainOption.SalesInvoiceBatches)
                .SwitchNewIFrame();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .IsSalesInvoiceBatchesPage()
                .FilterBySaleInvoiceBatchId(saleBatchId)
                .ClickOnFirstCheckboxAtRow()
                .ClickOnPostBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SalesInvoiceBatchesDetailPage>()
                .IsWariningSaleInvoiceBatchesPopup()
                //Line 8: Click on [Yes] btn
                .ClickOnYesOnWarningPopupBtn()
                .SwitchToChildWindow(1)
                .SwitchNewIFrame();

            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .FilterBySaleInvoiceBatchId(saleBatchId)
                .VerifyStatusOfFirstRecord(status)
                //Line 9: Double click on the row
                .ClickOnFirstRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            SalesInvoiceBatchesDetailPage salesInvoiceBatchesDetailPage = PageFactoryManager.Get<SalesInvoiceBatchesDetailPage>();
            string invoiceId = "218";

            salesInvoiceBatchesDetailPage
                .IsSalesInvoiceBatchesDetailPage(status, saleBatchId)
                //Line 10: Invoices tab -> double click on the row 
                .ClickOnInvoiceTab()
                .FilterByInvoiceId(invoiceId)
                .DoubleClickOnAnyInvoiceRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            SalesInvoiceDetailPage salesInvoiceDetailPage = PageFactoryManager.Get<SalesInvoiceDetailPage>();
            string lineId = "274";
            salesInvoiceDetailPage
                .IsSaleInvoiceDetailPage(status, invoiceId)
                //Line 11: Click on [Lines tab] and double click on any rows
                .ClickOnLinesTab()
                .FilterByLinesId(lineId)
                .ClickOnFirstLinesRecord()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SaleInvoiceLinePage>()
                .IsOnSaleInvoiceLinePage()
                .VerifyDisplayOfMarkInvoiceLineForCreditBtn()
                .ClickOnMarkInvoiceLineForCreditBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNoteLinePage>()
                .IsOnCreditNoteLinePage()
                .VerifyCurrentUrl()
                .ClickCloseBtn()
                .SwitchToChildWindow(4);
            PageFactoryManager.Get<SaleInvoiceLinePage>()
                .VerifyDisplayUnmarkInvoiceLineFromCreditBtn();

        }
    }
}
