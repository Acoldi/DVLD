namespace DVLD.Application.LocalDrivingLicenseApplication
{
    partial class frmLocalApplicationCard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnColse = new System.Windows.Forms.Button();
            this.ctrlLocalApplicationCardInfo1 = new DVLD.Application.Controls.ctrlLocalApplicationCardInfo();
            this.SuspendLayout();
            // 
            // btnColse
            // 
            this.btnColse.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnColse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnColse.Image = global::DVLD.Properties.Resources.Close_Icon30;
            this.btnColse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnColse.Location = new System.Drawing.Point(751, 431);
            this.btnColse.Name = "btnColse";
            this.btnColse.Size = new System.Drawing.Size(105, 37);
            this.btnColse.TabIndex = 4;
            this.btnColse.Text = "Close";
            this.btnColse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnColse.UseVisualStyleBackColor = true;
            this.btnColse.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlLocalApplicationCardInfo1
            // 
            this.ctrlLocalApplicationCardInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlLocalApplicationCardInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ctrlLocalApplicationCardInfo1.Location = new System.Drawing.Point(13, 14);
            this.ctrlLocalApplicationCardInfo1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlLocalApplicationCardInfo1.Name = "ctrlLocalApplicationCardInfo1";
            this.ctrlLocalApplicationCardInfo1.Size = new System.Drawing.Size(849, 409);
            this.ctrlLocalApplicationCardInfo1.TabIndex = 0;
            // 
            // frmLocalApplicationCard
            // 
            this.AcceptButton = this.btnColse;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnColse;
            this.ClientSize = new System.Drawing.Size(868, 473);
            this.Controls.Add(this.btnColse);
            this.Controls.Add(this.ctrlLocalApplicationCardInfo1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmLocalApplicationCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Local Application Info";
            this.Load += new System.EventHandler(this.frmLocalApplicationCard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlLocalApplicationCardInfo ctrlLocalApplicationCardInfo1;
        private System.Windows.Forms.Button btnColse;
    }
}