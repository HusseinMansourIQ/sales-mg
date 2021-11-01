using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Management_2.BL
{
    class cls_depts
    {
        DAL.cls_dal dal = new DAL.cls_dal();


        public DataTable search_for_debts(string search, DateTime from, DateTime to , int mony,string opp)
        {
           
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@search", SqlDbType.NVarChar, (50));
            param[0].Value = search;

            param[1] = new SqlParameter("@from", SqlDbType.DateTime);
            param[1].Value = from;

            param[2] = new SqlParameter("@to", SqlDbType.DateTime);
            param[2].Value = to;

            param[3] = new SqlParameter("@mony", SqlDbType.Int);
            param[3].Value = mony;

            param[4] = new SqlParameter("@opp", SqlDbType.NVarChar, (50));
            param[4].Value = opp;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_search_for_debts", param);
            return dt;
        }
        public DataTable search_debts_by_period(string val)
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@val", SqlDbType.NVarChar, (50));
            param[0].Value = val;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_search_debts_by_period", param);
            return dt;
        }


    }
}
