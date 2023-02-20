using System;
using System.Collections.Generic;
using System.Text;

namespace si_automated_tests.Source.Core
{
    public class TextTranslation
    {
        public static string ToString(string str, string culture)
        {
            switch (culture)
            {
                case "French":
                    switch (str)
                    {
                        case "Resources":
                            return "Ressources";
                        case "Daily Allocation":
                            return "Allocation quotidienne";
                        case "Default Allocation":
                            return "Allocation par défaut";
                        default:
                            break;
                    }
                    break;
                default:
                    //English is default localization
                    break;
            }
            return str;
        }
    }
}
