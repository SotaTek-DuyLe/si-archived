using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Applications
{
    public class RoundInstanceDetailPage : BasePageCommonActions
    {
        public RoundInstanceDetailPage()
        {
            unallocatedTableEle = new TableElement("//div[@id='grid']//div[@class='grid-canvas']", UnallocatedRow, new List<string>() { UnallocatedCheckbox, UnallocatedState, UnallocatedID });
            unallocatedTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };

            slickRoundTableEle = new TableElement("//div[@id='grid']//div[@class='grid-canvas']", SlickRoundRow, new List<string>() { RoundDescriptionCell });
            slickRoundTableEle.GetDataView = (IEnumerable<IWebElement> rows) =>
            {
                return rows.OrderBy(row => row.GetCssValue("top").Replace("px", "").AsInteger()).ToList();
            };
        }
        public readonly By WorkSheetTab = By.XPath("//a[@aria-controls='worksheet-tab']");
        public readonly By ExpandRoundsGo = By.XPath("//button[@id='t-toggle-rounds']");
        public readonly By IdFilterInput = By.XPath("//div[@id='grid']//div[contains(@class, 'l3')]//input");
        public readonly string UnallocatedRow = "./div[contains(@class, 'assured')]";
        public readonly string UnallocatedCheckbox = "./div[contains(@class, 'slick-cell l0 r0')]//input";
        public readonly string UnallocatedState = "./div[contains(@class, 'slick-cell l1 r1')]";
        public readonly string UnallocatedID = "./div[contains(@class, 'slick-cell l3 r3')]";

        public readonly string SlickRoundRow = "./div[contains(@class, 'slick-group')]";
        public readonly string RoundDescriptionCell = "./div[contains(@class, 'slick-cell l0')]";

        private TableElement slickRoundTableEle;
        public TableElement SlickRoundTableEle
        {
            get => slickRoundTableEle;
        }

        private TableElement unallocatedTableEle;
        public TableElement UnallocatedTableEle
        {
            get => unallocatedTableEle;
        }

        [AllureStep]
        public RoundInstanceDetailPage VerifyRoundInstanceStatusCompleted()
        {
            IWebElement cell = UnallocatedTableEle.GetCell(0, 1);
            IWebElement img = cell.FindElement(By.XPath("./div//img"));
            Assert.IsTrue(img.GetAttribute("src").Contains("coretaskstate/s3.png"));
            return this;
        }

    }
}
