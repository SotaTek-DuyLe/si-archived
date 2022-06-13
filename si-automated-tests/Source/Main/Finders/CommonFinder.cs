using si_automated_tests.Source.Core;
using si_automated_tests.Source.Main.DBModels;
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

        public List<TaskDBModel> GetTask(int taskId)
        {
            string query = "select * from tasks where tasks.taskID = " + taskId + ";";
            return FindList<TaskDBModel>(query);
        }

        public List<ServiceUnitDBModel> GetServiceUnit(int serviceunitID)
        {
            string query = "select * from serviceunits s2 where s2.serviceunitID = " + serviceunitID + ";";
            return FindList<ServiceUnitDBModel>(query);
        }

        public List<PiorityDBModel> GetPriority(int priorityID)
        {
            string query = "SELECT * FROM test.dbo.priorities p WHERE p.priorityID = " + priorityID + ";";
            return FindList<PiorityDBModel>(query);
        }

        public List<TaskLineDBModel> GetTaskLine(int taskId)
        {
            string query = "SELECT * FROM test.dbo.tasklines WHERE taskid = " + taskId + ";";
            return FindList<TaskLineDBModel>(query);
        }

        public List<TaskLineTypeDBModel> GetTaskLineType(int tasklinetypeId)
        {
            string query = "SELECT * from test.dbo.tasklinetypes where tasklinetypeId = " + tasklinetypeId + ";";
            return FindList<TaskLineTypeDBModel>(query);
        }

        public List<AssetTypeDBModel> GetAssetType(int assettypeId)
        {
            string query = "SELECT * from test.dbo.assettypes a where a.assettypeID =" + assettypeId + ";";
            return FindList<AssetTypeDBModel>(query);
        }

        public List<ProductDBModel> GetProduct(int productId)
        {
            string query = "SELECT * FROM test.dbo.products p  WHERE p.productID  =" + productId + ";";
            return FindList<ProductDBModel>(query);
        }
    }
}
