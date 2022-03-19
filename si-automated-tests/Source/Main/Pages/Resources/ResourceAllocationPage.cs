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

        //Left panel
        private readonly By firstRoundRow = By.XPath("//tbody[contains(@data-bind,'roundMenu')]/tr");

        //Right panel
        private readonly By headers = By.XPath("//div[contains(@class,'active')]//div[@class='ui-state-default slick-header-column slick-header-sortable ui-sortable-handle']/span[1]");
        private readonly By inputBoxes = By.XPath("//div[contains(@class,'active')]//div[@class='slick-headerrow ui-state-default']//input");
        private readonly By firstResultFields = By.XPath("//div[contains(@class,'active')]//div[@class='ui-widget-content slick-row even'][1]/div");

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
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
                    Assert.AreEqual(expected, _firstResultFields[i].Text);
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
    }
}
