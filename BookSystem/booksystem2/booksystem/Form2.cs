using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace booksystem
{
    public partial class Form2 : Form
    {
        new Form2 Parent;
        public string SelectedRowIndex;
        string booksql;
        DBClass dbc = new DBClass();
        private OracleConnection odpConn = new OracleConnection();



        public Form2()
        {
            InitializeComponent();
            dbc.DB_ObjCreate(); 
            dbc.DB_Open();
            dbc.DB_Access();
        }
        public void member_counter()
        {
            int i;
            i = dataGridView1.RowCount;
            label1.Text = "총 " + i + " 권";
        }
        public void sql_execute(String sqlstr, DataSet dsstr)    //사용자 함수 정의
        {
           
            dbc.DCom.CommandText = sqlstr;
            dbc.DA.SelectCommand = dbc.DCom;
            dbc.DA.Fill(dsstr, "book");              
            
           
        
        }
        public void showDataGridView() //사용자 정의 함수
        {
            try
            {
                odpConn.ConnectionString = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION =   (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))   (CONNECT_DATA =     (SERVER = DEDICATED)     (SERVICE_NAME = xe)   ) );";
                odpConn.Open();
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = new OracleCommand("SELECT * from book", odpConn);
                DataTable dt = new DataTable();
                oda.Fill(dt);
                odpConn.Close();
                dataGridView1.DataSource = dt;
                dataGridView1.AutoResizeColumns();
                dataGridView1.AutoSizeColumnsMode =DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.SelectionMode =DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 발생 : " + ex.ToString());
                odpConn.Close();
            }
        }
 



        public void list_search(String Find)
        {
            if(Find =="")
            {
                MessageBox.Show("검색 미입력");
            }
            else
            {

                booksql = "Select * From book Where B_title Like'%" + Find + "%'";
            }
            sql_execute(booksql, dbc.DS);
            if (dbc.DS.Tables["book"].Rows.Count == 0)
            {
                
                MessageBox.Show("해당 도서가 없습니다");
                booksql = "Select * From book ORDER BY b_title ASC";
                sql_execute(booksql, dbc.DS);
            }
        }




        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                dbc.DS.Clear();
                dbc.DA.Fill(dbc.DS, "book");
                dataGridView1.DataSource = dbc.DS.Tables["book"].DefaultView;
                member_counter();
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
            catch (Exception DE)
            {
                MessageBox.Show(DE.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text =="")
            {
                MessageBox.Show("검색어를 입력하세요!");
            }
            else
            {
                Form5 frm5 = new Form5();
                frm5.Owner = this;
                frm5.ShowDialog();
                frm5.Dispose();
            }
         
        }

   
        public String TxtS
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value.ToString(); }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                textBox2.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                SelectedRowIndex = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            }
            catch (DataException DE)
            { MessageBox.Show(DE.Message); }
            catch (Exception DE)
            { MessageBox.Show(DE.Message); }

            }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip m = new ContextMenuStrip();

                m.Items.Add("수정");
                m.Items.Add("삭제");


                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;



                m.Show(dataGridView1, new Point(e.X, e.Y));

                m.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemCliked);


            }
        }
        void my_menu_ItemCliked(object sender, ToolStripItemClickedEventArgs e)
        {

            switch (e.ClickedItem.Text)
            {
                case "수정":
                    Form8 form = new Form8(this);
                    form.Show();
                    break;
                case "삭제":
                    try
                    {
                        string ConStr = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION =   (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))   (CONNECT_DATA =     (SERVER = DEDICATED)     (SERVICE_NAME = xe)   ) );";
                        OracleConnection conn = new OracleConnection(ConStr);
                        conn.Open();
                        OracleDataAdapter DBAdapter = new OracleDataAdapter();

                        DBAdapter.SelectCommand = new OracleCommand(string.Format("DELETE FROM book WHERE b_code = {0}", SelectedRowIndex), conn);
                        DataSet DS = new DataSet();
                        DBAdapter.Fill(DS, "book");
                        DataTable phoneTable = DS.Tables["book"];
                        dataGridView1.DataSource = phoneTable;
                    }
                    catch (DataException DE)
                    { MessageBox.Show(DE.Message); }
                    catch (Exception DE)
                    { MessageBox.Show(DE.Message); }

                    showDataGridView();
                    break;

            }


        }

    }
}
