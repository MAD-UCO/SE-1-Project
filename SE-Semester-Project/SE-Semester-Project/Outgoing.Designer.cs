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
            this.btnAudio = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.cboAddresses = new System.Windows.Forms.ComboBox();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.btnVideo = new System.Windows.Forms.Button();
            this.grpMedia = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutgoing = new System.Windows.Forms.RichTextBox();
            this.grpMain.SuspendLayout();
            this.grpMedia.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.LightBlue;
            this.btnSend.ForeColor = System.Drawing.Color.Black;
            this.btnSend.Location = new System.Drawing.Point(216, -1);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 28);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnAudio
            // 
            this.btnAudio.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAudio.Location = new System.Drawing.Point(0, 17);
            this.btnAudio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(150, 28);
            this.btnAudio.TabIndex = 4;
            this.btnAudio.Text = "RECORD AUDIO";
            this.btnAudio.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.RosyBrown;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(0, 89);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 28);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnMessages
            // 
            this.btnMessages.BackColor = System.Drawing.Color.AliceBlue;
            this.btnMessages.Location = new System.Drawing.Point(194, 17);
            this.btnMessages.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(100, 100);
            this.btnMessages.TabIndex = 5;
            this.btnMessages.Text = "MESSAGES";
            this.btnMessages.UseVisualStyleBackColor = false;
            this.btnMessages.Click += new System.EventHandler(this.btnMessages_Click);
            // 
            // cboAddresses
            // 
            this.cboAddresses.FormattingEnabled = true;
            this.cboAddresses.Location = new System.Drawing.Point(16, -1);
            this.cboAddresses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAddresses.Name = "cboAddresses";
            this.cboAddresses.Size = new System.Drawing.Size(150, 28);
            this.cboAddresses.TabIndex = 6;
            this.cboAddresses.Text = "To:";
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.btnVideo);
            this.grpMain.Controls.Add(this.btnMessages);
            this.grpMain.Controls.Add(this.btnClear);
            this.grpMain.Controls.Add(this.btnAudio);
            this.grpMain.Location = new System.Drawing.Point(16, 465);
            this.grpMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpMain.Name = "grpMain";
            this.grpMain.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpMain.Size = new System.Drawing.Size(300, 125);
            this.grpMain.TabIndex = 6;
            this.grpMain.TabStop = false;
            // 
            // btnVideo
            // 
            this.btnVideo.BackColor = System.Drawing.Color.AliceBlue;
            this.btnVideo.Location = new System.Drawing.Point(0, 53);
            this.btnVideo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(150, 28);
            this.btnVideo.TabIndex = 7;
            this.btnVideo.Text = "UPLOAD VIDEO";
            this.btnVideo.UseVisualStyleBackColor = false;
            // 
            // grpMedia
            // 
            this.grpMedia.Controls.Add(this.label1);
            this.grpMedia.Location = new System.Drawing.Point(16, 383);
            this.grpMedia.Name = "grpMedia";
            this.grpMedia.Size = new System.Drawing.Size(300, 75);
            this.grpMedia.TabIndex = 7;
            this.grpMedia.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "\"Insert Media Player\"";
            // 
            // txtOutgoing
            // 
            this.txtOutgoing.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOutgoing.Location = new System.Drawing.Point(16, 35);
            this.txtOutgoing.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutgoing.Name = "txtOutgoing";
            this.txtOutgoing.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOutgoing.Size = new System.Drawing.Size(300, 350);
            this.txtOutgoing.TabIndex = 0;
            this.txtOutgoing.Text = "";
            // 
            // Outgoing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(332, 603);
            this.Controls.Add(this.grpMedia);
            this.Controls.Add(this.cboAddresses);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.txtOutgoing);
            this.Controls.Add(this.btnSend);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Outgoing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OUTGOING";
            this.grpMain.ResumeLayout(false);
            this.grpMedia.ResumeLayout(false);
            this.grpMedia.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnSend;
        private Button btnAudio;
        private Button btnClear;
        private Button btnMessages;
        private ComboBox cboAddresses;
        private GroupBox grpMain;
        private GroupBox grpMedia;
        private Button btnVideo;
        private Label label1;
        private RichTextBox txtOutgoing;
    }
}