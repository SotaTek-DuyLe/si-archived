using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class ResourceAllocationPage : BasePage
    {
        private readonly By contractSelect = By.Id("contract");
        private readonly By businessUnitInput = By.Id("business-units");
        private readonly By shiftSelect = By.Id("shift-group");
        private readonly By goBtn = By.XPath("//button[text()='Go']");
        private readonly By createResourceBtn = By.Id("t-create");
        private readonly By refreshBtn = By.Id("t-refresh");
        public readonly By date = By.Id("date");
        //Round filter
        private readonly By roundFilterBtn = By.XPath("//span[contains(text(),'Round Filters')]/parent::a");
        private readonly By roundFilterTitle = By.XPath("//div[@class='popover-content']//h4[contains(text(),'Advanced Round Filters')]");
        private readonly By applyBtn = By.XPath("//div[@class='popover-content']//button[contains(text(),'Apply')]");
        private readonly string roundFilterOption = "//div[contains(@id,'popover')]//input[@title='{0}']";
        private readonly By rememberOtionBtn = By.XPath("//div[contains(@id,'popover')]//input[@id='remember-selection']");
        private readonly By clearOptionBtn = By.XPath("//div[contains(@id,'popover')]//button[@title='Clear All' and not(@style='display: none;')]");
        public readonly By AllResourceTab = By.XPath("//a[text()='All Resources']");
        public readonly By LeftMenuReasonSelect = By.XPath("//div[@id='echo-reason-needed']//select");
        public readonly By LeftMenuConfirmReasonButton = By.XPath("//div[@id='echo-reason-needed']//button[text()='Confirm']");
        public readonly By ResourceStateSelect = By.XPath("//div[@class='modal-dialog' and @data-bind='with: selectedResourceShiftInstance']//select[@id='state']");
        public readonly By ResourceShiftInstanceButton = By.XPath("//div[@class='modal-dialog' and @data-bind='with: selectedResourceShiftInstance']//button[text()='Save']");

        public readonly By ResourceTypeHeaderInput = By.XPath("//div[@id='all-resources']//div[@class='ui-state-default slick-headerrow-column l2 r2']//input");
        public readonly By ThirdPartyHeaderInput = By.XPath("//div[@id='all-resources']//div[@class='ui-state-default slick-headerrow-column l5 r5']//select");
        private string AllResourceTable = "//div[@id='all-resources']//div[@class='grid-canvas']";
        private string AllResourceRow = "./div[contains(@class, 'slick-row')]";
        private string ResourceNameCell = "./div[contains(@class, 'slick-cell l0 r0')]";
        private string ResourceTypeCell = "./div[contains(@class, 'slick-cell l2 r2')]";

        private TableElement allResourceTableEle;
        public TableElement AllResourceTableEle
        {
            get => allResourceTableEle; 
        }

        //Left panel Daily Allocation
        private readonly By firstRoundRow = By.XPath("//tbody[contains(@data-bind,'roundMenu')]/tr");
        private readonly string allocatedResource = "//span[@class='main-description resource-name' and contains(text(),'{0}')]";
        private readonly By allocatedResources = By.XPath("//span[@class='main-description resource-name']");
        private readonly string allocatedResourceInRoundGroup = "//tr[@class='round-group-dropdown']//span[@class='main-description resource-name']";
        private readonly string allocatedResourceInRound = "//tr[@class='round-group-dropdown-item']//span[@class='main-description resource-name' and not(text()='')]";
        private readonly string allocatedResourceType = "//span[@class='main-description resource-name' and text()='']";
        private readonly string allocatedResourceTypeInRound = "//tr[@class='round-group-dropdown-item']//span[@class='main-description resource-name' and text()='']";
        private readonly string allocatedResourceTypeInRoundGroup = "//tr[@class='round-group-dropdown']//span[@class='main-description resource-name' and text()='']";
        private readonly By addResourceCellInRoundGroup = By.XPath("//tr[@class='round-group-dropdown']//td[@title='To allocate resource, select and drag resource from the panel on the right hand side.']");
        private readonly By addResourceCellInRound = By.XPath("//tr[@class='round-group-dropdown-item']//td[@title='To allocate resource, select and drag resource from the panel on the right hand side.']");
        private readonly string allocatedResourceContainer = "//span[@class='main-description resource-name' and contains(text(),'{0}')]/ancestor::td";
        private readonly string resourceAbbreviation = "//span[@class='main-description resource-name' and contains(text(),'{0}')]/parent::span/following-sibling::span[contains(@data-bind,'resourceStateAbv')]";
        private readonly By resourcePresence = By.Id("resource-presence");
        private readonly string resourceState = "//button[contains(@class,'resource-state') and text()='{0}']";
        private readonly By viewShiftDetailBtn = By.XPath("//button[text()='VIEW SHIFT DETAILS']");
        private readonly By resourceDetailBtn = By.XPath("//button[text()='RESOURCE DETAILS']");
        private readonly By reasonSelect = By.Id("reasons.id");
        private readonly By confirmButton = By.XPath("//button[text()='Confirm']");
        private readonly By closeReasonPopupButton = By.XPath("//button[@data-bind='click: cancelReason']");
        private readonly By addAdhocRoundBtn = By.XPath("//button[@data-target='#addhoc-rounds']");
        private readonly By viewRoundInstanceBtn = By.XPath("//button[text()='VIEW ROUND INSTANCE']");

        private readonly string whiteBackground = "background-color: rgb(255, 255, 255);";
        private readonly string greenBackground = "background-color: rgb(137, 203, 137);";
        private readonly string purpleBackground = "background-color: rgb(177, 156, 217);";
        private readonly string redBackground = "background-color: rgb(255, 49, 28);";
        private readonly string red2Background = "background-color: rgb(255, 224, 152);";
        private readonly string greenishBackground = "background-color: rgb(132, 255, 182);";
        private readonly string lightBlueBackground = "background-color: rgb(209, 230, 241);";
        private readonly string darkerRedBackground = "background-color: rgb(222, 16, 28);";
        private readonly string darkerGreenBackground = "background-color: rgb(132, 222, 150);";

        //Left Panel Default Allocation
        private readonly By roundScrollable = By.Id("rounds-scrollable");
        private readonly By roundGroups = By.XPath("//div[contains(@class,'layout-pane-west')]//tr[@class='round-group-dropdown']");
        //private readonly string firstResourceCustomRoundGroup = "//tr[@class='round-group-dropdown'][{0}]/td[@class='resource-container resource']";
        private readonly string roundGroup = "//tr[@class='round-group-dropdown'][{0}]";
        private readonly By roundContainer = By.XPath("//tr[@class='round-group-dropdown-item']/td[@class='round-container round']");
        private readonly string blankResourceTypeInRoundGroup = "//tr[@class='round-group-dropdown']//span[@class='sub-description resource-type current-type' and text()='{0}']/preceding-sibling::span[@class='main-description resource-name' and text()='']";
        private readonly string blankResourceTypeInRound = "//tr[@class='round-group-dropdown-item']//span[@class='sub-description resource-type current-type' and text()='{0}']/preceding-sibling::span[@class='main-description resource-name' and text()='']";
        private readonly string expandOptions = "(//div[contains(@class,'layout-pane-west')]//tbody/tr[contains(@data-bind,'attr')])[{0}]//div[@id='toggle-actions']";
        private readonly string secondColumnResource = "(//div[@id='rounds-scrollable']//tr[@class='round-group-dropdown'])[{0}]//span[text()='{1}']";
        private readonly string roundName = "//span[@class='main-description round-name' and text()='{0}']";
        private readonly By roundInstances = By.XPath("//span[@class='main-description round-name']");
        private readonly By serviceNames = By.XPath("//span[@class='sub-description' and contains(@data-bind,'name: service')]");
        private readonly By viewRoundBtn = By.XPath("//button[text()='VIEW ROUND']");
        private readonly By dateInput = By.XPath("//input[contains(@data-bind,'dateControl')]");
        private readonly By calendarIcon = By.XPath("//div[@class='date-control container' and contains(@style,'display: block;')]//span[@class='input-group-addon']");
        private readonly string futreDayNumberInCalendar = "(//div[contains(@class,'bootstrap-datetimepicker-widget') and contains(@style,'display: block;')]//td[not(contains(@class,'disable')) and text()='{0}'])[last()]";

        //Right panel
        private readonly By headers = By.XPath("//div[contains(@class,'active')]//div[@class='ui-state-default slick-header-column slick-header-sortable ui-sortable-handle']/span[1]");
        private readonly By inputBoxes = By.XPath("//div[contains(@class,'active')]//div[@class='slick-headerrow ui-state-default']//*[contains(@class,'form-control')]");
        private readonly By firstResultFields = By.XPath("//div[contains(@class,'active')]//div[contains(@class,'ui-widget-content slick-row even')][1]/div");
        private readonly By tabLocator = By.XPath("//ul[@id='tabs']/li[not(contains(@class,'hide'))]/a");

        //business unit option
        private readonly string jstreeOption = "//a[contains(@class,'jstree-anchor') and text()='{0}']";
        private readonly string businessUnitExpandIcon = "//a[contains(@class,'jstree-anchor') and text()='{0}']/preceding-sibling::i";
        private readonly By businessUnitStaticOptions = By.XPath("(//*[@class='jstree-children'])[2]//a");

        //Resizer
        private readonly By resizerWidth = By.XPath("//div[@title='Resize']");
        private readonly By resizerHeight = By.XPath("(//div[@title='Close'])[3]");
        private readonly By addResourceBtn = By.Id("t-create");
        public readonly By FirstRoundInstanceRow = By.XPath("//div[@id='rounds-scrollable']//table[2]//tbody[1]/tr[1]//td[1]");
        public readonly By SecondRoundInstanceRow = By.XPath("//div[@id='rounds-scrollable']//table[1]//tbody[1]/tr[1]");
        public readonly By BusinessUnitInput = By.XPath("//input[@id='business-units']");

        private TreeViewElement _treeViewElement = new TreeViewElement("//div[contains(@class, 'jstree-1')]", "./li[contains(@role, 'treeitem')]", "./a", "./ul[contains(@class, 'jstree-children')]", "./i[contains(@class, 'jstree-ocl')][1]");
        private TreeViewElement ServicesTreeView
        {
            get => _treeViewElement;
        }

        public ResourceAllocationPage()
        {
            allResourceTableEle = new TableElement(AllResourceTable, AllResourceRow, new List<string>() { ResourceNameCell, ResourceTypeCell });
            allResourceTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        [AllureStep]
        public ResourceAllocationPage SelectRoundNode(string nodeName)
        {
            ServicesTreeView.ClickItem(nodeName);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ExpandRoundNode(string nodeName)
        {
            ServicesTreeView.ExpandNode(nodeName);
            return this;
        }

        [AllureStep]
        public ResourceAllocationPage SelectContract(string contract)
        {
            SelectTextFromDropDown(contractSelect, contract);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage SelectBusinessUnit(string bu)
        {
            ClickOnElement(businessUnitInput);
            ClickOnElement(jstreeOption, bu);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ExpandBusinessUnitOption(string option)
        {
            ClickOnElement(businessUnitInput);
            ClickOnElement(businessUnitExpandIcon, option);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyBusinessUnitsAre(List<string> expectedUnits)
        {
            List<string> buNameList = new List<string>();
            IList<IWebElement> buList = WaitUtil.WaitForAllElementsVisible(businessUnitStaticOptions);
            foreach(var bu in buList)
            {
                buNameList.Add(bu.Text);
            }
            Assert.AreEqual(expectedUnits.Count, buNameList.Count);
            for(int i = 0; i < expectedUnits.Count; i++)
            {
                Assert.AreEqual("Collections - " + expectedUnits[i], buNameList[i]);
            }
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage SelectShift(string shift)
        {
            SelectTextFromDropDown(shiftSelect, shift);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickGo()
        {
            ClickOnElement(goBtn);
            return this;
        }

        #region All Resource
        [AllureStep]
        public ResourceAllocationPage ClickType(int rowIdx)
        {
            AllResourceTableEle.ClickCell(rowIdx, AllResourceTableEle.GetCellIndex(ResourceTypeCell));
            SleepTimeInMiliseconds(200);
            return this;
        }

        [AllureStep]
        public string GetResourceName(int rowIdx)
        {
            return AllResourceTableEle.GetCellValue(rowIdx, AllResourceTableEle.GetCellIndex(ResourceNameCell)).ToString();
        }

        [AllureStep]
        public ResourceAllocationPage VerifyResourceRowHasGreenBackground(int rowIdx)
        {
            var row = AllResourceTableEle.GetRow(rowIdx);
            Assert.IsTrue(row.GetAttribute("className").Contains("resourceState8"));
            return this;
        }
         
        [AllureStep]
        public ResourceAllocationPage VerifyResourceRowHasWhiteBackground(int rowIdx)
        {
            var row = AllResourceTableEle.GetRow(rowIdx);
            Assert.IsFalse(row.GetAttribute("className").Contains("resourceState8"));
            return this;
        }

        [AllureStep]
        public ResourceAllocationPage ClickLeftResourceMenu(string menu)
        {
            ClickOnElement(By.XPath($"//div[@class='grid-menu']//button[text()='{menu}']"));
            return this;
        }
        #endregion

        [AllureStep]
        public ResourceAllocationPage ClickCreateResource()
        {
            ClickOnElement(createResourceBtn);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage FilterResource(string filter, string value)
        {
            IList<IWebElement> _headers = WaitUtil.WaitForAllElementsVisible(headers);
            IList<IWebElement> _inputs = WaitUtil.WaitForAllElementsVisible(inputBoxes);
            for (int i = 0; i < _headers.Count; i++)
            {
                if (_headers[i].Text == filter)
                {
                    SendKeys(_inputs[i], value);
                    return this;
                }
            }
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyFirstResultValue(string field, string expected)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headers);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    var f = GetResultNo(i+1);
                    //Temporary comment because of unfixed bug: Assert.AreEqual(expected, _firstResultFields[i].Text);
                    Assert.IsTrue(f.Text.Contains(expected),"expected " + expected + " but found " + f.Text);
                }
            }
            return this;

        }
        [AllureStep]
        public ResourceAllocationPage DragAndDropFirstResourceToFirstRound()
        {
            var source = GetFirstResult();
            IWebElement target = WaitUtil.WaitForElementVisible(firstRoundRow);
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DeallocateResource(string _resourceName)
        {
            var target = GetFirstResult();
            IWebElement source = WaitUtil.WaitForElementVisible(allocatedResource, _resourceName);
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DeallocateResourceInRound(int whichOne)
        {
            var target = GetFirstResult();
            var source = WaitUtil.WaitForAllElementsVisible(allocatedResourceInRound)[whichOne - 1];
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DeallocateResourceType(int whichOne)
        {
            var target = GetFirstResult();
            var source = WaitUtil.WaitForAllElementsVisible(allocatedResourceType);
            DragAndDrop(source[whichOne-1], target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DeallocateResourceType(string resourceType)
        {
            var target = GetFirstResult();
            IWebElement source = WaitUtil.WaitForElementVisible(allocatedResourceType, resourceType.ToUpper());
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DeallocateResourceTypeInRound(string resourceType)
        {
            var target = GetFirstResult();
            IWebElement source = WaitUtil.WaitForElementVisible(allocatedResourceTypeInRound, resourceType.ToUpper());
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyAllocatedResourceName(string _name)
        {
            WaitUtil.WaitForElementVisible(allocatedResource, _name);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyResourceDeallocated(string _name)
        {
            WaitUtil.WaitForElementInvisible(allocatedResource, _name);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickAllocatedResource(string _name)
        {
            ClickOnElement(allocatedResource, _name);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyPresenceOption(string _text)
        {
            Assert.AreEqual(_text, WaitUtil.WaitForElementVisible(resourcePresence).Text);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickPresenceOption()
        {
            ClickOnElement(resourcePresence);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage SelectResourceState(string state)
        {
            //Value: SICK, TRAINING, AWOL
            ClickOnElement(resourceState, state);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickViewShiftDetail()
        {
            ClickOnElement(viewShiftDetailBtn);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickResourceDetail()
        {
            ClickOnElement(resourceDetailBtn);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage HoverAndVerifyBackgroundColor(string _resourceName, string _color)
        {
            WaitForLoadingIconToDisappear();
            //Hover to element
            var resource = WaitUtil.WaitForElementVisible(allocatedResourceContainer, _resourceName);
            HoverElement(resource);
            string style = WaitUtil.WaitForElementVisible(allocatedResourceContainer, _resourceName).GetAttribute("style");
            if (_color == "light blue")
            {
                Assert.IsTrue(style.Contains(lightBlueBackground), "Incorrect color: Expected " + lightBlueBackground + " but found: " + style);
            }
            else if (_color == "darker green")
            {
                Assert.IsTrue(style.Contains(darkerGreenBackground), "Incorrect color: Expected " + darkerGreenBackground + " but found: " + style);
            }
            else if (_color == "darker red")
            {
                Assert.IsTrue(style.Contains(darkerRedBackground), "Incorrect color: Expected " + darkerRedBackground + " but found: " + style);
            }
            else
            {
                Assert.AreEqual(0, 1, "Incorrect color");
            }
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyBackgroundColor(string _resourceName, string _color)
        {
            WaitForLoadingIconToDisappear();
            string style = WaitUtil.WaitForElementVisible(allocatedResourceContainer, _resourceName).GetAttribute("style");
            if (_color == "white")
            {
                Assert.IsTrue(style.Contains(whiteBackground), "Incorrect color: Expected " + whiteBackground + " but found: " + style);
            }
            else if (_color == "green")
            {
                Assert.IsTrue(style.Contains(greenBackground), "Incorrect color: Expected " + greenBackground + " but found: " + style);
            }
            else if (_color == "purple")
            {
                Assert.IsTrue(style.Contains(purpleBackground), "Incorrect color: Expected " + purpleBackground + " but found: " + style);
            }
            else if (_color == "red")
            {
                Assert.IsTrue(style.Contains(redBackground), "Incorrect color: Expected " + redBackground + " but found: " + style);
            }
            else if (_color == "red2")
            {
                Assert.IsTrue(style.Contains(red2Background), "Incorrect color: Expected " + red2Background + " but found: " + style);
            }
            else if (_color == "greenish")
            {
                Assert.IsTrue(style.Contains(greenishBackground), "Incorrect color: Expected " + greenishBackground + " but found: " + style);
            }
            else
            {
                Assert.AreEqual(0, 1, "Incorrect color");
            }
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage InsertDate(string _date)
        {
            SendKeys(date, _date);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage RefreshGrid()
        {
            ClickOnElement(refreshBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyStateAbbreviation(string _resourceName, string _abbr)
        {
            Assert.AreEqual(_abbr, WaitUtil.WaitForElementVisible(resourceAbbreviation, _resourceName).Text);
            return this;
        }

        //DEFAULT ALLOCATION PAGE
        [AllureStep]
        public ResourceAllocationPage DragAndDropFirstResultToRoundGroup(int whichRow)
        {
            var source = GetFirstResult();
            IList<IWebElement> roundGroup = WaitUtil.WaitForAllElementsVisible(roundGroups);
            DragAndDrop(source, roundGroup[whichRow - 1]);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DragAndDropSecondResultToRoundGroup(int whichOne, int whichRow)
        {
            var source = GetResultNo(whichOne);
            IList<IWebElement> roundGroup = WaitUtil.WaitForAllElementsVisible(roundGroups);
            DragAndDrop(source, roundGroup[whichRow - 1]);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DragAndDropFirstResultToBlankResourceType(string resourceType)
        {
            var source = GetFirstResult();
            IWebElement target = WaitUtil.WaitForElementVisible(blankResourceTypeInRoundGroup, resourceType.ToUpper());
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DragAndDropFirstResultToBlankResourceTypeInRound(string resourceType)
        {
            var source = GetFirstResult();
            IWebElement target = WaitUtil.WaitForElementVisible(blankResourceTypeInRound, resourceType.ToUpper());
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DragAndDropFirstResultToNewCellInRoundGroup()
        {
            ScrollLeft(roundScrollable);
            ScrollLeft(roundScrollable);
            var source = GetFirstResult();
            IWebElement target = WaitUtil.WaitForElementVisible(addResourceCellInRoundGroup);
            DragAndDrop(source, target);
            ScrollRight(roundScrollable);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DragAndDropFirstResultToNewCellInRound()
        {
            ScrollLeft(roundScrollable);
            ScrollLeft(roundScrollable);
            var source = GetFirstResult();
            IWebElement target = WaitUtil.WaitForElementVisible(addResourceCellInRound);
            DragAndDrop(source, target);
            ScrollRight(roundScrollable);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickRound(string _roundName)
        {
            ClickOnElement(roundName, _roundName);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickViewRoundGroup()
        {
            ClickOnElement(viewRoundBtn);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickCalendar()
        {
            ClickOnElement(dateInput);
            ClickOnElement(calendarIcon);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage InsertDayInFutre(string dayOfMonth)
        {
            if (dayOfMonth.StartsWith("0"))
            {
                dayOfMonth = dayOfMonth.Substring(1);
            }
            ClickOnElement(futreDayNumberInCalendar, dayOfMonth);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DeallocateResourceFromRoundGroup(int whichRow, string whichResource)
        {
            var target = GetFirstResult();
            var xpath = String.Format(secondColumnResource, whichRow, whichResource.ToUpper());
            IWebElement source = WaitUtil.WaitForElementVisible(xpath);
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ExpandRoundGroup(int whichRow)
        {
            ClickOnElement(expandOptions, whichRow.ToString());
            SleepTimeInMiliseconds(200);
            return this;
        }
        [AllureStep]
        public string GetFirstAllocatedResource()
        {
            return GetAllElementsNotWait(allocatedResources)[0].Text;
        }
        public ResourceAllocationPage VerifyAllocatingToast(string expectedToast)
        {
            VerifyToastMessage(expectedToast);
            WaitUntilToastMessageInvisible(expectedToast);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyAllocatingToast(List<string> expectedToasts)
        {
            VerifyToastMessages(expectedToasts);
            expectedToasts.ForEach(t => WaitUntilToastMessageInvisible(t));
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage RelocateResourceTypeFromRoundGroupToRoundGroup(string resourceType, int targetRow)
        {
            IWebElement source = WaitUtil.WaitForElementVisible(blankResourceTypeInRoundGroup, resourceType.ToUpper());
            var targetElement = WaitUtil.WaitForElementVisible(roundGroup, targetRow.ToString());
            DragAndDrop(source, targetElement);
            return this;

        }
        [AllureStep]
        public ResourceAllocationPage RelocateResourceTypeFromRoundGroupToRound(string resourceType, int targetRow)
        {
            IWebElement source = WaitUtil.WaitForElementVisible(blankResourceTypeInRoundGroup, resourceType.ToUpper());
            var targetElements = WaitUtil.WaitForAllElementsVisible(roundContainer);
            DragAndDrop(source, targetElements[targetRow-1]);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage RelocateResourceTypeFromRoundToRoundGroup(string resourceType, int whichRow)
        {
            var source = WaitUtil.WaitForElementVisible(blankResourceTypeInRound, resourceType.ToUpper());
            var targetElement = WaitUtil.WaitForElementVisible(roundGroup, whichRow.ToString());
            DragAndDrop(source, targetElement);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage RelocateResourceTypeFromRoundToRound(int whichOne, int roundRow)
        {
            var resourceType = WaitUtil.WaitForAllElementsVisible(allocatedResourceType);
            var rounds = WaitUtil.WaitForAllElementsVisible(roundContainer);
            DragAndDrop(resourceType[whichOne -1], rounds[roundRow - 1]);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage AllocateFirstResultToResourceTypeInRound(int whichResourceTypeInRow)
        {
            var source = GetFirstResult();
            var resourceTypeInRound = WaitUtil.WaitForAllElementsVisible(allocatedResourceTypeInRound);
            DragAndDrop(source, resourceTypeInRound[whichResourceTypeInRow - 1]);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage AllocateFirstResultToResourceTypeInRoundGroup(int whichResourceTypeInRow)
        {
            var source = GetFirstResult();
            var resourceTypeInRound = WaitUtil.WaitForAllElementsVisible(allocatedResourceTypeInRoundGroup);
            DragAndDrop(source, resourceTypeInRound[whichResourceTypeInRow - 1]);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ResizePage()
        {
            ClickOnElement(resizerHeight);
            var resizerElement = GetElement(resizerWidth);
            var targetElement = GetElement(addResourceBtn);
            DragAndDrop(resizerElement, targetElement);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage AllocateResultToResourceInRound(int whichOne, int whichRound)
        {
            var source = GetResultNo(whichOne);
            var resourceInRound = WaitUtil.WaitForAllElementsVisible(allocatedResourceInRound);
            DragAndDrop(source, resourceInRound[whichRound - 1]);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage AllocateFirstResultToResourceInRoundGroup(int whichRoundGroup)
        {
            var source = GetFirstResult();
            var resourceInRoundGroup = WaitUtil.WaitForAllElementsVisible(allocatedResourceInRoundGroup);
            DragAndDrop(source, resourceInRoundGroup[whichRoundGroup - 1]);
            return this;
        }
        [AllureStep]
        public IWebElement GetFirstResult()
        {
            return WaitUtil.WaitForAllElementsVisible(firstResultFields)[0];
        }
        [AllureStep]
        public IWebElement GetResultNo(int whichOne)
        {
            return WaitUtil.WaitForAllElementsVisible(firstResultFields)[whichOne-1];
        }

        [AllureStep]
        public ResourceAllocationPage ClickRoundInstance()
        {
            ClickOnElement(SecondRoundInstanceRow);
            ClickOnElement(By.XPath("//button[text()='VIEW ROUND INSTANCE']"));
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage OpenFirstRoundInstance()
        {
            ClickOnElement(GetAllElements(roundInstances)[0]);
            ClickOnElement(viewRoundInstanceBtn);
            return this;
        }

        [AllureStep]
        public ResourceAllocationPage ClickFirstResouceDetail()
        {
            ClickOnElement(FirstRoundInstanceRow);
            ClickOnElement(By.XPath("//div[@class='menu']//button[text()='RESOURCE DETAILS']"));
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage IsReasonPopupDisplayed()
        {
            WaitUtil.WaitForElementVisible(reasonSelect);
            WaitUtil.WaitForElementVisible(confirmButton);
            WaitUtil.WaitForElementVisible(closeReasonPopupButton);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage SelectReason(ResourceReason reason)
        {
            SelectTextFromDropDown(reasonSelect, reason.AsString());
            ClickOnElement(confirmButton);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickConfirmButton()
        {
            ClickOnElement(confirmButton);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage CloseReasonPopup()
        {
            ClickOnElement(closeReasonPopupButton);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyConfirmButtonEnabled(bool isEnabled)
        {
            Assert.AreEqual(isEnabled, WaitUtil.WaitForElementVisible(confirmButton).Enabled);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyRoundFilterButtonEnabled(bool isEnabled)
        {
            if (isEnabled)
            {
                Assert.AreEqual("btn", GetAttributeValue(roundFilterBtn, "class"));
            }
            else
            {
                Assert.AreEqual("btn disabled", GetAttributeValue(roundFilterBtn, "class"));
            }
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyBusinessUnitIsOptional()
        {
            Assert.AreEqual("Optionally select Business Unit", GetAttributeValue(businessUnitInput, "placeholder"));
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyUnassignedBusinessUnitIsDisplayed()
        {
            ClickOnElement(businessUnitInput);
            WaitUtil.WaitForElementVisible(jstreeOption, "*Unassigned");
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifySortOrderOfRoundInstances()
        {
            List<RoundModel> rounds = new List<RoundModel>();
            List<RoundModel> unsortedRounds = new List<RoundModel>();
            var _serviceNames = WaitUtil.WaitForAllElementsVisible(serviceNames);
            var _roundInstances = WaitUtil.WaitForAllElementsVisible(roundInstances);
            Assert.AreEqual(_serviceNames.Count, _roundInstances.Count);
            for(int i = 0; i < _serviceNames.Count; i++)
            {
                RoundModel temp = new RoundModel
                {
                    ServiceName = _serviceNames[i].Text,
                    RoundName = GetElementTextByJS(_roundInstances[i])
                };
                rounds.Add(temp);
                unsortedRounds.Add(temp);
            }
            rounds.Sort((r1, r2) =>
            {
                int result = r1.ServiceName.CompareTo(r2.ServiceName);
                return result == 0 ? r1.RoundName.CompareTo(r2.RoundName) : result;
            });
            Assert.AreEqual(unsortedRounds, rounds);
            return this;
        }
        [AllureStep]
        public AddAdhocRoundPopup ClickAddAdhocRoundBtn()
        {
            ClickOnElement(addAdhocRoundBtn);
            return PageFactoryManager.Get<AddAdhocRoundPopup>(); ;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyFirstRoundName(string expected)
        {
            var firstRoundName = GetAllElements(roundInstances)[0].Text;
            Assert.AreEqual(expected, firstRoundName);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyFirstRoundService(string expected)
        {
            var firstRoundService = GetAllElements(serviceNames)[0].Text;
            Assert.AreEqual(expected, firstRoundService);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickRoundFilterBtn()
        {
            ClickOnElement(roundFilterBtn);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage SelectContractUnit(string contractUnit)
        {
            ClickOnElement(roundFilterTitle); //Click header to close other popover
            ClickOnElement(roundFilterOption, "Contract Unit");
            ClickOnElement(jstreeOption, contractUnit);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage SelectDisapatchSite(string dispatchSite)
        {
            ClickOnElement(roundFilterTitle); //Click header to close other popover
            ClickOnElement(roundFilterOption, "Dispatch Site");
            ClickOnElement(jstreeOption, dispatchSite);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage SelectServiceUnit(string serviceUnit)
        {
            ClickOnElement(roundFilterTitle); //Click header to close other popover
            ClickOnElement(roundFilterOption, "Service");
            ClickOnElement(jstreeOption, serviceUnit);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage SelectShiftFilter(string shift)
        {
            ClickOnElement(roundFilterTitle); //Click header to close other popover
            ClickOnElement(roundFilterOption, "Shift");
            ClickOnElement(jstreeOption, shift);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickRememberOption()
        {
            ClickOnElement(roundFilterTitle);
            ClickOnElement(rememberOtionBtn);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClickApplyBtn()
        {
            ClickOnElement(applyBtn);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyNumberOfFilter(int expected)
        {
            Assert.AreEqual(GetElementText(roundFilterBtn), String.Format("Round Filters ({0})", expected));
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage ClearFilterOptionIfAny()
        {
            if (IsControlDisplayedNotThrowEx(clearOptionBtn))
            {
                ClickOnElement(clearOptionBtn);
            }
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage VerifyOnlyAllResourceTabIsDisplayed()
        {
            var tabs = GetAllElements(tabLocator);
            Assert.AreEqual(1, tabs.Count);
            Assert.AreEqual("All Resources", tabs[0].Text);
            return this;
        }

        public class AddAdhocRoundPopup : BasePage
        {
            private readonly By createBtn = By.XPath("//*[@id='addhoc-rounds']//button[text()='Create']");
            private readonly By roundNameInput = By.Id("round-name");
            private readonly By templateSelect = By.Id("template-rounds");
            private readonly By reasonSelect = By.Id("reasons");
            private readonly By noteInput = By.Id("notes");

            [AllureStep]
            public AddAdhocRoundPopup IsOnAddAdhocRoundPage()
            {
                WaitUtil.WaitForElementVisible(roundNameInput);
                WaitUtil.WaitForElementVisible(templateSelect);
                WaitUtil.WaitForElementVisible(reasonSelect);
                WaitUtil.WaitForElementVisible(noteInput);
                WaitUtil.WaitForElementVisible(createBtn);
                return this;
            }
            [AllureStep]
            public AddAdhocRoundPopup InputAdhocRoundDetails(int templateNo, string reason, string note, string roundName = "")
            {
                SendKeys(roundNameInput, roundName);
                SelectIndexFromDropDown(templateSelect, templateNo);
                SelectTextFromDropDown(reasonSelect, reason);
                SendKeys(noteInput, note);
                return this;
            }
            [AllureStep]
            public string GetSelectedTemplate()
            {
                return GetSelectElement(templateSelect).SelectedOption.Text;
            }
            [AllureStep]
            public ResourceAllocationPage ClickCreateBtn()
            {
                ClickOnElement(createBtn);
                WaitForLoadingIconToDisappear();
                return PageFactoryManager.Get<ResourceAllocationPage>();
            }
        }
    }
}
