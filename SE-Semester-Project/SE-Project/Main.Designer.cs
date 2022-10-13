namespace SE_Semester_Project
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.cboAddresses = new System.Windows.Forms.ComboBox();
            this.txtOutgoing = new System.Windows.Forms.RichTextBox();
            this.cboFileList = new System.Windows.Forms.ComboBox();
            this.btnCompose = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnStartTime = new System.Windows.Forms.Button();
            this.btnFinishTime = new System.Windows.Forms.Button();
            this.tmrStartEnd = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // cboAddresses
            // 
            this.cboAddresses.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboAddresses.FormattingEnabled = true;
            this.cboAddresses.Location = new System.Drawing.Point(12, 17);
            this.cboAddresses.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboAddresses.Name = "cboAddresses";
            this.cboAddresses.Size = new System.Drawing.Size(225, 28);
            this.cboAddresses.TabIndex = 6;
            this.cboAddresses.Text = "To:";
            // 
            // txtOutgoing
            // 
            this.txtOutgoing.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutgoing.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutgoing.Enabled = false;
            this.txtOutgoing.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtOutgoing.Location = new System.Drawing.Point(12, 50);
            this.txtOutgoing.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutgoing.Name = "txtOutgoing";
            this.txtOutgoing.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOutgoing.Size = new System.Drawing.Size(400, 175);
            this.txtOutgoing.TabIndex = 0;
            this.txtOutgoing.Text = "";
            this.txtOutgoing.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.txtOutgoing_ContentsResized);
            // 
            // cboFileList
            // 
            this.cboFileList.BackColor = System.Drawing.SystemColors.Window;
            this.cboFileList.Enabled = false;
            this.cboFileList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cboFileList.FormattingEnabled = true;
            this.cboFileList.Location = new System.Drawing.Point(12, 229);
            this.cboFileList.Name = "cboFileList";
            this.cboFileList.Size = new System.Drawing.Size(225, 28);
            this.cboFileList.TabIndex = 7;
            this.cboFileList.Text = "\"Select file\"";
            // 
            // btnCompose
            // 
            this.btnCompose.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCompose.Location = new System.Drawing.Point(382, 17);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(30, 30);
            this.btnCompose.TabIndex = 9;
            this.btnCompose.UseVisualStyleBackColor = true;
            this.btnCompose.Click += new System.EventHandler(this.btnCompose_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Enabled = false;
            this.btnUpload.Location = new System.Drawing.Point(287, 229);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(125, 28);
            this.btnUpload.TabIndex = 11;
            this.btnUpload.Text = "\"Browse\"";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnMessages
            // 
            this.btnMessages.Location = new System.Drawing.Point(420, 207);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(75, 50);
            this.btnMessages.TabIndex = 13;
            this.btnMessages.UseVisualStyleBackColor = true;
            this.btnMessages.Click += new System.EventHandler(this.btnMessages_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.LightGreen;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSend.Location = new System.Drawing.Point(420, 17);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 50);
            this.btnSend.TabIndex = 16;
            this.btnSend.Text = "send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Location = new System.Drawing.Point(420, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 50);
            this.button2.TabIndex = 17;
            this.button2.Text = "delete";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnStartTime
            // 
            this.btnStartTime.Location = new System.Drawing.Point(420, 133);
            this.btnStartTime.Name = "btnStartTime";
            this.btnStartTime.Size = new System.Drawing.Size(75, 29);
            this.btnStartTime.TabIndex = 18;
            this.btnStartTime.Text = "start";
            this.btnStartTime.UseVisualStyleBackColor = true;
            this.btnStartTime.Click += new System.EventHandler(this.btnStartTime_Click);
            // 
            // btnFinishTime
            // 
            this.btnFinishTime.Location = new System.Drawing.Point(420, 170);
            this.btnFinishTime.Name = "btnFinishTime";
            this.btnFinishTime.Size = new System.Drawing.Size(75, 29);
            this.btnFinishTime.TabIndex = 19;
            this.btnFinishTime.Text = "end";
            this.btnFinishTime.UseVisualStyleBackColor = true;
            this.btnFinishTime.Click += new System.EventHandler(this.btnFinishTime_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(507, 278);
            this.Controls.Add(this.btnFinishTime);
            this.Controls.Add(this.btnStartTime);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnMessages);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnCompose);
            this.Controls.Add(this.cboFileList);
            this.Controls.Add(this.cboAddresses);
            this.Controls.Add(this.txtOutgoing);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "\"Product Name\"";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private ComboBox cboAddresses;
        private RichTextBox txtOutgoing;
        private ComboBox cboFileList;
        private Button btnCompose;
        private Button btnUpload;
        private Button btnMessages;
        private Button btnSend;
        private Button button2;
        private Button btnStartTime;
        private Button btnFinishTime;
        private System.Windows.Forms.Timer tmrStartEnd;
    }
}