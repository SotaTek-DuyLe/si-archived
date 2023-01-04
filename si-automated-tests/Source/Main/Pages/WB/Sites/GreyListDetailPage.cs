using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.WB
{
    public class GreyListDetailPage : BasePageCommonActions
    {
        public readonly By GreyListCodeSelect = By.XPath("//select[@id='code']");
        public readonly By CommentInput = By.XPath("//textarea[@id='comment']");
        public readonly By TicketInput = By.XPath("//button[@data-id='ticket']");
    }
}
