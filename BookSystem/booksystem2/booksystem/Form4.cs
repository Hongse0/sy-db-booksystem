using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Oracle.DataAccess.Client;

namespace booksystem
{
    public partial class Form4 : Form
    {
        DBClass dbc = new DBClass();

        public Form4()
        {
            InitializeComponent();
            dbc.DB_Open();
            dbc.DB_ObjCreate(); 
           
        }
        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("텍스트 상자에 모든 데이터 입력하셨으면 추가합니다");

                dbc.DS.Clear();
                dbc.DA.Fill(dbc.DS, "book");
                dbc.PhoneTable = dbc.DS.Tables["book"];
                
                DataRow newRow = dbc.PhoneTable.NewRow();
                newRow["b_code"] = textBox1.Text;
                newRow["b_title"] = textBox2.Text;
                newRow["b_name"] = textBox3.Text;
                newRow["b_publishing"] = textBox4.Text;
                newRow["b_date"] = textBox5.Text;
                newRow["b_price"] = textBox6.Text;
                newRow["b_count"] = textBox7.Text;
                dbc.PhoneTable.Rows.Add(newRow);
                dbc.DA.Update(dbc.DS, "book");
                dbc.DS.AcceptChanges();
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
            this.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {
                
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
