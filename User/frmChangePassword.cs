using BusinessDVLD;
using DVLD.GloabalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class frmChangePassword : Form
    {
        private int _UserID = -1;
        private clsUser _User;

        public int UserID
        {
            get { return _UserID; }
        }

        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            textBoxCurrentPassword.Focus();

            _User = clsUser.FindUser(_UserID);

            if (_User == null)
            {
                MessageBox.Show($"Could not foind user with id {_UserID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_UserID);
        }

        private void textBoxCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxCurrentPassword.Text.Trim() == "")
            {
                errorProvider1.SetError(textBoxCurrentPassword, "This field can't be empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(textBoxCurrentPassword, null);
            }

            if (textBoxCurrentPassword.Text.Trim() != _User.Password)
            {
                errorProvider1.SetError(textBoxCurrentPassword, "Current password is wrong");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(textBoxCurrentPassword, null);
            }
        }

        private void txbNewPasswrod_Validating(object sender, CancelEventArgs e)
        {
            if (txbNewPasswrod.Text.Trim() == "")
            {
                errorProvider1.SetError(txbNewPasswrod, "This field can't be empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbNewPasswrod, null);
            }

        }

        private void txbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txbConfirmPassword.Text.Trim() == "")
            {
                errorProvider1.SetError(txbConfirmPassword, "This field can't be empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbConfirmPassword, null);
            }


            if (txbNewPasswrod.Text.Trim() != txbNewPasswrod.Text.Trim())
            {
                errorProvider1.SetError(txbConfirmPassword, "Password is not confirmed");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Follow the red mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = txbConfirmPassword.Text;

            if (_User.Save())
            {
                MessageBox.Show("Password changed successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (_UserID == clsGloabal.CurrentUser.ID)
                    clsGloabal.CurrentUser = _User;
            }
            else
            {
                MessageBox.Show("Error, password is not changed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
