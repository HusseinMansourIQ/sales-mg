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
using Microsoft.VisualBasic.ApplicationServices;

namespace Sales_Management_2.PL
{
    public partial class frm_pass_confirm : Form
    {
        BL.cls_user_mng users = new BL.cls_user_mng();
        //  PL.frm_userupdate u_up = new frm_userupdate();
        DAL.cls_dal dal = new DAL.cls_dal();
        string user_name = null;
        string pass = null;
        string user_type = null;
        string arabic_name = null;
        int user_id;
        public frm_pass_confirm()
        {
            InitializeComponent();
        }

        private void frm_pass_confirm_Load(object sender, EventArgs e)
        {
            try
            {
 user_name = PL.frm_userupdate.user_name;
            pass = PL.frm_userupdate.pass;
            user_type = PL.frm_userupdate.user_type;
            arabic_name = PL.frm_userupdate.arabic_name;
            user_id = PL.frm_userupdate.user_id;
            }
            catch { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (users.admin_check(textBox1.Text).Rows.Count > 0)
                {

                    if (PL.frm_userupdate.confirm == "add")
                    {
                        if (users.user_check(user_name).Rows.Count > 0)
                        {
                            MessageBox.Show("اسم المستخدم موجود مسبقا يرجى تغيير اسم المستخدم قبل المتابعة");
                            Close();
                        }
                        else if (MessageBox.Show(" هل تريد اضافة المستخدم ", "رسالة تنبيه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            users.user_insert(user_name, pass, user_type, arabic_name);
                            MessageBox.Show("تم اضافة المستخدم بنجاح");
                            frm_userupdate.getmainform.dgv_userupdate.DataSource = dal.SelectData("sp_get_all_users", null);
                            frm_userupdate.getmainform.dgv_userupdate.Refresh();
                            frm_userupdate.getmainform.dgv_design();
                            frm_userupdate.getmainform.btn_save.Enabled = true;
                            frm_userupdate.getmainform.btn_update.Enabled = false;

                            Close();
                        }

                    }
                    else if (PL.frm_userupdate.confirm == "del")
                    {

                        if (MessageBox.Show(" هل تريد حذف المستخدم ", "رسالة تنبيه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            try
                            {
                                users.user_delete(PL.frm_userupdate.user_id);
                                frm_userupdate.getmainform.dgv_userupdate.DataSource = dal.SelectData("sp_get_all_users", null);
                                frm_userupdate.getmainform.dgv_design();
                                frm_userupdate.getmainform.btn_save.Enabled = true;
                                frm_userupdate.getmainform.btn_update.Enabled = false;
                                Close();
                            }
                            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }
                        }
                        else
                        {
                            Close();
                        }


                    }
                    else if (PL.frm_userupdate.confirm == "update")
                    {
                        try
                        {
                            if (MessageBox.Show(" هل تريد حفظ التعديل ", "رسالة تنبيه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {

                                users.user_update(user_name, pass, user_type, arabic_name, user_id);
                                MessageBox.Show("تم حفظ التعديل بنجاح");
                                frm_userupdate.getmainform.dgv_userupdate.DataSource = dal.SelectData("sp_get_all_users", null);
                                frm_userupdate.getmainform.dgv_design();
                                frm_userupdate.getmainform.btn_save.Enabled = true;
                                frm_userupdate.getmainform.btn_update.Enabled = false;
                                Close();
                            }
                            else
                            {
                                Close();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }
                    }
                }
                else
                {
                    MessageBox.Show("عذرا لا تملك صلاحية الاضافة كونك ليس من ضمن مدراء البرنامج");
                }
            }
           catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
