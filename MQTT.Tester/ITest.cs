using System;

namespace CoAP.Tester
{
    interface ITest
    {
        String Name { get; }
        String Brief { get; }
        String Description { get; set; }
        Boolean Passed { get; }
        String Summary { get; set; }
        void Run(String serverURI,CoAP.Tester.FormMain.StepOperation stepOperation);
    }

    class FlowTest2 : ITest
    {
        public String Name { get; private set; }

        public String Brief { get; private set; }

        public String Description { get; set; }

        public FlowTest2(String name, String brief, String description)
        {
            Name = name;
            Brief = brief;
            Description = description;
        }

        public String Summary { get; set; }
        public Boolean Passed { get; set; }
        public void Run(String serverURI, CoAP.Tester.FormMain.StepOperation stepOperation) { }
    }
}
