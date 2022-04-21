using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Inspections
{
    public class AllInspectionListingPage : BasePage
    {
        private readonly By allRowInInspectionTabel = By.XPath("//div[@class='grid-canvas']/div");

        //DYNAMIC LOCATOR
        private const string columnInRowInspection = "//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        public List<InspectionModel> getAllInspectionInList()
        {
            List<InspectionModel> allModel = new List<InspectionModel>();
            List<IWebElement> allRow = GetAllElements(allRowInInspectionTabel);
            for (int i = 0; i < allRow.Count; i++)
            {
                string id = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[0])[i]);
                string point = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[1])[i]);
                string inspectionType = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[2])[i]);
                string createdDate = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[3])[i]);
                string createdByUser = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[4])[i]);
                string inspectionScheme = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[5])[i]);
                string assignedUser = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[6])[i]);
                string allocatedUnit = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[7])[i]);
                string status = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[8])[i]);
                string clientReference = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[9])[i]);
                string contract = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[10])[i]);
                string validFrom = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[11])[i]);
                string validTo = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[12])[i]);
                string completionDate = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[13])[i]);
                string lastUpdated = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[14])[i]);
                string cancelledDate = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[15])[i]);
                string gpsDescription = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[16])[i]);
                string source = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[17])[i]);
                string service = GetElementText(GetAllElements(columnInRowInspection, CommonConstants.InspectionColumnInListingPage[18])[i]);
                allModel.Add(new InspectionModel(id, point, inspectionType, createdDate, createdByUser, inspectionScheme, assignedUser, allocatedUnit, status, clientReference, contract, validFrom, validTo, completionDate, lastUpdated, cancelledDate, gpsDescription, source, service));
            }
            return allModel;
        }

        public AllInspectionListingPage VerifyTheFirstInspection(InspectionModel inspectionModelExpected, InspectionModel inspectionModelActual, string location, string contract, string source, string service)
        {
            Assert.AreEqual(inspectionModelExpected.ID, inspectionModelActual.ID);
            Assert.AreEqual(location, inspectionModelActual.point);
            Assert.AreEqual(inspectionModelExpected.inspectionType, inspectionModelActual.inspectionType);
            Assert.AreEqual(inspectionModelExpected.createdByUser, inspectionModelActual.createdByUser);
            Assert.AreEqual(inspectionModelExpected.assignedUser, inspectionModelActual.assignedUser);
            Assert.AreEqual(inspectionModelExpected.allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(inspectionModelExpected.status, inspectionModelActual.status);
            Assert.AreEqual(contract, inspectionModelActual.contract);
            Assert.AreEqual(inspectionModelExpected.validFrom, inspectionModelActual.validFrom);
            Assert.AreEqual(inspectionModelExpected.validTo, inspectionModelActual.validTo);
            Assert.AreEqual(source, inspectionModelActual.source);
            Assert.AreEqual(service, inspectionModelActual.service);
            return this;
        }
    }
}
