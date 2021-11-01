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
using System.Data.SqlClient;
using System.Data.Common;

namespace Sales_Management_2.PL
{
    public partial class frm_bill_management : Form

    {
        DAL.cls_dal dal = new DAL.cls_dal();
        BL.Cls_Sales sales = new BL.Cls_Sales();

        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();

        public static int SalesBill_Id=0;
        public static int s = 0;
        public static string bill_type = null;
        public static string bill_cat = null;
        public static string bill_date = null;
        public static string tot_B_dis = null;
        public static string tot_A_dis = null;
        public static string tot_dis = null;
        public static string paied_mon = null;
        public static string rest_mon = null;
        public static string balance_A_bill = null;
        public static string user_name = null;
        public static string cust_name = null;
        public static string cust_tel = null;
        public static string cust_addres = null;
        public static string balance_on_cust = null;
        public static DataTable dt_opp_det = null;



        public frm_bill_management()
        {
            InitializeComponent();
            Autofill_salerText();
            dgv1_design();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void form_sizing()
        {//ضبط حجم الفورم مع اي شاشة وشرط تفعيل  خاصية الماكسمايز
            try
            {
                this.Location = new Point(0, 0);
                this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }
        }
        private void dgv1_design()
        {
          try
          { 
            dgv_es1_des.BorderStyle = BorderStyle.FixedSingle;
            dgv_es1_des.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_es1_des.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dgv_es1_des.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_es1_des.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_es1_des.BackgroundColor = Color.White;

            dgv_es1_des.EnableHeadersVisualStyles = false;
            dgv_es1_des.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv_es1_des.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_es1_des.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

          }

          catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

        }

        private void frm_bill_management_Load(object sender, EventArgs e)
        {
            form_sizing();
            try
            {         
                lbl_username.Text = Program.U_ArabicName;

                this.dgv_es1_des.DataSource = dal.SelectData("sp_get_all_bill", null);
                

                // lbl_username.Text = frm_login.arabic_name;
                // string full_name = "(" + frm_login.username + "/" + frm_login.pass + ")" + frm_login.arabic_name;

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }

            lbl_count.Text = dgv_es1_des.Rows.Count.ToString();

            add_botton_1_to_dgv_es1_des();
            add_botton_2_to_dgv_es1_des();
            dgv_es1_des_hide_colomns();
            txt_CostmerName.Focus();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               // this.dgv_es1_des.DataSource = es1.get_es1_des_by_status(comboBox1.Text);
                lbl_count.Text = dgv_es1_des.Rows.Count.ToString();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }

        }
        private void dgv_es1_des_hide_colomns()
        {
            try
            {
                // this.dgv_es1_des.Columns[3].Visible = false;
                this.dgv_es1_des.Columns[0].Width = 50;
                this.dgv_es1_des.Columns[1].Width = 50;
                this.dgv_es1_des.Columns[2].Width = 50;
                this.dgv_es1_des.Columns[3].Width = 140;

                this.dgv_es1_des.Columns[4].Width = 90;
                this.dgv_es1_des.Columns[5].Width = 50;
                this.dgv_es1_des.Columns[6].Width = 200;
                this.dgv_es1_des.Columns[7].Width = 60;
                this.dgv_es1_des.Columns[8].Width = 60;
                this.dgv_es1_des.Columns[9].Width = 60;
                this.dgv_es1_des.Columns[10].Width = 60;
                this.dgv_es1_des.Columns[11].Width = 60;
                this.dgv_es1_des.Columns[12].Width = 60;



                ////////////////////////////////////////////////////////dgv_es2_det

                // if (this.dgv_es2_des.CurrentRow.Cells[18].Value.ToString() == "close")
                try
                {/// تلوين حسب حالة الاستمارة
                    foreach (DataGridViewRow dgv_des in this.dgv_es1_des.Rows)
                    {
                        if (dgv_des.Cells[4].Value.ToString() == "ارجاع مواد")
                        { dgv_des.DefaultCellStyle.BackColor = Color.LightPink; }
                        //if (dgv_des.Cells[4].Value.ToString() == "بيع مواد")
                        //{ dgv_des.DefaultCellStyle.BackColor = Color.LightBlue; }
                        if (dgv_des.Cells[4].Value.ToString() == "تسديد ديون")
                        { dgv_des.DefaultCellStyle.BackColor = Color.GreenYellow; }

                    }

                    // This resizes the width of the row headers to fit the numbers
                    this.dgv_es1_des.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }


            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            ////try
            ////{
            ////    //this.dgv_es1_des.DataSource = es1.es1_free_search(textBox1.Text);
            ////    lbl_count.Text = dgv_es1_des.Rows.Count.ToString();

            ////}
            ////catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }
            ///
            if (txt_bill_no.Text == "" || txt_bill_no.Text == string.Empty)
            {
                MessageBox.Show("الرجاء ادخال رقم القائمة اولا");
                txt_CostmerName.Clear();
                //txt_bill_no.Clear();

            }
            else
            {
                try
                {
                    DataTable dt_get_bill = new DataTable();
                    dt_get_bill = sales.get_bill_by_id(Convert.ToInt32(txt_bill_no.Text));
                    if (dt_get_bill.Rows.Count == 0)
                    {
                        MessageBox.Show("هذه القائمة غير موجودة ....يرجى التاكد من الرقم لطفا");
                    }
                    else
                    {
                        dgv_es1_des.DataSource = dt_get_bill;
                        //MessageBox.Show("هذه القائمة مكررة");
                        dgv_es1_des_hide_colomns();
                       // txt_CostmerName.Clear();
                        txt_bill_no.Clear();

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }


        }

        private void dgv_es1_des_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //  دالة ترقيم الروهيدر في الداتاكرد فيو تلقائيا
            // Loops through each row in the DataGridView, and adds the
            // row number to the header
            try
            {
                foreach (DataGridViewRow dGVRow in this.dgv_es1_des.Rows)
                {
                    dGVRow.HeaderCell.Value = String.Format("{0}", dGVRow.Index + 1);
                }

                // This resizes the width of the row headers to fit the numbers
                this.dgv_es1_des.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }


        }
        private void add_botton_1_to_dgv_es1_des()
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
                    this.dgv_es1_des.Columns.Insert(0, button);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }
        private void add_botton_2_to_dgv_es1_des()
        {
            //اظافة column button الى الداتا كردفيو
            try
            {
                DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
                {
                    button2.Name = "button2";
                    button2.HeaderText = "طباعة";
                    button2.Text = "طباعة";
                    button2.UseColumnTextForButtonValue = true; //dont forget this line
                    this.dgv_es1_des.Columns.Insert(1, button2);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }



        private void btn_refresh_Click(object sender, EventArgs e)
        {
           try
            {
           this.dgv_es1_des.DataSource = dal.SelectData("sp_get_all_bill", null);
            lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                dgv_es1_des_hide_colomns();
                txt_CostmerName.Clear();
                txt_bill_no.Clear();
                txt_CostmerName.Focus();
                //add_botton_1_to_dgv_es1_des();
                //add_botton_2_to_dgv_es1_des();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

            //dgv_es1_des_hide_colomns();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    print.rpt_print_es1 report = new print.rpt_print_es1();
            //    report.SetDataSource(es1.get_es1_to_print(Convert.ToInt32(es1_des_id)));
            //    print.frm_print frm3 = new print.frm_print();
            //    frm3.crystalReportViewer1.ReportSource = report;
            //    frm3.ShowDialog();
            //    this.Cursor = Cursors.Default;
            //}
            //catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }


        }

        private void dgv_es1_des_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 & this.dgv_es1_des.Rows.Count > 0)
            {
                try
                {
                   SalesBill_Id = Convert.ToInt32(this.dgv_es1_des.CurrentRow.Cells[2].Value);
                   
                    dt_opp_det = new DataTable();
                    dt_opp_det = sales.get_SalesOpp_by_SalesBill_Id(SalesBill_Id);

                    DataTable dt_bill_det = new DataTable();
                    dt_bill_det = sales.get_SelesBill_det_by_id_(SalesBill_Id);
                    
                    if(dt_bill_det.Rows.Count==1)
                    {
                        bill_date = dt_bill_det.Rows[0][1].ToString();
                        bill_cat = dt_bill_det.Rows[0][2].ToString();
                        bill_type = dt_bill_det.Rows[0][3].ToString();
                        tot_B_dis = dt_bill_det.Rows[0][4].ToString();
                        tot_A_dis = dt_bill_det.Rows[0][5].ToString();
                        tot_dis = dt_bill_det.Rows[0][6].ToString();
                        paied_mon = dt_bill_det.Rows[0][7].ToString();
                        rest_mon = dt_bill_det.Rows[0][8].ToString();
                        balance_A_bill = dt_bill_det.Rows[0][9].ToString();
                        user_name = dt_bill_det.Rows[0][10].ToString();
                        cust_name = dt_bill_det.Rows[0][11].ToString();
                        cust_tel = dt_bill_det.Rows[0][12].ToString();
                        cust_addres = dt_bill_det.Rows[0][13].ToString();
                        balance_on_cust = dt_bill_det.Rows[0][14].ToString();

                        PL.frm_Bill_Detailes frm_bill_det = new frm_Bill_Detailes();
                        frm_bill_det.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show( "اما هذه القائمة مكررة او غير موجودة اصلا");
                        
                    }
                   

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

            }
             else if (e.ColumnIndex == 1 & this.dgv_es1_des.Rows.Count > 0)
             {
                try
                {
                    SalesBill_Id = Convert.ToInt32(this.dgv_es1_des.CurrentRow.Cells[2].Value);

                    this.Cursor = Cursors.WaitCursor;
                    print.rpt_print_salles_bill report = new print.rpt_print_salles_bill();
                    report.SetDataSource(sales.get_bill_by_id_for_print(Convert.ToInt32(SalesBill_Id)));
                    print.frm_print frm3 = new print.frm_print();
                    frm3.crystalReportViewer1.ReportSource = report;
                    frm3.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_22"); }
            }

        }
        public bool isEnter = false;
        private void txt_CostmerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
               // Get_CostumerDataTotextBox();


                isEnter = false;

                // MessageBox.Show(isEnter.ToString());
            }

            else
            {
                if (txt_CostmerName.Text == "" || txt_CostmerName.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء ادخال الاسم اولا");
                    // txt_CostmerName.Clear();
                    txt_bill_no.Clear();
                    lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                    txt_CostmerName.Focus();
                }
                else
                {
                    try
                    {
                        DataTable dt_cust = new DataTable();
                        dt_cust = sales.sp_get_bill_by_Cust_name(txt_CostmerName.Text);
                        if (dt_cust.Rows.Count == 0)
                        {
                            MessageBox.Show(" ....يرجى التاكد من الاسم لطفا");
                            lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                            txt_CostmerName.Focus();
                        }
                        else
                        {
                            dgv_es1_des.DataSource = dt_cust;
                            // txt_CostmerName.Clear();
                            txt_bill_no.Clear();
                            lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                            dgv_es1_des_hide_colomns();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }

                isEnter = true;
            }
              //  check_CostmerName2();
           
            // MessageBox.Show(isEnter.ToString());
        }

        private void txt_CostmerName_TextChanged(object sender, EventArgs e)
        {
            // جلب الاسماء من قاعدة البينات وحسب تطابق الادخال في التيكست بوكس
           // Get_CostumerDataTotextBox();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (txt_CostmerName.Text == "" || txt_CostmerName.Text == string.Empty)
            {
                MessageBox.Show("الرجاء ادخال الاسم اولا");
               // txt_CostmerName.Clear();
                txt_bill_no.Clear();
                lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                txt_CostmerName.Focus();
            }
            else
            {
                try
                {
                    DataTable dt_cust = new DataTable();
                    dt_cust = sales.sp_get_bill_by_Cust_name(txt_CostmerName.Text);
                    if (dt_cust.Rows.Count == 0)
                    {
                        MessageBox.Show(" ....يرجى التاكد من الاسم لطفا");
                        lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                        txt_CostmerName.Focus();
                    }
                    else
                    {
                      dgv_es1_des.DataSource = dt_cust;
                       // txt_CostmerName.Clear();
                        txt_bill_no.Clear();
                        lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                        dgv_es1_des_hide_colomns();
                    }

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
                SqlConnection connection = new SqlConnection(@"Server =.;Database=DB_SaleApp;Integrated Security = True");
                SqlDataAdapter da = new SqlDataAdapter();
                //دالة لتغيير خصائص تيكست بوكس للاملاء التلقائي
                txt_CostmerName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_CostmerName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                da = new SqlDataAdapter("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table order by Cust_name asc", connection);
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

        public bool isEnter2 = false;
        private void txt_bill_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                // Get_CostumerDataTotextBox();


                isEnter2 = false;

                // MessageBox.Show(isEnter.ToString());
            }

            else
            {
                if (txt_bill_no.Text == "" || txt_bill_no.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء ادخال رقم القائمة اولا");
                    txt_CostmerName.Clear();
                    //txt_bill_no.Clear();
                    txt_bill_no.Focus();
                }
                else
                {
                    try
                    {
                        DataTable dt_get_bill = new DataTable();
                        dt_get_bill = sales.get_bill_by_id(Convert.ToInt32(txt_bill_no.Text));
                        if (dt_get_bill.Rows.Count == 0)
                        {
                            MessageBox.Show("هذه القائمة غير موجودة ....يرجى التاكد من الرقم لطفا");
                            txt_bill_no.Focus();
                        }
                        else
                        {
                            dgv_es1_des.DataSource = dt_get_bill;
                            //MessageBox.Show("هذه القائمة مكررة");
                            dgv_es1_des_hide_colomns();
                            // txt_CostmerName.Clear();
                            txt_bill_no.Clear();

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }


                isEnter2 = true;
            }
        }

        private void txt_bill_no_KeyPress(object sender, KeyPressEventArgs e)
        {

            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {

                e.Handled = true;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //if (dgv_es1_des.Rows.Count > 0)
            //{
                
            //    try
            //    {
            //        int id = Convert.ToInt32(this.dgv_es1_des.CurrentRow.Cells[2].Value);
            //        sales.delete_sales_bill(id);
            //        dgv_es1_des.Rows.RemoveAt(dgv_es1_des.CurrentRow.Index);
            //        this.dgv_es1_des.DataSource = dal.SelectData("sp_get_all_bill", null);
            //        lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
            //        dgv_es1_des_hide_colomns();
            //        txt_CostmerName.Clear();
            //        txt_bill_no.Clear();
            //        txt_CostmerName.Focus();

            //    }
            //    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            //}
           
        }

        private void btn_delet_Click(object sender, EventArgs e)
        {
            if (txt_billNo_to_delet.Text == string.Empty || txt_billNo_to_delet.Text == "" || txt_billNo_to_delet.Text == " ")
            {
                MessageBox.Show("يجب ادخال الرقم اولا ");
            }
            else
            {
                try
                {
                    DataTable dtbill = new DataTable();
                    dtbill = sales.get_bill_by_id(Convert.ToInt32(txt_billNo_to_delet.Text));

                   
                    
                    if (dtbill.Rows.Count < 1)
                    {
                        MessageBox.Show("هذه القائمة غير موجودة ....يرجى التاكد من الرقم ");
                    }
                    else if (MessageBox.Show("هل تريد حذف الفاتورة رقم  " + txt_billNo_to_delet.Text, "تنــبــيــه " , MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                
                    {
                        //int id = Convert.ToInt32(this.dgv_es1_des.CurrentRow.Cells[2].Value);
                        sales.delete_sales_bill(Convert.ToInt32(txt_billNo_to_delet.Text));
                        
                      //  dgv_es1_des.Rows.RemoveAt(dgv_es1_des.CurrentRow.Index);
                        this.dgv_es1_des.DataSource = dal.SelectData("sp_get_all_bill", null);
                        lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                        dgv_es1_des_hide_colomns();
                        txt_CostmerName.Clear();
                        txt_bill_no.Clear();
                        txt_CostmerName.Focus();
                        MessageBox.Show("  تم الحذف بنجاح ");
                    }
                }
                catch (Exception ex) { MessageBox.Show("  يرجى التاكد من كتابة الرقم بصورة صحيحة "); MessageBox.Show(ex.ToString()); }

            }
            
        }
        private void txt_billNo_to_delet_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {

                e.Handled = true;
            }
        }
    
    }
}
