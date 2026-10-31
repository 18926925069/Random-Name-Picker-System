namespace 随机点名系统
{
    partial class Settings
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
            this.grpLanguage = new System.Windows.Forms.GroupBox();
            this.radEnglish = new System.Windows.Forms.RadioButton();
            this.radChinese = new System.Windows.Forms.RadioButton();
            this.grpHelp = new System.Windows.Forms.GroupBox();
            this.btnOpenHelp = new System.Windows.Forms.Button();
            this.labHelpTips = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpLanguage.SuspendLayout();
            this.grpHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLanguage
            // 
            this.grpLanguage.Controls.Add(this.radEnglish);
            this.grpLanguage.Controls.Add(this.radChinese);
            this.grpLanguage.Location = new System.Drawing.Point(12, 12);
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.Size = new System.Drawing.Size(320, 70);
            this.grpLanguage.TabIndex = 0;
            this.grpLanguage.TabStop = false;
            this.grpLanguage.Text = "Language";
            // 
            // 
            // radChinese
            // 
            this.radChinese.AutoSize = true;
            this.radChinese.Location = new System.Drawing.Point(20, 30);
            this.radChinese.Name = "radChinese";
            this.radChinese.Size = new System.Drawing.Size(75, 16);
            this.radChinese.TabIndex = 0;
            this.radChinese.TabStop = true;
            this.radChinese.Text = "中文";
            this.radChinese.UseVisualStyleBackColor = true;
            this.radChinese.CheckedChanged += new System.EventHandler(this.radChinese_CheckedChanged);
            // 
            // radEnglish
            // 
            this.radEnglish.AutoSize = true;
            this.radEnglish.Location = new System.Drawing.Point(170, 30);
            this.radEnglish.Name = "radEnglish";
            this.radEnglish.Size = new System.Drawing.Size(75, 16);
            this.radEnglish.TabIndex = 1;
            this.radEnglish.TabStop = true;
            this.radEnglish.Text = "English";
            this.radEnglish.UseVisualStyleBackColor = true;
            this.radEnglish.CheckedChanged += new System.EventHandler(this.radEnglish_CheckedChanged);
            // 
            // grpHelp
            // 
            this.grpHelp.Controls.Add(this.btnOpenHelp);
            this.grpHelp.Controls.Add(this.labHelpTips);
            this.grpHelp.Location = new System.Drawing.Point(12, 92);
            this.grpHelp.Name = "grpHelp";
            this.grpHelp.Size = new System.Drawing.Size(320, 100);
            this.grpHelp.TabIndex = 1;
            this.grpHelp.TabStop = false;
            this.grpHelp.Text = "Help";
            // 
            // labHelpTips
            // 
            this.labHelpTips.Location = new System.Drawing.Point(18, 25);
            this.labHelpTips.Name = "labHelpTips";
            this.labHelpTips.Size = new System.Drawing.Size(286, 36);
            this.labHelpTips.TabIndex = 0;
            this.labHelpTips.Text = "The help page is opened in your browser and follows the language selected above.";
            // 
            // btnOpenHelp
            // 
            this.btnOpenHelp.Location = new System.Drawing.Point(18, 64);
            this.btnOpenHelp.Name = "btnOpenHelp";
            this.btnOpenHelp.Size = new System.Drawing.Size(286, 26);
            this.btnOpenHelp.TabIndex = 1;
            this.btnOpenHelp.Text = "Open help";
            this.btnOpenHelp.UseVisualStyleBackColor = true;
            this.btnOpenHelp.Click += new System.EventHandler(this.btnOpenHelp_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(176, 204);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(257, 204);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // Enter runs OK and Esc runs Cancel, so the dialog can be used without the mouse.
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(344, 237);
            this.Controls.Add(this.grpLanguage);
            this.Controls.Add(this.grpHelp);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            // The language is applied as soon as it changes, so Esc and the X button (Cancel) do not undo it.
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.grpLanguage.ResumeLayout(false);
            this.grpLanguage.PerformLayout();
            this.grpHelp.ResumeLayout(false);
            this.grpHelp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLanguage;
        private System.Windows.Forms.RadioButton radChinese;
        private System.Windows.Forms.RadioButton radEnglish;
        private System.Windows.Forms.GroupBox grpHelp;
        private System.Windows.Forms.Label labHelpTips;
        private System.Windows.Forms.Button btnOpenHelp;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
