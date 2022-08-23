using System;
using System.Collections.Generic;
using System.Text;
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

        public AdminRolePriviledgePage TurnOnDenyUpdate()
        {
            Assert.NotNull(GetAttributeValue(denyUpdateBtn, "src"));
            if (GetAttributeValue(denyUpdateBtn, "src").Equals("images/icons/tristate0.png"))
            {
                ClickOnElement(denyUpdateBtn);

            }
            return this;
        }
        public AdminRolePriviledgePage ClickSaveButton()
        {
            ClickOnElement(saveButton);
            return this;
        }
    }
}
