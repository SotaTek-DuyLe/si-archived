using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class PartyActionDBModel
    {
        public int partyactionID { get; set; }
        public int partyD { get; set; }
        public int actioncreatedbyuserID { get; set; }
        public bool wb_autoprint { get; set; }
        public bool wb_driverrequired { get; set; }
        public bool wb_usestoredpo { get; set; }
        public bool wb_usemanualpo { get; set; }
        public bool wb_externalroundrequired { get; set; }
        public bool wb_usestoredround { get; set; }
        public bool wb_usemanualround { get; set; }
        public bool wb_allowmanualname { get; set; }
        public bool wb_authorizetipping { get; set; }
        public string wb_licencenumber { get; set; }
        public DateTime wb_licencenumberexpiry { get; set; }
        public DateTime wb_dormantdate { get; set; }
        public string wb_creditlimitwarning { get; set; }
        public bool wb_restrictproducts { get; set; }

        public PartyActionDBModel()
        {
        }
    }
}
