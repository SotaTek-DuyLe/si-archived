using System;
using Newtonsoft.Json;

namespace si_automated_tests.Source.Main.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "username")]
        public string UserName;

        [JsonProperty(PropertyName = "password")]
        public string Password;

        [JsonProperty(PropertyName = "displayname")]
        public string DisplayName;

        [JsonProperty(PropertyName = "data")]
        public string Data;

        [JsonProperty(PropertyName = "role")]
        public string Role;

        [JsonProperty(PropertyName = "home contract")]
        public string HomeContract;

        [JsonProperty(PropertyName = "email")]
        public string Email;

    }
}
