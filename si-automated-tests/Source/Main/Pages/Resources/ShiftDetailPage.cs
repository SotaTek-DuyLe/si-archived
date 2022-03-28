using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Resources
{
    public class ShiftDetailPage : BasePage
    {
        private readonly By deallocateBtn = By.XPath("//button[@title='Remove Allocation']");
        private readonly By stateSelect = By.Id("state");
        private readonly By resolutionCodeSelect = By.Id("resolution-code");
        private readonly By saveBtn = By.XPath("//button[text()='Save']");

        public ShiftDetailPage IsOnShiftDetailPage()
        {
            WaitUtil.WaitForElementVisible(stateSelect);
            WaitUtil.WaitForElementVisible(resolutionCodeSelect);
            return this;
        }
        public ShiftDetailPage SelectState(string state)
        {
            SelectTextFromDropDown(stateSelect, state);
            return this;
        }
        public ShiftDetailPage SelectResolutionCode(string code)
        {
            SelectTextFromDropDown(resolutionCodeSelect, code);
            return this;
        }
        public ShiftDetailPage RemoveAllocation()
        {
            ClickOnElement(deallocateBtn);
            return this;
        }
        public ShiftDetailPage SaveDetail()
        {
            ClickOnElement(saveBtn);
            return this;
        }
    }
}
