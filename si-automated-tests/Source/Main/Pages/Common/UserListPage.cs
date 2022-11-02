using System;
using System.Collections.Generic;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Constants;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models.Services;


namespace si_automated_tests.Source.Main.Pages.Common
{
    public class UserListPage : BasePageCommonActions
    {
        private string UserTable = "//div[@class='grid-canvas']";
        private string UserRow = "./div[contains(@class, 'slick-row')]";
        private string CheckboxCell = "./div[contains(@class, 'l0')]//input";
        private string IdCell = "./div[contains(@class, 'l1')]";
        private string UserNameCell = "./div[contains(@class, 'l2')]";
        private string NameCell = "./div[contains(@class, 'l3')]";
        private string ContractCell = "./div[contains(@class, 'l4')]";

        public TableElement UserTableEle
        {
            get => new TableElement(UserTable, UserRow, new List<string>() { CheckboxCell, IdCell, UserNameCell, NameCell, ContractCell });
        }

        public readonly By UserNameHeaderInput = By.XPath("//div[contains(@class, 'slick-headerrow-column l2')]//input");

        public UserListPage DoubleClickRow(int idx)
        {
            UserTableEle.DoubleClickRow(idx);
            return this;
        }
    }
}
