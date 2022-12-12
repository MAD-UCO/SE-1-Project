namespace SE_Final_Project
{
    partial class Splash
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
            this.lblNitro = new System.Windows.Forms.Label();
            this.lblMoveBit = new System.Windows.Forms.Label();
            this.lblLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNitro
            // 
            this.lblNitro.AutoSize = true;
            this.lblNitro.Font = new System.Drawing.Font("Impact", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNitro.ForeColor = System.Drawing.Color.Lime;
            this.lblNitro.Location = new System.Drawing.Point(560, 351);
            this.lblNitro.Name = "lblNitro";
            this.lblNitro.Size = new System.Drawing.Size(179, 75);
            this.lblNitro.TabIndex = 24;
            this.lblNitro.Text = "NITRO";
            // 
            // lblMoveBit
            // 
            this.lblMoveBit.AutoSize = true;
            this.lblMoveBit.Font = new System.Drawing.Font("Impact", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoveBit.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblMoveBit.Location = new System.Drawing.Point(65, 293);
            this.lblMoveBit.Name = "lblMoveBit";
            this.lblMoveBit.Size = new System.Drawing.Size(489, 145);
            this.lblMoveBit.TabIndex = 23;
            this.lblMoveBit.Text = "MOVEBIT";
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.ForeColor = System.Drawing.Color.White;
            this.lblLoading.Location = new System.Drawing.Point(7, 696);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(160, 21);
            this.lblLoading.TabIndex = 25;
            this.lblLoading.Text = "Loading application...";
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.ClientSize = new System.Drawing.Size(805, 730);
            this.ControlBox = false;
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.lblNitro);
            this.Controls.Add(this.lblMoveBit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNitro;
        private System.Windows.Forms.Label lblMoveBit;
        private System.Windows.Forms.Label lblLoading;
    }
}