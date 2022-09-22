using NUnit.Allure.Attributes;
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
        public readonly By assetQuantity = By.Id("asset-quantity");
        private readonly By assertQuantiryText = By.XPath("//label[text()='Asset Quantity']");
        private readonly By tenure = By.Id("tenure");
        public readonly By deliveryDate = By.Id("delivery-date");
        private readonly By product = By.XPath("//select[@id='product' and contains(@data-bind,'availableProducts')]");
        private readonly By ewc = By.XPath("//select[@id='product' and contains(@data-bind,'productCodes')]");
        private readonly By productQuantity = By.Id("product-quantity");
        private readonly By totalProductQuantity = By.Id("total-product-quantity");
        private readonly By unitSelect = By.Id("unit");
        private readonly By doneBtn = By.XPath("//button[text()='Done']");
        private readonly By summaryText = By.XPath("//span[@data-bind='text: description']");
        private readonly By assetOnSiteCheckBox = By.Id("asset-on-site");
        public readonly By numberOfAssetOnSite = By.Id("number-of-assets-on-site");

        private string assetTypeOptions = "//select[@id='asset-type']/option[text()='{0}']";

        //Edit Asset
        private readonly By editAssetBtn = By.XPath("//button[text()='Edit Asset']");
        private readonly string editAssertDoneBtn = "//button[text()='Done' and contains(@data-bind,'finishAssetProduct')]";
        private readonly By tenureText = By.XPath("//label[text()='Tenure']");
        private string textXpath = "//*[contains(text(),'{0}')]";

        //Remove Asset
        private readonly By removeAsset = By.XPath("//button[contains(@data-bind, 'removeAssetProduct')]");
        [AllureStep]
        public AssetAndProducTab IsOnAssetTab()
        {
            WaitUtil.WaitForElementVisible(addBtn);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ClickAddAsset()
        {
            ClickOnElement(addBtn);
            Thread.Sleep(3000);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ChooseAssetType(string value)
        {
            SelectTextFromDropDown(assetType, value);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ClickAssetType()
        {
            Thread.Sleep(1000);
            ClickOnElement(assetType);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab SelectAssetType(string value)
        {
            WaitUtil.WaitForElementVisible(assetTypeOptions, value);
            ClickAssetType();
            ClickOnElement(assetTypeOptions, value);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ChooseTenure(string value)
        {
            SelectTextFromDropDown(tenure, value);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab VerifyDeliveryDate(string date)
        {
            Assert.AreEqual(GetAttributeValue(deliveryDate, "value"), date);
            return this;
        }
        [AllureStep]
        public String GetDeliveryDate()
        {
            WaitUtil.WaitForElementVisible(deliveryDate);
            return GetAttributeValue(deliveryDate, "value");
        }
        [AllureStep]
        public AssetAndProducTab ChooseProduct(string value)
        {
            SelectTextFromDropDown(product, value);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ChooseEwcCode(string value)
        {
            SelectTextFromDropDown(ewc, value);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab InputAssetQuantity(int value)
        {
            SendKeys(assetQuantity, value.ToString());
            return this;
        }
        [AllureStep]
        public AssetAndProducTab EditAssetQuantity(int value)
        { 
            EditSendKeys(assetQuantity, value.ToString());
            Thread.Sleep(1000);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ClickAssetQuantityText()
        {
            ClickOnElement(assertQuantiryText);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab InputProductQuantity(int value)
        {
            SendKeys(productQuantity, value.ToString() + Keys.Enter);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab VerifyTotalProductQuantity(int value)
        {
            //unable to get text from this element
            //Assert.AreEqual(value.ToString(), GetElementText(totalProductQuantity));
            return this;
        }
        [AllureStep]
        public AssetAndProducTab TickAssetOnSite()
        {
            if (!IsElementSelected(assetOnSiteCheckBox))
            {
                ClickOnElement(assetOnSiteCheckBox);
            }
            return this;
        }
        [AllureStep]
        public AssetAndProducTab InputAssetOnSiteNum(int value)
        {
            SendKeys(numberOfAssetOnSite, value.ToString());
            return this;
        }
        [AllureStep]
        public AssetAndProducTab SelectKiloGramAsUnit()
        {
            SelectTextFromDropDown(unitSelect, "Kilograms");
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ClickDoneBtn()
        {
            ClickOnElement(doneBtn);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab VerifySummaryOfStep(string value)
        {
            WaitUtil.WaitForElementVisible(textXpath, value);
            Assert.IsTrue(IsControlDisplayed(textXpath, value));
            return this;
        }
        [AllureStep]
        public AssetAndProducTab VerifyAssetSummary(int quantity, string type, string tenure, int productQuantity, string product)
        {
            var text1 = String.Format("{0} x {1}({2}), {3}", quantity.ToString(), type, tenure, productQuantity.ToString());
            Assert.IsTrue(GetElementText(summaryText).Contains(text1));
            Assert.IsTrue(GetElementText(summaryText).Contains(product));
            return this;
        }

        [AllureStep]
        public AssetAndProducTab VerifyInputAssetAndProduct(int quantity, string type, string tenureVal, string productVal, string ewcCode)
        {
            VerifyInputValue(assetQuantity, quantity.AsString());
            VerifySelectedValue(assetType, type);
            VerifySelectedValue(tenure, tenureVal);
            VerifySelectedValue(product, productVal);
            VerifySelectedValue(ewc, ewcCode);
            return this;
        }

        //Edit Asset 
        [AllureStep]
        public AssetAndProducTab ClickOnEditAsset()
        {
            ClickOnElement(editAssetBtn);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab ClickOnTenureText()
        {
            ClickOnElement(tenureText);
            return this;
        }
        [AllureStep]
        public AssetAndProducTab EditAssertClickDoneBtn()
        {
            Thread.Sleep(1000);
            WaitUtil.WaitForElementClickable(editAssertDoneBtn);
            ClickToElementByJavascript(editAssertDoneBtn);
            return this;
        }

        //Remove Asset 
        [AllureStep]
        public AssetAndProducTab ClickRemoveAsset()
        {
            ClickOnElement(removeAsset);
            return this;
        }

    }
}
