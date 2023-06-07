using System;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages
{
    public class TaskDetailTab : BasePage
    {
        private readonly By taskID = By.ClassName("id");

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

        //Calendar
        private readonly string futreDayNumberInCalendar = "(//div[contains(@class,'bootstrap-datetimepicker-widget') and contains(@class,'open') and contains(@style,'z-index:')]//td[contains(@class,'day') and not(contains(@class,'disable')) and text()='{0}'])[last()]";
        private readonly string calendarIcon = "//label[contains(text(),'{0}')]/following-sibling::div//img";
        [AllureStep]
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
        [AllureStep]
        public TaskDetailTab VerifyTaskId(String id)
        {
            Assert.AreEqual(GetElementText(taskID), id);
            return this;
        }
        [AllureStep]
        public TaskDetailTab VerifyDueDate(string expected)
        {
            //Not usable because text is hidden in DOM
            Assert.IsTrue(GetElementText(dueDate).Contains(expected));
            return this;
        }
        [AllureStep]
        public string GetDueDate()
        {
            return GetAttributeValue(dueDate, "value").Substring(0,10);
        }
        [AllureStep]
        public TaskDetailTab VerifyCompletionDate(string date)
        {
            var actual = GetAttributeValue(completionDate, "value");
            Assert.IsTrue(actual.Contains(date), "Expected " + date + " but found " + actual);
            return this;
        }
        [AllureStep]
        public TaskDetailTab VerifyEndDate(string date)
        {
            var actual = GetAttributeValue(endDate, "value");
            Assert.IsTrue(GetAttributeValue(endDate, "value").Contains(date), "Expected " + date + " but found " + actual);
            return this;
        }
        [AllureStep]
        public TaskDetailTab VerifyTaskState(string _state)
        {
            string state = GetFirstSelectedItemInDropdown(detailTaskState);
            Assert.AreEqual(_state, state);
            return this;
        }
        [AllureStep]
        public TaskDetailTab VerifyNote(string note)
        {
            Assert.IsTrue(GetAttributeValue(taskNote, "value").Contains(note));
            return this;
        }
        [AllureStep]
        public TaskDetailTab ClickStateDetais()
        {
            ClickOnElement(detailTaskState);
            Thread.Sleep(1000);
            return this;
        }
        [AllureStep]
        public TaskDetailTab ChooseTaskState(string status)
        {

            ClickOnElement(detailsTaskStateOption, status);
            Thread.Sleep(1000);
            return this;
        }
        [AllureStep]
        public TaskDetailTab InputReferenceValue(string value)
        {
            SendKeys(taskRefInput, value);
            ClickOnElement(taskRefText);
            return this;
        }
        [AllureStep]
        public TaskDetailTab VerifyReferenceValue(string value)
        {
            Assert.AreEqual(GetAttributeValue(taskRefInput, "value"), value);
            return this;
        }
        [AllureStep]
        public TaskDetailTab InputPurchaseOrderValue(string value)
        {
            SendKeys(purchaseOrderNumberInput, value);
            return this;
        }
        [AllureStep]
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
        [AllureStep]
        public TaskDetailTab VerifyPurchaseOrderValueNotPresent(string po)
        {

            WaitUtil.WaitForElementInvisible(purchaseOrderValue, po);
            Assert.IsTrue(IsControlUnDisplayed(purchaseOrderValue, po));
            return this;
        }
        [AllureStep]
        public TaskDetailTab SelectDateFromCalendar(string field, string date)
        {
            ClickOnElement(calendarIcon, field);
            ClickOnElement(futreDayNumberInCalendar, date);
            return this;
        }
    }
}
