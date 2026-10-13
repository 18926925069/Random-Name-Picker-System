using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 随机点名系统
{
    public partial class Form1 : Form
    {
        private List<string> _names = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            /*AddName form2 = new AddName();
            form2.Show();
            */
            using (AddName addForm = new AddName())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    _names = addForm.Names;
                }
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string InputTxt = openFileDialog.FileName;
                try
                {
                    string[] lines = File.ReadAllLines(openFileDialog.FileName, Encoding.Default);

                    foreach (string line in lines)
                    {
                        string name = line.Trim();
                        if (name.Length > 0)
                        {
                            _names.Add(name);
                        }
                    }

                    MessageBox.Show("导入成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导入失败：" + ex.Message);
                }
            }

            
        }

        private void listViewRandomName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRandomName_Click(object sender, EventArgs e)
        {

        }
    }
    
}
