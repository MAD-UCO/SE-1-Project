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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.cboAddresses = new System.Windows.Forms.ComboBox();
            this.btnCompose = new System.Windows.Forms.Button();
            this.txtOutgoing = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnDuration = new System.Windows.Forms.Button();
            this.btnMessages = new System.Windows.Forms.Button();
            this.cboFileList = new System.Windows.Forms.ComboBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.playerMain = new AxWMPLib.AxWindowsMediaPlayer();
            this.clearbtn = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerMain)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboAddresses
            // 
            this.cboAddresses.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAddresses.FormattingEnabled = true;
            this.cboAddresses.Location = new System.Drawing.Point(11, 175);
            this.cboAddresses.Margin = new System.Windows.Forms.Padding(2);
            this.cboAddresses.Name = "cboAddresses";
            this.cboAddresses.Size = new System.Drawing.Size(170, 25);
            this.cboAddresses.TabIndex = 0;
            this.cboAddresses.SelectedIndexChanged += new System.EventHandler(this.cboAddresses_SelectedIndexChanged);
            this.cboAddresses.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboAddresses_KeyUp);
            // 
            // btnCompose
            // 
            this.btnCompose.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompose.Location = new System.Drawing.Point(503, 231);
            this.btnCompose.Margin = new System.Windows.Forms.Padding(2);
            this.btnCompose.Name = "btnCompose";
            this.btnCompose.Size = new System.Drawing.Size(74, 42);
            this.btnCompose.TabIndex = 1;
            this.btnCompose.Text = "comp";
            this.btnCompose.UseVisualStyleBackColor = true;
            this.btnCompose.Click += new System.EventHandler(this.btnCompose_Click);
            // 
            // txtOutgoing
            // 
            this.txtOutgoing.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutgoing.Location = new System.Drawing.Point(10, 204);
            this.txtOutgoing.Margin = new System.Windows.Forms.Padding(2);
            this.txtOutgoing.Name = "txtOutgoing";
            this.txtOutgoing.Size = new System.Drawing.Size(488, 346);
            this.txtOutgoing.TabIndex = 2;
            this.txtOutgoing.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.LightGreen;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(503, 287);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 41);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "SND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Silver;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(503, 341);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 41);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "(X|";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(503, 396);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 41);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnDuration
            // 
            this.btnDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuration.Location = new System.Drawing.Point(503, 450);
            this.btnDuration.Margin = new System.Windows.Forms.Padding(2);
            this.btnDuration.Name = "btnDuration";
            this.btnDuration.Size = new System.Drawing.Size(75, 41);
            this.btnDuration.TabIndex = 6;
            this.btnDuration.Text = "Duration";
            this.btnDuration.UseVisualStyleBackColor = true;
            this.btnDuration.Click += new System.EventHandler(this.btnDuration_Click);
            // 
            // btnMessages
            // 
            this.btnMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMessages.Location = new System.Drawing.Point(503, 507);
            this.btnMessages.Margin = new System.Windows.Forms.Padding(2);
            this.btnMessages.Name = "btnMessages";
            this.btnMessages.Size = new System.Drawing.Size(75, 41);
            this.btnMessages.TabIndex = 7;
            this.btnMessages.Text = "msgs";
            this.btnMessages.UseVisualStyleBackColor = true;
            this.btnMessages.Click += new System.EventHandler(this.btnMessages_Click);
            // 
            // cboFileList
            // 
            this.cboFileList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFileList.FormattingEnabled = true;
            this.cboFileList.Location = new System.Drawing.Point(245, 175);
            this.cboFileList.Margin = new System.Windows.Forms.Padding(2);
            this.cboFileList.Name = "cboFileList";
            this.cboFileList.Size = new System.Drawing.Size(132, 25);
            this.cboFileList.TabIndex = 8;
            this.cboFileList.SelectedIndexChanged += new System.EventHandler(this.cboFileList_SelectedIndexChanged);
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(381, 175);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(94, 23);
            this.btnUpload.TabIndex = 9;
            this.btnUpload.Text = "BROWSE";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(503, 175);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(74, 42);
            this.btnLogout.TabIndex = 11;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(339, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // playerMain
            // 
            this.playerMain.Enabled = true;
            this.playerMain.Location = new System.Drawing.Point(10, 231);
            this.playerMain.Margin = new System.Windows.Forms.Padding(2);
            this.playerMain.MaximumSize = new System.Drawing.Size(488, 345);
            this.playerMain.Name = "playerMain";
            this.playerMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("playerMain.OcxState")));
            this.playerMain.Size = new System.Drawing.Size(488, 345);
            this.playerMain.TabIndex = 10;
            // 
            // clearbtn
            // 
            this.clearbtn.BackColor = System.Drawing.Color.Red;
            this.clearbtn.Location = new System.Drawing.Point(478, 175);
            this.clearbtn.Name = "clearbtn";
            this.clearbtn.Size = new System.Drawing.Size(20, 22);
            this.clearbtn.TabIndex = 13;
            this.clearbtn.UseVisualStyleBackColor = false;
            this.clearbtn.Click += new System.EventHandler(this.clearbtn_Click);
            // 
            // preview
            // 
            this.preview.Location = new System.Drawing.Point(451, 120);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(72, 21);
            this.preview.TabIndex = 14;
            this.preview.Text = "button1";
            this.preview.UseVisualStyleBackColor = true;
            this.preview.Click += new System.EventHandler(this.preview_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.tableLayoutPanel1.Controls.Add(this.textBox6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(21, 242);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(434, 195);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label3.Location = new System.Drawing.Point(3, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(153, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(303, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(128, 20);
            this.textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(153, 65);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(144, 20);
            this.textBox3.TabIndex = 5;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(303, 65);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(128, 20);
            this.textBox4.TabIndex = 6;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(153, 127);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(144, 20);
            this.textBox5.TabIndex = 7;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(303, 127);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(128, 20);
            this.textBox6.TabIndex = 8;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(449, 85);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 16;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(371, 81);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(593, 583);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.clearbtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.playerMain);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.cboFileList);
            this.Controls.Add(this.btnMessages);
            this.Controls.Add(this.btnDuration);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtOutgoing);
            this.Controls.Add(this.btnCompose);
            this.Controls.Add(this.cboAddresses);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MoveBit - Composer";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerMain)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboAddresses;
        private System.Windows.Forms.Button btnCompose;
        private System.Windows.Forms.RichTextBox txtOutgoing;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnDuration;
        private System.Windows.Forms.Button btnMessages;
        private System.Windows.Forms.ComboBox cboFileList;
        private System.Windows.Forms.Button btnUpload;
        private AxWMPLib.AxWindowsMediaPlayer playerMain;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button clearbtn;
        private System.Windows.Forms.Button preview;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Timer timer1;
    }
}

