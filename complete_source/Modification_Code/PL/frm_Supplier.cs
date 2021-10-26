using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Sales_Management_2.PL
{

    public partial class frm_Supplier : Form
    {
        string sname;
        string sadress;
        string sphon;
        string bos;
        string bts;
        Form fc = Application.OpenForms["frm_buyingProduct"];
        PL.frm_buyingProduct buy = new frm_buyingProduct();
        BL.Cls_InsertSupplier sup = new BL.Cls_InsertSupplier();
        DataTable DT = new DataTable();
        public frm_Supplier()
        {
            InitializeComponent();
            try
            {
                add_botton_1_to_dgv_es1_des();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


            createDataTable();
        }
        public void CleareBoxes()
        {
            try
            {
                txt_SupplierName_.Clear();
                txt_SuppliePhon.Clear();
                txt_SupplierAdress.Clear();
                txt_BalanceOnSupplier.Text = "0";
                txt_BalanceToSupplier.Text = "0";
                txt_Note.Clear();
                txt_SupplierName_.Focus();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }
        public void createDataTable()
        {
            try
            {
                DT = sup.FillSupplierDGV();
                dgv_Supplier.DataSource = DT;
                this.dgv_Supplier.Columns[0].HeaderText = "رمز المجهز";
                this.dgv_Supplier.Columns[1].HeaderText = "تسلسل المجهز";
                this.dgv_Supplier.Columns[2].HeaderText = "اسم المجهز";
                this.dgv_Supplier.Columns[3].HeaderText = "رقم الهاتف";
                this.dgv_Supplier.Columns[4].HeaderText = "العنوان";
                this.dgv_Supplier.Columns[5].HeaderText = "له";
                this.dgv_Supplier.Columns[6].HeaderText = "عليه";
                this.dgv_Supplier.Columns[7].HeaderText = "تاريخ الادخال";
                this.dgv_Supplier.Columns[8].HeaderText = "الملاحظات";
                this.dgv_Supplier.Columns[9].HeaderText = "البائع الذي قام باخر تعديل";

                txt_series.Text = (this.dgv_Supplier.RowCount + 1).ToString();
                txt_SupplaierID.Text = (this.dgv_Supplier.RowCount + 1).ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            //  عنونة حقول الداتا كرد فيو و تحديد عدد الاعمدة المراد عرضها


        }
        private void dgv_Supplier_DoubleClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل ترغب بتعديل  " + this.dgv_Supplier.CurrentRow.Cells[3].Value.ToString() + " ؟ ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                try
                {
                    txt_SupplaierID.Text = this.dgv_Supplier.CurrentRow.Cells[1].Value.ToString();
                    txt_series.Text = this.dgv_Supplier.CurrentRow.Cells[2].Value.ToString();
                    txt_SupplierName_.Text = this.dgv_Supplier.CurrentRow.Cells[3].Value.ToString();
                    txt_SuppliePhon.Text = this.dgv_Supplier.CurrentRow.Cells[4].Value.ToString();
                    txt_SupplierAdress.Text = this.dgv_Supplier.CurrentRow.Cells[5].Value.ToString();
                    txt_BalanceToSupplier.Text = this.dgv_Supplier.CurrentRow.Cells[6].Value.ToString();
                    txt_BalanceOnSupplier.Text = this.dgv_Supplier.CurrentRow.Cells[7].Value.ToString();
                    dateTimePicker1.Text = this.dgv_Supplier.CurrentRow.Cells[8].Value.ToString();
                    txt_Note.Text = this.dgv_Supplier.CurrentRow.Cells[9].Value.ToString();
                    dgv_Supplier.Sort(dgv_Supplier.Columns[1], ListSortDirection.Ascending);

                    //  dgv_Supplier.Rows.RemoveAt(dgv_Supplier.CurrentRow.Index);
                    txt_SupplierName_.SelectAll();
                    btn_Close.Enabled = false;
                    btn_Save.Enabled = false;
                    // btn_InsertNewSupplier.Enabled = false;
                    btn_DeleteSupplier.Enabled = false;
                    btn_UpdateNewSupplier.Enabled = true;
                    dgv_Supplier.Enabled = false;


                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }
            }


        }


        private void btn_Close_Click(object sender, EventArgs e)
        {
            //معرفة حالة الفورم مفتوح ام مغلق 
            /*      Form fc = Application.OpenForms["frm_buyingProduct"];
            if (fc != null)
               {

                   // buy.txt_SupplierName.Text = this.txt_SupplierName_.Text;
                   buy.txt_SupplierPhone.Text = sadress;
                   buy.txt_SupplierPhone.Text = sphon;
                   buy.txt_BalanceOnSupplier.Text = bos;
                   buy.txt_BalanceToSupplier.Text =bos;
                   // buy.txt_SupplierName.Refresh();
                   // CleareBoxes();

               }*/
            // frm_buyingProduct frm = new frm_buyingProduct();


            // frm_buyingProduct.getmainform .textBox2.Text= "fffgggggggggfff";
            //  frm.textBox2.Text = "fffgggggggggfff";
            // frm.textBox2.Refresh(); 
            //  frm.Update();

            Close();
        }

        private void frm_Supplier_Load(object sender, EventArgs e)
        {
            try
            {
               this.dgv_Supplier.DataSource = sup.SearchForSupplier("");//استدعاء دالى البحث عن طريق تيكست سيرجدوت جينج

            dgv1_design();
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
                    this.dgv_Supplier.Columns.Insert(0, button);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }


        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_SupplierName_.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء ادخال اسم المجهز");
                    txt_SupplierName_.Focus();
                }
                else
                {
                    sname = this.txt_SupplierName_.Text;
                    sadress = this.txt_SupplierAdress.Text;
                    sphon = this.txt_SuppliePhon.Text;
                    bos = txt_BalanceOnSupplier.Text;
                    bts = this.txt_BalanceToSupplier.Text;
                    Form fc = Application.OpenForms["frm_buyingProduct"];
                    if (fc != null)
                    {
                        // دالة الادراج مع البرارميترات
                        sup.InsertSupplier(txt_series.Text, txt_SupplierName_.Text, txt_SuppliePhon.Text, txt_SupplierAdress.Text, txt_BalanceToSupplier.Text, txt_BalanceOnSupplier.Text, DateTime.Parse(dateTimePicker1.Text), txt_Note.Text);
                        createDataTable();//تحديث الداتا كرد فيو
                        MessageBox.Show("تمت الاضافة بنجاح");
                        // CleareBoxes();
                        //btn_Close.Focus();
                        Close();

                    }
                    else
                    {
                        sup.InsertSupplier(txt_series.Text, txt_SupplierName_.Text, txt_SuppliePhon.Text, txt_SupplierAdress.Text, txt_BalanceToSupplier.Text, txt_BalanceOnSupplier.Text, DateTime.Parse(dateTimePicker1.Text), txt_Note.Text);
                        createDataTable();
                        btn_Close.Enabled = true;
                        btn_Save.Enabled = false;
                        //   btn_InsertNewSupplier.Enabled = true;
                        btn_DeleteSupplier.Enabled = false;
                        btn_UpdateNewSupplier.Enabled = false;
                        MessageBox.Show("تمت الاضافة بنجاح");
                        CleareBoxes();
                        btn_Close.Focus();
                    }

                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
        private void txt_SuppliePhon_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_BalanceToSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_BalanceOnSupplier_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txt_SupplierName__KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_SupplierName_.Text != string.Empty)
            {
                //dgv_Supplier.DataSource = sup.SearchForSupplier(txt_Search.Text);
                txt_SuppliePhon.Focus();
            }
        }

        private void txt_SuppliePhon_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_SuppliePhon.Text != string.Empty)
            {
                txt_SupplierAdress.Focus();
            }
        }

        private void txt_SupplierAdress_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_SupplierAdress.Text != string.Empty)
            {
                txt_BalanceToSupplier.Focus();
            }
        }

        private void txt_BalanceToSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_BalanceToSupplier.Text != string.Empty)
            {
                txt_BalanceOnSupplier.Focus();
            }
        }

        private void txt_BalanceOnSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_BalanceOnSupplier.Text != string.Empty)
            {
                txt_Note.Focus();
            }
        }

        private void txt_Note_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_Note.Text != string.Empty)
            {
                btn_Save.Focus();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    lbl_clock.Text = DateTime.Now.ToString("hh:mm:ss tt");
            //    dateTimePicker1.Value = DateTime.Now;
            //    dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            //}
            //catch (Exception ex) { MessageBox.Show(ex.ToString()); }


        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgv_Supplier.DataSource = sup.SearchForSupplier(txt_Search.Text);//استدعاء دالى البحث عن طريق تيكست سيرجدوت جينج

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void btn_InsertNewSupplier_Click(object sender, EventArgs e)
        {
            btn_Save.Enabled = true;
        }

        private void btn_UpdateNewSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                sup.UpdateSupplaier(txt_series.Text, txt_SupplierName_.Text, txt_SuppliePhon.Text, txt_SupplierAdress.Text,
                    txt_BalanceToSupplier.Text, txt_BalanceOnSupplier.Text, dateTimePicker1.Value, txt_Note.Text,
                    Convert.ToInt32(txt_SupplaierID.Text), Program.U_ArabicName);
                MessageBox.Show("تم التعديل بنجاح");
                createDataTable();
                btn_Close.Enabled = true;
                dgv_Supplier.Enabled = true;
                CleareBoxes();
                btn_Save.Enabled = true;
                btn_UpdateNewSupplier.Enabled = false;

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void تعديلمعلوماتهذاالزبونToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل ترغب بتعديل  " + this.dgv_Supplier.CurrentRow.Cells[3].Value.ToString() + " ؟ ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                try
                {
                    txt_SupplaierID.Text = this.dgv_Supplier.CurrentRow.Cells[1].Value.ToString();
                    txt_series.Text = this.dgv_Supplier.CurrentRow.Cells[2].Value.ToString();
                    txt_SupplierName_.Text = this.dgv_Supplier.CurrentRow.Cells[3].Value.ToString();
                    txt_SuppliePhon.Text = this.dgv_Supplier.CurrentRow.Cells[4].Value.ToString();
                    txt_SupplierAdress.Text = this.dgv_Supplier.CurrentRow.Cells[5].Value.ToString();
                    txt_BalanceToSupplier.Text = this.dgv_Supplier.CurrentRow.Cells[6].Value.ToString();
                    txt_BalanceOnSupplier.Text = this.dgv_Supplier.CurrentRow.Cells[7].Value.ToString();
                    dateTimePicker1.Text = this.dgv_Supplier.CurrentRow.Cells[8].Value.ToString();
                    txt_Note.Text = this.dgv_Supplier.CurrentRow.Cells[9].Value.ToString();
                    dgv_Supplier.Sort(dgv_Supplier.Columns[1], ListSortDirection.Ascending);

                    //  dgv_Supplier.Rows.RemoveAt(dgv_Supplier.CurrentRow.Index);
                    txt_SupplierName_.SelectAll();
                    btn_Close.Enabled = false;
                    btn_Save.Enabled = false;
                    //  btn_InsertNewSupplier.Enabled = false;
                    btn_DeleteSupplier.Enabled = false;
                    btn_UpdateNewSupplier.Enabled = true;
                    dgv_Supplier.Enabled = false;


                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

            }
            else
            {
                return;
            }
        }

        private void حذفهذهالزبونToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("سوف يتم حذف " + this.dgv_Supplier.CurrentRow.Cells[2].Value.ToString() + " من قاعدة البيانات نهائيا  ... هل تريد المتابعة ؟ ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                try
                {
                    dgv_Supplier.Rows.RemoveAt(dgv_Supplier.CurrentRow.Index);

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
            else
            {
                return;
            }


        }

        private void dgv_Supplier_MouseDown(object sender, MouseEventArgs e)
        {
            //btn_UpdateNewSupplier_Click( sender,  e);
        }

        private void dgv_Supplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_Supplier_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgv_Supplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (MessageBox.Show("هل ترغب بتعديل  " + this.dgv_Supplier.CurrentRow.Cells[3].Value.ToString() + " ؟ ", "تنبيه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
            //{
            //    try
            //    {
            //        txt_SupplaierID.Text = this.dgv_Supplier.CurrentRow.Cells[1].Value.ToString();
            //        txt_series.Text = this.dgv_Supplier.CurrentRow.Cells[2].Value.ToString();
            //        txt_SupplierName_.Text = this.dgv_Supplier.CurrentRow.Cells[3].Value.ToString();
            //        txt_SuppliePhon.Text = this.dgv_Supplier.CurrentRow.Cells[4].Value.ToString();
            //        txt_SupplierAdress.Text = this.dgv_Supplier.CurrentRow.Cells[5].Value.ToString();
            //        txt_BalanceToSupplier.Text = this.dgv_Supplier.CurrentRow.Cells[6].Value.ToString();
            //        txt_BalanceOnSupplier.Text = this.dgv_Supplier.CurrentRow.Cells[7].Value.ToString();
            //        dateTimePicker1.Text = this.dgv_Supplier.CurrentRow.Cells[8].Value.ToString();
            //        txt_Note.Text = this.dgv_Supplier.CurrentRow.Cells[9].Value.ToString();
            //        dgv_Supplier.Sort(dgv_Supplier.Columns[1], ListSortDirection.Ascending);

            //        //  dgv_Supplier.Rows.RemoveAt(dgv_Supplier.CurrentRow.Index);
            //        txt_SupplierName_.SelectAll();
            //        btn_Close.Enabled = false;
            //        btn_Save.Enabled = false;
            //        // btn_InsertNewSupplier.Enabled = false;
            //        btn_DeleteSupplier.Enabled = false;
            //        btn_UpdateNewSupplier.Enabled = true;
            //        dgv_Supplier.Enabled = false;

            //    }
            //    catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }
            //}


        }

        private void btn_DeleteSupplier_Click(object sender, EventArgs e)
        {
            if (txt_SupplierName_.Text == string.Empty || txt_SupplierName_.Text == "" || txt_SupplierName_.Text == " ")
            {
                MessageBox.Show("يجب اختيار المجهز المراد حذفه  ");
            }
            else
            {
                try
                {
                    DataTable dtsup = new DataTable();
                    dtsup = sup.check_Supplier_name( txt_SupplierName_.Text);



                    if (dtsup.Rows.Count < 1)
                    {
                        MessageBox.Show("هذا المجهز غير موجود ....يرجى التاكد من الاسم ");
                    }
                    else if (MessageBox.Show("هل تريد حذف    " + txt_SupplierName_.Text, "تنــبــيــه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)

                    {
                        int sup_id = Convert.ToInt32(txt_SupplaierID.Text);
                        sup.delete_supplier(Convert.ToInt32(sup_id));

                        this.dgv_Supplier.DataSource = sup.SearchForSupplier("");//استدعاء دالى البحث عن طريق تيكست سيرجدوت جينج
                       // lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();
                        dgv1_design();
                        MessageBox.Show("  تم الحذف بنجاح ");
                        btn_DeleteSupplier.Enabled = false;
                    }
                }
                catch (Exception ex) { MessageBox.Show("  يرجى التاكد من كتابة الرقم بصورة صحيحة "); MessageBox.Show(ex.ToString()); }

            }

        }
        private void arrang_dgv()
        {

        }
        private void dgv1_design()
        {
            try
            {
                dgv_Supplier.BorderStyle = BorderStyle.FixedSingle;
                dgv_Supplier.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_Supplier.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgv_Supplier.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_Supplier.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_Supplier.BackgroundColor = Color.White;

                dgv_Supplier.EnableHeadersVisualStyles = false;
                dgv_Supplier.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgv_Supplier.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_Supplier.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            }

            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }
        }

        private void dgv_Supplier_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                //  دالة ترقيم الروهيدر في الداتاكرد فيو تلقائيا
                // Loops through each row in the DataGridView, and adds the
                // row number to the header
                foreach (DataGridViewRow dGVRow in this.dgv_Supplier.Rows)
                {
                    dGVRow.HeaderCell.Value = String.Format("{0}", dGVRow.Index + 1);
                }

                // This resizes the width of the row headers to fit the numbers
                this.dgv_Supplier.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                dgv_arrange();
                lbl_totalProductsQTY.Text = (this.dgv_Supplier.RowCount).ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
       private void dgv_arrange()
        {
            if(dgv_Supplier.Rows.Count>0)
            {
                try
                {
                dgv_Supplier.Columns[0].Width = 40;
                dgv_Supplier.Columns[1].Visible = false;
                dgv_Supplier.Columns[2].Visible = false;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
              


            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(sup.SearchForSupplier(txt_Search.Text).Rows.Count==0)
                {
                    MessageBox.Show("الاسم غير موجود");
                }
                else
                {
                 dgv_Supplier.DataSource = sup.SearchForSupplier(txt_Search.Text);
                }
               
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void dgv_Supplier_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==0 & dgv_Supplier.Rows.Count>0)
            {
                try
                {
                    txt_SupplaierID.Text = this.dgv_Supplier.CurrentRow.Cells[1].Value.ToString();
                    txt_series.Text = this.dgv_Supplier.CurrentRow.Cells[2].Value.ToString();
                    txt_SupplierName_.Text = this.dgv_Supplier.CurrentRow.Cells[3].Value.ToString();
                    txt_SuppliePhon.Text = this.dgv_Supplier.CurrentRow.Cells[4].Value.ToString();
                    txt_SupplierAdress.Text = this.dgv_Supplier.CurrentRow.Cells[5].Value.ToString();
                    txt_BalanceToSupplier.Text = this.dgv_Supplier.CurrentRow.Cells[6].Value.ToString();
                    txt_BalanceOnSupplier.Text = this.dgv_Supplier.CurrentRow.Cells[7].Value.ToString();
                    dateTimePicker1.Text = this.dgv_Supplier.CurrentRow.Cells[8].Value.ToString();
                    txt_Note.Text = this.dgv_Supplier.CurrentRow.Cells[9].Value.ToString();
                    dgv_Supplier.Sort(dgv_Supplier.Columns[1], ListSortDirection.Ascending);

                    //  dgv_Supplier.Rows.RemoveAt(dgv_Supplier.CurrentRow.Index);
                    txt_SupplierName_.SelectAll();
                    btn_Close.Enabled = false;
                    btn_Save.Enabled = false;
                    // btn_InsertNewSupplier.Enabled = false;
                    btn_DeleteSupplier.Enabled = true;
                    btn_UpdateNewSupplier.Enabled = true;
                    dgv_Supplier.Enabled = false;

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }
            }
        }
    }

    }

