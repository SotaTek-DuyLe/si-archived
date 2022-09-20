using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages
{
    public class TaskAllocationPage : BasePage
    {
        private readonly By contractSelect = By.Id("contract");
        private readonly By servicesInput = By.Id("services");
        private readonly By goBtn = By.Id("button-go");
        private readonly string serviceOption = "//a[contains(@class,'jstree-anchor') and text()='{0}']";
        private readonly string serviceExpandIcon = "//a[contains(@class,'jstree-anchor') and text()='{0}']/preceding-sibling::i";

        //unallocated
        private readonly string firstCheckBox = "//div[@id='unallocated']//div[@class='ui-widget-content slick-row even']/div";
        private readonly string firstRow = "//div[@id='unallocated']//div[@class='ui-widget-content slick-row even']";

        //round grid
        private readonly string firstOptionInRoundGrid = "//div[@id='roundGrid']//div[@class='ui-widget-content slick-row even']/div[4]";


        [AllureStep]
        public TaskAllocationPage Input(string contract, string service)
        {
            SelectTextFromDropDown(contractSelect, contract);
            ClickOnElement(servicesInput);
            ClickOnElement(serviceExpandIcon, "Collections");
            ClickOnElement(serviceOption, service);
            ClickOnElement(goBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public TaskAllocationPage DragAnDropTest()
        {
            ClickOnElement(firstCheckBox);
            //SwitchNewIFrame();
            IWebElement source = GetElement(firstRow);
            IWebElement target = GetElement(firstOptionInRoundGrid);
            AlternativeDragAndDrop(source, target);
            return this;
        }
    }
}
