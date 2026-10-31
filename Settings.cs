using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 随机点名系统
{
    /// <summary>
    /// The "Settings / 设置" dialog: the application language and the help page live here, so the main window only needs
    /// one entry point instead of one button per option.
    /// The language is applied as soon as the user switches it (ApplyLanguage is called immediately), therefore OK,
    /// Cancel, Esc and the X button all leave the chosen language in place. The dialog itself is created in the English
    /// layout and translated in the constructor when the application language is Chinese.
    /// </summary>
    public partial class Settings : Form
    {
        private bool English = true;

        /// <summary>
        /// The chosen language. The main window copies it after ShowDialog returns, because a switch made in the dialog
        /// can be undone from the dialog's own CancelButton.
        /// </summary>
        public bool IsEnglish
        {
            get { return English; }
        }

        public Settings(bool english)
        {
            InitializeComponent();
            English = english;
            radChinese.Checked = !english;
            radEnglish.Checked = english;
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
                grpHelp.Text = "帮助";
                labHelpTips.Text = "帮助页面将在浏览器中打开，语言与上方所选一致。";
                btnOpenHelp.Text = "打开帮助";
                btnOK.Text = "确定";
                btnCancel.Text = "取消";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // The language has already been applied; OK only tells the main window which language to use from now on.
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // The language is applied immediately, so it is handed back even when the dialog is closed with Cancel.
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
