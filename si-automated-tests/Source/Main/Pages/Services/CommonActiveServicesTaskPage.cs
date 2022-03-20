using System;
using System.Collections.Generic;
using System.Text;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Services
{
    public class CommonActiveServicesTaskPage : BasePage
    {
        private string tribeYarnsWithDate = "//div[text()='Tribe Yarns']/following-sibling::div/div[text()='{0}']";
        private string sidraTeddingtontribeYarnsStartDate = "//div[text()='Sidra - Teddington']/following-sibling::div[contains(@class, 'r5') and contains(.,'{0}')]";
        private string sidraTeddingtontribeYarnsEndDate = "//div[text()='Sidra - Teddington']/following-sibling::div[contains(@class, 'r6') and contains(.,'{0}')]";

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
    }
}
