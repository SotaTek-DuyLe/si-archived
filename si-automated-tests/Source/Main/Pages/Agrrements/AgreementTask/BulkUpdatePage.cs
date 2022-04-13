using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Pages.Agrrements.AgreementTask
{
    public class BulkUpdatePage : BasePage
    {
        private readonly By taskBulkUpdateTitle = By.XPath("//label[contains(text(),'Task Bulk Update')]");
        private readonly By completedDateInput = By.Id("completedDate.id");
        private readonly By endDateInput = By.Id("endDate.id");
        private readonly By noteInput = By.Id("notes.id");
        private readonly By useBackGroundCheckBox = By.XPath("//label[contains(text(),'Use Background Transaction')]/following-sibling::input");
        private readonly By standardCommercialCollectionSpan = By.XPath("//label[text()='Standard - Commercial Collection']/parent::span");

        private readonly By taskState = By.Id("taskStates.id");
        private readonly By taskStateCompleted = By.XPath("//select[@name='taskStates']/option[text()='Completed']");
        private readonly By resolutionCodeSelect = By.XPath("//label[contains(text(),'Resolution Code')]/following-sibling::echo-select/select");
        private string taskBulkUpdateNumText = "//label[contains(text(),'{0}')]";
    }
}
