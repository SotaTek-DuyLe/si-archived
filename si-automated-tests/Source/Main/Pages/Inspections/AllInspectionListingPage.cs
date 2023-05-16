using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;

namespace si_automated_tests.Source.Main.Pages.Inspections
{
    public class AllInspectionListingPage : BasePage
    {
        private readonly By allRowInInspectionTabel = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By firstInspectionRow= By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By firstRow = By.XPath("//div[@class='grid-canvas']/div[1]");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By clearBtn = By.CssSelector("button[title='Clear Filters']");
        private readonly By statusInput = By.XPath("//div[contains(@class, 'l9')]//input");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");


        //DYNAMIC LOCATOR
        private const string columnInRowInspection = "//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";

        [AllureStep]
        public List<InspectionModel> getAllInspectionInList(int numberOfRow)
        {
            List<InspectionModel> allModel = new List<InspectionModel>();
            for (int i = 0; i < numberOfRow; i++)
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
        [AllureStep]
        public AllInspectionListingPage VerifyTheFirstInspection(InspectionModel inspectionModelExpected, InspectionModel inspectionModelActual, string location, string contract, string source, string service)
        {
            Assert.AreEqual(inspectionModelExpected.ID, inspectionModelActual.ID);
            Assert.AreEqual(location, inspectionModelActual.point);
            Assert.AreEqual(inspectionModelExpected.inspectionType, inspectionModelActual.inspectionType);
            Assert.IsTrue(inspectionModelActual.createdByUser.Contains(inspectionModelExpected.createdByUser));
            Assert.AreEqual(inspectionModelExpected.assignedUser, inspectionModelActual.assignedUser);
            Assert.AreEqual(inspectionModelExpected.allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(inspectionModelExpected.status, inspectionModelActual.status);
            Assert.AreEqual(contract, inspectionModelActual.contract);
            Assert.AreEqual(inspectionModelExpected.validFrom, inspectionModelActual.validFrom);
            Assert.AreEqual(inspectionModelExpected.validTo, inspectionModelActual.validTo);
            Assert.IsTrue(inspectionModelActual.source.Contains(source));
            Assert.AreEqual(service, inspectionModelActual.service);
            return this;
        }
        [AllureStep]
        public AllInspectionListingPage VerifyTheFirstInspection(InspectionModel inspectionModelExpected, InspectionModel inspectionModelActual, string location, string contract, string source, string service, string status)
        {
            Assert.AreEqual(inspectionModelExpected.ID, inspectionModelActual.ID);
            Assert.AreEqual(location, inspectionModelActual.point);
            Assert.AreEqual(inspectionModelExpected.inspectionType, inspectionModelActual.inspectionType);
            Assert.IsTrue(inspectionModelActual.createdByUser.Contains(inspectionModelExpected.createdByUser));
            Assert.AreEqual(inspectionModelExpected.assignedUser, inspectionModelActual.assignedUser);
            Assert.AreEqual(inspectionModelExpected.allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(status, inspectionModelActual.status);
            Assert.AreEqual(contract, inspectionModelActual.contract);
            Assert.AreEqual(inspectionModelExpected.validFrom, inspectionModelActual.validFrom);
            Assert.AreEqual(inspectionModelExpected.validTo, inspectionModelActual.validTo);
            Assert.IsTrue(inspectionModelActual.source.Contains(source));
            Assert.AreEqual(service, inspectionModelActual.service);
            return this;
        }
        [AllureStep]
        public AllInspectionListingPage VerifyTheFirstInspection(PointHistoryModel pointHistoryModel, InspectionModel inspectionModelActual, string location, string contract, string source, string service, string createdByUser, string assignedUser, string allocatedUnit)
        {
            Assert.AreEqual(pointHistoryModel.ID, inspectionModelActual.ID);
            Assert.AreEqual(location, inspectionModelActual.point);
            Assert.True(inspectionModelActual.inspectionType.Contains(pointHistoryModel.type));
            Assert.AreEqual(createdByUser, inspectionModelActual.createdByUser);
            Assert.True(assignedUser.Contains(inspectionModelActual.assignedUser));
            Assert.AreEqual(allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(pointHistoryModel.state, inspectionModelActual.status);
            Assert.AreEqual(contract, inspectionModelActual.contract);
            Assert.True(inspectionModelActual.validFrom.Contains(pointHistoryModel.date));
            Assert.True(inspectionModelActual.validTo.Contains(pointHistoryModel.dueDate));
            Assert.True(inspectionModelActual.source.Contains(source));
            Assert.AreEqual(service, inspectionModelActual.service);
            return this;
        }
        [AllureStep]
        public AllInspectionListingPage VerifyTheFirstInspection(PointHistoryModel pointHistoryModel, InspectionModel inspectionModelActual, string location, string contract, string source, string service, string createdByUser, string assignedUser, string allocatedUnit, string status)
        {
            Assert.AreEqual(pointHistoryModel.ID, inspectionModelActual.ID);
            Assert.AreEqual(location, inspectionModelActual.point);
            Assert.True(inspectionModelActual.inspectionType.Contains(pointHistoryModel.type));
            Assert.AreEqual(createdByUser, inspectionModelActual.createdByUser);
            Assert.True(assignedUser.Contains(inspectionModelActual.assignedUser));
            Assert.AreEqual(allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(status, inspectionModelActual.status);
            Assert.AreEqual(contract, inspectionModelActual.contract);
            Assert.True(inspectionModelActual.validFrom.Contains(pointHistoryModel.date));
            Assert.True(inspectionModelActual.validTo.Contains(pointHistoryModel.dueDate));
            Assert.True(inspectionModelActual.source.Contains(source));
            Assert.AreEqual(service, inspectionModelActual.service);
            return this;
        }
        [AllureStep]
        public AllInspectionListingPage VerifyTheFirstInspection(InspectionModel inspectionModelActual, string location, string contract, string source, string service, string createdByUser, string assignedUser, string allocatedUnit, string id, string type, string state, string date, string dueDate)
        {
            Assert.AreEqual(id, inspectionModelActual.ID);
            Assert.AreEqual(location, inspectionModelActual.point);
            Assert.AreEqual(type, inspectionModelActual.inspectionType);
            Assert.AreEqual(createdByUser, inspectionModelActual.createdByUser);
            Assert.IsTrue(assignedUser.Contains(inspectionModelActual.assignedUser));
            Assert.AreEqual(allocatedUnit, inspectionModelActual.allocatedUnit);
            Assert.AreEqual(state, inspectionModelActual.status);
            Assert.AreEqual(contract, inspectionModelActual.contract);
            Assert.AreEqual(date + " 00:00", inspectionModelActual.validFrom);
            Assert.AreEqual(dueDate + " 00:00", inspectionModelActual.validTo);
            Assert.AreEqual(source, inspectionModelActual.source);
            Assert.AreEqual(service, inspectionModelActual.service);
            return this;
        }
        [AllureStep]
        public DetailInspectionPage DoubleClickFirstInspectionRow()
        {
            WaitUtil.WaitForElementVisible(firstRow);
            DoubleClickOnElement(firstInspectionRow);
            return PageFactoryManager.Get<DetailInspectionPage>();
        }
        [AllureStep]
        public AllInspectionListingPage FilterInspectionByStatus(string statusValue)
        {
            SendKeys(statusInput, statusValue);
            return this;
        }
        [AllureStep]
        public AllInspectionListingPage FilterInspectionById(string id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id);
            WaitForLoadingIconToDisappear();
            return this;
        }
        [AllureStep]
        public AllInspectionListingPage ClickClearInInspectionListingBtn()
        {
            ClickOnElement(clearBtn);
            return this;
        }
    }
}
