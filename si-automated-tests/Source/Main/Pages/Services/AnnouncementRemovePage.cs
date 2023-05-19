using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class AnnouncementRemovePage : BasePageCommonActions
    {
        public readonly By YesButton = By.XPath("//div[@class='modal-dialog']//button[text()='Yes']");
    }
}
