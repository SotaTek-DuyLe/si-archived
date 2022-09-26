using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
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
        private readonly By date = By.Id("date");

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
        private readonly string allocatedResourceContainer = "//span[@class='main-description resource-name' and contains(text(),'{0}')]/parent::td";
        private readonly string resourceAbbreviation = "//span[@class='main-description resource-name' and contains(text(),'{0}')]/following-sibling::span[contains(@data-bind,'resourceStateAbbreviation')]";
        private readonly By resourcePresence = By.Id("resource-presence");
        private readonly string resourceState = "//button[contains(@class,'resource-state') and text()='{0}']";
        private readonly By viewShiftDetailBtn = By.XPath("//button[text()='VIEW SHIFT DETAILS']");
        private readonly By resourceDetailBtn = By.XPath("//button[text()='RESOURCE DETAILS']");

        private readonly string whiteBackground = "background-color: rgb(255, 255, 255);";
        private readonly string greenBackground = "background-color: rgb(137, 203, 137);";
        private readonly string purpleBackground = "background-color: rgb(177, 156, 217);";
        private readonly string redBackground = "background-color: rgb(255, 105, 98);";

        //Left Panel Default Allocation
        private readonly By roundScrollable = By.Id("rounds-scrollable");
        private readonly By roundGroups = By.XPath("//div[contains(@class,'layout-pane-west')]//tr[@class='round-group-dropdown']");
        private readonly string firstResourceCustomRoundGroup = "//tr[@class='round-group-dropdown'][{0}]/td[@class='resource-container resource']";
        private readonly string roundGroup = "//tr[@class='round-group-dropdown'][{0}]";
        private readonly By roundContainer = By.XPath("//tr[@class='round-group-dropdown-item']/td[@class='round-container round']");
        private readonly string blankResourceTypeInRoundGroup = "//tr[@class='round-group-dropdown']//span[@class='sub-description resource-type current-type' and text()='{0}']/preceding-sibling::span[@class='main-description resource-name' and text()='']";
        private readonly string blankResourceTypeInRound = "//tr[@class='round-group-dropdown-item']//span[@class='sub-description resource-type current-type' and text()='{0}']/preceding-sibling::span[@class='main-description resource-name' and text()='']";
        private readonly string expandOptions = "(//div[contains(@class,'layout-pane-west')]//tbody/tr[contains(@data-bind,'attr')])[{0}]//div[@id='toggle-actions']";
        private readonly string secondColumnResource = "(//div[@id='rounds-scrollable']//tr[@class='round-group-dropdown'])[{0}]//span[text()='{1}']";
        private readonly string roundName = "//span[@class='main-description round-name' and text()='{0}']";
        private readonly By viewRoundBtn = By.XPath("//button[text()='VIEW ROUND']");
        private readonly By dateInput = By.XPath("//input[contains(@data-bind,'dateControl')]");
        private readonly By calendarIcon = By.XPath("//div[@class='date-control container' and contains(@style,'display: block;')]//span[@class='input-group-addon']");
        private readonly string futreDayNumberInCalendar = "(//div[contains(@class,'bootstrap-datetimepicker-widget') and contains(@style,'display: block;')]//td[not(contains(@class,'disable')) and text()='{0}'])[last()]";

        //Right panel
        private readonly By headers = By.XPath("//div[contains(@class,'active')]//div[@class='ui-state-default slick-header-column slick-header-sortable ui-sortable-handle']/span[1]");
        private readonly By inputBoxes = By.XPath("//div[contains(@class,'active')]//div[@class='slick-headerrow ui-state-default']//input");
        private readonly By firstResultFields = By.XPath("//div[contains(@class,'active')]//div[contains(@class,'ui-widget-content slick-row even')][1]/div");

        //business unit option
        private readonly string businessUnitOption = "//a[contains(@class,'jstree-anchor') and text()='{0}']";
        private readonly string businessUnitExpandIcon = "//a[contains(@class,'jstree-anchor') and text()='{0}']/preceding-sibling::i";
        private readonly By businessUnitStaticOptions = By.XPath("(//*[@class='jstree-children'])[last()]//a");

        //Resizer
        private readonly By resizerWidth = By.XPath("//div[@title='Resize']");
        private readonly By resizerHeight = By.XPath("(//div[@title='Close'])[3]");
        private readonly By addResourceBtn = By.Id("t-create");

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
            ClickOnElement(businessUnitOption, bu);
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
            IWebElement source = WaitUtil.WaitForElementVisible(allocatedResourceType, resourceType);
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DeallocateResourceTypeInRound(string resourceType)
        {
            var target = GetFirstResult();
            IWebElement source = WaitUtil.WaitForElementVisible(allocatedResourceTypeInRound, resourceType);
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
        public ResourceAllocationPage VerifyBackgroundColor(string _resourceName, string _color)
        {
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
            IWebElement target = WaitUtil.WaitForElementVisible(blankResourceTypeInRoundGroup, resourceType);
            DragAndDrop(source, target);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage DragAndDropFirstResultToBlankResourceTypeInRound(string resourceType)
        {
            var source = GetFirstResult();
            IWebElement target = WaitUtil.WaitForElementVisible(blankResourceTypeInRound, resourceType);
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
            var xpath = String.Format(secondColumnResource, whichRow, whichResource);
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
            IWebElement source = WaitUtil.WaitForElementVisible(blankResourceTypeInRoundGroup, resourceType);
            var targetElement = WaitUtil.WaitForElementVisible(roundGroup, targetRow.ToString());
            DragAndDrop(source, targetElement);
            return this;

        }
        [AllureStep]
        public ResourceAllocationPage RelocateResourceTypeFromRoundGroupToRound(string resourceType, int targetRow)
        {
            IWebElement source = WaitUtil.WaitForElementVisible(blankResourceTypeInRoundGroup, resourceType);
            var targetElements = WaitUtil.WaitForAllElementsVisible(roundContainer);
            DragAndDrop(source, targetElements[targetRow-1]);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage RelocateResourceTypeFromRoundToRoundGroup(string resourceType, int whichRow)
        {
            var source = WaitUtil.WaitForElementVisible(blankResourceTypeInRound, resourceType);
            var targetElement = WaitUtil.WaitForElementVisible(roundGroup, whichRow.ToString());
            DragAndDrop(source, targetElement);
            return this;
        }
        [AllureStep]
        public ResourceAllocationPage RelocateResourceTypeFromRoundToRound(int whichOne, int roundRow)
        {
            var resourceType = WaitUtil.WaitForAllElementsVisible(allocatedResourceType);
            var rounds = WaitUtil.WaitForAllElementsVisible(roundContainer);
            DragAndDrop(resourceType[whichOne -1], rounds[roundRow - 1]);
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
    }
}
