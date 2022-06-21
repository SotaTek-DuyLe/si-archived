using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.WB.Tickets
{
    public class CreateNewTicketPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Weighbridge Ticket']");
        private readonly By ticketDateInput = By.CssSelector("input#ticket-date");
        private readonly By stationDd = By.CssSelector("select#stations");
        private readonly By vehicleReg = By.XPath("//label[text()='Vehicle Reg']/following-sibling::div//input");
        private readonly By ticketTypeInput = By.CssSelector("select#ticket-type");
        private readonly By vehicleTypeInput = By.CssSelector("input#resource-type");
        private readonly By ticketTypeDd = By.CssSelector("select[id='ticket-type']");
        private readonly By ticketTypeLabel = By.XPath("//label[text()='Ticket Type']");
        private readonly By haulierDd = By.CssSelector("select[id='haulier']");
        private readonly By addTicketLineBtn = By.XPath("//button[text()='Add']");
        private readonly By licenceNumberExpDate = By.CssSelector("input[id='licence-number-expiry']");
        private readonly By licenceNumber = By.CssSelector("input[id='licence-number']");

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
            //Click out of dropdown
            ClickOnElement(ticketTypeLabel);
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
