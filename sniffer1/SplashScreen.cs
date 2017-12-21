using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace sniffer1
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }
        

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            for (int i = 100; i >= 0; --i)
            { // 实现渐变效果
                this.Opacity = i / 100.0;
                System.Threading.Thread.Sleep(5);
            }

        }
    }
}
