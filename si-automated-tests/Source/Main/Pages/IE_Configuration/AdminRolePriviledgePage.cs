using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.IE_Configuration
{
    public class AdminRolePriviledgePage : BasePage
    {
        private By denyUpdateBtn = By.XPath("//span[text()='Parties']/../../following-sibling::td[8]/div/img");
        private By saveButton = By.XPath("//img[@title='Save']");
        private By btn = By.XPath("//a[text()='Admin Roles']");

        [AllureStep]
        public AdminRolePriviledgePage TurnOnDenyUpdate()
        {
            Assert.NotNull(GetAttributeValue(denyUpdateBtn, "src"));
            if (GetAttributeValue(denyUpdateBtn, "src").Contains("images/icons/tristate0.png", StringComparison.OrdinalIgnoreCase))
            {
                ClickOnElement(denyUpdateBtn);

            }
            return this;
        }
        [AllureStep]
        public AdminRolePriviledgePage ClickSaveButton()
        {
            ClickOnElement(saveButton);
            return this;
        }
    }
}
