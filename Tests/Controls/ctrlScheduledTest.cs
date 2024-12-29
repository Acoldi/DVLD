using BusinessDVLD;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD.Tests.ctrlScheduleTest;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        private clsTestTypes.enTestTypes _TestTypeID = clsTestTypes.enTestTypes.VisionTest;
        public clsTestTypes.enTestTypes TestType
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

        private int _TestID = -1;
        public int TestID
        {
            get
            {
                return _TestID;
            }
        }

        private int _LocalAppID = -1;
        public clsLDLA LocalApplication;
        private clsTestAppointment _TestAppointment;
        public int TestAppointmentID = -1;

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int TestAppointmentID)
        {
            this.TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);
            
            if (_TestAppointment == null)
            {
                MessageBox.Show($"Test appointmetn with ID {this.TestAppointmentID} is not found",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.TestAppointmentID = -1;
                return;
            }

            _TestID = _TestAppointment.TestID;

            _LocalAppID = _TestAppointment.LocalDrivingLicenseApplicationID;
            LocalApplication = clsLDLA.FindLDLAppByID(_LocalAppID);

            if (LocalApplication == null)
            {
                MessageBox.Show($"Local Application with ID {_LocalAppID} is not found",
                    "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = LocalApplication.LicenseClassInfo.ClassName;
            lblName.Text = LocalApplication.PersonFullName;
            lblTrials.Text = LocalApplication.TotalTrialsPerTest(this.TestType).ToString();
            lblAppointmentDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            lblTestID.Text = (_TestAppointment.TestID != -1)? _TestAppointment.TestID.ToString(): "Not Taken Yet";
        }

    }
}
