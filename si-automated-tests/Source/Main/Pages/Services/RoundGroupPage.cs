using NUnit.Framework;
using OpenQA.Selenium;
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
    }
}
