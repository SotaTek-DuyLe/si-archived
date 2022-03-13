using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Agrrements
{
    public class AgreementTaskPage : BasePage
    {
        private readonly By detailsTab = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By taskLinesTab = By.XPath("//a[@aria-controls='taskLines-tab']");
        private readonly By saveBtn = By.XPath("//button[@title='Save']");
        private readonly By closeWithoutSavingBtn = By.XPath("//button[@title='Close Without Saving']");

        //Task Lines Locator 
        private static string typeColumn = "//th[text()='Type']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Type']/preceding-sibling::th) + boolean(//th[text()='Type'])]";
        private static string typeSelected = typeColumn + "//option[@value='2']";
        private static string assetTypeColumn = "//th[text()='Asset Type']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Asset Type']/preceding-sibling::th) + boolean(//th[text()='Asset Type'])]";
        private static string assetTypeSelected = assetTypeColumn + "//option[@selected='true']";
        private static string scheduleAssertQuantityInput = "//th[text()='Scheduled Asset Qty']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Scheduled Asset Qty']/preceding-sibling::th) + boolean(//th[text()='Scheduled Asset Qty'])]//input";
        private static string productColumn = "//th[text()='Product']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Product']/preceding-sibling::th) + boolean(//th[text()='Product'])]";
        private static string productSelected = productColumn + "//option[@selected='true']";
        private static string scheduleProductQuantityColumn = "//th[text()='Scheduled Product Quantity']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Scheduled Product Quantity']/preceding-sibling::th) + boolean(//th[text()='Scheduled Product Quantity'])]";
        //private static string scheduleProductQuantityInput = scheduleProductQuantityColumn + "//input";
        private static string unitColumn = "//th[text()='Unit']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Unit']/preceding-sibling::th) + boolean(//th[text()='Unit'])]";
        private static string unitSelected = unitColumn + "//option[@selected='true']";
        private static string stateColumn = "//th[text()='State']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='State']/preceding-sibling::th) + boolean(//th[text()='State'])]";
        private static string stateCompleted = stateColumn + "//option[text()='Completed']";

        private static string actualAssetQuantityText = "//th[text()='Actual Asset Quantity']";
        private static string actualAssetQuantityInput = "//th[text()='Actual Asset Quantity']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Actual Asset Quantity']/preceding-sibling::th) + boolean(//th[text()='Actual Asset Quantity'])]//input";

        public AgreementTaskPage IsOnAgreementTaskPage()
        {
            WaitUtil.WaitForElementVisible(detailsTab);
            Assert.IsTrue(IsControlDisplayed(detailsTab));
            Assert.IsTrue(!IsControlDisplayed(taskLinesTab));
            return this;
        }

        public AgreementTaskPage CLickOnSaveBtn()
        {
            ClickOnElement(saveBtn);
            return this;
        }

        public AgreementTaskPage ClickCloseWithoutSaving()
        {
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }

        public AgreementTaskPage ClickToTaskLinesTab()
        {
            ClickOnElement(taskLinesTab);
            return this;
        }

        public AgreementTaskPage VerifyTaskLine(string type, string assetType, string scheduleAssetQty, string product, string productAssetQty, string unit, string state)
        {
            WaitUtil.WaitForElementVisible(typeSelected);
            Assert.AreEqual(GetElementText(typeSelected), type);
            Assert.AreEqual(GetElementText(assetTypeSelected), assetType);
            Assert.AreEqual(GetElementText(productSelected), product);
            Assert.AreEqual(GetElementText(unitSelected), unit);
            //other field with input locator then cannot get the value to verify
            return this;
        }

        public AgreementTaskPage InputActuaAssetQuantity(int i)
        {
            WaitUtil.WaitForElementVisible(actualAssetQuantityInput);
            EditSendKeys(actualAssetQuantityInput, i.ToString());
            return this;
        }
        public AgreementTaskPage ClickOnAcualAssetQuantityText()
        {
            ClickOnElement(actualAssetQuantityText);
            return this;
        }
        public AgreementTaskPage SelectCompletedState()
        {
            ClickOnElement(stateColumn);
            ClickOnElement(stateCompleted);
            return this;
        }

    }
}
