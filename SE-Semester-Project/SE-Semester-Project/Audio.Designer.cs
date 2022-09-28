namespace SE_Semester_Project
{
    partial class frmAudio
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
            this.btnVideo = new System.Windows.Forms.Button();
            this.btnText = new System.Windows.Forms.Button();
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnVideo
            // 
            this.btnVideo.BackColor = System.Drawing.Color.AliceBlue;
            this.btnVideo.Location = new System.Drawing.Point(0, 194);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(75, 100);
            this.btnVideo.TabIndex = 6;
            this.btnVideo.Text = "<-VIDEO";
            this.btnVideo.UseVisualStyleBackColor = false;
            this.btnVideo.Click += new System.EventHandler(this.btnVideo_Click);
            // 
            // btnText
            // 
            this.btnText.BackColor = System.Drawing.Color.AliceBlue;
            this.btnText.Location = new System.Drawing.Point(200, 194);
            this.btnText.Name = "btnText";
            this.btnText.Size = new System.Drawing.Size(75, 100);
            this.btnText.TabIndex = 7;
            this.btnText.Text = "TEXT->";
            this.btnText.UseVisualStyleBackColor = false;
            this.btnText.Click += new System.EventHandler(this.btnText_Click);
            // 
            // grpMain
            // 
            this.grpMain.Controls.Add(this.btnVideo);
            this.grpMain.Controls.Add(this.btnText);
            this.grpMain.Location = new System.Drawing.Point(5, 250);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(275, 300);
            this.grpMain.TabIndex = 8;
            this.grpMain.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "\"Insert audio player\"";
            // 
            // frmAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpMain);
            this.Name = "frmAudio";
            this.Text = "AUDIO MESSAGE";
            this.grpMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnVideo;
        private Button btnText;
        private GroupBox grpMain;
        private Label label1;
    }
}