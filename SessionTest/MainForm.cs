using SessionTest.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;

namespace SessionTest
{
    public partial class MainForm : Form
    {
        //不许扫描本机
        public MainForm()
        {
            InitializeComponent();
            if (menuButtonPanel1.DefaultImage != null)
            {
                menuButtonPanel1.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel2.DefaultImage != null)
            {
                menuButtonPanel2.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel3.DefaultImage != null)
            {
                menuButtonPanel3.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel4.DefaultImage != null)
            {
                menuButtonPanel4.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel5.DefaultImage != null)
            {
                menuButtonPanel5.ShowDefaultImage(null, null);
            }
            if (menuButtonPanel6.DefaultImage != null)
            {
                menuButtonPanel6.ShowDefaultImage(null, null);
            }            
            //load 内容panel
            int l = layoutBottom.Controls.Count;
            for (int i = 0; i < l; i++)
            {
                Control ctrl = layoutBottom.Controls[0];
                ctrl.Dock = DockStyle.Fill;
                panelContext.Controls.Add(ctrl);
            }
            ThreadPool.QueueUserWorkItem(ScanIpsL);
        }


        #region 端口扫描
        void AddDataSc(string x)
        {
            int i = x.IndexOf('/');
            string y1 = x.Remove(i);
            string x2 = x.Remove(0, i + 1).Trim();
            string y2 = x2.Remove(3);
            string x3 = x2.Remove(0, 3 + 1).Trim();
            int i3 = x3.IndexOf(' ');
            string y3 = x3.Remove(i3);
            string x4 = x3.Remove(0, i3 + 1).Trim();
            int i4 = x4.IndexOf(' ');
            string y4 = "";
            string y5 = "";
            if (i4 != -1)
            {
                y4 = x4.Remove(i4);
                y5 = x4.Remove(0, i4 + 1).Trim();
            }
            else
            {
                y4 = x4;
            }
            //Console.WriteLine("{0}\r\n{1}\r\n{2}\r\n{3}\r\n{4}", y1, y2, y3, y4, y5);
            if (dvSc.InvokeRequired)
            {
                dvSc.Invoke(new Action(() => dvSc.Rows.Add(y1, y2, y3, y4, y5)));
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync(CustomConfig.currentIp);
            var wait = new ScanWaittingForm(bw);
            wait.ShowDialog();
            return;
        }


        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            NmapPortScan(e.Argument.ToString());
        }

        #region 调用Nmap进行以太网设备扫描及端口扫描功能
        bool Nmap(string arg, int mode)
        {
            try
            {
                string path = Path.GetFullPath(ConfigurationManager.AppSettings["nmap"]);
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(path, arg);
                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                ParseLine(p.StandardOutput, mode);
                p.WaitForExit();
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
#endif
                return false;
            }
        }
      
        string itName = ConfigurationManager.AppSettings["itName"];//iot gprs
        string iotName = ConfigurationManager.AppSettings["iotName"];
        string gprsName = ConfigurationManager.AppSettings["gprsName"];
        public static List<CncRowModel> addrsOld = new List<CncRowModel>();
        bool isFirstCnc = true;
        private void ParseLine(StreamReader reader, int mode)
        {
            List<CncRowModel> addrsNew = new List<CncRowModel>();
            if (mode == 0)
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();//每次读取一行
                    if (line.StartsWith("Nmap scan report for "))
                    {
                        Console.WriteLine(line);
                        IPAddress ipadd;
                        string str = line.Replace("Nmap scan report for ", "").Trim();
                        if (!IPAddress.TryParse(str, out ipadd)) //&& !str.Contains("localhost"))
                        {
                            string nameCnc = str.Remove(str.IndexOf('('));
                            string typ = itName;
                            string adr = str.Substring(str.IndexOf('(')+1, str.IndexOf(')')- 1 - str.IndexOf('('));
                            string time = DateTime.Now.ToString();
                            DataGridViewRow row = new DataGridViewRow();
                            //row.CreateCells(dvCn, nameCnc, typ, adr, time);//SetValues(nameCnc, typ, adr, time);
                            CncRowModel cncRow = new CncRowModel(nameCnc, typ, adr, time, row);
                            if (isFirstCnc)
                            {
                                addrsOld.Add(cncRow);
                                if (dvCn.InvokeRequired)
                                {
                                    dvCn.Invoke(new Action(() =>
                                    {
                                        row.CreateCells(dvCn, nameCnc, typ, adr, time); dvCn.Rows.Add(row);
                                    }));
                                }
                            }
                            else
                            {
                                addrsNew.Add(cncRow);
                            }
                        }
                    }
                }
                if (isFirstCnc) { isFirstCnc = false; }
                else
                {
                    //判断哪个设备增加了
                    foreach (CncRowModel item in addrsNew)
                    {
                        if (!addrsOld.Exists(row => row.Adr == item.Adr))
                        {
                            addrsOld.Add(item);
                            if (dvCn.InvokeRequired)
                            {
                                dvCn.Invoke(new Action(() => { item.Row.CreateCells(dvCn, item.Name, item.Typ, item.Adr, item.Time); dvCn.Rows.Add(item.Row); }));
                            }
                        }
                    }
                    for (int i = 0; i < addrsOld.Count; i++)
                    {
                        //判断哪个设备离线了
                        if (!addrsNew.Exists(row => row.Adr == addrsOld[i].Adr))
                        {
                            if (dvCn.InvokeRequired)
                            {
                                dvCn.Invoke(new Action(() =>
                                {
                                    addrsOld[i].Row.DefaultCellStyle.ForeColor = Color.Gray;
                                }));
                                addrsOld.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
            }
            else if (mode == 1)
            {
                Regex rNum = new Regex("^\\d{1,5}/");
                dvSc.Invoke(new Action(() => dvSc.Rows.Clear()));
                
                string nameSc = "";
                string macAddrSc = "";
                string deviceSc = "";
                string systemSc = "";
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();//每次读取一行

                    if (line.StartsWith("Nmap scan report for "))
                    {
                        nameSc = line.Replace("Nmap scan report for ", "").Trim();
                    }
                    else if(line.Contains("Host seems down."))
                    {
                        MessageBox.Show("请检查扫描地址!");
                        return;
                    }
                    else if (rNum.IsMatch(line))
                    {
                        AddDataSc(line);
                    }
                    else if (line.StartsWith("MAC Address"))
                    {
                        macAddrSc = line.Trim().Replace("MAC Address","MAC地址");
                    }
                    else if (line.StartsWith("Device type"))
                    {
                        deviceSc = line.Trim().Replace("Device type","设备类型");
                    }
                    else if (line.StartsWith("Running"))
                    {
                        systemSc = line.Trim().Replace("Running","操作系统");
                    }
                }
                this.Invoke(new Action(() => { labelName.Text = nameSc; labelMacAdd.Text = macAddrSc; labelDeviceType.Text = deviceSc; labelSystem.Text = systemSc; }));
            }
        }

        bool NmapPortScan(string ip)
        {
            string arg = string.Format("-sV -O {0}", ip);
            return Nmap(arg, 1);
        }
        bool NmapIpGet(string ip)
        {
            string arg = string.Format("-sL {0}/24", ip);
            return Nmap(arg, 0);
        }
        #endregion
        #endregion
        #region 连接管理

        void ScanIpsL(object o)
        {
            string hn = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(hn);
            var x = ip.AddressList;
            foreach (var item in x)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    while (true)
                    {
                        NmapIpGet(item.ToString());
                    }
                }
            }
        }

        void CircleGetIp()
        {

        }
        void CreateServer()
        {

        }


        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ConfigForm cgfm;
        private void button2_Click(object sender, EventArgs e)
        {
            if (cgfm != null)
            {
                cgfm.Show();
            }
            else
            {
                cgfm = new ConfigForm();
                cgfm.ShowDialog();
            }
        }
    }
}
