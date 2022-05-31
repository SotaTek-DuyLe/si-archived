using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace si_automated_tests.Source.Main.Pages.Services
{
    public class RoundGroupPage : BasePage
    {
        private readonly By roundGroupInput = By.XPath("//div[@id='details-tab']//input[@name='roundGroup']");
        private readonly By sortOrderInput = By.XPath("//div[@id='details-tab']//input[@name='sortOrder']");
        private readonly By dispatchSiteSelect = By.XPath("//div[@id='details-tab']//select[@id='dispatchSite.id']");
        private readonly By dispatchSiteSelectOpt = By.XPath("//div[@id='details-tab']//select[@id='dispatchSite.id']//option");
        private readonly By generationOfRoundInstanceSelect = By.XPath("//div[@id='details-tab']//select[@id='generationOfRoundInstance.id']");
        private readonly By defaultWorkSheetSelect = By.XPath("//div[@id='details-tab']//select[@id='defaultWorkSheet.id']");
        private readonly By recordWorkingTimeSelect = By.XPath("//div[@id='details-tab']//select[@id='recordWorkingTime.id']");
        private readonly By copyBtn = By.XPath("//button[@title='Copy']");
        private readonly By optimiseBtn = By.XPath("//button[@title='Optimise']");
        private readonly By roundTab = By.XPath("//a[@aria-controls='rounds-tab']");
        private readonly By addNewItemBtn = By.XPath("//div[@id='rounds-tab']//button");
        private readonly By roundRows = By.XPath("//div[@id='rounds-tab']//table//tbody//tr");

        public RoundGroupPage VerifyDefaultDataOnAddForm()
        {
            Assert.IsEmpty(GetElement(roundGroupInput).GetAttribute("value"));
            Assert.IsEmpty(GetElement(sortOrderInput).GetAttribute("value"));
            Assert.IsEmpty(GetFirstSelectedItemInDropdown(dispatchSiteSelect));
            Assert.IsTrue(GetFirstSelectedItemInDropdown(generationOfRoundInstanceSelect) == "Default");
            Assert.IsTrue(GetFirstSelectedItemInDropdown(defaultWorkSheetSelect) == "Crew Worksheet");
            Assert.IsTrue(GetFirstSelectedItemInDropdown(recordWorkingTimeSelect) == "Default");
            return this;
        }

        public RoundGroupPage VerifyRoundGroup(string value)
        {
            Assert.IsTrue(GetElement(roundGroupInput).GetAttribute("value") == value);
            return this;
        }

        public RoundGroupPage ClickOnDispatchSiteAndVerifyData()
        {
            ClickOnElement(dispatchSiteSelect);
            List<string> options = GetAllElements(dispatchSiteSelectOpt).Select(x => x.Text).Where(x => !string.IsNullOrEmpty(x)).ToList();
            Assert.AreEqual(new List<string>() { "Kingston Tip", "Townmead Tip & Depot (East)", "Townmead Weighbridge" }, options);
            return this;
        }

        public RoundGroupPage SelectDispatchSite(string value)
        {
            SelectTextFromDropDown(dispatchSiteSelect, value);
            return this;
        }

        public RoundGroupPage EnterRoundGroupValue(string value)
        {
            SendKeys(roundGroupInput, value);
            return this;
        }

        public RoundGroupPage VerifyServiceButtonsVisible()
        {
            WaitUtil.WaitForElementVisible(copyBtn);
            Assert.IsTrue(IsControlDisplayed(copyBtn));
            Assert.IsTrue(IsControlDisplayed(optimiseBtn));
            return this;
        }

        public RoundGroupPage ClickRoundTab()
        {
            ClickOnElement(roundTab);
            return this;
        }

        public RoundGroupPage IsRoundTab()
        {
            Assert.IsTrue(IsControlDisplayed(roundTab));
            Assert.IsTrue(IsControlDisplayed(roundRows));
            return this;
        }

        public RoundGroupPage ClickAddNewItem()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }

        public int GetIndexNewRoundRow()
        {
            return GetAllElements(roundRows).Count - 1;
        }

        public RoundGroupPage EnterRoundValue(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            SendKeys(webElement.FindElement(By.XPath("./td/input[@id='round.id']")), value);
            return this;
        }

        public RoundGroupPage EnterRoundTypeValue(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            IWebElement select = webElement.FindElement(By.XPath("./td/select[@id='roundType.id']"));
            SelectElement selectedValue = new SelectElement(select);
            selectedValue.SelectByText(value);
            return this;
        }

        public RoundGroupPage EnterDispatchSiteValue(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            IWebElement select = webElement.FindElement(By.XPath("./td/select[@id='dispatchSite.id']"));
            SelectElement selectedValue = new SelectElement(select);
            selectedValue.SelectByText(value);
            return this;
        }

        public RoundGroupPage EnterShiftValue(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            IWebElement select = webElement.FindElement(By.XPath("./td/select[@id='shift.id']"));
            SelectElement selectedValue = new SelectElement(select);
            selectedValue.SelectByText(value);
            return this;
        }

        public RoundGroupPage VerifyRoundColor(int rowIdx, string value)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            IWebElement input = webElement.FindElement(By.XPath("./td/input[@name='color']"));
            Assert.IsTrue(input.GetAttribute("value") == value);
            return this;
        }

        public RoundGroupPage DoubleClickRound(int rowIdx)
        {
            IWebElement webElement = GetAllElements(roundRows)[rowIdx];
            DoubleClickOnElement(webElement);
            return this;
        }
    }
}
