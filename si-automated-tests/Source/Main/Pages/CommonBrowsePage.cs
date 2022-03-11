using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages
{
    public class CommonBrowsePage : BasePage
    {
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");
        private readonly By firstResult = By.XPath("//div[@class='ui-widget-content slick-row even']");
        public CommonBrowsePage FilterItem(int id)
        {
            SendKeys(filterInputById, id.ToString());
            ClickOnElement(applyBtn);
            return this;
        }
        public CommonBrowsePage OpenFirstResult()
        {
            DoubleClickOnElement(firstResult);
            return PageFactoryManager.Get<CommonBrowsePage>();
        }
    }
}
