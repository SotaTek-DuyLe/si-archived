using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class DetailCreditNotePage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Credit Note']");
        private readonly By detailTab = By.CssSelector("a[aria-controls='details-tab']");
        private readonly By assignedUser = By.XPath("//label[text()='Assigned User']/following-sibling::div");
        private readonly By assignedUserOption = By.XPath("//label[text()='Assigned User']/following-sibling::div//option");

        //DYNAMIC
        private readonly string partyName = "//p[text()='{0}']";

        [AllureStep]
        public DetailCreditNotePage IsCreditNoteDetailPage(string partyNameValue)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(partyName, partyNameValue);
            return this;
        }

        [AllureStep]
        public DetailCreditNotePage ClickOnDetailTab()
        {
            ClickOnElement(detailTab);
            return this;
        }

        [AllureStep]
        public List<string> GetAllAssignedUser()
        {
            ClickOnElement(assignedUser);
            List<string> allUserName = new List<string>();
            List<IWebElement> allUserElement = GetAllElements(assignedUserOption);
            foreach(IWebElement webElement in allUserElement)
            {
                allUserName.Add(GetElementText(webElement));
            }
            return allUserName;
        }
    }
}
