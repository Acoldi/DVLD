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

namespace DVLD.TestType
{
    public partial class frmUpdateTestType : Form
    {

        private clsTestTypes _TestType;
        private int _TestTypeID = -1;
        
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            this._TestTypeID = TestTypeID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Follow the red mark", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TypeTitle = txbTitle.Text;
            _TestType.Fees = Convert.ToDecimal(txbFees.Text);
            _TestType.Description = txbDescription.Text;

            if (_TestType.Save())
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
            lblID.Text = _TestTypeID.ToString();

            _TestType = clsTestTypes.FindTestType((clsTestTypes.enTestTypes)_TestTypeID);

            if (_TestType == null)
            {
                MessageBox.Show($"Application type with {_TestTypeID} not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txbTitle.Text = _TestType.TypeTitle;
            txbFees.Text = _TestType.Fees.ToString();
            txbDescription.Text = _TestType.Description;
        }

        private void txbDescription_Validating(object sender, CancelEventArgs e)
        {
            if (txbDescription.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txbDescription, "This filed can't be empty");
                return;
            }
            else
                errorProvider1.SetError(txbDescription, null);
        }
    }
}
