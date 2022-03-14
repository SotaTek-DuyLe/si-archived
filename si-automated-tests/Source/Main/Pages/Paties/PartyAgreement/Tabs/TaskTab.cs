using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.PartyAgreement.Tabs
{
    public class TaskTab : BasePage
    {
        private readonly By taskType = By.XPath("//div[@class='slick-cell l11 r11']");
        private readonly By taskDueDate = By.XPath("//div[@class='slick-cell l13 r13']");

        public TaskTab VerifyFirstTaskType(string expected)
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            Assert.AreEqual(expected, GetElementText(listTaskType[0]));
            return this;
        }
        public TaskTab VerifyFirstTaskDueDate(string expected)
        {
            IList<IWebElement> listTaskDueDate = WaitUtil.WaitForAllElementsVisible(taskDueDate);
            Assert.IsTrue(GetElementText(listTaskDueDate[0]).Contains(expected));
            return this;
        }
        public TaskTab VerifySecondTaskType(string expected)
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            Assert.AreEqual(expected, GetElementText(listTaskType[1]));
            return this;
        }
        public TaskTab VerifySecondTaskDueDate(string expected)
        {
            IList<IWebElement> listTaskDueDate = WaitUtil.WaitForAllElementsVisible(taskDueDate);
            Assert.IsTrue(GetElementText(listTaskDueDate[1]).Contains(expected));
            return this;
        }
        public TaskTab OpenFirstTask()
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            DoubleClickOnElement(listTaskType[0]);
            return this;
        }
        public TaskTab OpenSecondTask()
        {
            IList<IWebElement> listTaskType = WaitUtil.WaitForAllElementsVisible(taskType);
            DoubleClickOnElement(listTaskType[1]);
            return this;
        }
    }
}
