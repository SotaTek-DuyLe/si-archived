using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage
{
    public class AddPrimaryContactPage : BasePage
    {
        private readonly By contactTitle = By.XPath("//h4[text()='CONTACT']");
        private readonly By startDateTitle = By.XPath("//span[@title='Start Date']");
        private readonly By endDateTitle = By.XPath("//span[@title='End Date']");
        private readonly By saveBtn = By.CssSelector("button[title='Save']");
        private readonly By closeWithoutSavingBtn = By.CssSelector("button[title='Close Without Saving']");
        private readonly By detailTab = By.XPath("//a[text()='Details']");
        private readonly By titleInput = By.CssSelector("input#contact-title");
        private readonly By firstNameInput = By.CssSelector("input#contact-first-name");
        private readonly By lastNameInput = By.CssSelector("input#contact-last-name");
        private readonly By position = By.CssSelector("input#contact-position");
        private readonly By greeting = By.CssSelector("input#contact-greeting");
        private readonly By telephone = By.CssSelector("input#contact-telephone");
        private readonly By mobile = By.CssSelector("input#contact-mobile");
        private readonly By email = By.CssSelector("input#contact-email");
        private readonly By receivedEmail = By.XPath("//label[text()='Receive Emails']/following-sibling::div//input");
        private readonly By contactGroups = By.CssSelector("button[data-id='contact-groups']");
        private readonly By startDate = By.CssSelector("input#start-date");
        private readonly By endDate = By.CssSelector("input#end-date");
        private readonly By selectAllBtn = By.XPath("//button[contains(@class, 'bs-select-all')]");
        private readonly By deselectAllBtn = By.XPath("//button[contains(@class, 'bs-deselect-all')]");

        //DYNAMIC LOCATOR
        private const string ContactGroupsOption = "//div[@class='bs-actionsbox']/following-sibling::ul//span[text()='{0}']";

        [AllureStep]
        public AddPrimaryContactPage IsCreatePrimaryContactPage()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(contactTitle);
            Assert.IsTrue(IsControlDisplayed(contactTitle));
            Assert.AreEqual(GetElementText(startDateTitle), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetElementText(endDateTitle), CommonConstants.EndDateAgreement);
            Assert.IsTrue(IsControlDisplayed(saveBtn));
            Assert.IsTrue(IsControlDisplayed(closeWithoutSavingBtn));
            //Verify mandatory fields
            Assert.AreEqual(GetCssValue(firstNameInput, "border-color"), CommonConstants.BoderColorMandatory);
            Assert.AreEqual(GetCssValue(lastNameInput, "border-color"), CommonConstants.BoderColorMandatory);
            //Verify default value in start date and end date
            Assert.AreEqual(GetAttributeValue(startDate, "value"), CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT));
            Assert.AreEqual(GetAttributeValue(endDate, "value"), CommonConstants.EndDateAgreement);
            //Verify placeholder
            Assert.AreEqual(GetAttributeValue(telephone, "placeholder"), "+442...");
            Assert.AreEqual(GetAttributeValue(mobile, "placeholder"), "+447...");

            return this;
        }
        [AllureStep]
        public AddPrimaryContactPage ClickAnyContactGroupsAndVerify(string option)
        {
            ClickOnElement(contactGroups);
            Assert.IsTrue(IsControlDisplayed(selectAllBtn));
            Assert.IsTrue(IsControlDisplayed(deselectAllBtn));
            Assert.IsTrue(IsControlDisplayed(string.Format(ContactGroupsOption, CommonConstants.ContactGroupsOptions[0])));
            Assert.IsTrue(IsControlDisplayed(string.Format(ContactGroupsOption, CommonConstants.ContactGroupsOptions[2])));
            //Click any options
            ClickOnElement(ContactGroupsOption, option);
            return this;
        }
        [AllureStep]
        public AddPrimaryContactPage EnterFirstName(string firstName)
        {
            SendKeys(firstNameInput, firstName);
            return this;
        }
        [AllureStep]
        public AddPrimaryContactPage EnterLastName(string lastName)
        {
            SendKeys(lastNameInput, lastName);
            return this;
        }
        [AllureStep]
        public AddPrimaryContactPage EnterMobileValue(string mobileValue)
        {
            SendKeys(mobile, mobileValue);
            return this;
        }
        [AllureStep]
        public AddPrimaryContactPage EnterValueRemainingFields(ContactModel contactModel)
        {
            SendKeys(titleInput, contactModel.Title);
            SendKeys(position, contactModel.Position);
            SendKeys(greeting, contactModel.Greeting);
            SendKeys(telephone, contactModel.Telephone);
            SendKeys(email, contactModel.Email);
            if (contactModel.ReceiveEmail)
            {
                ClickOnElement(receivedEmail);
            }
            return this;
        }

    }
}
