using System.Collections.Generic;
using System.Threading;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.DetailReceipt;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ReceiptTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateReceiptTest : BaseTest
    {
        [Category("102_Create a Receipt")]
        [Test]
        public void TC_102_Create_a_Receipt()
        {
            DetailReceiptModel input = new DetailReceiptModel();
            input.Party = "Jaflong Tandoori";
            input.PaymentMethod = "Credit";
            input.PaymentReference = "payment ref 1";
            input.Notes = "testing Sales Receipt creation";
            input.Value = "5";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser36.UserName, AutoUser36.Password)
                .IsOnHomePage(AutoUser36);
           
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.RMC)
                .OpenOption("Receipts")
                .SwitchNewIFrame();

            PageFactoryManager.Get<DetailReceiptPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailReceiptPage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SalesReceiptPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptPage>()
                .IsInputPartyNameHasError()
                .ClickSaveBtn()
                .VerifyToastMessage("Please select a party");
            PageFactoryManager.Get<SalesReceiptPage>()
                .SearchPartyNameAndSelect("jaflong tandoori")
                .IsInputPartyNameValid()
                .IsAccountRefReadOnly()
                .IsAccountNumberReadOnly()
                .ClickPaymentMethodAndVerifyListMethod()
                .SelectPaymentMethod(input.PaymentMethod)
                .InputPaymentRef(input.PaymentReference)
                .InputNotes(input.Notes)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Sales Receipt")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptPage>()
                .ClickLinesTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptPage>()
                .ClickAddNewLine()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .ClickObjectTypeAndVerifyListType()
                .SelectObjectType("Sales Invoice")
                .InputInvoice("28441")
                .VerifyToastMessage("No data available for the selected type.");
            Thread.Sleep(300);
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .InputInvoice("1")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .NetPriceHasValue()
                .GrossPriceHasValue()
                .VatPriceHasValue()
                .ValuePriceContainValue("0")
                .ClickOnSaveBtn()
                .VerifyToastMessage("Please enter value that's greater than 0.")
                .WaitUntilToastMessageInvisible("Please enter value that's greater than 0.");
            Thread.Sleep(300);
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .InputValuePrice(input.Value)
                .IsReceiptValueDisplay();
            Thread.Sleep(2000);
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .ClickOnSaveBtn()
                .VerifyToastMessage("Sales receipt line can only be created against a posted invoice.");
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.RMC)
                .OpenOption("Sales Invoice Batches")
                .SwitchNewIFrame();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .ClickSalesInvoiceBatches(1)
                .ClickPost()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SalesInvoiceBatchesConfirmPostPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceBatchesConfirmPostPage>()
                .VerifyTextInfo("Do you wish to post the selected sales invoice batch(es) record(s)")
                .ClickYesBtn()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .SwitchNewIFrame();
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .VerifySalesInvoiceBatchesIsPosted(1);
            PageFactoryManager.Get<SalesInvoiceBatchesPage>()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .ClickOnSaveBtn()
                .VerifyToastMessage("Successfully saved Sales ReceiptLine")
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .VerifyAmountOwned("83.2")
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SalesReceiptPage>()
                .VerifySaleReceiptLines()
                .CloseCurrentWindow()
                .SwitchToFirstWindow();

            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.RMC)
                .OpenOption("Receipts")
                .SwitchNewIFrame();
            PageFactoryManager.Get<DetailReceiptPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailReceiptPage>()
                .VerifyDetailReceipt(input)
                .DoubleDetailReceipt()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SalesReceiptPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptPage>()
                .ClickDetailsTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptPage>()
                .VerifyNotDisplayErrorMessage();

            PageFactoryManager.Get<SalesReceiptPage>()
                .ClickLinesTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptPage>()
                .VerifyNotDisplayErrorMessage();
        }
    }
}
