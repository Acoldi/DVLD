namespace DVLD
{
    partial class ctrlPersonCardWithFilter
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grbFilter = new System.Windows.Forms.GroupBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnFInd = new System.Windows.Forms.Button();
            this.txbSearch = new System.Windows.Forms.TextBox();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ctrlPersonCard1 = new DVLD.ctrlPersonCard();
            this.grbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grbFilter
            // 
            this.grbFilter.Controls.Add(this.btnAddPerson);
            this.grbFilter.Controls.Add(this.btnFInd);
            this.grbFilter.Controls.Add(this.txbSearch);
            this.grbFilter.Controls.Add(this.cmbFilterBy);
            this.grbFilter.Controls.Add(this.label1);
            this.grbFilter.Location = new System.Drawing.Point(-3, 2);
            this.grbFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbFilter.Name = "grbFilter";
            this.grbFilter.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbFilter.Size = new System.Drawing.Size(838, 57);
            this.grbFilter.TabIndex = 94;
            this.grbFilter.TabStop = false;
            this.grbFilter.Text = "Filter";
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPerson.Image = global::DVLD.Properties.Resources.AddPerson45;
            this.btnAddPerson.Location = new System.Drawing.Point(763, 18);
            this.btnAddPerson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(49, 33);
            this.btnAddPerson.TabIndex = 95;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnFInd
            // 
            this.btnFInd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFInd.Image = global::DVLD.Properties.Resources.SearchPerson48;
            this.btnFInd.Location = new System.Drawing.Point(696, 18);
            this.btnFInd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFInd.Name = "btnFInd";
            this.btnFInd.Size = new System.Drawing.Size(49, 33);
            this.btnFInd.TabIndex = 94;
            this.btnFInd.UseVisualStyleBackColor = true;
            this.btnFInd.Click += new System.EventHandler(this.btnFInd_Click);
            // 
            // txbSearch
            // 
            this.txbSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txbSearch.Location = new System.Drawing.Point(351, 25);
            this.txbSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txbSearch.Name = "txbSearch";
            this.txbSearch.Size = new System.Drawing.Size(325, 26);
            this.txbSearch.TabIndex = 8;
            this.txbSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbSearch_KeyPress);
            this.txbSearch.Validating += new System.ComponentModel.CancelEventHandler(this.txbSearch_Validating);
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbFilterBy.BackColor = System.Drawing.SystemColors.Menu;
            this.cmbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "ID",
            "NationalNo"});
            this.cmbFilterBy.Location = new System.Drawing.Point(133, 23);
            this.cmbFilterBy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(209, 28);
            this.cmbFilterBy.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "Find By:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ctrlPersonCard1.Location = new System.Drawing.Point(5, 64);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.Size = new System.Drawing.Size(839, 300);
            this.ctrlPersonCard1.TabIndex = 95;
            // 
            // ctrlPersonCardWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.ctrlPersonCard1);
            this.Controls.Add(this.grbFilter);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ctrlPersonCardWithFilter";
            this.Size = new System.Drawing.Size(844, 369);
            this.Load += new System.EventHandler(this.ctrlPersonCardWithFilter_Load);
            this.grbFilter.ResumeLayout(false);
            this.grbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grbFilter;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnFInd;
        private System.Windows.Forms.TextBox txbSearch;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private ctrlPersonCard ctrlPersonCard1;
    }
}
