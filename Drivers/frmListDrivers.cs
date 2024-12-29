using BusinessDVLD;
using DVLD.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private DataTable _Drivers;

        private void form_Load(object sender, EventArgs e)
        {
            _Drivers = clsDriver.ListDriversView();

            cmbFilterBy.SelectedIndex = 0;
            dataGridView1.DataSource = _Drivers;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                // DriverID
                dataGridView1.Columns[0].Width = 100;
                // PersonID
                dataGridView1.Columns[1].Width = 100;
                // National no
                dataGridView1.Columns[2].Width = 150;
                // Full name
                dataGridView1.Columns[3].Width = 300;
                // Active Licences
                dataGridView1.Columns[4].Width = 180;
            }

        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                txbFilterValue.Text = "";
                txbFilterValue.Visible = false;
                _Drivers.DefaultView.RowFilter = "";
            }
            else
            {
                txbFilterValue.Visible = true;
                txbFilterValue.Focus();
                txbFilterValue.Text = "";
            }

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void txbFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterValue = cmbFilterBy.Text;

            if (txbFilterValue.Text == "" || cmbFilterBy.Text == "None")
            {  // No refresh is needed, as there are no changes on the DB level
                _Drivers.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            // Dealing with numbers // Validation is another function's responsibility
            if (FilterValue == "Driver ID" || FilterValue == "Person ID" || FilterValue == "Active Licenses")
            {
                _Drivers.DefaultView.RowFilter = $"[{FilterValue}] = {int.Parse(txbFilterValue.Text)}";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();

            }
            else
            {
                _Drivers.DefaultView.RowFilter = $"[{FilterValue}] LIKE '{txbFilterValue.Text}%'";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();

            }

        }

        private void txbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "Driver ID" || cmbFilterBy.Text == "Person ID")
            {
                e.Handled = !char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar);
                return;
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicensHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDriverLicenses frm = new frmDriverLicenses((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
    }
}
