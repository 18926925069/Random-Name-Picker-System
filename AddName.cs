using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace 随机点名系统
{
    public partial class AddName : Form
    {
        public bool English = true;
        string btnAddText, tips, btnOkText, cue;
        public AddName()
        {
            InitializeComponent();
        }
        public void ApplyLanguage()
        {
            if (English)
            {
                btnAddText = "Add";
                tips = "Enter a name and click Add";
                btnOkText = "OK";
                cue = "Name cannot be empty.";
            }
            else
            {
                btnAddText = "添加";
                tips = "输入姓名后点击添加";
                btnOkText = "确定";
                cue = "姓名不能为空";
            }

            LabTips.Text = tips;
            btnOK.Text = btnOkText;
            btnAdd.Text = btnAddText;
        }

        private void AddName_Load(object sender, EventArgs e)
        {
            ApplyLanguage();
        }

        private List<string> _names = new List<string>();
        public List<string> Names
        {
            get { return _names; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ApplyLanguage();
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(cue);
                txtName.Focus();
                return;
            }
            _names.Add(name);
            txtName.Clear();
            txtName.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ApplyLanguage();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LabTips_Click(object sender, EventArgs e)
        {

        }
    }
}
