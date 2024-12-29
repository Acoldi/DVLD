namespace DVLD.Licenses.LocalLicense
{
    partial class ctrlLicenseInfoWithFilter
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
            this.ctrlLicenseInfoCard1 = new DVLD.Licenses.LocalLicense.ctrlLicenseInfoCard();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.btnFInd = new System.Windows.Forms.Button();
            this.txbLicenseID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlLicenseInfoCard1
            // 
            this.ctrlLicenseInfoCard1.BackColor = System.Drawing.Color.White;
            this.ctrlLicenseInfoCard1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ctrlLicenseInfoCard1.Location = new System.Drawing.Point(4, 69);
            this.ctrlLicenseInfoCard1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlLicenseInfoCard1.Name = "ctrlLicenseInfoCard1";
            this.ctrlLicenseInfoCard1.Size = new System.Drawing.Size(794, 325);
            this.ctrlLicenseInfoCard1.TabIndex = 0;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.btnFInd);
            this.gbFilter.Controls.Add(this.txbLicenseID);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Location = new System.Drawing.Point(0, 0);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(791, 66);
            this.gbFilter.TabIndex = 1;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // btnFInd
            // 
            this.btnFInd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFInd.Image = global::DVLD.Properties.Resources.LicenseClass30;
            this.btnFInd.Location = new System.Drawing.Point(480, 25);
            this.btnFInd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFInd.Name = "btnFInd";
            this.btnFInd.Size = new System.Drawing.Size(49, 33);
            this.btnFInd.TabIndex = 95;
            this.btnFInd.UseVisualStyleBackColor = true;
            this.btnFInd.Click += new System.EventHandler(this.btnFInd_Click);
            // 
            // txbLicenseID
            // 
            this.txbLicenseID.BackColor = System.Drawing.Color.White;
            this.txbLicenseID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbLicenseID.Location = new System.Drawing.Point(180, 27);
            this.txbLicenseID.Name = "txbLicenseID";
            this.txbLicenseID.Size = new System.Drawing.Size(294, 30);
            this.txbLicenseID.TabIndex = 1;
            this.txbLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbLicenseID_KeyPress);
            this.txbLicenseID.Validating += new System.ComponentModel.CancelEventHandler(this.txbLicenseID_Validating);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "Filte";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "License ID:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlLicenseInfoWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.ctrlLicenseInfoCard1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ctrlLicenseInfoWithFilter";
            this.Size = new System.Drawing.Size(802, 397);
            this.Load += new System.EventHandler(this.ctrlLicenseInfoWithFilter_Load);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlLicenseInfoCard ctrlLicenseInfoCard1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox txbLicenseID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFInd;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
