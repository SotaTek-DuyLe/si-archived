using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Core;
using NUnit.Framework;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Finders;
using si_automated_tests.Source.Main.Pages;
using si_automated_tests.Source.Main.Pages.ResourceTerm;
using static si_automated_tests.Source.Main.Models.UserRegistry;

namespace si_automated_tests.Source.Test.ResourceTermTests
{
    [Author("Chang", "trang.nguyenthi@sotatek.com")]
    [Parallelizable(scope: ParallelScope.Fixtures)]
    [TestFixture]
    public class ResourceTermTests : BaseTest
    {
        [Category("Resource Term")]
        [Category("Chang")]
        [Test(Description = "Resource term Entitlements state displays vehicle states")]
        public void TC_160_Resource_term_Entitlements_state_displays_vehicle_states()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/grids/resourceterms");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser31.UserName, AutoUser31.Password);
            ResourceTermPage resourceTermPage = PageFactoryManager.Get<ResourceTermPage>();
            resourceTermPage.WaitForLoadingIconToDisappear();
            resourceTermPage.DoubleClickRow(0)
                .SwitchToChildWindow(2)
                .WaitForLoadingIconToDisappear();
            ResourceTermDetailPage resourceTermDetailPage = PageFactoryManager.Get<ResourceTermDetailPage>();
            resourceTermDetailPage.ClickOnElement(resourceTermDetailPage.EntitlementTab);
            List<string> resourceStateExpected = new List<string>() { "Allocated", "Available", "AWOL", "Holiday", "Jury Service", "Scheduled", "Sick", "TOIL", "Training" };
            resourceTermDetailPage.VerifyResourceStateValue(0, resourceStateExpected);
            CommonFinder finder = new CommonFinder(DbContext);
            var humanResourceClass = finder.GetResourceClasses().FirstOrDefault(x => x.resourceclass == "Human");
            var resourceStates = finder.GetResourceStates(humanResourceClass.resourceclassID).Select(x => x.resourcestate).ToList();
            Assert.That(resourceStates, Is.EquivalentTo(resourceStateExpected));
        }

        [Category("Resource Term")]
        [Category("Huong")]
        [Test(Description = "validation on resource states selected")]
        public void TC_317_Resource_Term_Entitlement()
        {
            PageFactoryManager.Get<LoginPage>()
               .GoToURL(WebUrl.MainPageUrl + "web/resource-terms/1");
            PageFactoryManager.Get<LoginPage>()
                .IsOnLoginPage()
                .Login(AutoUser31.UserName, AutoUser31.Password);
            ResourceTermDetailPage resourceTermDetailPage = PageFactoryManager.Get<ResourceTermDetailPage>();
            resourceTermDetailPage.WaitForLoadingIconToDisappear();
            resourceTermDetailPage.ClickOnElement(resourceTermDetailPage.EntitlementTab);
            //Click '+Add' and select following (Choose state which isn't added already on resource term):
            //Resource State = 'Jury Service'; Entitlement(Days) = 60; Pro Rata = True; Start Date = current date; End Date = current date + 6 months.Save Resoure Term form
            resourceTermDetailPage.ClickOnElement(resourceTermDetailPage.AddEntitleButton);
            resourceTermDetailPage.WaitForLoadingIconToDisappear();
            int rowIdx = resourceTermDetailPage.GetNewRowIdx();
            DateTime londonCurrentDate = CommonUtil.ConvertLocalTimeZoneToTargetTimeZone(DateTime.Now, "GMT Standard Time").AddHours(6);
            string startDate = londonCurrentDate.ToString("dd/MM/yyyy hh:mm");
            string endDate = londonCurrentDate.AddMonths(6).ToString("dd/MM/yyyy hh:mm");
            resourceTermDetailPage.EditResourceEntitleValues(rowIdx, "Jury Service", "60", true, startDate, endDate);
            resourceTermDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            //Click 'Refresh' on Resource Term form
            resourceTermDetailPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            resourceTermDetailPage.VerifyResourceEntitleValues(rowIdx, "Jury Service", "60", true, startDate, endDate);

            //On the Same Resource Term in Entitlements tab, click '+Add' and select following:
            //Resource State = 'Jury Service'; Entitlement(Days) = 45; Pro Rata = False; Start Date = current date - 1month; End Date = current date + 1 months.Save Resoure Term form
            resourceTermDetailPage.ClickOnElement(resourceTermDetailPage.AddEntitleButton);
            resourceTermDetailPage.WaitForLoadingIconToDisappear();
            int rowIdx2 = resourceTermDetailPage.GetNewRowIdx();
            string startDate2 = londonCurrentDate.AddMonths(-1).ToString("dd/MM/yyyy hh:mm");
            string endDate2 = londonCurrentDate.AddMonths(1).ToString("dd/MM/yyyy hh:mm");
            resourceTermDetailPage.EditResourceEntitleValues(rowIdx2, "Jury Service", "45", false, startDate2, endDate2);
            resourceTermDetailPage.ClickSaveBtn()
                .VerifyToastMessage("Error - overlapping resource state validity period. Ensure start and end dates do not overlap")
                .WaitUntilToastMessageInvisible("Error - overlapping resource state validity period. Ensure start and end dates do not overlap");

            //Click 'Refresh' on Resource Term form
            resourceTermDetailPage.ClickRefreshBtn()
               .WaitForLoadingIconToDisappear();
            resourceTermDetailPage.VerifyNewRowIsDisappear(rowIdx2);

            //On the Same Resource Term in Entitlements tab, click '+Add' and select following:
            //Resource State = 'Jury Service'; Entitlement(Days) = 15; Pro Rata = False; Start Date = current date + 4months; End Date = current date + 12 months.Save Resoure Term form
            resourceTermDetailPage.ClickOnElement(resourceTermDetailPage.AddEntitleButton);
            resourceTermDetailPage.WaitForLoadingIconToDisappear();
            int rowIdx3 = resourceTermDetailPage.GetNewRowIdx();
            string startDate3 = londonCurrentDate.AddMonths(4).ToString("dd/MM/yyyy hh:mm");
            string endDate3 = londonCurrentDate.AddMonths(12).ToString("dd/MM/yyyy hh:mm");
            resourceTermDetailPage.EditResourceEntitleValues(rowIdx3, "Jury Service", "15", false, startDate3, endDate3);
            resourceTermDetailPage.ClickSaveBtn()
                .VerifyToastMessage("Error - overlapping resource state validity period. Ensure start and end dates do not overlap")
                .WaitUntilToastMessageInvisible("Error - overlapping resource state validity period. Ensure start and end dates do not overlap");

            //Click 'Refresh' on Resource Term form
            resourceTermDetailPage.ClickRefreshBtn()
               .WaitForLoadingIconToDisappear();
            resourceTermDetailPage.VerifyNewRowIsDisappear(rowIdx3);

            //On the Same Resource Term in Entitlements tab, click '+Add' and select following: )Select different state than above)
            //Resource State = 'Sick'; Entitlement(Days) = 15; Pro Rata = False; Start Date = current date + 4months; End Date = current date + 12 months.Save Resoure Term form
            resourceTermDetailPage.ClickOnElement(resourceTermDetailPage.AddEntitleButton);
            resourceTermDetailPage.WaitForLoadingIconToDisappear();
            int rowIdx4 = resourceTermDetailPage.GetNewRowIdx();
            string startDate4 = londonCurrentDate.AddMonths(4).ToString("dd/MM/yyyy hh:mm");
            string endDate4 = londonCurrentDate.AddMonths(12).ToString("dd/MM/yyyy hh:mm");
            resourceTermDetailPage.EditResourceEntitleValues(rowIdx4, "Sick", "15", false, startDate4, endDate4);
            resourceTermDetailPage.ClickSaveBtn()
                .VerifyToastMessage(MessageSuccessConstants.SuccessMessage)
                .WaitUntilToastMessageInvisible(MessageSuccessConstants.SuccessMessage);

            //Click 'Refresh' on Resource Term form
            resourceTermDetailPage.ClickRefreshBtn()
                .WaitForLoadingIconToDisappear();
            resourceTermDetailPage.VerifyResourceEntitleValues(rowIdx4, "Sick", "15", false, startDate4, endDate4);
        }
    }
}
