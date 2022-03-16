﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Pages.Agrrements;

namespace si_automated_tests.Source.Main.Pages.Paties.SiteServices
{
    public class SiteServicesCommonPage : BasePage
    {
        private readonly By agreementIdInput = By.XPath("((//span[text()='Agreement ID']/ancestor::div[contains(@class,'ui-state-default')])[1]/following-sibling::div[1]//input)[count(//span[text()='Agreement ID']/parent::div/preceding-sibling::div)]");
        private readonly By idInput = By.XPath("//div[@class='slick-headerrow-columns']/div[2]//input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstResult = By.XPath("//div[@class='ui-widget-content slick-row even']");
        private readonly By firstRowResultId = By.XPath("//div[@class='ui-widget-content slick-row even']//div[2]");
        private readonly By firstRowAgreementId = By.XPath("//div[@class='ui-widget-content slick-row even']//div[count(//span[text()='Agreement ID']/parent::div/preceding-sibling::div) + boolean(count(//span[text()='Agreement ID']))]");

        public SiteServicesCommonPage FilterId(int id)
        {
            SendKeys(idInput, id.ToString());
            ClickOnElement(applyBtn);
            return this;
        }

        public SiteServicesCommonPage FilterAgreementId(int id)
        {
            SendKeys(agreementIdInput, id.ToString());
            ClickOnElement(applyBtn);
            return this;
        }
        public SiteServicesCommonPage VerifyFirstLineAgreementResult(int id, int agreementId)
        {
            WaitUtil.WaitForAllElementsVisible(firstRowResultId);
            Assert.AreEqual(GetElementText(firstRowResultId), id.ToString());
            Assert.AreEqual(GetElementText(firstRowAgreementId), agreementId.ToString());
            return this;
        }
        public AgreementLinePage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<AgreementLinePage>();
        }

        public SiteServicesCommonPage VerifyAgreementWindowClosed()
        {
            Assert.AreEqual(GetNumberOfWindowHandle(), 1);
            return this;
        }
    }
}
