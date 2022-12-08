using SE_Semester_Project;

namespace SE_Final_Project
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.cboAddresses = new System.Windows.Forms.ComboBox();
            this.btnCompose = new System.Windows.Forms.Button();
            this.txtOutgoing = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnRegion = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.cboFileList = new System.Windows.Forms.ComboBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.playerMain = new AxWMPLib.AxWindowsMediaPlayer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnNewMessageIcon = new System.Windows.Forms.Button();
            this.pnlStartDuration = new System.Windows.Forms.TableLayoutPanel();
            this.txtVideoDuration = new System.Windows.Forms.TextBox();
            this.txtVideoStart = new System.Windows.Forms.TextBox();
            this.txtAudioDuration = new System.Windows.Forms.TextBox();
            this.txtAudioStart = new System.Windows.Forms.TextBox();
            this.txtTextDuration = new System.Windows.Forms.TextBox();
            this.txtTextStart = new System.Windows.Forms.TextBox();
            this.chkStartDuration = new System.Windows.Forms.CheckBox();
            this.lblTextMessage = new System.Windows.Forms.Label();
            this.lblAudioMessage = new System.Windows.Forms.Label();
            this.lblVideoMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.playerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlStartDuration.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboAddresses
            // 
            this.cboAddresses.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAddresses.FormattingEnabled = true;
            this.cboAddresses.Location = new System.Drawing.Point(15, 215);
            this.cboAddresses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboAddresses.Name = "cboAddresses";
            this.cboAddresses.Size = new System.Drawing.Size(225, 28);
            this.cboAddresses.TabIndex = 0;
            this.cboAddresses.SelectedIndexChanged += new System.EventHandler(this.CboAddresses_SelectedIndexChanged);
            this.cboAddresses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboAddresses_KeyUp);
            // 
            // btnCompose
            // 
            this.btnCompose.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompose.Location = new System.Drawing.Point(672, 285);
            this.btnCompose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(99, 52);
            this.btnCompose.TabIndex = 1;
            this.btnCompose.Text = "comp";
            this.btnCompose.UseVisualStyleBackColor = true;
            this.btnCompose.Click += new System.EventHandler(this.BtnCompose_Click);
            // 
            // txtOutgoing
            // 
            this.txtOutgoing.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutgoing.Location = new System.Drawing.Point(15, 250);
            this.txtOutgoing.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutgoing.Name = "txtOutgoing";
            this.txtOutgoing.Size = new System.Drawing.Size(649, 425);
            this.txtOutgoing.TabIndex = 2;
            this.txtOutgoing.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.LightGreen;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(671, 353);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 50);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "SND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // btnRegion
            // 
            this.btnRegion.BackColor = System.Drawing.Color.Silver;
            this.btnRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegion.Location = new System.Drawing.Point(671, 420);
            this.btnRegion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegion.Name = "btnRegion";
            this.btnRegion.Size = new System.Drawing.Size(100, 50);
            this.btnRegion.TabIndex = 4;
            this.btnRegion.Text = "REGION";
            this.btnRegion.UseVisualStyleBackColor = false;
            this.btnRegion.Click += new System.EventHandler(this.BtnRegion_Click);
            // 
            // btnMessages
            // 
            this.btnMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMessages.Location = new System.Drawing.Point(671, 624);
            this.btnMessages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(100, 50);
            this.btnMessages.TabIndex = 7;
            this.btnMessages.Text = "msgs";
            this.btnMessages.UseVisualStyleBackColor = true;
            this.btnMessages.Click += new System.EventHandler(this.BtnMessages_Click);
            // 
            // cboFileList
            // 
            this.cboFileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFileList.FormattingEnabled = true;
            this.cboFileList.Location = new System.Drawing.Point(359, 215);
            this.cboFileList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboFileList.Name = "cboFileList";
            this.cboFileList.Size = new System.Drawing.Size(175, 28);
            this.cboFileList.TabIndex = 8;
            this.cboFileList.SelectedIndexChanged += new System.EventHandler(this.CboFileList_SelectedIndexChanged);
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(540, 215);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(125, 28);
            this.btnUpload.TabIndex = 9;
            this.btnUpload.Text = "BROWSE";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(671, 215);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(99, 52);
            this.btnLogout.TabIndex = 11;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // playerMain
            // 
            this.playerMain.Enabled = true;
            this.playerMain.Location = new System.Drawing.Point(15, 247);
            this.playerMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.playerMain.MaximumSize = new System.Drawing.Size(651, 425);
            this.playerMain.Name = "playerMain";
            this.playerMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("playerMain.OcxState")));
            this.playerMain.Size = new System.Drawing.Size(488, 345);
            this.playerMain.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(166, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(452, 194);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // btnNewMessageIcon
            // 
            this.btnNewMessageIcon.BackColor = System.Drawing.Color.Red;
            this.btnNewMessageIcon.Enabled = false;
            this.btnNewMessageIcon.Location = new System.Drawing.Point(756, 609);
            this.btnNewMessageIcon.Name = "btnNewMessageIcon";
            this.btnNewMessageIcon.Size = new System.Drawing.Size(25, 25);
            this.btnNewMessageIcon.TabIndex = 13;
            this.btnNewMessageIcon.UseVisualStyleBackColor = false;
            this.btnNewMessageIcon.Visible = false;
            // 
            // pnlStartDuration
            // 
            this.pnlStartDuration.ColumnCount = 3;
            this.pnlStartDuration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.Controls.Add(this.txtTextDuration, 2, 0);
            this.pnlStartDuration.Controls.Add(this.txtAudioDuration, 2, 1);
            this.pnlStartDuration.Controls.Add(this.txtVideoDuration, 2, 2);
            this.pnlStartDuration.Controls.Add(this.txtTextStart, 1, 0);
            this.pnlStartDuration.Controls.Add(this.txtAudioStart, 1, 1);
            this.pnlStartDuration.Controls.Add(this.txtVideoStart, 1, 2);
            this.pnlStartDuration.Controls.Add(this.lblTextMessage, 0, 0);
            this.pnlStartDuration.Controls.Add(this.lblAudioMessage, 0, 1);
            this.pnlStartDuration.Controls.Add(this.lblVideoMessage, 0, 2);
            this.pnlStartDuration.Location = new System.Drawing.Point(114, 285);
            this.pnlStartDuration.Name = "pnlStartDuration";
            this.pnlStartDuration.RowCount = 3;
            this.pnlStartDuration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.Size = new System.Drawing.Size(488, 299);
            this.pnlStartDuration.TabIndex = 14;
            // 
            // txtVideoDuration
            // 
            this.txtVideoDuration.Location = new System.Drawing.Point(327, 201);
            this.txtVideoDuration.Name = "txtVideoDuration";
            this.txtVideoDuration.Size = new System.Drawing.Size(156, 22);
            this.txtVideoDuration.TabIndex = 8;
            // 
            // txtVideoStart
            // 
            this.txtVideoStart.Location = new System.Drawing.Point(165, 201);
            this.txtVideoStart.Name = "txtVideoStart";
            this.txtVideoStart.Size = new System.Drawing.Size(156, 22);
            this.txtVideoStart.TabIndex = 7;
            // 
            // txtAudioDuration
            // 
            this.txtAudioDuration.Location = new System.Drawing.Point(327, 102);
            this.txtAudioDuration.Name = "txtAudioDuration";
            this.txtAudioDuration.Size = new System.Drawing.Size(156, 22);
            this.txtAudioDuration.TabIndex = 6;
            // 
            // txtAudioStart
            // 
            this.txtAudioStart.Location = new System.Drawing.Point(165, 102);
            this.txtAudioStart.Name = "txtAudioStart";
            this.txtAudioStart.Size = new System.Drawing.Size(156, 22);
            this.txtAudioStart.TabIndex = 5;
            // 
            // txtTextDuration
            // 
            this.txtTextDuration.Location = new System.Drawing.Point(327, 3);
            this.txtTextDuration.Name = "txtTextDuration";
            this.txtTextDuration.Size = new System.Drawing.Size(156, 22);
            this.txtTextDuration.TabIndex = 4;
            // 
            // txtTextStart
            // 
            this.txtTextStart.Location = new System.Drawing.Point(165, 3);
            this.txtTextStart.Name = "txtTextStart";
            this.txtTextStart.Size = new System.Drawing.Size(156, 22);
            this.txtTextStart.TabIndex = 3;
            // 
            // chkStartDuration
            // 
            this.chkStartDuration.AutoSize = true;
            this.chkStartDuration.ForeColor = System.Drawing.Color.White;
            this.chkStartDuration.Location = new System.Drawing.Point(672, 488);
            this.chkStartDuration.Name = "chkStartDuration";
            this.chkStartDuration.Size = new System.Drawing.Size(18, 17);
            this.chkStartDuration.TabIndex = 15;
            this.chkStartDuration.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkStartDuration.UseVisualStyleBackColor = true;
            this.chkStartDuration.CheckedChanged += new System.EventHandler(this.chkStartDuration_CheckedChanged);
            // 
            // lblTextMessage
            // 
            this.lblTextMessage.AutoSize = true;
            this.lblTextMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage.ForeColor = System.Drawing.Color.White;
            this.lblTextMessage.Location = new System.Drawing.Point(3, 0);
            this.lblTextMessage.Name = "lblTextMessage";
            this.lblTextMessage.Size = new System.Drawing.Size(143, 25);
            this.lblTextMessage.TabIndex = 9;
            this.lblTextMessage.Text = "Text Message:";
            // 
            // lblAudioMessage
            // 
            this.lblAudioMessage.AutoSize = true;
            this.lblAudioMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudioMessage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblAudioMessage.Location = new System.Drawing.Point(3, 99);
            this.lblAudioMessage.Name = "lblAudioMessage";
            this.lblAudioMessage.Size = new System.Drawing.Size(155, 25);
            this.lblAudioMessage.TabIndex = 10;
            this.lblAudioMessage.Text = "Audio Message:";
            // 
            // lblVideoMessage
            // 
            this.lblVideoMessage.AutoSize = true;
            this.lblVideoMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVideoMessage.ForeColor = System.Drawing.Color.White;
            this.lblVideoMessage.Location = new System.Drawing.Point(3, 198);
            this.lblVideoMessage.Name = "lblVideoMessage";
            this.lblVideoMessage.Size = new System.Drawing.Size(155, 25);
            this.lblVideoMessage.TabIndex = 11;
            this.lblVideoMessage.Text = "Video Message:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(787, 683);
            this.Controls.Add(this.chkStartDuration);
            this.Controls.Add(this.pnlStartDuration);
            this.Controls.Add(this.btnNewMessageIcon);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.playerMain);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.cboFileList);
            this.Controls.Add(this.btnMessages);
            this.Controls.Add(this.btnRegion);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtOutgoing);
            this.Controls.Add(this.btnCompose);
            this.Controls.Add(this.cboAddresses);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MoveBit - Composer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playerMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlStartDuration.ResumeLayout(false);
            this.pnlStartDuration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboAddresses;
        private System.Windows.Forms.Button btnCompose;
        private System.Windows.Forms.RichTextBox txtOutgoing;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnRegion;
        private System.Windows.Forms.Button btnMessages;
        private System.Windows.Forms.ComboBox cboFileList;
        private System.Windows.Forms.Button btnUpload;
        private AxWMPLib.AxWindowsMediaPlayer playerMain;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnNewMessageIcon;
        private System.Windows.Forms.TableLayoutPanel pnlStartDuration;
        private System.Windows.Forms.TextBox txtVideoDuration;
        private System.Windows.Forms.TextBox txtVideoStart;
        private System.Windows.Forms.TextBox txtAudioDuration;
        private System.Windows.Forms.TextBox txtAudioStart;
        private System.Windows.Forms.TextBox txtTextDuration;
        private System.Windows.Forms.TextBox txtTextStart;
        private System.Windows.Forms.CheckBox chkStartDuration;
        private System.Windows.Forms.Label lblTextMessage;
        private System.Windows.Forms.Label lblAudioMessage;
        private System.Windows.Forms.Label lblVideoMessage;
    }
}

