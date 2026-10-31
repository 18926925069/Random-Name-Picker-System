//version:v1.0
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
            this.grpWeights = new System.Windows.Forms.GroupBox();
            this.tableWeights = new System.Windows.Forms.TableLayoutPanel();
            this.listWeights = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colPercent = new System.Windows.Forms.ColumnHeader();
            this.colWeight = new System.Windows.Forms.ColumnHeader();
            this.flowWeightButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnWeightMinus = new System.Windows.Forms.Button();
            this.btnWeightPlus = new System.Windows.Forms.Button();
            this.btnWeightReset = new System.Windows.Forms.Button();
            this.labWeightSummary = new System.Windows.Forms.Label();
            this.grpHelp = new System.Windows.Forms.GroupBox();
            this.tableHelp = new System.Windows.Forms.TableLayoutPanel();
            this.labHelpTips = new System.Windows.Forms.Label();
            this.btnOpenHelp = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpLanguage.SuspendLayout();
            this.grpWeights.SuspendLayout();
            this.tableWeights.SuspendLayout();
            this.flowWeightButtons.SuspendLayout();
            this.grpHelp.SuspendLayout();
            this.tableHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLanguage
            // 
            this.grpLanguage.Controls.Add(this.radEnglish);
            this.grpLanguage.Controls.Add(this.radChinese);
            this.grpLanguage.Location = new System.Drawing.Point(12, 12);
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.Size = new System.Drawing.Size(420, 60);
            this.grpLanguage.TabIndex = 0;
            this.grpLanguage.TabStop = false;
            this.grpLanguage.Text = "Language";
            // 
            // radChinese
            // 
            this.radChinese.AutoSize = true;
            this.radChinese.Location = new System.Drawing.Point(20, 26);
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
            this.radEnglish.Location = new System.Drawing.Point(200, 26);
            this.radEnglish.Name = "radEnglish";
            this.radEnglish.Size = new System.Drawing.Size(75, 16);
            this.radEnglish.TabIndex = 1;
            this.radEnglish.TabStop = true;
            this.radEnglish.Text = "English";
            this.radEnglish.UseVisualStyleBackColor = true;
            this.radEnglish.CheckedChanged += new System.EventHandler(this.radEnglish_CheckedChanged);
            // 
            // grpWeights
            // 
            this.grpWeights.Controls.Add(this.tableWeights);
            this.grpWeights.Location = new System.Drawing.Point(12, 78);
            this.grpWeights.Name = "grpWeights";
            this.grpWeights.Size = new System.Drawing.Size(420, 232);
            this.grpWeights.TabIndex = 1;
            this.grpWeights.TabStop = false;
            this.grpWeights.Text = "Weights";
            // 
            // tableWeights
            // 
            this.tableWeights.ColumnCount = 1;
            this.tableWeights.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableWeights.Controls.Add(this.listWeights, 0, 0);
            this.tableWeights.Controls.Add(this.flowWeightButtons, 0, 1);
            this.tableWeights.Controls.Add(this.labWeightSummary, 0, 2);
            this.tableWeights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableWeights.Location = new System.Drawing.Point(3, 17);
            this.tableWeights.Name = "tableWeights";
            this.tableWeights.Padding = new System.Windows.Forms.Padding(12, 6, 12, 8);
            this.tableWeights.RowCount = 3;
            this.tableWeights.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableWeights.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableWeights.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableWeights.Size = new System.Drawing.Size(414, 212);
            this.tableWeights.TabIndex = 0;
            // 
            // listWeights
            // 
            this.listWeights.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colPercent,
            this.colWeight});
            this.listWeights.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listWeights.FullRowSelect = true;
            this.listWeights.GridLines = true;
            this.listWeights.HideSelection = false;
            this.listWeights.Location = new System.Drawing.Point(12, 6);
            this.listWeights.Margin = new System.Windows.Forms.Padding(0);
            this.listWeights.MultiSelect = true;
            this.listWeights.Name = "listWeights";
            this.listWeights.Size = new System.Drawing.Size(390, 150);
            this.listWeights.TabIndex = 0;
            this.listWeights.UseCompatibleStateImageBehavior = false;
            this.listWeights.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 214;
            // 
            // colPercent
            // 
            this.colPercent.Text = "Chance";
            this.colPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colPercent.Width = 86;
            // 
            // colWeight
            // 
            this.colWeight.Text = "Weight";
            this.colWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colWeight.Width = 86;
            // 
            // flowWeightButtons
            // 
            this.flowWeightButtons.Controls.Add(this.btnWeightMinus);
            this.flowWeightButtons.Controls.Add(this.btnWeightPlus);
            this.flowWeightButtons.Controls.Add(this.btnWeightReset);
            this.flowWeightButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowWeightButtons.Location = new System.Drawing.Point(12, 156);
            this.flowWeightButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowWeightButtons.Name = "flowWeightButtons";
            this.flowWeightButtons.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flowWeightButtons.Size = new System.Drawing.Size(390, 30);
            this.flowWeightButtons.TabIndex = 1;
            this.flowWeightButtons.WrapContents = false;
            // 
            // btnWeightMinus
            // 
            this.btnWeightMinus.Location = new System.Drawing.Point(0, 4);
            this.btnWeightMinus.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnWeightMinus.Name = "btnWeightMinus";
            this.btnWeightMinus.Size = new System.Drawing.Size(92, 24);
            this.btnWeightMinus.TabIndex = 0;
            this.btnWeightMinus.Text = "- 1";
            this.btnWeightMinus.UseVisualStyleBackColor = true;
            this.btnWeightMinus.Click += new System.EventHandler(this.btnWeightMinus_Click);
            // 
            // btnWeightPlus
            // 
            this.btnWeightPlus.Location = new System.Drawing.Point(98, 4);
            this.btnWeightPlus.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnWeightPlus.Name = "btnWeightPlus";
            this.btnWeightPlus.Size = new System.Drawing.Size(92, 24);
            this.btnWeightPlus.TabIndex = 1;
            this.btnWeightPlus.Text = "+ 1";
            this.btnWeightPlus.UseVisualStyleBackColor = true;
            this.btnWeightPlus.Click += new System.EventHandler(this.btnWeightPlus_Click);
            // 
            // btnWeightReset
            // 
            this.btnWeightReset.Location = new System.Drawing.Point(196, 4);
            this.btnWeightReset.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnWeightReset.Name = "btnWeightReset";
            this.btnWeightReset.Size = new System.Drawing.Size(160, 24);
            this.btnWeightReset.TabIndex = 2;
            this.btnWeightReset.Text = "Reset to 1";
            this.btnWeightReset.UseVisualStyleBackColor = true;
            this.btnWeightReset.Click += new System.EventHandler(this.btnWeightReset_Click);
            // 
            // labWeightSummary
            // 
            this.labWeightSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labWeightSummary.Location = new System.Drawing.Point(15, 189);
            this.labWeightSummary.Name = "labWeightSummary";
            this.labWeightSummary.Size = new System.Drawing.Size(384, 15);
            this.labWeightSummary.TabIndex = 2;
            this.labWeightSummary.Text = "Select one or more names, then use -1 / +1. A weight of 0 means the name cannot be picked.";
            // 
            // grpHelp
            // 
            this.grpHelp.Controls.Add(this.tableHelp);
            this.grpHelp.Location = new System.Drawing.Point(12, 316);
            this.grpHelp.Name = "grpHelp";
            this.grpHelp.Size = new System.Drawing.Size(420, 96);
            this.grpHelp.TabIndex = 2;
            this.grpHelp.TabStop = false;
            this.grpHelp.Text = "Help";
            // 
            // tableHelp
            // 
            this.tableHelp.ColumnCount = 1;
            this.tableHelp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableHelp.Controls.Add(this.labHelpTips, 0, 0);
            this.tableHelp.Controls.Add(this.btnOpenHelp, 0, 1);
            this.tableHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableHelp.Location = new System.Drawing.Point(3, 17);
            this.tableHelp.Name = "tableHelp";
            this.tableHelp.Padding = new System.Windows.Forms.Padding(15, 4, 15, 8);
            this.tableHelp.RowCount = 2;
            this.tableHelp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableHelp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableHelp.Size = new System.Drawing.Size(414, 76);
            this.tableHelp.TabIndex = 0;
            // 
            // labHelpTips
            // 
            this.labHelpTips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labHelpTips.Location = new System.Drawing.Point(18, 4);
            this.labHelpTips.Name = "labHelpTips";
            this.labHelpTips.Size = new System.Drawing.Size(378, 34);
            this.labHelpTips.TabIndex = 0;
            this.labHelpTips.Text = "The help page is opened in your browser and follows the language selected above.";
            // 
            // btnOpenHelp
            // 
            this.btnOpenHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenHelp.Location = new System.Drawing.Point(18, 42);
            this.btnOpenHelp.Margin = new System.Windows.Forms.Padding(0);
            this.btnOpenHelp.Name = "btnOpenHelp";
            this.btnOpenHelp.Size = new System.Drawing.Size(378, 26);
            this.btnOpenHelp.TabIndex = 1;
            this.btnOpenHelp.Text = "Open help";
            this.btnOpenHelp.UseVisualStyleBackColor = true;
            this.btnOpenHelp.Click += new System.EventHandler(this.btnOpenHelp_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(276, 422);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(357, 422);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 4;
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
            this.ClientSize = new System.Drawing.Size(444, 457);
            this.Controls.Add(this.grpLanguage);
            this.Controls.Add(this.grpWeights);
            this.Controls.Add(this.grpHelp);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            // The language is applied as soon as it changes, so Esc and the X button (Cancel) do not undo it.
            // Weights follow the normal dialog convention: they are written back on OK only.
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.grpLanguage.ResumeLayout(false);
            this.grpLanguage.PerformLayout();
            this.grpWeights.ResumeLayout(false);
            this.tableWeights.ResumeLayout(false);
            this.tableWeights.PerformLayout();
            this.flowWeightButtons.ResumeLayout(false);
            this.grpHelp.ResumeLayout(false);
            this.tableHelp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLanguage;
        private System.Windows.Forms.RadioButton radChinese;
        private System.Windows.Forms.RadioButton radEnglish;
        private System.Windows.Forms.GroupBox grpWeights;
        private System.Windows.Forms.TableLayoutPanel tableWeights;
        private System.Windows.Forms.ListView listWeights;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colPercent;
        private System.Windows.Forms.ColumnHeader colWeight;
        private System.Windows.Forms.FlowLayoutPanel flowWeightButtons;
        private System.Windows.Forms.Button btnWeightMinus;
        private System.Windows.Forms.Button btnWeightPlus;
        private System.Windows.Forms.Button btnWeightReset;
        private System.Windows.Forms.Label labWeightSummary;
        private System.Windows.Forms.GroupBox grpHelp;
        private System.Windows.Forms.TableLayoutPanel tableHelp;
        private System.Windows.Forms.Label labHelpTips;
        private System.Windows.Forms.Button btnOpenHelp;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
