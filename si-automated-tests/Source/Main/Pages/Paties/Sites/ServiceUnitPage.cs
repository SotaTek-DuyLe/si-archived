﻿using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Paties.Sites
{
    public class ServiceUnitPage : BasePageCommonActions
    {
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By ServiceUnitPointTab = By.XPath("//a[@aria-controls='serviceUnitPoints-tab']");
        public readonly By LockCheckbox = By.XPath("//input[contains(@data-bind, 'locked.id')]");
        public readonly By LockReferenceInput = By.XPath("//input[@id='lockReference.id']");
        public readonly By LockHelpButton = By.XPath("//span[contains(@class, 'lock-help')]");
        public readonly By LockHelpContent = By.XPath("//div[contains(@class, 'popover-content')]");
        public readonly By ClientReferenceInput = By.XPath("//input[@name='clientReference']");

        #region ServiceUnitPoint
        public readonly By AddPointButton = By.XPath("//button[@data-target='#add-service-unit-points']");
        public readonly By AddServiceUnitButton = By.XPath("//button[text()='Add Service Unit Points']");
        public readonly By SearchButton = By.XPath("//button[text()='Search']");
        public readonly By NodeRadio = By.XPath("//input[@type='radio' and @value='Node']");
        public readonly By SectorSelect = By.XPath("//select[contains(@data-bind, 'sector ')]");
        public readonly By ClientRefInput = By.XPath("//input[@id='client-reference']");
        private string SearchResultTable = "//div[@id='results-grid']//div[contains(@data-bind, 'selectedType() === \"Node\"')]//div[@class='grid-canvas']";
        private string SearchResultRow = "./div[contains(@class, 'slick-row')]";
        private string CheckboxCell = "./div[contains(@class, 'l0')]//input[@type='checkbox']";
        private string IdCell = "./div[contains(@class, 'l1')]";
        private string PointNodeIdCell = "./div[contains(@class, 'l2')]";
        private string NodeCell = "./div[contains(@class, 'l3')]";
        private string ClientRefCell = "./div[contains(@class, 'l4')]";
        private string StreetCell = "./div[contains(@class, 'l5')]";
        private string ServiceUnitCell = "./div[contains(@class, 'l6')]";

        public TableElement SearchResultTableEle
        {
            get => new TableElement(SearchResultTable, SearchResultRow, new List<string>() { CheckboxCell, IdCell, PointNodeIdCell, NodeCell, ClientRefCell, StreetCell, ServiceUnitCell });
        }

        private string ServiceUnitPointTable = "//div[@id='serviceUnitPoints-tab']//table//tbody";
        private string ServiceUnitPointRow = "./tr";
        private string ServiceUnitPointIdCell = "./td[@data-bind='text: id.value']";
        private string PointIdCell = "./td[@data-bind='text: pointId.value']";
        public TableElement ServiceUnitPointTableEle
        {
            get => new TableElement(ServiceUnitPointTable, ServiceUnitPointRow, new List<string>() { ServiceUnitPointIdCell, PointIdCell });
        }

        public ServiceUnitPage VerifyPointIdOnServiceUnitPointList(string pointId)
        {
            var cell = ServiceUnitPointTableEle.GetRowByCellValue(1, pointId);
            Assert.IsNotNull(cell);
            return this;
        }

        public ServiceUnitPage VerifySearchResult(string pointnodeId)
        {
            var cell = SearchResultTableEle.GetRowByCellValue(2, pointnodeId);
            Assert.IsNotNull(cell);
            return this;
        }

        public ServiceUnitPage CheckSearchResult(string pointnodeId)
        {
            SearchResultTableEle.ClickCellOnCellValue(0, 2, pointnodeId);
            return this;
        }
        #endregion
    }
}
