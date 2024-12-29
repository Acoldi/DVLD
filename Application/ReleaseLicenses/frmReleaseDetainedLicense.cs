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
using System.Xml.Linq;

namespace DVLD.Application
{
    public partial class frmReleaseDetainedLicense : Form
    {
        private int _LicenseID = -1;

        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int licenseID)
        {
            InitializeComponent();
            _LicenseID = licenseID;
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
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
           //     llbShowLicenseInfo.Enabled = true;
                llbShowLicenseHistory.Enabled = true;
                ctrlLicenseInfoWithFilter1.FilterEnabled = false;

                ctrlLicenseInfoWithFilter1.LoadInfo(_LicenseID);
            }
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            if (LicenseID == -1)
            {
                return;
            }
            llbShowLicenseHistory.Enabled = true;

            if (!ctrlLicenseInfoWithFilter1.License.IsDetained())
            {
                MessageBox.Show("License is not detained", "Fail",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDetainedLicenseID.Text = ctrlLicenseInfoWithFilter1.License.DetainedLicenseInfo.ID.ToString();
            lblDetainDate.Text = ctrlLicenseInfoWithFilter1.License.DetainedLicenseInfo.DetainDate.ToString();
            lblFineFees.Text = ctrlLicenseInfoWithFilter1.License.DetainedLicenseInfo.FineFees.ToString();
            lblAppFees.Text = clsApplicationTypes.FindApplicationType(
                (int)clsApplication.enApplicationType.Release_Detained_Driving_License).ApplicationTypeFees.ToString();
            lblLicenseID.Text = ctrlLicenseInfoWithFilter1.License.ID.ToString();
            lblCreatedBy.Text = ctrlLicenseInfoWithFilter1.License.DetainedLicenseInfo.CreatedByUserInfo.UserName.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblAppFees.Text) + Convert.ToDecimal(lblFineFees.Text)).ToString();

            btnRelease.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Releasing license?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            int ReleaseApplicationID = -1;

            if (ctrlLicenseInfoWithFilter1.License.ReleaseDetainedLicense(
                ref ReleaseApplicationID, clsGloabal.CurrentUser.ID
                ))
            {
                MessageBox.Show("License Released", "Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblReleaseAppID.Text = ReleaseApplicationID.ToString();

                btnRelease.Enabled = false;
                ctrlLicenseInfoWithFilter1.FilterEnabled = false;
                llbShowLicenseInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("License Not Released", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShwoLicenseInfo frm = new frmShwoLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void llbShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlLicenseInfoWithFilter1.LicenseID == -1)
            {
                return;
            }
            frmDriverLicenses frm = new frmDriverLicenses(ctrlLicenseInfoWithFilter1.License.driver.PersonID);
            frm.ShowDialog();
        }
    }
}
