using BusinessDVLD;
using DVLD.GloabalClasses;
using DVLD.Licenses;
using DVLD.Licenses.LocalLicense;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.DetaineLicense
{
    public partial class frmDetaineLicense : Form
    {
        private int _LicenseID = -1;
        private int _DetainedLicenseID = -1;

        public frmDetaineLicense()
        {
            InitializeComponent();
        }

        public frmDetaineLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGloabal.CurrentUser.UserName;

            if (_LicenseID == -1)
            {
                llbShowLicenseInfo.Enabled = false;
                llbShowLicenseHistory.Enabled = false;
            }
            else
            {
                ctrlLicenseInfoWithFilter1.FilterEnabled = false;
                llbShowLicenseInfo.Enabled = true;
                llbShowLicenseHistory.Enabled = true;
            }
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int LicneseID)
        {
            _LicenseID = ctrlLicenseInfoWithFilter1.License.ID;
            lblLicenseID.Text = _LicenseID.ToString();

            llbShowLicenseHistory.Enabled = (_LicenseID != -1);

            if (_LicenseID == -1) { return; }

            if (ctrlLicenseInfoWithFilter1.License.IsDetained())
            {
                MessageBox.Show("This license is already detained", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
                return;
            }

            //if (!ctrlLicenseInfoWithFilter1.License.isActive)
            //{
            //    MessageBox.Show("License is not active", "Not Allowed",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    btnDetain.Enabled = false;
            //}

            btnDetain.Enabled = true;
            txbFineFees.Focus();
        }

        private void txbAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            { return; }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {


                decimal Fees = txbFineFees.Text.Trim() == "" ? 0 : Convert.ToDecimal(txbFineFees.Text);

                int DetainedID = ctrlLicenseInfoWithFilter1.License.Detain(Fees,
                                    clsGloabal.CurrentUser.ID);
                if (DetainedID == -1)
                {
                    MessageBox.Show("License Not Detained", "Fail", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    return;
                }

                _DetainedLicenseID = DetainedID;
                lblDetainAppID.Text = DetainedID.ToString();
                MessageBox.Show("License Detained", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnDetain.Enabled = false;
                ctrlLicenseInfoWithFilter1.FilterEnabled = false;

                llbShowLicenseInfo.Enabled = true;
            }

        }
        
        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_DetainedLicenseID != -1)
            {
                frmShwoLicenseInfo frm = new frmShwoLicenseInfo(_LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("License is not detained a license yet", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void llbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_LicenseID != -1)
            {
                frmDriverLicenses frm = new frmDriverLicenses(ctrlLicenseInfoWithFilter1.License.driver.PersonID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select a license", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            if (_LicenseID == -1)
            {
                ctrlLicenseInfoWithFilter1.FilterFocus();
            }
        }
    }
}
