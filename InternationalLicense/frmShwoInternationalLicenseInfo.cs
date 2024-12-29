using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.InternationalLicense
{
    public partial class frmShwoInternationalLicenseInfo : Form
    {
        private int _InternationallicenseID = -1;
        public frmShwoInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationallicenseID = InternationalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShwoLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseInfoCard1.LoadInfo(_InternationallicenseID);
        }
    }
}
