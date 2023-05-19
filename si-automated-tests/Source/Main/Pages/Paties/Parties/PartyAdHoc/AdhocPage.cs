using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Adhoc;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyCalendar;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySitePage;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyVehiclePage;
using si_automated_tests.Source.Main.Pages.WB.Tickets;
using CanlendarServiceTask = si_automated_tests.Source.Main.Models.Suspension.ServiceTaskModel;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAdHoc
{
    public class AdhocPage : BasePage
    {
        private readonly By adhocCells = By.XPath("//div[@id='adhoc-tab']//div[@class='grid-canvas']//div//*");
        private readonly string adhocTable = "//div[@id='adhoc-tab']//div[@class='grid-canvas']";
        private string adhocRow = "./div[contains(@class, 'slick-row')]";
        private string siteCell = "./div[@class='slick-cell l0 r0']";
        private string addressCell = "./div[@class='slick-cell l1 r1']";
        private string serviceCell = "./div[@class='slick-cell l2 r2']";
        private string taskTypeCell = "./div[@class='slick-cell l3 r3']";
        private string taskLineCell = "./div[@class='slick-cell l4 r4']";
        private string createAdhocBtnCell = "./div[@class='slick-cell l5 r5']//button";
        
        public TableElement AdhocTableEle
        {
            get => new TableElement(adhocTable, adhocRow, new List<string>() { siteCell, addressCell, serviceCell, taskTypeCell, taskLineCell, createAdhocBtnCell });
        } 

        [AllureStep]
        public AdhocPage ClickCreateAdHocTask(string taskType, string taskLine)
        {
            var cell = AdhocTableEle.GetCellByCellValues(AdhocTableEle.GetCellIndex(createAdhocBtnCell), new Dictionary<int, object>() 
            { 
                { AdhocTableEle.GetCellIndex(taskTypeCell), taskType }, 
                { AdhocTableEle.GetCellIndex(taskLineCell), taskLine }, 
            });
            cell?.Click();
            return this;
        }
    }
}
