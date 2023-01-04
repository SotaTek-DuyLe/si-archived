using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
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
        private readonly By firstInvoiceScheduleDd = By.XPath("(//label[text()='Invoice Schedule:']/following-sibling::div/select)[1]");
        private readonly By firstInvoiceContactDd = By.XPath("(//label[text()='Invoice Contact:']/following-sibling::div/select)[1]");
        private readonly By firstInvoiceAddressDd = By.XPath("(//label[text()='Invoice Address:']/following-sibling::div/select)[1]");
        private readonly By firstBillingRuleDd = By.XPath("(//label[text()='Billing Rule:']/following-sibling::div/select)[1]");
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By secondHistoryItem = By.XPath("(//strong[text()='Update - AgreementLine']/following-sibling::div)[1]");
        private readonly By firstHistoryItem = By.XPath("(//strong[text()='Update - AgreementLine']/following-sibling::div)[1]");
        private readonly By firstUpdatedUser = By.XPath("(//strong[@data-bind='text: $data.createdByUser'])[1]");
        private readonly By secondUpdatedUser = By.XPath("(//strong[@data-bind='text: $data.createdByUser'])[2]");
        private readonly By secondInvoiceScheduleDd = By.XPath("(//label[text()='Invoice Schedule:']/following-sibling::div/select)[2]");
        private readonly By secondInvoiceContactDd = By.XPath("(//label[text()='Invoice Contact:']/following-sibling::div/select)[2]");
        private readonly By secondInvoiceAddressDd = By.XPath("(//label[text()='Invoice Address:']/following-sibling::div/select)[2]");
        private readonly By secondBillingRuleDd = By.XPath("(//label[text()='Billing Rule:']/following-sibling::div/select)[2]");

        //DYNAMIC LOCATOR
        private const string primaryContactValue = "//select[@id='primary-contact']/option[text()='{0}']";
        private const string primaryContactDisplayed = "//div[@data-bind='with: primaryContact']/p[text()='{0}']";
        private const string invoiceContactValue = "//select[@id='invoice-contact']/option[text()='{0}']";
        private const string invoiceContactDisplayed = "//div[@data-bind='with: invoiceContact']/p[text()='{0}']";
        private const string invoiceContactValueAtServiceTable = "//label[text()='Invoice Contact:']/following-sibling::div/select/option[text()='{0}']";
        private const string titleDetail = "//span[text()='{0}']";
        private const string nameDetail = "//p[text()='{0}']";
        private const string strongTag = "(//strong[text()='{0}'])[1]";
        private const string firstInvoiceScheduleOption = "(//label[text()='Invoice Schedule:']/following-sibling::div/select)[1]/option[text()='{0}']";
        private const string firstInvoiceContactOption = "(//label[text()='Invoice Contact:']/following-sibling::div/select)[1]/option[text()='{0}']";
        private const string firstInvoiceAddressOption = "(//label[text()='Invoice Address:']/following-sibling::div/select)[1]/option[text()='{0}']";
        private const string firstBillingRuleOption = "(//label[text()='Billing Rule:']/following-sibling::div/select)[1]/option[text()='{0}']";

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

        [AllureStep]
        public AgreementDetailPage ClickOnFirstInvoiceScheduleAndSelectAnyOption(string invoiceScheduleValue)
        {
            ClickOnElement(firstInvoiceScheduleDd);
            ClickOnElement(firstInvoiceScheduleOption, invoiceScheduleValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnFirstInvoiceContactAndSelectAnyOption(string invoiceContactValue)
        {
            ClickOnElement(firstInvoiceContactDd);
            ClickOnElement(firstInvoiceContactOption, invoiceContactValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnFirstInvoiceAddressAndSelectAnyOption(string invoiceAddressValue)
        {
            ClickOnElement(firstInvoiceAddressDd);
            ClickOnElement(firstInvoiceAddressOption, invoiceAddressValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnFirstBillingRuleAndSelectAnyOption(string billingRuleValue)
        {
            ClickOnElement(firstBillingRuleDd);
            ClickOnElement(firstBillingRuleOption, billingRuleValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public AgreementDetailPage VerifyTitleUpdateInHistoryTab(string titleValue)
        {
            Assert.IsTrue(IsControlDisplayed(strongTag, titleValue));
            return this;
        }

        [AllureStep]
        public AgreementDetailPage VerifyHistoryAfterUpdateFirstServiced(string[] historyTitle, string[] valueExp, string userUpdatedValue)
        {
            Assert.AreEqual(userUpdatedValue, GetElementText(firstUpdatedUser));
             string[] allInfoDisplayed = GetElementText(firstHistoryItem).Split(Environment.NewLine);
            for (int i = 0; i < historyTitle.Length; i++)
            {
                Assert.AreEqual(historyTitle[i] + ": " + valueExp[i] + ".", allInfoDisplayed[i]);
            }
            return this;
        }

        [AllureStep]
        public AgreementDetailPage VerifyHistoryAfterUpdateSecondServiced(string[] historyTitle, string[] valueExp, string userUpdatedValue)
        {
            Assert.AreEqual(userUpdatedValue, GetElementText(secondUpdatedUser));
            string[] allInfoDisplayed = GetElementText(secondHistoryItem).Split(Environment.NewLine);
            for (int i = 0; i < historyTitle.Length; i++)
            {
                Assert.AreEqual(historyTitle[i] + ": " + valueExp[i] + ".", allInfoDisplayed[i]);
            }
            return this;
        }

        [AllureStep]
        public AgreementDetailPage VerifyValueSelectedInSeviced(string[] firstAllValueUpdatedAgreement, string[] secondAllValueUpdatedAgreement)
        {
            //First row
            Assert.AreEqual(firstAllValueUpdatedAgreement[0], GetFirstSelectedItemInDropdown(firstInvoiceScheduleDd), "Invoice Schedule at first serviced is not correct");
            Assert.AreEqual(firstAllValueUpdatedAgreement[1], GetFirstSelectedItemInDropdown(firstInvoiceAddressDd), "Invoice Address at first serviced is not correct");
            Assert.AreEqual(firstAllValueUpdatedAgreement[2], GetFirstSelectedItemInDropdown(firstInvoiceContactDd), "Invoice Contact at first serviced is not correct");
            Assert.AreEqual(firstAllValueUpdatedAgreement[3], GetFirstSelectedItemInDropdown(firstBillingRuleDd), "Billing Rule at first serviced is not correct");
            //Second row
            Assert.AreEqual(secondAllValueUpdatedAgreement[0], GetFirstSelectedItemInDropdown(secondInvoiceScheduleDd), "Invoice Schedule at second serviced is not correct");
            Assert.AreEqual(secondAllValueUpdatedAgreement[1], GetFirstSelectedItemInDropdown(secondInvoiceAddressDd), "Invoice Address at second serviced is not correct");
            Assert.AreEqual(secondAllValueUpdatedAgreement[2], GetFirstSelectedItemInDropdown(secondInvoiceContactDd), "Invoice Contact at second serviced is not correct");
            Assert.AreEqual(secondAllValueUpdatedAgreement[3], GetFirstSelectedItemInDropdown(secondBillingRuleDd), "Billing Rule at second serviced is not correct");
            return this;
        }

        #region
        private readonly By retirePopupTitle = By.XPath("//h4[text()='Are you sure you want to retire this Agreement?']");
        private readonly By closeBtn = By.XPath("//button[text()='×']");
        private readonly By cancelBtn = By.XPath("//button[text()='OK']/preceding-sibling::button[text()='Cancel']");
        private readonly By okBtn = By.XPath("//button[text()='OK']");
        private readonly By bodyRetiredPopup = By.CssSelector("div[class='bootbox-body']");

        #endregion

        [AllureStep]
        public AgreementDetailPage IsRetiredPopup()
        {
            WaitUtil.WaitForElementVisible(retirePopupTitle);
            Assert.IsTrue(IsControlDisplayed(retirePopupTitle), "Title is not displayed");
            Assert.IsTrue(IsControlDisplayed(closeBtn), "Close button is not displayed");
            Assert.IsTrue(IsControlDisplayed(cancelBtn), "Cancel button is not displayed");
            Assert.IsTrue(IsControlDisplayed(okBtn), "OK is not displayed");
            foreach (string associateObject in CommonConstants.AssociateObjectAgreement)
            {
                Assert.IsTrue(GetElementText(bodyRetiredPopup).Contains(associateObject), associateObject + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnCancelBtn()
        {
            ClickOnElement(cancelBtn);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage VerifyPopupIsDisappear()
        {
            WaitUtil.WaitForElementInvisible(retirePopupTitle);
            Assert.IsTrue(IsControlUnDisplayed(retirePopupTitle));
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnXBtn()
        {
            ClickOnElement(closeBtn);
            return this;
        }

        #region Add Service
        private readonly By addServiceBtn = By.XPath("//button[text()='Add Services']");
        private readonly By titleAddServicePopup = By.XPath("//div[@id='add-service-modal']//h4[text()='Add Service']");
        private readonly By closeAddServicePopupBtn = By.XPath("//div[@id='add-service-modal']//button[text()='×']");
        private readonly By nextBtn = By.XPath("//button[text()='Next']");

        //STEP 1: Site and Service
        private readonly By servicedSiteDd = By.CssSelector("select[id='collection-site']");
        private readonly By serviceDd = By.CssSelector("select[id='contract']");

        //STEP 2: Asset and Product
        private readonly By addBtnInStep2 = By.XPath("//div[@id='step-2']//button[contains(string(), 'Add')]");
        private readonly By assetDdInStep2 = By.CssSelector("div[id='step-2'] select[id='asset-type']");
        private readonly By tenureDdInStep2 = By.CssSelector("div[id='step-2'] select[id='tenure']");
        private readonly By productDdInStep2 = By.XPath("//div[@id='step-2']//label[text()='Product']/following-sibling::select");
        private readonly By ewcCodeDdInStep2 = By.XPath("//div[@id='step-2']//label[text()='EWC Code']/following-sibling::select");
        private readonly By unitDdInStep2 = By.XPath("//div[@id='step-2']//label[text()='Unit']/following-sibling::select");
        private readonly By productQtyAssetInStep2 = By.XPath("//div[@id='step-2']//input[@id='product-quantity']");
        private readonly By assetTypeLabel = By.XPath("//label[text()='Asset Type']");
        private readonly By doneBtnInStep2 = By.XPath("//div[@id='step-2']//button[text()='Done']");

        //STEP 3: Schedule services
        private readonly By addRegularServicesInStep3 = By.XPath("//div[@id='step-3']//button[text()='Add regular service']");
        private readonly By assetTypeInStep3 = By.XPath("//div[@id='step-3']//label[text()='Asset Type']");
        private readonly By firstAssetTypeValueInStep3 = By.XPath("(//div[@id='step-3']//span[@data-bind='text: assetType().name'])[1]");
        private readonly By firstProductValueInStep3 = By.XPath("(//div[@id='step-3']//span[@data-bind='text: product().name'])[1]");
        private readonly By firstUnitValueInStep3 = By.XPath("(//div[@id='step-3']//span[@data-bind='text: unit().name'])[1]");
        private readonly By firstEWCCodeValueInStep3 = By.XPath("(//div[@id='step-3']//span[@data-bind='text: productCode().name'])[1]");
        private readonly By firstProductQtyPerAssetInStep3 = By.XPath("(//div[@id='step-3']//input[@data-bind='value: productQty'])[1]");

        [AllureStep]
        public AgreementDetailPage ClickOnAddServiceBtn()
        {
            ClickOnElement(addServiceBtn);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage IsAddServicePopup()
        {
            WaitUtil.WaitForElementVisible(titleAddServicePopup);
            Assert.IsTrue(IsControlDisplayed(titleAddServicePopup));
            Assert.IsTrue(IsControlDisplayed(closeAddServicePopupBtn));
            return this;
        }

        [AllureStep]
        public AgreementDetailPage SelectServicedSite(string servicedSiteValue)
        {
            SelectTextFromDropDown(servicedSiteDd, servicedSiteValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage SelectService(string serviceValue)
        {
            SelectTextFromDropDown(serviceDd, serviceValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnNextBtn()
        {
            ClickOnElement(nextBtn);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnAddBtnStep2()
        {
            ClickOnElement(addBtnInStep2);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage SelectAssetTypeInStep2(string assetTypeValue)
        {
            WaitUtil.WaitForElementVisible(assetTypeLabel);
            SelectTextFromDropDown(assetDdInStep2, assetTypeValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage SelectTenureInStep2(string tenureValue)
        {
            SelectTextFromDropDown(tenureDdInStep2, tenureValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage SelectProductInStep2(string productValue)
        {
            SelectTextFromDropDown(productDdInStep2, productValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage SelectEWCCodeInStep2(string ewcCodeValue)
        {
            SelectTextFromDropDown(ewcCodeDdInStep2, ewcCodeValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage InputProductQtyPerAsset(string productQty)
        {
            SendKeys(productQtyAssetInStep2, productQty);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage SelectUnitInStep2(string unitValue)
        {
            SelectTextFromDropDown(unitDdInStep2, unitValue);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickDoneBtnInStep2()
        {
            ClickOnElement(doneBtnInStep2);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage ClickOnAddRegularServiceStep3()
        {
            ClickOnElement(addRegularServicesInStep3);
            return this;
        }

        [AllureStep]
        public AgreementDetailPage VerifyValueInStep3(string assetTypeExp, string productExp, string unitExp, string ewcCodeExp, string productQty)
        {
            WaitUtil.WaitForElementVisible(assetTypeInStep3);
            Assert.AreEqual(assetTypeExp, GetElementText(firstAssetTypeValueInStep3), "Asset Type in Step 3 is not correct");
            Assert.AreEqual(productExp, GetElementText(firstProductValueInStep3), "Product in Step 3 is not correct");
            Assert.AreEqual(unitExp, GetElementText(firstUnitValueInStep3), "Unit in Step 3 is not correct");
            Assert.AreEqual(ewcCodeExp, GetElementText(firstEWCCodeValueInStep3), "EWC Code in Step 3 is not correct");
            Assert.AreEqual(productQty, GetAttributeValue(firstProductQtyPerAssetInStep3, "value"), "Product Qty Per Asset in Step 3 is not correct");
            //Readonly
            Assert.AreEqual("true", GetAttributeValue(productQty, "readonly"), "Product Qty Per Asset in Step 3 is not readonly" );
            return this;
        }
        #endregion
    }
}
