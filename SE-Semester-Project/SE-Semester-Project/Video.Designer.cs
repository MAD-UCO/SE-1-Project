namespace SE_Semester_Project
{
    partial class frmVideo
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
            this.grpMain = new System.Windows.Forms.GroupBox();
            this.btnText = new System.Windows.Forms.Button();
            this.btnAudio = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpMain
            // 
            this.grpMain.BackColor = System.Drawing.SystemColors.Control;
            this.grpMain.Controls.Add(this.btnText);
            this.grpMain.Controls.Add(this.btnAudio);
            this.grpMain.Location = new System.Drawing.Point(5, 250);
            this.grpMain.Name = "grpMain";
            this.grpMain.Size = new System.Drawing.Size(275, 300);
            this.grpMain.TabIndex = 9;
            this.grpMain.TabStop = false;
            // 
            // btnText
            // 
            this.btnText.BackColor = System.Drawing.Color.AliceBlue;
            this.btnText.Location = new System.Drawing.Point(0, 194);
            this.btnText.Name = "btnText";
            this.btnText.Size = new System.Drawing.Size(75, 100);
            this.btnText.TabIndex = 6;
            this.btnText.Text = "<-TEXT";
            this.btnText.UseVisualStyleBackColor = false;
            this.btnText.Click += new System.EventHandler(this.btnText_Click);
            // 
            // btnAudio
            // 
            this.btnAudio.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAudio.Location = new System.Drawing.Point(200, 194);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(75, 100);
            this.btnAudio.TabIndex = 7;
            this.btnAudio.Text = "AUDIO->";
            this.btnAudio.UseVisualStyleBackColor = false;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "\"Insert video player\"";
            // 
            // frmVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(284, 561);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpMain);
            this.Name = "frmVideo";
            this.Text = "VIDEO MESSAGE";
            this.grpMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox grpMain;
        private Button btnText;
        private Button btnAudio;
        private Label label1;
    }
}