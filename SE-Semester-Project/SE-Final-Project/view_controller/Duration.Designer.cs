namespace SE_Final_Project
{
    partial class Duration
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
            this.txtHH = new System.Windows.Forms.TextBox();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtMM = new System.Windows.Forms.TextBox();
            this.txtSS = new System.Windows.Forms.TextBox();
            this.lblLeftColon = new System.Windows.Forms.Label();
            this.lblRightColon = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtHH
            // 
            this.txtHH.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHH.Location = new System.Drawing.Point(40, 40);
            this.txtHH.Name = "txtHH";
            this.txtHH.Size = new System.Drawing.Size(75, 38);
            this.txtHH.TabIndex = 0;
            this.txtHH.Text = "HH";
            this.txtHH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrompt.ForeColor = System.Drawing.Color.White;
            this.lblPrompt.Location = new System.Drawing.Point(35, 9);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(324, 25);
            this.lblPrompt.TabIndex = 1;
            this.lblPrompt.Text = "Enter an end time for your message ";
            // 
            // txtMM
            // 
            this.txtMM.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMM.Location = new System.Drawing.Point(161, 40);
            this.txtMM.Name = "txtMM";
            this.txtMM.Size = new System.Drawing.Size(75, 38);
            this.txtMM.TabIndex = 2;
            this.txtMM.Text = "MM";
            this.txtMM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSS
            // 
            this.txtSS.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSS.Location = new System.Drawing.Point(282, 40);
            this.txtSS.Name = "txtSS";
            this.txtSS.Size = new System.Drawing.Size(75, 38);
            this.txtSS.TabIndex = 3;
            this.txtSS.Text = "SS";
            this.txtSS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblLeftColon
            // 
            this.lblLeftColon.AutoSize = true;
            this.lblLeftColon.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeftColon.ForeColor = System.Drawing.Color.White;
            this.lblLeftColon.Location = new System.Drawing.Point(127, 40);
            this.lblLeftColon.Name = "lblLeftColon";
            this.lblLeftColon.Size = new System.Drawing.Size(22, 32);
            this.lblLeftColon.TabIndex = 4;
            this.lblLeftColon.Text = ":";
            // 
            // lblRightColon
            // 
            this.lblRightColon.AutoSize = true;
            this.lblRightColon.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRightColon.ForeColor = System.Drawing.Color.White;
            this.lblRightColon.Location = new System.Drawing.Point(248, 40);
            this.lblRightColon.Name = "lblRightColon";
            this.lblRightColon.Size = new System.Drawing.Size(22, 32);
            this.lblRightColon.TabIndex = 5;
            this.lblRightColon.Text = ":";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(148, 103);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 38);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "ACCEPT";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // EndTimeDialog
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(382, 153);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblRightColon);
            this.Controls.Add(this.lblLeftColon);
            this.Controls.Add(this.txtSS);
            this.Controls.Add(this.txtMM);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.txtHH);
            this.Name = "EndTimeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EndTimeDialogue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHH;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtMM;
        private System.Windows.Forms.TextBox txtSS;
        private System.Windows.Forms.Label lblLeftColon;
        private System.Windows.Forms.Label lblRightColon;
        private System.Windows.Forms.Button btnAccept;
    }
}