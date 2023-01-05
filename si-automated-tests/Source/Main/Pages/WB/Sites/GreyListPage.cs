using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.WB
{
    public class GreyListPage : BasePageCommonActions
    {
        private string GreyListTable = "//div[@class='grid-canvas']";
        private string GreyListRow = "./div[contains(@class, 'slick-row')]";
        private string GreyListCheckboxCell = "./div[contains(@class, 'l0')]//input";
        private string GreyListIdCell = "./div[contains(@class, 'l1')]";
        private string GreyListVehicle = "./div[contains(@class, 'l2')]";
        private string GreyListTicket = "./div[contains(@class, 'l3')]";
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By deleteItemBtn = By.XPath("//button[text()='Delete Item']");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By filterInputByTicketNumber = By.XPath("//div[contains(@class, 'l3 r3')]/descendant::input");
        private readonly By selectAllCheckbox = By.XPath("//div[@title='Select/Deselect All']//input");
        private readonly By firstResult = By.XPath("//div[@class='grid-canvas']/div[1]");
        private readonly By firstCheckboxResult = By.XPath("//div[@class='grid-canvas']/div[1]//input");

        public TableElement GreyListTableEle
        {
            get => new TableElement(GreyListTable, GreyListRow, new List<string>() { GreyListCheckboxCell, GreyListIdCell, GreyListVehicle, GreyListTicket });
        }

        [AllureStep]
        public GreyListPage DoubleClickRow(string ticketNumber)
        {
            var row = GreyListTableEle.GetRowByCellValue(3, ticketNumber);
            DoubleClickOnElement(row);
            return this;
        }

        [AllureStep]
        public GreyListPage IsGreyListPage()
        {
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            WaitUtil.WaitForElementVisible(deleteItemBtn);
            return this;
        }

        [AllureStep]
        public GreyListPage ClickOnAddNewItemBtn()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }

        [AllureStep]
        public GreyListPage FilterGreylistCodeByIdToDelete(string greylistId)
        {
            SendKeys(filterInputById, greylistId);
            WaitForLoadingIconToDisappear();
            SleepTimeInSeconds(2);
            ClickOnElement(firstCheckboxResult);
            return this;
        }

        [AllureStep]
        public GreyListPage FilterGreylistCodeById(string greylistId)
        {
            SendKeys(filterInputById, greylistId);
            WaitForLoadingIconToDisappear();
            ClickOnElement(selectAllCheckbox);
            DoubleClickOnElement(firstResult);
            return this;
        }

        [AllureStep]
        public GreyListPage FilterGreylistCodeByTicket(string ticketNumberValue)
        {
            SendKeys(filterInputByTicketNumber, ticketNumberValue);
            WaitForLoadingIconToDisappear();
            ClickOnElement(selectAllCheckbox);
            DoubleClickOnElement(firstResult);
            return this;
        }

        [AllureStep]
        public GreyListPage ClickOnDeleteBtn()
        {
            ClickOnElement(deleteItemBtn);
            return this;
        }

        [AllureStep]
        public GreyListPage VerifyRecordNoLongerDisplayInGrid()
        {
            Assert.IsTrue(IsControlUnDisplayed(firstResult));
            return this;
        }

    }
}
