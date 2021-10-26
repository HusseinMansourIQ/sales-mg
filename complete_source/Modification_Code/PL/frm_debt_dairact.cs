using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_Management_2.PL
{

    public partial class frm_debt_dairact : Form
    {
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        AutoCompleteStringCollection coll2 = new AutoCompleteStringCollection();
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;Integrated Security=True");
        SqlDataAdapter da;
        SqlDataAdapter da2;
        DataTable DT = new DataTable();
        DataTable DT2 = new DataTable();

        string Single_price;
        string Whole_Price;
        string x;
        int edit = 0;
        double new_balance = 0;
        double BalanceOnCostmer;
        public int cust_id;
        public int Produc_ID;
        public int no_save = 0;
        DAL.cls_dal dal = new DAL.cls_dal();
        BL.Cls_AutoFill autofill = new BL.Cls_AutoFill();
        BL.Cls_Sales sales = new BL.Cls_Sales();
        BL.Cls_Customeres cstmr = new BL.Cls_Customeres();
        BL.Cls_Products prod = new BL.Cls_Products();

        public frm_debt_dairact()
        {
            InitializeComponent();
            txt_BalanceAftercurentBill.LostFocus += new EventHandler(txt_BalanceAftercurentBill_LostFocus);


            Autofill_salerText();
        }
        private void txt_BalanceAftercurentBill_LostFocus(object sender, EventArgs e)
        {
            // do your stuff
           

        }

        private void btn_SaveOnly_Click(object sender, EventArgs e)
        {
            
            if (txt_CostmerName.Text == string.Empty || txt_CostmerName.Text == "") { MessageBox.Show("يجب ادخال اسم الزبون اولا "); }
            else if (richTextBox1.Text == string.Empty || richTextBox1.Text == "") { MessageBox.Show("يجب ادخال التفاصيل اولا "); }
            else if (txt_totalAfterDis.Text == string.Empty || txt_totalAfterDis.Text == "") { MessageBox.Show("يجب ادخال المجموع اولا "); }
            else if (Convert.ToDouble(txt_totalAfterDis.Text) < 250) { MessageBox.Show("لايمكن ان يكون المجموع اقل  من  250 دينار ");txt_totalAfterDis.Focus();txt_PaidMony.Clear(); }
            else if (txt_PaidMony.Text == string.Empty || txt_PaidMony.Text == "") { MessageBox.Show("يجب ادخال المبلغ الواصل اولا "); }
            else
            {
                try
                {    decimal rest_mony= (Convert.ToDecimal(txt_totalAfterDis.Text) - Convert.ToDecimal(txt_PaidMony.Text));
                    txt_RestMony.Text = rest_mony.ToString();
                  
                    if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                    new_balance = (Convert.ToDouble(txt_BalanceOnCostmer.Text) + Convert.ToDouble(txt_RestMony.Text)) ;
                    if (new_balance >= 0)
                    {
                        lbl_new_balance.Text = "مطلوب";
                        txt_BalanceAftercurentBill.Text = new_balance.ToString();
                    }
                    else
                    {
                        lbl_new_balance.Text = "يطلب";
                        txt_BalanceAftercurentBill.Text = Math.Abs(new_balance).ToString();

                    }
                    decimal PaidMony = Convert.ToDecimal(txt_PaidMony.Text);
                    //احضار اخر قائمة+1 
                    int max_SalesBill_Id = Convert.ToInt32(sales.GetLastSalesBill_Id().Rows[0][0]);
                   
                    DataTable dt_check = new DataTable();
                    dt_check= cstmr.check_Cust_name(txt_CostmerName.Text);
                   
                    if(dt_check.Rows.Count==1)
                    {
                        sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, "", cust_id, "",
                                      txt_totalAfterDis.Text, "", txt_PaidMony.Text,
                                     txt_RestMony.Text, Convert.ToDecimal(new_balance),
                                     Program.U_ArabicName, "بيع مباشر", richTextBox1.Text);

                        cstmr.update_balance(cust_id, rest_mony);
                        MessageBox.Show("تم حفظ القائمة بنجاح");
                        Clear_CustomerBoxes();
                    }
                    else if (dt_check.Rows.Count >1)
                    {
                        MessageBox.Show("يوجد اكثر من اسم ....يرجى كتابة الاسم بالكامل للمتابعة");
                    }
                    else if (dt_check.Rows.Count == 0)
                    {
                        txt_adress.Clear();
                        txt_phonNo.Clear();
                        if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                        cstmr.InsertCustomer("0", txt_CostmerName.Text, txt_phonNo.Text, txt_adress.Text, "0", txt_BalanceOnCostmer.Text, DateTime.Parse(dateTimePicker1.Text), "اضافة مباشرة");
                        cust_id = Convert.ToInt32(dal.SelectData("Sp_get_max_cust_id", null).Rows[0][0]);
                       
                        sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, "", cust_id, "",
                                      "", "", txt_PaidMony.Text,
                                     "", Convert.ToDecimal(new_balance),
                                     Program.U_ArabicName, "بيع مباشر", richTextBox1.Text);

                        cstmr.update_balance(cust_id, rest_mony);
                        MessageBox.Show(" هذا الزبون غير موجدود وتم اضافته الى قاعدة البيانات و حفظ القائمة بنجاح");
                        Clear_CustomerBoxes();
                    }

                    // Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
        }
        public void Autofill_salerText()
        {
            try
            {
                //دالة لتغيير خصائص تيكست بوكس للاملاء التلقائي
                txt_CostmerName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_CostmerName.AutoCompleteSource = AutoCompleteSource.CustomSource;
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
                    txt_CostmerName.AutoCompleteCustomSource = coll;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
        public void check_CostmerName()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;Integrated Security=True"))
                {
                    da = new SqlDataAdapter("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table where Cust_name='" + txt_CostmerName.Text + "'", connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    //if (txt_CostmerName.Text == string.Empty) { MessageBox.Show(" قم بادخال اسم المجهز رجاءا "); txt_CostmerName.Focus(); }

                    if (dt.Rows.Count > 0)
                    {
                        btn_SaveOnly.Focus();
                    }
                    else if (MessageBox.Show(" هذا الزبون غير موجود هل تريد اضافته الى قاعدة البانات مباشرتا ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                        //PL.frm_CostamerManage frm = new PL.frm_CostamerManage();
                        //frm.ShowDialog();
                        txt_adress.Clear();
                        txt_phonNo.Clear();
                        cstmr.InsertCustomer("0",txt_CostmerName.Text, txt_phonNo.Text, txt_adress.Text, "0", txt_BalanceAftercurentBill.Text, DateTime.Parse(dateTimePicker1.Text), "اضافة مباشرة");
                    }
                    else
                    {
                        txt_adress.Clear();
                        txt_phonNo.Clear();
                        // txt_BalanceForCostmer.Text = "0";
                        txt_BalanceOnCostmer.Text = "0";
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        public void Clear_CustomerBoxes()
        {
            try
            {
                txt_adress.Clear();
                txt_phonNo.Clear();
                txt_CostmerName.Clear();
                txt_CostmerName.Focus();
                txt_BalanceOnCostmer.Text = "0";
                richTextBox1.Clear();
                Autofill_salerText();
                txt_PaidMony.Text = "0";
                txt_totalAfterDis.Clear();
                txt_RestMony.Text = "0";
                txt_BalanceAftercurentBill.Text = "0";
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
        public void Get_CostumerDataTotextBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;Integrated Security=True"))
                {
                    SqlCommand command = new SqlCommand("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table where Cust_name='" + txt_CostmerName.Text + "'", connection);
                    connection.Open();

                    SqlDataReader read = command.ExecuteReader();


                    while (read.Read())
                    {
                        if (read.HasRows)
                        {
                            cust_id = Convert.ToInt32(read["Cust_Id"]);
                            txt_phonNo.Text = (read["Cust_Tel"].ToString());
                            txt_adress.Text = (read["Cust_addres"].ToString());
                            BalanceOnCostmer = Convert.ToDouble((read["BalanceOnCostmer"]));

                            // txt_BalanceForCostmer.Text = (read["BalanceToCostmer"].ToString());
                            if (Convert.ToInt32(read["BalanceOnCostmer"]) >= 0)
                            {
                                lbl_balance.Text = "مطلوب";
                                txt_BalanceOnCostmer.Text = BalanceOnCostmer.ToString();
                            }
                            else
                            {
                                lbl_balance.Text = "يطلب";
                                txt_BalanceOnCostmer.Text = Math.Abs(BalanceOnCostmer).ToString();

                                // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                            }
                        }

                        else
                        {
                            cust_id = 0;
                            txt_CostmerName.Clear();
                            txt_phonNo.Clear();
                            txt_adress.Clear();
                        }

                    }
                    read.Close();
                    // connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txt_CostmerName_TextChanged(object sender, EventArgs e)
        {
            Get_CostumerDataTotextBox();
        }

        private void txt_totalAfterDis_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) & txt_totalAfterDis.Text != string.Empty)
            {
                txt_PaidMony.Focus();
                try
                {
                    ///txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                    if (txt_totalAfterDis.Text == " ")
                    { txt_totalAfterDis.Text = "0"; }
                  
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }

        }

        private void txt_totalAfterDis_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_PaidMony_TextChanged(object sender, EventArgs e)
        {
          

        }

        private void txt_PaidMony_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) & txt_PaidMony.Text != string.Empty & txt_totalAfterDis.Text!=string.Empty)
            {
                try
                {
                    txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                    if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                    new_balance = (Convert.ToDouble(txt_BalanceOnCostmer.Text) + Convert.ToDouble(txt_RestMony.Text));
                    txt_BalanceAftercurentBill.Text = new_balance.ToString();
                   
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                btn_SaveOnly.Focus();
            }
        }

        private void txt_totalAfterDis_MouseLeave(object sender, EventArgs e)
        {               
        }

        private void txt_PaidMony_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_PaidMony_MouseLeave(object sender, EventArgs e)
        {
        }

        private void txt_PaidMony_MouseDown(object sender, MouseEventArgs e)
        {
           

        }

        private void txt_PaidMony_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ///txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
            //    if (txt_totalAfterDis.Text == " ")
            //    { txt_totalAfterDis.Text = "0"; }
            //    if (Convert.ToDouble(txt_totalAfterDis.Text) <= 250)
            //    {
            //        MessageBox.Show("لايمكن ان يكون المجموع اقل  او يساوي  250 دينار ");
            //    }
            //    else { txt_PaidMony.Focus(); }
            //}
            //catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void txt_PaidMony_Leave(object sender, EventArgs e)
        {
            if(txt_PaidMony.Text !=string.Empty & txt_totalAfterDis.Text !=string.Empty)
            {

              
              try
              {
                txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                new_balance = (Convert.ToDouble(txt_BalanceOnCostmer.Text) + Convert.ToDouble(txt_RestMony.Text));
                txt_BalanceAftercurentBill.Text = new_balance.ToString();
                btn_SaveOnly.Focus();
                    txt_PaidMony.Text = string.Format("{0:#,##0}", double.Parse(txt_PaidMony.Text));
                }
              catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txt_totalAfterDis_Leave(object sender, EventArgs e)
        {
            try
            {
                ///txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                if (txt_totalAfterDis.Text == " ")
                { txt_totalAfterDis.Text = "0"; }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void txt_CostmerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                richTextBox1.Focus();
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_totalAfterDis.Focus();
            }
        }

        private void frm_debt_dairact_Load(object sender, EventArgs e)
        {
            txt_CostmerName.Focus();
        }

        private void txt_totalAfterDis_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void txt_totalAfterDis_ModifiedChanged(object sender, EventArgs e)
        {
        }

        private void txt_BalanceOnCostmer_TextChanged(object sender, EventArgs e)
        {
            txt_BalanceOnCostmer.Text = string.Format("{0:#,##0}", double.Parse(txt_BalanceOnCostmer.Text));

        }

        private void txt_RestMony_TextChanged(object sender, EventArgs e)
        {
            txt_RestMony.Text = string.Format("{0:#,##0}", double.Parse(txt_RestMony.Text));
        }

        private void txt_BalanceAftercurentBill_TextChanged(object sender, EventArgs e)
        {
            txt_BalanceAftercurentBill.Text = string.Format("{0:#,##0}", double.Parse(txt_BalanceAftercurentBill.Text));
        }
    }
}
