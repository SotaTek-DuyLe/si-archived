using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Pages.Tasks;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class TaskConfirmationPage : BasePageCommonActions
    {
        private readonly By contractTitle = By.XPath("//label[text()='Contract']");
        private readonly By serviceTitle = By.XPath("//label[text()='Services']");
        private readonly By scheduleTitle = By.XPath("//label[text()='Scheduled Date']");
        private readonly By goBtn = By.CssSelector("button[id='button-go']");
        private readonly By contractDd = By.XPath("//label[text()='Contract']/following-sibling::span/select");
        private readonly By serviceInput = By.XPath("//label[text()='Services']/following-sibling::input");
        private readonly By scheduledDateInput = By.XPath("//label[text()='Scheduled Date']/following-sibling::input");
        private readonly By expandRoundsBtn = By.XPath("//span[text()='Expand Rounds']/parent::button");
        private readonly By expandRoundLegsBtn = By.XPath("//span[text()='Expand Round Legs']/parent::button");
        private readonly By descInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l4')]/input");
        private readonly By descAtFirstColumn = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l4')]");
        private readonly By statusAtFirstColumn = By.XPath("(//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l19')])[1]");
        private readonly By scheduledDateAtFirstColumn = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l17')]");
        private readonly By dueDateAtFirstColumn = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l18')]");
        private readonly By allRowInGrid = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div");

        private readonly By selectAndDeselectBtn = By.CssSelector("div[title='Select/Deselect All']");
        private readonly By firstRowAfterFiltering = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'slick-row')]/div[contains(@class, 'l4')]/parent::div");
        public readonly By ContractSelect = By.XPath("//select[@id='contract']");
        public readonly By ServiceInput = By.XPath("//input[@id='services']");
        public readonly By ButtonGo = By.XPath("//button[@id='button-go']");
        public readonly By ExpandRoundsGo = By.XPath("//button[@id='t-toggle-rounds']");
        public readonly By BulkReallocateButton = By.XPath("//button[@id='t-bulk-reallocate']");
        public readonly By ButtonConfirm = By.XPath("//div[@role='dialog']//button[text()='Confirm']");
        public readonly By ScheduleDateInput = By.XPath("//input[@id='date']");
        public readonly By IdFilterInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]//input");
        public readonly string UnallocatedRow = "./div[contains(@class, 'assured')]";
        public readonly string ServiceTaskRow = "./div[contains(@class, 'virtual')]";
        public readonly string UnallocatedCheckbox = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        public readonly string UnallocatedState = "./div[contains(@class, 'slick-cell l1 r1')]";
        public readonly string UnallocatedID = "./div[contains(@class, 'slick-cell l3 r3')]";

        public readonly string SlickRoundRow = "./div[contains(@class, 'slick-group')]";
        public readonly string RoundDescriptionCell = "./div[contains(@class, 'slick-cell l0')]";
        private readonly By confirmationNeededTitlePopup = By.XPath("//h4[text()='Confirmation Needed']");
        private readonly By confirmationNeededContent1Popup = By.XPath("//p[text()='You have selected a large number of routes, which may slow down the system.']");
        private readonly By confiramtionNeededContent2Popup = By.XPath("//p[text()='Are you sure you want to load them all?']");
        private readonly By confirmBtn = By.XPath("//button[text()='Confirm']");
        private readonly By closeBtn = By.XPath("//button[text()='Close']");

        public readonly By ShowOutstandingTaskButton = By.XPath("//div[@id='tabs-container']//button[@id='t-outstanding']");
        public readonly By OutstandingTab = By.XPath("//div[@id='tabs-container']//li//a[@aria-controls='outstanding']");
        private readonly By firstRoundInGrid = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[1]");
        public readonly By BulkUpdateButton = By.XPath("//button[@title='Bulk Update']");
        private readonly By statusDd = By.XPath("//label[text()='Status']/following-sibling::select[1]");
        private readonly By closeBtnBulkUpdate = By.XPath("//button[@type='button' and text()='×']");

        //DYNAMIC
        private readonly string anyContractOption = "//label[text()='Contract']/following-sibling::span/select/option[text()='{0}']";
        private readonly string anyServicesByServiceGroup = "//li[contains(@class, 'serviceGroups')]//a[text()='{0}']/preceding-sibling::i";
        private readonly string anyRoundByDay = "//a[text()='{0}']/following-sibling::ul//a[text()='{1}']";
        private readonly string anyChildOfTree = "//a[text()='{0}']/parent::li/i";
        private readonly string chirldOfTree = "//a[text()='{0}']";
        private readonly string firstRoundByRoundNameInGrid = "//span[contains(string(), '{0}')]/parent::div/parent::div";
        private readonly string optionInStatusFirstRow = "(//div[@id='grid']//div[@class='grid-canvas']//div[contains(@class, 'l19')])/select/option[{0}]";
        private readonly string statusOptionInBulkUpdate = "//label[text()='Status']/following-sibling::select[1]/option[{0}]";

        #region Bulk update
        public readonly By BulkUpdateStateSelect = By.XPath("//div[@class='bulk-confirmation']//select[1]");
        public readonly By BulkUpdateReasonSelect = By.XPath("//div[@class='bulk-confirmation']//select[2]");
        public readonly By ConfirmButton = By.XPath("//button[text()='Confirm']");
        #endregion

        #region Reallocated Grid
        private readonly By selectAndDeselectAllCheckbox = By.XPath("//div[contains(@id,'reallocated')]//div[@title='Select/Deselect All']//input");
        public readonly By FromInput = By.XPath("//input[@id='from']");
        private string reallocatedTable = "//div[contains(@id, 'reallocated')]//div[@class='grid-canvas']";
        private string reallocatedRow = "./div[contains(@class, 'slick-row')]";
        private string reallocatedCheckboxCell = "./div[@class='slick-cell l1 r1']//input";
        private string descriptionCell = "./div[@class='slick-cell l21 r21']";
        private string townCell = "./div[@class='slick-cell l21 r21']";
        private string subcontractCell = "./div[@class='slick-cell l31 r31']";
        private string subcontractReasonCell = "./div[@class='slick-cell l32 r32']";

        private TableElement reallocatedTableEle;
        public TableElement ReallocatedTableEle
        {
            get => reallocatedTableEle;
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

        public TaskConfirmationPage VerifyReallocatedTask(string reason)
        {
            IWebElement e = ReallocatedTableEle.GetCell(0, 0);
            Actions actions = new Actions(this.driver);
            actions.MoveToElement(e).Build().Perform();
            SleepTimeInMiliseconds(500);
            reallocatedTableEle = new TableElement(reallocatedTable, reallocatedRow, new List<string>() { townCell, subcontractCell, subcontractReasonCell, descriptionCell, reallocatedCheckboxCell });
            reallocatedTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
            e = ReallocatedTableEle.GetCell(0, 1);
            actions = new Actions(this.driver);
            actions.MoveToElement(e).Build().Perform();
            SleepTimeInMiliseconds(500);
            int rowCount = ReallocatedTableEle.GetRows().Count;
            for (int i = 0; i < rowCount; i++)
            {
                VerifyCellValue(ReallocatedTableEle, i, 1, "✓");
                VerifyCellValue(ReallocatedTableEle, i, 2, reason);
            }
            return this;
        }

        /// <summary>
        /// SelectServiceTaskAllocation
        /// </summary>
        /// <returns>Task Descriptions</returns>
        public IEnumerable<string> SelectServiceTaskAllocation()
        {
            int rowCount = ReallocatedTableEle.GetRows().Count;
            for (int i = 0; i < rowCount; i++)
            {
                ReallocatedTableEle.ClickCell(i, 4);
            }
            SleepTimeInMiliseconds(300);
            rowCount = ReallocatedTableEle.GetRows().Count;
            for (int i = 0; i < rowCount; i++)
            {
                yield return ReallocatedTableEle.GetCellValue(i, 3).AsString();
            }
        }

        public TaskConfirmationPage ClickFirstCheckbox()
        {
            ClickOnElement("//div[contains(@id, 'reallocated')]//div[@title='Select/Deselect All']//input");
            return this;
        }

        public TaskConfirmationPage ScrollDownToElementAndVerifyTaskSubcontract(IEnumerable<string> descriptions, string reason)
        {
            foreach (var description in descriptions)
            {
                var row = ReallocatedTableEle.GetRowByCellValue(3, description);
                if (row != null)
                {
                    Actions actions = new Actions(this.driver);
                    actions.MoveToElement(row).Build().Perform();
                    SleepTimeInMiliseconds(300);
                    IWebElement subContractCellEle = row.FindElement(By.XPath(subcontractCell));
                    actions = new Actions(this.driver);
                    actions.MoveToElement(subContractCellEle).Build().Perform();
                    SleepTimeInMiliseconds(500);
                    Assert.IsTrue(subContractCellEle.Text == "✓");
                    IWebElement subContractReasonCellEle = row.FindElement(By.XPath(subcontractReasonCell));
                    Assert.IsTrue(subContractReasonCellEle.Text == reason);
                }
                else
                {
                    int count = 0;
                    while (row == null && count < 3)
                    {
                        var lastRow = ReallocatedTableEle.GetRows().LastOrDefault();
                        Actions focusactions = new Actions(this.driver);
                        focusactions.MoveToElement(lastRow).Build().Perform();
                        WaitForLoadingIconToDisappear();
                        row = ReallocatedTableEle.GetRowByCellValue(3, description);
                        count++;
                    }
                    Assert.IsNotNull(row);

                    Actions actions = new Actions(this.driver);
                    actions.MoveToElement(row).Build().Perform();
                    SleepTimeInMiliseconds(300);
                    IWebElement subContractCellEle = row.FindElement(By.XPath(subcontractCell));
                    actions = new Actions(this.driver);
                    actions.MoveToElement(subContractCellEle).Build().Perform();
                    SleepTimeInMiliseconds(500);
                    Assert.IsTrue(subContractCellEle.Text == "✓");
                    IWebElement subContractReasonCellEle = row.FindElement(By.XPath(subcontractReasonCell));
                    Assert.IsTrue(subContractReasonCellEle.Text == reason);
                }
            }
            return this;
        }

        public TaskConfirmationPage DragDropTaskAllocationToRoundGrid(string roundGroup, string round)
        {
            IWebElement cell = RoundInstanceTableEle.GetCellByCellValues(3, new Dictionary<int, object>()
            {
                { 1, roundGroup },
                { 2, round }
            });
            WaitUtil.WaitForElementClickable(cell).Click();
            WaitForLoadingIconToDisappear();
            cell = RoundInstanceTableEle.GetCellByCellValues(3, new Dictionary<int, object>()
            {
                { 1, roundGroup },
                { 2, round }
            });
            IWebElement row = ReallocatedTableEle.GetRow(0);
            Actions a = new Actions(driver);
            a.ClickAndHold(row).Perform();
            a.MoveToElement(cell).Perform();
            a.Release().Perform();
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage VerifyTaskAllocated(string roundGroup, string round)
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

        [AllureStep]
        public TaskConfirmationPage DragRoundInstanceToReallocattedGrid(string roundGroup, string round, int dragcellIdx = 3)
        {
            IWebElement cell = RoundInstanceTableEle.GetCellByCellValues(dragcellIdx, new Dictionary<int, object>()
            {
                { 1, roundGroup },
                { 2, round }
            });
            IWebElement grid = GetElement(By.XPath("//div[contains(@id, 'reallocated')]//div[contains(@class, 'grid-canvas')]"));
            DragAndDrop(cell, grid);
            return this;
        }
        #endregion

        [AllureStep]
        public TaskConfirmationPage IsTaskConfirmationPage()
        {
            WaitUtil.WaitForElementVisible(contractTitle);
            Assert.IsTrue(IsControlDisplayed(serviceTitle));
            Assert.IsTrue(IsControlDisplayed(scheduleTitle));
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage SelectContract(string contractName)
        {
            ClickOnElement(contractDd);
            //Select contract
            ClickOnElement(anyContractOption, contractName);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickServicesAndSelectServiceInTree(string serviceGroupName, string serviceName, string roundName, string dayName)
        {
            ClickOnElement(serviceInput);
            ClickOnElement(anyServicesByServiceGroup, serviceGroupName);
            ClickOnElement(anyChildOfTree, serviceName);
            ClickOnElement(anyChildOfTree, roundName);
            ClickOnElement(chirldOfTree, dayName);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickServicesAndSelectServiceInTree(string serviceGroupName, string serviceName)
        {
            ClickOnElement(serviceInput);
            ClickOnElement(anyServicesByServiceGroup, serviceGroupName);
            ClickOnElement(chirldOfTree, serviceName);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickServicesAndSelectServiceInTree(string serviceGroupName, string serviceName, string roundName)
        {
            ClickOnElement(serviceInput);
            ClickOnElement(anyServicesByServiceGroup, serviceGroupName);
            ClickOnElement(anyChildOfTree, serviceName);
            ClickOnElement(chirldOfTree, roundName);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage SendDateInScheduledDate(string dateValue)
        {
            InputCalendarDate(scheduledDateInput, dateValue);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickGoBtn()
        {
            ClickOnElement(goBtn);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage IsConfirmationNeededPopup()
        {
            WaitUtil.WaitForElementVisible(confirmationNeededTitlePopup);
            Assert.IsTrue(IsControlDisplayed(confirmationNeededTitlePopup));
            Assert.IsTrue(IsControlDisplayed(confirmationNeededContent1Popup));
            Assert.IsTrue(IsControlDisplayed(confiramtionNeededContent2Popup));
            Assert.IsTrue(IsControlDisplayed(confirmBtn));
            Assert.IsTrue(IsControlDisplayed(closeBtn));
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnConfirmBtn()
        {
            ClickOnElement(confirmBtn);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnExpandRoundsBtn()
        {
            ClickOnElement(expandRoundsBtn);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnExpandRoundLegsBtn()
        {
            ClickOnElement(expandRoundLegsBtn);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage SendKeyInDesc(string descValue)
        {
            SendKeys(descInput, descValue);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage VerifyDisplayResultAfterSearchWithDesc(string descExp)
        {
            Assert.IsTrue(GetElementText(descAtFirstColumn).Trim().ToLower().Contains(descExp.Trim().ToLower()));
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage VerifyScheduledDateAndDueDateAfterSearchWithDesc(string dateValue)
        {
            Assert.IsTrue(GetElementText(scheduledDateAtFirstColumn).Contains(dateValue), "Wrong [Scheduled Date]");
            Assert.IsTrue(GetElementText(dueDateAtFirstColumn).Contains(dateValue), "Wrong [Due Date]");
            return this;
        }


        [AllureStep]
        public TaskConfirmationPage VerifyNoDisplayResultAfterSearchWithDesc()
        {
            Assert.IsTrue(IsControlUnDisplayed(allRowInGrid));
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnSelectAndDeselectBtn()
        {
            ClickOnElement(selectAndDeselectBtn);
            return this;
        }

        [AllureStep]
        public DetailTaskPage DoubleClickOnFirstTask()
        {
            DoubleClickOnElement(firstRowAfterFiltering);
            return PageFactoryManager.Get<DetailTaskPage>();
        }

        public TaskConfirmationPage()
        {
            unallocatedTableEle = new TableElement("//div[@id='grid']//div[@class='grid-canvas']", UnallocatedRow, new List<string>() { UnallocatedCheckbox, UnallocatedState, UnallocatedID });
            unallocatedTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };

            serviceTaskTableEle = new TableElement("//div[@id='grid']//div[@class='grid-canvas']", ServiceTaskRow, new List<string>() { UnallocatedCheckbox, UnallocatedState, UnallocatedID });
            serviceTaskTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };

            reallocatedTableEle = new TableElement(reallocatedTable, reallocatedRow, new List<string>() { townCell, descriptionCell });
            reallocatedTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
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

        private TableElement serviceTaskTableEle;
        public TableElement ServiceTaskTableEle
        {
            get => serviceTaskTableEle;
        }

        private TreeViewElement _treeViewElement = new TreeViewElement("//div[contains(@class, 'jstree-1')]", "./li[contains(@role, 'treeitem')]", "./a", "./ul[contains(@class, 'jstree-children')]", "./i[contains(@class, 'jstree-ocl')][1]");
        private TreeViewElement ServicesTreeView
        {
            get => _treeViewElement;
        }
        [AllureStep]
        public TaskConfirmationPage SelectRoundNode(string nodeName)
        {
            ServicesTreeView.ClickItem(nodeName);
            return this;
        }
        [AllureStep]
        public TaskConfirmationPage ExpandRoundNode(string nodeName)
        {
            ServicesTreeView.ExpandNode(nodeName);
            return this;
        }
        [AllureStep]
        public TaskConfirmationPage VerifyRoundInstanceStatusCompleted()
        {
            IWebElement cell = UnallocatedTableEle.GetCell(0, 1);
            IWebElement img = cell.FindElement(By.XPath("./div//img"));
            Assert.IsTrue(img.GetAttribute("src").Contains("coretaskstate/s3.png"));
            return this;
        }
        [AllureStep]
        public TaskConfirmationPage DoubleClickRoundInstance()
        {
            slickRoundTableEle.DoubleClickRow(0);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickServiceTask(int idx)
        {
            serviceTaskTableEle.ClickCell(idx, 0);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage DoubleClickRoundInstanceDetail()
        {
            UnallocatedTableEle.DoubleClickRow(0);
            return this;
        }

        [AllureStep]
        public RoundInstanceDetailPage DoubleClickOnFirstRound()
        {
            DoubleClickOnElement(firstRoundInGrid);
            SleepTimeInMiliseconds(3000);
            return PageFactoryManager.Get<RoundInstanceDetailPage>();
        }

        [AllureStep]
        public RoundInstanceDetailPage DoubleClickOnFirstRound(string roundName)
        {
            DoubleClickOnElement(firstRoundByRoundNameInGrid, roundName);
            SleepTimeInMiliseconds(3000);
            return PageFactoryManager.Get<RoundInstanceDetailPage>();
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnFirstRound()
        {
            ClickOnElement(By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[2]//input[@type='checkbox']"));
            SleepTimeInMiliseconds(3000);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnStatusAtFirstColumn()
        {
            ClickOnElement(statusAtFirstColumn);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage VerifyTheDisplayOfTheOrderStatus(string[] taskStateValues)
        {
            for (int i = 0; i < taskStateValues.Length; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(optionInStatusFirstRow, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnBulkUpdateBtn()
        {
            ClickOnElement(BulkUpdateButton);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnStatusDdInBulkUpdatePopup()
        {
            ClickOnElement(statusDd);
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage VerifyOrderInTaskStateDd(string[] taskStateValues)
        {
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(statusOptionInBulkUpdate, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }

        [AllureStep]
        public TaskConfirmationPage ClickOnCloseBulkUpdateModel()
        {
            ClickOnElement(closeBtnBulkUpdate);
            return this;
        }
    }
}
