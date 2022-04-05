﻿using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages
{
    public class CommonBrowsePage : BasePage
    {
        private readonly By addNewItemBtn = By.XPath("//button[text()='Add New Item']");
        private readonly string customBtn = "//button[text()='{0}']";
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstResult = By.XPath("//div[@class='ui-widget-content slick-row even']");
        private readonly By headers = By.XPath("//div[@class='ui-state-default slick-header-column slick-header-sortable ui-sortable-handle']/span[1]");
        private readonly By firstResultFields = By.XPath("//div[@class='ui-widget-content slick-row even'][1]/div");
        public CommonBrowsePage FilterItem(int id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id.ToString());
            ClickOnElement(applyBtn);
            return this;
        }
        public CommonBrowsePage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<CommonBrowsePage>();
        }
        public CommonBrowsePage ClickAddNewItem()
        {
            ClickOnElement(addNewItemBtn);
            return this;
        }
        public CommonBrowsePage VerifyFirstResultValue(string field, string expected)
        {
            IList<IWebElement> hds = WaitUtil.WaitForAllElementsVisible(headers);
            for (int i = 0; i < hds.Count; i++)
            {
                if (hds[i].Text.Equals(field, StringComparison.OrdinalIgnoreCase))
                {
                    IList<IWebElement> _firstResultFields = WaitUtil.WaitForAllElementsVisible(firstResultFields);
                    Assert.AreEqual(expected, _firstResultFields[i + 1].Text);
                }
            }
            return this;
        }
        public CommonBrowsePage ClickButton(string _buttonName)
        {
            ClickOnElement(customBtn, _buttonName);
            return this;
        }
    }
}
