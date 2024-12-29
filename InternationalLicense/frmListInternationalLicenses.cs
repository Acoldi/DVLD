using BusinessDVLD;
using DVLD.Application.InternationalLicense;
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

namespace DVLD.InternationalLicense
{
    public partial class frmListInternationalLicenses : Form
    {
        private DataTable _InternationalLicenses;

        public frmListInternationalLicenses()
        {
            InitializeComponent();
        }

        private void frnListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _InternationalLicenses = clsInternationalLicense.GetInternationalLicenses();

            cmbFilterBy.SelectedIndex = 0;
            dataGridView1.DataSource = _InternationalLicenses;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "I.Licence ID";
                dataGridView1.Columns[0].Width = 100;

                dataGridView1.Columns[1].HeaderText = "Application ID";
                dataGridView1.Columns[1].Width = 100;
                                                                           
                dataGridView1.Columns[2].HeaderText = "Driver ID";       
                dataGridView1.Columns[2].Width = 100;                      
                                                                           
                dataGridView1.Columns[3].HeaderText = "L.License ID";       
                dataGridView1.Columns[3].Width = 100;                       
                                                                           
                dataGridView1.Columns[4].HeaderText = "Issue Date";         
                dataGridView1.Columns[4].Width = 250;

                dataGridView1.Columns[5].HeaderText = "Expiration Date";
                dataGridView1.Columns[5].Width = 250;

                dataGridView1.Columns[6].HeaderText = "Is Active";
                dataGridView1.Columns[6].Width = 150;

            }

        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                txbFilterValue.Text = "";
                txbFilterValue.Visible = false;
                _InternationalLicenses.DefaultView.RowFilter = "";
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
                //Application ID
                //Driver ID
                //L.License ID
                //Issue Date
                //Expiration Date
                //Is Active


                case "I.Licence ID":
                    FilterValue = "ID";
                    break;

                case "Application ID":
                    FilterValue = "ApllicationID";
                    break;

                case "Driver ID":
                    FilterValue = "DriverID";
                    break;

                case "L.License ID":
                    FilterValue = "IssuedByLocalLicenseID";
                    break;

                 case "Issue Date":
                    FilterValue = "IssueDate";
                    break;

                case "Expiration Date":
                    FilterValue = "ExperationDate";
                    break;

                case "Is Active":
                    FilterValue = "IsActive";
                    break;

            }

            if (txbFilterValue.Text == "" || cmbFilterBy.Text == "None")
            {  // No refresh is needed, as there are no changes on the DB level
                _InternationalLicenses.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            // Dealing with numbers // Validation is another function's responsibility
            if (FilterValue == "ApllicationID" || FilterValue == "ID"
                || FilterValue == "IssuedByLocalLicenseID" || FilterValue == "DriverID")
            {
                _InternationalLicenses.DefaultView.RowFilter = $"{FilterValue} = {int.Parse(txbFilterValue.Text)}";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();

            }
            else
            {
                _InternationalLicenses.DefaultView.RowFilter = $"{FilterValue} LIKE '{txbFilterValue.Text}%'";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();

            }

        }

        private void txbFilterValue_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void txbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "ID" || cmbFilterBy.Text == "ApllicationID"
                || cmbFilterBy.Text == "DriverID" || cmbFilterBy.Text == "IDIssuedByLocalLicenseID")
            {
                e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
             frmInternationalLicense frm = new frmInternationalLicense();
            frm.ShowDialog();

            frnListDetainedLicenses_Load(null, null);
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsDriver Driver = clsDriver.FindDriverByID((int)dataGridView1.CurrentRow.Cells[2].Value);
            
            frmShowPersonInfo frm = new frmShowPersonInfo(Driver.PersonID);
            frm.ShowDialog();
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            frmShwoInternationalLicenseInfo frm = new frmShwoInternationalLicenseInfo((int)dataGridView1.
                CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            clsDriver Driver = clsDriver.FindDriverByID((int)dataGridView1.CurrentRow.Cells[2].Value);

            frmDriverLicenses frmDriverLicenses = new frmDriverLicenses(Driver.PersonID);
            frmDriverLicenses.ShowDialog();
        }

    }
}
