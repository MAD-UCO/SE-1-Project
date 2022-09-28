namespace SE_Semester_Project
{
    partial class frmText
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
            this.txtBoxMain = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnAudio = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnVideo = new System.Windows.Forms.Button();
            this.cboAddresses = new System.Windows.Forms.ComboBox();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxMain
            // 
            this.txtBoxMain.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtBoxMain.Location = new System.Drawing.Point(5, 2);
            this.txtBoxMain.Name = "txtBoxMain";
            this.txtBoxMain.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtBoxMain.Size = new System.Drawing.Size(275, 250);
            this.txtBoxMain.TabIndex = 0;
            this.txtBoxMain.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.LightBlue;
            this.btnSend.ForeColor = System.Drawing.Color.Black;
            this.btnSend.Location = new System.Drawing.Point(12, 58);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(250, 100);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnAudio
            // 
            this.btnAudio.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAudio.Location = new System.Drawing.Point(0, 194);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(75, 100);
            this.btnAudio.TabIndex = 4;
            this.btnAudio.Text = "<-AUDIO";
            this.btnAudio.UseVisualStyleBackColor = false;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RosyBrown;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(212, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "CLEAR";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnVideo
            // 
            this.btnVideo.BackColor = System.Drawing.Color.AliceBlue;
            this.btnVideo.Location = new System.Drawing.Point(200, 194);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(75, 100);
            this.btnVideo.TabIndex = 5;
            this.btnVideo.Text = "VIDEO->";
            this.btnVideo.UseVisualStyleBackColor = false;
            this.btnVideo.Click += new System.EventHandler(this.btnVideo_Click);
            // 
            // cboAddresses
            // 
            this.cboAddresses.FormattingEnabled = true;
            this.cboAddresses.Location = new System.Drawing.Point(12, 22);
            this.cboAddresses.Name = "cboAddresses";
            this.cboAddresses.Size = new System.Drawing.Size(139, 23);
            this.cboAddresses.TabIndex = 6;
            this.cboAddresses.Text = "To:";
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.cboAddresses);
            this.grpMain.Controls.Add(this.btnVideo);
            this.grpMain.Controls.Add(this.button1);
            this.grpMain.Controls.Add(this.btnAudio);
            this.grpMain.Controls.Add(this.btnSend);
            this.grpMain.Location = new System.Drawing.Point(5, 250);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(275, 300);
            this.grpMain.TabIndex = 6;
            this.grpMain.TabStop = false;
            // 
            // frmText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(284, 561);
            this.Controls.Add(this.grpMain);
            this.Controls.Add(this.txtBoxMain);
            this.Name = "frmText";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TEXT MESSAGE";
            this.grpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox txtBoxMain;
        private Button btnSend;
        private Button btnAudio;
        private Button button1;
        private Button btnVideo;
        private ComboBox cboAddresses;
        private GroupBox grpMain;
    }
}