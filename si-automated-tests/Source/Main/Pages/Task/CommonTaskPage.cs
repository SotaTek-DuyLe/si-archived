using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;

namespace si_automated_tests.Source.Main.Pages.Task
{
    public class CommonTaskPage : BasePage
    {
        private readonly By deleteItemBtn = By.XPath("//button[text()='Delete Item']");
        private readonly By taskIdInput = By.XPath("//div[contains(@class,'r5')]//input");
        private readonly By applyBtn = By.XPath("//button[contains(.,'Apply')]");

        private string taskWithId = "//div[contains(@class,'r5') and contains(.,'{0}')]";

        public CommonTaskPage FilterTaskId(int id)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForAllElementsVisible(deleteItemBtn);
            SendKeys(taskIdInput, id.ToString());
            SleepTimeInMiliseconds(1000);
            ClickOnElement(applyBtn);
            return this;
        }
        
        public AgreementTaskDetailsPage OpenTaskWithId(int id)
        {
            DoubleClickOnElement(taskWithId, id.ToString());
            return new AgreementTaskDetailsPage();
        }
    }
}
