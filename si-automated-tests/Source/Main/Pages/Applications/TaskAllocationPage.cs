using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class TaskAllocationPage : BasePageCommonActions
    {
        public readonly By ContractSelect = By.XPath("//select[@id='contract']");
        public readonly By ButtonGo = By.XPath("//button[@id='button-go']");
        public readonly By FromInput = By.XPath("//input[@id='from']");
        public readonly By ToInput = By.XPath("//input[@id='to']");
        public readonly By ServiceInput = By.XPath("//input[@id='services']");
        public readonly By UnallocatedTable = By.XPath("//div[@id='unallocated']//div[@class='grid-canvas']");
        public readonly By LockFilterInput = By.XPath("//div[contains(@id, 'round-tab')]//div[contains(@class, 'l27')]//input");
        public readonly By IdFilterInput = By.XPath("//div[contains(@id, 'round-tab')]//div[contains(@class, 'l3')]//input");

        public readonly string UnallocatedRow = "./div[contains(@class, 'slick-row')]";
        public readonly string UnallocatedCheckbox = "./div[contains(@class, 'slick-cell l1 r1')]//input";
        public TableElement UnallocatedTableEle
        {
            get => new TableElement("//div[contains(@id, 'round-tab')]//div[@class='grid-canvas']", UnallocatedRow, new List<string>() { UnallocatedCheckbox });
        }

        private TreeViewElement _treeViewElement = new TreeViewElement("//div[contains(@class, 'jstree-1')]", "./li[contains(@role, 'treeitem')]", "./a", "./ul[contains(@class, 'jstree-children')]", "./i[contains(@class, 'jstree-ocl')][1]");
        private TreeViewElement ServicesTreeView
        {
            get => _treeViewElement;
        }

        public readonly string RoundInstanceTable = "//div[@id='roundGrid']//div[@class='grid-canvas']";
        public readonly string RoundInstanceRow = "./div[contains(@class, 'slick-row')]";
        public readonly string RoundInstanceServiceCell = "./div[contains(@class, 'l0')]";
        public readonly string RoundInstanceRoundGroupCell = "./div[contains(@class, 'l1')]";
        public readonly string RoundInstanceRoundCell = "./div[contains(@class, 'l2')]";
        public readonly string RoundInstanceFromCell = "./div[contains(@class, 'l3')]";
        public readonly string RoundInstanceToCell = "./div[contains(@class, 'l4')]";

        public TableElement RoundInstanceTableEle
        {
            get => new TableElement(RoundInstanceTable, RoundInstanceRow, new List<string>() { RoundInstanceServiceCell, RoundInstanceRoundGroupCell, RoundInstanceRoundCell, RoundInstanceFromCell, RoundInstanceToCell });
        }

        public readonly By AllocatingConfirmMsg = By.XPath("//div[text()='Allocating 1 Task(s) onto Round Instance for a different day!']");
        public readonly By AllocatingConfirmMsg2 = By.XPath("//div[text()='Allocating 1 locked Tasks from/to dispatched round. Ensure that new Crew can attempt service']");
        public readonly By AllocateAllButton = By.XPath("//button[text()='Allocate All']");
        public readonly By AllocationReasonSelect = By.XPath("//select[contains(@data-bind, 'options: allocationReasons')]");
        public readonly By AllocationConfirmReasonButton = By.XPath("//button[contains(@data-bind, 'click: confirmReasonCallback')]");

        public TaskAllocationPage DoubleClickFromCellOnRound(string round)
        {
            RoundInstanceTableEle.DoubleClickCellOnCellValue(4, 2, round);
            return this;
        }

        public TaskAllocationPage SelectRoundNode(string nodeName)
        {
            ServicesTreeView.ClickItem(nodeName);
            return this;
        }

        public TaskAllocationPage ExpandRoundNode(string nodeName)
        {
            ServicesTreeView.ExpandNode(nodeName);
            return this;
        }

        public TaskAllocationPage DragRoundInstanceToUnlocattedGrid(string roundGroup, string round)
        {
            IWebElement cell = RoundInstanceTableEle.GetCellByCellValues(3, new Dictionary<int, object>() 
            {
                { 1, roundGroup },
                { 2, round }
            });
            IWebElement grid = GetElement(UnallocatedTable);
            DragAndDrop(cell, grid);
            return this;
        }

        public TaskAllocationPage ClickUnallocatedRow()
        {
            UnallocatedTableEle.ClickCell(0, 0);
            return this;
        }

        public TaskAllocationPage DragUnallocatedRowToRoundInstance(string roundGroup, string round)
        {
            IWebElement cell = RoundInstanceTableEle.GetCellByCellValues(4, new Dictionary<int, object>()
            {
                { 1, roundGroup },
                { 2, round }
            });
            WaitUtil.WaitForElementClickable(cell).Click();
            WaitForLoadingIconToDisappear();
            cell = RoundInstanceTableEle.GetCellByCellValues(4, new Dictionary<int, object>()
            {
                { 1, roundGroup },
                { 2, round }
            });
            IWebElement row = UnallocatedTableEle.GetRow(0);
            Actions a = new Actions(driver);
            a.ClickAndHold(row).Perform();
            a.MoveToElement(cell).Perform();
            a.Release().Perform();
            return this;
        }

        public TaskAllocationPage VerifyTaskAllocated(string roundGroup, string round)
        {
            SleepTimeInMiliseconds(200);
            IWebElement cell = RoundInstanceTableEle.GetCellByCellValues(4, new Dictionary<int, object>()
            {
                { 1, roundGroup },
                { 2, round }
            });
            string borderLeft = cell.FindElement(By.XPath("./div/span")).GetCssValue("border-left-color");
            Assert.IsTrue(borderLeft.Contains("rgba(0, 128, 0, 1)"));
            return this;
        }

        public TaskAllocationPage UnallocatedHorizontalScrollToElement(By element, bool isScrollRight = true)
        {
            IWebElement e = this.driver.FindElement(element);
            Actions actions = new Actions(this.driver);
            actions.MoveToElement(e).Perform();
            return this;
        }
    }
}
