using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace si_automated_tests.Source.Main.Pages.Task
{
    public class TaskLineTab : TaskTabNavigation
    {
        private readonly By firstTaskType = By.Id("taskLineType.id");
        private readonly By firstTaskAssetType = By.XPath("//*[contains(@params,'assetType')]/select");
        private readonly By firstTaskQty = By.Id("scheduledAssetQuantity.id");
        private readonly By firstTaskProduct = By.XPath("//*[contains(@params,'product')]/select");
        private readonly By firstTaskUnit = By.XPath("//*[contains(@params,'unitOfMeasure')]/select");
        private readonly By firsTaskState = By.Id("itemState.id");

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
    }
}
