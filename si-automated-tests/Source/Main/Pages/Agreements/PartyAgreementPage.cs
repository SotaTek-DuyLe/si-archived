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
using System.Linq;
using si_automated_tests.Source.Core.WebElements;
using NUnit.Allure.Attributes;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement
{
    //Agreement Detail Page
    //Can be opened through Party Detail Page -> Agreement Tab -> Double click one agreement
    //Or Agreements Main Page -> Double click one agreement
    public class PartyAgreementPage : BasePageCommonActions
    {
        private readonly By agreementId = By.XPath("//h4[@title='Id']");
        private readonly By detailsTabBtn = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By saveAndCloseBtn = By.XPath("//button[@title='Save and Close']");
        private readonly By closeWithoutSavingBtn = By.XPath("//a[@aria-controls='details-tab']/ancestor::body//button[@title='Close Without Saving']");
        private readonly By closeBtn = By.XPath("//button[@title='Close Without Saving']");
        private readonly By status = By.XPath("//div[@title='Agreement Status']");
        private string agreementStatus = "//div[@title='Agreement Status']//span[text()='{0}']";
        private readonly By statusInRed = By.XPath("//div[@title='Agreement Status' and @class='red-status']");

        private readonly By retireBtn = By.XPath("//button[@title='Retire']");

        public readonly By agreementTypeInput = By.Id("agreement-type");
        public readonly By startDateInput = By.Id("start-date");
        public readonly By endDateInput = By.Id("end-date");
        public readonly By primaryContract = By.Id("primary-contact");
        public readonly By invoiceContact = By.Id("invoice-contact");
        public readonly By correspondenceAddressInput = By.Id("correspondence-address");
        public readonly By invoiceAddressInput = By.Id("invoice-address");
        public readonly By invoiceScheduleInput = By.Id("invoice-schedule");
        private readonly By paymentMethodInput = By.Id("payment-method");
        private readonly By vatCodeInput = By.Id("vat-code");
        private readonly By serviceInput = By.Id("search");

        public readonly By addServiceBtn = By.XPath("//button[text()='Add Services']");
        public readonly By approveBtn = By.XPath("//button[text()='Approve']");
        public readonly By cancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By confirmApproveBtn = By.XPath("//button[@data-bb-handler='confirm']");

        //Agreement line panel
        public readonly By serviceAgreementPanel = By.XPath("//div[@class='panel panel-default' and contains(@data-bind,'agreementLines')]");
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
        private readonly By dataTab = By.XPath("//a[text()='Data']");
        public readonly By PricesTab = By.XPath("//a[@aria-controls='prices-tab']");

        //Task Tab locator 
        private readonly By taskTabBtn = By.XPath("//a[@aria-controls='tasks-tab']");

        //Agreement Tab locator
        private readonly By agreementTabBtn = By.XPath("//a[@aria-controls='agreements-tab']");
        private string agreementWithDate = "//div[text()='{0}']";

        //Retire Popup
        private readonly By retirePopUpTitle = By.XPath("//*[text()='Are you sure you want to retire this Agreement?']");
        private readonly By retirePopUpCancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By retirePopUpOKBtn = By.XPath("//button[text()='OK']");

        //Dynamic Locator
        private string expandAgreementLineByServicesName = "//span[text()='{0}' and contains(@data-bind, 'serviceName')]/ancestor::div[@class='panel-heading']//button[@title='Expand/close agreement line']";
        private string regularFrequency = "//td[text()='Commercial Collection']/following-sibling::td/p[text()='{0}']";
        private string editAgreementByAddress = "//p[text()='{0}']//ancestor::div[@class='panel-heading']//button[text()='Edit']";
        private string expandAgreementHeader = "//div[contains(@id, 'agreement-line')]//span[@data-bind='text: name' and text()='{0}']//preceding-sibling::span";

        private string agreementType = "//h4[contains(.,'{0}')]";
        private string agreementName = "//p[text()='{0}']";
        public readonly By SiteName = By.XPath("//div[@id='details-tab']//div[@class='panel-heading']//span[@data-bind='text: siteName']");

        private string AgreementTable = "//div[@id='agreements-tab']//div[@class='grid-canvas']";
        private string AgreementRow = "./div[contains(@class, 'slick-row')]";
        private string AgreementCheckboxCell = "./div[contains(@class, 'l0')]//input[@type='checkbox']";
        private string AgreementIdCell = "./div[contains(@class, 'l1')]";
        private string AgreementNameCell = "./div[contains(@class, 'l2')]";
        private string AgreementRefCell = "./div[contains(@class, 'l3')]";

        private TableElement agreementTableEle;
        public TableElement AgreementTableEle
        {
            get => agreementTableEle;
        }

        public PartyAgreementPage()
        {
            agreementTableEle = new TableElement(AgreementTable, AgreementRow, new List<string>() { AgreementCheckboxCell, AgreementIdCell, AgreementNameCell, AgreementRefCell });
            agreementTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        [AllureStep]
        public PartyAgreementPage VerifyTasklineInAgreement(int agreementIdx, string assestType, string assestQty, string product, string amountProduct, string unit)
        {
            string assestTypeCell = "./td[@data-bind='text: assetType']";
            string assestQtyCell = "./td[@data-bind='text: assetQty']";
            string productCell = "./td[@data-bind='text: product']";
            string amountProductCell = "./td[@data-bind='text: productQty']";
            string unitCell = "./td[@data-bind='text: unit']";
            TableElement taskLineTableEle = new TableElement(
                $"//div[@id='service-phase-collapse-0-{agreementIdx}']//tbody[@data-bind='foreach: taskLines']", 
                "./tr", 
                new List<string>() { assestTypeCell, assestQtyCell, productCell, amountProductCell, unitCell });
            VerifyCellValue(taskLineTableEle, 0, 0, assestType);
            VerifyCellValue(taskLineTableEle, 0, 1, assestQty);
            VerifyCellValue(taskLineTableEle, 0, 2, product);
            VerifyCellValue(taskLineTableEle, 0, 3, amountProduct);
            VerifyCellValue(taskLineTableEle, 0, 4, unit);
            return this;
        }

        [AllureStep]
        public PartyAgreementPage ExpandAgreementHeader(string headerName)
        {
            ClickOnElement(By.XPath(string.Format(expandAgreementHeader, headerName)));
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public int GetServicePanelUnDisplayCount()
        {
            return GetAllElements(serviceAgreementPanel).Count;
        }

        [AllureStep]
        public PartyAgreementPage VerifyServicePanelUnDisplayAfterClickRemove(int rowCountBefore)
        {
            Assert.IsTrue(GetServicePanelUnDisplayCount() < rowCountBefore);
            return this;
        }

        [AllureStep]
        public PartyAgreementPage VerifyServicePanelUnDisplay()
        {
            Assert.IsTrue(IsControlUnDisplayed(serviceAgreementPanel));
            return this;
        }

        [AllureStep]

        public PartyAgreementPage DoubleClickAgreement(int rowIdx)
        {
            agreementTableEle.DoubleClickRow(rowIdx);
            return this;
        }
        [AllureStep]

        public PartyAgreementPage WaitForAgreementPageLoadedSuccessfully(string type, string name)
        {
            WaitUtil.WaitForElementVisible(agreementType, type.ToUpper());
            WaitUtil.WaitForElementVisible(agreementName, name.ToUpper());
            WaitUtil.WaitForPageLoaded();
            return this;
        }
        [AllureStep]
        public string GetAgreementId()
        {
            int i = 5;
            while (i > 0)
            {
                if (GetElementText(agreementId).Equals("0"))
                {
                    SleepTimeInMiliseconds(2000);
                    i--;
                }
                else { break; }
            }
            return GetElementText(agreementId);
        }
        [AllureStep]
        public PartyAgreementPage ClickOnDetailsTab()
        {
            ClickOnElement(detailsTabBtn);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage CloseWithoutSaving()
        {
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }
        [AllureStep]
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
        [AllureStep]
        public PartyAgreementPage SelectAgreementType(string text)
        {
            SelectTextFromDropDown(agreementTypeInput, text);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyStartDateIsCurrentDate()
        {
            Assert.AreEqual(CommonUtil.GetLocalTimeNow("dd/MM/yyyy"), WaitUtil.WaitForElementVisible(startDate).Text);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyStartDateIs(string date)
        {
            Assert.AreEqual(date, WaitUtil.WaitForElementVisible(startDate).Text);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyAllStartDate(string date)
        {
            IList<IWebElement> startDates = WaitUtil.WaitForAllElementsVisible(startDate);
            foreach(var startDate in startDates)
            {
                Assert.AreEqual(date, startDate.Text);
            }
            
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyEndDateIsPredefined()
        {
            Assert.AreEqual("01/01/2050", WaitUtil.WaitForElementVisible(endDate).Text);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyEndDate(string date)
        {
            Assert.AreEqual(date, WaitUtil.WaitForElementVisible(endDate).Text);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage EnterStartDate(string startDate)
        {
            SendKeys(startDateInput, startDate);
            return this;
        }
        [AllureStep]
        public string GetAgreementStatus()
        {
            return WaitUtil.WaitForElementVisible(status).Text;
        }
        [AllureStep]
        public PartyAgreementPage VerifyAgreementStatus(string _status)
        {
            WaitUtil.WaitForTextVisibleInElement(status, _status);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyAgreementStatusWithText(string _status)
        {
            Assert.IsTrue(IsControlDisplayed(agreementStatus, _status));
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyAgreementStatusInRedBackground()
        {
            Assert.IsTrue(IsControlDisplayed(statusInRed));
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyNewOptionsAvailable()
        {
            WaitUtil.WaitForElementVisible(addServiceBtn);
            WaitUtil.WaitForElementVisible(approveBtn);
            WaitUtil.WaitForElementVisible(cancelBtn);
            return this;
        }
        [AllureStep]
        public DetailPartyPage ClosePartyAgreementPage()
        {
            ClickOnElement(closeBtn);
            return PageFactoryManager.Get<DetailPartyPage>();
        }
        [AllureStep]
        public PartyAgreementPage ClickApproveAgreement()
        {
            ScrollDownToElement(approveBtn);
            ClickOnElement(approveBtn);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage ConfirmApproveBtn()
        {
            ClickOnElement(confirmApproveBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public AddServicePage ClickAddService()
        {
            ClickOnElement(addServiceBtn);
            return PageFactoryManager.Get<AddServicePage>();
        }
        [AllureStep]
        public PartyAgreementPage VerifyServicePanelPresent()
        {
            WaitUtil.WaitForElementVisible(serviceAgreementPanel);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyServiceSiteAddres(string add)
        {
            Assert.IsTrue(add.Contains(GetElementText(panelSiteAddress)));
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyServiceStartDate(string startDate)
        {
            Assert.AreEqual(startDate, GetElementText(serviceStartDate));
            return this;
        }
        [AllureStep]
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
        [AllureStep]
        public PartyAgreementPage VerifyRegularAssetTypeStartDate(string startDate)
        {
            ScrollDownToElement(regularAssertTypeStartDate);
            Assert.AreEqual(startDate, GetElementText(regularAssertTypeStartDate));
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyAssetAndProductAssetTypeStartDate(string startDate)
        {
            
            Assert.AreEqual(startDate, GetElementText(assetAndProductAssetTypeStartDate));
            return this;
        }
        [AllureStep]
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
        [AllureStep]
        public PartyAgreementPage ExpandAdhocOnAgreementFields()
        {
            IList<IWebElement> fields = WaitUtil.WaitForAllElementsVisible(subExpandBtns);
            fields?.LastOrDefault()?.Click();
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyAgreementLineFormHasGreenBorder()
        {
            string borderColor = GetElement(By.XPath("//div[@id='details-tab']//div[@class='panel-heading']")).GetCssValue("border-color");
            Assert.IsTrue(ColorHelper.IsGreenColor(borderColor));
            return this;
        }
        [AllureStep]

        public PartyAgreementPage CreateAdhocTaskBtnInAgreementLine(string taskType)
        {
            List<IWebElement> rows = GetAllElements("(//div[@id='details-tab']//table[@class='table'])[4]//tbody//tr");
            foreach (var row in rows)
            {
                IReadOnlyCollection<IWebElement> cells = row.FindElements(By.XPath("//td[@data-bind='text: name']"));
                foreach (var cell in cells)
                {
                    string title = GetElementText(cell);
                    if (title == taskType)
                    {
                        IWebElement adhocBtn = row.FindElements(By.XPath("//button[text()='Create Ad-Hoc Task']")).FirstOrDefault();
                        adhocBtn?.Click();
                        return this;
                    }
                }
            }
            return this;
        }
        [AllureStep]
        public PartyAgreementPage ExpandAgreementLine()
        {
            ClickOnElement(expandBtn);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage ExpandAgreementLineByService(string service)
        {
            ClickOnElement(expandAgreementLineByServicesName, service);
            return this;
        }
        [AllureStep]
        public TaskTab OpenTaskTab()
        {
            ClickOnElement(taskTab);
            return PageFactoryManager.Get<TaskTab>();
        }
        [AllureStep]

        public PartyAgreementPage ClickEditAgreementBtn()
        {
            ScrollDownToElement(editBtn);
            ClickOnElement(editBtn);
            return this;
        }
        [AllureStep]

        public PartyAgreementPage ClickRemoveAgreementBtn()
        {
            ScrollDownToElement(removeBtn);
            ClickOnElement(removeBtn);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage ClickKeepAgreementBtn()
        {
            ScrollDownToElement(keepBtn);
            ClickOnElement(keepBtn);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage ClickEditAgreementByAddressBtn(string address)
        {
            ClickOnElement(editAgreementByAddress, address);
            return this;
        }
        [AllureStep]

        public PartyAgreementPage VerifyCreateAdhocButtonsAreDisabled()
        {
            IList<IWebElement> createAdhocBtns = WaitUtil.WaitForAllElementsVisible(createAdhocBtn);
            foreach (var btn in createAdhocBtns)
            {
                Assert.AreEqual(false, btn.Enabled);
            }
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyCreateAdhocButtonsAreEnabled()
        {
            IList<IWebElement> createAdhocBtns = WaitUtil.WaitForAllElementsVisible(createAdhocBtn);
            foreach (var btn in createAdhocBtns)
            {
                Assert.AreEqual(true, btn.Enabled);
            }
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifySchedule(string schedule)
        {
            Assert.IsTrue(IsControlDisplayed(regularFrequency, schedule));
            return this;
        }

        //Task tab
        [AllureStep]
        public PartyAgreementPage ClickTaskTabBtn()
        {
            WaitUtil.WaitForElementClickable(taskTabBtn);
            ClickOnElement(taskTabBtn);
            return this;
        }

        //Agreement Tab
        [AllureStep]
        public PartyAgreementPage ClickToAgreementTab()
        {
            ClickOnElement(agreementTabBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PartyAgreementPage OpenAnAgreementWithDate(string date)
        {
            DoubleClickOnElement(agreementWithDate, date);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyElementIsMandatory(By by)
        {
            IWebElement webEle = GetElement(by);
            string cssColor = webEle.GetCssValue("border-color");
            Assert.IsTrue(ColorHelper.IsRedColor(cssColor));
            return this;
        }
        [AllureStep]
        //Verify border at Agreement 
        public PartyAgreementPage VerifyBlueBorder()
        {
            Assert.IsTrue(IsControlDisplayed(blueBorder));
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyDotRedBorder()
        {
            Assert.IsTrue(IsControlDisplayed(dotRedBorder));
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyDotRedBorderDisappear()
        {
            Assert.IsTrue(IsControlUnDisplayed(dotRedBorder));
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyAgreementLineNum(int num)
        {
            List<IWebElement> lineNum = GetAllElements(serviceAgreementPanel);
            Assert.AreEqual(num, lineNum.Count);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyAgreementLineDisappear()
        {
            int i = 5;
            while (i > 0)
            {
                if (IsControlUnDisplayed(serviceAgreementPanel))
                {
                    break;
                }
                else
                {
                    SleepTimeInMiliseconds(1000);
                }
            }
            Assert.IsTrue(IsControlUnDisplayed(serviceAgreementPanel));
            return this;
        }

        //Retire popup
        [AllureStep]
        public PartyAgreementPage ClickRetireButton()
        {
            ClickOnElement(retireBtn);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage VerifyRetirePopup()
        {
            Assert.IsTrue(IsControlDisplayed(retirePopUpTitle));
            Assert.IsTrue(IsControlDisplayed(retirePopUpOKBtn));
            return this;
        }
        [AllureStep]
        public PartyAgreementPage RetirePopupClickOK()
        {
            ClickOnElement(retirePopUpOKBtn);
            return this;
        }
        [AllureStep]
        public DataTab ClickDataTab()
        {
            ClickOnElement(dataTab);
            return new DataTab();
        }
    }
}
