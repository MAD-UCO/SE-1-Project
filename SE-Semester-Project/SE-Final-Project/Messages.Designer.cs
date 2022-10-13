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
            ((System.ComponentModel.ISupportInitialize)(this.playerMessages)).BeginInit();
            this.grpTextCanvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboMessages
            // 
            this.cboMessages.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMessages.FormattingEnabled = true;
            this.cboMessages.Location = new System.Drawing.Point(12, 22);
            this.cboMessages.Name = "cboMessages";
            this.cboMessages.Size = new System.Drawing.Size(225, 28);
            this.cboMessages.TabIndex = 0;
            this.cboMessages.SelectedIndexChanged += new System.EventHandler(this.cboMessages_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(670, 431);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 50);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // playerMessages
            // 
            this.playerMessages.Enabled = true;
            this.playerMessages.Location = new System.Drawing.Point(0, 0);
            this.playerMessages.MaximumSize = new System.Drawing.Size(650, 425);
            this.playerMessages.Name = "playerMessages";
            this.playerMessages.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("playerMessages.OcxState")));
            this.playerMessages.Size = new System.Drawing.Size(650, 425);
            this.playerMessages.TabIndex = 4;
            // 
            // grpTextCanvas
            // 
            this.grpTextCanvas.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.grpTextCanvas.Controls.Add(this.lblDefault);
            this.grpTextCanvas.Controls.Add(this.playerMessages);
            this.grpTextCanvas.Controls.Add(this.lblSouth);
            this.grpTextCanvas.Controls.Add(this.lblEast);
            this.grpTextCanvas.Controls.Add(this.lblCenter);
            this.grpTextCanvas.Controls.Add(this.lblWest);
            this.grpTextCanvas.Controls.Add(this.lblNorth);
            this.grpTextCanvas.Location = new System.Drawing.Point(12, 56);
            this.grpTextCanvas.Name = "grpTextCanvas";
            this.grpTextCanvas.Size = new System.Drawing.Size(650, 425);
            this.grpTextCanvas.TabIndex = 5;
            this.grpTextCanvas.TabStop = false;
            this.grpTextCanvas.Enter += new System.EventHandler(this.grpTextCanvas_Enter);
            // 
            // lblDefault
            // 
            this.lblDefault.AutoSize = true;
            this.lblDefault.Location = new System.Drawing.Point(0, 0);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(49, 16);
            this.lblDefault.TabIndex = 5;
            this.lblDefault.Text = "Default";
            // 
            // lblSouth
            // 
            this.lblSouth.AutoSize = true;
            this.lblSouth.Location = new System.Drawing.Point(250, 392);
            this.lblSouth.Name = "lblSouth";
            this.lblSouth.Size = new System.Drawing.Size(55, 16);
            this.lblSouth.TabIndex = 4;
            this.lblSouth.Text = "SOUTH";
            // 
            // lblEast
            // 
            this.lblEast.AutoSize = true;
            this.lblEast.Location = new System.Drawing.Point(507, 204);
            this.lblEast.MaximumSize = new System.Drawing.Size(175, 0);
            this.lblEast.Name = "lblEast";
            this.lblEast.Size = new System.Drawing.Size(43, 16);
            this.lblEast.TabIndex = 3;
            this.lblEast.Text = "EAST";
            this.lblEast.Click += new System.EventHandler(this.lblEast_Click);
            // 
            // lblCenter
            // 
            this.lblCenter.AutoSize = true;
            this.lblCenter.Location = new System.Drawing.Point(250, 204);
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(63, 16);
            this.lblCenter.TabIndex = 2;
            this.lblCenter.Text = "CENTER";
            // 
            // lblWest
            // 
            this.lblWest.AutoSize = true;
            this.lblWest.Location = new System.Drawing.Point(0, 204);
            this.lblWest.Name = "lblWest";
            this.lblWest.Size = new System.Drawing.Size(47, 16);
            this.lblWest.TabIndex = 1;
            this.lblWest.Text = "WEST";
            // 
            // lblNorth
            // 
            this.lblNorth.AutoSize = true;
            this.lblNorth.Location = new System.Drawing.Point(250, 0);
            this.lblNorth.Name = "lblNorth";
            this.lblNorth.Size = new System.Drawing.Size(56, 16);
            this.lblNorth.TabIndex = 0;
            this.lblNorth.Text = "NORTH";
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(782, 503);
            this.Controls.Add(this.grpTextCanvas);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cboMessages);
            this.Name = "Messages";
            this.Text = "MoveBit - Messages";
            this.Load += new System.EventHandler(this.Messages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playerMessages)).EndInit();
            this.grpTextCanvas.ResumeLayout(false);
            this.grpTextCanvas.PerformLayout();
            this.ResumeLayout(false);

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
    }
}