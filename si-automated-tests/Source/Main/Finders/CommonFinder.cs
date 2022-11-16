using NUnit.Allure.Attributes;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using System.Collections.Generic;
using System.Linq;
using PointAddressModel = si_automated_tests.Source.Main.DBModels.PointAddressModel;

namespace si_automated_tests.Source.Main.Finders
{
    public class CommonFinder : ServiceProvider
    {
        public CommonFinder(DatabaseContext context) : base(context)
        {

        }
        [AllureStep]
        public List<SiteDBModel> GetSitesByPartyId(int partyId)
        {
            string query = "select * from sites where partyID=" + partyId;
            return FindList<SiteDBModel>(query);
        }
        [AllureStep]
        public List<ServiceDBModel> GetServiceTypes()
        {
            string query = "select * from servicetypes;";
            return FindList<ServiceDBModel>(query);
        }
        [AllureStep]
        public List<ServiceJoinServiceGroupDBModel> GetServiceAndServiceGroupInfo(int serviceId)
        {
            string query = "SELECT * FROM services s join servicegroups s2 on s.servicegroupID = s2.servicegroupID  WHERE s.serviceID = " + serviceId;
            return FindList<ServiceJoinServiceGroupDBModel>(query);
        }
        [AllureStep]
        public List<EventDBModel> GetEvent(int eventId)
        {
            string query = "select* from events where eventid = " + eventId + ";";
            return FindList<EventDBModel>(query);
        }
        [AllureStep]
        public List<PointAddressModel> GetPointAddress(string id)
        {
            string query = "select * from pointaddresses where pointaddressID="+id+";";
            return FindList<PointAddressModel>(query);
        }
        [AllureStep]
        public List<ServiceUnitPointDBModel> GetServiceUnitPoint(int serviceunitpointid)
        {
            string query = "select * from serviceunitpoints where serviceunitpointid=" + serviceunitpointid + ";";
            return FindList<ServiceUnitPointDBModel>(query);
        }
        [AllureStep]
        public List<PointTypeDBModel> GetPointType(int pointtypeId)
        {
            string query = "SELECT * FROM pointtypes p WHERE p.pointtypeID =" + pointtypeId + ";";
            return FindList<PointTypeDBModel>(query);
        }
        [AllureStep]
        public List<ServiceUnitDBModel> GetServiceUnit(int serviceunitid)
        {
            string query = "select * from serviceunits where serviceunitid=" + serviceunitid + ";";
            return FindList<ServiceUnitDBModel>(query);
        }
        [AllureStep]
        public List<ServiceUnitTypeDBModel> GetServiceUnitType(int serviceunittypeID)
        {
            string query = "SELECT * FROM serviceunittypes s WHERE s.serviceunittypeID  = " + serviceunittypeID + ";";
            return FindList<ServiceUnitTypeDBModel>(query);
        }
        [AllureStep]
        public List<PointSegmentDBModel> GetPointSegment(int pointsegmentID)
        {
            string query = "SELECT * FROM pointsegments p WHERE p.pointsegmentID = " + pointsegmentID + ";";
            return FindList<PointSegmentDBModel>(query);
        }
        [AllureStep]
        public List<StreetDBModel> GetStreet(int streetID)
        {
            string query = "SELECT * FROM streets s WHERE s.streetID = " + streetID + ";";
            return FindList<StreetDBModel>(query);
        }
        [AllureStep]
        public List<ServiceUnitPointDBModel> GetServiceUnitPointWithNoLock(int serviceunitID)
        {
            string query = "SELECT T0.* FROM [serviceunitpoints] AS T0 WITH (NOLOCK)  WHERE T0.[serviceunitID] = " + serviceunitID + ";";
            return FindList<ServiceUnitPointDBModel>(query);
        }
        [AllureStep]
        public bool IsObjectNoticeExist(string echoTypeId, string id)
        {
            string query = "select * from objectnotices where echotypeID=" + echoTypeId + " and objectnoticeid=" + id;
            return FindList<ObjectNoticeModel>(query).Count != 0;
        }
        [AllureStep]
        public List<InspectionDBModel> GetInspectionById(int inspectionId)
        {
            string query = "select * from inspections where inspectionID=" + inspectionId + ";";
            return FindList<InspectionDBModel>(query);
        }
        [AllureStep]
        public List<PointAreaDBModel> GetPointArea(int pointAreaId)
        {
            string query = "SELECT * FROM pointareas p WHERE p.pointareaID = " + pointAreaId + ";";
            return FindList<PointAreaDBModel>(query);
        }
        [AllureStep]
        public List<PointNodeDBModel> GetPointNode(int pointNodeId)
        {
            string query = "SELECT * FROM pointnodes p WHERE p.pointnodeID = " + pointNodeId + ";";
            return FindList<PointNodeDBModel>(query);
        }
        [AllureStep]
        public ServiceUnitModel GetServiceUnitById(int serviceId)
        {
            return FindList<ServiceUnitModel>("select * from serviceunits where serviceunitID=" + serviceId + ";").FirstOrDefault();
        }
        [AllureStep]
        public List<TaskDBModel> GetMultipleTask(int firstTaskId, int secondTaskId)
        {
            string query = "select * from tasks where taskID in (" + firstTaskId + "," + secondTaskId + ");";
            return FindList<TaskDBModel>(query);
        }
        [AllureStep]
        public List<TaskDBModel> GetTask(int taskId)
        {
            string query = "select * from tasks where taskID = " + taskId + ";";
            return FindList<TaskDBModel>(query);
        }
        [AllureStep]
        public List<ServiceTaskDBModel> GetTaskService(int serviceTaskId)
        {
            string query = "select * from servicetasks where servicetaskID = " + serviceTaskId + ";";
            return FindList<ServiceTaskDBModel>(query);
        }
        [AllureStep]
        public List<StreetTypeDBModel> GetStreetWithDate()
        {
            string query = "select * from streettypes where startdate <= GETDATE() and enddate > GETDATE()";
            return FindList<StreetTypeDBModel>(query);
        }
        [AllureStep]
        public List<RoadTypeDBModel> GetRoadTypeWithDate()
        {
            string query = "select roadtype from roadtypes where startdate <= GETDATE() and enddate > GETDATE()";
            return FindList<RoadTypeDBModel>(query);
        }
        [AllureStep]
        public List<PostCodeOutWardDBModel> GetStreetPostCodeOutWardsByStreetId(int streetId)
        {
            string query = "select * from streetpostcodeoutwards SP inner join postcodeoutwards P on P.postcodeoutwardID = SP.postcodeoutwardID where SP.streetID = " + streetId + ";";
            return FindList<PostCodeOutWardDBModel>(query);
        }
        [AllureStep]
        public List<SectorDBModel> GetSectorByStreetId(int streetId)
        {
            string query = "select * from streetsbysector_v ST inner join sectors S on S.sectorID = ST.sectorID where streetID = " + streetId + "; ";
            return FindList<SectorDBModel>(query);
        }
        [AllureStep]
        public List<PointSegmentDBModel> GetPointSegmentByStreetId(int streetId)
        {
            string query = "select * from streetpointsegments SS inner join pointsegments S on SS.pointsegmentID = S.pointsegmentID where streetID = " + streetId + "; ";
            return FindList<PointSegmentDBModel>(query);
        }
        [AllureStep]
        public List<TaskReAllocationModel> GetTaskReAllocationModels(List<int> taskIds)
        {
            string query = $"select * from taskreallocations where taskid in ({string.Join(',', taskIds)})";
            return FindList<TaskReAllocationModel>(query);
        }
        [AllureStep]
        public List<ResolutionCodeModel> GetResolutionCodeModels(int resolutioncodeid)
        {
            string query = $"select * from resolutioncodes where resolutioncodeid={resolutioncodeid}";
            return FindList<ResolutionCodeModel>(query);
        }
        [AllureStep]
        public List<RoundLegInstanceReallocationsModel> GetRoundLegInstanceReallocationsModel(List<string> roundleginstanceids)
        {
            string query = $"select * from roundleginstancereallocations where roundleginstanceid in ({string.Join(',', roundleginstanceids)})";
            return FindList<RoundLegInstanceReallocationsModel>(query);
        }
        [AllureStep]
        public List<TaskLineDBModel> GetTaskLineByTaskId(int taskid)
        {
            string query = "select * from tasklines where taskid= " + taskid + ";";
            return FindList<TaskLineDBModel>(query);
        }
        [AllureStep]
        public List<ResolutionCodeDBModel> GetResolutionCodeById(int id)
        {
            string query = "SELECT * FROM resolutioncodes r WHERE r.resolutioncodeID = " + id + ";";
            return FindList<ResolutionCodeDBModel>(query);
        }
        [AllureStep]
        public List<AgreementLineActionDBModel> GetAgreementLineActionById(int agreementlineid)
        {
            string query = "select * from agreementlineactions a  where agreementlineid=" + agreementlineid + ";";
            return FindList<AgreementLineActionDBModel>(query);
        }
        [AllureStep]
        public List<ResourceClassModel> GetResourceClasses()
        {
            return FindList<ResourceClassModel>("select * from resourceclasses");
        }
        [AllureStep]
        public List<ResourceStateModel> GetResourceStates(int resourceclassID)
        {
            string query = "select * from resourcestates where resourceclassID=" + resourceclassID + ";";
            return FindList<ResourceStateModel>(query);
        }
        [AllureStep]
        public List<PartyActionDBModel> GetPartyActionByPartyIdAndUserId(string partyID, string actioncreatedbyuserID)
        {
            string query = "SELECT * FROM SotatekTesting.dbo.partyactions WHERE partyID = " + partyID + " and actioncreatedbyuserID = " + actioncreatedbyuserID + ";";
            return FindList<PartyActionDBModel>(query);
        }

        public List<TaskLineDBModel> GetTaskLineStateByTaskLineId(string tasklineId)
        {
            string query = "SELECT t2.tasklinestate  FROM SotatekTesting.dbo.tasklines t join SotatekTesting.dbo.tasklinestates t2 on t.tasklinestateID = t2.tasklinestateID WHERE t.tasklineID = " + tasklineId + ";";
            return FindList<TaskLineDBModel>(query);
        }

        public List<TaskStateDBModel> GetTaskStateByTaskId(string taskId)
        {
            string query = "SELECT t2.taskstate  FROM SotatekTesting.dbo.tasks t join SotatekTesting.dbo.taskstates t2 on t.taskstateID = t2.taskstateID  WHERE t.taskID = " + taskId + "; ";
            return FindList<TaskStateDBModel>(query);
        }

        public List<SubscriptionDBModel> GetSubscriptionById(string subscriptionId)
        {
            string query = "select * from subscriptions where subscriptionid=" + subscriptionId + ";";
            return FindList<SubscriptionDBModel>(query);
        }

        public List<ContractDBModel> GetContractById(string contractId)
        {
            string query = "select * from contacts where contactid=" + contractId + ";";
            return FindList<ContractDBModel>(query);
        }
        
        public List<ServiceTaskScheduleDBModel> GetServiceTaskScheduleBySTSID(string servicetaskscheduleID)
        {
            string query = "select * from servicetaskschedules where servicetaskscheduleID=" + servicetaskscheduleID + ";";
            return FindList<ServiceTaskScheduleDBModel>(query);
        }
    }

}
