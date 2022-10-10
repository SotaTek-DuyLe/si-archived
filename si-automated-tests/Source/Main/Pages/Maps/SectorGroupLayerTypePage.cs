using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Pages.Maps
{
    public class SectorGroupLayerTypePage : BasePageCommonActions
    {
        public readonly By SectorGroupTab = By.XPath("//a[@aria-controls='sector-group-tab']");
        public readonly By SectorGroupNameInput = By.XPath("//input[@id='sectorgroup-name']");
        public readonly By SectorGroupTypeSelect = By.XPath("//select[@id='sectorgroups-type']");
        public readonly By SectorInput = By.XPath("//input[@id='sectorgroups']");
    }
}
