using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class DetailTaskLinePage : BasePage
    {
        private readonly By titleTask = By.XPath("//h4[text()='Task']");
        private readonly By titleTaskDetail = By.XPath("//h4[text()='Task Lines']");

        //DETAIL TAB
        private readonly By orderValue = By.CssSelector("input#order");
        private readonly By assetTypeDd = By.XPath("//label[contains(string(), 'Asset Type')]/following-sibling::echo-select/select");
        private readonly By minAssetQty = By.CssSelector("input#min-asset-qty");
        private readonly By scheduledAssetQty = By.CssSelector("input#scheduled-asset-quantity");
        private readonly By productDd = By.XPath("//label[contains(string(), 'Product')]/following-sibling::echo-select/select");
        private readonly By minProductQty = By.CssSelector("input#min-product-qty");
        private readonly By scheduleProductQty = By.CssSelector("input#scheduled-product-quantity");
        private readonly By destinationSiteDd = By.CssSelector("select#destinationSite.id");
        private readonly By resolutionCodeDd = By.CssSelector("select#resolution-code");
        private readonly By clientRef = By.CssSelector("input#clientReference");
        private readonly By taskLineTypeDd = By.CssSelector("select#taskline-type");
        private readonly By assetInput = By.CssSelector("//div[@id='asset']/input");
        private readonly By maxAssetQty = By.CssSelector("input#max-asset-qty");
        private readonly By actualAssetQuantity = By.CssSelector("input#actual-asset-quantity");
        private readonly By unitDd = By.XPath("//label[contains(string(), 'Unit')]/following-sibling::echo-select/select");
        private readonly By maxProductQty = By.CssSelector("input#max-product-qty");
        private readonly By actualProductQty = By.CssSelector("input#actual-product-quantity");
        private readonly By siteProductDd = By.CssSelector("select#siteProduct.id");
        private readonly By state = By.CssSelector("select#state");
        private readonly By completedDate = By.CssSelector("input#completed-date");
        private readonly By ewcCode = By.CssSelector("select#product-code");

        public DetailTaskLinePage WaitForTaskLineDetailDisplayed()
        {
            WaitUtil.WaitForElementVisible(titleTask);
            WaitUtil.WaitForElementVisible(titleTaskDetail);
            return this;
        }

        public int GetTaskLineId()
        {
            return int.Parse(GetCurrentUrl().Replace("web/task-lines/", ""));
        }

        public DetailTaskLinePage VerifyTaskLineInfo(TaskLineModel taskLineModel)
        {
            Assert.AreEqual(taskLineModel.order, GetAttributeValue(orderValue, "value"));
            Assert.AreEqual(taskLineModel.assetType, GetFirstSelectedItemInDropdown(assetTypeDd));
            Assert.AreEqual(taskLineModel.minAssetQty, GetAttributeValue(minAssetQty, "value"));
            Assert.AreEqual(taskLineModel.scheduledAssetQty, GetAttributeValue(scheduledAssetQty, "value"));
            Assert.AreEqual(taskLineModel.product, GetFirstSelectedItemInDropdown(productDd));
            Assert.AreEqual(taskLineModel.minProductQty, GetAttributeValue(minProductQty, "value"));
            Assert.AreEqual(taskLineModel.scheduledProductQuantity, GetAttributeValue(scheduleProductQty, "value"));
            Assert.AreEqual(taskLineModel.destinationSite, GetFirstSelectedItemInDropdown(destinationSiteDd));
            Assert.AreEqual(taskLineModel.resolutionCode, GetFirstSelectedItemInDropdown(resolutionCodeDd));
            //Assert.AreEqual(taskLineModel., GetAttributeValue(clientRef, "value"));
            Assert.AreEqual(taskLineModel.type, GetFirstSelectedItemInDropdown(taskLineTypeDd));
            Assert.AreEqual(taskLineModel.asset, GetAttributeValue(assetInput, "value"));
            Assert.AreEqual(taskLineModel.maxAssetQty, GetAttributeValue(maxAssetQty, "value"));
            Assert.AreEqual(taskLineModel.actualAssetQuantity, GetAttributeValue(actualAssetQuantity, "value"));
            Assert.AreEqual(taskLineModel.unit, GetFirstSelectedItemInDropdown(unitDd));
            Assert.AreEqual(taskLineModel.maxProductQty, GetAttributeValue(maxProductQty, "value"));
            Assert.AreEqual(taskLineModel.actualProductQuantity, GetAttributeValue(actualProductQty, "value"));
            Assert.AreEqual(taskLineModel.siteProduct, GetFirstSelectedItemInDropdown(siteProductDd));
            Assert.AreEqual(taskLineModel.state, GetFirstSelectedItemInDropdown(state));
            return this;
        }
    }
}
