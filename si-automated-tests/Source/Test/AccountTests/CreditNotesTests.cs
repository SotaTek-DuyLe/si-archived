using System;
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
                .ClickMainOption("Accounts")
                .ExpandOption(Contract.NSC);
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
                .SwitchNewIFrame();
            //Create credit note 1
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNotePage>()
                .IsOnCreditNotePage()
                .SearchForParty(partyName)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Credit Note");
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNoteLinePage>()
                .IsOnCreditNoteLinePage()
                .InputInfo(lineType, site, product, priceElement, description, quantity, price)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Credit Note Line")
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
                .VerifyToastMessage("Successfully saved Credit Note");
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNoteLinePage>()
                .IsOnCreditNoteLinePage()
                .InputInfo(lineType, site, product, priceElement, description, quantity, price)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Credit Note Line")
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
                .ClickMainOption("Accounts")
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
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Credit Note");
            PageFactoryManager.Get<CreditNotePage>()
                .VerifyNewTabsArePresent()
                .SwitchToTab("Lines");
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreditNoteLinePage>()
                .IsOnCreditNoteLinePage()
                .InputInfo(lineType, site, product, priceElement, description, quantity, price)
                .SelectVatRate(vat)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Credit Note Line")
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<LinesTab>()
                .IsOnLinesTab()
                .VerifyLineInfo(partyName, product, description, quantity, vat, price)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();

        }
    }
}
