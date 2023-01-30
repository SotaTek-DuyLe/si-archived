using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class WBPartySettingDBModel
    {
        public string licencenumber { get; set; }
        public DateTime licencenumberexpiry { get; set; }
        public int creditlimitwarning { get; set; }
        public int partysettingID { get; set; }

        public WBPartySettingDBModel()
        {
        }
    }
}
