using System;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Task;

namespace si_automated_tests.Source.Main.Pages
{
    public class TaskDetailTab : BasePage
    {
        private readonly By dueDate = By.Id("dueDate.id");

        public TaskDetailTab VerifyDueDate(string expected)
        {
            //Not usable because text is hidden in DOM
            Assert.IsTrue(GetElementText(dueDate).Contains(expected));
            return this;
        }
    }
}
