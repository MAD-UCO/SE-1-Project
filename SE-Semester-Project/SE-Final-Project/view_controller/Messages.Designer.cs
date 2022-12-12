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
            this.lblNitro = new System.Windows.Forms.Label();
            this.lblMoveBit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.playerMessages)).BeginInit();
            this.grpTextCanvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboMessages
            // 
            this.cboMessages.BackColor = System.Drawing.Color.DarkBlue;
            this.cboMessages.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMessages.ForeColor = System.Drawing.Color.White;
            this.cboMessages.FormattingEnabled = true;
            this.cboMessages.Location = new System.Drawing.Point(12, 214);
            this.cboMessages.Name = "cboMessages";
            this.cboMessages.Size = new System.Drawing.Size(299, 29);
            this.cboMessages.TabIndex = 0;
            this.cboMessages.SelectedIndexChanged += new System.EventHandler(this.CboMessages_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.DarkBlue;
            this.btnBack.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(668, 631);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(110, 40);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "<";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // playerMessages
            // 
            this.playerMessages.Enabled = true;
            this.playerMessages.Location = new System.Drawing.Point(12, 248);
            this.playerMessages.MaximumSize = new System.Drawing.Size(650, 425);
            this.playerMessages.Name = "playerMessages";
            this.playerMessages.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("playerMessages.OcxState")));
            this.playerMessages.Size = new System.Drawing.Size(650, 425);
            this.playerMessages.TabIndex = 4;
            // 
            // grpTextCanvas
            // 
            this.grpTextCanvas.BackColor = System.Drawing.Color.DarkBlue;
            this.grpTextCanvas.Controls.Add(this.lblDefault);
            this.grpTextCanvas.Controls.Add(this.lblSouth);
            this.grpTextCanvas.Controls.Add(this.lblEast);
            this.grpTextCanvas.Controls.Add(this.lblCenter);
            this.grpTextCanvas.Controls.Add(this.lblWest);
            this.grpTextCanvas.Controls.Add(this.lblNorth);
            this.grpTextCanvas.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTextCanvas.Location = new System.Drawing.Point(12, 248);
            this.grpTextCanvas.Name = "grpTextCanvas";
            this.grpTextCanvas.Size = new System.Drawing.Size(650, 425);
            this.grpTextCanvas.TabIndex = 5;
            this.grpTextCanvas.TabStop = false;
            // 
            // lblDefault
            // 
            this.lblDefault.AutoSize = true;
            this.lblDefault.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefault.ForeColor = System.Drawing.Color.White;
            this.lblDefault.Location = new System.Drawing.Point(6, 16);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(64, 21);
            this.lblDefault.TabIndex = 5;
            this.lblDefault.Text = "DEFAULT";
            // 
            // lblSouth
            // 
            this.lblSouth.AutoSize = true;
            this.lblSouth.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSouth.ForeColor = System.Drawing.Color.White;
            this.lblSouth.Location = new System.Drawing.Point(281, 359);
            this.lblSouth.MaximumSize = new System.Drawing.Size(200, 110);
            this.lblSouth.Name = "lblSouth";
            this.lblSouth.Size = new System.Drawing.Size(54, 21);
            this.lblSouth.TabIndex = 4;
            this.lblSouth.Text = "SOUTH";
            // 
            // lblEast
            // 
            this.lblEast.AutoSize = true;
            this.lblEast.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEast.ForeColor = System.Drawing.Color.White;
            this.lblEast.Location = new System.Drawing.Point(502, 200);
            this.lblEast.MaximumSize = new System.Drawing.Size(200, 0);
            this.lblEast.Name = "lblEast";
            this.lblEast.Size = new System.Drawing.Size(43, 21);
            this.lblEast.TabIndex = 3;
            this.lblEast.Text = "EAST";
            // 
            // lblCenter
            // 
            this.lblCenter.AutoSize = true;
            this.lblCenter.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCenter.ForeColor = System.Drawing.Color.White;
            this.lblCenter.Location = new System.Drawing.Point(281, 200);
            this.lblCenter.MaximumSize = new System.Drawing.Size(200, 0);
            this.lblCenter.Name = "lblCenter";
            this.lblCenter.Size = new System.Drawing.Size(59, 21);
            this.lblCenter.TabIndex = 2;
            this.lblCenter.Text = "CENTER";
            // 
            // lblWest
            // 
            this.lblWest.AutoSize = true;
            this.lblWest.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWest.ForeColor = System.Drawing.Color.White;
            this.lblWest.Location = new System.Drawing.Point(6, 200);
            this.lblWest.MaximumSize = new System.Drawing.Size(200, 0);
            this.lblWest.Name = "lblWest";
            this.lblWest.Size = new System.Drawing.Size(47, 21);
            this.lblWest.TabIndex = 1;
            this.lblWest.Text = "WEST";
            // 
            // lblNorth
            // 
            this.lblNorth.AutoSize = true;
            this.lblNorth.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNorth.ForeColor = System.Drawing.Color.White;
            this.lblNorth.Location = new System.Drawing.Point(281, 16);
            this.lblNorth.MaximumSize = new System.Drawing.Size(200, 0);
            this.lblNorth.Name = "lblNorth";
            this.lblNorth.Size = new System.Drawing.Size(54, 21);
            this.lblNorth.TabIndex = 0;
            this.lblNorth.Text = "NORTH";
            // 
            // lblNitro
            // 
            this.lblNitro.AutoSize = true;
            this.lblNitro.Font = new System.Drawing.Font("Impact", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNitro.ForeColor = System.Drawing.Color.Lime;
            this.lblNitro.Location = new System.Drawing.Point(489, 88);
            this.lblNitro.Name = "lblNitro";
            this.lblNitro.Size = new System.Drawing.Size(141, 59);
            this.lblNitro.TabIndex = 22;
            this.lblNitro.Text = "NITRO";
            // 
            // lblMoveBit
            // 
            this.lblMoveBit.AutoSize = true;
            this.lblMoveBit.Font = new System.Drawing.Font("Impact", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoveBit.ForeColor = System.Drawing.Color.GhostWhite;
            this.lblMoveBit.Location = new System.Drawing.Point(156, 56);
            this.lblMoveBit.Name = "lblMoveBit";
            this.lblMoveBit.Size = new System.Drawing.Size(327, 98);
            this.lblMoveBit.TabIndex = 21;
            this.lblMoveBit.Text = "MOVEBIT";
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(787, 683);
            this.Controls.Add(this.lblNitro);
            this.Controls.Add(this.grpTextCanvas);
            this.Controls.Add(this.lblMoveBit);
            this.Controls.Add(this.playerMessages);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cboMessages);
            this.Name = "Messages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Messages_FormClosing);
            this.Load += new System.EventHandler(this.Messages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playerMessages)).EndInit();
            this.grpTextCanvas.ResumeLayout(false);
            this.grpTextCanvas.PerformLayout();
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
        private System.Windows.Forms.Label lblNitro;
        private System.Windows.Forms.Label lblMoveBit;
    }
}