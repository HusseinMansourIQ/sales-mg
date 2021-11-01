using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_Management_2.PL
{
    public partial class frm_Debts : Form
    {
        BL.Cls_Customeres cost = new BL.Cls_Customeres();
        DAL.cls_dal dal = new DAL.cls_dal();
        BL.cls_depts depts = new BL.cls_depts();
        BL.Cls_Sales sales = new BL.Cls_Sales();
        string opp = null;
        DataTable dgv_dt = new DataTable();

        public frm_Debts()
        {
            InitializeComponent();
           


            try
            {
                CreateTable();
                this.dgv_Costomeres.DataSource = cost.SearchForCustomer_depts(" ");//استدعاء دالى البحث عن طريق تيكست سيرجدوت جينج
                add_botton_2_to_dgv_es1_des();
                double sum = 0;
                double daain = 0;
                double madeen = 0;
                if (dgv_Costomeres.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_Costomeres.Rows.Count; ++i)
                    {
                        if ((Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value) < 0))
                        {
                            daain += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                        }
                        else if ((Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value) > 0))
                        {
                            madeen += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                        }
                        sum += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                    }
                    lbl_dain.Text = string.Format("{0:#,##0}", double.Parse(Math.Abs(daain).ToString()));
                    lbl_madeen.Text = string.Format("{0:#,##0}", double.Parse(Math.Abs(madeen).ToString()));
                    lbl_sum.Text = string.Format("{0:#,##0}", double.Parse(sum.ToString()));



                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }





        }
        public void CleareBoxes()
        {
            //txt_CustomerName.Clear();
            //txt_CustomerPhon.Clear();
            //txt_CustomerAdress.Clear();
            //txt_BalanceOnCustomer.Text = "0";
            //txt_BalanceToCustomer.Text = "0";
            //txt_Note.Clear();
            //txt_CustomerName.Focus();
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_Debts_Load(object sender, EventArgs e)
        {
            dt_to.Value = System.DateTime.Now;
            rbt_all.Checked = true;
            //dt_from.Value=
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                dt_to.Value = DateTime.Now;
                dt_to.CustomFormat = "yyyy-MM-dd";

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            ////لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            //if (e.KeyCode == Keys.Enter && txt_CustomerName.Text != string.Empty)
            //{
            //    txt_CustomerPhon.Focus();
            //}

        }

        private void txt_CostomerPhon_KeyDown(object sender, KeyEventArgs e)
        {
            ////لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            //if (e.KeyCode == Keys.Enter && txt_CustomerPhon.Text != string.Empty)
            //{
            //    txt_CustomerAdress.Focus();
            //}

        }

        private void txt_CostomerAdress_KeyDown(object sender, KeyEventArgs e)
        {
            ////لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            //if (e.KeyCode == Keys.Enter && txt_CustomerAdress.Text != string.Empty)
            //{
            //    txt_Note.Focus();
            //}

        }

        private void txt_Note_KeyDown(object sender, KeyEventArgs e)
        {
            ////لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            //if (e.KeyCode == Keys.Enter && txt_Note.Text != string.Empty)
            //{
            //    btn_Insert.Focus();
            //}

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
            if (rbt_all.Checked == true)
                try
                {
                    this.dgv_Costomeres.DataSource = dal.SelectData("sp_get_all_debts", null);//استدعاء دالى البحث عن طريق تيكست سيرجدوت جينج
                    double sum = 0;
                    double daain = 0;
                    double madeen = 0;
                    if (dgv_Costomeres.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgv_Costomeres.Rows.Count; ++i)
                        {
                            if ((Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value) < 0))
                            {
                                daain += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                            }
                            else if ((Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value) > 0))
                            {
                                madeen += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                            }
                            sum += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                        }
                        lbl_dain.Text = string.Format("{0:#,##0}", double.Parse(Math.Abs(daain).ToString()));
                        lbl_madeen.Text = string.Format("{0:#,##0}", double.Parse(Math.Abs(madeen).ToString()));
                        lbl_sum.Text = string.Format("{0:#,##0}", double.Parse(sum.ToString()));


                    }

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }
            else
            {
                try
                {
                    dgv_Costomeres.DataSource = depts.search_for_debts(txt_Search.Text, dt_from.Value, dt_to.Value, Convert.ToInt32(txt_mony.Text), opp);
                    double sum = 0;
                    double daain = 0;
                    double madeen = 0;
                    if (dgv_Costomeres.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgv_Costomeres.Rows.Count; ++i)
                        {
                            if ((Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value) < 0))
                            {
                                daain += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                            }
                            else if ((Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value) > 0))
                            {
                                madeen += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                            }
                            sum += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                        }
                        lbl_dain.Text = string.Format("{0:#,##0}", double.Parse(Math.Abs(daain).ToString()));
                        lbl_madeen.Text = string.Format("{0:#,##0}", double.Parse(Math.Abs(madeen).ToString()));
                        lbl_sum.Text = string.Format("{0:#,##0}", double.Parse(sum.ToString()));



                    }

                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

                //cost.InsertCustomer(txt_series.Text, txt_CustomerName.Text, txt_CustomerPhon.Text, txt_CustomerAdress.Text, txt_BalanceToCustomer.Text, txt_BalanceOnCustomer.Text, DateTime.Parse(dateTimePicker1.Text), txt_Note.Text);
                //MessageBox.Show("تمت الاضافة بنجاح");
                //dgv_Costomeres.DataSource = cost.FillCustomeresDGV();
                //txt_series.Text = (this.dgv_Costomeres.RowCount + 1).ToString();

                //CleareBoxes();

            }
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgv_Costomeres.DataSource = cost.SearchForCustomer_depts(txt_Search.Text);//استدعاء دالة البحث عن طريق تيكست سيرجدوت جينج

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }

        private void dgv_Costomeres_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Costomeres.Rows.Count > 0 & e.ColumnIndex == 0)
            {
                try
                {
                    string cust_name = this.dgv_Costomeres.Rows[e.RowIndex].Cells[1].Value.ToString();

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

        private void dgv_Costomeres_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //  دالة ترقيم الروهيدر في الداتاكرد فيو تلقائيا
            // Loops through each row in the DataGridView, and adds the
            // row number to the header
            try
            {
                foreach (DataGridViewRow dGVRow in this.dgv_Costomeres.Rows)
                {
                    dGVRow.HeaderCell.Value = String.Format("{0}", dGVRow.Index + 1);
                }

                // This resizes the width of the row headers to fit the numbers
                this.dgv_Costomeres.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                if (dgv_Costomeres.Rows.Count > 0)
                {
                    this.dgv_Costomeres.Columns[0].Width = 60;
                    this.dgv_Costomeres.Columns[1].Width = 225;
                    this.dgv_Costomeres.Columns[2].Width = 150;
                    this.dgv_Costomeres.Columns[3].Width = 150;
                    this.dgv_Costomeres.Columns[4].Width = 225;
                    //this.dgv_Costomeres.Columns[8].Visible = false;
                    //this.dgv_Costomeres.Columns[8].Visible = false;
                    lbl_count.Text = dgv_Costomeres.Rows.Count.ToString();

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }

        }

        private void rbt_equal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbt_equal.Checked == true)
            { opp = "="; }
            //else if (rbt_larger.Checked == true)
            //    opp = ">";
            //else if (rbt_less.Checked == true)
            //    opp = "<";
            //MessageBox.Show(opp);
        }

        private void rbt_larger_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbt_equal.Checked == true)
            //{ opp = "="; }
            //else 
            if (rbt_larger.Checked == true)
                opp = ">";
            //else if (rbt_less.Checked == true)
            //    opp = "<";


        }

        private void rbt_less_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbt_equal.Checked == true)
            //{ opp = "="; }
            //else if (rbt_larger.Checked == true)
            //    opp = ">";
            //else
            if (rbt_less.Checked == true)
                opp = "<";


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dgv_Costomeres.Rows.Count>0)
            {
                

                try
                {
                    dgv_dt.Clear();
                   
                    for (int i = 0; i < dgv_Costomeres.Rows.Count; i++)
                    {
                        
                        dgv_dt.Rows.Add();
                        //== null ? DBNull.Value : dgv_Costomeres.Cells[i].Value;
                        dgv_dt.Rows[i][0] = dgv_Costomeres.Rows[i].Cells[1].Value;
                        dgv_dt.Rows[i][1] = dgv_Costomeres.Rows[i].Cells[2].Value;
                        dgv_dt.Rows[i][2] = dgv_Costomeres.Rows[i].Cells[3].Value;
                        dgv_dt.Rows[i][3] = dgv_Costomeres.Rows[i].Cells[4].Value;
                        dgv_dt.Rows[i][4] = dgv_Costomeres.Rows[i].Cells[6].Value;
                        dgv_dt.Rows[i][5] = dgv_Costomeres.Rows[i].Cells[7].Value;
                        dgv_dt.Rows[i][6] = dgv_Costomeres.Rows[i].Cells[9].Value;

                    }
                    this.Cursor = Cursors.WaitCursor;
                    print.rpt_print_debts report = new print.rpt_print_debts();
                    report.Database.Tables["debts"].SetDataSource(dgv_dt);
                    print.frm_print frm3 = new print.frm_print();
                    frm3.crystalReportViewer1.ReportSource = report;
                    frm3.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
        }

        private void lbl_count_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgv_Costomeres.DataSource = depts.search_debts_by_period(comboBox1.Text);//استدعاء دالى البحث عن طريق تيكست سيرجدوت جينج

                double sum = 0;
                double daain = 0;
                double madeen = 0;
                if (dgv_Costomeres.Rows.Count > 0)
                {
                    for (int i = 0; i < dgv_Costomeres.Rows.Count; ++i)
                    {
                        if ((Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value) < 0))
                        {
                            daain += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                        }
                        else if ((Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value) > 0))
                        {
                            madeen += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                        }
                        sum += Convert.ToDouble(dgv_Costomeres.Rows[i].Cells[4].Value);
                    }
                    lbl_dain.Text = string.Format("{0:#,##0}", double.Parse(Math.Abs(daain).ToString()));
                    lbl_madeen.Text = string.Format("{0:#,##0}", double.Parse(Math.Abs(madeen).ToString()));
                    lbl_sum.Text = string.Format("{0:#,##0}", double.Parse(sum.ToString()));
                    // txt_BalanceOnCostmer.Text = string.Format("{0:#,##0}", double.Parse(txt_BalanceOnCostmer.Text));

                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }
        private void CreateTable()
        {// انشاء دالة نوع جدول لغرض املءها من الداتاكرد فيو وارسالها الى السيرفر
         // DataTable dgv_dt = new DataTable();
            dgv_dt.Columns.AddRange(new DataColumn[7]
                {
            new DataColumn("Cust_name", typeof(string)),
            new DataColumn("Cust_Tel", typeof(string)),
            new DataColumn("Cust_addres", typeof(string)),
            new DataColumn("BalanceOnCostmer", typeof(string)),
            new DataColumn("SalesBill_Date", typeof(string)),
            new DataColumn("Bill_category", typeof(string)),
            new DataColumn("SalesBill_Id", typeof(string)),

            }
                );
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
                    this.dgv_Costomeres.Columns.Insert(0, button);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }
        }

        private void rbt_all_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
