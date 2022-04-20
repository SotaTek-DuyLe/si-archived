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
                .Login(AutoUser4.UserName, AutoUser4.Password)
                .IsOnHomePage(AutoUser4);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Accounts")
                .ExpandOption("North Star Commercial")
                .OpenOption("Credit Notes")
                .SwitchNewIFrame();
        }

        [Category("Account")]
        [Test]
        public void TC_78_82()
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
            string batchId = PageFactoryManager.Get<CommonBrowsePage>()
                .GetFirstResultValueOfField("Credit Note Batch #");
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickFirstItem()
                .ClickButton("Add to Batch");
            PageFactoryManager.Get<CreditNoteBatchOptionPage>()
                .ClickExistingBatch()
                .SelectBatchId(batchId);
        }
    }
}
