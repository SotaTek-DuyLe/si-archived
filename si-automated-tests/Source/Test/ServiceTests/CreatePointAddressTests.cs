using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PointAddress;
using si_automated_tests.Source.Main.Pages.Search.PointAddress;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class CreatePointAddressTests : BaseTest
    {


        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO POINT ADDRESS
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser26.UserName, AutoUser26.Password)
                .IsOnHomePage(AutoUser26);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Richmond Commercial")
                .OpenOption("Point Addresses")
                .SwitchNewIFrame();
        }
        [Category("PointAddress")]
        [Test]
        public void TC_101_Create_point_address()
        {
            CommonFinder finder = new CommonFinder(DatabaseContext);
            string postCode = "TW9 1DN";
            string postCodeOption = "The Quadrant, Richmond TW9 1DN";
            string location = "35 THE QUADRANT, RICHMOND, TW9 1DN";
            string propertyName = "Greggs Richmond";
            string property = "5";
            string toProperty = "7";
            string pointSegment = "The Quadrant 5 To 50 Between The Square And Quadrant Road";
            string pointAddType = "Commercial";
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreatePointAddressPage>()
                .IsOnFirstScreen()
                .SearchPostCode(postCode)
                .SelectResultInScreen1(postCodeOption)
                .IsOnSecondScreen()
                .SelectResultInScreen2(location)
                .IsOnThirdScreen()
                .InputValuesInScreen3(propertyName, property, toProperty, pointSegment, pointAddType)
                .VerifyToastMessage("Address created.");
            //VERIFY ON UI
            String pointAddressId = PageFactoryManager.Get<PointAddressDetailPage>()
                .WaitForPointAddressDetailDisplayed()
                .VerifyDetailsInDetailsTab(propertyName, property, toProperty, postCodeOption, pointAddType)
                .GetPointAddressId();
            //VERIFY ON DB
            PointAddressModel pointAddress = finder.GetPointAddress(pointAddressId)[0];
            Assert.AreEqual(propertyName, pointAddress.Propertyname);
            Assert.AreEqual(property, pointAddress.Property.ToString());
            Assert.AreEqual(toProperty, pointAddress.Toproperty.ToString());
            Assert.AreEqual(PageFactoryManager.Get<PointAddressDetailPage>().GetPointAddressName(), pointAddress.Sourcedescription);
        }
    }

}
