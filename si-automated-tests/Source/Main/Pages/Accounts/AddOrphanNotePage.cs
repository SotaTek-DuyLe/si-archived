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
        private string creditNoteTable = "//div[@id='add-orphan-notes']//tbody[@data-bind='foreach: creditNotes']";
        private string creditNoteRow = "./tr";
        private string checkboxCell = "./td//input";
        private string noteIdCell = "./td[@data-bind='text: noteId']";
        private string creditDateCell = "./td[@data-bind='text: creditDate']";
        private string partyCell = "./td[@data-bind='text: party']";
        private string accountCell = "./td[@data-bind='text: accountRef']";
        private string accountNumberCell = "./td[@data-bind='text: accountNumber']";
        private string netCell = "./td[@data-bind='text: net']";

        public TableElement CreditNoteTableEle
        {
            get => new TableElement(creditNoteTable, creditNoteRow, new System.Collections.Generic.List<string>() { checkboxCell, noteIdCell, creditDateCell, partyCell, accountCell, accountNumberCell, netCell });
        }

        public AddOrphanNotePage SelectCreditNote()
        {
            CreditNoteTableEle.ClickCell(0, 0);
            return this;
        }

        public AddOrphanNotePage VerifyNetValueHasValueGreaterThanZero()
        {
            double netVal = CreditNoteTableEle.GetCellValue(0, CreditNoteTableEle.GetCellIndex(netCell)).AsDouble();
            Assert.IsTrue(netVal > 0);
            return this;
        }
    }
}
