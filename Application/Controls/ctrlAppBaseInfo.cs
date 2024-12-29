using BusinessDVLD;
using DVLD.GloabalClasses;
using DVLD.People;
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


namespace DVLD.Application.Controls
{
    public partial class ctrlBaseAppInfo : UserControl
    {
        private int _ApplicationID = -1;
        public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        private clsApplication _Application { get; set; }
        public clsApplication AppInfo
        {
            get { return _Application; }
        }
        
        public ctrlBaseAppInfo()
        {
            InitializeComponent();
        }

        public void LoadInfo(int AppID)
        {
            _ApplicationID = AppID;
            _Application = clsApplication.FindApplication(AppID);
            if (_Application == null)
            {
                ResetAppInfo();
                MessageBox.Show("Application with ID: " + AppID + " is not found", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
                _LoadInfo();
        }

        public void ResetAppInfo()
        {
            lblID.Text = "[???]";
            lblApType.Text = "[???]";
            lblApplicant.Text = "[???]";
            lblPaidFees.Text = "[???]";
            lblStatus.Text = "[???]";
            lblCreatedByUser.Text = clsGloabal.CurrentUser.UserName;
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblLastStatusDate.Text = DateTime.Now.ToShortDateString();

            llbShowPersonInfo.Enabled = false;
        }

        private void _LoadInfo()
        {
            lblID.Text = _ApplicationID.ToString();
            lblApType.Text = _Application.ApplicationTypeInfo.TypeTitle;
            lblApplicant.Text = _Application.PersonInfo.FirstName;
            lblPaidFees.Text = _Application.PaidFees.ToString();
            lblStatus.Text = _Application.StatusText;
            lblCreatedByUser.Text = _Application.UserInfo.Person.FirstName;
            lblDate.Text = _Application.Date.ToShortDateString();
            lblLastStatusDate.Text = _Application.LastStatusDate.ToShortDateString();

            llbShowPersonInfo.Enabled = true;
        }



        private void _Refresh()
        {
            LoadInfo(_ApplicationID);
        }

        private void lblShowPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(_Application.PersonInfo.ID);
            frm.ShowDialog();
        }
    }
}
