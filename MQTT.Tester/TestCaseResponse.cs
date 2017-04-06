using System;
using System.Collections.Generic;
using System.Text;

namespace CoAP.Tester
{
    class TestCaseResponse : TestCaseBase
    {
        private string code = null;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string sameMID = null;

        public string SameMID
        {
            get { return sameMID; }
            set { sameMID = value; }
        }
                
    }
}
