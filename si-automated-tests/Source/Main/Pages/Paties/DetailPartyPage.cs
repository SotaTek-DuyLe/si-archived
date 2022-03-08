using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    public class DetailPartyPage : BasePage
    {
        private const string AllTabDisplayed = "//li[@role='presentation' and not(contains(@style, 'visibility: collapse'))]/a";
        private const string AllTabInDropdown = "//ul[@class='dropdown-menu']//a";
        private const string DropdownBtn = "//li[contains(@class, 'dropdown')]/a[contains(@class, 'dropdown-toggle')]";
        private const string SuccessfullyToastMessage = "//div[@class='notifyjs-corner']//div[text()='Successfully saved party.']";
        private const string FrameMessage = "//div[@class='notifyjs-corner']/div";
        private const string LoadingData = "//div[@class='loading-data']";

        private string PartyName = "//div[text()='{0}']";

        public List<string> GetAllTabDisplayed()
        {
            List<string> allTabs = new List<string>();
            List<IWebElement> allElements = GetAllElements(AllTabDisplayed);
            for(int i = 0; i < allElements.Count; i++)
            {
                allTabs.Add(GetElementText(allElements[i]));
            }
            return allTabs;
        }
        public List<string> GetAllTabInDropdown()
        {
            
            List<string> allTabs = new List<string>();
            if (!IsControlDisplayedNotThrowEx(DropdownBtn))
            {
                return allTabs;
            }
            List<IWebElement> allElements = GetAllElements(AllTabInDropdown);
            for (int i = 0; i < allElements.Count; i++)
            {
                allTabs.Add(GetElementText(allElements[i]));
            }
            return allTabs;
        }
        public DetailPartyPage MergeAllTabInDetailPartyAndVerify()
        {
            WaitUtil.WaitForElementVisible(AllTabDisplayed);
            List<string> allTabDisplayed = GetAllTabDisplayed();
            List<string> allTabInDropdown = GetAllTabInDropdown();
            allTabDisplayed.AddRange(allTabInDropdown);
            Assert.AreEqual(allTabDisplayed, PartyTabConstant.AllTabInDetailParty.ToList());
            return this;
        }
        public DetailPartyPage ClickAllTabAndVerify()
        {
            List<IWebElement> allElements = GetAllElements(AllTabDisplayed);
            int clickButtonIdx = 0;
            while (clickButtonIdx < allElements.Count)
            {
                ClickOnElement(allElements[clickButtonIdx]);
                clickButtonIdx++;
                WaitUtil.WaitForElementInvisible(LoadingData);
                Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
                allElements = GetAllElements(AllTabDisplayed);
            }
            return this;
        }
        public DetailPartyPage ClickAllTabInDropdownAndVerify()
        {
            if (IsControlDisplayedNotThrowEx(DropdownBtn))
            {
                List<IWebElement> allElements = GetAllElements(AllTabInDropdown);
                int clickButtonIdx = 0;
                while (clickButtonIdx < allElements.Count)
                {
                    ClickOnElement(DropdownBtn);
                    Thread.Sleep(500);
                    ClickOnElement(allElements[clickButtonIdx]);
                    clickButtonIdx++;
                    WaitUtil.WaitForElementInvisible(LoadingData);
                    Assert.IsFalse(IsControlDisplayedNotThrowEx(FrameMessage));
                    allElements = GetAllElements(AllTabInDropdown);
                }
            }
            return this;
        }
        public DetailPartyPage VerifyDisplaySuccessfullyMessage()
        {
            Assert.IsTrue(IsControlDisplayed(SuccessfullyToastMessage));
            WaitUtil.WaitForElementInvisible(SuccessfullyToastMessage);
            return this;
        }

        public DetailPartyPage ClickOnParty(string name)
        {
            ClickOnElement(PartyName, name);
            return this;
        }

    }
}
