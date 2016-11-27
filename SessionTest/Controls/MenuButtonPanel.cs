using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SessionTest.Controls
{
    public class MenuButtonPanel:Panel
    {
        public MenuButtonPanel()
        {
            this.BackColor = Color.Black;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(10);
            this.CaptionLabel = new Label();
            this.CaptionLabel.AutoSize = true;
            this.CaptionLabel.BackColor = Color.Transparent;
            this.CaptionLabel.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CaptionLabel.ForeColor = Color.White;
            this.Controls.Add(CaptionLabel);
            this.CaptionText = "Caption";
            this.SizeChanged += MenuButtonPanel_SizeChanged;
            this.MouseDown += MenuButtonPanel_MouseDown;
            this.MouseEnter += MenuButtonPanel_MouseEnter;
            this.MouseLeave += MenuButtonPanel_MouseLeave;
            CaptionLabel.MouseDown += MenuButtonPanel_MouseDown;
            CaptionLabel.MouseEnter += MenuButtonPanel_MouseEnter;
            CaptionLabel.MouseLeave += MenuButtonPanel_MouseLeave;
            this.DoubleBuffered = true;
        }

        private void MenuButtonPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!IsDown)
            {
                ShowDefaultImage(null, null);
            }
        }

        private void MenuButtonPanel_MouseEnter(object sender, EventArgs e)
        {
            ShowCheckedImage(null, null);
        }

        public bool IsDown = false;
        private void MenuButtonPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ShowCheckedImage(sender, e);
            AppContextRecord.ClickMenuButton(this);
            Panel content = this.Panel;
            IsDown = true;
            if (content != null)
            {
                content.BringToFront(); 
            }
        }

        public void ShowCheckedImage(object sender, MouseEventArgs e)
        {
            if (CheckedImage != null)
            {
                this.BackgroundImage = CheckedImage;
            }
        }

        public void ShowDefaultImage(object sender, MouseEventArgs e)
        {
            if (DefaultImage != null)
            {
                this.BackgroundImage = DefaultImage;
            }
        }

        private void MenuButtonPanel_SizeChanged(object sender, EventArgs e)
        {
            this.CaptionLabel.Location = new Point((this.Width - CaptionLabel.Width) / 2, (this.Height - CaptionLabel.Height) - 5);
        }

        [Browsable(true)]
        public Panel Panel { get; set; }


        public Label CaptionLabel { get; set; }

        [Browsable(true)]
        public string CaptionText
        {
            get { return this.CaptionLabel.Text; }
            set { this.CaptionLabel.Text = value; }
        }
        [Browsable(true)]
        public Image CheckedImage
        {
            get;set;
        }
        [Browsable(true)]
        public Image DefaultImage
        {
            get; set;
        }
    }
}
