using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Task
{
    public class TaskTabNavigation : BasePage
    {
        private readonly By taskLineTab = By.XPath("//a[contains(text(),'Task Lines')]");
        public TaskTabNavigation OpenTaskDetailTab()
        {

            return this;
        }
        public TaskTabNavigation OpenDataTab()
        {

            return this;
        }
        public TaskTabNavigation OpenSourceDataTab()
        {

            return this;
        }
        public TaskLineTab OpenTaskLineTab()
        {
            ClickOnElement(taskLineTab);
            return PageFactoryManager.Get<TaskLineTab>();
        }
        public TaskTabNavigation OpenHistoryTab()
        {

            return this;
        }
        public TaskTabNavigation OpenSubscriptionTab()
        {

            return this;
        }
        public TaskTabNavigation OpenNotificationTab()
        {

            return this;
        }
        public TaskTabNavigation OpenInspectionTab()
        {

            return this;
        }
        public TaskTabNavigation OpenBillingTab()
        {

            return this;
        }
        public TaskTabNavigation OpenVerdictTab()
        {

            return this;
        }
        public TaskTabNavigation OpenIndicatorTab()
        {

            return this;
        }
        public TaskTabNavigation OpenAccountStatementTab()
        {

            return this;
        }
        public TaskTabNavigation OpenMapTab()
        {

            return this;
        }
    }
}
