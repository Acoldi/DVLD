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

namespace DVLD.TestType
{
    public partial class frmListTestTypes : Form
    {
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private DataTable _dtTestTypes;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsTestTypes.GetAllTestTypes();
            lblRecords.Text = dataGridView1.Rows.Count.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "ID";

                dataGridView1.Columns[1].HeaderText = "Title";
                dataGridView1.Columns[1].Width = 100;

                dataGridView1.Columns[2].HeaderText = "Description";
                dataGridView1.Columns[2].Width = 330;


                dataGridView1.Columns[2].HeaderText = "Fees";
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType frm = new frmUpdateTestType((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListTestTypes_Load(null, null);
        }
    }
}

