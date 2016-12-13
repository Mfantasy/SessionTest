using System;
using System.Collections.Generic;
using System.Text;

namespace CoAP.Tester
{
    class OptionTypes
    {
        private string uriPath = null;

        public string UriPath
        {
            get { return uriPath; }
            set { uriPath = value; }
        }
        private string uriQuery = null;

        public string UriQuery
        {
            get { return uriQuery; }
            set { uriQuery = value; }
        }
        private string accept = null;

        public string Accept
        {
            get { return accept; }
            set { accept = value; }
        }
        private string content_Format = null;

        public string Content_Format
        {
            get { return content_Format; }
            set { content_Format = value; }
        }
        private string block1 = null;

        public string Block1
        {
            get { return block1; }
            set { block1 = value; }
        }
        private string block2 = null;

        public string Block2
        {
            get { return block2; }
            set { block2 = value; }
        }
        private string observe = null;

        public string Observe
        {
            get { return observe; }
            set { observe = value; }
        }
        private string eTag = null;

        public string ETag
        {
            get { return eTag; }
            set { eTag = value; }
        }
        private string if_Match = null;

        public string If_Match
        {
            get { return if_Match; }
            set { if_Match = value; }
        }
        private string if_None_Match = null;

        public string If_None_Match
        {
            get { return if_None_Match; }
            set { if_None_Match = value; }
        }
        private string uri_Host = null;

        public string Uri_Host
        {
            get { return uri_Host; }
            set { uri_Host = value; }
        }
        private string uri_Port = null;

        public string Uri_Port
        {
            get { return uri_Port; }
            set { uri_Port = value; }
        }
        private string proxy_Uri = null;

        public string Proxy_Uri
        {
            get { return proxy_Uri; }
            set { proxy_Uri = value; }
        }
        private string proxy_Scheme = null;

        public string Proxy_Scheme
        {
            get { return proxy_Scheme; }
            set { proxy_Scheme = value; }
        }
        private string max_Age = null;

        public string Max_Age
        {
            get { return max_Age; }
            set { max_Age = value; }
        }
        private string location_Path = null;

        public string Location_Path
        {
            get { return location_Path; }
            set { location_Path = value; }
        }
        private string location_Query = null;

        public string Location_Query
        {
            get { return location_Query; }
            set { location_Query = value; }
        }
    }
}
