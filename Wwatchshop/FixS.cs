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
    public partial class FixS : Form
    {
        public FixS()
        {
            InitializeComponent();
        }
        #region Database
        MySqlConnection connect = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=watchshop");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
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
        #region DataGrid
        private void FixS_Load(object sender, EventArgs e)
        {
            showEquipment();
        }
        private void showEquipment()//Function Show Data in DataGridView From DataBase
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
        private void dataEquiment_CellClick(object sender, DataGridViewCellEventArgs e)//Function Show Data From DataGridView To TextBox
        {
            dataEquiment.CurrentRow.Selected = true;
            textBox1.Text = dataEquiment.Rows[e.RowIndex].Cells["first name"].FormattedValue.ToString();
            textBox2.Text = dataEquiment.Rows[e.RowIndex].Cells["last name"].FormattedValue.ToString();
            textBox3.Text = dataEquiment.Rows[e.RowIndex].Cells["date"].FormattedValue.ToString();
            textBox4.Text = dataEquiment.Rows[e.RowIndex].Cells["service"].FormattedValue.ToString();
            textBox5.Text = dataEquiment.Rows[e.RowIndex].Cells["type"].FormattedValue.ToString();
            textBox6.Text = dataEquiment.Rows[e.RowIndex].Cells["wname"].FormattedValue.ToString();
        }
        private void button1_Click(object sender, EventArgs e)//Function Config Receipt Show BTN
        {
            txtResult.Clear();
            txtResult.Text += "****************************************************************************\n";
            txtResult.Text += "***                                              ใบซ่อมนาฬิกา                                                  ***\n";
            txtResult.Text += "****************************************************************************\n";
            txtResult.Text += "วันนี้ :" + DateTime.Now + "\n\n";

            txtResult.Text += "ชื่อ :" + textBox1.Text + "\n\n";
            txtResult.Text += "นามสกุล :" + textBox2.Text + "\n\n";
            txtResult.Text += "วันที่ส่งซ่อม :" + textBox3.Text + "\n\n";
            txtResult.Text += "ชนิดบริการ :" + textBox4.Text + "\n\n";
            txtResult.Text += "ชนิดนาฬิกา :" + textBox5.Text + "\n\n";
            txtResult.Text += "ชื่อนาฬิกา :" + textBox6.Text + "\n\n";
            txtResult.Text += "เบอร์โทรติดต่อสอบถามทางร้าน : 0870600878 \n\n";
            txtResult.Text += "ลงชื่อ                          ผู้แจ้งซ่อม\n\n\n";
            txtResult.Text += "ลงชื่อ                          ผู้รับซ่อม\n\n";
            txtResult.Text += "**หมายเหตุ**\nเอกสารนี้ไม่ใช่ใบเสร็จ\n(กรุณารอรับใบเสร็จเมื่อรับสินค้าและชำระค่าสินค้าและบริการจากทางร้านแล้ว)\n**กรุณามารับตามเวลาที่ทางร้านเปิดบริการโดยยื่นใบซ่อมกับพนักงาน**\n\n\n";
            txtResult.Text += "****************************************************************************\n";
        }
        private void button2_Click(object sender, EventArgs e)//Function Config Receipt Print BTN
        {
            string message = "ต้องการจะปริ้นใบซ่อมใช่หรือไม่?";
            string title = "เตือน";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                return;
            }

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)//Function Config Receipt Print
        {
            e.Graphics.DrawString(txtResult.Text, new Font("Microsoft Sans Serif", 18, FontStyle.Bold), Brushes.Black, new Point(10, 10));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "";
            textBox2.Text += "";
            textBox3.Text += "";
            textBox4.Text += "";
            textBox5.Text += "";
            textBox6.Text += "";
        }
        #endregion
        #region ChangPage
        private void pictureBox4_Click(object sender, EventArgs e)//Change Page To Menu
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
