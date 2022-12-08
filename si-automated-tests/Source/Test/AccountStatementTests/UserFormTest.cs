using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask;
using si_automated_tests.Source.Main.Pages.Common;
using si_automated_tests.Source.Main.Pages.Events;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Services;
using si_automated_tests.Source.Main.Pages.Task;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using ServiceUnitDetailPage = si_automated_tests.Source.Main.Pages.Services.ServiceUnitDetailPage;

namespace si_automated_tests.Source.Test.AccountStatementTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class UserFormTest : BaseTest
    {
        [Category("TaskLine")]
        [Category("Huong")]
        [Test(Description = "The error 500 occurs when user without home contract loads the forms")]
        public void TC_191_The_error_500_occurs_when_user_without_home_contract_loads_the_forms()
        {
            void TestLoadFormCorrectly()
            {
                //Events -> Click on the contract -> Double click on any event
                PageFactoryManager.Get<NavigationBase>()
                    .ClickMainOption(MainOption.Events)
                    .OpenOption(Contract.Municipal)
                    .SwitchNewIFrame()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<EventsListingPage>()
                    .ClickOnFirstRecord()
                    .SwitchToChildWindow(2)
                    .WaitForLoadingIconToDisappear();
                EventDetailPage eventDetailPage = PageFactoryManager.Get<EventDetailPage>();
                eventDetailPage
                    .WaitForEventDetailDisplayed()
                    .VerifyNotDisplayErrorMessage()
                    .WaitForLoadingIconToDisappear();
                eventDetailPage.ClickCloseBtn()
                    .SwitchToFirstWindow()
                    .SwitchNewIFrame();
                //Tasks -> Click on contract -> Double click on any task
                PageFactoryManager.Get<NavigationBase>()
                   .ClickMainOption(MainOption.Tasks)
                   .OpenOption(Contract.Municipal)
                   .SwitchNewIFrame()
                   .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<CommonTaskPage>()
                    .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<CommonBrowsePage>()
                   .OpenFirstResult()
                   .WaitForLoadingIconToDisappear()
                   .SwitchToChildWindow(2)
                   .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<AgreementTaskDetailsPage>()
                     .VerifyNotDisplayErrorMessage()
                     .ClickCloseBtn()
                     .SwitchToFirstWindow()
                     .SwitchNewIFrame();
                //Services -> Regions -> UK -> Contract-> Sectors -> Point addresses -> Double click on any point addresses
                PageFactoryManager.Get<NavigationBase>()
                   .ClickMainOption(MainOption.Services)
                   .ExpandOption("Regions")
                   .ExpandOption(Region.UK)
                   .ExpandOption(Contract.Municipal)
                   .ExpandOptionLast("Richmond")
                   .OpenOption("Point Addresses")
                   .SwitchNewIFrame()
                   .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<CommonBrowsePage>()
                   .OpenFirstResult()
                   .WaitForLoadingIconToDisappear()
                   .SwitchToChildWindow(2)
                   .WaitForLoadingIconToDisappear();
                ServicePointAddressesDetailPage pointAddressesDetailPage = PageFactoryManager.Get<ServicePointAddressesDetailPage>();
                pointAddressesDetailPage
                   .VerifyNotDisplayErrorMessage()
                   .ClickOnElement(pointAddressesDetailPage.DetailTab);
                pointAddressesDetailPage
                   .WaitForLoadingIconToDisappear()
                   .ClickCloseBtn()
                   .SwitchToFirstWindow()
                   .SwitchNewIFrame();
                //Point segments
                PageFactoryManager.Get<NavigationBase>()
                   .ClickMainOption(MainOption.Services)
                   .ExpandOption("Regions")
                   .ExpandOption(Region.UK)
                   .ExpandOption(Contract.Municipal)
                   .ExpandOption(Contract.Municipal)
                   .OpenOption("Point Segments")
                   .SwitchNewIFrame()
                   .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<CommonBrowsePage>()
                   .OpenFirstResult()
                   .WaitForLoadingIconToDisappear()
                   .SwitchToChildWindow(2)
                   .WaitForLoadingIconToDisappear();
                ServicePointSegmentDetailPage pointSegmentDetailPage = PageFactoryManager.Get<ServicePointSegmentDetailPage>();
                pointSegmentDetailPage
                   .VerifyNotDisplayErrorMessage()
                   .ClickOnElement(pointSegmentDetailPage.DetailTab);
                pointSegmentDetailPage
                   .WaitForLoadingIconToDisappear()
                   .ClickCloseBtn()
                   .SwitchToFirstWindow()
                   .SwitchNewIFrame();
                //Point nodes
                PageFactoryManager.Get<NavigationBase>()
                   .ClickMainOption(MainOption.Services)
                   .ExpandOption("Regions")
                   .ExpandOption(Region.UK)
                   .ExpandOption(Contract.Municipal)
                   .ExpandOption(Contract.Municipal)
                   .OpenOption("Point Nodes")
                   .SwitchNewIFrame()
                   .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<CommonBrowsePage>()
                   .OpenFirstResult()
                   .WaitForLoadingIconToDisappear()
                   .SwitchToChildWindow(2)
                   .WaitForLoadingIconToDisappear();
                ServicePointNodeDetailPage pointNodeDetailPage = PageFactoryManager.Get<ServicePointNodeDetailPage>();
                pointNodeDetailPage
                   .VerifyNotDisplayErrorMessage()
                   .ClickOnElement(pointNodeDetailPage.DetailTab);
                pointNodeDetailPage
                   .WaitForLoadingIconToDisappear()
                   .ClickCloseBtn()
                   .SwitchToFirstWindow()
                   .SwitchNewIFrame();
                //Point areas
                PageFactoryManager.Get<NavigationBase>()
                   .ClickMainOption(MainOption.Services)
                   .ExpandOption("Regions")
                   .ExpandOption(Region.UK)
                   .ExpandOption(Contract.Municipal)
                   .ExpandOption(Contract.Municipal)
                   .OpenOption("Point Areas")
                   .SwitchNewIFrame()
                   .WaitForLoadingIconToDisappear();
                PageFactoryManager.Get<CommonBrowsePage>()
                   .OpenFirstResult()
                   .WaitForLoadingIconToDisappear()
                   .SwitchToChildWindow(2)
                   .WaitForLoadingIconToDisappear();
                ServicePointAreaDetailPage pointAreaDetailPage = PageFactoryManager.Get<ServicePointAreaDetailPage>();
                pointAreaDetailPage
                    .VerifyNotDisplayErrorMessage()
                    .ClickOnElement(pointAreaDetailPage.DetailTab);
                pointAreaDetailPage
                   .WaitForLoadingIconToDisappear()
                   .ClickCloseBtn()
                   .SwitchToFirstWindow()
                   .SwitchNewIFrame();
                //Expand any service group -> Services -> Active service units -> Double click on any row
                PageFactoryManager.Get<NavigationBase>()
                    .ClickMainOption(MainOption.Services)
                    .ExpandOption("Regions")
                    .ExpandOption(Region.UK)
                    .ExpandOption(Contract.Municipal)
                    .ExpandOption("Recycling")
                    .ExpandOption("Communal Recycling")
                    .OpenOption("Active Service Units");
                ServiceUnitPage serviceUnit = PageFactoryManager.Get<ServiceUnitPage>();
                serviceUnit.SwitchToFrame(serviceUnit.UnitIframe);
                serviceUnit.WaitForLoadingIconToDisappear();
                serviceUnit.DoubleClickServiceUnit()
                           .SwitchToChildWindow(2);
                ServiceUnitDetailPage serviceUnitDetail = PageFactoryManager.Get<ServiceUnitDetailPage>();
                serviceUnitDetail.WaitForLoadingIconToDisappear(false)
                    .VerifyNotDisplayErrorMessage();
                serviceUnitDetail.ClickOnDetailTab()
                    .WaitForLoadingIconToDisappear()
                    .VerifyNotDisplayErrorMessage()
                    .ClickCloseBtn()
                    .SwitchToFirstWindow()
                    .SwitchNewIFrame();
            }
            //Verify that the forms load correctly when user doesn't have home contract set 
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/users");
            //Login
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser3.UserName, AutoUser3.Password);
            UserListPage userListPage = PageFactoryManager.Get<UserListPage>();
            userListPage.WaitForLoadingIconToDisappear();
            userListPage.SendKeys(userListPage.UserNameHeaderInput, "auto15");
            userListPage.SendKeysWithoutClear(userListPage.UserNameHeaderInput, Keys.Enter);
            userListPage.WaitForLoadingIconToDisappear();
            userListPage.DoubleClickRow(0)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            UserDetailPage userDetailPage = PageFactoryManager.Get<UserDetailPage>();
            userDetailPage.VerifySelectedValue(userDetailPage.HomeContractSelect, "");
            //Click on Data access role
            userDetailPage.ClickOnElement(userDetailPage.DataAccessRoleTab);
            userDetailPage.WaitForLoadingIconToDisappear();
            userDetailPage.VerifyCheckboxIsSelected(userDetailPage.tfUserCheckbox, false);
            userDetailPage.VerifyCheckboxIsSelected(userDetailPage.RichmondCheckbox, true);
            //Log in to echo using this user
            userDetailPage.CloseCurrentWindow()
                .SwitchToFirstWindow();
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser3)
                .ClickUserNameDd()
                .ClickLogoutBtn();
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser15.UserName, AutoUser15.Password);
            TestLoadFormCorrectly();
            //Close the above forms and log out. Log back with your user with home contract set
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser15)
                .ClickUserNameDd()
                .ClickLogoutBtn();
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser3.UserName, AutoUser3.Password);
            TestLoadFormCorrectly();
        }
    }
}
