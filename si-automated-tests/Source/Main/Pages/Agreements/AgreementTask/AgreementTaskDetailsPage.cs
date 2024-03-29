﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Task;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask
{
    //Details page for a task inside Agreement
    public class AgreementTaskDetailsPage : BasePageCommonActions
    {
        private readonly By taskTypeURL = By.XPath("//a[@class='typeUrl']");
        private readonly By taskImage = By.XPath("//img[@src='/web/content/images/form/save.svg']");
        private string taskType = "//span[text()='Task']";
        private string taskTypeName = "//span[text()='Task']/following-sibling::span";

        private readonly By detailsTab = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By taskLinesTab = By.XPath("//a[@aria-controls='taskLines-tab']");
        private readonly By historyTab = By.XPath("//a[@aria-controls='history-tab']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By closeWithoutSavingBtn = By.XPath("//button[@title='Close Without Saving']");

        //Details Tab Locator 
        private readonly By detailTaskState = By.Id("taskState.id");
        private string detailsTaskStateOption = "//select[@name='taskState']//option[text()='{0}']";

        //Task Lines Locator 
        private static By type = By.Id("taskLineType.id");
        private static string typeColumn = "//th[text()='Type']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Type']/preceding-sibling::th) + boolean(//th[text()='Type'])]";
        private static string typeSelected = typeColumn + "//option[@value='2']";
        private static string typeSelectedValue = typeColumn + "//option[@value='{0}']";
        private static string assetTypeColumn = "//td[contains(@data-bind, 'assetType')]";
        private static string productColumn = "//td[contains(@data-bind, 'product')]";
        private static string unitColumn = "//echo-select[contains(@params,'unit')]/select";
        private static string stateColumn = "//select[@id='itemState.id']";
        private static string stateCompleted = stateColumn + "//option[text()='Completed']";
        private static string stateSelected = stateColumn + "//option[@value='{0}']";
        private static By state = By.Id("itemState.id");
        private readonly By scheduleAssetQty = By.Id("scheduledAssetQuantity.id");
        private readonly By scheduleProductQty = By.Id("scheduledProductQuantity.id");

        private static string actualAssetQuantityText = "//th[text()='Asset']";
        private static By actualAssetQuantityInput = By.Id("actualAssetQuantity.id");

        #region Task Line Table
        private string TaskLineTable = "//div[@id='taskLines-tab']//table//tbody";
        private string TaskLineRow = "./tr";
        private string OrderCell = "./td//input[@id='order.id']";
        private string TypeCell = "./td//select[@id='taskLineType.id']";
        private string AssetTypeCell = "./td//echo-select[contains(@params, 'assetType')]//select";
        private string AssetActualCell = "./td//input[@id='actualAssetQuantity.id']";
        private string AssetScheduleCell = "./td//input[@id='scheduledAssetQuantity.id']";
        private string ProductCell = "./td//echo-select[contains(@params, 'product')]//select";
        private string ProductActualCell = "./td//input[@id='actualProductQuantity.id']";
        private string ProductScheduleCell = "./td//input[@id='scheduledProductQuantity.id']";
        private string UnitCell = "./td//echo-select[contains(@params, 'unitOfMeasure')]//select";
        private string SiteDestinationCell = "./td//select[@id='destinationSite.id']";
        private string SiteProductCell = "./td//select[@id='siteProduct.id']";
        private string StateCell = "./td//select[@id='itemState.id']";
        private string ResolutionCodeCell = "./td//select[@id='resCode.id']";

        public TableElement TaskLineTableEle
        {
            get => new TableElement(TaskLineTable, TaskLineRow, new List<string>() 
            { 
                OrderCell, TypeCell, AssetTypeCell, 
                AssetActualCell, AssetScheduleCell, ProductCell, 
                ProductActualCell, ProductScheduleCell, UnitCell,
                SiteDestinationCell, SiteProductCell, StateCell, ResolutionCodeCell });
        }

        [AllureStep]
        public AgreementTaskDetailsPage DoubleClickTaskLine(int rowIdx = 0)
        {
            TaskLineTableEle.DoubleClickRow(rowIdx);
            return this;
        }

        [AllureStep]
        public AgreementTaskDetailsPage VerifyTaskLineProduct(int rowIdx, string value)
        {
            VerifyCellValue(TaskLineTableEle, rowIdx, 5, value);
            return this;
        }

        [AllureStep]
        public AgreementTaskDetailsPage VerifyTaskLineState(int rowIdx, string value)
        {
            VerifyCellValue(TaskLineTableEle, rowIdx, 11, value);
            return this;
        }
        #endregion
        public readonly By AddNewAgreementTaskDetailButton = By.XPath("//div[@id='taskLines-tab']//button[@title='Add New Item']");

        #region Task Details Header
        private readonly By OrderHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[1]//th[2]");
        private readonly By TypeHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[1]//th[3]");
        private readonly By AssetTypeHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[2]//th[1]");
        private readonly By AssetActualHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[2]//th[2]");
        private readonly By ProductHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[2]//th[3]");
        private readonly By ProductActualHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[2]//th[4]");
        private readonly By UnitHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[2]//th[5]");
        private readonly By DestinationHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[2]//th[6]");
        private readonly By SiteProductHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[2]//th[7]");
        private readonly By StateHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[1]//th[8]");
        private readonly By ResolutionCodeHeader = By.XPath("//div[@id='taskLines-tab']//table//thead//tr[1]//th[9]");

        [AllureStep]
        public AgreementTaskDetailsPage VerifyHeaderColumn()
        {
            VerifyElementVisibility(OrderHeader, true);
            VerifyElementVisibility(TypeHeader, true);
            VerifyElementVisibility(AssetTypeHeader, true);
            VerifyElementVisibility(AssetActualHeader, true);
            VerifyElementVisibility(ProductHeader, true);
            VerifyElementVisibility(ProductActualHeader, true);
            VerifyElementVisibility(UnitHeader, true);
            VerifyElementVisibility(DestinationHeader, true);
            VerifyElementVisibility(SiteProductHeader, true);
            VerifyElementVisibility(StateHeader, true);
            VerifyElementVisibility(ResolutionCodeHeader, true);
            return this;
        }
        #endregion

        [AllureStep]
        public AgreementTaskDetailsPage WaitingForTaskDetailsPageLoadedSuccessfully()
        {
            WaitUtil.WaitForElementVisible(taskTypeURL);
            WaitUtil.WaitForElementVisible(taskImage);
            WaitUtil.WaitForElementVisible(taskType);
            WaitUtil.WaitForElementVisible(taskTypeName);
            int i = 5;
            while (i > 0)
            {
                if (GetElementText(taskTypeName).Equals("") || GetElementText(taskTypeURL).Equals(""))
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else
                {
                    break;
                }
            }
            Assert.IsTrue(IsControlDisplayed(taskTypeURL));
            Assert.IsTrue(IsControlDisplayed(taskImage));
            Assert.IsTrue(IsControlDisplayed(taskType));
            Assert.IsTrue(IsControlDisplayed(taskTypeName));
            return this;
        }
        [AllureStep]
        public AgreementTaskDetailsPage IsOnAgreementTaskPage()
        {
            WaitUtil.WaitForElementVisible(detailsTab);
            Assert.IsTrue(IsControlDisplayed(detailsTab));
            Assert.IsTrue(!IsControlDisplayed(taskLinesTab));
            return this;
        }
        [AllureStep]
        public AgreementTaskDetailsPage CLickOnSaveBtn()
        {
            ClickOnElement(saveBtn);
            return this;
        }
        [AllureStep]
        public AgreementTaskDetailsPage ClickCloseWithoutSaving()
        {
            WaitForLoadingIconToDisappear();
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }

        //Details Tab
        [AllureStep]
        public AgreementTaskDetailsPage ClickToDetailsTab() {
            ClickOnElement(detailsTab);
            WaitForLoadingIconToDisappear();
            return this;

        }
        [AllureStep]

        public AgreementTaskDetailsPage ClickStateDetais()
        {
            ClickOnElement(detailTaskState);
            Thread.Sleep(1000);
            return this;
        }
        [AllureStep]
        public AgreementTaskDetailsPage ChooseTaskState(string status)
        {
            
            ClickOnElement(detailsTaskStateOption, status);
            Thread.Sleep(1000);
            return this;
        }
        //Task Line tab
        [AllureStep]
        public AgreementTaskDetailsPage ClickToTaskLinesTab()
        {
            ClickOnElement(taskLinesTab);
            return this;
        }
        [AllureStep]
        public AgreementTaskDetailsPage VerifyTaskLine(string _type, string _assetType, string _scheduleAssetQty, string product, string productAssetQty, string unit, string _state)
        {
            WaitUtil.WaitForElementVisible(type);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(type), _type);
            Assert.AreEqual(GetAttributeValue(assetTypeColumn, "title"), _assetType);
            Assert.AreEqual(GetAttributeValue(productColumn, "title"), product);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(unitColumn), unit);
            Assert.AreEqual(GetAttributeValue(scheduleAssetQty, "value"), _scheduleAssetQty);
            Assert.AreEqual(GetAttributeValue(scheduleProductQty, "value"), productAssetQty);
            Assert.AreEqual(GetFirstSelectedItemInDropdown(state), _state);
            return this;
        }
        [AllureStep]
        public AgreementTaskDetailsPage InputActuaAssetQuantity(int i)
        {
            EditSendKeys(actualAssetQuantityInput, i.ToString());
            return this;
        }
        [AllureStep]
        public AgreementTaskDetailsPage ClickOnAcualAssetQuantityText()
        {
            ClickOnElement(actualAssetQuantityText);
            return this;
        }
        [AllureStep]
        public AgreementTaskDetailsPage SelectCompletedState()
        {
            Thread.Sleep(500);
            ClickOnElement(stateColumn);
            Thread.Sleep(1000);
            ClickOnElement(stateCompleted);
            Thread.Sleep(1000);
            return this;
        }
        [AllureStep]
        public HistoryTab ClickHistoryTab()
        {
            ClickOnElement(historyTab);
            return new HistoryTab();
        }

    }
}
