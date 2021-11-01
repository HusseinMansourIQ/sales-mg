using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Sales_Management_2.BL
{
    class Cls_Sales
    {
        DAL.cls_dal dal = new DAL.cls_dal();
        //  DataTable dt2 = new DataTable;
        public void AddSalesBill(int SalesBill_Series, DateTime SalesBill_Date, string SalesBill_Type, int Cust_Id,
                                string Total_Befor_Discount, string Total_After_Discount, string Total_Discount,
                                string Paid_Money, string Rest_Money, decimal Cust_Balance_After_Bill, string User_Name,string Bill_category,string nots)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@SalesBill_Series", SqlDbType.Int);
            param[0].Value = SalesBill_Series;

            param[1] = new SqlParameter("@SalesBill_Date", SqlDbType.DateTime);
            param[1].Value = SalesBill_Date;

            param[2] = new SqlParameter("@SalesBill_Type", SqlDbType.NVarChar, (50));
            param[2].Value = SalesBill_Type;

            param[3] = new SqlParameter("@Cust_Id", SqlDbType.Int);
            param[3].Value = Cust_Id;

            param[4] = new SqlParameter("@Total_Befor_Discount", SqlDbType.NVarChar, (50));
            param[4].Value = Total_Befor_Discount;

            param[5] = new SqlParameter("@Total_After_Discount", SqlDbType.NVarChar, (50));
            param[5].Value = Total_After_Discount;

            param[6] = new SqlParameter("@Total_Discount", SqlDbType.NVarChar, (50));
            param[6].Value = Total_Discount;

            param[7] = new SqlParameter("@Paid_Money", SqlDbType.NVarChar, (50));
            param[7].Value = Paid_Money;

            param[8] = new SqlParameter("@Rest_Money", SqlDbType.NVarChar, (50));
            param[8].Value = Rest_Money;

            param[9] = new SqlParameter("@Cust_Balance_After_Bill", SqlDbType.Decimal);
            param[9].Value = Cust_Balance_After_Bill;

            param[10] = new SqlParameter("@User_Name", SqlDbType.NVarChar, (50));
            param[10].Value = User_Name;

            param[11] = new SqlParameter("@Bill_category", SqlDbType.NVarChar, (50));
            param[11].Value = Bill_category;

            param[12] = new SqlParameter("@nots", SqlDbType.NVarChar, (50));
            param[12].Value = nots;

            dal.Sqlcomand("Sp_AddSalesBill", param);
            dal.Close();
        }
        public void AddSalesOpp(int Pro_Id, string QTY, string PriceAfterDiscount, string TotalPrice,
                        int SalesBill_Id, int User_id,string PriceBeforDiscount)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@Pro_Id", SqlDbType.Int);
            param[0].Value = Pro_Id;

            param[1] = new SqlParameter("@QTY", SqlDbType.NVarChar, (50));
            param[1].Value = QTY;

            param[2] = new SqlParameter("@PriceAfterDiscount", SqlDbType.NVarChar, (50));
            param[2].Value = PriceAfterDiscount;

            param[3] = new SqlParameter("@TotalPrice", SqlDbType.NVarChar, (50));
            param[3].Value = TotalPrice;

            param[4] = new SqlParameter("@SalesBill_Id", SqlDbType.Int);
            param[4].Value = SalesBill_Id;

            param[5] = new SqlParameter("@User_id", SqlDbType.Int);
            param[5].Value = User_id;

            param[6] = new SqlParameter("@PriceBeforDiscount", SqlDbType.Int);
            param[6].Value = PriceBeforDiscount;


            dal.Sqlcomand("Sp_AddSalesOpp", param);
            dal.Close();
        }
        public void  delete_sales_bill(int id)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", SqlDbType.Int);
            param[0].Value = id;

            dal.Sqlcomand("sp_delete_sales_bill", param);
            dal.Close();
        }
        public DataTable get_bill_by_id(int SalesBill_Id)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SalesBill_Id", SqlDbType.Int);
            param[0].Value = SalesBill_Id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_bill_by_id", param);
            return dt;
        }
        public DataTable sp_get_bill_by_Cust_name(string Cust_name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Cust_name", SqlDbType.NVarChar,50);
            param[0].Value = Cust_name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_bill_by_Cust_name", param);
            return dt;
        }

        public DataTable get_bill_by_id_for_print(int SalesBill_Id)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SalesBill_Id", SqlDbType.Int);
            param[0].Value = SalesBill_Id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_bill_by_id_for_print", param);
            return dt;
        }
        public DataTable get_bill_and_opp_by_Cust_name(string Cust_name)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Cust_name", SqlDbType.NVarChar,50);
            param[0].Value = Cust_name;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_bill_and_opp_by_Cust_name", param);
            return dt;
        }

        public DataTable get_SalesOpp_by_SalesBill_Id(int SalesBill_Id)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SalesBill_Id", SqlDbType.Int);
            param[0].Value = SalesBill_Id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_SalesOpp_by_SalesBill_Id", param);
            return dt;
        }
        public DataTable get_SelesBill_det_by_id_(int SalesBill_Id)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@SalesBill_Id", SqlDbType.Int);
            param[0].Value = SalesBill_Id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_SelesBill_det_by_id_", param);
            return dt;
        }


        //public void EditPurchaseBill(int PurchaseBill_Id)
        //{
        //    dal.Open();
        //    SqlParameter[] param = new SqlParameter[11];
        //    param[0] = new SqlParameter("@PurchaseBill_Id", SqlDbType.Int);
        //    param[0].Value = PurchaseBill_Id;

        //    dal.Sqlcomand("Sp_EditPurchaseBill", param);
        //    dal.Close();
        //}

        //public DataTable EditPurchaseBill(int SalesBill_Id)
        //{
        //    //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

        //    SqlParameter[] param = new SqlParameter[1];
        //    param[0] = new SqlParameter("@SalesBill_Id", SqlDbType.NVarChar, (50));
        //    param[0].Value = SalesBill_Id;

        //    dal.Open();
        //    //DataTable dt = new DataTable();
        //    dt2 = dal.SelectData("Sp_EditPurchaseBill", param);
        //    return dt2;
        //}

        public DataTable GetLastSalesBill_Id()
        {
            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("Sp_GetLastSalesBill_Id", null);
            dal.Close();
            return dt;
        }

    }

}
