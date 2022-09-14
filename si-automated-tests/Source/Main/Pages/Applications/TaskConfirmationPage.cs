using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class TaskConfirmationPage : BasePage
    {
        private readonly By contractTitle = By.XPath("//label[text()='Contract']");
        private readonly By serviceTitle = By.XPath("//label[text()='Services']");
        private readonly By scheduleTitle = By.XPath("//label[text()='Scheduled Date']");
        private readonly By goBtn = By.XPath("button[id='button-go']");
        private readonly By contractDd = By.XPath("//label[text()='Contract']/following-sibling::span/select");
        private readonly By serviceInput = By.XPath("//label[text()='Services']/following-sibling::input");
        private readonly By scheduledDateInput = By.XPath("//label[text()='Scheduled Date']/following-sibling::input");
        private readonly By expandRoundsBtn = By.XPath("//span[text()='Expand Rounds']/parent::button");
        private readonly By expandRoundLegsBtn = By.XPath("//span[text()='Expand Round Legs']/parent::button");
        private readonly By descInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l4')]/input");
        private readonly By descAtFirstColumn = By.XPath("//div[@id='grid']//div[@class='grid-canvas']/div[contains(@class, 'assured')]/div[contains(@class, 'l4')]");

        //DYNAMIC
        private readonly string anyContractOption = "//label[text()='Contract']/following-sibling::span/select/option[text()='{0}']";
        private readonly string anyServicesByServiceGroup = "//li[contains(@class, 'serviceGroups')]//a[text()='{0}']/preceding-sibling::i";
        private readonly string anyRoundByDay = "//a[text()='{0}']/following-sibling::ul//a[text()='{1}']";
        private readonly string anyChildOfTree = "//a[text()='{0}']/parent::li/i";

        public TaskConfirmationPage IsTaskConfirmationPage()
        {
            WaitUtil.WaitForElementVisible(contractTitle);
            Assert.IsTrue(IsControlDisplayed(serviceTitle));
            Assert.IsTrue(IsControlDisplayed(scheduleTitle));
            Assert.IsTrue(IsControlDisplayed(goBtn));
            return this;
        }

        public TaskConfirmationPage SelectContract(string contractName)
        {
            ClickOnElement(contractDd);
            //Select contract
            ClickOnElement(anyContractOption, contractName);
            return this;
        }

        public TaskConfirmationPage ClickServicesAndSelectServiceInTree(string serviceGroupName, string serviceName, string roundName, string dayName)
        {
            ClickOnElement(serviceInput);
            ClickOnElement(anyServicesByServiceGroup, serviceGroupName);
            ClickOnElement(anyChildOfTree, serviceName);
            ClickOnElement(anyChildOfTree, roundName);
            ClickOnElement(anyChildOfTree, dayName);
            return this;
        }

        public TaskConfirmationPage SendDateInScheduledDate(string dateValue)
        {
            SendKeys(scheduledDateInput, dateValue);
            return this;
        }

        public TaskConfirmationPage ClickGoBtn()
        {
            ClickOnElement(goBtn);
            return this;
        }

        public TaskConfirmationPage ClickOnExpandRoundsBtn()
        {
            ClickOnElement(expandRoundsBtn);
            return this;
        }

        public TaskConfirmationPage ClickOnExpandRoundLegsBtn()
        {
            ClickOnElement(expandRoundLegsBtn);
            return this;
        }

        public TaskConfirmationPage SendKeyInDesc(string descValue)
        {
            SendKeys(descInput, descValue);
            WaitForLoadingIconToDisappear();
            return this;
        }

        public TaskConfirmationPage VerifyDisplayResultAfterSearchWithDesc(string descExp)
        {
            Assert.AreEqual(descExp, GetElementText(descAtFirstColumn));
            return this;
        }
    }

}
