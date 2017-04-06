using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CoAP.Tester
{
    static class TestCaseXMLToObject
    {
        /// <summary>
        /// read the xml file and translate it to object.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<FlowTest> Read(String fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);

            XmlNode root = xmlDoc.SelectSingleNode("root");
            if (root == null)
                return null;
            XmlNodeList xnList = root.ChildNodes;
            List<FlowTest> ftList = new List<FlowTest>();
            foreach (XmlNode node in xnList)
            {
                TestCase tc = NodeToTestCase(node);
                FlowTest ft = new FlowTest(tc.Name, tc.Objective);
                ft.TC = tc;
                ftList.Add(ft);
            }
            return ftList;
        }
        public static TestCase NodeToTestCase(XmlNode node)
        {
            //XmlNodeList nodeList=node.ChildNodes;

            TestCase tc = new TestCase();

            string name = ((XmlElement)node).GetAttribute("name");
            if (name != null)
                tc.Name = name;

            XmlNodeList xnlGroup = ((XmlElement)node).GetElementsByTagName("group");
            if (xnlGroup != null&&xnlGroup.Count>0)
                tc.Group = xnlGroup.Item(0).InnerText;
            XmlNodeList xnlIdentifier = ((XmlElement)node).GetElementsByTagName("identifier");
            if (xnlIdentifier != null&&xnlIdentifier.Count>0)
                tc.Identifier = xnlIdentifier.Item(0).InnerText;
            XmlNodeList xnlObjective = ((XmlElement)node).GetElementsByTagName("objective");
            if (xnlObjective != null&&xnlObjective.Count>0)
                tc.Objective = xnlObjective.Item(0).InnerText;
            XmlNodeList xnlConfiguration = ((XmlElement)node).GetElementsByTagName("configuration");
            if (xnlConfiguration != null&&xnlConfiguration.Count>0)
                tc.Configuration = xnlConfiguration.Item(0).InnerText;

            XmlNodeList xnlPreTestCondition = ((XmlElement)node).GetElementsByTagName("pre-testconditions");
            if (xnlPreTestCondition != null&&xnlPreTestCondition.Count>0)
                tc.Pre_testconditions = xnlPreTestCondition.Item(0).InnerText;

            XmlNodeList xnlTestSequence = ((XmlElement)node).GetElementsByTagName("testSequence");
            if (xnlTestSequence != null&&xnlTestSequence.Count>0)
                tc.TestSequence = xnlTestSequence.Item(0).InnerText;

            List<TestCaseBase> tcbList = new List<TestCaseBase>();

            tc.tcbList = tcbList;

            XmlNodeList xnlTestCaseItem = ((XmlElement)node).GetElementsByTagName("testCaseItem");
            if (xnlTestCaseItem != null && xnlTestCaseItem.Count > 0)
            {
                XmlNodeList xnlRequestResponseList = xnlTestCaseItem.Item(0).ChildNodes;
                foreach (XmlNode reNode in xnlRequestResponseList)
                {
                    TestCaseBase tcb = NodeToTestCaseBase(reNode);
                    tcbList.Add(tcb);
                }
            }

            return tc;
        }

        public static TestCaseBase NodeToTestCaseBase(XmlNode reNode)
        {
            TestCaseBase tcb;
            if (reNode.Name == "testCaseRequest")
            {
                TestCaseRequest tcrq = new TestCaseRequest();
                tcb = tcrq;
                tcb.IsRequest = true;

                XmlNodeList xnMethod = ((XmlElement)reNode).GetElementsByTagName("method");
                if (xnMethod != null)
                {
                    tcrq.Method = xnMethod.Item(0).InnerText;
                }
            }
            else
            {
                TestCaseResponse tcrs = new TestCaseResponse();
                tcb = tcrs;
                tcb.IsRequest = false;

                XmlNodeList xnCode = ((XmlElement)reNode).GetElementsByTagName("code");
                if (xnCode != null)
                {
                    tcrs.Code = xnCode.Item(0).InnerText;
                }
                XmlNodeList xnSameMID = ((XmlElement)reNode).GetElementsByTagName("sameMID");
                if (xnSameMID != null && xnSameMID.Count>0)
                {
                    tcrs.SameMID = xnSameMID.Item(0).InnerText;
                }
            }

            XmlNodeList xnMessageType = ((XmlElement)reNode).GetElementsByTagName("messageType");
            if (xnMessageType != null && xnMessageType.Count>0)
            {
                tcb.MessageType = xnMessageType.Item(0).InnerText;
            }

            XmlNodeList xnPayLoad = ((XmlElement)reNode).GetElementsByTagName("payLoad");
            if (xnPayLoad != null&&xnPayLoad.Count>0)
            {
                tcb.PayLoad = xnPayLoad.Item(0).InnerText;
            }

            XmlNodeList xnToken = ((XmlElement)reNode).GetElementsByTagName("token");
            if (xnToken != null&&xnToken.Count>0)
            {
                tcb.Token = xnToken.Item(0).InnerText;
            }

            OptionTypes ots = new OptionTypes();

            XmlNodeList xnlOptionTypeList = ((XmlElement)reNode).GetElementsByTagName("optionType");
            if (xnlOptionTypeList != null&&xnlOptionTypeList.Count > 0)
            {                
                XmlNode xnlOptionType = xnlOptionTypeList.Item(0);
                ots = NodeToOptionTypes(xnlOptionType);                
            }
            tcb.OptionType = ots;

            return tcb;
        }

        public static OptionTypes NodeToOptionTypes(XmlNode optionNode)
        {
            OptionTypes ots = new OptionTypes();

            XmlNodeList xnUriPath = ((XmlElement)optionNode).GetElementsByTagName("uriPath");
            if (xnUriPath != null&&xnUriPath.Count>0)
            {
                ots.UriPath = xnUriPath.Item(0).InnerText;
            }

            XmlNodeList xnUriQuery = ((XmlElement)optionNode).GetElementsByTagName("uriQuery");
            if (xnUriQuery != null&&xnUriQuery.Count>0)
            {
                ots.UriQuery = xnUriQuery.Item(0).InnerText;
            }

            XmlNodeList xnAccept = ((XmlElement)optionNode).GetElementsByTagName("accept");
            if (xnAccept != null && xnAccept.Count>0)
            {
                ots.Accept = xnAccept.Item(0).InnerText;
            }

            XmlNodeList xnContent_Format = ((XmlElement)optionNode).GetElementsByTagName("content-Format");
            if (xnContent_Format != null && xnContent_Format.Count>0)
            {
                ots.Content_Format = xnContent_Format.Item(0).InnerText;
            }

            XmlNodeList xnBlock1 = ((XmlElement)optionNode).GetElementsByTagName("block1");
            if (xnBlock1 != null&&xnBlock1.Count>0)
            {
                ots.Block1 = xnBlock1.Item(0).InnerText;
            }
            XmlNodeList xnBlock2 = ((XmlElement)optionNode).GetElementsByTagName("block2");
            if (xnBlock2 != null && xnBlock2.Count>0)
            {
                ots.Block2 = xnBlock2.Item(0).InnerText;
            }

            XmlNodeList xnObserve = ((XmlElement)optionNode).GetElementsByTagName("observe");
            if (xnObserve != null&&xnObserve.Count>0)
            {
                ots.Observe = xnObserve.Item(0).InnerText;
            }

            XmlNodeList xnETag = ((XmlElement)optionNode).GetElementsByTagName("eTag");
            if (xnETag != null&&xnETag.Count>0)
            {
                ots.ETag = xnETag.Item(0).InnerText;
            }

            XmlNodeList xnIf_Match = ((XmlElement)optionNode).GetElementsByTagName("if-Match");
            if (xnIf_Match != null&&xnIf_Match.Count>0)
            {
                ots.If_Match = xnIf_Match.Item(0).InnerText;
            }

            XmlNodeList xnIf_None_Mathch = ((XmlElement)optionNode).GetElementsByTagName("if-None-Match");
            if (xnIf_None_Mathch != null&&xnIf_None_Mathch.Count>0)
            {
                ots.If_None_Match = xnIf_None_Mathch.Item(0).InnerText.Trim().ToUpper();
            }

            XmlNodeList xnUri_Host = ((XmlElement)optionNode).GetElementsByTagName("uri-Host");
            if (xnUri_Host != null&&xnUri_Host.Count>0)
            {
                ots.Uri_Host = xnUri_Host.Item(0).InnerText;
            }

            XmlNodeList xnUri_Port = ((XmlElement)optionNode).GetElementsByTagName("uri-Port");
            if (xnUri_Port != null&&xnUri_Port.Count>0)
            {
                ots.Uri_Port = xnUri_Port.Item(0).InnerText;
            }

            XmlNodeList xnProxy_Uri = ((XmlElement)optionNode).GetElementsByTagName("proxy-Uri");
            if (xnProxy_Uri != null && xnProxy_Uri.Count>0)
            {
                ots.Proxy_Uri = xnProxy_Uri.Item(0).InnerText;
            }

            XmlNodeList xnProxy_Scheme = ((XmlElement)optionNode).GetElementsByTagName("proxy-Scheme");
            if (xnProxy_Scheme != null && xnProxy_Scheme.Count>0)
            {
                ots.Proxy_Scheme = xnProxy_Scheme.Item(0).InnerText.Trim().ToUpper();
            }

            XmlNodeList xnMax_Age = ((XmlElement)optionNode).GetElementsByTagName("max-Age");
            if (xnMax_Age != null && xnMax_Age.Count>0)
            {
                ots.Max_Age = xnMax_Age.Item(0).InnerText;
            }

            XmlNodeList xnLocation_Path = ((XmlElement)optionNode).GetElementsByTagName("location-Path");
            if (xnLocation_Path != null && xnLocation_Path.Count>0)
            {
                ots.Location_Path = xnLocation_Path.Item(0).InnerText;
            }

            XmlNodeList xnLocation_Query = ((XmlElement)optionNode).GetElementsByTagName("location-Query");
            if (xnLocation_Query != null&&xnLocation_Query.Count>0)
            {
                ots.Location_Query = xnLocation_Query.Item(0).InnerText;
            }

            return ots;
        }


        public static TestCase GetNewTestCase(FlowTest flowTest)
        {
            TestCase tc = flowTest.TC;
            tc.tcbList.Clear();
            foreach (IStep step in (List<IStep>)flowTest.Steps)
            {
                TestCaseBase tcb = new TestCaseBase();
                if (step is RequestBuildStep)
                {
                    tcb = StepToTestCaseRequest((RequestBuildStep)step);
                }
                else if (step is ResponseCheckStep)
                {
                    tcb = StepToTestCaseResponse((ResponseCheckStep)step);
                }
                tc.tcbList.Add(tcb);
            }
            return tc;
        }

        public static void BuildTestCaseRequest(IRequestBuilder builder, TestCaseRequest tcRequest)
        {
            if (builder is MethodBuilder)
                tcRequest.Method = (builder as MethodBuilder).Method.ToString();
            else if (builder is MessageTypeBuilder)
                tcRequest.MessageType = (builder as MessageTypeBuilder).MessageType.ToString();
            else if (builder is TokenBuilder)
            {
                tcRequest.Token = Utils.ToHexString((builder as TokenBuilder).Token);
            }
            else if (builder is PayloadBuilder)
            {
                PayloadBuilder pb = builder as PayloadBuilder;
                if (pb.Payload != null)
                    tcRequest.PayLoad = Utils.ToHexString(pb.Payload);
                else
                    tcRequest.PayLoad = pb.PayloadString;
                tcRequest.OptionType.Content_Format = pb.ContentType.ToString();
            }

            else if (builder is OptionBuilder)
            {
                OptionBuilder ob = builder as OptionBuilder;
                switch (ob.OptionType)
                {
                    case OptionType.UriPath:
                        tcRequest.OptionType.UriPath = ob.StringValue;
                        break;
                    case OptionType.UriQuery:
                        tcRequest.OptionType.UriQuery = ob.StringValue;
                        break;
                    case OptionType.Accept:
                        tcRequest.OptionType.Accept = ob.IntValue.ToString();
                        break;
                    case OptionType.ContentType:
                        tcRequest.OptionType.Content_Format = ob.IntValue.ToString();
                        break;
                    case OptionType.ETag:
                        tcRequest.OptionType.ETag = ob.RawValue == null ? ob.StringValue : Utils.ToHexString(ob.RawValue);
                        break;
                    case OptionType.IfMatch:
                        tcRequest.OptionType.If_Match = ob.RawValue == null ? ob.StringValue : Utils.ToHexString(ob.RawValue);
                        break;
                    case OptionType.IfNoneMatch:
                        tcRequest.OptionType.If_None_Match = "TRUE";
                        break;
                    case OptionType.Observe:
                        //tcRequest.OptionType.Observe = ob.IntValue.ToString();
                        tcRequest.OptionType.Observe = ob.IntValue == null ? (ob.StringValue == null ? "" : ob.StringValue) : ob.IntValue.ToString();
                        break;
                    case OptionType.Block2:
                        //tcRequest.OptionType.Block2 = ob.IntValue.ToString();
                        tcRequest.OptionType.Block2 = ob.IntValue == null ? (ob.StringValue == null ? "" : ob.StringValue) : ob.IntValue.ToString();
                        break;
                    case OptionType.Block1:
                        //tcRequest.OptionType.Block1 = ob.IntValue.ToString();
                        tcRequest.OptionType.Block1 = ob.IntValue == null ? (ob.StringValue == null ? "" : ob.StringValue) : ob.IntValue.ToString();
                        break;
                    case OptionType.LocationPath:
                        tcRequest.OptionType.Location_Path = ob.StringValue.ToString();
                        break;
                    case OptionType.LocationQuery:
                        tcRequest.OptionType.Location_Query = ob.StringValue.ToString();
                        break;
                    case OptionType.MaxAge:
                        //tcRequest.OptionType.Max_Age = ob.IntValue.ToString();
                        tcRequest.OptionType.Max_Age = ob.IntValue == null ? (ob.StringValue == null ? "" : ob.StringValue) : ob.IntValue.ToString();
                        break;
                    case OptionType.ProxyScheme:
                        tcRequest.OptionType.Proxy_Scheme = "TRUE";                        
                        break;
                    case OptionType.ProxyUri:
                        tcRequest.OptionType.Proxy_Uri = ob.StringValue.ToString();
                        break;
                    case OptionType.Token:
                        tcRequest.Token = ob.StringValue.ToString();
                        break;
                    case OptionType.UriHost:
                        tcRequest.OptionType.Uri_Host = ob.StringValue.ToString();
                        break;
                    case OptionType.UriPort:
                        //tcRequest.OptionType.Uri_Port = ob.IntValue.ToString();
                        tcRequest.OptionType.Uri_Port = ob.IntValue == null ? (ob.StringValue == null ? "" : ob.StringValue) : ob.IntValue.ToString();
                        break;
                }
            }
        }

        public static TestCaseRequest StepToTestCaseRequest(RequestBuildStep step)
        {
            TestCaseRequest tcRequest = new TestCaseRequest();
            tcRequest.IsRequest = true;
            IRequestBuilder builder = step.RequestBuilder;
            if (builder is GroupBuilder)
            {
                foreach (IRequestBuilder rb in builder as GroupBuilder)
                {
                    BuildTestCaseRequest(rb, tcRequest);
                }
            }
            else
            {
                BuildTestCaseRequest(builder, tcRequest);
            }
            return tcRequest;
        }

        public static TestCaseResponse StepToTestCaseResponse(ResponseCheckStep step)
        {
            TestCaseResponse tcResponse = new TestCaseResponse();
            tcResponse.IsRequest = false;
            ICheck check = step.ResponseCheck;
            if (check is GroupCheck)
            {
                foreach (ICheck ck in check as GroupCheck)
                {
                    BuildTestCaseResponse(ck, tcResponse);
                }
            }
            else
                BuildTestCaseResponse(check, tcResponse);
            return tcResponse;
        }

        public static void BuildTestCaseResponse(ICheck check, TestCaseResponse tcResponse)
        {
            if (check is CodeCheck)
                tcResponse.Code = (check as CodeCheck).Expected.ToString();
            else if (check is MessageTypeCheck)
                tcResponse.MessageType = (check as MessageTypeCheck).Expected.ToString();
            else if (check is MessageIDCheck)
                tcResponse.SameMID = "true".ToUpper();
            else if (check is PayLoadStringCheck)
                tcResponse.PayLoad = (check as PayLoadStringCheck).Expected.ToString();
            else if (check is PayLoadByteCheck)
            {
                tcResponse.PayLoad = Utils.ToHexString((check as PayLoadByteCheck).Expected);
            }
            else if (check is TokenByteCheck)
            {
                tcResponse.Token = Utils.ToHexString((check as TokenByteCheck).Expected);
            }
            else if (check is TokenStringCheck)
            {
                tcResponse.Token = (check as TokenStringCheck).Expected.ToString();
            }
            else if (check is TokenExistCheck)
            {
                tcResponse.Token = "";
            }
            else if (check is PayLoadExistCheck)
            {
                tcResponse.PayLoad = "";
            }
            else if (check is OptionCheck)
            {
                OptionCheck oc = check as OptionCheck;
                switch (oc.OptionType)
                {
                    case OptionType.LocationPath:

                        tcResponse.OptionType.Location_Path = oc.StringValue == null ? "" : oc.StringValue;
                        break;
                    case OptionType.LocationQuery:
                        tcResponse.OptionType.Location_Query = oc.StringValue == null ? "" : oc.StringValue;
                        break;
                    case OptionType.Observe:
                        tcResponse.OptionType.Observe = oc.IntValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : oc.IntValue.ToString();
                        break;
                    //case OptionType.Token:
                    //    tcResponse.Token = oc.RawValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : Utils.ToHexString(oc.RawValue);
                    //    break;
                    case OptionType.Accept:
                        tcResponse.OptionType.Accept = oc.IntValue.ToString();
                        break;
                    case OptionType.Block1:
                        tcResponse.OptionType.Block1 = oc.IntValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : oc.IntValue.ToString();
                        break;
                    case OptionType.Block2:
                        tcResponse.OptionType.Block2 = oc.IntValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : oc.IntValue.ToString();
                        break;
                    case OptionType.ContentType:
                        tcResponse.OptionType.Content_Format = oc.IntValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : oc.IntValue.ToString();
                        break;
                    case OptionType.ETag:
                        tcResponse.OptionType.ETag = oc.RawValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : Utils.ToHexString(oc.RawValue);
                        break;
                    case OptionType.IfMatch:
                        tcResponse.OptionType.If_Match = oc.StringValue == null ? "" : oc.StringValue;
                        break;
                    case OptionType.IfNoneMatch:
                        tcResponse.OptionType.If_None_Match = "TRUE";
                        break;
                    case OptionType.MaxAge:
                        tcResponse.OptionType.Max_Age = oc.IntValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : oc.IntValue.ToString();
                        break;
                    case OptionType.ProxyScheme:
                        tcResponse.OptionType.Proxy_Scheme = "TRUE";
                        break;
                    case OptionType.ProxyUri:
                        tcResponse.OptionType.Proxy_Uri = oc.StringValue == null ? "" : oc.StringValue;
                        break;
                    case OptionType.UriHost:
                        tcResponse.OptionType.Uri_Host = oc.StringValue == null ? "" : oc.StringValue;
                        break;
                    case OptionType.UriPath:
                        tcResponse.OptionType.UriPath = oc.StringValue == null ? "" : oc.StringValue;
                        break;
                    case OptionType.UriPort:
                        tcResponse.OptionType.Uri_Port = oc.IntValue == null ? (oc.StringValue == null ? "" : oc.StringValue) : oc.IntValue.ToString();
                        break;
                    case OptionType.UriQuery:
                        tcResponse.OptionType.UriQuery = oc.StringValue == null ? "" : oc.StringValue;
                        break;
                }
            }
        }

        public static XmlDocument CreateXML(List<TestCase> tcList)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", ""));
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);
            
            foreach (TestCase tc in tcList)
            {
                XmlElement tcElement = TestCaseToNode(tc, doc);
                root.AppendChild(tcElement);
            }
            return doc;
        }

        public static XmlElement TestCaseToNode(TestCase tc, XmlDocument doc)
        {
            XmlElement tcElement = doc.CreateElement("testCase");
            tcElement.SetAttribute("name", tc.Name);

            XmlElement groupElement = doc.CreateElement("group");
            groupElement.InnerText = tc.Group;
            tcElement.AppendChild(groupElement);

            XmlElement identifierElement = doc.CreateElement("identifier");
            identifierElement.InnerText = tc.Identifier;
            tcElement.AppendChild(identifierElement);

            XmlElement objectiveElement = doc.CreateElement("objective");
            objectiveElement.InnerText = tc.Objective;
            tcElement.AppendChild(objectiveElement);

            XmlElement configurationElement = doc.CreateElement("configuration");
            configurationElement.InnerText = tc.Configuration;
            tcElement.AppendChild(configurationElement);

            XmlElement pretestconElement = doc.CreateElement("pre-testconditions");
            pretestconElement.InnerText = tc.Pre_testconditions;
            tcElement.AppendChild(pretestconElement);

            XmlElement testSequenceElement = doc.CreateElement("testSequence");
            testSequenceElement.InnerText=tc.TestSequence;
            tcElement.AppendChild(testSequenceElement);

            XmlElement testCaseItemElement = doc.CreateElement("testCaseItem");
            TestCaseBaseToNode(tc.tcbList, doc, testCaseItemElement);
            tcElement.AppendChild(testCaseItemElement);

            return tcElement;
        }

        public static void TestCaseBaseToNode(List<TestCaseBase> tcbList, XmlDocument doc, XmlElement testCaseItemElement)
        {
            foreach (TestCaseBase tcb in tcbList)
            {
                XmlElement reElement;
                if (tcb.IsRequest)
                {
                    reElement = TestCaseRequestToNode((TestCaseRequest)tcb, doc);
                }
                else
                {
                    reElement = TestCaseResonseToNode((TestCaseResponse)tcb, doc);
                }
                testCaseItemElement.AppendChild(reElement);
            }
        }

        public static XmlElement TestCaseRequestToNode(TestCaseRequest tcRequest, XmlDocument doc)
        {
            XmlElement tcrElement = doc.CreateElement("testCaseRequest");

            XmlElement methodElement = doc.CreateElement("method");
            methodElement.InnerText = tcRequest.Method;
            tcrElement.AppendChild(methodElement);

            XmlElement messageTypeElement = doc.CreateElement("messageType");
            messageTypeElement.InnerText = tcRequest.MessageType;
            tcrElement.AppendChild(messageTypeElement);

            XmlElement payLoadElement = doc.CreateElement("payLoad");
            payLoadElement.InnerText=tcRequest.PayLoad;
            tcrElement.AppendChild(payLoadElement);

            XmlElement tokenElement = doc.CreateElement("token");
            tokenElement.InnerText = tcRequest.Token;
            tcrElement.AppendChild(tokenElement);

            XmlElement optionType = doc.CreateElement("optionType");
            tcrElement.AppendChild(optionType);

            OptionTypesToNode(tcRequest.OptionType, doc, optionType);

            return tcrElement;
        }

        public static XmlElement TestCaseResonseToNode(TestCaseResponse tcResponse, XmlDocument doc)
        {
            XmlElement tcrElement = doc.CreateElement("testCaseResponse");

            XmlElement code = doc.CreateElement("code");
            code.InnerText = tcResponse.Code;
            tcrElement.AppendChild(code);

            XmlElement messageTypeElement = doc.CreateElement("messageType");
            messageTypeElement.InnerText = tcResponse.MessageType;
            tcrElement.AppendChild(messageTypeElement);

            if (tcResponse.PayLoad != null)
            {
                XmlElement payLoadElement = doc.CreateElement("payLoad");
                payLoadElement.InnerText = tcResponse.PayLoad;
                tcrElement.AppendChild(payLoadElement);
            }

            if (tcResponse.Token != null)
            {
                XmlElement tokenElement = doc.CreateElement("token");
                tokenElement.InnerText = tcResponse.Token;
                tcrElement.AppendChild(tokenElement);
            }

            if (tcResponse.SameMID != null)
            {
                XmlElement sameIDElement = doc.CreateElement("sameMID");
                sameIDElement.InnerText = tcResponse.SameMID;
                tcrElement.AppendChild(sameIDElement);
            }

            //OptionTypesToNode(tcResponse.OptionType, doc, tcrElement);

            XmlElement optionType = doc.CreateElement("optionType");
            tcrElement.AppendChild(optionType);

            OptionTypesToNode(tcResponse.OptionType, doc, optionType);

            return tcrElement;
        }

        public static void OptionTypesToNode(OptionTypes ots,XmlDocument doc,XmlElement tcrElement)
        {
            if (ots.UriPath != null)
            {
                XmlElement uriPathElement = doc.CreateElement("uriPath");
                uriPathElement.InnerText = ots.UriPath;
                tcrElement.AppendChild(uriPathElement);
            }
            if (ots.UriQuery != null)
            {
                XmlElement uriQuery = doc.CreateElement("uriQuery");
                uriQuery.InnerText = ots.UriQuery;
                tcrElement.AppendChild(uriQuery);
            }
            if (ots.Accept != null)
            {
                XmlElement accept = doc.CreateElement("accept");
                accept.InnerText = ots.Accept;
                tcrElement.AppendChild(accept);
            }
            if (ots.Content_Format != null)
            {
                XmlElement contentFormat = doc.CreateElement("content-Format");
                contentFormat.InnerText = ots.Content_Format;
                tcrElement.AppendChild(contentFormat);
            }
            if (ots.Block1 != null)
            {
                XmlElement block1 = doc.CreateElement("block1");
                block1.InnerText = ots.Block1;
                tcrElement.AppendChild(block1);
            }
            if (ots.Block2 != null)
            {
                XmlElement block2 = doc.CreateElement("block2");
                block2.InnerText = ots.Block2;
                tcrElement.AppendChild(block2);
            }
            if (ots.Observe != null)
            {
                XmlElement observe = doc.CreateElement("observe");
                observe.InnerText = ots.Observe;
                tcrElement.AppendChild(observe);
            }
            if (ots.ETag != null)
            {
                XmlElement eTag = doc.CreateElement("eTag");
                eTag.InnerText = ots.ETag;
                tcrElement.AppendChild(eTag);
            }
            if (ots.If_Match != null)
            {
                XmlElement ifMatch = doc.CreateElement("if-Match");
                ifMatch.InnerText = ots.If_Match;
                tcrElement.AppendChild(ifMatch);
            }
            if (ots.If_None_Match!=null)
            {
                XmlElement ifNoneMatch = doc.CreateElement("if-None-Match");
                ifNoneMatch.InnerText = ots.If_None_Match;
                tcrElement.AppendChild(ifNoneMatch);
            }
            if (ots.Uri_Host != null)
            {
                XmlElement uriHost = doc.CreateElement("uri-Host");
                uriHost.InnerText = ots.Uri_Host;
                tcrElement.AppendChild(uriHost);
            }
            if (ots.Uri_Port!=null)
            {
                XmlElement uriPort = doc.CreateElement("uri-Port");
                uriPort.InnerText = ots.Uri_Port;
                tcrElement.AppendChild(uriPort);
            }
            if (ots.Proxy_Uri != null)
            {
                XmlElement proxyUri = doc.CreateElement("proxy-Uri");
                proxyUri.InnerText = ots.Proxy_Uri;
                tcrElement.AppendChild(proxyUri);
            }
            if (ots.Proxy_Scheme != null)
            {
                XmlElement proxyScheme = doc.CreateElement("proxy-Scheme");
                proxyScheme.InnerText = ots.Proxy_Scheme;
                tcrElement.AppendChild(proxyScheme);
            }
            if (ots.Max_Age != null)
            {
                XmlElement maxAge = doc.CreateElement("max-Age");
                maxAge.InnerText = ots.Max_Age;
                tcrElement.AppendChild(maxAge);
            }
            if (ots.Location_Path != null)
            {
                XmlElement locationPath = doc.CreateElement("location-Path");
                locationPath.InnerText = ots.Location_Path;
                tcrElement.AppendChild(locationPath);
            }
            if (ots.Location_Query != null)
            {
                XmlElement locationQuery = doc.CreateElement("location-Query");
                locationQuery.InnerText = ots.Location_Query;
                tcrElement.AppendChild(locationQuery);
            }
        }
         
    }
}
