using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Paties;

namespace si_automated_tests.Source.Main.Pages
{
    public class PartyCommonPage : BasePage
    {
        private const string AddNewItem = "//button[text()='Add New Item']";

        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstResult = By.XPath("//div[@class='ui-widget-content slick-row even']");


        public PartyCommonPage ClickAddNewItem()
        {
            SwitchNewIFrame();
            ClickOnElement(AddNewItem);
            return this;
        }
        public List<PartyModel> GetAllPartyListing()
        {
            List<PartyModel> list = new List<PartyModel>();
            SwitchNewIFrame();
            List<IWebElement> AllPartyRows = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]");
            List<IWebElement> AllPartyName = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[3]");
            List<IWebElement> AllContractName = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[4]");
            List<IWebElement> AllAccontNumber = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[5]");
            List<IWebElement> AllPartyType = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[17]");
            List<IWebElement> AllStartDate = GetAllElements("//div[@class='grid-canvas']/div[contains(@class, 'ui-widget-content')]/div[25]");
            for (int i = 0; i < AllPartyRows.Count; i++)
            {
                string partyName = GetElementText(AllPartyName[i]);
                string contractName = GetElementText(AllContractName[i]);
                string partyType = GetElementText(AllPartyType[i]);
                string startDate = GetElementText(AllStartDate[i]);
                list.Add(new PartyModel(partyName, contractName, partyType, startDate));
            }

            return list;
        }
        public PartyCommonPage VerifyPartyCreated(PartyModel partyModelInput, PartyModel partyModelActual)
        {
            Assert.AreEqual(partyModelActual.ContractName, partyModelInput.ContractName);
            Assert.AreEqual(partyModelActual.PartyName, partyModelInput.PartyName);
            Assert.AreEqual(partyModelActual.PartyType, partyModelInput.PartyType);
            Assert.AreEqual(partyModelActual.StartDate, partyModelInput.StartDate);
            return this;
        }
        public PartyCommonPage FilterPartyById(int id)
        {
            SendKeys(filterInputById, id.ToString());
            WaitForLoadingIconToDisappear();
            ClickOnElement(applyBtn);
            return this;
        }
        public DetailPartyPage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<DetailPartyPage>();
        }
    }
}
