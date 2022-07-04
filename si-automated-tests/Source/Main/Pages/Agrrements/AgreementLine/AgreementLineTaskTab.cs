using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models.Agreement;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTabs;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementLine
{
    public class AgreementLineTaskTab : TaskTab
    {
        private string allRows = "//div[@class='grid-canvas']/div";
        private string idColumns = "//div[@class='grid-canvas']/div/div[contains(@class,'r5')]/div";
        private string taskStateColumn = "//div[@class='grid-canvas']/div/div[contains(@class,'r6')]";
        public List<AgreementTaskModel> GetAllTaskInListAgreementLine()
        {
            List<AgreementTaskModel> list = new List<AgreementTaskModel>();
            List<IWebElement> allRow = GetAllElements(allRows);

            List<IWebElement> ids = GetAllElements(idColumns);
            List<IWebElement> taskStates = GetAllElements(taskStateColumn);

            for (int i = 0; i < allRow.Count; i++)
            {
                string id = GetElementText(ids[i]);
                string taskState = GetElementText(taskStates[i]);

                AgreementTaskModel task = new AgreementTaskModel("", "", id, taskState, "", "", "", ""); //No need other data here 
                list.Add(task);
            }
            return list;
        }

        public AgreementLineTaskTab VerifyTaskStateWithIdsAgreementLine(int[] idList, string state)
        {
            List<AgreementTaskModel> listTasks = this.GetAllTaskInListAgreementLine();
            int n = 0;
            for (int j = 0; j < idList.Length; j++)
            {

                int id = idList[j];
                for (int i = 0; i < listTasks.Count; i++)
                {
                    if ((listTasks[i].Id).Equals(id.ToString()))
                    {
                        Assert.AreEqual(listTasks[i].Id, id.ToString());
                        Assert.AreEqual(listTasks[i].TaskState, state);
                        n++;
                        break;
                    }
                }
            }
            Assert.AreEqual(n, idList.Length);
            return this;
        }
    }
}
