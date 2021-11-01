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

namespace Sales_Management_2.PL
{
    public partial class frm_user_insert : Form
    {
        public frm_user_insert()
        {
            InitializeComponent();
            //fillcombo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
              SqlConnection connect = new SqlConnection();
            connect.ConnectionString = "Data Source=AMC-PC;Initial Catalog=DB1;Integrated Security=True";

            connect.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM userstable where password='" + txt_adminpass.Text + "' and usertype = 'admin'", connect);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count >= 1)
            {
                if (txt_username.Text != "" && txt_password.Text != "")
                {
                    

                        //cmd = new SqlCommand("insert into collection2 (name,birthplace,livingplace,mothername,wifename,noofchildren,commencementdate,enterprisename,enterpriselocation,booknumber,bookdate,notes,intermission_y) values(@name,@birthplace,@livingplace,@mothername,@wifename,@noofchildren,@commencementdate,@enterprisename,@enterpriselocation,@booknumber,@bookdate,@notes,@intermission_y)", connect);
                        cmd = new SqlCommand("INSERT INTO userstable (username, password,usertype)VALUES(@username,@password,@usertype)", connect);

                        cmd.Parameters.AddWithValue("@username", txt_username.Text);
                        cmd.Parameters.AddWithValue("@password", txt_password.Text);
                        cmd.Parameters.AddWithValue("@usertype", comboBox1.SelectedItem.ToString());
                        SqlCommand cmd2 = new SqlCommand("SELECT  username, password FROM userstable where username='" + txt_username.Text + "'and password='" + txt_password.Text + "' ", connect);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        if (dt2.Rows.Count < 1)
                        {

                            cmd.ExecuteNonQuery();

                            connect.Close();


                            MessageBox.Show("تمت الاضافة بنجاح");
                            txt_adminpass.Clear();
                            txt_password.Clear();
                            txt_username.Clear();
                            txt_adminpass.Focus();
                        }
                        else
                        {
                            MessageBox.Show("هذا الاسم موجود مسبقا");
                        }
                    
                }
                else if ((txt_username.Text == "" && txt_password.Text == ""))
                {
                    MessageBox.Show("الرجاء املاء كافة الحقول قبل الحفظ");
                    connect.Close();
                }
            }

            else if (dt.Rows.Count < 1)
            {
                MessageBox.Show("اما كلمة المرور التي ادخلتها غير صحيحة او انك لا تملك صلاحيات الدخول الى هذه المعلومات ");
                txt_username.Clear();
                connect.Close();
            }

            connect.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
           

        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void fillcombo()
        {
            try
            {
SqlConnection connect = new SqlConnection();
            connect.ConnectionString = "Data Source=AMC-PC;Initial Catalog=DB1;Integrated Security=True";
            if (connect.State == ConnectionState.Closed)
            {
                connect.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter("SELECT usertype FROM userstable", connect);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i]["usertype"].ToString());
            }
            connect.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            

        }

        private void usersinsert_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}





