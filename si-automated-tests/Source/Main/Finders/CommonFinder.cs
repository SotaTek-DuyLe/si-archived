using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Finders
{
    public class CommonFinder : ServiceProvider
    {
        public CommonFinder(DatabaseContext context) : base(context)
        {

        }

        public List<SiteDBModel> GetSitesByPartyId(int partyId)
        {
            string query = "select * from sites where partyID=" + partyId;
            return FindList<SiteDBModel>(query);
        }

        public List<ServiceDBModel> GetServiceTypes()
        {
            string query = "select * from servicetypes;";
            return FindList<ServiceDBModel>(query);
        }
        public List<PointAddressModel> GetPointAddress(string id)
        {
            string query = "select * from pointaddresses where pointaddressID="+id+";";
            return FindList<PointAddressModel>(query);
        }
    }
}
