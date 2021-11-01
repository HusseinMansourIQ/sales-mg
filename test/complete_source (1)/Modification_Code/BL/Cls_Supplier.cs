using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Sales_Management_2.BL
{
    class Cls_InsertSupplier
    {
        DAL.cls_dal dal = new DAL.cls_dal();
        public DataTable FillSupplierDGV()
        {

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("SP_GetAllSupplieres", null);
            dal.Close();
            return dt;
        }
        public DataTable SearchForSupplier(string Supplier_name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Supplier_name", SqlDbType.NVarChar, (50));
            param[0].Value = Supplier_name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("SearchForSupplier", param);
            return dt;
        }
        public DataTable check_Supplier_name(string Supplier_Name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Supplier_Name", SqlDbType.NVarChar, (50));
            param[0].Value = Supplier_Name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_check_Supplier_name", param);
            return dt;
        }

        public void InsertSupplier(string Supplier_series, string Supplier_name, string Supplier_Tel, string Supplier_addres,
                                    string BalanceToSupplier, string BalanceOnSupplier, DateTime Insert_date, string Note)
        {
            DAL.cls_dal dal = new DAL.cls_dal();
            dal.Open();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@Supplier_series", SqlDbType.NVarChar);
            param[0].Value = Supplier_series;

            param[1] = new SqlParameter("@Supplier_name", SqlDbType.NVarChar);
            param[1].Value = Supplier_name;

            param[2] = new SqlParameter("@Supplier_Tel", SqlDbType.NVarChar);
            param[2].Value = Supplier_Tel;

            param[3] = new SqlParameter("@Supplier_addres", SqlDbType.NVarChar);
            param[3].Value = Supplier_addres;

            param[4] = new SqlParameter("@BalanceToSupplier", SqlDbType.NVarChar);
            param[4].Value = BalanceToSupplier;

            param[5] = new SqlParameter("@BalanceOnSupplier", SqlDbType.NVarChar);
            param[5].Value = BalanceOnSupplier;

            param[6] = new SqlParameter("@Insert_date", SqlDbType.Date);
            param[6].Value = Insert_date;

            // param[7] = new SqlParameter("@Ope_id", SqlDbType.Int);
            //param[7].Value = Ope_id;

            param[7] = new SqlParameter("@Note", SqlDbType.NVarChar);
            param[7].Value = Note;

            dal.Sqlcomand("SP_insertSupplier", param);
            dal.Close();
        }
        public void UpdateSupplaier(string Supplier_series, string Supplier_name, string Supplier_Tel, string Supplier_addres,
                            string BalanceToSupplier, string BalanceOnSupplier, DateTime Update_date, string Note, int Supplier_Id, string UserWhoUpdate)
        {
            DAL.cls_dal dal = new DAL.cls_dal();
            dal.Open();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Supplier_series", SqlDbType.NVarChar);
            param[0].Value = Supplier_series;

            param[1] = new SqlParameter("@Supplier_name", SqlDbType.NVarChar);
            param[1].Value = Supplier_name;

            param[2] = new SqlParameter("@Supplier_Tel", SqlDbType.NVarChar);
            param[2].Value = Supplier_Tel;

            param[3] = new SqlParameter("@Supplier_addres", SqlDbType.NVarChar);
            param[3].Value = Supplier_addres;

            param[4] = new SqlParameter("@BalanceToSupplier", SqlDbType.NVarChar);
            param[4].Value = BalanceToSupplier;

            param[5] = new SqlParameter("@BalanceOnSupplier", SqlDbType.NVarChar);
            param[5].Value = BalanceOnSupplier;

            param[6] = new SqlParameter("@Update_date", SqlDbType.Date);
            param[6].Value = Update_date;

            param[7] = new SqlParameter("@Note", SqlDbType.NVarChar);
            param[7].Value = Note;

            param[8] = new SqlParameter("@Supplier_Id", SqlDbType.NVarChar);
            param[8].Value = Supplier_Id;

            param[9] = new SqlParameter("@UserWhoUpdate", SqlDbType.NVarChar);
            param[9].Value = UserWhoUpdate;



            dal.Sqlcomand("Sp_UpdateSupplaier", param);
            dal.Close();

        }
        public DataTable update_balance_for_suplier(int Supplier_Id, decimal balance)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Supplier_Id", SqlDbType.Int);
            param[0].Value = Supplier_Id;

            param[1] = new SqlParameter("@balance", SqlDbType.Decimal);
            param[1].Value = balance;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_update_balance_for_suplier", param);
            return dt;
        }
        public void delete_supplier(int id)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Value = id;

            dal.Sqlcomand("sp_delete_supplier", param);
            dal.Close();
        }
    }
}
