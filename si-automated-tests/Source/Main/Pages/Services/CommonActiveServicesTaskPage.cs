using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class CommonActiveServicesTaskPage : BasePage
    {
        private readonly By partyNameInput = By.XPath("//div[@class='slick-headerrow-columns']/div[count(//span[text()='Party']/parent::div/preceding-sibling::div) + 1]//input");
        private readonly By applyBtn = By.XPath("//button[@type='button' and @title='Apply Filters']");

        private readonly By allRows = By.XPath("//div[@class='grid-canvas']/div");
        private readonly By partyNameColumns = By.XPath("//div[@class='grid-canvas']/div/div[count(//span[text()='Party']/parent::div/preceding-sibling::div) + 1]");
        private readonly By startDateColumns = By.XPath("//div[@class='grid-canvas']/div/div[count(//span[text()='Start Date']/parent::div/preceding-sibling::div) + 1]");
        private readonly By endDateColumns = By.XPath("//div[@class='grid-canvas']/div/div[count(//span[text()='End Date']/parent::div/preceding-sibling::div) + 1]");

        //DYNAMIC LOCATOR
        private string tribeYarnsWithDate = "//div[text()='Tribe Yarns']/following-sibling::div/div[text()='{0}']";
        private string sidraTeddingtontribeYarnsStartDate = "//div[text()='Sidra - Teddington']/following-sibling::div[contains(@class, 'r5') and contains(.,'{0}')]";
        private string sidraTeddingtontribeYarnsEndDate = "//div[text()='Sidra - Teddington']/following-sibling::div[contains(@class, 'r6') and contains(.,'{0}')]";
        private static string tommorowDate = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy", 1);
        private string startDateTommorowPartyName = "//div[text()='" + tommorowDate + "']/parent::div/preceding-sibling::div[text()='{0}']";
        
        public CommonActiveServicesTaskPage InputPartyNameToFilter(string name)
        {
            SendKeys(partyNameInput, name);
            return this;
        }
        //date type are STARTDATE or ENDDATE
        public ServicesTaskPage OpenTaskWithPartyNameAndDate(string name, string date, string dateType)
        {
            int n = 3;
            int j = 0;
            while(n > 0)
            {
                List<IWebElement> allRowsList = GetAllElements(allRows);
                List<IWebElement> partyNameList = GetAllElements(partyNameColumns);
                List<IWebElement> dateList = new List<IWebElement>();
                if (dateType.Equals("STARTDATE"))
                {
                    dateList = GetAllElements(startDateColumns);
                }
                else
                {
                    dateList = GetAllElements(endDateColumns);
                }
                
                for (int i = 0; i < allRowsList.Count; i++)
                {
                    if (GetElementText(partyNameList[i]) == name && GetElementText(dateList[i]) == date)
                    {
                        DoubleClickOnElement(allRowsList[i]);
                        j = 1;
                        return new ServicesTaskPage();
                    }
                }
                if(j == 0)
                {
                    ClickRefreshBtn();
                    WaitForLoadingIconToDisappear();
                    SleepTimeInMiliseconds(1000);
                    n--;
                    allRowsList.Clear();
                    dateList.Clear();
                    partyNameList.Clear();
                }
            }
            return new ServicesTaskPage();
        }

        public List<IWebElement> VerifyTaskWithPartyNameAndDate(int num, string name, string date, string dateType)
        {
            int n = 3;
            int j = 0;
            List<IWebElement> result = new List<IWebElement>();
            while (n > 0)
            {
                List<IWebElement> allRowsList = GetAllElements(allRows);
                List<IWebElement> partyNameList = GetAllElements(partyNameColumns);
                List<IWebElement> dateList = new List<IWebElement>();
                if (dateType.Equals("STARTDATE"))
                {
                    dateList = GetAllElements(startDateColumns);
                }
                else
                {
                    dateList = GetAllElements(endDateColumns);
                }

                for (int i = 0; i < allRowsList.Count; i++)
                {
                    if (GetElementText(partyNameList[i]) == name && GetElementText(dateList[i]) == date)
                    {
                        result.Add(allRowsList[i]);
                        j = 1;
                    }
                }
                if (j == 0)
                {
                    ClickRefreshBtn();
                    WaitForLoadingIconToDisappear();
                    SleepTimeInMiliseconds(5000);
                    n--;
                    allRowsList.Clear();
                    dateList.Clear();
                    partyNameList.Clear();
                }
                else {break;}
            }
            Assert.AreEqual(num, result.Count);
            return result;
        }

        public ServicesTaskPage OpenATask(IWebElement result)
        {
            DoubleClickOnElement(result);
            return new ServicesTaskPage();
        }

        public ServicesTaskPage OpenTommorowTaskWithPartyName(string partyName)
        {
            int i = 3;
            while (i > 0)
            {
                if (IsControlUnDisplayed(startDateTommorowPartyName, partyName))
                {
                    ClickRefreshBtn();
                    WaitForLoadingIconToDisappear();
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else
                {
                    break;
                }
            }
            DoubleClickOnElement(startDateTommorowPartyName, startDateTommorowPartyName);
            return new ServicesTaskPage();
        }
        public CommonActiveServicesTaskPage OpenTribleYarnsWithDate(string date)
        {
            int i = 3;
            while (i > 0)
            {
                if (IsControlUnDisplayed(tribeYarnsWithDate, date))
                {
                    ClickRefreshBtn();
                    WaitForLoadingIconToDisappear();
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else
                {
                    break;
                }
            }
            DoubleClickOnElement(tribeYarnsWithDate, date);
            return this;
        }
        public CommonActiveServicesTaskPage OpenSidraTeddingtonStartDate(string date)
        {
            int i = 3;
            while(i > 0)
            {
                if (IsControlUnDisplayed(sidraTeddingtontribeYarnsStartDate, date))
                {
                    ClickRefreshBtn();
                    WaitForLoadingIconToDisappear();
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else
                {
                    break;
                }
            }
            
            DoubleClickOnElement(sidraTeddingtontribeYarnsStartDate, date);
            return this;
        }
        public CommonActiveServicesTaskPage OpenSidraTeddingtonEndDate(string date)
        {
            int i = 3;
            while (i > 0)
            {
                if (IsControlUnDisplayed(sidraTeddingtontribeYarnsEndDate, date))
                {
                    ClickRefreshBtn();
                    WaitForLoadingIconToDisappear();
                    SleepTimeInMiliseconds(1000);
                    i--;
                }
                else
                {
                    break;
                }
            }
            DoubleClickOnElement(sidraTeddingtontribeYarnsEndDate, date);
            return this;
        }
        public CommonActiveServicesTaskPage ClickApplyBtn()
        {
            ClickOnElement(applyBtn);
            return this;
        }
    }
}
