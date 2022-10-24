﻿using System;
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
    public class TaskTypeEchoExtraPage : BasePageCommonActions
    {
        public readonly By TabPage = By.XPath("//div[@class='tabstrip_s1']/ul");
        
        public By GetCompleteTaskTypeStateCheckboxXPath(string echoId)
        {
            return By.XPath($"//div[@tpfx='TabPage_' and contains(@style, 'visibility: visible')]//table//tbody//tr[@echoid='{echoId}']//td[4]//input");
        }

        public By GetNotCompleteTaskTypeStateCheckboxXPath(string echoId)
        {
            return By.XPath($"//div[@tpfx='TabPage_' and contains(@style, 'visibility: visible')]//table//tbody//tr[@echoid='{echoId}']//td[4]//input");
        }
    }
}
