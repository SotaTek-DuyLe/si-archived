using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreateInvoiceBatchPage : BasePage
    {
        private readonly By typeInput = By.XPath("//button[@data-id='account-type']");
        private readonly By customerInput = By.XPath("//button[@data-id='services-list']");
        private readonly By paymentMethodInput = By.XPath("//button[@data-id='payment-method']");
        private readonly By addBtn = By.XPath("//button[contains(text(),'Add')]");
        private readonly By invoiceScheduleSelect = By.XPath("//select[contains(@data-bind,'invoiceScheduleOptions')]");
        private readonly By toPeriodSelect = By.XPath("//select[contains(@data-bind,'toPeriod')]");
        private readonly By fromPeriodSelect = By.XPath("//select[contains(@data-bind,'fromPeriod')]");
        private readonly By maxBillDateInput = By.Id("max-bill-date");
        private readonly By generateDateInput = By.Id("generate-date");

        private readonly String dropDownOption = "//ul[@aria-expanded='true']//span[@class='text' and text()='{0}']";

        public CreateInvoiceBatchPage()
        {
            SwitchToLastWindow();
        }
        public CreateInvoiceBatchPage IsOnBatchPage()
        {
            WaitUtil.WaitForElementVisible(typeInput);
            WaitUtil.WaitForElementVisible(customerInput);
            WaitUtil.WaitForElementVisible(paymentMethodInput);
            return this;
        }
        public CreateInvoiceBatchPage SelectAccountType(string type)
        {
            ClickOnElement(typeInput);
            ClickOnElement(dropDownOption, type);
            return this;
        }
        public CreateInvoiceBatchPage SelectCustomer(string customer)
        {
            ClickOnElement(customerInput);
            ClickOnElement(dropDownOption, customer);
            return this;
        }
        public CreateInvoiceBatchPage SelectPaymentMethod(string method)
        {
            ClickOnElement(paymentMethodInput);
            ClickOnElement(dropDownOption, method);
            return this;
        }
        public CreateInvoiceBatchPage SelectInputs(string type, string customer, string method)
        {
            SelectAccountType(type);
            SelectCustomer(customer);
            SelectPaymentMethod(method);
            return this;
        }
        public CreateInvoiceBatchPage ClickAddBtn()
        {
            ClickOnElement(addBtn);
            return this;
        }
        public CreateInvoiceBatchPage InputInvoiceSchedule(string scheduleType, string maxBillDate)
        {
            ClickAddBtn();
            SelectTextFromDropDown(invoiceScheduleSelect, scheduleType);
            SelectIndexFromDropDown(toPeriodSelect, 1);
            SelectIndexFromDropDown(fromPeriodSelect, 1);
            SendKeys(maxBillDateInput, maxBillDate);
            return this;
        }
        public CreateInvoiceBatchPage InputGenerateDate(string date)
        {
            SendKeys(generateDateInput, date);
            return this;
        }
    }
}
