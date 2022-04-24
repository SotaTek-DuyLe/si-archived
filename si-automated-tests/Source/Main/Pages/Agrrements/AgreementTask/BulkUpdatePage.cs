using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
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
        private readonly By resolutionCodeText = By.XPath("//label[contains(text(),'Resolution Code')]");
        private readonly By resolutionCodeSelect = By.XPath("//label[contains(text(),'Resolution Code')]/following-sibling::echo-select/select");
        private readonly By taskCompletedDateInput = By.XPath("//div[contains(@data-bind,'taskTypeId')]//input[@id='completedDate.id']");
        private readonly By taskEndDateInput = By.XPath("//div[contains(@data-bind,'taskTypeId')]//input[@id='endDate.id']");
        private readonly By taskNote = By.XPath("//div[contains(@data-bind,'taskTypeId')]//textarea[@id='notes.id']");
        
        private string taskBulkUpdateNumText = "//label[contains(text(),'{0}')]";

        public BulkUpdatePage VerifyBulkUpdatePage(int num)
        {
            string numText = "Update " + num.ToString() + " Selected Tasks";
            WaitUtil.WaitForElementVisible(taskBulkUpdateTitle);
            Assert.IsTrue(IsControlDisplayed(taskBulkUpdateTitle));
            Assert.IsTrue(IsControlDisplayed(taskBulkUpdateNumText, numText));
            Assert.IsTrue(IsControlDisplayed(completedDateInput));
            Assert.IsTrue(IsControlDisplayed(endDateInput));
            Assert.IsTrue(IsControlDisplayed(noteInput));
            Assert.IsTrue(IsElementSelected(useBackGroundCheckBox));
            Assert.IsTrue(IsControlDisplayed(standardCommercialCollectionSpan));
            return this;
        }
        public BulkUpdatePage ExpandStandardCommercialCollection()
        {
            ClickOnElement(standardCommercialCollectionSpan);
            return this;
        }

        public BulkUpdatePage SelectCompletedState()
        {
            WaitUtil.WaitForElementVisible(taskStateCompleted);
            ClickOnElement(taskState);
            ClickOnElement(taskStateCompleted);
            return this;
        }
        public BulkUpdatePage ClickResolutionText()
        {
            ClickOnElement(resolutionCodeText);
            return this;
        }
        public BulkUpdatePage ClickTaskCompletedDate()
        {
            ClickOnElement(taskCompletedDateInput);
            return this;
        }
        public BulkUpdatePage VerifyTaskCompletedDateValue(string date)
        {
            String completedDate = GetAttributeValue(taskCompletedDateInput, "value");
            Assert.IsTrue(completedDate.Contains(date));
            return this;
        }

        public BulkUpdatePage ClickTaskEndDate()
        {
            ClickOnElement(taskEndDateInput);
            return this;
        }
        public BulkUpdatePage VerifyTaskEndDateValue(string date)
        {
            String endDate = GetAttributeValue(taskEndDateInput, "value");
            Assert.IsTrue(endDate.Contains(date));
            return this;
        }
        public BulkUpdatePage InputNote(string note)
        {
            SendKeys(taskNote, note);
            return this;
        }
    }
}
