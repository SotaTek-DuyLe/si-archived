using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class SalesReceiptLinesPage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//h4[text()='SALES RECEIPT LINE']");
        private readonly By objectType = By.XPath("//select[@id='echo-type']");
        private readonly By objectTypeOpt = By.XPath("//select[@id='echo-type']//option");
        private readonly By inputInvoice = By.XPath("//input[@id='echo-id']");
        private readonly By netPrice = By.XPath("//input[@id='net-value']");
        private readonly By vatPrice = By.XPath("//input[@id='vat-charge']");
        private readonly By grossPrice = By.XPath("//input[@id='gross-value']");
        private readonly By valuePrice = By.XPath("//input[@id='value']");
        private readonly By amountOwedPrice = By.XPath("//input[@id='amount-owed']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By receiptValue = By.XPath("//h5[@title='Receipt Value']");

        private string LineTable = "//div[@id='salesReceiptLines-tab']//div[@class='grid-canvas']";
        private string LineRow = "./div[contains(@class, 'slick-row')]";
        private string LineCheckboxCell = "./div[@class='slick-cell l0 r0']//input";
        private string LineTargetTypeCell = "./div[@class='slick-cell l2 r2']";
        private string LineTargetIdCell = "./div[@class='slick-cell l3 r3']";
        private string SiteCell = "./div[@class='slick-cell l4 r4']";
        public TableElement LineTableEle
        {
            get => new TableElement(LineTable, LineRow, new List<string> { LineCheckboxCell, LineTargetTypeCell, LineTargetIdCell, SiteCell });
        }

        [AllureStep]
        public SalesReceiptLinesPage VerifyLine(string targetType, string targetId, string site)
        {
            VerifyCellValue(LineTableEle, 0, LineTableEle.GetCellIndex(LineTargetTypeCell), targetType);
            VerifyCellValue(LineTableEle, 0, LineTableEle.GetCellIndex(LineTargetIdCell), targetId);
            VerifyCellValue(LineTableEle, 0, LineTableEle.GetCellIndex(SiteCell), site);
            return this;
        }

        [AllureStep]
        public SalesReceiptLinesPage ClickObjectTypeAndVerifyListType()
        {
            ClickOnElement(objectType);
            Thread.Sleep(100);
            List<string> types = GetAllElements(objectTypeOpt).Select(x => GetElementText(x)).Where(x => x != "Select...").ToList();
            Assert.AreEqual(new List<string>() { "Weighbridge Ticket", "Sales Invoice" }, types);
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage SelectObjectType(string option)
        {
            SelectTextFromDropDown(objectType, option);
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage InputInvoice(string value)
        {
            SendKeys(inputInvoice, value);
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage NetPriceHasValue()
        {

            int i = 5;
            while (i > 0)
            {
                if (GetInputValue(netPrice).Equals(""))
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else { break; }
            }
            Assert.IsNotEmpty(GetInputValue(netPrice));
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage VatPriceHasValue()
        {
            int i = 5;
            while (i > 0)
            {
                if (GetInputValue(vatPrice).Equals(""))
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else { break; }
            }
            Assert.IsNotEmpty(GetInputValue(vatPrice));
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage GrossPriceHasValue()
        {
            int i = 5;
            while (i > 0)
            {
                if (GetInputValue(grossPrice).Equals(""))
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else { break; }
            }
            Assert.IsNotEmpty(GetInputValue(grossPrice));
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage ValuePriceContainValue(string value)
        {
            Assert.IsTrue(GetInputValue(valuePrice) == value);
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage InputValuePrice(string value)
        {
            SendKeys(valuePrice, value);
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage IsReceiptValueDisplay()
        {
            WaitUtil.WaitForElementVisible(receiptValue);
            Assert.IsTrue(IsControlDisplayed(receiptValue));
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage VerifyAmountOwned(string value)
        {
            Assert.IsTrue(GetInputValue(amountOwedPrice) == value);
            return this;
        }
        [AllureStep]
        public SalesReceiptLinesPage ClickOnSaveBtn()
        {
            ClickOnElement(saveBtn);
            return this;
        }

        [AllureStep]
        public SalesReceiptLinesPage IsSalesReceiptLinesPage(string objectTypeValue, string invoiceId)
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.AreEqual(objectTypeValue, GetFirstSelectedItemInDropdown(objectType));
            Assert.AreEqual(invoiceId, GetAttributeValue(inputInvoice, "value"));
            return this;
        }


    }
}
