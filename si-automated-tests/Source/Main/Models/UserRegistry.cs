using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using si_automated_tests.Source.Main.Constants;

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
        public static User AutoUser20;
        public static User AutoUser21;
        public static User AutoUser22;
        public static User AutoUser23;
        public static User AutoUser24;
        public static User AutoUser25;
        public static User AutoUser26;
        public static User AutoUser27;
        public static User AutoUser28;
        public static User AutoUser29;
        public static User AutoUser30;
        public static User AutoUser31;
        public static User AutoUser32;
        public static User AutoUser33;
        public static User AutoUser34;
        public static User AutoUser35;
        public static User AutoUser36;
        public static User AutoUser37;
        public static User AutoUser38;
        public static User AutoUser39;
        public static User AutoUser40;
        public static User AutoUser41;
        public static User AutoUser42;
        public static User AutoUser43;
        public static User AutoUser44;
        public static User AutoUser45;
        public static User AutoUser46;
        public static User AutoUser47;
        public static User AutoUser48;
        public static User AutoUser49;
        public static User AutoUser50;
        public static User AutoUser51;
        public static User AutoUser52;
        public static User AutoUser53;
        public static User AutoUser54;
        public static User AutoUser55;
        public static User AutoUser56;
        public static User AutoUser57;
        public static User AutoUser58;
        public static User AutoUser59;
        public static User AutoUser60;
        public static User AutoUser66;
        public static User AutoUser67;
        public static User AutoUser68;
        public static User AutoUser69;
        public static User AutoUser71;
        public static User AutoUser82;
        public static User TestAutoUser;

        public UserRegistry()
        {
            string iconPath;
            if (WebUrl.MainPageUrl.Contains("https://test.echoweb.co.uk/"))
            {
                iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source/Main/Data/TestUserData.json");
            } else if (WebUrl.MainPageUrl.Contains("https://demo.echoweb.co.uk/"))
            {
                iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source/Main/Data/DemoUserData.json");
            } else
            {
                iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source/Main/Data/TestUserData.json");
            }
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
            AutoUser20 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_20"].ToString());
            AutoUser21 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_21"].ToString());
            AutoUser22 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_22"].ToString());
            AutoUser23 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_23"].ToString());
            AutoUser24 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_24"].ToString());
            AutoUser25 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_25"].ToString());
            AutoUser26 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_26"].ToString());
            AutoUser27 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_27"].ToString());
            AutoUser28 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_28"].ToString());
            AutoUser29 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_29"].ToString());
            AutoUser30 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_30"].ToString());
            AutoUser31 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_31"].ToString());
            AutoUser32 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_32"].ToString());
            AutoUser33 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_33"].ToString());
            AutoUser34 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_34"].ToString());
            AutoUser35 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_35"].ToString());
            AutoUser36 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_36"].ToString());
            AutoUser37 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_37"].ToString());
            AutoUser38 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_38"].ToString());
            AutoUser39 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_39"].ToString());
            AutoUser40 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_40"].ToString());
            AutoUser41 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_41"].ToString());
            AutoUser42 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_42"].ToString());
            AutoUser43 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_43"].ToString());
            AutoUser44 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_44"].ToString());
            AutoUser45 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_45"].ToString());
            AutoUser46 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_46"].ToString());
            AutoUser47 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_47"].ToString());
            AutoUser48 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_48"].ToString());
            AutoUser49 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_49"].ToString());
            AutoUser50 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_50"].ToString());
            AutoUser51 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_51"].ToString());
            AutoUser52 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_52"].ToString());
            AutoUser53 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_53"].ToString());
            AutoUser54 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_54"].ToString());
            AutoUser55 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_55"].ToString());
            AutoUser56 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_56"].ToString());
            AutoUser57 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_57"].ToString());
            AutoUser58 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_58"].ToString());
            AutoUser59 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_59"].ToString());
            AutoUser60 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_60"].ToString());
            AutoUser66 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_66"].ToString());
            AutoUser67 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_67"].ToString());
            AutoUser68 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_68"].ToString());
            AutoUser69 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_69"].ToString());
            AutoUser71 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_71"].ToString());
            AutoUser82 = JsonConvert.DeserializeObject<User>(jsonObject["auto_user_82"].ToString());
            TestAutoUser = JsonConvert.DeserializeObject<User>(jsonObject["testauto"].ToString());
        } 
    }
}
