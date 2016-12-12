using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public List<CncRowModel> Captions { get; set; }
        public ConfigForm(List<CncRowModel> addrsOld)
        {
            InitializeComponent();
            this.Captions = addrsOld;                  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RadioButton rb = new RadioButton();
            rb.Text = "rrrrrrrrrrrrrrrrrrrr";
            tableLayoutPanel1.Controls.Add(rb);
            this.Hide();
        }
    }
}
