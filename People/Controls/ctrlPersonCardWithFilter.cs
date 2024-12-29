using BusinessDVLD;
using DVLD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        virtual protected void OnDataBack(int PersonID)
        {
            OnPersonSelected?.Invoke(PersonID);
        }

        private bool _ShowAddPersonButton = true;
        public bool ShowAddPersonButton
        { 
            set 
            {
                _ShowAddPersonButton = value;
                btnAddPerson.Visible = _ShowAddPersonButton;
            }
            get
            {
                return _ShowAddPersonButton;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            set
            {
                _FilterEnabled = value;
                grbFilter.Enabled = _FilterEnabled;
            }
            get
            {
                return _FilterEnabled;
            }
        }

        private int _personID = -1;
        public int PersonID
        { 
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            cmbFilterBy.SelectedIndex = 0;
            txbSearch.Text = PersonID.ToString();
            _FindNow();
        }

        private void _FindNow()
        {
            if (cmbFilterBy.SelectedIndex == 0)
            {
                ctrlPersonCard1.LoadInfo(int.Parse(txbSearch.Text));
            }
            else
            {
                ctrlPersonCard1.LoadInfo(txbSearch.Text);
            }

            // Raise the event
            if (grbFilter.Enabled) // Conditions to raise/fire the event
                OnPersonSelected?.Invoke(ctrlPersonCard1.PersonID);
        }

        private void btnFInd_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Ensure valid information, trace the red mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FindNow();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cmbFilterBy.SelectedIndex = 1;
            txbSearch.Focus();
        }

        private void txbSearch_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbSearch.Text))
            {
                errorProvider1.SetError(txbSearch, "This field can't be empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbSearch, null);
            }

        }
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson();
            frm.Databack += OnDatabackEvent;
        }

        protected virtual void OnDatabackEvent(Object sender, int PersonID)
        {
            cmbFilterBy.SelectedIndex = 0;
            txbSearch.Text = PersonID.ToString();
            LoadPersonInfo(PersonID);
        }

        // Like properties, we don't expose controls directly
        public void _FilterFocus()
        {
            txbSearch.Focus();
        }

        private void txbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If filter by PersonID, allow only digits
            if (cmbFilterBy.SelectedIndex == 0)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
                btnFInd.PerformClick();
        }
    }
}
