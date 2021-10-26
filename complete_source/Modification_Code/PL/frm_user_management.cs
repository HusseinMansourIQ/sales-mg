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
    public partial class frm_usercontrol : Form
    {
        public frm_usercontrol()
        {
            InitializeComponent();
        }

        private void usercontrol_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //usersinsert frm = new usersinsert();

            //frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //frm_confirm2 frm = new frm_confirm2();
            //frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //frm_confirm3 frm = new frm_confirm3();
            //frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //frm_confirm4 frm = new frm_confirm4();
            //frm.ShowDialog();
        }
    }
}
