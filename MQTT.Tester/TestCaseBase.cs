using System;
using System.Collections.Generic;
using System.Text;

namespace CoAP.Tester
{
    public class TestCaseBase
    {
        public TestCaseBase()
        {
            optionType = new OptionTypes();
        }
        private OptionTypes optionType;

        internal OptionTypes OptionType
        {
            get { return optionType; }
            set { optionType = value; }
        }


        private bool isRequest;

        public bool IsRequest
        {
            get { return isRequest; }
            set { isRequest = value; }
        }

        private string messageType=null;

        public string MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        private string payLoad = null;

        public string PayLoad
        {
            get { return payLoad; }
            set { payLoad = value; }
        }
        private string token = null;

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

    }
}
