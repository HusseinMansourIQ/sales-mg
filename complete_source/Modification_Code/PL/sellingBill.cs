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
using System.Timers;

namespace Sales_Management_2.PL
{
    public partial class sellingBill : Form
    {
        // لاعادة البينات الى الفورم الاول عند اغلاق الفورم الثاني
        private static sellingBill frm;
        static void Frm_formclosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        } // لاعادة البينات الى الفورم الاول عند اغلاق الفورم الثاني
        public static sellingBill getsellingBillForm   // لاعادة البينات الى الفورم الاول عند اغلاق الفورم الثاني
        {
            get
            {
                if (frm == null)
                {
                    frm = new sellingBill();
                    frm.FormClosed += new FormClosedEventHandler(Frm_formclosed);
                }
                return frm;
            }

        }




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
        public int cust_id;
        public int Produc_ID;
        public int no_save = 0;
        double new_balance = 0;
        double BalanceOnCostmer;
        DAL.cls_dal dal = new DAL.cls_dal();
        BL.Cls_AutoFill autofill = new BL.Cls_AutoFill();
        BL.Cls_Sales sales = new BL.Cls_Sales();
        BL.Cls_Customeres cstmr = new BL.Cls_Customeres();
        BL.Cls_Products prod = new BL.Cls_Products();

        public sellingBill()
        {

            InitializeComponent();
           // this.txt_Bill_ID.Text = sales.GetLastSalesBill_Id().Rows[0][0].ToString();
            if (frm == null)
            {
                frm = this;

            }


            //استدعاء الاملاء التلقائي للروهيدر تيكست
            this.dgv_sellingPill.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.DataBindingComplete);


            createDataTable();
            Autofill_productText();
            Autofill_salerText();
           




        }
        //دالة لتفريخ خانات المنتج
        public void Clear_ProdBoxes()
        {
            try
            {
            txt_prodName.Clear();
            txt_qty.Clear();
            txt_prodUnit.Clear();
            txt_availableQNTY.Clear();
            txt_discount.Text = "0";
            txt_price.Text="0";
            txt_prodName.Focus();
            //txt_PaidMony.Text = "0";
            txt_RestMony.Text = "0";
            txt_BalanceAftercurentBill.Text = "0";
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
          
        }
        //دالة تفريغ خانات الزبون
        public void Clear_CustomerBoxes()
        {
            try
            {
            txt_adress.Clear();
            txt_phonNo.Clear();
            txt_CostmerName.Clear();
            txt_CostmerName.Focus();
            txt_BalanceOnCostmer.Text = "0";
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           
        }


        private void sellingBill_Load(object sender, EventArgs e)
        {
            frm_SalesBill_Type sbl = new frm_SalesBill_Type();
            sbl.ShowDialog();
            txt_prodName.Focus();
            form_sizing();

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

        private void btn_close_Click(object sender, EventArgs e)
        {
            frm = null;
            Close();
        }

        private void rb_mufrad_CheckedChanged(object sender, EventArgs e)
        {
            x = "مفرد";
            //  MessageBox.Show(x);
        }

        private void rb_jumla_CheckedChanged(object sender, EventArgs e)
        {
            x = "جملة ";
            // MessageBox.Show(x);
        }
        private void btn_insert_Click(object sender, EventArgs e)
        {



            if (txt_qty.Text != string.Empty && txt_prodName.Text != string.Empty && txt_discount.Text != string.Empty && txt_price.Text != string.Empty)
            {
                try
                {
   //حساب مجموع مبلغ كل مادة بعد ادخال العدد والخصم 
                double prod_PriceAfterDiscount = Convert.ToDouble(txt_price.Text) - Convert.ToDouble(txt_discount.Text);
                double t_ProdCostBeforDiscount = Convert.ToDouble(txt_price.Text) * Convert.ToDouble(txt_qty.Text);
                double t_ProdCostAfterDiscount = prod_PriceAfterDiscount * Convert.ToDouble(txt_qty.Text);
                double t_prod_cost = prod_PriceAfterDiscount * Convert.ToDouble(txt_qty.Text);
                double t_discount = Convert.ToDouble(txt_discount.Text) * Convert.ToDouble(txt_qty.Text);

                // ادراج الطلبية الى الداتاكردفيو وحسب الترتيب المطلوب 
                DataRow dr = DT.NewRow();

                dr[0] = txt_prodName.Text;
                dr[1] = txt_qty.Text;
                dr[2] = txt_prodUnit.Text;
                dr[3] =Convert.ToDouble( txt_price.Text);
                dr[4] =Convert.ToDouble( prod_PriceAfterDiscount);
                dr[5] =Convert.ToDouble( t_prod_cost);
                dr[6] = Convert.ToDouble(t_discount);
                dr[7] = Produc_ID;
                DT.Rows.Add(dr);
                   
                    //dgv_sellingPill.Columns[4].ValueType = typeof(double);
                    //dgv_sellingPill.Rows[0].Cells[3].Value = string.Format("{0:#,##0}", double.Parse(dgv_sellingPill.Rows[2].Cells[9].Value.ToString()));
                  
                    //حساب المجموع الخاص بمجموعة حقول ضمن داتا كردفيو
                     txt_totalAfterDis.Text = (from DataGridViewRow row in dgv_sellingPill.Rows
                                          where row.Cells[5].FormattedValue.ToString()!= string.Empty
                                          select Convert.ToDouble(row.Cells[5].FormattedValue)).Sum().ToString();
                     txt_total_Dis.Text = (from DataGridViewRow row in dgv_sellingPill.Rows
                                      where row.Cells[6].FormattedValue.ToString()!= string.Empty
                                      select Convert.ToDouble(row.Cells[6].FormattedValue)).Sum().ToString();
                     txt_totalBeforDis.Text = Convert.ToString(Convert.ToDouble(txt_totalAfterDis.Text) + Convert.ToDouble(txt_total_Dis.Text));
                
                    Clear_ProdBoxes();
                //txt_discount.Text = "0";
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
             
            }
            else
                MessageBox.Show("الرجاء قم باملاء كافة الحقول المطلوبة ");
        }

        public void createDataTable()
        {
            try
            {
 //  عنونة حقول الداتا كرد فيو و تحديد عدد الاعمدة المراد عرضها
            //DT.Columns.Add("ت");
            DT.Columns.Add("اسم المادة");
            DT.Columns.Add("العدد");
            DT.Columns.Add("وحدةالقياس");
            DT.Columns.Add("السعرالاصلي");
            DT.Columns.Add("السعر بعدالخصم");
            DT.Columns.Add("اجمالي المبلغ");
            DT.Columns.Add("اجمالي الخصم");
            DT.Columns.Add("ID");

            dgv_sellingPill.DataSource = DT;
                dgv_sellingPill.Columns[4].ValueType = typeof(double);
                // dgv_sellingPill.RowHeadersWidth = 50;
                dgv_sellingPill.Columns[0].Width = 240;
            dgv_sellingPill.Columns[1].Width = 60;
            dgv_sellingPill.Columns[2].Width = 80;
            dgv_sellingPill.Columns[3].Width = 80;
            dgv_sellingPill.Columns[4].Width = 80;
            dgv_sellingPill.Columns[5].Width = 80;
            dgv_sellingPill.Columns[6].Width = 80;
            dgv_sellingPill.Columns[7].Width = 10;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           

        }
        private void dgv_sellingPill_DoubleClick(object sender, EventArgs e)
        {
            try
            {
            txt_prodName.Text = this.dgv_sellingPill.CurrentRow.Cells[0].Value.ToString();
            txt_qty.Text = this.dgv_sellingPill.CurrentRow.Cells[1].Value.ToString();
            txt_prodUnit.Text = this.dgv_sellingPill.CurrentRow.Cells[2].Value.ToString();
            txt_price.Text = this.dgv_sellingPill.CurrentRow.Cells[3].Value.ToString();
            txt_discount.Text = (Convert.ToDouble(txt_price.Text) - (Convert.ToDouble(this.dgv_sellingPill.CurrentRow.Cells[4].Value.ToString()))).ToString();
            dgv_sellingPill.Rows.RemoveAt(dgv_sellingPill.CurrentRow.Index);
            txt_qty.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            

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
                else if (MessageBox.Show("هذا الزبون غير موجود هل تريج اضافته الى قاعدة البانات ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                {
                    PL.frm_CostamerManage frm = new PL.frm_CostamerManage();
                    frm.ShowDialog();
                    txt_adress.Clear();
                    txt_phonNo.Clear();
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
        public void check_ProductName()
        {
            try
            {
  // دالة فحص اسم الزبون اذا كان موجود سابقا تجعل الفوكس على خانة المنتوجات واذا كان غير موجود يتم اظهار رسالة من خيارين وتشغل من خلال حدث الكي داون ل تيكست اسم الزبون 
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;Integrated Security=True"))
            {
                da = new SqlDataAdapter("SELECT PRODUCT_Table.* FROM PRODUCT_Table where Pro_Name='" + txt_prodName.Text + "'", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txt_qty.Focus();
                }
                else
                {
                    if (MessageBox.Show("هذه المادة غير موجودة... هل تريج اضافته الى قاعدة البانات ؟ ", "تنــبــيــه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                        PL.frm_buyingProduct frm = new PL.frm_buyingProduct();
                        frm.ShowDialog();
                        txt_availableQNTY.Clear();
                        txt_price.Clear();
                        txt_qty.Clear();
                        txt_discount.Clear();
                    }
                    else
                    {
                        txt_availableQNTY.Clear();
                        txt_price.Clear();
                        txt_price.ReadOnly = false;
                        txt_price.MaxLength = 8;
                        txt_qty.Clear();
                        txt_discount.Clear();
                    }

                }
                txt_price.ReadOnly = false;
                txt_discount.Text = "0";
            }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
          
        }
        public void Autofill_productText()
        {
            try
            {
             txt_prodName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_prodName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            da2 = new SqlDataAdapter("SELECT Pro_Name, Pro_Qty, Single_price, Pro_Unit FROM  PRODUCT_Table order by Pro_Name asc", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            con.Open();
            if (dt2.Rows.Count > 0)
            {
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    string name = dt2.Rows[j]["Pro_Name"].ToString();
                    coll2.Add(name);
                }
                txt_prodName.AutoCompleteCustomSource = coll2;
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
        //جلب المنتوجات من قاعدة البيانات الى الفورم
        public void get_ProductDataTotextBox()
        {
            try
            {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand("SELECT PRODUCT_Table.* FROM PRODUCT_Table where Pro_Name='" + txt_prodName.Text + "'", connection);
                connection.Open();

                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    Produc_ID = Convert.ToInt32(read["Pro_Id"]);
                    txt_availableQNTY.Text = (read["Pro_Qty"].ToString());
                    Single_price = (read["Single_price"].ToString());
                    Whole_Price = (read["WholeSale_price"].ToString());
                        lbl_price.Text = (read["Pro_Price"].ToString());
                       

                        if (rb_jumla.Checked == true)
                    {
                        txt_price.Text = Whole_Price;

                    }
                    else { txt_price.Text = Single_price; }
                    txt_prodUnit.Text = (read["Pro_Unit"].ToString());
                        txt_price.Text = string.Format("{0:#,##0}", double.Parse(txt_price.Text));
                        lbl_price.Text = string.Format("{0:#,##0}", double.Parse(lbl_price.Text));
                    }
                read.Close();
            }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
        //جلب الكوستومرز من قاعدة البيانات الى الفورم
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
                        if (Convert.ToInt32(read["BalanceOnCostmer"]) >= 0)
                        {
                            lbl_balance.Text = "مطلوب";
                            txt_BalanceOnCostmer.Text = BalanceOnCostmer.ToString();
                        }
                        else
                        {
                            lbl_balance.Text = "يطلب";
                            txt_BalanceOnCostmer.Text = Math.Abs(BalanceOnCostmer).ToString();

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
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
        private void txt_costName_TextChanged(object sender, EventArgs e)
        {
            // جلب الاسماء من قاعدة البينات وحسب تطابق الادخال في التيكست بوكس
            Get_CostumerDataTotextBox();
        }
        private void txt_prodName_TextChanged(object sender, EventArgs e)
        {
            // جلب المنتجات من قاعدة البينات وحسب تطابق الادخال في التيكست بوكس
            get_ProductDataTotextBox();
        }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //كود لجعل التيكست بوكس يستقبل ارقام وفاصة عشرية فقط
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            txt_discount.AcceptsReturn = true;
        }

        private void txt_discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {

                e.Handled = true;
            }
        }

        private void txt_phonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {

                e.Handled = true;
            }
        }

        private void txt_prodName_KeyDown(object sender, KeyEventArgs e)
        {
            //للتاكد من المادة موجودة او لا عند الضغط على انتر
            if (e.KeyCode != Keys.Enter)
            {
                get_ProductDataTotextBox();
            }
            else if(txt_prodName.Text!=string.Empty)
            {

                check_ProductName();
            }
        }

        private void txt_discount_KeyDown(object sender, KeyEventArgs e)
        {

            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_discount.Text != string.Empty)
                SendKeys.Send("{Tab}");//تحويل مفتاح انتر الى مفتاح تاب (عدم اضافة سطر جديد عند الضغط على مفتاح انتر)
                                       // btn_insert.Focus();
        }

        private void txt_qty_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_qty.Text != string.Empty)
            {
                txt_discount.Focus();
            }

        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public bool isEnter = false;
        private void txt_CostmerName_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                Get_CostumerDataTotextBox();
                isEnter = false;
                // MessageBox.Show(isEnter.ToString());
            }

            else
                check_CostmerName2();
            isEnter = true;
            // MessageBox.Show(isEnter.ToString());
        }

        private void txt_CostmerName_TextChanged(object sender, EventArgs e)
        {
            Get_CostumerDataTotextBox();
        }

        private void txt_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {

                e.Handled = true;
            }
        }

        private void txt_phonNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void تعديلهذهالفقرةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
             txt_prodName.Text = this.dgv_sellingPill.CurrentRow.Cells[0].Value.ToString();
            txt_qty.Text = this.dgv_sellingPill.CurrentRow.Cells[1].Value.ToString();
            txt_prodUnit.Text = this.dgv_sellingPill.CurrentRow.Cells[2].Value.ToString();
            txt_price.Text = this.dgv_sellingPill.CurrentRow.Cells[3].Value.ToString();
            if (edit == 0) { txt_discount.Text = (Convert.ToDouble(txt_price.Text) - (Convert.ToDouble(this.dgv_sellingPill.CurrentRow.Cells[4].Value.ToString()))).ToString(); }
            else if (edit == 1) { txt_discount.Text = "77777"; }
            dgv_sellingPill.Rows.RemoveAt(dgv_sellingPill.CurrentRow.Index);
            dgv_sellingPill.Sort(dgv_sellingPill.Columns[0], ListSortDirection.Ascending);
            txt_qty.Focus();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           

        }

        private void حذفهذهالفقرةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            dgv_sellingPill.Rows.RemoveAt(dgv_sellingPill.CurrentRow.Index);

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void حذفالكلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            DT.Clear();
            this.dgv_sellingPill.Refresh();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void dgv_sellingPill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_sellingPill_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
txt_totalAfterDis.Text = (from DataGridViewRow row in dgv_sellingPill.Rows
                                      where row.Cells[5].FormattedValue.ToString() != string.Empty
                                      select Convert.ToDouble(row.Cells[6].FormattedValue)).Sum().ToString();
            txt_total_Dis.Text = (from DataGridViewRow row in dgv_sellingPill.Rows
                                  where row.Cells[6].FormattedValue.ToString() != string.Empty
                                  select Convert.ToDouble(row.Cells[6].FormattedValue)).Sum().ToString();
            txt_totalBeforDis.Text = Convert.ToString(Convert.ToDouble(txt_totalAfterDis.Text) + Convert.ToDouble(txt_total_Dis.Text));
            //Clear_ProdBoxes();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            

        }
        private void DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
              //  دالة ترقيم الروهيدر في الداتاكرد فيو تلقائيا
            // Loops through each row in the DataGridView, and adds the
            // row number to the header
            foreach (DataGridViewRow dGVRow in this.dgv_sellingPill.Rows)
            {
                dGVRow.HeaderCell.Value = String.Format("{0}", dGVRow.Index + 1);
            }

            // This resizes the width of the row headers to fit the numbers
            this.dgv_sellingPill.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           
        }

        private void btn_SaveOnly_Click(object sender, EventArgs e)
        {
            if (rb_mufrad.Checked == false && rb_jumla.Checked == false) { MessageBox.Show("(الرجاء اختيار نوع القائمة (جملة او مفرد "); }
            else if (dgv_sellingPill.RowCount < 1)
            {
                MessageBox.Show("لايمكن حفظ قائمة فارغة..الرجاء التاكد من ادخال المواد المراد بيعها");
                txt_prodName.Focus();

            }
            else if (txt_PaidMony.Text == "" || txt_PaidMony.Text == string.Empty) { MessageBox.Show("الرجاء قم بادخال المبلغ الواصل "); }

            else if (txt_CostmerName.Text == string.Empty || txt_CostmerName.Text=="بلا")
            {

                if (MessageBox.Show("لم يتم كتابة اسم الزبون هل تريد المتابعة وحفظ القائمة بدون اسم ", "تنبيه ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                        if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                        new_balance = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)) + BalanceOnCostmer;
                        if (new_balance >= 0)
                        {
                            lbl_new_balance.Text = "مطلوب";
                            txt_BalanceAftercurentBill.Text = new_balance.ToString();
                        }
                        else
                        {
                            lbl_new_balance.Text = "يطلب";
                            txt_BalanceAftercurentBill.Text = Math.Abs(new_balance).ToString();
                            // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                        }
                        // decimal restmony = (Convert.ToDecimal(txt_totalAfterDis.Text) - Convert.ToDecimal(txt_PaidMony.Text));

                      if (Convert.ToDouble(txt_RestMony.Text) > 0)
                        {
                            MessageBox.Show("يجب تسديد كامل المبلغ او تغيير اسم الزبون ليتم ادراجه ضمن قائمة الديون");
                      }
                      else
                      {
                            //احضار اخر قائمة+1 
                            int max_SalesBill_Id = Convert.ToInt32(sales.GetLastSalesBill_Id().Rows[0][0]);

                            sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, x, 49, txt_totalBeforDis.Text,
                                            txt_totalAfterDis.Text, txt_total_Dis.Text, txt_PaidMony.Text,
                                           txt_RestMony.Text, Convert.ToDecimal(txt_BalanceAftercurentBill.Text), Program.U_ArabicName, "بيع مواد","");
                            //اضافة لمواد الى جدول المواد مع رقم اخر قائمة
                            for (int i = 0; i < dgv_sellingPill.Rows.Count; i++)
                            {
                                //MessageBox.Show(Convert.ToString(dgv_sellingPill.Rows[i].Cells[7].Value));
                                // MessageBox.Show(Program.User_id.ToString());

                                sales.AddSalesOpp(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value),
                                    dgv_sellingPill.Rows[i].Cells[1].Value.ToString(),
                                    dgv_sellingPill.Rows[i].Cells[4].Value.ToString(),
                                    dgv_sellingPill.Rows[i].Cells[5].Value.ToString(),
                                    max_SalesBill_Id,
                                    Program.User_id,
                                      dgv_sellingPill.Rows[i].Cells[3].Value.ToString());

                                prod.update_Pro_Qty(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value), Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[1].Value));

                            }
                            Clear_ProdBoxes();
                            Clear_CustomerBoxes();
                            DT.Clear();
                            dgv_sellingPill.Refresh();
                            txt_prodName.Focus();
                            txt_PaidMony.Clear();
                            no_save = 0;

                            txt_CostmerName.ReadOnly = false;
                            txt_adress.Visible = true;
                            txt_phonNo.Visible = true;
                            txt_BalanceOnCostmer.Visible = true;
                            MessageBox.Show("تم حفظ القائمة بنجاح");


                      }
                    }
                    catch (Exception ex){MessageBox.Show(ex.ToString());}
                }
                else
                {
                    txt_CostmerName.Focus();
                    return;
                }
            }
            else
            {
                //cust_id = 0;
              try
              {
                    txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                    if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                    new_balance = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)) + BalanceOnCostmer;
                    if (new_balance >= 0)
                    {
                        lbl_new_balance.Text = "مطلوب";
                        txt_BalanceAftercurentBill.Text = new_balance.ToString();
                    }
                    else
                    {
                        lbl_new_balance.Text = "يطلب";
                        txt_BalanceAftercurentBill.Text = Math.Abs(new_balance).ToString();
                        // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                    }
                 if (no_save==0)
                 {
                      decimal restmony =( Convert.ToDecimal(txt_totalAfterDis.Text) - Convert.ToDecimal(txt_PaidMony.Text));
                      //احضار اخر قائمة+1 
                      int max_SalesBill_Id = Convert.ToInt32(sales.GetLastSalesBill_Id().Rows[0][0]);

                      sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, x, cust_id, txt_totalBeforDis.Text,
                                      txt_totalAfterDis.Text, txt_total_Dis.Text, txt_PaidMony.Text,
                                     txt_RestMony.Text,Convert.ToDecimal( txt_BalanceAftercurentBill.Text), Program.U_ArabicName,"بيع مواد","");
                        //اضافة لمواد الى جدول المواد مع رقم اخر قائمة
                      for (int i = 0; i < dgv_sellingPill.Rows.Count; i++)
                      {
                        //MessageBox.Show(Convert.ToString(dgv_sellingPill.Rows[i].Cells[7].Value));
                        // MessageBox.Show(Program.User_id.ToString());

                            sales.AddSalesOpp(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value),
                                dgv_sellingPill.Rows[i].Cells[1].Value.ToString(),
                                dgv_sellingPill.Rows[i].Cells[4].Value.ToString(),
                                dgv_sellingPill.Rows[i].Cells[5].Value.ToString(),
                                max_SalesBill_Id,
                                Program.User_id,
                                  dgv_sellingPill.Rows[i].Cells[3].Value.ToString());
                       
                         prod.update_Pro_Qty(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value),Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[1].Value));
                    
                      }
                        cstmr.update_balance(cust_id, restmony);

                        Clear_ProdBoxes();
                        Clear_CustomerBoxes();
                        DT.Clear();
                        dgv_sellingPill.Refresh();
                        txt_prodName.Focus();
                        txt_PaidMony.Clear();
                        no_save = 0;

                        txt_CostmerName.ReadOnly = false;
                        txt_adress.Visible = true;
                        txt_phonNo.Visible = true;
                        txt_BalanceOnCostmer.Visible = true;
                        MessageBox.Show("تم حفظ القائمة بنجاح");
                    }
                 else if (no_save == 1) //اظافة الزبون مباشرتا 
                 {
                        decimal restmony = (Convert.ToDecimal(txt_totalAfterDis.Text) - Convert.ToDecimal(txt_PaidMony.Text));

                        cstmr.InsertCustomer("0", txt_CostmerName.Text, txt_phonNo.Text, txt_adress.Text,"0",txt_BalanceOnCostmer.Text, System.DateTime.Now, "بلا");

                        cust_id = Convert.ToInt32( dal.SelectData("Sp_get_max_cust_id", null).Rows[0][0]);
                        //احضار اخر قائمة+1 
                        int max_SalesBill_Id = Convert.ToInt32(sales.GetLastSalesBill_Id().Rows[0][0]);

                        sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, x, cust_id, txt_totalBeforDis.Text,
                                          txt_totalAfterDis.Text, txt_total_Dis.Text, txt_PaidMony.Text,
                                         txt_RestMony.Text, Convert.ToDecimal(txt_BalanceAftercurentBill.Text), Program.U_ArabicName, "بيع مواد","");
                        //اضافة لمواد الى جدول المواد مع رقم اخر قائمة
                        for (int i = 0; i < dgv_sellingPill.Rows.Count; i++)
                        {
                            //MessageBox.Show(Convert.ToString(dgv_sellingPill.Rows[i].Cells[7].Value));
                            // MessageBox.Show(Program.User_id.ToString());

                            sales.AddSalesOpp(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value),
                                dgv_sellingPill.Rows[i].Cells[1].Value.ToString(),
                                dgv_sellingPill.Rows[i].Cells[4].Value.ToString(),
                                dgv_sellingPill.Rows[i].Cells[5].Value.ToString(),
                                max_SalesBill_Id,
                                Program.User_id,
                                dgv_sellingPill.Rows[i].Cells[3].Value.ToString());

                            prod.update_Pro_Qty(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value), Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[1].Value));

                        }
                       cstmr.update_balance(cust_id, restmony);
                      
                        Clear_ProdBoxes();
                        Clear_CustomerBoxes();
                        DT.Clear();
                        dgv_sellingPill.Refresh();
                        txt_prodName.Focus();
                        txt_PaidMony.Clear();
                        no_save = 0;

                        txt_CostmerName.ReadOnly = false;
                        txt_adress.Visible = true;
                        txt_phonNo.Visible = true;
                        txt_BalanceOnCostmer.Visible = true;
                        MessageBox.Show("تم حفظ القائمة بنجاح");
                 }
                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }


        }


        public void check_CostmerName2()
        {
            try
            {
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;Integrated Security=True"))
            {
                SqlDataAdapter da3 = new SqlDataAdapter("SELECT CUSTOMER_Table.* FROM CUSTOMER_Table where Cust_name='" + txt_CostmerName.Text + "'", connection);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                //if (txt_CostmerName.Text == string.Empty) { MessageBox.Show(" قم بادخال اسم المجهز رجاءا "); txt_CostmerName.Focus(); }

                if (dt3.Rows.Count > 0)
                {
                    btn_SaveOnly.Focus();
                }
                else if (txt_CostmerName.Text == string.Empty)
                {
                    MessageBox.Show("لايوجد اسم الزبون ");
                }
                else
                {
                    if (MessageBox.Show("هذا الزبون غير موجود هل تريج اضافته الى قاعدة البانات ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                        PL.frm_CostamerManage frm = new PL.frm_CostamerManage();
                        frm.ShowDialog();
                        txt_adress.Clear();
                        txt_phonNo.Clear();
                    }
                    else
                    {
                        cust_id = 0;
                        txt_adress.Clear();
                        txt_phonNo.Clear();
                      //  txt_BalanceForCostmer.Text = "0";
                        txt_BalanceOnCostmer.Text = "0";
                        txt_phonNo.Focus();
                    }

                }
            }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void btn_SaveOnly_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(txt_CostmerName.Text);
            PL.frm_SalesBill_Type frmtype = new PL.frm_SalesBill_Type();
            frmtype.ShowDialog();

        }

        private void frm_sellingBill_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txt_PaidMony_TextChanged(object sender, EventArgs e)
        {
        }

        private void txt_PaidMony_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && txt_PaidMony.Text != string.Empty)
            {
                try
                { btn_SaveOnly.Focus();
                txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                new_balance = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)) + BalanceOnCostmer;
                if (new_balance >= 0)
                {
                    lbl_new_balance.Text = "مطلوب";
                    txt_BalanceAftercurentBill.Text = new_balance.ToString();
                }
                else
                {
                    lbl_new_balance.Text = "يطلب";
                    txt_BalanceAftercurentBill.Text = Math.Abs(new_balance).ToString();
                    // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                }

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
               
            }

        }

        private void txt_RestMony_TextChanged(object sender, EventArgs e)
        {
            txt_RestMony.Text = string.Format("{0:#,##0}", double.Parse(txt_RestMony.Text));
        }

        private void btn_serch_Click(object sender, EventArgs e)
        {
            PL.frm_bill_management frm = new frm_bill_management();
            frm.Show();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void txt_PaidMony_Leave(object sender, EventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
           
              //  btn_SaveOnly.Focus();
              if (txt_PaidMony.Text=="" || txt_PaidMony.Text==string.Empty)
            {
                MessageBox.Show("الرجاء عدم ترك حقل المبلغ الواصل فارغ");
               
            }
            else
            {
                try
                {
                txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                new_balance = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)) + BalanceOnCostmer;
                if (new_balance >= 0)
                {
                    lbl_new_balance.Text = "مطلوب";
                    txt_BalanceAftercurentBill.Text = new_balance.ToString();
                }
                else
                {
                    lbl_new_balance.Text = "يطلب";
                    txt_BalanceAftercurentBill.Text = Math.Abs(new_balance).ToString();
                    // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                }
                    txt_PaidMony.Text = string.Format("{0:#,##0}", double.Parse(txt_PaidMony.Text));
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
               

            }
           

        }

        private void txt_PaidMony_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

        }

        private void btn_SaveOnly_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_qty.Text != string.Empty)
            {
                btn_SaveOnly.Focus();
            }

        }

        private void btn_SaveOnly_MouseHover(object sender, EventArgs e)
        {
            btn_SaveOnly.BackColor = Color.FromArgb(255, 192, 255);
        }

        private void btn_SaveOnly_MouseLeave(object sender, EventArgs e)
        {
            btn_SaveOnly.BackColor = Color.IndianRed;
        }

        private void txt_totalBeforDis_TextChanged(object sender, EventArgs e)
        {
            txt_totalBeforDis.Text = string.Format("{0:#,##0}", double.Parse(txt_totalBeforDis.Text));
        }

        private void txt_total_Dis_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_totalAfterDis_TextChanged(object sender, EventArgs e)
        {
            txt_totalAfterDis.Text = string.Format("{0:#,##0}", double.Parse(txt_totalAfterDis.Text));
        }

        private void btn_SaveAndPrnt_Click(object sender, EventArgs e)
        {
            if (rb_mufrad.Checked == false && rb_jumla.Checked == false) { MessageBox.Show("(الرجاء اختيار نوع القائمة (جملة او مفرد "); }
            else if (dgv_sellingPill.RowCount < 1)
            {
                MessageBox.Show("لايمكن حفظ قائمة فارغة..الرجاء التاكد من ادخال المواد المراد بيعها");
                txt_prodName.Focus();

            }
            else if (txt_PaidMony.Text == "" || txt_PaidMony.Text == string.Empty) { MessageBox.Show("الرجاء قم بادخال المبلغ الواصل "); }

            else if (txt_CostmerName.Text == string.Empty || txt_CostmerName.Text == "بلا")
            {

                if (MessageBox.Show("لم يتم كتابة اسم الزبون هل تريد المتابعة وحفظ القائمة بدون اسم ", "تنبيه ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                        if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                        new_balance = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)) + BalanceOnCostmer;
                        if (new_balance >= 0)
                        {
                            lbl_new_balance.Text = "مطلوب";
                            txt_BalanceAftercurentBill.Text = new_balance.ToString();
                        }
                        else
                        {
                            lbl_new_balance.Text = "يطلب";
                            txt_BalanceAftercurentBill.Text = Math.Abs(new_balance).ToString();
                            // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                        }
                        // decimal restmony = (Convert.ToDecimal(txt_totalAfterDis.Text) - Convert.ToDecimal(txt_PaidMony.Text));

                        if (Convert.ToDouble(txt_RestMony.Text) > 0)
                        {
                            MessageBox.Show("يجب تسديد كامل المبلغ او تغيير اسم الزبون ليتم ادراجه ضمن قائمة الديون");
                        }
                        else
                        {
                            //احضار اخر قائمة+1 
                            int max_SalesBill_Id = Convert.ToInt32(sales.GetLastSalesBill_Id().Rows[0][0]);

                            sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, x, 49, txt_totalBeforDis.Text,
                                            txt_totalAfterDis.Text, txt_total_Dis.Text, txt_PaidMony.Text,
                                           txt_RestMony.Text, Convert.ToDecimal(txt_BalanceAftercurentBill.Text), Program.U_ArabicName, "بيع مواد","");
                            //اضافة لمواد الى جدول المواد مع رقم اخر قائمة
                            for (int i = 0; i < dgv_sellingPill.Rows.Count; i++)
                            {
                                //MessageBox.Show(Convert.ToString(dgv_sellingPill.Rows[i].Cells[7].Value));
                                // MessageBox.Show(Program.User_id.ToString());

                                sales.AddSalesOpp(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value),
                                    dgv_sellingPill.Rows[i].Cells[1].Value.ToString(),
                                    dgv_sellingPill.Rows[i].Cells[4].Value.ToString(),
                                    dgv_sellingPill.Rows[i].Cells[5].Value.ToString(),
                                    max_SalesBill_Id,
                                    Program.User_id,
                                      dgv_sellingPill.Rows[i].Cells[3].Value.ToString());

                                prod.update_Pro_Qty(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value), Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[1].Value));

                            }
                            Clear_ProdBoxes();
                            Clear_CustomerBoxes();
                            DT.Clear();
                            dgv_sellingPill.Refresh();
                            txt_prodName.Focus();
                            txt_PaidMony.Clear();
                            no_save = 0;

                            txt_CostmerName.ReadOnly = false;
                            txt_adress.Visible = true;
                            txt_phonNo.Visible = true;
                            txt_BalanceOnCostmer.Visible = true;
                            MessageBox.Show("تم حفظ القائمة بنجاح");

                            ////////////////////  print bill after save
                           // int SalesBill_Id = Convert.ToInt32(PL.frm_bill_management.SalesBill_Id);
                            this.Cursor = Cursors.WaitCursor;
                            print.rpt_print_salles_bill report = new print.rpt_print_salles_bill();
                            report.SetDataSource(sales.get_bill_by_id_for_print(Convert.ToInt32(max_SalesBill_Id)));
                            print.frm_print frm3 = new print.frm_print();
                            frm3.crystalReportViewer1.ReportSource = report;
                            frm3.ShowDialog();
                            this.Cursor = Cursors.Default;


                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                }
                else
                {
                    txt_CostmerName.Focus();
                    return;
                }
            }
            else
            {
                //cust_id = 0;
                try
                {
                    txt_RestMony.Text = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                    if (txt_BalanceOnCostmer.Text == string.Empty) { txt_BalanceOnCostmer.Text = "0"; }
                    new_balance = (Convert.ToDouble(txt_totalAfterDis.Text) - Convert.ToDouble(txt_PaidMony.Text)) + BalanceOnCostmer;
                    if (new_balance >= 0)
                    {
                        lbl_new_balance.Text = "مطلوب";
                        txt_BalanceAftercurentBill.Text = new_balance.ToString();
                    }
                    else
                    {
                        lbl_new_balance.Text = "يطلب";
                        txt_BalanceAftercurentBill.Text = Math.Abs(new_balance).ToString();
                        // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                    }
                    if (no_save == 0)
                    {
                        decimal restmony = (Convert.ToDecimal(txt_totalAfterDis.Text) - Convert.ToDecimal(txt_PaidMony.Text));
                        //احضار اخر قائمة+1 
                        int max_SalesBill_Id = Convert.ToInt32(sales.GetLastSalesBill_Id().Rows[0][0]);

                        sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, x, cust_id, txt_totalBeforDis.Text,
                                        txt_totalAfterDis.Text, txt_total_Dis.Text, txt_PaidMony.Text,
                                       txt_RestMony.Text, Convert.ToDecimal(txt_BalanceAftercurentBill.Text), Program.U_ArabicName, "بيع مواد","");
                        //اضافة لمواد الى جدول المواد مع رقم اخر قائمة
                        for (int i = 0; i < dgv_sellingPill.Rows.Count; i++)
                        {
                            //MessageBox.Show(Convert.ToString(dgv_sellingPill.Rows[i].Cells[7].Value));
                            // MessageBox.Show(Program.User_id.ToString());

                            sales.AddSalesOpp(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value),
                                dgv_sellingPill.Rows[i].Cells[1].Value.ToString(),
                                dgv_sellingPill.Rows[i].Cells[4].Value.ToString(),
                                dgv_sellingPill.Rows[i].Cells[5].Value.ToString(),
                                max_SalesBill_Id,
                                Program.User_id,
                                  dgv_sellingPill.Rows[i].Cells[3].Value.ToString());

                            prod.update_Pro_Qty(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value), Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[1].Value));

                        }
                        cstmr.update_balance(cust_id, restmony);

                        Clear_ProdBoxes();
                        Clear_CustomerBoxes();
                        DT.Clear();
                        dgv_sellingPill.Refresh();
                        txt_prodName.Focus();
                        txt_PaidMony.Clear();
                        no_save = 0;

                        txt_CostmerName.ReadOnly = false;
                        txt_adress.Visible = true;
                        txt_phonNo.Visible = true;
                        txt_BalanceOnCostmer.Visible = true;
                        MessageBox.Show("تم حفظ القائمة بنجاح");

                        ////////////////////  print bill after save
                        //int SalesBill_Id = Convert.ToInt32(PL.frm_bill_management.SalesBill_Id);
                        this.Cursor = Cursors.WaitCursor;
                        print.rpt_print_salles_bill report = new print.rpt_print_salles_bill();
                        report.SetDataSource(sales.get_bill_by_id_for_print(Convert.ToInt32(max_SalesBill_Id)));
                        print.frm_print frm3 = new print.frm_print();
                        frm3.crystalReportViewer1.ReportSource = report;
                        frm3.ShowDialog();
                        this.Cursor = Cursors.Default;
                    }
                    else if (no_save == 1) //اظافة الزبون مباشرتا 
                    {
                        decimal restmony = (Convert.ToDecimal(txt_totalAfterDis.Text) - Convert.ToDecimal(txt_PaidMony.Text));

                        cstmr.InsertCustomer("0", txt_CostmerName.Text, txt_phonNo.Text, txt_adress.Text, "0", txt_BalanceOnCostmer.Text, System.DateTime.Now, "بلا");

                        cust_id = Convert.ToInt32(dal.SelectData("Sp_get_max_cust_id", null).Rows[0][0]);
                        //احضار اخر قائمة+1 
                        int max_SalesBill_Id = Convert.ToInt32(sales.GetLastSalesBill_Id().Rows[0][0]);

                        sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, x, cust_id, txt_totalBeforDis.Text,
                                          txt_totalAfterDis.Text, txt_total_Dis.Text, txt_PaidMony.Text,
                                         txt_RestMony.Text, Convert.ToDecimal(txt_BalanceAftercurentBill.Text), Program.U_ArabicName, "بيع مواد","");
                        //اضافة لمواد الى جدول المواد مع رقم اخر قائمة
                        for (int i = 0; i < dgv_sellingPill.Rows.Count; i++)
                        {
                            //MessageBox.Show(Convert.ToString(dgv_sellingPill.Rows[i].Cells[7].Value));
                            // MessageBox.Show(Program.User_id.ToString());

                            sales.AddSalesOpp(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value),
                                dgv_sellingPill.Rows[i].Cells[1].Value.ToString(),
                                dgv_sellingPill.Rows[i].Cells[4].Value.ToString(),
                                dgv_sellingPill.Rows[i].Cells[5].Value.ToString(),
                                max_SalesBill_Id,
                                Program.User_id,
                                dgv_sellingPill.Rows[i].Cells[3].Value.ToString());

                            prod.update_Pro_Qty(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value), Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[1].Value));

                        }
                        cstmr.update_balance(cust_id, restmony);

                        Clear_ProdBoxes();
                        Clear_CustomerBoxes();
                        DT.Clear();
                        dgv_sellingPill.Refresh();
                        txt_prodName.Focus();
                        txt_PaidMony.Clear();
                        no_save = 0;

                        txt_CostmerName.ReadOnly = false;
                        txt_adress.Visible = true;
                        txt_phonNo.Visible = true;
                        txt_BalanceOnCostmer.Visible = true;
                        MessageBox.Show("تم حفظ القائمة بنجاح");

                        ////////////////////  print bill after save
                     //  int SalesBill_Id = Convert.ToInt32(PL.frm_bill_management.SalesBill_Id);
                        this.Cursor = Cursors.WaitCursor;
                        print.rpt_print_salles_bill report = new print.rpt_print_salles_bill();
                        report.SetDataSource(sales.get_bill_by_id_for_print(Convert.ToInt32(max_SalesBill_Id)));
                        print.frm_print frm3 = new print.frm_print();
                        frm3.crystalReportViewer1.ReportSource = report;
                        frm3.ShowDialog();
                        this.Cursor = Cursors.Default;
                    }

                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Autofill_productText();
                Autofill_salerText();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (rb_mufrad.Checked == false && rb_jumla.Checked == false) { MessageBox.Show("(الرجاء اختيار نوع القائمة (جملة او مفرد "); }
            else if (dgv_sellingPill.RowCount < 1)
            {
                MessageBox.Show("لايمكن حفظ قائمة فارغة..الرجاء التاكد من ادخال المواد المراد بيعها");
                txt_prodName.Focus();

            }


            else if (txt_CostmerName.Text == string.Empty)
            {

                MessageBox.Show("لم يتم كتابة اسم الزبون هل تريد المتابعة وحفظ القائمة بدون اسم ", "تنبيه ", MessageBoxButtons.OK);
                // check_CostmerName2();
                txt_CostmerName.Focus();
                // check_CostmerName();
                txt_CostmerName.Text = ("بلا");
            }
            else if (txt_PaidMony.Text == "" || txt_PaidMony.Text == string.Empty) { MessageBox.Show("الرجاء قم بادخال المبلغ الواصل "); }

            else
            {

                //cust_id = 0;
                try
                {
                    decimal restmony = (Convert.ToDecimal(txt_totalAfterDis.Text) - Convert.ToDecimal(txt_PaidMony.Text));
                    //احضار اخر قائمة+1 
                    int max_SalesBill_Id = Convert.ToInt32(sales.GetLastSalesBill_Id().Rows[0][0]);

                    sales.AddSalesBill(max_SalesBill_Id, dateTimePicker1.Value, x, cust_id, txt_totalBeforDis.Text,
                                      txt_totalAfterDis.Text, txt_total_Dis.Text, txt_PaidMony.Text,
                                     txt_RestMony.Text, Convert.ToDecimal(txt_BalanceAftercurentBill.Text), Program.U_ArabicName, "بيع مواد","");
                    //اضافة لمواد الى جدول المواد مع رقم اخر قائمة
                    for (int i = 0; i < dgv_sellingPill.Rows.Count; i++)
                    {
                        //MessageBox.Show(Convert.ToString(dgv_sellingPill.Rows[i].Cells[7].Value));
                        // MessageBox.Show(Program.User_id.ToString());



                        sales.AddSalesOpp(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value),
                            dgv_sellingPill.Rows[i].Cells[1].Value.ToString(),
                            dgv_sellingPill.Rows[i].Cells[4].Value.ToString(),
                            dgv_sellingPill.Rows[i].Cells[5].Value.ToString(),
                            max_SalesBill_Id,
                            Program.User_id,
                              dgv_sellingPill.Rows[i].Cells[3].Value.ToString());

                        prod.update_Pro_Qty(Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[7].Value), Convert.ToInt32(dgv_sellingPill.Rows[i].Cells[1].Value));
                        // MessageBox.Show("تم حفظ العملية بنجاح");

                        //DT.Columns.Add("اسم المادة");
                        //DT.Columns.Add("العدد");
                        //DT.Columns.Add("وحدةالقياس");
                        //DT.Columns.Add("السعرالاصلي");
                        //DT.Columns.Add("السعر بعدالخصم");
                        //DT.Columns.Add("اجمالي المبلغ");
                        //DT.Columns.Add("اجمالي الخصم");
                        //DT.Columns.Add("ID");


                    }
                    if (no_save == 1) //اظافة الزبون مباشرتا 
                    {
                        cstmr.InsertCustomer("1", txt_CostmerName.Text, txt_phonNo.Text, txt_adress.Text, "0", restmony.ToString(), System.DateTime.Now, "بلا");
                        //cust_id=
                        //تحديث حساب العميل بعد الحفظ
                        // cstmr.update_balance(cust_id, restmony);

                        MessageBox.Show("تم حفظ القائمة بنجاح");
                        Clear_ProdBoxes();
                        Clear_CustomerBoxes();
                        DT.Clear();
                        dgv_sellingPill.Refresh();
                        txt_prodName.Focus();
                        txt_PaidMony.Clear();
                        no_save = 0;


                    }
                    else
                    {
                        //تحديث حساب العميل بعد الحفظ
                        cstmr.update_balance(cust_id, restmony);

                        MessageBox.Show("تم حفظ القائمة بنجاح");
                        Clear_ProdBoxes();
                        Clear_CustomerBoxes();
                        DT.Clear();
                        dgv_sellingPill.Refresh();
                        txt_prodName.Focus();
                        txt_PaidMony.Clear();



                    }


                    //  Close();
                    // this.txt_Bill_ID.Text = sales.GetLastSalesBill_Id().Rows[0][0].ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            //txt_price.Text = string.Format("{0:#,##0}", double.Parse(txt_price.Text));
        }

        private void txt_BalanceOnCostmer_TextChanged(object sender, EventArgs e)
        {
            txt_BalanceOnCostmer.Text = string.Format("{0:#,##0}", double.Parse(txt_BalanceOnCostmer.Text));
        }

        private void txt_BalanceAftercurentBill_TextChanged(object sender, EventArgs e)
        {
            txt_BalanceAftercurentBill.Text = string.Format("{0:#,##0}", double.Parse(txt_BalanceAftercurentBill.Text));

        }
    }
}
