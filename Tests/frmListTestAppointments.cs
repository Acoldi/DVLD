using BusinessDVLD;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private clsTestTypes.enTestTypes _TestType = clsTestTypes.enTestTypes.VisionTest;
        private int _LocalApplicationID = -1;
        private DataTable _dtTestAppointments;

        public clsTestTypes.enTestTypes TestType
        {
            set { 
                _TestType = value;

                switch (_TestType)
                {
                    case clsTestTypes.enTestTypes.VisionTest:
                        pbMain.Image = Resources.VistionTest188;
                        this.Text = "Vistion Test Appointments";
                        lblTitle.Text = this.Text;
                        break;

                    case clsTestTypes.enTestTypes.WrittenTest:
                        pbMain.Image = Resources.WrittenTest_188png;
                        this.Text = "Written Test Appointments";
                        lblTitle.Text = this.Text;
                        break;

                    case clsTestTypes.enTestTypes.StreetTest:
                        pbMain.Image = Resources.StreetTest188;
                        this.Text = "Street Test Appointments";
                        lblTitle.Text = this.Text;
                        break;

                }
            }
        }

        public frmListTestAppointments(int LocalApplicationID, clsTestTypes.enTestTypes TestTypeID)
        {
            InitializeComponent();

            _LocalApplicationID = LocalApplicationID;
            this.TestType = TestTypeID;
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            TestType = _TestType;
            ctrlLocalApplicationCardInfo1.LoadInfo(_LocalApplicationID);

            _dtTestAppointments = clsTestAppointment.GetTestAppointmentsPerTestType(_LocalApplicationID, (int)_TestType);
            dgvAppointments.DataSource = _dtTestAppointments;
            if (dgvAppointments.Rows.Count > 0)
            {
                dgvAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvAppointments.Columns[0].Width = 150;

                dgvAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvAppointments.Columns[1].Width = 150;

                dgvAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvAppointments.Columns[2].Width = 250;

                dgvAppointments.Columns[3].HeaderText = "Is Locked";
                dgvAppointments.Columns[3].Width = 180;

            }

            lblRecords.Text = dgvAppointments.Rows.Count.ToString();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (clsLDLA.HasActiveScheduledTest(_LocalApplicationID, _TestType))
            {
                MessageBox.Show("Person has an active test appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTakenTest = clsLDLA.GetLastAttendedTest(_LocalApplicationID, _TestType);

            if (LastTakenTest == null)
            {
                frmScheduleTest frm = new frmScheduleTest(_LocalApplicationID, (int)_TestType);
                frm.ShowDialog();
                frmListTestAppointments_Load(null, null);
                return;
            }

            if (LastTakenTest.TestResults == true)
            {
                MessageBox.Show("Already passed this test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest scheduleTest1 = new frmScheduleTest(_LocalApplicationID, (int)_TestType);
            scheduleTest1.ShowDialog();
            frmListTestAppointments_Load(null, null);
            
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_LocalApplicationID, (int)_TestType, (int)dgvAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmScheduleTest frmScheduleTest = new frmScheduleTest(_LocalApplicationID,
                        (int)_TestType, (int)dgvAppointments.CurrentRow.Cells[0].Value);
            frmScheduleTest.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)dgvAppointments.CurrentRow.Cells[0].Value, _TestType);
            frm.ShowDialog();
            frmListTestAppointments_Load(null, null);
        }
    }
}
