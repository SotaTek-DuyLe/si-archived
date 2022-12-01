using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using System;
using System.Collections.Generic;
using System.Text;


namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class SectorGroupDetailPage : BasePageCommonActions
    {
        public readonly By SectorGroupInput = By.XPath("//input[contains(@data-bind, 'sectorGroup')]");
        public readonly By SectorGroupTypeSelect = By.XPath("//select[@id='sector-group-type']");
        public readonly By SectorSelect = By.XPath("//select[@id='sector']");
        public readonly By NextButton = By.XPath("//button[text()='Next']");
        public readonly By PreviousButton = By.XPath("//button[text()='Previous']");
        public readonly By FinishButton = By.XPath("//button[text()='Finish']");
        public readonly By AddServiceButton = By.XPath("//button[text()='Add']");
        public readonly By RemoveServiceButton = By.XPath("//button[text()='Remove']");
        public readonly By SectorNameInput = By.XPath("//input[@id='sector-name']");
        public readonly By SectorTypeSelect = By.XPath("//select[@id='sector-type']");

        private ServiceTreeViewElement _treeViewElement = new ServiceTreeViewElement("//div[@id='service-box'][1]", "./li", "./span[@data-bind='text: name']", "./ul", "./img[@alt='Expand']");
        private ServiceTreeViewElement ServicesTreeView
        {
            get => _treeViewElement;
        }

        [AllureStep]
        public SectorGroupDetailPage SelectNode(string nodeName)
        {
            ServicesTreeView.ClickItem(nodeName);
            return this;
        }
        [AllureStep]
        public SectorGroupDetailPage ExpandNode(string nodeName)
        {
            ServicesTreeView.ExpandNode(nodeName);
            return this;
        }
    }
}
