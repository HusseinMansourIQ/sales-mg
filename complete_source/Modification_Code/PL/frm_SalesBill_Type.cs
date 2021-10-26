using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sales_Management_2.PL
{
    public partial class frm_SalesBill_Type : Form
    {
        DAL.cls_dal dal = new DAL.cls_dal();
        BL.Cls_Customeres cstmr = new BL.Cls_Customeres();
        // PL.sellingBill sales = new sellingBill();
        DataTable DT = new DataTable();
        string n;
        public int type = 0;
        public int save = 0;
        
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        SqlDataAdapter da;
        string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;" + "Integrated Security=True");

        public frm_SalesBill_Type()
        {
            InitializeComponent();
            Autofill_salerText();
            rdbn_Mufrad.Checked = true;
        }
        public void Get_CostumerDataTotextBox()
        {
            try
            {
                string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + domain + "selapp.mdf;" + "Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table where Cust_name='" + txt_NameFromDataBase.Text + "'", connection);
                connection.Open();

                SqlDataReader read = command.ExecuteReader();


                while (read.Read())
                {
                    if (read.HasRows)
                    {
                        sellingBill.getsellingBillForm.cust_id = Convert.ToInt32(read["Cust_Id"]);
                        sellingBill.getsellingBillForm.txt_CostmerName.Text = (read["Cust_name"].ToString());
                        sellingBill.getsellingBillForm.txt_phonNo.Text = (read["Cust_Tel"].ToString());
                        sellingBill.getsellingBillForm.txt_adress.Text = (read["Cust_addres"].ToString());
                      //  sellingBill.getsellingBillForm.txt_BalanceForCostmer.Text = (read["BalanceToCostmer"].ToString());
                        sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Text = (read["BalanceOnCostmer"].ToString());
                    }

                    else
                    {
                        sellingBill.getsellingBillForm.cust_id = 0;
                        sellingBill.getsellingBillForm.txt_CostmerName.Text = null;
                        sellingBill.getsellingBillForm.txt_phonNo.Text = null;
                        sellingBill.getsellingBillForm.txt_adress.Text = null;
                       // sellingBill.getsellingBillForm.txt_BalanceForCostmer.Text = null;
                        sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Text = null;
                            sellingBill.getsellingBillForm.txt_CostmerName.Enabled = true;
                            sellingBill.getsellingBillForm.txt_phonNo.ReadOnly = false;
                            sellingBill.getsellingBillForm.txt_adress.ReadOnly = false;
                            sellingBill.getsellingBillForm.txt_phonNo.Enabled = true;
                            sellingBill.getsellingBillForm.txt_adress.Enabled = true;

                        }

                    }
                read.Close();
                // connection.Close();
            }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
        public void Autofill_salerText()
        {
            try
            { //دالة لتغيير خصائص تيكست بوكس للاملاء التلقائي
            txt_NameFromDataBase.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_NameFromDataBase.AutoCompleteSource = AutoCompleteSource.CustomSource;
            da = new SqlDataAdapter("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table order by Cust_name asc", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    string name = dt.Rows[j]["Cust_name"].ToString();
                    coll.Add(name);

                }
                txt_NameFromDataBase.AutoCompleteCustomSource = coll;
            }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           
        }
        public void check_CostmerName()
        {
            try
            {
                string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + domain + "selapp.mdf;" + "Integrated Security=True"))
            {
                da = new SqlDataAdapter("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table where Cust_name='" + txt_NameFromDataBase.Text + "'", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //if (txt_CostmerName.Text == string.Empty) { MessageBox.Show(" قم بادخال اسم المجهز رجاءا "); txt_CostmerName.Focus(); }

                if (dt.Rows.Count > 0)
                {
                    btn_continue.Focus();
                }
                else if (MessageBox.Show("هذا الزبون غير موجود هل تريج اضافته الى قاعدة البانات ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    PL.frm_CostamerManage frm = new PL.frm_CostamerManage();
                    frm.txt_CustomerName.Text = txt_NameFromDataBase.Text;
                        sellingBill.getsellingBillForm.Text = txt_NameFromDataBase.Text;
                        frm.ShowDialog();
                        sellingBill.getsellingBillForm.txt_CostmerName.Enabled = true;
                        sellingBill.getsellingBillForm.txt_phonNo.ReadOnly = false;
                        sellingBill.getsellingBillForm.txt_adress.ReadOnly = false;
                        sellingBill.getsellingBillForm.txt_phonNo.Enabled = true;
                        sellingBill.getsellingBillForm.txt_adress.Enabled = true;

                        // txt_NameFromDataBase.Clear();
                        // txt_phonNo.Clear();
                    }
                    else
                {

                    rdbn_NoSave.Checked = true;
                    sellingBill.getsellingBillForm.txt_CostmerName.Text = txt_NameFromDataBase.Text;
                        sellingBill.getsellingBillForm.Text = txt_NameFromDataBase.Text;
                        sellingBill.getsellingBillForm.txt_CostmerName.ReadOnly = true;
                    sellingBill.getsellingBillForm.txt_phonNo.Visible = false;
                    sellingBill.getsellingBillForm.txt_adress.Visible = false;
                    //sellingBill.getsellingBillForm.txt_BalanceForCostmer.Visible = false;
                    sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Visible = false;
                    sellingBill.getsellingBillForm.txt_prodName.Focus();
                        sellingBill.getsellingBillForm.txt_CostmerName.Enabled = true;
                        sellingBill.getsellingBillForm.txt_phonNo.ReadOnly = false;
                        sellingBill.getsellingBillForm.txt_adress.ReadOnly = false;
                        sellingBill.getsellingBillForm.txt_phonNo.Enabled = true;
                        sellingBill.getsellingBillForm.txt_adress.Enabled = true;


                        if (type == 0) { sellingBill.getsellingBillForm.rb_mufrad.Checked = true; }
                    else { sellingBill.getsellingBillForm.rb_jumla.Checked = true; }



                    this.Close();





                }
            }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }



        private void frm_SalesBill_Type_Load(object sender, EventArgs e)
        {
            rdbn_Mufrad.Checked = true;
            rbn_NoName.Checked = true;
            btn_continue.Focus();
        }

        private void rdbn_Mufrad_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbn_Mufrad.Checked==true)
            type = 0;
        }

        private void rdbn_Jumla_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbn_Jumla.Checked==true)
            type = 1;
        }

        private void rbn_NoName_CheckedChanged(object sender, EventArgs e)
        {
            if(rbn_NoName.Checked==true)
            {
                save = 0;
                n = txt_NameFromDataBase.Text;

            }
            //txt_NameFromDataBase.Clear();
            //txt_NoSave.Clear();
        }

        private void rdbn_NoSave_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbn_NoSave.Checked == true)
            {
                save = 1;
               
            }
               
           

        }

        private void rdbn_NameFromDataBase_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbn_NameFromDataBase.Checked)
            {
            save = 2;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            if (save == 0)
            {


                if (type == 0) { sellingBill.getsellingBillForm.rb_mufrad.Checked = true; }
                if (type == 1) { sellingBill.getsellingBillForm.rb_jumla.Checked = true; }

                sellingBill.getsellingBillForm.txt_CostmerName.Text = "بلا";
                    sellingBill.getsellingBillForm.cust_id = 0;
                sellingBill.getsellingBillForm.txt_CostmerName.ReadOnly = true;
                sellingBill.getsellingBillForm.txt_phonNo.Visible = false;
                sellingBill.getsellingBillForm.txt_adress.Visible = false;
               // sellingBill.getsellingBillForm.txt_BalanceForCostmer.Visible = false;
                sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Visible = false;
                sellingBill.getsellingBillForm.txt_BalanceAftercurentBill.Text = "0";
                sellingBill.getsellingBillForm.txt_RestMony.Text = "0";
                sellingBill.getsellingBillForm.txt_PaidMony.Clear();
                sellingBill.getsellingBillForm.Text="بلا";

                    this.Close();
            }

            else if (save == 1)
            {
                DataTable dt_cstmr_check = new DataTable();
                dt_cstmr_check = cstmr.check_Cust_name(txt_NoSave.Text);
                

                if (txt_NoSave.Text == string.Empty) { MessageBox.Show("الرجاء ادخال اسم المشتري "); }
                else if (dt_cstmr_check.Rows.Count == 0)
                {
                    sellingBill.getsellingBillForm.no_save = 1;

                    sellingBill.getsellingBillForm.txt_CostmerName.Text = txt_NoSave.Text;
                        sellingBill.getsellingBillForm.Text = txt_NoSave.Text;
                        sellingBill.getsellingBillForm.txt_CostmerName.ReadOnly = false;
                    sellingBill.getsellingBillForm.txt_phonNo.Visible = true;
                    sellingBill.getsellingBillForm.txt_adress.Visible = true;
                    //sellingBill.getsellingBillForm.txt_CostmerName.ReadOnly = false;
                    sellingBill.getsellingBillForm.txt_phonNo.Clear() ;
                    sellingBill.getsellingBillForm.txt_adress.Clear();
                    // sellingBill.getsellingBillForm.txt_BalanceForCostmer.Visible = false;
                    sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Visible = true;
                    sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Text = "0";
                    sellingBill.getsellingBillForm.txt_BalanceAftercurentBill.Text = "0";
                    sellingBill.getsellingBillForm.txt_RestMony.Text = "0";
                    sellingBill.getsellingBillForm.txt_PaidMony.Clear();
                        sellingBill.getsellingBillForm.txt_CostmerName.Enabled = true;
                        sellingBill.getsellingBillForm.txt_phonNo.ReadOnly = false;
                        sellingBill.getsellingBillForm.txt_adress.ReadOnly = false;
                        sellingBill.getsellingBillForm.txt_phonNo.Enabled = true;
                        sellingBill.getsellingBillForm.txt_adress.Enabled = true;

                        // cstmr.InsertCustomer(null, txt_NoSave.Text, null, null, "0", "0", System.DateTime.Now, null);
                        if (type == 0) { sellingBill.getsellingBillForm.rb_mufrad.Checked = true; }
                    if (type == 1) { sellingBill.getsellingBillForm.rb_jumla.Checked = true; }

                    this.Close();

                }
                else if (dt_cstmr_check.Rows.Count==1)
                {
                  if (MessageBox.Show("هذا الزبون  موجود مسبقا هل تريد المتابعة ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                            sellingBill.getsellingBillForm.cust_id =Convert.ToInt32( dt_cstmr_check.Rows[0]["Cust_Id"]);
                            sellingBill.getsellingBillForm.txt_CostmerName.Text = dt_cstmr_check.Rows[0]["Cust_name"].ToString();
                            sellingBill.getsellingBillForm.Text = sellingBill.getsellingBillForm.txt_CostmerName.Text;
                            sellingBill.getsellingBillForm.txt_CostmerName.ReadOnly = true;
                        sellingBill.getsellingBillForm.txt_phonNo.Text = dt_cstmr_check.Rows[0]["Cust_Tel"].ToString();
                        sellingBill.getsellingBillForm.txt_adress.Text= dt_cstmr_check.Rows[0]["Cust_addres"].ToString();
                       // sellingBill.getsellingBillForm.txt_BalanceForCostmer.Visible = false;
                        sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Text= dt_cstmr_check.Rows[0]["BalanceOnCostmer"].ToString();
                        sellingBill.getsellingBillForm.txt_BalanceAftercurentBill.Text = "0";
                        sellingBill.getsellingBillForm.txt_RestMony.Text = "0";
                        sellingBill.getsellingBillForm.txt_PaidMony.Clear();
                        sellingBill.getsellingBillForm.txt_phonNo.Visible = true;
                        sellingBill.getsellingBillForm.txt_adress.Visible = true;
                            sellingBill.getsellingBillForm.txt_CostmerName.Enabled = true;
                            sellingBill.getsellingBillForm.txt_phonNo.ReadOnly = false;
                            sellingBill.getsellingBillForm.txt_adress.ReadOnly = false;
                            sellingBill.getsellingBillForm.txt_phonNo.Enabled = true;
                            sellingBill.getsellingBillForm.txt_adress.Enabled = true;


                            if (type == 0) { sellingBill.getsellingBillForm.rb_mufrad.Checked = true; }
                        if (type == 1) { sellingBill.getsellingBillForm.rb_jumla.Checked = true; }

                        sellingBill.getsellingBillForm.no_save = 0;
                        this.Close();

                  }
                }
                else if (dt_cstmr_check.Rows.Count >1)

                {
                    MessageBox.Show("الرجاء كتابة اسم الزبون بالكامل وذلك لوجود اكثر من زبون بهذا الاسم");
                    //sellingBill.getsellingBillForm.txt_CostmerName.Text = txt_NoSave.Text;
                    //sellingBill.getsellingBillForm.txt_CostmerName.ReadOnly = true;
                    //sellingBill.getsellingBillForm.txt_phonNo.Visible = false;
                    //sellingBill.getsellingBillForm.txt_adress.Visible = false;
                    //sellingBill.getsellingBillForm.txt_BalanceForCostmer.Visible = false;
                    //sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Visible = false;

                    //this.Close();

                }
            }
            else if (save==2)
            {
                check_CostmerName();
                if (type == 0) { sellingBill.getsellingBillForm.rb_mufrad.Checked = true; }
                if (type == 1) { sellingBill.getsellingBillForm.rb_jumla.Checked = true; }

                sellingBill.getsellingBillForm.txt_phonNo.Visible = true;
                sellingBill.getsellingBillForm.txt_adress.Visible = true;
               // sellingBill.getsellingBillForm.txt_BalanceForCostmer.Visible = true;
                sellingBill.getsellingBillForm.txt_BalanceOnCostmer.Visible = true;
             
                sellingBill.getsellingBillForm.txt_CostmerName.Text = txt_NameFromDataBase.Text;
                    sellingBill.getsellingBillForm.Text = txt_NameFromDataBase.Text;
                    sellingBill.getsellingBillForm.txt_CostmerName.ReadOnly = true;
                sellingBill.getsellingBillForm.txt_phonNo.ReadOnly = true;
                sellingBill.getsellingBillForm.txt_adress.ReadOnly = true;
                sellingBill.getsellingBillForm.txt_BalanceOnCostmer.ReadOnly = true;
                sellingBill.getsellingBillForm.txt_BalanceAftercurentBill.Text = "0";
                sellingBill.getsellingBillForm.txt_RestMony.Text = "0";
                sellingBill.getsellingBillForm.txt_PaidMony.Clear();
                    sellingBill.getsellingBillForm.txt_CostmerName.Enabled = true;
                    sellingBill.getsellingBillForm.txt_phonNo.ReadOnly = false;
                    sellingBill.getsellingBillForm.txt_adress.ReadOnly = false;
                    sellingBill.getsellingBillForm.txt_phonNo.Enabled = true;
                    sellingBill.getsellingBillForm.txt_adress.Enabled = true;

                   // sellingBill.getsellingBillForm.no_save = 0;

                    this.Close();

                //}
            }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txt_NoSave_TextChanged(object sender, EventArgs e)
        {
            rdbn_NoSave.Checked = true;
            txt_NoSave.Focus();
            //  n = txt_NameFromDataBase.Text;
           // txt_NameFromDataBase.Clear();
           // rdbn_NameFromDataBase.Checked = false;
        }

        private void txt_NameFromDataBase_TextChanged(object sender, EventArgs e)
        {


            rdbn_NameFromDataBase.Checked = true;

            txt_NameFromDataBase.Focus();
            Get_CostumerDataTotextBox();
            //  txt_NoSave.Clear();


        }

        private void txt_NameFromDataBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                Get_CostumerDataTotextBox();

                //
            }

            else
                check_CostmerName();

        }

        private void txt_NoSave_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_NameFromDataBase_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //SqlParameter[] param = new SqlParameter[1];
            //param[0] = new SqlParameter("@SalesBill_Id", SqlDbType.NVarChar, (50));
            //param[0].Value = txt_Bill_ID.Text;

            //dal.Open();

            //DT = dal.SelectData("Sp_EditPurchaseBill", param);

            //PL.sellingBill.getsellingBillForm.dgv_sellingPill.DataSource = DT;

            //Close();

        }

        private void txt_NoSave_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txt_NoSave_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
