//version:v1.0
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace 随机点名系统
{
    /// <summary>
    /// Turns a name-list text file into lines of text.
    /// The encoding is detected automatically: a BOM wins (UTF-32 / UTF-8 / UTF-16), otherwise the bytes are tried
    /// as strict UTF-8 first and fall back to the system default encoding (GBK on Chinese systems).
    /// </summary>
    internal static class LineSplitter
    {
        /// <summary>
        /// True when the last call decoded "UTF-8 looking" bytes that contained no real text, so the bytes were almost
        /// certainly saved in the system default encoding. The caller can then ask the user to check the garbled result.
        /// </summary>
        internal static bool LastFallbackMayBeWrong { get; private set; }

        /// <summary>
        /// Reads all lines of a text file and detects the encoding. Errors (file missing, locked, no permission) are
        /// thrown as IOException / UnauthorizedAccessException and handled by the caller.
        /// </summary>
        internal static string[] ReadLines(string path)
        {
            LastFallbackMayBeWrong = false;

            byte[] bytes = File.ReadAllBytes(path);

            // The order matters: the first two bytes of a UTF-32LE BOM are the same as UTF-16LE, so UTF-32 must be checked first.
            Encoding encoding;
            int skip = 0; // How many bytes the BOM itself occupies; it must be skipped when decoding, or the first name keeps an invisible character.
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
            else if (bytes.Length == 0)
            {
                return new string[0];
            }
            else
            {
                // Without a BOM the encoding cannot be determined with certainty, so try UTF-8 first:
                // a Notepad file is UTF-8 by default, and decoding it as GBK would turn every name into garbage.
                // Only when strict UTF-8 decoding really fails are the bytes decoded with the system default encoding.
                return DecodeUtf8WithFallback(bytes);
            }

            string text = encoding.GetString(bytes, skip, bytes.Length - skip);

            // Split on both \r and \n so Windows (\r\n), Linux (\n) and old Mac (\r) line endings are all handled.
            return text.Split(new char[] { '\r', '\n' });
        }

        /// <summary>
        /// Decodes bytes without a BOM. The UTF-8 decoder with throwOnInvalidBytes = true rejects every byte sequence
        /// that is not valid UTF-8 (truncated characters, invalid continuation bytes, ...), so a GBK or GB18030 file does
        /// not silently turn into mojibake: an exception is thrown instead and the file is decoded with the system default
        /// encoding (GBK on Chinese systems). A file that ends in the middle of a character (typically a download that was
        /// cut off) keeps every name before that character, and the user is asked to check the result, instead of losing
        /// the whole file or turning it into mojibake.
        /// </summary>
        private static string[] DecodeUtf8WithFallback(byte[] bytes)
        {
            try
            {
                string text = new UTF8Encoding(false, true).GetString(bytes);
                if (HasContent(text))
                {
                    return text.Split(new char[] { '\r', '\n' });
                }

                // The bytes are valid UTF-8 but contain no letters or digits. That happens with some GBK byte pairs,
                // so retry with the system default encoding and tell the user the result may need checking.
                LastFallbackMayBeWrong = true;
            }
            catch (DecoderFallbackException)
            {
                // The strict decoder has no CharsUnknown property below .NET Framework 4, so the position of the bad
                // bytes cannot be read from the exception; IsTruncatedUtf8 decides from the end of the file instead.
                if (IsTruncatedUtf8(bytes))
                {
                    LastFallbackMayBeWrong = true;
                    return DecodeUtf8Prefix(bytes, bytes.Length - 1);
                }
            }

            // Not UTF-8; decode with the system default encoding below.
            return Encoding.Default.GetString(bytes).Split(new char[] { '\r', '\n' });
        }

        /// <summary>
        /// True when the bytes are UTF-8 except for one sequence at the very end that the file cuts off, i.e. a download
        /// that was interrupted. Only then is the decoded prefix worth keeping; a file with invalid bytes in the middle (a
        /// GBK file) is decoded as a whole with the system default encoding, so its names do not end up half UTF-8, half GBK.
        /// </summary>
        private static bool IsTruncatedUtf8(byte[] bytes)
        {
            UTF8Encoding strictUtf8 = new UTF8Encoding(false, true);
            for (int cut = 1; cut <= 3 && cut < bytes.Length; cut++)
            {
                byte lead = bytes[bytes.Length - cut];
                int expected;
                if (lead >= 0xF0 && lead <= 0xF7)
                {
                    expected = 4;
                }
                else if (lead >= 0xE0 && lead <= 0xEF)
                {
                    expected = 3;
                }
                else if (lead >= 0xC0 && lead <= 0xDF)
                {
                    expected = 2;
                }
                else
                {
                    continue; // The byte before the cut is a complete character or an ASCII byte, so nothing is missing.
                }

                if (cut >= expected)
                {
                    continue; // The character is complete: this lead byte is not the one that is cut off.
                }

                // Every byte after the lead byte must be a continuation byte, or the sequence is invalid rather than cut off.
                bool continuationOnly = true;
                for (int i = bytes.Length - cut + 1; i < bytes.Length; i++)
                {
                    if ((bytes[i] & 0xC0) != 0x80)
                    {
                        continuationOnly = false;
                        break;
                    }
                }

                if (!continuationOnly)
                {
                    continue;
                }

                try
                {
                    strictUtf8.GetString(bytes, 0, bytes.Length - cut);
                    return true;
                }
                catch (DecoderFallbackException)
                {
                    // The bytes before this sequence are not valid UTF-8 either, so this is a whole-file encoding problem.
                }

                return false;
            }

            return false;
        }

        /// <summary>
        /// Decodes the beginning of the bytes up to the character that is cut off. The binary search finds the largest
        /// valid prefix. Decoding a prefix as one call is safe: when two chunks are decoded independently, an incomplete
        /// sequence at the end of the first chunk is an error, so no character is produced from bytes on both sides of
        /// the split. The result is therefore exactly what a single call over the whole prefix would return.
        /// </summary>
        private static string[] DecodeUtf8Prefix(byte[] bytes, int end)
        {
            UTF8Encoding strictUtf8 = new UTF8Encoding(false, true);
            int low = 0;
            int high = end;
            string best = string.Empty;
            while (low <= high)
            {
                int middle = low + (high - low) / 2;
                try
                {
                    best = strictUtf8.GetString(bytes, 0, middle);
                    low = middle + 1;
                }
                catch (DecoderFallbackException)
                {
                    high = middle - 1;
                }
            }

            return best.Split(new char[] { '\r', '\n' });
        }

        /// <summary>
        /// Splits a piece of text into names. Separators are commas (both ASCII and full-width), spaces, tabs and
        /// line breaks. Empty entries are dropped; a name repeated inside the same text is kept only once.
        /// </summary>
        internal static string[] SplitNames(string text)
        {
            string[] parts = text.Split(new char[] { ' ', ',', '，', '\u3000', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            System.Collections.Generic.List<string> names = new System.Collections.Generic.List<string>();
            for (int i = 0; i < parts.Length; i++)
            {
                string name = parts[i].Trim();
                if (name.Length > 0 && !names.Contains(name))
                {
                    names.Add(name);
                }
            }

            return names.ToArray();
        }

        /// <summary>
        /// Reads one line of a name file as a name plus an optional weight ("张三 3", "张三<TAB>3" or "张三,3").
        /// A line without a trailing number (or whose number is outside 0..<see cref="WeightStore.MaxWeight"/>, such as an
        /// unreasonable count) is treated as a name with weight -1, meaning "no weight in the file".
        /// Returns false for blank lines. The weight is never left inside the name, so the digits in "1234" on their own
        /// line stay a name and do not become a weight.
        /// </summary>
        internal static bool ParseEntry(string line, out string name, out int weight)
        {
            name = null;
            weight = -1;
            if (line == null)
            {
                return false;
            }

            string trimmed = line.Trim();
            if (trimmed.Length == 0)
            {
                return false;
            }

            // Only the last token can be the weight; the name may contain spaces of its own.
            int split = trimmed.LastIndexOfAny(new char[] { ' ', '\t', ',', '，', '\u3000' });
            if (split >= 0 && split < trimmed.Length - 1)
            {
                string last = trimmed.Substring(split + 1);
                int parsed;
                if (IsDigits(last) && int.TryParse(last, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsed)
                    && parsed >= 0 && parsed <= WeightStore.MaxWeight)
                {
                    string head = trimmed.Substring(0, split).TrimEnd();
                    if (head.Length > 0)
                    {
                        name = head;
                        weight = parsed;
                        return true;
                    }
                }
            }

            name = trimmed;
            return true;
        }

        /// <summary>
        /// True when every character is an ASCII digit. int.TryParse alone would also accept a leading "+" or white space,
        /// which must stay part of the name instead.
        /// </summary>
        private static bool IsDigits(string text)
        {
            if (text.Length == 0)
            {
                return false;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '0' || text[i] > '9')
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// True when at least one line contains more than white space. Either the file really is empty, or the encoding
        /// guess was wrong and the decoded text is garbage; the caller decides how to warn in each case.
        /// </summary>
        internal static bool HasContent(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().Length > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasContent(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsWhiteSpace(text[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
