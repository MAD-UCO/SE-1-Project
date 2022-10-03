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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Messages));
            this.cboMessages = new System.Windows.Forms.ComboBox();
            this.txtMessages = new System.Windows.Forms.RichTextBox();
            this.picSound = new System.Windows.Forms.PictureBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picSound)).BeginInit();
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
            // picSound
            // 
            this.picSound.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picSound.ErrorImage")));
            this.picSound.Image = ((System.Drawing.Image)(resources.GetObject("picSound.Image")));
            this.picSound.Location = new System.Drawing.Point(14, 51);
            this.picSound.Name = "picSound";
            this.picSound.Size = new System.Drawing.Size(481, 175);
            this.picSound.TabIndex = 2;
            this.picSound.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(445, 232);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(50, 40);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 237);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(94, 29);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "\"Start\"";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(112, 238);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(94, 29);
            this.btnEnd.TabIndex = 5;
            this.btnEnd.Text = "\"End\"";
            this.btnEnd.UseVisualStyleBackColor = true;
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(507, 278);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.picSound);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.cboMessages);
            this.Name = "Messages";
            this.Text = "Messages";
            this.Load += new System.EventHandler(this.Messages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSound)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox cboMessages;
        private RichTextBox txtMessages;
        private PictureBox picSound;
        private Button btnBack;
        private Button btnStart;
        private Button btnEnd;
    }
}