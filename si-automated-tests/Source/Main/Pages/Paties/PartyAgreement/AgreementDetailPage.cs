using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.PartyAgreement
{
    public class AgreementDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='COMMERCIAL COLLECTIONS']");
        private readonly By agreementName = By.XPath("//p[text()='JAFLONG TANDOORI']");
        private readonly By primaryContactDd = By.CssSelector("select#primary-contact");
        private readonly By invoiceContactDd = By.CssSelector("select#invoice-contact");
        private readonly By invoiceContactAtServiceTable = By.XPath("//label[text()='Invoice Contact:']/following-sibling::div/select");
        private readonly By numberOfInvoiceContactAtServiceTable = By.XPath("//label[text()='Invoice Contact:']/following-sibling::div/select/option");
        private readonly By addInvoiceContactBdn = By.XPath("//select[@id='invoice-contact']/following-sibling::span[text()='Add']");
        private readonly By removeBtn = By.XPath("//button[text()='Remove']");
        private readonly By expandedBtn = By.XPath("//button[@title='Expand/close agreement line']");
        private readonly By adhoc = By.XPath("//span[text()='Ad-hoc']/parent::span/parent::div");
        private const string allPrimaryContactValue = "//select[@id='primary-contact']/option";

        //DYNAMIC LOCATOR
        private const string primaryContactValue = "//select[@id='primary-contact']/option[text()='{0}']";
        private const string primaryContactDisplayed = "//div[@data-bind='with: primaryContact']/p[text()='{0}']";
        private const string invoiceContactValue = "//select[@id='invoice-contact']/option[text()='{0}']";
        private const string invoiceContactDisplayed = "//div[@data-bind='with: invoiceContact']/p[text()='{0}']";
        private const string invoiceContactValueAtServiceTable = "//label[text()='Invoice Contact:']/following-sibling::div/select/option[text()='{0}']";
        private const string titleDetail = "//span[text()='{0}']";
        private const string nameDetail = "//p[text()='{0}']";

        [AllureStep]
        public AgreementDetailPage WaitForDetailAgreementLoaded()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(agreementName);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage WaitForDetailAgreementLoaded(string titleA, string agreementNameA)
        {
            WaitUtil.WaitForElementVisible(string.Format(titleDetail, titleA));
            WaitUtil.WaitForElementVisible(string.Format(nameDetail, agreementNameA));
            return this;
        }
        [AllureStep]
        public AgreementDetailPage ClickPrimaryContactDd()
        {
            ClickOnElement(primaryContactDd);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage ClickInvoiceContactDd()
        {
            ClickOnElement(invoiceContactDd);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage VerifyValueInPrimaryContactDd(string[] expectedOption)
        {
            foreach (string option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(primaryContactValue, option)));
            }
            return this;
        }
        [AllureStep]
        public AgreementDetailPage SelectAnyPrimaryContactAndVerify(ContactModel contactModel)
        {
            ClickOnElement(primaryContactValue, contactModel.FirstName + " " + contactModel.LastName);
            //Verify
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Title));
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Telephone));
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Mobile));
            Assert.IsTrue(IsControlDisplayed(primaryContactDisplayed, contactModel.Email));
            //Verify contact saved in primary contact dd
            Assert.AreEqual(GetFirstSelectedItemInDropdown(primaryContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage VerifyValueInInvoiceContactDd(string[] expectedOption)
        {
            foreach (string option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(invoiceContactValue, option)));
            }
            return this;
        }
        [AllureStep]
        public AgreementDetailPage VerifyFirstValueInInvoiceContactDd(ContactModel contactModel)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage SelectAnyInvoiceContactAndVerify(ContactModel contactModel)
        {
            ClickOnElement(invoiceContactValue, contactModel.FirstName + " " + contactModel.LastName);
            //Verify
            Assert.IsTrue(IsControlDisplayed(invoiceContactDisplayed, contactModel.Title));
            Assert.IsTrue(IsControlDisplayed(invoiceContactDisplayed, contactModel.Telephone));
            Assert.IsTrue(IsControlDisplayed(invoiceContactDisplayed, contactModel.Mobile));
            Assert.IsTrue(IsControlDisplayed(invoiceContactDisplayed, contactModel.Email));
            //Verify contact saved in primary contact dd
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceContactDd), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage ClickInvoiceContactDdAtServiceTable()
        {
            ClickOnElement(invoiceContactAtServiceTable);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage VerifyValueInInvoiceContactServiceTable(string[] expectedOption)
        {
            foreach (string option in expectedOption)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(invoiceContactValueAtServiceTable, option)));
            }
            return this;
        }
        [AllureStep]
        public AgreementDetailPage SelectAnyInvoiceContactServiceTableAndVerify(ContactModel contactModel)
        {
            ClickOnElement(string.Format(invoiceContactValueAtServiceTable, contactModel.FirstName + " " + contactModel.LastName));
            Assert.AreEqual(GetFirstSelectedItemInDropdown(invoiceContactAtServiceTable), contactModel.FirstName + " " + contactModel.LastName);
            return this;
        }
        [AllureStep]
        public AddInvoiceContactPage ClickAddInvoiceContactBtn()
        {
            ClickOnElement(addInvoiceContactBdn);
            return new AddInvoiceContactPage();
        }
        [AllureStep]
        public AgreementDetailPage ScrollToTheRemoveBtn()
        {
            ScrollDownToElement(removeBtn);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage ClickExpandBtn()
        {
            ClickOnElement(expandedBtn);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage ScrollToAdhoc()
        {
            ScrollDownToElement(adhoc);
            return this;
        }
        [AllureStep]
        public AgreementDetailPage VerifyNumberOfContact(int numberOfContact)
        {
            Assert.AreEqual(numberOfContact, GetAllElements(allPrimaryContactValue).Count);
            return this;
        }
    }
}
