using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace 随机点名系统
{
    public partial class Form1 : Form
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

        public Form1()
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
            }
            else
            {
                Text = "随机点名系统";
                btnAdd.Text = "手动添加";
                btnInput.Text = "导入'.txt'文档";
                btnRandomName.Text = "点名";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // When the using block ends, addForm.Dispose() is called automatically to release the form's resources, even if an exception is thrown.
            using (AddName addForm = new AddName())
            {
                addForm.English = this.English;
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Copy with Clear + AddRange instead of assigning _names = addForm.Names to take over the reference:
                    // if we took the reference directly, the next entry in the child form would modify the same List and the two forms would share state.
                    _names.Clear();
                    _names.AddRange(addForm.Names);
                }
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                // Every import replaces the previous list; otherwise the list keeps growing and duplicated people are more likely to be picked.
                _names.Clear();
                foreach (string line in ReadLines(openFileDialog.FileName))
                {
                    string name = line.Trim();
                    if (name.Length > 0)
                    {
                        _names.Add(name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(English ? "Import failed: " + ex.Message : "导入失败：" + ex.Message);
                return;
            }

            MessageBox.Show(English
                ? "Imported " + _names.Count + " name(s)."
                : "导入成功，共 " + _names.Count + " 人。");
        }

        /// <summary>
        /// Reads all lines of a text file and detects UTF-8, Unicode (with BOM) and GBK encodings automatically.
        /// The File.ReadAllBytes + Split approach is used because File.ReadAllLines does not exist in .NET 2.0.
        /// </summary>
        private static string[] ReadLines(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);

            // Check whether the file starts with a BOM (byte order mark, a few special bytes that identify the encoding).
            // The order matters: the first two bytes of a UTF-32LE BOM are the same as UTF-16LE, so UTF-32 must be checked first.
            Encoding encoding;
            int skip = 0; // How many bytes the BOM itself occupies; it must be skipped when decoding, or the first name will have an invisible character in front of it.
            if (bytes.Length >= 4 && bytes[0] == 0xFF && bytes[1] == 0xFE && bytes[2] == 0x00 && bytes[3] == 0x00)
            {
                encoding = new UTF32Encoding(false, true);
                skip = 4;
            }
            else if (bytes.Length >= 4 && bytes[0] == 0x00 && bytes[1] == 0x00 && bytes[2] == 0xFE && bytes[3] == 0xFF)
            {
                encoding = new UTF32Encoding(true, true);
                skip = 4;
            }
            else if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
            {
                encoding = Encoding.UTF8;
                skip = 3;
            }
            else if (bytes.Length >= 2 && bytes[0] == 0xFF && bytes[1] == 0xFE)
            {
                // UTF-32 has already been ruled out above, so the remaining FF FE can only be UTF-16LE.
                encoding = Encoding.Unicode;
                skip = 2;
            }
            else if (bytes.Length >= 2 && bytes[0] == 0xFE && bytes[1] == 0xFF)
            {
                encoding = Encoding.BigEndianUnicode;
                skip = 2;
            }
            else if (IsValidUtf8(bytes))
            {
                // Without a BOM the encoding cannot be determined with certainty, so try UTF-8 first:
                // as soon as an invalid byte sequence appears, give up and use the system default encoding (GBK on Chinese systems).
                encoding = Encoding.UTF8;
            }
            else
            {
                // Encoding.Default must not be used to decode every file: a UTF-8 file saved by Notepad
                // would be decoded as GBK on Chinese systems and become garbled, so the UTF-8 branch above must come first.
                encoding = Encoding.Default;
            }

            string text = encoding.GetString(bytes, skip, bytes.Length - skip);
            // Split on both \r and \n so Windows (\r\n), Linux (\n) and old Mac (\r) line endings are all handled correctly.
            return text.Split(new char[] { '\r', '\n' });
        }

        /// <summary>
        /// Checks whether a byte sequence is valid UTF-8. Every UTF-8 character is encoded in 1 to 4 bytes;
        /// the high bits of the first byte determine how many bytes follow (see the 0xF0 / 0xE0 / 0xC0 checks below),
        /// and every following byte must start with 10 (i.e. fall in the range 0x80~0xBF).
        /// </summary>
        private static bool IsValidUtf8(byte[] bytes)
        {
            int i = 0;
            while (i < bytes.Length)
            {
                int length; // How many bytes the current character occupies in total
                if (bytes[i] < 0x80)
                {
                    length = 1;
                }
                else if ((bytes[i] & 0xF0) == 0xF0)
                {
                    length = 4;
                }
                else if ((bytes[i] & 0xE0) == 0xE0)
                {
                    length = 3;
                }
                else if ((bytes[i] & 0xC0) == 0xC0)
                {
                    length = 2;
                }
                else
                {
                    return false; // A "continuation byte" such as 10xxxxxx appeared at the start, so this is not UTF-8
                }

                if (i + length > bytes.Length)
                {
                    return false; // The file ends in the middle of a character, i.e. it was truncated
                }

                for (int j = 1; j < length; j++)
                {
                    if ((bytes[i + j] & 0xC0) != 0x80)
                    {
                        return false;
                    }
                }

                i += length;
            }

            return true;
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
