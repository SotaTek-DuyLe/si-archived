using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.DetailReceipt;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ReceiptTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreateReceiptTest : BaseTest
    {
        [Category("102_Create a Receipt")]
        [Category("Huong")]
        [Test]
        public void TC_102_Create_a_Receipt()
        {
            DetailReceiptModel input = new DetailReceiptModel();
            input.Party = "Chicken City";
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
                .ExpandOption(Contract.Commercial)
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
                .SearchPartyNameAndSelect("chicken city")
                .IsInputPartyNameValid()
                .IsAccountRefReadOnly()
                .IsAccountNumberReadOnly()
                .ClickPaymentMethodAndVerifyListMethod()
                .SelectPaymentMethod(input.PaymentMethod)
                .InputPaymentRef(input.PaymentReference)
                .InputNotes(input.Notes)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();

            //Navigate to Lines tab
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
            // In 'Invoice ID' enter '3'
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .InputInvoice("3")
                .WaitForLoadingIconToDisappear();

            //Click 'Save' on the Sales Receipt Line
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

            //Click 'Save' on the SRL
            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .ClickOnSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);

            PageFactoryManager.Get<SalesReceiptLinesPage>()
                .ClickCloseBtn()
                .SwitchToChildWindow(2);

            PageFactoryManager.Get<SalesReceiptPage>()
                .ClickLinesTab()
                .WaitForLoadingIconToDisappear()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<SalesReceiptLinesPage>().VerifyLine("SalesInvoice", "3", input.Party);
            PageFactoryManager.Get<SalesReceiptPage>()
                .ClickCloseBtn()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<DetailReceiptPage>()
                .ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<DetailReceiptPage>()
                .VerifyReceipt(input.Party, input.PaymentMethod, input.PaymentReference);
        }
    }
}
