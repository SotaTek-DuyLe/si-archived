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

        //DYNAMIC
        private const string stationOption = "//select[@id='stations']/option[text()='{0}']";
        private const string anyOption = "//li[text()='{0}']";

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

        public CreateNewTicketPage VerifyDisplayTicketTypeInput()
        {
            WaitUtil.WaitForElementVisible(ticketTypeInput);
            Assert.IsTrue(IsControlDisplayed(ticketTypeInput));
            return this;
        }
    }
}
