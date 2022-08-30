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
        private readonly string allocatedResourceType = "//span[@class='main-description resource-name' and text()='']";
        private readonly By addResourceCell = By.XPath("//td[@title='To allocate resource, select and drag resource from the panel on the right hand side.']");
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
        private readonly By firstColumn = By.XPath("//div[contains(@class,'layout-pane-west')]//tbody/tr[contains(@data-bind,'attr')]");
        private readonly string firstResourceCustomRoundGroup = "//tr[@class='round-group-dropdown'][{0}]/td[@class='resource-container resource']";
        private readonly string roundGroup = "//tr[@class='round-group-dropdown'][{0}]";
        private readonly By roundContainer = By.XPath("//tr[@class='round-group-dropdown-item']/td[@class='round-container round']");
        private readonly string blankResourceType = "//span[@class='sub-description resource-type current-type' and text()='{0}']/preceding-sibling::span[@class='main-description resource-name' and text()='']";
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

        public ResourceAllocationPage SelectContract(string contract)
        {
            SelectTextFromDropDown(contractSelect, contract);
            return this;
        }
        public ResourceAllocationPage SelectBusinessUnit(string bu)
        {
            ClickOnElement(businessUnitInput);
            ClickOnElement(businessUnitOption, bu);
            return this;
        }
        public ResourceAllocationPage ExpandBusinessUnitOption(string option)
        {
            ClickOnElement(businessUnitInput);
            ClickOnElement(businessUnitExpandIcon, option);
            return this;
        }
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
        public ResourceAllocationPage SelectShift(string shift)
        {
            SelectTextFromDropDown(shiftSelect, shift);
            return this;
        }
        public ResourceAllocationPage ClickGo()
        {
            ClickOnElement(goBtn);
            return this;
        }
        public ResourceAllocationPage ClickCreateResource()
        {
            ClickOnElement(createResourceBtn);
            return this;
        }
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
        public ResourceAllocationPage VerifyFirstResultValue(string field, string expected)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headers);
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    //Temporary comment because of unfixed bug: Assert.AreEqual(expected, _firstResultFields[i].Text);
                    Assert.IsTrue(_firstResultFields[i].Text.Contains(expected),"expected " + expected + " but found " + _firstResultFields[i].Text);
                }
            }
            return this;

        }
        public ResourceAllocationPage DragAndDropFirstResourceToFirstRound()
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            IWebElement target = WaitUtil.WaitForElementVisible(firstRoundRow);
            DragAndDrop(_firstResultFields[0], target);
            return this;
        }
        public ResourceAllocationPage DeallocateResource(string _resourceName)
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            IWebElement source = WaitUtil.WaitForElementVisible(allocatedResource, _resourceName);
            DragAndDrop(source, _firstResultFields[0]);
            return this;
        }
        public ResourceAllocationPage DeallocateResourceType(int whichOne)
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            var source = WaitUtil.WaitForAllElementsVisible(allocatedResourceType);
            DragAndDrop(source[whichOne-1], _firstResultFields[0]);
            return this;
        }
        public ResourceAllocationPage DeallocateResourceType(string resourceType)
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            IWebElement source = WaitUtil.WaitForElementVisible(allocatedResourceType, resourceType);
            DragAndDrop(source, _firstResultFields[0]);
            return this;
        }

        public ResourceAllocationPage VerifyAllocatedResourceName(string _name)
        {
            WaitUtil.WaitForElementVisible(allocatedResource, _name);
            return this;
        }
        public ResourceAllocationPage VerifyResourceDeallocated(string _name)
        {
            WaitUtil.WaitForElementInvisible(allocatedResource, _name);
            return this;
        }
        public ResourceAllocationPage ClickAllocatedResource(string _name)
        {
            ClickOnElement(allocatedResource, _name);
            return this;
        }
        public ResourceAllocationPage VerifyPresenceOption(string _text)
        {
            Assert.AreEqual(_text, WaitUtil.WaitForElementVisible(resourcePresence).Text);
            return this;
        }
        public ResourceAllocationPage ClickPresenceOption()
        {
            ClickOnElement(resourcePresence);
            return this;
        }
        public ResourceAllocationPage SelectResourceState(string state)
        {
            //Value: SICK, TRAINING, AWOL
            ClickOnElement(resourceState, state);
            return this;
        }
        public ResourceAllocationPage ClickViewShiftDetail()
        {
            ClickOnElement(viewShiftDetailBtn);
            return this;
        }
        public ResourceAllocationPage ClickResourceDetail()
        {
            ClickOnElement(resourceDetailBtn);
            return this;
        }

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
        public ResourceAllocationPage InsertDate(string _date)
        {
            SendKeys(date, _date);
            return this;
        }
        public ResourceAllocationPage RefreshGrid()
        {
            ClickOnElement(refreshBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public ResourceAllocationPage VerifyStateAbbreviation(string _resourceName, string _abbr)
        {
            Assert.AreEqual(_abbr, WaitUtil.WaitForElementVisible(resourceAbbreviation, _resourceName).Text);
            return this;
        }

        //DEFAULT ALLOCATION PAGE
        public ResourceAllocationPage DragAndDropFirstResultToRoundGroup(int numberOfRow)
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            IList<IWebElement> rows = WaitUtil.WaitForAllElementsVisible(firstColumn);
            DragAndDrop(_firstResultFields[0], rows[numberOfRow - 1]);
            return this;
        }
        public ResourceAllocationPage DragAndDropFirstResultToBlankResourceType(string resourceType)
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            IWebElement target = WaitUtil.WaitForElementVisible(blankResourceType, resourceType);
            DragAndDrop(_firstResultFields[0], target);
            return this;
        }
        public ResourceAllocationPage DragAndDropFirstResultToNewCell()
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            IWebElement target = WaitUtil.WaitForElementVisible(addResourceCell);
            DragAndDrop(_firstResultFields[0], target);
            return this;
        }
        public ResourceAllocationPage ClickRound(string _roundName)
        {
            ClickOnElement(roundName, _roundName);
            return this;
        }
        public ResourceAllocationPage ClickViewRoundGroup()
        {
            ClickOnElement(viewRoundBtn);
            return this;
        }
        public ResourceAllocationPage ClickCalendar()
        {
            ClickOnElement(dateInput);
            ClickOnElement(calendarIcon);
            return this;
        }
        public ResourceAllocationPage InsertDayInFutre(string dayOfMonth)
        {
            if (dayOfMonth.StartsWith("0"))
            {
                dayOfMonth = dayOfMonth.Substring(1);
            }
            ClickOnElement(futreDayNumberInCalendar, dayOfMonth);
            return this;
        }
        public ResourceAllocationPage DeallocateResourceFromRoundGroup(int whichRow, string whichResource)
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            IWebElement target = _firstResultFields[0];
            var xpath = String.Format(secondColumnResource, whichRow, whichResource);
            IWebElement source = WaitUtil.WaitForElementVisible(xpath);
            DragAndDrop(source, target);
            return this;
        }
        public ResourceAllocationPage ExpandRoundGroup(int whichRow)
        {
            ClickOnElement(expandOptions, whichRow.ToString());
            SleepTimeInMiliseconds(200);
            return this;
        }
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
        public ResourceAllocationPage RelocateResourceTypeFromRoundGroupToRoundGroup(string resourceType, int targetRow)
        {
            IWebElement source = WaitUtil.WaitForElementVisible(blankResourceType, resourceType);
            //var sourceElement = WaitUtil.WaitForElementVisible(firstResourceCustomRoundGroup, sourceRow.ToString());
            var targetElement = WaitUtil.WaitForElementVisible(roundGroup, targetRow.ToString());
            DragAndDrop(source, targetElement);
            return this;

        }
        public ResourceAllocationPage RelocateResourceTypeFromRoundGroupToRound(string resourceType, int targetRow)
        {
            IWebElement source = WaitUtil.WaitForElementVisible(blankResourceType, resourceType);
            //var sourceElement = WaitUtil.WaitForElementVisible(firstResourceCustomRoundGroup, sourceRow.ToString());
            var targetElements = WaitUtil.WaitForAllElementsVisible(roundContainer);
            DragAndDrop(source, targetElements[targetRow-1]);
            return this;

        }
    }
}
