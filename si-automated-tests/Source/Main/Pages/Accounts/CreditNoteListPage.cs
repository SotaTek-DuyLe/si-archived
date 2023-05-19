using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreditNoteListPage : BasePageCommonActions
    {
        private string CreditNoteTable = "//div[@class='grid-canvas']";
        private string CreditNoteRow = "./div[contains(@class, 'slick-row')]";
        private string CheckboxCell = "./div[contains(@class, 'l0')]//input";
        private string IdCell = "./div[contains(@class, 'l1')]";
        private string CreditDateCell = "./div[contains(@class, 'l2')]";
        private string PartyCell = "./div[contains(@class, 'l3')]";
        private string AccountingCell = "./div[contains(@class, 'l4')]";
        private string ClientRefCell = "./div[contains(@class, 'l5')]";
        private string CreditStatusCell = "./div[contains(@class, 'l6')]";

        public TableElement CreditNoteTableEle
        {
            get => new TableElement(CreditNoteTable, CreditNoteRow, new System.Collections.Generic.List<string>() { CheckboxCell, IdCell, CreditDateCell, PartyCell, AccountingCell, ClientRefCell, CreditStatusCell });
        }

        public CreditNoteListPage VerifyCreditNoteStatus(string id, List<string> status)
        {
            var cell = CreditNoteTableEle.GetCellByCellValues(6, new System.Collections.Generic.Dictionary<int, object>() { { 1, id } });
            Assert.IsTrue(status.Contains(cell.Text));
            return this;
        }
    }
}
