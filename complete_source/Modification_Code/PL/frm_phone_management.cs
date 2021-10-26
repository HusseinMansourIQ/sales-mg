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
    public partial class frm_phone_management : Form
    {
        public frm_phone_management()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if(txt_name1.Text==""||txt_phon1.Text==""||txt_name2.Text==""||txt_phon2.Text=="")
            {
                MessageBox.Show("يجب املاء كافة الحقول");
            }
            else
            {
                try
                {
                Properties.Settings.Default.name1 = txt_name1.Text;
                Properties.Settings.Default.name2 = txt_name2.Text;
                Properties.Settings.Default.phone1 = txt_phon1.Text;
                Properties.Settings.Default.phone2 = txt_phon2.Text;
                Properties.Settings.Default.Save();
                frm_main.getmainform.lbl_salerName1.Text = Properties.Settings.Default.name1;
                frm_main.getmainform.txt_salerName2.Text = Properties.Settings.Default.name2;
                frm_main.getmainform.lbl_tel1.Text = Properties.Settings.Default.phone1;
                frm_main.getmainform.lbl_tel2.Text = Properties.Settings.Default.phone2;

                MessageBox.Show("تم الحفظ بنجاح");

                Close();
                }
                catch { }
                
            }
        }

        private void frm_phone_management_Load(object sender, EventArgs e)
        {
            try
            {
             txt_name1.Text = Properties.Settings.Default.name1;
            txt_name2.Text = Properties.Settings.Default.name2;
            txt_phon1.Text = Properties.Settings.Default.phone1;
            txt_phon2.Text = Properties.Settings.Default.phone2;
            }
            catch { }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
