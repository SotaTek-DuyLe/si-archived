using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agrrements
{
    //Task tab inside An Agreement 
    public class AgreementPageTaskTab : BasePage
    {
        private readonly By taskTabBtn = By.XPath("//a[@aria-controls='tasks-tab']");
        private readonly By refreshBtn = By.XPath("//button[@title='Refresh']");
        private string firstTask = "(//div[text()='Deliver Commercial Bin'])[2]";
        private string secondTask = "(//div[text()='Deliver Commercial Bin'])[1]";
        private string dueDateColumn = "/following-sibling::div[2]";
        private string taskTypeColumn = "//div[contains(@class,'r11')]";
        private string allColumnTitle = "//div[contains(@class, 'slick-header-columns')]/div/span[1]";
        private string eachColumn = "//div[@class='grid-canvas']/div/div[{0}]";
        private string allRows = "//div[@class='grid-canvas']/div";
        private string deliverCommercialBinWithDateRows = "//div[@class='grid-canvas']/div[contains(.,'Deliver Commercial Bin') and contains(.,'{0}')]";
        private string retiredTasks = "//div[@class='grid-canvas']/div[contains(@class,'retired')]";

        private readonly By createAdhocBtn = By.XPath("//button[text()='Create Ad-Hoc Task']");
    }
}
