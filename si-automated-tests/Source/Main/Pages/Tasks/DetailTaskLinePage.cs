using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class DetailTaskLinePage : BasePage
    {
        private readonly By titleTask = By.XPath("//h4[text()='Task']");
        private readonly By titleTaskDetail = By.XPath("//h4[text()='Task Lines']");
        private readonly By hyperLinkTask = By.XPath("//h4[text()='Task']/following-sibling::span/a");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");

        //DETAIL TAB
        private readonly By orderValue = By.CssSelector("input#order");
        private readonly By assetTypeDd = By.XPath("//label[contains(string(), 'Asset Type')]/following-sibling::echo-select/select");
        private readonly By minAssetQty = By.CssSelector("input#min-asset-qty");
        private readonly By scheduledAssetQty = By.CssSelector("input#scheduled-asset-quantity");
        private readonly By productDd = By.XPath("//label[contains(string(), 'Product')]/following-sibling::echo-select/select");
        private readonly By minProductQty = By.CssSelector("input#min-product-qty");
        private readonly By scheduleProductQty = By.CssSelector("input#scheduled-product-quantity");
        private readonly By destinationSiteDd = By.CssSelector("select[id='destinationSite.id']");
        private readonly By resolutionCodeDd = By.CssSelector("select#resolution-code");
        private readonly By clientRef = By.CssSelector("input#clientReference");
        private readonly By taskLineTypeDd = By.CssSelector("select#taskline-type");
        private readonly By assetInput = By.XPath("//div[@id='asset']/input");
        private readonly By maxAssetQty = By.CssSelector("input#max-asset-qty");
        private readonly By actualAssetQuantity = By.CssSelector("input#actual-asset-quantity");
        private readonly By unitDd = By.XPath("//label[contains(string(), 'Unit')]/following-sibling::echo-select/select");
        private readonly By maxProductQty = By.CssSelector("input#max-product-qty");
        private readonly By actualProductQty = By.CssSelector("input#actual-product-quantity");
        private readonly By siteProductDd = By.CssSelector("select[id='siteProduct.id']");
        private readonly By state = By.CssSelector("select#state");
        private readonly By completedDate = By.CssSelector("input#completed-date");
        private readonly By ewcCode = By.CssSelector("select#product-code");
        private readonly By confirmation = By.CssSelector("input[id='autoConfirmation']");

        //DATA TAb
        private readonly By dataTab = By.CssSelector("a[aria-controls='data-tab']");

        //HISTORY TAB
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By actionCreateTitle = By.XPath("(//strong[text()='Action : ']/following-sibling::span[text()='Create'])[1]");
        private readonly string actionUpdateTitle = "(//strong[text()='Action : ']/following-sibling::span[text()='Update'])[{0}]";
        private readonly By actionCreateUser = By.XPath("//span[text()='Create']/parent::div/following-sibling::div//strong[text()='User : ']/following-sibling::span");
        private readonly string actionUpdateUser = "(//span[text()='Update']/parent::div/following-sibling::div//strong[text()='User : ']/following-sibling::span)[{0}]";
        private readonly By actionCreateContent = By.XPath("//span[text()='Create']/parent::div/parent::div/following-sibling::div/div");
        private readonly string actionUpdateContent = "(//span[text()='Update']/parent::div/parent::div/following-sibling::div/div)[{0}]";
        private readonly By actionCreateDate = By.XPath("//span[text()='Create']/parent::div/following-sibling::div//strong[text()='Date : ']/following-sibling::span");
        private readonly string actionUpdateDate = "(//span[text()='Update']/parent::div/following-sibling::div//strong[text()='Date : ']/following-sibling::span)[{0}]";

        //DYNAMIC
        private readonly string assetTypeOption = "//label[contains(string(), 'Asset Type')]/following-sibling::echo-select/select//option[text()='{0}']";
        private readonly string destinationOption = "//select[@id='destinationSite.id']/option[text()='{0}']";
        private readonly string productOption = "//label[contains(string(), 'Product')]/following-sibling::echo-select/select//option[text()='{0}']";
        private readonly string stateOption = "//select[@id='state']/option[text()='{0}']";
        private readonly string siteProductOption = "//select[@id='siteProduct.id']/option[text()='{0}']";
        private readonly string resolutionCodeOption = "//select[@id='resolution-code']/option[text()='{0}']";

        [AllureStep]
        public DetailTaskLinePage WaitForTaskLineDetailDisplayed()
        {
            WaitUtil.WaitForElementVisible(titleTask);
            WaitUtil.WaitForElementVisible(titleTaskDetail);

            return this;
        }

        [AllureStep]
        public DetailTaskLinePage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage ChangeState(string stateValue)
        {
            ClickOnElement(this.state);
            ClickOnElement(stateOption, stateValue);
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage VerifyCurrentTaskState(string currentTaskStateValue)
        {
            Assert.AreEqual(currentTaskStateValue, GetFirstSelectedItemInDropdown(this.state));
            return this;
        }

        [AllureStep]
        public int GetTaskLineId()
        {
            return int.Parse(GetCurrentUrl().Replace(WebUrl.MainPageUrl + "web/task-lines/", ""));
        }

        [AllureStep]
        public DetailTaskLinePage VerifyTaskLineInfo(TaskLineModel taskLineModel)
        {
            Assert.AreEqual(taskLineModel.order, GetAttributeValue(orderValue, "value"));
            Assert.AreEqual(taskLineModel.assetType, GetFirstSelectedItemInDropdown(assetTypeDd));
            //Assert.AreEqual(taskLineModel.minAssetQty, GetAttributeValue(minAssetQty, "value"));
            Assert.AreEqual(taskLineModel.scheduledAssetQty, GetAttributeValue(scheduledAssetQty, "value"));
            if(taskLineModel.product.Equals(String.Empty)) {
                Assert.AreEqual("Select...", GetFirstSelectedItemInDropdown(productDd));
            } else
            {
                Assert.AreEqual(taskLineModel.product, GetFirstSelectedItemInDropdown(productDd));
            }
            //Assert.AreEqual(taskLineModel.minProductQty, GetAttributeValue(minProductQty, "value"));
            Assert.AreEqual(taskLineModel.scheduledProductQuantity, GetAttributeValue(scheduleProductQty, "value"));
            Assert.AreEqual(taskLineModel.destinationSite, GetFirstSelectedItemInDropdown(destinationSiteDd));
            if(taskLineModel.resolutionCode.Equals(String.Empty))
            {
                Assert.AreEqual("Select...", GetFirstSelectedItemInDropdown(resolutionCodeDd));
            } else
            {
                Assert.AreEqual(taskLineModel.resolutionCode, GetFirstSelectedItemInDropdown(resolutionCodeDd));
            }
            //Assert.AreEqual(taskLineModel., GetAttributeValue(clientRef, "value"));
            Assert.AreEqual(taskLineModel.type, GetFirstSelectedItemInDropdown(taskLineTypeDd));
            //Assert.AreEqual(taskLineModel.asset, GetAttributeValue(assetInput, "value"));
            //Assert.AreEqual(taskLineModel.maxAssetQty, GetAttributeValue(maxAssetQty, "value"));
            Assert.AreEqual(taskLineModel.actualAssetQuantity, GetAttributeValue(actualAssetQuantity, "value"));
            if (taskLineModel.unit.Equals(String.Empty))
            {
                Assert.AreEqual("Select...", GetFirstSelectedItemInDropdown(unitDd));
            }
            else
            {
                Assert.AreEqual(taskLineModel.unit, GetFirstSelectedItemInDropdown(unitDd));
            }
            //Assert.AreEqual(taskLineModel.maxProductQty, GetAttributeValue(maxProductQty, "value"));
            Assert.AreEqual(taskLineModel.actualProductQuantity, GetAttributeValue(actualProductQty, "value"));
            Assert.AreEqual(taskLineModel.siteProduct, GetFirstSelectedItemInDropdown(siteProductDd));
            Assert.AreEqual(taskLineModel.state, GetFirstSelectedItemInDropdown(state));
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage InputAllFieldInDetailTab(TaskLineModel taskLineModelNew)
        {
            SendKeys(orderValue, taskLineModelNew.order);
            //Asset Type
            ClickOnElement(assetTypeDd);
            ClickOnElement(assetTypeOption, taskLineModelNew.assetType);
            //Min Asset Qty
            SendKeys(this.minAssetQty, taskLineModelNew.minAssetQty);
            //Scheduled Asset Qty
            SendKeys(this.scheduledAssetQty, taskLineModelNew.scheduledAssetQty);
            //Min Product Qty
            SendKeys(this.minProductQty, taskLineModelNew.minProductQty);
            //Product
            ClickOnElement(productDd);
            ClickOnElement(productOption, taskLineModelNew.product);
            //Scheduled Product Qty
            SendKeys(this.scheduleProductQty, taskLineModelNew.scheduledProductQuantity);
            //Destination Site
            ClickOnElement(destinationSiteDd);
            ClickOnElement(destinationOption, taskLineModelNew.destinationSite);
            //Max ASset Qty
            SendKeys(this.maxAssetQty, taskLineModelNew.maxAssetQty);
            //Actual Asset Qty
            SendKeys(this.actualAssetQuantity, taskLineModelNew.actualAssetQuantity);
            //Max Product Qty
            SendKeys(this.maxProductQty, taskLineModelNew.maxProductQty);
            //Actual Product Qty
            SendKeys(this.actualProductQty, taskLineModelNew.actualProductQuantity);
            //Client ref
            SendKeys(this.clientRef, taskLineModelNew.clientRef);
            //State
            SleepTimeInSeconds(1);
            ClickOnElement(this.state);
            WaitUtil.WaitForElementVisible(stateOption, taskLineModelNew.state);
            ClickOnElement(stateOption, taskLineModelNew.state);
            //Site Product
            ClickOnElement(siteProductDd);
            ClickOnElement(siteProductOption, taskLineModelNew.siteProduct);
            return this;

        }

        [AllureStep]
        public DetailTaskLinePage SelectResolutionCode(string resolutionCodeValue)
        {
            //Resolution Code
            ClickOnElement(resolutionCodeDd);
            ClickOnElement(resolutionCodeOption, resolutionCodeValue);
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage VerifyTaskLineInfo(TaskLineModel taskLineModel, TaskLineDBModel taskLineDBModel, string productNameDB)
        {
            Assert.AreEqual(taskLineDBModel.scheduledassetquantity.ToString(), taskLineModel.scheduledAssetQty);
            Assert.AreEqual(productNameDB, taskLineModel.product);
            Assert.AreEqual(taskLineDBModel.scheduledproductquantity.ToString(), taskLineModel.scheduledProductQuantity);
            return this;
        }

        [AllureStep]
        public DetailTaskPage ClickOnHyperlinkOnHeader()
        {
            ClickOnElement(hyperLinkTask);
            return PageFactoryManager.Get<DetailTaskPage>();
        }

        [AllureStep]
        public DetailTaskLinePage VerifyHyperlinkOnHeader(TaskAndTaskTypeByTaskIdDBModel taskAndTaskTypeByTaskIdDBModel, string taskId)
        {
            string title = GetElementText(hyperLinkTask);
            Assert.AreEqual("(" + taskId + ") : " + taskAndTaskTypeByTaskIdDBModel.tasktype + " : " + taskAndTaskTypeByTaskIdDBModel.task, title);
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage ClickOnDataTab()
        {
            ClickOnElement(dataTab);
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage VerifyActionCreateWithUserDisplay(string timeCreated, string[] historyTitle, string[] valueExp, string userName)
        {
            Assert.IsTrue(IsControlDisplayed(actionCreateTitle));
            Assert.AreEqual(userName, GetElementText(actionCreateUser));
            Assert.AreEqual(timeCreated, GetElementText(actionCreateDate));
            //Content
            string[] allContent = GetElementText(actionCreateContent).Split(Environment.NewLine);
            for (int i = 0; i < allContent.Length; i++)
            {
                Assert.AreEqual(historyTitle[i] + ": " + valueExp[i] + ".", allContent[i]);
            }
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage VerifyActionUpdateWithUserDisplay(string timeUpdated, string[] historyTitle, string[] valueExp, string userName, int row)
        {
            Assert.IsTrue(IsControlDisplayed(actionUpdateTitle, row.ToString()));
            Assert.AreEqual(userName, GetElementText(actionUpdateUser, row.ToString()));
            Assert.IsTrue(GetElementText(actionUpdateDate, row.ToString()).Contains(timeUpdated));
            //Content
            string[] allContent = GetElementText(string.Format(actionUpdateContent, row)).Split(Environment.NewLine);
            for (int i = 0; i < allContent.Length; i++)
            {
                Assert.AreEqual(historyTitle[i] + ": " + valueExp[i] + ".", allContent[i]);
            }
            return this;
        }

        [AllureStep]
        public DetailTaskLinePage VeriryConfirmationAndCompletedDate(string time, string valueConfirmationDate)
        {
            Assert.AreEqual(valueConfirmationDate, GetAttributeValue(confirmation, "value"));
            Assert.IsTrue(GetAttributeValue(completedDate, "value").Contains(time));
            return this;
        }
    }
}
