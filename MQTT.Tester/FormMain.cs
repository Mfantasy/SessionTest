using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;
using CoAP.EndPoint;
using CoAP.EndPoint.Resources;
using CoAP.Net;
using System.Xml.Xsl;
using System.Xml.XPath;
using Microsoft.Win32;
using System.Threading;

namespace CoAP.Tester
{

    public partial class FormMain : Form
    {
        #region browser
        private static readonly HeaderRow[] emptyHeaderRows = new HeaderRow[0];
        private static readonly OptionRow[] emptyOptionRows = new OptionRow[0];
        private static readonly Byte[] emptyPayload = new Byte[0];

        private delegate void DisplayDiscoverHandler(Request request, Response response);
        private Action<Message, Byte[]> _displayMessageHandler;
        private DisplayDiscoverHandler _displayDiscoverHandler;
        private EventHandler _resourceClickHandler;
        private Request _currentRequest;
        private List<Byte> _currentPayload = new List<Byte>(4096);
        private readonly String _tempImagePath = Path.GetTempFileName();
        private Boolean _observing = false;
        private IEndPoint _coapEndPoint;
        private Int32 _preferBlockSize; 
        #endregion

        private int tabwidth = 0;
        private int listBox1Width = 0;
        double listBox1Pro,listBox2Pro,btnPro;

        public delegate void StepOperation(String lStepText, int value);
        private delegate void StatusBarOperation(ToolStripProgressBar pbTotal, int value, ToolStripStatusLabel lTCName, String tcName);
        StatusBarOperation statusBarOperation;

        private delegate void StatusOperation(bool isVisiable);
        StatusOperation statusOperation;

        private List<string> groupList = new List<string>();
        private RunningState _runningState;
        private Object _waitingHandle = new Byte[0];
        private List<TestCaseItem> tcItemList = new List<TestCaseItem>();
        public FormMain()
        {
            InitializeComponent();
            
            treeListView1.CheckBoxes = CheckBoxesTypes.None;
            statusOperation = new StatusOperation(SetStatusVisibility);
            SetStatusVisibility(false);


            #region browser
            // controls
            //comboBoxAccept.Items.AddRange(MediaTypeEntry.All);

            // handlers
            _displayMessageHandler = new Action<Message, Byte[]>(DisplayMessage);
            _displayDiscoverHandler = new DisplayDiscoverHandler(DisplayDiscover);
            _resourceClickHandler = new EventHandler(btnResource_Click); 
            #endregion
            
        }
        RunningState RunningState
        {
            get { return _runningState; }
            set
            {
                _runningState = value;
                tsbtnRun.Enabled = _runningState != RunningState.Running;
                tsbtnStop.Enabled = _runningState != RunningState.Stopped;
                tsbtnPause.Enabled = _runningState == RunningState.Running;
            }
        }

        private void Run()
        {
            if (RunningState == RunningState.Stopped)
            {
                tcItemList.Clear();
                treeListView1.Items.Clear();
                foreach (TreeNode node in treeViewCases.Nodes)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        if (child.Checked)
                        {
                            SetTreeNodeImageKey(child, null);
                            (child.Tag as ITest).Summary = String.Empty;
                        }
                    }
                }
                backgroundWorkerTest = new BackgroundWorker();
                backgroundWorkerTest.DoWork += new DoWorkEventHandler(this.backgroundWorkerTest_DoWork);
                backgroundWorkerTest.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerTest_RunWorkerCompleted);
                backgroundWorkerTest.RunWorkerAsync("coap://" + tscbxTargetAddress.Text.Trim());
            }
            else if (RunningState == RunningState.Paused)
            {
                lock (_waitingHandle)
                {
                    System.Threading.Monitor.PulseAll(_waitingHandle);
                }
            }
            RunningState = RunningState.Running;
        }

        private void Pause()
        {
            RunningState = RunningState.Paused;
        }

        private void Stop()
        {
            if (RunningState == RunningState.Paused)
            {
                lock (_waitingHandle)
                {
                    System.Threading.Monitor.PulseAll(_waitingHandle);
                }
            }
            RunningState = RunningState.Stopped;
        }

        private void RunTests(String serverURI)
        {
            int selectedTestCaseCount = 0;
            int executedTestCaseCount = 0;
            foreach (TreeNode node in treeViewCases.Nodes)
            {
                foreach (TreeNode child in node.Nodes)
                {
                    if (child.Checked)
                        selectedTestCaseCount++;
                }
            }
            this.Invoke(statusBarOperation, pbTotal, 0, lTCName, "");

            #region run
            foreach (TreeNode node in treeViewCases.Nodes)
            {
                foreach (TreeNode child in node.Nodes)
                {
                    if (RunningState == RunningState.Paused)
                    {
                        lock (_waitingHandle)
                        {
                            System.Threading.Monitor.Wait(_waitingHandle);
                        }
                    }

                    if (RunningState == RunningState.Stopped)
                        break;

                    if (child.Checked)
                    {
                        ITest test = child.Tag as ITest;
                        if (test != null)
                        {
                            //UpdateNodeState(child, "run");
                            UpdateNodeState(child, "run2");
                            StepOperation stepOperation = new StepOperation(InvokeControl);
                            test.Run(serverURI, stepOperation);
                            TestCaseItem tcItem = (test as FlowTest).TCItem;

                            executedTestCaseCount++;
                            int value = ((executedTestCaseCount * 100) / selectedTestCaseCount);
                            this.Invoke(statusBarOperation, pbTotal, value, lTCName, tcItem.Name);


                            tcItem.IsPassed = test.Passed;
                            //UpdateNodeState(child, test.Passed ? "pass" : "fail");
                            UpdateNodeState(child, test.Passed ? "duihao" : "chahao");
                            if (tcItem != null)
                            {
                                tcItemList.Add(tcItem);
                                //AddTreeListViewItem(treeListView1,tcItem);
                            }
                        }
                    }
                }
            } 
            #endregion            
        }

        private void InvokeControl(String lStepText, int value)
        {
            this.Invoke(statusBarOperation, pbTestCase, value, lStep, lStepText);
        }

        private void UpdateProgressBar(ToolStripProgressBar pbTotal, int value, ToolStripStatusLabel lTCName, String tcName)
        {
            pbTotal.Value = value;
            lTCName.Text = tcName;
        }

        private void UpdateNodeState(TreeNode node, String imageKey)
        {
            if (node.TreeView.InvokeRequired)
            {
                node.TreeView.Invoke(new Action<TreeNode, String>(UpdateNodeState), node, imageKey);
            }
            else
            {
                node.ImageKey = node.SelectedImageKey = imageKey;
                if (node.IsSelected)
                    DisplayTestInfo(node.Tag as ITest);
            }
        }

        private void SetTreeNodeImageKey(TreeNode node, String key)
        {
            if (node.TreeView.InvokeRequired)
            {
                node.TreeView.Invoke(new Action<TreeNode, String>(SetTreeNodeImageKey), node, key);
            }
            else
            {
                node.ImageKey = node.SelectedImageKey = key;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            RunningState = RunningState.Stopped;
            //tabControlTestInfo.TabPages.Remove(tabTestSetting);
            //LoadTests();
            //LoadTests2();
            MainForm_Load(sender, e);
            LoadDefaultTestCase();
            listBox1Pro = listTestSteps.Width/(double)tabControlTestInfo.Width;
            listBox2Pro = listBox2.Width / (double)tabControlTestInfo.Width;
            btnPro = btnAddStep.Width / (double)tabControlTestInfo.Width;
        }

        private void LoadTests()
        {
            ITest[] tests = new ITest[] { 
                new FlowTest2("TD_COAP_CORE_1_1", "Perform GET transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_1_1\t\r\nObjective:\tPerform GET transaction (CON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers the resource /test that handle2 GET with an arbitrary payload\r\n\r\nTest Sequence:\r\n\tStep 1. Send a GET request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 1 (GET)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- The same Message ID as that of the previous request\r\n\t\t- Content type option\r\n"),
                new FlowTest2("TD_COAP_CORE_1_2", "Perform GET transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_1_2\t\r\nObjective:\tPerform GET transaction (CON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers the resource /test that handle2 GET with an arbitrary payload\r\n\r\nTest Sequence:\r\n\tStep 1. Send a GET request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 1 (GET)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- The same Message ID as that of the previous request\r\n\t\t- Content type option\r\n"),
                new FlowTest2("TD_COAP_CORE_1_3", "Perform POST transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_1_3\r\nObjective:\tPerform POST transaction (CON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server accepts creation of new resource on /test (resource does not exists yet)\r\n\r\nTest Sequence:\r\n\tStep 1. Send a POST request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 2 (POST)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 65 (2.01 Created)\r\n\t\t- The same Message ID as that of the previous request\r\n"),
                new FlowTest2("TD_COAP_CORE_1_4", "Perform POST transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_1_4\r\nObjective:\tPerform POST transaction (CON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server accepts creation of new resource on /test (resource does not exists yet)\r\n\r\nTest Sequence:\r\n\tStep 1. Send a POST request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 2 (POST)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 65 (2.01 Created)\r\n\t\t- The same Message ID as that of the previous request\r\n"),
                new FlowTest2("TD_COAP_CORE_1_5", "Perform PUT transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_1_5\r\nObjective:\tPerform PUT transaction (CON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles PUT\r\n\r\nTest Sequence:\r\n\tStep 1. Send a PUT request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 3 (PUT)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 68 (2.04 Changed)\r\n\t\t- The same Message ID as that of the previous request\r\n"),
                new FlowTest2("TD_COAP_CORE_1_6", "Perform PUT transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_1_6\r\nObjective:\tPerform PUT transaction (CON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles PUT\r\n\r\nTest Sequence:\r\n\tStep 1. Send a PUT request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 3 (PUT)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 68 (2.04 Changed)\r\n\t\t- The same Message ID as that of the previous request\r\n"),
                new FlowTest2("TD_COAP_CORE_1_7", "Perform DELETE transaction (CON mode)",
                   "Identifier:\tTD_COAP_CORE_1_7\r\nObjective:\tPerform DELETE transaction (CON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles DELETE\r\n\r\nTest Sequence:\r\n\tStep 1. Send a DELETE request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 4 (DELETE)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 66 (2.02 Deleted)\r\n\t\t- The same Message ID as that of the previous request\r\n"),
                new FlowTest2("TD_COAP_CORE_1_8", "Perform DELETE transaction (CON mode)",
                   "Identifier:\tTD_COAP_CORE_1_8\r\nObjective:\tPerform DELETE transaction (CON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles DELETE\r\n\r\nTest Sequence:\r\n\tStep 1. Send a DELETE request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 4 (DELETE)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 66 (2.02 Deleted)\r\n\t\t- The same Message ID as that of the previous request\r\n"),
                new FlowTest2("TD_COAP_CORE_1_9", "Perform GET transaction with a separate response",
                    "Identifier:\tTD_COAP_CORE_1_9\r\nObjective:\tPerform GET transaction with a separate response (expected response)\r\nConfiguration:\tCoAP_CFG_02\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /separate which cannot be served immediately and which cannot be acknowledged in a piggy-backed way.\r\n\r\nTest Sequence:\r\n\tStep 1. Send a request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 1 (GET)\r\n\t\t- Client generated Message ID\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 2 (ACK)\r\n\t\t- Message ID same as the request\r\n\t\t- empty Payload\r\n\tStep 3. Check server's response containing:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- Payload = Content of the requested resource\r\n\t\t- Content type option\r\n\tStep 4. Send a response with:\r\n\t\t- Type = 2 (ACK)\r\n\t\t- Message ID same as the response\r\n\t\t- empty Payload\r\n"),
                new FlowTest2("TD_COAP_CORE_1_10", "Perform GET transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_1_10\t\r\nObjective:\tPerform GET transaction (NON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers the resource /test that handles GET\r\n\r\nTest Sequence:\r\n\tStep 1. Send a GET request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 1 (GET)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- Content type option\r\n"),
                new FlowTest2("TD_COAP_CORE_1_11", "Perform GET transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_1_11\t\r\nObjective:\tPerform GET transaction (NON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers the resource /test that handles GET\r\n\r\nTest Sequence:\r\n\tStep 1. Send a GET request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 1 (GET)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- Content type option\r\n"),
                new FlowTest2("TD_COAP_CORE_1_12", "Perform POST transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_1_12\r\nObjective:\tPerform POST transaction (NON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server accepts creation of new resource on /test (resource does not exists yet)\r\n\r\nTest Sequence:\r\n\tStep 1. Send a POST request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 2 (POST)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 65 (2.01 Created)\r\n"),
                new FlowTest2("TD_COAP_CORE_1_13", "Perform POST transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_1_13\r\nObjective:\tPerform POST transaction (NON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server accepts creation of new resource on /test (resource does not exists yet)\r\n\r\nTest Sequence:\r\n\tStep 1. Send a POST request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 2 (POST)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 65 (2.01 Created)\r\n"),
                new FlowTest2("TD_COAP_CORE_1_14", "Perform PUT transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_1_14\r\nObjective:\tPerform PUT transaction (NON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles PUT\r\n\r\nTest Sequence:\r\n\tStep 1. Send a PUT request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 3 (PUT)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 68 (2.04 Changed)\r\n"),
                new FlowTest2("TD_COAP_CORE_1_15", "Perform PUT transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_1_15\r\nObjective:\tPerform PUT transaction (NON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles PUT\r\n\r\nTest Sequence:\r\n\tStep 1. Send a PUT request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 3 (PUT)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 68 (2.04 Changed)\r\n"),
                new FlowTest2("TD_COAP_CORE_1_16", "Perform DELETE transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_1_16\r\nObjective:\tPerform DELETE transaction (NON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles DELETE\r\n\r\nTest Sequence:\r\n\tStep 1. Send a DELETE request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 4 (DELETE)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 66 (2.02 Deleted)\r\n"),
                new FlowTest2("TD_COAP_CORE_1_17", "Perform DELETE transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_1_17\r\nObjective:\tPerform DELETE transaction (NON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles DELETE\r\n\r\nTest Sequence:\r\n\tStep 1. Send a DELETE request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 4 (DELETE)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 66 (2.02 Deleted)\r\n")
            };

            treeViewCases.Nodes[0].Nodes.Clear();
            foreach (var test in tests)
            {
                treeViewCases.Nodes[0].Nodes.Add(CreateNode(test));
            }
            treeViewCases.ExpandAll();
        }

        private void LoadTests2()
        {
            ITest[] tests = new ITest[] { 
                new FlowTest("TD_COAP_CORE_2_1", "Perform GET transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_2_1\t\r\nObjective:\tPerform GET transaction (CON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers the resource /test that handle2 GET with an arbitrary payload\r\n\r\nTest Sequence:\r\n\tStep 1. Send a GET request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 1 (GET)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- The same Message ID as that of the previous request\r\n\t\t- Content type option\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.GET))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))
                        .Add(Checks.CodeEqual(Code.Content))
                        .Add(Checks.MessageIdMatch())
                        .Add(Checks.HasOption(OptionType.ContentType)))),
                new FlowTest("TD_COAP_CORE_2_2", "Perform GET transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_2_2\t\r\nObjective:\tPerform GET transaction (CON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers the resource /test that handle2 GET with an arbitrary payload\r\n\r\nTest Sequence:\r\n\tStep 1. Send a GET request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 1 (GET)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- The same Message ID as that of the previous request\r\n\t\t- Content type option\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.GET))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))
                        .Add(Checks.CodeEqual(Code.Empty))
                        .Add(Checks.MessageIdMatch())
                        .Add(Checks.HasOption(OptionType.ContentType)))),
                new FlowTest("TD_COAP_CORE_2_3", "Perform POST transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_2_3\r\nObjective:\tPerform POST transaction (CON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server accepts creation of new resource on /test (resource does not exists yet)\r\n\r\nTest Sequence:\r\n\tStep 1. Send a POST request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 2 (POST)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 65 (2.01 Created)\r\n\t\t- The same Message ID as that of the previous request\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.POST))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))
                        .Add(RequestBuilders.Payload("TD_COAP_CORE_2_3", MediaType.TextPlain))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))
                        .Add(Checks.CodeEqual(Code.Created))
                        .Add(Checks.MessageIdMatch()))),
                new FlowTest("TD_COAP_CORE_2_4", "Perform POST transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_2_4\r\nObjective:\tPerform POST transaction (CON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server accepts creation of new resource on /test (resource does not exists yet)\r\n\r\nTest Sequence:\r\n\tStep 1. Send a POST request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 2 (POST)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 65 (2.01 Created)\r\n\t\t- The same Message ID as that of the previous request\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.POST))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))
                        .Add(RequestBuilders.Payload("TD_COAP_CORE_2_4", MediaType.TextPlain))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))
                        .Add(Checks.CodeEqual(Code.Empty))
                        .Add(Checks.MessageIdMatch()))),
                new FlowTest("TD_COAP_CORE_2_5", "Perform PUT transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_2_5\r\nObjective:\tPerform PUT transaction (CON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles PUT\r\n\r\nTest Sequence:\r\n\tStep 1. Send a PUT request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 3 (PUT)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 68 (2.04 Changed)\r\n\t\t- The same Message ID as that of the previous request\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.PUT))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))
                        .Add(RequestBuilders.Payload("TD_COAP_CORE_2_5", MediaType.TextPlain))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))
                        .Add(Checks.CodeEqual(Code.Changed))
                        .Add(Checks.MessageIdMatch()))),
                new FlowTest("TD_COAP_CORE_2_6", "Perform PUT transaction (CON mode)",
                    "Identifier:\tTD_COAP_CORE_2_6\r\nObjective:\tPerform PUT transaction (CON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles PUT\r\n\r\nTest Sequence:\r\n\tStep 1. Send a PUT request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 3 (PUT)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 68 (2.04 Changed)\r\n\t\t- The same Message ID as that of the previous request\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.PUT))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))
                        .Add(RequestBuilders.Payload("TD_COAP_CORE_2_6", MediaType.TextPlain))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))
                        .Add(Checks.CodeEqual(Code.Empty))
                        .Add(Checks.MessageIdMatch()))),
               new FlowTest("TD_COAP_CORE_2_7", "Perform DELETE transaction (CON mode)",
                   "Identifier:\tTD_COAP_CORE_2_7\r\nObjective:\tPerform DELETE transaction (CON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles DELETE\r\n\r\nTest Sequence:\r\n\tStep 1. Send a DELETE request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 4 (DELETE)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 66 (2.02 Deleted)\r\n\t\t- The same Message ID as that of the previous request\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.DELETE))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))
                        .Add(Checks.CodeEqual(Code.Deleted))
                        .Add(Checks.MessageIdMatch()))),
               new FlowTest("TD_COAP_CORE_2_8", "Perform DELETE transaction (CON mode)",
                   "Identifier:\tTD_COAP_CORE_2_8\r\nObjective:\tPerform DELETE transaction (CON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles DELETE\r\n\r\nTest Sequence:\r\n\tStep 1. Send a DELETE request with:\r\n\t\t- Type = 0 (CON)\r\n\t\t- Code = 4 (DELETE)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Code = 66 (2.02 Deleted)\r\n\t\t- The same Message ID as that of the previous request\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.DELETE))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))
                        .Add(Checks.CodeEqual(Code.Empty))
                        .Add(Checks.MessageIdMatch()))),
                new FlowTest("TD_COAP_CORE_2_9", "Perform GET transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_2_9\t\r\nObjective:\tPerform GET transaction (NON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers the resource /test that handles GET\r\n\r\nTest Sequence:\r\n\tStep 1. Send a GET request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 1 (GET)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- Content type option\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.MessageType(MessageType.NON))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))
                        .Add(Checks.CodeEqual(Code.Content))
                        .Add(Checks.HasOption(OptionType.ContentType)))),
                new FlowTest("TD_COAP_CORE_2_10", "Perform GET transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_2_10\t\r\nObjective:\tPerform GET transaction (NON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers the resource /test that handles GET\r\n\r\nTest Sequence:\r\n\tStep 1. Send a GET request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 1 (GET)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 69 (2.05 Content)\r\n\t\t- Content type option\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.MessageType(MessageType.NON))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))
                        .Add(Checks.CodeEqual(Code.Empty))
                        .Add(Checks.HasOption(OptionType.ContentType)))),
                new FlowTest("TD_COAP_CORE_2_11", "Perform POST transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_2_11\r\nObjective:\tPerform POST transaction (NON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server accepts creation of new resource on /test (resource does not exists yet)\r\n\r\nTest Sequence:\r\n\tStep 1. Send a POST request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 2 (POST)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 65 (2.01 Created)\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.POST))
                        .Add(RequestBuilders.MessageType(MessageType.NON))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))
                        .Add(RequestBuilders.Payload("TD_COAP_CORE_2_11", MediaType.TextPlain))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))
                        .Add(Checks.CodeEqual(Code.Created)))),
                new FlowTest("TD_COAP_CORE_2_12", "Perform POST transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_2_12\r\nObjective:\tPerform POST transaction (NON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server accepts creation of new resource on /test (resource does not exists yet)\r\n\r\nTest Sequence:\r\n\tStep 1. Send a POST request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 2 (POST)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 65 (2.01 Created)\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.POST))
                        .Add(RequestBuilders.MessageType(MessageType.NON))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))
                        .Add(RequestBuilders.Payload("TD_COAP_CORE_2_12", MediaType.TextPlain))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))
                        .Add(Checks.CodeEqual(Code.Empty)))),
                new FlowTest("TD_COAP_CORE_2_13", "Perform PUT transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_2_13\r\nObjective:\tPerform PUT transaction (NON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles PUT\r\n\r\nTest Sequence:\r\n\tStep 1. Send a PUT request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 3 (PUT)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 68 (2.04 Changed)\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.PUT))
                        .Add(RequestBuilders.MessageType(MessageType.NON))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))
                        .Add(RequestBuilders.Payload("TD_COAP_CORE_2_13", MediaType.TextPlain))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))
                        .Add(Checks.CodeEqual(Code.Changed)))),
                new FlowTest("TD_COAP_CORE_2_14", "Perform PUT transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_2_14\r\nObjective:\tPerform PUT transaction (NON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles PUT\r\n\r\nTest Sequence:\r\n\tStep 1. Send a PUT request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 3 (PUT)\r\n\t\t- An arbitrary payload\r\n\t\t- Content type option\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 68 (2.04 Changed)\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.PUT))
                        .Add(RequestBuilders.MessageType(MessageType.NON))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))
                        .Add(RequestBuilders.Payload("TD_COAP_CORE_2_14", MediaType.TextPlain))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))
                        .Add(Checks.CodeEqual(Code.Empty)))),
                new FlowTest("TD_COAP_CORE_2_15", "Perform DELETE transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_2_15\r\nObjective:\tPerform DELETE transaction (NON mode) (expected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles DELETE\r\n\r\nTest Sequence:\r\n\tStep 1. Send a DELETE request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 4 (DELETE)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 66 (2.02 Deleted)\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.DELETE))
                        .Add(RequestBuilders.MessageType(MessageType.NON))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))
                        .Add(Checks.CodeEqual(Code.Deleted)))),
                new FlowTest("TD_COAP_CORE_2_16", "Perform DELETE transaction (NON mode)",
                    "Identifier:\tTD_COAP_CORE_2_16\r\nObjective:\tPerform DELETE transaction (NON mode) (unexpected response)\r\nConfiguration:\tCoAP_CFG_01\r\n\r\nPre-test conditions:\r\n\t- Server offers a resource /test that handles DELETE\r\n\r\nTest Sequence:\r\n\tStep 1. Send a DELETE request with:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 4 (DELETE)\r\n\tStep 2. Check server's response containing:\r\n\t\t- Type = 1 (NON)\r\n\t\t- Code = 66 (2.02 Deleted)\r\n")
                    .AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.DELETE))
                        .Add(RequestBuilders.MessageType(MessageType.NON))
                        .Add(RequestBuilders.Option(OptionType.UriPath, "/test"))))
                    .AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))
                        .Add(Checks.CodeEqual(Code.Empty))))
            };
            treeViewCases.Nodes.Add("Group1");
            treeViewCases.Nodes[0].Nodes.Clear();
            foreach (var test in tests)
            {
                treeViewCases.Nodes[0].Nodes.Add(CreateNode(test));
            }
            treeViewCases.ExpandAll();
        }

        private void BuildFlowTest(FlowTest ft)
        {
            foreach (TestCaseBase tcb in ft.TC.tcbList)
            {
                if (tcb.IsRequest)
                {
                    TestCaseRequest tcRequest = tcb as TestCaseRequest;
                    GroupBuilder groupBuilder = RequestBuilders.Group();
                    RequestBuild(tcRequest, groupBuilder);
                    ft.AddStep(Steps.BuildRequest(groupBuilder));
                }
                else
                {
                    TestCaseResponse tcResponse = tcb as TestCaseResponse;
                    GroupCheck groupCheck = Checks.Group();
                    ResponseBuilder(tcResponse, groupCheck);
                    ft.AddStep(Steps.CheckResponse(groupCheck));
                }
            }
        }
        private void RequestBuild(TestCaseRequest tcRequest, GroupBuilder groupBuilder)
        {
            #region tcRequest builder
            if (!String.IsNullOrEmpty(tcRequest.Method.Trim()))
            {
                Method method = (Method)Enum.Parse(typeof(Method), tcRequest.Method.Trim(), true);
                groupBuilder.Add(RequestBuilders.Method(method));
            }
            if (!String.IsNullOrEmpty(tcRequest.MessageType.Trim()))
            {
                MessageType messageType = (MessageType)Enum.Parse(typeof(MessageType), tcRequest.MessageType.Trim(), true);
                groupBuilder.Add(RequestBuilders.MessageType(messageType));
            }
            if (!String.IsNullOrEmpty(tcRequest.PayLoad))
            {
                int mediaType = -1;
                int.TryParse(tcRequest.OptionType.Content_Format, out mediaType);
                if (tcRequest.PayLoad.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    Byte[] payload = Utils.ToBytes(tcRequest.PayLoad);
                    if (payload != null)
                        groupBuilder.Add(RequestBuilders.Payload(payload, mediaType));
                }
                else
                    groupBuilder.Add(RequestBuilders.Payload(tcRequest.PayLoad, mediaType));

            }
            if (!String.IsNullOrEmpty(tcRequest.Token))
            {
                Byte[] token;
                if (tcRequest.Token.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                    token = Utils.ToBytes(tcRequest.Token);
                else
                    token = System.Text.Encoding.UTF8.GetBytes(tcRequest.Token);
                groupBuilder.Add(RequestBuilders.Token(token));
            }

            #region tcRequest.OptionType
            OptionTypes ot = tcRequest.OptionType;
            if (!String.IsNullOrEmpty(ot.Accept))
            {
                int accept = -1;
                int.TryParse(ot.Accept, out accept);
                groupBuilder.Add(RequestBuilders.Option(OptionType.Accept, accept));
            }
            //if (!String.IsNullOrEmpty(ot.Block1))
            //{
            //    int block1 = 0;
            //    if (int.TryParse(ot.Block1, out block1))
            //        groupBuilder.Add(RequestBuilders.Option(OptionType.Block1, block1));
            //    else
            //        groupBuilder.Add(RequestBuilders.Option(OptionType.Block1,ot.Block1));
            //}
            if (!String.IsNullOrEmpty(ot.Block2))
            {
                int block2 = 0;
                if (int.TryParse(ot.Block2, out block2))
                    groupBuilder.Add(RequestBuilders.Option(OptionType.Block2, block2));
                else
                    groupBuilder.Add(RequestBuilders.Option(OptionType.Block2, ot.Block2));
            }
            if (!String.IsNullOrEmpty(ot.Content_Format))
            {
                int mediaType = -1;
                if (int.TryParse(ot.Content_Format, out mediaType))
                    groupBuilder.Add(RequestBuilders.Option(OptionType.ContentType, mediaType));
            }
            if (!String.IsNullOrEmpty(ot.ETag))
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.ETag, ot.ETag));
            }
            if (!String.IsNullOrEmpty(ot.If_Match))
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.IfMatch, ot.If_Match));
            }
            if (ot.If_None_Match == "TRUE")
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.IfNoneMatch));
            }
            if (!String.IsNullOrEmpty(ot.Location_Path))
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.LocationPath, ot.Location_Path));
            }
            if (!String.IsNullOrEmpty(ot.Location_Query))
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.LocationQuery, ot.Location_Query));
            }
            if (!String.IsNullOrEmpty(ot.Max_Age))
            {
                int maxAge = -1;
                if (int.TryParse(ot.Max_Age, out maxAge))
                    groupBuilder.Add(RequestBuilders.Option(OptionType.MaxAge, maxAge));
                else
                    groupBuilder.Add(RequestBuilders.Option(OptionType.MaxAge, ot.Max_Age));
            }
            if (!String.IsNullOrEmpty(ot.Observe))
            {
                int observe = -1;
                if (int.TryParse(ot.Observe, out observe))
                    groupBuilder.Add(RequestBuilders.Option(OptionType.Observe, observe));
                else
                    groupBuilder.Add(RequestBuilders.Option(OptionType.Observe, ot.Observe));
            }
            if (ot.Proxy_Scheme == "TRUE")
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.ProxyScheme));
            }
            if (!String.IsNullOrEmpty(ot.Proxy_Uri))
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.ProxyUri, ot.Proxy_Uri));
            }

            if (!String.IsNullOrEmpty(ot.Uri_Host))
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.UriHost, ot.Uri_Host));
            }
            if (!String.IsNullOrEmpty(ot.Uri_Port))
            {
                int port = -1;
                if (int.TryParse(ot.Uri_Port, out port))
                    groupBuilder.Add(RequestBuilders.Option(OptionType.UriPort, port));
                else
                    groupBuilder.Add(RequestBuilders.Option(OptionType.UriPort, ot.Uri_Port));
            }
            if (!String.IsNullOrEmpty(ot.UriPath))
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.UriPath, ot.UriPath));
            }
            if (!String.IsNullOrEmpty(ot.UriQuery))
            {
                groupBuilder.Add(RequestBuilders.Option(OptionType.UriQuery, ot.UriQuery));
            }
            #endregion

            #endregion
        }
        private void ResponseBuilder(TestCaseResponse tcResponse, GroupCheck groupCheck)
        {
            #region response check
            if (!string.IsNullOrEmpty(tcResponse.MessageType))
            {
                MessageType messageType = (MessageType)Enum.Parse(typeof(MessageType), tcResponse.MessageType.Trim(), true);
                groupCheck.Add(Checks.MessageTypeEqual(messageType));
            }
            if (!string.IsNullOrEmpty(tcResponse.Code))
            {
                int code = -1;
                int.TryParse(tcResponse.Code, out code);
                groupCheck.Add(Checks.CodeEqual(code));
            }
            if (!string.IsNullOrEmpty(tcResponse.SameMID))
            {
                if (tcResponse.SameMID.Trim().ToUpper() == "TRUE")
                    groupCheck.Add(Checks.MessageIdMatch());
            }
            else if (tcResponse.SameMID != null)
            {
                if (tcResponse.SameMID.Trim().ToUpper() == "TRUE")
                    groupCheck.Add(Checks.MessageIdMatch());
            }
            if (tcResponse.PayLoad != null)
            {
                string payLoad = tcResponse.PayLoad;
                if (payLoad.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    byte[] bytes = Utils.ToBytes(payLoad);
                    groupCheck.Add(Checks.PayLoadByteEqual(bytes));
                }
                else if (payLoad.Trim().Length > 0)
                {
                    groupCheck.Add(Checks.PayLoadStringEqual(payLoad));
                }
                else
                {
                    groupCheck.Add(Checks.ExistCheckPayLoad());
                }
            }
            if (tcResponse.Token != null)
            {
                string token = tcResponse.Token;
                if (token.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    byte[] bytes = Utils.ToBytes(token);
                    groupCheck.Add(Checks.TokenByteEqual(bytes));
                }
                else if (token.Trim().Length > 0)
                {
                    groupCheck.Add(Checks.TokenStringEqual(token));
                }
                else
                {
                    groupCheck.Add(Checks.ExistCheckToken());
                }
            }


            #region tcRequest.OptionType
            OptionTypes ot = tcResponse.OptionType;
            if (!String.IsNullOrEmpty(ot.Accept))
            {
                int accept = -1;
                if (int.TryParse(ot.Accept, out accept))
                {
                    //groupCheck.Add(Checks.HasOption(OptionType.Accept));
                    groupCheck.Add(Checks.OptionEqual(OptionType.Accept, accept));
                }
            }
            else if (ot.Accept != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.Accept));
            }
            if (!String.IsNullOrEmpty(ot.Block1))
            {
                int block1 = -1;
                if (int.TryParse(ot.Block1, out block1))
                    groupCheck.Add(Checks.OptionEqual(OptionType.Block1, block1));
                else
                    groupCheck.Add(Checks.OptionEqual(OptionType.Block1, ot.Block1));
            }
            else if (ot.Block1 != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.Block1));
            }
            if (!String.IsNullOrEmpty(ot.Block2))
            {
                int block2 = -1;
                if (int.TryParse(ot.Block2, out block2))
                    groupCheck.Add(Checks.OptionEqual(OptionType.Block2, block2));
                else
                    groupCheck.Add(Checks.OptionEqual(OptionType.Block2, ot.Block2));
            }
            else if (ot.Block2 != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.Block2));
            }
            if (!String.IsNullOrEmpty(ot.Content_Format))
            {
                int mediaType = -1;
                if (int.TryParse(ot.Content_Format, out mediaType))
                {
                    //groupCheck.Add(Checks.HasOption(OptionType.ContentType));
                    groupCheck.Add(Checks.OptionEqual(OptionType.ContentType, mediaType));
                }
            }
            else if (ot.Content_Format != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.ContentType));
            }
            if (!String.IsNullOrEmpty(ot.ETag))
            {
                //groupCheck.Add(Checks.HasOption(OptionType.ETag));
                groupCheck.Add(Checks.OptionEqual(OptionType.ETag, ot.ETag));
            }
            else if (ot.ETag != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.ETag));
            }

            if (!String.IsNullOrEmpty(ot.If_Match))
            {
                groupCheck.Add(Checks.OptionEqual(OptionType.IfMatch, ot.If_Match));
            }
            else if (ot.If_Match != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.IfMatch));
            }

            if (ot.If_None_Match == "TRUE")
            {
                groupCheck.Add(Checks.HasOption(OptionType.IfNoneMatch));
            }

            if (!String.IsNullOrEmpty(ot.Location_Path))
            {
                groupCheck.Add(Checks.OptionEqual(OptionType.LocationPath, ot.Location_Path));
            }
            else if (ot.Location_Path != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.LocationPath));
            }

            if (!String.IsNullOrEmpty(ot.Location_Query))
            {
                groupCheck.Add(Checks.OptionEqual(OptionType.LocationQuery, ot.Location_Query));
            }
            else if (ot.Location_Query != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.LocationQuery));
            }

            if (!String.IsNullOrEmpty(ot.Max_Age))
            {
                int maxAge = -1;
                if (int.TryParse(ot.Max_Age, out maxAge))
                    groupCheck.Add(Checks.OptionEqual(OptionType.MaxAge, maxAge));
                else
                    groupCheck.Add(Checks.OptionEqual(OptionType.MaxAge, ot.Max_Age));
            }
            else if (ot.Max_Age != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.MaxAge));
            }

            if (!String.IsNullOrEmpty(ot.Observe))
            {
                int observe = -1;
                if (int.TryParse(ot.Observe, out observe))
                    groupCheck.Add(Checks.OptionEqual(OptionType.Observe, observe));
                else
                    groupCheck.Add(Checks.OptionEqual(OptionType.Observe, ot.Observe));
            }
            else if (ot.Observe != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.Observe));
            }

            if (ot.Proxy_Scheme == "TRUE")
            {
                groupCheck.Add(Checks.HasOption(OptionType.ProxyScheme));
            }

            if (!String.IsNullOrEmpty(ot.Proxy_Uri))
            {
                groupCheck.Add(Checks.OptionEqual(OptionType.ProxyUri, ot.Proxy_Uri));
            }
            else if (ot.Proxy_Uri != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.ProxyUri));
            }

            if (!String.IsNullOrEmpty(ot.Uri_Host))
            {
                groupCheck.Add(Checks.OptionEqual(OptionType.UriHost, ot.Uri_Host));
            }
            else if (ot.Uri_Host != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.UriHost));
            }

            if (!String.IsNullOrEmpty(ot.Uri_Port))
            {
                int port = -1;
                if (int.TryParse(ot.Uri_Port, out port))
                    groupCheck.Add(Checks.OptionEqual(OptionType.UriPort, port));
                else
                    groupCheck.Add(Checks.OptionEqual(OptionType.UriPort, ot.Uri_Port));
            }
            else if (ot.Uri_Port != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.UriPort));
            }

            if (!String.IsNullOrEmpty(ot.UriPath))
            {
                groupCheck.Add(Checks.OptionEqual(OptionType.UriPath, ot.UriPath));
            }
            else if (ot.UriPath != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.UriPath));
            }

            if (!String.IsNullOrEmpty(ot.UriQuery))
            {
                groupCheck.Add(Checks.OptionEqual(OptionType.UriQuery, ot.UriQuery));
            }
            else if (ot.UriQuery != null)
            {
                groupCheck.Add(Checks.HasOption(OptionType.UriQuery));
            }
            #endregion

            #endregion
        }

        private void LoadTestCase(List<FlowTest> ftList)
        {
            foreach (FlowTest ft in ftList)
            {
                ft.Description = "Identifier:\t" + ft.TC.Identifier + "\r\nObjective:\t" + ft.TC.Objective + "\r\nConfiguration:\t" + ft.TC.Configuration + "\r\n\r\nPre-test conditions:\r\n\t" + ft.TC.Pre_testconditions + "\r\n\r\nTest Sequence:\r\n\t" + ft.TC.TestSequence;
                BuildFlowTest(ft);
                int number = -1;
                if (!groupList.Contains(ft.TC.Group.Trim()))
                {
                    groupList.Add(ft.TC.Group.Trim());
                    number = treeViewCases.Nodes.Count;
                    treeViewCases.Nodes.Add(ft.TC.Group.Trim());
                }
                else
                {
                    int count = treeViewCases.Nodes.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (treeViewCases.Nodes[i].Text == ft.TC.Group)
                        {
                            number = i;
                            break;
                        }
                    }
                }
                TreeNode treeNode = CreateNode((ITest)ft);               
                treeViewCases.Nodes[number].Nodes.Add(treeNode);
            }
        }

        private TreeNode CreateNode(ITest test)
        {
            TreeNode node = new TreeNode(test.Name);
            node.ToolTipText = test.Brief;
            node.ForeColor = Color.Gray;
            node.Tag = test;
            return node;
        }

        private void DisplayTestInfo(ITest test)
        {
            if (test == null)
            {
                //tbxDescription.Text = String.Empty;
                ////tbxSummary.Text = String.Empty;
                listTestSteps.DataSource = null;
                tsbtnRemove.Enabled = false;
                btnSave.Tag = null;

                txtIdentifier.Text = txtObjective.Text = txtConfiguration.Text = txtPreTestCon.Text = txtTestSequence.Text = "";
            }
            else
            {
                //tbxDescription.Text = test.Description;
                ////tbxSummary.Text = test.Summary;

                FlowTest flowTest = test as FlowTest;
                if (flowTest != null)
                {
                    listTestSteps.Tag = flowTest;
                    listTestSteps.DataSource = flowTest.Steps;
                }
                tsbtnRemove.Enabled = true;

                btnSave.Tag = test;
                FlowTest ft = (btnSave.Tag as FlowTest);
                if (ft.TC != null)
                {
                    txtIdentifier.Text = ft.TC.Name;
                    txtObjective.Text = ft.TC.Objective;
                    txtConfiguration.Text = ft.TC.Configuration;
                    txtPreTestCon.Text = ft.TC.Pre_testconditions;
                    txtTestSequence.Text = ft.TC.TestSequence;
                }
                tabControlTestInfo.SelectedIndex = 0;
            }
        }

        private void DisplayStep(IStep step)
        {
            if (step is RequestBuildStep)
            {
                RequestBuildStepForm form = new RequestBuildStepForm();
                form.Step = step as RequestBuildStep;
                form.ShowDialog();
            }
            else if (step is ResponseCheckStep)
            {
                ResponseCheckStepForm form = new ResponseCheckStepForm();
                form.Step = step as ResponseCheckStep;
                form.ShowDialog();
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = (listTestSteps.Tag as FlowTest).Steps;
            listTestSteps.DataSource = bs;
            listTestSteps.SelectedItem = step;
        }

        private void CheckAll(TreeNode node)
        {
            treeViewCases.SuspendLayout();
            foreach (TreeNode child in node.Nodes)
            {
                if (child.Checked != node.Checked)
                    child.Checked = node.Checked;
            }
            treeViewCases.ResumeLayout();
        }

        private void CheckIfAll(TreeNode node)
        {
            Boolean allChecked = true;
            foreach (TreeNode child in node.Nodes)
            {
                if (!child.Checked)
                {
                    allChecked = false;
                    break;
                }
            }
            node.Checked = allChecked;
        }

        private void treeViewCases_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                e.Node.ForeColor = e.Node.Checked ? Color.Empty : Color.Gray;
                if (e.Action != TreeViewAction.Unknown)
                    CheckIfAll(e.Node.Parent);
            }
            else if (e.Action != TreeViewAction.Unknown)
            {
                CheckAll(e.Node);
            }
        }

        private void treeViewCases_AfterSelect(object sender, TreeViewEventArgs e)
        {
            DisplayTestInfo(e.Node.Tag as ITest);
        }

        private void tsbtnRun_Click(object sender, EventArgs e)
        {
            //backgroundWorkerTest = new BackgroundWorker();
            RunTestCase();
        }

        private void RunTestCase()
        {
            //tcItemList.Clear();
            //treeListView1.Items.Clear();
            Run();
        }

        private void tsbtnPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void tsbtnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void backgroundWorkerTest_DoWork(object sender, DoWorkEventArgs e)
        {
            //SetStatusVisibility(true);
            this.Invoke(statusOperation, true);
            statusBarOperation = new StatusBarOperation(UpdateProgressBar);
            
            RunTests(e.Argument.ToString());            
        }

        private void backgroundWorkerTest_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunningState = RunningState.Stopped;
            BindingResultToList();
            tabControlTestInfo.SelectedIndex = 2;
            System.Threading.Thread thread = new System.Threading.Thread(new ThreadStart(ThreadFun));
            thread.Start();
        }
        
        public void ThreadFun()
        {
            System.Threading.Thread.Sleep(5000);            
            this.Invoke(statusOperation, false);
        }
        
        private void SetStatusVisibility(bool isVisiable)
        {
            statusStrip2.Visible = isVisiable;
        }

        #region Bind Result to List
        private void BindingResultToList()
        {
            foreach (TestCaseItem tcItem in tcItemList)
                AddTreeListViewItem(treeListView1, tcItem);
        }

        private void AddTreeListViewItem(TreeListView tlView, TestCaseItem tcItem)
        {
            if (tlView.InvokeRequired)
            {
                tlView.Invoke(new Action<TreeListView, TestCaseItem>(AddTreeListViewItem), tlView, tcItem);
            }
            else
            {
                treeListView1.Items.Sortable = false;

                String passed = tcItem.IsPassed ? "通过" : "未通过";
                //TreeListViewItem tlvItem = new TreeListViewItem(passed, tcItem.IsPassed ? 0 : 1);
                TreeListViewItem tlvItem = new TreeListViewItem(passed, tcItem.IsPassed ? 2 : 3);
                tlvItem.Tag = tcItem;
                tlvItem.SubItems.Add(tcItem.Name);
                AddStepToTestCase(tlvItem, tcItem);
                treeListView1.Items.Add(tlvItem);
            }
        }

        private void AddStepToTestCase(TreeListViewItem tlvItem, TestCaseItem tcItem)
        {
            tlvItem.Items.Sortable = false;
            //tcItem.StepItemList.Reverse();
            foreach (StepItem stepItem in tcItem.StepItemList)
            {
                TreeListViewItem tlvStep;
                String title = "";
                title = stepItem.IsRequest ? "请求" : "响应";
                tlvStep = new TreeListViewItem(title,stepItem.IsRequest?4:5);
                tlvStep.SubItems.Add(stepItem.Name);
                AddBaseErroToStep(tlvStep, stepItem);
                tlvItem.Items.Add(tlvStep);

            }
        }

        private void AddBaseErroToStep(TreeListViewItem tlvStep, StepItem stepItem)
        {
            tlvStep.Items.Sortable = false;
            //stepItem.RowItemList.Reverse();
            foreach (RowItem rowItem in stepItem.RowItemList)
            {
                //TreeListViewItem tlvRow = new TreeListViewItem(rowItem.Name);
                TreeListViewItem tlvRow = new TreeListViewItem("");
                tlvRow.SubItems.Add(rowItem.Name);
                tlvRow.SubItems.Add(rowItem.Expected);
                tlvRow.SubItems.Add(rowItem.Received);
                tlvRow.SubItems.Add(rowItem.ErrorInfo);
                tlvStep.Items.Add(tlvRow);
            }
        }
        #endregion

        private void listTestSteps_DoubleClick(object sender, EventArgs e)
        {
            IStep step = listTestSteps.SelectedItem as IStep;
            if (step != null)
                DisplayStep(step);
        }

        private void tsbtnRemove_Click(object sender, EventArgs e)
        {
            DeleteTestCase();
        }

        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            AddTestCase();
        }

        private void AddTestCase()
        {
            #region new testcase
            String groupName = "";
            TreeNode root;
            if (treeViewCases.SelectedNode == null)
            {
                if (treeViewCases.Nodes.Count > 0)
                {
                    root = treeViewCases.Nodes[0];
                }
                else
                {
                    groupName = "组1";
                    root = treeViewCases.Nodes.Add(groupName);
                    groupList.Add(groupName);
                }
            }
            else
            {
                if (treeViewCases.SelectedNode.Parent == null)
                    root = treeViewCases.SelectedNode;
                else
                    root = treeViewCases.SelectedNode.Parent;
            }
            groupName = root.Text;
            String name = "New Test";
            FlowTest ft = new FlowTest(name, null);
            TestCase tc = new TestCase();
            tc.Name = name;
            tc.Identifier = name;
            ft.TC = tc;
            tc.Group = groupName;

            TreeNode node = CreateNode(ft);
            root.Nodes.Add(node);
            treeViewCases.SelectedNode = node;
            node.BeginEdit();
            #endregion
        }

        private void DeleteTestCase()
        {
            if (treeViewCases.SelectedNode != null && treeViewCases.SelectedNode.Parent != null)
            {
                treeViewCases.SelectedNode.Remove();
            }
        }

        //private void tbxDescription_Leave(object sender, EventArgs e)
        //{
        //    if (treeViewCases.SelectedNode != null)
        //    {
        //        ITest test = treeViewCases.SelectedNode.Tag as ITest;
        //        if (test != null)
        //            test.Description = tbxDescription.Text;
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "修改")
            {
                if (btnSave.Tag != null)
                {
                    btnSave.Text = "保存";
                    TextBoxReadOnly(false);
                }
            }
            else
            {
                btnSave.Text = "修改";
                FlowTest ft = btnSave.Tag as FlowTest;
                TextBoxReadOnly(true);
                ft.Name= ft.TC.Name= ft.TC.Identifier = txtIdentifier.Text;
                if ((treeViewCases.SelectedNode.Tag as FlowTest) != null)
                {
                    treeViewCases.SelectedNode.Text = txtIdentifier.Text;
                }
                ft.TC.Objective = txtObjective.Text;
                ft.TC.Configuration = txtConfiguration.Text;
                ft.TC.Pre_testconditions = txtPreTestCon.Text;
                ft.TC.TestSequence = txtTestSequence.Text;
            }
        }

        private void TextBoxReadOnly(bool isReadOnly)
        {
            txtIdentifier.ReadOnly = isReadOnly;
            txtObjective.ReadOnly = isReadOnly;
            txtConfiguration.ReadOnly = isReadOnly;
            txtPreTestCon.ReadOnly = isReadOnly;
            txtTestSequence.ReadOnly = isReadOnly;
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                string strStep = listBox2.SelectedItem.ToString();
                FlowTest ft = listTestSteps.Tag as FlowTest;
                if (ft == null)
                    return;
                switch (strStep)
                {
                    case "Get Request":
                        ft.AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.GET))));
                        break;
                    case "Post Request":
                        ft.AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.POST))));
                        break;
                    case "Put Request":
                        ft.AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.PUT))));
                        break;
                    case "Delete Request":
                        ft.AddStep(Steps.BuildRequest(RequestBuilders.Group()
                        .Add(RequestBuilders.Method(Method.DELETE))));
                        break;
                    case "ACK Response":
                        ft.AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.ACK))));
                        break;
                    case "CON Response":
                        ft.AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.CON))));
                        break;
                    case "NON Response":
                        ft.AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.NON))));
                        break;
                    case "RST Response":
                        ft.AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.RST))));
                        break;
                    case "Unknown Response":
                        ft.AddStep(Steps.CheckResponse(Checks.Group()
                        .Add(Checks.MessageTypeEqual(MessageType.Unknown))));
                        break;
                }
                BindingSource bs = new BindingSource();
                bs.DataSource = ft.Steps;
                listTestSteps.DataSource = bs;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            IStep step = (IStep)listTestSteps.SelectedItem;

            FlowTest ft = listTestSteps.Tag as FlowTest;
            List<IStep> stepList = (List<IStep>)ft.Steps;
            stepList.Remove(step);

            BindingSource bs = new BindingSource();
            bs.DataSource = ft.Steps;
            listTestSteps.DataSource = bs;
        }
        
        private void FormMain_Resize(object sender, EventArgs e)
        {
            MainForm_Resize(sender, e);

            listTestSteps.Width = Convert.ToInt32(tabControlTestInfo.Width * listBox1Pro);
            listBox2.Width = Convert.ToInt32(tabControlTestInfo.Width*listBox2Pro);
            btnAddStep.Width = Convert.ToInt32(tabControlTestInfo.Width*btnPro);
            btnRemove.Width = Convert.ToInt32(tabControlTestInfo.Width*btnPro);
            btnAddStep.Location = new Point(listTestSteps.Width + 17, 134);
            btnRemove.Location = new Point(btnAddStep.Location.X, 197);

            listBox2.Location = new Point(btnAddStep.Location.X+btnAddStep.Width+5,listBox2.Location.Y);

        }

        private void treeViewCases_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            FlowTest ft = e.Node.Tag as FlowTest;
            if (ft != null)
            {
                if (e.Label != null && e.Label.Trim() != "")
                {                     
                     ft.Name = ft.TC.Identifier = ft.TC.Name = txtIdentifier.Text = e.Label;                    
                }
            }
        }

        private void LoadDefaultTestCase()
        {
            String fileName = System.Windows.Forms.Application.StartupPath + "//standard.xml";
            if (File.Exists(fileName))
            {
                LoadFile(fileName);
                treeViewCases.ExpandAll();
            }
            else
            {
                MessageBox.Show("默认用例不存在");
            }
        }

        #region Menu Orders

        private void 默认用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDefaultTestCase();
        }

        private void 导入用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML文件|*.xml";
            if (DialogResult.OK == ofd.ShowDialog())
            {
                LoadFile(ofd.FileName);
                treeViewCases.ExpandAll();
            }
        }

        private void LoadFile(String fileName)
        {
            try
            {
                List<FlowTest> tcList = TestCaseXMLToObject.Read(fileName);
                LoadTestCase(tcList);
            }
            catch
            {
                MessageBox.Show("测试用例文件异常");
            }
        }

        private void 导出用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML文件|*.xml";
            sfd.FileName = "测试用例.xml";
            sfd.RestoreDirectory = true;
            if (DialogResult.OK == sfd.ShowDialog())
            {
                List<TestCase> tcList = new List<TestCase>();
                foreach (TreeNode node in treeViewCases.Nodes)
                {
                    foreach (TreeNode n in node.Nodes)
                    {
                        FlowTest ft = (FlowTest)(n.Tag as ITest);
                        TestCase tc = TestCaseXMLToObject.GetNewTestCase(ft);
                        tc.Name = n.Text;
                        tc.Group = node.Text;
                        tcList.Add(tc);
                    }
                }
                XmlDocument doc = TestCaseXMLToObject.CreateXML(tcList);
                doc.Save(sfd.FileName);
            }
        }

        private void 添加用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTestCase();
        }

        private void 删除用例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTestCase();
        }

        private void 开始检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunTestCase();
        }

        private void 暂停检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void 停止检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stop();
        }

        //private void 导入测试用例ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    ofd.Filter = "XML文件|*.xml";
        //    if (DialogResult.OK == ofd.ShowDialog())
        //    {
        //        //List<TestCaseItem> tcItemList = TestCaseResultIO.XMLToList(ofd.FileName);
        //        try
        //        {
        //            tcItemList = TestCaseResultIO.XMLToList(ofd.FileName);
        //            BindingResultToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("测试用例结果异常");
        //        }
        //    }
        //}

        private void 导出测试用例结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML文件|*.xml";
            sfd.FileName = "测试用例结果.xml";
            sfd.RestoreDirectory = true;
            if (DialogResult.OK == sfd.ShowDialog())
            {
                XmlDocument doc = TestCaseResultIO.ResultToXML(tcItemList);
                doc.Save(sfd.FileName);

                String htmlPath = sfd.FileName.Substring(0, sfd.FileName.LastIndexOf('.')) + ".html";
                //String xmlPath = System.Windows.Forms.Application.StartupPath + "//result.xml";
                //doc.Save(xmlPath);
                //MessageBox.Show(System.Environment.CurrentDirectory);

                XslCompiledTransform xslt = new XslCompiledTransform();
                String xslPath = System.Windows.Forms.Application.StartupPath + "//result.xsl";
                if (File.Exists(xslPath))
                {
                    xslt.Load(xslPath, new XsltSettings(true, true), null); //Load方法加载样式表，xslFile代表要加载的xslt样式表，如books.xsl
                    XPathDocument xDoc = new XPathDocument(sfd.FileName);  //使用指定文件中的XML数据进行初始化，如books.xml
                    XmlTextWriter xtWriter = new XmlTextWriter(htmlPath, Encoding.UTF8); //使用指定文件中创建XmlTextWriter类的实例，htmlFile就是要写入的文件。如果该文件存在，就截断该文件并用新内容对其进行改写，如books.html ,第二个参数是生成的编码方式。如果为空，则用UTF-8形式写出该文件。

                    xslt.Transform(xDoc, xtWriter); //Transform方法是实现格式转换，使用指定的输入文档执行转换，然后结果输出到XmlTextWriter
                    xtWriter.Close(); //Write the XML to file and close the writer

                    //System.Diagnostics.Process.Start("explorer.exe", htmlPath);
                    System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo(htmlPath);
                    System.Diagnostics.Process Pro = System.Diagnostics.Process.Start(Info);
                }
                else
                {
                    MessageBox.Show("result.xsl文件缺失");
                }
            }
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String path = System.Windows.Forms.Application.StartupPath + "//帮助文档.pdf";
            if (File.Exists(path))
            {
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(path);
                System.Diagnostics.Process.Start(info);
            }
            else
            {
                MessageBox.Show("帮助文档缺失");
            }
        }

        #endregion
        
        private void treeListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TreeListView tlView = sender as TreeListView;
            //ListView.SelectedListViewItemCollection slvic = tlView.SelectedItems;
            //if (slvic != null && slvic.Count > 0)
            //{
            //    ListViewItem lvItem = slvic[0];
            //    if (lvItem.Tag != null && lvItem.Tag is TestCaseItem)
            //    {
            //        foreach (TreeNode node in treeViewCases.Nodes)
            //        {
            //            foreach (TreeNode child in node.Nodes)
            //            {
            //                if (ReferenceEquals((child.Tag as FlowTest).TCItem, lvItem.Tag))
            //                {
            //                    treeViewCases.SelectedNode = child;
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}
           
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            int count = treeViewCases.Nodes.Count;
            count++;
            String groupName = String.Empty;
            bool isContinue = true;
            while (isContinue)
            {
                groupName = "组" + count;
                if (!IsGroupExist(groupName))
                    isContinue = false;
                count++;
            }
            treeViewCases.Nodes.Add(groupName);
        }
        private bool IsGroupExist(String groupName)
        {
            for (int i = 0; i < treeViewCases.Nodes.Count; i++)
            {
                if (groupName == treeViewCases.Nodes[i].Text)
                    return true;
            }
            return false;
        }
        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            if (treeViewCases.SelectedNode != null && treeViewCases.Nodes.Contains(treeViewCases.SelectedNode))
            {
                if (groupList.Contains(treeViewCases.SelectedNode.Text))
                {
                    groupList.Remove(treeViewCases.SelectedNode.Text);
                }
                treeViewCases.Nodes.Remove(treeViewCases.SelectedNode);
            }
            else
            {
                MessageBox.Show("请选中某个组");
            }            
        }

        #region browser

        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {

        }

         public String Url
        {
            get { return toolStripComboBoxUrl.Text.Trim(); }
            set { toolStripComboBoxUrl.Text = value; }
        }

        public String Payload
        {
            get { return toolStripTextBoxPayload.Text.Trim(); }
        }

        private void PerformCoAP(Method method, Boolean observe)
        {
            ClearDisplay();
            String urlString = this.Url;
            if (String.IsNullOrEmpty(urlString))
                return;
            
            Uri uri = new Uri(urlString);
            PerformCoAP(method, uri, true, observe);
        }

        private Request PerformCoAP(Method method, Uri uri, Boolean messageVisible, Boolean observe)
        {
            // 取消前一个request
            Request request = _currentRequest;
            if (request != null)
            {
                request.Canceled = true;
                request.Responding -= request_Responding;
                request.Respond -= request_Respond;
            }

            Observing = observe;

            request = new Request(method);
            _currentRequest = request;

            if (tsmiNON.Checked)
                request.Type = MessageType.NON;

            if (!tsmiRetranmissions.Checked)
                request.MaxRetransmit = -1;

            request.URI = uri;
            request.EndPoint = _coapEndPoint;

            if (observe)
                request.MarkObserve();

            if (_preferBlockSize > 0)
                request.SetOption(new BlockOption(OptionType.Block2, 0, BlockOption.EncodeSZX(_preferBlockSize), false));

            if (method == Method.POST || method == Method.PUT)
            {
                request.SetPayload(this.Payload, MediaType.TextPlain);
            }

            if (messageVisible)
            {
                _currentPayload.Clear();
                request.Responding += request_Responding;
                request.Respond += request_Respond;

                #region debug options
                // debug options
                //if (checkBoxDebug.Checked)
                //{
                //    if (comboBoxAccept.SelectedIndex > 0)
                //    {
                //        MediaTypeEntry entry = comboBoxAccept.SelectedItem as MediaTypeEntry;
                //        request.AddOption(Option.Create(OptionType.Accept, entry.Value));
                //    }

                //    CheckStringOption(request, OptionType.ProxyUri, textBoxProxyUri);
                //    CheckHexStringOption(request, OptionType.ETag, textBoxETag);
                //    CheckHexStringOption(request, OptionType.Token, textBoxToken);
                //    CheckStringOption(request, OptionType.UriHost, textBoxUriHost);
                //    CheckIntOption(request, OptionType.UriPort, textBoxUriPort);
                //    CheckHexStringOption(request, OptionType.IfMatch, textBoxIfMatch);
                //    CheckIntOption(request, OptionType.Observe, textBoxObserve);
                //} 
                #endregion
            }

            request.Send();
            return request;
        }

        void request_Respond(Object sender, ResponseEventArgs e)
        {
            HandleMessage(e.Response, e.Response.Payload);
        }

        void request_Responding(Object sender, ResponseEventArgs e)
        {
            Byte[] payload = e.Response.Payload;
            if (payload != null)
                _currentPayload.AddRange(payload);
            HandleMessage(e.Response, _currentPayload.ToArray());
            // Sleep to wait UI to react
            System.Threading.Thread.Sleep(30);
        }

        private void HandleMessage(Message msg, Byte[] payload)
        {
            if (msg != null)
            {
                if (msg.HasOption(OptionType.Observe) && !_observing)
                {
                    // TODO msg.Reject();
                    return;
                }
            }
            DisplayMessage(msg, payload);
        }

        private void CheckIntOption(Request request, OptionType type, TextBox textBox)
        {
            String text = textBox.Text.Trim();
            if (!String.IsNullOrEmpty(text))
            {
                Int32 val = 0;
                if (Int32.TryParse(text, out val))
                    request.AddOption(Option.Create(type, val));
            }
        }

        private void CheckStringOption(Request request, OptionType type, TextBox textBox)
        {
            String text = textBox.Text.Trim();
            if (!String.IsNullOrEmpty(text))
            {
                request.AddOption(Option.Create(type, text));
            }
        }

        private void CheckHexStringOption(Request request, OptionType type, TextBox textBox)
        {
            String text = textBox.Text.Trim();
            if (!String.IsNullOrEmpty(text))
            {
                if (text.StartsWith("0x"))
                {
                    text = text.Substring(2);
                    if (text.Length % 2 > 0)
                        text = "0" + text;
                    Byte[] tmp = new Byte[text.Length / 2];
                    for (Int32 i = 0, j = 0; i < text.Length; i += 2)
                    {
                        Int16 high = Int16.Parse(text[i].ToString(), System.Globalization.NumberStyles.HexNumber);
                        Int16 low = Int16.Parse(text[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                        tmp[j++] = Convert.ToByte(high * 16 + low);
                    }
                    request.AddOption(Option.Create(type, tmp));
                }
                else
                {
                    request.AddOption(Option.Create(type, text));
                }
            }
        }

        private void NavigateTo(String url)
        {
            if (String.IsNullOrEmpty(url))
                return;
            this.Url = url;
            //Discover();
        }

        private void ClearDisplay()
        {
            labelResponseStatus.Text = String.Empty;
            dataGridViewHeaders.DataSource = emptyHeaderRows;
            dataGridViewOptions.DataSource = emptyOptionRows;
            textBoxPayloadString.Text = "no response yet";
            textBoxPayloadRaw.Text = String.Empty;
            webBrowserPayload.DocumentStream = new MemoryStream(emptyPayload);
        }

        private void DisplayMessage(Message msg, Byte[] payload)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(_displayMessageHandler, msg, payload);
                return;
            }

            if (msg == null)
            {
                ClearDisplay();
                labelResponseStatus.Text = "Time out";
            }
            else
            {
                IEnumerable<Option> opts = msg.GetOptions();
                IList<Option> options = opts as IList<Option>;
                if (options == null)
                    options = new List<Option>(opts);

                labelResponseStatus.Text = msg.CodeString;
                if (Observing)
                {
                    if (msg.HasOption(OptionType.Observe))
                        labelResponseStatus.Text += " (Observing)";
                    else
                    {
                        if (Code.GetResponseClass(msg.Code) == Code.SuccessCode)
                            labelResponseStatus.Text += " (Not observable)";
                        Observing = false;
                    }
                }

                // show headers
                dataGridViewHeaders.DataSource = GetHeaderRows(msg, options);

                // show options
                dataGridViewOptions.DataSource = GetOptionRows(options);

                // show payload
                if (payload == null)
                    payload = emptyPayload;

                groupBoxPayload.Text = String.Format("Payload ({0} bytes)", payload.Length);
                textBoxPayloadString.Text = System.Text.Encoding.Default.GetString(payload);
                textBoxPayloadRaw.Text = BitConverter.ToString(payload).Replace('-', ' ');

                if (MediaType.IsImage(msg.ContentType))
                {
                    // show image

                    // doesn't work :(
                    //webBrowserPayload.DocumentStream = new MemoryStream(payloadBytes);

                    // save to a temp file
                    File.WriteAllBytes(_tempImagePath, payload);
                    String path = "file:///" + _tempImagePath.Replace('\\', '/');
                    webBrowserPayload.DocumentText = String.Format("<html><head><title></title></head><body><img src=\"{0}\" alt=\"\"></body></html>", path);
                }
                else
                {
                    webBrowserPayload.DocumentStream = new MemoryStream(payload);
                }
            }
        }

        private void DisableWebBrowserSounds()
        {
            // Disable sounds in webBrowser using API
            CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_DISABLE_NAVIGATION_SOUNDS, SET_FEATURE_ON_PROCESS, true);
        }

        private static List<OptionRow> GetOptionRows(IEnumerable<Option> options)
        {
            List<OptionRow> optionRows = new List<OptionRow>();
            foreach (Option opt in options)
            {
                optionRows.Add(new OptionRow(opt.Name, opt.ToString(), GetOptionInfo(opt)));
            }
            return optionRows;
        }

        private static String GetOptionInfo(Option opt)
        {
            switch (opt.Type)
            {
                case OptionType.ContentType:
                case OptionType.MaxAge:
                case OptionType.UriPort:
                case OptionType.Observe:
                    return opt.IntValue.ToString();
                case OptionType.ProxyUri:
                case OptionType.ETag:
                case OptionType.UriHost:
                case OptionType.LocationPath:
                case OptionType.LocationQuery:
                case OptionType.UriPath:
                case OptionType.Token:
                case OptionType.UriQuery:
                case OptionType.Block2:
                case OptionType.Block1:
                    return String.Format("{0} byte(s)", opt.Length);
                default:
                    return null;
            }
        }

        private static List<HeaderRow> GetHeaderRows(Message msg, IList<Option> options)
        {
            List<HeaderRow> headerRows = new List<HeaderRow>();
            headerRows.Add(new HeaderRow("Type", msg.Type.ToString()));
            headerRows.Add(new HeaderRow("Code", msg.CodeString));
            headerRows.Add(new HeaderRow("ID", msg.ID.ToString()));
            headerRows.Add(new HeaderRow("Options", options.Count.ToString()));
            return headerRows;
        }

        #region Observe

        private Boolean Observing
        {
            get { return _observing; }
            set 
            {
                _observing = value;
                toolStripButtonObserve.Text = _observing ? "Cancel" : "Observe";
            }
        }

        private void BeginObserving()
        {
            PerformCoAP(Method.GET, true);
        }

        private void EndObserving()
        {
            Observing = false;
        }

        #endregion

        #region Discover

        private void Discover()
        {
            String urlString = this.Url;
            if (String.IsNullOrEmpty(urlString))
                return;
            Uri baseUri = new Uri(urlString);
            Uri discoverUri = new Uri(GetAbsolutePath(baseUri) + CoapConstants.DefaultWellKnownURI);
            Request request = PerformCoAP(Method.GET, discoverUri, false, false);
            Action<Request> discoverHandler = new Action<Request>(HandleDiscover);
            discoverHandler.BeginInvoke(request, null, null);
        }

        private void HandleDiscover(Request request)
        {
            Response response = request.WaitForResponse();
            DisplayDiscover(request, response);
        }

        private void DisplayDiscover(Request request, Response response)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(_displayDiscoverHandler, request, response);
                return;
            }
            if (null == response)
            {
                // timeout
                groupBoxResources.Text = "Timeout";
            }
            else
            {
                groupBoxResources.Text = response.Source.ToString();
                Resource root = RemoteResource.NewRoot(response.PayloadString);
                RemoveResources();
                PopulateResources(root, GetAbsolutePath(request.URI));
            }
        }

        private void RemoveResources()
        {
            foreach (Button btnResource in flowLayoutPanelResources.Controls)
            {
                btnResource.Click -= this._resourceClickHandler;
            }
            flowLayoutPanelResources.SuspendLayout();
            flowLayoutPanelResources.Controls.Clear();
            flowLayoutPanelResources.ResumeLayout(false);
        }

        private void PopulateResources(Resource currentRoot, String baseUri)
        {
            flowLayoutPanelResources.SuspendLayout();
            Resource[] resources = currentRoot.GetSubResources();
            foreach (Resource res in resources)
            {
                if (res.SubResourceCount > 0)
                {
                    PopulateResources(res, baseUri);
                }
                else
                {
                    Button btnResource = new Button();
                    btnResource.Text = res.Path;
                    btnResource.Tag = baseUri + res.Path;
                    btnResource.AutoSize = true;
                    btnResource.Click += this._resourceClickHandler;
                    flowLayoutPanelResources.Controls.Add(btnResource);
                    toolTip1.SetToolTip(btnResource, res.Title);
                }
            }
            flowLayoutPanelResources.ResumeLayout(false);
            flowLayoutPanelResources.PerformLayout();
        }

        #endregion

        private static String GetAbsolutePath(Uri uri)
        {
            if ("/".Equals(uri.AbsolutePath) && !uri.OriginalString.EndsWith("/"))
                return uri.OriginalString;
            else
                return uri.OriginalString.Substring(0, uri.OriginalString.Length - uri.PathAndQuery.Length);
        }

        #region Assistant classes

        class HeaderRow
        {
            private String _header;
            private String _value;

            public HeaderRow(String header, String value)
            {
                _header = header;
                _value = value;
            }

            public String Header
            {
                get { return _header; }
                set { _header = value; }
            }

            public String Value
            {
                get { return _value; }
                set { _value = value; }
            }
        }

        class OptionRow
        {
            private String _option;
            private String _value;
            private String _info;

            public OptionRow(String option, String value, String info)
            {
                _option = option;
                _value = value;
                _info = info;
            }

            public String Option
            {
                get { return _option; }
                set { _option = value; }
            }

            public String Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public String Info
            {
                get { return _info; }
                set { _info = value; }
            }
        }

        class MediaTypeEntry
        {
            private Int32 _value;
            private String _name;

            public static readonly MediaTypeEntry[] All;

            static MediaTypeEntry()
            {
                All = new MediaTypeEntry[] { 
                    new MediaTypeEntry(-1, ""),
                    new MediaTypeEntry(0, "text/plain"),
                    new MediaTypeEntry(1, "text/xml"),
                    new MediaTypeEntry(2, "text/csv"),
                    new MediaTypeEntry(3, "text/html"),
                    new MediaTypeEntry(21, "image/gif"),
                    new MediaTypeEntry(22, "image/jpeg"),
                    new MediaTypeEntry(23, "image/png"),
                    new MediaTypeEntry(24, "image/tiff"),
                    new MediaTypeEntry(25, "audio/raw"),
                    new MediaTypeEntry(26, "video/raw"),
                    new MediaTypeEntry(40, "application/link-format"),
                    new MediaTypeEntry(41, "application/xml"),
                    new MediaTypeEntry(42, "application/octet-stream"),
                    new MediaTypeEntry(43, "application/rdf+xml"),
                    new MediaTypeEntry(44, "application/soap+xml"),
                    new MediaTypeEntry(45, "application/atom+xml"),
                    new MediaTypeEntry(46, "application/xmpp+xml"),
                    new MediaTypeEntry(47, "application/exi"),
                    new MediaTypeEntry(48, "application/xbx+xml"),
                    new MediaTypeEntry(49, "application/fastinfoset"),
                    new MediaTypeEntry(50, "application/soap+fastinfoset"),
                    new MediaTypeEntry(51, "application/json"),
                };
            }

            public MediaTypeEntry(Int32 value, String name)
            {
                _value = value;
                _name = name;
            }

            public Int32 Value
            {
                get { return _value; }
            }

            public String Name
            {
                get { return _name; }
            }
        }

        #endregion

        #region Event handlers for controls

        private void btnResource_Click(object sender, EventArgs e)
        {
            Button btnResource = sender as Button;
            NavigateTo(btnResource.Tag.ToString());
        }

        private void toolStripButtonGet_Click(object sender, EventArgs e)
        {
            PerformCoAP(Method.GET, false);
        }

        private void toolStripButtonPost_Click(object sender, EventArgs e)
        {
            PerformCoAP(Method.POST, false);
        }

        private void toolStripButtonPut_Click(object sender, EventArgs e)
        {
            PerformCoAP(Method.PUT, false);
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            PerformCoAP(Method.DELETE, false);
        }

        private void toolStripButtonObserve_Click(object sender, EventArgs e)
        {
            if (Observing)
            {
                EndObserving();
            }
            else
            {
                BeginObserving();
            }
        }

        private void toolStripButtonDiscover_Click(object sender, EventArgs e)
        {
            Discover();
        }

        private void toolStripComboBoxUrl_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Enter
            if (e.KeyChar == 13)
            {
                groupBoxResources.Focus();
                Discover();
                e.Handled = true;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Size maxSize = flowLayoutPanelResources.MaximumSize;
            Size minSize = flowLayoutPanelResources.MinimumSize;
            minSize.Width = maxSize.Width = groupBoxResources.Width - 20;
            flowLayoutPanelResources.MaximumSize = maxSize;
            flowLayoutPanelResources.MinimumSize = minSize;
        }

        private void toolStripComboBoxUrl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DisableWebBrowserSounds();
            tsmiDraft18.Tag = Spec.Draft18;
            tsmiDraft13.Tag = Spec.Draft13;
            tsmiDraft12.Tag = Spec.Draft12;
            tsmiDraft08.Tag = Spec.Draft08;
            tsmiDraft03.Tag = Spec.Draft03;
            tsmiDraft18.Checked = true;

            tsmiBlockSize16.Tag = 16;
            tsmiBlockSize32.Tag = 32;
            tsmiBlockSize64.Tag = 64;
            tsmiBlockSize128.Tag = 128;
            tsmiBlockSize256.Tag = 256;
            tsmiBlockSize512.Tag = 512;
            tsmiBlockSize1024.Tag = 1024;
        }

        #endregion

        #region API

        public enum INTERNETFEATURELIST
        {
            FEATURE_OBJECT_CACHING = 0,
            FEATURE_ZONE_ELEVATION = 1,
            FEATURE_MIME_HANDLING = 2,
            FEATURE_MIME_SNIFFING = 3,
            FEATURE_WINDOW_RESTRICTIONS = 4,
            FEATURE_WEBOC_POPUPMANAGEMENT = 5,
            FEATURE_BEHAVIORS = 6,
            FEATURE_DISABLE_MK_PROTOCOL = 7,
            FEATURE_LOCALMACHINE_LOCKDOWN = 8,
            FEATURE_SECURITYBAND = 9,
            FEATURE_RESTRICT_ACTIVEXINSTALL = 10,
            FEATURE_VALIDATE_NAVIGATE_URL = 11,
            FEATURE_RESTRICT_FILEDOWNLOAD = 12,
            FEATURE_ADDON_MANAGEMENT = 13,
            FEATURE_PROTOCOL_LOCKDOWN = 14,
            FEATURE_HTTP_USERNAME_PASSWORD_DISABLE = 15,
            FEATURE_SAFE_BINDTOOBJECT = 16,
            FEATURE_UNC_SAVEDFILECHECK = 17,
            FEATURE_GET_URL_DOM_FILEPATH_UNENCODED = 18,
            FEATURE_TABBED_BROWSING = 19,
            FEATURE_SSLUX = 20,
            FEATURE_DISABLE_NAVIGATION_SOUNDS = 21,
            FEATURE_DISABLE_LEGACY_COMPRESSION = 22,
            FEATURE_FORCE_ADDR_AND_STATUS = 23,
            FEATURE_XMLHTTP = 24,
            FEATURE_DISABLE_TELNET_PROTOCOL = 25,
            FEATURE_FEEDS = 26,
            FEATURE_BLOCK_INPUT_PROMPTS = 27,
            FEATURE_ENTRY_COUNT = 28
        }

        private const int SET_FEATURE_ON_THREAD = 0x00000001;
        private const int SET_FEATURE_ON_PROCESS = 0x00000002;
        private const int SET_FEATURE_IN_REGISTRY = 0x00000004;
        private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 0x00000008;
        private const int SET_FEATURE_ON_THREAD_INTRANET = 0x00000010;
        private const int SET_FEATURE_ON_THREAD_TRUSTED = 0x00000020;
        private const int SET_FEATURE_ON_THREAD_INTERNET = 0x00000040;
        private const int SET_FEATURE_ON_THREAD_RESTRICTED = 0x00000080;

        [DllImport("urlmon.dll")]
        [PreserveSig]
        [return: MarshalAs(UnmanagedType.Error)]
        static extern int CoInternetSetFeatureEnabled(
            INTERNETFEATURELIST FeatureEntry,
            [MarshalAs(UnmanagedType.U4)] int dwFlags,
            bool fEnable);

        #endregion

        private void tsmiDraft_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            if (menu.Checked)
                return;
            ToolStripItem[] items =
                new ToolStripItem[] { tsmiDraft03, tsmiDraft08, tsmiDraft12, tsmiDraft13, tsmiDraft18 };
            foreach (ToolStripItem item in items)
            {
                if (Object.ReferenceEquals(menu, item))
                    continue;
                (item as ToolStripMenuItem).Checked = false;
            }
            menu.Checked = true;
        }

        private void tsmiDraft_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            if (menu.Checked)
            {
                if (menu.Tag is IEndPoint)
                    _coapEndPoint = (IEndPoint)menu.Tag;
                else if (menu.Tag is ISpec)
                {
                    ISpec spec = (ISpec)menu.Tag;
                    if (spec == Spec.Draft18)
                        menu.Tag = _coapEndPoint = EndPointManager.Draft18;
                    else if (spec == Spec.Draft13)
                        menu.Tag = _coapEndPoint = EndPointManager.Draft13;
                    else if (spec == Spec.Draft12)
                        menu.Tag = _coapEndPoint = EndPointManager.Draft12;
                    else if (spec == Spec.Draft08)
                        menu.Tag = _coapEndPoint = EndPointManager.Draft08;
                    else if (spec == Spec.Draft03)
                        menu.Tag = _coapEndPoint = EndPointManager.Draft03;
                }
            }
        }

        private void tsmiBlock_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            if (menu.Checked)
                return;
            ToolStripItem[] items = new ToolStripItem[] 
            { 
                tsmiBlockLateNegotiation, tsmiBlockSize16, tsmiBlockSize32, tsmiBlockSize64,
                tsmiBlockSize128, tsmiBlockSize256,tsmiBlockSize512, tsmiBlockSize1024
            };
            foreach (ToolStripItem item in items)
            {
                if (Object.ReferenceEquals(menu, item))
                    continue;
                (item as ToolStripMenuItem).Checked = false;
            }
            menu.Checked = true;
        }

        private void tsmiBlock_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            if (menu.Checked)
                _preferBlockSize = menu.Tag == null ? 0 : (Int32)menu.Tag;
        }

        private void tsmiMessageType_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            if (menu.Checked)
                return;
            ToolStripItem[] items = new ToolStripItem[] { tsmiCON, tsmiNON };
            foreach (ToolStripItem item in items)
            {
                if (Object.ReferenceEquals(menu, item))
                    continue;
                (item as ToolStripMenuItem).Checked = false;
            }
            menu.CheckState = CheckState.Indeterminate;
        }
        
        #endregion

        private void 打开服务器端ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String path = System.Windows.Forms.Application.StartupPath + "//PlugtestServer.exe";
            if (File.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                MessageBox.Show("服务器端软件缺失");
            }
        }

    }

    enum RunningState
    {
        Stopped,
        Running,
        Paused
    }
}
