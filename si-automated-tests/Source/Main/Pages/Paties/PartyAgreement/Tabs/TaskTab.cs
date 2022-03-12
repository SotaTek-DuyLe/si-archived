using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.PartyAgreement.Tabs
{
    public class TaskTab : BasePage
    {
        private readonly By firstTaskType = By.XPath("//div[@class='slick-cell l11 r11']");
        private readonly By firstTaskDueDate = By.XPath("//div[@class='slick-cell l13 r13']");

        public TaskTab VerifyFirstTaskType(string expected)
        {
            Assert.AreEqual(expected, GetElementText(firstTaskType));
            return this;
        }
        public TaskTab VerifyFirstTaskDueDate(string expected)
        {
            Assert.IsTrue(GetElementText(firstTaskDueDate).Contains(expected));
            return this;
        }
        public TaskTab OpenFirstTask()
        {
            DoubleClickOnElement(firstTaskType);
            return this;
        }
    }
}
