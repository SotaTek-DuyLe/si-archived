using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Applications;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.EventTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class RIEventTests : BaseTest
    {
        //CONFIRM: DB with UI
        [Category("RI Events")]
        [Category("Chang")]
        [Test(Description = "Lookup wb_siteproduct/RI events")]
        public void TC_258_Look_up_wb_site_product_RI_events()
        {
            CommonFinder commonFinder = new CommonFinder(DbContext);

            string roundEventType = "Tipping";
            string resource = "COM1 NST";
            string location = "Townmead Weighbridge, 1 Townmead Road, Richmond, TW94EL";
            string firstWasteTypeValue = "Plastic";
            string secondWasteTypeValue = "Paper & Cardboard";
            string relLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source/Main/Resources/echo.jpeg");
            string newPath = new Uri(relLogo).LocalPath;
            string ticketNumber = CommonUtil.GetRandomNumber(5);
            string grossWeight = "76887";
            string netWeight = "212";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser66.UserName, AutoUser66.Password)
                .IsOnHomePage(AutoUser66);
            //Open any round-instance
            PageFactoryManager.Get<NavigationBase>()
                .GoToURL(WebUrl.MainPageUrl + "web/round-instances/6776");

            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .IsRoundInstancePage()
                //Step line 29: Click on [Add new item] in [Events] tab
                .ClickOnEventTab()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceDetailPage>()
                .ClickOnAddNewItemEventTab()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();

            PageFactoryManager.Get<RoundInstanceEventNewForm>()
                .IsRoundInstanceEventNewForm()
                .SelectAnyRoundEventType(roundEventType)
                .SelectAnyResource(resource)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .IsRoundInstanceEventDetailPage()
                .WaitForRoundEventTypeDisplayed(roundEventType)
                //Step line 30: Click on [Location] dd and select a site
                .ClickOnDetailsTab()
                .SelectAnyLocation(location)
                .WaitForLoadingIconToDisappear();
            //API query
            List<WBSiteProduct> allSites = commonFinder.GetWBSiteProductsBySiteId("105");
            List<int> allProductId = allSites.Select(o => o.productID).ToList();
            List<ProductDBModel> productDBModels = commonFinder.GetProductByListId(allProductId);
            List<string> allProductName = productDBModels.Select(o => o.product).ToList();

            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .VerifyLocationIsPopulated(location)
                .ClickOnDataTab()
                //Step line 31: Fill all fields
                .InputTicketNumber(ticketNumber)
                .InputGrossWeight(grossWeight)
                //Step line 32: Verify [Product] with DB
                //.ClickAndVerifyAllValueInWasteTypeDd(allProductName)
                //Step line 33: Select [product] and save the form
                .SelectAnyWasteType(firstWasteTypeValue)
                .InputTicketPhoto(newPath)
                .InputNetWeight(netWeight)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .VerifyAllValueArePopulatedInForm(ticketNumber, grossWeight, firstWasteTypeValue, netWeight)
                //Step line 34: Clear [Location] and verify [Waste type]
                .ClickOnDetailsTab()
                .SelectFirstLocation()
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .ClickOnDataTab()
                .VerifyValueInWasteTypeIsBlank()
                //Step line 35: Click on [Detail] tab and change [Location
                .ClickOnDetailsTab()
                .SelectAnyLocation(location)
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .ClickOnDataTab()
                .SelectAnyWasteType(secondWasteTypeValue)
                .ClickSaveBtn()
                .VerifyDisplayToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            string[] historyValues = { "Site", "105", "3" };
            PageFactoryManager.Get<RoundInstanceEventDetailPage>()
                .VerifyAllValueArePopulatedInForm(ticketNumber, grossWeight, secondWasteTypeValue, netWeight)
                //Step line 36: Verify [History] tab
                .ClickOnHistoryTab()
                .ClickOnFirstValueInDetailColumn()
                .IsHistoryPopup()
                .VerifyHistoryPopup(CommonConstants.HistoryRIEvent, historyValues);
        }
    }
}
