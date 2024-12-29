using BusinessDVLD;
using DVLD.GloabalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.ApplicationTypes
{
    public partial class frmUpdateApplicationType : Form
    {
        private clsApplicationTypes _AppType;
        private int _AppTypeID = -1;

        public frmUpdateApplicationType(int appTypeID)
        {
            InitializeComponent();
            _AppTypeID = appTypeID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Follow the red mark", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _AppType.TypeTitle = txbTitle.Text;
            _AppType.ApplicationTypeFees = Convert.ToDecimal(txbFees.Text);

            if (_AppType.Save())
            {
                MessageBox.Show("Application type updated successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnClose.Focus();
            }
            else
                MessageBox.Show("Error happened while saving data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbTitle_Validating(object sender, CancelEventArgs e)
        {
            if (txbTitle.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txbTitle, "This filed can't be empty");
                return;
            }
            else
                errorProvider1.SetError(txbTitle, null);
        }

        private void txbFees_Validating(object sender, CancelEventArgs e)
        {
            if (txbFees.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txbFees, "This filed can't be empty");
                return;
            }
            else
                errorProvider1.SetError(txbFees, null);

            if (!clsValidation.isNumber(txbFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbFees, "Please enter numeric value");
                return;
            }
            else
                errorProvider1.SetError(txbFees, null);

        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            lblID.Text = _AppTypeID.ToString();

            _AppType = clsApplicationTypes.FindApplicationType(_AppTypeID);

            if (_AppType == null)
            {
                MessageBox.Show($"Application type with {_AppTypeID} not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txbTitle.Text = _AppType.TypeTitle;
            txbFees.Text = _AppType.ApplicationTypeFees.ToString();
        }

    }
}
