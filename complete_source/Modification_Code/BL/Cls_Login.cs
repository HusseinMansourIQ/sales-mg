using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Sales_Management_2.DAL;

namespace Sales_Management.PL
{
    class Cls_Login
    {
        public DataTable Login(string U_Name, string U_Pass)
        {
            cls_dal DAL = new cls_dal();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@U_Name", SqlDbType.NVarChar, 50);
            param[0].Value = U_Name;

            param[1] = new SqlParameter("@U_Pass", SqlDbType.NVarChar, 50);
            param[1].Value = U_Pass;
            //param[2] = new SqlParameter("@User_id", SqlDbType.Int);
            //param[2].Value = User_id;


            DAL.Open();
            DataTable dt = new DataTable();
            dt = DAL.SelectData("SP_LOGIN", param);
            return dt;
        }



    }
}
