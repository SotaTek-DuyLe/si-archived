﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ObjectNoticeTab : BasePageCommonActions
    {
        public readonly By objectNoticeTab = By.XPath("//a[@aria-controls='objectNotices-tab']");
        private readonly string ObjectNoticeTable = "//div[@id='objectNotices-tab']//div[@class='grid-canvas']";
        private readonly string ObjectNoticeRow = "./div[contains(@class, 'slick-row')]";
        private readonly string IdCell = "./div[contains(@class, 'l1 r1')]";
        private readonly string DescriptionCell = "./div[contains(@class, 'l4 r4')]";
        private readonly string StartDateCell = "./div[contains(@class, 'l5 r5')]";
        private readonly string EndDateCell = "./div[contains(@class, 'l6 r6')]";
        private readonly string SystemCell = "./div[contains(@class, 'l7 r7')]";

        public TableElement ObjectNoticeTableEle
        {
            get => new TableElement(ObjectNoticeTable, ObjectNoticeRow, new List<string>() { IdCell, DescriptionCell, StartDateCell, EndDateCell, SystemCell });
        }

        public BasePageCommonActions VerifyNewObjectNotice(string description, string system, string startDate)
        {
            int rowIdx = ObjectNoticeTableEle.GetRows().Count - 1;
            Assert.IsTrue(ObjectNoticeTableEle.GetCellValue(rowIdx, 1).AsString() == description);
            Assert.IsTrue(ObjectNoticeTableEle.GetCellValue(rowIdx, 2).AsString() == startDate);
            Assert.IsTrue(ObjectNoticeTableEle.GetCellValue(rowIdx, 4).AsString() == system);
            return this;
        }

        public string GetIdNewObjectNotice()
        {
            int rowIdx = ObjectNoticeTableEle.GetRows().Count - 1;
            return ObjectNoticeTableEle.GetCellValue(rowIdx, 0).AsString();
        }

        public BasePageCommonActions DoubleClickNewObjectNotice()
        {
            int rowIdx = ObjectNoticeTableEle.GetRows().Count - 1;
            ObjectNoticeTableEle.DoubleClickRow(rowIdx);
            return this;
        }
    }
}
