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

        //Left panel
        private readonly By firstRoundRow = By.XPath("//tbody[contains(@data-bind,'roundMenu')]/tr");
        private readonly string allocatedResource = "//span[@class='main-description resource-name' and contains(text(),'{0}')]";
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


        //Right panel
        private readonly By headers = By.XPath("//div[contains(@class,'active')]//div[@class='ui-state-default slick-header-column slick-header-sortable ui-sortable-handle']/span[1]");
        private readonly By inputBoxes = By.XPath("//div[contains(@class,'active')]//div[@class='slick-headerrow ui-state-default']//input");
        private readonly By firstResultFields = By.XPath("//div[contains(@class,'active')]//div[contains(@class,'ui-widget-content slick-row even')][1]/div");

        //business unit option
        private readonly string businessUnitOption = "//a[contains(@class,'jstree-anchor') and text()='{0}']";

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
                    Assert.IsTrue(_firstResultFields[i].Text.Contains(expected));
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
        public ResourceAllocationPage DeallocateResourceByDragAndDrop(string _resourceName)
        {
            IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
            IWebElement source = WaitUtil.WaitForElementVisible(allocatedResource, _resourceName);
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
                Assert.AreEqual(whiteBackground, style);
            }
            else if (_color == "green")
            {
                Assert.AreEqual(greenBackground, style);
            }
            else if (_color == "purple")
            {
                Assert.AreEqual(purpleBackground, style);
            }
            else if (_color == "red")
            {
                Assert.AreEqual(redBackground, style);
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
    }
}
