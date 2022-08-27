using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace si_automated_tests.Source.Main.Pages.Common
{
    public class LocalLanguagePage : BasePageCommonActions
    {
        public readonly By LanguageSelect = By.XPath("//select[@id='localeLanguages']");
        public readonly By CloseButton = By.XPath("//button[@id='close-window']");
        public readonly By SaveButton = By.XPath("//button[@id='save']");
    }
}
