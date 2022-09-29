using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Wwatchshop
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        #region Sidebar
        bool sidebarExpand;
        private void menuBtn_Click(object sender, EventArgs e)//Menu BTN SideBar Trigger
        {
            sidebarTimer.Start();
        }
        private void sidebarTimer_Tick(object sender, EventArgs e)//SideBar Config
        {
            if (sidebarExpand)
            {
                sidebar.Width -= 10;
                if (sidebar.Width == sidebar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebarTimer.Stop();
                }
            }
            else
            {
                sidebar.Width += 10;
                if (sidebar.Width == sidebar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebarTimer.Stop();
                }
            }
        }
        #endregion 
        #region CurrentTime
        private void Menu_Load(object sender, EventArgs e)//Form Load To Run Timer
        {
            timer1.Start();
            
        }
        private void timer1_Tick(object sender, EventArgs e)//Timer Now And Config
        {
            DateTime datetime =DateTime.Now;
            this.label4.Text = datetime.ToString();
        }
        #endregion
        #region ChangPage
        private void pictureBox4_Click(object sender, EventArgs e)//Change Page To Service Page
        {
            this.Hide();
            Service ser = new Service();
            ser.ShowDialog();
        }
        private void pictureBox6_Click(object sender, EventArgs e)//Change Page To Strap Page
        {
            this.Hide();
            strap st = new strap();
            st.ShowDialog();
        }
        private void pictureBox7_Click(object sender, EventArgs e)//Change Page To Form1 Page
        {
            this.Hide();
            Form1 st = new Form1();
            st.ShowDialog(); 
        }
        private void pictureBox8_Click(object sender, EventArgs e)//Change Page To Printre Page
        {
            this.Hide();
            printre re = new printre();
            re.ShowDialog();
        }
        private void fixBtn_Click(object sender, EventArgs e)//Change Page To FixS Page
        {
            this.Hide();
            FixS Fs = new FixS();
            Fs.ShowDialog();
        }
        #endregion
        #region Exit
        private void pictureBox1_Click(object sender, EventArgs e)//Exit Program BTN
        {
            string message = "ต้องการจะออกจากโปรแกรมใช่หรือไม่?";
            string title = "เตือน";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }
        #endregion

    }
}
