using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Agreement
{
    public class AgreementTaskModel
    {
        private string taskState;
        private string taskType;
        private string description;
        private string dueDate;
        private string completedDate;
        public AgreementTaskModel(string state, string type, string _description, string _dueDate, string _completedDate)
        {
            this.taskState = state;
            this.taskType = type;
            this.description = _description;
            this.dueDate = _dueDate;
            this.completedDate = _completedDate;
        }
        public string TaskState { get => taskState; set => taskState = value; }
        public string TaskType { get => taskType; set => taskType = value; }
        public string Description { get => description; set => description = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }
        public string CompletedDate { get => completedDate; set => completedDate = value; }
    }
}
