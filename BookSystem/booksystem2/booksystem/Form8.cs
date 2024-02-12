using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Types;

using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace booksystem
{  
    public partial class Form8 : Form
    {
        private OracleConnection odpConn = new OracleConnection();
        Form2 f2;
        DBClass dbc = new DBClass();
        public Form8(Form2 f)
        {
            InitializeComponent();
            dbc.DB_Open();
            dbc.DB_ObjCreate();
            f2 = f;
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            /*     try
                 {
                     dbc.DS.Clear();
                     dbc.DA.Fill(dbc.DS, "book");
                     dbc.PhoneTable = dbc.DS.Tables["book"];

                     DataColumn[] PrimaryKey = new DataColumn[1];
                     PrimaryKey[0] = dbc.PhoneTable.Columns["b_code"];

                     dbc.PhoneTable.PrimaryKey = PrimaryKey;
                     DataRow currRow = dbc.PhoneTable.Rows.Find(f2.SelectedRowIndex);

                     currRow.BeginEdit();
                     currRow["b_price"] = textBox1.Text;
                     currRow["b_count"] = textBox2.Text;
                     currRow.EndEdit();
                     DataSet UpdatedSet = dbc.DS.GetChanges(DataRowState.Modified);
                     if (UpdatedSet.HasErrors)
                     { MessageBox.Show("변경된 데이터에 문제가 있습니다."); }
                     else
                     {
                         dbc.DBAdapter.Update(UpdatedSet, "book");
                         dbc.DS.AcceptChanges();
                     }
                     f2.dataGridView1.DataSource = dbc.DS.Tables["book"].DefaultView;
                 }
                 catch (DataException DE)
                 { MessageBox.Show(DE.Message); }
                 catch (Exception DE)
                 { MessageBox.Show(DE.Message); }
            */
            try
            {
                string ConStr = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION =   (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))   (CONNECT_DATA =     (SERVER = DEDICATED)     (SERVICE_NAME = xe)   ) );";
                OracleConnection conn = new OracleConnection(ConStr);
                conn.Open();
                OracleDataAdapter DBAdapter = new OracleDataAdapter();

                DBAdapter.SelectCommand = new OracleCommand(string.Format("UPDATE book set b_count =:b_count ,b_price=:b_price where b_code = {0}",f2.SelectedRowIndex), conn);
                DBAdapter.SelectCommand.Parameters.Add("b_count", OracleDbType.Varchar2, 20);
                DBAdapter.SelectCommand.Parameters["b_count"].Value = textBox1.Text.Trim();
                DBAdapter.SelectCommand.Parameters.Add("b_price", OracleDbType.Varchar2, 20);
                DBAdapter.SelectCommand.Parameters["b_price"].Value = textBox2.Text.Trim();
                DataSet DS = new DataSet();
                DBAdapter.Fill(DS, "book");
                DataTable phoneTable = DS.Tables["book"];
                f2.dataGridView1.DataSource = phoneTable;
            }
            catch (DataException DE)
            { MessageBox.Show(DE.Message); }
            catch (Exception DE)
            { MessageBox.Show(DE.Message); }

            f2.showDataGridView();
            this.Dispose();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
