//version:v1.0
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 随机点名系统
{
    /// <summary>
    /// The "Settings / 设置" dialog: the application language, the weight of every name and the help page live here, so the
    /// main window only needs one entry point instead of one button per option.
    /// The language is applied as soon as the user switches it (ApplyLanguage is called immediately), therefore OK,
    /// Cancel, Esc and the X button all leave the chosen language in place. The dialog itself is created in the English
    /// layout and translated in the constructor when the application language is Chinese.
    /// The weights are edited in a copy and written back to the main window on OK only, so Cancel really cancels them.
    /// </summary>
    public partial class Settings : Form
    {
        private bool English = true;

        /// <summary>
        /// The names of the main list, in display order. The dialog only reads them; it never changes the main list.
        /// </summary>
        private readonly List<string> _names = new List<string>();

        /// <summary>
        /// The copy of the weights that is edited here. OK copies it into the store of the main window.
        /// </summary>
        private readonly WeightStore _working = new WeightStore();

        /// <summary>
        /// The store that belongs to the main window. It is kept so that OK can copy the edited values into it.
        /// </summary>
        private WeightStore _target;


        /// <summary>
        /// The chosen language. The main window copies it after ShowDialog returns, because a switch made in the dialog
        /// can be undone from the dialog's own CancelButton.
        /// </summary>
        public bool IsEnglish
        {
            get { return English; }
        }


        public Settings(bool english, IList<string> names, WeightStore weights)
        {
            InitializeComponent();
            English = english;

            _target = weights;
            _names.AddRange(names);
            foreach (string name in _names)
            {
                _working.SetWeight(name, weights.GetWeight(name));
            }


            radChinese.Checked = !english;
            radEnglish.Checked = english;
            RebuildGrid();
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            if (English)
            {
                Text = "Settings";
                grpLanguage.Text = "Language";
                radChinese.Text = "中文";
                radEnglish.Text = "English";
                grpWeights.Text = "Weights";
                colName.Text = "Name";
                colPercent.Text = "Chance";
                colWeight.Text = "Weight";
                btnWeightMinus.Text = "- 1";
                btnWeightPlus.Text = "+ 1";
                btnWeightReset.Text = "Reset to 1";
                grpHelp.Text = "Help";
                labHelpTips.Text = "The help page is opened in your browser and follows the language selected above.";
                btnOpenHelp.Text = "Open help";
                btnOK.Text = "OK";
                btnCancel.Text = "Cancel";
            }
            else
            {
                Text = "设置";
                grpLanguage.Text = "语言";
                radChinese.Text = "中文";
                radEnglish.Text = "English";
                grpWeights.Text = "权重（被抽到的概率）";
                colName.Text = "姓名";
                colPercent.Text = "概率";
                colWeight.Text = "权重";
                btnWeightMinus.Text = "-1";
                btnWeightPlus.Text = "+1";
                btnWeightReset.Text = "重置为 1";
                grpHelp.Text = "帮助";
                labHelpTips.Text = "帮助页面将在浏览器中打开，语言与上方所选一致。";
                btnOpenHelp.Text = "打开帮助";
                btnOK.Text = "确定";
                btnCancel.Text = "取消";
            }

            UpdateSummary();
        }

        /// <summary>
        /// Rebuilds the whole list from the names and the working weights. Rebuilding is cheap for a class list and keeps
        /// the display order identical to the main list, which a virtual-mode or sorting approach would not.
        /// </summary>
        private void RebuildGrid()
        {
            listWeights.BeginUpdate();
            listWeights.Items.Clear();
            foreach (string name in _names)
            {
                ListViewItem item = new ListViewItem(name);
                item.SubItems.Add(WeightStore.FormatPercent(_working.PercentOf(name, _names)));
                item.SubItems.Add(WeightStore.FormatWeight(_working.GetWeight(name)).Replace(".0", string.Empty));
                listWeights.Items.Add(item);
            }

            listWeights.EndUpdate();
            UpdateSummary();
        }

        /// <summary>
        /// Refreshes only the percentage column. A weight change changes the percentage of every row, not just one, so
        /// the whole column is rewritten after every -1 / +1.
        /// </summary>
        private void RefreshPercentages()
        {
            for (int i = 0; i < listWeights.Items.Count; i++)
            {
                string name = listWeights.Items[i].Text;
                listWeights.Items[i].SubItems[1].Text = WeightStore.FormatPercent(_working.PercentOf(name, _names));
                listWeights.Items[i].SubItems[2].Text = WeightStore.FormatWeight(_working.GetWeight(name)).Replace(".0", string.Empty);
            }

            UpdateSummary();
        }

        private void UpdateSummary()
        {
            long total = _working.TotalWeight(_names);
            if (English)
            {
                labWeightSummary.Text = _names.Count + " name(s), total weight " + total
                    + ". Weight 0 means the name cannot be picked; weight 2 means twice the chance of weight 1.";
            }
            else
            {
                labWeightSummary.Text = "共 " + _names.Count + " 人，权重合计 " + total
                    + "。权重 0 表示不会被抽到；权重 2 的被抽中概率是权重 1 的两倍。";
            }
        }

        /// <summary>
        /// The names selected in the list. Only the first column is used, so a click on any cell of a row works.
        /// </summary>
        private List<string> SelectedNames()
        {
            List<string> result = new List<string>();
            foreach (ListViewItem item in listWeights.SelectedItems)
            {
                result.Add(item.Text);
            }

            return result;
        }

        /// <summary>
        /// Shows a hint when nothing is selected. The buttons stay enabled, which is friendlier than greying them out
        /// after every selection change.
        /// </summary>
        private bool RequireSelection()
        {
            if (listWeights.SelectedItems.Count > 0)
            {
                return true;
            }

            MessageBox.Show(English
                ? "Select one or more names in the list first."
                : "请先在列表中选中一个或多个姓名。",
                English ? "Weights" : "权重", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private void ChangeSelected(int delta, bool setToFixedValue)
        {
            if (!RequireSelection())
            {
                return;
            }

            foreach (ListViewItem item in listWeights.SelectedItems)
            {
                int current = _working.GetWeight(item.Text);
                int next = setToFixedValue ? delta : current + delta;
                _working.SetWeight(item.Text, next);
            }

            RefreshPercentages();
        }

        private void btnWeightMinus_Click(object sender, EventArgs e)
        {
            ChangeSelected(-1, false);
        }

        private void btnWeightPlus_Click(object sender, EventArgs e)
        {
            ChangeSelected(1, false);
        }

        private void btnWeightReset_Click(object sender, EventArgs e)
        {
            ChangeSelected(1, true);
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            // Only now are the edited weights copied to the main window, so Cancel keeps the old weights.
            if (_target != null)
            {
                foreach (string name in _names)
                {
                    _target.SetWeight(name, _working.GetWeight(name));
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // The language is applied immediately, so it is handed back even when the dialog is closed with Cancel.
            // Weights are not handed back here.
            DialogResult = DialogResult.Cancel;
        }

        private void radChinese_CheckedChanged(object sender, EventArgs e)
        {
            if (radChinese.Checked)
            {
                English = false;
                ApplyLanguage();
            }
        }

        private void radEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (radEnglish.Checked)
            {
                English = true;
                ApplyLanguage();
            }
        }

        private void btnOpenHelp_Click(object sender, EventArgs e)
        {
            string helpFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Help-" + (English ? "EN" : "ZH") + ".html");
            if (File.Exists(helpFile))
            {
                Process.Start(helpFile);
            }
            else
            {
                MessageBox.Show((English ? "The help file was not found:\n" : "找不到帮助文件：\n") + helpFile,
                    English ? "Help" : "帮助", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
