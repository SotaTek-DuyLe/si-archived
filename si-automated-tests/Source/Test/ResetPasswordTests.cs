using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.SystemTools.SystemMonitoring;
using si_automated_tests.Source.Main.Pages.UserAndRole;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test
{
    class ResetPasswordTests
    {
        [SetUp]
        public void Setup()
        {
            IWebDriverManager.SetDriver("ie");
            new UserRegistry();
        }
        [TearDown]
        public void TearDown()
        {
            IWebDriverManager.GetDriver().Close();
            IWebDriverManager.GetDriver().Quit();
        }
        [Test]
        public void TC_003([Random(1, 999999, 1)] int random)
        {
            string newPassword = "newPassword" + random + "@#_";

            PageFactoryManager.Get<LoginPage>()
                .GoToURL(Url.MainPageUrlIE);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .SendKeyToUsername(AutoUser4.UserName)
                .SendKeyToPassword(AutoUser4.Password + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage(AutoUser4.UserName)
                .ClickSystemTool()
                .ClickUserAndRole()
                .ClickUser()
                .ExpandGroup()
                .ClickUserName("Tomek")
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
                .SendKeyToUsername("Tomek")
                .SendKeyToPassword(newPassword + Keys.Enter);
            PageFactoryManager.Get<HomePage>()
                .IsOnHomePage("Tomek");
        }
    }
}
