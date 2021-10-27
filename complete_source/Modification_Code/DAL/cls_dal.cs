using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Sales_Management_2.DAL
{
    class cls_dal
    {
        public SqlConnection sqlcon;   
        public SqlConnection sqlcon2;
       public string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
        //كونسكتر انشاء الاتصال
        public cls_dal()

        {
            
            //User Id=amc;Password=amc     integrated security=true
            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
            // sqlcon = new SqlConnection(@"Server=.;Database=DB_SaleApp;Persist Security Info=true;User Id=sales;Password=sales;Max Pool Size=50000;Pooling=True");
            //sqlcon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //this will detect where the app is current running
            sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ domain + "selapp.mdf;" + "Integrated Security=True");
            sqlcon2= new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ domain + "selapp.mdf;" + "Integrated Security=True");
        }
           

        //ميثود فتح الاتصال فقط اذا كان مغلق
        public void Open()
        {
            if (sqlcon.State != ConnectionState.Open)
            {
                sqlcon.Open();
            }
        }
        public void Open2()
        {
            if (sqlcon2.State != ConnectionState.Open)
            {
                sqlcon2.Open();
            }
        }
        //ميثود غلق الاتصال فقط اذا كان مفتوح 
        public void Close()
        {
            if (sqlcon.State == ConnectionState.Open)
            {
                sqlcon.Close();
            }
        }
        public void Close2()
        {
            if (sqlcon2.State == ConnectionState.Open)
            {
                sqlcon2.Close();
            }
        }
        //ميثود قرائة البيانات من والى السيرفر
        public DataTable SelectData(string st_Procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = st_Procedure;
            sqlcmd.Connection = sqlcon;
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        //ميثود تنفيذ عمليات insert ,update,delete
        public void Sqlcomand(string st_Procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = st_Procedure;
            sqlcmd.Connection = sqlcon;

            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }
        public DataSet ds_SelectData(string st_Procedure, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = st_Procedure;
            sqlcmd.Connection = sqlcon;
            if (param != null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

    }
}
