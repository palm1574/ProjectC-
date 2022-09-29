using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Wwatchshop
{
    public partial class Service : Form
    {
        public Service()
        {
            InitializeComponent();
        }
        
        #region Database
        MySqlConnection connect = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=watchshop");
        public void openConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }
        public void closeConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=watchshop";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        #endregion
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
        #region Datauser
        private void button1_Click(object sender, EventArgs e)//Check Input User 
        {
            
            if (textBox1.TextLength != 0 && textBox2.TextLength != 0 && textBox3.TextLength != 0 )
            {
                if(textBox3.Text.Length == 10)
                {
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO `userservice`(`first name`, `last name`, `tel`,`email`, `line`, `facebook`, `date`,`time`, `service`, `type`,`status`,`paystatus`) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + dateTimePicker1.Text + "','" + dateTimePicker2.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','กำลังเตรียมดำเนินงาน','ยังไม่ชำระเงิน')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open(); ;
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("บันทึกสำเร็จ");
                        this.Hide();
                        Menu menu = new Menu();
                        menu.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("โปรดกรอกข้อมูลให้ครบถ้วน");
                    }
                }
                else
                {
                    MessageBox.Show("เบอร์โทรศัพท์ต้องมี 10 ตัวอักษร");
                }
                
            }
            else
            {
                MessageBox.Show("โปรดกรอกข้อมูลให้ครบถ้วน");
            }
            
        }
        #endregion
        #region ChangPage
        private void pictureBox3_Click(object sender, EventArgs e)//Change Page To Strap Page
        {
            this.Hide();
            strap st = new strap();
            st.ShowDialog();
        }
        private void pictureBox4_Click(object sender, EventArgs e)//Change Page To Menu Page
        {
            this.Hide();
            Menu mu = new Menu();
            mu.ShowDialog();
        }
        #endregion
        #region KeyPress
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)//Only Num : Tel
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
