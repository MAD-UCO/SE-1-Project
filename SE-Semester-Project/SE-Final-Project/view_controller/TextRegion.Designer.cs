namespace SE_Final_Project.view_controller
{
    partial class TextRegion
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
            this.lblDisplayLocation = new System.Windows.Forms.Label();
            this.cboDisplayLocation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(81, 103);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 38);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "ACCEPT";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // lblDisplayLocation
            // 
            this.lblDisplayLocation.AutoSize = true;
            this.lblDisplayLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayLocation.ForeColor = System.Drawing.Color.White;
            this.lblDisplayLocation.Location = new System.Drawing.Point(38, 20);
            this.lblDisplayLocation.Name = "lblDisplayLocation";
            this.lblDisplayLocation.Size = new System.Drawing.Size(206, 25);
            this.lblDisplayLocation.TabIndex = 2;
            this.lblDisplayLocation.Text = "Enter Display Location";
            // 
            // cboDisplayLocation
            // 
            this.cboDisplayLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDisplayLocation.FormattingEnabled = true;
            this.cboDisplayLocation.Location = new System.Drawing.Point(43, 64);
            this.cboDisplayLocation.Name = "cboDisplayLocation";
            this.cboDisplayLocation.Size = new System.Drawing.Size(201, 33);
            this.cboDisplayLocation.TabIndex = 3;
            // 
            // TextRegion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(282, 153);
            this.Controls.Add(this.cboDisplayLocation);
            this.Controls.Add(this.lblDisplayLocation);
            this.Controls.Add(this.btnAccept);
            this.Name = "TextRegion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Region";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lblDisplayLocation;
        private System.Windows.Forms.ComboBox cboDisplayLocation;
    }
}