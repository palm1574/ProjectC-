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
    public partial class Form1 : Form
    {
        public Form1()
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
        #endregion
        #region lOGIN
        public void dataAdapterLogin(String query)//Function Login
        {
            openConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter = new MySqlDataAdapter(query, connect);
            table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("สำเร็จ");
                this.Hide();
                Menu menu = new Menu();
                menu.ShowDialog();

            }
            else
            {
                MessageBox.Show("ลองอีกครั้ง");
            }
        }  
        private void loginBtn_Click(object sender, EventArgs e)//Login BTN
        {
            string login = "SELECT * FROM userinfo WHERE Username = '" + textBoxUsername.Text + "' AND Password ='" + textBoxPassword.Text + "'";
            if (textBoxUsername.Text == "adddd" && textBoxPassword.Text == "minnn")
            {
                this.Hide();
                adminmunu ad = new adminmunu();
                ad.ShowDialog();
            }
            else
            {
                dataAdapterLogin(login);
            }
        }
        #endregion
        #region RegisterBtn
        private void regisBtn_Click(object sender, EventArgs e)//Register BTN
        {
            this.Hide();
            Register reg = new Register();
            reg.ShowDialog();
        }
        #endregion
        #region HideShowPass
        private void button1_Click(object sender, EventArgs e)//BTN Show Pass
        {
           
            if (textBoxPassword.PasswordChar == '*')
            {
                button2.BringToFront();
                textBoxPassword.PasswordChar = '\0';
            }

        }
        private void button2_Click(object sender, EventArgs e)//BTN Hide Pass
        {
            if (textBoxPassword.PasswordChar == '\0')
            {
                button1.BringToFront();
                textBoxPassword.PasswordChar = '*';
            }
        }
        #endregion
        #region KeyPress
        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)//Username Only Eng
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)//Password Only Eng
        {
            if (System.Text.Encoding.UTF8.GetByteCount(new char[] { e.KeyChar }) > 1)
            {
                e.Handled = true;
            }
        }
        #endregion
        #region Exit
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
