using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.PartyAgreement;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Paties
{
    public class AgreementTab : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");

        private readonly By firstAgreementId = By.XPath("//div[@class='slick-cell l1 r1']");
        private readonly By firstAgreementName = By.XPath("//div[@class='slick-cell l2 r2']");
        private readonly By firstAgreementStartDate = By.XPath("//div[@class='slick-cell l9 r9']");
        private readonly By firstAgreementEndDate = By.XPath("//div[@class='slick-cell l10 r10']");
        private readonly By firstAgreementType = By.XPath("//div[@class='slick-cell l13 r13']");
        private readonly By firstAgreementStatus = By.XPath("//div[@class='slick-cell l14 r14']");

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
        public int GetAgreementId()
        {
            return Int32.Parse(WaitUtil.WaitForElementVisible(firstAgreementId).Text);
        }
    }
}
