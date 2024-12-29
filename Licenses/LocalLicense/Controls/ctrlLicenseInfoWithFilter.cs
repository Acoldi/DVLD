using BusinessDVLD;
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

namespace DVLD.Licenses.LocalLicense
{
    public partial class ctrlLicenseInfoWithFilter : UserControl
    {
        public delegate void LicenseSelected(int LicneseID);
        public event LicenseSelected OnLicenseSelected;

        protected virtual void LicenseSelectedHandler(int LicenseID)
        {
            OnLicenseSelected?.Invoke(LicenseID);
        }

        private int _LicenseID = -1;
        public int LicenseID { get { return ctrlLicenseInfoCard1.licenseID; } }
        public clsLicense License
        {
            get
            {
                return ctrlLicenseInfoCard1.LicenseInfo;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            set
            {
                _FilterEnabled = value;

                gbFilter.Enabled = value;
            }
            get
            {
                return _FilterEnabled;
            }
        }

        public ctrlLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public void LoadInfo(int LicenseID)
        {
            txbLicenseID.Text = LicenseID.ToString();
            ctrlLicenseInfoCard1.LoadInfo(LicenseID);

            if (ctrlLicenseInfoCard1.licenseID == -1) { MessageBox.Show("No license with ID " + LicenseID, 
                "fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            _LicenseID = ctrlLicenseInfoCard1.licenseID;

            OnLicenseSelected?.Invoke(_LicenseID);
        }

        private void txbLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (txbLicenseID.Text.Trim() == "")
            {
                errorProvider1.SetError(txbLicenseID, "Enter License ID");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbLicenseID, null);
            }


            if (!int.TryParse(txbLicenseID.Text.Trim(), out _LicenseID))
            {
                errorProvider1.SetError(txbLicenseID, "Invalid Number");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbLicenseID, null);
            }

        }


        private void btnFInd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("See red mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadInfo(_LicenseID);
            
        }

        private void ctrlLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {

        }

        public void FilterFocus()
        {
            txbLicenseID.Focus();
        }

        private void txbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFInd.PerformClick();
            }
        }
    }
}
