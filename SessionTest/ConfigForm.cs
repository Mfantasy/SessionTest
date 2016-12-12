using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SessionTest
{
    public partial class ConfigForm : Form
    {
       
        public ConfigForm()
        {
            InitializeComponent();
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDownGPRS.Value == numericUpDownNB.Value)
            {
                MessageBox.Show("不允许打开相同端口");
                return;
            }
            foreach (var item in this.tableLayoutPanel1.Controls)
            {
                if ((item as RadioButton).Checked)
                {
                 CustomConfig.currentIp = (item as RadioButton).Tag.ToString();
                }
            }
            if (gprsRBOpen.Checked)
            {
                CustomConfig.gsOpened = true;
            }
            else
            {
                CustomConfig.gsOpened = false;
            }
            if (nbIotRBOpen.Checked)
            {
                CustomConfig.nbOpened = true;
            }
            else
            {
                CustomConfig.nbOpened = false;
            }            
            this.Hide();            
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            RefreshRB();
            if (CustomConfig.gsOpened)
            {
                gprsRBOpen.Checked = true;
            }
            else
            {
                gprsRBClose.Checked = true;
            }
            if (CustomConfig.nbOpened)
            {
                nbIotRBOpen.Checked = true;
            }
            else
            {
                nbiotClose.Checked = true;
            }
            numericUpDownNB.Value = CustomConfig.nbPort;
            numericUpDownGPRS.Value = CustomConfig.gsPort;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            RefreshRB();                       
        }
        void RefreshRB()
        {
            RadioButton rb = new RadioButton();
            rb.AutoSize = true;
            rb.Text = string.Format("测试地址({0})", CustomConfig.testIp);         
            rb.Tag = CustomConfig.testIp;
            rb.Parent = tableLayoutPanel1;
            rb.Checked = true;
            foreach (var item in MainForm.addrsOld)
            {
                RadioButton rab = new RadioButton();
                rab.AutoSize = true;
                rab.Text = item.Name + "\t" + item.Adr;
                rab.Tag = item.Adr;
                rab.Parent = tableLayoutPanel1;
            }                                              
        }
    }
}
