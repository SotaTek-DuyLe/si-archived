﻿using System.Collections.Generic;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Search.PointSegment
{
    public class PointSegmentListingPage : BasePage
    {
        private readonly By idColumn = By.XPath("//span[text()='ID']");
        private readonly By allPointSegmentRows = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By containerPage = By.XPath("//div[@class='slick-viewport']");

        //DYNAMIC LOCATOR
        private const string columnInRowPointAddress = "//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private readonly By firstPointSegementRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");

        [AllureStep]
        public PointSegmentListingPage WaitForPointSegmentsPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(idColumn);
            WaitUtil.WaitForAllElementsVisible(allPointSegmentRows);
            return this;
        }

        [AllureStep]
        public List<PointSegmentModel> getAllPointSegmentInList(int numberOfRow)
        {
            List<PointSegmentModel> allModel = new List<PointSegmentModel>();
            for (int i = 0; i < numberOfRow; i++)
            {
                string id = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointSegmentColumn[0])[i]);
                string name = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointSegmentColumn[1])[i]);
                string pointSegmentType = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointSegmentColumn[2])[i]);
                string pointSegmentClass = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointSegmentColumn[3])[i]);
                string startDate = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointSegmentColumn[4])[i]);
                string endDate = GetElementText(GetAllElements(columnInRowPointAddress, CommonConstants.PointSegmentColumn[4])[i]);

                allModel.Add(new PointSegmentModel(id, name, pointSegmentType, pointSegmentClass, startDate, endDate));
            }
            return allModel;
        }

        [AllureStep]
        public PointSegmentDetailPage DoubleClickFirstPointSegmentRow()
        {
            DoubleClickOnElement(firstPointSegementRow);
            return PageFactoryManager.Get<PointSegmentDetailPage>();
        }

        [AllureStep]
        public PointSegmentListingPage FilterSegmentById(string id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id);
            ClickOnElement(applyBtn);
            return this;
        }

        [AllureStep]
        public PointSegmentListingPage VerifyDisplayVerticalScrollBarPointSegmentPage()
        {
            List<IWebElement> webElements = GetAllElements(allPointSegmentRows);
            if (webElements.Count >= 25)
            {
                VerifyDisplayVerticalScrollBar(containerPage);
            }
            return this;
        }

    }
}
