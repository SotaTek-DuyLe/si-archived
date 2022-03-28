using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Task
{
    public class TaskLineTab : BasePage
    {
        private readonly By firstTaskType = By.Id("taskLineType.id");
        private readonly By firstTaskAssetType = By.XPath("//*[contains(@params,'assetType')]/select");
        private readonly By firstTaskQty = By.Id("scheduledAssetQuantity.id");
        private readonly By firstTaskProduct = By.XPath("//*[contains(@params,'product')]/select");
        private readonly By firstTaskUnit = By.XPath("//*[contains(@params,'unitOfMeasure')]/select");
        private readonly By firsTaskState = By.Id("itemState.id");
        private static string stateColumn = "//th[text()='State']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='State']/preceding-sibling::th) + boolean(//th[text()='State'])]";
        private static string stateCompleted = stateColumn + "//option[text()='Completed']";

        private static string actualAssetQuantityText = "//th[text()='Actual Asset Quantity']";
        private static string actualAssetQuantityInput = "//th[text()='Actual Asset Quantity']/ancestor::thead/following-sibling::tbody/tr[1]/td[count(//th[text()='Actual Asset Quantity']/preceding-sibling::th) + boolean(//th[text()='Actual Asset Quantity'])]//input";

        public TaskLineTab VerifyFirstTaskInfo(string type, string assetType, string product, string unit, string state)
        {
            Assert.AreEqual(type, GetFirstSelectedItemInDropdown(firstTaskType));
            Assert.AreEqual(assetType, GetFirstSelectedItemInDropdown(firstTaskAssetType));
            //Assert.AreEqual(qty, GetElementText(firstTaskQty));
            Assert.AreEqual(product, GetFirstSelectedItemInDropdown(firstTaskProduct));
            Assert.AreEqual(unit, GetFirstSelectedItemInDropdown(firstTaskUnit));
            Assert.AreEqual(state, GetFirstSelectedItemInDropdown(firsTaskState));
            return this;
        }
        public TaskLineTab VerifyTaskLineInfo(string type, string assetType, string scheduleAssetQty, string product, string unit, string state)
        {
            Assert.AreEqual(type, GetFirstSelectedItemInDropdown(firstTaskType));
            Assert.AreEqual(assetType, GetFirstSelectedItemInDropdown(firstTaskAssetType));
            Assert.AreEqual(scheduleAssetQty, GetAttributeValue(firstTaskQty, "value"));
            Assert.AreEqual(product, GetFirstSelectedItemInDropdown(firstTaskProduct));
            Assert.AreEqual(unit, GetFirstSelectedItemInDropdown(firstTaskUnit));
            Assert.AreEqual(state, GetFirstSelectedItemInDropdown(firsTaskState));
            return this;
        }
        public TaskLineTab InputActuaAssetQuantity(int i)
        {
            EditSendKeys(actualAssetQuantityInput, i.ToString());
            return this;
        }
        public TaskLineTab ClickOnAcualAssetQuantityText()
        {
            ClickOnElement(actualAssetQuantityText);
            return this;
        }
        public TaskLineTab SelectCompletedState()
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
