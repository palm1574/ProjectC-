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
    public partial class strap : Form
    {
        public strap()
        {
            InitializeComponent();
        }
        
        #region Database
        MySqlConnection connect = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=watchshop");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
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
        #region SideBar
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
            searchData("");
        }
        private void timer1_Tick(object sender, EventArgs e)//Timer Now And Config
        {
            DateTime datetime = DateTime.Now;
            this.label6.Text = datetime.ToString();
        }
        #endregion
        #region Datagrid
        private void searchData(string valueToSearch)//Function Search Data From DataBase
        {
            string query = "SELECT * FROM `userservice` WHERE CONCAT(`id`, `first name`, `last name`, `tel`, `email`, `line`, `facebook`, `date`, `time`, `service`, `type`, `status`, `wname`, `paystatus`) like '%" + valueToSearch+"%'";
            command = new MySqlCommand(query, connect);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataEquiment.DataSource = table;
        }
        private void button1_Click(object sender, EventArgs e)//Search BTN
        {
            string valueToSearch = textBoxValueToSearch.Text.ToString();
            searchData(valueToSearch);
        }
        private void button2_Click(object sender, EventArgs e)//Delete Data BTN From DataBase
        {
            int selectedRow = dataEquiment.CurrentCell.RowIndex;
            int deletedId = Convert.ToInt32(dataEquiment.Rows[selectedRow].Cells["id"].Value);

            MySqlConnection conn = databaseConnection();
            String sql = "DELETE FROM userservice WHERE id = '" + deletedId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd.ExecuteNonQuery();
            conn.Close();
            string message = "ต้องการยกเลิกใช่ไหม";
            string title = "เตือน";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                if (rows > 0)
                {
                    MessageBox.Show("ยกเลิกสำเร็จ");
                    searchData("");
                }
            }
            else
            {
                return;
            }
        }
        #endregion 
        #region ChangePage
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
