using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyWBTicketPage
{
    public class DeleteWBLocation : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Warning']");
        private readonly By contentPopup = By.XPath("//div[text()='Are you sure you want to delete this Weighbridge Site Location?']");
        private readonly By yesBtn = By.XPath("//button[text()='Yes']");
        private readonly By noBtn = By.XPath("//button[text()='No']");

        public DeleteWBLocation IsWarningPopupDisplayed()
        {
            WaitUtil.WaitForElementVisible(title);
            Assert.IsTrue(IsControlDisplayed(contentPopup));
            Assert.IsTrue(IsControlDisplayed(yesBtn));
            Assert.IsTrue(IsControlDisplayed(noBtn));
            return this;
        }

        public DeleteWBLocation ClickYesBtn()
        {
            ClickOnElement(yesBtn);
            return this;
        }
    }
}
