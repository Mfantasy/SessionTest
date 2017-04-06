using CoAP.Tester;
using SessionTest;
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
            int i = 0;
            foreach (TreeNode item in treeView1.Nodes[0].Nodes)
            {
                item.Text = "MQTT"+i.ToString();
                i++;
            }
            
        }

        void Test(int i)
        {
            Console.WriteLine(i);
            Thread.Sleep(50);
        }



        List<CncRowModel> list;
        private void button2_Click(object sender, EventArgs e)
        {
            
            Thread th = new Thread(Test);
            th.Start();
            while (true)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    Console.WriteLine(lst[i]);
                }
            }
        }

        List<int> lst = new List<int> { 123,1,2,2,2,3};
        void Test()
        {
            lock (lst)
            {
                for (int i = 0; i < 9999999; i++)
                {
                    lst.Add(i);
                }
            }
        }
    }
}
