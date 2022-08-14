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
        public TaskAllocationPage()
        {
            unallocatedTableEle = new TableElement("//div[@class='tab-pane echo-grid active']//div[@class='grid-canvas']", UnallocatedRow, new List<string>() { UnallocatedCheckbox, UnallocatedDescription, UnallocatedService });
            unallocatedTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
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
        public By GetAllocatingConfirmMsg(int count) 
        {
            return By.XPath($"//div[text()='Allocating {count} Task(s) onto Round Instance for a different day!']");
        }

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
    }
}
