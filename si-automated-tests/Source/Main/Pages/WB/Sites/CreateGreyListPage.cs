using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.WB.Sites
{
    public class CreateGreyListPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='Weighbridge Grey List']");
        private readonly By detailTab = By.XPath("//a[text()='Details']");

        //DETAIL
        private readonly By vehicleTitle = By.XPath("//label[text()='Vehicle']");
        private readonly By vehicleInput = By.XPath("//label[text()='Vehicle']/following-sibling::div//input");
        private readonly By startDateInput = By.CssSelector("input[id='start-date']");
        private readonly By endDateInput = By.CssSelector("input[id='end-date']");
        private readonly By ticketDd = By.XPath("//select[@id='ticket']");
        private readonly By ticketLabel = By.XPath("//label[text()='Ticket']");
        private readonly By ticketBtn = By.XPath("//label[text()='Ticket']/following-sibling::div//button");
        private readonly By ticketButton = By.XPath("//label[text()='Ticket']/following-sibling::div//button/span[1]");
        private readonly By customerDd = By.CssSelector("select[id='customer']");
        private readonly By haulierDd = By.CssSelector("select[id='haulier']");
        private readonly By visitedSiteDd = By.CssSelector("select[id='site']");
        private readonly By greylistCodeDd = By.XPath("//label[text()='Greylist Code']/following-sibling::div//button");
        private readonly By greylistCodeButton = By.XPath("//label[text()='Greylist Code']/following-sibling::div//button/span[1]");
        private readonly By greylistCodeLabel = By.XPath("//label[text()='Greylist Code']");
        private readonly By commentInput = By.CssSelector("textarea[id='comment']");
        private readonly By lastUpdatedUserInput = By.CssSelector("textarea[id='last-updated-user']");
        private readonly By lastUpdatedInput = By.CssSelector("textarea[id='last-updated']");
        private readonly By greylistCodeSearchInput = By.XPath("//ul[@aria-expanded='true']/preceding-sibling::div/input[@aria-label='Search']");

        //DYNAMIC
        private const string anyLiOption = "//li[contains(text(), '{0}')]";
        private const string noResultsMatchedGreylistCode = "//li[contains(text(), 'No results matched \"{0}\"')]";
        private const string anyGreyListCode = "//li//span[text()='{0}']";
        private const string anyTicket = "//li//span[text()='{0}']";
        private const string customerOption = "//select[@id='customer']/option[contains(text(), '{0}')]";
        private const string haulierOption = "//select[@id='haulier']/option[contains(text(), '{0}')]";

        [AllureStep]
        public CreateGreyListPage IsCreateWBGreyListPage()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(detailTab);
            WaitUtil.WaitForElementVisible(vehicleTitle);
            Assert.IsTrue(IsControlDisplayed(vehicleInput), "[Vehicle] is not displayed");
            Assert.IsTrue(IsControlDisplayed(startDateInput), "[Start Date] is not displayed");
            Assert.IsTrue(IsControlDisplayed(endDateInput), "[End Date] is not displayed");
            Assert.IsTrue(IsControlDisplayed(ticketBtn), "[Ticket] is not displayed");
            Assert.IsTrue(IsControlDisplayed(customerDd), "[Customer] is not displayed");
            Assert.IsTrue(IsControlDisplayed(haulierDd), "[Haulier] is not displayed");
            Assert.IsTrue(IsControlDisplayed(visitedSiteDd), "[Visited Site] is not displayed");
            Assert.IsTrue(IsControlDisplayed(greylistCodeDd), "[Greylist Code] is not displayed");
            Assert.IsTrue(IsControlDisplayed(commentInput), "[Comment] is not displayed");
            Assert.IsTrue(IsControlDisplayed(lastUpdatedUserInput), "[lastUpdatedUser] is not displayed");
            Assert.IsTrue(IsControlDisplayed(lastUpdatedInput), "[lastUpdated] is not displayed");
            return this;
        }

        [AllureStep]
        public CreateGreyListPage InputVehicle(string vehicleValue)
        {
            SendKeys(vehicleInput, vehicleValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage SelectVehicleName(string vehicleValue)
        {
            WaitUtil.WaitForElementVisible(anyLiOption, vehicleValue);
            ClickOnElement(anyLiOption, vehicleValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage ClickOnGreylistCode()
        {
            ClickOnElement(greylistCodeDd);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage InputValueInGreyListCode(string greylistCodeValue)
        {
            SendKeys(greylistCodeSearchInput, greylistCodeValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage ClickOnAnyGreyListCode(string greylistCodeValue)
        {
            ClickOnElement(anyGreyListCode, greylistCodeValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage VerifyNoResultMatchedGreylistCode(string greylistCodeValue)
        {
            Assert.IsTrue(IsControlDisplayed(noResultsMatchedGreylistCode, greylistCodeValue), greylistCodeValue + " is displayed in [Greylist Code] dd");
            return this;
        }

        [AllureStep]
        public CreateGreyListPage ClickOnGreylistCodeAndVerifyValueMatchingDB(List<string> listGreycodeDB)
        {
            ClickOnElement(greylistCodeLabel);
            ClickOnElement(greylistCodeDd);
            foreach(string greylist in listGreycodeDB)
            {
                Assert.IsTrue(IsControlDisplayed(anyGreyListCode, greylist), greylist + " is not displayed in DD");
            }
            return this;
        }

        [AllureStep]
        public CreateGreyListPage VerifyValueInLastUpdatedBy(string lastUpdatedByValue)
        {
            Assert.AreEqual(lastUpdatedByValue, GetAttributeValue(lastUpdatedUserInput, "value"), "Value at [Last Updated by] is not correct");
            return this;
        }

        [AllureStep]
        public CreateGreyListPage VerifyValueInLastUpdated(string lastUpdatedValue)
        {
            Assert.AreEqual(lastUpdatedValue, GetAttributeValue(lastUpdatedInput, "value"), "Value at [Last Updated] is not correct");
            return this;
        }

        [AllureStep]
        public CreateGreyListPage ClickAndSelectCustomer(string customerValue)
        {
            ClickOnElement(customerDd);
            ClickOnElement(customerOption, customerValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage ClickAndSelectHaulier(string haulierValue)
        {
            ClickOnElement(haulierDd);
            ClickOnElement(haulierOption, haulierValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage ClickAndSelectVisitedSite(string visitedSiteValue)
        {
            SelectTextFromDropDown(visitedSiteDd, visitedSiteValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage InputComment(string commentValue)
        {
            SendKeys(commentInput, commentValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage VerifyCommentValue(string commentValue)
        {
            Assert.AreEqual(commentValue, GetAttributeValue(commentInput, "value"));
            return this;
        }

        [AllureStep]
        public CreateGreyListPage InputStartDate(string startDateValue)
        {
            SendKeys(startDateInput, startDateValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage InputEndDate(string endDateValue)
        {
            SendKeys(endDateInput, endDateValue);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage ClickOnTicketDdAndVerify(string[] ticketValue)
        {
            ClickOnElement(ticketLabel);
            ClickOnElement(ticketBtn);

            foreach(string ticket in ticketValue)
            {
                Assert.IsTrue(IsControlDisplayed(anyTicket, ticket), ticket + " is not displayed");
            }
            return this;
        }

        [AllureStep]
        public CreateGreyListPage SelectOneTicket(string ticketName)
        {
            ClickOnElement(anyTicket, ticketName);
            return this;
        }

        [AllureStep]
        public CreateGreyListPage VerifyValueInCustomer(string customerValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(customerDd).Contains(customerValue), "Value at Customer dd is not correct");
            return this;
        }

        [AllureStep]
        public CreateGreyListPage VerifyValueInHaulier(string haulierValue)
        {
            Assert.IsTrue(GetFirstSelectedItemInDropdown(haulierDd).Contains(haulierValue), "Value at Haulier dd is not correct");
            return this;
        }

        [AllureStep]
        public CreateGreyListPage VerifyValueInVisitedSite(string visitedValue)
        {
            Assert.AreEqual(visitedValue, GetFirstSelectedItemInDropdown(visitedSiteDd));
            return this;
        }

        [AllureStep]
        public CreateGreyListPage VerifyAllValueInWBGreylistDetail(string vehicleExp, string startDateExp, string endDateExp, string ticketNumberExp, string customerExp, string haulierExp, string visitedSiteExp, string greylistCodeExp, string[] commentExp, string lastUpdatedByExp, string lastUpdatedExp)
        {
            Assert.AreEqual(vehicleExp, GetAttributeValue(vehicleInput, "value"));
            Assert.AreEqual(startDateExp, GetAttributeValue(startDateInput, "value"));
            Assert.AreEqual(endDateExp, GetAttributeValue(endDateInput, "value"));
            Assert.AreEqual(ticketNumberExp, GetElementText(ticketButton));
            Assert.IsTrue(GetFirstSelectedItemInDropdown(customerDd).Contains(customerExp));
            Assert.IsTrue(GetFirstSelectedItemInDropdown(haulierDd).Contains(haulierExp));
            Assert.AreEqual(visitedSiteExp, GetFirstSelectedItemInDropdown(visitedSiteDd));
            Assert.AreEqual(greylistCodeExp, GetElementText(greylistCodeButton));
            Assert.AreEqual(lastUpdatedByExp, GetAttributeValue(lastUpdatedUserInput, "value"));
            Assert.IsTrue(GetAttributeValue(lastUpdatedInput, "value").Contains(lastUpdatedExp));
            //Comment
            string[] title = { "Ticket Number: ", "Ticket Price: ", "Amount Paid: " };
            string textDisplayed = GetAttributeValue(commentInput, "value");
            string[] formatText = textDisplayed.Split(Environment.NewLine);
            for (int i = 0; i < formatText.Length; i++)
            {
                Assert.IsTrue(formatText[i].Contains(title[i] + commentExp[i]), "Value at " + title[i] + "is not correct");
            }
            return this;
        }

    }
}
