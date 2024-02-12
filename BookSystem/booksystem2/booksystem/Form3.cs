using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace booksystem
{
    public partial class Form3 : Form
    {
        string sqlstr;    
        DBClass dbc = new DBClass();
        public Form3()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
            dbc.DB_Open();
        }
        public void member_counter()
        {
            int i;
            i = dataGridView1.RowCount;
            label1.Text = "총 " + i + " 명";
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

        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                dbc.DB_ObjCreate(); 
                dbc.DB_Open();
                dbc.DB_Access();

                sqlstr = "Select * From member ORDER BY m_id ASC";
                dbc.DCom.CommandText = sqlstr;
                dbc.DA.SelectCommand = dbc.DCom;
                dbc.DA.Fill(dbc.DS, "member");
                dbc.DS.Tables["member"].Clear();
                dbc.DA.Fill(dbc.DS, "member");
                dataGridView1.DataSource = dbc.DS.Tables["member"].DefaultView;
                member_counter();
                member_header();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
                textBox2.Text = currRow["m_name"].ToString();
                textBox3.Text = currRow["m_phone"].ToString();
                textBox4.Text = currRow["m_adress"].ToString();
                
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
