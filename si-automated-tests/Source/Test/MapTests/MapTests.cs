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
    public class MapTests : BaseTest
    {
        public override void Setup()
        {
            base.Setup();
        }

        [Category("Sectors")]
        [Category("Huong")]
        [Test(Description = "Verify that user can add Sector Group from the Maps")]
        public void TC_195_Verify_that_user_can_add_Sector_Group_from_the_Maps()
        {
            //LOGIN AND GO TO POINT ADDRESS
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser27.UserName, AutoUser27.Password)
                .IsOnHomePage(AutoUser27);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Maps)
                .ExpandOption(Contract.Commercial);
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Sector Groups")
                .SwitchNewIFrame();
            SectorGroupPage sectorGroupPage = PageFactoryManager.Get<SectorGroupPage>();
            sectorGroupPage.WaitForLoadingIconToDisappear();
            sectorGroupPage.WaitForLoadingIconToDisappear();
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
            sectorGroupDetailPage.VerifyContainToastMessage("Template Sector is required");

            //Enter values in mandatory fields
            sectorGroupDetailPage.SendKeys(sectorGroupDetailPage.SectorGroupInput, "Week 1");
            sectorGroupDetailPage.SelectTextFromDropDown(sectorGroupDetailPage.SectorGroupTypeSelect, "Allocations");
            sectorGroupDetailPage.SelectTextFromDropDown(sectorGroupDetailPage.SectorSelect, Contract.Commercial);
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
            sectorGroupDetailPage.ExpandNode(Contract.Commercial)
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
            sectorGroupDetailPage.VerifyToastMessage("Sector Type is required")
                .WaitUntilToastMessageInvisible("Sector Type is required");

            //Enter values in mandatory fields:
            sectorGroupDetailPage.SendKeys(sectorGroupDetailPage.SectorNameInput, "Monday");
            sectorGroupDetailPage.SelectIndexFromDropDown(sectorGroupDetailPage.SectorTypeSelect, 1);
            sectorGroupDetailPage.ClickOnElement(sectorGroupDetailPage.FinishButton);
            sectorGroupDetailPage.SwitchToChildWindow(3)
                .WaitForLoadingIconToDisappear();
            SectorGroupLayerTypePage sectorGroupLayerTypePage = PageFactoryManager.Get<SectorGroupLayerTypePage>();
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.SectorGroupTab);
            sectorGroupLayerTypePage.WaitForLoadingIconToDisappear();
            sectorGroupLayerTypePage.VerifyInputValue(sectorGroupLayerTypePage.SectorGroupNameInput, "Week 1")
                .VerifySelectedValue(sectorGroupLayerTypePage.SectorGroupTypeSelect, "Allocations")
                .VerifyInputValue(sectorGroupLayerTypePage.SectorInput, Contract.Commercial);
        }

        [Category("Sectors")]
        [Category("Huong")]
        [Test(Description = "The GpsTrailData not loaded when add GpsEventData columns into grid")]
        public void TC_184_The_GpsTrailData_not_loaded_when_add_GpsEventData_columns_into_grid()
        {
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser27.UserName, AutoUser27.Password)
                .IsOnHomePage(AutoUser27);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Maps)
                .OpenOption(Contract.Commercial);
            PageFactoryManager.Get<NavigationBase>()
                .SwitchNewIFrame()
                .WaitForLoadingIconToDisappear();
            MapListingPage mapListingPage = PageFactoryManager.Get<MapListingPage>();
            mapListingPage.InputCalendarDate(mapListingPage.FromInput, "26/08/2022 00:00");
            mapListingPage.ClickOnElement(mapListingPage.ToInput);
            mapListingPage.ClickOnElement(mapListingPage.GoButton);
            mapListingPage.WaitForLoadingIconToDisappear();
            mapListingPage.VerifyElementVisibility(mapListingPage.ResourceCom, true)
                .VerifyElementVisibility(mapListingPage.RightHandLayer, true)
                .VerifyElementVisibility(mapListingPage.RoundsLayer, true);
            mapListingPage.ClickOnElement(mapListingPage.TrailTab);
            //Right click on any column in the grid
            mapListingPage.RightClickOnElement(mapListingPage.GetHeaderColumn("Lat"));
            mapListingPage.ClickOnElement(mapListingPage.MoreButton);
            mapListingPage.WaitForLessButtonDisplayed();
            mapListingPage.WaitForLoadingIconToDisappear();
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Lift Time"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Lifter Speed"));
            mapListingPage.ClickOnElement(mapListingPage.UpdateButton);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Lift Time"), true);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Lifter Speed"), true);

            //Right click on any column in the grid -> Click more -> Select 3 more options e.g. Bin count, gross weight, RFID number -> Update
            mapListingPage.RightClickOnElement(mapListingPage.GetHeaderColumn("Lat"));
            mapListingPage.ClickOnElement(mapListingPage.MoreButton);
            mapListingPage.WaitForLessButtonDisplayed();
            mapListingPage.WaitForLoadingIconToDisappear();
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Bin Count"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Gross Weight"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("RFID Number"));
            mapListingPage.ClickOnElement(mapListingPage.UpdateButton);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Bin Count"), true);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Gross Weight"), true);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("RFID Number"), true);

            //Right click on any column in the grid -> Click more -> Unselect the ticket options and set 5 different options -> Update 
            mapListingPage.RightClickOnElement(mapListingPage.GetHeaderColumn("Lat"));
            mapListingPage.ClickOnElement(mapListingPage.MoreButton);
            mapListingPage.WaitForLessButtonDisplayed();
            mapListingPage.WaitForLoadingIconToDisappear();
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Lift Time"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Lifter Speed"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Bin Count"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Gross Weight"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("RFID Number"));

            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Dismissed Date"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Driver door status"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Full Message"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("Gps Event Image"));
            mapListingPage.ClickOnElement(mapListingPage.GetExtraCheckbox("GPS Threshold Alert Level"));
            mapListingPage.ClickOnElement(mapListingPage.UpdateButton);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Lift Time"), false);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Lifter Speed"), false);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Bin Count"), false);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Gross Weight"), false);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("RFID Number"), false);

            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Dismissed Date"), true);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Driver door status"), true);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Full Message"), true);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("Gps Event Image"), true);
            mapListingPage.VerifyElementVisibility(mapListingPage.GetHeaderColumn("GPS Threshold Alert Level"), true);
        }

        [Category("Sectors")]
        [Category("Huong")]
        [Test(Description = "")]
        public void TC_196_Sector_Group()
        {
            //Verify that user can add new Sectors (parent sectors) under the Sector Group
            PageFactoryManager.Get<LoginPage>()
                .GoToURL(WebUrl.MainPageUrl);
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser27.UserName, AutoUser27.Password)
                .IsOnHomePage(AutoUser27);
            PageFactoryManager.Get<NavigationBase>()
                .ClickMainOption(MainOption.Maps)
                .ExpandOption(Contract.Commercial);
            PageFactoryManager.Get<NavigationBase>()
                .OpenOption("Sector Groups")
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
                .FilterItem(1)
                .OpenFirstResult()
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            SectorGroupLayerTypePage sectorGroupLayerTypePage = PageFactoryManager.Get<SectorGroupLayerTypePage>();
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.AddSectorButton);
            sectorGroupLayerTypePage.VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorNameInput, true)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorColorInput, true)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorTypeSelect, true)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewParentSectorInput, true)
                .VerifyElementEnable(sectorGroupLayerTypePage.NewSectorTypeSelect, false);
            //Click on 'Create'
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.CreateButton);
            sectorGroupLayerTypePage.VerifyToastMessage("Sector Name is required")
                .WaitUntilToastMessageInvisible("Sector Name is required");

            //Enter Sector Name: Tuesday
            //Click on 'Create'
            string sectorName = "Tuesday";
            string colorInput = sectorGroupLayerTypePage.GetInputValue(sectorGroupLayerTypePage.NewSectorColorInput);
            string parentSector = sectorGroupLayerTypePage.GetInputValue(sectorGroupLayerTypePage.NewParentSectorInput);
            string sectorType = sectorGroupLayerTypePage.GetFirstSelectedItemInDropdown(sectorGroupLayerTypePage.NewSectorTypeSelect);
            sectorGroupLayerTypePage.SendKeys(sectorGroupLayerTypePage.NewSectorNameInput, sectorName);
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.CreateButton);
            sectorGroupLayerTypePage.VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorNameInput, false)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorColorInput, false)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorTypeSelect, false)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewParentSectorInput, false);
            sectorGroupLayerTypePage.VerifyNewSectorInLeftPanel(sectorName, "0 polygon(s)");

            //In left panel, click on Tuesday sector
            sectorGroupLayerTypePage.ClickOnSector(sectorName)
                .SleepTimeInMiliseconds(300);

            //Under the map, navigate to Sector tab
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.SectorTab);
            sectorGroupLayerTypePage.WaitForLoadingIconToDisappear();
            sectorGroupLayerTypePage.VerifySectorTab(sectorName, colorInput, sectorType, parentSector);

            //Edit 'Sector Name' field: 'Sector Name'=Tuesday X
            //Click 'Save' button
            sectorName = "Tuesday33";
            sectorGroupLayerTypePage.SendKeysWithoutClear(sectorGroupLayerTypePage.SectorNameInSectorTab, "33");
            sectorGroupLayerTypePage.SleepTimeInMiliseconds(2000);
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.UpdateSectorGroupButton);
            sectorGroupLayerTypePage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);
            sectorGroupLayerTypePage.VerifyInputValue(sectorGroupLayerTypePage.SectorNameInSectorTab, sectorName);

            ///Verify that user can add child Sectors under the parent Sectors
            //On the same Sector Group form, click '+' by 'Monday' Sector
            sectorGroupLayerTypePage.ClickAddSectorInChildSector("Monday");
            sectorGroupLayerTypePage.VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorNameInput, true)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorColorInput, true)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorTypeSelect, true)
                .VerifyElementVisibility(sectorGroupLayerTypePage.NewParentSectorInput, true)
                .VerifyElementEnable(sectorGroupLayerTypePage.NewSectorTypeSelect, true)
                .VerifyInputValue(sectorGroupLayerTypePage.NewParentSectorInput, "Monday");
            //Click on 'Create'
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.CreateButton);
            sectorGroupLayerTypePage.VerifyToastMessage("Sector Name is required")
                .WaitUntilToastMessageInvisible("Sector Name is required");

            //Enter Sector Name: 'Child sector for Monday'
            //Click on 'Create'
            string childSectorName = "Child sector for Monday";
            sectorGroupLayerTypePage.SendKeys(sectorGroupLayerTypePage.NewSectorNameInput, childSectorName);
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.CreateButton);
            sectorGroupLayerTypePage.VerifyToastMessage("Sector Type is required")
                .WaitUntilToastMessageInvisible("Sector Type is required");
            sectorGroupLayerTypePage.SelectTextFromDropDown(sectorGroupLayerTypePage.NewSectorTypeSelect, "Zone");
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.CreateButton);
            sectorGroupLayerTypePage.SleepTimeInMiliseconds(1000);
            sectorGroupLayerTypePage.VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorNameInput, false)
               .VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorColorInput, false)
               .VerifyElementVisibility(sectorGroupLayerTypePage.NewSectorTypeSelect, false)
               .VerifyElementVisibility(sectorGroupLayerTypePage.NewParentSectorInput, false);
            sectorGroupLayerTypePage.VerifyNewChildSectorInLeftPanel(childSectorName, "3 polygon(s)");
            sectorGroupLayerTypePage.VerifyNewParentSectorInLeftPanel("Monday", "1 sector(s)");

            ///Verify that user can delete child sector if it's the only child sector for the Parent Sector
            sectorGroupLayerTypePage.ClickDeleteSectorInChildSector(childSectorName)
                .SleepTimeInMiliseconds(300);
            sectorGroupLayerTypePage.VerifySectorInLeftPanelDisappear(childSectorName);
            sectorGroupLayerTypePage.ClickOnElement(sectorGroupLayerTypePage.UpdateSectorGroupButton);
            sectorGroupLayerTypePage.VerifyToastMessage(MessageSuccessConstants.SuccessMessage);

            //Verify that user can delete Parent Sector if it doesn't have children
            sectorGroupLayerTypePage.CloseCurrentWindow()
                .SwitchToFirstWindow()
                .SwitchNewIFrame();
            PageFactoryManager.Get<CommonBrowsePage>()
               .FilterItem(2)
               .OpenFirstResult()
               .SwitchToChildWindow(2)
               .WaitForLoadingIconToDisappear();
        }
    }
}
