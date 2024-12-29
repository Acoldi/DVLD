using BusinessDVLD;
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

namespace DVLD.Licenses
{
    public partial class ctrlLicensHistory : UserControl
    {
        private int _DriverID = -1;
        public int DriverID { get { return _DriverID; } }
        private clsDriver _Driver;

        private DataTable _LocalLicenses;
        private DataTable _InternationalLicenses;

        public ctrlLicensHistory()
        {
            InitializeComponent();
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindDriverByID(_DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("No driver with ID " + _DriverID , "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _DriverID = -1;
                return;
            }

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }
        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindDriverByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("No driver with person ID " + PersonID , "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _Driver.ID;

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();

        }

        public void Clear()
        {
            _LocalLicenses.Clear();
            _InternationalLicenses.Clear();
        }

        private void _LoadLocalLicenseInfo()
        {
            _LocalLicenses = _Driver.GetLicenses();
            dgvLocal.DataSource = _LocalLicenses;

            if (dgvLocal.Columns.Count > 0)
            {
                dgvLocal.Columns[0].HeaderText = "Lice. ID";
                dgvLocal.Columns[0].Width = 100;

                dgvLocal.Columns[1].HeaderText = "App. ID";
                dgvLocal.Columns[1].Width = 90;

                dgvLocal.Columns[2].HeaderText = "Class Name";
                dgvLocal.Columns[2].Width = 250;

                dgvLocal.Columns[3].HeaderText = "Issue Date";
                dgvLocal.Columns[3].Width = 150;

                dgvLocal.Columns[4].HeaderText = "Expiration Date";
                dgvLocal.Columns[4].Width = 150;

                dgvLocal.Columns[5].HeaderText = "Is Active";
                dgvLocal.Columns[5].Width = 100;

            }
        }

        private void _LoadInternationalLicenseInfo()
        {
            _InternationalLicenses = _Driver.GetInternationalLicenses();
            dgvInternational.DataSource = _InternationalLicenses;

            if (dgvLocal.Columns.Count > 0)
            {
                dgvLocal.Columns[0].HeaderText = "I.Lice. ID";
                dgvLocal.Columns[0].Width = 100;

                dgvLocal.Columns[1].HeaderText = "App. ID";
                dgvLocal.Columns[1].Width = 100;

                dgvLocal.Columns[2].HeaderText = "L.License ID";
                dgvLocal.Columns[2].Width = 250;

                dgvLocal.Columns[3].HeaderText = "Issue Date";
                dgvLocal.Columns[3].Width = 200;

                dgvLocal.Columns[4].HeaderText = "Expiration Date";
                dgvLocal.Columns[4].Width = 200;

                dgvLocal.Columns[5].HeaderText = "Is Active";
                dgvLocal.Columns[5].Width = 100;

            }
        }



        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShwoLicenseInfo frm = new frmShwoLicenseInfo( (int)dgvLocal.CurrentRow.Cells[0].Value );
            frm.ShowDialog();
        }
    }
}
