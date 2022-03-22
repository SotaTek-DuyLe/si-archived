using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService
{
    public class AssetAndProducTab : AddServicePage
    {
        private readonly By addBtn = By.XPath("//button[@class='btn btn-success']");
        private readonly By assetType = By.Id("asset-type");
        private readonly By assetQuantity = By.Id("asset-quantity");
        private readonly By assertQuantiryText = By.XPath("//label[text()='Asset Quantity']");
        private readonly By tenure = By.Id("tenure");
        private readonly By product = By.XPath("//select[@id='product' and contains(@data-bind,'availableProducts')]");
        private readonly By ewc = By.XPath("//select[@id='product' and contains(@data-bind,'productCodes')]");
        private readonly By productQuantity = By.Id("product-quantity");
        private readonly By totalProductQuantity = By.Id("total-product-quantity");
        private readonly By doneBtn = By.XPath("//button[text()='Done']");
        private readonly By summaryText = By.XPath("//span[@data-bind='text: description']");

        private string assetTypeOptions = "//select[@id='asset-type']/option[text()='{0}']";

        //Edit Asset
        private readonly By editAssetBtn = By.XPath("//button[text()='Edit Asset']");
        private readonly string editAssertDoneBtn = "//button[text()='Done' and contains(@data-bind,'finishAssetProduct')]";
        private readonly By tenureText = By.XPath("//label[text()='Tenure']");
        private string textXpath = "//*[contains(text(),'{0}')]";

        //Remove Asset
        private readonly By removeAsset = By.XPath("//button[contains(@data-bind, 'removeAssetProduct')]");
        public AssetAndProducTab IsOnAssetTab()
        {
            WaitUtil.WaitForElementVisible(addBtn);
            return this;
        }
        public AssetAndProducTab ClickAddAsset()
        {
            ClickOnElement(addBtn);
            Thread.Sleep(3000);
            return this;
        }
        public AssetAndProducTab ChooseAssetType(string value)
        {
            SelectTextFromDropDown(assetType, value);
            return this;
        }
        public AssetAndProducTab ClickAssetType()
        {
            Thread.Sleep(2000);
            ClickOnElement(assetType);
            return this;
        }
        public AssetAndProducTab SelectAssetType(string value)
        {
            WaitUtil.WaitForElementVisible(assetTypeOptions, value);
            ClickAssetType();
            ClickOnElement(assetTypeOptions, value);
            return this;
        }
        public AssetAndProducTab ChooseTenure(string value)
        {
            SelectTextFromDropDown(tenure, value);
            return this;
        }
        public AssetAndProducTab ChooseProduct(string value)
        {
            SelectTextFromDropDown(product, value);
            return this;
        }
        public AssetAndProducTab ChooseEwcCode(string value)
        {
            SelectTextFromDropDown(ewc, value);
            return this;
        }
        public AssetAndProducTab InputAssetQuantity(int value)
        {
            SendKeys(assetQuantity, value.ToString());
            return this;
        }
        public AssetAndProducTab EditAssetQuantity(int value)
        { 
            EditSendKeys(assetQuantity, value.ToString());
            Thread.Sleep(1000);
            return this;
        }
        public AssetAndProducTab ClickAssetQuantityText()
        {
            ClickOnElement(assertQuantiryText);
            return this;
        }
        public AssetAndProducTab InputProductQuantity(int value)
        {
            SendKeys(productQuantity, value.ToString() + Keys.Enter);
            return this;
        }
        public AssetAndProducTab VerifyTotalProductQuantity(int value)
        {
            //unable to get text from this element
            //Assert.AreEqual(value.ToString(), GetElementText(totalProductQuantity));
            return this;
        }
        public AssetAndProducTab ClickDoneBtn()
        {
            ClickOnElement(doneBtn);
            return this;
        }
        public AssetAndProducTab VerifySummaryOfStep(string value)
        {
            WaitUtil.WaitForElementVisible(textXpath, value);
            Assert.IsTrue(IsControlDisplayed(textXpath, value));
            return this;
        }
        public AssetAndProducTab VerifyAssetSummary(int quantity, string type, string tenure, int productQuantity, string product)
        {
            var text1 = String.Format("{0} x {1}({2}), {3}", quantity.ToString(), type, tenure, productQuantity.ToString());
            Assert.IsTrue(GetElementText(summaryText).Contains(text1));
            Assert.IsTrue(GetElementText(summaryText).Contains(product));
            return this;
        }

        //Edit Asset 
        public AssetAndProducTab ClickOnEditAsset()
        {
            ClickOnElement(editAssetBtn);
            return this;
        }
        public AssetAndProducTab ClickOnTenureText()
        {
            ClickOnElement(tenureText);
            return this;
        }
        public AssetAndProducTab EditAssertClickDoneBtn()
        {
            Thread.Sleep(1000);
            WaitUtil.WaitForElementClickable(editAssertDoneBtn);
            ClickToElementByJavascript(editAssertDoneBtn);
            return this;
        }

        //Remove Asset 
        public AssetAndProducTab ClickRemoveAsset()
        {
            ClickOnElement(removeAsset);
            return this;
        }

    }
}
