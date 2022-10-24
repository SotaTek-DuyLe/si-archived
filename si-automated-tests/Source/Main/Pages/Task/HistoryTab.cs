using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Task
{
    public class HistoryTab : BasePage
    {
        private string UpdateTaskValue = "//strong[text()='Update']//following-sibling::div[contains(.,'{0}')]";
        private string UpdateTaskTime = "//strong[text()='Update']/parent::div/following-sibling::div/strong[contains(text(),'{0}')]";

        private string CreatedTaskValue = "//strong[text()='Created']//following-sibling::div[contains(.,'{0}')]";
        private string CreatedTaskTime = "//strong[text()='Created']/parent::div/following-sibling::div/strong[contains(text(),'{0}')]";

        [AllureStep]
        public HistoryTab VerifyUpdateTaskTimeAndValue(string value, string time)
        {
            WaitUtil.WaitForElementVisible(UpdateTaskValue, value);
            WaitUtil.WaitForElementVisible(UpdateTaskTime, time);
            Assert.IsTrue(IsControlDisplayed(UpdateTaskValue, value));
            Assert.IsTrue(IsControlDisplayed(UpdateTaskTime, time));
            return this;
        }
        [AllureStep]
        public HistoryTab VerifyCreatedTaskTimeAndValue(string value, string time)
        {
            WaitUtil.WaitForElementVisible(CreatedTaskValue, value);
            WaitUtil.WaitForElementVisible(CreatedTaskTime, time);
            Assert.IsTrue(IsControlDisplayed(CreatedTaskValue, value));
            Assert.IsTrue(IsControlDisplayed(CreatedTaskTime, time));
            return this;
        }
    }
}
