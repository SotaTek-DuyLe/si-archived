using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundInstanceEventDetailPage : BasePage
    {
        private readonly By title = By.XPath("//h4[contains(string(), 'Round Instance Event')]");

        //DETAIL TAB
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By roundEventTypeDd = By.XPath("//label[text()='Round Event Type']/following-sibling::select");
        private readonly By resourceDd = By.XPath("//label[text()='Resource']/following-sibling::select");
        private readonly By locationDd = By.XPath("//label[text()='Location']/following-sibling::div/select");
        private readonly By firstLocationOption = By.XPath("//label[text()='Location']/following-sibling::div/select/option[1]");

        //DATA TAB
        private readonly By dataTab = By.CssSelector("a[aria-controls='data-tab']");
        private readonly By ticketNumberInput = By.XPath("//label[text()='Ticket Number']/following-sibling::input");
        private readonly By grossWeightInput = By.XPath("//label[text()='Gross Weight (T)']/following-sibling::input");
        private readonly By wasteTypeDd = By.XPath("//label[text()='Waste Type']/following-sibling::select");
        private readonly By inputImage = By.CssSelector("input[type='file']");
        private readonly By netWeightInput = By.XPath("//label[text()='Net Weight']/following-sibling::input");

        //HISTORY TAB
        private readonly By historyTab = By.CssSelector("a[aria-controls='history-tab']");
        private readonly By firstValueInDetailColumn = By.XPath("//div[@class='grid-canvas']/div[1]//button[@title='Click to see full details']");

        //HISTORY POPUP
        private readonly By titleHistoryPopup = By.XPath("//h4[text()='History Details']");
        private readonly By tagContent = By.XPath("//h4[text()='History Details']/parent::div/following-sibling::div[@class='modal-body']");
        private readonly By closeHistoryPopupBtn = By.XPath("//h4[text()='History Details']/preceding-sibling::button");

        //DYNAMIC
        private readonly string anyRoundEventTypeOption = "//label[text()='Round Event Type']/following-sibling::select/option[text()='{0}']";
        private readonly string anyResourceOption = "//label[text()='Resource']/following-sibling::select/option[text()='{0}']";
        private readonly string roundEventType = "//p[text()='{0}']";
        private readonly string optionInWasteType = "//label[text()='Waste Type']/following-sibling::select/option[text()='{0}']";

        [AllureStep]
        public RoundInstanceEventDetailPage IsRoundInstanceEventDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(detailTab));
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage SelectRoundEventTypeAndResource(string roundEventTypeValue, string resourceValue)
        {
            //Select - Round Event Type
            ClickOnElement(roundEventTypeDd);
            ClickOnElement(anyRoundEventTypeOption, roundEventTypeValue);

            //Select - Resource
            ClickOnElement(resourceDd);
            ClickOnElement(anyResourceOption, resourceValue);
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage ClickOnHistoryTab()
        {
            ClickOnElement(historyTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage ClickOnFirstValueInDetailColumn()
        {
            ClickOnElement(firstValueInDetailColumn);
            return this;
        }
        [AllureStep]
        public RoundInstanceEventDetailPage IsHistoryPopup()
        {
            WaitUtil.WaitForAllElementsVisible(titleHistoryPopup);
            Assert.IsTrue(IsControlDisplayed(tagContent));
            Assert.IsTrue(IsControlDisplayed(closeHistoryPopupBtn));
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage VerifyHistoryPopup(string[] historyTitle, string[] values)
        {
            string[] actual = GetElementText(tagContent).Split(Environment.NewLine);
            for (int i = 0; i < historyTitle.Length; i++)
            {
                Assert.AreEqual(historyTitle[i] + ": " + values[i] + ".", actual[i]);
            }
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage ClickOnCloseHistoryPopup()
        {
            ClickOnElement(closeHistoryPopupBtn);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage WaitForRoundEventTypeDisplayed(string v)
        {
            WaitUtil.WaitForElementVisible(roundEventType, v);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage ClickOnDetailsTab()
        {
            ClickOnElement(detailTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage SelectAnyLocation(string locationValue)
        {
            SelectTextFromDropDown(locationDd, locationValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage SelectFirstLocation()
        {
            ClickOnElement(locationDd);
            ClickOnElement(firstLocationOption);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage VerifyLocationIsPopulated(string locationValue)
        {
            Assert.AreEqual(locationValue, GetFirstSelectedItemInDropdown(locationDd), "Location is not populated");
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage ClickOnDataTab()
        {
            ClickOnElement(dataTab);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage InputTicketNumber(string ticketNumberValue)
        {
            SendKeys(ticketNumberInput, ticketNumberValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage InputGrossWeight(string grossWeightValue)
        {
            SendKeys(grossWeightInput, grossWeightValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage ClickAndVerifyAllValueInWasteTypeDd(List<string> allProducts)
        {
            ClickOnElement(wasteTypeDd);
            //Verify
            foreach(string productName in allProducts)
            {
                Assert.IsTrue(IsControlDisplayed(optionInWasteType, productName), productName + " is not displayed in [Waste Type] dd");
            }
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage SelectAnyWasteType(string wasteTypeValue)
        {
            ClickOnElement(optionInWasteType, wasteTypeValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage InputTicketPhoto(string urlImage)
        {
            SendKeysWithUrl(inputImage, urlImage);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage InputNetWeight(string netWeightValue)
        {
            SendKeys(netWeightInput, netWeightValue);
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage VerifyAllValueArePopulatedInForm(string ticketNumber, string grossWeight, string wasteType, string netWeight)
        {
            Assert.AreEqual(ticketNumber, GetAttributeValue(ticketNumberInput, "value"), "[Ticket Number] is not populated");
            Assert.AreEqual(grossWeight, GetAttributeValue(grossWeightInput, "value"), "[Gross Weight] is not populated");
            Assert.AreEqual(wasteType, GetFirstSelectedItemInDropdown(wasteTypeDd), "[Waste Type] is not populated");
            Assert.AreEqual(netWeight, GetAttributeValue(netWeightInput, "value"), "[Net Weight] is not populated");
            return this;
        }

        [AllureStep]
        public RoundInstanceEventDetailPage VerifyValueInWasteTypeIsBlank()
        {
            Assert.AreEqual("", GetFirstSelectedItemInDropdown(wasteTypeDd), "[Waste Type] is not cleared");
            return this;
        }

    }
}
