
using NUnit.Allure.Attributes;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAccountStatement
{
    public class AccountStatementPage : BasePage
    {
        private string buttonNamed = "//div[@id='partyAccountStatements-tab']//button[text()='{0}']";
        public AccountStatementPage()
        {
            WaitUtil.WaitForElementVisible(buttonNamed, "Create Credit Note");
            WaitUtil.WaitForElementVisible(buttonNamed, "Take Payment");
        }
        [AllureStep]
        private AccountStatementPage ClickOnButtonNamed(string value)
        {
            ClickOnElement(buttonNamed, value);
            return this;
        }
        [AllureStep]
        public AccountStatementPage ClickCreateCreditNote()
        {
            ClickOnButtonNamed("Create Credit Note");
            return this;
        }
        [AllureStep]
        public AccountStatementPage ClickTakePayment()
        {
            ClickOnButtonNamed("Take Payment");
            return this;
        }
        [AllureStep]
        public AccountStatementPage ClickCreateSaleInvoice()
        {
            ClickOnButtonNamed("Create Sales Invoice");
            return this;
        }

    }
}
