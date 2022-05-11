using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Adhoc;
using TaskLineModel = si_automated_tests.Source.Main.Models.Adhoc.TaskLinesModel;
using CanlendarServiceTask = si_automated_tests.Source.Main.Models.Suspension.ServiceTaskModel;
using OpenQA.Selenium.Support.UI;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAdHoc
{
    public class TaskLinesPage : BasePage
    {
        private readonly By taskLineRows = By.XPath("//div[@id='taskLines-tab']//table//tbody/tr");
        private readonly By selectType = By.XPath("//select[@id='taskLineType.id']");
        private readonly By itemState = By.XPath("//select[@id='itemState.id']");
        private readonly By echoSelects = By.XPath("//div[@id='taskLines-tab']//table//tbody/tr/td/echo-select/select");
        private readonly By scheduledAssetQuantityInput = By.XPath("//input[@id='scheduledAssetQuantity.id']");
        private readonly By scheduledProductQuantityInput = By.XPath("//input[@id='scheduledProductQuantity.id']");

        public List<TaskLineModel> GetAllTaskLines()
        {
            List<TaskLineModel> taskLines = new List<TaskLineModel>();
            var rows = GetAllElements(taskLineRows);
            foreach (var row in rows)
            {
                TaskLineModel linesModel = new TaskLineModel();
                linesModel.Type = GetSelectedCombobox(row, selectType);
                linesModel.AssetType = GetSelectedCombobox(echoSelects, 0);
                linesModel.ScheduledAssetQty = row.FindElement(scheduledAssetQuantityInput).Text;
                linesModel.Product = GetSelectedCombobox(echoSelects, 1);
                linesModel.ScheduledProductQuantity = row.FindElement(scheduledProductQuantityInput).Text;
                linesModel.Unit = GetSelectedCombobox(echoSelects, 2);
                linesModel.State = GetSelectedCombobox(row, itemState);
                taskLines.Add(linesModel);
            }
            return taskLines;
        }

        public TaskLinesPage VerifyTaskLine()
        {
            TaskLineModel taskLines = GetAllTaskLines().FirstOrDefault();
            Assert.IsTrue(taskLines.Type == "Service");
            Assert.IsTrue(taskLines.AssetType == "1100L");
            Assert.IsTrue(taskLines.ScheduledAssetQty == "4");
            Assert.IsTrue(taskLines.Product == "General Recycling");
            Assert.IsTrue(taskLines.ScheduledProductQuantity == "1000");
            Assert.IsTrue(taskLines.Unit == "Kilograms");
            Assert.IsTrue(taskLines.State == "Unallocated");
            return this;
        }

        private string GetSelectedCombobox(IWebElement webElement, By xpath)
        {
            SelectElement select = new SelectElement(webElement.FindElement(xpath));
            return GetElementText(select.SelectedOption);
        }
        
        private string GetSelectedCombobox(By xpath, int index)
        {
            SelectElement select = new SelectElement(GetAllElements(xpath).ElementAt(index));
            return GetElementText(select.SelectedOption);
        }
    }
}
