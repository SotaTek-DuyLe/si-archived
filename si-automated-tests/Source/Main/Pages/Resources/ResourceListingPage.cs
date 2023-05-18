using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class ResourceListingPage : BasePageCommonActions
    {
        private readonly By BusinessUnitGroupHeader = By.XPath("//div[contains(@class, 'slick-header-column')]//span[text()='Business Unit Group']");
        private readonly By ClientReferenceHeader = By.XPath("//div[contains(@class, 'slick-header-column')]//span[text()='Client Reference']");
        public readonly By BusinessUnitGroupHeaderInput = By.XPath("//div[@class='ui-state-default slick-headerrow-column l8 r8']//input");
        public readonly By ClientReferenceHeaderInput = By.XPath("//div[@class='ui-state-default slick-headerrow-column l7 r7']//input");
        public readonly By ClearFilterButton = By.XPath("//button[@title='Clear Filters']");

        private readonly string ResourceTable = "//div[@class='grid-canvas']";
        private readonly string ResourceRow = "./div[contains(@class, 'slick-row')]";
        private readonly string ClientReferenceCell = "./div[@class='slick-cell l7 r7']";
        private readonly string BusinessUnitGroupCell = "./div[@class='slick-cell l8 r8']";

        public TableElement ResourceTableEle
        {
            get => new TableElement(ResourceTable, ResourceRow, new System.Collections.Generic.List<string>() { ClientReferenceCell, BusinessUnitGroupCell });
        }

        [AllureStep]
        public ResourceListingPage VerifyBusinessUnitGroupHeaderVisible()
        {
            VerifyElementVisibility(BusinessUnitGroupHeader, true);
            return this;
        }

        [AllureStep]
        public ResourceListingPage VerifyClientReferenceVisible()
        {
            VerifyElementVisibility(ClientReferenceHeader, true);
            return this;
        }

        [AllureStep]
        public ResourceListingPage VerifyBusinessUnitGroupColumn(string businessUnit)
        {
            int count = ResourceTableEle.GetRows().Count;
            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(ResourceTableEle.GetCellValue(i, ResourceTableEle.GetCellIndex(BusinessUnitGroupCell)).AsString().Contains(businessUnit));
            }
            return this;
        }

        [AllureStep]
        public ResourceListingPage VerifyClientReferenceColumn(string clientRef)
        {
            int count = ResourceTableEle.GetRows().Count;
            for (int i = 0; i < count; i++)
            {
                Assert.IsTrue(ResourceTableEle.GetCellValue(i, ResourceTableEle.GetCellIndex(ClientReferenceCell)).AsString().Contains(clientRef));
            }
            return this;
        }
    }
}
