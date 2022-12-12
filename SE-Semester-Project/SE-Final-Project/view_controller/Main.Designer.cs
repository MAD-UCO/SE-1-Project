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
            this.btnRegion = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.cboFileList = new System.Windows.Forms.ComboBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnNewMessageIcon = new System.Windows.Forms.Button();
            this.pnlStartDuration = new System.Windows.Forms.TableLayoutPanel();
            this.txtTextDuration = new System.Windows.Forms.TextBox();
            this.txtAudioDuration = new System.Windows.Forms.TextBox();
            this.txtVideoDuration = new System.Windows.Forms.TextBox();
            this.txtTextStart = new System.Windows.Forms.TextBox();
            this.txtAudioStart = new System.Windows.Forms.TextBox();
            this.txtVideoStart = new System.Windows.Forms.TextBox();
            this.lblAudioMessage = new System.Windows.Forms.Label();
            this.lblVideoMessage = new System.Windows.Forms.Label();
            this.lblTextMessage = new System.Windows.Forms.Label();
            this.btnDuration = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.pnlRegion = new System.Windows.Forms.TableLayoutPanel();
            this.cboRegion = new System.Windows.Forms.ComboBox();
            this.playerMain = new AxWMPLib.AxWindowsMediaPlayer();
            this.lblMoveBit = new System.Windows.Forms.Label();
            this.lblNitro = new System.Windows.Forms.Label();
            this.pnlStartDuration.SuspendLayout();
            this.pnlRegion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerMain)).BeginInit();
            this.SuspendLayout();
            // 
            // cboAddresses
            // 
            this.cboAddresses.BackColor = System.Drawing.Color.DarkBlue;
            this.cboAddresses.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAddresses.ForeColor = System.Drawing.Color.White;
            this.cboAddresses.FormattingEnabled = true;
            this.cboAddresses.Location = new System.Drawing.Point(15, 215);
            this.cboAddresses.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboAddresses.Name = "cboAddresses";
            this.cboAddresses.Size = new System.Drawing.Size(225, 29);
            this.cboAddresses.TabIndex = 0;
            this.cboAddresses.SelectedIndexChanged += new System.EventHandler(this.CboAddresses_SelectedIndexChanged);
            this.cboAddresses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CboAddresses_KeyUp);
            // 
            // btnCompose
            // 
            this.btnCompose.BackColor = System.Drawing.Color.DarkBlue;
            this.btnCompose.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompose.ForeColor = System.Drawing.Color.White;
            this.btnCompose.Location = new System.Drawing.Point(15, 171);
            this.btnCompose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(110, 40);
            this.btnCompose.TabIndex = 1;
            this.btnCompose.Text = "COMPOSE";
            this.btnCompose.UseVisualStyleBackColor = false;
            this.btnCompose.Click += new System.EventHandler(this.BtnCompose_Click);
            // 
            // txtOutgoing
            // 
            this.txtOutgoing.BackColor = System.Drawing.Color.DarkBlue;
            this.txtOutgoing.Enabled = false;
            this.txtOutgoing.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutgoing.ForeColor = System.Drawing.Color.White;
            this.txtOutgoing.Location = new System.Drawing.Point(15, 250);
            this.txtOutgoing.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutgoing.Name = "txtOutgoing";
            this.txtOutgoing.Size = new System.Drawing.Size(649, 425);
            this.txtOutgoing.TabIndex = 2;
            this.txtOutgoing.Text = "";
            // 
            // btnRegion
            // 
            this.btnRegion.BackColor = System.Drawing.Color.DarkBlue;
            this.btnRegion.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegion.Location = new System.Drawing.Point(670, 308);
            this.btnRegion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegion.Name = "btnRegion";
            this.btnRegion.Size = new System.Drawing.Size(110, 40);
            this.btnRegion.TabIndex = 4;
            this.btnRegion.Text = "REGION";
            this.btnRegion.UseVisualStyleBackColor = false;
            this.btnRegion.Click += new System.EventHandler(this.BtnRegion_Click);
            // 
            // btnMessages
            // 
            this.btnMessages.BackColor = System.Drawing.Color.DarkBlue;
            this.btnMessages.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMessages.Location = new System.Drawing.Point(671, 632);
            this.btnMessages.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(110, 40);
            this.btnMessages.TabIndex = 7;
            this.btnMessages.Text = "MESSAGES";
            this.btnMessages.UseVisualStyleBackColor = false;
            this.btnMessages.Click += new System.EventHandler(this.BtnMessages_Click);
            // 
            // cboFileList
            // 
            this.cboFileList.BackColor = System.Drawing.Color.DarkBlue;
            this.cboFileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFileList.ForeColor = System.Drawing.Color.White;
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
            this.btnUpload.BackColor = System.Drawing.Color.DarkBlue;
            this.btnUpload.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.ForeColor = System.Drawing.Color.White;
            this.btnUpload.Location = new System.Drawing.Point(540, 210);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(125, 35);
            this.btnUpload.TabIndex = 9;
            this.btnUpload.Text = "BROWSE";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.DarkBlue;
            this.btnLogout.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(671, 13);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(110, 40);
            this.btnLogout.TabIndex = 11;
            this.btnLogout.Text = "LOGOUT";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // btnNewMessageIcon
            // 
            this.btnNewMessageIcon.BackColor = System.Drawing.Color.OrangeRed;
            this.btnNewMessageIcon.Enabled = false;
            this.btnNewMessageIcon.Location = new System.Drawing.Point(756, 617);
            this.btnNewMessageIcon.Name = "btnNewMessageIcon";
            this.btnNewMessageIcon.Size = new System.Drawing.Size(25, 25);
            this.btnNewMessageIcon.TabIndex = 13;
            this.btnNewMessageIcon.UseVisualStyleBackColor = false;
            this.btnNewMessageIcon.Visible = false;
            // 
            // pnlStartDuration
            // 
            this.pnlStartDuration.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
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
            this.pnlStartDuration.Controls.Add(this.lblAudioMessage, 0, 1);
            this.pnlStartDuration.Controls.Add(this.lblVideoMessage, 0, 2);
            this.pnlStartDuration.Controls.Add(this.lblTextMessage, 0, 0);
            this.pnlStartDuration.Location = new System.Drawing.Point(361, 445);
            this.pnlStartDuration.Name = "pnlStartDuration";
            this.pnlStartDuration.RowCount = 3;
            this.pnlStartDuration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.pnlStartDuration.Size = new System.Drawing.Size(420, 168);
            this.pnlStartDuration.TabIndex = 14;
            // 
            // txtTextDuration
            // 
            this.txtTextDuration.BackColor = System.Drawing.Color.DarkBlue;
            this.txtTextDuration.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextDuration.ForeColor = System.Drawing.Color.White;
            this.txtTextDuration.Location = new System.Drawing.Point(282, 4);
            this.txtTextDuration.Name = "txtTextDuration";
            this.txtTextDuration.Size = new System.Drawing.Size(134, 28);
            this.txtTextDuration.TabIndex = 4;
            // 
            // txtAudioDuration
            // 
            this.txtAudioDuration.BackColor = System.Drawing.Color.DarkBlue;
            this.txtAudioDuration.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAudioDuration.ForeColor = System.Drawing.Color.White;
            this.txtAudioDuration.Location = new System.Drawing.Point(282, 59);
            this.txtAudioDuration.Name = "txtAudioDuration";
            this.txtAudioDuration.Size = new System.Drawing.Size(134, 28);
            this.txtAudioDuration.TabIndex = 6;
            // 
            // txtVideoDuration
            // 
            this.txtVideoDuration.BackColor = System.Drawing.Color.DarkBlue;
            this.txtVideoDuration.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVideoDuration.ForeColor = System.Drawing.Color.White;
            this.txtVideoDuration.Location = new System.Drawing.Point(282, 114);
            this.txtVideoDuration.Name = "txtVideoDuration";
            this.txtVideoDuration.Size = new System.Drawing.Size(134, 28);
            this.txtVideoDuration.TabIndex = 8;
            // 
            // txtTextStart
            // 
            this.txtTextStart.BackColor = System.Drawing.Color.DarkBlue;
            this.txtTextStart.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextStart.ForeColor = System.Drawing.Color.White;
            this.txtTextStart.Location = new System.Drawing.Point(143, 4);
            this.txtTextStart.Name = "txtTextStart";
            this.txtTextStart.Size = new System.Drawing.Size(132, 28);
            this.txtTextStart.TabIndex = 3;
            // 
            // txtAudioStart
            // 
            this.txtAudioStart.BackColor = System.Drawing.Color.DarkBlue;
            this.txtAudioStart.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAudioStart.ForeColor = System.Drawing.Color.White;
            this.txtAudioStart.Location = new System.Drawing.Point(143, 59);
            this.txtAudioStart.Name = "txtAudioStart";
            this.txtAudioStart.Size = new System.Drawing.Size(132, 28);
            this.txtAudioStart.TabIndex = 5;
            // 
            // txtVideoStart
            // 
            this.txtVideoStart.BackColor = System.Drawing.Color.DarkBlue;
            this.txtVideoStart.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVideoStart.ForeColor = System.Drawing.Color.White;
            this.txtVideoStart.Location = new System.Drawing.Point(143, 114);
            this.txtVideoStart.Name = "txtVideoStart";
            this.txtVideoStart.Size = new System.Drawing.Size(132, 28);
            this.txtVideoStart.TabIndex = 7;
            // 
            // lblAudioMessage
            // 
            this.lblAudioMessage.AutoSize = true;
            this.lblAudioMessage.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAudioMessage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblAudioMessage.Location = new System.Drawing.Point(4, 56);
            this.lblAudioMessage.Name = "lblAudioMessage";
            this.lblAudioMessage.Size = new System.Drawing.Size(121, 21);
            this.lblAudioMessage.TabIndex = 10;
            this.lblAudioMessage.Text = "Audio Message:";
            // 
            // lblVideoMessage
            // 
            this.lblVideoMessage.AutoSize = true;
            this.lblVideoMessage.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVideoMessage.ForeColor = System.Drawing.Color.White;
            this.lblVideoMessage.Location = new System.Drawing.Point(4, 111);
            this.lblVideoMessage.Name = "lblVideoMessage";
            this.lblVideoMessage.Size = new System.Drawing.Size(121, 21);
            this.lblVideoMessage.TabIndex = 11;
            this.lblVideoMessage.Text = "Video Message:";
            // 
            // lblTextMessage
            // 
            this.lblTextMessage.AutoSize = true;
            this.lblTextMessage.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextMessage.ForeColor = System.Drawing.Color.White;
            this.lblTextMessage.Location = new System.Drawing.Point(4, 1);
            this.lblTextMessage.Name = "lblTextMessage";
            this.lblTextMessage.Size = new System.Drawing.Size(108, 21);
            this.lblTextMessage.TabIndex = 9;
            this.lblTextMessage.Text = "Text Message:";
            // 
            // btnDuration
            // 
            this.btnDuration.BackColor = System.Drawing.Color.DarkBlue;
            this.btnDuration.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuration.Location = new System.Drawing.Point(671, 399);
            this.btnDuration.Name = "btnDuration";
            this.btnDuration.Size = new System.Drawing.Size(110, 40);
            this.btnDuration.TabIndex = 15;
            this.btnDuration.Text = "DURATION";
            this.btnDuration.UseVisualStyleBackColor = false;
            this.btnDuration.Click += new System.EventHandler(this.btnDuration_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Lime;
            this.btnSend.CausesValidation = false;
            this.btnSend.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSend.Location = new System.Drawing.Point(671, 210);
            this.btnSend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(110, 50);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.BackColor = System.Drawing.Color.DarkBlue;
            this.btnAddNew.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.Location = new System.Drawing.Point(671, 264);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(110, 40);
            this.btnAddNew.TabIndex = 16;
            this.btnAddNew.Text = "+";
            this.btnAddNew.UseVisualStyleBackColor = false;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // pnlRegion
            // 
            this.pnlRegion.ColumnCount = 1;
            this.pnlRegion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlRegion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlRegion.Controls.Add(this.cboRegion, 0, 0);
            this.pnlRegion.Location = new System.Drawing.Point(585, 353);
            this.pnlRegion.Name = "pnlRegion";
            this.pnlRegion.RowCount = 1;
            this.pnlRegion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlRegion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.pnlRegion.Size = new System.Drawing.Size(196, 40);
            this.pnlRegion.TabIndex = 18;
            this.pnlRegion.Visible = false;
            // 
            // cboRegion
            // 
            this.cboRegion.BackColor = System.Drawing.Color.DarkBlue;
            this.cboRegion.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRegion.ForeColor = System.Drawing.Color.White;
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Location = new System.Drawing.Point(3, 3);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(189, 29);
            this.cboRegion.TabIndex = 0;
            this.cboRegion.SelectedIndexChanged += new System.EventHandler(this.cboRegion_SelectedIndexChanged);
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
            // lblMoveBit
            // 
            this.lblMoveBit.AutoSize = true;
            this.lblMoveBit.Font = new System.Drawing.Font("Impact", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoveBit.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblMoveBit.Location = new System.Drawing.Point(156, 56);
            this.lblMoveBit.Name = "lblMoveBit";
            this.lblMoveBit.Size = new System.Drawing.Size(327, 98);
            this.lblMoveBit.TabIndex = 19;
            this.lblMoveBit.Text = "MOVEBIT";
            // 
            // lblNitro
            // 
            this.lblNitro.AutoSize = true;
            this.lblNitro.Font = new System.Drawing.Font("Impact", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNitro.ForeColor = System.Drawing.Color.Lime;
            this.lblNitro.Location = new System.Drawing.Point(489, 88);
            this.lblNitro.Name = "lblNitro";
            this.lblNitro.Size = new System.Drawing.Size(141, 59);
            this.lblNitro.TabIndex = 20;
            this.lblNitro.Text = "NITRO";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(787, 683);
            this.Controls.Add(this.lblNitro);
            this.Controls.Add(this.lblMoveBit);
            this.Controls.Add(this.pnlRegion);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.btnDuration);
            this.Controls.Add(this.pnlStartDuration);
            this.Controls.Add(this.btnNewMessageIcon);
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
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MoveBit - Composer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.pnlStartDuration.ResumeLayout(false);
            this.pnlStartDuration.PerformLayout();
            this.pnlRegion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playerMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboAddresses;
        private System.Windows.Forms.Button btnCompose;
        private System.Windows.Forms.RichTextBox txtOutgoing;
        private System.Windows.Forms.Button btnRegion;
        private System.Windows.Forms.Button btnMessages;
        private System.Windows.Forms.ComboBox cboFileList;
        private System.Windows.Forms.Button btnUpload;
        private AxWMPLib.AxWindowsMediaPlayer playerMain;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnNewMessageIcon;
        private System.Windows.Forms.TableLayoutPanel pnlStartDuration;
        private System.Windows.Forms.TextBox txtVideoDuration;
        private System.Windows.Forms.TextBox txtVideoStart;
        private System.Windows.Forms.TextBox txtAudioDuration;
        private System.Windows.Forms.TextBox txtAudioStart;
        private System.Windows.Forms.TextBox txtTextDuration;
        private System.Windows.Forms.TextBox txtTextStart;
        private System.Windows.Forms.Label lblTextMessage;
        private System.Windows.Forms.Label lblAudioMessage;
        private System.Windows.Forms.Label lblVideoMessage;
        private System.Windows.Forms.Button btnDuration;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TableLayoutPanel pnlRegion;
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.Label lblMoveBit;
        private System.Windows.Forms.Label lblNitro;
    }
}

