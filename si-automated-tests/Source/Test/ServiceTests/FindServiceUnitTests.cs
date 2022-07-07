using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class FindServiceUnitTests : BaseTest
    {
        [Category("Find Service Unit")]
        [Test(Description = "Find Service unit from Point Address")]
        public void TC_127_Find_service_unit_from_point_address()
        {
            string searchForAddresses = "Addresses";
            string pointAddressId = "483986";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser56.UserName, AutoUser56.Password)
                .IsOnHomePage(AutoUser56);
            PageFactoryManager.Get<HomePage>()
                .ClickOnSearchBtn()
                .IsSearchModel()
                .ClickAnySearchForOption(searchForAddresses)
                .ClickAndSelectRichmondCommercialSectorValue()
                .ClickOnSearchBtnInPopup()
                .WaitForLoadingIconToDisappear()
                .SwitchNewIFrame();
            PageFactoryManager.Get<PointAddressListingPage>()
                .WaitForPointAddressPageDisplayed()
                .FilterPointAddressWithId(pointAddressId)
                .DoubleClickFirstPointAddressRow()
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Step 1: Open [Find Service Unit] form => Detail Point Address > Action > Find Service Unit
            PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .ClickOnAllServicesTab()
                .WaitForLoadingIconToDisappear();
            PointAddressDetailPage pointAddressDetailPage = PageFactoryManager.Get<PointAddressDetailPage>();
            pointAddressDetailPage
                .ClickOnAnyActionBtn(2)
                .ClickOnAnyFindServiceUnitBtn(2)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            //Step 2: Verify top bar actions
            FindServiceUnitDetailPage findServiceUnitDetailPage = PageFactoryManager.Get<FindServiceUnitDetailPage>();
            findServiceUnitDetailPage
                .IsFindServiceUnitPage();


        }
    }
}
