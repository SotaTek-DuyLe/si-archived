using System;
namespace si_automated_tests.Source.Main.Models
{
    public class LocationModel
    {
        private string id;
        private string location;
        private string active;
        private string client;

        public LocationModel(string id, string location, string active, string client)
        {
            this.Id = id;
            this.Location = location;
            this.Active = active;
            this.Client = client;
        }

        public string Id { get => id; set => id = value; }
        public string Location { get => location; set => location = value; }
        public string Active { get => active; set => active = value; }
        public string Client { get => client; set => client = value; }
    }
}
