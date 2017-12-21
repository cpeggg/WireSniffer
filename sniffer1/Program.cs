/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2017/11/25
 * Time: 15:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace sniffer1
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            //SplashScreen.ShowSplashScreen();

            // 进行自己的操作：加载组件，加载文件等等  
            /*
            // 关闭  
            if (SplashScreen.Instance != null)
            {
                SplashScreen.Instance.BeginInvoke(new MethodInvoker(SplashScreen.Instance.Close));
                SplashScreen.Instance = null;
            }
            */
            //SplashScreen ss = new SplashScreen();
            //ss.Show();
            //ss.Close();
            Application.Run(new MainForm());
			
		}
		
	}
}
