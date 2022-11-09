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
            this.btnAccept = new System.Windows.Forms.Button();
            this.lblPrompt = new System.Windows.Forms.Label();
            this.txtSS = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(91, 104);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 38);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "ACCEPT";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrompt.ForeColor = System.Drawing.Color.White;
            this.lblPrompt.Location = new System.Drawing.Point(12, 29);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new System.Drawing.Size(254, 25);
            this.lblPrompt.TabIndex = 1;
            this.lblPrompt.Text = "Enter a duration in seconds:";
            // 
            // txtSS
            // 
            this.txtSS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSS.Location = new System.Drawing.Point(19, 57);
            this.txtSS.Name = "txtSS";
            this.txtSS.Size = new System.Drawing.Size(238, 30);
            this.txtSS.TabIndex = 7;
            // 
            // Duration
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(282, 153);
            this.Controls.Add(this.txtSS);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblPrompt);
            this.Name = "Duration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EndTimeDialogue";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Duration_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblPrompt;
        private System.Windows.Forms.TextBox txtSS;
    }
}