using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models.Agreement;

using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Agrrements;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement
{
    //Agreement Detail Page
    //Can be opened through Party Detail Page -> Agreement Tab -> Double click one agreement
    //Or Agreements Main Page -> Double click one agreement
    public class PartyAgreementPage : BasePage
    {
        private readonly By detailsTabBtn = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By saveAndCloseBtn = By.XPath("//button[@title='Save and Close']");
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");
        private readonly By closeBtn = By.XPath("//button[@title='Close Without Saving']");
        private readonly By status = By.XPath("//div[@title='Agreement Status']");
        private string agreementStatus = "//div[@title='Agreement Status']//span[text()='{0}']";

        private readonly By agreementTypeInput = By.Id("agreement-type");
        private readonly By startDateInput = By.Id("start-date");
        private readonly By endDateInput = By.Id("end-date");
        private readonly By primaryContract = By.Id("primary-contact");
        private readonly By invoiceContact = By.Id("invoice-contact");
        private readonly By correspondenceAddressInput = By.Id("correspondence-address");
        private readonly By invoiceAddressInput = By.Id("invoice-address");
        private readonly By invoiceScheduleInput = By.Id("invoice-schedule");
        private readonly By paymentMethodInput = By.Id("payment-method");
        private readonly By vatCodeInput = By.Id("vat-code");
        private readonly By serviceInput = By.Id("search");

        private readonly By addServiceBtn = By.XPath("//button[text()='Add Services']");
        private readonly By approveBtn = By.XPath("//button[text()='Approve']");
        private readonly By cancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By confirmApproveBtn = By.XPath("//button[@data-bb-handler='confirm']");

        //Agreement line panel
        private readonly By serviceAgreementPanel = By.XPath("//div[@class='panel panel-default' and contains(@data-bind,'agreementLines')]");
        private readonly By serviceStartDate = By.XPath("//div[@class='panel panel-default' and contains(@data-bind,'agreementLines')]/descendant::span[contains(@data-bind,'displayStartDate')]");
        private readonly By panelSiteAddress = By.XPath("//p[contains(@data-bind,'siteAddress')]");
        private readonly By expandBtn = By.XPath("//button[@title='Expand/close agreement line']");
        private readonly By subExpandBtns = By.XPath("//div[contains(@class,'panel-heading clickable')]");
        private readonly By editBtn = By.XPath("//button[text()='Edit']");
        private readonly By removeBtn = By.XPath("//button[text()='Remove']");
        private readonly By keepBtn = By.XPath("//button[text()='Keep']");
        private readonly By assetAndProductAssetTypeStartDate = By.XPath("//tbody[contains(@data-bind,'assetProducts')]//span[@title='Start Date']");
        private readonly By regularAssertTypeStartDate = By.XPath("//span[text()='Regular']/ancestor::div[1]/following-sibling::div//span[contains(@data-bind,'displayStartDate')]");
        private readonly By serviceTaskLineTypeStartDates = By.XPath("//th[text()='Task Line Type']/ancestor::thead[1]/following-sibling::tbody//span[@title='Start Date']");
        private readonly By createAdhocBtn = By.XPath("//button[text()='Create Ad-Hoc Task']");

        private readonly By blueBorder = By.XPath("//div[contains(@data-bind,'#0886AD')]");
        private readonly String dotRedBorder = "//div[@style='border: 3px dotted red;']";

        //Summary title
        private readonly By startDate = By.XPath("//span[@title='Start Date']");
        private readonly By endDate = By.XPath("//span[@title='End Date']");

        //Tabs
        private readonly By taskTab = By.XPath("//a[text()='Tasks']");

        //Task Tab locator 
        private readonly By taskTabBtn = By.XPath("//a[@aria-controls='tasks-tab']");

        //Agreement Tab locator
        private readonly By agreementTabBtn = By.XPath("//a[@aria-controls='agreements-tab']");
        private string agreementWithDate = "//div[text()='{0}']";

        //Dynamic Locator
        private string expandAgreementLineByServicesName = "//span[text()='{0}' and contains(@data-bind, 'serviceName')]/ancestor::div[@class='panel-heading']//button[@title='Expand/close agreement line']";
        private string regularFrequency = "//td[text()='Commercial Collection']/following-sibling::td/p[text()='{0}']";
        private string editAgreementByAddress = "//p[text()='{0}']//ancestor::div[@class='panel-heading']//button[text()='Edit']";
        public PartyAgreementPage ClickOnDetailsTab()
        {
            ClickOnElement(detailsTabBtn);
            return this;
        }
        public PartyAgreementPage CloseWithoutSaving()
        {
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }
        public PartyAgreementPage IsOnPartyAgreementPage()
        {
            WaitUtil.WaitForElementVisible(agreementTypeInput);
            WaitUtil.WaitForElementVisible(startDateInput);
            WaitUtil.WaitForElementVisible(endDateInput);
            WaitUtil.WaitForElementVisible(primaryContract);
            WaitUtil.WaitForElementVisible(invoiceContact);
            WaitUtil.WaitForElementVisible(correspondenceAddressInput);
            WaitUtil.WaitForElementVisible(invoiceAddressInput);
            WaitUtil.WaitForElementVisible(invoiceScheduleInput);
            WaitUtil.WaitForElementVisible(paymentMethodInput);
            WaitUtil.WaitForElementVisible(vatCodeInput);
            WaitUtil.WaitForElementVisible(serviceInput);
            return this;
        }
        public PartyAgreementPage SelectAgreementType(string text)
        {
            SelectTextFromDropDown(agreementTypeInput, text);
            return this;
        }
        public PartyAgreementPage VerifyStartDateIsCurrentDate()
        {
            Assert.AreEqual(CommonUtil.GetLocalTimeNow("dd/MM/yyyy"), WaitUtil.WaitForElementVisible(startDate).Text);
            return this;
        }
        public PartyAgreementPage VerifyStartDateIs(string date)
        {
            Assert.AreEqual(date, WaitUtil.WaitForElementVisible(startDate).Text);
            return this;
        }
        public PartyAgreementPage VerifyAllStartDate(string date)
        {
            IList<IWebElement> startDates = WaitUtil.WaitForAllElementsVisible(startDate);
            foreach(var startDate in startDates)
            {
                Assert.AreEqual(date, startDate.Text);
            }
            
            return this;
        }
        public PartyAgreementPage VerifyEndDateIsPredefined()
        {
            Assert.AreEqual("01/01/2050", WaitUtil.WaitForElementVisible(endDate).Text);
            return this;
        }
        public PartyAgreementPage EnterStartDate(string startDate)
        {
            SendKeys(startDateInput, startDate);
            return this;
        }
        public string GetAgreementStatus()
        {
            return WaitUtil.WaitForElementVisible(status).Text;
        }
        public PartyAgreementPage VerifyAgreementStatus(string _status)
        {
            WaitUtil.WaitForTextVisibleInElement(status, _status);
            return this;
        }
        public PartyAgreementPage VerifyAgreementStatusWithText(string _status)
        {
            Thread.Sleep(10000);
            WaitUtil.WaitForElementInvisible(agreementStatus, _status);
            Assert.IsTrue(IsControlDisplayed(agreementStatus, _status));
            return this;
        }
        public PartyAgreementPage VerifyNewOptionsAvailable()
        {
            WaitUtil.WaitForElementVisible(addServiceBtn);
            WaitUtil.WaitForElementVisible(approveBtn);
            WaitUtil.WaitForElementVisible(cancelBtn);
            return this;
        }
        public DetailPartyPage ClosePartyAgreementPage()
        {
            ClickOnElement(closeBtn);
            return PageFactoryManager.Get<DetailPartyPage>();
        }
        public PartyAgreementPage ClickApproveAgreement()
        {
            ScrollDownToElement(approveBtn);
            ClickOnElement(approveBtn);
            return this;
        }
        public PartyAgreementPage ConfirmApproveBtn()
        {
            WaitUtil.WaitForElementClickable(confirmApproveBtn);
            ClickOnElement(confirmApproveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public AddServicePage ClickAddService()
        {
            ClickOnElement(addServiceBtn);
            return PageFactoryManager.Get<AddServicePage>();
        }
        public PartyAgreementPage VerifyServicePanelPresent()
        {
            WaitUtil.WaitForElementVisible(serviceAgreementPanel);
            return this;
        }
        public PartyAgreementPage VerifyServiceSiteAddres(string add)
        {
            Assert.IsTrue(add.Contains(GetElementText(panelSiteAddress)));
            return this;
        }
        public PartyAgreementPage VerifyServiceStartDate(string startDate)
        {
            Assert.AreEqual(startDate, GetElementText(serviceStartDate));
            return this;
        }
        public PartyAgreementPage VerifyTaskLineTypeStartDates(string startDate)
        {
            Assert.AreEqual(startDate, GetElementText(serviceTaskLineTypeStartDates));
            IList<IWebElement> elements = WaitUtil.WaitForAllElementsVisible(serviceTaskLineTypeStartDates);
            foreach (IWebElement element in elements)
            {
                Assert.AreEqual(startDate, GetElementText(element));
            }
            return this;
        }
        public PartyAgreementPage VerifyRegularAssetTypeStartDate(string startDate)
        {
            ScrollDownToElement(regularAssertTypeStartDate);
            Assert.AreEqual(startDate, GetElementText(regularAssertTypeStartDate));
            return this;
        }
        public PartyAgreementPage VerifyAssetAndProductAssetTypeStartDate(string startDate)
        {
            
            Assert.AreEqual(startDate, GetElementText(assetAndProductAssetTypeStartDate));
            return this;
        }
        public PartyAgreementPage ExpandAllAgreementFields()
        {
            IList<IWebElement> fields = WaitUtil.WaitForAllElementsVisible(subExpandBtns);
            foreach(var field in fields)
            {
                Thread.Sleep(300);
                field.Click();
            }
            return this;
        }
        public PartyAgreementPage ExpandAgreementLine()
        {
            ClickOnElement(expandBtn);
            return this;
        }
        public PartyAgreementPage ExpandAgreementLineByService(string service)
        {
            ClickOnElement(expandAgreementLineByServicesName, service);
            return this;
        }
        public TaskTab OpenTaskTab()
        {
            ClickOnElement(taskTab);
            return PageFactoryManager.Get<TaskTab>();
        }

        public PartyAgreementPage ClickEditAgreementBtn()
        {
            ScrollDownToElement(editBtn);
            ClickOnElement(editBtn);
            return this;
        }

        public PartyAgreementPage ClickRemoveAgreementBtn()
        {
            ScrollDownToElement(removeBtn);
            ClickOnElement(removeBtn);
            return this;
        }
        public PartyAgreementPage ClickKeepAgreementBtn()
        {
            ScrollDownToElement(keepBtn);
            ClickOnElement(keepBtn);
            return this;
        }
        public PartyAgreementPage ClickEditAgreementByAddressBtn(string address)
        {
            ClickOnElement(editAgreementByAddress, address);
            return this;
        }

        public PartyAgreementPage VerifyCreateAdhocButtonsAreDisabled()
        {
            IList<IWebElement> createAdhocBtns = WaitUtil.WaitForAllElementsVisible(createAdhocBtn);
            foreach (var btn in createAdhocBtns)
            {
                Assert.AreEqual(false, btn.Enabled);
            }
            return this;
        }
        public PartyAgreementPage VerifyCreateAdhocButtonsAreEnabled()
        {
            IList<IWebElement> createAdhocBtns = WaitUtil.WaitForAllElementsVisible(createAdhocBtn);
            foreach (var btn in createAdhocBtns)
            {
                Assert.AreEqual(true, btn.Enabled);
            }
            return this;
        }

        public PartyAgreementPage VerifySchedule(string schedule)
        {
            Assert.IsTrue(IsControlDisplayed(regularFrequency, schedule));
            return this;
        }

        //Task tab
        public PartyAgreementPage ClickTaskTabBtn()
        {
            WaitUtil.WaitForElementClickable(taskTabBtn);
            ClickOnElement(taskTabBtn);
            return this;
        }
        
        //Agreement Tab
        public PartyAgreementPage ClickToAgreementTab()
        {
            ClickOnElement(agreementTabBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public PartyAgreementPage OpenAnAgreementWithDate(string date)
        {
            DoubleClickOnElement(agreementWithDate, date);
            return this;
        }

        //Verify border at Agreement 
        public PartyAgreementPage VerifyBlueBorder()
        {
            Assert.IsTrue(IsControlDisplayed(blueBorder));
            return this;
        }
        public PartyAgreementPage VerifyDotRedBorder()
        {
            Assert.IsTrue(IsControlDisplayed(dotRedBorder));
            return this;
        }
        public PartyAgreementPage VerifyDotRedBorderDisappear()
        {
            Assert.IsTrue(IsControlUnDisplayed(dotRedBorder));
            return this;
        }

        public PartyAgreementPage VerifyAgreementLineDisappear()
        {
            Assert.IsTrue(IsControlUnDisplayed(serviceAgreementPanel));
            return this;
        }
    }
}
