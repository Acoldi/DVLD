using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmFindPerson : Form
    {
        public delegate void DatabackEventHandler(object sender, int personID);

        public event DatabackEventHandler Databack;

        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Databack?.Invoke(this, ctrlPersonCardWithFilter1.PersonID);
        }
    }
}
