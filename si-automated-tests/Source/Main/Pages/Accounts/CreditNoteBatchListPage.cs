using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreditNoteBatchListPage : BasePageCommonActions
    {
        public readonly By AddOrphanCreditNoteButton = By.XPath("//button[text()='Add Orphan Credit Notes']");
        private string CreditNodeTable = "//div[@class='grid-canvas']";
        private string CreditNodeRow = "./div[contains(@class, 'slick-row')]";
        private string CheckboxCell = "./div[contains(@class, 'l0')]//input";
        private string IdCell = "./div[contains(@class, 'l1')]";
        private string CreditDateCell = "./div[contains(@class, 'l2')]";
        private string PostDateCell = "./div[contains(@class, 'l3')]";
        private string StatusCell = "./div[contains(@class, 'l4')]";

        public TableElement CreditNoteTableEle
        {
            get => new TableElement(CreditNodeTable, CreditNodeRow, new System.Collections.Generic.List<string>() { CheckboxCell, IdCell, CreditDateCell, PostDateCell, StatusCell });
        }

        public CreditNoteBatchListPage ClickCreditNote(string status)
        {
            CreditNoteTableEle.ClickCellOnCellValue(1, 4, status);
            return this;
        }
    }
}
