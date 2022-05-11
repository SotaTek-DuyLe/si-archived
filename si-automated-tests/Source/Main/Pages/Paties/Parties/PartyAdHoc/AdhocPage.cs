using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
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

        public List<AdhocModel> GetAllAdhocTasks()
        {
            List<IWebElement> cells = GetAllElements(adhocCells);
            List<AdhocModel> adhocs = new List<AdhocModel>();
            for (int i = 0; i < cells.Count; i = i + 6)
            {
                AdhocModel adhocModel = new AdhocModel();
                adhocModel.Site = cells.Count > i ? GetElementText(cells[i]) : "";
                adhocModel.Address = cells.Count > i + 1 ? GetElementText(cells[i + 1]) : "";
                adhocModel.Service = cells.Count > i + 2 ? GetElementText(cells[i + 2]) : "";
                adhocModel.TaskType = cells.Count > i + 3 ? GetElementText(cells[i + 3]) : "";
                adhocModel.TaskLines = cells.Count > i + 4 ? GetElementText(cells[i + 4]) : "";
                adhocModel.CreateAdHocBtn = cells.Count > i + 5 ? cells[i + 5].FindElement(By.XPath("//button[text()='Create Ad-hoc Task']")) : null;
                adhocs.Add(adhocModel);
            }
            return adhocs;
        }

        public AdhocPage ClickCreateAdHocTask(string taskType)
        {
            AdhocModel adhoc = GetAllAdhocTasks().FirstOrDefault(x => x.TaskType == taskType);
            adhoc?.CreateAdHocBtn?.Click();
            return this;
        }
    }
}
