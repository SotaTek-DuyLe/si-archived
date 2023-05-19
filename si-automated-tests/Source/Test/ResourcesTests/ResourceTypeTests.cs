using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using si_automated_tests.Source.Main.Models.Suspension;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Paties;
using si_automated_tests.Source.Main.Pages.Sites;
using si_automated_tests.Source.Main.Pages.Paties.Parties.PartySuspension;
using static si_automated_tests.Source.Main.Models.UserRegistry;
using si_automated_tests.Source.Main.Pages.Paties.Sites;

namespace si_automated_tests.Source.Test.ResourcesTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ResourceTypeTests : BaseTest
    {
        [Category("Sites")]
        [Category("Huong")]
        [Test()]
        public void TC_249_Qualification_Constraints_to_ResourceTypes()
        {
            //Verify whether Required Qualification Tab is in the ResourceType Form
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl + "web/grids/resourcetypes");
            PageFactoryManager.Get<LoginPage>()
                .Login(AutoUser71.UserName, AutoUser71.Password);
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(6)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            SitePage sitePage = PageFactoryManager.Get<SitePage>();
            sitePage.ClickOnElement(sitePage.RequiredQualificationTab);
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyElementVisibility(sitePage.AddQualificationButton, true)
                .VerifyElementVisibility(sitePage.QualificationTable, true);

            //Verify whether user able to click Add New Item Button
            sitePage.ClickOnElement(sitePage.AddQualificationButton);
            sitePage.SleepTimeInMiliseconds(500);
            sitePage.VerifyElementVisibility(sitePage.ToogleSelectQualificationButton, true)
                .VerifyElementVisibility(sitePage.ConfirmAddQualificationButton, true)
                .VerifyElementVisibility(sitePage.CancelAddQualificationButton, true);

            //Verify user able to select single or multiple QC from the popup Window
            sitePage.ClickOnElement(sitePage.ToogleSelectQualificationButton);
            sitePage.SelectByDisplayValueOnUlElement(sitePage.ExpandedListbox, "H&S - Human")
                .SleepTimeInMiliseconds(200);
            sitePage.ClickOnElement(sitePage.ConfirmAddQualificationButton);

            //Verify when new constraint is added and is visible in the main panel
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time");
            sitePage.VerifyNewQualification("H&S - Human", londonCurrentDate.ToString("dd/MM/yyyy"), "01/01/2050");

            //Verify whether user able to Retire QC
            sitePage.ClickRetireQualification(0);
            sitePage.WaitForLoadingIconToDisappear();
            sitePage.VerifyToastMessage("Successfully retired Required Qualification");
        }
    }
}
