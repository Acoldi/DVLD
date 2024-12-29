using BusinessDVLD;
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
    public partial class frmScheduleTest : Form
    {
        private int _LocalAppID = -1;
        private int _TestAppointmentID = -1;
        private int _TestTypeID = -1;

        public frmScheduleTest(int LocalAppID, int TestTypeID, int TestAppointmentID = -1)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = (clsTestTypes.enTestTypes) _TestTypeID;
            ctrlScheduleTest1.LoadInfo(_LocalAppID, _TestAppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
