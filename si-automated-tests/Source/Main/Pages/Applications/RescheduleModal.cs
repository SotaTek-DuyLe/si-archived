using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RescheduleModal : BasePageCommonActions
    {
        public readonly By roundInstanceRow = By.XPath("//div[@id='reschedule-modal']//table//tbody//tr");
        public readonly By InputRescheduleDate = By.XPath("//div[@id='reschedule-modal']//input[@id='selectedRescheduleDate']");
        public readonly By ButtonCancel = By.XPath("//div[@id='reschedule-modal']//button[text()='Cancel']");
        public readonly By ButtonOk = By.XPath("//div[@id='reschedule-modal']//button[text()='OK']");
        public readonly By ButtonReschedule = By.XPath("//button[text()='Reschedule']");

        public RescheduleModal IsRescheduleModelDisplayedCorrectly()
        {
            IWebElement row = GetElement(roundInstanceRow);
            List<IWebElement> cells = row.FindElements(By.XPath("./td")).ToList();
            if (cells.Count > 1)
            {
                //PlannedDate
                Assert.IsTrue(DateTime.Now.ToString("dd/MM/yyyy") == cells[1].Text);
            }
            if (cells.Count > 2)
            {
                //CurrentDate
                Assert.IsTrue(DateTime.Now.ToString("dd/MM/yyyy") == cells[2].Text);
            }
            if (cells.Count > 3)
            {
                IWebElement button = cells[3].FindElement(By.XPath("./button[text()='Remove']"));
                Assert.IsTrue(button.Enabled);
            }

            string borderColor = GetElement(InputRescheduleDate).GetCssValue("border-color");
            Assert.IsTrue(borderColor == "rgb(169, 68, 66)");
            Assert.IsTrue(IsControlDisplayed(ButtonCancel));
            Assert.IsTrue(IsControlDisplayed(ButtonOk));
            return this;
        }
    }
}
