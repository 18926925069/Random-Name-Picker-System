namespace 随机点名系统
{
    partial class Index
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnInput = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flpbtnAdd = new System.Windows.Forms.FlowLayoutPanel();
            this.flpbtnInput = new System.Windows.Forms.FlowLayoutPanel();
            this.flpbtnRandomName = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRandomName = new System.Windows.Forms.Button();
            this.FlpTxt = new System.Windows.Forms.FlowLayoutPanel();
            this.listViewRandomName = new System.Windows.Forms.ListView();
            this.flpbtnSwitch = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flpbtnAdd.SuspendLayout();
            this.flpbtnInput.SuspendLayout();
            this.flpbtnRandomName.SuspendLayout();
            this.FlpTxt.SuspendLayout();
            this.flpbtnSwitch.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "Name.txt";
            this.openFileDialog.Filter = "\"文本文件(*.txt)|*.txt|所有文件(*.*)|*.*\"";
            this.openFileDialog.Title = "选择含有名字的文件...";
            // 
            // btnInput
            // 
            this.btnInput.Location = new System.Drawing.Point(3, 3);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(102, 36);
            this.btnInput.TabIndex = 0;
            this.btnInput.Text = "导入txt格式名单";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 35);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "手动添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.flpbtnAdd, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flpbtnInput, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.flpbtnRandomName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.FlpTxt, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flpbtnSwitch, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(-1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.49144F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.38631F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(329, 409);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // flpbtnAdd
            // 
            this.flpbtnAdd.Controls.Add(this.btnAdd);
            this.flpbtnAdd.Location = new System.Drawing.Point(3, 3);
            this.flpbtnAdd.Name = "flpbtnAdd";
            this.flpbtnAdd.Size = new System.Drawing.Size(103, 40);
            this.flpbtnAdd.TabIndex = 2;
            // 
            // flpbtnInput
            // 
            this.flpbtnInput.Controls.Add(this.btnInput);
            this.flpbtnInput.Location = new System.Drawing.Point(221, 3);
            this.flpbtnInput.Name = "flpbtnInput";
            this.flpbtnInput.Size = new System.Drawing.Size(105, 40);
            this.flpbtnInput.TabIndex = 3;
            // 
            // flpbtnRandomName
            // 
            this.flpbtnRandomName.Controls.Add(this.btnRandomName);
            this.flpbtnRandomName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpbtnRandomName.Location = new System.Drawing.Point(112, 207);
            this.flpbtnRandomName.Name = "flpbtnRandomName";
            this.flpbtnRandomName.Size = new System.Drawing.Size(103, 199);
            this.flpbtnRandomName.TabIndex = 5;
            // 
            // btnRandomName
            // 
            this.btnRandomName.Location = new System.Drawing.Point(3, 3);
            this.btnRandomName.Name = "btnRandomName";
            this.btnRandomName.Size = new System.Drawing.Size(100, 51);
            this.btnRandomName.TabIndex = 0;
            this.btnRandomName.Text = "抽取一个名字";
            this.btnRandomName.UseVisualStyleBackColor = true;
            this.btnRandomName.Click += new System.EventHandler(this.btnRandomName_Click);
            // 
            // FlpTxt
            // 
            this.FlpTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.FlpTxt, 3);
            this.FlpTxt.Controls.Add(this.listViewRandomName);
            this.FlpTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlpTxt.Location = new System.Drawing.Point(3, 50);
            this.FlpTxt.Name = "FlpTxt";
            this.FlpTxt.Size = new System.Drawing.Size(323, 151);
            this.FlpTxt.TabIndex = 4;
            // 
            // listViewRandomName
            // 
            this.listViewRandomName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listViewRandomName.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewRandomName.HideSelection = false;
            this.listViewRandomName.Location = new System.Drawing.Point(3, 3);
            this.listViewRandomName.Name = "listViewRandomName";
            this.listViewRandomName.Size = new System.Drawing.Size(326, 154);
            this.listViewRandomName.TabIndex = 0;
            this.listViewRandomName.UseCompatibleStateImageBehavior = false;
            // 
            // flpbtnSwitch
            // 
            this.flpbtnSwitch.Controls.Add(this.btnSettings);
            this.flpbtnSwitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpbtnSwitch.Location = new System.Drawing.Point(3, 207);
            this.flpbtnSwitch.Name = "flpbtnSwitch";
            this.flpbtnSwitch.Size = new System.Drawing.Size(103, 199);
            this.flpbtnSwitch.TabIndex = 6;
            // 
            // btnSettings
            // 
            // The language switch and the help page moved into the Settings dialog; this is the only button that opens it.
            this.btnSettings.Location = new System.Drawing.Point(3, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(100, 35);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "设置";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 410);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Index";
            this.Text = "随机点名系统";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flpbtnAdd.ResumeLayout(false);
            this.flpbtnInput.ResumeLayout(false);
            this.flpbtnRandomName.ResumeLayout(false);
            this.FlpTxt.ResumeLayout(false);
            this.flpbtnSwitch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flpbtnAdd;
        private System.Windows.Forms.FlowLayoutPanel flpbtnInput;
        private System.Windows.Forms.FlowLayoutPanel FlpTxt;
        private System.Windows.Forms.FlowLayoutPanel flpbtnRandomName;
        private System.Windows.Forms.ListView listViewRandomName;
        private System.Windows.Forms.Button btnRandomName;
        private System.Windows.Forms.FlowLayoutPanel flpbtnSwitch;
        private System.Windows.Forms.Button btnSettings;
    }
}

