using BusinessDVLD;
using DVLD.GloabalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = -1;
        private clsTestTypes.enTestTypes _TestTypeID = clsTestTypes.enTestTypes.VisionTest;

        private int _TestID = -1;
        private clsTest _Test { get; set; }

        public frmTakeTest(int TestAppointmentID, clsTestTypes.enTestTypes TestTypeiD)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeiD;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestType = _TestTypeID;

            ctrlScheduledTest1.LoadInfo(_TestAppointmentID);

            if (ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;


            // If test is taken, we disable btnSave and infrom the user.
            _TestID = ctrlScheduledTest1.TestID;
            if (_TestID != -1)
            {
                _Test = clsTest.Find(_TestID);
                if (_Test.TestResults)
                    rdPass.Checked = true;
                else
                    rdFail.Checked = false;

                txbNotes.Text = _Test.Notes;
                lblMessage.Visible = true;
                
                rdFail.Enabled = false;
                rdPass.Enabled = false;
                btnSave.Enabled = false;
            }
            else
                _Test = new clsTest();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to save results, you can't edit it again", "Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResults = (rdPass.Checked);
            _Test.Notes = txbNotes.Text.Trim();
            _Test.CreatedByUserID = clsGloabal.CurrentUser.ID;

            if (_Test.Save())
            {
                MessageBox.Show("Data Saved", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled=false;
            }
            else
            {
                MessageBox.Show("Data is not saved", "Fail",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
