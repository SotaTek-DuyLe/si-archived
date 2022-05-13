using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class PointAddressModel
    {
        private int pointaddressID;
        private int postcodeID;
        private String propertyname;
        private int property;
        private int toproperty;
        private String sourcedescription;

        public int PointaddressID { get => pointaddressID; set => pointaddressID = value; }
        public int PostcodeID { get => postcodeID; set => postcodeID = value; }
        public string Propertyname { get => propertyname; set => propertyname = value; }
        public int Property { get => property; set => property = value; }
        public int Toproperty { get => toproperty; set => toproperty = value; }
        public string Sourcedescription { get => sourcedescription; set => sourcedescription = value; }
    }
}
