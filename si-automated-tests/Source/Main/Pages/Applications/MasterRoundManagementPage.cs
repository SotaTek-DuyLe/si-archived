using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class MasterRoundManagementPage : BasePageCommonActions
    {
        private readonly By contractSelect = By.Id("contract");
        private readonly By servicesInput = By.Id("services");
        private readonly By dateInput = By.Id("effective-date");
        private readonly By goBtn = By.Id("button-go");
        private readonly By searchRoundBtn = By.CssSelector("button[id='t-rounds-filter']");
        private readonly By searchRoundInput = By.CssSelector("div[id='search-panel']>input");
        private readonly By descInput = By.XPath("//div[contains(@id, 'round-tab')]//div[contains(@class, 'l4')]//input");
        private readonly By firstResultRowInTaskGrid = By.XPath("//div[contains(@id, 'round-tab')]//div[@class='grid-canvas']/div[1]");
        private readonly string serviceOption = "//a[contains(@class,'jstree-anchor') and text()='{0}']";
        private readonly string serviceExpandIcon = "//a[contains(@class,'jstree-anchor') and text()='{0}']/preceding-sibling::i";
        private readonly string descInputAtAnyTab = "(//div[contains(@id, 'round-tab')]//div[contains(@class, 'l4')]//input)[{0}]";
        private readonly string descAtFirstRowAnyTab = "(//div[contains(@id, 'round-tab')]//div[@class='grid-canvas']//div[contains(@class, 'l4') and contains(string(), '{0}')])[1]";

        //unalocated task tab
        private readonly By tabContainer = By.XPath("//div[@id='tabs-container']/ul");
        private readonly By unallocatedHeaders = By.XPath("//div[@id='unallocated']//div[contains(@class,'ui-state-default slick-header-column')]/span[1]");
        private readonly By firstUnallocatedTask = By.XPath("//div[@id='unallocated']//div[@class='grid-canvas']/div[1]");
        private readonly By UnallocatedTaskGrid = By.XPath("//div[@id='unallocated']//div[@class='grid-canvas']");
        private readonly By activeUnallocatedTask = By.XPath("//div[@id='unallocated']//div[@class='grid-canvas']/div[contains(@class,'active')]");
        private readonly By activeUnallocatedTaskField = By.XPath("//div[@id='unallocated']//div[@class='grid-canvas']/div[contains(@class,'active')]/div");

        //round tab
        private readonly By roundTabHeaders = By.XPath("//div[contains(@id,'round-tab') and contains(@class,'active')]//div[contains(@class,'ui-state-default slick-header-column')]/span[1]");
        private readonly String roundFilterHeader = "//div[contains(@id,'round-tab') and contains(@class,'active')]//div[contains(@class,'slick-headerrow-columns')]/div[{0}]//input[@class='value form-control']";
        private readonly By lastTask = By.XPath("(//div[contains(@id,'round-tab') and contains(@class,'active')]//div[@class='grid-canvas']/div[contains(@class,'content slick-row')])[last()]");
        private readonly By firstAllocatedTask = By.XPath("(//div[contains(@id,'round-tab') and contains(@class,'active')]//div[@class='grid-canvas']/div[contains(@class,'content slick-row')])");
        private readonly By firstAllocatedTaskField = By.XPath("(//div[contains(@id,'round-tab') and contains(@class,'active')]//div[@class='grid-canvas']/div[contains(@class,'content slick-row')]/div)");
        private readonly By selectedAllocatedTask = By.XPath("(//div[contains(@id,'round-tab') and contains(@class,'active')]//div[@class='grid-canvas']/div[contains(@class,'content slick-row active')])");
        private readonly String lastTaskField = "(//div[contains(@id,'round-tab') and contains(@class,'active')]//div[@class='grid-canvas']/div[contains(@class,'content slick-row')])[last()]/div[contains(@class,'{0}')]";
        private readonly String taskField = "//div[contains(@id,'round-tab') and contains(@class,'active')]//div[@class='grid-canvas']/div[contains(@class,'content slick-row')]/div[contains(@class,'{0}')]";
        private readonly By slickViewport = By.XPath("(//div[@class='slick-viewport'])[last()]");
        private readonly By descriptionFilterHeader = By.XPath("//div[@class='tab-pane echo-grid active']//div[@class='slick-headerrow ui-state-default']//div[contains(@class,'l4 r4')]//input");

        public readonly By Subcontract = By.XPath("//button[@id='t-selected-rows-subcontract']");
        public readonly By SubcontractSelect = By.XPath("//select[@id='subcontractReasons']");
        public readonly By SubcontractConfirmButton = By.XPath("//button[text()='Confirm']");
        private readonly string roundItem = "//li[contains(@class, 'list-group-item round')]//span[contains(text(), '{0}')]";

        //round grid
        private readonly By rounds = By.XPath("//ul[@id='rounds']/li[not(contains(@style, 'display: none;'))]");
        private readonly string taskTable = "//div[contains(@id,'round-tab') and contains(@class,'active')]//div[@class='grid-canvas']";
        private readonly string taskRow = "./div[contains(@class, 'slick-row')]";
        private readonly string taskCheckboxCell = "./div[contains(@class, 'l1 r1')]//input";
        private readonly string taskSubcontractCell = "./div[contains(@class, 'l23 r23')]";
        private readonly string taskSubcontractReasonCell = "./div[contains(@class, 'l24 r24')]";

        private TableElement taskTableEle;
        public TableElement TaskTableEle
        {
            get => taskTableEle;
        }

        public MasterRoundManagementPage()
        {
            taskTableEle = new TableElement(taskTable, taskRow, new List<string>() { taskCheckboxCell, taskSubcontractCell, taskSubcontractReasonCell });
            taskTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        [AllureStep]
        public MasterRoundManagementPage DragRoundToGrid()
        {
            By roundItemXpath = By.XPath(string.Format(roundItem, "REF1-AM Monday "));
            DragAndDrop(roundItemXpath, UnallocatedTaskGrid);
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage ScrollToSubcontractHeader()
        {
            IWebElement e = TaskTableEle.GetCell(0, 1);
            Actions actions = new Actions(this.driver);
            actions.MoveToElement(e).Build().Perform();
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage SelectFirstAndSecondTask()
        {
            TaskTableEle.ClickCell(0, 0);
            SleepTimeInMiliseconds(1000);
            TaskTableEle.ClickCell(1, 0);
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage VerifyFirstAndSecondConfirmedTask(string reason)
        {
            VerifyCellValue(TaskTableEle, 0, 1, "✓");
            VerifyCellValue(TaskTableEle, 1, 1, "✓");
            VerifyCellValue(TaskTableEle, 0, 2, reason);
            VerifyCellValue(TaskTableEle, 1, 2, reason);
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage IsOnPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(contractSelect);
            WaitUtil.WaitForElementVisible(servicesInput);
            WaitUtil.WaitForElementVisible(dateInput);
            WaitUtil.WaitForElementVisible(goBtn);
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage InputSearchDetails(string contract, string service, string date)
        {
            SelectTextFromDropDown(contractSelect, contract);
            ClickOnElement(servicesInput);
            ClickOnElement(serviceOption, service);
            SendKeys(dateInput, date);
            ClickOnElement(goBtn);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForPageLoaded();
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage InputSearchDetails(string contract, string service)
        {
            SelectTextFromDropDown(contractSelect, contract);
            ClickOnElement(servicesInput);
            ClickOnElement(serviceOption, service);
            ClickOnElement(goBtn);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForPageLoaded();
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage InputSearchDetails(string contract, string service, string subService, string date)
        {
            SelectTextFromDropDown(contractSelect, contract);
            ClickOnElement(servicesInput);
            ClickOnElement(serviceExpandIcon, service);
            ClickOnElement(serviceOption, subService);
            if(!string.IsNullOrEmpty(date)) SendKeys(dateInput, date);
            ClickOnElement(goBtn);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForPageLoaded();
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage InputSearchDetails(string date)
        {
            SendKeys(dateInput, date);
            ClickOnElement(goBtn);
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForPageLoaded();
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage ClickFirstUnallocatedTask()
        {
            ClickOnElement(firstUnallocatedTask);
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage ClickFirstAllocatedTask()
        {
            ClickOnElement(firstAllocatedTask);
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage DragAndDropFirstUnallocatedTaskToFirstRound()
        {
            var newSource = GetElement(activeUnallocatedTask);
            var target = GetAllElements(rounds)[0];
            DragAndDrop(newSource, target);
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage DragAndDropSelectedAllocatedTaskToSecondRound()
        {
            var newSource = GetElement(selectedAllocatedTask);
            var target = GetAllElements(rounds)[1];
            AlternativeDragAndDrop(newSource, target);
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage DragAndDropFirstRoundToGrid()
        {
            var target = GetElement(tabContainer);
            var source = GetAllElements(rounds)[0];
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public String GetFirstRoundName()
        {
            return GetAllElements(rounds)[0].Text;
        }
        [AllureStep]
        public MasterRoundManagementPage DragAndDropSecondRoundToGrid()
        {
            var target = GetElement(tabContainer);
            var source = GetAllElements(rounds)[1];
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public TaskModel GetFirstTaskDetails()
        {
            TaskModel temp = new TaskModel();
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(unallocatedHeaders);
            for (int i = 0; i < hds.Count; i++)
            {
                //Changed Description to Site for validation
                if (hds[i].Text.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(activeUnallocatedTaskField);
                    temp.Description = _firstResultFields[i].Text;
                }
                else if (hds[i].Text.Equals("Start Date", StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(activeUnallocatedTaskField);
                    temp.StartDate = _firstResultFields[i].Text;
                    break;
                }
            }
            return temp;
        }
        [AllureStep]
        public TaskModel GetFirstTaskDetailsInActiveRoundTab()
        {
            TaskModel temp = new TaskModel();
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(roundTabHeaders);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstAllocatedTaskField);
                    temp.Description = _firstResultFields[i].Text;
                }
                else if (hds[i].Text.Equals("Start Date", StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstAllocatedTaskField);
                    temp.StartDate = _firstResultFields[i].Text;
                    break;
                }
            }
            return temp;
        }
        [AllureStep]
        public MasterRoundManagementPage FilterRoundBy(string fieldName, string valueToInput)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(roundTabHeaders);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement inputBox = GetElement(String.Format(roundFilterHeader, (i + 1).ToString()));
                    SendKeys(inputBox, valueToInput);
                    break;
                }
            }
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage FilterDescription(string value)
        {
            SendKeys(descriptionFilterHeader, value);
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage VerifyTaskModelDescriptionAndStartDate(TaskModel expected)
        {
            //verify last task in grid
            ScrollDownInElement(slickViewport);
            TaskModel actual = new TaskModel();
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(roundTabHeaders);
            for (int i = 0; i < hds.Count; i++)
            {

                if (hds[i].Text.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement e = GetElement(String.Format(lastTaskField, i.ToString()));
                    actual.Description = e.Text;
                    ScrollMaxToLeft(slickViewport);
                    continue;
                }
                else if (hds[i].Text.Equals("Start Date", StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement e = GetElement(String.Format(lastTaskField, i.ToString()));
                    actual.StartDate = e.Text;
                    break;
                }
            }
            Assert.AreEqual(actual.Description.Contains(expected.Description), true, "Expected " + actual.Description + " to contain " + expected.Description);
            Assert.AreEqual(actual.StartDate.Contains(expected.StartDate), true, "Expected " + actual.StartDate + " to contain " + expected.StartDate);
            return this;
        }
        [AllureStep]
        public MasterRoundManagementPage VerifyTaskModelDescriptionAndEndDate(TaskModel expected)
        {
            //Verify first task in grid
            //ScrollDownInElement(slickViewport);
            TaskModel actual = new TaskModel();
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsPresent(roundTabHeaders);
            for (int i = 0; i < hds.Count; i++)
            {

                if (hds[i].Text.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement e = GetElement(String.Format(taskField, i.ToString()));
                    actual.Description = e.Text;
                    ScrollMaxToLeft(slickViewport);
                    continue;
                }
                else if (hds[i].Text.Equals("End Date", StringComparison.OrdinalIgnoreCase))
                {
                    IWebElement e = GetElement(String.Format(taskField, i.ToString()));
                    actual.EndDate = e.Text;
                    break;
                }
            }
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(actual.EndDate.Contains(expected.EndDate), true);
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage ClickOnSearchRoundBtn()
        {
            ClickOnElement(searchRoundBtn);
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage SendKeyInSearchRound(string roundName)
        {
            SendKeys(searchRoundInput, roundName);
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage SendKeyInDescInput(string descValue)
        {
            SendKeys(descInput, descValue);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage SendKeyInDescInput(string descValue, string tabIndex)
        {
            SendKeys(string.Format(descInputAtAnyTab, tabIndex), descValue);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage VerifyNoRecordInTaskGrid()
        {
            Assert.IsTrue(IsControlUnDisplayed(firstResultRowInTaskGrid));
            return this;
        }

        [AllureStep]
        public MasterRoundManagementPage VerifyFirstRecordWithDescInTaskGrid(string descExp)
        {
            //Get active tab
            Assert.IsTrue(IsControlDisplayed(descAtFirstRowAnyTab, descExp));
            return this;
        }
    }

}
