using BusinessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class frmAddEditUser : Form
    {
        private enum Mode { AddNew, Update }
        private Mode _Mode;
        private int _UserID = -1;
        public clsUser _User;

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = Mode.Update;
        }
        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = Mode.AddNew;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID == -1 && _Mode == Mode.AddNew)
            {
                MessageBox.Show("Select a person", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (_Mode == Mode.AddNew && clsUser.IsUserExistByPersonID(ctrlPersonCardWithFilter1.PersonID))
            {
                MessageBox.Show("This person is already a user", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tabLoginInfo.Enabled = false;
                btnSave.Enabled = false;
                tab.SelectedIndex = 0;
                return;
            }

            tabLoginInfo.Enabled = true;
            btnSave.Enabled = true;
            tab.SelectedIndex = 1;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == Mode.AddNew)
            {
                lblAddEditUser.Text = "Add New User";
                this.Text = "Add New User";

                _User = new clsUser();

                tabLoginInfo.Enabled = false;
            }
            else
            {
                ctrlPersonCardWithFilter1.FilterEnabled = false;

                lblAddEditUser.Text = "Update User";
                this.Text = "Update User";
                
                tabLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            txbUserName.Text = "";
            txbPassword.Text = "";
            txbConfirmPassword.Text = "";
            chbIsActive.Checked = true;
        }

        private void _LoadInfo()
        { // btnSave is disabled

            // Load _User info. _UserID is known
            _User = clsUser.FindUser(_UserID);

            // Disable filter
            //ctrlPersonCardWithFilter1.FilterEnabled = false;
            
            if (_User == null)
            {
                MessageBox.Show("User With User ID " + _UserID + " Is not found", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblID.Text = _User.ID.ToString();
            txbPassword.Text = _User.Password.ToString();
            txbConfirmPassword.Text = _User.Password.ToString();
            chbIsActive.Checked = _User.IsActive;

            // Load ctrl Info
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == Mode.Update)
                _LoadInfo();
        }

        private void txbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbUserName.Text))
            {
                errorProvider1.SetError(txbUserName,"This field can't be empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbUserName, null);
            }

            if (_Mode == Mode.AddNew)
            {
                if (clsUser.IsUserExistByUserName(txbUserName.Text))
                {
                    errorProvider1.SetError(txbUserName, "This UserName is already used");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(txbUserName, null);
                }
            }
        }

        private void txbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbPassword.Text))
            {
                errorProvider1.SetError(txbPassword, "This field can't be empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbPassword, null);
            }
        }

        private void txbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbConfirmPassword.Text))
            {
                errorProvider1.SetError(txbConfirmPassword, "This field can't be empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbConfirmPassword, null);
            }

            if (txbConfirmPassword.Text != txbConfirmPassword.Text)
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
                MessageBox.Show("Some fields are not valid, follow the red mark", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID; // No need, this user has the same person, did not change
            _User.UserName = txbUserName.Text.Trim();
            _User.Password = txbPassword.Text.Trim();
            _User.IsActive = chbIsActive.Checked;

            if (_User.Save())
            {

                if (_Mode == Mode.AddNew)
                    MessageBox.Show("User added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("User updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _Mode = Mode.Update;

                _UserID = _User.ID;
                lblID.Text = _UserID.ToString();

                this.Text = "Update User";
                lblAddEditUser.Text = "Update User";

                // Disable filter
                ctrlPersonCardWithFilter1.FilterEnabled = false;
            }
            else
            {
                MessageBox.Show("Error saving data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
