using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.WB.Sites;

namespace si_automated_tests.Source.Main.Pages.WB.Tickets
{
    public class CreateNewTicketPage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//h4[text()='Weighbridge Ticket']");
        private readonly By ticketDateInput = By.CssSelector("input#ticket-date");
        public readonly By stationDd = By.CssSelector("select#stations");
        private readonly By vehicleReg = By.XPath("//label[text()='Vehicle Reg']/following-sibling::div//input");
        private readonly By ticketTypeInput = By.CssSelector("select#ticket-type");
        private readonly By vehicleTypeInput = By.CssSelector("input#resource-type");
        private readonly By ticketTypeDd = By.CssSelector("select[id='ticket-type']");
        private readonly By ticketTypeLabel = By.XPath("//label[text()='Ticket Type']");
        public readonly By haulierDd = By.CssSelector("select[id='haulier']");
        private readonly By addTicketLineBtn = By.XPath("//button[text()='Add']");
        private readonly By licenceNumberExpDate = By.CssSelector("input[id='licence-number-expiry']");
        private readonly By licenceNumber = By.CssSelector("input[id='licence-number']");
        private readonly By netQuantity = By.XPath("//input[contains(@data-bind, 'value: netQuantity')]");
        public readonly By SourcePartySelect = By.XPath("//select[@id='source-party']");
        public readonly By PONumberInput = By.XPath("//input[@id='po-number']");
        public readonly By AddButton = By.XPath("//button[text()='Add' and contains(@data-bind, 'addTicketLine')]");
        private readonly By ticketNumberInput = By.CssSelector("input[id='ticket-number']");
        private readonly By driverNameInput = By.CssSelector("input[id='driver-name']");

        //Warning popup
        private readonly By titleWarningPopup = By.XPath("//h4[text()=\"Warning - Customer's account type has allow cash payment set to true.\"]");
        private readonly By bodyWarningPopup = By.XPath("//div[text()='Take Payment due?']");
        private readonly By yesBtn = By.XPath("//div[text()='Take Payment due?']/ancestor::div//button[text()='Yes']");
        private readonly By noBtn = By.XPath("//div[text()='Take Payment due?']/ancestor::div//button[text()='No']");

        //Ticket line
        private readonly By productDd = By.XPath("//tbody[@data-bind='foreach: ticketLines']//td[2]/select");
        private readonly By locationDd = By.XPath("//tbody[@data-bind='foreach: ticketLines']//td[3]/select");
        private readonly By firstWeightInput = By.XPath("//tbody[@data-bind='foreach: ticketLines']//td[7]/input");
        private readonly By firstDateInput = By.XPath("//tbody[@data-bind='foreach: ticketLines']//td[8]//input");
        private readonly By secondWeightInput = By.XPath("//tbody[@data-bind='foreach: ticketLines']//td[9]/input");
        private readonly By secondDateInput = By.XPath("//tbody[@data-bind='foreach: ticketLines']//td[11]//input");

        //DYNAMIC
        private const string stationOption = "//select[@id='stations']/option[text()='{0}']";
        private const string anyOption = "//li[text()='{0}']";
        private const string ticketTypeOption = "//select[@id='ticket-type']/option[text()='{0}']";
        private const string haulierOption = "//select[@id='haulier']/option[text()='{0}']";
        private const string productOption = "//tbody[@data-bind='foreach: ticketLines']//td[2]/select/option[text()='{0}']";
        private const string locationOption = "//tbody[@data-bind='foreach: ticketLines']//td[3]/select/option[text()='{0}']";

        public readonly By TakePaymentTitle = By.XPath("//div[@role='dialog']//div[text()='Take Payment due?']");
        public readonly By YesTakePaymentButton = By.XPath("//div[@role='dialog' and contains(@style,'display: block;')]//button[text()='Yes']");
        public readonly By NoTakePaymentButton = By.XPath("//div[@role='dialog' and contains(@style,'display: block;')]//button[text()='No']");
        public readonly By TakePaymentButton = By.XPath("//div[@class='text-center']//button[contains(text(), 'Take Payment')]");
        public readonly By CancelTicketButton = By.XPath("//div[@class='text-center']//button[contains(text(), 'Cancel Ticket')]");
        public readonly By DuplicateTicketButton = By.XPath("//div[@class='text-center']//button[contains(text(), 'Duplicate Ticket')]");
        public readonly By CopyToGreyListButton = By.XPath("//div[@class='text-center']//button[contains(text(), 'Copy To Greylist')]");
        public readonly By MarkForCreditButton = By.XPath("//div[@class='text-center']//button[contains(text(), 'Mark For Credit')]");
        public readonly By UnmarkForCreditButton = By.XPath("//div[@class='text-center']//button[contains(text(), 'Unmark From Credit')]");

        public readonly By TicketState = By.XPath("//h5[@data-bind='text: ticketState']");
        public readonly By IdTicket = By.XPath("//h4[@title='Id']");
        public readonly By HistoryTab = By.XPath("//a[@aria-controls='history-tab']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");

        #region Cancel Popup
        public readonly By CancelExpandReasonButton = By.XPath("//div[@id='ticket-state-resolution-codes-cancel']//button[@data-id='resolution-codes']");
        public readonly By CancelReasonSelect = By.XPath("//ul[@class='dropdown-menu inner' and @aria-expanded='true']");
        public readonly By CancelReasonNote = By.XPath("//div[@id='ticket-state-resolution-codes-cancel']//textarea[@id='resolution-note']");
        public readonly By CancelReasonButton = By.XPath("//div[@id='ticket-state-resolution-codes-cancel']//button[text()='Cancel Ticket']");

        [AllureStep]
        public CreateNewTicketPage ClickCancelExpandReasonButton()
        {
            WaitUtil.WaitForElementVisible(CancelExpandReasonButton);
            ClickOnElement(CancelExpandReasonButton);
            return this;
        }
        #endregion

        #region History Tab
        private string HistoryTable = "//div[@id='history-tab']//div[@class='grid-canvas']";
        private string HistoryRow = "./div[contains(@class, 'slick-row')]";
        private string HistoryTypeCell = "./div[contains(@class, 'l0')]";
        private string HistoryActionCell = "./div[contains(@class, 'l1')]";
        private string HistoryEchoIdCell = "./div[contains(@class, 'l2')]";
        private string HistoryEchoTypeIdCell = "./div[contains(@class, 'l3')]";
        private string HistoryDetailsCell = "./div[contains(@class, 'l4')]";

        public TableElement HistoryTableEle
        {
            get => new TableElement(HistoryTable, HistoryRow, new List<string>() { HistoryTypeCell, HistoryActionCell, HistoryEchoIdCell, HistoryEchoTypeIdCell, HistoryDetailsCell });
        }
        [AllureStep]
        public CreateNewTicketPage VerifyHistory(List<string> actions)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                VerifyCellValue(HistoryTableEle, i, 1, actions[i]);
            }
            return this;
        }
        #endregion

        #region Take Payment
        public readonly By PayButton = By.XPath("//div[@id='pay-for-ticket']//button[@data-bind='click: payForTicket']");

        #endregion

        #region Copy To GreyList
        public readonly By GreyListSelect = By.XPath("//select[@id='greylist-code']");
        public readonly By CommentInput = By.XPath("//textarea[@id='greylist-comment']");
        public readonly By SaveGreyListButton = By.XPath("//div[@id='payment-grey-lists-modal']//button[text()='Save']");
        #endregion

        #region Ticket Line Table
        private string TicketLineTable = "//div[@id='details-tab']//table//tbody[@data-bind='foreach: ticketLines']";
        private string TicketLineRow = "./tr[not(@data-bind)]";
        private string TicketLineProductCell = "./td//select[contains(@data-bind, 'products')]";
        private string TicketLineLocationCell = "./td//select[contains(@data-bind, 'locations')]";
        private string TicketLineUnitCell = "./td//select[contains(@data-bind, 'units')]";
        private string TicketLineFirstWeightCell = "./td//input[contains(@data-bind, 'ticketLine.grossWeight')]";
        private string TicketLineFirstDateCell = "./td[contains(@data-bind, 'ticketLine.grossDate')]//div";
        private string TicketLineSecondWeightCell = "./td//input[contains(@data-bind, 'ticketLine.tareWeight')]";
        private string TicketLineIsStoreTareCell = "./td//input[@id='isStoredTare']";
        private string TicketLineSecondDateCell = "./td[contains(@data-bind, 'ticketLine.tareDate')]//div";

        public TableElement TicketLineTableEle
        {
            get => new TableElement(TicketLineTable, TicketLineRow, new List<string>() { TicketLineProductCell, TicketLineLocationCell, TicketLineUnitCell, TicketLineFirstWeightCell, TicketLineFirstDateCell, TicketLineSecondWeightCell, TicketLineIsStoreTareCell, TicketLineSecondDateCell });
        }

        [AllureStep]
        public (string firstDate, string secondDate) InputTicketLineData(int rowIdx, string product, string firstWeight, string secondWeight)
        {
            TicketLineTableEle.SetCellValue(rowIdx, 0, product);
            TicketLineTableEle.SetCellValue(rowIdx, 3, firstWeight);
            IWebElement firstDateCell = TicketLineTableEle.GetCell(rowIdx, 4);
            ClickOnElement(firstDateCell.FindElement(By.XPath("./span[@class='input-group-addon']")));
            IWebElement secondWeightCell = TicketLineTableEle.GetCell(rowIdx, 5);
            ClickOnElement(secondWeightCell);
            TicketLineTableEle.SetCellValue(rowIdx, 5, secondWeight);
            IWebElement secondDateCell = TicketLineTableEle.GetCell(rowIdx, 7);
            ClickOnElement(secondDateCell.FindElement(By.XPath("./span[@class='input-group-addon']")));
            secondWeightCell = TicketLineTableEle.GetCell(rowIdx, 5);
            ClickOnElement(secondWeightCell);
            return (firstDateCell.FindElement(By.XPath("./input")).GetAttribute("value"), secondDateCell.FindElement(By.XPath("./input")).GetAttribute("value"));
        }
        [AllureStep]
        public CreateNewTicketPage VerifyTicketLineData(int rowIdx, string product, string firstWeight, string secondWeight, string firtDate, string secondDate)
        {
            return this;
        }
        #endregion

        #region Price Line Table
        private readonly string AddPriceLineButton = "./button[contains(@data-bind, 'addPriceLine')]";
        //private readonly string PriceLineRow = "./tr";
        //private readonly string DescriptionCell = "./td//input[contains(@data-bind, 'description')]";
        //private readonly string UnitCell = "./td//select[contains(@data-bind, 'units')]";
        //private readonly string NetQtyCell = "./td//input[contains(@data-bind, 'quantity')]";
        //private readonly string OverrideUnitPriceCell = "./td//input[contains(@data-bind, 'overridePrice')]";
        //private readonly string RemoveButtonCell = "./td//button[text()='Remove']";

        [AllureStep]
        public CreateNewTicketPage ClickAddPriceLine(int ticketLineIdx)
        {
            string priceLineTable = $"//div[@id='details-tab']//table//tbody[@data-bind='foreach: ticketLines']//tr[contains(@data-bind, 'ticketLine.priceLines')][{ticketLineIdx}]/td";
            IWebElement priceLineRow = GetElement(By.XPath(priceLineTable));
            ClickOnElement(priceLineRow.FindElement(By.XPath(AddPriceLineButton)));
            return this;
        }
        #endregion
        [AllureStep]
        public CreateNewTicketPage VerifyTakePayment()
        {
            VerifyElementVisibility(TakePaymentTitle, true);
            VerifyElementVisibility(YesTakePaymentButton, true);
            VerifyElementVisibility(NoTakePaymentButton, true);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage IsCreateNewTicketPage()
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(title));
            Assert.IsTrue(IsControlDisplayed(ticketDateInput));
            Assert.IsTrue(IsControlDisplayed(stationDd));
            //Default value in ticketDateInput
            Assert.AreEqual(GetAttributeValue(ticketDateInput, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            //Mandatory field
            Assert.AreEqual(GetCssValue(stationDd, "border-color"), CommonConstants.BoderColorMandatory);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickStationDdAndSelectStation(String stationName)
        {
            ClickOnElement(stationDd);
            ClickOnElement(stationOption, stationName);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyDisplayVehicleRegInput()
        {
            Assert.IsTrue(IsControlDisplayed(vehicleReg));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage InputVehicleRegInput(string vehicleValue)
        {
            SendKeys(vehicleReg, vehicleValue);
            ClickOnElement(anyOption, vehicleValue);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage InputVehicleRegInputAndClickOK(string vehicleValue)
        {
            SendKeys(vehicleReg, vehicleValue);
            ClickOnElement(anyOption, vehicleValue);
            ClickOnElement(By.XPath("//div[@id='grey-lists-modal']//button[text()='OK']"));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyDisplayVehicleType(string vehicleTypeName)
        {
            WaitUtil.WaitForElementVisible(vehicleTypeInput);
            Assert.IsTrue(IsControlDisplayed(vehicleTypeInput));
            //Verify value
            Assert.AreEqual(GetAttributeValue(vehicleTypeInput, "value"), vehicleTypeName);
            return this;
        }

        //Ticket Type Dd
        [AllureStep]
        public CreateNewTicketPage VerifyDisplayTicketTypeInput()
        {
            WaitUtil.WaitForElementVisible(ticketTypeInput);
            Assert.IsTrue(IsControlDisplayed(ticketTypeInput));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickTicketType()
        {
            ClickOnElement(ticketTypeDd);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyValueInTicketTypeDd()
        {
            foreach (string ticket in CommonConstants.TicketType)
            {
                Assert.IsTrue(IsControlDisplayed(ticketTypeOption, ticket));
            }
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyFirstSelectedValueInTicketType(string ticketTypeValue)
        {
            Assert.AreEqual(ticketTypeValue, GetFirstSelectedItemInDropdown(ticketTypeDd));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickAnyTicketType(string ticketType)
        {
            ClickOnElement(ticketTypeOption, ticketType);
            //Click out of dropdown
            ClickOnElement(ticketTypeLabel);
            return this;
        }

        //Haulier Dd
        [AllureStep]
        public CreateNewTicketPage VerifyDisplayHaulierDd()
        {
            WaitUtil.WaitForElementVisible(haulierDd);
            Assert.IsTrue(IsControlDisplayed(haulierDd));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickAnyHaulier(string haulierName)
        {
            ClickOnElement(haulierOption, haulierName);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickAnySource(string sourceName)
        {
            SelectTextFromDropDown(SourcePartySelect, sourceName);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickAddTicketLineBtn()
        {
            ScrollToBottomOfPage();
            ClickOnElement(addTicketLineBtn);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage InputLicenceNumberExpDate()
        {
            SendKeys(licenceNumberExpDate, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage InputLicenceNumberExpDate(string licenceNumberValueExp)
        {
            SendKeys(licenceNumberExpDate, licenceNumberValueExp);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage InputLicenceNumber()
        {
            SendKeys(licenceNumber, CommonUtil.GetRandomNumber(5));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage InputLicenceNumber(string licenceNumberValue)
        {
            SendKeys(licenceNumber, licenceNumberValue);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyValueInLicenceNumber(string licenceNumberValue)
        {
            Assert.AreEqual(licenceNumberValue, GetAttributeValue(licenceNumber, "value"));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyValueInLicenceNumberExp(string licenceNumberExpValue)
        {
            Assert.AreEqual(licenceNumberExpValue, GetAttributeValue(licenceNumberExpDate, "value"));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage InputNetQuantity(string netQtyValue)
        {
            SendKeys(netQuantity, netQtyValue);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickOnNoWarningPopup()
        {
            WaitUtil.WaitForElementVisible(titleWarningPopup);
            ClickOnElement(noBtn);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickOnYesWarningPopup()
        {
            WaitUtil.WaitForElementVisible(titleWarningPopup);
            ClickOnElement(yesBtn);
            return this;
        }
        [AllureStep]
        public string GetTicketNumber()
        {
            return GetAttributeValue(ticketNumberInput, "value");
        }
        //PAY for this ticket
        private readonly By titlePayForThisTicketPopup = By.XPath("//h4[text()='Pay for this ticket']");
        private readonly By amountPaidInput = By.CssSelector("input[id='amount-paid']");
        private readonly By paymentMethodDd = By.CssSelector("select[id='payment-method']");
        private readonly By paymentRefInput = By.CssSelector("input[id='payment-reference']");
        private readonly By noteInput = By.CssSelector("textarea[id='payment-notes']");
        private readonly By payBtn = By.XPath("//button[text()='Pay']");
        private readonly By cancelPayForThisTicketPopupBtn = By.XPath("//button[text()='Pay']/following-sibling::button[text()='Cancel']");

        [AllureStep]
        public CreateNewTicketPage IsPayForThisTicketPopup()
        {
            WaitUtil.WaitForElementVisible(titlePayForThisTicketPopup);
            Assert.IsTrue(IsControlDisplayed(amountPaidInput));
            Assert.IsTrue(IsControlDisplayed(paymentMethodDd));
            Assert.IsTrue(IsControlDisplayed(paymentRefInput));
            Assert.IsTrue(IsControlDisplayed(noteInput));
            Assert.IsTrue(IsControlEnabled(payBtn));
            Assert.IsTrue(IsControlEnabled(cancelPayForThisTicketPopupBtn));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyAmountPaidValue(string grossValue)
        {
            Assert.AreEqual(grossValue, GetAttributeValue(amountPaidInput, "value"));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage InputAmountPaidValue(string grossValue)
        {
            SendKeys(amountPaidInput, grossValue);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage ClickOnPayBtn()
        {
            ClickOnElement(payBtn);
            return this;
        }

        //Warning - Amount Paid not equal to ticket price popup
        private readonly By titleWarningAmountPaidPopup = By.XPath("//h4[text()='Warning - Amount Paid not equal to ticket price.']");
        private readonly By contentWarningAmountPaidPopup = By.XPath("//div[text()='Put on a grey List?']");
        private readonly By yesBtnWarningAmountPaidPopup = By.XPath("//div[text()='Put on a grey List?']/ancestor::div/following-sibling::div/button[text()='Yes']");
        private readonly By noBtnWarningAmountPaidPopup = By.XPath("//div[text()='Put on a grey List?']/ancestor::div/following-sibling::div/button[text()='No']");

        [AllureStep]
        public CreateNewTicketPage IsWarningAmountPaidNotEqualToTicketPrice()
        {
            WaitUtil.WaitForElementVisible(titleWarningAmountPaidPopup);
            WaitUtil.WaitForElementVisible(contentWarningAmountPaidPopup);
            Assert.IsTrue(IsControlDisplayed(yesBtnWarningAmountPaidPopup));
            Assert.IsTrue(IsControlDisplayed(noBtnWarningAmountPaidPopup));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage ClickOnYesWarningAmountPaidPopupBtn()
        {
            ClickOnElement(yesBtnWarningAmountPaidPopup);
            return this;
        }

        //Grey list popup
        private readonly By greylistCodeDd = By.CssSelector("select[id='greylist-code']");
        private readonly By commentInput = By.CssSelector("textarea[id='greylist-comment']");
        private readonly By saveBtnInGreyListCodePopup = By.XPath("//div[@id='payment-grey-lists-modal']//button[text()='Save']");

        [AllureStep]
        public CreateNewTicketPage IsGreyListCodePopup()
        {
            WaitUtil.WaitForElementVisible(greylistCodeDd);
            WaitUtil.WaitForElementVisible(commentInput);
            Assert.IsTrue(IsControlDisplayed(saveBtnInGreyListCodePopup));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyCommentValueGreyListCodePopup(string ticketNumberValue, string ticketPriceValue, string amountPaidValue)
        {
            string[] title = { "Ticket Number: ", "Ticket Price: ", "Amount Paid: " };
            string[] exValue = { ticketNumberValue, ticketPriceValue, amountPaidValue };
            string textDisplayed = GetAttributeValue(commentInput, "value");
            string[] formatText = textDisplayed.Split(Environment.NewLine);
            for (int i = 0; i < formatText.Length; i++)
            {
                Assert.IsTrue(formatText[i].Contains(title[i] + exValue[i]), "Value at " + title[i] + "is not correct");
            }
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage SelectOnGreylistCode(string greylistCodeValue)
        {
            SelectTextFromDropDown(greylistCodeDd, greylistCodeValue);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage ClickOnSaveGreyListCodePopupBtn()
        {
            ClickOnElement(saveBtnInGreyListCodePopup);
            return this;
        }

        //Ticket line - Product Column
        [AllureStep]
        public CreateNewTicketPage ClickProductDd()
        {
            ClickOnElement(productDd);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickAnyProductValue(string productValue)
        {
            ClickOnElement(productOption, productValue);
            return this;
        }

        //Ticket line - Location Column
        [AllureStep]
        public CreateNewTicketPage VerifyNoLocationIsPrepolulated()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(locationDd), "Select...");
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyActiveLocationIsDisplayed(string locationActiveName)
        {
            ClickOnElement(locationDd);
            //Verify
            Assert.IsTrue(IsControlDisplayed(locationOption, locationActiveName));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyLocationNotDisplayed(string locatioName)
        {
            Assert.IsTrue(IsControlUnDisplayed(locationOption, locatioName));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyActiveLocationIsDisplayed(string[] allLocationActiveName)
        {
            ClickOnElement(locationDd);
            //Verify
            foreach (string location in allLocationActiveName)
            {
                Assert.IsTrue(IsControlDisplayed(locationOption, location));
            }
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyLocationPrepolulated(string locationName)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(locationDd), locationName);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage VerifyLocationDeletedNotDisplay(string locationDeleted)
        {
            ClickOnElement(locationDd);
            //Verify
            Assert.IsTrue(IsControlUnDisplayed(locationOption, locationDeleted));
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage ClickLocationDd()
        {
            ClickOnElement(locationDd);
            return this;
        }
        [AllureStep]
        public CreateNewTicketPage SelectLocation(string locationName)
        {
            ClickOnElement(locationOption, locationName);
            return this;
        }

        //Ticket line - First Weight
        [AllureStep]
        public CreateNewTicketPage InputFirstWeight(int firstW)
        {
            SendKeys(firstWeightInput, firstW.ToString());
            return this;
        }

        //Ticket line - First Date
        [AllureStep]
        public CreateNewTicketPage InputFirstDate()
        {
            SendKeys(firstDateInput, CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2));
            return this;
        }

        //Ticket line - Second Weight
        [AllureStep]
        public CreateNewTicketPage InputSecondWeight(int secondW)
        {
            SendKeys(secondWeightInput, secondW.ToString());
            return this;
        }

        //Ticket line - Second Date
        [AllureStep]
        public CreateNewTicketPage InputSecondDate()
        {
            SendKeys(secondDateInput, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }

        //Driver name
        [AllureStep]
        public CreateNewTicketPage InputDriverName(string driverNameValue)
        {
            SendKeys(driverNameInput, driverNameValue);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyValueAtDriverName(string driverNameValue)
        {
            Assert.AreEqual(driverNameValue, GetAttributeValue(driverNameInput, "value"));
            return this;
        }

        //Greylist code model

        #region
        private readonly By titleGreylistCodeMode = By.XPath("//div[@id='grey-lists-modal']//div[@class='modal-header']/h4");
        private readonly By greylistIdTitle = By.XPath("//div[text()='Grey List ID ']");
        private readonly By greylistCodeTitle = By.XPath("//div[text()='Greylist Code']");
        private readonly By greylistCodeText = By.CssSelector("div[data-bind='text: $data.name']");
        private readonly By okBtn = By.XPath("//div[@id='grey-lists-modal']//button[text()='OK']");
        private readonly By greyId = By.XPath("//span[@title='Click to open the grey list']");
        private readonly string greyListCodeByIndex = "(//div[@class='row']//div[@data-bind='text: $data.name'])[{0}]";

        [AllureStep]
        public CreateNewTicketPage IsGreylistCodeModel(string resourceNameValue, string greylistCodeValue)
        {
            WaitUtil.WaitForElementVisible(titleGreylistCodeMode);
            WaitUtil.WaitForElementVisible(DetailTab);
            Assert.IsTrue(IsControlDisplayed(greylistIdTitle));
            Assert.IsTrue(IsControlDisplayed(greylistCodeTitle));
            Assert.AreEqual("Vehicle " + resourceNameValue + " is on the grey list", GetElementText(titleGreylistCodeMode));
            Assert.AreEqual(greylistCodeValue, GetElementText(greylistCodeText));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage IsGreylistCodeModel(string resourceNameValue, string[] greylistCodeValue)
        {
            WaitUtil.WaitForElementVisible(titleGreylistCodeMode);
            Assert.IsTrue(IsControlDisplayed(greylistIdTitle));
            Assert.IsTrue(IsControlDisplayed(greylistCodeTitle));
            Assert.AreEqual("Vehicle " + resourceNameValue + " is on the grey list", GetElementText(titleGreylistCodeMode));
            for (int i = 0; i < greylistCodeValue.Length; i++)
            {
                Assert.AreEqual(greylistCodeValue[i], GetElementText(greyListCodeByIndex, (i + 1).ToString()));
            }
            return this;
        }

        [AllureStep]
        public string GetGreylistCodeId()
        {
            return GetElementText(greyId);
        }

        [AllureStep]
        public CreateNewTicketPage VerityGreylistCodeModelIsNotDisplayed()
        {
            Assert.IsTrue(IsControlUnDisplayed(greylistIdTitle));
            Assert.IsTrue(IsControlUnDisplayed(greylistCodeTitle));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage ClickOnOkBtn()
        {
            ClickOnElement(okBtn);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage WaitForPopupDisappear()
        {
            WaitUtil.WaitForElementVisible(okBtn);
            WaitUtil.WaitForElementVisible(greylistCodeTitle);
            WaitUtil.WaitForPageLoaded();
            SleepTimeInSeconds(2);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyGreylistCodeModelDisappear()
        {
            WaitUtil.WaitForElementInvisible(titleGreylistCodeMode);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage ClickOnGreyListId()
        {
            ClickOnElement(greyId);
            return PageFactoryManager.Get<CreateGreyListPage>();
        }

        #endregion

        [AllureStep]
        public string GetWBTicketId()
        {
            return GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/weighbridge-tickets/", "");
        }

        #region Text under [Source]
        private readonly By accountNumberUnderSource = By.XPath("//select[@id='source-party']/following-sibling::div/p[contains(text(), 'Account Number')]");
        private readonly By clientUnderSource = By.XPath("//select[@id='source-party']/following-sibling::div/p[contains(text(), 'Client #')]");
        private readonly By accountRefUnderSource = By.XPath("//select[@id='source-party']/following-sibling::div/p[contains(text(), 'Account Reference')]");
        private readonly By correspondenceAddressUnderSource = By.XPath("//select[@id='source-party']/following-sibling::div/p[contains(text(), 'Correspondence Address')]");
        private readonly By manualSourcePartyInputNextToSourceField = By.CssSelector("input[id='manual-source-party']");
        private readonly By manualDestinationInputNextToSourceField = By.XPath("//label[text()='Destination']/following-sibling::div/input[@id='manual-destination-party']");
        private readonly By sourceInput = By.CssSelector("//select[@id='source-party']");

        [AllureStep]
        public CreateNewTicketPage VerifyTextUnderSourceField(string accountNumberValue)
        {
            Assert.AreEqual(("Account Number" + accountNumberValue), GetElementText(accountNumberUnderSource));
            Assert.IsTrue(IsControlDisplayed(clientUnderSource), "[Client #] is not displayed");
            Assert.IsTrue(IsControlDisplayed(accountRefUnderSource), "[Account Reference ] is not displayed");
            Assert.IsTrue(IsControlDisplayed(correspondenceAddressUnderSource), "[Correspondence Address ] is not displayed");
            Assert.IsTrue(IsControlDisplayed(manualSourcePartyInputNextToSourceField));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyTextFieldIsNotDisplayedNextToSourceDd()
        {
            Assert.IsTrue(IsControlUnDisplayed(manualSourcePartyInputNextToSourceField));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage InputManualSourceParty(string manualSourcePartyValue)
        {
            SendKeys(manualSourcePartyInputNextToSourceField, manualSourcePartyValue);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyValueInManualSourceParty(string manualSourcePartyValue)
        {
            Assert.AreEqual(manualSourcePartyValue, GetAttributeValue(manualSourcePartyInputNextToSourceField, "value"));
            return this;
        }

        [AllureStep]
        public string GetSourceValue()
        {
            return GetAttributeValue(sourceInput, "value");
        }

        [AllureStep]
        public CreateNewTicketPage VerifyTextFieldNextToDestinationField()
        {
            Assert.IsTrue(IsControlDisplayed(manualDestinationInputNextToSourceField));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage InputManualDestinationParty(string manualDestinationPartyValue)
        {
            SendKeys(manualDestinationInputNextToSourceField, manualDestinationPartyValue);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyValueInDestinationParty(string manualDestinationPartyValue)
        {
            Assert.AreEqual(manualDestinationPartyValue, GetAttributeValue(manualDestinationInputNextToSourceField, "value"));
            return this;
        }

        #endregion

        #region [Please select a reason for making the changes?] popup
        private readonly By titleInReasonPopup = By.XPath("//div[@id='ticket-state-resolution-codes']//h4[text()='Please select a reason for making the changes?']");
        private readonly By reasonBtnInReasonPopup = By.CssSelector("div[id='ticket-state-resolution-codes'] button[data-id='resolution-codes']");
        private readonly By noteTextInReasonPopup = By.CssSelector("div[id='ticket-state-resolution-codes'] textarea[id='resolution-note']");
        private readonly By cancelBtnInReasonPopup = By.XPath("//div[@id='ticket-state-resolution-codes']//button[text()='Cancel']");
        private readonly By saveBtnInReasonPopup = By.XPath("//div[@id='ticket-state-resolution-codes']//button[text()='Save']");

        [AllureStep]
        public CreateNewTicketPage IsSelectReasonPopup()
        {
            WaitUtil.WaitForElementVisible(titleInReasonPopup);
            Assert.IsTrue(IsControlDisplayed(titleInReasonPopup));
            Assert.IsTrue(IsControlDisplayed(reasonBtnInReasonPopup));
            Assert.IsTrue(IsControlDisplayed(noteTextInReasonPopup));
            Assert.IsTrue(IsControlDisplayed(cancelBtnInReasonPopup));
            Assert.IsTrue(IsControlDisplayed(saveBtnInReasonPopup));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage InputNoteInReasonPopup()
        {
            SendKeys(noteTextInReasonPopup, "Note auto");
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage ClickOnSaveBtnInReasonPopup()
        {
            ClickOnElement(saveBtnInReasonPopup);
            return this;
        }

        #endregion

        #region PO Number
        private readonly By ddUnderPONumber = By.CssSelector("select[id='po-number']");
        private readonly By freeEntryFieldNextToPONumber = By.CssSelector("input[id='po-number']");
        private readonly string poNumberOption = "//select[@id='po-number']/option[text()='{0}']";

        [AllureStep]
        public CreateNewTicketPage VerifyDisplayDdUnderPONumber()
        {
            Assert.IsTrue(IsControlDisplayed(ddUnderPONumber));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyDisplayFreeEntryFieldNextToPONumber()
        {
            Assert.IsTrue(IsControlDisplayed(freeEntryFieldNextToPONumber));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage InputFreeEntryFieldNextToPONumber(string value)
        {
            SendKeys(freeEntryFieldNextToPONumber, value);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage ClickOnPONumberAndVerifyValue(string[] poNumberValue)
        {
            foreach(string poNumber in poNumberValue)
            {
                Assert.IsTrue(IsControlDisplayed(string.Format(poNumberOption, poNumber), poNumber + " is not displayed in [PO Number] dd"));
            }
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyDefaultValueInPoNumberDd(string poNumberValue) 
        {
            Assert.AreEqual(poNumberValue, GetFirstSelectedItemInDropdown(ddUnderPONumber));
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage ClickOnAnyPONumber(string poNumberValue)
        {
            ClickOnElement(poNumberOption, poNumberValue);
            return this;
        }

        [AllureStep]
        public CreateNewTicketPage VerifyValueInFreeEntryField(string freeEntryFieldValue)
        {
            Assert.AreEqual(freeEntryFieldValue, GetAttributeValue(freeEntryFieldNextToPONumber, "value"));
            return this;
        }

        #endregion
    }

}
