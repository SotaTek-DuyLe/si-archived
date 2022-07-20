using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class ObjectNoticeModel
    {
        public int objectnoteID { get; set; }

        public string title { get; set; }

        public int echotypeID { get; set; }

        public int echoID { get; set; }
    }
}
