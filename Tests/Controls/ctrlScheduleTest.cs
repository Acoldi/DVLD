using BusinessDVLD;
using DVLD.GloabalClasses;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class ctrlScheduleTest : UserControl
    {
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        public enum enMode { AddNew, Update }
        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTime, RetakeTest }
        private enCreationMode _CreationMode = enCreationMode.FirstTime;

        private clsTestTypes.enTestTypes _TestTypeID = clsTestTypes.enTestTypes.VisionTest;
        public clsTestTypes.enTestTypes TestTypeID
        {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestTypes.enTestTypes.VisionTest:
                        gbScheduleTestInfo.Text = "Vistion Test";
                        pbMain.Image = Resources.VistionTest188;
                        break;

                    case clsTestTypes.enTestTypes.WrittenTest:
                        gbScheduleTestInfo.Text = "Written Test";
                        pbMain.Image = Resources.WrittenTest_188png;
                        break;

                    case clsTestTypes.enTestTypes.StreetTest:
                        gbScheduleTestInfo.Text = "Street Test";
                        pbMain.Image = Resources.StreetTest188;
                        break;

                }
            }
        }

        private clsLDLA _LocalApplication;
        private int _LocalApplicationID = -1;
        private clsTestAppointment _TestAppointment;
        private int _TestAppointmentID = -1;

        public void LoadInfo(int LDLAppID, int TestAppointmentID=-1)
        {
            // In case TestAppointment is not provided, we are in add new mode.
            if (TestAppointmentID == -1)
                this._Mode = enMode.AddNew;
            else
                this._Mode = enMode.Update;

            _LocalApplicationID = LDLAppID;
            _TestAppointmentID = TestAppointmentID;
            _LocalApplication = clsLDLA.FindLDLAppByID(LDLAppID);

            if (_LocalApplication == null)
            {
                MessageBox.Show($"Local Application with ID {LDLAppID} is not found",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            if (_LocalApplication.DoesAttendTestType(this._TestTypeID))
                _CreationMode = enCreationMode.RetakeTest;
            else
                _CreationMode = enCreationMode.FirstTime;


            if (_CreationMode == enCreationMode.RetakeTest)
            {
                gbRetakeTestInfo.Enabled = true;
                lblRetakeAppid.Text = "0";
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestFees.Text = clsApplicationTypes.FindApplicationType
                        ((int)clsApplication.enApplicationType.Retake_Test).ApplicationTypeFees.ToString();
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRetakeAppid.Text = "N/A";
                lblRetakeTestFees.Text = "0";
            }

            lblLocalAppID.Text = _LocalApplicationID.ToString();
            lblLicenseClass.Text = _LocalApplication.LicenseClassInfo.ClassName;
            lblName.Text = _LocalApplication.PersonFullName;
            lblTrials.Text = _LocalApplication.TotalTrialsPerTest(this.TestTypeID).ToString();



            if (_Mode == enMode.AddNew)
            {
                // Fees to pay
                lblFees.Text = clsTestTypes.FindTestType(this._TestTypeID).Fees.ToString();
                dtpDate.MinDate = DateTime.Now;
                lblRetakeAppid.Text = "N/A";

                _TestAppointment = new clsTestAppointment();
            }
            else
            {
                _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

                if (_TestAppointment == null)
                {
                    MessageBox.Show($"Test appointment with ID {_TestAppointmentID} is not found",
                        "Falure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSave.Enabled = false;
                    return;
                }

                // Paid Fees
                lblFees.Text = _TestAppointment.PaidFees.ToString();

                if (_TestAppointment.AppointmentDate < DateTime.Now)
                    dtpDate.MinDate = _TestAppointment.AppointmentDate;
                else
                    dtpDate.MinDate = DateTime.Now;

                dtpDate.MinDate = _TestAppointment.AppointmentDate;

            }

            lblTotalFees.Text = (decimal.Parse(lblFees.Text) + decimal.Parse(lblRetakeTestFees.Text)).ToString();


            lblUserMessage.Visible = false;
            btnSave.Enabled = true;
            dtpDate.Enabled = true;

            if (_IsActiveTestAppointmentExist())
            {
                lblUserMessage.Text = "Ther is already an active appointment for this person";
                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return;
            }

            if (!_PassedPreviousTest())
            {
                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return;
            }

            if (_IsAppointmentLocked())
            {
                lblUserMessage.Visible = true;
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return;
            }
            
        }

        private bool _PassedPreviousTest()
        {
            switch (TestTypeID)
            {
                case clsTestTypes.enTestTypes.VisionTest:
                    lblUserMessage.Visible = false;
                    return true;

                case clsTestTypes.enTestTypes.WrittenTest:
                    if (!_LocalApplication.PassedTestOfTestType(clsTestTypes.enTestTypes.VisionTest))
                    {
                        lblUserMessage.Text = "You have to pass vision test first";
                        return false;
                    }
                    return true;

                case clsTestTypes.enTestTypes.StreetTest:
                    if (!_LocalApplication.PassedTestOfTestType(clsTestTypes.enTestTypes.WrittenTest))
                    {
                        lblUserMessage.Text = "You have to pass Written test first";
                        lblUserMessage.Visible = true;
                        return false;
                    }
                    return true;
            }
            return false;
        }

        private bool _IsActiveTestAppointmentExist()
        {
            if (_Mode == enMode.AddNew && _LocalApplication.HasActiveScheduledTest(_TestTypeID))
            {
                return true;
            }
            return false;
        }

        private bool _IsAppointmentLocked()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Text = "Person already took this test, appointment is locked";
                return true;
            }
            return false;
        }

        private bool _AddNewRetakeApplication()
        {
            clsApplication RetakeTypeApplication = new clsApplication
            {
                ApplicationStatus = clsApplication.enStatus.Completed,
                ApplicantPersonID = _LocalApplication.ApplicantPersonID,
                ApplicationTypeID = (int)clsApplication.enApplicationType.Retake_Test,
                PaidFees = Convert.ToDecimal(lblRetakeTestFees.Text),
                LastStatusDate = DateTime.Now,
                Date = DateTime.Now,
                CreatedByUserID = clsGloabal.CurrentUser.ID
            };

            if (!RetakeTypeApplication.Save())
            {
                MessageBox.Show("Retake application not saved", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _TestAppointment.RetakeTestAppID = RetakeTypeApplication.ApplicationID;

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_CreationMode == enCreationMode.RetakeTest)
                if (!_AddNewRetakeApplication())
                    return;

            _TestAppointment.CreatedByUserID = clsGloabal.CurrentUser.ID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpDate.Value;
            _TestAppointment.PaidFees = Convert.ToDecimal(lblFees.Text);
            _TestAppointment.TestTypeID = _TestTypeID;

            if (!_TestAppointment.Save())
            {
                MessageBox.Show("Data not saved", "Falure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
