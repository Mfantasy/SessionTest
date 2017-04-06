using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CoAP.Tester
{
    static class TestCaseResultIO
    {
        public static XmlDocument ResultToXML(List<TestCaseItem> tcItemList)
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "utf-8", ""));
            
            XmlElement root = doc.CreateElement("root");
            doc.AppendChild(root);


            foreach (TestCaseItem tcItem in tcItemList)
            {
                XmlElement testcaseResult = doc.CreateElement("testcaseresult");
                
                XmlElement resultName = doc.CreateElement("testcasename");
                resultName.InnerText = tcItem.TestCaseName;
                resultName.SetAttribute("group",tcItem.Group);
                testcaseResult.AppendChild(resultName);

                XmlElement result = doc.CreateElement("result");
                result.InnerText = tcItem.IsPassed ? "TRUE" : "FALSE";
                testcaseResult.AppendChild(result);

                XmlElement tcItemDetail = doc.CreateElement("testcaseitem");
                TestCaseItemToDetial(tcItemDetail, tcItem.StepItemList,doc);
                testcaseResult.AppendChild(tcItemDetail);

                root.AppendChild(testcaseResult);
            }
            return doc;
        }

        private static void TestCaseItemToDetial(XmlElement tcItemDetail, List<StepItem> stepItemList,XmlDocument doc)
        {
            foreach (StepItem stepItem in stepItemList)
            {
                String tagName;
                XmlElement step;
                if (stepItem.IsRequest)
                {
                    tagName = "request";
                    step = doc.CreateElement(tagName);
                    step.InnerText = stepItem.Name;
                }
                else
                {
                    tagName = "response";
                    step = doc.CreateElement(tagName);
                    XmlElement responseName = doc.CreateElement("name");
                    responseName.InnerText = stepItem.Name;
                    step.AppendChild(responseName);

                    XmlElement responseItem = doc.CreateElement("responseitem");
                    CreateResponseItems(stepItem.RowItemList, responseItem, doc);
                    step.AppendChild(responseItem);

                }
                tcItemDetail.AppendChild(step);
            }
        }

        private static void CreateResponseItems(List<RowItem> rowItemList, XmlElement responseItem, XmlDocument doc)
        {
            foreach (RowItem row in rowItemList)
            {
                XmlElement item = doc.CreateElement("item");

                XmlElement Name = doc.CreateElement("names");
                Name.InnerText = row.Name;
                item.AppendChild(Name);

                XmlElement expected = doc.CreateElement("expected");
                expected.InnerText = row.Expected;
                item.AppendChild(expected);

                XmlElement received = doc.CreateElement("received");
                received.InnerText = row.Received;
                item.AppendChild(received);

                XmlElement errorinfo = doc.CreateElement("errorinfo");
                errorinfo.InnerText = row.ErrorInfo;
                item.AppendChild(errorinfo);

                responseItem.AppendChild(item);
            }
        }

        public static List<TestCaseItem> XMLToList(String filename)
        {
            List<TestCaseItem> tcItemList = new List<TestCaseItem>();
            
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode root= doc.SelectSingleNode("root");
            foreach (XmlNode node in root.ChildNodes)
            {
                TestCaseItem tcItem = new TestCaseItem();

                XmlNodeList testcasename = ((XmlElement)node).GetElementsByTagName("testcasename");
                if (testcasename != null)
                {
                    tcItem.Group = ((XmlElement)testcasename.Item(0)).GetAttribute("group");
                    tcItem.Name=tcItem.TestCaseName=((XmlElement)testcasename.Item(0)).InnerText;
                }
                XmlNodeList result = ((XmlElement)node).GetElementsByTagName("result");
                if (result != null)
                    tcItem.IsPassed = ((XmlElement)result.Item(0)).InnerText.Trim().ToUpper() == "TRUE" ? true : false;

                XmlNodeList testcaseitem = ((XmlElement)node).GetElementsByTagName("testcaseitem");
                if (testcaseitem != null)
                    tcItem.StepItemList = TestCaseItemToObj(testcaseitem.Item(0));
                tcItemList.Add(tcItem);

            }
            return tcItemList;
        }

        private static List<StepItem> TestCaseItemToObj(XmlNode testCaseItem)
        {
            List<StepItem> stepItem = new List<StepItem>();

            XmlNodeList nodeList= testCaseItem.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                StepItem item=new StepItem();
                String tag= ((XmlElement)node).Name;
                if (tag == "request")
                {
                    item.IsRequest = true;
                    item.Name = node.InnerText;
                    item.RowItemList = new List<RowItem>();
                }
                else if(tag=="response")
                {
                    item.IsRequest = false;
                    XmlNode responseNameNode=((XmlElement)node).FirstChild;;
                    item.Name = (responseNameNode != null) ? responseNameNode.InnerText : "";

                    XmlNodeList responseItemList=((XmlElement)node).GetElementsByTagName("item");
                    ResponseItemToObj(item,responseItemList);
                }
                stepItem.Add(item);
            }

            return stepItem;

        }

        private static void ResponseItemToObj(StepItem item, XmlNodeList responseItemList)
        {
            item.RowItemList = new List<RowItem>();
            foreach (XmlNode node in responseItemList)
            {
                RowItem row = new RowItem();
                XmlNodeList nameList= ((XmlElement)node).GetElementsByTagName("names");
                row.Name = nameList != null && nameList.Count > 0 ? nameList.Item(0).InnerText : "";

                XmlNodeList expectedList = ((XmlElement)node).GetElementsByTagName("expected");
                row.Expected = expectedList != null && expectedList.Count > 0 ? expectedList.Item(0).InnerText : "";

                XmlNodeList receivedList = ((XmlElement)node).GetElementsByTagName("received");
                row.Received = receivedList != null && receivedList.Count > 0 ? receivedList.Item(0).InnerText : "";

                XmlNodeList errorinfoList = ((XmlElement)node).GetElementsByTagName("errorinfo");
                row.ErrorInfo = errorinfoList != null && errorinfoList.Count > 0 ? errorinfoList.Item(0).InnerText : "";
                item.RowItemList.Add(row);
            }
        }
        

    }
}
