﻿using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceUnitDetailPage : BasePageCommonActions
    {
        public readonly By StartDateInput = By.XPath("//div[@id='details-tab']//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//div[@id='details-tab']//input[@id='endDate.id']");
        public readonly By ServiceUnitInput = By.XPath("//div[@id='details-tab']//input[@name='serviceUnit']");
        public readonly By ClientReferenceInput = By.XPath("//div[@id='details-tab']//input[@name='clientReference']");
        public readonly By ColorInput = By.XPath("//div[@id='details-tab']//input[@name='colour']");
        public readonly By PointSegmentInput = By.CssSelector("input[name='pointSegment']");
        public readonly By StreetInput = By.XPath("//input[contains(@data-bind, 'street.value')]");
        public readonly By ServiceLevelSelect = By.XPath("//select[@id='serviceLevel.id']");
        public readonly By NoteInput = By.XPath("//input[@id='3']");
        public readonly By AccessPointInput = By.XPath("//input[@id='4']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        public readonly By DataTab = By.XPath("//a[@aria-controls='data-tab']");
        public readonly By AssetsTab = By.XPath("//a[@aria-controls='assets-tab']");
        public readonly By ServiceTaskScheduleTab = By.XPath("//a[@aria-controls='serviceTaskSchedules-tab']");
        public readonly By ServiceUnitPointTab = By.XPath("//a[@aria-controls='serviceUnitPoints-tab']");
        public readonly By AddNewImageButton = By.XPath("//button[contains(text(), 'Add New')]");
        public readonly By AddNewItemButton = By.XPath("//div[@id='serviceTaskSchedules-tab']//button");
        public readonly By AddPointButton = By.XPath("//div[@id='serviceUnitPoints-tab']//button");
        public readonly By AddServiceUnitPointDiv = By.XPath("//div[@id='add-service-unit-points']");
        public readonly By AddServiceUnitPointCloseButton = By.XPath("//div[@id='add-service-unit-points']//button[@class='close']");
        public readonly By retireBtn = By.XPath("button[title='Retire']");

        #region
        public readonly By searchPointSegmentBtn = By.XPath("//div[@id='details-tab']//div[contains(@class,'searchButton')]/button");

        //Point Segment Search popup
        private readonly By titlePointSegmentSearch = By.XPath("//h4[text()='Point Segment Search']");
        private readonly By sectorsPointSegmentSearchDd = By.XPath("//label[contains(string(), 'Sectors')]/following-sibling::select");
        private readonly By streetSearchInput = By.CssSelector("div[id='searchFields.street']>input");
        private readonly By searchInPointSegmentSearchPopupBtn = By.XPath("//button[contains(@data-bind, 'enable: searchForm.canSubmit()')]");
        private readonly By pointSegmentsDd = By.XPath("//label[contains(string(), 'Point Segments')]/following-sibling::echo-select/select");
        private readonly By savePointSegmentSearchBtn = By.XPath("//button[contains(string(), 'Save') and contains(@data-bind, 'segmentsForm.canSubmit()')]");
        private readonly By refreshHeaderBtn = By.XPath("(//button[@title='Refresh'])[1]");
        private readonly By lockReferenceInput = By.CssSelector("input[name='lockReference']");
        private readonly By lockInput = By.XPath("//label[contains(string(), 'Lock')]/parent::span/parent::div/following-sibling::input");
        private readonly string anyStreetOption = "//div[@id='searchFields.street']//li[text()='{0}']";

        [AllureStep]
        public ServiceUnitDetailPage ClickSearchPointSegmentBtn()
        {
            ClickOnElement(searchPointSegmentBtn);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage IsPointSegmentSearchPopup(string sectorsValueExp)
        {
            WaitUtil.WaitForElementVisible(titlePointSegmentSearch);
            WaitUtil.WaitForElementVisible(sectorsPointSegmentSearchDd);
            Assert.AreEqual(sectorsValueExp, GetFirstSelectedItemInDropdown(sectorsPointSegmentSearchDd));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage SendKeyInStreetInput(string valueStreet)
        {
            SendKeys(streetSearchInput, valueStreet);
            //Select
            ClickOnElement(anyStreetOption, valueStreet);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickSearchPointSegment()
        {
            ClickOnElement(searchInPointSegmentSearchPopupBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public string GetValueInPointSegmentsDd()
        {
            SleepTimeInMiliseconds(1000);
            return GetFirstSelectedItemInDropdown(pointSegmentsDd);
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickSavePointSegmentSearchBtn()
        {
            ClickOnElement(savePointSegmentSearchBtn);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickRefreshHeaderBtn()
        {
            ClickOnElement(refreshHeaderBtn);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyValueInPointSegmentDetailTab(string pointSegmentExp)
        {
            Assert.IsTrue(pointSegmentExp.Contains(GetAttributeValue(PointSegmentInput, "value")));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyValueInStreetDetailTab(string streetExp)
        {
            Assert.IsTrue(streetExp.Contains(GetAttributeValue(StreetInput, "value")));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage SendKeyLockReferenceInput(string lockRefValue)
        {
            SendKeys(lockReferenceInput, lockRefValue);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage CheckLockInput()
        {
            ClickOnElement(lockInput);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyValueInServiceUnitAfterUpdating(string serviceUnitValueExp)
        {
            Assert.AreEqual(serviceUnitValueExp, GetAttributeValue(ServiceUnitInput, "value"));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyValueInClientRefAfterUpdating(string clientRefExp)
        {
            Assert.AreEqual(clientRefExp, GetAttributeValue(ClientReferenceInput, "value"));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyValueInColorAfterUpdating(string colorValueExp)
        {
            Assert.AreEqual(colorValueExp, GetAttributeValue(ColorInput, "value"));
            return this;
        }

        #endregion


        #region ServiceUnitPointTable
        public readonly string ServiceUnitPointTable = "//div[@id='serviceUnitPoints-tab']//table//tbody";
        public readonly string ServiceUnitPointRow = "./tr";
        public readonly string ServiceUnitPointIdCell = "./td[@data-bind='text: id.value']";
        public readonly string PointIdCell = "./td[@data-bind='text: pointId.value']";
        public readonly string DescriptionCell = "./td//a";
        public readonly string TypeCell = "./td//select[@id='serviceUnitPointType.id']";
        public readonly string QualifierCell = "./td//select[@id='serviceUnitPointQualifier.id']";
        public readonly string StartDateCell = "./td//input[@id='startDate.id']";
        public readonly string EndDateCell = "./td//input[@id='endDate.id']";
        public TableElement ServiceUnitPointTableEle
        {
            get => new TableElement(ServiceUnitPointTable, ServiceUnitPointRow, new List<string>() { ServiceUnitPointIdCell, PointIdCell, DescriptionCell, TypeCell, QualifierCell, StartDateCell, EndDateCell });
        }
        #endregion

        #region Assets Tab
        public readonly By AddNewAssetItemButton = By.XPath("//div[@id='assets-tab']//button[text()[contains(.,'Add New Item')]]");
        public readonly By AddExistAssetButton = By.XPath("//div[@id='assets-tab']//button[text()[contains(.,'Add Existing Asset')]]");
        public readonly By DeleteAssetItemButton = By.XPath("//div[@id='assets-tab']//button[text()[contains(.,'Delete Item')]]");
        public readonly string AssetTable = "//div[@id='assets-tab']//table//tbody";
        public readonly string AssetRow = "./tr";
        public readonly string AssetCheckboxCell = "./td//input[@type='checkbox']";
        public readonly string AssetidCell = "./td[@data-bind='text: id.value']";
        public readonly string AssetCell = "./td[@data-bind='text: asset.value']";
        public readonly string AssetTypeCell = "./td[@data-bind='text: assetTypeName.value']";
        public readonly string AssetStateCell = "./td[@data-bind='text: stateDesc.value']";
        public readonly string AssetProductCell = "./td[@data-bind='text: productName.value']";
        public readonly string AssetReferenceCell = "./td[@data-bind='text: assetReference.value']";

        public TableElement AssetTableEle
        {
            get => new TableElement(AssetTable, AssetRow, new List<string>() { AssetCheckboxCell , AssetidCell, AssetCell , AssetTypeCell, AssetStateCell, AssetProductCell, AssetReferenceCell });
        }

        public readonly By BinsSelect = By.XPath("//div[@id='add-existing-asset']//ul//li[1]//ul");
        public readonly By ConfirmButton = By.XPath("//div[@id='add-existing-asset']//button[text()='Confirm']");
        public readonly By MainAssetDropDownButton = By.XPath("//div[@id='add-existing-asset']//button[@data-id='mainAsset']");
        public readonly By MainAssetSelect = By.XPath("//ul[@class='dropdown-menu inner']");
        #endregion

        #region announcements-tab
        public readonly By AddNewAnnouncementItemButton = By.XPath("//div[@id='announcements-tab']//button[text()[contains(.,'Add New Item')]]");
        public readonly By DeleteAnnouncementItemButton = By.XPath("//div[@id='announcements-tab']//button[text()[contains(.,'Delete Item')]]");
        public readonly By AnnouncementTab = By.XPath("//a[@aria-controls='announcements-tab']");
        public readonly string AnnouncementTable = "//div[@id='announcements-tab']//div[@class='grid-canvas']";
        public readonly string AnnouncementRow = "./div[contains(@class, 'slick-row ')]";
        public readonly string AnnouncementCheckboxCell = "./div[contains(@class, 'l0')]//input";
        public readonly string AnnouncementIdCell = "./div[contains(@class, 'l1')]";
        public readonly string AnnouncementNameCell = "./div[contains(@class, 'l2')]";
        public readonly string AnnouncementTypeCell = "./div[contains(@class, 'l3')]";
        public readonly string AnnouncementValidFromCell = "./div[contains(@class, 'l4')]";
        public readonly string AnnouncementValidToCell = "./div[contains(@class, 'l5')]";

        public TableElement AnnouncementTableEle
        {
            get => new TableElement(AnnouncementTable, AnnouncementRow, new List<string>() { AnnouncementCheckboxCell , AnnouncementIdCell , AnnouncementNameCell , AnnouncementTypeCell , AnnouncementValidFromCell , AnnouncementValidToCell });
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyNewAnnouncement(string announcement, string type, string from, string to)
        {
            VerifyCellValue(AnnouncementTableEle, 0, 2, announcement);
            VerifyCellValue(AnnouncementTableEle, 0, 3, type);
            VerifyCellValue(AnnouncementTableEle, 0, 4, from);
            VerifyCellValue(AnnouncementTableEle, 0, 5, to);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickAnnouncementCheckbox(int rowIdx)
        {
            AnnouncementTableEle.ClickCell(rowIdx, 0);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyAnnouncementDeleted(string announcement)
        {
            Assert.IsNull(AnnouncementTableEle.GetRowByCellValue(2, announcement));
            return this;
        }
        #endregion

        #region Map tab
        public readonly By MapTab = By.XPath("//a[@aria-controls='map-tab']"); 
        public readonly By ResetMapButton = By.XPath("//div[@id='map-tab']//div[text()='Reset Map']"); 
        public readonly By SaveMapButton = By.XPath("//div[@id='map-tab']//div[text()='Save Map ']");
        #endregion

        #region Risk tab
        public readonly By RiskTab = By.XPath("//a[@aria-controls='risks-tab']");
        public readonly By RiskTabIframe = By.XPath("//iframe[@id='risks-tab']");
        public readonly By BulkCreateButton = By.XPath("//button[text()[contains(.,'Bulk Create')]]");
        public readonly By RetireButton = By.XPath("//button[text()[contains(.,'Retire')]]");
        public readonly By ShowAllButton = By.XPath("//button[text()[contains(.,'Show All')]]");
        public readonly By ShowActiveButton = By.XPath("//button[text()[contains(.,'Show Active')]]");

        public readonly string ReviewRiskTable = "//div[@id='risk-grid']//div[@class='grid-canvas']";
        public readonly string ReviewRiskRow = "./div[contains(@class, 'slick-row ')]";
        public readonly string RiskRegisterCheckboxCell = "./div[contains(@class, 'l0')]//input";
        public readonly string RiskRegisterIdCell = "./div[contains(@class, 'l1')]";
        public readonly string RiskRegisterRiskCell = "./div[contains(@class, 'l2')]";
        public readonly string RiskRegisterLevelCell = "./div[contains(@class, 'l3')]";
        public readonly string RiskRegisterTypeCell = "./div[contains(@class, 'l4')]";
        public readonly string RiskRegisterContractCell = "./div[contains(@class, 'l5')]";
        public readonly string RiskRegisterServiceCell = "./div[contains(@class, 'l6')]";
        public readonly string RiskRegisterTargetCell = "./div[contains(@class, 'l7')]";
        public readonly string RiskRegisterProximityCell = "./div[contains(@class, 'l8')]";
        public readonly string RiskRegisterStartDateCell = "./div[contains(@class, 'l9')]";
        public readonly string RiskRegisterEndDateCell = "./div[contains(@class, 'l10')]";

        public TableElement ReviewRiskTableEle
        {
            get => new TableElement(ReviewRiskTable, ReviewRiskRow, new List<string>() 
            {
                RiskRegisterCheckboxCell, RiskRegisterIdCell, RiskRegisterRiskCell, RiskRegisterLevelCell,
                RiskRegisterTypeCell, RiskRegisterContractCell, RiskRegisterServiceCell, RiskRegisterTargetCell, RiskRegisterProximityCell,
                RiskRegisterStartDateCell, RiskRegisterEndDateCell
            });
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyNewRiskRegister(RiskRegisterModel riskRegisterModel)
        {
            Assert.IsNotNull(ReviewRiskTableEle.GetRowByCellValue(2, riskRegisterModel.Risk));
            Assert.IsNotNull(ReviewRiskTableEle.GetRowByCellValue(3, riskRegisterModel.Level));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyRiskRegisterNotDisplayExpiredRecord()
        {
            List<object> endDates = ReviewRiskTableEle.GetColumnValues(10);
            foreach (var endDate in endDates)
            {
                Assert.IsTrue(CommonUtil.StringToDateTime(endDate.ToString(), CommonConstants.DATE_DD_MM_YYYY_HH_MM_FORMAT) > DateTime.Now);
            }
            return this;
        }
        #endregion

        #region subscriptions-tab
        public readonly By SubscriptionTab = By.XPath("//a[@aria-controls='subscriptions-tab']");
        public readonly By SubscriptionTabIframe = By.XPath("//iframe[@id='subscriptions-tab']");
        public readonly By AddNewSubscriptionItemButton = By.XPath("//button[text()[contains(.,'Add New Item')]]");
        public readonly By SubscriptionRefreshButton = By.XPath("//button[@title='Refresh']");
        public readonly string SubscriptionTable = "//div[@class='grid-canvas']";
        public readonly string SubscriptionRow = "./div[contains(@class, 'slick-row')]";
        public readonly string SubscriptionIdCell = "./div[contains(@class, 'l0')]";
        public readonly string SubscriptionContractIdCell = "./div[contains(@class, 'l1')]";
        public readonly string SubscriptionContractCell = "./div[contains(@class, 'l2')]";
        public readonly string SubscriptionMobileCell = "./div[contains(@class, 'l3')]";
        public readonly string SubscriptionStateCell = "./div[contains(@class, 'l4')]";
        public readonly string SubscriptionStartDateCell = "./div[contains(@class, 'l5')]";
        public readonly string SubscriptionEndDateCell = "./div[contains(@class, 'l6')]";
        public readonly string SubscriptionNoteCell = "./div[contains(@class, 'l7')]";
        public readonly string SubscriptionSubjectCell = "./div[contains(@class, 'l8')]";
        public readonly string SubscriptionDescriptionCell = "./div[contains(@class, 'l9')]";

        public TableElement SubscriptionTableEle
        {
            get => new TableElement(SubscriptionTable, SubscriptionRow, new List<string>()
            {
                SubscriptionIdCell, SubscriptionContractIdCell, SubscriptionContractCell,
                SubscriptionMobileCell, SubscriptionStateCell, SubscriptionStartDateCell,
                SubscriptionEndDateCell, SubscriptionNoteCell, SubscriptionSubjectCell, SubscriptionDescriptionCell
            });
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyNewSubscription(string contract, string mobile)
        {
            VerifyCellValue(SubscriptionTableEle, 0, 2, contract);
            VerifyCellValue(SubscriptionTableEle, 0, 3, mobile);
            return this;
        }
        #endregion

        #region notifications tab
        public readonly By NotificationTab = By.XPath("//a[@aria-controls='notifications-tab']");
        public readonly By Notificationiframe = By.XPath("//iframe[@id='notifications-tab']");
        public readonly By NotificationRefreshButton = By.XPath("//button[@title='Refresh']");
        #endregion

        #region indicator-tab
        public readonly By IndicatorTab = By.XPath("//a[@aria-controls='indicators-tab']");
        public readonly By IndicatorIframe = By.XPath("//iframe[@id='indicators-tab']");
        public readonly By IndicatorAddNewItemButton = By.XPath("//button[text()[contains(.,'Add New Item')]]");
        public readonly By SelectIndicatorButton = By.XPath("//button[@data-id='indicators']");
        public readonly By IndicatorUl = By.XPath("//ul[@aria-expanded='true']");
        public readonly By IndicatorConfirmButton = By.XPath("//button[contains(text(), 'Confirm')]");
        #endregion

        #region Service Unit Point tab
        public readonly By AddressRadio = By.XPath("//div[@id='add-service-unit-points']//input[@value='Address']");
        public readonly By SectorSelect = By.XPath("//div[@id='add-service-unit-points']//select");

        [AllureStep]
        public ServiceUnitDetailPage VerifyRadioIsSelected()
        {
            Assert.IsTrue(this.driver.FindElement(AddressRadio).GetAttribute("checked").Contains("true"));
            return this;
        }
        #endregion
        [AllureStep]
        public ServiceUnitDetailPage SelectRandomServiceLevel()
        {
            string selectedServiceLevel = GetFirstSelectedItemInDropdown(ServiceLevelSelect);
            List<string> selections = GetSelectDisplayValues(ServiceLevelSelect);
            int index = 0;
            while (true)
            {
                Random rnd = new Random();
                index = rnd.Next(selections.Count);
                if (selectedServiceLevel != selections[index])
                {
                    break;
                }
            }
            SelectTextFromDropDown(ServiceLevelSelect, selections[index]);
            return this;
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [AllureStep]
        public ServiceUnitDetailPage EditServiceUnitPoint(int rowIdx, string type, string qualifier)
        {
            ServiceUnitPointTableEle.SetCellValue(rowIdx, 3, type);
            ServiceUnitPointTableEle.SetCellValue(rowIdx, 4, qualifier);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage DoubleClickServiceUnitPoint(int rowIdx)
        {
            ServiceUnitPointTableEle.DoubleClickRow(rowIdx);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyStartDateAndEndDateIsReadOnly()
        {
            Assert.IsFalse(ServiceUnitPointTableEle.GetCellEnable(0, 5));
            Assert.IsFalse(ServiceUnitPointTableEle.GetCellEnable(0, 6));
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyAssetAddedByAddNewItemButton(string asset, string assetType, string state, string product, string reference)
        {
            int lastRowIdx = AssetTableEle.GetRows().Count - 1;
            VerifyCellValue(AssetTableEle, lastRowIdx, 2, asset);
            VerifyCellValue(AssetTableEle, lastRowIdx, 3, assetType);
            VerifyCellValue(AssetTableEle, lastRowIdx, 4, state);
            VerifyCellValue(AssetTableEle, lastRowIdx, 5, product);
            VerifyCellValue(AssetTableEle, lastRowIdx, 6, reference);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage VerifyAssetAddedByAddExistItemButton(string assetType)
        {
            VerifyCellValue(AssetTableEle, 0, 3, assetType);
            return this;
        }
        [AllureStep]
        public ServiceUnitDetailPage ClickAssetCheckBox(int rowIdx)
        {
            AssetTableEle.ClickCell(rowIdx, 0);
            return this;
        }


    }
}
