using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class AddOrphanNotePage : BasePageCommonActions
    {
        public readonly By ConfirmButton = By.XPath("//button[text()='Confirm']");
        public readonly By CancelButton = By.XPath("//button[text()='Cancel']");
        private string CreditNoteTable = "//div[@id='add-orphan-notes']//tbody[@data-bind='foreach: creditNotes']";
        private string CreditNoteRow = "./tr";
        private string CheckboxCell = "./td//input";
        private string NoteIdCell = "./td";
        private string CreditDateCell = "./td";
        private string PartyCell = "./td";
        private string AccountCell = "./td";
        private string AccountNumberCell = "./td";
        private string NetCell = "./td";

        public TableElement CreditNoteTableEle
        {
            get => new TableElement(CreditNoteTable, CreditNoteRow, new System.Collections.Generic.List<string>() { CheckboxCell, NoteIdCell, CreditDateCell, PartyCell, AccountCell, AccountNumberCell, NetCell });
        }

        public AddOrphanNotePage SelectCreditNote()
        {
            CreditNoteTableEle.ClickCell(0, 0);
            return this;
        }

        public AddOrphanNotePage VerifyNetValueHasValueGreaterThanZero()
        {
            int netVal = CreditNoteTableEle.GetCellValue(0, 6).AsInteger();
            Assert.IsTrue(netVal > 0);
            return this;
        }
    }
}
