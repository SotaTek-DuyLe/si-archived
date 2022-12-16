using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.IE_Configuration
{
    public class TaskTypePage : BasePage
    {
        private readonly string taskLineToTaskMappingTab = "//a[text()='{0}']";
        private readonly string selects = "(//div[contains(@id,'TABPAGE') and @style='display: block;']//select)";
        private By saveButton = By.XPath("//img[@title='Save']");

        [AllureStep]
        public TaskTypePage SwitchToTabNamed(string name)
        {
            ClickOnElement(taskLineToTaskMappingTab, name);
            return this;
        }
        [AllureStep]
        public TaskTypePage SelectTaskStateAllCompleted(string state)
        {
            SelectTextFromDropDown(By.XPath(selects + "[1]"), state);
            return this;
        }
        [AllureStep]
        public TaskTypePage SelectResolutionCodeAllCompleted(string code)
        {
            SelectTextFromDropDown(By.XPath(selects + "[2]"), code);
            return this;
        }
        [AllureStep]
        public TaskTypePage SelectTaskStateAllCompletedFailed(string state)
        {
            SelectTextFromDropDown(By.XPath(selects + "[3]"), state);
            return this;
        }
        [AllureStep]
        public TaskTypePage SelectResolutionCodeAllCompletedFailed(string code)
        {
            SelectTextFromDropDown(By.XPath(selects + "[4]"), code);
            return this;
        }
        [AllureStep]
        public TaskTypePage SelectTaskStateAllFailed(string state)
        {
            SelectTextFromDropDown(By.XPath(selects + "[5]"), state);
            return this;
        }
        [AllureStep]
        public TaskTypePage SelectResolutionCodeAllFailed(string code)
        {
            SelectTextFromDropDown(By.XPath(selects + "[6]"), code);
            return this;
        }
        [AllureStep]
        public TaskTypePage SelectAllRandom()
        {
            var num = GetNumberOfOptionInSelect(By.XPath(selects + "[1]"));
            var rand = CommonUtil.GetRandomNumberBetweenRange(0, num);
            SelectIndexFromDropDown(By.XPath(selects + "[1]"), rand);

            num = GetNumberOfOptionInSelect(By.XPath(selects + "[2]"));
            rand = CommonUtil.GetRandomNumberBetweenRange(0, num);
            SelectIndexFromDropDown(By.XPath(selects + "[2]"), rand);

            num = GetNumberOfOptionInSelect(By.XPath(selects + "[3]"));
            rand = CommonUtil.GetRandomNumberBetweenRange(0, num);
            SelectIndexFromDropDown(By.XPath(selects + "[3]"), rand);

            num = GetNumberOfOptionInSelect(By.XPath(selects + "[4]"));
            rand = CommonUtil.GetRandomNumberBetweenRange(0, num);
            SelectIndexFromDropDown(By.XPath(selects + "[4]"), rand);

            num = GetNumberOfOptionInSelect(By.XPath(selects + "[5]"));
            rand = CommonUtil.GetRandomNumberBetweenRange(0, num);
            SelectIndexFromDropDown(By.XPath(selects + "[5]"), rand);

            num = GetNumberOfOptionInSelect(By.XPath(selects + "[6]"));
            rand = CommonUtil.GetRandomNumberBetweenRange(0, num);
            SelectIndexFromDropDown(By.XPath(selects + "[6]"), rand);

            return this;
        }
        

        [AllureStep]
        public TaskTypePage IsOnTaskLineToTaskMappingTab()
        {
            WaitUtil.WaitForElementVisible(By.XPath("//span[text()='All Completed (Core Task State: Closed)']"));
            WaitUtil.WaitForElementVisible(By.XPath("//span[text()='All Completed/Failed (Core Task State: Closed/Failed)']"));
            WaitUtil.WaitForElementVisible(By.XPath("//span[text()='All Failed (Core Task State: Failed)']"));
            return this;
        }
        [AllureStep]
        public TaskTypePage ClickSaveButton()
        {
            ClickOnElement(saveButton);
            return this;
        }
    }
}
