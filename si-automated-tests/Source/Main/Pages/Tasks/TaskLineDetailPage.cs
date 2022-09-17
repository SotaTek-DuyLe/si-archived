using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;

namespace si_automated_tests.Source.Main.Pages.Tasks
{
    public class TaskLineDetailPage : BasePageCommonActions
    {
        public readonly By DetailTab = By.CssSelector("a[aria-controls='details-tab']");
        public readonly By HistoryTab = By.CssSelector("a[aria-controls='history-tab']");
        public readonly By ProductSelect = By.XPath("//div[@id='details-tab']//echo-select[contains(@params, 'product')]//select");
        public readonly By MinAssetQty = By.XPath("//div[@id='details-tab']//input[@id='min-asset-qty']");
        public readonly By MaxAssetQty = By.XPath("//div[@id='details-tab']//input[@id='max-asset-qty']");
        public readonly By MinProductQty = By.XPath("//div[@id='details-tab']//input[@id='min-product-qty']");
        public readonly By MaxProductQty = By.XPath("//div[@id='details-tab']//input[@id='max-product-qty']");
        public readonly By StateSelect = By.XPath("//div[@id='details-tab']//select[@id='state']");
        public readonly By HistoryDetail = By.XPath("//div[@id='history-tab']//div[@data-bind='html: ew.renderChangesHtml(changes)'][1]");
    }
}
