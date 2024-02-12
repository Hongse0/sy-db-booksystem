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

namespace booksystem
{
    public partial class Form5 : Form
    {
        new Form2 Parent;
        DBClass dbc = new DBClass();

        public Form5()
        {
            InitializeComponent();
            dbc.DB_ObjCreate(); 
            dbc.DB_Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex != 0)
            {
                ListBox1.SelectedIndex = ListBox1.SelectedIndex - 1;
            }
            else
            {
                MessageBox.Show("이곳은 레코드의 처음입니다.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex != ListBox1.Items.Count - 1)
            {
                ListBox1.SelectedIndex = ListBox1.SelectedIndex + 1;
            }
            else
            {
                MessageBox.Show("이곳은 레코드의 마지막입니다.");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Parent = (Form2)Owner;
            dbc.PhoneTable = dbc.DS.Tables["book"];
            DataRow[] ResultRows = dbc.PhoneTable.Select("b_title like '%" + Parent.TxtS + "%'");
            DataColumn[] PrimaryKey = new DataColumn[1];
            PrimaryKey[0] = dbc.PhoneTable.Columns["b_code"];
            dbc.PhoneTable.PrimaryKey = PrimaryKey;
            DataRow currRow = dbc.PhoneTable.Rows.Find(ListBox1.Text.Substring(0, 6));
            textBox1.Text = currRow["b_code"].ToString();
            textBox2.Text = currRow["b_title"].ToString();
            textBox3.Text = currRow["b_name"].ToString();
            textBox4.Text = currRow["b_publishing"].ToString();
            textBox5.Text = currRow["b_date"].ToString();
            textBox6.Text = currRow["b_price"].ToString();
            textBox7.Text = currRow["b_count"].ToString();
           
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dbc.DS.Clear();
            dbc.DA.Fill(dbc.DS, "book");
            Parent = (Form2)Owner;
            dbc.PhoneTable = dbc.DS.Tables["book"];
            DataRow[] ResultRows
            = dbc.PhoneTable.Select("b_title like '%" + Parent.TxtS + "%'");
            ListBox1.Items.Clear();
            foreach (DataRow currRow in ResultRows)
            {
                ListBox1.Items.Add(currRow["b_code"].ToString()
                + " " + currRow["b_title"].ToString());
            }
        }
    }
}
