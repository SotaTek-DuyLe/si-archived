using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
using System;
using System.Collections.Generic;

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

        public List<ServiceJoinServiceGroupDBModel> GetServiceAndServiceGroupInfo(int serviceId)
        {
            string query = "SELECT * FROM services s join servicegroups s2 on s.servicegroupID = s2.servicegroupID  WHERE s.serviceID = " + serviceId;
            return FindList<ServiceJoinServiceGroupDBModel>(query);
        }

        public List<EventDBModel> GetEvent(int eventId)
        {
            string query = "select* from events where eventid = " + eventId + ";";
            return FindList<EventDBModel>(query);
        }

        public List<PointAddressModel> GetPointAddress(string id)
        {
            string query = "select * from pointaddresses where pointaddressID="+id+";";
            return FindList<PointAddressModel>(query);
        }

        public List<ServiceUnitPointDBModel> GetServiceUnitPoint(int serviceunitpointid)
        {
            string query = "select * from serviceunitpoints where serviceunitpointid=" + serviceunitpointid + ";";
            return FindList<ServiceUnitPointDBModel>(query);
        }

        public List<PointTypeDBModel> GetPointType(int pointtypeId)
        {
            string query = "SELECT * FROM pointtypes p WHERE p.pointtypeID =" + pointtypeId + ";";
            return FindList<PointTypeDBModel>(query);
        }

        public List<ServiceUnitDBModel> GetServiceUnit(int serviceunitid)
        {
            string query = "select * from serviceunits where serviceunitid=" + serviceunitid + ";";
            return FindList<ServiceUnitDBModel>(query);
        }

        public List<ServiceUnitTypeDBModel> GetServiceUnitType(int serviceunittypeID)
        {
            string query = "SELECT * FROM serviceunittypes s WHERE s.serviceunittypeID  = " + serviceunittypeID + ";";
            return FindList<ServiceUnitTypeDBModel>(query);
        }

        public List<PointSegmentDBModel> GetPointSegment(int pointsegmentID)
        {
            string query = "SELECT * FROM pointsegments p WHERE p.pointsegmentID = " + pointsegmentID + ";";
            return FindList<PointSegmentDBModel>(query);
        }

        public List<StreetDBModel> GetStreet(int streetID)
        {
            string query = "SELECT * FROM streets s WHERE s.streetID = " + streetID + ";";
            return FindList<StreetDBModel>(query);
        }

        public List<ServiceUnitPointDBModel> GetServiceUnitPointWithNoLock(int serviceunitID)
        {
            string query = "SELECT T0.* FROM [serviceunitpoints] AS T0 WITH (NOLOCK)  WHERE T0.[serviceunitID] = " + serviceunitID + ";";
            return FindList<ServiceUnitPointDBModel>(query);
        }

        public bool IsObjectNoticeExist(string echoTypeId, string id)
        {
            string query = "select * from objectnotices where echotypeID=" + echoTypeId + " and objectnoticeid=" + id;
            return FindList<ObjectNoticeModel>(query).Count != 0;
        }

        public List<InspectionDBModel> GetInspectionById(int inspectionId)
        {
            string query = "select * from inspections where inspectionID=" + inspectionId + ";";
            return FindList<InspectionDBModel>(query);
        }
    }
}
