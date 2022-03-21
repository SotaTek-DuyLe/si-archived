using System;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Models
{
    public class ContactModel
    {
        private string id;
        private string title;
        private string firstName;
        private string lastName;
        private string position;
        private string greeting;
        private string telephone;
        private string mobile;
        private string email;
        private bool receiveEmail;
        private string contactGroups;
        private string startDate;
        private string endDate;

        public ContactModel()
        {
            string randomNumb = CommonUtil.GetRandomNumber(3);
            this.title = "Mr.";
            this.firstName = "John" + randomNumb;
            this.lastName = "Mercury" + randomNumb;
            this.position = "Manager" + randomNumb;
            this.greeting = "Hello" + randomNumb;
            this.telephone = "+442785434" + randomNumb;
            this.mobile = "+447785434" + randomNumb;
            this.email = "john.mercury@email.com";
            this.receiveEmail = true;
            this.contactGroups = "Operational";
            this.startDate = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            this.endDate = CommonConstants.EndDateAgreement;
        }

        public ContactModel(bool receiveEmail, string startDate)
        {
            string randomNumb = CommonUtil.GetRandomNumber(3);
            this.title = "Mr.";
            this.firstName = "John" + randomNumb;
            this.lastName = "Mercury" + randomNumb;
            this.position = "Manager" + randomNumb;
            this.greeting = "Hello" + randomNumb;
            this.telephone = "+442785434" + randomNumb;
            this.mobile = "+447785434" + randomNumb;
            this.email = "john.mercury@email.com";
            this.receiveEmail = receiveEmail;
            this.contactGroups = "Operational";
            this.startDate = startDate;
            this.endDate = CommonConstants.EndDateAgreement;
        }

        public ContactModel(string firstName, string lastName, string mobile)
        {
            this.title = "Mr.";
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = "Manager";
            this.greeting = "Hello";
            this.telephone = "+442785434111";
            this.mobile = mobile;
            this.email = "john.mercury@email.com";
            this.receiveEmail = true;
            this.contactGroups = "Operational";
            this.startDate = CommonUtil.GetLocalTimeNow(CommonConstants.DATE_DD_MM_YYYY_FORMAT);
            this.endDate = CommonConstants.EndDateAgreement;
        }

        public ContactModel(string id, string title, string firstName, string lastName, string position, string telephone, string mobile, string email, bool receiveEmail, string contactGroups, string startDate, string endDate)
        {
            this.id = id;
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = position;
            this.telephone = telephone;
            this.mobile = mobile;
            this.email = email;
            this.receiveEmail = receiveEmail;
            this.contactGroups = contactGroups;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public ContactModel(string id, string title, string firstName, string lastName, string position, string greeting, string telephone, string mobile, string email, bool receiveEmail, string contactGroups, string startDate, string endDate)
        {
            this.id = id;
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = position;
            this.greeting = greeting;
            this.telephone = telephone;
            this.mobile = mobile;
            this.email = email;
            this.receiveEmail = receiveEmail;
            this.contactGroups = contactGroups;
            this.startDate = startDate;
            this.endDate = endDate;
        }


        public string Title { get => title; set => title = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Position { get => position; set => position = value; }
        public string Greeting { get => greeting; set => greeting = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Mobile { get => mobile; set => mobile = value; }
        public string Email { get => email; set => email = value; }
        public bool ReceiveEmail { get => receiveEmail; set => receiveEmail = value; }
        public string ContactGroups { get => contactGroups; set => contactGroups = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
        public string Id { get => id; set => id = value; }
    }
}
