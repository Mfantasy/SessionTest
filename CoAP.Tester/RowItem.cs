using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CoAP.Tester
{
    public class RowItem
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private bool isPassed;

        public bool IsPassed
        {
            get { return isPassed; }
            set { isPassed = value; }
        }

        private Image icon;

        public Image Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        
        private string expected;

        public string Expected
        {
            get { return expected; }
            set { expected = value; }
        }
        private string received;

        public string Received
        {
            get { return received; }
            set { received = value; }
        }
        private string errorInfo;

        public string ErrorInfo
        {
            get { return errorInfo; }
            set { errorInfo = value; }
        }

        private RowItem parent;

        public RowItem Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        
    }

    public class StepItem
    {
        private bool isPassed;

        public bool IsPassed
        {
            get { return isPassed; }
            set { isPassed = value; }
        }

        private bool isRequest;

        public bool IsRequest
        {
            get { return isRequest; }
            set { isRequest = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
        private List<RowItem> rowItemList;

        public List<RowItem> RowItemList
        {
            get { return rowItemList; }
            set { rowItemList = value; }
        }

    }
    
    public class TestCaseItem
    {
        public CoAP.Tester.FormMain.StepOperation stepOperation;
        private bool isPassed;

        public bool IsPassed
        {
            get { return isPassed; }
            set { isPassed = value; }
        }
        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private String group;

        public String Group
        {
            get { return group; }
            set { group = value; }
        }
        private String testCaseName;

        public String TestCaseName
        {
            get { return testCaseName; }
            set { testCaseName = value; }
        }

        private List<StepItem> stepItemList;

        public List<StepItem> StepItemList
        {
            get { return stepItemList; }
            set {                     
                    stepItemList = value;
                }
        }

        public void RunDelegate(String lStepText, int value)
        {
            stepOperation(lStepText,value);
        }

    }

}
