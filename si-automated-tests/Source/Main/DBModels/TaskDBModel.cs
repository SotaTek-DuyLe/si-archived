using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskDBModel
    {
        public int taskID { get; set; }
        public int tasktypeID { get; set; }
        public string task { get; set; }
        public int serviceunitID { get; set; }
        public int servicetaskID { get; set; }
        public int contractID { get; set; }
        public int siteID { get; set; }
        public int partyID { get; set; }
        public int roundinstanceID { get; set; }
        public string taskreference { get; set; }
        public string tasknotes { get; set; }
        public int resolutioncodeID { get; set; }
        public DateTime taskduedate { get; set; }
        public DateTime taskenddate { get; set; }
        public int priorityID { get; set; }
        public int servicetimebandID { get; set; }
        public int taskstateID { get; set; }
        public DateTime taskcreateddate { get; set; }
        public DateTime taskscheduleddate { get; set; }
        public DateTime taskcompleteddate { get; set; }
        public DateTime prebookeddate { get; set; }
        public int taskindicatorID { get; set; }
        public int slotcount { get; set; }
        public int taskorder { get; set; }

        public int taskId { get; set; }
        public bool proximityalert { get; set; }
        public int PartyId { get => partyId; set => partyId = value; }
        public int AgreementId { get => agreementId; set => agreementId = value; }
        public int AgreementlinetasktypeId { get => agreementlinetasktypeId; set => agreementlinetasktypeId = value; }
        public int ServiceTaskId { get => serviceTaskId; set => serviceTaskId = value; }

        private int partyId;
        private int agreementId;
        private int agreementlinetasktypeId;
        private int serviceTaskId;

        public TaskDBModel()
        {
        }
    }
}
