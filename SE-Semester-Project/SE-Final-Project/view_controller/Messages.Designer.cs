namespace SE_Final_Project
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Messages));
            this.cboMessages = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.playerMessages = new AxWMPLib.AxWindowsMediaPlayer();
            this.grpTextCanvas = new System.Windows.Forms.GroupBox();
            this.lblDefault = new System.Windows.Forms.Label();
            this.lblSouth = new System.Windows.Forms.Label();
            this.lblEast = new System.Windows.Forms.Label();
            this.lblCenter = new System.Windows.Forms.Label();
            this.lblWest = new System.Windows.Forms.Label();
            this.lblNorth = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.textAudio = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.playerMessages)).BeginInit();
            this.grpTextCanvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboMessages
            // 
            this.cboMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMessages.FormattingEnabled = true;
            this.cboMessages.Location = new System.Drawing.Point(9, 174);
            this.cboMessages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboMessages.Name = "cboMessages";
            this.cboMessages.Size = new System.Drawing.Size(225, 25);
            this.cboMessages.TabIndex = 0;
            this.cboMessages.SelectedIndexChanged += new System.EventHandler(this.CboMessages_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(504, 506);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 41);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // playerMessages
            // 
            this.playerMessages.Enabled = true;
            this.playerMessages.Location = new System.Drawing.Point(13, 386);
            this.playerMessages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.playerMessages.MaximumSize = new System.Drawing.Size(488, 345);
            this.playerMessages.Name = "playerMessages";
            this.playerMessages.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("playerMessages.OcxState")));
            this.playerMessages.Size = new System.Drawing.Size(487, 207);
            this.playerMessages.TabIndex = 4;
            // 
            // grpTextCanvas
            // 
            this.grpTextCanvas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpTextCanvas.Controls.Add(this.lblDefault);
            this.grpTextCanvas.Controls.Add(this.lblSouth);
            this.grpTextCanvas.Controls.Add(this.lblEast);
            this.grpTextCanvas.Controls.Add(this.lblCenter);
            this.grpTextCanvas.Controls.Add(this.lblWest);
            this.grpTextCanvas.Controls.Add(this.lblNorth);
            this.grpTextCanvas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTextCanvas.Location = new System.Drawing.Point(9, 202);
            this.grpTextCanvas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpTextCanvas.Name = "grpTextCanvas";
            this.grpTextCanvas.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpTextCanvas.Size = new System.Drawing.Size(488, 204);
            this.grpTextCanvas.TabIndex = 5;
            this.grpTextCanvas.TabStop = false;
            // 
            // lblDefault
            // 
            this.lblDefault.AutoSize = true;
            this.lblDefault.Location = new System.Drawing.Point(0, 0);
            this.lblDefault.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(61, 20);
            this.lblDefault.TabIndex = 5;
            this.lblDefault.Text = "Default";
            // 
            // lblSouth
            // 
            this.lblSouth.AutoSize = true;
            this.lblSouth.Location = new System.Drawing.Point(188, 265);
            this.lblSouth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSouth.MaximumSize = new System.Drawing.Size(150, 89);
            this.lblSouth.Name = "lblSouth";
            this.lblSouth.Size = new System.Drawing.Size(65, 20);
            this.lblSouth.TabIndex = 4;
            this.lblSouth.Text = "SOUTH";
            // 
            // lblEast
            // 
            this.lblEast.AutoSize = true;
            this.lblEast.Location = new System.Drawing.Point(328, 162);
            this.lblEast.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEast.MaximumSize = new System.Drawing.Size(150, 0);
            this.lblEast.Name = "lblEast";
            this.lblEast.Size = new System.Drawing.Size(51, 20);
            this.lblEast.TabIndex = 3;
            this.lblEast.Text = "EAST";
            // 
            // lblCenter
            // 
            this.lblCenter.AutoSize = true;
            this.lblCenter.Location = new System.Drawing.Point(188, 162);
            this.lblCenter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCenter.MaximumSize = new System.Drawing.Size(150, 0);
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(74, 20);
            this.lblCenter.TabIndex = 2;
            this.lblCenter.Text = "CENTER";
            // 
            // lblWest
            // 
            this.lblWest.AutoSize = true;
            this.lblWest.Location = new System.Drawing.Point(0, 162);
            this.lblWest.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWest.MaximumSize = new System.Drawing.Size(150, 0);
            this.lblWest.Name = "lblWest";
            this.lblWest.Size = new System.Drawing.Size(55, 20);
            this.lblWest.TabIndex = 1;
            this.lblWest.Text = "WEST";
            // 
            // lblNorth
            // 
            this.lblNorth.AutoSize = true;
            this.lblNorth.Location = new System.Drawing.Point(188, 0);
            this.lblNorth.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNorth.MaximumSize = new System.Drawing.Size(150, 0);
            this.lblNorth.Name = "lblNorth";
            this.lblNorth.Size = new System.Drawing.Size(65, 20);
            this.lblNorth.TabIndex = 0;
            this.lblNorth.Text = "NORTH";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(124, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(339, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // timer4
            // 
            this.timer4.Interval = 1000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timer5
            // 
            this.timer5.Interval = 1000;
            this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
            // 
            // textAudio
            // 
            this.textAudio.BackColor = System.Drawing.Color.IndianRed;
            this.textAudio.Location = new System.Drawing.Point(502, 259);
            this.textAudio.Multiline = true;
            this.textAudio.Name = "textAudio";
            this.textAudio.Size = new System.Drawing.Size(133, 69);
            this.textAudio.TabIndex = 6;
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(679, 604);
            this.Controls.Add(this.textAudio);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grpTextCanvas);
            this.Controls.Add(this.playerMessages);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cboMessages);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Messages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Messages_FormClosing);
            this.Load += new System.EventHandler(this.Messages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playerMessages)).EndInit();
            this.grpTextCanvas.ResumeLayout(false);
            this.grpTextCanvas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboMessages;
        private System.Windows.Forms.Button btnBack;
        private AxWMPLib.AxWindowsMediaPlayer playerMessages;
        private System.Windows.Forms.GroupBox grpTextCanvas;
        private System.Windows.Forms.Label lblSouth;
        private System.Windows.Forms.Label lblEast;
        private System.Windows.Forms.Label lblCenter;
        private System.Windows.Forms.Label lblWest;
        private System.Windows.Forms.Label lblNorth;
        private System.Windows.Forms.Label lblDefault;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Timer timer5;
        private System.Windows.Forms.TextBox textAudio;
    }
}