using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class AssetDetailItemPage : BasePageCommonActions
    {
        public readonly By AssetInput = By.XPath("//input[@name='asset']");
        public readonly By AssetReferenceInput = By.XPath("//input[@name='assetReference']");
        public readonly By AssetTypeSelect = By.XPath("//select[@id='assetType.id']");
        public readonly By ProductSelect = By.XPath("//echo-select[contains(@params, 'product')]//select");
        public readonly By StateSelect = By.XPath("//select[@id='state.id']");
        public readonly By AgreementLineSelect = By.XPath("//select[@id='agreementLine.id']");
        private readonly By title = By.XPath("//span[text()='Asset']");
        private readonly By dataTab = By.CssSelector("a[aria-controls='data-tab']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By trailTab = By.CssSelector("a[aria-controls='trail-tab']");

        //DYNAMIC
        private readonly string anyMessages = "//*[contains(text(),'{0}')]";
        private readonly string assetTypeOption = "//select[@id='assetType.id']/option[text()='{0}']";


        [AllureStep]
        public AssetDetailItemPage IsOnPage()
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(AssetInput));
            return this;
        }

        #region Data tab

        [AllureStep]
        public AssetDetailItemPage ClickOnDataTab()
        {
            ClickOnElement(dataTab);
            return this;
        }

        [AllureStep]
        public AssetDetailItemPage VerifyNotDisplayMessageAttributeConfig(string messageError)
        {
            Assert.IsTrue(IsControlUnDisplayed(anyMessages, messageError));
            return this;
        }

        #endregion

        #region Detail tab
        [AllureStep]
        public AssetDetailItemPage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            return this;
        }

        [AllureStep]
        public AssetDetailItemPage ClickAndSelectAssetType(string assetTypeValue)
        {
            ClickOnElement(AssetTypeSelect);
            ClickOnElement(assetTypeOption, assetTypeValue);
            return this;
        }

        #endregion

        #region Trial tab
        private readonly By locationAtFirstRowTrailTab = By.CssSelector("div[id='trail-tab'] tr:nth-child(1) a");

        [AllureStep]
        public AssetDetailItemPage ClickOnTrailTab()
        {
            ClickOnElement(trailTab);
            return this;
        }

        [AllureStep]
        public AssetDetailItemPage VerifyServiceUnitAtFirstRowTrailTab(string serviceUnitNameExp)
        {
            Assert.AreEqual(serviceUnitNameExp, GetElementText(locationAtFirstRowTrailTab), "Service unit name is incorrect");
            return this;
        }

        [AllureStep]
        public AssetDetailItemPage ClickOnServiceUnitLinkAtFirstRowTrailTab()
        {
            ClickOnElement(locationAtFirstRowTrailTab);
            return this;
        }

        #endregion

        [AllureStep]
        public AssetDetailItemPage VerifyCurrentUrl()
        {
            Assert.IsTrue(GetCurrentUrl().Contains(WebUrl.MainPageUrl + "web/assets/"));
            string assetId = GetCurrentUrl().Split("assets/")[1];
            Assert.IsTrue(assetId != "0");
            return this;
        }
    }
}
