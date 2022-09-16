using NTextCat;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Models.Services;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Common;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartyAdHoc;
using si_automated_tests.Source.Main.Pages.Services;
using System;
using System.Linq;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    [AllureNUnit]
    public class ServiceUnitPointTest : BaseTest
    {
        [Category("ServiceUnitPoint")]
        [Test(Description = "Verify that correct info displays in add Service Unit Point pop up")]
        public void TC_166_Service_Unit_Point()
        {
            PageFactoryManager.Get<LoginPage>()
                   .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser40.UserName, AutoUser40.Password)
                .IsOnHomePage(AutoUser40);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.RMC)
                .ExpandOption("Collections")
                .ExpandOption("Commercial Collections")
                .OpenOption("Active Service Units");
            ServiceUnitPage serviceUnit = PageFactoryManager.Get<ServiceUnitPage>();
            serviceUnit.SwitchToFrame(serviceUnit.UnitIframe);
            serviceUnit.WaitForLoadingIconToDisappear();
            serviceUnit.DoubleClickServiceUnitById("230038")
                       .SwitchToChildWindow(2);

            ServiceUnitDetailPage serviceUnitDetail = PageFactoryManager.Get<ServiceUnitDetailPage>();
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.ServiceUnitPointTab);
            serviceUnitDetail.WaitForLoadingIconToDisappear(false);
            serviceUnitDetail.ClickOnElement(serviceUnitDetail.AddPointButton);
            serviceUnitDetail.VerifyElementVisibility(serviceUnitDetail.AddServiceUnitPointDiv, true);
            serviceUnitDetail.VerifyRadioIsSelected()
                .VerifySelectedValue(serviceUnitDetail.SectorSelect, "Richmond")
                .ClickOnElement(serviceUnitDetail.AddServiceUnitPointCloseButton);
            serviceUnitDetail.ClickCloseBtn()
                .SwitchToFirstWindow();
            serviceUnit.ClickOnElement(serviceUnit.GetToogleButton(AutoUser40.DisplayName));
            serviceUnit.SelectByDisplayValueOnUlElement(serviceUnit.UserDropDown, "Locale Languages")
                .SwitchToChildWindow(2);
            LocalLanguagePage localLanguagePage = PageFactoryManager.Get<LocalLanguagePage>();
            localLanguagePage.SelectTextFromDropDown(localLanguagePage.LanguageSelect, "French")
                .ClickOnElement(localLanguagePage.SaveButton);
            localLanguagePage.SwitchToFirstWindow();
            string description = serviceUnit.GetElementText(serviceUnit.CreateDescriptionButton);
            var factory = new RankedLanguageIdentifierFactory();
            var identifier = factory.Load(@"Source\Main\Data\Core14.profile.xml");
            var languages = identifier.Identify(description);
            var mostCertainLanguage = languages.FirstOrDefault();
            Assert.IsNotNull(mostCertainLanguage);
            Assert.IsTrue(mostCertainLanguage.Item1.Iso639_2T == "fra");
        }
    }
}
