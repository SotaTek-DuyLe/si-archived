using System;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.WB
{
    public class DetailWBStationPage : BasePage
    {
        private readonly By title = By.XPath("//h4[text()='WEIGHBRIDGE STATION']");
        private readonly By notesTab = By.CssSelector("a[aria-controls='notes-tab']");
        private readonly By titleInNotesTab = By.XPath("//label[text()='Title']/following-sibling::input");
        private readonly By noteInNotesTab = By.XPath("//label[text()='Note']/following-sibling::textarea");

        //DYANMIC
        private readonly string stationName = "//h4[text()='{0}']";

        [AllureStep]
        public DetailWBStationPage IsDetailWBStationPage(string stationNameValue)
        {
            WaitUtil.WaitForElementVisible(title);
            WaitUtil.WaitForElementVisible(stationName, stationNameValue);
            return this;
        }

        [AllureStep]
        public DetailWBStationPage ClickOnNotesTab()
        {
            ClickOnElement(notesTab);
            return this;
        }

        [AllureStep]
        public DetailWBStationPage IsNotesTab()
        {
            Assert.IsTrue(IsControlDisplayed(titleInNotesTab), "Title in Notes tab is not displayed");
            Assert.IsTrue(IsControlDisplayed(noteInNotesTab), "Note in Notes tab is not displayed");
            return this;
        }
    }
}
