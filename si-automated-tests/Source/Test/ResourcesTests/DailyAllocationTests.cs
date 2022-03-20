using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Resources.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    public class DailyAllocationTests : BaseTest
    {
        [Category("Resources")]
        [Test]
        public void TC_41_42_Create_Resource_And_Daily_Allocation()
        {
            string resourceName = "Neil Armstrong " + CommonUtil.GetRandomNumber(5);
            string dateInFutre = CommonUtil.GetLocalTimeMinusDay("dd/MM/yyyy",1);
            string resourceType = "Driver";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser4.UserName, AutoUser4.Password)
                .IsOnHomePage(AutoUser4);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption("Resources")
                .OpenOption("Daily Allocation")
                .SwitchNewIFrame();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SelectContract("North Star")
                .SelectBusinessUnit("North Star")
                .SelectShift("AM")
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .ClickCreateResource()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ResourceDetailTab>()
                .IsOnDetailTab()
                .InputResourceName(resourceName)
                .SelectResourceType(resourceType)
                .TickContractRoam()
                .ClickSaveBtn()
                .VerifyToastMessage("Successfully saved resource.")
                .ClickCloseBtn()
                .SwitchToLastWindow()
                .SwitchNewIFrame()
                .SwitchToTab("All Resources");
            //ALLOCATE FOR CURRENT DATE
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .VerifyPresenceOption("IN/OUT")
                .ClickPresenceOption();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "green")
                .ClickAllocatedResource(resourceName)
                .VerifyPresenceOption("IN/OUT")
                .ClickPresenceOption();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "white")
                .InsertDate(dateInFutre + Keys.Enter)
                .ClickGo()
                .WaitForLoadingIconToDisappear();
            Thread.Sleep(1000);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .SwitchToTab("All Resources");
            //ALLOCATE FOR FUTURE DATE
            PageFactoryManager.Get<ResourceAllocationPage>()
                .FilterResource("Resource", resourceName)
                .VerifyFirstResultValue("Resource", resourceName)
                .DragAndDropFirstResourceToFirstRound()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyAllocatedResourceName(resourceName)
                .ClickAllocatedResource(resourceName)
                .VerifyPresenceOption("PRE-CONFIRM/UN-CONFIRM")
                .ClickPresenceOption();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "purple")
                .ClickAllocatedResource(resourceName)
                .VerifyPresenceOption("PRE-CONFIRM/UN-CONFIRM")
                .ClickPresenceOption();
            Thread.Sleep(500);
            PageFactoryManager.Get<ResourceAllocationPage>()
                .VerifyBackgroundColor(resourceName, "white");
        }
    }
}
