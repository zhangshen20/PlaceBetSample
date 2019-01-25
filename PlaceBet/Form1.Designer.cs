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
            this.textBoxAccountNumber.Location = new System.Drawing.Point(168, 52);
            this.textBoxAccountNumber.Name = "textBoxAccountNumber";
            this.textBoxAccountNumber.Size = new System.Drawing.Size(141, 26);
            this.textBoxAccountNumber.TabIndex = 0;
            this.textBoxAccountNumber.Text = "2124435";
            // 
            // labelAccountNumber
            // 
            this.labelAccountNumber.AutoSize = true;
            this.labelAccountNumber.Location = new System.Drawing.Point(32, 57);
            this.labelAccountNumber.Name = "labelAccountNumber";
            this.labelAccountNumber.Size = new System.Drawing.Size(128, 20);
            this.labelAccountNumber.TabIndex = 1;
            this.labelAccountNumber.Text = "Account Number";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(34, 99);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(78, 20);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Password";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(168, 95);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = 'x';
            this.textBoxPassword.Size = new System.Drawing.Size(141, 26);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.Text = "Likey00001";
            // 
            // labelPropositionId
            // 
            this.labelPropositionId.AutoSize = true;
            this.labelPropositionId.Location = new System.Drawing.Point(34, 138);
            this.labelPropositionId.Name = "labelPropositionId";
            this.labelPropositionId.Size = new System.Drawing.Size(106, 20);
            this.labelPropositionId.TabIndex = 4;
            this.labelPropositionId.Text = "Proposition Id";
            // 
            // textBoxPropositionId
            // 
            this.textBoxPropositionId.Location = new System.Drawing.Point(168, 138);
            this.textBoxPropositionId.Name = "textBoxPropositionId";
            this.textBoxPropositionId.Size = new System.Drawing.Size(141, 26);
            this.textBoxPropositionId.TabIndex = 5;
            // 
            // buttonPlaceBet
            // 
            this.buttonPlaceBet.Location = new System.Drawing.Point(341, 138);
            this.buttonPlaceBet.Name = "buttonPlaceBet";
            this.buttonPlaceBet.Size = new System.Drawing.Size(111, 72);
            this.buttonPlaceBet.TabIndex = 6;
            this.buttonPlaceBet.Text = "Place Bet";
            this.buttonPlaceBet.UseVisualStyleBackColor = true;
            // 
            // labelOdds
            // 
            this.labelOdds.AutoSize = true;
            this.labelOdds.Location = new System.Drawing.Point(32, 184);
            this.labelOdds.Name = "labelOdds";
            this.labelOdds.Size = new System.Drawing.Size(47, 20);
            this.labelOdds.TabIndex = 7;
            this.labelOdds.Text = "Odds";
            // 
            // textBoxOdds
            // 
            this.textBoxOdds.Location = new System.Drawing.Point(168, 184);
            this.textBoxOdds.Name = "textBoxOdds";
            this.textBoxOdds.Size = new System.Drawing.Size(141, 26);
            this.textBoxOdds.TabIndex = 8;
            // 
            // buttonAuthenticate
            // 
            this.buttonAuthenticate.Location = new System.Drawing.Point(341, 52);
            this.buttonAuthenticate.Name = "buttonAuthenticate";
            this.buttonAuthenticate.Size = new System.Drawing.Size(111, 67);
            this.buttonAuthenticate.TabIndex = 9;
            this.buttonAuthenticate.Text = "Authenticate";
            this.buttonAuthenticate.UseVisualStyleBackColor = true;
            this.buttonAuthenticate.Click += new System.EventHandler(this.buttonAuthenticate_Click);
            // 
            // textWebResponse
            // 
            this.textWebResponse.Location = new System.Drawing.Point(38, 262);
            this.textWebResponse.Name = "textWebResponse";
            this.textWebResponse.Size = new System.Drawing.Size(414, 26);
            this.textWebResponse.TabIndex = 10;
            this.textWebResponse.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 731);
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

