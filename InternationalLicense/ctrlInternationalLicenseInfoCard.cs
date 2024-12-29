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

namespace DVLD.Licenses.InternationalLicense
{
    public partial class ctrlInternationalLicenseInfoCard : UserControl
    {
        private int _InternationalLicenseID;
        private clsInternationalLicense _InternationalLicense;
        public int licenseID
        {
            get
            {
                return _InternationalLicenseID;
            }
        }

        public clsInternationalLicense LicenseInfo
        {
            get
            {
                return _InternationalLicense;
            }
        }

        public ctrlInternationalLicenseInfoCard()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        public void LoadInfo(int InternationalLicenseID)
        {
            /// Compse DriverInfo with internationalLicense [ ]
            /// Loading by LicenseID
            //clsLicense license = clsLicense.FindLicense(LicenseID);
            //if (license != null)
            //{
            //    MessageBox.Show("Licnese with ID = " + LicenseID + " is not found", "Error",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);
            if (_InternationalLicense == null)
            {
                MessageBox.Show("No international license with ID: "+ InternationalLicenseID, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }
            _InternationalLicenseID = _InternationalLicense.ID;

            lblILAppID.Text = _InternationalLicenseID.ToString();
            lblName.Text = _InternationalLicense.LocalLicenseInfo.driver.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicense.LocalLicenseInfo.driver.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicense.LocalLicenseInfo.driver.PersonInfo.Gendor == "M" ? "Male" : "Female";
            lblLocalLicenseID.Text = _InternationalLicense.IssuedByLicenseID.ToString();
            lblILAppID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();

            lblInterLicenseID.Text = _InternationalLicenseID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicense.LocalLicenseInfo.driver.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _InternationalLicense.LocalLicenseInfo.driverID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();

            _LoadImage();

        }
        private void _LoadImage()
        {
            if (_InternationalLicense.LocalLicenseInfo.driver.PersonInfo.Gendor == "m")
            {
                pbPersonPic.Image = Resources.MaleProPhoto_173;
            }
            else
            {
                pbPersonPic.Image = Resources.FemaleProPhoto_173;
            }

            if (_InternationalLicense.LocalLicenseInfo.driver.PersonInfo.ImagePath != "")
            {
                if (File.Exists(_InternationalLicense.LocalLicenseInfo.driver.PersonInfo.ImagePath))
                    pbPersonPic.Load(_InternationalLicense.LocalLicenseInfo.driver.PersonInfo.ImagePath);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
