using System;
using System.Collections.Generic;

namespace CoAP.Tester
{
    class TemplateTest : AbstractTest
    {
        private Request _request;
        private String _resourceURI;
        private List<IRequestBuilder> _requestBuilders = new List<IRequestBuilder>();
        private ICheck _responseCheck;

        public TemplateTest(String resourceURI, String name, String brief)
            : base(name, brief, brief)
        {
            _resourceURI = resourceURI;
        }

        public TemplateTest(String resourceURI, String name, String brief, String description)
            : base(name, brief, description)
        {
            _resourceURI = resourceURI;
        }

        public TemplateTest(String resourceURI, String name, String brief, String description, Boolean sync, Boolean verbose)
            : base(name, brief, description, sync, verbose)
        {
            _resourceURI = resourceURI;
        }

        public override String ResourceURI
        {
            get { return _resourceURI; }
        }

        public TemplateTest AddRequestBuilder(IRequestBuilder requestBuilder)
        {
            _requestBuilders.Add(requestBuilder);
            return this;
        }

        public TemplateTest SetResponseCheck(ICheck check)
        {
            _responseCheck = check;
            return this;
        }

        protected override Request PrepareRequest()
        {
            _request = Request.NewGet();
            foreach (IRequestBuilder builder in _requestBuilders)
            {
                _request = builder.Build(_request);
            }
            return _request;
        }

        protected override void CheckResponse(Response response)
        {
            if (_responseCheck != null)
            {
                if (!_responseCheck.Check(response, _request,null))
                {
                    AppendReason(_responseCheck.Reason);
                    
                }                    
            }
        }
    }
}
