using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Sales_Management_2.BL
{
    class Cls_Products
    {
        DAL.cls_dal dal = new DAL.cls_dal();
        public DataTable FillProductDGV()
        {

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("Sp_GetAllProducts", null);
            dal.Close();
            return dt;
        }
        public DataTable SearchForProduct(string Pro_Name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Pro_Name", SqlDbType.NVarChar, (50));
            param[0].Value = Pro_Name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("Sp_SearchForProduct", param);
            return dt;
        }
        public DataTable update_Pro_Qty(int Pro_Id, int Pro_Qty)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Pro_Id", SqlDbType.Int);
            param[0].Value = Pro_Id;

            param[1] = new SqlParameter("@Pro_Qty ", SqlDbType.Int);
            param[1].Value = Pro_Qty;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_update_Pro_Qty", param);
            return dt;
        }

        public void UpdateProduct(int Pro_Id, string Pro_Name, int Pro_Qty, string Pro_Unit,
                    string Pro_Price, string Single_price, string WholeSale_price,
                    int ReOrederLimit, DateTime Update_date, string UserWhoUpdate, string Note)
        {
            DAL.cls_dal dal = new DAL.cls_dal();
            dal.Open();
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@Pro_Id", SqlDbType.Int);
            param[0].Value = Pro_Id;

            param[1] = new SqlParameter("@Pro_Name", SqlDbType.NVarChar, (50));
            param[1].Value = Pro_Name;

            param[2] = new SqlParameter("@Pro_Qty", SqlDbType.Int);
            param[2].Value = Pro_Qty;

            param[3] = new SqlParameter("@Pro_Unit", SqlDbType.NVarChar, (50));
            param[3].Value = Pro_Unit;

            param[4] = new SqlParameter("@Pro_Price", SqlDbType.NVarChar, (50));
            param[4].Value = Pro_Price;

            param[5] = new SqlParameter("@Single_price", SqlDbType.NVarChar, (50));
            param[5].Value = Single_price;

            param[6] = new SqlParameter("@WholeSale_price", SqlDbType.NVarChar, (50));
            param[6].Value = WholeSale_price;

            param[7] = new SqlParameter("@ReOrederLimit", SqlDbType.Int);
            param[7].Value = ReOrederLimit;

            param[8] = new SqlParameter("@Update_date", SqlDbType.DateTime);
            param[8].Value = Update_date;

            param[9] = new SqlParameter("@UserWhoUpdate", SqlDbType.NVarChar, (50));
            param[9].Value = UserWhoUpdate;

            param[10] = new SqlParameter("@Note", SqlDbType.NVarChar, (50));
            param[10].Value = Note;


            dal.Sqlcomand("Sp_UpdateProduct", param);
            dal.Close();

        }
        public void insertProduct(string Pro_Name, int Pro_Qty, string Pro_Unit,
            string Pro_Price, string Single_price, string WholeSale_price,
            DateTime Insert_date, string UserWhoInsert,string Note)
        {
            DAL.cls_dal dal = new DAL.cls_dal();
            dal.Open();
            SqlParameter[] param = new SqlParameter[9];

            param[0] = new SqlParameter("@Pro_Name", SqlDbType.NVarChar, (50));
            param[0].Value = Pro_Name;

            param[1] = new SqlParameter("@Pro_Qty", SqlDbType.Int);
            param[1].Value = Pro_Qty;

            param[2] = new SqlParameter("@Pro_Unit", SqlDbType.NVarChar, (50));
            param[2].Value = Pro_Unit;

            param[3] = new SqlParameter("@Pro_Price", SqlDbType.NVarChar, (50));
            param[3].Value = Pro_Price;

            param[4] = new SqlParameter("@Single_price", SqlDbType.NVarChar, (50));
            param[4].Value = Single_price;

            param[5] = new SqlParameter("@WholeSale_price", SqlDbType.NVarChar, (50));
            param[5].Value = WholeSale_price;

            param[6] = new SqlParameter("@Insert_date", SqlDbType.DateTime);
            param[6].Value = Insert_date;

            param[7] = new SqlParameter("UserWhoInsert", SqlDbType.NVarChar, (50));
            param[7].Value = UserWhoInsert;

            param[8] = new SqlParameter("Note", SqlDbType.NVarChar, (50));
            param[8].Value = Note;


            dal.Sqlcomand("SP_insertProduct", param);
            dal.Close();

        }

        public DataTable check_prod_name(string Pro_Name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Pro_Name", SqlDbType.NVarChar, (50));
            param[0].Value = Pro_Name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_check_prod_name", param);
            return dt;
        }

        public void delete_PRODUCT(int id)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Value = id;

            dal.Sqlcomand("sp_delete_PRODUCT", param);
            dal.Close();
        }

    }
}
