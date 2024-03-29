﻿using System;
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

        [JsonProperty(PropertyName = "location name")]
        public string LocationName;

        [JsonProperty(PropertyName = "address name")]
        public string AddressName;

        [JsonProperty(PropertyName = "event id with icon")]
        public string EventIDWithIcon;

        [JsonProperty(PropertyName = "event id without icon")]
        public string EventIDWithoutIcon;
    }
}
