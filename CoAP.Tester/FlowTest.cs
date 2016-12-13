using System;
using System.Collections.Generic;
using System.Text;

namespace CoAP.Tester
{   
    public class FlowTest : ITest
    {
        //private TestCase tc;
        private List<IStep> _steps = new List<IStep>();
        private StringBuilder _summaries;
        private String _summary;
        private TestCaseItem tcItem;

        public TestCaseItem TCItem
        {
            get { return tcItem; }
            set { tcItem = value; }
        }

        private List<StepItem> _stepItemList;

        public List<StepItem> StepItemList
        {
            get { return _stepItemList; }
            set { _stepItemList = value; }
        }

        public TestCase TC { get; set;}

        public String Name { get; set; }

        public String Brief { get; private set; }

        public String Description { get; set; }

        public String Summary
        {
            get
            {
                if (_summary == null)
                    _summary = _summaries == null ? null : _summaries.ToString();
                return _summary;
            }
            set { _summary = value; }
        }

        public Boolean Passed { get; private set; }

        public IEnumerable<IStep> Steps
        {
            get { return _steps; }
        }

        public FlowTest(String name, String brief)
            : this(name, brief, brief)
        { }

        public FlowTest(String name, String brief, String description)
        {
            Name = name;
            Brief = brief;
            Description = description;
        }

        public FlowTest AddStep(IStep step)
        {
            _steps.Add(step);
            return this;
        }

        public void Run(String serverURI, CoAP.Tester.FormMain.StepOperation stepOperation)
        {
            Passed = true;
            _summaries = new StringBuilder();
            _summary = null;

            tcItem = new TestCaseItem();
            tcItem.Name = TC.Name;
            tcItem.Group = TC.Group;
            tcItem.TestCaseName = TC.Name;
            tcItem.StepItemList = new List<StepItem>();
            tcItem.stepOperation = stepOperation;
            TestContext context = new TestContext();
            context.Verbose.AppendLine("   TESTER     \t          \tDUT");

            Int32 stepIndex = 1;

            int stepCount = _steps.Count;
            int executedStepCount = 0;
            tcItem.stepOperation("", 0);
            foreach (IStep step in _steps)
            {
                executedStepCount++;
                int value=(executedStepCount*100)/stepCount;
                context.Verbose.Append(stepIndex++).Append(". ");
                //_summaries.Append(stepIndex++).Append(". ")
                //    .Append(step.GetType().Name).Append("\t");
                if (step.Go(context, serverURI,tcItem,value))
                {
                    //context.Verbose.AppendLine("\tPASSED");
                    context.Verbose.AppendLine();
                    _summaries.AppendLine("PASSED");
                    
                }
                else
                {
                    //context.Verbose.AppendLine("\tFAILED");
                    context.Verbose.AppendLine();
                    Passed = false;
                    _summaries.AppendLine("FAILED");
                    foreach (var item in step.Summaries)
                    {
                        _summaries.Append("\t- ").AppendLine(item);
                        context.Verbose.Append("\t- ").AppendLine(item);
                    }
                }
                tcItem.IsPassed = Passed;
                //_stepItemList.Add(tcItem);
                _summary = null;
            }
            context.Verbose.AppendLine().AppendLine(Passed ? "   PASSED" : "   FAILED");
            _summary = context.Verbose.ToString();
        }

        class TestContext : ITestContext
        {
            private BlockingQueue<Request> _requestQueue = new BlockingQueue<Request>();
            private BlockingQueue<Response> _responseQueue = new BlockingQueue<Response>();
            private StringBuilder _verbose = new StringBuilder();

            public BlockingQueue<Request> RequestQueue
            {
                get { return _requestQueue; }
            }

            public BlockingQueue<Response> ResponseQueue
            {
                get { return _responseQueue; }
            }

            public StringBuilder Verbose
            {
                get { return _verbose; }
            }
        }
    }

    class Steps
    {
        public static RequestBuildStep BuildRequest(IRequestBuilder builder)
        {
            return new RequestBuildStep() { RequestBuilder = builder };
        }

        public static ResponseCheckStep CheckResponse(ICheck check)
        {
            return new ResponseCheckStep() { ResponseCheck = check };
        }
    }

    public interface ITestContext
    {
        BlockingQueue<Request> RequestQueue { get; }
        BlockingQueue<Response> ResponseQueue { get; }
        StringBuilder Verbose { get; }
    }

    public interface IStep
    {
        //String name{get;set;}
        IEnumerable<String> Summaries { get; }
        Boolean Go(ITestContext context, String serverURI,TestCaseItem rowItem,int value);
    }

    public class RequestBuildStep : IStep
    {
        private static readonly String[] Empty = new String[0];

        public IRequestBuilder RequestBuilder { get; set; }

        public IEnumerable<String> Summaries
        {
            get { return Empty; }
        }

        public Boolean Go(ITestContext context, String serverURI, TestCaseItem tcItem,int value)
        {
            Request request = Request.NewGet();

            //Uri uri = new Uri(new Uri(serverURI), flowTest.ResourceURI);
            Uri uri = new Uri(serverURI);
            request.URI = uri;

            if (RequestBuilder != null)
                request = RequestBuilder.Build(request);

            request.Respond += delegate(object sender, ResponseEventArgs e)
            {
                context.ResponseQueue.Offer(e.Response);
            };
            
            context.RequestQueue.Offer(request);
            context.Verbose.AppendFormat("{0}({1})\t--------->\t            ", request.Type, request.CodeString);

            StepItem stepItem = new StepItem();
            stepItem.IsRequest = true;
            stepItem.Name = request.Method.ToString() + " Request " ;
            stepItem.IsPassed = true;
            stepItem.RowItemList = new List<RowItem>();
            tcItem.StepItemList.Add(stepItem);

            tcItem.stepOperation(request.Method.ToString() + " Request",value);

            request.Send();
            return true;
        }

        public override String ToString()
        {
            String req = "";
            if (RequestBuilder is GroupBuilder)
            {
                foreach (IRequestBuilder rb in RequestBuilder as GroupBuilder)
                {
                    if (rb is MethodBuilder)
                    {
                        req = (rb as MethodBuilder).Method.ToString();                        
                    }
                }
            }
            req += "  Request";
            return req;
        }
    }

    public class ResponseCheckStep : IStep
    {
        private IEnumerable<String> _summeries;

        public List<RowItem> _rowItems;

        public ICheck ResponseCheck { get; set; }

        public Boolean Go(ITestContext context, String serverURI, TestCaseItem tcItem,int value)
        {
            Request request = context.RequestQueue.Poll(1000);
            Response response = context.ResponseQueue.Poll(2000);
            if (response == null)
            {
                context.Verbose.Append("           \t          \t            ");
                _summeries = new String[] { "Timeout" };

                StepItem stepItem = new StepItem();
                //stepItem.RowItemList = new List<RowItem>();
                stepItem.IsPassed = false;
                stepItem.IsRequest = false;
                //stepItem.Name = response.Type.ToString() + " Response Code=" + response.Code.ToString();
                stepItem.Name =this.ToString();
                stepItem.RowItemList = new List<RowItem>();
                RowItem rowItem = new RowItem();
                rowItem.ErrorInfo = "Time out";
                rowItem.IsPassed = false;
                rowItem.Name = this.ToString();
                stepItem.RowItemList.Add(rowItem);
                tcItem.StepItemList.Add(stepItem);
                tcItem.stepOperation("Time out", value);
                
                return false;
            }
            else
            {
                context.Verbose.AppendFormat("           \t<---------\t{0}(code={1})", response.Type, response.Code);
                StepItem stepItem = new StepItem();
                stepItem.RowItemList = new List<RowItem>();
                stepItem.IsRequest = false;
                stepItem.Name = response.Type.ToString() + " Response";
                tcItem.StepItemList.Add(stepItem);
                tcItem.stepOperation(response.Type.ToString() + " Response", value);
                if (ResponseCheck != null)
                    return ResponseCheck.Check(response, request,stepItem);
                else
                    return true;
            }
        }

        public IEnumerable<String> Summaries
        {
            get 
            {
                return _summeries == null ?
                    (ResponseCheck is GroupCheck ?
                        (ResponseCheck as GroupCheck).Reasons :
                        new String[] { ResponseCheck.Reason })
                    : _summeries;
            }
        }

        public List<RowItem> RowItems
        {
            get
            {
                //return _rowItems == null ? 
                //    (ResponseCheck is GroupCheck ? 
                //    (ResponseCheck as GroupCheck).RowItems : new List<RowItem>().Add(ResponseCheck.BaseItemInfo)) : _rowItems;
                if (_rowItems == null)
                {
                    if (ResponseCheck is GroupCheck)                    
                        return (ResponseCheck as GroupCheck).RowItems;                    
                    else
                    {
                        List<RowItem> rowItems= new List<RowItem>();
                        rowItems.Add(ResponseCheck.BaseItemInfo);
                        return rowItems;
                    }
                }
                else                
                    return _rowItems;                
            }
        }

        public override String ToString()
        {
            String res="";
            if (ResponseCheck is GroupCheck)
            {
                foreach (ICheck ck in ResponseCheck as GroupCheck)
                {
                    if(ck is MessageTypeCheck)
                        res = (ck as MessageTypeCheck).Expected.ToString();
                }
            }
            res += "  Response";
            return res;
        }
    }
}
