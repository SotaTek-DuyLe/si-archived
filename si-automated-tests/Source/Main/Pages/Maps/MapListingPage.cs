﻿using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class MapListingPage : BasePageCommonActions
    {
        private readonly By resourceTab = By.XPath("//a[@aria-controls='west-resources-tab']/parent::li");
        private readonly By optionTab = By.XPath("//a[@aria-controls='west-resource-options-tab']/parent::li");
        private readonly By resourceTrailText = By.XPath("//label[contains(string(), 'Resource trail')]");
        private readonly By roundTab = By.XPath("//a[@aria-controls='rounds-tab']/parent::li");
        private readonly By bulkUpdateBtn = By.CssSelector("button[title='Bulk Update']");
        public readonly By FromInput = By.XPath("//input[@id='from-date']");
        public readonly By ToInput = By.XPath("//input[@id='to-date']");
        public readonly By TrailTab = By.XPath("//a[@aria-controls='trail-tab']");
        public readonly By GoButton = By.XPath("//button[contains(text(), 'Go')]");
        public readonly By ResourceCom = By.XPath("//span[@title='COM1 NST (REL)']");
        public readonly By RightHandLayer = By.XPath("//li[@id='COM1 NST']");
        public readonly By RoundsLayer = By.XPath("//li[@id='Rounds']");
        //DYNAMIC
        private readonly string anyMapOpject = "//span[text()='{0}']/ancestor::li";
        private readonly string anyOptionInOptionsTab = "//label[contains(string(), '{0}')]/parent::div";

        #region Trail tab
        private readonly string HeaderColumn = "//span[@class='slick-column-name' and text()='{0}']";
        public readonly By MoreButton = By.XPath("//div[@class='echo-slick-column-picker']//button[text()='More >>']");
        public readonly By UpdateButton = By.XPath("//div[@class='echo-slick-column-picker']//button[text()='Update']");
        public readonly By LessButton = By.XPath("//div[@class='echo-slick-column-picker']//button[text()='<< Less']");
        private readonly string ExtraCheckbox = "//label[text()='{0}']//preceding-sibling::input";

        [AllureStep]
        public By GetHeaderColumn(string name)
        {
            return By.XPath(string.Format(HeaderColumn, name));
        }

        [AllureStep]
        public By GetExtraCheckbox(string name)
        {
            return By.XPath(string.Format(ExtraCheckbox, name));
        }

        [AllureStep]
        public MapListingPage WaitForLessButtonDisplayed()
        {
            WaitUtil.WaitForElementVisible(LessButton);
            return this;
        }
        #endregion

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
        private readonly By firstRoundNameInLeftHand = By.XPath("(//span[@class='map-object-name']/ancestor::li)[1]");
        private readonly By firstShowRoundInstanceBtn = By.XPath("(//button[text()='Show Round Instance']/parent::div)[1]");
        private readonly By firstRoundRecord = By.XPath("//div[@id='rounds-tab']//div[@class='grid-canvas']/div[1]");

        //DYNAMIC
        private readonly string roundNameInLeftHandByResource = "(//span[@class='map-object-name' and text()='{0}']/ancestor::li)[1]";

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
        public MapListingPage ClickOnFirstRoundInLeftHand()
        {
            ClickOnElement(firstRoundNameInLeftHand);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnFirstRoundInLeftHand(string resourceName)
        {
            ClickOnElement(roundNameInLeftHandByResource, resourceName);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnOptionsTab()
        {
            ClickOnElement(optionTab);
            return this;
        }
        [AllureStep]
        public MapListingPage ClickOnRoundNameInLeftHand()
        {
            ClickOnElement(roundNameInLeftHand);
            return this;
        }

        [AllureStep]
        public MapListingPage VerifyOptionIsNotDisplay(string optionName)
        {
            Assert.IsTrue(IsControlUnDisplayed(anyOptionInOptionsTab, optionName), optionName + "is displayed in the [Options] tab");
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnFirstShowRoundInstanceBtnRoundTab()
        {
            ClickOnElement(firstShowRoundInstanceBtnRoundTab);
            return this;
        }

        [AllureStep]
        public MapListingPage WaitForRoundsTabLoaded()
        {
            WaitUtil.WaitForElementVisible(firstRoundRecord);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnFirstShowRoundInstanceBtn()
        {
            ClickOnElement(firstShowRoundInstanceBtn);
            return this;
        }

        #endregion

        #region WORKSHEET TAB
        private readonly By workSheetTab = By.XPath("//a[@aria-controls='worksheet-tab']/parent::li");
        private readonly By fromDateInput = By.XPath("//input[@id='from-date']");
        private readonly By goBtn = By.XPath("//button[contains(@class, 'btn-go')]");
        private readonly By idInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]/input");
        private readonly By descInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l4')]/input");
        private readonly By partyInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l7')]/input");
        private readonly By idValueInFistRow = By.XPath("//div[@id='grid']//div[@class='grid-canvas']//div[contains(@class, 'l3')]");
        private readonly By partyValueInFirstRow = By.XPath("(//div[@id='grid']//div[@class='grid-canvas']//div[contains(@class, 'l7')])[1]");
        private readonly By statusValueInFirstRow = By.XPath("(//img[@src='/web/content/images/coretaskstate/s2.svg']/parent::div/parent::div/following-sibling::div[contains(@class, 'l20')])[1]");
        private readonly By statusOfTaskOnHoldAtFirstColumn = By.XPath("(//img[@src='/web/content/style/images/task-onhold.png']/parent::div/parent::div/following-sibling::div[contains(@class, 'l20')])[1]");
        private readonly By checkboxIdOfFirstRow = By.XPath("(//img[@src='/web/content/images/coretaskstate/s2.svg']/parent::div/parent::div/preceding-sibling::div[contains(@class, 'l0')])[1]");
        private readonly By frameWorksheet = By.CssSelector("iframe[id='worksheet-tab']");

        //DYNAMIC
        private readonly string statusOptionInFirstRow = "//div[@id='grid']//div[@class='grid-canvas']//select/option[{0}]";

        [AllureStep]
        public MapListingPage SendKeyInFromDate(string dateValue)
        {
            InputCalendarDate(FromInput, dateValue);
            return this;
        }

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
        public MapListingPage SendDateInFromDateInput(string dateValue)
        {
            InputCalendarDate(fromDateInput, dateValue);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnGoBtn()
        {
            ClickOnElement(goBtn);
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
        public MapListingPage ClickOnStatusInFirstRowAndVerify(string[] taskStateValues, string[] taskStateOnHoldValues)
        {
            if (IsControlDisplayedNotThrowEx(statusValueInFirstRow))
            {
                ClickOnElement(statusValueInFirstRow);
                for (int i = 0; i < taskStateValues.Length; i++)
                {
                    Assert.AreEqual(taskStateValues[i], GetElementText(statusOptionInFirstRow, (i + 2).ToString()), "Task state at " + i + "is incorrect");
                }
            }
            else if (IsControlDisplayedNotThrowEx(statusOfTaskOnHoldAtFirstColumn))
            {
                ClickOnElement(statusOfTaskOnHoldAtFirstColumn);
                for (int i = 0; i < taskStateOnHoldValues.Length; i++)
                {
                    Assert.AreEqual(taskStateOnHoldValues[i], GetElementText(statusOptionInFirstRow, (i + 2).ToString()), "Task state at " + i + "is incorrect");
                }
            }
            
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnCheckboxFirstRow()
        {
            ClickOnElement(checkboxIdOfFirstRow);
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

        #region
        private readonly By routeTab = By.CssSelector("div[id='south-data'] a[aria-controls='route-tab']");
        private readonly By firstRoute = By.CssSelector("div[id='route-tab'] div[class='grid-canvas']");
        private readonly By firstGenerateGuidedRoute = By.XPath("(//div[@id='route-tab']//button[text()='Generate Guided Route'])[1]");

        //DYNAMIC
        private readonly string generateGuidedRouteBtnByResource = "//div[@id='route-tab']//div[text()='{0}']//following-sibling::div/button[text()='Generate Guided Route']";

        [AllureStep]
        public MapListingPage ClickOnRouteTab()
        {
            ClickOnElement(routeTab);
            return this;
        }

        [AllureStep]
        public MapListingPage WaitForRouteTabLoaded()
        {
            WaitUtil.WaitForAllElementsVisible(firstRoute);
            return this;
        }

        [AllureStep]
        public MapListingPage ClickOnGenerateGuidedRouteBtnByResourceName(string resourceName)
        {
            ClickOnElement(generateGuidedRouteBtnByResource, resourceName);
            return this;
        }

        #endregion


    }
}
