using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class HelpPage : BasePageCommonActions
    {
        public readonly By HelpTitle = By.XPath("//h2[text()='Help']");
        public readonly By EchoWikiLink = By.XPath("//a[text()='ECHO WIKI']");
        public readonly By ButtonClose = By.XPath("//a[text()='Close']");
    }
}
