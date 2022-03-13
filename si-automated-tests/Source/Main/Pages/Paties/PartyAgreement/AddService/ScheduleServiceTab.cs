using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.PartyAgreement.AddService
{
    public class ScheduleServiceTab : AddServicePage
    {
        private readonly By addAssetScheduleBtn = By.XPath("//button[contains(@data-bind,'addAssetSchedule')]");
        private readonly By doneAssetScheduleBtn = By.XPath("//button[contains(@data-bind,'finishAssetSchedule')]");
        private readonly By doneCurrentAssetScheduleBtn = By.XPath("//div[@id='asset-selection']//button[contains(@data-bind,'finishAssetSchedule')]");
        private readonly By doneScheduleReqBtn = By.XPath("//button[contains(@data-bind,'finishScheduleRequirement')]");
        //private readonly By doneAssetScheduleBtn = By.XPath("//button[text()='Done']");
        private readonly By summaryText = By.XPath("//div[@data-bind='text: description']");
        private readonly By addScheduelBtn = By.XPath("//button[contains(@data-bind,'addScheduleRequirement')]");
        private readonly string frequencyOption = "//span[text()='{0}']";
        private readonly By anyDayOption = By.Id("any-day");
        private readonly string dayOption = "//span[@data-bind='text: shortDayName' and text()='{0}']/parent::button";
        private readonly string scheduleSummaryText = "//a[contains(@data-bind,'getScheduleRequirementDescription')]";

        //Regular Service locator
        private readonly By notSetLink = By.XPath("//a[contains(text(),'Not set')]");
        private readonly By weeklyBtn = By.XPath("//span[text()='Weekly']");



        public ScheduleServiceTab IsOnScheduleTab()
        {
            WaitUtil.WaitForElementVisible(addAssetScheduleBtn);
            return this;
        }
        public ScheduleServiceTab ClickAddService()
        {
            ClickOnElement(addAssetScheduleBtn);
            return this;
        }
        public ScheduleServiceTab ClickDoneScheduleBtn()
        {
            ClickOnElement(doneAssetScheduleBtn);
            return this;
        }
        public ScheduleServiceTab ClickDoneCurrentScheduleBtn()
        {
            ScrollDownToElement(doneCurrentAssetScheduleBtn);
            ClickOnElement(doneCurrentAssetScheduleBtn);
            return this;
        }
        public ScheduleServiceTab ClickDoneRequirementBtn()
        {
            ScrollDownToElement(doneScheduleReqBtn);
            ClickOnElement(doneScheduleReqBtn);
            return this;
        }
        public ScheduleServiceTab VerifyAssetSummary(int quantity, string type, int productQuantity, string product)
        {
            var text1 = String.Format("{0} x {1}, {2}", quantity.ToString(), type, productQuantity.ToString());
            Assert.IsTrue(GetElementText(summaryText).Contains(text1));
            Assert.IsTrue(GetElementText(summaryText).Contains(product));
            return this;
        }
        public ScheduleServiceTab ClickAddScheduleRequirement()
        {
            ClickOnElement(addScheduelBtn);
            return this;
        }
        public ScheduleServiceTab SelectFrequencyOption(string option)
        {
            ClickOnElement(frequencyOption,option);
            return this;
        }
        public ScheduleServiceTab UntickAnyDayOption()
        {
            if (IsElementSelected(anyDayOption)) ClickOnElement(anyDayOption);
            return this;
        }
        public ScheduleServiceTab SelectDayOfWeek(string day)
        {
            ClickOnElement(dayOption, day);
            return this;
        }
        public ScheduleServiceTab VerifyScheduleSummary(string expected)
        {
            Assert.AreEqual(expected, GetElementText(scheduleSummaryText));
            return this;
        }
        public ScheduleServiceTab ClickOnNotSetLink()
        {
            ScrollDownToElement(notSetLink);
            ClickOnElement(notSetLink);
            return this;
        }

        public ScheduleServiceTab ClickOnWeeklyBtn()
        {
            ClickOnElement(weeklyBtn);
            return this;
        }
    }
}
