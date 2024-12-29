using BusinessDVLD;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses.LocalLicense
{
    public partial class ctrlLicenseInfoCard : UserControl
    {
        private int _LicenseID;
        private clsLicense _License;
        public int licenseID
        {
            get
            {
                return _LicenseID;
            }
        }

        public clsLicense LicenseInfo
        {
            get
            {
                return _License;
            }
        }

        public ctrlLicenseInfoCard()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.FindLicense(LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Licnese with ID = " + _LicenseID + " is not found", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblClassName.Text = _License.licenseClass.ClassName;
            lblName.Text = _License.driver.PersonInfo.FullName;
            lblNationalNo.Text = _License.driver.PersonInfo.NationalNo;
            lblGendor.Text = _License.driver.PersonInfo.Gendor == "M" ? "Male" : "Female";
            lblLiceneID.Text = _LicenseID.ToString();
            lblIssueDate.Text = _License.issueDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.notes;

            lblIsActive.Text = _License.isActive ? "Yes" : "No";
            lblDateOfBirth.Text = _License.driver.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _License.driverID.ToString();
            lblEcpirationDate.Text = _License.expirationDate.ToShortDateString();
            lblIsDetained.Text = _License.IsDetained()? "Yes" : "No";

            _LoadImage();

        }
        private void _LoadImage()
        {
            if (_License.driver.PersonInfo.Gendor == "m")
            {
                pbPersonPic.Image = Resources.MaleProPhoto_173;
            }
            else
            {
                pbPersonPic.Image = Resources.FemaleProPhoto_173;
            }

            if (_License.driver.PersonInfo.ImagePath != "")
            {
                if (File.Exists(_License.driver.PersonInfo.ImagePath))
                    pbPersonPic.Load(_License.driver.PersonInfo.ImagePath);
            }
        }
    }
}
