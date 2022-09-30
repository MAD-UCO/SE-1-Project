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
            this.txtMessges = new System.Windows.Forms.RichTextBox();
            this.cboMessages = new System.Windows.Forms.ComboBox();
            this.grpMediaPlayer = new System.Windows.Forms.GroupBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grpMediaPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessges
            // 
            this.txtMessges.Location = new System.Drawing.Point(16, 35);
            this.txtMessges.Name = "txtMessges";
            this.txtMessges.Size = new System.Drawing.Size(300, 350);
            this.txtMessges.TabIndex = 0;
            this.txtMessges.Text = "";
            // 
            // cboMessages
            // 
            this.cboMessages.FormattingEnabled = true;
            this.cboMessages.Location = new System.Drawing.Point(16, 1);
            this.cboMessages.Name = "cboMessages";
            this.cboMessages.Size = new System.Drawing.Size(150, 28);
            this.cboMessages.TabIndex = 1;
            this.cboMessages.Text = "SELECT MESSAGE";
            // 
            // grpMediaPlayer
            // 
            this.grpMediaPlayer.Controls.Add(this.label1);
            this.grpMediaPlayer.Location = new System.Drawing.Point(16, 400);
            this.grpMediaPlayer.Name = "grpMediaPlayer";
            this.grpMediaPlayer.Size = new System.Drawing.Size(300, 100);
            this.grpMediaPlayer.TabIndex = 5;
            this.grpMediaPlayer.TabStop = false;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(16, 541);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(50, 50);
            this.btnReturn.TabIndex = 6;
            this.btnReturn.Text = "<";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(38, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "\"Media Player with play, pause, stop\"";
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 603);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.grpMediaPlayer);
            this.Controls.Add(this.cboMessages);
            this.Controls.Add(this.txtMessges);
            this.Name = "Messages";
            this.Text = "Messages";
            this.grpMediaPlayer.ResumeLayout(false);
            this.grpMediaPlayer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox txtMessges;
        private ComboBox cboMessages;
        private GroupBox grpMediaPlayer;
        private Button btnReturn;
        private Label label1;
    }
}