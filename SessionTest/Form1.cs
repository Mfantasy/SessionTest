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

namespace SessionTest
{
    public partial class MainForm : Form
    {
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
        }

        void ScanIpsL()
        {
                
        }



        private void menuButtonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            NmapPortScan("192.168.10.1");
            return;
            string hn = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(hn);
            var x = ip.AddressList;
            foreach (var item in x)
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    NmapIpGet(item.ToString());
            }
            //扫描网络区域 -sL ip /24 用listBox装起来.实现点击事件.   -O -sV 服务版本,系统.
            
            
            //DataGridView dgv = new DataGridView();
            
            //var x = Path.GetFullPath("../../../nmap-7.01/nmap");
            //Process p = new Process();
            //p.StartInfo = new ProcessStartInfo();
            //p.StartInfo.FileName = x;
            //p.StartInfo.Arguments = "-sV 192.168.10.1";
            //p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.UseShellExecute = false; 
            //p.StartInfo.CreateNoWindow = true;
            //p.Start();

            //System.IO.StreamReader reader = p.StandardOutput;//截取输出流
       
            //p.WaitForExit();
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
                    MessageBox.Show(ex.Message);
#endif
                    return false;
                }
            }
            
        private void ParseLine(StreamReader reader, int mode)
        {
            Regex rNum = new Regex("^\\d{1,5}/");
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();//每次读取一行
                if (mode == 0)
                {
                    if (line.StartsWith("Nmap scan report for "))
                    {
                        IPAddress ipadd;
                        string str = line.Replace("Nmap scan report for ", "").Trim();
                        if (!IPAddress.TryParse(str, out ipadd))
                        {
                            Console.WriteLine(str);
                        }
                    }
                }
                else if(mode == 1)
                {              
                    if(line.StartsWith("Nmap scan report for "))
                    {
                        Console.WriteLine(line.Replace("Nmap scan report for ", ""));
                    }
                    else if (rNum.IsMatch(line))
                    {
                        Console.WriteLine(line);
                        var x = line.Split('/');
                    }
                    else if (line.StartsWith("MAC Address"))
                    {
                        Console.WriteLine(line);
                    }
                    else if (line.StartsWith("Device type"))
                    {
                        Console.WriteLine(line);
                    }
                    else if (line.StartsWith("Running"))
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        bool NmapPortScan(string ip)
        {
            string arg = string.Format("-sV -O {0}", ip);
            return Nmap(arg, 1);
        }
        bool NmapIpGet(string ip)
        {
            string arg = string.Format("-sL {0}/24",ip);
            return Nmap(arg, 0);
        }
        #endregion
        #region 连接管理
        void CircleGetIp()
        {
                
        }
        #endregion
    }
}
