using System;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class TasksListingPage : BasePage
    {
        private readonly By firstRecord = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))]");
        private readonly By deleteItem = By.XPath("//button[text()='Delete Item']");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By filterInputById = By.XPath("//div[contains(@class, 'l5 r5')]/descendant::input");

        public TasksListingPage FilterByTaskId(String taskId)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(deleteItem);
            SendKeys(filterInputById, taskId);
            ClickOnElement(applyBtn);
            return this;
        }

        public DetailTaskPage ClickOnFirstRecord()
        {
            DoubleClickOnElement(firstRecord);
            return PageFactoryManager.Get<DetailTaskPage>();
        }
    }
}
