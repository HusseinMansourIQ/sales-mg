using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Sales_Management_2.BL
{
    class Cls_AutoFill
    {
        public DataTable AutoFill(string Cust_name)
        {
            DAL.cls_dal dal = new DAL.cls_dal();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@Cust_name", SqlDbType.NVarChar, (50));
            param[0].Value = Cust_name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("Sp_AutoFill", param);
            return dt;
        }

    }
}
