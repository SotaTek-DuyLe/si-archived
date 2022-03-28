using System;
namespace si_automated_tests.Source.Main.Models
{
    public class SiteModel
    {
        private string id;
        private string name;
        private string address;
        private string siteType;
        private string startDate;
        private string endDate;
        private string clientReference;
        private string accountingReference;
        private string abv;

        public SiteModel(string id, string name, string siteType, string startDate, string endDate, string clientRef)
        {
            this.id = id;
            this.name = name;
            this.siteType = siteType;
            this.startDate = startDate;
            this.endDate = endDate;
            this.clientReference = clientRef;
        }

        public SiteModel(string id, string name, string address, string siteType, string startDate, string endDate, string clientRef, string accountingRef, string abv)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.siteType = siteType;
            this.startDate = startDate;
            this.endDate = endDate;
            this.clientReference = clientRef;
            this.accountingReference = accountingRef;
            this.abv = abv;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string SiteType { get => siteType; set => siteType = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string EndDate { get => endDate; set => endDate = value; }
        public string ClientReference { get => clientReference; set => clientReference = value; }
        public string AccountingReference { get => accountingReference; set => accountingReference = value; }
        public string Abv { get => abv; set => abv = value; }
    }
}
