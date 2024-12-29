using BusinessDVLD;
using DVLD.GloabalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmLogIn : Form
    {

        public frmLogIn()
        {
            InitializeComponent();

        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindUserByUserNameAndPassword(txbUserName.Text, txbPassword.Text);

            if (User == null)
            {
                MessageBox.Show("Please try again with different username/password", "Invalid Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!User.IsActive)
            {
                MessageBox.Show("This account is deactivated, contact your admin", "Inactive User Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbxRememberMe.Checked)
                clsGloabal.RememberUserNameAndPassword(txbUserName.Text, txbPassword.Text);
            else
                clsGloabal.RememberUserNameAndPassword("", "");

            clsGloabal.CurrentUser = User;
            this.Hide();
            Main frm = new Main(this);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (clsGloabal.GetCurrentCredentials(ref UserName, ref Password))
            {
                txbUserName.Text = UserName;
                txbPassword.Text = Password;

                cbxRememberMe.Checked = true;
            }
            else
            {
                cbxRememberMe.Checked = false;
            }

        }
    }
}
