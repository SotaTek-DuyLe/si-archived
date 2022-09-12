using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;

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
        public readonly By SourcePartySelect = By.XPath("//select[@id='source-party']");
        public readonly By PONumberInput = By.XPath("//input[@id='po-number']");
        public readonly By AddButton = By.XPath("//button[text()='Add' and contains(@data-bind, 'addTicketLine')]");
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
        public readonly By CancelReasonSelect = By.XPath("//div[@id='ticket-state-resolution-codes-cancel']//select[@id='resolution-codes']"); 
        public readonly By CancelReasonNote = By.XPath("//div[@id='ticket-state-resolution-codes-cancel']//textarea[@id='resolution-note']"); 
        public readonly By CancelReasonButton = By.XPath("//div[@id='ticket-state-resolution-codes-cancel']//button[text()='Cancel Ticket']"); 
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

        public CreateNewTicketPage VerifyTicketLineData(int rowIdx, string product, string firstWeight, string secondWeight, string firtDate, string secondDate)
        {
            return this;
        }
        #endregion

        #region Price Line Table
        private readonly string AddPriceLineButton = "./button[contains(@data-bind, 'addPriceLine')]";
        private readonly string PriceLineRow = "./tr";
        private readonly string DescriptionCell = "./td//input[contains(@data-bind, 'description')]";
        private readonly string UnitCell = "./td//select[contains(@data-bind, 'units')]";
        private readonly string NetQtyCell = "./td//input[contains(@data-bind, 'quantity')]";
        private readonly string OverrideUnitPriceCell = "./td//input[contains(@data-bind, 'overridePrice')]";
        private readonly string RemoveButtonCell = "./td//button[text()='Remove']";

        public CreateNewTicketPage ClickAddPriceLine(int ticketLineIdx)
        {
            string priceLineTable = $"//div[@id='details-tab']//table//tbody[@data-bind='foreach: ticketLines']//tr[contains(@data-bind, 'ticketLine.priceLines')][{ticketLineIdx}]/td";
            IWebElement priceLineRow = GetElement(By.XPath(priceLineTable));
            ClickOnElement(priceLineRow.FindElement(By.XPath(AddPriceLineButton)));
            return this;
        }
        #endregion

        public CreateNewTicketPage VerifyTakePayment()
        {
            VerifyElementVisibility(TakePaymentTitle, true);
            VerifyElementVisibility(YesTakePaymentButton, true);
            VerifyElementVisibility(NoTakePaymentButton, true);
            return this;
        }

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

        public CreateNewTicketPage ClickStationDdAndSelectStation(String stationName)
        {
            ClickOnElement(stationDd);
            ClickOnElement(stationOption, stationName);
            return this;
        }

        public CreateNewTicketPage VerifyDisplayVehicleRegInput()
        {
            Assert.IsTrue(IsControlDisplayed(vehicleReg));
            return this;
        }

        public CreateNewTicketPage InputVehicleRegInput(string vehicleValue)
        {
            SendKeys(vehicleReg, vehicleValue);
            ClickOnElement(anyOption, vehicleValue);
            ClickOnElement(title);
            return this;
        }

        public CreateNewTicketPage InputVehicleRegInputAndClickOK(string vehicleValue)
        {
            SendKeys(vehicleReg, vehicleValue);
            ClickOnElement(anyOption, vehicleValue);
            ClickOnElement(By.XPath("//div[@id='grey-lists-modal']//button[text()='OK']"));
            return this;
        }

        public CreateNewTicketPage VerifyDisplayVehicleType(string vehicleTypeName)
        {
            WaitUtil.WaitForElementVisible(vehicleTypeInput);
            Assert.IsTrue(IsControlDisplayed(vehicleTypeInput));
            //Verify value
            Assert.AreEqual(GetAttributeValue(vehicleTypeInput, "value"), vehicleTypeName);
            return this;
        }

        //Ticket Type Dd
        public CreateNewTicketPage VerifyDisplayTicketTypeInput()
        {
            WaitUtil.WaitForElementVisible(ticketTypeInput);
            Assert.IsTrue(IsControlDisplayed(ticketTypeInput));
            return this;
        }

        public CreateNewTicketPage ClickTicketType()
        {
            ClickOnElement(ticketTypeDd);
            return this;
        }

        public CreateNewTicketPage VerifyValueInTicketTypeDd()
        {
            foreach(string ticket in CommonConstants.TicketType)
            {
                Assert.IsTrue(IsControlDisplayed(ticketTypeOption, ticket));
            }
            return this;
        }

        public CreateNewTicketPage ClickAnyTicketType(string ticketType)
        {
            ClickOnElement(ticketTypeOption, ticketType);
            //Click out of dropdown
            ClickOnElement(ticketTypeLabel);
            return this;
        }

        //Haulier Dd
        public CreateNewTicketPage VerifyDisplayHaulierDd()
        {
            WaitUtil.WaitForElementVisible(haulierDd);
            Assert.IsTrue(IsControlDisplayed(haulierDd));
            return this;
        }

        public CreateNewTicketPage ClickAnyHaulier(string haulierName)
        {
            ClickOnElement(haulierOption, haulierName);
            return this;
        }

        public CreateNewTicketPage ClickAddTicketLineBtn()
        {
            ScrollToBottomOfPage();
            ClickOnElement(addTicketLineBtn);
            return this;
        }

        public CreateNewTicketPage InputLicenceNumberExpDate()
        {
            SendKeys(licenceNumberExpDate, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }

        public CreateNewTicketPage InputLicenceNumber()
        {
            SendKeys(licenceNumber, CommonUtil.GetRandomNumber(5));
            return this;
        }

        //Ticket line - Product Column
        public CreateNewTicketPage ClickProductDd()
        {
            ClickOnElement(productDd);
            return this;
        }

        public CreateNewTicketPage ClickAnyProductValue(string productValue)
        {
            ClickOnElement(productOption, productValue);
            return this;
        }

        //Ticket line - Location Column
        public CreateNewTicketPage VerifyNoLocationIsPrepolulated()
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(locationDd), "Select...");
            return this;
        }

        public CreateNewTicketPage VerifyActiveLocationIsDisplayed(string locationActiveName)
        {
            ClickOnElement(locationDd);
            //Verify
            Assert.IsTrue(IsControlDisplayed(locationOption, locationActiveName));
            return this;
        }

        public CreateNewTicketPage VerifyLocationNotDisplayed(string locatioName)
        {
            Assert.IsTrue(IsControlUnDisplayed(locationOption, locatioName));
            return this;
        }

        public CreateNewTicketPage VerifyActiveLocationIsDisplayed(string[] allLocationActiveName)
        {
            ClickOnElement(locationDd);
            //Verify
            foreach(string location in allLocationActiveName)
            {
                Assert.IsTrue(IsControlDisplayed(locationOption, location));
            }
            return this;
        }

        public CreateNewTicketPage VerifyLocationPrepolulated(string locationName)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(locationDd), locationName);
            return this;
        }

        public CreateNewTicketPage VerifyLocationDeletedNotDisplay(string locationDeleted)
        {
            ClickOnElement(locationDd);
            //Verify
            Assert.IsTrue(IsControlUnDisplayed(locationOption, locationDeleted));
            return this;
        }

        public CreateNewTicketPage ClickLocationDd()
        {
            ClickOnElement(locationDd);
            return this;
        }

        public CreateNewTicketPage SelectLocation(string locationName)
        {
            ClickOnElement(locationOption, locationName);
            return this;
        }

        //Ticket line - First Weight
        public CreateNewTicketPage InputFirstWeight(int firstW)
        {
            SendKeys(firstWeightInput, firstW.ToString());
            return this;
        }

        //Ticket line - First Date
        public CreateNewTicketPage InputFirstDate()
        {
            SendKeys(firstDateInput, CommonUtil.GetLocalTimeMinusDay(CommonConstants.DATE_DD_MM_YYYY_FORMAT, 2));
            return this;
        }

        //Ticket line - Second Weight
        public CreateNewTicketPage InputSecondWeight(int secondW)
        {
            SendKeys(secondWeightInput, secondW.ToString());
            return this;
        }

        //Ticket line - Second Date
        public CreateNewTicketPage InputSecondDate()
        {
            SendKeys(secondDateInput, CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            return this;
        }
    }
}
