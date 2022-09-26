using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using si_automated_tests.Source.Main.Pages.Paties.PartyAgreement;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    //Agreement tab inside Party Detail Page
    public class AgreementTab : BasePage
    {
        private const string agreementTabId = "//div[@id='agreements-tab']";
        private readonly By addNewItemBtn = By.XPath(agreementTabId + "//button[text()='Add New Item']");

        private readonly By firstAgreementId = By.XPath(agreementTabId + "//div[@class='slick-cell l1 r1']");
        private readonly By firstAgreementName = By.XPath(agreementTabId + "//div[@class='slick-cell l2 r2']");
        private readonly By firstAgreementStartDate = By.XPath(agreementTabId + "//div[@class='slick-cell l9 r9']");
        private readonly By firstAgreementEndDate = By.XPath(agreementTabId + "//div[@class='slick-cell l10 r10']");
        private readonly By firstAgreementType = By.XPath(agreementTabId + "//div[@class='slick-cell l13 r13']");
        private readonly By firstAgreementStatus = By.XPath(agreementTabId + "//div[@class='slick-cell l15 r15']");
        private readonly By firstAgreementRow = By.XPath(agreementTabId + "//div[@class='grid-canvas']/div[1]");


        [AllureStep]
        public PartyAgreementPage ClickAddNewItem()
        {
            ClickOnElement(addNewItemBtn);
            return PageFactoryManager.Get<PartyAgreementPage>();
        }
        [AllureStep]
        public AgreementTab IsOnAgreementTab()
        {
            WaitUtil.WaitForElementVisible(addNewItemBtn);
            Assert.IsTrue(IsControlDisplayed(addNewItemBtn));
            return this;
        }
        [AllureStep]
        public AgreementTab VerifyFirstAgreementInfo(string name, string startDate, string endDate, string type, string status)
        {
            Assert.AreEqual(name, GetElementText(firstAgreementName));
            Assert.AreEqual(startDate, GetElementText(firstAgreementStartDate));
            Assert.AreEqual(endDate, GetElementText(firstAgreementEndDate));
            Assert.AreEqual(type, GetElementText(firstAgreementType));
            Assert.AreEqual(status, GetElementText(firstAgreementStatus));
            return this;
        }
        [AllureStep]
        public AgreementTab VerifyAgreementAppear(string id, string name, string _startDate, string _endDate, string _status)
        {
            List<IWebElement> idList = GetAllElements(firstAgreementId);
            List<IWebElement> nameList = GetAllElements(firstAgreementName);
            List<IWebElement> startDateList = GetAllElements(firstAgreementStartDate);
            List<IWebElement> endDateList = GetAllElements(firstAgreementEndDate);
            List<IWebElement> statusList = GetAllElements(firstAgreementStatus);
            int n = 0;
            for(int i = 0; i< idList.Count; i++)
            {
                if (GetElementText(idList[i]).Equals(id))
                {
                    Assert.AreEqual(name, GetElementText(nameList[i]));
                    Assert.AreEqual(_startDate, GetElementText(startDateList[i]));
                    Assert.AreEqual(_endDate, GetElementText(endDateList[i]));
                    Assert.AreEqual(_status, GetElementText(statusList[i]));
                    n = 0;
                    break;
                }
                else { n++; }
            }
            //Fail if cannot find element 
            Assert.AreEqual(n, 0);
            return this;
        }
        [AllureStep]
        public PartyAgreementPage OpenAgreementWithId(int id)
        {
            List<IWebElement> idList = GetAllElements(firstAgreementId);
            for (int i = 0; i < idList.Count; i++)
            {
                if (GetElementText(idList[i]).Equals(id.ToString()))
                {
                    DoubleClickOnElement(idList[i]);
                    break;
                }
            }
            return PageFactoryManager.Get<PartyAgreementPage>(); 
        }
        [AllureStep]
        public PartyAgreementPage OpenFirstAgreement()
        {
            DoubleClickOnElement(firstAgreementId);
            return PageFactoryManager.Get<PartyAgreementPage>();
        }
        [AllureStep]
        public int GetAgreementId()
        {
            return Int32.Parse(WaitUtil.WaitForElementVisible(firstAgreementId).Text);
        }

        [AllureStep]
        public AgreementDetailPage OpenFirstAgreementRow()
        {
            DoubleClickOnElement(firstAgreementRow);
            return PageFactoryManager.Get<AgreementDetailPage>();
        }
    }
}
