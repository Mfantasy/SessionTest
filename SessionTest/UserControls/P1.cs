using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTest.UserControls
{
    class P1:UserPanelBase
    {
        public P1()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // P1
            // 
            this.BackColor = System.Drawing.Color.Red;
            this.Name = "P1";
            this.ResumeLayout(false);

        }
    }
}
