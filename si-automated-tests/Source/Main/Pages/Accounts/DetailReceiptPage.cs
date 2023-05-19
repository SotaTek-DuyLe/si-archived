using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.DetailReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class DetailReceiptPage : BasePageCommonActions
    {
        private readonly By addnewItemBtn = By.XPath("//button[contains(string(), 'Add New Item')]");
        private readonly By receiptRows = By.XPath("//div[@class='slick-viewport']//div[@class='grid-canvas']//div[contains(@class,'ui-widget-content')]");

        private string ReceiptTable = "//div[@class='grid-canvas']";
        private string ReceiptRow = "./div[contains(@class, 'slick-row')]";
        private string ReceiptCheckboxCell = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        private string ReceiptPartyCell = "./div[contains(@class, 'slick-cell l3 r3')]";
        private string ReceiptPaymentMethodCell = "./div[contains(@class, 'slick-cell l4 r4')]";
        private string PaymentRefCell = "./div[contains(@class, 'slick-cell l5 r5')]";
        public TableElement ReceiptTableEle
        {
            get => new TableElement(ReceiptTable, ReceiptRow, new List<string> { ReceiptCheckboxCell, ReceiptPartyCell, ReceiptPaymentMethodCell, PaymentRefCell });
        }

        [AllureStep]
        public DetailReceiptPage VerifyReceipt(string party, string paymentMethod, string paymentRef)
        {
            VerifyCellValue(ReceiptTableEle, 0, ReceiptTableEle.GetCellIndex(ReceiptPartyCell), party);
            VerifyCellValue(ReceiptTableEle, 0, ReceiptTableEle.GetCellIndex(ReceiptPaymentMethodCell), paymentMethod);
            VerifyCellValue(ReceiptTableEle, 0, ReceiptTableEle.GetCellIndex(PaymentRefCell), paymentRef);
            return this;
        }

        [AllureStep]
        public DetailReceiptPage ClickAddNewItem()
        {
            ClickOnElement(addnewItemBtn);
            return this;
        }
        [AllureStep]

        public DetailReceiptPage VerifyDetailReceipt(DetailReceiptModel inputDetailReceipt)
        {
            DetailReceiptModel detailReceiptLine = GetAllDetailReceiptModel().FirstOrDefault();
            Assert.IsTrue(detailReceiptLine.Party == inputDetailReceipt.Party);
            Assert.IsTrue(detailReceiptLine.PaymentMethod == inputDetailReceipt.PaymentMethod);
            Assert.IsTrue(detailReceiptLine.PaymentReference == inputDetailReceipt.PaymentReference);
            Assert.IsTrue(detailReceiptLine.Notes == inputDetailReceipt.Notes);
            Assert.IsTrue(detailReceiptLine.Value == inputDetailReceipt.Value);
            return this;
        }
        [AllureStep]
        public DetailReceiptPage DoubleDetailReceipt()
        {
            List<IWebElement> rows = GetAllElements(receiptRows);
            foreach (var row in rows)
            {
                DoubleClickOnElement(row);
                break;
            }
            return this;
        }
        [AllureStep]
        public List<DetailReceiptModel> GetAllDetailReceiptModel()
        {
            List<DetailReceiptModel> receiptLines = new List<DetailReceiptModel>();
            List<IWebElement> rows = GetAllElements(receiptRows);
            foreach (var row in rows)
            {
                IWebElement idCell = row.FindElement(By.XPath("./div[contains(@class,'l1')]"));
                IWebElement partyCell = row.FindElement(By.XPath("./div[contains(@class,'l3')]"));
                IWebElement paymentMethodCell = row.FindElement(By.XPath("./div[contains(@class,'l4')]"));
                IWebElement paymentReferenceCell = row.FindElement(By.XPath("./div[contains(@class,'l5')]"));
                IWebElement notesCell = row.FindElement(By.XPath("./div[contains(@class,'l6')]"));
                IWebElement valueCell = row.FindElement(By.XPath("./div[contains(@class,'l7')]"));
                receiptLines.Add(new DetailReceiptModel()
                {
                    Id = GetElementText(idCell),
                    Party = GetElementText(partyCell),
                    PaymentMethod = GetElementText(paymentMethodCell),
                    PaymentReference = GetElementText(paymentReferenceCell),
                    Notes = GetElementText(notesCell),
                    Value = GetElementText(valueCell).Replace("£", "").Replace(".00", ""),
                });
            }
            return receiptLines;
        }
    }
}
