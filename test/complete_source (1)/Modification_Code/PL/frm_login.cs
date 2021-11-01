using Sales_Management.PL;
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
    public partial class frm_login : Form

    {
        Cls_Login login = new Cls_Login();
        public frm_login()
        {
            InitializeComponent();
        }

        private void frm_login_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            { DataTable dt = login.Login(txt_Uname.Text, txt_u_pass.Text);
            if (dt.Rows.Count > 0)
            {
                frm_main.getmainform.btn_buying.Enabled = true;
                frm_main.getmainform.btn_changePHone.Enabled = true;
                frm_main.getmainform.btn_costManage.Enabled = true;
                frm_main.getmainform.btn_prodManage.Enabled = true;
                frm_main.getmainform.btn_selling.Enabled = true;
                frm_main.getmainform.btn_bill_management.Enabled = true;
                frm_main.getmainform.btn_userManag.Enabled = true;
                frm_main.getmainform.btn_BackBill.Enabled = true;
                frm_main.getmainform.btn_tasdeed.Enabled = true;
                frm_main.getmainform.btn_login.Enabled = false;
                frm_main.getmainform.button1.Enabled = true;
                frm_main.getmainform.btnbackup_restor.Enabled = true;
                frm_main.getmainform.linkLabel1.Text = "Signed " + txt_Uname.Text + " into the platform";
                Program.U_ArabicName = dt.Rows[0]["U_ArapicName"].ToString();
                Program.User_id =Convert.ToInt32( dt.Rows[0]["User_id"]);

                frm_main.getmainform.lbl_salerName1.Text = Properties.Settings.Default.name1;
                frm_main.getmainform.txt_salerName2.Text = Properties.Settings.Default.name2;
                frm_main.getmainform.lbl_tel1.Text = Properties.Settings.Default.phone1;
                frm_main.getmainform.lbl_tel2.Text = Properties.Settings.Default.phone2;
                frm_main.getmainform.btn_userManag.Enabled = true;
                frm_main.getmainform.btn_suppliersManage.Enabled = true;
                frm_main.getmainform.btn_debtManage.Enabled = true;
                frm_main.getmainform.btn_burchis_bill_manag.Enabled = true;

                    Properties.Settings.Default.pass = (dt.Rows[0]["U_Pass"]).ToString();
                    Properties.Settings.Default.Save();

                    //label3.Text = Program.U_ArabicName;
                    this.Close();
            }
            else
            {
                MessageBox.Show("الرجاء التاكد من اسم المستخدم او كلمة المرور");
                txt_Uname.Clear();
                txt_u_pass.Clear();
                txt_Uname.Focus();
            }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           


        }

        private void txt_u_pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_u_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_u_name_KeyDown(object sender, KeyEventArgs e)
        {
            //لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغ
            //txt_u_pass.Focus();
            if (e.KeyCode == Keys.Enter && txt_u_pass.Text != string.Empty)
                SendKeys.Send("{Tab}");   //تحويل مفتاح انتر الى مفتاح تاب (عدم اضافة سطر جديد عند الضغط على مفتاح انتر)
        }

        private void txt_u_pass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
//لجعل المؤشر ينتقل الى المربع الخانة المطلوبة فقط اذا كان الخانة الحالية غير فارغة
            if (e.KeyCode == Keys.Enter && txt_u_pass.Text != string.Empty)
            {
                DataTable dt = login.Login(txt_Uname.Text, txt_u_pass.Text);
                if (dt.Rows.Count > 0)
                {
                    frm_main.getmainform.btn_buying.Enabled = true;
                    frm_main.getmainform.btn_changePHone.Enabled = true;
                    frm_main.getmainform.btn_costManage.Enabled = true;
                    frm_main.getmainform.btn_prodManage.Enabled = true;
                    frm_main.getmainform.btn_selling.Enabled = true;
                    frm_main.getmainform.btn_bill_management.Enabled = true;
                    frm_main.getmainform.btn_userManag.Enabled = true;
                    frm_main.getmainform.btn_BackBill.Enabled = true;
                    frm_main.getmainform.btn_tasdeed.Enabled = true;
                    frm_main.getmainform.btn_login.Enabled = false;
                    frm_main.getmainform.linkLabel1.Text = txt_Uname.Text;
                    Program.U_ArabicName = dt.Rows[0]["U_ArapicName"].ToString();
                    Program.User_id = Convert.ToInt32(dt.Rows[0]["User_id"]);
                    frm_main.getmainform.lbl_salerName1.Text = Properties.Settings.Default.name1;
                    frm_main.getmainform.txt_salerName2.Text = Properties.Settings.Default.name2;
                    frm_main.getmainform.lbl_tel1.Text = Properties.Settings.Default.phone1;
                    frm_main.getmainform.lbl_tel2.Text = Properties.Settings.Default.phone2;
                    frm_main.getmainform.btn_userManag.Enabled = true;
                    frm_main.getmainform.btn_suppliersManage.Enabled = true;
                    frm_main.getmainform.btn_debtManage.Enabled = true;
                    frm_main.getmainform.btn_burchis_bill_manag.Enabled = true;
                        frm_main.getmainform.button1.Enabled = true;
                        frm_main.getmainform.btnbackup_restor.Enabled = true;
                        Properties.Settings.Default.pass = (dt.Rows[0]["U_Pass"]).ToString();
                        Properties.Settings.Default.Save();

                        this.Close();
                }
                else
                {
                    MessageBox.Show("الرجاء التاكد من اسم المستخدم او كلمة المرور");
                    txt_Uname.Clear();
                    txt_u_pass.Clear();
                    txt_Uname.Focus();
                }
            }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            

        }

        private void btn_login_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Uname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_Uname.Text != string.Empty)
            {
                SendKeys.Send("{Tab}");
                //txt_u_pass.Focus();
            }

        }

        private void txt_Uname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
