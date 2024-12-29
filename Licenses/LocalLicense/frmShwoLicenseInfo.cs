using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.LocalLicense
{
    public partial class frmShwoLicenseInfo : Form
    {
        private int _licenseID = -1;
        public frmShwoLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            _licenseID = LicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShwoLicenseInfo_Load(object sender, EventArgs e)
        {

            ctrlLicenseInfoCard1.LoadInfo(_licenseID);
        }
    }
}
