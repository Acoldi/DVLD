using BusinessDVLD;
using DVLD.GloabalClasses;
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

namespace DVLD.Application
{
    public partial class frmReplaceLostOrDamagedLicense : Form
    {
        public frmReplaceLostOrDamagedLicense()
        {
            InitializeComponent();
        }

        private int _NewLicenseID = -1;

        private void frmReplceLocalLicense_Load(object sender, EventArgs e)
        {
            rdDamaged.Checked = true;
            ctrlLicenseInfoWithFilter1.OnLicenseSelected += CtrlLicenseInfoWithFilter1_OnLicenseSelected;
            //I can set this in Designer.cs!

            // Fill fillable fields
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblCreatedByUserName.Text = clsGloabal.CurrentUser.UserName;
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationType(GetApplicationTypeID()).ApplicationTypeFees.ToString();

        }

        private void CtrlLicenseInfoWithFilter1_OnLicenseSelected(int LicenseID)
        {
            if (LicenseID == -1)
            {
                return;
            }
            llbShowLicenseHistory.Enabled = true;

            if (ctrlLicenseInfoWithFilter1.License.IsLicenseExpired())
            {
                MessageBox.Show("License is expired!", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Replace.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.License.isActive)
            {
                MessageBox.Show("License is not active!", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Replace.Enabled = false;
                return;
            }
            Replace.Enabled = true;

            lblLicenseFees.Text = ctrlLicenseInfoWithFilter1.License.licenseClass.ClassFees.ToString();
            lblNotes.Text = ctrlLicenseInfoWithFilter1.License.notes;
            lblOldLicenseID.Text = ctrlLicenseInfoWithFilter1.License.ID.ToString();
            lblExpirationDate.Text = ctrlLicenseInfoWithFilter1.License.licenseClass.ValidationLength.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblLicenseFees.Text)).ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlLicenseInfoWithFilter1.LicenseID == -1)
            {
                MessageBox.Show("Enter license ID", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
            }
            frmShwoLicenseInfo frm = new frmShwoLicenseInfo(ctrlLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }

        private void frmRenewLocalLicense_Activated(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.FilterFocus();
        }

        private void rdDamaged_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement foe Damaged Licens";
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationType(GetApplicationTypeID()).ApplicationTypeFees.ToString()
                ;
        }

        private void rsLost_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement foe Loast Licens";
            lblApplicationFees.Text = clsApplicationTypes.FindApplicationType(GetApplicationTypeID()).ApplicationTypeFees.ToString()
                ;
        }

        private int GetApplicationTypeID()
        {
            return rdDamaged.Checked ? (int)clsApplication.
                enApplicationType.Replacement_for_a_Damaged_Driving_License :
                (int)clsApplication.enApplicationType.Replacement_for_a_Lost_Driving_License;
        }

        private clsLicense.enIssueReason GetIssueReason()
        {
            return rdDamaged.Checked ? clsLicense.enIssueReason.Damaged :
                clsLicense.enIssueReason.Lost;
        }

        private void Replace_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to Replace this license?", "Confirm"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            clsLicense _NewLicense = ctrlLicenseInfoWithFilter1.License.Replace(GetIssueReason(),
                clsGloabal.CurrentUser.ID);

            if (_NewLicense == null)
            {
                MessageBox.Show("License not replaced", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblRLApplicationID.Text = _NewLicense.applicationId.ToString();
            _NewLicenseID = _NewLicense.ID;
            lblRenewedLicenseID.Text = _NewLicense.ID.ToString();

            llbShowLicenseInfo.Enabled = true;
            ctrlLicenseInfoWithFilter1.FilterEnabled = false;
            Replace.Enabled = false;

            MessageBox.Show("License replaced", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
