using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.PartySitePage;
using si_automated_tests.Source.Main.Pages.Resources;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ServiceTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ContractTests : BaseTest
    {
        private string successToast = MessageSuccessConstants.SuccessMessage;
        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO BUSINESS UNITS
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser29.UserName, AutoUser29.Password)
                .IsOnHomePage(AutoUser29);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Services)
                .ExpandOption("Regions")
                .ExpandOption(Region.UK)
                .ExpandOption(Contract.RMC);
        }
        [Category("Create Contract And Unit Site")]
        [Category("Dee")]
        [Test]
        public void TC_121_A_create_contract_and_unit_site()
        {
            string postalCode = "TW9 1DN";
            string site = "APARTMENT 6";
            string siteName = "Contra 2 " + CommonUtil.GetRandomNumber(5);
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Contract Sites")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ContractSiteDetailPage>()
                .ClickAddBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreateEditSiteAddressPage>()
                .IsOnCreateEditSiteAddressPage()
                .SearchForSite(postalCode)
                .SelectResultInScreen1(postalCode)
                .SelectResultInScreen2(site)
                .InsertSiteName(siteName)
                .ClickCreateBtn()
                .SleepTimeInMiliseconds(2000)
                .SwitchToLastWindow()
                .WaitForLoadingIconToDisappear();
            PageFactoryManager.Get<ContractSiteDetailPage>()
                .ClickSaveBtn()
                .VerifyToastMessage(successToast);
            //Verify duplcate cant be created
            PageFactoryManager.Get<ContractSiteDetailPage>()
                .ClickAddBtn()
                .SwitchToLastWindow();
            PageFactoryManager.Get<CreateEditSiteAddressPage>()
                .IsOnCreateEditSiteAddressPage()
                .SearchForSite(postalCode)
                .SelectResultInScreen1(postalCode)
                .SelectResultInScreen2(site)
                .VerifyDuplicateErrorMessage()
                .CloseCurrentWindow()
                .SwitchToLastWindow();
            PageFactoryManager.Get<BasePage>()
                .SleepTimeInMiliseconds(5000);
        }
        [Category("Create Contract And Unit Site")]
        [Category("Dee")]
        [Test]
        public void TC_121_B_create_contract_and_unit_site()
        {
            string addressName = "Greggs";
            string address = "35 THE QUADRANT";
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Contract Sites")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ContractSiteDetailPage>()
                .SearchForAddress(addressName)
                .SelectAddress(address)
                .ClickSaveBtn()
                .VerifyToastMessage(successToast);
            PageFactoryManager.Get<BasePage>()
                .SleepTimeInMiliseconds(5000);
        }
        [Category("Create Contract And Unit Site")]
        [Category("Dee")]
        [Test]
        public void TC_121_C_create_contract_and_unit_site()
        {
            successToast = MessageSuccessConstants.SuccessMessage;
            string name = "Municipal " + CommonUtil.GetRandomNumber(5);
            string reference = "Test " + CommonUtil.GetRandomNumber(5);
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Contract Units")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .ClickAddNewItem()
                .SwitchToLastWindow();
            PageFactoryManager.Get<ContractUnitDetailPage>()
                .InputContractUnitDetails(name, reference)
                .ClickSaveBtn()
                .VerifyToastMessage(successToast)
                .CloseCurrentWindow()
                .SwitchToLastWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .VerifyFirstResultValue("Name", name);
        }
    }
}
