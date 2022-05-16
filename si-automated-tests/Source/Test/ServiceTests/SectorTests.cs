using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using System;
using System.Collections.Generic;
using System.Text;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class SectorTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO POINT ADDRESS
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser27.UserName, AutoUser27.Password)
                .IsOnHomePage(AutoUser27);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Services")
                .ExpandOption("Regions")
                .ExpandOption("London")
                .ExpandOption("North Star Commercial")
                .ExpandOption("Richmond Commercial")
                .OpenOption("Point Nodes")
                .SwitchNewIFrame();
        }
        [Category("PointNode")]
        [Test]
        public void TC_103_Create_point_node()
        {
            string _des = "The Quadrant Richmond";
            string _lat = "-51.462496441865326";
            string _long = "-0.30280400159488435";
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<PointNodeDetailPage>()
                .InputPointNodeDetails(_des, _lat, _long)
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved Point Node")
                .WaitUntilToastMessageInvisible("Successfully saved Point Node")
                .GoToAllTabAndConfirmNoError();
        }
    }
}
