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

namespace DVLD.User
{
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID = -1;
        public int UserID
        {
            get { return _UserID; }
        }

        private clsUser _User;
        //public clsUser User
        //{
        //    get { return _User; }
        //}

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindUser(UserID);

            if (_User == null)
            {
                MessageBox.Show("User with ID " + UserID + " is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _UserID = UserID; // Just in case

            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";

            ctrlPersonCard1.LoadInfo(_User.PersonID);
        }
    }
}
