﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class TaskConfirmationPage : BasePageCommonActions
    {
        private readonly By contractTitle = By.XPath("//label[text()='Contract']");
        private readonly By serviceTitle = By.XPath("//label[text()='Services']");
        private readonly By scheduleTitle = By.XPath("//label[text()='Scheduled Date']");
        private readonly By goBtn = By.XPath("button[id='button-go']");
        private readonly By contractDd = By.XPath("//label[text()='Contract']/following-sibling::span/select");
        private readonly By serviceInput = By.XPath("//label[text()='Services']/following-sibling::input");
        private readonly By scheduledDateInput = By.XPath("//label[text()='Scheduled Date']/following-sibling::input");
        private readonly By expandRoundsBtn = By.XPath("//span[text()='Expand Rounds']/parent::button");
        private readonly By expandRoundLegsBtn = By.XPath("//span[text()='Expand Round Legs']/parent::button");
        private readonly By descInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l4')]/input");
        private readonly By descAtFirstColumn = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured')]/div[contains(@class, 'l4')]");
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

        //DYNAMIC
        private readonly string anyContractOption = "//label[text()='Contract']/following-sibling::span/select/option[text()='{0}']";
        private readonly string anyServicesByServiceGroup = "//li[contains(@class, 'serviceGroups')]//a[text()='{0}']/preceding-sibling::i";
        private readonly string anyRoundByDay = "//a[text()='{0}']/following-sibling::ul//a[text()='{1}']";
        private readonly string anyChildOfTree = "//a[text()='{0}']/parent::li/i";

        public TaskConfirmationPage IsTaskConfirmationPage()
        {
            WaitUtil.WaitForElementVisible(contractTitle);
            Assert.IsTrue(IsControlDisplayed(serviceTitle));
            Assert.IsTrue(IsControlDisplayed(scheduleTitle));
            Assert.IsTrue(IsControlDisplayed(goBtn));
            return this;
        }

        public TaskConfirmationPage SelectContract(string contractName)
        {
            ClickOnElement(contractDd);
            //Select contract
            ClickOnElement(anyContractOption, contractName);
            return this;
        }

        public TaskConfirmationPage ClickServicesAndSelectServiceInTree(string serviceGroupName, string serviceName, string roundName, string dayName)
        {
            ClickOnElement(serviceInput);
            ClickOnElement(anyServicesByServiceGroup, serviceGroupName);
            ClickOnElement(anyChildOfTree, serviceName);
            ClickOnElement(anyChildOfTree, roundName);
            ClickOnElement(anyChildOfTree, dayName);
            return this;
        }

        public TaskConfirmationPage SendDateInScheduledDate(string dateValue)
        {
            SendKeys(scheduledDateInput, dateValue);
            return this;
        }

        public TaskConfirmationPage ClickGoBtn()
        {
            ClickOnElement(goBtn);
            return this;
        }

        public TaskConfirmationPage ClickOnExpandRoundsBtn()
        {
            ClickOnElement(expandRoundsBtn);
            return this;
        }

        public TaskConfirmationPage ClickOnExpandRoundLegsBtn()
        {
            ClickOnElement(expandRoundLegsBtn);
            return this;
        }

        public TaskConfirmationPage SendKeyInDesc(string descValue)
        {
            SendKeys(descInput, descValue);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public TaskConfirmationPage VerifyDisplayResultAfterSearchWithDesc(string descExp)
        {
            Assert.AreEqual(descExp, GetElementText(descAtFirstColumn));
            return this;
        }

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
