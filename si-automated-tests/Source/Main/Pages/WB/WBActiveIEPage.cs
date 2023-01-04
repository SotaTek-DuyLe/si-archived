using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.WB
{
    public class WBActiveIEPage : BasePage
    {
        private readonly By title = By.XPath("//span[contains(string(), 'SystemSetting')]");
        private readonly By name = By.XPath("//span[contains(string(), 'Weighbridge Active')]");
        private readonly By settingValueInput = By.XPath("//span[text()='Setting Value']/parent::td/following-sibling::td//textarea");
        private readonly By saveBtn = By.XPath("//img[@title='Save']/parent::td");

        [AllureStep]
        public WBActiveIEPage IsWBActivePage()
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(name);
            return this;
        }

        [AllureStep]
        public WBActiveIEPage InputSettingValue(string settingValue)
        {
            SendKeys(settingValueInput, settingValue);
            return this;
        }

        [AllureStep]
        public WBActiveIEPage ClickOnSaveWBSetting()
        {
            ClickOnElement(saveBtn);
            return this;
        }
    }
}
