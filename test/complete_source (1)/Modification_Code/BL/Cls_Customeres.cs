using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Sales_Management_2.BL
{
    class Cls_Customeres
    {
        DAL.cls_dal dal= new DAL.cls_dal();

        public DataTable FillCustomeresDGV()
        {
            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("SP_GetAllCustomeres", null);
            dal.Close();
            return dt;
        }
        public void InsertCustomer(string Cost_series, string Cust_name, string Cust_Tel, string Cust_addres,
                            string BalanceToCostmer, string BalanceOnCostmer, DateTime Insert_date, string Note)
        {
            DAL.cls_dal dal = new DAL.cls_dal();
            dal.Open();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Cost_series", SqlDbType.NVarChar);
            param[0].Value = Cost_series;

            param[1] = new SqlParameter("@Cust_name", SqlDbType.NVarChar);
            param[1].Value = Cust_name;

            param[2] = new SqlParameter("@Cust_Tel", SqlDbType.NVarChar);
            param[2].Value = Cust_Tel;

            param[3] = new SqlParameter("@Cust_addres", SqlDbType.NVarChar);
            param[3].Value = Cust_addres;

            param[4] = new SqlParameter("@BalanceToCostmer", SqlDbType.NVarChar);
            param[4].Value = BalanceToCostmer;

            param[5] = new SqlParameter("@BalanceOnCostmer", SqlDbType.NVarChar);
            param[5].Value = BalanceOnCostmer;

            param[6] = new SqlParameter("@Insert_date", SqlDbType.Date);
            param[6].Value = Insert_date;

            param[7] = new SqlParameter("@Note", SqlDbType.NVarChar);
            param[7].Value = Note;
            dal.Sqlcomand("SP_insertCustomer", param);
            dal.Close();
        }
        public void update_customer(int Cust_Id, string Cust_name, string Cust_Tel, string Cust_addres,
                           string BalanceToCostmer, string BalanceOnCostmer, string user_name, string Note)
        {
            DAL.cls_dal dal = new DAL.cls_dal();
            dal.Open();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Cust_Id", SqlDbType.NVarChar);
            param[0].Value = Cust_Id;

            param[1] = new SqlParameter("@Cust_name", SqlDbType.NVarChar);
            param[1].Value = Cust_name;

            param[2] = new SqlParameter("@Cust_Tel", SqlDbType.NVarChar);
            param[2].Value = Cust_Tel;

            param[3] = new SqlParameter("@Cust_addres", SqlDbType.NVarChar);
            param[3].Value = Cust_addres;

            param[4] = new SqlParameter("@BalanceToCostmer", SqlDbType.NVarChar);
            param[4].Value = BalanceToCostmer;

            param[5] = new SqlParameter("@BalanceOnCostmer", SqlDbType.NVarChar);
            param[5].Value = BalanceOnCostmer;

            param[6] = new SqlParameter("@user_name", SqlDbType.NVarChar);
            param[6].Value = user_name;

            param[7] = new SqlParameter("@Note", SqlDbType.NVarChar);
            param[7].Value = Note;
            dal.Sqlcomand("sp_update_customer", param);
            dal.Close();
        }
        public DataTable SearchForCustomer(string Cust_name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Cust_name", SqlDbType.NVarChar, (50));
            param[0].Value = Cust_name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("SearchForCustomer", param);
            return dt;
        }
        public DataTable SearchForCustomer_depts(string Cust_name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Cust_name", SqlDbType.NVarChar, (50));
            param[0].Value = Cust_name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_SearchForCustomer_depts", param);
            return dt;
        }

        public DataTable get_cust_detailes_by_SalesBill_Id(int SalesBill_Id)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SalesBill_Id", SqlDbType.Int);
            param[0].Value = SalesBill_Id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_cust_detailes_by_SalesBill_Id", param);
            return dt;
        }

        public DataTable check_Cust_name(string Cust_name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Cust_name", SqlDbType.NVarChar, (50));
            param[0].Value = Cust_name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_check_Cust_name", param);
            return dt;
        }

        public DataTable update_balance(int Cust_Id, decimal balance)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Cust_Id", SqlDbType.Int);
            param[0].Value = Cust_Id;

            param[1] = new SqlParameter("@balance", SqlDbType.Decimal);
            param[1].Value = balance;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_update_balance", param);
            return dt;
        }
        public void delete_cust(int id)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Value = id;

            dal.Sqlcomand("sp_delete_customer", param);
            dal.Close();
        }


    }
}
