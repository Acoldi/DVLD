using BusinessDVLD;
using DVLD.GloabalClasses;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmAddEditPerson : Form
    {
        private int _PersonID = -1;

        private clsPerson _Person;

        public delegate void DatabackEventHandler(object sender, int PersonID);

        public event DatabackEventHandler Databack;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson Person
        {
            get { return _Person; }
        }

        private enum Mode { AddNew, Update}
        private Mode _Mode;

        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();

            this._PersonID = PersonID;
            _Mode = Mode.Update;
        }

        public frmAddEditPerson()
        {
            InitializeComponent();
            _Mode = Mode.AddNew;
        }

        private void _LoadPersonInfo()
        {  // _PersonID is known

            _Person = clsPerson.FindPerson(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Person with ID: " + _PersonID + " could not be found", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblID.Text = _Person.ID.ToString();

            txbFirstName.Text = _Person.FirstName;
            txbSecondName.Text = _Person.SecondName;
            txbThirdName.Text = _Person.ThirdName;
            txbLastName.Text = _Person.LastName;
            mtxbNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            rdMaile.Checked = _Person.Gendor == "M";
            txbPhone.Text = _Person.Phone;
            mtxbEmail.Text = _Person.Email;
            cmbCountries.SelectedIndex = cmbCountries.FindString(_Person.Country.CountryName);
            txbAddress.Text = _Person.Address;

            if (_Person.ImagePath != "")
                pbProPic.ImageLocation = _Person.ImagePath;

            llbRemoveImage.Visible = _Person.ImagePath != "";
        }

        private void _ResetDefautValues()
        { // AddNew mode
            _Person = new clsPerson();

            _FillCountryCombox();

            cmbCountries.SelectedIndex = cmbCountries.FindString("Japan");

            if (_Mode == Mode.AddNew)
                lblTitle.Text = "Add New Person";
            else
                lblTitle.Text = "Update Person Info";

            // Enable/Disable removeImage button
            llbRemoveImage.Visible = (pbProPic.ImageLocation != null);

            // Default pro image
            pbProPic.Image = Resources.MaleProPhoto_173;
           

            llbRemoveImage.Visible = pbProPic.ImageLocation != null;


            // Allow age greater than 18 and less that 100
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Today.AddYears(-100);

            dtpDateOfBirth.Value = DateTime.Today.AddYears(-18);

            txbFirstName.Text = "";
            txbSecondName.Text = "";
            txbThirdName.Text = "";
            txbLastName.Text = "";
            txbAddress.Text = "";
            txbPhone.Text = "";
            mtxbEmail.Text = "";
            mtxbNationalNo.Text = "";
            rdMaile.Checked = true;

        }

        private void _FillCountryCombox()
        {
            DataTable tb = clsCountry.GetCountriesList();

            foreach (DataRow row in tb.Rows)
            {
                cmbCountries.Items.Add(row["CountryName"]);
            }
        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefautValues();

            if (_Mode == Mode.Update)
                _LoadPersonInfo();
        }

        private void llblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.png;*.jpeg;*.jpg;*.gif;*.bmb";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Can utilize exeptions here
                pbProPic.Load(openFileDialog1.FileName);
                llbRemoveImage.Visible = true;
            }
        }

        private void llbRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbProPic.ImageLocation= null;

            if (rdMaile.Checked)
                pbProPic.Image = Resources.MaleProPhoto_173;
            else
                pbProPic.Image = Resources.FemaleProPhoto_173;

            llbRemoveImage.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _HandleSavingPersonImage()
        {
            // Person image path is the same old image, if it changed, we update it
            if (pbProPic.ImageLocation != _Person.ImagePath)
            {

                // Delete older image from Image Folder
                if (_Person.ImagePath != "")
                {
                    File.Delete(_Person.ImagePath);
                }

                // If an image is selected
                if (pbProPic.ImageLocation != null)
                {
                    string ImageName = pbProPic.ImageLocation;

                    if (clsUtil.SaveImageToImagesFolder(ref ImageName))
                    {
                        // Update image location in pb
                        pbProPic.ImageLocation = ImageName;
                    }
                    else
                    {
                        MessageBox.Show("Error copying image file to folder images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Ensure proper input, follow the red dots", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandleSavingPersonImage())
                return;

            clsCountry country = clsCountry.FindCountry(cmbCountries.Text);

            _Person.FirstName = txbFirstName.Text.Trim();
            _Person.SecondName = txbSecondName.Text.Trim();
            _Person.ThirdName = txbThirdName.Text.Trim();
            _Person.LastName = txbLastName.Text.Trim();
            _Person.NationalNo = mtxbNationalNo.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            if (rdMaile.Checked) _Person.Gendor = "M"; else _Person.Gendor = "F";
            _Person.Phone = txbPhone.Text.Trim();
            _Person.Email = mtxbEmail.Text.Trim();
            _Person.NationalCountryID = cmbCountries.SelectedIndex + 1;
            _Person.Address = txbAddress.Text.Trim();

            if (pbProPic.ImageLocation != null)
                _Person.ImagePath = pbProPic.ImageLocation;

            if (_Person.Save())
            {
                

                lblID.Text = _Person.ID.ToString();
                _Mode = Mode.Update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Databack?.Invoke(this, _Person.ID);
            }
            else
            {
                MessageBox.Show("Error saving Person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            // Assume the sendor is textBox control
            TextBox tb = (TextBox)sender;

            if (string.IsNullOrEmpty(tb.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tb, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(tb, null);
            }

        }

        private void ValidateEmail(object sender, CancelEventArgs e)
        {
            if (mtxbEmail.Text.Trim() == "") // It won't be null, we initialized it to ""
            {
                // This field is optional let it be ""
                return;
            }

            if (clsValidation.ValidateEmail(mtxbEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(mtxbEmail, "Invalid Email");
            }
            else
            {
                errorProvider1.SetError(mtxbEmail, null);
            }
        }

        private void mtxbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(mtxbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(mtxbNationalNo, "This field is required");
                return;
            }
            else
            {
                errorProvider1.SetError(mtxbNationalNo, null);
            }

            if (mtxbNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExistByNationalNo(mtxbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(mtxbNationalNo, "This national number is used by another person");
            }
            else
            {
                errorProvider1.SetError(mtxbNationalNo, null);
            }
        }

        private void rdFemale_Click(object sender, EventArgs e)
        {
            if (pbProPic.ImageLocation == null)
                pbProPic.Image = Resources.FemaleProPhoto_173;
        }

        private void rdMaile_Click(object sender, EventArgs e)
        {
            if (pbProPic.ImageLocation == null)
                pbProPic.Image = Resources.MaleProPhoto_173;
        }
    }
}
