using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundCalendarPage : BasePageCommonActions
    {
        public readonly By SelectContact = By.XPath("//select[@id='contract']");
        public readonly By SelectShiftGroup = By.XPath("//select[@id='shift-group']");
        public readonly By InputService = By.XPath("//input[@id='services']");

        private TreeViewElement ServicesTreeView
        {
            get => new TreeViewElement("//div[contains(@class, 'jstree-1')]", "./li[contains(@role, 'treeitem')]", "./ul[contains(@class, 'jstree-children')]");
        }

        public RoundCalendarPage SelectNode(string nodeName)
        {
            ServicesTreeView.ClickItem(nodeName);
            return this;
        }
    }
}
