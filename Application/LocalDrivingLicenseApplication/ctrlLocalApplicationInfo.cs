using BusinessDVLD;
using DVLD.GloabalClasses;
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

namespace DVLD.Application.Controls
{
    public partial class ctrlLocalApplicationCardInfo : UserControl
    {
        private int _LocalApplicationID = -1;
        public int LocalApplicationID
        {
            get { return _LocalApplicationID; }
        }

        private clsLDLA _LocalApplication { get; set; }

        public clsLDLA LocalApplicationInfo // Will I need this?
        {
            get { return _LocalApplication; }
        }

        public ctrlLocalApplicationCardInfo()
        {
            InitializeComponent();
        }

        public void LoadInfo(int LDLAppID)
        {
            _LocalApplicationID = LDLAppID;

            _LocalApplication = clsLDLA.FindLDLAppByID(LDLAppID);
            if (_LocalApplication == null)
            {
                _ResetPersonInfo();

                MessageBox.Show("Application with ID: " + LDLAppID + " is not found", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                _FillLocalAppInfo();
        }

        private void _ResetPersonInfo()
        {
            _LocalApplicationID = -1;
            lblLocalAppID.Text = "[???]";
            lblPassedTests.Text = "[???]";
            lblAppliedForLicense.Text = "[???]";

            llbShowLicenseInfo.Enabled = false;
            ctrlBaseAppInfo1.ResetAppInfo();
        }

        private void _FillLocalAppInfo()
        {
            lblLocalAppID.Text = _LocalApplicationID.ToString();
            lblAppliedForLicense.Text = _LocalApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = clsLDLA.GetPassedTestsCount(LocalApplicationID).ToString();

            llbShowLicenseInfo.Enabled = true; // if there is a an active license

            ctrlBaseAppInfo1.LoadInfo(_LocalApplication.ApplicationID);
        }

        private void llbShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShwoLicenseInfo frm = new frmShwoLicenseInfo(_LocalApplication.LicenseClassInfo.ID);
            frm.ShowDialog();
        }
    }
}
