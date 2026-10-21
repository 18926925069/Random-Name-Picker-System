using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 随机点名系统
{
    public partial class AddName : Form
    {
        /// <summary>
        /// Whether the UI uses English. Set by the main form before ShowDialog; the text is refreshed from it when the form loads.
        /// </summary>
        public bool English = true;

        private readonly List<string> _names = new List<string>();

        /// <summary>
        /// The names entered this time. The main form only reads this after "OK" has been clicked.
        /// </summary>
        public List<string> Names
        {
            get { return _names; }
        }

        public AddName()
        {
            InitializeComponent();
        }

        private void ApplyLanguage()
        {
            if (English)
            {
                Text = "Add Names";
                LabTips.Text = "Type names separated by commas or spaces, then click Add.";
                btnAdd.Text = "Add";
                btnOK.Text = "OK";
            }
            else
            {
                Text = "添加人员";
                LabTips.Text = "输入姓名，多个姓名用中文逗号或空格隔开，再点击添加。";
                btnAdd.Text = "添加";
                btnOK.Text = "完成";
            }
        }

        private void AddName_Load(object sender, EventArgs e)
        {
            ApplyLanguage();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Compare against the existing names and keep only the new ones.
            List<string> names = SplitNames(txtName.Text, _names);
            if (names.Count == 0)
            {
                // The input box is empty or contains only duplicates. Show a hint, but do not interrupt the data entry.
                MessageBox.Show(English ? "Nothing to add." : "没有可添加的姓名（可能为空或重复）。");
                txtName.Focus();
                return;
            }

            _names.AddRange(names);
            txtName.Clear();
            txtName.Focus();
        }

        /// <summary>
        /// Splits a piece of text into a list of names by commas (both ASCII and full-width), spaces, tabs and line breaks.
        /// RemoveEmptyEntries drops empty items; names already in the existing list are not added again.
        /// </summary>
        private static List<string> SplitNames(string text, List<string> existing)
        {
            // Besides the half-width symbols, the full-width comma and full-width space commonly produced by input methods are also treated as separators.
            List<string> result = new List<string>();
            string[] parts = text.Split(new char[] { ' ', ',', '，', '\u3000', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                string name = part.Trim();
                // A name repeated within the same input is added only once, otherwise a single paste would enter the same person twice.
                if (name.Length > 0 && !existing.Contains(name) && !result.Contains(name))
                {
                    result.Add(name);
                }
            }

            return result;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Let users type a name and click "OK" directly, without forcing them to click "Add" first.
            _names.AddRange(SplitNames(txtName.Text, _names));
            if (_names.Count == 0)
            {
                MessageBox.Show(English
                    ? "Please add at least one name, or close this window to cancel."
                    : "请先添加至少一个姓名；也可以直接关闭本窗口取消。");
                return;
            }

            // Setting DialogResult makes ShowDialog return and closes the window automatically, so there is no need to call Close().
            DialogResult = DialogResult.OK;
        }
    }
}
