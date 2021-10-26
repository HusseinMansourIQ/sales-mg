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
using System.IO;

namespace Sales_Management_2.PL
{
    public partial class frm_buyingProduct : Form
    {
        private static frm_buyingProduct form;

        BL.Cls_InsertSupplier sup = new BL.Cls_InsertSupplier();
        static void Frm_formclosed(object sender, FormClosedEventArgs e)
        {
            form = null;
        }
        public static frm_buyingProduct getmainform
        {
            get
            {
                if (form == null)
                {
                    form = new frm_buyingProduct();
                    form.FormClosed += new FormClosedEventHandler(Frm_formclosed);
                }
                return form;
            }

        }
        

        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        AutoCompleteStringCollection coll2 = new AutoCompleteStringCollection();
         
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + (AppDomain.CurrentDomain.BaseDirectory).ToString() + "selapp.mdf;" + "Integrated Security=True");
        SqlDataAdapter da;
        SqlDataAdapter da2;
        
        DataTable DT = new DataTable();
        DAL.cls_dal dal = new DAL.cls_dal();
        BL.Cls_AutoFill autofill = new BL.Cls_AutoFill();
        BL.Cls_Purchase purch = new BL.Cls_Purchase();
        BL.Cls_Products prod = new BL.Cls_Products();
        int Supplier_Id;
        int r_index;
        decimal BalanceOnSupplier;
        decimal new_BalanceOnSupplier;
        int f = 0;// flag for supplieres
        int f2 =0;//flag for doublicated products
        string prod_id = null;
        public frm_buyingProduct()
        {
            InitializeComponent();
            createDataTable();
            ResizDGV();
            Autofill_Supplier_NameText();
            Autofill_productText();
            this.dgv_buyingProducts.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.DataBindingComplete);// لاملاء رو هيدرز تلقائيا
                                                                                                                                 //  this.lbl_PorchaseBill_Id.Text = purch.GetLastPurchBill_Id().Rows[0][0].ToString();

        }


        public void Clear_ProdBoxes()
        {
            try
            {
            txt_prodName.Clear();
            txt_BuyingPrice.Clear();
            txt_QTY.Clear();
            txt_Unit.Clear();
            txt_SingleSellPrice.Clear();
            txt_WholeSellPrice.Clear();
            txt_ItemTotalPrice.Clear();
            lbl_ProductId.Text = "";
            txt_prodName.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

           
        }
        //دالة تفريغ خانات المجهز
        public void Clear_SupplierBoxes()
        {
            try
            {
txt_BillNo.Clear();
            txt_SupplierName.Clear();
            txt_SupplierPhone.Clear();
            txt_SupplierAdress.Clear();
            txt_BillNo.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            
        }
        public void Autofill_Supplier_NameText()

        {
            try
            {
 //دالة لتغيير خصائص تيكست بوكس للاملاء التلقائي
            txt_SupplierName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_SupplierName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            da = new SqlDataAdapter("SELECT Supplier_Table.* FROM Supplier_Table order by Supplier_Name asc", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    string name = dt.Rows[j]["Supplier_Name"].ToString();
                    coll.Add(name);

                }
                txt_SupplierName.AutoCompleteCustomSource = coll;
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

        public void check_Supplier_Name()
        {
            try
            {
                string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + domain + "selapp.mdf;" + "Integrated Security=True"))
                {
                    da = new SqlDataAdapter("SELECT Supplier_Table.* FROM Supplier_Table where Supplier_Name='" + txt_SupplierName.Text + "'", connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    // if (txt_SupplierName.Text == string.Empty) { MessageBox.Show(" قم بادخال اسم المجهز رجاءا "); txt_SupplierName.Focus(); }
                    if ((dt.Rows.Count == 1) && (txt_SupplierName.Text != string.Empty))
                    {

                        if (dt.Rows[0]["BalanceOnSupplier"].ToString() == "")
                        {
                            BalanceOnSupplier = 0;
                            txt_BalanceOnSupplier.Text = "0";
                            txt_prodName.Focus();
                        }
                        else
                        {
                            BalanceOnSupplier = Convert.ToDecimal(dt.Rows[0]["BalanceOnSupplier"]);
                            txt_BalanceOnSupplier.Text = dt.Rows[0]["BalanceOnSupplier"].ToString();
                            txt_prodName.Focus();
                        }
                    }
                    else if ((dt.Rows.Count > 1) && (txt_SupplierName.Text != string.Empty))
                    {
                        MessageBox.Show("يوجد اكثر من تاجر بهذا الاسم يرجى كتابة الاسم بالكامل ", "تنــبــيــه ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    else
                    {
                        // MessageBox.Show("هذا المجهز غير موجود..... قم باضافته الى قاعدة البانات اولا ", "تنبيه ", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
                        //if (MessageBox.Show(" هذا المجهز غير موجود.....اذا تريد اضافته الى قاعدة البيانات اضغط كلا  وتاكد من املاء الحقول المهمة او اضغط نعم للحفظ بدون معلومات  ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                        //{
                        //   sup.InsertSupplier("0", txt_SupplierName.Text, txt_SupplierPhone.Text, txt_SupplierAdress.Text, txt_new_balance.Text, txt_BalanceOnSupplier.Text, System.DateTime.Now, "بلا");
                        //   Supplier_Id=Convert.ToInt32( dal.SelectData("sp_get_last_sup_id", null).Rows[0][0]);
                        //    f = 0;
                        //}
                        //else
                        //{
                        f = 1;
                        txt_prodName.Focus();
                        //}
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           
        }
        public void check_ProductName()
        {
          try
          {
                string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
                // دالة فحص اسم الزبون اذا كان موجود سابقا تجعل الفوكس على خانة المنتوجات واذا كان غير موجود يتم اظهار رسالة من خيارين وتشغل من خلال حدث الكي داون ل تيكست اسم الزبون 
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + domain + "selapp.mdf;" + "Integrated Security=True"))
            {
                da = new SqlDataAdapter("SELECT PRODUCT_Table.* FROM PRODUCT_Table where Pro_Name='" + txt_prodName.Text + "'", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                   
                    txt_QTY.Focus();
                }
                else
                {
                  //  MessageBox.Show("هذه المادة غير موجودة في قاعدة البانات وسوف يتم اضافتها الى قاعدة البانات تلقائيا الرجاء التاكد من املاء كافة البيانات الخاصة بهذه المادة ", "تنــبــيــه ", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    // PL.frm_prodManage frm = new PL.frm_prodManage();
                    //  frm.ShowDialog();
                    txt_QTY.Clear();
                    txt_Unit.Clear();
                    txt_BuyingPrice.Clear();
                    txt_SingleSellPrice.Clear();
                    txt_WholeSellPrice.Clear();
                    txt_QTY.Focus();

                    // txt_price.ReadOnly = false;
                    // txt_discount.Text = "0";
                }
            }
          }
          catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            
        }
        public void Get_ProductDataTotextBox()
        {
            try
            {
                string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + domain + "selapp.mdf;" + "Integrated Security=True"))
            {
                SqlCommand command = new SqlCommand("SELECT PRODUCT_Table.* FROM PRODUCT_Table where Pro_Name='" + txt_prodName.Text + "'", connection);
                connection.Open();

                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {
                    txt_QTY.Text = (read["Pro_Qty"].ToString());
                    txt_SingleSellPrice.Text = (read["Single_price"].ToString());
                    txt_BuyingPrice.Text = (read["Pro_Price"].ToString());
                    txt_WholeSellPrice.Text = (read["Wholesale_price"].ToString());
                    txt_Unit.Text = (read["Pro_Unit"].ToString());
                    lbl_ProductId.Text = (read["Pro_Id"].ToString());//////////////????????
                     prod_id= (read["Pro_Id"].ToString());
                    }
                read.Close();
            }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           

        }
        public void Get_SupplierDataTotextBox()
        {
            try
            {
                string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + domain + "selapp.mdf;" + "Integrated Security=True"))
             {
                SqlCommand command = new SqlCommand("SELECT Supplier_Table.* FROM Supplier_Table where Supplier_name='" + txt_SupplierName.Text + "'", connection);
                connection.Open();

                SqlDataReader read = command.ExecuteReader();

                while (read.Read())
                {


                    txt_SupplierPhone.Text = (read["Supplier_Tel"].ToString());
                    txt_SupplierAdress.Text = (read["Supplier_addres"].ToString());
                    txt_new_balance.Text = (read["BalanceToSupplier"].ToString());
                   // txt_BalanceOnSupplier.Text = (read["BalanceOnSupplier"].ToString());
                    Supplier_Id =Convert.ToInt32 (read["Supplier_Id"]);
                    if (read["BalanceOnSupplier"].ToString() == "")
                    {
                        BalanceOnSupplier = 0;
                        txt_BalanceOnSupplier.Text = "0";
                       
                    }
                    else
                    {
                        BalanceOnSupplier = Convert.ToDecimal(read["BalanceOnSupplier"]);
                        if (BalanceOnSupplier < 0)
                        {
                            lbl_to_on.Text = "مطلوب";
                           
                            txt_BalanceOnSupplier.Text = Math.Abs(BalanceOnSupplier).ToString();
                        }
                        else
                        {
                            lbl_to_on.Text = "يطلب";
                            txt_BalanceOnSupplier.Text = BalanceOnSupplier.ToString();
                           // txt_BalanceAftercurentBill.Text = Math.Abs(new_balance).ToString();
                            // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                        }
                        txt_BalanceOnSupplier.Text = read["BalanceOnSupplier"].ToString();
                       // txt_prodName.Focus();
                    }


                }
                read.Close();
                //connection.Close();
            }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           

        }
        public void createDataTable()
        {
            try
            {
 //  عنونة حقول الداتا كرد فيو و تحديد عدد الاعمدة المراد عرضها
            // DT.Columns.Add("ت");

            DT.Columns.Add("اسم المادة");
            DT.Columns.Add("الكمية");
            DT.Columns.Add("وحدة القياس");
            DT.Columns.Add("سعر الشراء");
            DT.Columns.Add("سعر البيع بالمفرد");
            DT.Columns.Add("سعر البيع بالجملة");
            DT.Columns.Add("المجموع");
            DT.Columns.Add("رمز المادة");
            dgv_buyingProducts.DataSource = DT;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           
        }
        public void ResizDGV()
        {
            if(dgv_buyingProducts.Rows.Count>0)
            {        
                try
            {
            
            //this.dgv_buyingProducts.RowHeadersWidth = 96;
            // this.dgv_buyingProducts.Columns[0].Width = 59;
            this.dgv_buyingProducts.Columns[0].Width = 315;
            this.dgv_buyingProducts.Columns[1].Width = 82;
            this.dgv_buyingProducts.Columns[2].Width = 120;
            this.dgv_buyingProducts.Columns[3].Width = 134;
            this.dgv_buyingProducts.Columns[4].Width = 134;
            this.dgv_buyingProducts.Columns[5].Width = 134;
            this.dgv_buyingProducts.Columns[6].Width = 140;
            this.dgv_buyingProducts.Columns[7].Width = 80;

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_buyingProduct_Load(object sender, EventArgs e)
        {
            try
            // btn_new.Enabled = false;try
            {
            this.lbl_PorchaseBill_Id.Text =(Convert.ToInt32( purch.GetLastPurchBill_Id().Rows[0][0].ToString())-1).ToString();
            txt_prodName.Focus();
                btn_new.Enabled = false;

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }


        private void txt_SupplierName_TextChanged(object sender, EventArgs e)
        {
            Get_SupplierDataTotextBox();
        }



        private void txt_QTY_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && txt_QTY.Text != string.Empty)
            {
                txt_BuyingPrice.Focus();


                /*     if(  purch.CheckQTY(Convert.ToInt32(lbl_ProductId.Text),Convert.ToInt32(txt_QTY.Text)).Rows.Count>0)
                     {
                           if (MessageBox.Show("الكمية المدخلة غير متاحة في المخزن ... هل تريد المتابعة ؟ ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
                           {
                            txt_ItemTotalPrice.Text = (Convert.ToDouble(txt_BuyingPrice.Text) * Convert.ToDouble(txt_QTY.Text)).ToString();
                            txt_Unit.Focus();
                           }
                           else 
                           {
                               Get_ProductDataTotextBox();
                               txt_ItemTotalPrice.Text = (Convert.ToDouble(txt_BuyingPrice.Text) * Convert.ToDouble(txt_QTY.Text)).ToString();
                               txt_QTY.SelectAll();

                           }
                     }
                     else
                       {
                           txt_Unit.Focus();

                       }
               */
            }

        }

        private void txt_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //كود لجعل التيكست بوكس يستقبل ارقام وفاصة عشرية فقط
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 9 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {
                e.Handled = true;
            }
        }

        private void txt_SupplierPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 9)
            {

                e.Handled = true;
            }
        }

        private void txt_BuyingPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 9 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {

                e.Handled = true;
            }
        }

        private void txt_SingleSellPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 9 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {

                e.Handled = true;
            }
        }

        private void txt_WholeSellPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 9 && e.KeyChar != Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator))
            {

                e.Handled = true;
            }
        }

        private void txt_PillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 9)
            {

                e.Handled = true;
                dateTimePicker2.Focus();
            }
        }

        private void txt_prodName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && (txt_prodName.Text == string.Empty))
            {
                MessageBox.Show("لطفا.لايمكن ترك اسم المادة فارغ ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //للتاكد من المادة موجودة او لا عند الضغط على انتر
            else if (e.KeyCode != Keys.Enter)
            {
                Get_ProductDataTotextBox();

            }
            else
            {

                check_ProductName();
            }
        }

        private void txt_Unit_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && txt_Unit.Text != string.Empty)
            {
                txt_BuyingPrice.Focus();

            }
        }

        private void txt_BuyingPrice_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_BuyingPrice.Text != string.Empty)
            {
                try
                {             
                    txt_SingleSellPrice.Focus();
                txt_ItemTotalPrice.Text = (Convert.ToDouble(txt_BuyingPrice.Text) * Convert.ToDouble(txt_QTY.Text)).ToString();


                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }
        }

        private void txt_SingleSellPrice_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && txt_SingleSellPrice.Text != string.Empty)
            {
                try
                {

                     txt_WholeSellPrice.Focus();
                txt_ItemTotalPrice.Text = (Convert.ToDouble(txt_BuyingPrice.Text) * Convert.ToDouble(txt_QTY.Text)).ToString();
           }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }
        }

        private void txt_WholeSellPrice_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_WholeSellPrice.Text != string.Empty)
            {
                try
                { btn_Insert.Focus();
                txt_ItemTotalPrice.Text = (Convert.ToDouble(txt_BuyingPrice.Text) * Convert.ToDouble(txt_QTY.Text)).ToString();

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
               

            }
        }

        private void txt_SupplierName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Enter)
            {
                Get_SupplierDataTotextBox();
            }

            else
                check_Supplier_Name();
            // isEnter = true;
            // MessageBox.Show(isEnter.ToString()
        }

        private void txt_prodName_TextChanged(object sender, EventArgs e)
        {
            Get_ProductDataTotextBox();
        }

        private void btn_Insert_Click_1(object sender, EventArgs e)
        {
           

            if (this.dgv_buyingProducts.Rows.Count > 0)
            {
                try
                {
                 for (int i = 0; i < dgv_buyingProducts.Rows.Count; i++)
                {
                    if (dgv_buyingProducts.Rows[i].Cells[0].Value.ToString() == txt_prodName.Text)
                    {
                        f2 = 1;
                    }
                    //else { f2 = 2; }

                }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
               
            }
            else if (this.dgv_buyingProducts.Rows.Count == 0)
            {
                f2 =0;
            }
           
            
            if (txt_prodName.Text == string.Empty || txt_QTY.Text == string.Empty || txt_SingleSellPrice.Text == string.Empty || txt_WholeSellPrice.Text == string.Empty || txt_BuyingPrice.Text == string.Empty)
            {
                MessageBox.Show("الرجاء قم باملاء كافة الحقول المطلوبة ");
            }
            else
            {
              
              if (f2 == 1)
              {
                MessageBox.Show("لا يمكن تكرار نفس المنتج في القائمة الواحدة", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    f2 = 0;
              }
              else if (f2==0)
              {
                    try
                    {
                        decimal Total_ItemPrice = Convert.ToDecimal(txt_BuyingPrice.Text) * Convert.ToDecimal(txt_QTY.Text);
                        InsertProduct();
                        //lbl_ProductId.Text = dal.SelectData("Sp_get_max_prod_Id", null).Rows[0][0].ToString();
                        //prod_id = dal.SelectData("Sp_get_max_prod_Id", null).Rows[0][0].ToString();
                        //MessageBox.Show(prod_id);
                        // ادراج الطلبية الى الداتاكردفيو وحسب الترتيب المطلوب 
                        DataRow dr = DT.NewRow();
                // dr[0] = dgv_buyingProducts.RowCount;
                dr[0] = txt_prodName.Text;
                dr[1] = txt_QTY.Text;
                dr[2] = txt_Unit.Text;
                dr[3] = txt_BuyingPrice.Text;
                dr[4] = txt_SingleSellPrice.Text;
                dr[5] = txt_WholeSellPrice.Text;
                dr[6] = Total_ItemPrice;
                dr[7] = prod_id;
                DT.Rows.Add(dr);
                       
               
               Clear_ProdBoxes();
                txt_prodName.Focus();


                //حساب المجموع الخاص بمجموعة حقول ضمن داتا كردفيو
                lbl_BillTotalPrice.Text = (from DataGridViewRow row in dgv_buyingProducts.Rows
                                           where row.Cells[6].FormattedValue.ToString() != string.Empty
                                           select Convert.ToDouble(row.Cells[6].FormattedValue)).Sum().ToString();

                btn_Insert.Text = "ادراج";
                btn_UpdatePill.Enabled = true;    
                btn_DeletePill.Enabled = true;
                btn_SavePill.Enabled = true;
                dgv_buyingProducts.ReadOnly = false;

                        ResizDGV();

                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                    

                }

            }
            
        }

        private void dgv_buyingProducts_DoubleClick(object sender, EventArgs e)
        {
            if (this.dgv_buyingProducts.Rows.Count > 0)
            {
               
                //MessageBox.Show(r_index.ToString());
                try
                {
                    r_index = this.dgv_buyingProducts.CurrentRow.Index;

                    txt_prodName.Text = this.dgv_buyingProducts.Rows[r_index].Cells[0].Value.ToString();
                    txt_QTY.Text = this.dgv_buyingProducts.Rows[r_index].Cells[1].Value.ToString();
                    txt_Unit.Text = this.dgv_buyingProducts.Rows[r_index].Cells[2].Value.ToString();
                    txt_BuyingPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[3].Value.ToString();
                    txt_SingleSellPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[4].Value.ToString();
                    txt_WholeSellPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[5].Value.ToString();
                    txt_ItemTotalPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[6].Value.ToString();
                    dgv_buyingProducts.Rows.RemoveAt(r_index);
                    dgv_buyingProducts.Sort(dgv_buyingProducts.Columns[0], ListSortDirection.Ascending);
                    txt_QTY.Focus();
                    btn_UpdatePill.Enabled = false;
                    btn_Insert.Text = "حفظ التعديل";
                    btn_DeletePill.Enabled = false;
                    btn_cancel_update.Enabled = true;
                    btn_SavePill.Enabled = false;
                    dgv_buyingProducts.ReadOnly = true;
                    MessageBox.Show("الرجاء اكمال التعديل والضغط على مفتاح حفظ التعديل للمتابعة");
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
        }

        private void dgv_buyingProducts_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
 lbl_BillTotalPrice.Text = (from DataGridViewRow row in dgv_buyingProducts.Rows
                                       where row.Cells[6].FormattedValue.ToString() != string.Empty
                                       select Convert.ToDouble(row.Cells[6].FormattedValue)).Sum().ToString();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           
        }
        private void DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {//  دالة ترقيم الروهيدر في الداتاكرد فيو تلقائيا
            // Loops through each row in the DataGridView, and adds the
            // row number to the header
            foreach (DataGridViewRow dGVRow in this.dgv_buyingProducts.Rows)
            {
                dGVRow.HeaderCell.Value = String.Format("{0}", dGVRow.Index + 1);
            }

            // This resizes the width of the row headers to fit the numbers
            this.dgv_buyingProducts.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void تعديلهذهالفقرةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgv_buyingProducts.Rows.Count > 0)
            {
                try
                {
                    txt_prodName.Text = this.dgv_buyingProducts.Rows[r_index].Cells[0].Value.ToString();
                    txt_QTY.Text = this.dgv_buyingProducts.Rows[r_index].Cells[1].Value.ToString();
                    txt_Unit.Text = this.dgv_buyingProducts.Rows[r_index].Cells[2].Value.ToString();
                    txt_BuyingPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[3].Value.ToString();
                    txt_SingleSellPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[4].Value.ToString();
                    txt_WholeSellPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[5].Value.ToString();
                    txt_ItemTotalPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[6].Value.ToString();
                    dgv_buyingProducts.Rows.RemoveAt(r_index);
                    dgv_buyingProducts.Sort(dgv_buyingProducts.Columns[0], ListSortDirection.Ascending);
                    txt_QTY.Focus();
                    btn_UpdatePill.Enabled = false;
                    btn_Insert.Text = "حفظ التعديل";
                    btn_DeletePill.Enabled = false;
                    btn_cancel_update.Enabled = true;
                    btn_SavePill.Enabled = false;

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }
            //dgv_buyingProducts_DoubleClick(sender, e);
        }

        private void حذفهذهالفقرةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
             dgv_buyingProducts.Rows.RemoveAt(r_index);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            

        }

        private void حذفالكلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هذا الزبون غير موجود هل تريج اضافته الى قاعدة البانات ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                try
                {
 DT.Clear();
                dgv_buyingProducts.Refresh();
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
               

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txt_SupplierAdress_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_SupplierAdress_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txt_SupplierAdress_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_BalanceToSupplier_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btn_SavePill_Click(object sender, EventArgs e)
        {
            try
            {
                //check_Supplier_Name();

                //if (txt_BillNo.Text == string.Empty || txt_SupplierName.Text==string.Empty)
                //{
                //    btn_new.Enabled = false;
                //    btn_SavePill.Enabled = true;
                //    //btn_UpdatePill.Enabled = false;
                //    MessageBox.Show("رجاءا ... قم بادخال اسم المجهز و رقم وتاريخ الفاتورة  قبل الحفظ  ");
                //    return;
                //}


                //else if (txt_PaidMony.Text == string.Empty)
                //{
                  
                //    MessageBox.Show("رجاءا ... قم بادخال  المبلغ  الواصل  ");
                //    //return;
                //}


                //else if (f==1)
                // {
                //     sup.InsertSupplier("0", txt_SupplierName.Text, txt_SupplierPhone.Text, txt_SupplierAdress.Text, txt_new_balance.Text, txt_BalanceOnSupplier.Text, System.DateTime.Now, "بلا");

                //     MessageBox.Show("رجاءا ... تم اضافة المجهز بالى قاعدة البيانات   ");

                // }



                //check_Supplier_Name();
                 if (dgv_buyingProducts.Rows.Count < 1)
                {
                        MessageBox.Show("لا يمكن حفظ قائمة فارغة");
                        btn_new.Enabled = false;
                        btn_SavePill.Enabled = true;

                }


                //else if (f == 1)
                //{
                //            sup.InsertSupplier("0", txt_SupplierName.Text, txt_SupplierPhone.Text, txt_SupplierAdress.Text, txt_new_balance.Text, txt_BalanceOnSupplier.Text, System.DateTime.Now, "بلا");
                //            Supplier_Id = Convert.ToInt32(dal.SelectData("sp_get_last_sup_id", null).Rows[0][0]);
                           
                //            txt_RestMony.Text = (Convert.ToDouble(lbl_BillTotalPrice.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                //            if (txt_BalanceOnSupplier.Text == string.Empty) { txt_BalanceOnSupplier.Text = "0"; }
                //            new_BalanceOnSupplier = (Convert.ToDecimal(txt_BalanceOnSupplier.Text) + Convert.ToDecimal(txt_RestMony.Text));

                //            //دالة اضافة معلومات الفاتورة الى جدول الفواتير  
                //            purch.AddPurchaseBill(Convert.ToInt32(lbl_PorchaseBill_Id.Text), Convert.ToDateTime(DateTime.Now),
                //                Convert.ToInt32(Supplier_Id), txt_BillNo.Text, dateTimePicker2.Value, lbl_BillTotalPrice.Text,
                //                Program.U_ArabicName, txt_PaidMony.Text, txt_RestMony.Text, txt_new_balance.Text);
                           


                //            //////////////////////              
                //            //دالة اضافة عمليات الشراء الى جدول العمليات
                //            for (int i = 0; i < dgv_buyingProducts.Rows.Count; i++)
                //            {
                                
   
                //                  purch.Sp_AddPurchaseOpp(
                //                  Convert.ToInt32(dgv_buyingProducts.Rows[i].Cells[7].Value),
                //                  Convert.ToInt32(lbl_PorchaseBill_Id.Text),
                //                  Convert.ToInt32(dgv_buyingProducts.Rows[i].Cells[1].Value),
                //                  dgv_buyingProducts.Rows[i].Cells[3].Value.ToString(),
                //                  Convert.ToInt32(txt_BillNo.Text),
                //                  Convert.ToString(Program.U_ArabicName),
                //                  Convert.ToDateTime(DateTime.Now),
                //                  dgv_buyingProducts.Rows[i].Cells[4].Value.ToString(),//single pricw
                //                  dgv_buyingProducts.Rows[i].Cells[5].Value.ToString(), //wholeprice
                //                  dgv_buyingProducts.Rows[i].Cells[3].Value.ToString() //BuyingPrice
                //                                );

                               

                //            }
                //    MessageBox.Show("تم حفظ القائمة واضافة المجهز الى قاعدة البيانات   ");
                  
                //    DT.Clear();
                //     //dgv_buyingProducts.DataSource = null;
                //    f2 = 0;
                   
                //    txt_SupplierName.Clear();
                //    txt_BillNo.Clear();
                //    txt_BillNo.Focus();

                //    Autofill_Supplier_NameText();
                //    Autofill_productText();
                   


                //    btn_new.Enabled = true;
                //    btn_SavePill.Enabled = false;
                //    btn_UpdatePill.Enabled = false;
                //    btn_DeletePill.Enabled = false;
                //}

                 else
                 {
                            //txt_RestMony.Text = (Convert.ToDouble(lbl_BillTotalPrice.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                            //if (txt_BalanceOnSupplier.Text == string.Empty) { txt_BalanceOnSupplier.Text = "0"; }
                            //new_BalanceOnSupplier = (Convert.ToDecimal(txt_BalanceOnSupplier.Text) + Convert.ToDecimal(txt_RestMony.Text));

                            ////دالة اضافة معلومات الفاتورة الى جدول الفواتير  
                            //purch.AddPurchaseBill(Convert.ToInt32(lbl_PorchaseBill_Id.Text), Convert.ToDateTime(DateTime.Now),
                            //    Convert.ToInt32(Supplier_Id), txt_BillNo.Text, dateTimePicker2.Value, lbl_BillTotalPrice.Text,
                            //    Program.U_ArabicName, txt_PaidMony.Text, txt_RestMony.Text, txt_new_balance.Text);
                            


                            //////////////////////              
                            //دالة اضافة عمليات الشراء الى جدول العمليات
                            for (int i = 0; i < dgv_buyingProducts.Rows.Count; i++)
                            {
                                purch.Sp_AddPurchaseOpp(
                                  Convert.ToInt32(dgv_buyingProducts.Rows[i].Cells[7].Value),
                                  Convert.ToInt32(lbl_PorchaseBill_Id.Text),
                                  Convert.ToInt32(dgv_buyingProducts.Rows[i].Cells[1].Value),
                                  dgv_buyingProducts.Rows[i].Cells[3].Value.ToString(),
                                  Convert.ToInt32(txt_BillNo.Text),
                                  Convert.ToString(Program.U_ArabicName),
                                  Convert.ToDateTime(DateTime.Now),
                                  dgv_buyingProducts.Rows[i].Cells[4].Value.ToString(),//single pricw
                                  dgv_buyingProducts.Rows[i].Cells[5].Value.ToString(), //wholeprice
                                   dgv_buyingProducts.Rows[i].Cells[3].Value.ToString() //BuyingPrice
                                                );

                               
                            }
                    MessageBox.Show("تم حفظ هذه القائمة بنجاح");
                    // dgv_buyingProducts.DataSource = null;
                   

                    DT.Clear();
                    f2 = 0;
                    Autofill_productText();
                    // Clear_ProdBoxes();
                    //txt_SupplierName.Clear();
                    txt_BillNo.Text="0";
                    //txt_prodName.Text="";
                    txt_prodName.Focus();
                    //Autofill_Supplier_NameText();
                   
                   
                    btn_new.Enabled = true;
                    btn_SavePill.Enabled = false;
                    btn_UpdatePill.Enabled = false;
                    btn_DeletePill.Enabled = false;
                }


            }
            catch (Exception ex) {MessageBox.Show(ex.ToString());}
        }

        private void lbl_BillTotalPrice_Click(object sender, EventArgs e)
        {

        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد فتح فاتورة جديدة ....  تاكد من حفظ الفاتورة السابقة قبل المتابعة للتاكيد اضغط (yes) للتراجع اضغط (no) ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)

                try
                {
                    int f2 = 0;
                  //  createDataTable();
                  //  ResizDGV();
                    Autofill_Supplier_NameText();
                    Autofill_productText();

                    this.lbl_PorchaseBill_Id.Text = purch.GetLastPurchBill_Id().Rows[0][0].ToString();
                    txt_BillNo.Focus();

                    this.lbl_PorchaseBill_Id.Text = purch.GetLastPurchBill_Id().Rows[0][0].ToString();
            btn_SavePill.Enabled = true;
            btn_new.Enabled = false;
            Clear_SupplierBoxes();
            Clear_ProdBoxes();
            DT.Clear();
            txt_BillNo.Focus();
            dgv_buyingProducts.Refresh();
                txt_PaidMony.Clear();
                txt_RestMony.Text = "0";
                txt_BalanceOnSupplier.Text = "0";
               
                btn_UpdatePill.Enabled = true;
                btn_DeletePill.Enabled = true;



            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                btn_SavePill.Enabled = true;
            }
        }

        private void dgv_buyingProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(this.dgv_buyingProducts.Rows.Count>0)
            {
                try
                { 
                    r_index = e.RowIndex;

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
               
              
            }
        }

        public void InsertProduct()
        {
            try
            {
                string domain = (AppDomain.CurrentDomain.BaseDirectory).ToString();
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= " + domain + "selapp.mdf;" + "Integrated Security=True"))
            {
                da = new SqlDataAdapter("SELECT PRODUCT_Table.* FROM PRODUCT_Table where Pro_Name='" + txt_prodName.Text + "'", connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count < 1)
                {
                    prod.insertProduct(txt_prodName.Text, Convert.ToInt32(txt_QTY.Text), txt_Unit.Text, txt_BuyingPrice.Text, txt_SingleSellPrice.Text, txt_WholeSellPrice.Text, Convert.ToDateTime(DateTime.Now), Convert.ToString(Program.U_ArabicName),"بلا");
                        
                        lbl_ProductId.Text = dal.SelectData("Sp_get_max_prod_Id", null).Rows[0][0].ToString();
                        prod_id = dal.SelectData("Sp_get_max_prod_Id", null).Rows[0][0].ToString();

                        MessageBox.Show( "تم ادراج المادة   "+ txt_prodName.Text +" بنجاح ");
                      //  MessageBox.Show(prod_id);
                    }
                }
                
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void txt_QTY_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Unit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_prodName_KeyPress(object sender, KeyPressEventArgs e)
        {



        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txt_SupplierName.Focus();

            }

        }

        private void txt_SingleSellPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_UpdatePill_Click(object sender, EventArgs e)
        { 
            if (dgv_buyingProducts.Rows.Count>0)
            {
                try
                {
                    txt_prodName.Text = this.dgv_buyingProducts.Rows[r_index].Cells[0].Value.ToString();
                    txt_QTY.Text = this.dgv_buyingProducts.Rows[r_index].Cells[1].Value.ToString();
                    txt_Unit.Text = this.dgv_buyingProducts.Rows[r_index].Cells[2].Value.ToString();
                    txt_BuyingPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[3].Value.ToString();
                    txt_SingleSellPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[4].Value.ToString();
                    txt_WholeSellPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[5].Value.ToString();
                    txt_ItemTotalPrice.Text = this.dgv_buyingProducts.Rows[r_index].Cells[6].Value.ToString();
                    dgv_buyingProducts.Rows.RemoveAt(r_index);
                    dgv_buyingProducts.Sort(dgv_buyingProducts.Columns[0], ListSortDirection.Ascending);
                    txt_QTY.Focus();
                    btn_UpdatePill.Enabled = false;
                    btn_Insert.Text = "حفظ التعديل";
                    btn_DeletePill.Enabled = false;
                    btn_cancel_update.Enabled = true;
                    btn_SavePill.Enabled = false;
                    dgv_buyingProducts.ReadOnly = true;
                    MessageBox.Show("الرجاء اكمال التعديل والضغط على مفتاح حفظ التعديل للمتابعة");


                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }


        }

        private void btn_cancel_update_Click(object sender, EventArgs e)
        {
            btn_UpdatePill.Enabled = true;
            btn_Insert.Text = "ادراج";
            btn_DeletePill.Enabled = true;
            btn_cancel_update.Enabled = false;
            btn_SavePill.Enabled = true;
            dgv_buyingProducts.ReadOnly = false;

        }

        private void btn_DeletePill_Click(object sender, EventArgs e)
        {
            if(dgv_buyingProducts.Rows.Count>0)
            {
                try
                {
                    dgv_buyingProducts.Rows.RemoveAt(r_index);
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }
          
        }

        private void txt_PaidMony_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && txt_PaidMony.Text != string.Empty)
            {
                try
                {
                btn_SavePill.Focus();
                txt_RestMony.Text = (Convert.ToDouble(lbl_BillTotalPrice.Text) - Convert.ToDouble(txt_PaidMony.Text)).ToString();
                if (txt_BalanceOnSupplier.Text == string.Empty) { txt_BalanceOnSupplier.Text = "0"; }
                new_BalanceOnSupplier = (Convert.ToDecimal(txt_BalanceOnSupplier.Text) + Convert.ToDecimal(txt_RestMony.Text)) ;
                if (new_BalanceOnSupplier >= 0)
                {
                    label13.Text = "يطلب";
                   txt_new_balance .Text = new_BalanceOnSupplier.ToString();
                }
                else
                {
                    label13.Text = "مطلوب";
                    txt_new_balance.Text = Math.Abs(new_BalanceOnSupplier).ToString();
                    // txt_BalanceOnCostmer.Text = ((Convert.ToInt32(txt_BalanceOnCostmer.Text) * Convert.ToInt32(txt_BalanceOnCostmer.Text)) / Convert.ToInt32(txt_BalanceOnCostmer.Text)).ToString();

                }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //dal = new DAL.cls_dal();
            //lbl_ProductId.Text = dal.SelectData("Sp_get_max_prod_Id2", null).Rows[0][0].ToString();
            //prod_id = dal.SelectData("Sp_get_max_prod_Id", null).Rows[0][0].ToString();
            //MessageBox.Show(prod_id);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }

}
