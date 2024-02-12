using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace booksystem
{
    public partial class Form7 : Form
    {
        string buyersql;
        DBClass dbc = new DBClass();
        public Form7()
        {
            InitializeComponent();
            dbc.DB_ObjCreate();
            dbc.DB_Open();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable phoneTable = dbc.DS.Tables["buyer"];
            if (e.RowIndex < 0)
            {
                return;
            }
            else if (e.RowIndex > phoneTable.Rows.Count - 1)
            {
                MessageBox.Show("해당하는 데이터가 존재하지 않습니다.");
                return;
            }
        }
        public void member_header3()
        {
            dataGridView1.Columns[0].HeaderText = "구매자";
            dataGridView1.Columns[1].HeaderText = "책 이름";
            dataGridView1.Columns[2].HeaderText = "구매 날짜";
            dataGridView1.Columns[3].HeaderText = "책 코드";



            dataGridView1.Columns[0].Width = 70;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 100;

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            try
            {
                dbc.DB_ObjCreate();
                dbc.DB_Open();
                dbc.DB_Access();

                buyersql = "Select * From buyer";
                dbc.DCom.CommandText = buyersql;
                dbc.DA.SelectCommand = dbc.DCom;
                dbc.DA.Fill(dbc.DS, "buyer");
                dbc.DS.Tables["buyer"].Clear();
                dbc.DA.Fill(dbc.DS, "buyer");
                dataGridView1.DataSource = dbc.DS.Tables["buyer"].DefaultView;
                member_header3();
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
