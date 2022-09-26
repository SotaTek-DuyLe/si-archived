using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.CommonTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class LoginTests : BaseTest
    {
        [Category("User")]
        [Category("Chang")]
        [Test]
        public void TC_001_login()
        {
            LoginPage login = new LoginPage();
            HomePage homePage = new HomePage();
            login.GoToURL(WebUrl.MainPageUrl);
            //Non-success case
            login
                .IsOnLoginPage()
                .SendKeyToUsername("acv")
                .SendKeyToPassword("cdcd")
                .ClickOnSignIn()
                .VerifyErrorMessageDisplay();
            //Success case
            login
                .SendKeyToUsername(AutoUser1.UserName)
                .SendKeyToPassword(AutoUser1.Password)
                .ClickOnSignIn();
            homePage
                .IsOnHomePage(AutoUser1)
                .ClickUserNameDd()
                .ClickLogoutBtn();
            //Logout
            login
                .IsOnLoginPage();
        }
    }
}
