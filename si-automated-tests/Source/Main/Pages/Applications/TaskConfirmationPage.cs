using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class TaskConfirmationPage : BasePageCommonActions
    {
        public TaskConfirmationPage()
        {
            unallocatedTableEle = new TableElement("//div[@id='grid']//div[@class='grid-canvas']", UnallocatedRow, new List<string>() { UnallocatedCheckbox, UnallocatedState, UnallocatedID });
            unallocatedTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };

            slickRoundTableEle = new TableElement("//div[@id='grid']//div[@class='grid-canvas']", SlickRoundRow, new List<string>() { RoundDescriptionCell });
            slickRoundTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        public readonly By ContractSelect = By.XPath("//select[@id='contract']");
        public readonly By ServiceInput = By.XPath("//input[@id='services']");
        public readonly By ButtonGo = By.XPath("//button[@id='button-go']");
        public readonly By ExpandRoundsGo = By.XPath("//button[@id='t-toggle-rounds']");
        public readonly By ButtonConfirm = By.XPath("//div[@role='dialog']//button[text()='Confirm']");
        public readonly By ScheduleDateInput = By.XPath("//input[@id='date']");
        public readonly By IdFilterInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]//input");
        public readonly string UnallocatedRow = "./div[contains(@class, 'assured')]";
        public readonly string UnallocatedCheckbox = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        public readonly string UnallocatedState = "./div[contains(@class, 'slick-cell l1 r1')]";
        public readonly string UnallocatedID = "./div[contains(@class, 'slick-cell l3 r3')]";

        public readonly string SlickRoundRow = "./div[contains(@class, 'slick-group')]";
        public readonly string RoundDescriptionCell = "./div[contains(@class, 'slick-cell l0')]";

        public readonly By ShowOutstandingTaskButton = By.XPath("//div[@id='tabs-container']//button[@id='t-outstanding']");
        public readonly By OutstandingTab = By.XPath("//div[@id='tabs-container']//li//a[@aria-controls='outstanding']");

        private TableElement slickRoundTableEle;
        public TableElement SlickRoundTableEle
        {
            get => slickRoundTableEle;
        }

        private TableElement unallocatedTableEle;
        public TableElement UnallocatedTableEle
        {
            get => unallocatedTableEle;
        }

        private TreeViewElement _treeViewElement = new TreeViewElement("//div[contains(@class, 'jstree-1')]", "./li[contains(@role, 'treeitem')]", "./a", "./ul[contains(@class, 'jstree-children')]", "./i[contains(@class, 'jstree-ocl')][1]");
        private TreeViewElement ServicesTreeView
        {
            get => _treeViewElement;
        }

        public TaskConfirmationPage SelectRoundNode(string nodeName)
        {
            ServicesTreeView.ClickItem(nodeName);
            return this;
        }

        public TaskConfirmationPage ExpandRoundNode(string nodeName)
        {
            ServicesTreeView.ExpandNode(nodeName);
            return this;
        }

        public TaskConfirmationPage VerifyRoundInstanceStatusCompleted()
        {
            IWebElement cell = UnallocatedTableEle.GetCell(0, 1);
            IWebElement img = cell.FindElement(By.XPath("./div//img"));
            Assert.IsTrue(img.GetAttribute("src").Contains("coretaskstate/s3.png"));
            return this;
        }

        public TaskConfirmationPage DoubleClickRoundInstance()
        {
            slickRoundTableEle.DoubleClickRow(0);
            return this;
        }
    }
}
