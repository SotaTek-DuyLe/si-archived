using System;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.WB.Sites
{
    public class RemoveGreyListPage : BasePage
    {
        private readonly By titleWarningPopup = By.XPath("//h4[text()='Warning']");
        private readonly By contentWarningPopup = By.XPath("//div[text()='Are you sure you want to delete this Weighbridge Grey List ?']");
        private readonly By yesBtn = By.XPath("//button[text()='Yes']");
        private readonly By noBtn = By.XPath("//button[text()='No']");
        private readonly By clostBtn = By.XPath("//button[text()='×']");

        [AllureStep]
        public RemoveGreyListPage IsRemoveGreyListPopup()
        {
            WaitUtil.WaitForElementVisible(titleWarningPopup);
            WaitUtil.WaitForElementVisible(contentWarningPopup);
            return this;
        }

        [AllureStep]
        public RemoveGreyListPage ClickOnYesBtn()
        {
            ClickOnElement(yesBtn);
            return this;
        }

        [AllureStep]
        public RemoveGreyListPage ClickOnNoBtn()
        {
            ClickOnElement(noBtn);
            return this;
        }


        [AllureStep]
        public RemoveGreyListPage ClickOnCloseBtn()
        {
            ClickOnElement(clostBtn);
            return this;
        }
    }
}
