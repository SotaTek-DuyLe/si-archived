using NUnit.Framework;
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
    public class ObjectNoticeFormPage : BasePageCommonActions
    {
        public readonly By NoticeTypeSelect = By.XPath("//div[@id='details-tab']//select[@id='noticeType.id']");
        public readonly By SystemSelect = By.XPath("//div[@id='details-tab']//select[@id='echoSystem.id']");
        public readonly By StandardNoticeTypeSelect = By.XPath("//div[@id='details-tab']//select[@id='standardNoticeType.id']");
        public readonly By DescriptionText = By.XPath("//div[@id='details-tab']//textarea[@id='description.id']");
        public readonly By HeaderStatus = By.XPath("//h5[@id='header-status']//span");
        public readonly By StartDateInput = By.XPath("//div[@id='details-tab']//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//div[@id='details-tab']//input[@id='endDate.id']");
    }
}
