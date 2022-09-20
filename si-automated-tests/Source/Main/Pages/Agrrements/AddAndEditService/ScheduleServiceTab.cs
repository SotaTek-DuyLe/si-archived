using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService
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
        private readonly By onceEveryDay = By.XPath("//a[text()='Once Every day']");
        private readonly By weeklyBtn = By.XPath("//span[text()='Weekly']");

        private readonly By startDateInput = By.XPath("//div[@data-bind='visible: recurrenceType() == 1']//label[text()='Starting on']/following-sibling::div/span/span");

        //Dynamic locator
        private string scheduleLink = "//a[text()='{0}']";

        [AllureStep]
        public ScheduleServiceTab IsOnScheduleTab()
        {
            WaitUtil.WaitForElementVisible(addAssetScheduleBtn);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab ClickAddService()
        {
            ClickOnElement(addAssetScheduleBtn);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab ClickDoneScheduleBtn()
        {
            ClickOnElement(doneAssetScheduleBtn);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab ClickDoneCurrentScheduleBtn()
        {
            ScrollDownToElement(doneCurrentAssetScheduleBtn);
            ClickOnElement(doneCurrentAssetScheduleBtn);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab ClickDoneRequirementBtn()
        {
            ScrollDownToElement(doneScheduleReqBtn);
            ClickOnElement(doneScheduleReqBtn);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab VerifyAssetSummary(int quantity, string type, int productQuantity, string product)
        {
            var text1 = String.Format("{0} x {1}, {2}", quantity.ToString(), type, productQuantity.ToString());
            Assert.IsTrue(GetElementText(summaryText).Contains(text1));
            Assert.IsTrue(GetElementText(summaryText).Contains(product));
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab ClickAddScheduleRequirement()
        {
            ClickOnElement(addScheduelBtn);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab SelectFrequencyOption(string option)
        {
            ClickOnElement(frequencyOption,option);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab UntickAnyDayOption()
        {
            if (IsElementSelected(anyDayOption)) ClickOnElement(anyDayOption);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab TickAnyDayOption()
        {
            if (!IsElementSelected(anyDayOption)) ClickOnElement(anyDayOption);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab SelectDayOfWeek(string day)
        {
            ClickOnElement(dayOption, day);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab VerifyScheduleSummary(string expected)
        {
            Assert.AreEqual(expected, GetElementText(scheduleSummaryText));
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab ClickOnNotSetLink()
        {
            ScrollDownToElement(notSetLink);
            ClickOnElement(notSetLink);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab ClickOnWeeklyBtn()
        {
            ClickOnElement(weeklyBtn);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab VerifyScheduleOnceEveryDay()
        {
            WaitUtil.WaitForElementVisible(onceEveryDay);
            Assert.IsTrue(IsControlDisplayed(onceEveryDay));
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab InputStartDate(string date)
        {
            EditSendKeys(startDateInput, date);
            //SendKeys(startDateInput, date);
            return this;
        }
        [AllureStep]
        public ScheduleServiceTab ClickOnSchedule(string schedule)
        {
            ClickOnElement(scheduleLink, schedule);
            return this;
        }

    }
}
