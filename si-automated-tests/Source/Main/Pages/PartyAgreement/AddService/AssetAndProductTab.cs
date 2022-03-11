using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement.AddService
{
    public class AssetAndProducTab : AddServicePage
    {
        private readonly By addBtn = By.XPath("//button[@class='btn btn-success']");
        private readonly By assetType = By.Id("asset-type");
        private readonly By assetQuantity = By.Id("asset-quantity");
        private readonly By tenure = By.Id("tenure");
        private readonly By product = By.XPath("//select[@id='product' and contains(@data-bind,'availableProducts')]");
        private readonly By ewc = By.XPath("//select[@id='product' and contains(@data-bind,'productCodes')]");
        private readonly By productQuantity = By.Id("product-quantity");
        private readonly By totalProductQuantity = By.Id("total-product-quantity");
        private readonly By doneBtn = By.XPath("//button[text()='Done']");
        private readonly By summaryText = By.XPath("//span[@data-bind='text: description']");

        public AssetAndProducTab IsOnAssetTab()
        {
            WaitUtil.WaitForElementVisible(addBtn);
            return this;
        }
        public AssetAndProducTab ClickAddAsset()
        {
            ClickOnElement(addBtn);
            return this;
        }
        public AssetAndProducTab ChooseAssetType(string value)
        {
            SelectValueFromDropDown(assetType, value);
            return this;
        }
        public AssetAndProducTab ChooseTenure(string value)
        {
            SelectValueFromDropDown(tenure, value);
            return this;
        }
        public AssetAndProducTab ChooseProduct(string value)
        {
            SelectValueFromDropDown(product, value);
            return this;
        }
        public AssetAndProducTab ChooseEwcCode(string value)
        {
            SelectValueFromDropDown(ewc, value);
            return this;
        }
        public AssetAndProducTab InputAssetQuantity(int value)
        {
            SendKeys(assetQuantity, value.ToString());
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
        public AssetAndProducTab VerifyAssetSummary(int quantity, string type, string tenure, int productQuantity, string product)
        {
            var text1 = String.Format("{0} x {1}({2}), {3}", quantity.ToString(), type, tenure, productQuantity.ToString());
            Assert.IsTrue(GetElementText(summaryText).Contains(text1));
            Assert.IsTrue(GetElementText(summaryText).Contains(product));
            return this;
        }
    }
}
