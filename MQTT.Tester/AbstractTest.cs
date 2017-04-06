using System;
using System.Collections.Generic;

namespace CoAP.Tester
{
    abstract class AbstractTest : ITest
    {
        private List<String> _reasons;

        private List<RowItem> _baseItems;

        public event EventHandler Completed;

        public String Name { get; private set; }

        public String Brief { get; private set; }

        public String Description { get; set; }

        public Boolean Sync { get; private set; }

        public Boolean Verbose { get; private set; }

        public IEnumerable<String> Reasons
        {
            get { return _reasons; }
        }

        public Boolean Passed
        {
            get { return _reasons == null || _reasons.Count == 0; }
        }

        public abstract String ResourceURI { get; }

        public AbstractTest(String name, String brief, String description)
            : this(name, brief, description, true, false)
        { }

        public AbstractTest(String name, String brief, String description, Boolean sync, Boolean verbose)
        {
            Name = name;
            Brief = brief;
            Description = description;
            Sync = sync;
            Verbose = verbose;
        }

        public void Run(String serverURI, CoAP.Tester.FormMain.StepOperation stepOperation)
        {
            if (String.IsNullOrEmpty(serverURI))
                throw new ArgumentNullException("serverURI");

            _reasons = null;

            Request request = PrepareRequest();

            Uri uri = new Uri(new Uri(serverURI), ResourceURI);
            request.URI = uri;

            request.Respond += new EventHandler<ResponseEventArgs>(request_Respond);

            request.Send();

            if (Sync)
            {
                Boolean running = true;
                request.Timeout += (o, e) => running = false;

                while (running)
                {
                    Response response = request.WaitForResponse();
                    //if (response == null || !response.IsEmptyACK)
                    //    break;
                }
            }
        }

        protected void Done()
        {
            EventHandler handler = Completed;
            if (handler != null)
                handler(this, null);
        }

        protected void AppendReason(String reason)
        {
            if (_reasons == null)
                _reasons = new List<String>();
            _reasons.Add(reason);
        }

        protected void AppendBaseItem(RowItem baseItem)
        {
            if (_baseItems == null)
                _baseItems = new List<RowItem>();
            _baseItems.Add(baseItem);
        }

        protected Boolean CheckType(MessageType expectedMessageType, MessageType actualMessageType)
        {
            Boolean success = expectedMessageType.Equals(actualMessageType);
            if (!success)
            {
                AppendReason(String.Format("Expected type {0}, but received {1}", expectedMessageType, actualMessageType));

                RowItem bi = new RowItem();
                bi.Name = "type";
                bi.ErrorInfo = String.Format("Expected type {0}, but received {1}", expectedMessageType, actualMessageType);
                bi.Expected = expectedMessageType.ToString();
                bi.Received = actualMessageType.ToString();
                bi.Parent = null;
                bi.IsPassed = false;
                AppendBaseItem(bi);

            }
            return success;
        }

        protected Boolean CheckInt(Int32 expected, Int32 actual, String fieldName)
        {
            Boolean success = expected == actual;
            if (!success)
            {
                AppendReason(String.Format("Expected {0}: {1}, but received: {2}", fieldName, expected, actual));

                RowItem bi = new RowItem();
                bi.Name = "fieldName";
                bi.ErrorInfo = String.Format("Expected {0}: {1}, but received: {2}", fieldName, expected, actual);
                bi.Expected = expected.ToString();
                bi.Received = actual.ToString();
                bi.Parent = null;
                bi.IsPassed = false;
                AppendBaseItem(bi);
            }
            return success;
        }

        protected Boolean HasContentType(Response response)
        {
            Boolean success = response.HasOption(OptionType.ContentType);
            if (!success)
            {
                AppendReason("Response without Content-Type");

                RowItem bi = new RowItem();
                bi.Name = "Content-Type";
                bi.ErrorInfo = "Response without Content-Type";               
                bi.Parent = null;
                bi.IsPassed = false;
                AppendBaseItem(bi);
            }
            return success;
        }

        protected abstract Request PrepareRequest();
        protected abstract void CheckResponse(Response response);

        private void request_Respond(object sender, ResponseEventArgs e)
        {
            CheckResponse(e.Response);
            //if (!e.Response.IsEmptyACK)
                Done();
        }


        public string Summary
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
