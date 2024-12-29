using BusinessDVLD;
using DVLD.Application.LocalDrivingLicenseApplication;
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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.DetaineLicense
{
    public partial class frmListDetainedLicenses : Form
    {
        private DataTable _DetainedLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frnListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _DetainedLicenses = clsDetainedLicense.GetAllDetainedLicenses();

            cmbFilterBy.SelectedIndex = 0;
            dataGridView1.DataSource = _DetainedLicenses;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Detained ID";
                dataGridView1.Columns[0].Width = 90;

                dataGridView1.Columns[1].HeaderText = "License ID";
                dataGridView1.Columns[1].Width = 90;

                dataGridView1.Columns[2].HeaderText = "Detain Date";
                dataGridView1.Columns[2].Width = 130;

                dataGridView1.Columns[3].HeaderText = "Is Released";
                dataGridView1.Columns[3].Width = 90;

                dataGridView1.Columns[4].HeaderText = "Fine Fees";
                dataGridView1.Columns[4].Width = 100;

                dataGridView1.Columns[5].HeaderText = "Release Date";
                dataGridView1.Columns[5].Width = 130;

                dataGridView1.Columns[6].HeaderText = "National No.";
                dataGridView1.Columns[6].Width = 90;

                dataGridView1.Columns[7].HeaderText = "Full Name";
                dataGridView1.Columns[7].Width = 220;

                dataGridView1.Columns[8].HeaderText = "Release App.ID";
                dataGridView1.Columns[8].Width = 100;

            }

        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                txbFilterValue.Text = "";
                txbFilterValue.Visible = false;
                _DetainedLicenses.DefaultView.RowFilter = "";
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
                case "Detained ID":
                    FilterValue = "ID";
                    break;

                case "License ID":
                    FilterValue = "LicenseID";
                    break;

                case "Detain Date":
                    FilterValue = "DetainDate";
                    break;

                 case "Is Released":
                    FilterValue = "IsReleased"; 
                    break;

                case "Fine Fees":
                    FilterValue = "FineFees";
                    break;

                case "Release Date":
                    FilterValue = "ReleaseDate"; // - [ ] Add date time picker
                    break;

                case "National No.":
                    FilterValue = "NationalNo";
                    break;

                 case "Full Name":
                    FilterValue = "FullName";
                    break;

                 case "Release App.ID":
                    FilterValue = "ReleaseApplicationID";
                    break;

            }

            if (txbFilterValue.Text == "" || cmbFilterBy.Text == "None")
            {  // No refresh is needed, as there are no changes on the DB level
                _DetainedLicenses.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            // Dealing with numbers // Validation is another function's responsibility
            if (FilterValue == "ReleaseApplicationID" || FilterValue == "FineFees"
                || FilterValue == "LicenseID" || FilterValue == "ID")
            {
                _DetainedLicenses.DefaultView.RowFilter = $"{FilterValue} = {int.Parse(txbFilterValue.Text)}";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();

            }
            else
            {
                _DetainedLicenses.DefaultView.RowFilter = $"{FilterValue} LIKE '{txbFilterValue.Text}%'";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();

            }

        }

        private void txbFilterValue_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "ReleaseApplicationID" || cmbFilterBy.Text == "FineFees"
                || cmbFilterBy.Text == "LicenseID" || cmbFilterBy.Text == "ID")
            {
                e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
             frmDetaineLicense frm = new frmDetaineLicense();
            frm.ShowDialog();

            frnListDetainedLicenses_Load(null, null);
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((string)dataGridView1.CurrentRow.Cells[6].Value);
            frm.ShowDialog();
        }

        private void ReleasetoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense((int)dataGridView1.
                CurrentRow.Cells[1].Value);
            frm.ShowDialog();

            frnListDetainedLicenses_Load(null, null);
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            frmShwoLicenseInfo frm = new frmShwoLicenseInfo((int)dataGridView1.
                CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            clsPerson Person = clsPerson.FindPerson((string
                )dataGridView1.CurrentRow.Cells[6].Value);
            if (Person == null) {return; }

            frmDriverLicenses frmDriverLicenses = new frmDriverLicenses(Person.ID);
            frmDriverLicenses.ShowDialog();
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();

            frnListDetainedLicenses_Load(null, null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            tsmRelease.Enabled = !(bool)dataGridView1.CurrentRow.Cells[3].Value;
        }
    }
}
