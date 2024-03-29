﻿using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Accounts;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using static si_automated_tests.Source.Main.Models.UserRegistry;
namespace si_automated_tests.Source.Test.AccountTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreditNotesTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO DEFAULT ALLOCATION
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser21.UserName, AutoUser21.Password)
                .IsOnHomePage(AutoUser21);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.Commercial);
        }

        [Category("Account")]
        [Category("Dee")]
        [Test]
        public void TC_78_82_Create_credit_notes_and_add_to_note_batch()
        {
            string partyName = "Greggs";
            string lineType = "Commercial Line Type";
            string site = "Greggs - 35 THE QUADRANT, RICHMOND, TW9 1DN";
            string product = "General Refuse";
            string priceElement = "Revenue";
            string description = "test description no." + CommonUtil.GetRandomNumber(5);
            string quantity = "1";
            string price = "100.00";
            string notes = "test note" + CommonUtil.GetRandomString(5);
            PageFactoryManager.Get<NavigationBase>()
                .OpenLastOption("Credit Notes")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //Create credit note 1
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNotePage>()
                .IsOnCreditNotePage()
                .SearchForParty(partyName)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNoteLinePage>()
                .IsOnCreditNoteLinePage()
                .SelectDepot(Contract.Commercial)
                .InputInfo(lineType, site, product, priceElement, description, quantity, price, price)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .VerifyLineInfo(partyName, product, description, quantity, price)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            string firstCreditId = PageFactoryManager.Get<CommonBrowsePage>()
                .GetFirstResultValueOfField("ID");

             //TC-82
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickFirstItem()
                .ClickButton("Add to Batch");
            PageFactoryManager.Get<CreditNoteBatchOptionPage>()
                .ClickNewBatch()
                .InputNotes(notes)
                .ClickSaveBtn()
                .SleepTimeInMiliseconds(3000)
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            //Verify first credit note has batchId
            string batchId = PageFactoryManager.Get<CommonBrowsePage>()
                .GetFirstResultValueOfField("Credit Note Batch #");

            //Create credit note 2
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNotePage>()
                .IsOnCreditNotePage()
                .SearchForParty(partyName)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNoteLinePage>()
                .IsOnCreditNoteLinePage()
                .SelectDepot(Contract.Commercial)
                .InputInfo(lineType, site, product, priceElement, description, quantity, price, price)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .VerifyLineInfo(partyName, product, description, quantity, price)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();

            string secondCreditId = PageFactoryManager.Get<CommonBrowsePage>()
                .GetFirstResultValueOfField("ID");

            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickFirstItem()
                .ClickButton("Add to Batch");
            PageFactoryManager.Get<CreditNoteBatchOptionPage>()
                .ClickExistingBatch()
                .SelectBatchId(batchId)
                .SleepTimeInMiliseconds(2000)
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Credit Note Batch #", batchId);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .OpenOption("Credit Note Batches")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .OpenFirstResult();

            //Verify credit notes ids are included in credit note batch
            PageFactoryManager.Get<CreditNoteBatchPage>()
                .SwitchToCreditNotesTab()
                .VerifyFirstCreditNoteId(secondCreditId)
                .VerifySecondCreditNoteId(firstCreditId);

        }
        [Category("Account")]
        [Category("Dee")]
        [Test]
        public void TC_144_()
        {
            string partyName = "Greggs";
            string lineType = "Commercial Line Type";
            string site = "Greggs - 35 THE QUADRANT, RICHMOND, TW9 1DN";
            string product = "General Refuse";
            string priceElement = "Revenue";
            string description = "test description no." + CommonUtil.GetRandomNumber(5);
            string quantity = "1";
            string price = "100.00";
            string vat = "20";
            string notes = "test note" + CommonUtil.GetRandomString(5);
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Credit Notes")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //Create credit note 1
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNotePage>()
                .IsOnCreditNotePage()
                .SearchForParty(partyName)
                .WaitForLoadingIconToDisappear() 
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNoteLinePage>()
                .IsOnCreditNoteLinePage()
                .SelectDepot(Contract.Commercial)
                .InputInfo(lineType, site, product, priceElement, description, quantity, price, price)
                .SelectVatRate(vat)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .VerifyLineInfo(partyName, product, description, quantity, vat, price)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
        }
        [Category("Account")]
        [Category("Dee")]
        [Test]
        public void TC_145_Rejecting_credit_note()
        {
            string partyName = "Greggs";
            string notes = "test note" + CommonUtil.GetRandomString(5);
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Credit Notes")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //Create credit note 1
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNotePage>()
                .IsOnCreditNotePage()
                .SearchForParty(partyName)
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickRefreshBtn();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Credit Status", "NEW")
                .OpenFirstResult()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNotePage>()
                .ClickRejectButton();
            PageFactoryManager.Get<RejectionPopup>()
                .VerifyOptionNumber(3)
                .SelectRejectReasonFromDropDown("Disputed")
                .InputRejectReason(notes)
                .ClickConfirmReject()
                .ClickRefreshBtn();
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyRejectButtonDisabled()
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .ClickRefreshBtn();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Credit Status", "REJECTED");
        }

        [Category("Account")]
        [Category("Huong")]
        [Test(Description = "Add Orphan Credit Notes to batch shows Rejected credit notes")]
        public void TC_187_Add_Orphan_Credit_Notes_to_batch_shows_Rejected_credit_notes()
        {
            //Verify that rejected credit note is not included in orphan credit notes
            PageFactoryManager.Get<NavigationBase>()
                .OpenLastOption("Credit Note Batches")
                .SwitchNewIFrame();
            CreditNoteBatchListPage creditNoteBatchListPage = PageFactoryManager.Get<CreditNoteBatchListPage>();
            creditNoteBatchListPage.WaitForLoadingIconToDisappear();
            creditNoteBatchListPage.ClickCreditNote("NEW")
                .ClickOnElement(creditNoteBatchListPage.AddOrphanCreditNoteButton);
            creditNoteBatchListPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            AddOrphanNotePage addOrphanNotePage = PageFactoryManager.Get<AddOrphanNotePage>();
            addOrphanNotePage.VerifyNetValueHasValueGreaterThanZero()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Accounts)
                .ExpandOption(Contract.Commercial)
                .OpenLastOption("Credit Notes")
                .SwitchNewIFrame();
            CreditNoteListPage creditNoteListPage = PageFactoryManager.Get<CreditNoteListPage>();
            creditNoteListPage.WaitForLoadingIconToDisappear();
            creditNoteListPage.VerifyCreditNoteStatus("2", new System.Collections.Generic.List<string>() { "NEW", "APPROVED" });
        }

        [Category("Account")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_323_1_Update_validations_and_errors_in_the_Sales_Invoice_and_Credit_Line_Forms()
        {
            //Verify whether user able to view new form to add Credit Note Line
            string partyName = "Greggs";
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Credit Notes")
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            //Create credit note 1
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNotePage>()
                .IsOnCreditNotePage()
                .SearchForParty(partyName)
                .WaitForLoadingIconToDisappear()
                .ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();

            //Check if all values 0, or Net is 0 and (Price or Quantity is 0)
            string lineType = "Commercial Line Type";
            string site = "Greggs - 35 THE QUADRANT, RICHMOND, TW9 1DN";
            string product = "General Refuse";
            string priceElement = "Revenue";
            string description = "test description no." + CommonUtil.GetRandomNumber(5);
            string quantity = "0";
            string price = "0";
            string vat = "0";
            PageFactoryManager.Get<CreditNoteLinePage>()
               .IsOnCreditNoteLinePage()
               .SelectDepot(Contract.Commercial)
               .InputInfo(lineType, site, product, priceElement, description, quantity, price, price)
               .ClickSaveBtn()
               .VerifyToastMessage("Net is required");

            //Check if user enter negative number or number with more than 2 decimals to Net field
            quantity = "10";
            string net = "12.345";
            PageFactoryManager.Get<CreditNoteLinePage>()
              .InputInfo(lineType, site, product, priceElement, description, quantity, price, net)
              .ClickSaveBtn()
              .VerifyToastMessage("Price or Net must be a positive number up to 2 decimal points.");

            //Check if user enter negative number or number with more than 2 decimals to Price field
            quantity = "10";
            net = "12.35";
            price = "12.345";
            PageFactoryManager.Get<CreditNoteLinePage>()
              .InputInfo(lineType, site, product, priceElement, description, quantity, price, net)
              .ClickSaveBtn()
              .VerifyToastMessage("Price or Net must be a positive number up to 2 decimal points.");

            //Check if user enter an invalid value to Quantity field (like 78789.78.78)
            quantity = "78789.78.78";
            price = "12.35";
            PageFactoryManager.Get<CreditNoteLinePage>()
              .InputInfo(lineType, site, product, priceElement, description, quantity, price, net)
              .ClickSaveBtn()
              .VerifyToastMessage("Invalid quantity");
        }

        [Category("Account")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_323_2_Update_validations_and_errors_in_the_Sales_Invoice_and_Credit_Line_Forms()
        {
            //Verify whether user able to view new form to add SalesInvoice Line
            string partyName = "Greggs";
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

            string lineType = "Commercial Line Type";
            string site = "Greggs - 35 THE QUADRANT, RICHMOND, TW9 1DN";
            string product = "General Refuse";
            string priceElement = "Revenue";
            string quantity = "12.111";
            string price = "100.00";
            PageFactoryManager.Get<SaleInvoiceLinePage>()
                .IsOnSaleInvoiceLinePage()
                .SelectDepot(Contract.Commercial)
                .InputInfo(lineType, site, product, priceElement, quantity, price)
                .ClickSaveBtn()
                .VerifyToastMessage("Invalid quantity");

            //Check if user enter negative number or number with more than 2 decimals to Price field
            price = "100.123";
            quantity = "12";
            PageFactoryManager.Get<SaleInvoiceLinePage>()
             .InputInfo(lineType, site, product, priceElement, quantity, price)
             .ClickSaveBtn()
             .VerifyToastMessage("Price or Net must be a positive number up to 2 decimal points.");

            //Check if user enter negative number or number with more than 2 decimals to Net field
            price = "10";
            string net = "12.345";
            PageFactoryManager.Get<SaleInvoiceLinePage>()
             .InputInfo(lineType, site, product, priceElement, quantity, price, net)
             .ClickSaveBtn()
             .VerifyToastMessage("Price or Net must be a positive number up to 2 decimal points.");

            //Check if user enter an invalid value to Quantity field (like 78789.78.78)
            quantity = "78789.78.78";
            net = "12.35";
            PageFactoryManager.Get<SaleInvoiceLinePage>()
              .InputInfo(lineType, site, product, priceElement, quantity, price, net)
              .ClickSaveBtn()
              .VerifyToastMessage("Invalid quantity");

            //Check if all values 0, or Net is 0 and (Price or Quantity is 0)
            PageFactoryManager.Get<SaleInvoiceLinePage>()
              .InputInfo(lineType, site, product, priceElement, "0", "0", "0")
              .ClickSaveBtn()
              .VerifyToastMessage(MessageSuccessConstants.SuccessMessage);
        }
    }
}
