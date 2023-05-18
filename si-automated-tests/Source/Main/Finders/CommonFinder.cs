using NUnit.Allure.Attributes;
using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
using si_automated_tests.Source.Main.Models;
using System;
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
            string query = "SELECT * FROM partyactions WHERE partyID = " + partyID + " and actioncreatedbyuserID = " + actioncreatedbyuserID + ";";
            return FindList<PartyActionDBModel>(query);
        }

        public List<TaskLineDBModel> GetTaskLineStateByTaskLineId(string tasklineId)
        {
            string query = "SELECT t2.tasklinestate  FROM tasklines t join tasklinestates t2 on t.tasklinestateID = t2.tasklinestateID WHERE t.tasklineID = " + tasklineId + ";";
            return FindList<TaskLineDBModel>(query);
        }

        public List<TaskStateDBModel> GetTaskStateByTaskId(string taskId)
        {
            string query = "SELECT t2.taskstate  FROM tasks t join taskstates t2 on t.taskstateID = t2.taskstateID  WHERE t.taskID = " + taskId + "; ";
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

        public List<UserDBModel> GetUserActive()
        {
            string query = "select * from users where enddate > getdate () order by displayname asc;";
            return FindList<UserDBModel>(query);
        }

        public List<string> GetUserInActive()
        {
            string query = "select * from users where enddate < getdate () and userID != 40;";
            return FindList<UserDBModel>(query).Select(p => p.displayname).ToList();
        }

        public List<ContractUnitDBModel> GetContractUnitByContractId(string contractId)
        {
            string query = "select * from contractunits c where c.contractID = " + contractId + "and c.enddate > getdate() order by contractunit asc;";
            return FindList<ContractUnitDBModel>(query);
        }

        public List<ContractUnitDBModel> GetContractUnits()
        {
            string query = "select * from contractunits;";
            return FindList<ContractUnitDBModel>(query);
        }

        public List<string> GetContractUnitUserListVByContractUnit(string contractUnitId)
        {
            string query = "select * from contractunitusers_list_v v join contractunits c on v.contractunitID = c.contractunitID WHERE c.contractunitID = " + contractUnitId + " and v.enddate > getdate();";
            return FindList<ContractUnitUserListVDBModel>(query).Select(x => x.displayname).ToList();
        }

        public List<string> GetUserWithFunction()
        {
            string query = "Declare @curday datetime set @curday = dbo.GetNowDate(1) set @curday = dateadd(d, datediff(d, 0, @curday), 0)IF 1 > 0 SELECT DISTINCT T0.userID as UserID,T0.displayname as UserName from users as T0 with(nolock) inner join usersroles as T1 with(nolock) on(T0.userID = T1.userID) inner join roles as T2 with(nolock) on(T1.roleID = T2.roleID) inner join userprivileges as T3 with(nolock) on(T2.roleID = T3.roleID) WHERE(T3.objectID = 1  and T3.echotypeID = 10) AND(T3.canread = 1 and T3.canupdate = 1) AND @curday between isnull(T0.startdate,'1 Jan 2000') and isnull(T0.enddate,'1 Jan 3000') order by T0.displayname";
            return FindList<UserDBModel>(query).Select(x => x.UserName).ToList();
        }

        [AllureStep]
        public List<SaleCreditLineDBModel> GetSaleCreditLineDBs()
        {
            string query = "select * from salescreditlines where partyID = 68 and salescreditID is NULL";
            return FindList<SaleCreditLineDBModel>(query).ToList();
        }
        [AllureStep]
        public List<TaskTypeDBModel> GetTaskTypes()
        {
            string query = "select * from tasktypes";
            return FindList<TaskTypeDBModel>(query).ToList();
        }
        [AllureStep]
        public SiteDBModel GetSitesById(string siteId)
        {
            string query = "select * from sites where siteID =" + siteId + ";";
            return FindList<SiteDBModel>(query).FirstOrDefault();
        }
        [AllureStep]
        public List<TaskTypeDBModel> GetTaskTypeByName(string tasktype)
        {
            string query = "select * from tasktypes where tasktype='" + tasktype + "'";
            return FindList<TaskTypeDBModel>(query).ToList();
        }
        [AllureStep]
        public List<WBSiteProduct> GetWBSiteProductsBySiteId(string siteId)
        {
            string query = "select * from wb_siteproducts WHERE siteID = " + siteId + ";";
            return FindList<WBSiteProduct>(query).ToList();
        }
        [AllureStep]
        public List<ProductDBModel> GetProductByListId(List<int> productIds)
        {
            string idQuery = "";
            for(int i = 0; i < productIds.Count; i++)
            {
                if(i == (productIds.Count - 1))
                {
                    idQuery += productIds[i].ToString();
                } else
                {
                    idQuery += productIds[i].ToString() + ",";
                }
            }
            Console.WriteLine("select * from products where productid in (" + idQuery + ");");
            string query = "select * from products where productid in (" + idQuery + ");";
            return FindList<ProductDBModel>(query).ToList();
        }

        [AllureStep]
        public List<GreyListCodeDBModel> GetGreyList()
        {
            string query = "select * from wb_greylistcodes;";
            return FindList<GreyListCodeDBModel>(query).ToList();
        }

        [AllureStep]
        public List<WBGreylistDBModel> GetGreyListById(string greylistID)
        {
            string query = "select * from wb_greylist where greylistid=" + greylistID + ";";
            return FindList<WBGreylistDBModel>(query).ToList();
        }

        [AllureStep]
        public List<WBPartySettingDBModel> GetWBPartySettingByPartyId(string partyId)
        {
            string query = "select licencenumber, licencenumberexpiry, * from wb_partysettings where partyid=" + partyId + ";";
            return FindList<WBPartySettingDBModel>(query).ToList();
        }

        [AllureStep]
        public List<PartiesDBModel> GetPartiesByPartyId(string partyId)
        {
            string query = "select * from parties where partyid=" + partyId + ";";
            return FindList<PartiesDBModel>(query).ToList();
        }

        [AllureStep]
        public List<WBPartySettingVDBModel> GetWBPartiesSettingsVByPartyId(string partyId)
        {
            string query = "select * from wb_partysettings_v where partyid=" + partyId + ";";
            return FindList<WBPartySettingVDBModel>(query).ToList();
        }

        [AllureStep]
        public WBTicketDBModel GetWBTicketByTicketId(string ticketId)
        {
            string query = "select driver,* from wb_tickets where ticketid=" + ticketId + ";";
            return FindList<WBTicketDBModel>(query).ToList()[0];
        }

        [AllureStep]
        public List<PurchaseOderListVDBModel> GetPurchaseOrderListVByPartyId(string partyId)
        {
            string query = "select * from purchaseorders_list_v where partyid=" + partyId + ";";
            return FindList<PurchaseOderListVDBModel>(query).ToList();
        }

        [AllureStep]
        public InvoiceScheduleModel GetInvoiceSchedule(string id)
        {
            string query = $"select * from invoiceschedules where invoicescheduleID ={id};";
            return FindList<InvoiceScheduleModel>(query).FirstOrDefault();
        }

        [AllureStep]
        public List<ScheduleDateModel> GetScheduleDateModel(int id)
        {
            string query = $"select * from scheduledates where scheduleID={id};";
            return FindList<ScheduleDateModel>(query);
        }

        [AllureStep]

        public List<ResourceShiftScheduleModel> GetResourceShiftSchedules(string resourceshiftscheduleID)
        {
            string query = $"select * from resourceshiftschedules where resourceshiftscheduleID = {resourceshiftscheduleID};";
            return FindList<ResourceShiftScheduleModel>(query);
        }

        [AllureStep]
        public List<AgreementLineAssetProductDBModel> GetAgreementLineAssetProductByAgreementLineId(string agreementLineId)
        {
            string query = "select productcodeID, * from agreementlineassetproducts where agreementlineID=" + agreementLineId + ";";
            return FindList<AgreementLineAssetProductDBModel>(query);
        }

        [AllureStep]
        public List<ResourceAllocationModel> GetResourceAllocation(int id)
        {
            string query = "select * from resourceallocations where resourceID=" + id.ToString() +" order by enddate desc";
            return FindList<ResourceAllocationModel>(query);
        }

        [AllureStep]
        public TaskDBModel GetProximityAlert(int taskId)
        {
            string query = $"select proximityalert from tasks where taskID={taskId};";
            return FindList<TaskDBModel>(query).FirstOrDefault();
        }

        [AllureStep]
        public List<BusinessUnitGroupDBModel> GetBusinessUnitGroupByContractId(int contractId)
        {
            string query = "select * from businessunitgroups WHERE contractID = " + contractId + ";";
            return FindList<BusinessUnitGroupDBModel>(query);
        }

        [AllureStep]
        public List<COSTAGREEMENTSDBModel> GetCostAgreementByPartyId(string partyId, string costAgreementId)
        {
            string query = "select approveddatetime, approveduserID,* FROM COSTAGREEMENTS where partyid=" + partyId + "and costagreementID = " + costAgreementId + ";";
            return FindList<COSTAGREEMENTSDBModel>(query);
        }

        [AllureStep]
        public List<ServiceAssetTypeDBModel> GetServiceAssetTypes(string serviceUnitID)
        {
            string query = @"select serviceunitID,assettype
                            from serviceassettypes SAT
                            inner
                            join assettypes ATT on ATT.assettypeID = SAT.assettypeID
                            inner
                            join serviceunits SU on SU.serviceID = SAT.serviceID
                            where serviceunitID = " + serviceUnitID + "; ";
            return FindList<ServiceAssetTypeDBModel>(query);
        }
    }
}
