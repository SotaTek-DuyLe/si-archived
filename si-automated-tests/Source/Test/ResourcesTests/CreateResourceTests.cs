using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    public class CreateResourceTests : BaseTest
    {
        [Test]
        public void TEST_30_Create_Human_Resource()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string startDate = CommonUtil.GetLocalTimeNow("dd/MM/yyyy");
            string defaultEndDate = "01/01/2050";
            string resourceType = "Driver";
            string service = "Clinical Waste";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser4.UserName, AutoUser4.Password)
                .IsOnHomePage(AutoUser4);
            PageFactoryManager.Get<NavigationBase>()
                .ClickResources()
                .OpenNS();
            PageFactoryManager.Get<HomePage>()
                .ClickTitle()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .SelectService(service)
                .TickSiteRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Name", resourceName)
                .VerifyFirstResultValue("Resource Type", resourceType)
                .VerifyFirstResultValue("Start Date", startDate)
                .VerifyFirstResultValue("End Date", defaultEndDate);
        }
        [Test]
        public void TEST_31_Allocate_Human_Resource_To_A_Round()
        {
            
        }
    }
}
