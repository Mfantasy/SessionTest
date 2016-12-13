namespace CoAP.Tester
{
    partial class ResponseCheckStepForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelCode = new System.Windows.Forms.Label();
            this.cbxCode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.checkMessageIdMatch = new System.Windows.Forms.CheckBox();
            this.checkContentType = new System.Windows.Forms.CheckBox();
            this.cbxContentType = new System.Windows.Forms.ComboBox();
            this.checkLocationPath = new System.Windows.Forms.CheckBox();
            this.tbxLocationPath = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkLocationQuery = new System.Windows.Forms.CheckBox();
            this.tbxLocationQuery = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkObserve = new System.Windows.Forms.CheckBox();
            this.tbxObserve = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkToken = new System.Windows.Forms.CheckBox();
            this.txtToken = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkAccept = new System.Windows.Forms.CheckBox();
            this.cbAccept = new System.Windows.Forms.ComboBox();
            this.checkUriQuery = new System.Windows.Forms.CheckBox();
            this.txtUriQuery = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkUriPath = new System.Windows.Forms.CheckBox();
            this.txtUriPath = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkBlock1 = new System.Windows.Forms.CheckBox();
            this.txtBlock1 = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkBlock2 = new System.Windows.Forms.CheckBox();
            this.txtBlock2 = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkUriHost = new System.Windows.Forms.CheckBox();
            this.txtUriHost = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkUriPort = new System.Windows.Forms.CheckBox();
            this.txtUriPort = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkProxyUri = new System.Windows.Forms.CheckBox();
            this.txtProxyUri = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkMaxAge = new System.Windows.Forms.CheckBox();
            this.txtMaxAge = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkIfMatch = new System.Windows.Forms.CheckBox();
            this.txtIfMatch = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkETag = new System.Windows.Forms.CheckBox();
            this.txtETag = new CoAP.Browser.Controls.WatermarkTextBox();
            this.checkPayLoad = new System.Windows.Forms.CheckBox();
            this.txtPayLoad = new CoAP.Browser.Controls.WatermarkTextBox();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(284, 342);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 31);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "保存(&S)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(392, 342);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 31);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(771, 330);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Request options";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelCode);
            this.flowLayoutPanel1.Controls.Add(this.cbxCode);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.cbxType);
            this.flowLayoutPanel1.Controls.Add(this.checkMessageIdMatch);
            this.flowLayoutPanel1.Controls.Add(this.checkContentType);
            this.flowLayoutPanel1.Controls.Add(this.cbxContentType);
            this.flowLayoutPanel1.Controls.Add(this.checkLocationPath);
            this.flowLayoutPanel1.Controls.Add(this.tbxLocationPath);
            this.flowLayoutPanel1.Controls.Add(this.checkLocationQuery);
            this.flowLayoutPanel1.Controls.Add(this.tbxLocationQuery);
            this.flowLayoutPanel1.Controls.Add(this.checkObserve);
            this.flowLayoutPanel1.Controls.Add(this.tbxObserve);
            this.flowLayoutPanel1.Controls.Add(this.checkToken);
            this.flowLayoutPanel1.Controls.Add(this.txtToken);
            this.flowLayoutPanel1.Controls.Add(this.checkAccept);
            this.flowLayoutPanel1.Controls.Add(this.cbAccept);
            this.flowLayoutPanel1.Controls.Add(this.checkUriQuery);
            this.flowLayoutPanel1.Controls.Add(this.txtUriQuery);
            this.flowLayoutPanel1.Controls.Add(this.checkUriPath);
            this.flowLayoutPanel1.Controls.Add(this.txtUriPath);
            this.flowLayoutPanel1.Controls.Add(this.checkBlock1);
            this.flowLayoutPanel1.Controls.Add(this.txtBlock1);
            this.flowLayoutPanel1.Controls.Add(this.checkBlock2);
            this.flowLayoutPanel1.Controls.Add(this.txtBlock2);
            this.flowLayoutPanel1.Controls.Add(this.checkUriHost);
            this.flowLayoutPanel1.Controls.Add(this.txtUriHost);
            this.flowLayoutPanel1.Controls.Add(this.checkUriPort);
            this.flowLayoutPanel1.Controls.Add(this.txtUriPort);
            this.flowLayoutPanel1.Controls.Add(this.checkProxyUri);
            this.flowLayoutPanel1.Controls.Add(this.txtProxyUri);
            this.flowLayoutPanel1.Controls.Add(this.checkMaxAge);
            this.flowLayoutPanel1.Controls.Add(this.txtMaxAge);
            this.flowLayoutPanel1.Controls.Add(this.checkIfMatch);
            this.flowLayoutPanel1.Controls.Add(this.txtIfMatch);
            this.flowLayoutPanel1.Controls.Add(this.checkETag);
            this.flowLayoutPanel1.Controls.Add(this.txtETag);
            this.flowLayoutPanel1.Controls.Add(this.checkPayLoad);
            this.flowLayoutPanel1.Controls.Add(this.txtPayLoad);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(8, 27);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(755, 295);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // labelCode
            // 
            this.labelCode.Location = new System.Drawing.Point(4, 4);
            this.labelCode.Margin = new System.Windows.Forms.Padding(4);
            this.labelCode.Name = "labelCode";
            this.labelCode.Padding = new System.Windows.Forms.Padding(4);
            this.labelCode.Size = new System.Drawing.Size(152, 28);
            this.labelCode.TabIndex = 104;
            this.labelCode.Text = "&Code";
            // 
            // cbxCode
            // 
            this.cbxCode.DisplayMember = "Name";
            this.cbxCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCode.FormattingEnabled = true;
            this.cbxCode.ItemHeight = 16;
            this.cbxCode.Location = new System.Drawing.Point(164, 4);
            this.cbxCode.Margin = new System.Windows.Forms.Padding(4);
            this.cbxCode.Name = "cbxCode";
            this.cbxCode.Size = new System.Drawing.Size(205, 24);
            this.cbxCode.TabIndex = 107;
            this.cbxCode.ValueMember = "Value";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(377, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(4);
            this.label2.Size = new System.Drawing.Size(152, 28);
            this.label2.TabIndex = 108;
            this.label2.Text = "&Type";
            // 
            // cbxType
            // 
            this.cbxType.DisplayMember = "Name";
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.ItemHeight = 16;
            this.cbxType.Location = new System.Drawing.Point(537, 4);
            this.cbxType.Margin = new System.Windows.Forms.Padding(4);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(205, 24);
            this.cbxType.TabIndex = 109;
            this.cbxType.ValueMember = "Value";
            // 
            // checkMessageIdMatch
            // 
            this.checkMessageIdMatch.Location = new System.Drawing.Point(4, 40);
            this.checkMessageIdMatch.Margin = new System.Windows.Forms.Padding(4);
            this.checkMessageIdMatch.Name = "checkMessageIdMatch";
            this.checkMessageIdMatch.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkMessageIdMatch.Size = new System.Drawing.Size(367, 28);
            this.checkMessageIdMatch.TabIndex = 131;
            this.checkMessageIdMatch.Text = "Match message ID";
            this.checkMessageIdMatch.UseVisualStyleBackColor = true;
            // 
            // checkContentType
            // 
            this.checkContentType.Location = new System.Drawing.Point(379, 40);
            this.checkContentType.Margin = new System.Windows.Forms.Padding(4);
            this.checkContentType.Name = "checkContentType";
            this.checkContentType.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkContentType.Size = new System.Drawing.Size(152, 28);
            this.checkContentType.TabIndex = 132;
            this.checkContentType.Text = "Content-Type";
            this.checkContentType.UseVisualStyleBackColor = true;
            // 
            // cbxContentType
            // 
            this.cbxContentType.DisplayMember = "Name";
            this.cbxContentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxContentType.FormattingEnabled = true;
            this.cbxContentType.ItemHeight = 16;
            this.cbxContentType.Location = new System.Drawing.Point(539, 40);
            this.cbxContentType.Margin = new System.Windows.Forms.Padding(4);
            this.cbxContentType.Name = "cbxContentType";
            this.cbxContentType.Size = new System.Drawing.Size(205, 24);
            this.cbxContentType.TabIndex = 133;
            this.cbxContentType.ValueMember = "Value";
            // 
            // checkLocationPath
            // 
            this.checkLocationPath.Location = new System.Drawing.Point(4, 76);
            this.checkLocationPath.Margin = new System.Windows.Forms.Padding(4);
            this.checkLocationPath.Name = "checkLocationPath";
            this.checkLocationPath.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkLocationPath.Size = new System.Drawing.Size(152, 28);
            this.checkLocationPath.TabIndex = 132;
            this.checkLocationPath.Text = "Location-Path";
            this.checkLocationPath.UseVisualStyleBackColor = true;
            // 
            // tbxLocationPath
            // 
            this.tbxLocationPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbxLocationPath.EmptyTextTip = "not set";
            this.tbxLocationPath.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.tbxLocationPath.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxLocationPath.Location = new System.Drawing.Point(164, 76);
            this.tbxLocationPath.Margin = new System.Windows.Forms.Padding(4);
            this.tbxLocationPath.Name = "tbxLocationPath";
            this.tbxLocationPath.Size = new System.Drawing.Size(205, 26);
            this.tbxLocationPath.TabIndex = 134;
            // 
            // checkLocationQuery
            // 
            this.checkLocationQuery.Location = new System.Drawing.Point(377, 76);
            this.checkLocationQuery.Margin = new System.Windows.Forms.Padding(4);
            this.checkLocationQuery.Name = "checkLocationQuery";
            this.checkLocationQuery.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkLocationQuery.Size = new System.Drawing.Size(152, 28);
            this.checkLocationQuery.TabIndex = 136;
            this.checkLocationQuery.Text = "Location-Query";
            this.checkLocationQuery.UseVisualStyleBackColor = true;
            // 
            // tbxLocationQuery
            // 
            this.tbxLocationQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbxLocationQuery.EmptyTextTip = "not set";
            this.tbxLocationQuery.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.tbxLocationQuery.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxLocationQuery.Location = new System.Drawing.Point(537, 76);
            this.tbxLocationQuery.Margin = new System.Windows.Forms.Padding(4);
            this.tbxLocationQuery.Name = "tbxLocationQuery";
            this.tbxLocationQuery.Size = new System.Drawing.Size(205, 26);
            this.tbxLocationQuery.TabIndex = 137;
            // 
            // checkObserve
            // 
            this.checkObserve.Location = new System.Drawing.Point(4, 112);
            this.checkObserve.Margin = new System.Windows.Forms.Padding(4);
            this.checkObserve.Name = "checkObserve";
            this.checkObserve.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkObserve.Size = new System.Drawing.Size(152, 28);
            this.checkObserve.TabIndex = 136;
            this.checkObserve.Text = "&Observe";
            this.checkObserve.UseVisualStyleBackColor = true;
            // 
            // tbxObserve
            // 
            this.tbxObserve.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbxObserve.EmptyTextTip = "use integer";
            this.tbxObserve.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.tbxObserve.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxObserve.Location = new System.Drawing.Point(164, 112);
            this.tbxObserve.Margin = new System.Windows.Forms.Padding(4);
            this.tbxObserve.Name = "tbxObserve";
            this.tbxObserve.Size = new System.Drawing.Size(205, 26);
            this.tbxObserve.TabIndex = 138;
            // 
            // checkToken
            // 
            this.checkToken.Location = new System.Drawing.Point(377, 112);
            this.checkToken.Margin = new System.Windows.Forms.Padding(4);
            this.checkToken.Name = "checkToken";
            this.checkToken.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkToken.Size = new System.Drawing.Size(152, 28);
            this.checkToken.TabIndex = 136;
            this.checkToken.Text = "To&ken";
            this.checkToken.UseVisualStyleBackColor = true;
            // 
            // txtToken
            // 
            this.txtToken.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtToken.EmptyTextTip = "use hex (0x..) or string";
            this.txtToken.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtToken.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtToken.Location = new System.Drawing.Point(537, 112);
            this.txtToken.Margin = new System.Windows.Forms.Padding(4);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(205, 26);
            this.txtToken.TabIndex = 139;
            // 
            // checkAccept
            // 
            this.checkAccept.Location = new System.Drawing.Point(4, 148);
            this.checkAccept.Margin = new System.Windows.Forms.Padding(4);
            this.checkAccept.Name = "checkAccept";
            this.checkAccept.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkAccept.Size = new System.Drawing.Size(152, 28);
            this.checkAccept.TabIndex = 142;
            this.checkAccept.Text = "&Accept";
            this.checkAccept.UseVisualStyleBackColor = true;
            // 
            // cbAccept
            // 
            this.cbAccept.DisplayMember = "Name";
            this.cbAccept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAccept.FormattingEnabled = true;
            this.cbAccept.ItemHeight = 16;
            this.cbAccept.Location = new System.Drawing.Point(164, 148);
            this.cbAccept.Margin = new System.Windows.Forms.Padding(4);
            this.cbAccept.Name = "cbAccept";
            this.cbAccept.Size = new System.Drawing.Size(205, 24);
            this.cbAccept.TabIndex = 143;
            this.cbAccept.ValueMember = "Value";
            // 
            // checkUriQuery
            // 
            this.checkUriQuery.Location = new System.Drawing.Point(377, 148);
            this.checkUriQuery.Margin = new System.Windows.Forms.Padding(4);
            this.checkUriQuery.Name = "checkUriQuery";
            this.checkUriQuery.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkUriQuery.Size = new System.Drawing.Size(152, 28);
            this.checkUriQuery.TabIndex = 144;
            this.checkUriQuery.Text = "Uri-Query";
            this.checkUriQuery.UseVisualStyleBackColor = true;
            this.checkUriQuery.Visible = false;
            // 
            // txtUriQuery
            // 
            this.txtUriQuery.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUriQuery.EmptyTextTip = "not set";
            this.txtUriQuery.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtUriQuery.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUriQuery.Location = new System.Drawing.Point(537, 148);
            this.txtUriQuery.Margin = new System.Windows.Forms.Padding(4);
            this.txtUriQuery.Name = "txtUriQuery";
            this.txtUriQuery.Size = new System.Drawing.Size(205, 26);
            this.txtUriQuery.TabIndex = 145;
            this.txtUriQuery.Visible = false;
            // 
            // checkUriPath
            // 
            this.checkUriPath.Location = new System.Drawing.Point(4, 184);
            this.checkUriPath.Margin = new System.Windows.Forms.Padding(4);
            this.checkUriPath.Name = "checkUriPath";
            this.checkUriPath.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkUriPath.Size = new System.Drawing.Size(152, 28);
            this.checkUriPath.TabIndex = 146;
            this.checkUriPath.Text = "Uri-&Path";
            this.checkUriPath.UseVisualStyleBackColor = true;
            this.checkUriPath.Visible = false;
            // 
            // txtUriPath
            // 
            this.txtUriPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUriPath.EmptyTextTip = "not set";
            this.txtUriPath.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtUriPath.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUriPath.Location = new System.Drawing.Point(164, 184);
            this.txtUriPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtUriPath.Name = "txtUriPath";
            this.txtUriPath.Size = new System.Drawing.Size(205, 26);
            this.txtUriPath.TabIndex = 147;
            this.txtUriPath.Visible = false;
            // 
            // checkBlock1
            // 
            this.checkBlock1.Location = new System.Drawing.Point(377, 184);
            this.checkBlock1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBlock1.Name = "checkBlock1";
            this.checkBlock1.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkBlock1.Size = new System.Drawing.Size(152, 28);
            this.checkBlock1.TabIndex = 148;
            this.checkBlock1.Text = "Block1";
            this.checkBlock1.UseVisualStyleBackColor = true;
            // 
            // txtBlock1
            // 
            this.txtBlock1.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBlock1.EmptyTextTip = "use integer";
            this.txtBlock1.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtBlock1.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBlock1.Location = new System.Drawing.Point(537, 184);
            this.txtBlock1.Margin = new System.Windows.Forms.Padding(4);
            this.txtBlock1.Name = "txtBlock1";
            this.txtBlock1.Size = new System.Drawing.Size(205, 26);
            this.txtBlock1.TabIndex = 149;
            // 
            // checkBlock2
            // 
            this.checkBlock2.Location = new System.Drawing.Point(4, 220);
            this.checkBlock2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBlock2.Name = "checkBlock2";
            this.checkBlock2.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkBlock2.Size = new System.Drawing.Size(152, 28);
            this.checkBlock2.TabIndex = 150;
            this.checkBlock2.Text = "Block2";
            this.checkBlock2.UseVisualStyleBackColor = true;
            // 
            // txtBlock2
            // 
            this.txtBlock2.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBlock2.EmptyTextTip = "use integer";
            this.txtBlock2.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtBlock2.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBlock2.Location = new System.Drawing.Point(164, 220);
            this.txtBlock2.Margin = new System.Windows.Forms.Padding(4);
            this.txtBlock2.Name = "txtBlock2";
            this.txtBlock2.Size = new System.Drawing.Size(205, 26);
            this.txtBlock2.TabIndex = 151;
            // 
            // checkUriHost
            // 
            this.checkUriHost.Location = new System.Drawing.Point(377, 220);
            this.checkUriHost.Margin = new System.Windows.Forms.Padding(4);
            this.checkUriHost.Name = "checkUriHost";
            this.checkUriHost.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkUriHost.Size = new System.Drawing.Size(152, 28);
            this.checkUriHost.TabIndex = 152;
            this.checkUriHost.Text = "Uri-&Host";
            this.checkUriHost.UseVisualStyleBackColor = true;
            this.checkUriHost.Visible = false;
            // 
            // txtUriHost
            // 
            this.txtUriHost.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUriHost.EmptyTextTip = "not set";
            this.txtUriHost.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtUriHost.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUriHost.Location = new System.Drawing.Point(537, 220);
            this.txtUriHost.Margin = new System.Windows.Forms.Padding(4);
            this.txtUriHost.Name = "txtUriHost";
            this.txtUriHost.Size = new System.Drawing.Size(205, 26);
            this.txtUriHost.TabIndex = 153;
            this.txtUriHost.Visible = false;
            // 
            // checkUriPort
            // 
            this.checkUriPort.Location = new System.Drawing.Point(4, 256);
            this.checkUriPort.Margin = new System.Windows.Forms.Padding(4);
            this.checkUriPort.Name = "checkUriPort";
            this.checkUriPort.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkUriPort.Size = new System.Drawing.Size(152, 28);
            this.checkUriPort.TabIndex = 154;
            this.checkUriPort.Text = "Uri-&Port";
            this.checkUriPort.UseVisualStyleBackColor = true;
            this.checkUriPort.Visible = false;
            // 
            // txtUriPort
            // 
            this.txtUriPort.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtUriPort.EmptyTextTip = "use integer";
            this.txtUriPort.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtUriPort.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUriPort.Location = new System.Drawing.Point(164, 256);
            this.txtUriPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtUriPort.Name = "txtUriPort";
            this.txtUriPort.Size = new System.Drawing.Size(205, 26);
            this.txtUriPort.TabIndex = 155;
            this.txtUriPort.Visible = false;
            // 
            // checkProxyUri
            // 
            this.checkProxyUri.Location = new System.Drawing.Point(377, 256);
            this.checkProxyUri.Margin = new System.Windows.Forms.Padding(4);
            this.checkProxyUri.Name = "checkProxyUri";
            this.checkProxyUri.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkProxyUri.Size = new System.Drawing.Size(152, 28);
            this.checkProxyUri.TabIndex = 156;
            this.checkProxyUri.Text = "Proxy-&Uri";
            this.checkProxyUri.UseVisualStyleBackColor = true;
            this.checkProxyUri.Visible = false;
            // 
            // txtProxyUri
            // 
            this.txtProxyUri.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtProxyUri.EmptyTextTip = "not set";
            this.txtProxyUri.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtProxyUri.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtProxyUri.Location = new System.Drawing.Point(537, 256);
            this.txtProxyUri.Margin = new System.Windows.Forms.Padding(4);
            this.txtProxyUri.Name = "txtProxyUri";
            this.txtProxyUri.Size = new System.Drawing.Size(205, 26);
            this.txtProxyUri.TabIndex = 157;
            this.txtProxyUri.Visible = false;
            // 
            // checkMaxAge
            // 
            this.checkMaxAge.Location = new System.Drawing.Point(4, 292);
            this.checkMaxAge.Margin = new System.Windows.Forms.Padding(4);
            this.checkMaxAge.Name = "checkMaxAge";
            this.checkMaxAge.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkMaxAge.Size = new System.Drawing.Size(152, 28);
            this.checkMaxAge.TabIndex = 158;
            this.checkMaxAge.Text = "Max-&Age";
            this.checkMaxAge.UseVisualStyleBackColor = true;
            // 
            // txtMaxAge
            // 
            this.txtMaxAge.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMaxAge.EmptyTextTip = "use integer";
            this.txtMaxAge.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtMaxAge.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMaxAge.Location = new System.Drawing.Point(164, 292);
            this.txtMaxAge.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxAge.Name = "txtMaxAge";
            this.txtMaxAge.Size = new System.Drawing.Size(205, 26);
            this.txtMaxAge.TabIndex = 159;
            // 
            // checkIfMatch
            // 
            this.checkIfMatch.Location = new System.Drawing.Point(377, 292);
            this.checkIfMatch.Margin = new System.Windows.Forms.Padding(4);
            this.checkIfMatch.Name = "checkIfMatch";
            this.checkIfMatch.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkIfMatch.Size = new System.Drawing.Size(152, 28);
            this.checkIfMatch.TabIndex = 160;
            this.checkIfMatch.Text = "If-&Match";
            this.checkIfMatch.UseVisualStyleBackColor = true;
            this.checkIfMatch.Visible = false;
            // 
            // txtIfMatch
            // 
            this.txtIfMatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtIfMatch.EmptyTextTip = "not set";
            this.txtIfMatch.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtIfMatch.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtIfMatch.Location = new System.Drawing.Point(537, 292);
            this.txtIfMatch.Margin = new System.Windows.Forms.Padding(4);
            this.txtIfMatch.Name = "txtIfMatch";
            this.txtIfMatch.Size = new System.Drawing.Size(205, 26);
            this.txtIfMatch.TabIndex = 161;
            this.txtIfMatch.Visible = false;
            // 
            // checkETag
            // 
            this.checkETag.Location = new System.Drawing.Point(4, 328);
            this.checkETag.Margin = new System.Windows.Forms.Padding(4);
            this.checkETag.Name = "checkETag";
            this.checkETag.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkETag.Size = new System.Drawing.Size(152, 28);
            this.checkETag.TabIndex = 162;
            this.checkETag.Text = "ETag";
            this.checkETag.UseVisualStyleBackColor = true;
            // 
            // txtETag
            // 
            this.txtETag.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtETag.EmptyTextTip = "not set";
            this.txtETag.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtETag.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtETag.Location = new System.Drawing.Point(164, 328);
            this.txtETag.Margin = new System.Windows.Forms.Padding(4);
            this.txtETag.Name = "txtETag";
            this.txtETag.Size = new System.Drawing.Size(205, 26);
            this.txtETag.TabIndex = 163;
            // 
            // checkPayLoad
            // 
            this.checkPayLoad.Location = new System.Drawing.Point(377, 328);
            this.checkPayLoad.Margin = new System.Windows.Forms.Padding(4);
            this.checkPayLoad.Name = "checkPayLoad";
            this.checkPayLoad.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.checkPayLoad.Size = new System.Drawing.Size(152, 28);
            this.checkPayLoad.TabIndex = 140;
            this.checkPayLoad.Text = "&PayLoad";
            this.checkPayLoad.UseVisualStyleBackColor = true;
            // 
            // txtPayLoad
            // 
            this.txtPayLoad.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPayLoad.EmptyTextTip = "use hex (0x..) or string";
            this.txtPayLoad.EmptyTextTipColor = System.Drawing.Color.Silver;
            this.txtPayLoad.EmptyTextTipFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPayLoad.Location = new System.Drawing.Point(537, 328);
            this.txtPayLoad.Margin = new System.Windows.Forms.Padding(4);
            this.txtPayLoad.Multiline = true;
            this.txtPayLoad.Name = "txtPayLoad";
            this.txtPayLoad.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPayLoad.Size = new System.Drawing.Size(205, 80);
            this.txtPayLoad.TabIndex = 164;
            this.txtPayLoad.WordWrap = false;
            // 
            // ResponseCheckStepForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(779, 395);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResponseCheckStepForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Response Check Step";
            this.Load += new System.EventHandler(this.ResponseCheckStepForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.ComboBox cbxCode;
        private System.Windows.Forms.CheckBox checkMessageIdMatch;
        private System.Windows.Forms.CheckBox checkContentType;
        private System.Windows.Forms.ComboBox cbxContentType;
        private System.Windows.Forms.CheckBox checkLocationPath;
        private Browser.Controls.WatermarkTextBox tbxLocationPath;
        private System.Windows.Forms.CheckBox checkLocationQuery;
        private Browser.Controls.WatermarkTextBox tbxLocationQuery;
        private System.Windows.Forms.CheckBox checkObserve;
        private Browser.Controls.WatermarkTextBox tbxObserve;
        private System.Windows.Forms.CheckBox checkToken;
        private Browser.Controls.WatermarkTextBox txtToken;
        private System.Windows.Forms.CheckBox checkPayLoad;
        private System.Windows.Forms.CheckBox checkAccept;
        private System.Windows.Forms.ComboBox cbAccept;
        private System.Windows.Forms.CheckBox checkUriQuery;
        private Browser.Controls.WatermarkTextBox txtUriQuery;
        private System.Windows.Forms.CheckBox checkUriPath;
        private Browser.Controls.WatermarkTextBox txtUriPath;
        private System.Windows.Forms.CheckBox checkBlock1;
        private Browser.Controls.WatermarkTextBox txtBlock1;
        private System.Windows.Forms.CheckBox checkBlock2;
        private Browser.Controls.WatermarkTextBox txtBlock2;
        private System.Windows.Forms.CheckBox checkUriHost;
        private Browser.Controls.WatermarkTextBox txtUriHost;
        private System.Windows.Forms.CheckBox checkUriPort;
        private Browser.Controls.WatermarkTextBox txtUriPort;
        private System.Windows.Forms.CheckBox checkProxyUri;
        private Browser.Controls.WatermarkTextBox txtProxyUri;
        private System.Windows.Forms.CheckBox checkMaxAge;
        private Browser.Controls.WatermarkTextBox txtMaxAge;
        private System.Windows.Forms.CheckBox checkIfMatch;
        private Browser.Controls.WatermarkTextBox txtIfMatch;
        private System.Windows.Forms.CheckBox checkETag;
        private Browser.Controls.WatermarkTextBox txtETag;
        private Browser.Controls.WatermarkTextBox txtPayLoad;

    }
}