using DVLD.Application.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application.LocalDrivingLicenseApplication
{
    public partial class frmLocalApplicationCard : Form
    {
        private int _LocalAppID = -1;
        public int LocalAppID
        {
            get { return LocalAppID; }
        }

        public frmLocalApplicationCard(int LocalAppID)
        {
            InitializeComponent();
            _LocalAppID = LocalAppID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocalApplicationCard_Load(object sender, EventArgs e)
        {
            ctrlLocalApplicationCardInfo1.LoadInfo(_LocalAppID);
        }
    }
}
