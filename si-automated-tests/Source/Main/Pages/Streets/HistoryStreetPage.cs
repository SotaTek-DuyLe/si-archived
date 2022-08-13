using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Pages.Streets
{
    public class HistoryStreetPage : BasePage
    {
        //DYNAMIC
        private const string anyColumnHistory = "//th[text()='{0}']";

        public HistoryStreetPage IsHistoryStreetPage()
        {
            WaitUtil.WaitForElementVisible(anyColumnHistory, CommonConstants.HistoryStreetColumn[0]);
            foreach(string column in CommonConstants.HistoryStreetColumn)
            {
                Assert.IsTrue(IsControlDisplayed(anyColumnHistory, column), column + " is not displayed");
            }
            return this;
        }

        public HistoryStreetPage VerifyCurrentUrlHistoryStreet(string streetId)
        {
            string currentUrl = GetCurrentUrl();
            Assert.AreEqual(WebUrl.MainPageUrl + "web/history/Street/" + streetId, currentUrl);
            return this;
        }
    }
}
