using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace booksystem
{
    public partial class Form6 : Form
    {
        string sqlstr, sqlstr2;
        DBClass dbc = new DBClass();
        DBClass dbk = new DBClass();
        OracleDataAdapter DBAdapter2;
        OracleCommandBuilder myCommandBuilder2;
        DataSet DS2;
        DataTable phoneTable2;
        private void DK_Open()
        {
            try
            {
                string connectionString = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION =   (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))   (CONNECT_DATA =     (SERVER = DEDICATED)     (SERVICE_NAME = xe)   ) );";

                string commandString = "select * from buyer";

                DBAdapter2 = new OracleDataAdapter(commandString, connectionString);
                myCommandBuilder2 = new OracleCommandBuilder(DBAdapter2);

                DS2 = new DataSet();
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }

        public Form6()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
            dbc.DB_Open();
            dbc.DB_Access();
            dbk.DB_ObjCreate();
            dbk.DB_Open();
            dbk.DB_Access();
            DK_Open();  
        }
       
        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            

        }
        public void member_header()
        {
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "이름";
            dataGridView1.Columns[2].Visible = false; ;
            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 90;
            
        }
        public void member_header2()
        {
            dataGridView2.Columns[0].HeaderText = "책코드";
            dataGridView2.Columns[1].HeaderText = "책제목";
            dataGridView2.Columns[2].Visible = false; ;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            
            

            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[1].Width = 60;
            dataGridView2.Columns[2].Width = 90;
            dataGridView2.Columns[3].Width = 90;
            dataGridView2.Columns[4].Width = 55;
            dataGridView2.Columns[5].Width = 80;
            dataGridView2.Columns[6].Width = 100;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable phoneTable = dbc.DS.Tables["member"];
            if (e.RowIndex < 0)
            {
                return;
            }
            else if (e.RowIndex > phoneTable.Rows.Count - 1)
            {
                MessageBox.Show("해당하는 데이터가 존재하지 않습니다.");
                return;
            }
            DataRow currRow = phoneTable.Rows[e.RowIndex];
            textBox1.Text = currRow["m_id"].ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable phoneTable = dbk.DS.Tables["book"];
            if (e.RowIndex < 0)
            {
                return;
            }
            else if (e.RowIndex > phoneTable.Rows.Count - 1)
            {
                MessageBox.Show("해당하는 데이터가 존재하지 않습니다.");
                return;
            }
            DataRow currRow = phoneTable.Rows[e.RowIndex];
            textBox2.Text = currRow["b_title"].ToString();
            textBox4.Text = currRow["b_code"].ToString();
            
            
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("텍스트 상자에 모든 데이터 입력하셨으면 추가합니다!");

                DS2.Clear();

                DBAdapter2.Fill(DS2, "buyer");


                phoneTable2 = DS2.Tables["buyer"];
                DataRow newRow = phoneTable2.NewRow();
                newRow["buy_code"] =textBox1.Text;
                newRow["buy_name"] = textBox2.Text;
                newRow["buy_date"] = textBox3.Text;
                newRow["buy_bcode"] = textBox4.Text;

                phoneTable2.Rows.Add(newRow);
                DBAdapter2.Update(DS2, "buyer");
                DS2.AcceptChanges();

               

                ClearTextBoxes();  
               
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
       
     

 
        private void Form6_Load(object sender, EventArgs e)
        {
            try
            {
            
                sqlstr = "Select * From member ORDER BY m_id ASC";
                dbc.DCom.CommandText = sqlstr;
                dbc.DA.SelectCommand = dbc.DCom;
                dbc.DA.Fill(dbc.DS, "member");
                dbc.DS.Tables["member"].Clear();
                dbc.DA.Fill(dbc.DS, "member");
                dataGridView1.DataSource = dbc.DS.Tables["member"].DefaultView;
           
                member_header();

               

                sqlstr2 = "Select * From book ORDER BY b_code ASC";
                dbk.DCom.CommandText = sqlstr2;
                dbk.DA.SelectCommand = dbk.DCom;
                dbk.DA.Fill(dbk.DS, "book");
                dbk.DS.Tables["book"].Clear();
                dbk.DA.Fill(dbk.DS, "book");
                dataGridView2.DataSource = dbk.DS.Tables["book"].DefaultView;

                member_header2();


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
    }
}

