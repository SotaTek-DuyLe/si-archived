using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Models.DBModels;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class ServiceTaskSchedulePage : BasePageCommonActions
    {
        public readonly By StartDateInput = By.XPath("//div[@id='details-tab']//input[@id='startDate.id']");
        public readonly By EndDateInput = By.XPath("//div[@id='details-tab']//input[@id='endDate.id']");
        public readonly By TimeBandInput = By.XPath("//div[@id='details-tab']//select[@id='timeband.id']");
        public readonly By UseRoundScheduleRadio = By.XPath("//div[@id='details-tab']//input[@id='rd-round']");
        public readonly By RoundSelect = By.XPath("//div[@id='div-round']//echo-select[contains(@params, 'round')]//select");
        public readonly By RoundLegSelect = By.XPath("//div[@id='div-roundLeg']//select[@id='roundLeg.id']");
    }
}
