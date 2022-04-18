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
        private readonly By dueDate = By.Id("dueDate.id");
        private readonly By completionDate = By.Id("completionDate.id");
        private readonly By endDate = By.Id("endDate.id");
        private readonly By detailTaskState = By.Id("taskState.id");
        private readonly By taskNote = By.Id("taskNotes.id");

        private string detailsTaskStateOption = "//select[@name='taskState']//option[text()='{0}']";

        public TaskDetailTab VerifyDueDate(string expected)
        {
            //Not usable because text is hidden in DOM
            Assert.IsTrue(GetElementText(dueDate).Contains(expected));
            return this;
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


    }
}
