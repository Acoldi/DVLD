namespace DVLD
{
    partial class frmLogIn
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
            this.txbPassword = new System.Windows.Forms.MaskedTextBox();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxRememberMe = new System.Windows.Forms.CheckBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogIN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbPassword
            // 
            this.txbPassword.BackColor = System.Drawing.Color.White;
            this.txbPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txbPassword.Location = new System.Drawing.Point(612, 201);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(147, 30);
            this.txbPassword.TabIndex = 106;
            // 
            // txbUserName
            // 
            this.txbUserName.BackColor = System.Drawing.Color.White;
            this.txbUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txbUserName.Location = new System.Drawing.Point(612, 133);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(147, 30);
            this.txbUserName.TabIndex = 105;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(448, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 31);
            this.label7.TabIndex = 104;
            this.label7.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(448, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 29);
            this.label1.TabIndex = 103;
            this.label1.Text = "User Name";
            // 
            // cbxRememberMe
            // 
            this.cbxRememberMe.AutoSize = true;
            this.cbxRememberMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbxRememberMe.Location = new System.Drawing.Point(612, 249);
            this.cbxRememberMe.Name = "cbxRememberMe";
            this.cbxRememberMe.Size = new System.Drawing.Size(162, 29);
            this.cbxRememberMe.TabIndex = 107;
            this.cbxRememberMe.Text = "Remember Me";
            this.cbxRememberMe.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Black;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(442, 566);
            this.splitter1.TabIndex = 109;
            this.splitter1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnClose.Image = global::DVLD.Properties.Resources.blackCloase45;
            this.btnClose.Location = new System.Drawing.Point(862, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(53, 43);
            this.btnClose.TabIndex = 110;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnLogIN
            // 
            this.btnLogIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.btnLogIN.Image = global::DVLD.Properties.Resources.Login50;
            this.btnLogIN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogIN.Location = new System.Drawing.Point(612, 336);
            this.btnLogIN.Name = "btnLogIN";
            this.btnLogIN.Size = new System.Drawing.Size(147, 54);
            this.btnLogIN.TabIndex = 108;
            this.btnLogIN.Text = "Log In";
            this.btnLogIN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogIN.UseVisualStyleBackColor = true;
            this.btnLogIN.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // frmLogIn
            // 
            this.AcceptButton = this.btnLogIN;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(928, 566);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.btnLogIN);
            this.Controls.Add(this.cbxRememberMe);
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.txbUserName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLogIn";
            this.Load += new System.EventHandler(this.frmLogIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txbPassword;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbxRememberMe;
        private System.Windows.Forms.Button btnLogIN;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnClose;
    }
}