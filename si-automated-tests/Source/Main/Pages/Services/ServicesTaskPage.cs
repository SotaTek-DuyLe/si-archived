using System;
using System.Collections.Generic;
using System.Text;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServicesTaskPage : BasePage
    {
        private string taskLineTab = "//a[@aria-controls='tasklines-tab']";
        private string scheduleTab = "//a[@aria-controls='schedules-tab']";
        private string closeWithoutSavingBtn = "//button[@title='Close Without Saving']";

        public ServicesTaskPage ClickOnTaskLineTab()
        {
            ClickOnElement(taskLineTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        public ServicesTaskPage ClickOnScheduleTask()
        {
            ClickOnElement(scheduleTab);
            return this;
        }

        public ServicesTaskPage CloseWithoutSaving()
        {
            ClickOnElement(closeWithoutSavingBtn);
            return this;
        }
    }
}
