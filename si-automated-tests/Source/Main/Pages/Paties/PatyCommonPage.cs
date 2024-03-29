﻿using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Paties;

namespace si_automated_tests.Source.Main.Pages
{
    public class PartyCommonPage : BasePage
    {
        private const string AddNewItem = "//button[text()='Add New Item']";

        private readonly By addNewItemLoading = By.XPath("//button[text()='Add New Item' and contains(@class, 'echo-disabled')]");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstResult = By.XPath("//div[@class='grid-canvas']/div[1]");
        private readonly By allPartyRows = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");


        [AllureStep]
        public PartyCommonPage ClickAddNewItem()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementInvisible(addNewItemLoading);
            ClickOnElement(AddNewItem);
            return this;
        }
        [AllureStep]
        public List<PartyModel> GetAllPartyListing()
        {
            List<PartyModel> list = new List<PartyModel>();
            List<IWebElement> AllPartyRows = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]");
            List<IWebElement> AllPartyName = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[3]");
            List<IWebElement> AllContractName = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[4]");
            List<IWebElement> AllAccountNumber = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[5]");
            List<IWebElement> AllAccountRef = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[6]");
            List<IWebElement> AllPartyType = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[17]");
            List<IWebElement> AllStartDate = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[25]");
            for (int i = 0; i < AllPartyRows.Count; i++)
            {
                string partyName = GetElementText(AllPartyName[i]);
                string contractName = GetElementText(AllContractName[i]);
                string accountNumber = GetElementText(AllAccountNumber[i]);
                string accountRef = GetElementText(AllAccountRef[i]);
                string partyType = GetElementText(AllPartyType[i]);
                string startDate = GetElementText(AllStartDate[i]);
                list.Add(new PartyModel(partyName, contractName, accountNumber, accountRef, partyType, startDate));
            }

            return list;
        }
        [AllureStep]
        public PartyCommonPage VerifyPartyCreated(PartyModel partyModelInput, PartyModel partyModelActual)
        {
            Assert.AreEqual(partyModelActual.ContractName, partyModelInput.ContractName);
            Assert.AreEqual(partyModelActual.PartyName, partyModelInput.PartyName);
            Assert.AreEqual(partyModelActual.PartyType, partyModelInput.PartyType);
            Assert.AreEqual(partyModelActual.StartDate, partyModelInput.StartDate);
            return this;
        }
        [AllureStep]
        public PartyCommonPage FilterPartyById(int id)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(AddNewItem);
            SendKeys(filterInputById, id.ToString());
            ClickOnElement(applyBtn);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public PartyCommonPage FilterPartyById(string id)
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(AddNewItem);
            SendKeys(filterInputById, id);
            SendKeys(filterInputById, Keys.Enter);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public DetailPartyPage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<DetailPartyPage>();
        }

        [AllureStep]
        public PartyCommonPage IsPartyPageLoaded()
        {
            WaitUtil.WaitForAllElementsVisible(allPartyRows);
            return this;
        }

        [AllureStep]
        public PartyCommonPage VerifyDisplayVerticalScrollBarPartiesPage()
        {
            VerifyDisplayVerticalScrollBar(containerPage);
            return this;
        }
    }
}
