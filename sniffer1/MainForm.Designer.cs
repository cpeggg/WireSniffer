/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/11/25
 * Time: 15:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace sniffer1
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		public System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		
		private System.Windows.Forms.Button button3;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.filter_cancel = new System.Windows.Forms.Button();
			this.filter_key = new System.Windows.Forms.ComboBox();
			this.filter_op = new System.Windows.Forms.ComboBox();
			this.filter_val = new System.Windows.Forms.TextBox();
			this.filter_rules = new System.Windows.Forms.DataGridView();
			this.过滤类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.操作符 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.filter_add = new System.Windows.Forms.Button();
			this.filter_apply = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.button7 = new System.Windows.Forms.Button();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.button8 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.filter_rules)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("华文楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label1.Location = new System.Drawing.Point(14, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(155, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "检测到以下设备";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("华文楷体", 12F);
			this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label2.Location = new System.Drawing.Point(475, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(206, 24);
			this.label2.TabIndex = 1;
			this.label2.Text = "包列表";
			// 
			// comboBox1
			// 
			this.comboBox1.BackColor = System.Drawing.SystemColors.Control;
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(14, 64);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(210, 22);
			this.comboBox1.TabIndex = 2;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1SelectedIndexChanged);
			this.comboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBox1_KeyDown);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("华文楷体", 10F);
			this.button1.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.button1.Location = new System.Drawing.Point(14, 112);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(82, 34);
			this.button1.TabIndex = 3;
			this.button1.Text = "开始抓包";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("华文楷体", 10F);
			this.button2.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.button2.Location = new System.Drawing.Point(177, 112);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(82, 34);
			this.button2.TabIndex = 4;
			this.button2.Text = "停止抓包";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowDrop = true;
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.Column1,
			this.Column2,
			this.Column3,
			this.Column4,
			this.Column5,
			this.Column6});
			this.dataGridView1.Location = new System.Drawing.Point(475, 72);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridView1.Size = new System.Drawing.Size(799, 307);
			this.dataGridView1.TabIndex = 5;
			this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "协议";
			this.Column1.Name = "Column1";
			this.Column1.ReadOnly = true;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "源IP地址";
			this.Column2.Name = "Column2";
			this.Column2.ReadOnly = true;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "目的IP地址";
			this.Column3.Name = "Column3";
			this.Column3.ReadOnly = true;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "时间";
			this.Column4.Name = "Column4";
			this.Column4.ReadOnly = true;
			// 
			// Column5
			// 
			this.Column5.HeaderText = "信息";
			this.Column5.Name = "Column5";
			this.Column5.ReadOnly = true;
			// 
			// Column6
			// 
			this.Column6.HeaderText = "索引";
			this.Column6.Name = "Column6";
			this.Column6.ReadOnly = true;
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("华文楷体", 10F);
			this.button3.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.button3.Location = new System.Drawing.Point(177, 178);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(82, 34);
			this.button3.TabIndex = 7;
			this.button3.Text = "保存包";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// textBox1
			// 
			this.textBox1.AllowDrop = true;
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Font = new System.Drawing.Font("华文楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textBox1.ForeColor = System.Drawing.SystemColors.Control;
			this.textBox1.Location = new System.Drawing.Point(475, 414);
			this.textBox1.Margin = new System.Windows.Forms.Padding(2);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(798, 188);
			this.textBox1.TabIndex = 8;
			this.textBox1.Text = "双击包以显示详细内容...";
			// 
			// button4
			// 
			this.button4.Font = new System.Drawing.Font("华文楷体", 10F);
			this.button4.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.button4.Location = new System.Drawing.Point(96, 178);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(82, 34);
			this.button4.TabIndex = 10;
			this.button4.Text = "查询包";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Button4Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("华文楷体", 10F);
			this.button5.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.button5.Location = new System.Drawing.Point(96, 112);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(82, 34);
			this.button5.TabIndex = 11;
			this.button5.Text = "继续抓包";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.Button5Click);
			// 
			// filter_cancel
			// 
			this.filter_cancel.Font = new System.Drawing.Font("华文楷体", 10F);
			this.filter_cancel.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.filter_cancel.Location = new System.Drawing.Point(339, 566);
			this.filter_cancel.Name = "filter_cancel";
			this.filter_cancel.Size = new System.Drawing.Size(82, 34);
			this.filter_cancel.TabIndex = 12;
			this.filter_cancel.Text = "取消过滤";
			this.filter_cancel.UseVisualStyleBackColor = true;
			this.filter_cancel.Click += new System.EventHandler(this.Filter_cancelClick);
			// 
			// filter_key
			// 
			this.filter_key.BackColor = System.Drawing.SystemColors.Control;
			this.filter_key.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.filter_key.FormattingEnabled = true;
			this.filter_key.Items.AddRange(new object[] {
			"IP地址",
			"源IP",
			"目的IP",
			"端口",
			"源端口",
			"目的端口",
			"IP版本",
			"协议",
			"应用数据",
			"合法校验和"});
			this.filter_key.Location = new System.Drawing.Point(14, 282);
			this.filter_key.Name = "filter_key";
			this.filter_key.Size = new System.Drawing.Size(109, 22);
			this.filter_key.TabIndex = 13;
			// 
			// filter_op
			// 
			this.filter_op.BackColor = System.Drawing.SystemColors.Control;
			this.filter_op.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.filter_op.FormattingEnabled = true;
			this.filter_op.Items.AddRange(new object[] {
			"等于",
			"不等于",
			"包含"});
			this.filter_op.Location = new System.Drawing.Point(127, 282);
			this.filter_op.Name = "filter_op";
			this.filter_op.Size = new System.Drawing.Size(109, 22);
			this.filter_op.TabIndex = 14;
			// 
			// filter_val
			// 
			this.filter_val.BackColor = System.Drawing.SystemColors.Control;
			this.filter_val.Location = new System.Drawing.Point(243, 282);
			this.filter_val.Multiline = true;
			this.filter_val.Name = "filter_val";
			this.filter_val.Size = new System.Drawing.Size(97, 23);
			this.filter_val.TabIndex = 15;
			this.filter_val.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Filter_valKeyDown);
			// 
			// filter_rules
			// 
			this.filter_rules.AllowUserToAddRows = false;
			this.filter_rules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.filter_rules.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
			this.filter_rules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.filter_rules.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.过滤类型,
			this.操作符,
			this.值});
			this.filter_rules.Location = new System.Drawing.Point(14, 360);
			this.filter_rules.Name = "filter_rules";
			this.filter_rules.ReadOnly = true;
			this.filter_rules.RowHeadersVisible = false;
			this.filter_rules.RowTemplate.Height = 23;
			this.filter_rules.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.filter_rules.Size = new System.Drawing.Size(408, 163);
			this.filter_rules.TabIndex = 16;
			// 
			// 过滤类型
			// 
			this.过滤类型.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.过滤类型.Frozen = true;
			this.过滤类型.HeaderText = "过滤类型";
			this.过滤类型.Name = "过滤类型";
			this.过滤类型.ReadOnly = true;
			this.过滤类型.Width = 116;
			// 
			// 操作符
			// 
			this.操作符.HeaderText = "操作符";
			this.操作符.Name = "操作符";
			this.操作符.ReadOnly = true;
			// 
			// 值
			// 
			this.值.HeaderText = "值";
			this.值.Name = "值";
			this.值.ReadOnly = true;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("华文楷体", 10F);
			this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label4.Location = new System.Drawing.Point(16, 258);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(91, 19);
			this.label4.TabIndex = 17;
			this.label4.Text = "过滤类型";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("华文楷体", 10F);
			this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label5.Location = new System.Drawing.Point(128, 259);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(71, 19);
			this.label5.TabIndex = 18;
			this.label5.Text = "操作符";
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("华文楷体", 10F);
			this.label6.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label6.Location = new System.Drawing.Point(241, 258);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(73, 19);
			this.label6.TabIndex = 19;
			this.label6.Text = "过滤值";
			// 
			// filter_add
			// 
			this.filter_add.Font = new System.Drawing.Font("华文楷体", 10F);
			this.filter_add.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.filter_add.Location = new System.Drawing.Point(341, 277);
			this.filter_add.Name = "filter_add";
			this.filter_add.Size = new System.Drawing.Size(82, 34);
			this.filter_add.TabIndex = 20;
			this.filter_add.Text = "添加规则";
			this.filter_add.UseVisualStyleBackColor = true;
			this.filter_add.Click += new System.EventHandler(this.filter_addClick);
			// 
			// filter_apply
			// 
			this.filter_apply.Font = new System.Drawing.Font("华文楷体", 10F);
			this.filter_apply.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.filter_apply.Location = new System.Drawing.Point(13, 566);
			this.filter_apply.Name = "filter_apply";
			this.filter_apply.Size = new System.Drawing.Size(82, 34);
			this.filter_apply.TabIndex = 21;
			this.filter_apply.Text = "应用过滤";
			this.filter_apply.UseVisualStyleBackColor = true;
			this.filter_apply.Click += new System.EventHandler(this.Button8Click);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("华文楷体", 10F);
			this.button6.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.button6.Location = new System.Drawing.Point(178, 566);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(82, 34);
			this.button6.TabIndex = 22;
			this.button6.Text = "删除规则";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.Button6Click);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("华文楷体", 12F);
			this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label7.Location = new System.Drawing.Point(475, 387);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(206, 24);
			this.label7.TabIndex = 23;
			this.label7.Text = "详细信息";
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(14, 178);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(82, 34);
			this.button7.TabIndex = 24;
			this.button7.Text = "打开包";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.Button7Click);
			// 
			// radioButton1
			// 
			this.radioButton1.Location = new System.Drawing.Point(14, 322);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(104, 24);
			this.radioButton1.TabIndex = 25;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "与过滤";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(95, 322);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(104, 24);
			this.radioButton2.TabIndex = 26;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = " 或过滤";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(1095, 388);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124, 16);
			this.label3.TabIndex = 27;
			this.label3.Text = "当前速率：0 byte/s";
			// 
			// chart1
			// 
			this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.chart1.BorderSkin.BackColor = System.Drawing.Color.Gainsboro;
			this.chart1.BorderSkin.BorderColor = System.Drawing.Color.Silver;
			this.chart1.BorderSkin.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
			chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
			chartArea1.AxisX.Title = "启动时间/s";
			chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
			chartArea1.AxisY.Title = "流量大小/byte";
			chartArea1.Name = "ChartArea1";
			this.chart1.ChartAreas.Add(chartArea1);
			legend1.Name = "traffic flow";
			this.chart1.Legends.Add(legend1);
			this.chart1.Location = new System.Drawing.Point(14, 12);
			this.chart1.Name = "chart1";
			series1.ChartArea = "ChartArea1";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			series1.Legend = "traffic flow";
			series1.Name = "traffic flow";
			this.chart1.Series.Add(series1);
			this.chart1.Size = new System.Drawing.Size(964, 588);
			this.chart1.TabIndex = 28;
			this.chart1.Text = "chart1";
			this.chart1.Visible = false;
			// 
			// button8
			// 
			this.button8.Enabled = false;
			this.button8.Location = new System.Drawing.Point(984, 385);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(105, 23);
			this.button8.TabIndex = 29;
			this.button8.Text = "流量统计";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(1276, 613);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.filter_apply);
			this.Controls.Add(this.filter_add);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.filter_rules);
			this.Controls.Add(this.filter_val);
			this.Controls.Add(this.filter_op);
			this.Controls.Add(this.filter_key);
			this.Controls.Add(this.filter_cancel);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("华文楷体", 10F);
			this.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "sniffer1";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFormDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFormDragEnter);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.filter_rules)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button filter_cancel;
        private System.Windows.Forms.ComboBox filter_key;
        private System.Windows.Forms.ComboBox filter_op;
        private System.Windows.Forms.TextBox filter_val;
        private System.Windows.Forms.DataGridView filter_rules;
        private System.Windows.Forms.DataGridViewTextBoxColumn 过滤类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 操作符;
        private System.Windows.Forms.DataGridViewTextBoxColumn 值;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button filter_add;
        private System.Windows.Forms.Button filter_apply;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button8;
    }
		
	}


