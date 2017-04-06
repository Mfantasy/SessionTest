namespace SessionTest
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.layoutTop = new System.Windows.Forms.FlowLayoutPanel();
            this.panel首页 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel连接管理 = new System.Windows.Forms.Panel();
            this.panel端口扫描 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.labelMacAdd = new System.Windows.Forms.Label();
            this.labelDeviceType = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelSystem = new System.Windows.Forms.Label();
            this.panelCoap测试 = new System.Windows.Forms.Panel();
            this.panelMQTT测试 = new System.Windows.Forms.Panel();
            this.panel测试报告 = new System.Windows.Forms.Panel();
            this.panelContext = new System.Windows.Forms.Panel();
            this.layoutBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuButtonPanel1 = new SessionTest.Controls.MenuButtonPanel();
            this.menuButtonPanel3 = new SessionTest.Controls.MenuButtonPanel();
            this.menuButtonPanel5 = new SessionTest.Controls.MenuButtonPanel();
            this.menuButtonPanel4 = new SessionTest.Controls.MenuButtonPanel();
            this.menuButtonPanel2 = new SessionTest.Controls.MenuButtonPanel();
            this.menuButtonPanel6 = new SessionTest.Controls.MenuButtonPanel();
            this.dvSc = new SessionTest.Controls.CustomDataView();
            this.portSc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeSc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateSc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverSc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.versionSc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dvCn = new SessionTest.Controls.CustomDataView();
            this.nameCn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeCn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ipCn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeCn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.layoutTop.SuspendLayout();
            this.panel首页.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel连接管理.SuspendLayout();
            this.panel端口扫描.SuspendLayout();
            this.panel测试报告.SuspendLayout();
            this.panelContext.SuspendLayout();
            this.layoutBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvSc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvCn)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Lime;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.layoutTop);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelContext);
            this.splitContainer1.Size = new System.Drawing.Size(1024, 768);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Aqua;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(988, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 30);
            this.button1.TabIndex = 7;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Aqua;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(956, 6);
            this.button2.Margin = new System.Windows.Forms.Padding(1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 6;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // layoutTop
            // 
            this.layoutTop.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.layoutTop.Controls.Add(this.menuButtonPanel1);
            this.layoutTop.Controls.Add(this.menuButtonPanel3);
            this.layoutTop.Controls.Add(this.menuButtonPanel5);
            this.layoutTop.Controls.Add(this.menuButtonPanel4);
            this.layoutTop.Controls.Add(this.menuButtonPanel2);
            this.layoutTop.Controls.Add(this.menuButtonPanel6);
            this.layoutTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTop.Name = "layoutTop";
            this.layoutTop.Size = new System.Drawing.Size(1024, 151);
            this.layoutTop.TabIndex = 0;
            // 
            // panel首页
            // 
            this.panel首页.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel首页.Controls.Add(this.label1);
            this.panel首页.Controls.Add(this.pictureBox2);
            this.panel首页.Controls.Add(this.pictureBox1);
            this.panel首页.Location = new System.Drawing.Point(100, 0);
            this.panel首页.Margin = new System.Windows.Forms.Padding(0);
            this.panel首页.Name = "panel首页";
            this.panel首页.Size = new System.Drawing.Size(100, 50);
            this.panel首页.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(191, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(480, 75);
            this.label1.TabIndex = 3;
            this.label1.Text = "会话层协议测试仪";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(29, 29);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 150);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-296, -79);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(385, 117);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel连接管理
            // 
            this.panel连接管理.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel连接管理.Controls.Add(this.dvCn);
            this.panel连接管理.Location = new System.Drawing.Point(200, 0);
            this.panel连接管理.Margin = new System.Windows.Forms.Padding(0);
            this.panel连接管理.Name = "panel连接管理";
            this.panel连接管理.Size = new System.Drawing.Size(100, 50);
            this.panel连接管理.TabIndex = 2;
            // 
            // panel端口扫描
            // 
            this.panel端口扫描.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel端口扫描.Controls.Add(this.button3);
            this.panel端口扫描.Controls.Add(this.labelMacAdd);
            this.panel端口扫描.Controls.Add(this.labelDeviceType);
            this.panel端口扫描.Controls.Add(this.labelName);
            this.panel端口扫描.Controls.Add(this.labelSystem);
            this.panel端口扫描.Controls.Add(this.dvSc);
            this.panel端口扫描.Location = new System.Drawing.Point(0, 0);
            this.panel端口扫描.Margin = new System.Windows.Forms.Padding(0);
            this.panel端口扫描.Name = "panel端口扫描";
            this.panel端口扫描.Size = new System.Drawing.Size(100, 50);
            this.panel端口扫描.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImage = global::SessionTest.Properties.Resources.btn;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Location = new System.Drawing.Point(23, -28);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(64, 64);
            this.button3.TabIndex = 8;
            this.button3.Text = "start";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelMacAdd
            // 
            this.labelMacAdd.AutoSize = true;
            this.labelMacAdd.BackColor = System.Drawing.Color.White;
            this.labelMacAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelMacAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMacAdd.Location = new System.Drawing.Point(0, -114);
            this.labelMacAdd.Margin = new System.Windows.Forms.Padding(20, 30, 10, 10);
            this.labelMacAdd.MaximumSize = new System.Drawing.Size(720, 41);
            this.labelMacAdd.Name = "labelMacAdd";
            this.labelMacAdd.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.labelMacAdd.Size = new System.Drawing.Size(30, 41);
            this.labelMacAdd.TabIndex = 5;
            // 
            // labelDeviceType
            // 
            this.labelDeviceType.AutoSize = true;
            this.labelDeviceType.BackColor = System.Drawing.Color.White;
            this.labelDeviceType.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelDeviceType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelDeviceType.Location = new System.Drawing.Point(0, -73);
            this.labelDeviceType.Margin = new System.Windows.Forms.Padding(20, 30, 80, 10);
            this.labelDeviceType.MaximumSize = new System.Drawing.Size(720, 41);
            this.labelDeviceType.Name = "labelDeviceType";
            this.labelDeviceType.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.labelDeviceType.Size = new System.Drawing.Size(30, 41);
            this.labelDeviceType.TabIndex = 6;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.BackColor = System.Drawing.Color.White;
            this.labelName.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelName.Location = new System.Drawing.Point(0, -32);
            this.labelName.Margin = new System.Windows.Forms.Padding(20, 30, 80, 10);
            this.labelName.MaximumSize = new System.Drawing.Size(720, 41);
            this.labelName.Name = "labelName";
            this.labelName.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.labelName.Size = new System.Drawing.Size(30, 41);
            this.labelName.TabIndex = 7;
            // 
            // labelSystem
            // 
            this.labelSystem.AutoSize = true;
            this.labelSystem.BackColor = System.Drawing.Color.White;
            this.labelSystem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelSystem.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSystem.Image = ((System.Drawing.Image)(resources.GetObject("labelSystem.Image")));
            this.labelSystem.Location = new System.Drawing.Point(0, 9);
            this.labelSystem.Margin = new System.Windows.Forms.Padding(20, 30, 10, 10);
            this.labelSystem.MaximumSize = new System.Drawing.Size(720, 41);
            this.labelSystem.Name = "labelSystem";
            this.labelSystem.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.labelSystem.Size = new System.Drawing.Size(30, 41);
            this.labelSystem.TabIndex = 4;
            // 
            // panelCoap测试
            // 
            this.panelCoap测试.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelCoap测试.Location = new System.Drawing.Point(300, 0);
            this.panelCoap测试.Margin = new System.Windows.Forms.Padding(0);
            this.panelCoap测试.Name = "panelCoap测试";
            this.panelCoap测试.Size = new System.Drawing.Size(100, 50);
            this.panelCoap测试.TabIndex = 3;
            // 
            // panelMQTT测试
            // 
            this.panelMQTT测试.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelMQTT测试.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMQTT测试.BackgroundImage")));
            this.panelMQTT测试.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMQTT测试.Location = new System.Drawing.Point(400, 0);
            this.panelMQTT测试.Margin = new System.Windows.Forms.Padding(0);
            this.panelMQTT测试.Name = "panelMQTT测试";
            this.panelMQTT测试.Size = new System.Drawing.Size(100, 50);
            this.panelMQTT测试.TabIndex = 4;
            // 
            // panel测试报告
            // 
            this.panel测试报告.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel测试报告.Controls.Add(this.webBrowser1);
            this.panel测试报告.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel测试报告.Location = new System.Drawing.Point(0, 0);
            this.panel测试报告.Margin = new System.Windows.Forms.Padding(0);
            this.panel测试报告.Name = "panel测试报告";
            this.panel测试报告.Size = new System.Drawing.Size(1024, 555);
            this.panel测试报告.TabIndex = 5;
            // 
            // panelContext
            // 
            this.panelContext.BackColor = System.Drawing.Color.White;
            this.panelContext.Controls.Add(this.panel测试报告);
            this.panelContext.Controls.Add(this.layoutBottom);
            this.panelContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContext.Location = new System.Drawing.Point(0, 0);
            this.panelContext.Margin = new System.Windows.Forms.Padding(0);
            this.panelContext.Name = "panelContext";
            this.panelContext.Size = new System.Drawing.Size(1024, 616);
            this.panelContext.TabIndex = 0;
            // 
            // layoutBottom
            // 
            this.layoutBottom.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.layoutBottom.Controls.Add(this.panel端口扫描);
            this.layoutBottom.Controls.Add(this.panel首页);
            this.layoutBottom.Controls.Add(this.panel连接管理);
            this.layoutBottom.Controls.Add(this.panelCoap测试);
            this.layoutBottom.Controls.Add(this.panelMQTT测试);
            this.layoutBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutBottom.Location = new System.Drawing.Point(0, 555);
            this.layoutBottom.Margin = new System.Windows.Forms.Padding(0);
            this.layoutBottom.Name = "layoutBottom";
            this.layoutBottom.Size = new System.Drawing.Size(1024, 61);
            this.layoutBottom.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1024, 555);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // menuButtonPanel1
            // 
            this.menuButtonPanel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuButtonPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel1.CaptionText = "首页";
            this.menuButtonPanel1.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel1.CheckedImage")));
            this.menuButtonPanel1.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel1.DefaultImage")));
            this.menuButtonPanel1.Location = new System.Drawing.Point(10, 10);
            this.menuButtonPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel1.Name = "menuButtonPanel1";
            this.menuButtonPanel1.Panel = this.panel首页;
            this.menuButtonPanel1.Size = new System.Drawing.Size(116, 116);
            this.menuButtonPanel1.TabIndex = 0;
            // 
            // menuButtonPanel3
            // 
            this.menuButtonPanel3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuButtonPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel3.CaptionText = "连接管理";
            this.menuButtonPanel3.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel3.CheckedImage")));
            this.menuButtonPanel3.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel3.DefaultImage")));
            this.menuButtonPanel3.Location = new System.Drawing.Point(146, 10);
            this.menuButtonPanel3.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel3.Name = "menuButtonPanel3";
            this.menuButtonPanel3.Panel = this.panel连接管理;
            this.menuButtonPanel3.Size = new System.Drawing.Size(116, 116);
            this.menuButtonPanel3.TabIndex = 2;
            // 
            // menuButtonPanel5
            // 
            this.menuButtonPanel5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuButtonPanel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel5.CaptionText = "端口扫描";
            this.menuButtonPanel5.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel5.CheckedImage")));
            this.menuButtonPanel5.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel5.DefaultImage")));
            this.menuButtonPanel5.Location = new System.Drawing.Point(282, 10);
            this.menuButtonPanel5.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel5.Name = "menuButtonPanel5";
            this.menuButtonPanel5.Panel = this.panel端口扫描;
            this.menuButtonPanel5.Size = new System.Drawing.Size(116, 116);
            this.menuButtonPanel5.TabIndex = 4;
            // 
            // menuButtonPanel4
            // 
            this.menuButtonPanel4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuButtonPanel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel4.CaptionText = "Coap会话测试";
            this.menuButtonPanel4.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel4.CheckedImage")));
            this.menuButtonPanel4.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel4.DefaultImage")));
            this.menuButtonPanel4.Location = new System.Drawing.Point(418, 10);
            this.menuButtonPanel4.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel4.Name = "menuButtonPanel4";
            this.menuButtonPanel4.Panel = this.panelCoap测试;
            this.menuButtonPanel4.Size = new System.Drawing.Size(116, 116);
            this.menuButtonPanel4.TabIndex = 3;
            // 
            // menuButtonPanel2
            // 
            this.menuButtonPanel2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuButtonPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel2.CaptionText = "MQTT会话测试";
            this.menuButtonPanel2.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel2.CheckedImage")));
            this.menuButtonPanel2.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel2.DefaultImage")));
            this.menuButtonPanel2.Location = new System.Drawing.Point(554, 10);
            this.menuButtonPanel2.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel2.Name = "menuButtonPanel2";
            this.menuButtonPanel2.Panel = this.panelMQTT测试;
            this.menuButtonPanel2.Size = new System.Drawing.Size(116, 116);
            this.menuButtonPanel2.TabIndex = 1;
            // 
            // menuButtonPanel6
            // 
            this.menuButtonPanel6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.menuButtonPanel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel6.CaptionText = "分析报告";
            this.menuButtonPanel6.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel6.CheckedImage")));
            this.menuButtonPanel6.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel6.DefaultImage")));
            this.menuButtonPanel6.Location = new System.Drawing.Point(690, 10);
            this.menuButtonPanel6.Margin = new System.Windows.Forms.Padding(10);
            this.menuButtonPanel6.Name = "menuButtonPanel6";
            this.menuButtonPanel6.Panel = this.panel测试报告;
            this.menuButtonPanel6.Size = new System.Drawing.Size(116, 116);
            this.menuButtonPanel6.TabIndex = 2;
            // 
            // dvSc
            // 
            this.dvSc.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dvSc.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dvSc.BackgroundColor = System.Drawing.Color.White;
            this.dvSc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dvSc.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dvSc.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvSc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvSc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvSc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.portSc,
            this.typeSc,
            this.stateSc,
            this.serverSc,
            this.versionSc});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvSc.DefaultCellStyle = dataGridViewCellStyle4;
            this.dvSc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvSc.GridColor = System.Drawing.Color.SkyBlue;
            this.dvSc.Location = new System.Drawing.Point(0, 0);
            this.dvSc.Margin = new System.Windows.Forms.Padding(0);
            this.dvSc.Name = "dvSc";
            this.dvSc.ReadOnly = true;
            this.dvSc.RowHeadersVisible = false;
            this.dvSc.RowTemplate.Height = 27;
            this.dvSc.Size = new System.Drawing.Size(100, 50);
            this.dvSc.TabIndex = 3;
            // 
            // portSc
            // 
            this.portSc.HeaderText = "端口号";
            this.portSc.Name = "portSc";
            this.portSc.ReadOnly = true;
            this.portSc.Width = 77;
            // 
            // typeSc
            // 
            this.typeSc.HeaderText = "类型";
            this.typeSc.Name = "typeSc";
            this.typeSc.ReadOnly = true;
            this.typeSc.Width = 62;
            // 
            // stateSc
            // 
            this.stateSc.HeaderText = "状态";
            this.stateSc.Name = "stateSc";
            this.stateSc.ReadOnly = true;
            this.stateSc.Width = 62;
            // 
            // serverSc
            // 
            this.serverSc.HeaderText = "运行服务";
            this.serverSc.Name = "serverSc";
            this.serverSc.ReadOnly = true;
            this.serverSc.Width = 92;
            // 
            // versionSc
            // 
            this.versionSc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.versionSc.HeaderText = "服务版本";
            this.versionSc.Name = "versionSc";
            this.versionSc.ReadOnly = true;
            // 
            // dvCn
            // 
            this.dvCn.AllowUserToAddRows = false;
            this.dvCn.AllowUserToDeleteRows = false;
            this.dvCn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dvCn.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dvCn.BackgroundColor = System.Drawing.Color.White;
            this.dvCn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dvCn.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dvCn.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvCn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvCn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvCn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameCn,
            this.typeCn,
            this.ipCn,
            this.timeCn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvCn.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvCn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvCn.GridColor = System.Drawing.Color.SkyBlue;
            this.dvCn.Location = new System.Drawing.Point(0, 0);
            this.dvCn.Name = "dvCn";
            this.dvCn.ReadOnly = true;
            this.dvCn.RowHeadersVisible = false;
            this.dvCn.RowTemplate.Height = 27;
            this.dvCn.Size = new System.Drawing.Size(100, 50);
            this.dvCn.TabIndex = 2;
            // 
            // nameCn
            // 
            this.nameCn.HeaderText = "名称";
            this.nameCn.Name = "nameCn";
            this.nameCn.ReadOnly = true;
            this.nameCn.Width = 62;
            // 
            // typeCn
            // 
            this.typeCn.HeaderText = "连接类型";
            this.typeCn.Name = "typeCn";
            this.typeCn.ReadOnly = true;
            this.typeCn.Width = 92;
            // 
            // ipCn
            // 
            this.ipCn.HeaderText = "设备地址";
            this.ipCn.Name = "ipCn";
            this.ipCn.ReadOnly = true;
            this.ipCn.Width = 92;
            // 
            // timeCn
            // 
            this.timeCn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.timeCn.HeaderText = "连接时间";
            this.timeCn.Name = "timeCn";
            this.timeCn.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会话层协议测试仪";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.layoutTop.ResumeLayout(false);
            this.panel首页.ResumeLayout(false);
            this.panel首页.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel连接管理.ResumeLayout(false);
            this.panel端口扫描.ResumeLayout(false);
            this.panel端口扫描.PerformLayout();
            this.panel测试报告.ResumeLayout(false);
            this.panelContext.ResumeLayout(false);
            this.layoutBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvSc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvCn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelContext;
        private Controls.MenuButtonPanel menuButtonPanel1;
        private Controls.MenuButtonPanel menuButtonPanel2;
        private System.Windows.Forms.Panel panel首页;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel layoutBottom;
        private System.Windows.Forms.Panel panel端口扫描;
        private System.Windows.Forms.Panel panel连接管理;
        private System.Windows.Forms.Panel panelCoap测试;
        private System.Windows.Forms.Panel panelMQTT测试;
        private System.Windows.Forms.Panel panel测试报告;
        private Controls.MenuButtonPanel menuButtonPanel3;
        private Controls.MenuButtonPanel menuButtonPanel5;
        private Controls.MenuButtonPanel menuButtonPanel4;
        private Controls.MenuButtonPanel menuButtonPanel6;
        private System.Windows.Forms.FlowLayoutPanel layoutTop;
        private Controls.CustomDataView dvCn;
        private Controls.CustomDataView dvSc;
        private System.Windows.Forms.DataGridViewTextBoxColumn portSc;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeSc;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateSc;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverSc;
        private System.Windows.Forms.DataGridViewTextBoxColumn versionSc;
        private System.Windows.Forms.Label labelSystem;
        private System.Windows.Forms.Label labelDeviceType;
        private System.Windows.Forms.Label labelMacAdd;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameCn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeCn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ipCn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeCn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

