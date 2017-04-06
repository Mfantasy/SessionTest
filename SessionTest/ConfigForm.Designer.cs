namespace SessionTest
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownNB = new System.Windows.Forms.NumericUpDown();
            this.nbiotClose = new System.Windows.Forms.RadioButton();
            this.nbIotRBOpen = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownGPRS = new System.Windows.Forms.NumericUpDown();
            this.gprsRBClose = new System.Windows.Forms.RadioButton();
            this.gprsRBOpen = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNB)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGPRS)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 239);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "端口扫描地址";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(574, 219);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(444, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 46);
            this.button1.TabIndex = 1;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(519, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(69, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownNB);
            this.groupBox2.Controls.Add(this.nbiotClose);
            this.groupBox2.Controls.Add(this.nbIotRBOpen);
            this.groupBox2.Location = new System.Drawing.Point(0, 282);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 75);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NB-IOT连接";
            // 
            // numericUpDownNB
            // 
            this.numericUpDownNB.Location = new System.Drawing.Point(284, 30);
            this.numericUpDownNB.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownNB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNB.Name = "numericUpDownNB";
            this.numericUpDownNB.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownNB.TabIndex = 5;
            this.numericUpDownNB.Value = new decimal(new int[] {
            6666,
            0,
            0,
            0});
            // 
            // nbiotClose
            // 
            this.nbiotClose.AutoSize = true;
            this.nbiotClose.Location = new System.Drawing.Point(113, 30);
            this.nbiotClose.Name = "nbiotClose";
            this.nbiotClose.Size = new System.Drawing.Size(47, 16);
            this.nbiotClose.TabIndex = 1;
            this.nbiotClose.TabStop = true;
            this.nbiotClose.Text = "关闭";
            this.nbiotClose.UseVisualStyleBackColor = true;
            // 
            // nbIotRBOpen
            // 
            this.nbIotRBOpen.AutoSize = true;
            this.nbIotRBOpen.Location = new System.Drawing.Point(12, 30);
            this.nbIotRBOpen.Name = "nbIotRBOpen";
            this.nbIotRBOpen.Size = new System.Drawing.Size(47, 16);
            this.nbIotRBOpen.TabIndex = 0;
            this.nbIotRBOpen.TabStop = true;
            this.nbIotRBOpen.Text = "开启";
            this.nbIotRBOpen.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDownGPRS);
            this.groupBox3.Controls.Add(this.gprsRBClose);
            this.groupBox3.Controls.Add(this.gprsRBOpen);
            this.groupBox3.Location = new System.Drawing.Point(0, 357);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(600, 76);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " GPRS连接";
            // 
            // numericUpDownGPRS
            // 
            this.numericUpDownGPRS.Location = new System.Drawing.Point(284, 26);
            this.numericUpDownGPRS.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownGPRS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownGPRS.Name = "numericUpDownGPRS";
            this.numericUpDownGPRS.Size = new System.Drawing.Size(120, 21);
            this.numericUpDownGPRS.TabIndex = 4;
            this.numericUpDownGPRS.Value = new decimal(new int[] {
            7777,
            0,
            0,
            0});
            // 
            // gprsRBClose
            // 
            this.gprsRBClose.AutoSize = true;
            this.gprsRBClose.Location = new System.Drawing.Point(113, 32);
            this.gprsRBClose.Name = "gprsRBClose";
            this.gprsRBClose.Size = new System.Drawing.Size(47, 16);
            this.gprsRBClose.TabIndex = 3;
            this.gprsRBClose.TabStop = true;
            this.gprsRBClose.Text = "关闭";
            this.gprsRBClose.UseVisualStyleBackColor = true;
            // 
            // gprsRBOpen
            // 
            this.gprsRBOpen.AutoSize = true;
            this.gprsRBOpen.Location = new System.Drawing.Point(12, 32);
            this.gprsRBOpen.Name = "gprsRBOpen";
            this.gprsRBOpen.Size = new System.Drawing.Size(47, 16);
            this.gprsRBOpen.TabIndex = 2;
            this.gprsRBOpen.TabStop = true;
            this.gprsRBOpen.Text = "开启";
            this.gprsRBOpen.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 442);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(69, 46);
            this.button3.TabIndex = 4;
            this.button3.Text = "刷新";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 505);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConfigForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigForm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNB)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGPRS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton nbiotClose;
        private System.Windows.Forms.RadioButton nbIotRBOpen;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownGPRS;
        private System.Windows.Forms.RadioButton gprsRBClose;
        private System.Windows.Forms.RadioButton gprsRBOpen;
        private System.Windows.Forms.NumericUpDown numericUpDownNB;
        private System.Windows.Forms.Button button3;
    }
}