using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.WB.Sites
{
    public class SiteListingPage : BasePage
    {
        public readonly By siteRow = By.XPath("//div[@class='grid-canvas']/div");
        public readonly By idRow = By.XPath("//div[@class='grid-canvas']/div/div[2]");

        //DYNAMIC
        public const string columnOfRow = "//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        public List<SiteModel> GetAllSiteDisplayed()
        {
            WaitForLoadingIconToDisappear();
            List<SiteModel> allSiteModel = new List<SiteModel>();
            List<IWebElement> allIdRow = GetAllElements(idRow);
            List<IWebElement> allNameRow = GetAllElements(string.Format(columnOfRow, "Name"));
            List<IWebElement> allSiteTypeRow = GetAllElements(string.Format(columnOfRow, "Site Type"));
            List<IWebElement> allStartDateRow = GetAllElements(string.Format(columnOfRow, "Start Date"));
            List<IWebElement> allEndDateRow = GetAllElements(string.Format(columnOfRow, "End Date"));
            List<IWebElement> allClientRef = GetAllElements(string.Format(columnOfRow, "Client Reference"));
            List<IWebElement> allRow = GetAllElements(siteRow);
            for(int i = 0; i < allRow.Count; i++)
            {
                string id = GetElementText(allIdRow[i]);
                string name = GetElementText(allNameRow[i]);
                string siteType = GetElementText(allSiteTypeRow[i]);
                string startDate = GetElementText(allStartDateRow[i]);
                string endDate = GetElementText(allEndDateRow[i]);
                string clientRef = GetElementText(allClientRef[i]);
                allSiteModel.Add(new SiteModel(id, name, siteType, startDate, endDate, clientRef));
            }
            return allSiteModel;
        }

        public SiteListingPage VerifySiteCreatedIsNotDisplayed(List<SiteModel> siteModelsActual, List<SiteModel> siteModelsCreated, List<SiteModel> siteModelsBefore)
        {
            Assert.AreEqual(siteModelsBefore.Count, siteModelsActual.Count);
            for(int i = 0; i < siteModelsActual.Count; i++)
            {
                for(int j = 0; j < siteModelsCreated.Count; j++)
                {
                    Assert.AreNotEqual(siteModelsActual[i].Name, siteModelsCreated[j].Name);
                }
            }
            return this;
        }

        public SiteListingPage VerifyDisplayNewSite(SiteModel siteModelInput, SiteModel siteModelNew)
        {
            Assert.AreEqual(siteModelInput.Name, siteModelNew.Name );
            Assert.AreEqual(siteModelInput.Id, siteModelNew.Id);
            Assert.AreEqual(siteModelInput.SiteType, siteModelNew.SiteType);
            Assert.AreEqual(siteModelInput.StartDate, siteModelNew.StartDate + " 00:00");
            Assert.AreEqual(siteModelInput.EndDate, siteModelNew.EndDate + " 00:00");
            return this;
        }
    }
}
