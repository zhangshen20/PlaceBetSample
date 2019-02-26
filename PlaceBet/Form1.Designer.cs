namespace PlaceBet
{
    partial class Form1
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
            this.textBoxAccountNumber = new System.Windows.Forms.TextBox();
            this.labelAccountNumber = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPropositionId = new System.Windows.Forms.Label();
            this.textBoxPropositionId = new System.Windows.Forms.TextBox();
            this.buttonPlaceBet = new System.Windows.Forms.Button();
            this.labelOdds = new System.Windows.Forms.Label();
            this.textBoxOdds = new System.Windows.Forms.TextBox();
            this.buttonAuthenticate = new System.Windows.Forms.Button();
            this.textWebResponse = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxAccountNumber
            // 
            this.textBoxAccountNumber.Location = new System.Drawing.Point(112, 34);
            this.textBoxAccountNumber.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxAccountNumber.Name = "textBoxAccountNumber";
            this.textBoxAccountNumber.Size = new System.Drawing.Size(95, 20);
            this.textBoxAccountNumber.TabIndex = 0;
            this.textBoxAccountNumber.Text = "jackyzhang1981";
            // 
            // labelAccountNumber
            // 
            this.labelAccountNumber.AutoSize = true;
            this.labelAccountNumber.Location = new System.Drawing.Point(21, 37);
            this.labelAccountNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAccountNumber.Name = "labelAccountNumber";
            this.labelAccountNumber.Size = new System.Drawing.Size(87, 13);
            this.labelAccountNumber.TabIndex = 1;
            this.labelAccountNumber.Text = "Account Number";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(23, 64);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(53, 13);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(112, 62);
            this.textBoxPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = 'x';
            this.textBoxPassword.Size = new System.Drawing.Size(95, 20);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.Text = "Shenzhang123";
            // 
            // labelPropositionId
            // 
            this.labelPropositionId.AutoSize = true;
            this.labelPropositionId.Location = new System.Drawing.Point(23, 90);
            this.labelPropositionId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPropositionId.Name = "labelPropositionId";
            this.labelPropositionId.Size = new System.Drawing.Size(50, 13);
            this.labelPropositionId.TabIndex = 4;
            this.labelPropositionId.Text = "Outcome";
            // 
            // textBoxPropositionId
            // 
            this.textBoxPropositionId.Location = new System.Drawing.Point(112, 90);
            this.textBoxPropositionId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPropositionId.Name = "textBoxPropositionId";
            this.textBoxPropositionId.Size = new System.Drawing.Size(95, 20);
            this.textBoxPropositionId.TabIndex = 5;
            // 
            // buttonPlaceBet
            // 
            this.buttonPlaceBet.Location = new System.Drawing.Point(227, 90);
            this.buttonPlaceBet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonPlaceBet.Name = "buttonPlaceBet";
            this.buttonPlaceBet.Size = new System.Drawing.Size(88, 47);
            this.buttonPlaceBet.TabIndex = 6;
            this.buttonPlaceBet.Text = "Place Bet";
            this.buttonPlaceBet.UseVisualStyleBackColor = true;
            this.buttonPlaceBet.Click += new System.EventHandler(this.buttonPlaceBet_Click);
            // 
            // labelOdds
            // 
            this.labelOdds.AutoSize = true;
            this.labelOdds.Location = new System.Drawing.Point(21, 120);
            this.labelOdds.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelOdds.Name = "labelOdds";
            this.labelOdds.Size = new System.Drawing.Size(32, 13);
            this.labelOdds.TabIndex = 7;
            this.labelOdds.Text = "Odds";
            // 
            // textBoxOdds
            // 
            this.textBoxOdds.Location = new System.Drawing.Point(112, 120);
            this.textBoxOdds.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxOdds.Name = "textBoxOdds";
            this.textBoxOdds.Size = new System.Drawing.Size(95, 20);
            this.textBoxOdds.TabIndex = 8;
            // 
            // buttonAuthenticate
            // 
            this.buttonAuthenticate.Location = new System.Drawing.Point(227, 34);
            this.buttonAuthenticate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonAuthenticate.Name = "buttonAuthenticate";
            this.buttonAuthenticate.Size = new System.Drawing.Size(88, 44);
            this.buttonAuthenticate.TabIndex = 9;
            this.buttonAuthenticate.Text = "Authenticate";
            this.buttonAuthenticate.UseVisualStyleBackColor = true;
            this.buttonAuthenticate.Click += new System.EventHandler(this.buttonAuthenticate_Click);
            // 
            // textWebResponse
            // 
            this.textWebResponse.Location = new System.Drawing.Point(25, 170);
            this.textWebResponse.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textWebResponse.Name = "textWebResponse";
            this.textWebResponse.Size = new System.Drawing.Size(290, 20);
            this.textWebResponse.TabIndex = 10;
            this.textWebResponse.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 475);
            this.Controls.Add(this.textWebResponse);
            this.Controls.Add(this.buttonAuthenticate);
            this.Controls.Add(this.textBoxOdds);
            this.Controls.Add(this.labelOdds);
            this.Controls.Add(this.buttonPlaceBet);
            this.Controls.Add(this.textBoxPropositionId);
            this.Controls.Add(this.labelPropositionId);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelAccountNumber);
            this.Controls.Add(this.textBoxAccountNumber);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAccountNumber;
        private System.Windows.Forms.Label labelAccountNumber;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPropositionId;
        private System.Windows.Forms.TextBox textBoxPropositionId;
        private System.Windows.Forms.Button buttonPlaceBet;
        private System.Windows.Forms.Label labelOdds;
        private System.Windows.Forms.TextBox textBoxOdds;
        private System.Windows.Forms.Button buttonAuthenticate;
        private System.Windows.Forms.TextBox textWebResponse;
    }
}

