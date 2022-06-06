using System;
using NUnit.Framework;
using si_automated_tests.Source.Core;

namespace si_automated_tests.Source.Main.Constants
{
    public class WebUrl
    {
        public static string MainPageUrl;
        public static string MainPageUrlIE;
        public static string InspectionTypeUrlIE;

        public WebUrl()
        {
            string url;
            try
            {
                url = TestContext.Parameters.Get("url");

            }
            catch (NullReferenceException)
            {
                Logger.Get().Info("Url not specified, using default url");
               url = "https://test.echoweb.co.uk/";
                //url = "https://demo.echoweb.co.uk/";
            }
            if (url == null)
            {
                Logger.Get().Info("Url not specified, using default url");
               url = "https://test.echoweb.co.uk/";
                //url = "https://demo.echoweb.co.uk/";
            }
            MainPageUrl = url;
            MainPageUrlIE = url + "echo2/echo2extra/mainframe.aspx";
            InspectionTypeUrlIE = url + "Echo2/Echo2Extra/PopupDefault.aspx?RefTypeName=none&TypeName=InspectionType&ObjectID=2#";
        }
        public static string GetDBName()
        {
            return MainPageUrl.Replace("https://", "").Replace(".echoweb.co.uk/", "");
        }
    }
}
