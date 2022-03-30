﻿using NUnit.Framework;
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
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");

        private readonly By firstAgreementId = By.XPath("//div[@class='slick-cell l1 r1']");
        private readonly By firstAgreementName = By.XPath("//div[@class='slick-cell l2 r2']");
        private readonly By firstAgreementStartDate = By.XPath("//div[@class='slick-cell l9 r9']");
        private readonly By firstAgreementEndDate = By.XPath("//div[@class='slick-cell l10 r10']");
        private readonly By firstAgreementType = By.XPath("//div[@class='slick-cell l13 r13']");
        private readonly By firstAgreementStatus = By.XPath("//div[@class='slick-cell l14 r14']");
        private readonly By firstAgreementRow = By.XPath("//div[@id='agreements-tab']//div[@class='grid-canvas']/div[1]");


        public PartyAgreementPage ClickAddNewItem()
        {
            ClickOnElement(addNewItemBtn);
            return PageFactoryManager.Get<PartyAgreementPage>();
        }

        public AgreementTab VerifyFirstAgreementInfo(string name, string startDate, string endDate, string type, string status)
        {
            Assert.AreEqual(name, GetElementText(firstAgreementName));
            Assert.AreEqual(startDate, GetElementText(firstAgreementStartDate));
            Assert.AreEqual(endDate, GetElementText(firstAgreementEndDate));
            Assert.AreEqual(type, GetElementText(firstAgreementType));
            Assert.AreEqual(status, GetElementText(firstAgreementStatus));
            return this;
        }
        public AgreementTab VerifyAgreementAppear(string id, string name, string startDate, string endDate, string status)
        {
            List<IWebElement> idList = GetAllElements(firstAgreementId);
            List<IWebElement> nameList = GetAllElements(firstAgreementName);
            List<IWebElement> startDateList = GetAllElements(startDate);
            List<IWebElement> endDateList = GetAllElements(endDate);
            List<IWebElement> statusList = GetAllElements(status);
            int n = 0;
            for(int i = 0; i< idList.Count; i++)
            {
                if (GetElementText(idList[i]).Equals(id))
                {
                    Assert.AreEqual(name, GetElementText(nameList[i]));
                    Assert.AreEqual(startDate, GetElementText(startDateList[i]));
                    Assert.AreEqual(endDate, GetElementText(endDateList[i]));
                    Assert.AreEqual(status, GetElementText(statusList[i]));
                    n = 0;
                    break;
                }
                else { n++; }
            }
            //Fail if cannot find element 
            Assert.AreEqual(n, 0);
            return this;
        }
        public PartyAgreementPage OpenAgreementWithId(int id)
        {
            List<IWebElement> idList = GetAllElements(firstAgreementId);
            for (int i = 0; i < idList.Count; i++)
            {
                if (GetElementText(idList[i]).Equals(id.ToString()))
                {
                    DoubleClickOnElement(idList[i]);
                }
            }
            return PageFactoryManager.Get<PartyAgreementPage>(); 
        }
        public PartyAgreementPage OpenFirstAgreement()
        {
            DoubleClickOnElement(firstAgreementId);
            return PageFactoryManager.Get<PartyAgreementPage>();
        }

        public int GetAgreementId()
        {
            return Int32.Parse(WaitUtil.WaitForElementVisible(firstAgreementId).Text);
        }


        public AgreementDetailPage OpenFirstAgreementRow()
        {
            DoubleClickOnElement(firstAgreementRow);
            return PageFactoryManager.Get<AgreementDetailPage>();
        }
    }
}
