using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Core.WebElements;
using si_automated_tests.Source.Main.Models;

namespace si_automated_tests.Source.Main.Pages.Paties.Parties.PartyContactPage
{
    public class ContractListPage : BasePageCommonActions
    {
        public readonly By AddNewContractButton = By.XPath("//button[contains(., 'Add New Item')]");

        private string ContactTable = "//div[@class='grid-canvas']";
        private string ContactRow = "./div[contains(@class, 'slick-row')]";
        private string TitleCell = "./div[@class='slick-cell l2 r2']";
        private string FirstNameCell = "./div[@class='slick-cell l3 r3']";
        private string LastNameCell = "./div[@class='slick-cell l4 r4']";
        private string TelephoneCell = "./div[@class='slick-cell l7 r7']";

        public TableElement ContactTableEle
        {
            get => new TableElement(ContactTable, ContactRow, new System.Collections.Generic.List<string>() { TitleCell, FirstNameCell, LastNameCell, TelephoneCell });
        }

        public ContractListPage IsContractListExist()
        {
            VerifyElementVisibility(By.XPath(ContactTable), true);
            return this;
        }

        public ContractListPage VerifyNewContact(ContactModel contactModel)
        {
            var rows = ContactTableEle.GetRows();
            List<ContactModel> contacts = new List<ContactModel>();
            for (int i = 0; i < rows.Count; i++)
            {
                contacts.Add(new ContactModel()
                {
                    Title = ContactTableEle.GetCellValue(i, ContactTableEle.GetCellIndex(TitleCell)).AsString(),
                    FirstName = ContactTableEle.GetCellValue(i, ContactTableEle.GetCellIndex(FirstNameCell)).AsString(),
                    LastName = ContactTableEle.GetCellValue(i, ContactTableEle.GetCellIndex(LastNameCell)).AsString(),
                    Mobile = ContactTableEle.GetCellValue(i, ContactTableEle.GetCellIndex(TelephoneCell)).AsString(),
                });
            }
            Assert.IsTrue(contacts.Any(x => x.Title == contactModel.Title && x.FirstName == contactModel.FirstName && x.LastName == contactModel.LastName));
            return this;
        }
    }
}
