using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.DBModels
{
    public class ServiceTaskDBModel
    {
        public int PartyId { get => partyId; set => partyId = value; }
        public int AgreementId { get => agreementId; set => agreementId = value; }
        public int AgreementlinetasktypeId { get => agreementlinetasktypeId; set => agreementlinetasktypeId = value; }

        private int partyId;
        private int agreementId;
        private int agreementlinetasktypeId;
        public ServiceTaskDBModel()
        {

        }
    }
}
