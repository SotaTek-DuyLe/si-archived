using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundDetailPage : BasePageCommonActions
    {
        private readonly By title = By.XPath("//span[text()='Round']");
        private readonly By roundInput = By.XPath("//div[@id='details-tab']//input[@name='round']");
        private readonly By roundTypeSelect = By.XPath("//div[@id='details-tab']//select[@id='roundType.id']");
        private readonly By dispatchSiteSelect = By.XPath("//div[@id='details-tab']//select[@id='dispatchSite.id']");
        private readonly By shiftSelect = By.XPath("//div[@id='details-tab']//select[@id='shift.id']");
        private const string AllTabDisplayed = "//li[@role='presentation' and not(contains(@style, 'visibility: collapse'))]/a";
        private const string FrameMessage = "//div[@class='notifyjs-corner']/div";
        private readonly By defaultResourceTab = By.XPath("//a[@aria-controls='defaultResources-tab']");
        public readonly By DetailTab = By.XPath("//a[@aria-controls='details-tab']");
        private readonly By toggleBtn = By.XPath("./td//div[@id='toggle-actions']");
        private readonly By defaultResourceRows = By.XPath("//div[@id='defaultResources-tab']//table//tbody//tr[contains(@data-bind, 'with: $data.getFields()')][not(ancestor::tr)]");
        private readonly By resourceDetailRows = By.XPath("//div[@id='defaultResources-tab']//table//tbody//tr[contains(@class, 'child-container-row')]");
        private readonly By typeSelect = By.XPath("./td//select[@id='type.id']");
        private readonly By inputQuantity = By.XPath("./td//input[@id='quantity.id']");
        private readonly By resourceSelect = By.XPath("./td//select[@id='resource.id']");
        private readonly By resourceSelectOpt = By.XPath("./td//select[@id='resource.id']//option");
        private readonly By hasScheduleCheckbox = By.XPath("./td//input[@type='checkbox']");
        private readonly By scheduleInput = By.XPath("./td//input[@id='schedule.id']");
        private readonly By retireBtn = By.XPath("./td//button[@title='Retire']");
        private readonly By editBtn = By.XPath("./td//button[@title='Edit']");
        private readonly By roundGroupHyperLink = By.XPath("//a[@class='typeUrl']");
        private readonly By contractUnit = By.Id("contractUnit.id");

        //SCHEDULES TAB
        private readonly By scheduleTab = By.CssSelector("a[aria-controls='schedules-tab']");
        private readonly By patternStartInput = By.XPath("//div[@id='schedules-tab']//input[@id='startDate.id']");
        private readonly By patternEndInput = By.XPath("//div[@id='schedules-tab']//input[@id='endDate.id']");

        private readonly By slotCountInput = By.CssSelector("input[id='slots.id']");

        [AllureStep]
        public RoundDetailPage IsRoundDetailPage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(DetailTab);
            return this;
        }

        [AllureStep]
        public RoundDetailPage VerifyRoundInput(string expectedValue)
        {
            VerifyInputValue(roundInput, expectedValue);
            return this;
        }
        [AllureStep]
        public RoundDetailPage VerifyRoundType(string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(roundTypeSelect) == expectedValue);
            return this;
        }
        [AllureStep]
        public RoundDetailPage VerifyDispatchSite(string expectedValue)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(dispatchSiteSelect),expectedValue);
            return this;
        }
        [AllureStep]
        public RoundDetailPage VerifyShift(string expectedValue)
        {
            int result = String.Compare(GetFirstSelectedItemInDropdown(shiftSelect), expectedValue, CultureInfo.CurrentCulture, CompareOptions.IgnoreSymbols);
            Assert.AreEqual(0, result);
            return this;
        }
        [AllureStep]
        public RoundDetailPage ClickAllTabAndVerify()
        {
            List<IWebElement> allElements = GetAllElements(AllTabDisplayed);
            int clickButtonIdx = 1;
            while (clickButtonIdx < allElements.Count)
            {
                ClickOnElement(allElements[clickButtonIdx]);
                clickButtonIdx++;
                WaitForLoadingIconToDisappear();
                Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
            }
            return this;
        }
        [AllureStep]
        public RoundDetailPage ClickDefaultResourceTab()
        {
            ClickOnElement(defaultResourceTab);
            return this;
        }
        [AllureStep]
        public RoundDetailPage ClickExpandButton(int rowIdx)
        {
            IWebElement webElement = GetAllElements(defaultResourceRows)[rowIdx];
            ClickOnElement(webElement.FindElement(toggleBtn));
            return this;
        }
        [AllureStep]
        public List<DefaultResourceModel> GetAllDefaultResourceModels()
        {
            List<DefaultResourceModel> defaultResources = new List<DefaultResourceModel>();
            List<IWebElement> webElements = GetAllElements(defaultResourceRows);
            List<IWebElement> webDetailElements = this.driver.FindElements(resourceDetailRows).ToList();
            for (int i = 0; i < webElements.Count; i++)
            {
                DefaultResourceModel defaultResource = new DefaultResourceModel();
                defaultResource.Type = GetFirstSelectedItemInDropdown(webElements[i].FindElement(typeSelect));
                defaultResource.Quantity = webElements[i].FindElement(inputQuantity).GetAttribute("value");
                bool containDetail = webDetailElements[i].FindElements(resourceSelect).Count != 0;
                if (containDetail)
                {
                    if (!webDetailElements[i].Displayed)
                    {
                        ClickExpandButton(i);
                        Thread.Sleep(300);
                    }
                    defaultResource.Detail = new DetailDefaultResourceModel()
                    {
                        Resource = GetFirstSelectedItemInDropdown(webDetailElements[i].FindElement(resourceSelect)),
                        HasSchedule = webDetailElements[i].FindElement(hasScheduleCheckbox).Selected,
                        Schedule = webDetailElements[i].FindElement(scheduleInput).GetAttribute("value"),
                    };
                }
                defaultResources.Add(defaultResource);
            }
            return defaultResources;
        }
        [AllureStep]
        public RoundDetailPage IsDefaultResourceSync(List<DefaultResourceModel> expected, List<DefaultResourceModel> actual)
        {
            Assert.That(actual, Is.EquivalentTo(expected));
            return this;
        }
        [AllureStep]
        public string GetRoundName()
        {
            WaitUtil.WaitForTextToDisappearInElement(roundGroupHyperLink, "");
            return GetElementText(roundGroupHyperLink);
        }
        [AllureStep]
        public RoundGroupPage ClickRoundGroupHyperLink()
        {
            WaitUtil.WaitForElementSize(roundGroupHyperLink);
            ClickOnElement(roundGroupHyperLink);
            return PageFactoryManager.Get<RoundGroupPage>();
        }
        [AllureStep]
        public RoundDetailPage VerifyContractUnit(string expected)
        {
            Assert.AreEqual(GetFirstSelectedItemInDropdown(contractUnit),expected);

            return this;
        }

        [AllureStep]
        public RoundDetailPage VerifyMinValueInSlotCountField()
        {
            Assert.AreEqual("0", GetAttributeValue(slotCountInput, "min"));
            return this;
        }

        [AllureStep]
        public RoundDetailPage InputSlotCount(string slotCountValue)
        {
            SendKeys(slotCountInput, slotCountValue);
            return this;
        }

        [AllureStep]
        public RoundDetailPage ClearSlotCount()
        {
            ClearInputValue(slotCountInput);
            return this;
        }

        [AllureStep]
        public RoundDetailPage VerifyValueInSlotCount(string slotCountValue)
        {
            Assert.AreEqual(slotCountValue, GetAttributeValue(slotCountInput, "value"));
            return this;
        }

        [AllureStep]
        public RoundDetailPage ClickOnSchedulesTab()
        {
            ClickOnElement(scheduleTab);
            waitForLoadingIconDisappear();
            return this;
        }

        [AllureStep]
        public RoundDetailPage InputPatternDateSchedulesTab(string startDateValue, string endDateValue)
        {
            InputCalendarDate(patternStartInput, startDateValue);
            InputCalendarDate(patternEndInput, endDateValue);
            return this;
        }
    }
}
