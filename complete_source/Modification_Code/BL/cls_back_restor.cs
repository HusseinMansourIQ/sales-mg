using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales_Management_2.BL
{
    class cls_back_restor
    {
        
            DAL.cls_dal dal = new DAL.cls_dal();


            public DataTable backup(string fileName)
            {
                DAL.cls_dal dal = new DAL.cls_dal();
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter("@fileName", SqlDbType.NVarChar, 500);
                param[0].Value = fileName;

                dal.Open();
                DataTable dt = new DataTable();
                dt = dal.SelectData("sp_backup", param);
                return dt;
            }

        public DataTable restor(string fileName)
        {
            DAL.cls_dal dal = new DAL.cls_dal();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@fileName", SqlDbType.NVarChar, 500);
            param[0].Value = fileName;

            dal.Open2();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_restor", param);
            return dt;
        }


    }
}
