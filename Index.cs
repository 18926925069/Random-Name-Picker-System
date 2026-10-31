using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 随机点名系统
{
    public partial class Index : Form
    {
        /// <summary>
        /// The name list. Duplicate names are allowed: a name that appears twice has twice the chance of being picked.
        /// </summary>
        private readonly List<string> _names = new List<string>();

        private bool English = true;

        /// <summary>
        /// Random number generator. Only one instance is created and reused for the whole program; see the comment in btnRandomName_Click.
        /// </summary>
        private readonly Random _random = new Random();

        public Index()
        {
            InitializeComponent();
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            if (English)
            {
                Text = "Random Roll Call";
                btnAdd.Text = "Add manually";
                btnInput.Text = "Import '.txt'";
                btnRandomName.Text = "Pick";
                btnSettings.Text = "Settings";
            }
            else
            {
                Text = "随机点名系统";
                btnAdd.Text = "手动添加";
                btnInput.Text = "导入'.txt'文档";
                btnRandomName.Text = "点名";
                btnSettings.Text = "设置";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // When the using block ends, addForm.Dispose() is called automatically to release the form's resources, even if an exception is thrown.
            using (AddName addForm = new AddName(English))
            {
                // The current names are passed in so the dialog can refuse names that are already in the list.
                addForm.SetExistingNames(_names);
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // "Add manually" only appends: the names entered in the dialog are added to the end of the current
                    // list, and closing the dialog with Cancel (or the X button) never touches the list. Importing a file
                    // is the action that replaces the whole list.
                    _names.AddRange(addForm.Names);
                }
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string lang = English ? "EN" : "ZH";
            // String concatenation instead of $"..." interpolation: the project targets .NET 2.0 and has to stay buildable
            // with the C# version of that toolchain, where interpolated strings do not exist yet.
            string defaultFile = Path.Combine(appDir, "Name-" + lang + ".txt");

            if (!File.Exists(defaultFile))
                defaultFile = Path.Combine(appDir, "Name-ZH.txt");

            if (File.Exists(defaultFile))
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(defaultFile);
                openFileDialog.FileName = Path.GetFileName(defaultFile);
            }
            else
            {
                openFileDialog.InitialDirectory = appDir;
            }

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] lines;
            List<string> importedNames = new List<string>();
            try
            {
                lines = ReadLines(openFileDialog.FileName);

                foreach (string line in lines)
                {
                    string name = line.Trim();
                    if (name.Length > 0)
                    {
                        importedNames.Add(name);
                    }
                }
            }
            catch (Exception ex)
            {
                // Nothing has been changed yet: the current list is only replaced from here on, after the file has been
                // read completely. A failed import therefore keeps the names the user already had.
                MessageBox.Show(DescribeImportError(ex), English ? "Import failed" : "导入失败",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (importedNames.Count == 0)
            {
                MessageBox.Show(English
                    ? "No names were found in this file. The name list was not changed."
                    : "该文件中没有找到任何姓名，名单未作修改。",
                    English ? "Nothing imported" : "没有可导入的内容",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //We append to the list.
            _names.AddRange(importedNames);

            string message = English
                ? "Imported " + _names.Count + " name(s)."
                : "导入成功，共 " + _names.Count + " 人。";
            if (LineSplitter.LastFallbackMayBeWrong)
            {
                message += English
                    ? "\n\nWarning: the file's encoding could not be detected with certainty, so the names may look garbled. Save the file as UTF-8 and import it again if so."
                    : "\n\n警告：无法确定文件编码，姓名可能出现乱码。如发现乱码，请将文件另存为 UTF-8 后再导入。";
            }

            MessageBox.Show(message, English ? "Import" : "导入", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Turns an exception thrown while reading the name file into a message the user can act on. The raw exception
        /// text of the framework is only used as a last resort, because messages such as "The process cannot access the
        /// file because it is being used by another process" mention technical details that do not help classroom use.
        /// </summary>
        private string DescribeImportError(Exception ex)
        {
            if (ex is FileNotFoundException)
            {
                return English ? "The file was not found. It may have been moved or deleted." : "找不到文件，它可能已被移动或删除。";
            }

            if (ex is DirectoryNotFoundException)
            {
                return English ? "The folder was not found. It may have been moved or deleted." : "找不到文件所在的文件夹，它可能已被移动或删除。";
            }

            if (ex is UnauthorizedAccessException)
            {
                return English ? "Access to the file was denied. Choose a file you are allowed to read." : "没有读取该文件的权限，请选择一个你有权限读取的文件。";
            }

            if (ex is IOException)
            {
                return English
                    ? "The file could not be read. It may be open in another program; close it and try again."
                    : "无法读取文件。该文件可能正被其他程序打开，请关闭后重试。";
            }

            return (English ? "Import failed: " : "导入失败：") + ex.Message;
        }

        /// <summary>
        /// Reads all lines of a text file and detects UTF-8, Unicode (with BOM) and GBK encodings. The decoding logic lives
        /// in LineSplitter so it can be reviewed and tested on its own.
        /// </summary>
        private static string[] ReadLines(string path)
        {
            return LineSplitter.ReadLines(path);
        }

        private void btnRandomName_Click(object sender, EventArgs e)
        {
            int count = _names.Count;
            if (count == 0)
            {
                MessageBox.Show(English ? "No names to pick from." : "没有可点名的人员");
                return;
            }

            // The random number generator is created only once (see the _random field). If new Random() were called every time,
            // the default seed comes from the system clock and clicking several times in quick succession would produce identical "random" results.
            string randomName = _names[_random.Next(count)];

            listViewRandomName.Items.Clear();
            listViewRandomName.Items.Add(randomName);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // The language switch and the help page both live in the Settings dialog now, so this window only needs one
            // entry point. ShowDialog returns after the language has already been applied inside the dialog, so the main
            // window refreshes its own texts unconditionally afterwards; that way closing with Cancel keeps the chosen
            // language instead of contradicting what the user already sees.
            using (Settings settingsForm = new Settings(English))
            {
                settingsForm.ShowDialog(this);
                English = settingsForm.IsEnglish;
                ApplyLanguage();
            }
        }
    }
}
