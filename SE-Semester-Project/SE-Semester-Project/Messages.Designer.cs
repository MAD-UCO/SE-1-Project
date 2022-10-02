namespace SE_Semester_Project
{
    partial class Messages
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
            this.cboMessages = new System.Windows.Forms.ComboBox();
            this.txtMessages = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // cboMessages
            // 
            this.cboMessages.FormattingEnabled = true;
            this.cboMessages.Location = new System.Drawing.Point(12, 17);
            this.cboMessages.Name = "cboMessages";
            this.cboMessages.Size = new System.Drawing.Size(175, 28);
            this.cboMessages.TabIndex = 0;
            this.cboMessages.Text = "\"Select a Message\"";
            this.cboMessages.SelectedIndexChanged += new System.EventHandler(this.cboMessages_SelectedIndexChanged);
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(12, 51);
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.Size = new System.Drawing.Size(425, 175);
            this.txtMessages.TabIndex = 1;
            this.txtMessages.Text = "";
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 278);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.cboMessages);
            this.Name = "Messages";
            this.Text = "Messages";
            this.Load += new System.EventHandler(this.Messages_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox cboMessages;
        private RichTextBox txtMessages;
    }
}