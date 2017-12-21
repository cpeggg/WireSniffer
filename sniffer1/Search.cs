/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/11/26
 * Time: 0:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
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

namespace sniffer1
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class Search_form : Form
	{
		public Search_form()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
		
		}
		private delegate void setDataGridViewDelegate(packet Packet);
		void Button1Click(object sender, EventArgs e)
		{
			dataGridView1.Rows.Clear();
            var tmp=textBox1.Text.ToString();
			var target=comboBox1.SelectedItem.ToString();
			var count=MainForm.pointer.packets.Count;
			//选择查询目标
//			switch(target)
//		{
//				case "协议类型":
//					//依次检查主窗体包列表的每一行，找出符合条件的包
//					for(int i=0;i<count-1;i++)
//						if(MainForm.pointer.dataGridView1.Rows[i].Cells[0].Value.ToString().Contains(tmp))
//					{
//						int index=dataGridView1.Rows.Add();
//						for(int j=0;j<6;j++)
//							dataGridView1.Rows[index].Cells[j].Value=MainForm.pointer.dataGridView1.Rows[i].Cells[j].Value;
//					}
//					break;
//				case"源IP":
//					for(int i=0;i<count-1;i++)
//						if(MainForm.pointer.dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(tmp))
//					{
//						int index=dataGridView1.Rows.Add();
//						for(int j=0;j<6;j++)
//							dataGridView1.Rows[index].Cells[j].Value=MainForm.pointer.dataGridView1.Rows[i].Cells[j].Value;
//					}
//					break;
//				case"目的IP":
//					for(int i=0;i<count-1;i++)
//						if(MainForm.pointer.dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(tmp))
//					{
//						int index=dataGridView1.Rows.Add();
//						for(int j=0;j<6;j++)
//							dataGridView1.Rows[index].Cells[j].Value=MainForm.pointer.dataGridView1.Rows[i].Cells[j].Value;
//					}
//					break;
//					
//				case"时间":
//					for(int i=0;i<count-1;i++)
//						if(MainForm.pointer.dataGridView1.Rows[i].Cells[3].Value.ToString().Contains(tmp))
//					{
//						int index=dataGridView1.Rows.Add();
//						for(int j=0;j<6;j++)
//							dataGridView1.Rows[index].Cells[j].Value=MainForm.pointer.dataGridView1.Rows[i].Cells[j].Value;
//					}
//					break;
//				case"信息段":
//					for(int i=0;i<count;i++)
//						if(MainForm.pointer.dataGridView1.Rows[i].Cells[4].Value.ToString().Contains(tmp))
//					{
//						int index=dataGridView1.Rows.Add();
//						for(int j=0;j<6;j++)
//							dataGridView1.Rows[index].Cells[j].Value=MainForm.pointer.dataGridView1.Rows[i].Cells[j].Value;
//					}
//					break;
//				case"索引":
//					for(int i=0;i<count;i++)
//						if(MainForm.pointer.dataGridView1.Rows[i].Cells[5].Value.ToString().Equals(tmp))
//					{
//						int index=dataGridView1.Rows.Add();
//						for(int j=0;j<6;j++)
//							dataGridView1.Rows[index].Cells[j].Value=MainForm.pointer.dataGridView1.Rows[i].Cells[j].Value;
//						break;
//					}
//					break;
//				default:
//					MessageBox.Show("Something wrong");break;
//					
//		}
		
			
			packet pac;
		switch(target)
		{

				case "数据":
					//依次检查主窗体包列表的每一行，找出符合条件的包
					for(int i=0;i<count-1;i++)
					{
						pac=(packet)(MainForm.pointer.packets[i]);
						if(pac.application_byte!=null && pac.application_byte.Count()!=0 && pac.application_byte.ToString().Contains(tmp))
						{
							
							show_pac(pac);
						}
						else  if (pac.application_info.Count!=0 )
						{
							foreach (KeyValuePair<string, string> kv in pac.application_info)
            				{
								if(kv.Value.ToString().Contains(tmp))
								{
									
									show_pac(pac);
									break;
								}
            				}
						}
							
					}
					break;
				case "索引":
					//依次检查主窗体包列表的每一行，找出符合条件的包
					for(int i=0;i<count-1;i++)
					{
						pac=(packet)(MainForm.pointer.packets[i]);
						
						if(pac.index==int.Parse(tmp))
						{
							
							show_pac(pac);
						}
					}
					break;
				default:
					MessageBox.Show("Something wrong");break;
					
		}
		
		if(dataGridView1.Rows.Count!=0)
		{
			int pacindex=(int)(dataGridView1.Rows[0].Cells[dataGridView1.ColumnCount - 1].Value);
			showpac_detail(pacindex);
		}
		
		}
		
		
		void showpac_detail(int pacindex)
		{
			textBox2.Text = "";
            int n = pacindex;
            //int n = int.Parse(dataGridView1.Rows[RowIndex].Cells[5].Value.ToString());
            packet pac=(packet)MainForm.pointer.packets[n];
            if (pac.frame_info.Count!=0) 
                textBox2.Text += "=====Frame info=====\r\n";
            foreach (KeyValuePair<string, string> kv in pac.frame_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.ethernet_info.Count!=0)
                textBox2.Text += "=====Ethernet info=====\r\n";

            foreach (KeyValuePair<string, string> kv in pac.ethernet_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.ip_info.Count!=0)
                textBox2.Text += "=====IP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in pac.ip_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.arp_info.Count!=0)
                textBox2.Text += "=====ARP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in pac.arp_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.icmp_info.Count != 0)
                textBox2.Text += "=====ICMP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in pac.icmp_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.igmp_info.Count!=0)
                textBox2.Text += "=====IGMP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in pac.igmp_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.tcp_info.Count!=0)
                textBox2.Text += "=====TCP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in pac.tcp_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.udp_info.Count!=0)
                textBox2.Text += "=====UDP info=====\r\n";
            foreach (KeyValuePair<string, string> kv in pac.udp_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.application_info.Count!=0)
                textBox2.Text += "=====Application info=====\r\n";
            foreach (KeyValuePair<string, string> kv in pac.application_info)
            {
                textBox2.Text += kv.Key + " : " + kv.Value + "\r\n";
            }
            if (pac.application_byte != null && pac.application_byte.Count()!=0)
                textBox2.Text += "=====Application bytes=====\r\n" + pac.application_byte.ToString();
        
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
        
        


		
		//退出查询，回到主窗体
		void Button2Click(object sender, EventArgs e)
		{
			this.Visible=false;
			MainForm.pointer.Visible=true;
		}
		
		void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
		{

	
		}
		void DataGridView1CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			int pacindex = (int)(dataGridView1.Rows[e.RowIndex].Cells[dataGridView1.ColumnCount - 1].Value);
			showpac_detail(pacindex);
		}
	}
}
