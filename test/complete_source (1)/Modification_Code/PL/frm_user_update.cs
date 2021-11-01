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
    public partial class frm_userupdate : Form
    {
        DAL.cls_dal dal = new DAL.cls_dal();
        BL.cls_user_mng users = new BL.cls_user_mng();
        public static int user_id;
        public static string confirm = null;
        public static string user_name = null;
        public static string pass = null;
        public static string user_type = null;
        public static string arabic_name = null;


        private static frm_userupdate frm;


        static void Frm_formclosed(object sender, FormClosedEventArgs e)
        {
            frm = null;
        }
        public static frm_userupdate getmainform
        {
            get
            {
                if (frm == null)
                {
                    frm = new frm_userupdate();
                    frm.FormClosed += new FormClosedEventHandler(Frm_formclosed);
                }
                return frm;
            }

        }
        public frm_userupdate()
        {
            InitializeComponent();
            add_botton_1_to_dgv();
            add_botton_2_to_dgv();

            //data_view();
            //fillcombo();
        }


        private void btn_add_Click(object sender, EventArgs e)
        {

        }


        private void dgv_categories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dgv_userupdate.Rows.Count>0)
            {
            try
            {
               txt_username.Text = dgv_userupdate.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }
           
            //txt_password.Text = dgv_userupdate.Rows[e.RowIndex].Cells[1].Value.ToString();
            //comboBox1.Text = dgv_userupdate.Rows[e.RowIndex].Cells[2].Value.ToString();
            //  lbl_ID.Text = dgv_userupdate.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        //public void data_view()

        //{
        //    SqlConnection connect = new SqlConnection();
        //    connect.ConnectionString = "Data Source=AMC-PC;Initial Catalog=DB1;Integrated Security=True";
        //    if (connect.State == ConnectionState.Closed)
        //    {
        //        connect.Open();
        //        SqlCommand cmd = connect.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "select *  from userstable ";
        //        cmd.ExecuteNonQuery();


        //        DataTable dt = new DataTable();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        dgv_userupdate.DataSource = dt;
        //        connect.Close();
        //    }
        //}
        //private void fillcombo()
        //{
        //    SqlConnection connect = new SqlConnection();
        //    connect.ConnectionString = "Data Source=AMC-PC;Initial Catalog=DB1;Integrated Security=True";
        //    if (connect.State == ConnectionState.Closed)
        //    {
        //        connect.Open();
        //    }
        //    SqlDataAdapter da = new SqlDataAdapter("SELECT usertype FROM userstable", connect);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        comboBox1.Items.Add(dt.Rows[i]["usertype"].ToString());
        //    }
        //    connect.Close();

        //}




        private void dgv_userupdate_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv_userupdate.Rows.Count > 0)
            {
            try
            {
            if (dgv_userupdate.Columns[e.ColumnIndex].Name == "password" && e.Value != null)
            {
                dgv_userupdate.Rows[e.RowIndex].Tag = e.Value;
                e.Value = new String('*', e.Value.ToString().Length);
            }

            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            }
        }

        private void dgv_userupdate_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv_userupdate.Rows.Count > 0)
            {         
                try
            {
               if (dgv_userupdate.CurrentRow.Tag != null)
                e.Control.Text = dgv_userupdate.CurrentRow.Tag.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }


            }
         
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void frm_userupdate_Load(object sender, EventArgs e)
        {
            btn_save.Enabled = true;
            comboBox1.Text = "user";
            btn_update.Enabled = false;
            try
            {
                dgv_userupdate.DataSource = dal.SelectData("sp_get_all_users", null);
                
                dgv_design();
                btn_save.Enabled = true;
                btn_update.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }

        private void dgv_userupdate_DataMemberChanged(object sender, EventArgs e)
        {

        }

        private void dgv_userupdate_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //  دالة ترقيم الروهيدر في الداتاكرد فيو تلقائيا
            // Loops through each row in the DataGridView, and adds the
            // row number to the header
            try
            {
                foreach (DataGridViewRow dGVRow in this.dgv_userupdate.Rows)
                {
                    dGVRow.HeaderCell.Value = String.Format("{0}", dGVRow.Index + 1);
                }

                // This resizes the width of the row headers to fit the numbers
                this.dgv_userupdate.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                dgv_design();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_8"); }
        }
        private void add_botton_1_to_dgv()
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
                    this.dgv_userupdate.Columns.Add(button);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }
        private void add_botton_2_to_dgv()
        {
            //اظافة column button الى الداتا كردفيو
            try
            {
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                {
                    button.Name = "button";
                    button.HeaderText = "حذف";
                    button.Text = "حذف";
                    button.UseColumnTextForButtonValue = true; //dont forget this line
                    this.dgv_userupdate.Columns.Add(button);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }

        }
      public void dgv_design()
        {
            try
            {
dgv_userupdate.DataSource = dal.SelectData("sp_get_all_users", null);
            dgv_userupdate.Refresh();
            if (this.dgv_userupdate.Rows.Count > 0)
            {
                this.dgv_userupdate.Columns[3].Visible = false;
                this.dgv_userupdate.Columns[5].Visible = false;
                this.dgv_userupdate.Columns[0].Width = 50;
                this.dgv_userupdate.Columns[1].Width = 50;
                this.dgv_userupdate.Columns[2].Width = 80;
                this.dgv_userupdate.Columns[4].Width = 80;

            }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            
        }


        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_password.Text == "" || txt_username.Text == "" || txt_username.Text == string.Empty || txt_password.Text == string.Empty || textBox1.Text == "" || textBox1.Text == string.Empty)
            {
                MessageBox.Show("يجب املاء كافة الحقول ", "رسالة تنبيه");
            }
            else if (comboBox1.Text == "") { MessageBox.Show(" يجب اختيار نوع المستخدم ", "رسالة تنبيه"); }

            else if (comboBox1.Text == "admin")
            {
                if (MessageBox.Show(" سوف يتمكن من اجراء التعديلات على البرنامج ,هل تريد فعلا اعطاء صلاحيات المدير لهذا المستخدم  ", "رسالة تنبيه", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                     confirm = "update";
                    user_name = txt_username.Text;
                    pass = txt_password.Text;
                    user_type = comboBox1.Text;
                    arabic_name = textBox1.Text;

                    PL.frm_pass_confirm frm_pass = new frm_pass_confirm();
                    frm_pass.ShowDialog();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                  
                }
                else
                {

                }


            }
            else
            {
                try
                {
                 confirm = "update";
                user_name = txt_username.Text;
                pass = txt_password.Text;
                user_type = comboBox1.Text;
                arabic_name = textBox1.Text;

                PL.frm_pass_confirm frm_pass = new frm_pass_confirm();
                frm_pass.ShowDialog();
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
               
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            txt_password.Clear();
            txt_username.Clear();
            comboBox1.Text = "user";
            textBox1.Clear();
            btn_save.Enabled = true;
            btn_update.Enabled = false;
            try
            {
                dgv_userupdate.DataSource = dal.SelectData("sp_get_all_users", null);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "ERROR_NO_13"); }
        }



        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_password.Text == "" || txt_username.Text == "" || txt_username.Text == string.Empty || txt_password.Text == string.Empty || textBox1.Text == "" || textBox1.Text == string.Empty)
            {
                MessageBox.Show("يجب املاء كافة الحقول ", "رسالة تنبيه");
            }      
            else if (comboBox1.Text == "") { MessageBox.Show(" يجب اختيار نوع المستخدم ", "رسالة تنبيه"); }

            else if (comboBox1.Text == "admin")
            {
              if(MessageBox.Show(" سوف يتمكن من اجراء التعديلات على البرنامج ,هل تريد فعلا اعطاء صلاحيات المدير لهذا المستخدم  ", "رسالة تنبيه", MessageBoxButtons.YesNo) == DialogResult.Yes)
              {
                    try
                    {
 confirm = "add";
                user_name = txt_username.Text;
                pass = txt_password.Text;
                user_type = comboBox1.Text;
                arabic_name = textBox1.Text;

                PL.frm_pass_confirm frm_pass = new frm_pass_confirm();
                frm_pass.ShowDialog();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                   
              }
              else
              {

              }
                   

            }
            else
            {
                try
                {
 confirm = "add";
                user_name = txt_username.Text;
                pass = txt_password.Text;
                user_type = comboBox1.Text;
                arabic_name = textBox1.Text;

                PL.frm_pass_confirm frm_pass = new frm_pass_confirm();
                frm_pass.ShowDialog();
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
               
            }
        }

        private void dgv_userupdate_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 & this.dgv_userupdate.Rows.Count > 0)
            {
               
                try
                {
                    user_id = Convert.ToInt32(this.dgv_userupdate.CurrentRow.Cells[5].Value);
                    btn_save.Enabled = false;
                    btn_update.Enabled = true;

                    txt_username.Text = this.dgv_userupdate.CurrentRow.Cells[2].Value.ToString();
                    txt_password.Text = this.dgv_userupdate.CurrentRow.Cells[3].Value.ToString();
                    comboBox1.Text = this.dgv_userupdate.CurrentRow.Cells[4].Value.ToString();
                    textBox1.Text = this.dgv_userupdate.CurrentRow.Cells[6].Value.ToString();

                    // btn_save.Text = "اضافة المستخدم";
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(this.dgv_userupdate.Rows[e.RowIndex].Cells[1].Value.ToString());
                    MessageBox.Show(ex.ToString(), " تابع,ERROR_NO_1");

                }
            }
            else if (e.ColumnIndex == 1 & this.dgv_userupdate.Rows.Count > 0)
            {
                try
                {
                    confirm = "del";
                    user_id = Convert.ToInt32(this.dgv_userupdate.CurrentRow.Cells[5].Value);
                    PL.frm_pass_confirm frm_pass = new frm_pass_confirm();
                    frm_pass.ShowDialog();

                }

                catch (Exception ex)
                {
                    // MessageBox.Show(this.dgv_userupdate.Rows[e.RowIndex].Cells[1].Value.ToString());
                    MessageBox.Show(ex.ToString(), " تابع,ERROR_NO_1");

                }



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
