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
    public partial class frm_CostamerManage : Form
    {
        BL.Cls_Customeres cost = new BL.Cls_Customeres();
        BL.Cls_Sales sales = new BL.Cls_Sales();

        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        AutoCompleteStringCollection coll2 = new AutoCompleteStringCollection();

        int cust_id;
        int r_index;
        public frm_CostamerManage()
        {
            InitializeComponent();
            Autofill_salerText();
            Autofill_salerText2();


            try
            {
            //this.dgv_Costomeres.DataSource = cost.SearchForCustomer("");//استدعاء دالى البحث عن طريق تيكست سيرجدوت جينج

                dgv_Costomeres.DataSource = cost.FillCustomeresDGV();
                txt_series.Text = (this.dgv_Costomeres.RowCount + 1).ToString();
                lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();
                dgv1_design();
               

            }

            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }



        }
        public void CleareBoxes()
        {
            try
            {
             txt_CustomerName.Clear();
            txt_CustomerPhon.Clear();
            txt_CustomerAdress.Clear();
            txt_BalanceOnCustomer.Text = "0";
           // txt_BalanceToCustomer.Text = "0";
            txt_Note.Clear();
            txt_CustomerName.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_CostamerManage_Load(object sender, EventArgs e)
        {
            add_botton_1_to_dgv_es1_des();
            add_botton_2_to_dgv_es1_des();

            btn_Insert.Enabled = true;
            btn_cancel_update.Enabled = false;
            btn_save_update.Enabled = false;
            btn_print_all.Enabled = true;
            dgv_arraing();

            if (dgv_Costomeres.Rows.Count > 0)
            {
                lbl_total_dept.Text = (from DataGridViewRow row in dgv_Costomeres.Rows
                                       where row.Cells[5].FormattedValue.ToString() != string.Empty
                                       select Convert.ToDouble(row.Cells[5].FormattedValue)).Sum().ToString();
                lbl_total_dept.Text = string.Format("{0:#,##0}", double.Parse(lbl_total_dept.Text));
            }

        }
        private void add_botton_1_to_dgv_es1_des()
        {
            //اظافة column button الى الداتا كردفيو
            try
            {
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                {
                    button.Name = "button";
                    button.HeaderText = "تعديل";
                    button.Text = "تعديل";
                    button.UseColumnTextForButtonValue = true; //dont forget this line
                    this.dgv_Costomeres.Columns.Insert(0, button);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }
        private void add_botton_2_to_dgv_es1_des()
        {
            //اظافة column button الى الداتا كردفيو
            try
            {
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                {
                    button.Name = "button";
                    button.HeaderText = "تفاصيل";
                    button.Text = "تفاصيل";
                    button.UseColumnTextForButtonValue = true; //dont forget this line
                    this.dgv_Costomeres.Columns.Insert(1, button);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {lbl_clock.Text = DateTime.Now.ToString("hh:mm:ss tt");
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";


            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_CustomerName.Text != string.Empty)
            {
                txt_CustomerPhon.Focus();
            }

        }

        private void txt_CostomerPhon_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter)
            {
                txt_CustomerAdress.Focus();
            }

        }

        private void txt_CostomerAdress_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter )
            {
                txt_BalanceOnCustomer.Focus();
            }

        }

        private void txt_Note_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter)
            {
                btn_Insert.Focus();
            }

        }

        private void txt_CostomerPhon_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (txt_CustomerName.Text == string.Empty)
            { MessageBox.Show("الرجاء ادخال اسم الزبون"); }
            else
            {
                try
                {
                    if(cost.check_Cust_name(txt_CustomerName.Text).Rows.Count>0)
                    {
                        MessageBox.Show("اسم الزبون موجود موجود مسبقا ....الرجاء كتابة اسم الزبون بالكامل");
                    }
                    else
                    { 
                        cost.InsertCustomer(txt_series.Text, txt_CustomerName.Text, txt_CustomerPhon.Text, txt_CustomerAdress.Text, "0", txt_BalanceOnCustomer.Text, DateTime.Parse(dateTimePicker1.Text), txt_Note.Text);
                        MessageBox.Show("تمت الاضافة بنجاح");
                        dgv_Costomeres.DataSource = cost.FillCustomeresDGV();
                        txt_series.Text = (this.dgv_Costomeres.RowCount + 1).ToString();
                        lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();


                        CleareBoxes();
                  
                    btn_Insert.Enabled = true;
                    btn_cancel_update.Enabled = false;
                    btn_save_update.Enabled = false;
                    btn_print_all.Enabled = false;
                        Autofill_salerText();
                    }
               
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

            }
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dgv_Costomeres_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            if (dgv_Costomeres.Rows.Count>0 & e.ColumnIndex==0)
            {
                try
                {
                    cust_id = Convert.ToInt32(this.dgv_Costomeres.Rows[e.RowIndex].Cells[2].Value);
                    // MessageBox.Show(cust_id.ToString());
                    r_index = e.RowIndex;

                    txt_series.Text=  this.dgv_Costomeres.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txt_CustomerName.Text = this.dgv_Costomeres.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txt_CustomerPhon.Text = this.dgv_Costomeres.Rows[e.RowIndex].Cells[6].Value.ToString();

                    txt_CustomerAdress.Text = this.dgv_Costomeres.Rows[e.RowIndex].Cells[7].Value.ToString();

                   // txt_BalanceToCustomer.Text = this.dgv_Costomeres.Rows[e.RowIndex].Cells[6].Value.ToString();

                    txt_BalanceOnCustomer.Text = this.dgv_Costomeres.Rows[e.RowIndex].Cells[5].Value.ToString();

                   // txt_series.Text = this.dgv_Costomeres.Rows[e.RowIndex].Cells[8].Value.ToString();

                    txt_Note.Text = this.dgv_Costomeres.Rows[e.RowIndex].Cells[10].Value.ToString();

                    //this.dgv_Costomeres.Rows[e.RowIndex].Cells[10].Value.ToString();

                    //this.dgv_Costomeres.Rows[e.RowIndex].Cells[11].Value.ToString();
                   
               
                    btn_Insert.Enabled = false;
                    btn_cancel_update.Enabled = true;
                    btn_save_update.Enabled = true;
                    btn_print_all.Enabled = false;

                  



                }
                catch (Exception ex) { MessageBox.Show("الرجاء الاختيار من داخل القائمة"); }
           

            }
            else if (dgv_Costomeres.Rows.Count > 0 & e.ColumnIndex == 1)
            {
                try
                {
                    string cust_name = this.dgv_Costomeres.Rows[e.RowIndex].Cells[4].Value.ToString() ;

                    this.Cursor = Cursors.WaitCursor;
                    print.rpt_user_history report = new print.rpt_user_history();
                    report.SetDataSource(sales.get_bill_and_opp_by_Cust_name(cust_name));
                    print.frm_print frm3 = new print.frm_print();
                    frm3.crystalReportViewer1.ReportSource = report;
                    frm3.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_22"); }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد حفظ التعديل ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    cost.update_customer(cust_id, txt_CustomerName.Text, txt_CustomerPhon.Text, txt_CustomerAdress.Text, "0", txt_BalanceOnCustomer.Text,Program.U_ArabicName, txt_Note.Text);
                    MessageBox.Show("تم التعديل بنجاح");
                    btn_Insert.Enabled = true;
                    dgv_Costomeres.DataSource = cost.FillCustomeresDGV();
                    
                  
                    btn_Insert.Enabled = true;
                    btn_cancel_update.Enabled = false;
                    btn_save_update.Enabled = false;
                    btn_print_all.Enabled = false;

                    CleareBoxes();
                    Autofill_salerText();
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

        }

        private void dgv_Costomeres_Click(object sender, EventArgs e)
        {

        }

        private void dgv_Costomeres_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //if (dgv_Costomeres.Rows.Count > 0 )
            //{
               

            //}

        }

        private void dgv_Costomeres_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (dgv_Costomeres.Rows.Count > 0)
            {
                try
                {
                cust_id = Convert.ToInt32(this.dgv_Costomeres.Rows[e.RowIndex].Cells[0].Value);
               // MessageBox.Show(cust_id.ToString());

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txt_Search.Text == "" || txt_Search.Text == string.Empty)
            {
                MessageBox.Show("الرجاء ادخال الاسم اولا");
                // txt_CostmerName.Clear();
                lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();
                txt_Search.Focus();
            }
            else
            {
                try
                {
                    DataTable dt_cust = new DataTable();
                    dt_cust = cost.SearchForCustomer(txt_Search.Text);
                    if (dt_cust.Rows.Count == 0)
                    {
                        MessageBox.Show(" ....يرجى التاكد من البحث لطفا");
                        lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();
                        txt_Search.Focus();
                        Autofill_salerText();
                        dgv_arraing();
                        lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();

                    }
                    else
                    {
                        this.dgv_Costomeres.DataSource = dt_cust;
                        // txt_CostmerName.Clear();
                        
                        lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }

        }
        public bool isEnter = false;
        private void txt_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                // Get_CostumerDataTotextBox();


                isEnter = false;

                // MessageBox.Show(isEnter.ToString());
            }

            else
            {
                if (txt_Search.Text == "" || txt_Search.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء ادخال الاسم اولا");
                    // txt_CostmerName.Clear();
                    lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();
                    txt_Search.Focus();
                }
                else
                {
                    try
                    {
                        DataTable dt_cust = new DataTable();
                        dt_cust = cost.SearchForCustomer(txt_Search.Text);
                        if (dt_cust.Rows.Count == 0)
                        {
                            MessageBox.Show(" ....يرجى التاكد من البحث لطفا");
                            lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();
                            txt_Search.Focus();
                           // Autofill_salerText();
                        }
                        else
                        {
                            this.dgv_Costomeres.DataSource = dt_cust;
                            // txt_CostmerName.Clear();
                           // Autofill_salerText();
                            lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();

                          //  cust_id = Convert.ToInt32(dt_cust.Rows[0][0]);

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }

            }

                isEnter = true;
         }
        public void Autofill_salerText()
        {

            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;Integrated Security=True");
                SqlDataAdapter da = new SqlDataAdapter();
                //دالة لتغيير خصائص تيكست بوكس للاملاء التلقائي
                txt_Search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_Search.AutoCompleteSource = AutoCompleteSource.CustomSource;
                da = new SqlDataAdapter("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table order by Cust_name asc", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {

                        string name = dt.Rows[j]["Cust_name"].ToString();
                       // cust_id =Convert.ToInt32( dt.Rows[j]["Cust_Id"].ToString());
                        coll.Add(name);

                    }
                    txt_Search.AutoCompleteCustomSource = coll;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
        public void Autofill_salerText2()
        {

            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;Integrated Security=True");
                SqlDataAdapter da = new SqlDataAdapter();
                //دالة لتغيير خصائص تيكست بوكس للاملاء التلقائي
                txt_delete_cust.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_delete_cust.AutoCompleteSource = AutoCompleteSource.CustomSource;
                da = new SqlDataAdapter("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table order by Cust_name asc", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {

                        string name = dt.Rows[j]["Cust_name"].ToString();
                        // cust_id =Convert.ToInt32( dt.Rows[j]["Cust_Id"].ToString());
                        coll2.Add(name);

                    }
                    txt_delete_cust.AutoCompleteCustomSource = coll2;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }


        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt_cust = new DataTable();
                dt_cust = cost.FillCustomeresDGV();
               
                    this.dgv_Costomeres.DataSource = dt_cust;
                    txt_Search.Clear();

                    lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();
                    Autofill_salerText();

                //object sumObject;
                // sumObject = dt_cust.Compute(su, string.Empty);
                ////////  حساب مجموع الديون
                if (dgv_Costomeres.Rows.Count > 0)
                {
                    lbl_total_dept.Text = (from DataGridViewRow row in dgv_Costomeres.Rows
                                           where row.Cells[5].FormattedValue.ToString() != string.Empty
                                           select Convert.ToDouble(row.Cells[5].FormattedValue)).Sum().ToString();
                    lbl_total_dept.Text = string.Format("{0:#,##0}", double.Parse(lbl_total_dept.Text));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void add_new_Click(object sender, EventArgs e)
        {
            //txt_CustomerName.Clear();
            //txt_CustomerAdress.Clear(); 
            //txt_CustomerPhon.Clear();
            //txt_Note.Clear();
            //txt_BalanceOnCustomer.Text="0";

            //btn_Insert.Enabled = true;
            //btn_cancel_update.Enabled = false;
            //btn_save_update.Enabled = false;
            //btn_deledt_cust.Enabled = false;


        }

        private void btn_cancel_update_Click(object sender, EventArgs e)
        {
            CleareBoxes();
            Autofill_salerText();
            btn_Insert.Enabled = true;
          
            btn_cancel_update.Enabled = false;
            btn_save_update.Enabled = false;
            btn_print_all.Enabled = false;
        }

        private void txt_BalanceOnCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter )
            {
              // SendKeys.Send("{Tab}");//تحويل مفتاح انتر الى مفتاح تاب (عدم اضافة سطر جديد عند الضغط على مفتاح انتر)
              txt_Note.Focus();
            }
               
        }
        private void dgv_arraing()
        {
            if (dgv_Costomeres.Rows.Count > 0)
            {
                this.dgv_Costomeres.Columns[2].Visible = false;//id
               // this.dgv_Costomeres.Columns[8].Visible = false;insrtt date
                this.dgv_Costomeres.Columns[0].Width = 60; 
                this.dgv_Costomeres.Columns[1].Width = 60;
                this.dgv_Costomeres.Columns[3].Width = 60; // تسلسل
                this.dgv_Costomeres.Columns[4].Width = 200;// الاسم
                this.dgv_Costomeres.Columns[5].Width = 100; //الحساب
                this.dgv_Costomeres.Columns[6].Width = 140;// tel

            }
            
        }
        private void dgv1_design()
        {
            try
            {
                dgv_Costomeres.BorderStyle = BorderStyle.FixedSingle;
                dgv_Costomeres.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_Costomeres.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgv_Costomeres.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_Costomeres.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_Costomeres.BackgroundColor = Color.White;

                dgv_Costomeres.EnableHeadersVisualStyles = false;
                dgv_Costomeres.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgv_Costomeres.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_Costomeres.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            }

            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

        }

        private void txt_BalanceOnCustomer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_BalanceOnCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_print_all_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                print.rpt_print_all_cust report = new print.rpt_print_all_cust();
                report.SetDataSource(cost.FillCustomeresDGV());
                print.frm_print frm3 = new print.frm_print();
                frm3.crystalReportViewer1.ReportSource = report;
                frm3.ShowDialog();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void btn_delet_cust_Click(object sender, EventArgs e)
        {
            if (txt_delete_cust.Text == string.Empty || txt_delete_cust.Text == "" || txt_delete_cust.Text == " ")
            {
                MessageBox.Show("يجب اختيار الزبون المراد حذفه  ");
            }
            else
            {
                try
                {
                    DataTable dtcust = new DataTable();
                    dtcust = cost.check_Cust_name(txt_delete_cust.Text);



                    if (dtcust.Rows.Count < 1)
                    {
                        MessageBox.Show("هذا الزبون غير موجود ....يرجى التاكد من الاسم ");
                    }
                    else if (MessageBox.Show("هل تريد حذف  الزبون  " + txt_delete_cust.Text, "تنــبــيــه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)

                    {
                        int cust_id = Convert.ToInt32(dtcust.Rows[0][0]);
                        cost.delete_cust(Convert.ToInt32(cust_id));
                       
                        dgv_Costomeres.DataSource = cost.FillCustomeresDGV();
                        lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();
                        dgv1_design();
                        MessageBox.Show("  تم الحذف بنجاح ");
                    }
                }
                catch (Exception ex) { MessageBox.Show("  يرجى التاكد من كتابة الرقم بصورة صحيحة "); MessageBox.Show(ex.ToString()); }

            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
          
        }

        private void dgv_Costomeres_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
           
        }
    }
}
