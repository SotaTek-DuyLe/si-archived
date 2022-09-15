using System;
namespace si_automated_tests.Source.Main.Models.ServiceStatus
{
    public class TaskInWorksheetModel
    {
        public string checkboxLocator { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string service { get; set; }
        public string party { get; set; }
        public string site { get; set; }
        public string street { get; set; }
        public string town { get; set; }
        public string postcode { get; set; }

        public TaskInWorksheetModel(string checkboxLocator, string id, string desc, string service, string party, string site, string street, string town, string postcode)
        {
            this.checkboxLocator = checkboxLocator;
            this.id = id;
            this.description = desc;
            this.service = service;
            this.party = party;
            this.site = site;
            this.street = street;
            this.town = town;
            this.postcode = postcode;
        }
    }
}
