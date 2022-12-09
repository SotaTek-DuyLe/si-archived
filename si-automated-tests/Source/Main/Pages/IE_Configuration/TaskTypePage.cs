using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.IE_Configuration
{
    public class TaskTypePage : BasePage
    {
        private readonly By taskLineToTaskMappingTab = By.XPath("//a[text()='TaskLines to Task Mapping']");

        public TaskTypePage SwitchTab()
        {
            ClickOnElement(taskLineToTaskMappingTab);
            return this;
        }
        public TaskTypePage IsOnTaskLineToTaskMappingTab()
        {
            return this;
        }
    }
}
