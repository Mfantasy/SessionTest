using SessionTest.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
        }
        public static void XuanZhuan(PictureBox picBox)
        {
            Bitmap MyBitmap = (Bitmap)picBox.Image;
            Graphics g = picBox.CreateGraphics();
            float MyAngle = 0;//旋转的角度
            while (MyAngle < 360)
            {
                Thread.Sleep(100);
                TextureBrush MyBrush = new TextureBrush(MyBitmap);
                picBox.Refresh();
                MyBrush.RotateTransform(MyAngle);
                g.FillRectangle(MyBrush, 0, 0, MyBitmap.Width, MyBitmap.Height);
                MyAngle += 0.5f;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            XuanZhuan(pictureBox1);
            //IPAddress ipadd;
            ////string str = line.Replace("Nmap scan report for ", "").Trim();
            //string str = "localhost(192.168.10.101)";
            //if (!IPAddress.TryParse(str, out ipadd))
            //{
            //    MessageBox.Show("");
            //}
        }
    }
}
