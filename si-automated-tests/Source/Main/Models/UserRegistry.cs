using System;
using System.IO;
using System.Reflection;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace si_automated_tests.Source.Main.Models
{
    public class UserRegistry
    {
        public static User AutoUser1;
        public static User AutoUser2;
        public static User AutoUser3;
        public static User AutoUser4;
        public static User AutoUser5;
        public static User AutoUser6;
        public static User AutoUser7;
        public static User AutoUser8;
        public static User AutoUser9;
        public static User AutoUser10;
        public static User AutoUser11;
        public static User AutoUser12;
        public static User AutoUser13;
        public static User AutoUser14;
        public static User AutoUser15;
        public static User TestAutoUser;

        public UserRegistry()
        {
            var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source/Main/Data/UserData.json");
            var newPath = new Uri(iconPath).LocalPath;
            JObject jsonObject = JObject.Parse(File.ReadAllText(newPath));

            AutoUser1 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_1"].ToString());
            AutoUser2 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_2"].ToString());
            AutoUser3 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_3"].ToString());
            AutoUser4 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_4"].ToString());
            AutoUser5 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_5"].ToString());
            AutoUser6 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_6"].ToString());
            AutoUser7 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_7"].ToString());
            AutoUser8 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_8"].ToString());
            AutoUser9 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_9"].ToString());
            AutoUser10 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_10"].ToString());
            AutoUser11 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_11"].ToString());
            AutoUser12 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_12"].ToString());
            AutoUser13 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_13"].ToString());
            AutoUser14 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_14"].ToString());
            AutoUser15 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_15"].ToString());
            TestAutoUser = JsonConvert.DeserializeObject<User>(jsonObject["testauto"].ToString());
        }
    }
}
