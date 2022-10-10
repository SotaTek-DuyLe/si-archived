using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.Maps;
using si_automated_tests.Source.Main.Pages.NavigationPanel;
using si_automated_tests.Source.Main.Pages.Search.PointAreas;
using si_automated_tests.Source.Main.Pages.Search.PointNodes;
using si_automated_tests.Source.Main.Pages.Services;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.MapTests
{
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class SectorTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
            //LOGIN AND GO TO POINT ADDRESS
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser27.UserName, AutoUser27.Password)
                .IsOnHomePage(AutoUser27);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Maps)
                .ExpandOption(Contract.RMC);
        }

        [Category("Sectors")]
        [Category("Huong")]
        [Test(Description = "Verify that user can add Sector Group from the Maps")]
        public void TC_195_Verify_that_user_can_add_Sector_Group_from_the_Maps()
        {
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Sector Groups")
                .SwitchNewIFrame();
            SectorGroupPage sectorGroupPage = PageFactoryManager.Get<SectorGroupPage>();
            sectorGroupPage.VerifyElementVisibility(sectorGroupPage.AddNewItemButton, true)
                .VerifyElementVisibility(sectorGroupPage.CopyItemButton, true);
            //Click 'Add New Item'
            sectorGroupPage.ClickOnElement(sectorGroupPage.AddNewItemButton);
            sectorGroupPage.SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            SectorGroupDetailPage sectorGroupDetailPage = PageFactoryManager.Get<SectorGroupDetailPage>();
            sectorGroupDetailPage.VerifyElementIsMandatory(sectorGroupDetailPage.SectorGroupInput)
                .VerifyElementIsMandatory(sectorGroupDetailPage.SectorGroupTypeSelect)
                .VerifyElementIsMandatory(sectorGroupDetailPage.SectorSelect);

            //click on 'Next' button
            sectorGroupDetailPage.ClickOnElement(sectorGroupDetailPage.NextButton);
            sectorGroupDetailPage.VerifyToastMessage("Missing Information");

            //Enter values in mandatory fields
            sectorGroupDetailPage.SendKeys(sectorGroupDetailPage.SectorGroupInput, "Week 1");
            sectorGroupDetailPage.SelectTextFromDropDown(sectorGroupDetailPage.SectorGroupTypeSelect, "Allocations");
            sectorGroupDetailPage.SelectTextFromDropDown(sectorGroupDetailPage.SectorSelect, "Richmond Commercial");
            sectorGroupDetailPage.VerifyElementIsMandatory(sectorGroupDetailPage.SectorGroupInput, false)
                .VerifyElementIsMandatory(sectorGroupDetailPage.SectorGroupTypeSelect, false)
                .VerifyElementIsMandatory(sectorGroupDetailPage.SectorSelect, false);

            //Click on 'Next' button
            sectorGroupDetailPage.ClickOnElement(sectorGroupDetailPage.NextButton);
            sectorGroupDetailPage.WaitForLoadingIconToDisappear();
            sectorGroupDetailPage.VerifyElementVisibility(sectorGroupDetailPage.PreviousButton, true)
                .VerifyElementVisibility(sectorGroupDetailPage.NextButton, true);

            //Click on 'Next' button
            sectorGroupDetailPage.ClickOnElement(sectorGroupDetailPage.NextButton);
            sectorGroupDetailPage.WaitForLoadingIconToDisappear();
            sectorGroupDetailPage.VerifyElementVisibility(sectorGroupDetailPage.PreviousButton, true)
                .VerifyElementVisibility(sectorGroupDetailPage.FinishButton, true);

            //Click on 'Previous' button
            sectorGroupDetailPage.ClickOnElement(sectorGroupDetailPage.PreviousButton);
            sectorGroupDetailPage.WaitForLoadingIconToDisappear();
            //In Step 2, in left column, under Richmond Commercial expand Collections and select a service click 'Add'
            sectorGroupDetailPage.ExpandNode("Richmond Commercial")
                .ExpandNode("Collections")
                .SelectNode("Commercial Collections")
                .ClickOnElement(sectorGroupDetailPage.AddServiceButton);

            //Click on 'Next' button
            sectorGroupDetailPage.ClickOnElement(sectorGroupDetailPage.NextButton);
            sectorGroupDetailPage.WaitForLoadingIconToDisappear();
            sectorGroupDetailPage.VerifyElementIsMandatory(sectorGroupDetailPage.SectorNameInput)
                .VerifyElementIsMandatory(sectorGroupDetailPage.SectorTypeSelect);

            //Click on 'Finish' button
            sectorGroupDetailPage.ClickOnElement(sectorGroupDetailPage.FinishButton);
            sectorGroupDetailPage.VerifyToastMessage("Missing Information")
                .WaitUntilToastMessageInvisible("Missing Information");

            //Enter values in mandatory fields:
            sectorGroupDetailPage.SendKeys(sectorGroupDetailPage.SectorNameInput, "Monday");
            sectorGroupDetailPage.SelectTextFromDropDown(sectorGroupDetailPage.SectorTypeSelect, "Zone");
            sectorGroupDetailPage.ClickOnElement(sectorGroupDetailPage.FinishButton);
            sectorGroupDetailPage.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            SectorGroupLayerTypePage sectorGroupLayerTypePage = PageFactoryManager.Get<SectorGroupLayerTypePage>();
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.SectorGroupTab);
            sectorGroupLayerTypePage.WaitForLoadingIconToDisappear();
            sectorGroupLayerTypePage.VerifyInputValue(sectorGroupLayerTypePage.SectorGroupNameInput, "Week 1")
                .VerifySelectedValue(sectorGroupLayerTypePage.SectorGroupTypeSelect, "Allocations")
                .VerifyInputValue(sectorGroupLayerTypePage.SectorInput, "Richmond Commercial");
        }
    }
}
