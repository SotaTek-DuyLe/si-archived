﻿using System;
namespace si_automated_tests.Source.Main.DBModels
{
    public class PartiesDBModel
    {
        public Decimal creditlimit { get; set; }
        public int partyID { get; set; }
        public int correspondence_siteID { get; set; }

        public PartiesDBModel()
        {
        }
    }
}
