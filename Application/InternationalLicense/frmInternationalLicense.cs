using BusinessDVLD;
using DVLD.GloabalClasses;
using DVLD.InternationalLicense;
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

namespace DVLD.Application.InternationalLicense
{
    public partial class frmInternationalLicense : Form
    {
        private int _LicenseID = -1;
        private int _InternationalLicenseID = -1;

        public frmInternationalLicense()
        {
            InitializeComponent();
        }

        public frmInternationalLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblLocalLicenseID.Text = clsGloabal.CurrentUser.UserName;

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
            lblLocalLicenseID.Text = _LicenseID.ToString();

            llbShowLicenseHistory.Enabled = (_LicenseID != -1);

            if (_LicenseID == -1) { return; }

            if (ctrlLicenseInfoWithFilter1.License.IsDetained())
            {
                MessageBox.Show("This license is detained", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.License.isActive)
            {
                MessageBox.Show("License is not active", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            if (ctrlLicenseInfoWithFilter1.License.licenseClassID != 3) // HardCoded?? // I can create enum in clsClass to solve this
            {
                MessageBox.Show("License is not of type \"Ordinary License Class\"", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            int _InternationalID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID
                (ctrlLicenseInfoWithFilter1.License.driverID);

            if (_InternationalID != -1)
            {
                MessageBox.Show("Person already has an international license with ID " + _InternationalID, "Not Allowed",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            lblApplicationDate.Text = ctrlLicenseInfoWithFilter1.License.ApplicationInfo.Date.ToShortDateString();
            lblIssueDate.Text = ctrlLicenseInfoWithFilter1.License.issueDate.ToShortDateString();
            lblTotalFees.Text = (clsLicenseClass.FindLicenseClass(3).ClassFees +
                                clsApplicationTypes.FindApplicationType((int)
                                clsApplication.enApplicationType.New_International_License)
                                .ApplicationTypeFees).ToString();
            lblExpirationDate.Text = ctrlLicenseInfoWithFilter1.License.expirationDate.ToShortDateString();
            lblCreatedByUser.Text = clsGloabal.CurrentUser.UserName;

            btnIssue.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Issue international license?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {

                int appID = -1;

                _InternationalLicenseID = ctrlLicenseInfoWithFilter1.License.
                    IssueInternationalLicense(clsGloabal.CurrentUser.ID, ref appID);
                
                if (_InternationalLicenseID == -1)
                {
                    MessageBox.Show("International License Not Issued", "Fail", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    return;
                }

                lblInternationalLicenseID.Text = _InternationalLicenseID.ToString();
                lblInternationalApplicationID.Text = appID.ToString();
                MessageBox.Show("International License Issued Successfully", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnIssue.Enabled = false;
                ctrlLicenseInfoWithFilter1.FilterEnabled = false;

                llbShowLicenseInfo.Enabled = true;
            }

        }
        
        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_InternationalLicenseID != -1)
            {
                frmShwoInternationalLicenseInfo frm = new frmShwoInternationalLicenseInfo(_InternationalLicenseID);
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
