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
    public partial class ScanWaittingForm : Form
    {
        public ScanWaittingForm(BackgroundWorker bw)
        {
            InitializeComponent();         
            this.Shown += ScanWaittingForm_Shown;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
        }

        private void ScanWaittingForm_Shown(object sender, EventArgs e)
        {
            //pictureBox1.Width = this.Height;
            //pictureBox1.Height = this.Height;
            //pictureBox1.Location = new Point(this.Width / 2 - this.Height / 2, 0);
            //this.Width = this.Height;
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
