using BusinessDVLD;
using DVLD.GloabalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.LocalDrivingLicenseApplication
{
    public partial class frmAddUpdateLocalDrivingLicenseApplicatio : Form
    {
        private int _LocalDrivingLicenseAppID = -1;
        private clsLDLA _LocalDrivingLicenseApplication;

        private int _SelectedPersonID = -1;
        public enum enMode { AddNew = 1, Update = 2 }
        public enMode Mode;

        public frmAddUpdateLocalDrivingLicenseApplicatio()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplicatio(int LocalAppID)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppID = LocalAppID;

            Mode = enMode.Update;
        }

        private void frmAddUpdateLocalDrivingLicenseApplicatio_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (Mode == enMode.Update)
            {
                _LoadInfo();
            }
        }

        private void _FillcmbLicesneClasses()
        {
            DataTable dataTable = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow row in dataTable.Rows)
            {
                cmbLlicenseClasses.Items.Add(row["ClassName"]);
            }
        }

        private void _ResetDefaultValues()
        {
            _FillcmbLicesneClasses();

            if (Mode == enMode.Update)
            {
                lblAddUpdate.Text = "Update Local Application";
                this.Text = "Update Local Application";

                btnSave.Enabled = true;

                return;
            }


            lblAddUpdate.Text = "Add New Local Application";
            this.Text = "Add New Local Application";

            _LocalDrivingLicenseApplication = new clsLDLA();
            tbApplicationInfo.Enabled = false;
            btnSave.Enabled = false;
            ctrlPersonCardWithFilter1._FilterFocus();

            lblLocalAppID.Text = "??";
            lblLocalAppFees.Text = clsApplicationTypes.FindApplicationType
                        ((int)clsApplication.enApplicationType.New_Local_Driving_License_Service)
                        .ApplicationTypeFees.ToString();
            cmbLlicenseClasses.SelectedIndex = 2;
            lblLocalAppDate.Text = DateTime.Today.ToShortDateString();
            lblCreatedByUser.Text = clsGloabal.CurrentUser.UserName;
        }

        private void _LoadInfo()
        {
            _LocalDrivingLicenseApplication = clsLDLA.FindLDLAppByID(_LocalDrivingLicenseAppID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show($"Local application with {_LocalDrivingLicenseAppID} is not found", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            // Filling first tab info
            this.Text = "Update Local Application";
            lblAddUpdate.Text = "Update Local Application";
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            btnSave.Enabled = true;

            // Filling second tab info
            tbApplicationInfo.Enabled = true;
            lblLocalAppID.Text = _LocalDrivingLicenseAppID.ToString();
            lblLocalAppFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblLocalAppDate.Text = _LocalDrivingLicenseApplication.Date.ToShortDateString();
            cmbLlicenseClasses.SelectedIndex = cmbLlicenseClasses.FindString(_LocalDrivingLicenseApplication.LicenseClassInfo.ClassName);
            lblCreatedByUser.Text = clsUser.FindUser(_LocalDrivingLicenseApplication.CreatedByUserID).UserName; // This could be improved
            // when utilizing compostions.
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClass.FindLicenseClass(cmbLlicenseClasses.Text).ID;

            // One may not have more than one active license class
            if (clsLicense.HasActiveLicense(ctrlPersonCardWithFilter1.PersonID, LicenseClassID))
            {
                MessageBox.Show("Person already have an active license of this class", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            // Person may not have an active application (Application of status "New")
            // for this specific LicenseClass of his choice.
            if (clsApplication.GetActiveApplicationID(ctrlPersonCardWithFilter1.PersonID,
                        (int)clsApplication.enApplicationType.New_Local_Driving_License_Service, LicenseClassID) != -1)
            {
                MessageBox.Show("This person already has active application of the same license class", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToDecimal(lblLocalAppFees.Text);
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsLDLA.enStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = (int)clsLDLA.enApplicationType.New_Local_Driving_License_Service;
            _LocalDrivingLicenseApplication.Date = DateTime.Now;
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGloabal.CurrentUser.ID;

            if (!_LocalDrivingLicenseApplication.Save())
            {
                MessageBox.Show("Data is not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Data saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mode = enMode.Update;
                //_LoadInfo();
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                tabControl1.SelectedTab = tabControl1.TabPages["tbApplicationInfo"];
                return;
            }

            // If we say `if Mode == enMode.AddNew then enable next tab and save button` what if the user did
            // not choose a person in the control?
            if (_SelectedPersonID != -1)
            {
                tabControl1.SelectedTab = tabControl1.TabPages["tbApplicationInfo"];

            }
            else
            {
                MessageBox.Show("Please choose a person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1._FilterFocus();
            }

        }

        // This happens after the Load() event
        private void frmAddUpdateLocalDrivingLicenseApplicatio_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1._FilterFocus();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = (int)obj;

            if (Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tbApplicationInfo.Enabled = true;

            }

            // If we say `if Mode == enMode.AddNew then enable next tab and save button` what if the user did
            // not choose a person in the control?
            if (_SelectedPersonID != -1)
            {
                btnSave.Enabled = true;
                tbApplicationInfo.Enabled = true;
            }
            else
            {
                ctrlPersonCardWithFilter1._FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
