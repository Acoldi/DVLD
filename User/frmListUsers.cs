using BusinessDVLD;
using DVLD.GloabalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers;

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();

            frmListUsers_Load(null, null);
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();

            dataGridView1.DataSource = _dtAllUsers;
            cmbFilterBy.SelectedIndex = 0;
            lblRecords.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "User ID";

                dataGridView1.Columns[1].HeaderText = "Person ID";
                dataGridView1.Columns[1].Width = 110;

                dataGridView1.Columns[2].HeaderText = "User Name";
                dataGridView1.Columns[2].Width = 120;

                dataGridView1.Columns[3].HeaderText = "Full Name";
                dataGridView1.Columns[3].Width = 350;

                dataGridView1.Columns[4].HeaderText = "Is Active";
            }

        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "Is Active")
            {
                txbFilterValue.Visible = false;
                cmbIsActive.Visible = true;
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 0;
            }
            else
            {
                txbFilterValue.Visible = (cmbFilterBy.Text != "None");
                cmbIsActive.Visible = false;
                txbFilterValue.Focus();
                txbFilterValue.Text = "";
            }

        }

        private void cmbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbIsActive.Text)
            {
                case "All":
                    _dtAllUsers.DefaultView.RowFilter = "";
                    break;
                
                case "No":
                    _dtAllUsers.DefaultView.RowFilter = "IsActive = 0";
                    break;

                case "Yes":
                    _dtAllUsers.DefaultView.RowFilter = "IsActive = 1";
                    break;

            }

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void txbFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "User ID" || cmbFilterBy.Text == "Person ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txbFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn;

            switch (cmbFilterBy.Text)
            {
                case ("None"):
                    FilterColumn = "";
                    break;

                case ("User ID"):
                    FilterColumn = "ID";
                    break;

                case ("Person ID"):
                    FilterColumn = "PersonID";
                    break;

                case ("Is Active"):
                    FilterColumn = "IsActive";
                    break;

                case ("User Name"):
                    FilterColumn = "UserName";
                    break;

                default:
                    FilterColumn = "";
                    break;

            }

            // If no filter is applied
            if (cmbFilterBy.Text == "None" || txbFilterValue.Text.Trim() == "")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecords.Text = dataGridView1.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "ID" || FilterColumn == "PersonID")
                _dtAllUsers.DefaultView.RowFilter = string.Format("{0} = {1}", FilterColumn, int.Parse(txbFilterValue.Text));

            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("{0} LIKE '{1}%'", FilterColumn, txbFilterValue.Text);

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();

            frmListUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListUsers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!clsUser.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("User not deleted, it has data linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Deleted successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListUsers_Load(null, null);
            }

            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
