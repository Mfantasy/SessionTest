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
        public static string scanIp = ConfigurationManager.AppSettings["defaultScanIp"];
    }
}
