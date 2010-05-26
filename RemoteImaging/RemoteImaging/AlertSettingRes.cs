using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging
{
    public partial class AlertSettingRes : Form
    {
        public AlertSettingRes(string reMsg, int index)
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            msg = reMsg;
            picIndex = index;
        }

        private string msg = "";
        private int picIndex = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void AlertSettingRes_Load(object sender, EventArgs e)
        {
            Screen[] screens = Screen.AllScreens;

            Screen screen = screens[0];//获取屏幕变量

            this.Location = new Point(screen.WorkingArea.Width - widthMax - 1, screen.WorkingArea.Height - 127);//WorkingArea为Windows桌面的工作区

            this.timer2.Interval = StayTime;

            pictureBox1.Image = imageList1.Images[picIndex];
            label1.Text = msg;
        }

        private int heightMax, widthMax;
        public int HeightMax
        {
            set
            {
                heightMax = value;
            }
            get
            {
                return heightMax;
            }

        }

        public int WidthMax
        {
            set
            {
                widthMax = value;
            }
            get
            {
                return widthMax;
            }
        }

     
        public int StayTime = 5000;

        private void ScrollUp()
        {
            if (Height < heightMax)
            {
                this.Height += 3;
                this.Location = new Point(this.Location.X, this.Location.Y - 3);
            }
            else
            {
                this.timer1.Enabled = false;
                this.timer2.Enabled = true;
            }

        }

        private void ScrollDown()
        {

            if (Height > 3)
            {
                this.Height -= 3;
                this.Location = new Point(this.Location.X, this.Location.Y + 3);
            }
            else
            {
                this.timer3.Enabled = false;
                this.Close();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            ScrollUp();
            this.Close();
            this.Dispose();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            timer3.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ScrollUp();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
