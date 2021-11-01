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
    public partial class frm_backup : Form
    {

        BL.cls_back_restor bak_res = new BL.cls_back_restor();
        string fileName = null;
        string restor_path = null;
        public frm_backup()
        {
            InitializeComponent();
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                txt_path.Enabled = false;
                btn_brows.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                txt_path.Enabled = true;
                btn_brows.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_createBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton2.Checked == true)
                {
                    fileName = txt_path.Text + "\\DB_SaleApp_" + DateTime.Now.ToShortDateString().Replace("/", "_") + "at" + DateTime.Now.ToShortTimeString().Replace(":", "-") + ".bak";
                    bak_res.backup(fileName);
                    MessageBox.Show("تم حفظ النسخة الاحتياطية بنجاح", "النسخ الاحتياطي");
                }
                else
                {
                    fileName = "D:\\DB_SaleApp_BACKUP" + "\\DB_SaleApp_" + DateTime.Now.ToShortDateString().Replace("/", "_") + "at" + DateTime.Now.ToShortTimeString().Replace(":", "-") + ".bak";
                    bak_res.backup(fileName);
                    MessageBox.Show("تم حفظ النسخة الاحتياطية بنجاح", "النسخ الاحتياطي");
                    txt_path.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

        }

        private void btn_brows_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_path.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void frm_backup_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void btn_restor_Click(object sender, EventArgs e)
        {
            if(txt_path.Text==string.Empty)
            {
                MessageBox.Show("يجب اختيار الملف اولا", "النسخ الاحتياطي");
            }
            else
            {
                try
                {
                    bak_res.restor(restor_path);
                    MessageBox.Show("تم استعادة النسخة الاحتياطية بنجاح", "النسخ الاحتياطي");
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }
            }
        }

        private void btn_chose_restor_path_Click(object sender, EventArgs e)
        {
             if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_path.Text= folderBrowserDialog1.SelectedPath;
                restor_path = @"'"+txt_path.Text+"'";
            }
        }

        private void btn_restorpath_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_path.Text == string.Empty)
            {
                MessageBox.Show("يجب اختيار الملف اولا", "النسخ الاحتياطي");
            }
            else
            {
                try
                {
                    bak_res.restor(restor_path);
                    MessageBox.Show("تم استعادة النسخة الاحتياطية بنجاح", "النسخ الاحتياطي");
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_17"); }

            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
