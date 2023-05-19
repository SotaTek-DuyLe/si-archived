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
                .ClickNextButton()
                .SendKeyToPassword("cdcd")
                .ClickOnSignIn()
                .VerifyErrorMessageDisplay();
            login.GoToURL(WebUrl.MainPageUrl);
            //Success case
            login
                .Login(AutoUser1.UserName, AutoUser1.Password);
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
