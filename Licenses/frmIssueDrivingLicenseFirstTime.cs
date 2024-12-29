using BusinessDVLD;
using DVLD.GloabalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.LocalDrivingLicenseApplication
{
    public partial class frmIssueDrivingLicenseFirstTime : Form
    {
        private int _LocalAppID = -1;
        private clsLDLA _LocalApplication;
        public frmIssueDrivingLicenseFirstTime(int localAppID)
        {
            InitializeComponent();
            _LocalAppID = localAppID;
        }

        private void frmIssueDrivingLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _LocalApplication = clsLDLA.FindLDLAppByID(_LocalAppID);

            if (_LocalApplication == null)
            {
                MessageBox.Show("No local application with ID " + _LocalAppID, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txbNotes.Focus();

            // If Person Did not pass all tests
            if (!_LocalApplication.PassedAllTests())
            {
                MessageBox.Show("Person did not pass all tests", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            // there should be no active License
            int LicenceID = _LocalApplication.GetActiveLicenseID();
            if ( LicenceID != -1)
            {
                MessageBox.Show("Person already have anc active license With ID " + LicenceID, "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlLocalApplicationCardInfo1.LoadInfo(_LocalAppID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalApplication.IssueLicenseFirstTime(txbNotes.Text.Trim(), clsGloabal.CurrentUser.ID);

            if (LicenseID == -1)
            {
                MessageBox.Show("Data not saved", "Fail",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Data saved", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }


        }
    }
}
