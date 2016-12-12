using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTest
{
    public static class CustomConfig
    {        
        public static string testIp = ConfigurationManager.AppSettings["testIp"];
        public static bool gsOpened = bool.Parse(ConfigurationManager.AppSettings["gsOpen"]);
        public static bool nbOpened = bool.Parse(ConfigurationManager.AppSettings["nbOpen"]);
        public static int nbPort = int.Parse(ConfigurationManager.AppSettings["nbPort"]);
        public static int gsPort = int.Parse(ConfigurationManager.AppSettings["gsPort"]);
        public static string currentIp = testIp;
    }
}
