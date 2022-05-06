using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Resources.Tabs
{
    public class ResourceTermTab : BasePage
    {
        private readonly By term = By.Id("resource-term");
        private readonly By entitlementTab = By.XPath("//a[contains(text(),'Entitlements')]");
        private readonly By workPattern = By.XPath("//a[contains(text(),'Work Pattern')]");
        private readonly By panelFrame = By.XPath("//iframe[@role='tabpanel']");
        public ResourceTermTab IsOnTermTab()
        {
            WaitForLoadingIconToDisappear();
            WaitUtil.WaitForElementVisible(term);
            return this;
        }
        public ResourceTermTab SelectTerm(string _text)
        {
            SelectTextFromDropDown(term, _text);
            return this;
        }
        public ResourceTermTab VerifyExtraTabsArePresent()
        {
            SwitchToLastWindow();
            SwitchToFrame(panelFrame);
            WaitUtil.WaitForElementVisible(entitlementTab);
            WaitUtil.WaitForElementVisible(workPattern);
            SwitchToDefaultContent();
            return this;
        }
    }
}
