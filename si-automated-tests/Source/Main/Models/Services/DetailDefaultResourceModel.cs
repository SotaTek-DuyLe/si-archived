using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Main.Models.Services
{
    public class DetailDefaultResourceModel
    {
        public string Resource { get; set; }
        public bool HasSchedule { get; set; }
        public string Schedule { get; set; }

        public override bool Equals(object obj)
        {
            return this.Resource == (obj as DetailDefaultResourceModel).Resource &&
                this.HasSchedule == (obj as DetailDefaultResourceModel).HasSchedule &&
                this.Schedule == (obj as DetailDefaultResourceModel).Schedule;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
