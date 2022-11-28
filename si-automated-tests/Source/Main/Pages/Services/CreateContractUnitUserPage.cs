using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class CreateContractUnitUserPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Contract Unit User']");
        private readonly By userDd = By.XPath("//button[@data-id='users']");
        private readonly By allUserOption = By.CssSelector("select[id='users']>option");

        //DYNAMIC
        private readonly string anyUserOption = "//select[@id='users']/option[text()='{0}']";

        [AllureStep]
        public CreateContractUnitUserPage IsCreateContractUnitUserPage()
        {
            WaitUtil.WaitForElementVisible(title);
            return this;
        }

        [AllureStep]
        public CreateContractUnitUserPage ClickOnUserDd()
        {
            ClickOnElement(userDd);
            return this;
        }

        [AllureStep]
        public List<string> GetAllUserInDd()
        {
            WaitUtil.WaitForAllElementsPresent(allUserOption);
            return GetAllElementText(allUserOption);
        }

        [AllureStep]
        public CreateContractUnitUserPage VerifyUserIsNotDiplayedInUserDd(List<string> allInActiveUser)
        {
            foreach(string inactiveUser in allInActiveUser)
            {
                Assert.IsTrue(IsControlUnDisplayed(anyUserOption, inactiveUser), inactiveUser + " is displayed in the list");
            }
            return this;
        }

    }
}
