using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Management_2.BL
{
    class cls_user_mng
    {
        DAL.cls_dal dal = new DAL.cls_dal();

        public DataTable user_update(string user_name, string pass, string user_type,string arabic_name,int User_id)
        {
            
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@user_name", SqlDbType.NVarChar, 50);
            param[0].Value = user_name;

            param[1] = new SqlParameter("@pass", SqlDbType.NVarChar, 30);
            param[1].Value = pass;

            param[2] = new SqlParameter("@user_type", SqlDbType.NVarChar, 30);
            param[2].Value = user_type;
            
            param[3] = new SqlParameter("@arabic_name", SqlDbType.NVarChar, 30);
            param[3].Value = @arabic_name;
            
            param[4] = new SqlParameter("@User_id", SqlDbType.NVarChar, 30);
            param[4].Value = @User_id;

            //param[2] = new SqlParameter("@User_id", SqlDbType.Int);
            //param[2].Value = User_id;


            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_user_update", param);
            return dt;
        }
        public DataTable user_insert(string user_name, string pass, string user_type, string arabic_name)
        {

            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@user_name", SqlDbType.NVarChar, 50);
            param[0].Value = user_name;

            param[1] = new SqlParameter("@pass", SqlDbType.NVarChar, 30);
            param[1].Value = pass;

            param[2] = new SqlParameter("@user_type", SqlDbType.NVarChar, 30);
            param[2].Value = user_type;

            param[3] = new SqlParameter("@arabic_name", SqlDbType.NVarChar, 30);
            param[3].Value = @arabic_name;


            //param[2] = new SqlParameter("@User_id", SqlDbType.Int);
            //param[2].Value = User_id;


            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_user_insert", param);
            return dt;
        }
        public DataTable user_check(string user_name)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@user_name", SqlDbType.NVarChar, 50);
            param[0].Value = user_name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_user_check", param);
            return dt;
        }
        public DataTable admin_check(string pass)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@pass", SqlDbType.NVarChar, 50);
            param[0].Value = pass;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_admin_check", param);
            return dt;
        }
        public DataTable user_delete(int user_id)
        {

            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@user_id", SqlDbType.NVarChar, 50);
            param[0].Value = user_id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_user_delete", param);
            return dt;
        }

    }
}
