﻿using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceUnitDetailPage : BasePageCommonActions
    {
        public readonly By StartDateInput = By.XPath("//div[@id='details-tab']//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//div[@id='details-tab']//input[@id='endDate.id']");
        public readonly By ServiceUnitInput = By.XPath("//div[@id='details-tab']//input[contains(@data-bind, 'serviceUnit.value')]");
        public readonly By ClientReferenceInput = By.XPath("//input[contains(@data-bind, 'clientReference.value')]");
        public readonly By ColorInput = By.XPath("//input[contains(@data-bind, 'colour.id')]");
        public readonly By PointSegmentInput = By.XPath("//input[contains(@data-bind, 'pointSegment.value')]");
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

        public ServiceUnitDetailPage VerifyNewAnnouncement(string announcement, string type, string from, string to)
        {
            VerifyCellValue(AnnouncementTableEle, 0, 2, announcement);
            VerifyCellValue(AnnouncementTableEle, 0, 3, type);
            VerifyCellValue(AnnouncementTableEle, 0, 4, from);
            VerifyCellValue(AnnouncementTableEle, 0, 5, to);
            return this;
        }

        public ServiceUnitDetailPage ClickAnnouncementCheckbox(int rowIdx)
        {
            AnnouncementTableEle.ClickCell(rowIdx, 0);
            return this;
        }

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

        public ServiceUnitDetailPage VerifyNewRiskRegister(RiskRegisterModel riskRegisterModel)
        {
            Assert.IsNotNull(ReviewRiskTableEle.GetRowByCellValue(2, riskRegisterModel.Risk));
            Assert.IsNotNull(ReviewRiskTableEle.GetRowByCellValue(3, riskRegisterModel.Level));
            return this;
        }

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

        public ServiceUnitDetailPage EditServiceUnitPoint(int rowIdx, string type, string qualifier)
        {
            ServiceUnitPointTableEle.SetCellValue(rowIdx, 3, type);
            ServiceUnitPointTableEle.SetCellValue(rowIdx, 4, qualifier);
            return this;
        }

        public ServiceUnitDetailPage DoubleClickServiceUnitPoint(int rowIdx)
        {
            ServiceUnitPointTableEle.DoubleClickRow(rowIdx);
            return this;
        }

        public ServiceUnitDetailPage VerifyStartDateAndEndDateIsReadOnly()
        {
            Assert.IsFalse(ServiceUnitPointTableEle.GetCellEnable(0, 5));
            Assert.IsFalse(ServiceUnitPointTableEle.GetCellEnable(0, 6));
            return this;
        }

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

        public ServiceUnitDetailPage VerifyAssetAddedByAddExistItemButton(string assetType)
        {
            VerifyCellValue(AssetTableEle, 0, 3, assetType);
            return this;
        }

        public ServiceUnitDetailPage ClickAssetCheckBox(int rowIdx)
        {
            AssetTableEle.ClickCell(rowIdx, 0);
            return this;
        }
    }
}
