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
    public partial class frm_purchase_bill_manag : Form

    {
        DAL.cls_dal dal = new DAL.cls_dal();
        BL.Cls_Purchase purch = new BL.Cls_Purchase();
        public static int PurchaseBill_Id = 0;
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



        public frm_purchase_bill_manag()
        {
            InitializeComponent();
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
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           
        }

        private void frm_purchase_bill_manag_Load(object sender, EventArgs e)
        {
            form_sizing();
            try
            {
                lbl_username.Text = Program.U_ArabicName;

                this.dgv_es1_des.DataSource = dal.SelectData("sp_get_all_purch_bill", null);

                // lbl_username.Text = frm_login.arabic_name;
                // string full_name = "(" + frm_login.username + "/" + frm_login.pass + ")" + frm_login.arabic_name;
 lbl_count.Text = dgv_es1_des.Rows.Count.ToString();

            add_botton_1_to_dgv_es1_des();
            add_botton_2_to_dgv_es1_des();
            dgv_es1_des_hide_colomns();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }

           


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
            if(dgv_es1_des.Rows.Count>0)
            {
                try
                {
                    //this.dgv_es1_des.Columns[2].Visible = false;
                    this.dgv_es1_des.Columns[4].Visible = false;
                    this.dgv_es1_des.Columns[0].Width = 50;
                    this.dgv_es1_des.Columns[1].Width = 50;
                    this.dgv_es1_des.Columns[2].Width = 50;
                    this.dgv_es1_des.Columns[3].Width = 100;

                    this.dgv_es1_des.Columns[4].Width = 90;
                    this.dgv_es1_des.Columns[5].Width = 200;
                    this.dgv_es1_des.Columns[6].Width = 80;
                    this.dgv_es1_des.Columns[7].Width = 100;
                    this.dgv_es1_des.Columns[8].Width = 100;
                    this.dgv_es1_des.Columns[9].Width = 60;
                    this.dgv_es1_des.Columns[10].Width = 100;
                    this.dgv_es1_des.Columns[11].Width = 100;
                    this.dgv_es1_des.Columns[12].Width = 100;

                    this.dgv_es1_des.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                }


                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }

            }
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

            }
            else
            {
                try
                {
                    DataTable dt_get_bill = new DataTable();
                    dt_get_bill = purch.get_purch_bill_by_id(Convert.ToInt32(txt_bill_no.Text));
                    if (dt_get_bill.Rows.Count == 0)
                    {
                        MessageBox.Show("هذه القائمة غير موجودة ....يرجى التاكد من الرقم لطفا");
                    }
                    else
                    {
                        dgv_es1_des.DataSource = dt_get_bill;
                        //MessageBox.Show("هذه القائمة مكررة");
                        dgv_es1_des_hide_colomns();
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
                this.dgv_es1_des.DataSource = dal.SelectData("sp_get_all_purch_bill", null);
                lbl_count.Text = dgv_es1_des.Rows.Count.ToString();
                dgv_es1_des_hide_colomns();

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
                    PurchaseBill_Id = Convert.ToInt32(this.dgv_es1_des.CurrentRow.Cells[2].Value);

                    dt_opp_det = null;
                  dt_opp_det = purch.get_purch_Opp_by_purch_Bill_Id(PurchaseBill_Id);

                    DataTable dt_bill_det = new DataTable();
                    dt_bill_det = purch.get_purch_Bill_det_by_id(PurchaseBill_Id);

                    if (dt_bill_det.Rows.Count == 1)
                    {
                        bill_date = dt_bill_det.Rows[0][5].ToString();//bill_date
                        bill_cat = dt_bill_det.Rows[0][4].ToString();//bill_no
                      //  bill_type = dt_bill_det.Rows[0][3].ToString();
                       // tot_B_dis = dt_bill_det.Rows[0][4].ToString();
                        tot_A_dis = dt_bill_det.Rows[0][6].ToString();//tot
                       // tot_dis = dt_bill_det.Rows[0][6].ToString();
                        paied_mon = dt_bill_det.Rows[0][8].ToString();
                        rest_mon = dt_bill_det.Rows[0][9].ToString();//rest_mon
                        balance_A_bill = dt_bill_det.Rows[0][10].ToString();
                        user_name = dt_bill_det.Rows[0][7].ToString();
                        cust_name = dt_bill_det.Rows[0][3].ToString();//sup_name
                        //cust_tel = dt_bill_det.Rows[0][12].ToString();
                        //cust_addres = dt_bill_det.Rows[0][13].ToString();
                        //balance_on_cust = dt_bill_det.Rows[0][14].ToString();

                        PL.frm_purch_bill_detailes frm_purch_det= new frm_purch_bill_detailes();
                        frm_purch_det.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("اما هذه القائمة مكررة او غير موجودة اصلا");
                    }


                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

            }
            else if (e.ColumnIndex == 1 & this.dgv_es1_des.Rows.Count > 0)
            {

            }

        }

        private void lbl_username_Click(object sender, EventArgs e)
        {

        }
    }
}
