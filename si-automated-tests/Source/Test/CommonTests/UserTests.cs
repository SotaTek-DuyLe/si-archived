using System;
using System.Threading;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.SystemTools.SystemMonitoring;
using si_automated_tests.Source.Main.Pages.UserAndRole;
using static si_automated_tests.Source.Main.Models.UserRegistry;


namespace si_automated_tests.Source.Test
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    [AllureNUnit]
    public class UserTests
    {
        [SetUp]
        public void Setup()
        {
            new WebUrl();
            CustomTestListener.OnTestStarted();
            IWebDriverManager.SetDriver("chrome");
            new UserRegistry();
        }
        [TearDown]
        public void TearDown()
        {
            CustomTestListener.OnTestFinished();
            IWebDriverManager.GetDriver().Quit();
        }
        [Category("User")]
        [Category("Dee")]
        //[Test]
        public void TC_002_Create_User_Test([Random(1, 999999, 1)] int random)
        {
            string newUserUrl = "https://test.echoweb.co.uk/echo2/echo2extra/PopupDefault.aspx?ObjectID=0&CTypeName=User&CReferenceName=none&CObjectID=0&TypeName=User&RefTypeName=none&ReferenceName=none&CreateNew=true&InEdit=true&CPath=";
            User user = new User();
            user.UserName = CommonUtil.GetRandomString(5) + " userName " + random;
            user.DisplayName = "displayname" + random;
            user.Email = "userEmail" + random + "@gmail.com";
            user.Password = "newPassword" + random + "@#_";
            string oldPassword = "oldPassword" + random + "@#_";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrlIE);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser3.UserName)
                .SendKeyToPassword(AutoUser3.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                //.IsOnHomePage(AutoUser3)
                .GoToURL(newUserUrl);
            //    .ClickSystemTool()
            //    .ClickUserAndRole()
            //    .ClickUser()
            //    .ClickGroup()
            //    .IsOnUserScreen()
            //    .ClickAction()
            //    .ClickNew();
            //PageFactoryManager.Get<BasePage>()
            //    .SwitchToLastWindow();

            PageFactoryManager.Get<UserDetailPage>()
                .IsOnUserDetailPage()
                .ClickSave();
            PageFactoryManager.Get<BasePage>()
                .VerifyAlertText("You must enter a value for User Name")
                .AcceptAlert()
                .VerifyAlertText("You must enter a value for Display Name")
                .AcceptAlert()
                .VerifyAlertText("You must enter a value for Email Address")
                .AcceptAlert()
                .SwitchToLastWindow();
            PageFactoryManager.Get<UserDetailPage>()
                .IsOnUserDetailPage()
                .EnterUserDetails(user.UserName, user.DisplayName, user.Email)
                .ClickSave();
            PageFactoryManager.Get<UserDetailPage>()
                .ClickDataAccessRoles()
                .SwitchToLastWindow();
            PageFactoryManager.Get<UserDetailPage>()
                .ChooseNorthStarCommercialAsAccessRole("")
                .ClickAdminRoles()
                .ChooseAdminRole("System Administrator")
                .ClickSaveAndClose()
                .IsOnUserScreen();

            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser3)
                .ClickSysMonitoring()
                .ClickEmail()
                .IsOnEmailPage()
                .ClickMoveLast()
                .ClickLastRow();

            string emailResetLink = PageFactoryManager.Get<EmailDetailPage>()
                .IsOnEmailDetailPage()
                .ClickBodyView()
                .GetPasswordResetLink();

            IWebDriverManager.GetDriver().Quit();
            IWebDriverManager.SetDriver("chrome");

            PageFactoryManager.Get<BasePage>()
                .GoToURL(emailResetLink);
            PageFactoryManager.Get<ResetPasswordPage>()
                .IsOnResetPasswordPage()
                .ResetPassword(user.Password)
                .VerifyResetSuccessfully()
                .ClickGoToLogin()
                .IsOnLoginPage()
                .SendKeyToUsername(user.UserName)
                .SendKeyToPassword(user.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(user);
        }

        [Category("User")]
        [Category("Dee")]
        //[Test]
        public void TC_003_Reset_Password_Test([Random(1, 999999, 1)] int random)
        {
            string newPassword = "newPassword" + random + "@#_";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrlIE);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser3.UserName)
                .SendKeyToPassword(AutoUser3.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser3)
                .ClickSystemTool()
                .ClickUserAndRole()
                .ClickUser()
                .ExpandGroup()
                .ClickUserName(TestAutoUser.UserName)
                .SwitchToRightFrame()
                .IsOnUserDetailPage()
                .ClickResetPassword()
                .VerifyAlertText("The password reset email has been sent to the user")
                .AcceptAlert();


            PageFactoryManager.Get<UserDetailPage>()
                .IsOnUserDetailPage()
                .SwitchToDefaultContent();
            PageFactoryManager.Get<HomePage>()
                .ClickSysMonitoring()
                .ClickEmail()
                .IsOnEmailPage()
                .ClickMoveLast()
                .ClickLastRow();

            string emailResetLink = PageFactoryManager.Get<EmailDetailPage>()
                .IsOnEmailDetailPage()
                .ClickBodyView()
                .GetPasswordResetLink();

            IWebDriverManager.GetDriver().Quit();
            IWebDriverManager.SetDriver("chrome");

            PageFactoryManager.Get<BasePage>()
                .GoToURL(emailResetLink);
            PageFactoryManager.Get<ResetPasswordPage>()
                .IsOnResetPasswordPage()
                .ResetPassword(newPassword)
                .VerifyResetSuccessfully()
                .ClickGoToLogin()
                .IsOnLoginPage()
                .SendKeyToUsername("testauto")
                .SendKeyToPassword(newPassword + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(TestAutoUser);
        }
    }
}
