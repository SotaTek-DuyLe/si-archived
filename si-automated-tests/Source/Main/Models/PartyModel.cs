using System;
namespace si_automated_tests.Source.Main.Models
{

    public class PartyModel
    {
        private string partyName;
        private string contractName;
        private string partyType;
        private string startDate;
        public string accountNumber { get; set; }
        public string accountRef { get; set; }

        public PartyModel(string partyName, string contractName, string startDate)
        {
            this.PartyName = partyName;
            this.ContractName = contractName;
            this.StartDate = startDate;
            this.PartyType = "Customer";
        }

        public PartyModel(string partyName, string contractName, string accountNumber, string accountRef, string partyType, string startDate)
        {
            this.PartyName = partyName;
            this.ContractName = contractName;
            this.StartDate = startDate;
            this.PartyType = partyType;
            this.accountNumber = accountNumber;
            this.accountRef = accountRef;
        }

        public PartyModel(string partyName, string contractName, string partyType, string startDate)
        {
            this.PartyName = partyName;
            this.ContractName = contractName;
            this.StartDate = startDate;
            this.PartyType = partyType;
        }
        public string PartyName { get => partyName; set => partyName = value; }
        public string ContractName { get => contractName; set => contractName = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string PartyType { get => partyType; set => partyType = value; }
    }
}
