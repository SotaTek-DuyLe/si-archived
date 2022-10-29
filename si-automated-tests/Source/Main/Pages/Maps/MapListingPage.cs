using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class MapListingPage : BasePage
    {
        private readonly By resourceTab = By.XPath("//a[@aria-controls='west-resources-tab']/parent::li");
        private readonly By optionTab = By.XPath("//a[@aria-controls='west-resource-options-tab']/parent::li");
        private readonly By resourceTrailText = By.XPath("//label[contains(string(), 'Resource trail')]");
        private readonly By roundTab = By.XPath("//a[@aria-controls='rounds-tab']/parent::li");
        private readonly By bulkUpdateBtn = By.CssSelector("button[title='Bulk Update']");

        //DYNAMIC
        private readonly string anyMapOpject = "//span[text()='{0}']/ancestor::li";

        [AllureStep]
        public MapListingPage WaitForMapsTabDisplayed()
        {
            WaitUtil.WaitForElementVisible(resourceTab);
            WaitUtil.WaitForElementVisible(optionTab);
            WaitUtil.WaitForElementVisible(resourceTrailText);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnAnyMapObject(string mapObjectName)
        {
            ClickOnElement(anyMapOpject, mapObjectName);
            return this;
        }

        #region ROUND TAB
        private readonly By firstShowRoundInstanceBtnRoundTab = By.XPath("(//div[@id='rounds-tab']//button[text()='Show Round Instance'])[1]");
        private readonly By roundInRightHand = By.XPath("//div[@title='Rounds']/ancestor::li[@id='Rounds']");
        private readonly By roundNameInLeftHand = By.XPath("//span[@class='map-object-name']/ancestor::li");

        [AllureStep]
        public MapListingPage ClickOnRoundTab()
        {
            ClickOnElement(roundTab);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnRoundInRightHand()
        {
            ClickOnElement(roundInRightHand);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnRoundNameInLeftHand()
        {
            ClickOnElement(roundNameInLeftHand);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnFirstShowRoundInstanceBtnRoundTab()
        {
            ClickOnElement(firstShowRoundInstanceBtnRoundTab);
            return this;
        }

        #endregion

        #region WORKSHEET TAB
        private readonly By workSheetTab = By.XPath("//a[@aria-controls='worksheet-tab']/parent::li");
        private readonly By idInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]/input");
        private readonly By descInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l4')]/input");
        private readonly By partyInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l6')]/input");
        private readonly By idValueInFistRow = By.XPath("//div[@id='grid']//div[@class='grid-canvas']//div[contains(@class, 'l3')]");
        private readonly By partyValueInFirstRow = By.XPath("(//div[@id='grid']//div[@class='grid-canvas']//div[contains(@class, 'l6')])[1]");
        private readonly By statusValueInFirstRow = By.XPath("(//div[@id='grid']//div[@class='grid-canvas']//div[contains(@class, 'l18')])[1]");
        private readonly By frameWorksheet = By.CssSelector("iframe[id='worksheet-tab']");

        //DYNAMIC
        private readonly string statusOptionInFirstRow = "//div[@id='grid']//div[@class='grid-canvas']//select/option[{0}]";

        [AllureStep]
        public MapListingPage ClickOnWorksheetTab()
        {
            ClickOnElement(workSheetTab);
            return this;
        }

        [AllureStep]
        public MapListingPage SwitchToWorksheetTab()
        {
            SwitchNewIFrame(frameWorksheet);
            return this;
        }

        [AllureStep]
        public MapListingPage FilterWorksheetById(string id)
        {
            SendKeys(idInput, id);
            return this;
        }

        [AllureStep]
        public MapListingPage FilterWorksheetByDesc(string descValue)
        {
            SendKeys(descInput, descValue);
            return this;
        }

        [AllureStep]
        public MapListingPage FilterWorksheetByPartyName(string partyValue)
        {
            SendKeys(partyInput, partyValue);
            return this;
        }

        [AllureStep]
        public MapListingPage VerifyTheDisplayOfTheWorksheetIdAfterFiltering(string id)
        {
            Assert.AreEqual(id, GetElementText(idValueInFistRow), "id in first row is incorrect");
            return this;
        }

        [AllureStep]
        public MapListingPage VerifyTheDisplayOfTheWorksheetIdAfterFilteringParty(string partyName)
        {
            Assert.AreEqual(partyName, GetElementText(partyValueInFirstRow), "party in first row is incorrect");
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnStatusInFirstRow()
        {
            ClickOnElement(statusValueInFirstRow);
            return this;
        }

        [AllureStep]
        public MapListingPage VerifyOrderInTaskStateDd(string[] taskStateValues)
        {
            for (int i = 0; i < taskStateValues.Length; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(statusOptionInFirstRow, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnBulkUpdateBtn()
        {
            ClickOnElement(bulkUpdateBtn);
            return this;
        }

        #endregion

        #region BULK UPDATE
        private readonly By statusDd = By.XPath("//label[text()='Status']/following-sibling::select[1]");

        //DYNAMIC
        private readonly string statusOptionInBulkUpdate = "//label[text()='Status']/following-sibling::select[1]/option[{0}]";

        [AllureStep]
        public MapListingPage ClickOnStatusDdInBulkUpdatePopup()
        {
            ClickOnElement(statusDd);
            return this;
        }

        [AllureStep]
        public MapListingPage VerifyOrderInTaskStateDdInBulkUpdate(string[] taskStateValues)
        {
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(taskStateValues[i], GetElementText(statusOptionInBulkUpdate, (i + 2).ToString()), "Task state at " + i + "is incorrect");
            }
            return this;
        }

        #endregion


    }
}
