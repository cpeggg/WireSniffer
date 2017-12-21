/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/11/25
 * Time: 15:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SharpPcap;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;

namespace sniffer1
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	
	public  partial class MainForm : Form
	{
		public static MainForm pointer=null;
        private int saved_file_index;   //用以记录当前输出文件的个数
        public MainForm()
		{
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            instance = new SplashScreen();
            instance.Owner = this;
            //instance.BackgroundImage = Properties.Resources.icon; // 载入图片
            instance.FormBorderStyle = FormBorderStyle.None;
            instance.StartPosition = FormStartPosition.CenterScreen;
            instance.Opacity = 0;

            instance.Show();
            InitializeComponent();
			
			combox1_Ini();
           
            this.button2.Enabled = false;
            this.button5.Enabled = false;
            this.filter_cancel.Enabled = false;
			//便于其他窗体调用本窗体
            pointer=this;
            this.applyfilter=false;
            this.radioButton1.Checked=true;
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
		
		
		
		
		
		
		
		//// /////////////////////////////////////////////////////////////////////////////////////////////////////////
		///---------------------------------------变量定义部分-------------------------------------------------------------
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//抓包线程
        private delegate void setDataGridViewDelegate(packet Packet);
        private delegate bool filterCheckDelegate(packet Packet);
        private bool applyfilter;

        private ICaptureDevice device;
        private int readTimeoutMilliseconds;
        public  string filter="";
        //抓到的所有包的所有信息
        public ArrayList packets;
        private ArrayList files;
        SplashScreen instance;
        private int bytes_persec = 0;       //用以统计每秒内的流量大小
        System.Timers.Timer myTimer;
        class traffic_statistics
        {
            private string _time;
            public string Time
            {
                get { return _time; }
                set { _time = value; }
            }
            private int _count;
            public int Count
            {
                get { return _count; }
                set { _count = value; }
            }
            public traffic_statistics(string time,int count)
            {
                _time = time;
                _count = count;
            }
        }
        List<traffic_statistics> traffic_statistics_list;
        System.DateTime start_time;

        //标定是否已保存的标志位
        //private bool is_saved;
        ///	//// /////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///---------------------------------------基本功能部分-------------------------------------------------------------
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void MainFormLoad(object sender, EventArgs e)
		{
            for (int i = 1; i <= 100; ++i)
            { // 实现渐变效果
                instance.Opacity = i / 100.0;
                System.Threading.Thread.Sleep(5);
            }


            myTimer = new System.Timers.Timer(1000);//定时周期1秒
            myTimer.Elapsed += myTimer_Elapsed;//到1秒了做的事件
            myTimer.AutoReset = true; //是否不断重复定时器操作
            Control.CheckForIllegalCrossThreadCalls = false;
            instance.Close();
        }
        private void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (bytes_persec<=1024)
                label3.Text = "当前速率：" + bytes_persec.ToString() + " byte/s";
            else if (bytes_persec<=1048576)
                label3.Text = string.Format("当前速率：{0:F3} Kb / s" ,(bytes_persec/1024.0)) ;
            else label3.Text = string.Format("当前速率：{0:F3} Mb / s", (bytes_persec / 1048576.0));
            TimeSpan ts1 = new TimeSpan(start_time.Ticks);
            TimeSpan ts2 = new TimeSpan(e.SignalTime.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            //你想转的格式
            


            traffic_statistics_list.Add(new traffic_statistics(ts3.ToString("c").Substring(0, 8), bytes_persec));
            
            bytes_persec = 0;
        }
		private void combox1_Ini()
        {
            //初始化下拉菜单的候选值
            ArrayList cboItems1 = new ArrayList();
			//检测设备
            var devices = CaptureDeviceList.Instance;
            //若设备数为零
            if (devices.Count < 1)
            {
                cboItems1.Add(new KeyValuePair<int, string>(-1, "找不到网络设备"));
            }
            else
            {
                int i = 0;
                foreach (ICaptureDevice dev in devices)
                {
                    string dev_friendlyname = dev.ToString();
                    dev_friendlyname = dev_friendlyname.Substring(dev_friendlyname.IndexOf("FriendlyName: "), dev_friendlyname.Length - dev_friendlyname.IndexOf("FriendlyName: ") - "FriendlyName: ".Length);
                    dev_friendlyname = dev_friendlyname.Substring("FriendlyName: ".Length, dev_friendlyname.IndexOf('\n') - "FriendlyName: ".Length);
					//在cboItems1中添加设备
                    cboItems1.Add(new KeyValuePair<int, string>(i, dev_friendlyname));
                    i++;
                }
            }

            //初始化Combox.Items，数据来源是cboItems1 
            comboBox1.ValueMember = "Key";
            comboBox1.DisplayMember = "Value";
            comboBox1.DataSource = cboItems1;
        }
		//开始抓包按钮，按下后显示变为重新抓包
		private  void Button1Click(object sender, EventArgs e)
		{
            start();
            this.button1.Text = "重新抓包";
            myTimer.Enabled = true;
            traffic_statistics_list = new List<traffic_statistics>();
            start_time = DateTime.Now;
            button8.Enabled = false;
        }
		//在设备列表中按下Enter键相当于单击btn1
		void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
                this.Button1Click(new object(), new EventArgs());
        }
		 public void start()
		 {
		 	//禁用开始抓包、继续抓包按钮，启用停止抓包按钮，禁止更改所选设备
		 	this.button1.Enabled = false;
            this.button5.Enabled = false;
            this.comboBox1.Enabled = false;
            this.button2.Enabled = true;
            //清除之前的数据
            this.packets = new ArrayList();
           
            this.dataGridView1.Rows.Clear();
          
            //读取要监听的网卡
            int eth = System.Int32.Parse(this.comboBox1.SelectedValue.ToString());
            var devices = CaptureDeviceList.Instance;
            this.device = devices[eth];
			//超时时间
            this.readTimeoutMilliseconds = 1000;
			//新线程
            Thread newThread = new Thread(new ThreadStart(threadHandler));
            newThread.Start();
		 }
		 
		 private void threadHandler()
        {            
            this.device.Open(DeviceMode.Promiscuous, this.readTimeoutMilliseconds);
            //抓数据包回调事件
            this.device.OnPacketArrival += new PacketArrivalEventHandler(device_OnPacketArrival); 
            //开始监听
            this.device.StartCapture();
        }
		
		private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            PcapPorcessContext(e.Packet);
        }
        private void PcapPorcessContext(SharpPcap.RawCapture pPacket)
        {
			//把包存起来（不论是否过滤）
            packet temp = new packet(pPacket);
            temp.index=packets.Count;
            PacketDotNet.Packet rPacket= PacketDotNet.Packet.ParsePacket(pPacket.LinkLayerType, pPacket.Data);
            bytes_persec += rPacket.Bytes.Length;
            packets.Add(temp);
            //判断是否过滤，若过滤，则调用过滤函数，否则直接显示
            bool flag=true;
            if (applyfilter)
            	flag=apply_filter(temp);
            if(!flag)
            	return;

            show_pac(temp);
        }
		
		void show_pac(packet temp)
		{
			//是否跨线程
			if (this.dataGridView1.InvokeRequired)
            {
                
                    this.dataGridView1.BeginInvoke(new setDataGridViewDelegate(setDataGridView), new object[] { temp });
                
            }
			else
			{
				//依次显示包的各个信息
				int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.FromName(temp.color);
                this.dataGridView1.Rows[index].Cells[0].Value = temp.protocol;
                this.dataGridView1.Rows[index].Cells[1].Value = temp.srcIp;
                this.dataGridView1.Rows[index].Cells[2].Value = temp.destIp;
                this.dataGridView1.Rows[index].Cells[3].Value = temp.time;
                this.dataGridView1.Rows[index].Cells[4].Value = temp.info;
                this.dataGridView1.Rows[index].Cells[5].Value = temp.index;
                this.dataGridView1.FirstDisplayedScrollingRowIndex = this.dataGridView1.Rows.Count - 1;
			}
            
		}
       
        private void setDataGridView(packet Packet)  
        {
            //依次显示包的各个信息
        	int index = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.FromName(Packet.color);
            this.dataGridView1.Rows[index].Cells[0].Value = Packet.protocol;
            this.dataGridView1.Rows[index].Cells[1].Value = Packet.srcIp;
            this.dataGridView1.Rows[index].Cells[2].Value = Packet.destIp;
            this.dataGridView1.Rows[index].Cells[3].Value = Packet.time;
            this.dataGridView1.Rows[index].Cells[4].Value = Packet.info;
            this.dataGridView1.Rows[index].Cells[5].Value = Packet.index;

            this.dataGridView1.FirstDisplayedScrollingRowIndex = this.dataGridView1.Rows.Count - 1;
        }
		//所选设备改变后必须重新开始抓包
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			this.button5.Enabled = false;
			this.button2.Enabled = false;
            this.button1.Enabled = true;
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			stopcatch();
            myTimer.Enabled = false;
            button8.Enabled = true;
		}
		void stopcatch()
		{
			//关设备
			try
            {
                this.device.StopCapture();
                this.device.Close();
            }
            catch (Exception)
            {
                ;
            }
			//恢复GUI
            this.button1.Enabled = true;
            this.button5.Enabled = true;
            this.comboBox1.Enabled = true;
            this.button2.Enabled = false;
		}
		//继续抓包
        void Button5Click(object sender, EventArgs e)
		{
			this.button1.Enabled = false;
            button8.Enabled = false;
            this.comboBox1.Enabled = false;
            this.button2.Enabled = true;
            this.button5.Enabled = false;
            myTimer.Enabled = true;
            //读取要监听的网卡
            int eth = System.Int32.Parse(this.comboBox1.SelectedValue.ToString());
            var devices = CaptureDeviceList.Instance;
            this.device = devices[eth];

            this.readTimeoutMilliseconds = 1000;

            Thread newThread = new Thread(new ThreadStart(threadHandler));
            newThread.Start();
		}
//// /////////////////////////////////////////////////////////////////////////////////////////////////////////
		///---------------------------------------文件保存部分（需调用Search窗体）-------------------------------------------------------------
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void button3_Click(object sender, EventArgs e)
        {
            string capFile = "";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
            sfd.Filter = "PCAP(*.pcap)|*.pcap";
            sfd.OverwritePrompt = true;
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    capFile = sfd.FileName;
                    this.device.Open();
                    SharpPcap.LibPcap.CaptureFileWriterDevice captureFileWriter = new SharpPcap.LibPcap.CaptureFileWriterDevice((SharpPcap.LibPcap.LibPcapLiveDevice)this.device, capFile);
                    int count = this.packets.Count;
//                    foreach (packet i in this.packets)
//                    {
//                    	if(i.index==)captureFileWriter.Write(i.pPacket);
//                    }
					foreach (DataGridViewRow r in dataGridView1.Rows)
				{
					if(	r.Selected||r.Cells[0].Selected||r.Cells[1].Selected||r.Cells[2].Selected||r.Cells[3].Selected||r.Cells[4].Selected||r.Cells[5].Selected)
					{
						int n=int.Parse(r.Cells[5].Value.ToString());
						captureFileWriter.Write(((packet)packets[n]).pPacket);
					}
   						 
				}
                    this.device.Close();
                    
                    MessageBox.Show("保存完毕");
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
            /*string filepath="";
            SaveFileDialog fd = new SaveFileDialog();
            fd.FileName = Application.StartupPath;
            fd.ShowDialog();
            filepath = fd.FileName;
            FileStream fs = new FileStream(filepath, FileMode.Create);
            byte[] bytes = Encoding.Default.GetBytes("本文件为系统保存包时记录的可读文件记录\r\n");
            int length = bytes.Length;
            fs.Write(bytes, 0, length);*/

            /*
            this.dataGridView1.Rows[index].Cells[0].Value = Packet.protocol;
            this.dataGridView1.Rows[index].Cells[1].Value = Packet.srcIp;
            this.dataGridView1.Rows[index].Cells[2].Value = Packet.destIp;
            this.dataGridView1.Rows[index].Cells[3].Value = Packet.time;
            this.dataGridView1.Rows[index].Cells[4].Value = Packet.info;
            this.dataGridView1.Rows[index].Cells[5].Value = packet_index;
            */
            /*
            foreach (packet p in packets)
            {
                bytes = Encoding.Default.GetBytes("=================================\r\n简易信息：\r\nProtocol:  " + p.protocol + "\r\nSource IP:  " + p.srcIp + "\r\nDestination IP:  " + p.destIp + "\r\nTime:  " + p.time + "\r\nInfo:  " + p.info + "\r\n---------------------------------\r\n详细信息： \r\n");
                length = bytes.Length;//Encoding.Default.GetByteCount("简易信息：\r\nProtocol:  " + p.protocol + "\r\nSource IP:  " + p.srcIp + "\r\nDestination IP:  " + p.destIp + "\r\nTime:  " + p.time + "\r\nInfo:  " + p.info + "\r\n详细信息： \r\n");
                fs.Write(bytes, 0, length);
                foreach (KeyValuePair<string, string> kv in p.frame_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                foreach (KeyValuePair<string, string> kv in p.ethernet_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                foreach (KeyValuePair<string, string> kv in p.ip_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                foreach (KeyValuePair<string, string> kv in p.arp_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                foreach (KeyValuePair<string, string> kv in p.icmp_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                foreach (KeyValuePair<string, string> kv in p.igmp_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                foreach (KeyValuePair<string, string> kv in p.tcp_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                foreach (KeyValuePair<string, string> kv in p.udp_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                foreach (KeyValuePair<string, string> kv in p.application_info)
                {
                    bytes = Encoding.Default.GetBytes(kv.Key + " : " + kv.Value + "\r\n");
                    length = bytes.Length;//Encoding.Default.GetByteCount(kv.Key + " : " + kv.Value + "\r\n");
                    fs.Write(bytes, 0, length);
                }
                if (p.application_byte != null)
                {
                    length = p.application_byte.Length;
                    fs.Write(p.application_byte, 0, length);
                }
            }
            fs.Close();
            MessageBox.Show("保存路径：" + filepath, "文件已保存");
            */
        }

	//// /////////////////////////////////////////////////////////////////////////////////////////////////////////
		///---------------------------------------包查询部分（需调用Search窗体）-------------------------------------------------------------
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void Button4Click(object sender, EventArgs e)
		{
			//查询之前必须先停止抓包
			if(this.button2.Enabled)
				stopcatch();
			bool isfind = false;
            foreach (Form fm in Application.OpenForms)
            {
                  //判断Form2是否存在，如果在激活并给予焦点
                if (fm.Name == "Search_form")
                {
                    fm.WindowState = FormWindowState.Maximized;
                    fm.WindowState = FormWindowState.Normal;
                    fm.Activate();
                    fm.Show();
                    this.Visible=false;
                    return;
                }
            }
            //如果窗口不存在，打开窗口
            if (!isfind) { Form fm = new Search_form(); fm.Show(); }
            this.Visible=false;
		}
		//// /////////////////////////////////////////////////////////////////////////////////////////////////////////
		///---------------------------------------分片重组部分-------------------------------------------------------------
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //检测是否为IP分片包，如果是则输出提示信息并等待用户确认
            int RowIndex = (int)(dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.ColumnCount - 1].Value);
            if ((RowIndex >= packets.Count)|(packets==null)) return;
            if (!((packet)packets[RowIndex]).df && RowIndex<packets.Count)
            {
                DialogResult dr = MessageBox.Show("检测到显示分片包信息，是否显示分片重组完整数据？否则显示单个包详细信息", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    string src = ((packet)packets[RowIndex]).srcIp;
                    string dst = ((packet)packets[RowIndex]).destIp;
                    string id = ((packet)packets[RowIndex]).ip_info["Identification(标识)"];
                    int index = RowIndex;
                    //int index = int.Parse(dataGridView1.Rows[RowIndex].Cells[5].Value.ToString());
                    List<byte> byteSource = new List<byte>();
                    /* 
                     * byte[] newData = new byte[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };  
                     * byteSource.AddRange(newData);  
                     * byteSource.Add(11);
                     * Insert(int index, T item); 
                     */
                    //向前查找所有IP分片包
                    while (index >= 0)
                    {
                        if (((packet)packets[index]).destIp == dst && ((packet)packets[index]).srcIp == src && ((packet)packets[index]).df == false && id == ((packet)packets[index]).ip_info["Identification(标识)"])
                            if (((packet)packets[index]).offset == 0)
                            {
                                byteSource.InsertRange(0, ((packet)packets[index]).application_byte);
                                break;
                            }
                            else
                                byteSource.InsertRange(0, ((packet)packets[index]).application_byte);
                        index--;
                    }
                    index = RowIndex;
                    //index = int.Parse(dataGridView1.Rows[RowIndex].Cells[5].Value.ToString());
                    //判断双击查看的包是否为分片的最后一个
                    if (((packet)packets[index]).mf == false)
                        showsliceddata(byteSource,e);
                    else
                    {
                        index = RowIndex;
                        //index = int.Parse(dataGridView1.Rows[RowIndex].Cells[5].Value.ToString()) + 1;
                        //向后查找所有IP分片包
                        while (index < packets.Count)
                        {
                            if (((packet)packets[index]).destIp == dst && ((packet)packets[index]).srcIp == src && ((packet)packets[index]).df == false && id == ((packet)packets[index]).ip_info["Identification(标识)"])
                                if (((packet)packets[index]).mf == false)
                                {
                                    byteSource.AddRange(((packet)packets[index]).application_byte);
                                    break;
                                }
                                else
                                    byteSource.AddRange(((packet)packets[index]).application_byte);
                            index++;
                        }
                        showsliceddata(byteSource,e);
                    }
                }
                else if (dr == DialogResult.Cancel)
                {
                    showpacketdetails(RowIndex);
                }
            }
            //检测是否为FTP-DATA文件传输（包括上传和下载）包，这个会导致文件的分片传输
            else if (((packet)packets[RowIndex]).protocol.IndexOf("FTP-DATA") >= 0)
            {
                DialogResult dr = MessageBox.Show("检测到文件传输包信息，是否导出传输的文件？否则显示单个包详细信息", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    string protoc = ((packet)packets[RowIndex]).protocol;
                    string srcIP= ((packet)packets[RowIndex]).srcIp;
                    string dstIP = ((packet)packets[RowIndex]).destIp;
                    int index = RowIndex - 1;
                    Dictionary<uint, byte[]> bytearray_index_recording = new Dictionary<uint, byte[]>();
                    index = 0;
                    List<byte> byteSource = new List<byte>();
                    //合并所有的文件传输比特流
                    while (index < dataGridView1.RowCount )
                    {
                        if (((packet)packets[index]).protocol == protoc && ((packet)packets[index]).srcIp == srcIP && ((packet)packets[index]).destIp == dstIP)
                        {
                            bytearray_index_recording.Add(uint.Parse(((packet)packets[index]).tcp_info["SequenceNumber(序号)"]), ((packet)packets[index]).application_byte);
                        }
                        index++;
                    }
                    ArrayList lst = new ArrayList(bytearray_index_recording.Keys);
                    lst.Sort();
                    foreach (uint i in lst)
                        byteSource.AddRange(bytearray_index_recording[i]);
                    //按全局变量saved_file_index的顺序依次保存合并后的文件内容
                    SaveFileDialog ofd = new SaveFileDialog();
                    ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                    ofd.Filter = "TXT文档(*.txt)|*.txt";
                    ofd.ValidateNames = true;
                    string filepath = Application.StartupPath+"\\a.txt";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        filepath = ofd.FileName;

                    }

                   FileStream fs = new FileStream(filepath, FileMode.Create);
                    byte[] rawdata = new byte[byteSource.Count];
                    for (int i = 0; i < rawdata.Length; i++)
                        rawdata[i] = byteSource[i];
                    fs.Write(rawdata, 0, rawdata.Length);
                    fs.Close();
                    MessageBox.Show("保存路径：" + filepath, "文件已保存");
                    saved_file_index++;
                }
                else if (dr == DialogResult.Cancel)
                {
                    showpacketdetails(RowIndex);
                }
            }
            else showpacketdetails(RowIndex);
        }
        //显示IP分片重组后的信息
        private void showsliceddata(List<byte> byteSource, DataGridViewCellEventArgs e)
        {
            int RowIndex = (int)(dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.ColumnCount - 1].Value);
            byte[] rawdata = new byte[byteSource.Count];
            for (int i = 0; i < rawdata.Length; i++)
                rawdata[i] = byteSource[i];
            string appdata = System.Text.Encoding.Default.GetString(rawdata);
            showpacketdetails(RowIndex);
            textBox1.Text += "\r\n分片重组后的信息如下：\r\n" + appdata;
        }
        //显示被双击的单个包的详细信息
        private void showpacketdetails(int RowIndex)
        {
            textBox1.Text = "";
            int n = RowIndex;
            //int n = int.Parse(dataGridView1.Rows[RowIndex].Cells[5].Value.ToString());
            if (((packet)packets[n]).frame_info.Count!=0) 
                textBox1.Text += "=====Frame info=====\r\n";
            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).frame_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).ethernet_info.Count!=0)
                textBox1.Text += "=====Ethernet info=====\r\n";

            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).ethernet_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).ip_info.Count!=0)
                textBox1.Text += "=====IP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).ip_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).arp_info.Count!=0)
                textBox1.Text += "=====ARP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).arp_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).icmp_info.Count != 0)
                textBox1.Text += "=====ICMP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).icmp_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).igmp_info.Count!=0)
                textBox1.Text += "=====IGMP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).igmp_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).tcp_info.Count!=0)
                textBox1.Text += "=====TCP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).tcp_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).udp_info.Count!=0)
                textBox1.Text += "=====UDP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).udp_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).application_info.Count!=0)
                textBox1.Text += "=====Application info=====\r\n";
            foreach (KeyValuePair<string, string> kv in ((packet)packets[n]).application_info)
            {
                textBox1.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (((packet)packets[n]).application_byte != null && ((packet)packets[n]).application_byte.Count()!=0)
                textBox1.Text += "=====Application bytes=====\r\n" + ((packet)packets[n]).application_byte.ToString();
        }

		//// /////////////////////////////////////////////////////////////////////////////////////////////////////////
		///---------------------------------------过滤部分-------------------------------------------------------------
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		//添加过滤规则
		private void filter_addClick(object sender, EventArgs e)
        {
            // 向列表中添加规则
            var key = this.filter_key.SelectedItem.ToString();
            var oper = this.filter_op.SelectedItem.ToString();
            var value = this.filter_val.Text.ToUpper();
            this.filter_val.Text = "";
			//向列表中添加规则
            int index = this.filter_rules.Rows.Add();
            this.filter_rules.Rows[index].Cells[0].Value = key;
            this.filter_rules.Rows[index].Cells[1].Value = oper;
            this.filter_rules.Rows[index].Cells[2].Value = value;
 
        }
		
				
		//Filter_val中有Enter按下时添加规则
		void Filter_valKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				var key = this.filter_key.SelectedItem.ToString();
            	var oper = this.filter_op.SelectedItem.ToString();
            	var value = this.filter_val.Text.ToUpper();
            
            	this.filter_val.Text = "";
            
            	int index = this.filter_rules.Rows.Add();
            	this.filter_rules.Rows[index].Cells[0].Value = key;
            	this.filter_rules.Rows[index].Cells[1].Value = oper;
            	this.filter_rules.Rows[index].Cells[2].Value = value;

			}
	
		}
		//删除选中的规则
		void Button6Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow r in filter_rules.Rows)
				{
					if(	r.Selected||r.Cells[0].Selected||r.Cells[1].Selected||r.Cells[2].Selected)
						filter_rules.Rows.Remove(r);
   						 
				}
	
		}
        //应用过滤规则
		void Button8Click(object sender, EventArgs e)
		{
			//applyfilter将告知抓包线程，需要过滤
			this.applyfilter=true;
			//取消过滤按钮使能
			this.filter_cancel.Enabled = true;
			this.filter_apply.Enabled = false;
			this.button6.Enabled=false;
			this.filter_key.Enabled=false;
			this.filter_op.Enabled=false;
			this.filter_val.Enabled=false;
			this.radioButton1.Enabled=false;
			this.radioButton2.Enabled=false;
			//显示已抓到的包
			show_est_pac_with_filter();
	
		}
		//检查是否满足过滤条件
        private bool _filter_check(packet Packet, string key, string oper, string value)
        {
            // 取出packet中对应key的value，string形式
            List<string> pac_value = new List<string>();
            switch (key)
            {
                case "IP地址":
            		//可能是源IP或目的IP
                    pac_value.Add(Packet.destIp);
                    pac_value.Add(Packet.srcIp);
                    break;
                case "源IP":                  
                    pac_value.Add(Packet.srcIp);
                    break;
                case "目的IP":
                    pac_value.Add(Packet.destIp);
                    
                    break;                    
                case "端口":
                    
                    if (Packet.tcp_info.Count > 0)
                    {
                    	//可能是源端口或目的端口
                        pac_value.Add(Packet.tcp_info["SourcePort(源端口)"]);
                        pac_value.Add(Packet.tcp_info["DestinationPort(目的端口)"]);
                    }
                    if (Packet.udp_info.Count > 0)
                    {
                    	//可能是源端口或目的端口
                        pac_value.Add(Packet.udp_info["SourcePort(源端口)"]);
                        pac_value.Add(Packet.udp_info["DestinationPort(目的端口)"]);
                    }
                    break;
                case "源端口":
                    
                    if (Packet.tcp_info.Count > 0)
                    {
                        pac_value.Add(Packet.tcp_info["SourcePort(源端口)"]);
                    }
                    if (Packet.udp_info.Count > 0)
                    {
                        pac_value.Add(Packet.udp_info["SourcePort(源端口)"]);
                    }
                    break;
                case "目的端口":
                    
                    if (Packet.tcp_info.Count > 0)
                    {
                        pac_value.Add(Packet.tcp_info["DestinationPort(目的端口)"]);
                    }
                    if (Packet.udp_info.Count > 0)
                    {
                        pac_value.Add(Packet.udp_info["DestinationPort(目的端口)"]);
                    }
                    break;                    
                case "IP版本":
                    if (Packet.ip_info.Count > 0)
                        pac_value.Add(Packet.ip_info["Version(版本)"]);
                    break;
                case "协议":
                    //从IP层往上依次判断
                    if (Packet.ip_info.Count > 0)
                        pac_value.Add("IP");
                    if (Packet.tcp_info.Count > 0)
                        pac_value.Add("TCP");
                    if (Packet.udp_info.Count > 0)
                        pac_value.Add("UDP");
                    if (Packet.icmp_info.Count > 0)
                        pac_value.Add("ICMP");
                    if (Packet.igmp_info.Count > 0)
                        pac_value.Add("IGMP");
                    if (Packet.arp_info.Count > 0)
                        pac_value.Add("ARP");
                    if (Packet.application_info.Count > 0)
                        pac_value.Add(Packet.application_info["ApplicationType"]);
                    break;

                case "应用数据":
                    if (Packet.application_info.Count > 0)
                        pac_value.Add(Packet.application_info["Data"]);
                    break;
                    //是否有合法校验和
                case "合法校验和":
                    if (Packet.color == "Red")
                        return false ^ (oper == "不等于");
                    else
                        return true ^ (oper =="不等于");

                default:
                	
                    break;
            }
			//过滤操作符，可以是等于，不等于或包含
            switch (oper)
            {
                case "等于":
                    if (include_array(pac_value, value))
                    {
                        return true;
                    }
                    break;
                case "不等于":
                    if (!include_array(pac_value, value))
                    {
                        return true;
                    }
                    break;
                case "包含":
                    if (include_array_like(pac_value, value))
                        return true;
                    break;
                default:
                    
                    return true;
            }
            return false;
        }
		//是否包含某字符串
        private bool include_array_like(List<string> arr, string find)
        {
            foreach (string i in arr)
            {
                if (i.IndexOf(find) >= 0)
                    return true;
            }
            return false;
        }

        private bool include_array(List<string> arr, string find)
        {
            foreach (string i in arr)
            {
                if (i == find)
                {
                    return true;
                }
            }
            return false;
        }
	
		
		//显示以前抓的包
		void show_est_pac_with_filter()
		{
			//先清空包列表
			this.dataGridView1.Rows.Clear();
			packet temp;
			int Count=packets.Count;
			//若不过滤，则直接显示
			if (!applyfilter)
				for(int i=0;i<Count;i++)
				{
				    temp=(packet)packets[i];
					show_pac(temp);
				}
			//依次检packets中的每个包	是否满足过滤规则，是则输出，否则忽略
			else
			{
				for(int j=0;j<Count;j++)
					if(apply_filter((packet)packets[j]))
				    	{
							temp=(packet)packets[j];
							show_pac(temp);
						}
			}
			
		}
		//对某个包应用过滤规则
        bool apply_filter(packet temp)
        {
            bool flag=false;
            DataGridViewRowCollection rules = this.filter_rules.Rows;
			//遍历所有规则
			if(!this.radioButton2.Checked)
			{
				flag=true;
				foreach (DataGridViewRow item in rules)
            	{
                	string key = (string)(item.Cells[0].Value);
                	string oper = (string)(item.Cells[1].Value);
                	string value = (string)(item.Cells[2].Value);
                	flag = flag && _filter_check(temp, key, oper, value);
            	}
			}
			else
			{
				flag=false;
				foreach (DataGridViewRow item in rules)
            	{
                	string key = (string)(item.Cells[0].Value);
                	string oper = (string)(item.Cells[1].Value);
                	string value = (string)(item.Cells[2].Value);
                	flag = flag || _filter_check(temp, key, oper, value);
            	}
			}
            return flag;
        }

		//取消过滤
		void Filter_cancelClick(object sender, EventArgs e)
		{
			//重置applyfilter
			applyfilter=false;
			//重置GUI
			this.filter_cancel.Enabled = false;
			this.filter_apply.Enabled = true;
			this.button6.Enabled=true;
			this.filter_key.Enabled=true;
			this.filter_op.Enabled=true;
			this.filter_val.Enabled=true;
			this.radioButton1.Enabled=true;
			this.radioButton2.Enabled=true;
			//显示所有包
			show_est_pac_with_filter();
	
		}
		//// /////////////////////////////////////////////////////////////////////////////////////////////////////////
		///---------------------------------------文件打开部分（需调用Search窗体）-------------------------------------------------------------
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////

		void Button7Click(object sender, EventArgs e)
		{
//			if (this.is_saved == true || MessageBox.Show("不保存并读取文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
//            {
                
                
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
                ofd.Filter = "PCAP(*.pcap)|*.pcap";
                ofd.ValidateNames = true;
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                	openfile(ofd.FileName);

                }
//            }
		}
		void openfile(string capFile)
		{
			                	try
                	{
                    	this.device.StopCapture();
                    	this.device.Close();
                	}
                	catch (Exception)
                	{
                    	;
                	}
                    this.packets = new ArrayList();
                    this.dataGridView1.Rows.Clear();
                    
                    SharpPcap.LibPcap.CaptureFileReaderDevice captureFileReader = new SharpPcap.LibPcap.CaptureFileReaderDevice(capFile);

                    SharpPcap.RawCapture pPacket;

                    int indx=0;
                    // Go through all packets in the file
                    while ((pPacket = captureFileReader.GetNextPacket()) != null)
                    {
                        try
                        {
                            packet temp = new packet(pPacket);
                            temp.index=indx;
                            indx++;
                            this.packets.Add(temp);

//                            if (filter_check(temp))
//                            {
                                if (this.dataGridView1.InvokeRequired)
                                {
                                    this.dataGridView1.BeginInvoke(new setDataGridViewDelegate(setDataGridView), new object[] { temp });
                                }
                                else
                                {
                                    int index = this.dataGridView1.Rows.Add();
                                    this.dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.FromName(temp.color);
                                    this.dataGridView1.Rows[index].Cells[0].Value = temp.time;
                                    this.dataGridView1.Rows[index].Cells[1].Value = temp.srcIp;
                                    this.dataGridView1.Rows[index].Cells[2].Value = temp.destIp;
                                    this.dataGridView1.Rows[index].Cells[3].Value = temp.protocol;
                                    this.dataGridView1.Rows[index].Cells[4].Value = temp.info;
                                    this.dataGridView1.Rows[index].Cells[5].Value = temp.index;

                                    this.dataGridView1.FirstDisplayedScrollingRowIndex = this.dataGridView1.Rows.Count - 1;
                                }
//                            }
                        }
                        catch (Exception)
                        {
                            ;
                        }                        
                    }
                    //this.is_saved = true;
                    captureFileReader.Close();
                    MessageBox.Show("读取完毕");
		}
		void MainFormDragDrop(object sender, DragEventArgs e)
		{
			 string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString(); 
			 openfile(path);
		}
		void MainFormDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))      //判断该文件是否可以转换到文件放置格式

            {

                e.Effect = DragDropEffects.Link;       //放置效果为链接放置

            }

            else

            {

                e.Effect = DragDropEffects.None;      //不接受该数据,无法放置，后续事件也无法触发

            }
		}

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "流量统计")
            {
                chart1.DataSource = traffic_statistics_list;
                chart1.DataBind();
                chart1.Series["traffic flow"].XValueMember = "Time";
                chart1.Series["traffic flow"].YValueMembers = "Count";
                chart1.Visible = true;
                button8.Text = "关闭";
            }
            else
            {
                chart1.Visible = false;
                button8.Text = "流量统计";
            }
        }




        //这两个括号不要误删    
    }

}


