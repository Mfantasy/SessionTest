using System;
using System.Collections.Generic;
using System.Text;

namespace CoAP.Tester
{
    public class TestCase
    {
        public List<TestCaseBase> tcbList=new List<TestCaseBase>();

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string group;

        public string Group
        {
            get { return group; }
            set { group = value; }
        }
        private string identifier;

        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }
        private string objective;

        public string Objective
        {
            get { return objective; }
            set { objective = value; }
        }
        private string configuration;

        public string Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }
        private string pre_testconditions;

        public string Pre_testconditions
        {
            get { return pre_testconditions; }
            set { pre_testconditions = value; }
        }
        private string testSequence;

        public string TestSequence
        {
            get { return testSequence; }
            set { testSequence = value; }
        }

        public static int Compare(FlowTest ft1, FlowTest ft2)
        {
            return ft1.TC.Group.CompareTo(ft2.TC.Group);
        }
    }
}
