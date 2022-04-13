﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask
{
    //Details page for a task inside Agreement
    public class AgreementTaskDetailsPage : BasePage
    {
        private readonly By detailsTab = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By taskLinesTab = By.XPath("//a[@aria-controls='taskLines-tab']");
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
        private static string assetTypeColumn = "//th[text()='Asset Type']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Asset Type']/preceding-sibling::th) + boolean(//th[text()='Asset Type'])]";
        private static string assetTypeSelected = assetTypeColumn + "//option[@selected='true']";
        private static string productColumn = "//th[text()='Product']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Product']/preceding-sibling::th) + boolean(//th[text()='Product'])]";
        private static string productSelected = productColumn + "//option[@selected='true']";
        private static string unitColumn = "//th[text()='Unit']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Unit']/preceding-sibling::th) + boolean(//th[text()='Unit'])]";
        private static string unitSelected = unitColumn + "//option[@selected='true']";
        private static string stateColumn = "//th[text()='State']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='State']/preceding-sibling::th) + boolean(//th[text()='State'])]";
        private static string stateCompleted = stateColumn + "//option[text()='Completed']";
        private static string stateSelected = stateColumn + "//option[@value='{0}']";
        private static By state = By.Id("itemState.id");
        private readonly By scheduleAssetQty = By.Id("scheduledAssetQuantity.id");
        private readonly By scheduleProductQty = By.Id("scheduledProductQuantity.id");

        private static string actualAssetQuantityText = "//th[text()='Actual Asset Quantity']";
        private static string actualAssetQuantityInput = "//th[text()='Actual Asset Quantity']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Actual Asset Quantity']/preceding-sibling::th) + boolean(//th[text()='Actual Asset Quantity'])]//input";

        public AgreementTaskDetailsPage IsOnAgreementTaskPage()
        {
            WaitUtil.WaitForElementVisible(detailsTab);
            Assert.IsTrue(IsControlDisplayed(detailsTab));
            Assert.IsTrue(!IsControlDisplayed(taskLinesTab));
            return this;
        }

        public AgreementTaskDetailsPage CLickOnSaveBtn()
        {
            ClickOnElement(saveBtn);
            return this;
        }

        public AgreementTaskDetailsPage ClickCloseWithoutSaving()
        {
            WaitForLoadingIconToDisappear();
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }

        //Details Tab
        public AgreementTaskDetailsPage ClickToDetailsTab() {
            ClickOnElement(detailsTab);
            WaitForLoadingIconToDisappear();
            return this;

        }

        public AgreementTaskDetailsPage ClickStateDetais()
        {
            ClickOnElement(detailTaskState);
            Thread.Sleep(1000);
            return this;
        }
        public AgreementTaskDetailsPage ChooseTaskState(string status)
        {
            
            ClickOnElement(detailsTaskStateOption, status);
            Thread.Sleep(1000);
            return this;
        }
        //Task Line tab
        public AgreementTaskDetailsPage ClickToTaskLinesTab()
        {
            ClickOnElement(taskLinesTab);
            return this;
        }

        public AgreementTaskDetailsPage VerifyTaskLine(string _type, string assetType, string _scheduleAssetQty, string product, string productAssetQty, string unit, string _state)
        {
            WaitUtil.WaitForElementVisible(type);
            String valueSelected = GetAttributeValue(type, "value");
            String typeValueSelected = String.Format(typeSelectedValue, valueSelected);
            Assert.AreEqual(GetElementText(typeValueSelected), _type);
            Assert.AreEqual(GetElementText(assetTypeSelected), assetType);
            Assert.AreEqual(GetElementText(productSelected), product);
            Assert.AreEqual(GetElementText(unitSelected), unit);
            Assert.AreEqual(GetAttributeValue(scheduleAssetQty, "value"), _scheduleAssetQty);
            Assert.AreEqual(GetAttributeValue(scheduleProductQty, "value"), productAssetQty);
            String stateValue = GetAttributeValue(state, "value");
            String stateValueSelected = String.Format(stateSelected, stateValue);
            Assert.AreEqual(GetElementText(stateValueSelected), _state);
            return this;
        }
        
        public AgreementTaskDetailsPage VerifyTaskLine1(string type, string assetType, string scheduleAssetQty, string product, string productAssetQty, string unit, string state)
        {
            WaitUtil.WaitForElementVisible(assetTypeSelected);
            Assert.AreEqual(GetElementText(assetTypeSelected), assetType);
            Assert.AreEqual(GetElementText(productSelected), product);
            Assert.AreEqual(GetElementText(unitSelected), unit);
            //other field with input locator then cannot get the value to verify
            return this;
        }

        public AgreementTaskDetailsPage InputActuaAssetQuantity(int i)
        {
            EditSendKeys(actualAssetQuantityInput, i.ToString());
            return this;
        }
        public AgreementTaskDetailsPage ClickOnAcualAssetQuantityText()
        {
            ClickOnElement(actualAssetQuantityText);
            return this;
        }
        public AgreementTaskDetailsPage SelectCompletedState()
        {
            Thread.Sleep(500);
            ClickOnElement(stateColumn);
            Thread.Sleep(1000);
            ClickOnElement(stateCompleted);
            Thread.Sleep(1000);
            return this;
        }

    }
}
