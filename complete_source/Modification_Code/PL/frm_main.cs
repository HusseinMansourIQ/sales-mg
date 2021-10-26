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

    public partial class frm_main : Form
    {
        private static frm_main frm;

        BL.cls_back_restor bak_res = new BL.cls_back_restor();
        static void Frm_formclosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }
        public static frm_main getmainform
        {
            get
            {
                if (frm == null)
                {
                    frm = new frm_main();
                    frm.FormClosed += new FormClosedEventHandler(Frm_formclosed);
                }
                return frm;
            }

        }

        public frm_main()
        {
            InitializeComponent();

            if (frm == null)
                frm = this;
            this.btn_selling.Enabled = false;
            this.btn_buying.Enabled = false;
            this.btn_costManage.Enabled = false;
            this.btn_prodManage.Enabled = false;
            this.btn_bill_management.Enabled = false;
            this.btn_userManag.Enabled = false;
            this.btn_changePHone.Enabled = false;
            this.btn_BackBill.Enabled = false;
            this.btn_tasdeed.Enabled = false;
            this.btn_debtManage.Enabled = false;
            this.btn_suppliersManage.Enabled = false;
            btn_burchis_bill_manag.Enabled = false;
            button1.Enabled = false;
            btnbackup_restor.Enabled = false;


            this.btnbackup_restor.Image = Image.FromFile("image/6.png");
            this.btnbackup_restor.ImageAlign = ContentAlignment.MiddleRight;
            this.btnbackup_restor.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_debtManage.Image = Image.FromFile("image/1.png");
            this.btn_debtManage.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_debtManage.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_selling.Image = Image.FromFile("image/2.png");
            this.btn_selling.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_selling.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_prodManage.Image = Image.FromFile("image/7.png");
            this.btn_prodManage.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_prodManage.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_burchis_bill_manag.Image = Image.FromFile("image/1.png");
            this.btn_burchis_bill_manag.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_burchis_bill_manag.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_buying.Image = Image.FromFile("image/2.png");
            this.btn_buying.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_buying.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_costManage.Image = Image.FromFile("image/4.png");
            this.btn_costManage.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_costManage.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_bill_management.Image = Image.FromFile("image/1.png");
            this.btn_bill_management.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_bill_management.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_BackBill.Image = Image.FromFile("image/2.png");
            this.btn_BackBill.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_BackBill.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_suppliersManage.Image = Image.FromFile("image/4.png");
            this.btn_suppliersManage.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_suppliersManage.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_userManag.Image = Image.FromFile("image/4.png");
            this.btn_userManag.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_userManag.TextAlign = ContentAlignment.MiddleLeft;

            this.btn_tasdeed.Image = Image.FromFile("image/2.png");
            this.btn_tasdeed.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_tasdeed.TextAlign = ContentAlignment.MiddleLeft;


            this.btn_changePHone.Image = Image.FromFile("image/5.png");
            this.btn_changePHone.ImageAlign = ContentAlignment.MiddleRight;
            this.btn_changePHone.TextAlign = ContentAlignment.MiddleLeft;

            this.button1.Image = Image.FromFile("image/3.png");
            this.button1.ImageAlign = ContentAlignment.MiddleRight;
            this.button1.TextAlign = ContentAlignment.MiddleLeft;


        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            btn_login.Focus();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            frm_login frm6 = new frm_login();
            frm6.ShowDialog();
        }
       

        private void btn_close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد حفظ نسخة احتياطية من قاعدة البيانات    " , "تنــبــيــه ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                try
                {
                    string fileName;
                        fileName = "D:\\DB_SaleApp_BACKUP" + "\\DB_SaleApp_" + DateTime.Now.ToShortDateString().Replace("/", "_") + "at" + DateTime.Now.ToShortTimeString().Replace(":", "-") + ".bak";
                        bak_res.backup(fileName);
                        MessageBox.Show("تم حفظ النسخة الاحتياطية بنجاح", "النسخ الاحتياطي");
                    // txt_path.Clear();
                    Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

            }
            else
            {
             Close();
            }
           
        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void btn_prodManage_Click(object sender, EventArgs e)
        {
            frm_prodManage frm3 = new frm_prodManage();
            frm3.ShowDialog();
        }

        private void btn_selling_Click_1(object sender, EventArgs e)
        {
            sellingBill frm = new sellingBill();
            frm.Show();
        }

        private void btn_buying_Click_1(object sender, EventArgs e)
        {
            frm_buyingProduct frm2 = new frm_buyingProduct();
            frm2.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
            lbl_clock.Text = DateTime.Now.ToString("hh:mm:ss tt");
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.CustomFormat = "yyyy-mm-dd";

            }
            catch { }

        }

        private void btn_HowToUseApp_Click(object sender, EventArgs e)
        {

        }

        private void btn_BackBill_Click(object sender, EventArgs e)
        {
            PL.frm_return_bill frm_re = new frm_return_bill();
            frm_re.Show();
        }


        private void btn_bill_management_Click(object sender, EventArgs e)
        {
            PL.frm_bill_management frm_blmng = new frm_bill_management();
            frm_blmng.Show();
        }

        private void btn_debtManage_Click_1(object sender, EventArgs e)
        {
            PL.frm_Debts frm_deb = new PL.frm_Debts();
            frm_deb.ShowDialog();
        }

        private void btn_costManage_Click_1(object sender, EventArgs e)
        {
            PL.frm_CostamerManage frm_cost = new PL.frm_CostamerManage();
            frm_cost.ShowDialog();
        }

        private void btn_changePHone_Click(object sender, EventArgs e)
        {
            PL.frm_phone_management frm_phon = new frm_phone_management();
            frm_phon.ShowDialog();
        }

        private void btn_tasdeed_Click(object sender, EventArgs e)
        {
            PL.frm_Tasdeed frm_tsd = new PL.frm_Tasdeed();
            frm_tsd.ShowDialog();
        }

        private void btn_userManag_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(Program.pass);
           // MessageBox.Show(Properties.Settings.Default.pass);
            BL.cls_user_mng usr_mng = new BL.cls_user_mng();
            DataTable dt = new DataTable();
            dt = usr_mng.admin_check(Properties.Settings.Default.pass);
            

            try
            {
                if(dt.Rows.Count > 0)
                {
                PL.frm_userupdate frm_user = new frm_userupdate();
                frm_user.ShowDialog();

                }
                else
                {
                    MessageBox.Show("عذرا لا تملك الصلاحية للدخول ");
                }

            }
            catch { }
        }

        private void btn_suppliersManage_Click(object sender, EventArgs e)
        {
            PL.frm_Supplier sup = new frm_Supplier();
            sup.Show();
        }

        private void btn_burchis_bill_manag_Click(object sender, EventArgs e)
        {
            PL.frm_purchase_bill_manag frm_p_m = new frm_purchase_bill_manag();
            frm_p_m.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PL.frm_debt_dairact frm_d = new frm_debt_dairact();
            frm_d.Show();
        }

        private void btnbackup_restor_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(Program.pass);
            // MessageBox.Show(Properties.Settings.Default.pass);
            BL.cls_user_mng usr_mng = new BL.cls_user_mng();
            DataTable dt = new DataTable();
            dt = usr_mng.admin_check(Properties.Settings.Default.pass);


            try
            {
                if (dt.Rows.Count > 0)
                {
                    PL.frm_backup frm_bakup = new frm_backup();
                    frm_bakup.ShowDialog();

                }
                else
                {
                    MessageBox.Show("عذرا لا تملك الصلاحية للدخول ");
                }

            }
            catch { }
        }

        private void lbl_designer_Click(object sender, EventArgs e)
        {

        }
    }
}

