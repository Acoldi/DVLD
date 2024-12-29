using BusinessDVLD;
using DVLD.People;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DVLD
{
    public partial class ctrlPersonCard : UserControl
    {
        private int _PersonID = -1;

        private clsPerson _Person = null;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadInfo(int PersonID)
        {
            _Person = clsPerson.FindPerson(PersonID);
            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("Person with ID: " + PersonID + " is not found", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                _FillPersonInfo();
        }

        public void LoadInfo(string NationalNo)
        {
            _Person = clsPerson.FindPerson(NationalNo);

            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("Person with National No: " + NationalNo + " is not found", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                _FillPersonInfo();

        }

        private void _ResetPersonInfo()
        {
            _PersonID = -1; // To indicate that there is no person info
            lblID.Text = "[???]";
            lblName.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblPhone.Text = "[???]";
            lblGendor.Text = "[???]";
            lblEmail.Text = "[???]";
            lblDateofBirth.Text = "YY/MM/DD";
            lblCountry.Text = "[???]";
            lblAddress.Text = "[???]";
            pbProfilePhoto.Image = Resources.MaleProPhoto_173;
            pbGendor.Image = Resources.Male_26;
        }

        private void _LoadPersonImage()
        {
            // Default photo
            switch (_Person.Gendor)
            {

                case "M":
                    pbProfilePhoto.Image = Resources.MaleProPhoto_173;
                    break;
                default:
                    pbProfilePhoto.Image = Resources.FemaleProPhoto_173;
                    break;

            }

            // If person has a profile photo
            if (_Person.ImagePath != "")
            {

                if (File.Exists(_Person.ImagePath))
                {
                    pbProfilePhoto.ImageLocation = _Person.ImagePath; // We don't use Load method, because it locks the image file
                    return;
                }

            }

        }

        private void _FillPersonInfo()
        {
            this._PersonID = _Person.ID;
            lblID.Text = _Person.ID.ToString();
            lblName.Text = _Person.FullName;
            lblNationalNo.Text = _Person.NationalNo.ToString();
            lblGendor.Text = _Person.Gendor == "M" ? "Male" : "Female";
            pbGendor.Image = _Person.Gendor == "M" ? Resources.Male_26 : Resources.female_26;
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateofBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblPhone.Text = _Person.Phone;
            lblCountry.Text = _Person.Country.CountryName;
            _LoadPersonImage();
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_PersonID);
    
            frm.ShowDialog();

            _Refresh();
        }

        private void _Refresh()
        {
            LoadInfo(_PersonID);
        }
    }
}
