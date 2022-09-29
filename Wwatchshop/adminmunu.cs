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
using MySql.Data.MySqlClient;

namespace Wwatchshop
{
    public partial class adminmunu : Form
    {
        public adminmunu()
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
        #region CurrentTime
        private void adminmunu_Load(object sender, EventArgs e)//Form Load To Run Timer
        {
            timer1.Start();
            showEquipment();
        }
        private void timer1_Tick(object sender, EventArgs e)//Timer Now And Config
        {
            DateTime datetime = DateTime.Now;
            this.label4.Text = datetime.ToString();
        }
        #endregion
        #region ShowEQ
        private void showEquipment()//Function Show Data in DataGridView From DataBase
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT `id`, `first name`, `last name`, `tel`, `date`, `time`, `service`, `type`, `status`,`wname`,`price`,`paystatus` FROM `userservice` WHERE `date`='" + dateTimePicker1.Text + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataEquiment.DataSource = ds.Tables[0].DefaultView;
        }
        private void button3_Click(object sender, EventArgs e)//Show Data BTN
        {
            showEquipment();
        }
        #endregion
        #region EditBtnandDeleteBtn
        private void button4_Click(object sender, EventArgs e)//Function Edit Status
        {
            int selectedRow = dataEquiment.CurrentCell.RowIndex;
            int editId = Convert.ToInt32(dataEquiment.Rows[selectedRow].Cells["id"].Value);
            MySqlConnection conn = databaseConnection();
            String sql = "UPDATE userservice SET status ='"+ comboBox1.Text + "' WHERE id = '"+editId+"'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if(rows > 0)
            {
                MessageBox.Show("success");
                showEquipment();
            }

        }
        private void dataEquiment_CellClick(object sender, DataGridViewCellEventArgs e)//Function Config DataGridView
        {
            dataEquiment.CurrentRow.Selected = true;
            comboBox1.Text = dataEquiment.Rows[e.RowIndex].Cells["status"].FormattedValue.ToString();
            comboBox2.Text = dataEquiment.Rows[e.RowIndex].Cells["paystatus"].FormattedValue.ToString();
            textBox1.Text = dataEquiment.Rows[e.RowIndex].Cells["wname"].FormattedValue.ToString();
            textBox2.Text = dataEquiment.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString();
        }
        private void button5_Click(object sender, EventArgs e)//Function Delete Data In Database
        {
            int selectedRow = dataEquiment.CurrentCell.RowIndex;
            int deletedId = Convert.ToInt32(dataEquiment.Rows[selectedRow].Cells["id"].Value);

            MySqlConnection conn = databaseConnection();
            String sql = "DELETE FROM userservice WHERE id = '" + deletedId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            
            string message = "ต้องการยกเลิกไหม?";
            string title = "เตือน";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                if (rows > 0)
                {
                    MessageBox.Show("ยกเลิกสำเร็จ");
                    showEquipment();
                }
            }
            else
            {
                return;
            }
        }
        private void button1_Click(object sender, EventArgs e)//Function Edit Watch Name
        {
            int selectedRow = dataEquiment.CurrentCell.RowIndex;
            int editId = Convert.ToInt32(dataEquiment.Rows[selectedRow].Cells["id"].Value);
            MySqlConnection conn = databaseConnection();
            String sql = "UPDATE userservice SET wname ='" + textBox1.Text + "' WHERE id = '" + editId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("สำเร็จ");
                showEquipment();
            }
        }
        private void button2_Click(object sender, EventArgs e)//Function Edit Pay Status
        {
            int selectedRow = dataEquiment.CurrentCell.RowIndex;
            int editId = Convert.ToInt32(dataEquiment.Rows[selectedRow].Cells["id"].Value);
            MySqlConnection conn = databaseConnection();
            String sql = "UPDATE userservice SET paystatus ='" + comboBox2.Text + "' WHERE id = '" + editId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            string message = "ต้องการเปลี่ยนสถานะจ่ายเงินใช่ไหม?";
            string title = "เตือน";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                if (rows > 0)
                {
                    MessageBox.Show("สำเร็จ");
                    showEquipment();
                }
            }
            else
            {
                return;
            }
        }
        private void button6_Click(object sender, EventArgs e)//Function Edit Price
        {
            int selectedRow = dataEquiment.CurrentCell.RowIndex;
            int editId = Convert.ToInt32(dataEquiment.Rows[selectedRow].Cells["id"].Value);
            MySqlConnection conn = databaseConnection();
            String sql = "UPDATE userservice SET price ='" + textBox2.Text + "' WHERE id = '" + editId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            if (rows > 0)
            {
                MessageBox.Show("สำเร็จ");
                showEquipment();
            }
        }
        private void button7_Click(object sender, EventArgs e)//Function Config DataGridView BTN
        {
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT `id`, `first name`, `last name`, `tel`, `date`, `time`, `service`, `type`, `status`,`wname`,`price`,`paystatus` FROM `userservice`";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataEquiment.DataSource = ds.Tables[0].DefaultView;
        }
        #endregion
        #region ChangePage
        private void pictureBox4_Click_1(object sender, EventArgs e)//Change Page To Form1
        {
            this.Hide();
            Form1 st = new Form1();
            st.ShowDialog();
        }
        #endregion
        #region Keypress
        private void textBox2_KeyPrss(object sender, KeyPressEventArgs e)//Input Only Num for Price
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
