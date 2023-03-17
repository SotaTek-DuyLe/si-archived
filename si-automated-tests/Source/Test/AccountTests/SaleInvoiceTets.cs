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
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.Commercial);
        }

        [Category("Account")]
        [Category("Dee")]
        [Test]
        public void TC_84_Create_sales_invoice()
        {
            string partyName = "Greggs";
            string lineType = "Commercial Line Type";
            string site = "Greggs - 35 THE QUADRANT, RICHMOND, TW9 1DN";
            string product = "General Refuse";
            string priceElement = "Revenue";
            string quantity = "1";
            string price = "100.00";

            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Sales Invoices")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create");
            PageFactoryManager.Get<CreateInvoicePage>()
                .IsOnCreateInvoicePage()
                .SearchForParty(partyName)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreateInvoicePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<SaleInvoiceLinePage>()
                .IsOnSaleInvoiceLinePage()
                //SelectDepot
                .InputInfo(lineType, site, product, priceElement, quantity, price)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .VerifyLineInfo(partyName, product, product + " {AssetType.LedgerCode},", quantity, price)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickFirstItem()
                .ClickButton("Post");
            PageFactoryManager.Get<PostConfirmationPage>()
                .ClickConfirm()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .SleepTimeInMiliseconds(2500)
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Status", "POSTED");
        }
        [Category("Account")]
        [Category("Dee")]
        [Test]
        public void TC_85_Create_sales_invoice_batch()
        {
            string type = "Credit";
            string customer = "Jaflong Tandoori - AL00000043";
            string method = "Credit/Debit Card";
            string invoiceType = "Monthly in Arrears";
            string dateNextMonth = "01/"+CommonUtil.GetLocalTimeMinusMonth("MM/yyyy", 1);
            string scheduelDate = CommonUtil.GetLocalTimeFromDate(dateNextMonth, "dd/MM/yyyy", -1);
            string currentDateTime = CommonUtil.GetTimeMinusHour(CommonUtil.GetLocalTimeNow("dd/MM/yyyy HH:mm"),"dd/MM/yyyy HH:mm", 1);

            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Sales Invoice Batches")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickButton("Create");
            PageFactoryManager.Get<CreateInvoiceBatchPage>()
                .IsOnBatchPage()
                .SelectInputs(type, customer, method)
                .InputInvoiceSchedule(invoiceType, scheduelDate)
                .InputGenerateDate(currentDateTime)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved sales invoice batch")
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Status", "PENDING")
                .VerifyFirstResultValue("Generation Scheduled Date", currentDateTime)
                .VerifyFirstResultValue("Account Types", type);           


        }
    }
}
