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
        public AddName()
        {
            InitializeComponent();
        }

        private void AddName_Load(object sender, EventArgs e)
        {
            
        }

        private List<string> _names = new List<string>();
        public List<string> Names
        {
            get { return _names; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("姓名不能为空");
                txtName.Focus();
                return;
            }
            _names.Add(name);
            txtName.Clear();
            txtName.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
