using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundDetailPage : BasePage
    {
        private readonly By roundInput = By.XPath("//div[@id='details-tab']//input[@name='round']");
        private readonly By roundTypeSelect = By.XPath("//div[@id='details-tab']//select[@id='roundType.id']");
        private readonly By dispatchSiteSelect = By.XPath("//div[@id='details-tab']//select[@id='dispatchSite.id']");
        private readonly By shiftSelect = By.XPath("//div[@id='details-tab']//select[@id='shift.id']");
        private const string AllTabDisplayed = "//li[@role='presentation' and not(contains(@style, 'visibility: collapse'))]/a";
        private const string FrameMessage = "//div[@class='notifyjs-corner']/div";
        private readonly By defaultResourceTab = By.XPath("//a[@aria-controls='defaultResources-tab']");
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

        public RoundDetailPage VerifyRoundInput(string expectedValue)
        {
            Assert.IsTrue(GetElement(roundInput).GetAttribute("value") == expectedValue);
            return this;
        }

        public RoundDetailPage VerifyRoundType(string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(roundTypeSelect) == expectedValue);
            return this;
        }

        public RoundDetailPage VerifyDispatchSite(string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(dispatchSiteSelect) == expectedValue);
            return this;
        }

        public RoundDetailPage VerifyShift(string expectedValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(shiftSelect) == expectedValue);
            return this;
        }

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

        public RoundDetailPage ClickDefaultResourceTab()
        {
            ClickOnElement(defaultResourceTab);
            return this;
        }

        public RoundDetailPage ClickExpandButton(int rowIdx)
        {
            IWebElement webElement = GetAllElements(defaultResourceRows)[rowIdx];
            ClickOnElement(webElement.FindElement(toggleBtn));
            return this;
        }

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

        public RoundDetailPage IsDefaultResourceSync(List<DefaultResourceModel> expected, List<DefaultResourceModel> actual)
        {
            Assert.That(actual, Is.EquivalentTo(expected));
            return this;
        }
    }
}
