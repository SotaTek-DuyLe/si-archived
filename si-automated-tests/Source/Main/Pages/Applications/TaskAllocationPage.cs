﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Applications;
using si_automated_tests.Source.Main.Models.ServiceStatus;
using System.Collections.Generic;
using System.Linq;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class TaskAllocationPage : BasePageCommonActions
    {
        public TaskAllocationPage()
        {
            unallocatedTableEle = new TableElement("//div[@class='tab-pane echo-grid active']//div[@class='grid-canvas']", UnallocatedRow, new List<string>() { UnallocatedCheckbox, UnallocatedDescription, UnallocatedService, UnallocatedID, UnallocatedStatus });
            unallocatedTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };

            roundTabTableEle = new TableElement("//div[contains(@id, 'round-tab')]//div[@class='grid-canvas']", UnallocatedRow, new List<string>() { UnallocatedCheckbox, UnallocatedDescription, UnallocatedService, UnallocatedID, UnallocatedStatus });
            roundTabTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        private int maxRetryCount = 30;

        public readonly By ContractSelect = By.XPath("//select[@id='contract']");
        public readonly By ButtonGo = By.XPath("//button[@id='button-go']");
        public readonly By FromInput = By.XPath("//input[@id='from']");
        public readonly By ToInput = By.XPath("//input[@id='to']");
        public readonly By ServiceInput = By.XPath("//input[@id='services']");
        public readonly By UnallocatedTable = By.XPath("//div[@id='unallocated']//div[@class='grid-canvas']");
        public readonly By LockFilterInput = By.XPath("//div[contains(@id, 'round-tab')]//div[contains(@class, 'l27')]//input");
        public readonly By IdFilterInput = By.XPath("//div[contains(@id, 'round-tab')]//div[contains(@class, 'l3')]//input");
        public readonly By ToggleRoundLegsButton = By.XPath("//button[@id='t-toggle-roundlegs']");
        

        public readonly string UnallocatedRow = "./div[contains(@class, 'slick-row')]";
        public readonly string UnallocatedCheckbox = "./div[contains(@class, 'slick-cell l1 r1')]//input";
        public readonly string UnallocatedDescription = "./div[contains(@class, 'slick-cell l4 r4')]";
        public readonly string UnallocatedService = "./div[contains(@class, 'slick-cell l5 r5')]";
        public readonly string UnallocatedID = "./div[contains(@class, 'slick-cell l3 r3')]";
<<<<<<< HEAD
        private readonly By taskName = By.XPath("//div[@id='tabs-container']//li[@role='presentation'][2]");
        private readonly By thirdTaskName = By.XPath("//div[@id='tabs-container']//li[@role='presentation'][3]");
        private readonly By contractTitle = By.XPath("//label[text()='Contract']");
        private readonly By checkboxSelectAllTask = By.XPath("//div[contains(@id, 'reallocated-')]//div[@title='Select/Deselect All']//input");
        private readonly By firstTaskInGrid = By.XPath("//div[contains(@id, 'reallocated-')]//div[@class='grid-canvas']/div[1]");
        private readonly By firstRoundForNextDayNotAllocated = By.XPath("(//div[@id='round-grid-container']//div[contains(@class, 'no-padding')]/div)[1]");
        private readonly By firstRoundGroupNotAllocated = By.XPath("(//div[@id='round-grid-container']//div[contains(@class, 'no-padding')]/div)[1]/parent::div/preceding-sibling::div[2]");
        private readonly By firstRoundNameNotAllocated = By.XPath("(//div[@id='round-grid-container']//div[contains(@class, 'no-padding')]/div)[1]/parent::div/preceding-sibling::div[1]");
        private readonly By allTaskInGrid = By.XPath("//div[contains(@id, 'reallocated-')]//div[@class='grid-canvas']/div");
        private readonly By allTaskAfterDragAndDrop = By.XPath("//div[contains(@id, 'round-tab-')]//div[@class='grid-canvas']/div");
        private readonly By firstRoundAllocated = By.XPath("//div[@id='roundGrid']//span[contains(@style, 'green') and contains(@style, 'background-color: white')]");
        private readonly By taskGrid = By.XPath("//div[contains(@id, 'reallocated-')]//div[@class='grid-canvas']");
=======
        public readonly string UnallocatedStatus = "./div[contains(@class, 'slick-cell l10 r10')]";
>>>>>>> bedfeb0ead2e444e84e990314568033d10400e89

        public readonly By ShowOutstandingTaskButton = By.XPath("//div[@id='tabs-container']//button[@id='t-outstanding']");
        public readonly By OutstandingTab = By.XPath("//div[@id='tabs-container']//li//a[@aria-controls='outstanding']");

        private TableElement unallocatedTableEle;
        public TableElement UnallocatedTableEle
        {
            get => unallocatedTableEle;
        }

        private TableElement roundTabTableEle;
        public TableElement RoundTabTableEle
        {
            get => roundTabTableEle;
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

        #region Outstanding Table
        private readonly string OutStandingTable = "//div[@id='outstanding']//div[@class='grid-canvas']";
        private readonly string OutStandingRow = "./div[contains(@class, 'slick-row')]";
        private readonly string OutStandingCheckbox = "./div[contains(@class, 'slick-cell l1 r1')]//input";
        private readonly string OutStandingId = "./div[contains(@class, 'slick-cell l2 r2')]";
        private readonly string OutStandingDescription = "./div[contains(@class, 'slick-cell l3 r3')]";
        private readonly string OutStandingService = "./div[contains(@class, 'slick-cell l4 r4')]";
        private readonly string OutStandingScheduledDate = "./div[contains(@class, 'slick-cell l5 r5')]";

        //DYNAMIC
        private readonly string idRows = "//div[contains(@id, 'reallocated')]//div[@class='grid-canvas']/div[{0}]//div[contains(@class, 'l3')]";
        private readonly string descRows = "//div[contains(@id, 'reallocated')]//div[@class='grid-canvas']/div[{0}]//div[contains(@class, 'l4')]";
        private readonly string serviceRows = "//div[contains(@id, 'reallocated')]//div[@class='grid-canvas']/div[{0}]//div[contains(@class, 'l5')]";

        public TableElement OutstandingTableEle
        {
            get => new TableElement(OutStandingTable, OutStandingRow, new List<string>() { OutStandingCheckbox, OutStandingId, OutStandingDescription, OutStandingService, OutStandingScheduledDate });
        }

        public TaskAllocationPage VerifyOutStandingData(List<OutstandingTaskModel> dataFromDbs)
        {
            foreach (var item in dataFromDbs)
            {
                Assert.IsNotNull(OutstandingTableEle.GetCellByValue(1, item.ID));
            }
            return this;
        }
        #endregion

        public readonly By AllocatingConfirmMsg = By.XPath("//div[text()='Allocating 1 Task(s) onto Round Instance for a different day!']");
        public readonly By AllocatingConfirmMsg2 = By.XPath("//div[text()='Allocating 1 locked Tasks from/to dispatched round. Ensure that new Crew can attempt service']");
        public readonly By AllocateAllButton = By.XPath("//button[text()='Allocate All']");
        public readonly By AllocationReasonSelect = By.XPath("//select[contains(@data-bind, 'options: allocationReasons')]");
        public readonly By AllocationConfirmReasonButton = By.XPath("//button[contains(@data-bind, 'click: confirmReasonCallback')]");
        public readonly By ReasonConfirmMsg = By.XPath("//label[contains(string(), 'Please select the reason below and confirm.')]");
        public readonly By ReasonSelect = By.XPath("//select[contains(@data-bind, 'allocationReasons')]");
        public readonly By ReasonConfirmButton = By.XPath("//div[@id='get-allocation-reason']//button[text()='Confirm']");

        public By GetAllocatingConfirmMsg(int count) 
        {
            return By.XPath($"//div[text()='Allocating {count} Task(s) onto Round Instance for a different day!']");
        }

        public TaskAllocationPage DoubleClickFromCellOnRound(string round)
        {
            RoundInstanceTableEle.DoubleClickCellOnCellValue(4, 2, round);
            return this;
        }

        public TaskAllocationPage DoubleClickRI(string roundgroup, string round)
        {
            IWebElement cell = RoundInstanceTableEle.GetCellByCellValues(3, new Dictionary<int, object>()
            {
                { 1, roundgroup },
                { 2, round }
            });
            cell.Click();
            SleepTimeInMiliseconds(300);
            DoubleClickOnElement(cell);
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

        public TaskAllocationPage DragRoundInstanceToUnlocattedGrid(string roundGroup, string round, int dragcellIdx = 3)
        {
            IWebElement cell = RoundInstanceTableEle.GetCellByCellValues(dragcellIdx, new Dictionary<int, object>() 
            {
                { 1, roundGroup },
                { 2, round }
            });
            IWebElement grid = GetElement(UnallocatedTable);
            DragAndDrop(cell, grid);
            return this;
        }

        public TaskAllocationPage DragRoundInstanceToReallocattedGrid(string roundGroup, string round, int dragcellIdx = 3)
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

        public TaskAllocationPage DragRoundInstanceToRoundGrid(string roundGroup, string round, int dragcellIdx = 3)
        {
            IWebElement cell = RoundInstanceTableEle.GetCellByCellValues(dragcellIdx, new Dictionary<int, object>()
            {
                { 1, roundGroup },
                { 2, round }
            });
            IWebElement grid = GetElement(By.XPath("//div[contains(@id, 'round-tab')]//div[@class='grid-canvas']"));
            DragAndDrop(cell, grid);
            return this;
        }

        public TaskAllocationPage ClickUnallocatedRow(int rowIdx = 0)
        {
            UnallocatedTableEle.ClickCell(rowIdx, 0);
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

        public List<string> GetRoundLegInstanceIds(List<string> descriptions)
        {
            List<string> RLIIds = new List<string>();
            foreach (var description in descriptions)
            {
                int count = 0;
                while (true)
                {
                    count++;
                    if (count > maxRetryCount) break;
                    IWebElement cell = UnallocatedTableEle.GetCellByCellValues(3, new Dictionary<int, object>() { { 1, description } });
                    if (cell == null)
                    {
                        IWebElement row = UnallocatedTableEle.GetRows().LastOrDefault();
                        Actions actions = new Actions(this.driver);
                        actions.MoveToElement(row).Perform();
                        SleepTimeInMiliseconds(300);
                    }
                    else
                    {
                        RLIIds.Add(cell.Text.Trim());
                        break;
                    }
                }
                Assert.IsTrue(count < maxRetryCount);
            }
            return RLIIds;
        }

        public List<RoundInstanceModel> ExpandRoundInstance(int rowCount)
        {
            List<RoundInstanceModel> roundInstanceDetails = new List<RoundInstanceModel>();
            void GetDetails()
            {
                List<IWebElement> rowDetails = UnallocatedTableEle.GetRows().Where(row =>
                {
                    IWebElement cell = row.FindElement(By.XPath(UnallocatedDescription));
                    var details = cell.FindElements(By.XPath("./span[@class='toggle']"));
                    return details.FirstOrDefault() != null;
                }).ToList();
                foreach (var item in rowDetails)
                {
                    RoundInstanceModel model = new RoundInstanceModel()
                    {
                        Description = item.FindElement(By.XPath(UnallocatedDescription)).Text.Trim(),
                        Service = item.FindElement(By.XPath(UnallocatedService)).Text.Trim()
                    };
                    if (!roundInstanceDetails.Any(x => x.Description == model.Description)) roundInstanceDetails.Add(model);
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                List<IWebElement> rows = null;
                int count = 0;
                while (true)
                {
                    count++;
                    rows = UnallocatedTableEle.GetRows().Where(row =>
                    {
                        IWebElement cell = row.FindElement(By.XPath(UnallocatedDescription));
                        var expandIcons = cell.FindElements(By.XPath("./span[@class='toggle expand']"));
                        return expandIcons.FirstOrDefault() != null;
                    }).ToList();
                    if (rows.Count == 0)
                    {
                        IWebElement row = UnallocatedTableEle.GetRows().LastOrDefault();
                        Actions actions = new Actions(this.driver);
                        actions.MoveToElement(row).Perform();
                        SleepTimeInMiliseconds(300);
                        GetDetails();
                    }
                    else if (rows.Count != 0 || count > maxRetryCount)
                    {
                        break;
                    }
                }
                Assert.IsTrue(count < maxRetryCount);
                IWebElement cell = rows.FirstOrDefault().FindElement(By.XPath(UnallocatedDescription));
                IWebElement expandIcon = cell.FindElement(By.XPath("./span[@class='toggle expand']"));
                string height = GetCssValue(By.XPath("//div[contains(@id, 'round-tab')]//div[@class='grid-canvas']"), "height");
                expandIcon.Click();
                WaitUtil.WaitCssAttributeChange(By.XPath("//div[contains(@id, 'round-tab')]//div[@class='grid-canvas']"), "height", height);
                GetDetails();
            }
            return roundInstanceDetails;
        }

        public List<RoundInstanceModel> ExpandRoundInstance(List<RoundInstanceModel> roundInstances)
        {
            List<RoundInstanceModel> roundInstanceDetails = new List<RoundInstanceModel>();
            void GetDetails()
            {
                List<IWebElement> rowDetails = UnallocatedTableEle.GetRows().Where(row =>
                {
                    IWebElement cell = row.FindElement(By.XPath(UnallocatedDescription));
                    var details = cell.FindElements(By.XPath("./span[@class='toggle']"));
                    return details.FirstOrDefault() != null;
                }).ToList();
                foreach (var item in rowDetails)
                {
                    RoundInstanceModel model = new RoundInstanceModel()
                    {
                        Description = item.FindElement(By.XPath(UnallocatedDescription)).Text.Trim(),
                        Service = item.FindElement(By.XPath(UnallocatedService)).Text.Trim()
                    };
                    if (!roundInstanceDetails.Any(x => x.Description == model.Description)) roundInstanceDetails.Add(model);
                }
            }
            foreach (var roundInstance in roundInstances)
            {
                int count = 0;
                while (true)
                {
                    count++;
                    if (count > maxRetryCount) break;
                    IWebElement cell = UnallocatedTableEle.GetCellByValue(1, roundInstance.Description);
                    if (cell == null)
                    {
                        IWebElement row = UnallocatedTableEle.GetRows().LastOrDefault();
                        Actions actions = new Actions(this.driver);
                        actions.MoveToElement(row).Perform();
                        SleepTimeInMiliseconds(300);
                        GetDetails();
                    }
                    else
                    {
                        IWebElement expandIcon = cell.FindElement(By.XPath("./span[@class='toggle expand']"));
                        string height = GetCssValue(By.XPath("//div[@class='tab-pane echo-grid active']//div[@class='grid-canvas']"), "height");
                        expandIcon.Click();
                        WaitUtil.WaitCssAttributeChange(By.XPath("//div[@class='tab-pane echo-grid active']//div[@class='grid-canvas']"), "height", height);
                        GetDetails();
                        break;
                    }
                }
                Assert.IsTrue(count < maxRetryCount);
            }
            return roundInstanceDetails;
        }

        public TaskAllocationPage ScrollToFirstRow()
        {
            int count = 0;
            while (count < maxRetryCount)
            {
                count++;
                IWebElement row = UnallocatedTableEle.GetRows().FirstOrDefault();
                if (row == null) break;
                if (row.GetCssValue("top") == "0px")
                {
                    Actions actions = new Actions(this.driver);
                    actions.MoveToElement(row).Perform();
                    SleepTimeInMiliseconds(300);
                    break;
                }
                else
                {
                    Actions actions = new Actions(this.driver);
                    actions.MoveToElement(row).Perform();
                    SleepTimeInMiliseconds(300);
                }
            }
            return this;
        }

        public List<RoundInstanceModel> SelectExpandedUnallocated(int rowCount)
        {
            List<RoundInstanceModel> roundInstances = new List<RoundInstanceModel>();
            for (int i = 0; i < rowCount; i++)
            {
                List<IWebElement> rows = null;
                int count = 0;
                while (true)
                {
                    count++;
                    rows = UnallocatedTableEle.GetRows().Where(row =>
                    {
                        IWebElement checkboxCell = row.FindElement(By.XPath(UnallocatedCheckbox));
                        IWebElement cell = row.FindElement(By.XPath(UnallocatedDescription));
                        var collapseIcons = cell.FindElements(By.XPath("./span[@class='toggle collapse']"));
                        return !checkboxCell.Selected && collapseIcons.FirstOrDefault() != null;
                    }).ToList();
                    if (rows.Count == 0)
                    {
                        IWebElement row = UnallocatedTableEle.GetRows().LastOrDefault();
                        Actions actions = new Actions(this.driver);
                        actions.MoveToElement(row).Perform();
                        SleepTimeInMiliseconds(300);
                    }
                    else if (rows.Count != 0 || count > maxRetryCount)
                    {
                        break;
                    }
                }
                Assert.IsTrue(count < maxRetryCount);
                IWebElement cell = rows.FirstOrDefault().FindElement(By.XPath(UnallocatedCheckbox));
                roundInstances.Add(new RoundInstanceModel()
                {
                    Description = rows.FirstOrDefault().FindElement(By.XPath(UnallocatedDescription)).Text.Trim(),
                    Service = rows.FirstOrDefault().FindElement(By.XPath(UnallocatedService)).Text.Trim()
                });
                cell.Click();
            }
            return roundInstances;
        }

        public TaskAllocationPage VerifyReAllocatedRows(List<RoundInstanceModel> expected, List<RoundInstanceModel> actual)
        {
            Assert.That(actual, Is.EquivalentTo(expected));
            return this;
        }

        public List<RoundInstanceModel> SelectRoundLegs(int rowCount)
        {
            List<RoundInstanceModel> roundInstances = new List<RoundInstanceModel>();
            List<IWebElement> rowDetails = UnallocatedTableEle.GetRows().Where(row =>
            {
                IWebElement cell = row.FindElement(By.XPath(UnallocatedDescription));
                var details = cell.FindElements(By.XPath("./span[@class='toggle']"));
                return details.FirstOrDefault() != null;
            }).ToList();
            int count = 0;
            foreach (var item in rowDetails)
            {
                if (count == rowCount) break;
                count++;
                IWebElement cell = item.FindElement(By.XPath(UnallocatedCheckbox));
                RoundInstanceModel model = new RoundInstanceModel()
                {
                    Description = item.FindElement(By.XPath(UnallocatedDescription)).Text.Trim(),
                    Service = item.FindElement(By.XPath(UnallocatedService)).Text.Trim()
                };
                roundInstances.Add(model);
                cell.Click();
            }
            return roundInstances;
        }

        public TaskAllocationPage DragRoundLegRowToRoundInstance(string roundGroup, string round)
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
            List<IWebElement> rowDetails = UnallocatedTableEle.GetRows().Where(row =>
            {
                IWebElement cell = row.FindElement(By.XPath(UnallocatedDescription));
                var details = cell.FindElements(By.XPath("./span[@class='toggle']"));
                return details.FirstOrDefault() != null;
            }).ToList();
            IWebElement row = rowDetails.FirstOrDefault();
            Actions a = new Actions(driver);
            a.ClickAndHold(row).Perform();
            a.MoveToElement(cell).Perform();
            a.Release().Perform();
            return this;
        }

        public TaskAllocationPage VerifyRoundLegIsAllocated(List<RoundInstanceModel> roundLegs)
        {
            foreach (var item in roundLegs)
            {
                int count = 0;
                while (true)
                {
                    count++;
                    IWebElement cell = UnallocatedTableEle.GetCellByValue(1, item.Description);
                    if (cell == null)
                    {
                        IWebElement row = UnallocatedTableEle.GetRows().LastOrDefault();
                        Actions actions = new Actions(this.driver);
                        actions.MoveToElement(row).Perform();
                        SleepTimeInMiliseconds(300);
                    }
                    else if(cell != null || count > maxRetryCount)
                    {
                        break;
                    }
                }
                Assert.IsTrue(count < maxRetryCount);
                List<IWebElement> rowDetails = UnallocatedTableEle.GetRows().Where(row =>
                {
                    IWebElement cell = row.FindElement(By.XPath(UnallocatedDescription));
                    var details = cell.FindElements(By.XPath("./span[@class='toggle']"));
                    return details.FirstOrDefault() != null;
                }).ToList();
                List<RoundInstanceModel> roundInstanceDetails = new List<RoundInstanceModel>();
                foreach (var rowDetail in rowDetails)
                {
                    RoundInstanceModel model = new RoundInstanceModel()
                    {
                        Description = rowDetail.FindElement(By.XPath(UnallocatedDescription)).Text.Trim(),
                        Service = rowDetail.FindElement(By.XPath(UnallocatedService)).Text.Trim()
                    };
                    if (!roundInstanceDetails.Any(x => x.Description == model.Description)) roundInstanceDetails.Add(model);
                }
                Assert.IsTrue(roundInstanceDetails.Any(x => x.Description == item.Description));
            }
            return this;
        }

<<<<<<< HEAD
        public TaskAllocationPage VerifyTaskNameDisplayed(string taskNameExp)
        {
            WaitUtil.WaitForElementVisible(contractTitle);
            Assert.AreEqual(taskNameExp, GetElementText(taskName).Replace("*", "").Trim());
            return this;
        }

        public TaskAllocationPage VerifyThirdTaskNameDisplayed(string taskNameExp)
        {
            WaitUtil.WaitForElementVisible(contractTitle);
            Assert.AreEqual(taskNameExp, GetElementText(thirdTaskName));
            return this;
        }

        public TaskAllocationPage VerifyTaskSelectedDisplayedInGrid(List<TaskInWorksheetModel> taskInWorksheetModels)
        {
            for(int i = 0; i < taskInWorksheetModels.Count; i++)
            {
                Assert.AreEqual(taskInWorksheetModels[i].id, GetElementText(string.Format(idRows, (i + 1))));
                Assert.AreEqual(taskInWorksheetModels[i].description, GetElementText(string.Format(descRows, (i + 1))));
                Assert.AreEqual(taskInWorksheetModels[i].service, GetElementText(string.Format(serviceRows, (i + 1))));
            }
            return this;
        }

        public TaskAllocationPage SelectTwoTaskAgainInGrid()
        {
            ClickOnElement(checkboxSelectAllTask);
            return this;
        }

        public TaskAllocationPage DragAndDropTwoTaskToFirstUnAllocatedRound()
        {
            IWebElement roundCell = GetElement(firstRoundForNextDayNotAllocated);
            WaitUtil.WaitForElementClickable(roundCell).Click();
            WaitForLoadingIconToDisappear();
            roundCell = GetElement(firstRoundForNextDayNotAllocated);
            IWebElement firstTask = GetElement(firstTaskInGrid);
            Actions a = new Actions(driver);
            a.ClickAndHold(firstTask).Perform();
            a.MoveToElement(roundCell).Perform();
            a.Release().Perform();
            WaitForLoadingIconToDisappear();
            return this;
        }

        public string GetRoundGroupFirstUnAllocated()
        {
            return GetElementText(firstRoundGroupNotAllocated);
        }

        public string GetRoundNameFirstUnAllocated()
        {
            return GetElementText(firstRoundNameNotAllocated);
        }

        //ROUND GRID
        private readonly By allRoundRows = By.XPath("//div[@id='roundGrid']//div[@class='grid-canvas']/div");
        private readonly string allServiceRows = "//div[@id='roundGrid']//div[@class='grid-canvas']/div[{0}]//div[contains(@class, 'l0 r0')]";
        private readonly string allRoundGroupRows = "//div[@id='roundGrid']//div[@class='grid-canvas']/div[{0}]//div[contains(@class, 'l1 r1')]";
        private readonly string allRoundNameRows = "//div[@id='roundGrid']//div[@class='grid-canvas']/div[{0}]//div[contains(@class, 'l2 r2')]";
        private readonly string allLocatorRoundAllocated = "//div[@id='roundGrid']//div[@class='grid-canvas']/div[{0}]//div[contains(@class, 'no-padding')]/span";
        private readonly string allLocatorRoundUnAllocated = "//div[@id='roundGrid']//div[@class='grid-canvas']/div[{0}]//div[contains(@class, 'no-padding')]/div";

        public List<RoundGroupModel> GetAllRoundInfoModelWithUnallocated()
        {
            List<RoundGroupModel> roundGroupModels = new List<RoundGroupModel>();
            List<IWebElement> allRounds = GetAllElements(allRoundRows);
            for(int i = 0; i < allRounds.Count; i++)
            {
                string service = GetElementText(string.Format(allServiceRows, (i + 1)));
                string roundGroup = GetElementText(string.Format(allRoundGroupRows, (i + 1)));
                string roundName = GetElementText(string.Format(allRoundNameRows, (i + 1)));
                string[] locators = new string[2];
                for(int j = 0; j < 2; j++) {
                    if(IsControlDisplayed(string.Format(allLocatorRoundUnAllocated, (j+1)))) {
                        locators[j] = string.Format(allLocatorRoundUnAllocated);
                    } else
                    {
                        locators[j] = string.Format(allLocatorRoundUnAllocated);
                    }
                }

                roundGroupModels.Add(new RoundGroupModel(service, roundGroup, roundName, locators));
            }
            return roundGroupModels;
        }

        //CONFIRMATION NEEDED POPUP
        private readonly By titleConfirmationPopup = By.XPath("//h4[text()='Confirmation Needed']");
        private readonly By contentConfirmationPopup = By.XPath("//div[text()='Allocating 2 Task(s) onto Round Instance for a different day!']");
        private readonly By closePopupBtn = By.XPath("//h4[text()='Confirmation Needed']/preceding-sibling::button");
        private readonly By allocateAllBtn = By.XPath("//button[text()='Allocate All']");
        private readonly By allocatedTasksBtn = By.XPath("//button[text()='Allocate Tasks where date is same']");

        //REASON NEEDED POPUP
        private readonly By titleReasonNeededPopup = By.XPath("//h4[text()='Reason Needed']");
        private readonly By contentReasonNeededPopup = By.XPath("//label[contains(text(), 'Please select the reason below and confirm.')]");
        private readonly By selectReasonDd = By.XPath("//label[contains(text(), 'Please select the reason below and confirm.')]/following-sibling::select");
        private readonly By badweatherReason = By.XPath("//label[contains(text(), 'Please select the reason below and confirm.')]/following-sibling::select/option[text()='Bad Weather']");
        private readonly By confirmBtn = By.XPath("//button[text()='Confirm']");
       

        public TaskAllocationPage IsConfirmationNeededPopup()
        {
            WaitUtil.WaitForElementVisible(titleConfirmationPopup);
            Assert.IsTrue(IsControlDisplayed(contentConfirmationPopup));
            Assert.IsTrue(IsControlDisplayed(closePopupBtn));
            return this;
        }

        public TaskAllocationPage ClickOnAllocateAllBtn()
        {
            ClickOnElement(allocateAllBtn);
            return this;
        }

        public TaskAllocationPage IsReasonNeededPopup()
        {
            WaitUtil.WaitForElementVisible(titleReasonNeededPopup);
            WaitUtil.WaitForElementVisible(contentReasonNeededPopup);
            Assert.IsTrue(IsControlDisplayed(contentReasonNeededPopup));
            return this;
        }

        public TaskAllocationPage ClickReasonDdAndSelectReason()
        {
            ClickOnElement(selectReasonDd);
            ClickOnElement(badweatherReason);
            return this;
        }

        public TaskAllocationPage ClickOnConfirmBtn()
        {
            ClickOnElement(confirmBtn);
            return this;
        }

        public TaskAllocationPage VerifyTaskNoLongerDisplayedInGrid()
        {
            Assert.IsTrue(IsControlUnDisplayed(allTaskInGrid));
            return this;
        }

        public TaskAllocationPage VerifyTaskDisplayedInGrid()
        {
            Assert.IsTrue(IsControlDisplayed(allTaskAfterDragAndDrop));
            return this;
        }

        public TaskAllocationPage HoverDateAndVerifyTaskDisplayGreenColor()
        {
            HoverOverElement(firstRoundForNextDayNotAllocated);
            //Verify
            return this;
        }

        public TaskAllocationPage DragAndDropRoundToGrid()
        {
            DragAndDrop(firstRoundAllocated, taskGrid);
            WaitForLoadingIconToDisappear();
            return this;
        }

        private readonly By idSearchTextbox = By.XPath("//div[contains(@id, 'round-tab')]//div[contains(@class, 'ui-state-default')]//div[contains(@class, 'l3')]//input[contains(@class, 'value')]");

        public TaskAllocationPage SendKeyInId(string taskId)
        {
            SendKeys(idSearchTextbox, taskId);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public TaskAllocationPage VerifyRoundInstanceStatusCompleted()
        {
            IWebElement cell = RoundTabTableEle.GetCell(0, 4);
            IWebElement img = cell.FindElement(By.XPath("./div//img"));
            Assert.IsTrue(img.GetAttribute("src").Contains("coretaskstate/s3.png"));
            return this;
        }
    }
}
