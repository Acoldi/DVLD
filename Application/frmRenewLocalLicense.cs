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

namespace DVLD.Licenses.LocalLicense
{
    public partial class frmRenewLocalLicense : Form
    {
        private int _NewLicenseID = -1;
        //public int LicenseID { get { return ctrlLicenseInfoWithFilter1.LicenseID; } }

        private clsLicense _NewLicense;
        //public clsLicense License { get { return ctrlLicenseInfoWithFilter1.License; } }

        public frmRenewLocalLicense()
        {
            InitializeComponent();
        }

        private void frmRenewLocalLicense_Load(object sender, EventArgs e)
        {


            llbShowLicenseInfo.Enabled = false;
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += CtrlLicenseInfoWithFilter1_OnLicenseSelected;
             //I can set this in Designer.cs!

            // Fill fillable fields
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationType((int)clsApplication.
                enApplicationType.Renew_Driving_License_Service).ApplicationTypeFees.ToString();
            lblCreatedByUserName.Text = clsGloabal.CurrentUser.UserName;

        }

        private void CtrlLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            if (LicenseID == -1)
            {
                return;
            }
            llbShowLicenseHistory.Enabled = true;


            if (!ctrlLicenseInfoWithFilter1.License.IsLicenseExpired())
            {
                MessageBox.Show("License is not expired yet!", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.License.isActive)
            {
                MessageBox.Show("License is not active!", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }


            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationType((int)clsApplication.
                enApplicationType.Renew_Driving_License_Service).ApplicationTypeFees.ToString();
            lblLicenseFees.Text = ctrlLicenseInfoWithFilter1.License.licenseClass.ClassFees.ToString();
            txbNotes.Text = ctrlLicenseInfoWithFilter1.License.notes;
            lblOldLicenseID.Text = ctrlLicenseInfoWithFilter1.License.ID.ToString();
            lblExpirationDate.Text = ctrlLicenseInfoWithFilter1.License.licenseClass.ValidationLength.ToString();
            lblCreatedByUserName.Text = clsGloabal.CurrentUser.UserName;
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblLicenseFees.Text)).ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew this license?", "Confirm"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            _NewLicense = ctrlLicenseInfoWithFilter1.License.RenewLicense(txbNotes.Text.Trim(),
                clsGloabal.CurrentUser.ID);

            if (_NewLicense == null)
            {
                MessageBox.Show("License not renewed", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblRLApplicationID.Text = _NewLicense.applicationId.ToString();
            _NewLicenseID = _NewLicense.ID;
            lblRenewedLicenseID.Text = _NewLicense.ID.ToString();

            llbShowLicenseInfo.Enabled = true;
            ctrlLicenseInfoWithFilter1.FilterEnabled = false;
            btnRenew.Enabled = false;

            MessageBox.Show("Licensed Renewed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShwoLicenseInfo frm = new frmShwoLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void frmRenewLocalLicense_Activated(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.FilterFocus();
        }
    }
}
