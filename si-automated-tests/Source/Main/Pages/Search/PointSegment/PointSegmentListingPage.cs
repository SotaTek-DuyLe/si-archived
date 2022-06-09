using System.Collections.Generic;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Search.PointSegment
{
    public class PointSegmentListingPage : BasePage
    {
        private readonly By idColumn = By.XPath("//span[text()='ID']");

        //DYNAMIC LOCATOR
        private const string columnInRowPointAddress = "//div[@class='grid-canvas']/div/div[count(//span[text()='{0}']/parent::div/preceding-sibling::div) + 1]";
        private readonly By firstPointSegementRow = By.XPath("//div[@class='grid-canvas']/div[not(contains(@style, 'display: none;'))][1]");
        private readonly By filterInputById = By.XPath("//div[@class='ui-state-default slick-headerrow-column l1 r1']/descendant::input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");

        public PointSegmentListingPage WaitForPointSegmentsPageDisplayed()
        {
            WaitUtil.WaitForElementVisible(idColumn);
            return this;
        }

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

        public PointSegmentDetailPage DoubleClickFirstPointSegmentRow()
        {
            DoubleClickOnElement(firstPointSegementRow);
            return PageFactoryManager.Get<PointSegmentDetailPage>();
        }

        public PointSegmentListingPage FilterSegmentById(string id)
        {
            WaitForLoadingIconToDisappear();
            SendKeys(filterInputById, id);
            ClickOnElement(applyBtn);
            return this;
        }

    }
}
