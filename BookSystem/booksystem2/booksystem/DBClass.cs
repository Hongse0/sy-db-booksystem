using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace booksystem
{
    class DBClass
    {
        private String ret;
        private String sql;
        private int selectedRowIndex;
        private int selectedKeyValue; 


        OracleConnection con;
        OracleCommand dcom;
        OracleDataAdapter da; // Data Provider인 DBAdapter 입니다.
        OracleDataAdapter dBAdapter;

        DataSet ds;// DataSet 객체입니다.
        OracleDataReader dr; //DataReader 객체입니다.
        OracleCommandBuilder myCommandBuilder; // 추가, 수정, 삭제시에 필요한 명령문을 자동으로 작성해주는 객체입니다.
        DataTable phoneTable;// DataTable 객체입니다.


        public String Ret { get { return ret; } set { ret = value; } }
        public String SQL { get { return sql; } set { sql = value; } }

        public OracleConnection Con { get { return con; } set { con = value; } }
        public int SelectedRowIndex
        {
            get { return selectedRowIndex; }
            set { selectedRowIndex = value; }
        }
        public OracleCommand DCom { get { return dcom; } set { dcom = value; } }
        public OracleDataAdapter DA { get { return da; } set { da = value; } }
        public int SelectedKeyValue
        {
            get { return selectedKeyValue; }
            set
            { selectedKeyValue = value; }
        }
        public OracleDataReader DR { get { return dr; } set { dr = value; } }
        public DataSet DS { get { return ds; } set { ds = value; } }
        public OracleCommandBuilder MyCommandBuilder { get { return myCommandBuilder; } set { myCommandBuilder = value; } }
        public DataTable PhoneTable { get { return phoneTable; } set { phoneTable = value; } }

        public OracleDataAdapter DBAdapter { get { return dBAdapter; } set { dBAdapter = value; } }

        public void DB_Open()
        {
            try
            {
                string connectionString = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION =   (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))   (CONNECT_DATA =     (SERVER = DEDICATED)     (SERVICE_NAME = xe)   ) );";

                string commandString = "select * from book";

                DA = new OracleDataAdapter(commandString, connectionString);
               
                MyCommandBuilder = new OracleCommandBuilder(DA);
                

                DS = new DataSet();
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        public void DB_Access()
        {
            try
            {
                string My_con = "User Id=hong1; Password=1111; Data Source=(DESCRIPTION =   (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))   (CONNECT_DATA =     (SERVER = DEDICATED)     (SERVICE_NAME = xe)   ) );";

                Con = new OracleConnection();
                Con.ConnectionString = My_con;
                DCom = new OracleCommand();
                DCom.Connection = Con;
                Con.Open();
            }
            catch (DataException DE)
            {
                MessageBox.Show(DE.Message);
            }
        }
        public void DB_ObjCreate()
        {
            PhoneTable = new DataTable();

        }
    }

}
