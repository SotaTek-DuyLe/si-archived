using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement.AddService
{
    public class InvoiceDetailTab : AddServicePage
    {
        private readonly By invoiceSchedule = By.XPath("(//select[contains(@data-bind,'invoiceSchedule')])[last()]");
        private readonly By invoiceContact = By.XPath("(//select[contains(@data-bind,'invoiceContact')])[last()]");
        private readonly By invoiceAddress = By.XPath("(//select[contains(@data-bind,'invoiceAddress')])[last()]");
        private readonly By billingRule = By.XPath("(//select[contains(@data-bind,'billingRule')])[last()]");
        private readonly By Page5InvoiceDetailsText = By.XPath("//span[text()='5']/following-sibling::p[text()='Invoice Details']");
        public InvoiceDetailTab IsOnInvoiceDetailsTab()
        {
            WaitUtil.WaitForAllElementsVisible(Page5InvoiceDetailsText);
            Assert.IsTrue(IsControlDisplayed(Page5InvoiceDetailsText));
            return this;
        }
        public InvoiceDetailTab VerifyInvoiceOptions(string value)
        {
            Assert.AreEqual(value, GetFirstSelectedItemInDropdown(invoiceSchedule));
            Assert.AreEqual(value, GetFirstSelectedItemInDropdown(invoiceContact));
            Assert.AreEqual(value, GetFirstSelectedItemInDropdown(invoiceAddress));
            return this;
        }
    }
}
