using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.SalesReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SalesReceiptPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='SALES RECEIPT']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By inputPartyName = By.XPath("//div[@id='party-name']//input");
        private readonly By inputAccRef = By.XPath("//input[@id='account-ref']");
        private readonly By inputAccNumber = By.XPath("//input[@id='account-number']");
        private readonly By inputPaymentRef = By.XPath("//input[@id='payment-ref']");
        private readonly By notes = By.XPath("//textarea[@id='notes']");
        private readonly By paymentMethod = By.XPath("//select[@id='payment-method']");
        private readonly By paymentMethodOpt = By.XPath("//select[@id='payment-method']//option");
        private readonly By autoCompleteParty = By.XPath("//div[@id='party-name']//ul//li");
        private readonly By lineTab = By.XPath("//a[@aria-controls='salesReceiptLines-tab']");
        private readonly By detailTab = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By addnewItemBtn = By.XPath("//button[contains(string(), 'Add New Item')]");
        private readonly By lineRows = By.XPath("//div[@class='slick-viewport']//div[@class='grid-canvas']//div[contains(@class,'ui-widget-content')]");
        private const string FrameMessage = "//div[@class='notifyjs-corner']/div";

        //DETAIL
        private readonly By partyInput = By.CssSelector("input[id='party-name']");
        private readonly By accountRef = By.Id("account-ref");
        private readonly By accountNumber = By.Id("account-number");
        private readonly By receiptDate = By.Id("receipt-date");

        [AllureStep]

        public SalesReceiptPage VerifyInfoInSaleReceiptScreen(PartyModel partyModel, string timeNowValue)
        {
            Assert.AreEqual(partyModel.PartyName, GetAttributeValue(partyInput, "value"));
            Assert.AreEqual(partyModel.accountRef, GetAttributeValue(accountRef, "value"));
            Assert.AreEqual(partyModel.accountNumber, GetAttributeValue(accountNumber, "value"));
            Assert.AreEqual(timeNowValue, GetAttributeValue(receiptDate, "value"));
            return this;
        }
        [AllureStep]
        public SalesReceiptPage IsInputPartyNameHasError()
        {
            Thread.Sleep(200);
            IWebElement input = GetElement(inputPartyName);
            string rgb = input.GetCssValue("border-color");
            string[] colorsOnly = rgb.Replace("rgb", "").Replace("(", "").Replace(")", "").Split(',');
            int R = colorsOnly[0].AsInteger();
            int G = colorsOnly[1].AsInteger();
            int B = colorsOnly[2].AsInteger();
            Assert.IsTrue(R > 100 && R > G * 2 && R > B * 2);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage SearchPartyNameAndSelect(string value)
        {
            IWebElement input = GetElement(inputPartyName);
            SendKeys(input, value);
            ClickOnElement(GetAllElements(autoCompleteParty).FirstOrDefault());
            return this;
        }
        [AllureStep]
        public SalesReceiptPage IsInputPartyNameValid()
        {
            IWebElement input = GetElement(inputPartyName);
            string borderColor = input.GetCssValue("border-color");
            Assert.AreNotEqual("rgb(169, 68, 66)", borderColor);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage IsAccountRefReadOnly()
        {
            IWebElement input = GetElement(inputAccRef);
            Assert.IsNotNull(input.GetAttribute("readonly"));
            return this;
        }
        [AllureStep]
        public SalesReceiptPage IsAccountNumberReadOnly()
        {
            IWebElement input = GetElement(inputAccNumber);
            Assert.IsNotNull(input.GetAttribute("readonly"));
            return this;
        }
        [AllureStep]
        public SalesReceiptPage ClickPaymentMethodAndVerifyListMethod()
        {
            ClickOnElement(paymentMethod);
            Thread.Sleep(100);
            List<string> paymentmethods = GetAllElements(paymentMethodOpt).Select(x => GetElementText(x)).Where(x => x != "Select...").ToList();
            Assert.AreEqual(new List<string>() { "Bank Transfer", "Credit", "Credit/Debit Card", "Direct Debit" },paymentmethods);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage SelectPaymentMethod(string option)
        {
            SelectTextFromDropDown(paymentMethod, option);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage InputPaymentRef(string value)
        {
            SendKeys(inputPaymentRef, value);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage InputNotes(string value)
        {
            SendKeys(notes, value);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage ClickLinesTab()
        {
            ClickOnElement(lineTab);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage ClickDetailsTab()
        {
            ClickOnElement(detailTab);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage ClickAddNewLine()
        {
            ClickOnElement(addnewItemBtn);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage CLickOnSaveBtn()
        {
            ClickOnElement(saveBtn);
            return this;
        }
        [AllureStep]
        public SalesReceiptPage VerifySaleReceiptLines()
        {
            SalesReceiptLineModel salesReceiptLine = GetAllSalesReceiptLineModel().FirstOrDefault();
            Assert.IsTrue(salesReceiptLine.TargetType == "SalesInvoice");
            Assert.IsTrue(salesReceiptLine.TargetId == "1");
            Assert.IsTrue(salesReceiptLine.Site == "Jaflong Tandoori");
            Assert.IsTrue(salesReceiptLine.Value == "£5.00");
            return this;
        }
        [AllureStep]
        public List<SalesReceiptLineModel> GetAllSalesReceiptLineModel()
        {
            List<SalesReceiptLineModel> receiptLines = new List<SalesReceiptLineModel>();
            List<IWebElement> rows = GetAllElements(lineRows);
            foreach (var row in rows)
            {
                IWebElement idCell = row.FindElement(By.XPath("./div[contains(@class,'l1')]"));
                IWebElement targetTypeCell = row.FindElement(By.XPath("./div[contains(@class,'l2')]"));
                IWebElement targetIdCell = row.FindElement(By.XPath("./div[contains(@class,'l3')]"));
                IWebElement siteCell = row.FindElement(By.XPath("./div[contains(@class,'l4')]"));
                IWebElement valueCell = row.FindElement(By.XPath("./div[contains(@class,'l6')]"));
                receiptLines.Add(new SalesReceiptLineModel() 
                { 
                    Id = GetElementText(idCell),
                    TargetType = GetElementText(targetTypeCell),
                    TargetId = GetElementText(targetIdCell),
                    Site = GetElementText(siteCell),
                    Value = GetElementText(valueCell),
                });
            }
            return receiptLines;
        }
        [AllureStep]
        public SalesReceiptPage VerifyNotDisplayErrorMessage()
        {
            Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
            return this;
        }
    }
}
