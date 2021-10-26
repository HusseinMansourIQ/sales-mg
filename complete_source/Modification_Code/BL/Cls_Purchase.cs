using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Sales_Management_2.DAL;

namespace Sales_Management_2.BL
{
    class Cls_Purchase
    // كلاس خاص بكل عمليات الشراء 
    {
        DAL.cls_dal dal = new DAL.cls_dal();
        public void AddPurchaseBill(int PurchaseBill_Id, DateTime Purchase_date, int Supplier_Id, string Bill_No,
             DateTime Bill_Date, string Total, string UserName,string Paid_Money, string Rest_Money, string Sup_Balance_After_Bill)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@PurchaseBill_Id", SqlDbType.Int);
            param[0].Value = PurchaseBill_Id;

            param[1] = new SqlParameter("@Purchase_date", SqlDbType.DateTime);
            param[1].Value = Purchase_date;

            param[2] = new SqlParameter("@Supplier_Id", SqlDbType.Int);
            param[2].Value = Supplier_Id;

            param[3] = new SqlParameter("@Bill_No", SqlDbType.NVarChar, (10));
            param[3].Value = Bill_No;


            param[4] = new SqlParameter("@Bill_Date", SqlDbType.Date);
            param[4].Value = Bill_Date;

            param[5] = new SqlParameter("@Total", SqlDbType.NVarChar);
            param[5].Value = Total;

            param[6] = new SqlParameter("@UserName", SqlDbType.NVarChar, (50));
            param[6].Value = UserName;

            param[7] = new SqlParameter("@Paid_Money", SqlDbType.NVarChar, (50));
            param[7].Value = Paid_Money;

            param[8] = new SqlParameter("@Rest_Money", SqlDbType.NVarChar, (50));
            param[8].Value = Rest_Money;

            param[9] = new SqlParameter("@Sup_Balance_After_Bill", SqlDbType.NVarChar, (50));
            param[9].Value = Sup_Balance_After_Bill;


            dal.Sqlcomand("Sp_AddPurchaseBill", param);
            dal.Close();
        }

        public void Sp_AddPurchaseOpp(int Pro_Id, int PurchaseBill_Id, int PurchUnit_QTY,
                                     string PurchUnit_Price, int Bill_No, string UserName, 
                                     DateTime InsertDate, string Single_price, string WholeSale_price,string Pro_Price)
        {
            dal.Open();
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Pro_Id", SqlDbType.Int);
            param[0].Value = Pro_Id;

            param[1] = new SqlParameter("@PurchaseBill_Id", SqlDbType.Int);
            param[1].Value = PurchaseBill_Id;

            param[2] = new SqlParameter("@PurchUnit_QTY", SqlDbType.Int);
            param[2].Value = PurchUnit_QTY;

            param[3] = new SqlParameter("@PurchUnit_Price", SqlDbType.NVarChar, (50));
            param[3].Value = PurchUnit_Price;

            param[4] = new SqlParameter("@Bill_No", SqlDbType.Int);
            param[4].Value = Bill_No;

            param[5] = new SqlParameter("@UserName", SqlDbType.NVarChar, (50));
            param[5].Value = UserName;

            param[6] = new SqlParameter("@InsertDate", SqlDbType.DateTime);
            param[6].Value = InsertDate;

            param[7] = new SqlParameter("@Single_price", SqlDbType.NVarChar, (50));
            param[7].Value = Single_price;

            param[8] = new SqlParameter("@WholeSale_price", SqlDbType.NVarChar, (50));
            param[8].Value = WholeSale_price;

            param[9] = new SqlParameter("@Pro_Price", SqlDbType.NVarChar, (50));
            param[9].Value = Pro_Price;


            dal.Sqlcomand("Sp_AddPurchaseOpp", param);
            dal.Close();
        }
        public DataTable GetLastPurchBill_Id()
        {
            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("Sp_GetLastPurchBill_Id", null);
            dal.Close();
            return dt;
        }
        public DataTable CheckQTY(int Pro_Id, int Pro_Qty)
        {
            cls_dal DAL = new cls_dal();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@Pro_Id", SqlDbType.NVarChar, 50);
            param[0].Value = Pro_Id;

            param[1] = new SqlParameter("@Pro_Qty", SqlDbType.NVarChar, 50);
            param[1].Value = Pro_Qty;

            DAL.Open();
            DataTable dt = new DataTable();
            dt = DAL.SelectData("Sp_CheckQTY", param);
            return dt;
        }
        public DataTable get_purch_bill_by_id(int PurchaseBill_Id)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PurchaseBill_Id", SqlDbType.Int);
            param[0].Value = PurchaseBill_Id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_purch_bill_by_id", param);
            return dt;
        }

        public DataTable get_purch_Opp_by_purch_Bill_Id(int PurchaseBill_Id)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PurchaseBill_Id", SqlDbType.Int);
            param[0].Value = PurchaseBill_Id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_purch_Opp_by_purch_Bill_Id", param);
            return dt;
        }
        public DataTable get_purch_Bill_det_by_id(int PurchaseBill_Id)
        {
            //دالة البحث عن مجهز عن طريق ستوريد بروسيجر بنفس اسم الدالة

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@PurchaseBill_Id", SqlDbType.Int);
            param[0].Value = PurchaseBill_Id;

            dal.Open();
            DataTable dt = new DataTable();
            dt = dal.SelectData("sp_get_purch_Bill_det_by_id", param);
            return dt;
        }

    }
}
