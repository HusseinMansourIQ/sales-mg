using System;
using System.Windows.Forms;

namespace Sales_Management_2.PL
{
    internal class FrmToBeClosed
    {
        public static explicit operator FrmToBeClosed(Form v)
        {
            throw new NotImplementedException();
        }

        internal void Close()
        {
            throw new NotImplementedException();
        }
    }
}