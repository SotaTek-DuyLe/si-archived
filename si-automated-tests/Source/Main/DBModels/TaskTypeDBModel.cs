using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class TaskTypeDBModel
    {
        public string Tasktype { get; set; }
        public int? Allcompletetasklines_taskstateID { get; set; }
        public int? Allcompletetasklines_rescodeID { get; set; }
        public int? Allfailedtasklines_taskstateID { get; set; }
        public int? Allfailedtasklines_rescodeID { get; set; }
        public int? Allclosedtasklines_taskstateID { get; set; }
        public int? Allclosedtasklines_rescodeID { get; set; }

        public TaskTypeDBModel()
        {
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var tasktypemodel = (TaskTypeDBModel)obj;
            return Tasktype.Equals(tasktypemodel.Tasktype)
                   && Allcompletetasklines_taskstateID == tasktypemodel.Allcompletetasklines_taskstateID
                   && Allcompletetasklines_rescodeID ==tasktypemodel.Allcompletetasklines_rescodeID
                   && Allfailedtasklines_taskstateID == tasktypemodel.Allfailedtasklines_taskstateID
                   && Allfailedtasklines_rescodeID == tasktypemodel.Allfailedtasklines_rescodeID
                   && Allclosedtasklines_taskstateID == tasktypemodel.Allclosedtasklines_taskstateID
                   && Allclosedtasklines_rescodeID == tasktypemodel.Allclosedtasklines_rescodeID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Tasktype, Allcompletetasklines_taskstateID, Allcompletetasklines_rescodeID, Allfailedtasklines_taskstateID, Allfailedtasklines_rescodeID, Allclosedtasklines_taskstateID, Allclosedtasklines_rescodeID);
        }
    }
}
