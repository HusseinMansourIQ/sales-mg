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
using System.Data.Common;

namespace Sales_Management_2.PL
{


    public partial class frm_prodManage : Form
    {
        BL.Cls_Products prod = new BL.Cls_Products();
        DAL.cls_dal dal = new DAL.cls_dal();
        DataTable DT = new DataTable();
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        SqlConnection con = new SqlConnection(@"Server =.;Database=DB_SaleApp;Integrated Security = True");
        SqlDataAdapter da2;
        DataTable dt2;
        int f = 1;
        int Prod_id;
        public frm_prodManage()
        {
            InitializeComponent();
          //  dgv_arrange();
            add_botton_1_to_dgv_es1_des();
            add_botton_2_to_dgv_es1_des();
           
            dgv1_design();

           // Autofill_productText();
            //  createDataTable();
            //Auto();

        }
        void CleareBoxes()
        {
            try
            { 
                //txt_sallerName.Clear();
            //txt_series.Clear();
            //txt_ProdID.Clear();
            txt_ProdName.Clear();
            txt_Qty.Clear();
            txt_unit.Clear();
            txt_price.Clear();
            txt_Single_price.Clear();
            txt_Wholesale_price.Clear();
          //  txt_ReOrderQTY.Clear();
            txt_Note.Clear();
            txt_ProdName.Focus();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

          
        }
        public void createDataTable()
        {
            //  عنونة حقول الداتا كرد فيو و تحديد عدد الاعمدة المراد عرضها
            try
            {
            DT = prod.FillProductDGV();
            DGV_Products.DataSource = DT;
            this.DGV_Products.Columns[0].HeaderText = "رمز المادة";
            this.DGV_Products.Columns[1].HeaderText = "تسلسل المادة";
            this.DGV_Products.Columns[2].HeaderText = "اسم المادة";
            this.DGV_Products.Columns[3].HeaderText = "العدد";
            this.DGV_Products.Columns[4].HeaderText = "وحدة القياس";
            this.DGV_Products.Columns[5].HeaderText = "سعر الشراء";
            this.DGV_Products.Columns[6].HeaderText = "سعر البيع بالمفرد";
            this.DGV_Products.Columns[7].HeaderText = "سعر البيع بالجملة";
            this.DGV_Products.Columns[8].HeaderText = "الحد الادنى";
            this.DGV_Products.Columns[9].HeaderText = "تاريخ الادخال";
            this.DGV_Products.Columns[10].HeaderText = "البائع الذي قام بالادخال";
            this.DGV_Products.Columns[11].HeaderText = "تاريخ التعديل";
            this.DGV_Products.Columns[12].HeaderText = "البائع الذي قام باخر تعديل";
            this.DGV_Products.Columns[13].HeaderText = "الملاحظات";
            lbl_totalProductsQTY.Text = (this.DGV_Products.RowCount).ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            
            //txt_SupplaierID.Text = (this.DGV_Products.RowCount + 1).ToString();
        }



        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frm_prodManage_Load(object sender, EventArgs e)
        {
            try
            {
               DGV_Products.DataSource= dal.SelectData("Sp_GetAllProducts", null);
                btn_cancel_update.Enabled = false;
                btn_Save.Enabled = true;
                txt_ProdName.Focus();
                lbl_totalProductsQTY.Text = (this.DGV_Products.RowCount).ToString();
                // dgv_arrange();


            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        /*  public void fillgrid()

          {
              da = new SqlDataAdapter("SELECT * FROM USER_Table where U_Name='" + txt_sallerName.Text + "'", con);
              DataTable dt = new DataTable();
              da.Fill(dt);
              if (dt.Rows.Count > 0)
              {
                  DGV_Products.DataSource = dt;
              }
          }
       */

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // fillgrid();
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {

            //dataGridView2.NewRowNeeded += DataGridView2_NewRowNeeded;
        }

        private void textBox3_TabIndexChanged(object sender, EventArgs e)
        {
            // textBox3.Clear();
            // textBox3.Focus();
        }

        private void txt_Qty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void txt_Single_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void txt_Wholesale_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //try
            //{ lbl_clock.Text = DateTime.Now.ToString("hh:mm:ss tt");
            //dateTimePicker1.Value = DateTime.Now;
            //dateTimePicker1.CustomFormat = "yyyy-MM-dd";

            //}
            //catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           

        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
              this.DGV_Products.DataSource = prod.SearchForProduct(txt_Search.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

           
        }

        private void DGV_Products_Click(object sender, EventArgs e)
        {
          

        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            try
            {     
                this.Cursor = Cursors.WaitCursor;
            print.rpt_print_all_prod report = new print.rpt_print_all_prod();
            report.SetDataSource(dal.SelectData("Sp_GetAllProducts", null));
            print.frm_print frm3 = new print.frm_print();
            frm3.crystalReportViewer1.ReportSource = report;
            frm3.ShowDialog();
            this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            { 
                if (f==0)
                {
                    if (txt_ProdName.Text == string.Empty)
                    { MessageBox.Show("لايجوز ترك حقل الاسم فارغ"); }
                    else if (txt_Qty.Text == string.Empty)
                    { MessageBox.Show("لطفا....لايمكن ترك حقل الكمية فارغ"); }
                    //else if (txt_unit.Text == string.Empty)
                    //{ MessageBox.Show("لطفا....لايمكن ترك حقل وحدة القياس فارغ"); }
                    else if (txt_price.Text == string.Empty)
                    { MessageBox.Show("لطفا....قم بادخال سعر الشراء"); }
                    else if (txt_Single_price.Text == string.Empty)
                    { MessageBox.Show("لطفا....قم بادخال سعر البيع بالمفرد"); }
                    else if (txt_Wholesale_price.Text == string.Empty)
                    { MessageBox.Show("لطفا....قم بادخال سعر بالجملة"); }

                    else
                    {
                        DataTable dt_check = new DataTable();
                        DT = prod.check_prod_name(txt_ProdName.Text);
                      if (dt_check.Rows.Count > 0)
                      {
                            MessageBox.Show("هذه المادة موجودة مسبقا");
                      }
                      else
                      {
                           prod.UpdateProduct(Convert.ToInt32(Prod_id),
                                           txt_ProdName.Text,
                                           Convert.ToInt32(txt_Qty.Text),
                                           txt_unit.Text,
                                           txt_price.Text,
                                           txt_Single_price.Text,
                                           txt_Wholesale_price.Text,
                                          5,
                                          System.DateTime.Now,
                                          Convert.ToString(Program.U_ArabicName),
                                           txt_Note.Text);
                        MessageBox.Show("تمت عملية التعديل بنجاح");
                        DGV_Products.DataSource = dal.SelectData("Sp_GetAllProducts", null);
                          //  dgv_arrange();
                            //createDataTable();
                            CleareBoxes();
                        btn_Close.Enabled = true;
                        btn_Save.Enabled = true;
                        btn_Save.Text = "ادراج المادة";
                        DGV_Products.Enabled = true;
                        f = 1;
                       }
                        //if (txt_ReOrderQTY.Text == string.Empty) { txt_ReOrderQTY.Text = "5"; }
                       
                    }
                }
                else if (f==1)
                {
                    if (txt_ProdName.Text == string.Empty) { MessageBox.Show("لايجوز ترك حقل الاسم فارغ"); }

                    else if (txt_Qty.Text == string.Empty) { MessageBox.Show("لطفا....لايمكن ترك حقل الكمية فارغ"); }

                    //else if (txt_unit.Text == string.Empty)
                    //{ MessageBox.Show("لطفا....لايمكن ترك حقل وحدة القياس فارغ"); }
                    else if (txt_price.Text == string.Empty) { MessageBox.Show("لطفا....قم بادخال سعر الشراء"); }

                    else if (txt_Single_price.Text == string.Empty) { MessageBox.Show("لطفا....قم بادخال سعر البيع بالمفرد"); }

                    else if (txt_Wholesale_price.Text == string.Empty) { MessageBox.Show("لطفا....قم بادخال سعر بالجملة"); }

                    else
                    {
                        DataTable dt_check = new DataTable();
                        dt_check = prod.check_prod_name(txt_ProdName.Text);
                      if(dt_check.Rows.Count>0)
                      {
                                MessageBox.Show("هذه المادة موجودة مسبقا");
                      }
                        else 
                        {
                            prod.insertProduct(txt_ProdName.Text,Convert.ToInt32( txt_Qty.Text), txt_unit.Text, txt_price.Text, txt_Single_price.Text, txt_Wholesale_price.Text, System.DateTime.Now, Program.U_ArabicName,txt_Note.Text);
                            MessageBox.Show("تمت عملية الادراج بنجاح");
                            DGV_Products.DataSource = dal.SelectData("Sp_GetAllProducts", null);
                           // dgv_arrange();
                            //createDataTable();
                            CleareBoxes();
                            btn_Close.Enabled = true;
                            btn_Save.Enabled = true;
                            btn_Save.Text = "ادراج المادة";
                            DGV_Products.Enabled = true;
                            f = 1;
                        }

                    }

                }
               


            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

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
                    this.DGV_Products.Columns.Insert(0, button);
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
                    button2.HeaderText = "حذف";
                    button2.Text = "حذف";
                    button2.UseColumnTextForButtonValue = true; //dont forget this line
                    this.DGV_Products.Columns.Insert(1, button2);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }

        private void lbl_totalProductsQTY_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void txt_ProdName_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_ProdName.Text != string.Empty)
            {
            //  SendKeys.Send("{Tab}");//تحويل مفتاح انتر الى مفتاح تاب (عدم اضافة سطر جديد عند الضغط على مفتاح انتر)
                                        txt_price.Focus();
            }
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                 DGV_Products.DataSource = dal.SelectData("Sp_GetAllProducts", null);
                //dgv_arrange();
                //createDataTable();
                CleareBoxes();
            btn_Close.Enabled = true;
            btn_Save.Enabled = true;
            btn_Save.Text = "ادراج المادة";
            DGV_Products.Enabled = true;
            btn_cancel_update.Enabled = false;
            f = 1;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }


        }

        private void txt_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_ProdName.Text != string.Empty)
            {
               // SendKeys.Send("{Tab}");//تحويل مفتاح انتر الى مفتاح تاب (عدم اضافة سطر جديد عند الضغط على مفتاح انتر)
                txt_Single_price.Focus();
            }
        }

        private void txt_Single_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_ProdName.Text != string.Empty)
            {
               // SendKeys.Send("{Tab}");//تحويل مفتاح انتر الى مفتاح تاب (عدم اضافة سطر جديد عند الضغط على مفتاح انتر)
                txt_Wholesale_price.Focus();
            }
        }

        private void txt_Wholesale_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_ProdName.Text != string.Empty)
            {
               // SendKeys.Send("{Tab}");//تحويل مفتاح انتر الى مفتاح تاب (عدم اضافة سطر جديد عند الضغط على مفتاح انتر)
                txt_Qty.Focus();
            }
        }

        private void txt_Qty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_ProdName.Text != string.Empty)
            {
                //SendKeys.Send("{Tab}");//تحويل مفتاح انتر الى مفتاح تاب (عدم اضافة سطر جديد عند الضغط على مفتاح انتر)
                btn_Save.Focus();
            }
        }
        private void dgv1_design()
        {
            try
            {
                DGV_Products.BorderStyle = BorderStyle.FixedSingle;
                DGV_Products.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                DGV_Products.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                DGV_Products.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                DGV_Products.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                DGV_Products.BackgroundColor = Color.White;

                DGV_Products.EnableHeadersVisualStyles = false;
                DGV_Products.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                DGV_Products.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                DGV_Products.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            }

            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

        }
      private void dgv_arrange()
        {
            if(DGV_Products.Rows.Count>0)
            {
                this.DGV_Products.Columns[2].Visible = false;
                this.DGV_Products.Columns[3].Visible = false;
                //this.DGV_Products.Columns[0].Visible = false;

                this.DGV_Products.Columns[0].Width = 50;
                this.DGV_Products.Columns[1].Width = 50;
                this.DGV_Products.Columns[4].Width = 225;
                this.DGV_Products.Columns[5].Width = 50;
                //DGV_Products.Columns[7].ValueType = typeof(double);
                //DGV_Products.Columns[8].ValueType = typeof(decimal);
                //DGV_Products.Columns[7].DefaultCellStyle.Format = "#.##0";
                //DGV_Products.Columns[8].DefaultCellStyle.Format = "#.##0";
                // DGV_Products.Rows[2].Cells[9].Value = string.Format("{0:#,##0}", double.Parse(DGV_Products.Rows[2].Cells[9].Value.ToString()));

            }
        }


        private void DGV_Products_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.ColumnIndex == 0 & DGV_Products.Rows.Count > 0)
            {
                try
                {
                    Prod_id =Convert.ToInt32( this.DGV_Products.CurrentRow.Cells[2].Value);
                    //txt_series.Text = this.DGV_Products.CurrentRow.Cells[3].Value.ToString();
                    txt_ProdName.Text = this.DGV_Products.CurrentRow.Cells[4].Value.ToString();
                    txt_Qty.Text = this.DGV_Products.CurrentRow.Cells[5].Value.ToString();
                    txt_unit.Text = this.DGV_Products.CurrentRow.Cells[6].Value.ToString();
                    txt_price.Text = this.DGV_Products.CurrentRow.Cells[7].Value.ToString();
                    txt_Single_price.Text = this.DGV_Products.CurrentRow.Cells[8].Value.ToString();
                    txt_Wholesale_price.Text = this.DGV_Products.CurrentRow.Cells[9].Value.ToString();
                    //  txt_ReOrderQTY.Text = this.DGV_Products.CurrentRow.Cells[8].Value.ToString();
                    txt_Note.Text = this.DGV_Products.CurrentRow.Cells[14].Value.ToString();

                    // dgv_Supplier.Rows.RemoveAt(DGV_Products.CurrentRow.Index);
                    // dgv_Supplier.Sort(DGV_Products.Columns[0], ListSortDirection.Ascending);
                    txt_ProdName.Focus();
                    btn_Close.Enabled = false;
                    btn_Save.Enabled = true;
                    btn_Save.Text = "حفظ التعديل";
                    btn_cancel_update.Enabled = true;
                    //DGV_Products.Enabled = false;
                    f = 0;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
            else if(e.ColumnIndex == 1 & DGV_Products.Rows.Count > 0)
            {
                   
               try
               {
                     Prod_id = Convert.ToInt32(this.DGV_Products.CurrentRow.Cells[2].Value);
                       
                    if (MessageBox.Show("هل تريد حذف    " + this.DGV_Products.CurrentRow.Cells[4].Value.ToString(), "تنــبــيــه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                               
                                prod.delete_PRODUCT(Prod_id);

                                MessageBox.Show("  تم الحذف بنجاح ");
                            DGV_Products.DataSource = dal.SelectData("Sp_GetAllProducts", null);
                            btn_cancel_update.Enabled = true;
                            btn_Save.Enabled = true;
                            txt_ProdName.Focus();
                    }
               }
               catch (Exception ex) { MessageBox.Show("  يرجى اغلاق الواجهة واعادة المحاولة "); MessageBox.Show(ex.ToString()); }

            }
            

        }

        private void DGV_Products_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                //  دالة ترقيم الروهيدر في الداتاكرد فيو تلقائيا
                // Loops through each row in the DataGridView, and adds the
                // row number to the header
                foreach (DataGridViewRow dGVRow in this.DGV_Products.Rows)
                {
                    dGVRow.HeaderCell.Value = String.Format("{0}", dGVRow.Index + 1);
                }

                // This resizes the width of the row headers to fit the numbers
                this.DGV_Products.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                dgv_arrange();
                lbl_totalProductsQTY.Text = (this.DGV_Products.RowCount).ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        public void Autofill_productText()
        {
            try
            {
                txt_Search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txt_Search.AutoCompleteSource = AutoCompleteSource.CustomSource;
                da2 = new SqlDataAdapter("SELECT Pro_Name, Pro_Qty, Single_price, Pro_Unit FROM  PRODUCT_Table order by Pro_Name asc", con);
               dt2 = new DataTable();
                da2.Fill(dt2);
                con.Open();
                if (dt2.Rows.Count > 0)
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        string name = dt2.Rows[j]["Pro_Name"].ToString();
                        coll.Add(name);
                    }
                    txt_Search.AutoCompleteCustomSource = coll;
                    con.Close();
                }

                else
                {

                    MessageBox.Show("product not found", "معلومات المادة", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    con.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                DGV_Products.DataSource = dal.SelectData("Sp_GetAllProducts", null);
                btn_cancel_update.Enabled = true;
                btn_Save.Enabled = true;
                txt_ProdName.Focus();


                // dgv_arrange();


            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
    }
}
