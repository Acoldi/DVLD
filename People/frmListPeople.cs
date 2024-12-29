using BusinessDVLD;
using DVLD.People;
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

namespace DVLD
{
    public partial class frmListPeople : Form
    {
        private static DataTable dtAllPeople = clsPerson.GetAllPeople();

        private DataTable _dtPeople = dtAllPeople.DefaultView.ToTable(false, "ID", "NationalNo", "FirstName", "SecondName", "ThirdName"
                                                        , "LastName", "Gendor", "DateOfBirth", "Phone", "Email");

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dtPeople;
            lblRecords.Text = _dtPeople.Rows.Count.ToString();

            // Filter by none
            cmbFilterBy.SelectedIndex = 0;

            if (_dtPeople.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[1].HeaderText = "National No.";
                dataGridView1.Columns[2].HeaderText = "First Name";
                dataGridView1.Columns[3].HeaderText = "Second Name";
                dataGridView1.Columns[4].HeaderText = "Third Name";
                dataGridView1.Columns[5].HeaderText = "Last Name";
                dataGridView1.Columns[6].HeaderText = "Gendor";
                dataGridView1.Columns[7].HeaderText = "Date Of Birth";
                dataGridView1.Columns[8].HeaderText = "Phone Number";
                dataGridView1.Columns[9].HeaderText = "Email";
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dataGridView1.SelectedCells[0].Value);
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.ShowDialog();

            _RefreshData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson((int)dataGridView1.SelectedCells[0].Value);
            frm.ShowDialog();

            _RefreshData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this person?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)dataGridView1.SelectedCells[0].Value))
                {
                    MessageBox.Show("Person deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _RefreshData();
                }
                else
                {
                    MessageBox.Show("Person is not deleted due to linked data", "Faile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void txbFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterBy = "";

            switch (cmbFilterBy.Text)
            {
                case "None":
                    FilterBy = "";
                    break;

                case "ID":
                    FilterBy = "ID";
                    break;

                case "National No.":
                    FilterBy = "NationalNo";
                break;

                case "First Name":
                    FilterBy = "FirstName";
                break;

                case "Second Name":
                    FilterBy = "SecondName";
                break;

                case "Third Name":
                    FilterBy = "ThirdName";
                break;

                case "Last Name":
                    FilterBy = "LastName";
                break;

                case "Date of Birth":
                    FilterBy = "DateOfBirth";
                break;

                case "Gendor":
                    FilterBy = "Gendor";
                break;

                case "Phone":
                    FilterBy = "Phone";
                break;

                case "Email":
                    FilterBy = "Email";
                break;

            }

            if (FilterBy == "None" || txbFilterValue.Text.Trim() == "")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            if (FilterBy == "ID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterBy, int.Parse(txbFilterValue.Text));
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FilterBy, txbFilterValue.Text);

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilterValue.Visible = cmbFilterBy.Text != "None";

            if (txbFilterValue.Visible)
            {
                txbFilterValue.Text = "";
                txbFilterValue.Focus();
            }
        }

        private void _RefreshData() // ?
        {
            dtAllPeople = clsPerson.GetAllPeople();

            _dtPeople = dtAllPeople.DefaultView.ToTable(false, "ID", "NationalNo", "FirstName", "SecondName", "ThirdName"
                                                        , "LastName", "Gendor", "DateOfBirth", "Phone", "Email");

            dataGridView1.DataSource = _dtPeople;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frmAddEditPerson = new frmAddEditPerson();
            frmAddEditPerson.ShowDialog();

            _RefreshData();
        }

        private void txbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
