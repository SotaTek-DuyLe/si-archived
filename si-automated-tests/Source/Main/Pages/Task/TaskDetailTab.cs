using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Task;

namespace si_automated_tests.Source.Main.Pages
{
    public class TaskDetailTab : BasePage
    {
        private readonly By taskTypeURL = By.XPath("//a[@class='typeUrl']");
        private readonly By taskImage = By.XPath("//img[@src='/web/content/images/form/save.svg']");
        private string taskType = "//span[text()='Task']";
        private string taskTypeName = "//span[text()='Task']/following-sibling::span";

        private readonly By taskRefInput = By.XPath("//label[contains(text(),'Task Reference')]/following-sibling::input");
        private readonly By taskRefText = By.XPath("//label[contains(text(),'Task Reference')]");

        private readonly By dueDate = By.Id("dueDate.id");
        private readonly By completionDate = By.Id("completionDate.id");
        private readonly By endDate = By.Id("endDate.id");
        public readonly By detailTaskState = By.Id("taskState.id");
        private readonly By taskNote = By.Id("taskNotes.id");
        private readonly By purchaseOrderNumberInput = By.Id("purchaseOrderNumber");

        private string detailsTaskStateOption = "//select[@name='taskState']//option[text()='{0}']";

        private string purchaseOrderValue = "//div[text()='Purchase Order #']/following-sibling::div[text()='{0}']";
        
        public TaskDetailTab IsOnTaskDetailTab()
        {
            WaitUtil.WaitForElementVisible(taskTypeURL);
            WaitUtil.WaitForElementVisible(taskImage);
            WaitUtil.WaitForElementVisible(taskType);
            WaitUtil.WaitForElementVisible(taskTypeName);
            int i = 5;
            while(i > 0)
            {
                if (GetElementText(taskTypeName).Equals(""))
                {
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else
                {
                    break;
                }
            }
            Assert.IsTrue(IsControlDisplayed(taskTypeURL));
            Assert.IsTrue(IsControlDisplayed(taskImage));
            Assert.IsTrue(IsControlDisplayed(taskType));
            Assert.IsTrue(IsControlDisplayed(taskTypeName));
            return this;
        }
        public TaskDetailTab VerifyDueDate(string expected)
        {
            //Not usable because text is hidden in DOM
            Assert.IsTrue(GetElementText(dueDate).Contains(expected));
            return this;
        }
        public string GetDueDate()
        {
            return GetAttributeValue(dueDate, "value").Substring(0,9);
        }
        public TaskDetailTab VerifyCompletionDate(string date)
        {
            Assert.IsTrue(GetAttributeValue(completionDate,"value").Contains(date));
            return this;
        }
        public TaskDetailTab VerifyEndDate(string date)
        {
            Assert.IsTrue(GetAttributeValue(endDate, "value").Contains(date));
            return this;
        }
        public TaskDetailTab VerifyTaskState(string _state)
        {
            string state = GetFirstSelectedItemInDropdown(detailTaskState);
            Assert.AreEqual(_state, state);
            return this;
        }
        public TaskDetailTab VerifyNote(string note)
        {
            Assert.IsTrue(GetAttributeValue(taskNote, "value").Contains(note));
            return this;
        }
        public TaskDetailTab ClickStateDetais()
        {
            ClickOnElement(detailTaskState);
            Thread.Sleep(1000);
            return this;
        }
        public TaskDetailTab ChooseTaskState(string status)
        {

            ClickOnElement(detailsTaskStateOption, status);
            Thread.Sleep(1000);
            return this;
        }
        public TaskDetailTab InputReferenceValue(string value)
        {
            SendKeys(taskRefInput, value);
            ClickOnElement(taskRefText);
            return this;
        }
        public TaskDetailTab VerifyReferenceValue(string value)
        {
            Assert.AreEqual(GetAttributeValue(taskRefInput, "value"), value);
            return this;
        }
        public TaskDetailTab InputPurchaseOrderValue(string value)
        {
            SendKeys(purchaseOrderNumberInput, value);
            return this;
        }
        public TaskDetailTab VerifyPurchaseOrderValueAtInput(string value)
        {
            Assert.AreEqual(GetAttributeValue(purchaseOrderNumberInput, "value"), value);
            return this;
        }
        public TaskDetailTab VerifyPurchaseOrderValue(string po)
        {

            WaitUtil.WaitForElementVisible(purchaseOrderValue, po);
            Assert.IsTrue(IsControlDisplayed(purchaseOrderValue, po));
            return this;
        }
        public TaskDetailTab VerifyPurchaseOrderValueNotPresent(string po)
        {

            WaitUtil.WaitForElementInvisible(purchaseOrderValue, po);
            Assert.IsTrue(IsControlUnDisplayed(purchaseOrderValue, po));
            return this;
        }
    }
}
