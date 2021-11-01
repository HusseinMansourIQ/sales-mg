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
    public partial class frm_Bill_Detailes : Form
    {
        BL.Cls_Sales sales = new BL.Cls_Sales();
        public frm_Bill_Detailes()
        {
            InitializeComponent();
            form_sizing();
            dgv1_design();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frm_Bill_Detailes_Load(object sender, EventArgs e)
        {
            fill_dgv_and_boxes();
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
        private void fill_dgv_and_boxes()
        {
            try
            {
                dgv_es1_des.DataSource = PL.frm_bill_management.dt_opp_det;

                txt_SalesBill_Id.Text = PL.frm_bill_management.SalesBill_Id.ToString();
                txt_SalesBill_date.Text = PL.frm_bill_management.bill_date;
                txt_adress.Text = PL.frm_bill_management.cust_addres;
                txt_BalanceAftercurentBill.Text = PL.frm_bill_management.balance_A_bill;
                txt_BalanceOnCostmer.Text = PL.frm_bill_management.balance_on_cust;
                txt_CostmerName.Text = PL.frm_bill_management.cust_name;
                txt_PaidMony.Text = PL.frm_bill_management.paied_mon;
                txt_phonNo.Text = PL.frm_bill_management.cust_tel;
                txt_RestMony.Text = PL.frm_bill_management.rest_mon;
                txt_totalAfterDis.Text = PL.frm_bill_management.tot_A_dis;
                txt_totalBeforDis.Text = PL.frm_bill_management.tot_B_dis;
                txt_total_Dis.Text = PL.frm_bill_management.tot_dis;
                lbl_bill_type.Text= PL.frm_bill_management.bill_type;
                lbl_bill_cat.Text= PL.frm_bill_management.bill_cat;
                lbl_prud_count.Text = this.dgv_es1_des.Rows.Count.ToString();

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

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

        private void txt_PaidMony_KeyPress(object sender, KeyPressEventArgs e)
        {
            //اجراء لجعل التيكست بوكس يستقبل فقط ارقام وخاصية المسح باك سبيس
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {

                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               int SalesBill_Id = Convert.ToInt32( PL.frm_bill_management.SalesBill_Id);

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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
