namespace SE_Semester_Project
{
    partial class Outgoing
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.cboAddresses = new System.Windows.Forms.ComboBox();
            this.txtOutgoing = new System.Windows.Forms.RichTextBox();
            this.cboFileList = new System.Windows.Forms.ComboBox();
            this.btnCompose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnSend.Enabled = false;
            this.btnSend.ForeColor = System.Drawing.Color.Black;
            this.btnSend.Location = new System.Drawing.Point(221, 427);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 28);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cboAddresses
            // 
            this.cboAddresses.FormattingEnabled = true;
            this.cboAddresses.Location = new System.Drawing.Point(12, 38);
            this.cboAddresses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAddresses.Name = "cboAddresses";
            this.cboAddresses.Size = new System.Drawing.Size(150, 28);
            this.cboAddresses.TabIndex = 6;
            this.cboAddresses.Text = "To:";
            // 
            // txtOutgoing
            // 
            this.txtOutgoing.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutgoing.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutgoing.Enabled = false;
            this.txtOutgoing.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOutgoing.Location = new System.Drawing.Point(11, 72);
            this.txtOutgoing.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutgoing.Name = "txtOutgoing";
            this.txtOutgoing.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOutgoing.Size = new System.Drawing.Size(310, 350);
            this.txtOutgoing.TabIndex = 0;
            this.txtOutgoing.Text = "\"User can type directly to textbox\"";
            // 
            // cboFileList
            // 
            this.cboFileList.BackColor = System.Drawing.SystemColors.Window;
            this.cboFileList.Enabled = false;
            this.cboFileList.FormattingEnabled = true;
            this.cboFileList.Location = new System.Drawing.Point(12, 428);
            this.cboFileList.Name = "cboFileList";
            this.cboFileList.Size = new System.Drawing.Size(175, 28);
            this.cboFileList.TabIndex = 7;
            this.cboFileList.Text = "\"Select existing file\"";
            // 
            // btnCompose
            // 
            this.btnCompose.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCompose.Location = new System.Drawing.Point(291, 35);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(30, 30);
            this.btnCompose.TabIndex = 9;
            this.btnCompose.UseVisualStyleBackColor = true;
            this.btnCompose.Click += new System.EventHandler(this.btnCompose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(185, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "\"Compose->\"";
            // 
            // btnUpload
            // 
            this.btnUpload.Enabled = false;
            this.btnUpload.Location = new System.Drawing.Point(12, 462);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(175, 28);
            this.btnUpload.TabIndex = 11;
            this.btnUpload.Text = "\"Browse\"";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // Outgoing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(332, 503);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCompose);
            this.Controls.Add(this.cboFileList);
            this.Controls.Add(this.cboAddresses);
            this.Controls.Add(this.txtOutgoing);
            this.Controls.Add(this.btnSend);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Outgoing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OUTGOING";
            this.Load += new System.EventHandler(this.Outgoing_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnSend;
        private ComboBox cboAddresses;
        private RichTextBox txtOutgoing;
        private ComboBox cboFileList;
        private Button btnCompose;
        private Label label1;
        private Button btnUpload;
    }
}