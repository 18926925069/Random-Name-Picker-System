using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 随机点名系统
{
    public partial class AddName : Form
    {
        /// <summary>
        /// Whether the UI uses English. The text is refreshed from it when the form loads.
        /// </summary>
        private bool English;

        private readonly List<string> _names = new List<string>();

        /// <summary>
        /// The names that were already in the main list when this dialog was opened. They are not added again; names typed
        /// into this dialog are checked against them so the same person cannot end up in the list twice by accident.
        /// </summary>
        private readonly List<string> _existingNames = new List<string>();

        /// <summary>
        /// The names to append to the main list. The main form reads this only when ShowDialog returned OK.
        /// </summary>
        public List<string> Names
        {
            get { return _names; }
        }

        public AddName(bool english)
        {
            InitializeComponent();
            English = english;
        }

        /// <summary>
        /// Seeds the dialog with the names from the main form, so names typed here are checked against the current list.
        /// </summary>
        public void SetExistingNames(IEnumerable<string> names)
        {
            _existingNames.AddRange(names);
        }

        private void ApplyLanguage()
        {
            if (English)
            {
                Text = "Add Names";
                LabTips.Text = "Type one or more names separated by commas, spaces or line breaks, then click Add. They are appended to the current list.";
                btnAdd.Text = "Add";
                btnOK.Text = "OK";
            }
            else
            {
                Text = "添加人员";
                LabTips.Text = "输入一个或多个姓名，用逗号、空格或换行分隔，再点击添加。新姓名将追加到现有名单之后。";
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
            List<string> names = SplitNames(txtName.Text, _names, _existingNames);
            if (names.Count == 0)
            {
                // The input box is empty or contains only names that are already in the list. Show a hint, but do not interrupt the data entry.
                MessageBox.Show(English
                    ? "Nothing to add. The names are empty or already in the list."
                    : "没有可添加的姓名：输入为空，或姓名已在现有名单中。");
                txtName.Focus();
                return;
            }

            _names.AddRange(names);
            txtName.Clear();
            txtName.Focus();
        }

        /// <summary>
        /// Splits a piece of text into a list of names by commas (both ASCII and full-width), spaces, tabs and line breaks.
        /// Empty entries are dropped; a name repeated inside the same input or already in the list is not added again.
        /// </summary>
        private static List<string> SplitNames(string text, List<string> existing, List<string> seeded)
        {
            List<string> result = new List<string>();
            string[] parts = LineSplitter.SplitNames(text);
            foreach (string name in parts)
            {
                // A name repeated within the same input is added only once, otherwise a single paste would enter the same person twice.
                // "existing" holds the names already added in this dialog and "seeded" the names that were in the main list.
                if (!existing.Contains(name) && !seeded.Contains(name))
                {
                    result.Add(name);
                }
            }

            return result;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Let users type a name and click "OK" directly, without forcing them to click "Add" first.
            _names.AddRange(SplitNames(txtName.Text, _names, _existingNames));
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
