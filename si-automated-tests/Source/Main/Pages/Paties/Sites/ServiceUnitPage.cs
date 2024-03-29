﻿using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
        public readonly By RiskTab = By.XPath("//a[@aria-controls='risks-tab']");
        public readonly By MapTab = By.XPath("//a[@aria-controls='map-tab']");
        public readonly By ServiceUnitPointTab = By.XPath("//a[@aria-controls='serviceUnitPoints-tab']");
        public readonly By LockCheckbox = By.XPath("//input[contains(@data-bind, 'locked.id')]");
        public readonly By LockReferenceInput = By.XPath("//input[@id='lockReference.id']");
        public readonly By LockHelpButton = By.XPath("//span[contains(@class, 'lock-help')]");
        public readonly By LockHelpContent = By.XPath("//div[contains(@class, 'popover-content')]");
        public readonly By ClientReferenceInput = By.XPath("//input[@name='clientReference']");
        public readonly By ServiceUnitTitle = By.XPath("//h5[@data-bind='text: serviceUnit']");
        #region ServiceUnitPoint
        public readonly By AddPointButton = By.XPath("//button[@data-target='#add-service-unit-points']");
        public readonly By AddServiceUnitButton = By.XPath("//button[text()='Add Service Unit Points']");
        public readonly By SearchButton = By.XPath("//button[text()='Search']");
        public readonly By StreetInput = By.XPath("//div[@id='street']//input[@data-bind=\"textInput: filter, valueUpdate: 'keypress'\"]");
        public readonly By StreetAutoCompleteTextBox = By.XPath("//div[@id='street']//ul[@class='autocomplete list-group']");
        public readonly By NodeRadio = By.XPath("//input[@type='radio' and @value='Node']/parent::label");
        public readonly By SectorSelect = By.XPath("//select[contains(@data-bind, 'sector ')]");
        public readonly By ClientRefInput = By.XPath("//input[@id='client-reference']");
        private string SearchResultTable = "//div[@id='results-grid']//div[contains(@data-bind, 'selectedType() === \"Node\"')]//div[@class='grid-canvas']";
        private string SearchResultRow = "./div[contains(@class, 'slick-row')]";
        private string CheckboxCell = "./div[contains(@class, 'l0')]//input[@type='checkbox']";
        private string PointNodeIdCell = "./div[contains(@class, 'l1')]";
        private string NodeCell = "./div[contains(@class, 'l2')]";
        private string ClientRefCell = "./div[contains(@class, 'l3')]";
        private string StreetCell = "./div[contains(@class, 'l4')]";
        private string ServiceUnitCell = "./div[contains(@class, 'l5')]";

        public TableElement SearchResultTableEle
        {
            get => new TableElement(SearchResultTable, SearchResultRow, new List<string>() { CheckboxCell, PointNodeIdCell, NodeCell, ClientRefCell, StreetCell, ServiceUnitCell });
        }

        private string ServiceUnitPointTable = "//div[@id='serviceUnitPoints-tab']//table//tbody";
        private string ServiceUnitPointRow = "./tr";
        private string ServiceUnitPointIdCell = "./td[@data-bind='text: id.value']";
        private string PointIdCell = "./td[@data-bind='text: pointId.value']";
        private string DescriptionCell = "./td//a[contains(@data-bind, 'point.value')]";
        private string ServiceUnitPointTypeCell = "./td//select[@id='serviceUnitPointType.id']";

        public TableElement ServiceUnitPointTableEle
        {
            get => new TableElement(ServiceUnitPointTable, ServiceUnitPointRow, new List<string>() { ServiceUnitPointIdCell, PointIdCell, DescriptionCell, ServiceUnitPointTypeCell });
        }

        [AllureStep]
        public ServiceUnitPage VerifyPointIdOnServiceUnitPointList(string pointId)
        {
            var cell = ServiceUnitPointTableEle.GetRowByCellValue(1, pointId);
            Assert.IsNotNull(cell);
            return this;
        }

        [AllureStep]
        public ServiceUnitPage VerifyServiceUnitType(string type)
        {
            VerifyCellValue(ServiceUnitPointTableEle, 0, 3, type);
            return this;
        }

        [AllureStep]
        public ServiceUnitPage VerifySearchResult(string pointnodeId)
        {
            var cell = SearchResultTableEle.GetRowByCellValue(SearchResultTableEle.GetCellIndex(PointNodeIdCell), pointnodeId);
            Assert.IsNotNull(cell);
            return this;
        }

        [AllureStep]
        public ServiceUnitPage ClickPointAddress(string pointAddress)
        {
            ServiceUnitPointTableEle.ClickCellOnCellValue(ServiceUnitPointTableEle.GetCellIndex(DescriptionCell), ServiceUnitPointTableEle.GetCellIndex(DescriptionCell), pointAddress);
            return this;
        }

        [AllureStep]
        public ServiceUnitPage CheckSearchResult(string pointnodeId)
        {
            SearchResultTableEle.ClickCellOnCellValue(0, SearchResultTableEle.GetCellIndex(PointNodeIdCell), pointnodeId);
            return this;
        }

        [AllureStep]
        public ServiceUnitPage SelectServiceUnitPointType(string type)
        {
            ServiceUnitPointTableEle.SetCellValue(0, 3, type);
            return this;
        }

        [AllureStep]
        public ServiceUnitPage DoubleClickServiceUnitPoint()
        {
            ServiceUnitPointTableEle.DoubleClickRow(0);
            return this;
        }
        #endregion

        #region Map
        private readonly By BluePoint = By.XPath("(//div[@id='map-tab']//img[@src='https://maps.gstatic.com/mapfiles/transparent.png'])[1]//parent::div");
        private readonly By RedPoint = By.XPath("(//div[@id='map-tab']//img[@src='https://maps.gstatic.com/mapfiles/transparent.png'])[2]//parent::div");

        public ServiceUnitPage DragBluePointToAnotherPosition()
        {
            var builder = new Actions(IWebDriverManager.GetDriver());
            var source = GetElement(BluePoint);
            builder.ClickAndHold(source)
                .MoveByOffset(100, 0)
                .Release(source)
                .Build()
                .Perform();
            return this;
        }

        public ServiceUnitPage VerifyBlueAndRedPointVisible()
        {
            VerifyElementVisibility(BluePoint, true);
            VerifyElementVisibility(RedPoint, true);
            return this;
        }
        #endregion

        #region Risk
        public readonly By RiskIframe = By.XPath("//div[@id='risks-tab']//iframe");
        public readonly By BulkCreateButton = By.XPath("//button[@title='Add risk register(s)']");
        private readonly string riskTable = "//div[@id='risk-grid']//div[@class='grid-canvas']";
        private readonly string riskRow = "./div[contains(@class,'slick-row')]";
        private readonly string riskCheckboxCell = "./div[@class='slick-cell l0 r0']//input";
        private readonly string riskNameCell = "./div[@class='slick-cell l2 r2']";
        private readonly string riskStartDateCell = "./div[@class='slick-cell l9 r9']";
        private readonly string riskEndDateCell = "./div[@class='slick-cell l10 r10']";
        public TableElement RiskTableEle
        {
            get => new TableElement(riskTable, riskRow, new List<string>() { riskCheckboxCell, riskNameCell, riskStartDateCell, riskEndDateCell });
        }

        [AllureStep]
        public ServiceUnitPage VerifyRiskSelect(string riskName, string startdate, string endDate)
        {
            Assert.IsNotNull(RiskTableEle.GetCellByCellValues(0, new Dictionary<int, object>()
            {
                { RiskTableEle.GetCellIndex(riskNameCell), riskName },
                { RiskTableEle.GetCellIndex(riskStartDateCell), startdate },
                { RiskTableEle.GetCellIndex(riskEndDateCell), endDate },
            }));
            return this;
        }
        #endregion

        #region Service task schedule
        public readonly By ServiceTaskScheduleTab = By.XPath("//a[@aria-controls='serviceTaskSchedules-tab']");
        public readonly By AddServiceTaskButton = By.XPath("//div[@id='serviceTaskSchedules-tab']//button[contains(., 'Add New Item')]");
        public readonly By CommercialCollectionOpt = By.XPath("//div[@id='add-service-task']//li[text()='Commercial Collection']");
        public readonly By CreateSTButton = By.XPath("//div[@id='add-service-task']//button[text()='Create']");

        public ServiceUnitPage ClickEditNewServiceTask()
        {
            ClickOnElement("(//div[@id='serviceTaskSchedules-tab']//button[contains(., 'Edit Service Task')])[1]");
            return this;
        }
        #endregion
    }
}
