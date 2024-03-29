﻿using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AddAndEditService
{
    public class AddServicePage : BasePageCommonActions
    {
        private readonly By serviceTabs = By.XPath("//span[contains(@class,'btn btn-circle')]");
        private readonly By saveBtn = By.XPath("//button[text()='Next']");
        private readonly By finishBtn = By.XPath("//button[text()='Finish']");
        private readonly By backBtn = By.XPath("//button[text()='Back']");
        public readonly By nextBtn = By.XPath("//button[text()='Next']");

        [AllureStep]
        public AddServicePage IsOnAddServicePage()
        {
            WaitUtil.WaitForAllElementsVisible(serviceTabs);
            Assert.IsTrue(IsControlDisplayed(serviceTabs));
            return this;
        }

        [AllureStep]
        public AddServicePage ClickSave()
        {
            ClickOnElement(saveBtn);
            return this;
        }
        [AllureStep]
        public AddServicePage ClickNext()
        {
            ScrollDownToElement(nextBtn);
            ClickOnElement(nextBtn);
            return this;
        }
        [AllureStep]
        public AddServicePage ClickBack()
        {
            ClickOnElement(backBtn);
            return this;
        }
        [AllureStep]
        public AddServicePage ClickFinish()
        {
            ClickOnElement(finishBtn);
            return this;
        }

    }
}
