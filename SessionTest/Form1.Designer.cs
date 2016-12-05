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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuButtonPanel6 = new SessionTest.Controls.MenuButtonPanel();
            this.panel测试报告 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.menuButtonPanel5 = new SessionTest.Controls.MenuButtonPanel();
            this.panel端口扫描 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.menuButtonPanel3 = new SessionTest.Controls.MenuButtonPanel();
            this.panel连接管理 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.menuButtonPanel1 = new SessionTest.Controls.MenuButtonPanel();
            this.panel首页 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuButtonPanel2 = new SessionTest.Controls.MenuButtonPanel();
            this.panelMQTT测试 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.menuButtonPanel4 = new SessionTest.Controls.MenuButtonPanel();
            this.panelCoap测试 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panelContext = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel测试报告.SuspendLayout();
            this.panel端口扫描.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel连接管理.SuspendLayout();
            this.panel首页.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelMQTT测试.SuspendLayout();
            this.panelCoap测试.SuspendLayout();
            this.panelContext.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelContext);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.menuButtonPanel6, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuButtonPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuButtonPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuButtonPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuButtonPanel2, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.menuButtonPanel4, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 118);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuButtonPanel6
            // 
            this.menuButtonPanel6.BackColor = System.Drawing.Color.Black;
            this.menuButtonPanel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel6.CaptionText = "分析报告";
            this.menuButtonPanel6.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel6.CheckedImage")));
            this.menuButtonPanel6.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel6.DefaultImage")));
            this.menuButtonPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonPanel6.Location = new System.Drawing.Point(613, 12);
            this.menuButtonPanel6.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.menuButtonPanel6.Name = "menuButtonPanel6";
            this.menuButtonPanel6.Panel = this.panel测试报告;
            this.menuButtonPanel6.Size = new System.Drawing.Size(94, 94);
            this.menuButtonPanel6.TabIndex = 2;
            // 
            // panel测试报告
            // 
            this.panel测试报告.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel测试报告.Controls.Add(this.label6);
            this.panel测试报告.Location = new System.Drawing.Point(200, 0);
            this.panel测试报告.Margin = new System.Windows.Forms.Padding(0);
            this.panel测试报告.Name = "panel测试报告";
            this.panel测试报告.Size = new System.Drawing.Size(100, 50);
            this.panel测试报告.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "测试报告";
            // 
            // menuButtonPanel5
            // 
            this.menuButtonPanel5.BackColor = System.Drawing.Color.Black;
            this.menuButtonPanel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel5.CaptionText = "端口扫描";
            this.menuButtonPanel5.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel5.CheckedImage")));
            this.menuButtonPanel5.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel5.DefaultImage")));
            this.menuButtonPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonPanel5.Location = new System.Drawing.Point(133, 12);
            this.menuButtonPanel5.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.menuButtonPanel5.Name = "menuButtonPanel5";
            this.menuButtonPanel5.Panel = this.panel端口扫描;
            this.menuButtonPanel5.Size = new System.Drawing.Size(94, 94);
            this.menuButtonPanel5.TabIndex = 4;
            // 
            // panel端口扫描
            // 
            this.panel端口扫描.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel端口扫描.Controls.Add(this.dataGridView1);
            this.panel端口扫描.Controls.Add(this.label2);
            this.panel端口扫描.Location = new System.Drawing.Point(500, 0);
            this.panel端口扫描.Margin = new System.Windows.Forms.Padding(0);
            this.panel端口扫描.Name = "panel端口扫描";
            this.panel端口扫描.Size = new System.Drawing.Size(100, 50);
            this.panel端口扫描.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column5,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 15);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(100, 35);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "端口号";
            this.Column1.Name = "Column1";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "类型";
            this.Column5.Name = "Column5";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "状态";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "运行服务";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "服务版本";
            this.Column4.Name = "Column4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口扫描";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // menuButtonPanel3
            // 
            this.menuButtonPanel3.BackColor = System.Drawing.Color.Black;
            this.menuButtonPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel3.CaptionText = "连接管理";
            this.menuButtonPanel3.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel3.CheckedImage")));
            this.menuButtonPanel3.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel3.DefaultImage")));
            this.menuButtonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonPanel3.Location = new System.Drawing.Point(253, 12);
            this.menuButtonPanel3.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.menuButtonPanel3.Name = "menuButtonPanel3";
            this.menuButtonPanel3.Panel = this.panel连接管理;
            this.menuButtonPanel3.Size = new System.Drawing.Size(94, 94);
            this.menuButtonPanel3.TabIndex = 2;
            // 
            // panel连接管理
            // 
            this.panel连接管理.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel连接管理.Controls.Add(this.label3);
            this.panel连接管理.Location = new System.Drawing.Point(400, 0);
            this.panel连接管理.Margin = new System.Windows.Forms.Padding(0);
            this.panel连接管理.Name = "panel连接管理";
            this.panel连接管理.Size = new System.Drawing.Size(100, 50);
            this.panel连接管理.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "连接管理";
            // 
            // menuButtonPanel1
            // 
            this.menuButtonPanel1.BackColor = System.Drawing.Color.Black;
            this.menuButtonPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel1.CaptionText = "首页";
            this.menuButtonPanel1.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel1.CheckedImage")));
            this.menuButtonPanel1.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel1.DefaultImage")));
            this.menuButtonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonPanel1.Location = new System.Drawing.Point(13, 12);
            this.menuButtonPanel1.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.menuButtonPanel1.Name = "menuButtonPanel1";
            this.menuButtonPanel1.Panel = this.panel首页;
            this.menuButtonPanel1.Size = new System.Drawing.Size(94, 94);
            this.menuButtonPanel1.TabIndex = 0;
            this.menuButtonPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.menuButtonPanel1_Paint);
            // 
            // panel首页
            // 
            this.panel首页.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel首页.Controls.Add(this.label1);
            this.panel首页.Controls.Add(this.pictureBox1);
            this.panel首页.Location = new System.Drawing.Point(300, 0);
            this.panel首页.Margin = new System.Windows.Forms.Padding(0);
            this.panel首页.Name = "panel首页";
            this.panel首页.Size = new System.Drawing.Size(100, 50);
            this.panel首页.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "首页";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-126, -437);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(192, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // menuButtonPanel2
            // 
            this.menuButtonPanel2.BackColor = System.Drawing.Color.Black;
            this.menuButtonPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel2.CaptionText = "MQTT测试";
            this.menuButtonPanel2.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel2.CheckedImage")));
            this.menuButtonPanel2.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel2.DefaultImage")));
            this.menuButtonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonPanel2.Location = new System.Drawing.Point(493, 12);
            this.menuButtonPanel2.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.menuButtonPanel2.Name = "menuButtonPanel2";
            this.menuButtonPanel2.Panel = this.panelMQTT测试;
            this.menuButtonPanel2.Size = new System.Drawing.Size(94, 94);
            this.menuButtonPanel2.TabIndex = 1;
            // 
            // panelMQTT测试
            // 
            this.panelMQTT测试.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelMQTT测试.Controls.Add(this.label5);
            this.panelMQTT测试.Location = new System.Drawing.Point(100, 0);
            this.panelMQTT测试.Margin = new System.Windows.Forms.Padding(0);
            this.panelMQTT测试.Name = "panelMQTT测试";
            this.panelMQTT测试.Size = new System.Drawing.Size(100, 50);
            this.panelMQTT测试.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "MQTT协议测试";
            // 
            // menuButtonPanel4
            // 
            this.menuButtonPanel4.BackColor = System.Drawing.Color.Black;
            this.menuButtonPanel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.menuButtonPanel4.CaptionText = "Coap测试";
            this.menuButtonPanel4.CheckedImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel4.CheckedImage")));
            this.menuButtonPanel4.DefaultImage = ((System.Drawing.Image)(resources.GetObject("menuButtonPanel4.DefaultImage")));
            this.menuButtonPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButtonPanel4.Location = new System.Drawing.Point(373, 12);
            this.menuButtonPanel4.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.menuButtonPanel4.Name = "menuButtonPanel4";
            this.menuButtonPanel4.Panel = this.panelCoap测试;
            this.menuButtonPanel4.Size = new System.Drawing.Size(94, 94);
            this.menuButtonPanel4.TabIndex = 3;
            // 
            // panelCoap测试
            // 
            this.panelCoap测试.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelCoap测试.Controls.Add(this.label4);
            this.panelCoap测试.Location = new System.Drawing.Point(0, 0);
            this.panelCoap测试.Margin = new System.Windows.Forms.Padding(0);
            this.panelCoap测试.Name = "panelCoap测试";
            this.panelCoap测试.Size = new System.Drawing.Size(100, 50);
            this.panelCoap测试.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Coap协议测试";
            // 
            // panelContext
            // 
            this.panelContext.BackColor = System.Drawing.Color.White;
            this.panelContext.Controls.Add(this.flowLayoutPanel1);
            this.panelContext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContext.Location = new System.Drawing.Point(0, 0);
            this.panelContext.Margin = new System.Windows.Forms.Padding(0);
            this.panelContext.Name = "panelContext";
            this.panelContext.Size = new System.Drawing.Size(800, 481);
            this.panelContext.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.LightGreen;
            this.flowLayoutPanel1.Controls.Add(this.panelCoap测试);
            this.flowLayoutPanel1.Controls.Add(this.panelMQTT测试);
            this.flowLayoutPanel1.Controls.Add(this.panel测试报告);
            this.flowLayoutPanel1.Controls.Add(this.panel首页);
            this.flowLayoutPanel1.Controls.Add(this.panel连接管理);
            this.flowLayoutPanel1.Controls.Add(this.panel端口扫描);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 396);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 85);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物联网安全卫士";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel测试报告.ResumeLayout(false);
            this.panel测试报告.PerformLayout();
            this.panel端口扫描.ResumeLayout(false);
            this.panel端口扫描.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel连接管理.ResumeLayout(false);
            this.panel连接管理.PerformLayout();
            this.panel首页.ResumeLayout(false);
            this.panel首页.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelMQTT测试.ResumeLayout(false);
            this.panelMQTT测试.PerformLayout();
            this.panelCoap测试.ResumeLayout(false);
            this.panelCoap测试.PerformLayout();
            this.panelContext.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelContext;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls.MenuButtonPanel menuButtonPanel1;
        private Controls.MenuButtonPanel menuButtonPanel2;
        private System.Windows.Forms.Panel panel首页;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel端口扫描;
        private System.Windows.Forms.Panel panel连接管理;
        private System.Windows.Forms.Panel panelCoap测试;
        private System.Windows.Forms.Panel panelMQTT测试;
        private System.Windows.Forms.Panel panel测试报告;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Controls.MenuButtonPanel menuButtonPanel3;
        private Controls.MenuButtonPanel menuButtonPanel5;
        private Controls.MenuButtonPanel menuButtonPanel4;
        private Controls.MenuButtonPanel menuButtonPanel6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}

