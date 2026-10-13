using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 随机点名系统
{
    public partial class Form1 : Form
    {
        private List<string> _names = new List<string>();
        bool English = true;
        private void ApplyLanguage()
        {
            string  add, input, random;

            if (English)
            {
                add = "Add manually";
                input = "Import '.txt' document";
                random = "Pick";
            }
            else
            {
                add = "手动添加";
                input = "导入'.txt'文档";
                random = "点名";
            }
            btnAdd.Text = add;
            btnInput.Text = input;
            btnRandomName.Text = random;
        }
        public Form1()
        {
            InitializeComponent();
            ApplyLanguage();
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
                addForm.English = this.English;
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
            int count = _names.Count;
            if (count <= 0)
            {
                MessageBox.Show("没有可点名的人员");
                return;
            }
            else if (count == 1)
            {
                MessageBox.Show("只有一人，无需点名");
            }
            else
            {
                Random random = new Random();
                int index = random.Next(count);
                string randomName = _names[index];
                listViewRandomName.Items.Clear();
                listViewRandomName.Items.Add(randomName);
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            English = false;
            ApplyLanguage();
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            English = true;
            ApplyLanguage();
        }
    }
    
}
