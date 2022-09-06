using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
using System.Collections.Generic;
using System.Linq;

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

        public List<PointAreaDBModel> GetPointArea(int pointAreaId)
        {
            string query = "SELECT * FROM pointareas p WHERE p.pointareaID = " + pointAreaId + ";";
            return FindList<PointAreaDBModel>(query);
        }

        public List<PointNodeDBModel> GetPointNode(int pointNodeId)
        {
            string query = "SELECT * FROM pointnodes p WHERE p.pointnodeID = " + pointNodeId + ";";
            return FindList<PointNodeDBModel>(query);
        }

        public ServiceUnitModel GetServiceUnitById(int serviceId)
        {
            return FindList<ServiceUnitModel>("select * from serviceunits where serviceunitID=" + serviceId + ";").FirstOrDefault();
        }

        public List<TaskDBModel> GetMultipleTask(int firstTaskId, int secondTaskId)
        {
            string query = "select * from tasks where taskID in (" + firstTaskId + "," + secondTaskId + ");";
            return FindList<TaskDBModel>(query);
        }

        public List<TaskDBModel> GetTask(int taskId)
        {
            string query = "select * from tasks where taskID = " + taskId + ";";
            return FindList<TaskDBModel>(query);
        }

        public List<StreetTypeDBModel> GetStreetWithDate()
        {
            string query = "select * from streettypes where startdate <= GETDATE() and enddate > GETDATE()";
            return FindList<StreetTypeDBModel>(query);
        }

        public List<RoadTypeDBModel> GetRoadTypeWithDate()
        {
            string query = "select roadtype from roadtypes where startdate <= GETDATE() and enddate > GETDATE()";
            return FindList<RoadTypeDBModel>(query);
        }

        public List<PostCodeOutWardDBModel> GetStreetPostCodeOutWardsByStreetId(int streetId)
        {
            string query = "select * from streetpostcodeoutwards SP inner join postcodeoutwards P on P.postcodeoutwardID = SP.postcodeoutwardID where SP.streetID = " + streetId + ";";
            return FindList<PostCodeOutWardDBModel>(query);
        }

        public List<SectorDBModel> GetSectorByStreetId(int streetId)
        {
            string query = "select * from streetsbysector_v ST inner join sectors S on S.sectorID = ST.sectorID where streetID = " + streetId + "; ";
            return FindList<SectorDBModel>(query);
        }

        public List<PointSegmentDBModel> GetPointSegmentByStreetId(int streetId)
        {
            string query = "select * from streetpointsegments SS inner join pointsegments S on SS.pointsegmentID = S.pointsegmentID where streetID = " + streetId + "; ";
            return FindList<PointSegmentDBModel>(query);
        }

        public List<TaskReAllocationModel> GetTaskReAllocationModels(List<int> taskIds)
        {
            string query = $"select * from taskreallocations where taskid in ({string.Join(',', taskIds)})";
            return FindList<TaskReAllocationModel>(query);
        }
        
        public List<ResolutionCodeModel> GetResolutionCodeModels(int resolutioncodeid)
        {
            string query = $"select * from resolutioncodes where resolutioncodeid={resolutioncodeid}";
            return FindList<ResolutionCodeModel>(query);
        }

        public List<RoundLegInstanceReallocationsModel> GetRoundLegInstanceReallocationsModel(List<string> roundleginstanceids)
        {
            string query = $"select * from roundleginstancereallocations where roundleginstanceid in ({string.Join(',', roundleginstanceids)})";
            return FindList<RoundLegInstanceReallocationsModel>(query);
        }

        public List<TaskLineDBModel> GetTaskLineByTaskId(int taskid)
        {
            string query = "select * from tasklines where taskid= " + taskid + ";";
            return FindList<TaskLineDBModel>(query);
        }

        public List<ResolutionCodeDBModel> GetResolutionCodeById(int id)
        {
            string query = "SELECT * FROM resolutioncodes r WHERE r.resolutioncodeID = " + id + ";";
            return FindList<ResolutionCodeDBModel>(query);
        }

        public List<AgreementLineActionDBModel> GetAgreementLineActionById(int agreementlineid)
        {
            string query = "select * from agreementlineactions a  where agreementlineid=" + agreementlineid + ";";
            return FindList<AgreementLineActionDBModel>(query);
        }
    }

}
