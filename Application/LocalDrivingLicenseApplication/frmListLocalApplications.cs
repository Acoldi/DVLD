using BusinessDVLD;
using DVLD.Licenses;
using DVLD.Licenses.LocalLicense;
using DVLD.Tests;
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

namespace DVLD.Application.LocalDrivingLicenseApplication
{
    public partial class frmListLocalApplications : Form
    {
        private DataTable _LocalApplications;

        public frmListLocalApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalApplications_Load(object sender, EventArgs e)
        {
            _LocalApplications = clsLDLA.GetLDLAview();

            cmbFilterBy.SelectedIndex = 0;
            dataGridView1.DataSource = _LocalApplications;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "L.D.L.App ID";
                dataGridView1.Columns[0].Width = 90;

                dataGridView1.Columns[1].HeaderText = "License Class";
                dataGridView1.Columns[1].Width = 220;

                dataGridView1.Columns[2].HeaderText = "National No.";
                dataGridView1.Columns[2].Width = 100;

                dataGridView1.Columns[3].HeaderText = "Full Name";
                dataGridView1.Columns[3].Width = 220;

                dataGridView1.Columns[4].HeaderText = "Application Date";
                dataGridView1.Columns[4].Width = 110;

                dataGridView1.Columns[5].HeaderText = "Passed Tests";
                dataGridView1.Columns[5].Width = 100;

                dataGridView1.Columns[6].HeaderText = "Status";
                dataGridView1.Columns[6].Width = 110;
            }

        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                txbFilterValue.Text = "";
                txbFilterValue.Visible = false;
                _LocalApplications.DefaultView.RowFilter = "";
            }
            else
            {
                txbFilterValue.Visible = true;
                txbFilterValue.Focus();
                txbFilterValue.Text = "";
            }

            lblRecords.Text = dataGridView1.Rows.Count .ToString();
        }

        private void txbFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterValue = "";

            switch (cmbFilterBy.Text)
            {
                case "L.D.L.App ID":
                    FilterValue = "LDLAPPID";
                    break;

                case "License Class":
                    FilterValue = "LicenseClassName";
                    break;

                case "National No.":
                    FilterValue = "NationalNo";
                    break;

                 case "Full Name":
                    FilterValue = "FullName";
                    break;

                case "Passed Tests":
                    FilterValue = "PassedTests";
                    break;

                case "Status":
                    FilterValue = "Status";
                    break;

            }

            if (txbFilterValue.Text == "" || cmbFilterBy.Text == "None")
            {  // No refresh is needed, as there are no changes on the DB level
                _LocalApplications.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            // Dealing with numbers // Validation is another function's responsibility
            if (FilterValue == "LDLAPPID" || FilterValue == "PassedTests")
            {
                _LocalApplications.DefaultView.RowFilter = $"{FilterValue} = {int.Parse(txbFilterValue.Text)}";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();

            }
            else
            {
                _LocalApplications.DefaultView.RowFilter = $"{FilterValue} LIKE '{txbFilterValue.Text}%'";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();

            }

        }

        private void txbFilterValue_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "L.D.L.App ID" || cmbFilterBy.Text == "Passed Tests")
            {
                e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplicatio frm = new frmAddUpdateLocalDrivingLicenseApplicatio();
            frm.ShowDialog();

            frmListLocalApplications_Load(null, null);
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplicatio frm = 
                    new frmAddUpdateLocalDrivingLicenseApplicatio((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            
            frmListLocalApplications_Load(null, null);
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalApplicationCard frm = new frmLocalApplicationCard((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this application?", "Delete",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
                return;

            clsLDLA LApp = clsLDLA.FindLDLAppByID((int)dataGridView1.CurrentRow.Cells[0].Value);
            if ( LApp == null)
            {
                MessageBox.Show("Local App not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LApp.Delete())
            {
                MessageBox.Show("Local Application Deleted", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListLocalApplications_Load(null, null);
                return;
            }
            else
            {
                MessageBox.Show("Local Application Not Deleted, data linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void CanceltoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Cancel this application?", "Cancel",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
                return;

            clsLDLA LApp = clsLDLA.FindLDLAppByID((int)dataGridView1.CurrentRow.Cells[0].Value);
            if (LApp == null)
            {
                MessageBox.Show("Local App not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (LApp.Cancel())
            {
                MessageBox.Show("Local Application Cancelled", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListLocalApplications_Load(null, null);
                return;
            }
            else
            {
                MessageBox.Show("Local Application Not Cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value
                                , clsTestTypes.enTestTypes.VisionTest);
            frm.ShowDialog();
            frmListLocalApplications_Load(null, null);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value
                                , clsTestTypes.enTestTypes.WrittenTest);
            frm.ShowDialog();
            frmListLocalApplications_Load(null, null);

        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestAppointments frm = new frmListTestAppointments((int)dataGridView1.CurrentRow.Cells[0].Value
                                , clsTestTypes.enTestTypes.StreetTest);
            frm.ShowDialog();
            frmListLocalApplications_Load(null, null);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            clsLDLA LocalApplication = clsLDLA.FindLDLAppByID((int)dataGridView1.CurrentRow.Cells[0].Value);
            int PassedTestCount = (int)dataGridView1.CurrentRow.Cells[5].Value;

            // Edit,Delete Application
            if (PassedTestCount > 0)
            {
                tsmEditApp.Enabled = false;
                tsmDelete.Enabled = false;
            }
            else
            {
                tsmEditApp.Enabled = true;
                tsmDelete.Enabled = true;
            }
            
            // Cancel
            if (LocalApplication.ApplicationStatus == clsApplication.enStatus.Completed)
                tsmCancel.Enabled = false;
            else
                tsmCancel.Enabled = true;

            // Schedule Test
            if (PassedTestCount == 3 || LocalApplication.ApplicationStatus == clsApplication.enStatus.Canceled)
                tsmScheduleTests.Enabled = false;
            else
                tsmScheduleTests.Enabled = true;
            
            // Issue License
            if (!LocalApplication.HasActiveLicense() && PassedTestCount == 3)
            {
                tsmIssueFirstTime.Enabled = true;
            }
            else
            {
                tsmIssueFirstTime.Enabled = false;
            }

            // Show License
            if (LocalApplication.ApplicationStatus == clsApplication.enStatus.Completed)
            {
                tsmShowLicense.Enabled = true;
            }
            else
                tsmShowLicense.Enabled = false;

            // If no test passed
            if (PassedTestCount == 0)
            {
                tsmVisionTest.Enabled = true;
                tsmWritterTest.Enabled = false;
                tsmStreetTest.Enabled = false;
            }
            else if (PassedTestCount == 1)
            {
                tsmVisionTest.Enabled = false;
                tsmWritterTest.Enabled = true;
                tsmStreetTest.Enabled = false;
            }
            else if (PassedTestCount == 2)
            {
                tsmVisionTest.Enabled = false;
                tsmWritterTest.Enabled = false;
                tsmStreetTest.Enabled = true;
            }
            else
            {
                tsmVisionTest.Enabled = false;
                tsmWritterTest.Enabled = false;
                tsmStreetTest.Enabled = false;
            }

        }

        private void tsmIssueFirstTime_Click(object sender, EventArgs e)
        {
            frmIssueDrivingLicenseFirstTime frm = new frmIssueDrivingLicenseFirstTime((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListLocalApplications_Load(null, null);
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            frmShwoLicenseInfo frm = new frmShwoLicenseInfo(clsLDLA.FindLDLAppByID((int)dataGridView1.
                CurrentRow.Cells[0].Value).GetActiveLicenseID());
            frm.ShowDialog();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.FindPerson((string
                )dataGridView1.CurrentRow.Cells[2].Value);
            if (Person == null) {return; }

            frmDriverLicenses frmDriverLicenses = new frmDriverLicenses(Person.ID);
            frmDriverLicenses.ShowDialog();
        }
    }
}
