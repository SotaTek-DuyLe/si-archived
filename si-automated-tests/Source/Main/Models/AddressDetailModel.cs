using System;
using si_automated_tests.Source.Main.Constants;

namespace si_automated_tests.Source.Main.Models
{
    public class AddressDetailModel
    {
        private string siteName;
        private int property;
        private string street;
        private string town;
        private string postCode;
        private string country;
        private string address;

        public AddressDetailModel(string siteName, string postCode)
        {
            this.SiteName = siteName;
            this.Property = 47;
            this.Street = "Sandy Ln";
            this.Town = Region.UK;
            this.PostCode = postCode;
            this.Country = "UK";
            this.Address = "101 Unknown Street";
        }

        public int Property { get => property; set => property = value; }
        public string Street { get => street; set => street = value; }
        public string Town { get => town; set => town = value; }
        public string PostCode { get => postCode; set => postCode = value; }
        public string Country { get => country; set => country = value; }
        public string SiteName { get => siteName; set => siteName = value; }
        public string Address { get => address; set => address = value; }
    }
}
