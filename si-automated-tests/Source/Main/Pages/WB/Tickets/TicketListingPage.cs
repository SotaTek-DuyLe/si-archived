using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.WB.Tickets
{
    public class TicketListingPage : BasePage
    {
        private readonly By addNewTicketBtn = By.XPath("//button[text()='Add New Item']");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private string TicketTable = "//div[@class='grid-canvas']";
        private string TicketRow = "./div[contains(@class, 'slick-row')]";
        private string TicketCheckboxCell = "./div[contains(@class, 'l0')]//input";
        private string TicketIdCell = "./div[contains(@class, 'l1')]";
        private string TicketNumber = "./div[contains(@class, 'l2')]";
        private readonly By loadingIconFrameTab = By.XPath("//div[@id='form']/following-sibling::div/div[@class='loading-data']");

        public TableElement TicketTableEle
        {
            get => new TableElement(TicketTable, TicketRow, new List<string>() { TicketCheckboxCell, TicketIdCell, TicketNumber });
        }

        [AllureStep]
        public string GetFirstTicketNumber()
        {
            return TicketTableEle.GetCellValue(0, 2).AsString();
        }

        private readonly By selectAllCheckbox = By.XPath("//div[@title='Select/Deselect All']//input");
        private readonly By firstResult = By.XPath("//div[@class='grid-canvas']/div[1]");

        [AllureStep]
        public TicketListingPage ClickAddNewTicketBtn()
        {
            ClickOnElement(addNewTicketBtn);
            return this;
        }
        [AllureStep]
        public TicketListingPage FilterTicketById(int id)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(addNewTicketBtn);
            SendKeys(filterInputById, id.ToString());
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public TicketListingPage FilterTicketById(string ticketId)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, ticketId);
            WaitUtil.WaitForElementVisible(loadingIconFrameTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public WeighbridgeTicketDetailPage OpenFirstResult()
        {
            ClickOnElement(selectAllCheckbox);
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<WeighbridgeTicketDetailPage>();
        }

        [AllureStep]
        public TicketListingPage IsTicketListingPage()
        {
            WaitUtil.WaitForElementVisible(addNewTicketBtn);
            return this;
        }

    }
}
