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
    public partial class Register : Form
    {
        public Register()
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
        #region Register
        private void button1_Click_1(object sender, EventArgs e)//Register Confirm BTN
        {
            if (textBox1.Text.Length >= 5 && textBox1.Text.Length <= 20 || textBox2.Text.Length >=5  && textBox2.Text.Length <= 20 )
            {

                if (textBox3.Text == textBox2.Text)
                {
                    MySqlConnection conn = databaseConnection();
                    string sql = "INSERT INTO `userinfo`(`username`, `password`) VALUES ('" + textBox1.Text + "','" + textBox3.Text + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open(); 
                    int rows = cmd.ExecuteNonQuery();

                    conn.Close();
                    if (rows > 0)
                    {
                        MessageBox.Show("สำเร็จ");
                        this.Hide();
                        Form1 mu = new Form1();
                        mu.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("รหัสผ่านต้องเหมือนกัน");
                }
            }
            else
            {
                MessageBox.Show("ชื่อผู้ใช้งานหรือรหัสผ่านต้องมีตัวอักษรอย่างน้อย 4 ตัวอักษรและไม่เกิน 21 ตัวอักษร");
            }
        }
        #endregion
        #region KeyPress
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//Username Only Eng And Num
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)//Password Only Eng And Num
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)//Confirm Password Only Eng And Num
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
        #endregion
        #region ChangePage
        private void button2_Click(object sender, EventArgs e)//Back To Previous Page : Form1
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }
        #endregion
        #region ExitBtn
        private void button3_Click(object sender, EventArgs e)//Exit Program BTN
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
