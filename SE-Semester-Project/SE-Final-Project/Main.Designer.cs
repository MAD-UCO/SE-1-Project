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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.cboFileList = new System.Windows.Forms.ComboBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.playerMain = new AxWMPLib.AxWindowsMediaPlayer();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.playerMain)).BeginInit();
            this.SuspendLayout();
            // 
            // cboAddresses
            // 
            this.cboAddresses.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAddresses.FormattingEnabled = true;
            this.cboAddresses.Location = new System.Drawing.Point(12, 22);
            this.cboAddresses.Name = "cboAddresses";
            this.cboAddresses.Size = new System.Drawing.Size(225, 28);
            this.cboAddresses.TabIndex = 0;
            this.cboAddresses.Text = "To:";
            this.cboAddresses.SelectedIndexChanged += new System.EventHandler(this.cboAddresses_SelectedIndexChanged);
            // 
            // btnCompose
            // 
            this.btnCompose.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompose.Location = new System.Drawing.Point(668, 91);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(98, 52);
            this.btnCompose.TabIndex = 1;
            this.btnCompose.Text = "comp";
            this.btnCompose.UseVisualStyleBackColor = true;
            this.btnCompose.Click += new System.EventHandler(this.btnCompose_Click);
            // 
            // txtOutgoing
            // 
            this.txtOutgoing.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutgoing.Location = new System.Drawing.Point(12, 56);
            this.txtOutgoing.Name = "txtOutgoing";
            this.txtOutgoing.Size = new System.Drawing.Size(650, 425);
            this.txtOutgoing.TabIndex = 2;
            this.txtOutgoing.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.LightGreen;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(668, 160);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 50);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "SND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Silver;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(668, 227);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 50);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "(X|";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(668, 294);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 50);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnd.Location = new System.Drawing.Point(668, 361);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(100, 50);
            this.btnEnd.TabIndex = 6;
            this.btnEnd.Text = "END";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnMessages
            // 
            this.btnMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMessages.Location = new System.Drawing.Point(668, 431);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(100, 50);
            this.btnMessages.TabIndex = 7;
            this.btnMessages.Text = "msgs";
            this.btnMessages.UseVisualStyleBackColor = true;
            this.btnMessages.Click += new System.EventHandler(this.btnMessages_Click);
            // 
            // cboFileList
            // 
            this.cboFileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFileList.FormattingEnabled = true;
            this.cboFileList.Location = new System.Drawing.Point(356, 22);
            this.cboFileList.Name = "cboFileList";
            this.cboFileList.Size = new System.Drawing.Size(175, 28);
            this.cboFileList.TabIndex = 8;
            this.cboFileList.SelectedIndexChanged += new System.EventHandler(this.cboFileList_SelectedIndexChanged);
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(537, 22);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(125, 28);
            this.btnUpload.TabIndex = 9;
            this.btnUpload.Text = "BROWSE";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // playerMain
            // 
            this.playerMain.Enabled = true;
            this.playerMain.Location = new System.Drawing.Point(12, 56);
            this.playerMain.MaximumSize = new System.Drawing.Size(650, 425);
            this.playerMain.Name = "playerMain";
            this.playerMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("playerMain.OcxState")));
            this.playerMain.Size = new System.Drawing.Size(650, 425);
            this.playerMain.TabIndex = 10;
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(668, 22);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(98, 52);
            this.btnLogout.TabIndex = 11;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(782, 503);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.playerMain);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.cboFileList);
            this.Controls.Add(this.btnMessages);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtOutgoing);
            this.Controls.Add(this.btnCompose);
            this.Controls.Add(this.cboAddresses);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MoveBit - Composer";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playerMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboAddresses;
        private System.Windows.Forms.Button btnCompose;
        private System.Windows.Forms.RichTextBox txtOutgoing;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnMessages;
        private System.Windows.Forms.ComboBox cboFileList;
        private System.Windows.Forms.Button btnUpload;
        private AxWMPLib.AxWindowsMediaPlayer playerMain;
        private System.Windows.Forms.Button btnLogout;
    }
}

