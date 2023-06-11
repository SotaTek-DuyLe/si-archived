using System;
namespace si_automated_tests.Source.Main.DBModels.GetTaskDebrief
{
    public class GetTaskDebriefDBModel
    {
        public int debrieftestID { get; set; }
        public string debrieftest { get; set; }
        public string resolutioncode { get; set; }
        public string notes { get; set; }
        public bool resolved { get; set; }
        public GetTaskDebriefDBModel()
        {
        }
    }
}
