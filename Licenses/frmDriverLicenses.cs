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
    public partial class frmDriverLicenses : Form
    {   // Support person-search if needed

        private int _PersonID = -1;
        public frmDriverLicenses(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        public frmDriverLicenses()
        {
            InitializeComponent();
        }


        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID == -1)
            {
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlPersonCardWithFilter1._FilterFocus();
            }
            else
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlLicensHistory1.LoadInfoByPersonID(_PersonID);
            }

        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _PersonID = (int)obj;
            if (_PersonID == -1)
            {
                ctrlLicensHistory1.Clear();
            }
            else
                ctrlLicensHistory1.LoadInfoByPersonID(_PersonID);

        }
    }
}
