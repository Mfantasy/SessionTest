using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SessionTest.UserControls
{
    class UserPanelBase:UserControl
    {

        public UserPanelBase()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UserPanelBase
            // 
            this.Name = "UserPanelBase";
            this.ResumeLayout(false);

        }

    
    }
}
