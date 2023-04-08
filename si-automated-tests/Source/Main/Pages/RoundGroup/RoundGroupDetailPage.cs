using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Round
{
    public class RoundGroupDetailPage : BasePage
    {
        private readonly By title = By.XPath("//span[text()='Round Group']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By roundGroupInput = By.XPath("//input[@name='roundGroup']");
        private readonly By slotCountInput = By.CssSelector("div[id='details-tab'] input[name='sortOrder']");
        private readonly By roundTab = By.CssSelector("a[aria-controls='rounds-tab']");

        //RIGHT ROUND PANEL
        private readonly By rightRoundPanelTitle = By.XPath("//div[@id='rightRoundPanel']//span[text()='Round']");
        private readonly By rightRoundPanelRoundInput = By.XPath("//div[@id='rightRoundPanel']//input[@name='round']");
        private readonly By rightRoundPanelSlotInput = By.CssSelector("div[id='rightRoundPanel'] input[id='slot.id']");

        //DYNAMIC
        private readonly string anyArrowInRoundTab = "//div[@id='rounds-tab']//tr[1]//div[@id='toggle-actions']";

        [AllureStep]
        public RoundGroupDetailPage IsRoundGroupDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage VerifyMinValueInSlotCountField()
        {
            Assert.AreEqual("0", GetAttributeValue(slotCountInput, "min"));
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage InputSlotCount(string slotCountValue)
        {
            SendKeys(slotCountInput, slotCountValue);
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage ClearSlotCount()
        {
            ClearInputValue(slotCountInput);
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage VerifyValueInSlotCount(string slotCountValue)
        {
            Assert.AreEqual(slotCountValue, GetAttributeValue(slotCountInput, "value"));
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage ClickOnRoundTab()
        {
            ClickOnElement(roundTab);
            WaitForLoadingIconToDisappear();
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage ClickOnArrowInAnyRow(string numberOfRow)
        {
            ClickOnElement(anyArrowInRoundTab, numberOfRow);
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage IsRightRoundPanel()
        {
            WaitUtil.WaitForElementVisible(rightRoundPanelTitle);
            WaitUtil.WaitForElementVisible(rightRoundPanelRoundInput);
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage VerifyMinValueInSlotFieldRightRoundPanel()
        {
            Assert.AreEqual("0", GetAttributeValue(rightRoundPanelSlotInput, "min"));
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage InputSlotRightRoundPanel(string slotCountValue)
        {
            SendKeys(rightRoundPanelSlotInput, slotCountValue);
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage ClearSlotRightRoundPanel()
        {
            ClearInputValue(rightRoundPanelSlotInput);
            return this;
        }

        [AllureStep]
        public RoundGroupDetailPage VerifyValueInSlotRightRoundPanel(string slotCountValue)
        {
            Assert.AreEqual(rightRoundPanelSlotInput, GetAttributeValue(rightRoundPanelSlotInput, "value"));
            return this;
        }
    }
}
