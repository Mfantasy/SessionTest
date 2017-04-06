using System;
using System.Collections.Generic;
using System.Text;

namespace CoAP.Tester
{
    class TestCaseRequest:TestCaseBase
    {
        private string method = null;

        public string Method
        {
            get { return method; }
            set { method = value; }
        }        
        
    }
}
