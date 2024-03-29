﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Accounts
{
    public class CreditNotePage :BasePage
    {
        private readonly By partyInput = By.XPath("//div[@id='party-name']//input");
        private readonly string partySelectOption = "//div[@id='party-name']//li[text()='{0}']";
        private readonly By creditDateInput = By.Id("credit-date");
        private readonly By noteInput = By.Id("notes");
        private readonly By accountRefInput = By.Id("account-ref");
        private readonly By yesBtn = By.XPath("//button[text()='Yes']");
        private readonly By partyInput_1 = By.XPath("//input[@id='party-name']");
        private readonly By linesTab = By.CssSelector("a[aria-controls='creditNoteLines-tab']");
        private readonly By rejectBtn = By.XPath("//button[text()='Reject']");
        private readonly By approveBtn = By.XPath("//button[text()='Approve']");
        private readonly By idInput = By.XPath("//div[contains(@class, 'l1 r1')]//input");
        private readonly By applyFilterBtn = By.CssSelector("button[title='Apply Filters']");
        private readonly By firstCreditLineRow = By.XPath("//div[@class='grid-canvas']/div[1]");

        //LINES TAB
        private readonly By idOfFirstLine = By.XPath("//div[@id='creditNoteLines-tab']//div[@class='grid-canvas']/div[1]/div[contains(@class, 'l1 r1')]");
        private readonly By targetTypeOfFirstLine = By.XPath("//div[@id='creditNoteLines-tab']//div[@class='grid-canvas']/div[1]/div[contains(@class, 'l2 r2')]");
        private readonly By targetIdOfFirstLine = By.XPath("//div[@id='creditNoteLines-tab']//div[@class='grid-canvas']/div[1]/div[contains(@class, 'l3 r3')]");
        private readonly By postedStateOfFirstLine = By.XPath("//div[@id='creditNoteLines-tab']//div[@class='grid-canvas']/div[1]/div[contains(@class, 'l13 r13')]");


        //POPUP
        private readonly By titlePopup = By.XPath("//h4[@class='modal-title']");
        private readonly By firstCheckboxItemRowCredit = By.XPath("//div[@class='grid-canvas']/div[1]//input");
        private readonly By confirmBtn = By.XPath("//button[text()='Confirm']");
        private readonly By createAdhocCreditNoteBtn = By.XPath("//button[text()='Create Adhoc Credit Note']");
        private readonly By cancelBtn = By.XPath("//button[text()='Cancel']");
        private readonly By idFirstCreditInPopup = By.XPath("//div[@class='grid-canvas']/div[1]/div[contains(@class, 'l1 r1')]");

        //New tabs
        private readonly By lineTab = By.XPath("//a[@aria-controls='creditNoteLines-tab']");
        private readonly By noteTab = By.XPath("//a[@aria-controls='notes-tab']");

        private string saleCreditLineTable = "//div[@id='credit-note-tickets']//div[@class='grid-canvas']";
        private string saleCreditLineRow = "./div[contains(@class, 'slick-row')]";
        private string checkboxCell = "./div[@class='slick-cell l0 r0']//input";
        private string idCell = "./div[@class='slick-cell l1 r1']";
        private string itemCell = "./div[@class='slick-cell l2 r2']";
        private string typeDateCell = "./div[@class='slick-cell l3 r3']";

        private TableElement saleCreditLineTableEle;
        public TableElement SaleCreditLineTableEle
        {
            get => saleCreditLineTableEle;
        }

        public CreditNotePage()
        {
            saleCreditLineTableEle = new TableElement(saleCreditLineTable, saleCreditLineRow, new List<string>() { checkboxCell, idCell, itemCell, typeDateCell });
            saleCreditLineTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }

        [AllureStep]
        public CreditNotePage VerifyNotDuplicateSaleCreditLine()
        {
            int rowCount = SaleCreditLineTableEle.GetRows().Count;
            List<string> ids = new List<string>();
            for (int i = 0; i < rowCount; i++)
            {
                ids.Add(SaleCreditLineTableEle.GetCellValue(i, SaleCreditLineTableEle.GetCellIndex(idCell)).AsString());
            }
            var duplicates = ids.GroupBy(x => x)
                                .Where(g => g.Count() > 1)
                                .Select(y => y.Key)
                                .ToList();
            Assert.IsTrue(duplicates.Count == 0);
            return this;
        }

        [AllureStep]
        public CreditNotePage IsOnCreditNotePage()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(partyInput);
            WaitUtil.WaitForElementVisible(creditDateInput);
            WaitUtil.WaitForElementVisible(noteInput);
            return this;
        }
        [AllureStep]

        public CreditNotePage IsPopupCreditNote()
        {
            WaitUtil.WaitForElementVisible(titlePopup);
            WaitUtil.WaitForElementVisible(confirmBtn);
            Assert.IsTrue(IsControlDisplayed(confirmBtn));
            Assert.IsTrue(IsControlDisplayed(createAdhocCreditNoteBtn));
            Assert.IsTrue(IsControlDisplayed(cancelBtn));
            return this;
        }
        [AllureStep]
        public CreditNotePage ClickOnFirstCreditRow()
        {
            WaitUtil.WaitForElementVisible(firstCreditLineRow);
            ClickOnElement(firstCheckboxItemRowCredit);
            return this;
        }
        [AllureStep]
        public CreditNotePage DoubleClickOnFirstCreditRow()
        {
            DoubleClickOnElement(firstCreditLineRow);
            return this;
        }
        [AllureStep]
        public CreditNotePage ClickOnConfirmBtn()
        {
            ClickOnElement(confirmBtn);
            return this;
        }
        [AllureStep]
        public CreditNotePage SearchForParty(string partyName)
        {
            SendKeys(partyInput, partyName);
            SleepTimeInMiliseconds(1000);
            ClickOnElement(partySelectOption, partyName);
            return this;
        }
        [AllureStep]
        public CreditNotePage VerifyNewTabsArePresent()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(lineTab);
            WaitUtil.WaitForElementVisible(noteTab);
            return this;
        }
        [AllureStep]
        public CreditNotePage ClickYesBtn()
        {
            ClickOnElement(yesBtn);
            return this;
        }
        [AllureStep]
        public CreditNotePage VerifyAccountReferenceIsReadonly()
        {
            Assert.AreEqual("true", GetAttributeValue(accountRefInput, "readonly"));
            return this;
        }
        [AllureStep]
        public CreditNotePage VerifyCurrenCreditNotetUrl(string contractId, string partyId)
        {
            string currentUrl = GetCurrentUrl();
            Assert.AreEqual(WebUrl.MainPageUrl + "web/credit-notes/new?contractId=" + contractId + "&partyId=" + partyId + "&createFromParty=true", currentUrl);
            return this;
        }
        [AllureStep]
        public CreditNotePage VerifyCurrentCreditNoteUrl()
        {
            string currentUrl = GetCurrentUrl();
            Assert.IsTrue(currentUrl.Contains(WebUrl.MainPageUrl + "web/credit-notes/"));
            return this;
        }
        [AllureStep]
        public CreditNotePage VerifyPartyNameUpdated(string partyNameValue)
        {
            Assert.AreEqual(partyNameValue, GetAttributeValue(partyInput_1, "value"));
            return this;
        }
        [AllureStep]
        public CreditNotePage ClickOnLinesTab()
        {
            ClickOnElement(linesTab);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public CreditNotePage VerifyRowOfNewLines(string targetTypeValue, string targetIdValue, string statusValue)
        {
            Assert.AreEqual(targetTypeValue, GetElementText(targetTypeOfFirstLine));
            Assert.AreEqual(targetIdValue, GetElementText(targetIdOfFirstLine));
            Assert.AreEqual(statusValue, GetElementText(postedStateOfFirstLine));
            return this;
        }
        [AllureStep]
        public string GetIdOfFirstRowInPopupCredit()
        {
            return GetElementText(idFirstCreditInPopup);
        }
        [AllureStep]
        public CreditNotePage ClickRejectButton()
        {
            ClickOnElement(rejectBtn);
            return this;
        }
        [AllureStep]
        public CreditNotePage ClickApproveButton()
        {
            ClickOnElement(approveBtn);
            return this;
        }
        [AllureStep]
        public CreditNotePage VerifyRejectButtonDisabled()
        {
            Assert.AreEqual(false, GetElement(rejectBtn).Enabled);
            return this;
        }

        [AllureStep]
        public CreditNotePage FilterByCreditId(string creditIdValue)
        {
            SendKeys(idInput, creditIdValue);
            ClickOnElement(applyFilterBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
    }
    public class RejectionPopup : BasePage {
        private readonly By reasonDropDown = By.XPath("//select[contains(@data-bind,'resolutionCodes')]");
        private readonly By reasonNote = By.XPath("//textarea[@data-bind = 'textInput: reasonText']");
        private readonly By rejectButton = By.XPath("//button[@data-bind = 'click: performReject']");

        public RejectionPopup()
        {
            WaitUtil.WaitForPageLoaded();
            WaitUtil.WaitForElementVisible(reasonDropDown);
            WaitUtil.WaitForElementVisible(reasonNote);
            WaitUtil.WaitForElementVisible(rejectButton);
        }
        [AllureStep]
        public RejectionPopup SelectRejectReasonFromDropDown(string reason)
        {
            SelectTextFromDropDown(reasonDropDown, reason);
            return this;
        }
        [AllureStep]
        public RejectionPopup InputRejectReason(string reason)
        {
            SendKeys(reasonNote, reason);
            return this;
        }
        [AllureStep]
        public RejectionPopup ClickConfirmReject()
        {
            ClickOnElement(rejectButton);
            SleepTimeInMiliseconds(1000);
            VerifyToastMessage("Success");
            return this;
        }
        [AllureStep]
        public RejectionPopup VerifyOptionNumber(int expectedSize)
        {
            Assert.AreEqual(GetSelectElement(reasonDropDown).Options.Count, expectedSize + 1);
            return this;
        }
        

    }

}
