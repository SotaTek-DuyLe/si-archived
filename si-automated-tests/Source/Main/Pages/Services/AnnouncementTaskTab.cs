using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class AnnouncementTaskTab : BasePageCommonActions
    {
        public readonly By AddNewItemButton = By.XPath("//div[@id='announcements-tab']//button[text()[contains(.,'Add New Item')]]");
        public readonly By DeleteItemButton = By.XPath("//div[@id='announcements-tab']//button[text()[contains(.,'Delete Item')]]");

        private string AnnouncementTaskTable = "//div[@id='announcements-tab']//div[@class='grid-canvas']";
        private string AnnouncementTaskRow = "./div[contains(@class, 'slick-row')]";
        private string CheckboxCell = "./div[contains(@class, 'l0')]//input[@type='checkbox']";
        private string IdCell = "./div[contains(@class, 'l1')]";
        private string NameCell = "./div[contains(@class, 'l2')]";
        private string TypeCell = "./div[contains(@class, 'l3')]";
        private string ValidFromCell = "./div[contains(@class, 'l4')]";
        private string ValidToCell = "./div[contains(@class, 'l5')]";

        public TableElement AnnouncementTaskTableEle
        {
            get => new TableElement(AnnouncementTaskTable, AnnouncementTaskRow, new List<string>() { CheckboxCell, IdCell, NameCell, TypeCell, ValidFromCell, ValidToCell });
        }
        [AllureStep]
        public AnnouncementTaskTab VerifyAnnouncementTaskData(string name, string type, string validFrom, string validTo)
        {
            VerifyCellValue(AnnouncementTaskTableEle, 0, 2, name);
            VerifyCellValue(AnnouncementTaskTableEle, 0, 3, type);
            VerifyCellValue(AnnouncementTaskTableEle, 0, 4, validFrom);
            VerifyCellValue(AnnouncementTaskTableEle, 0, 5, validTo);
            return this;
        }
    }
}
