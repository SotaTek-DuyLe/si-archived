using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class SectorGroupPage : BasePageCommonActions
    {
        public readonly By AddNewItemButton = By.XPath("//button[text()='Add New Item']");
        public readonly By CopyItemButton = By.XPath("//button[text()='Copy Item']");
        private readonly string SectorTable = "//div[@class='grid-canvas']";
        
    }
}
