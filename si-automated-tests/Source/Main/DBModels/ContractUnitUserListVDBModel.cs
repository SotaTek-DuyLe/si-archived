using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class ContractUnitUserListVDBModel
    {
        public int contractunituserID { get; set; }
        public int contractunitID { get; set; }
        public string contractunit { get; set; }
        public string displayname { get; set; }

        public ContractUnitUserListVDBModel()
        {
        }
    }
}
