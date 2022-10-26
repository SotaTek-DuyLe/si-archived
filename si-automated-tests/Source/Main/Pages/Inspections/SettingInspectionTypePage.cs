
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.Pages.Tasks.Inspection;

namespace si_automated_tests.Source.Main.Pages.Inspections
{
    public class SettingInspectionTypePage : BasePage
    {
        private readonly By inspectionTypeTitle = By.XPath("//span[contains(string(),'InspectionType')]");
        private readonly By firstRoleInRightColumn = By.CssSelector("select[class='inspectionTypeAdminRoles']>option:nth-child(1)");
        private readonly By saveRoleBtn = By.CssSelector("img[title='Save']");
        private readonly By removeBtn = By.CssSelector("a[id='removeAdminRole']");
        private readonly By loadingIconDisappear = By.XPath("//div[@id='Progress' and contains(@style, 'visibility: hidden')]");
        private readonly By allRoleInRightColumn = By.CssSelector("select[class='inspectionTypeAdminRoles']");
        private readonly string anyTab = "//a[text()='{0}']/parent::li";
        private readonly string inspectionTypeName = "//span[contains(string(),'{0}')]";

        [AllureStep]
        public SettingInspectionTypePage WaitForInpsectionTypeSettingDisplayed(string inspectionNameValue)
        {
            WaitUtil.WaitForElementVisible(inspectionTypeTitle);
            WaitUtil.WaitForElementVisible(inspectionTypeName, inspectionNameValue);
            return this;
        }
        [AllureStep]
        public SettingInspectionTypePage ClickRolesTab()
        {
            ClickOnElement(anyTab, "Roles");
            return this;
        }

        //public SettingInspectionTypePage SelectAllRoleInRightColumn()
        //{
        //    ClickOnElement(firstRoleInRightColumn);
        //    if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        //    {
        //        SendKeys(allRoleInRightColumn, Keys.Command + "a");
        //    } else
        //    {
        //        SendKeys(allRoleInRightColumn, Keys.Control + "a");
        //    }
        //    return this;

        //}
        [AllureStep]
        public SettingInspectionTypePage SelectAllRoleInRightColumn()
        {
            SelectElement oSelect = new SelectElement(driver.FindElement(allRoleInRightColumn));
            IList<IWebElement> elementCount = oSelect.Options;
            for (int i = 0; i < elementCount.Count; i++)
            {
                oSelect.SelectByText(GetElementText(elementCount[i]));
            }
            //Select all
            return this;
        }
        [AllureStep]
        public SettingInspectionTypePage ClickRemoveBtn()
        {
            ClickOnElement(removeBtn);
            return this;
        }
        [AllureStep]
        public SettingInspectionTypePage ClickSaveBtnToUpdateRole()
        {
            ClickOnElement(saveRoleBtn);
            return this;
        }
        [AllureStep]
        public SettingInspectionTypePage WaitForLoadingIconDisappear()
        {
            WaitUtil.WaitForAllElementsPresent(loadingIconDisappear);
            WaitUtil.WaitForPageLoaded();
            return this;

        }
        [AllureStep]
        public DetailInspectionPage OpenDetailInspectionWithId(string inspectionId)
        {
            GoToURL(WebUrl.MainPageUrl + "web/inspections/" + inspectionId);
            return PageFactoryManager.Get<DetailInspectionPage>();
        }
    }
}
