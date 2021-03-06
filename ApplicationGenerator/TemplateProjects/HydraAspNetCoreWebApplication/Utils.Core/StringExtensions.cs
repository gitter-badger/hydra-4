﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security;
using System.Security.Authentication;

namespace Utils
{
    public enum QuoteTextType
    {
        None,
        SingleQuote,
        DoubleQuote
    }

    public static class StringExtensions
    {
        public const string REGEX_IDENTIFIER = @"^(?:((?!\d)\w+(?:\.(?!\d)\w+)*)\.)?((?!\d)\w+)$";
        public const string REGEX_IDENTIFIER_MIDSTRING = @"(?:((?!\d)\w+(?:\.(?!\d)\w+)*)\.)?((?!\d)\w+)";
        public const string REGEX_IDENTIFIER_CHARS = @"[\w\d]";
        public const string REGEX_GUID = "(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$";
        public const string REGEX_GUID_MIDSTRING = "(?im)[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?";

        private static string BytOrderMarkUtf8;
        private static List<string> insignificantWordList;
        private static List<string> insignificantWordListLevel1;

        static StringExtensions()
        {
            BytOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
        }

        public static bool IsVowel(this char c)
        {
            return "aeiouAEIOU".IndexOf(c) >= 0;
        }

        public static bool IsConsonant(this char c)
        {
            return !c.IsVowel();
        }

        public static string[] Split(this string str, char ch, StringSplitOptions options)
        {
            return str.Split(new char[] { ch }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool IsNumeric(this string input)
        {
            int number;

            return int.TryParse(input, out number);
        }

        public static string Replace(this string value, Dictionary<string, string> replacments)
        {
            foreach (var replacement in replacments)
            {
                value = value.RegexReplace(@"{\s*?" + replacement.Key + @"\s*?}", replacement.Value);
            }

            return value;
        }

        public static String[] SplitCombine(this string str, params char[] separator)
        {
            var parts = str.Split(separator);
            var list = new List<string>();
            var lastPart = string.Empty;
            var separatorText = string.Join(string.Empty, separator);

            foreach (var part in parts)
            {
                var combinedPart = lastPart.AppendIfMissing(separatorText) + part;

                list.Add(combinedPart);
                lastPart = combinedPart;
            }

            return list.ToArray();
        }

        public static bool IsInsignificantWord(this string str, IEnumerable<string> exclusions = null)
        {
            if (exclusions != null)
            {
                return insignificantWordList.Where(w => !exclusions.Any(e => w == e)).Contains(str);
            }
            else
            {
                return insignificantWordList.Contains(str);
            }
        }

        public static bool IsInsignificantWord(this string str, int level, IEnumerable<string> exclusions = null)
        {
            if (exclusions != null)
            {
                return insignificantWordListLevel1.Where(w => !exclusions.Any(e => w == e)).Contains(str);
            }
            else
            {
                return insignificantWordListLevel1.Contains(str);
            }
        }

        public static SecureString ToSecureString(this string str)
        {
            var secureString = new SecureString();

            foreach (var ch in str)
            {
                secureString.AppendChar(ch);
            }

            return secureString;
        }

        public static bool Match(this Regex regex, string input, Action<Match> a)
        {
            if (regex.IsMatch(input))
            {
                a(regex.Match(input));

                return true;
            }

            return false;
        }

#if INCLUDE_FARE
        public static string GetRandomText(string pattern)
        {
            return new Regex(pattern).GetRandomText();
        }

        public static string GetRandomText(this Regex regex)
        {
            var xeger = new Xeger(regex.ToString());
            var generatedString = xeger.Generate();

            return generatedString;
        }
#endif
        public static string GetGroupValue(this Match match, string groupName)
        {
            return match.Groups[groupName].Value;
        }

        public static string Replace(this Match match, string input, string replacement)
        {
            var index = match.Index;
            var length = input.Length;

            return input.Left(index) + replacement + input.Right(length - (index + match.Length));
        }

        public static string Replace(this Match match, string input, string replacement, int indexIncrement)
        {
            var index = match.Index + indexIncrement;
            var length = input.Length;

            return input.Left(index) + replacement + input.Right(length - (index + match.Length));
        }

        public static string Replace(this Group group, string input, string replacement)
        {
            var index = group.Index;
            var length = input.Length;

            return input.Left(index) + replacement + input.Right(length - (index + group.Length));
        }

        public static string GetGroupValue(this Regex regex, string input, string groupName)
        {
            var match = regex.Match(input);
            var value = match.Groups[groupName].Value;

            return value;
        }

        public static bool IsCrOrLf(this char ch)
        {
            return ch == '\r' || ch == '\n';
        }

        public static bool IsDigit(this char ch)
        {
            return ch >= '0' && ch <= '9';
        }

        public static bool IsCrLfString(this string str)
        {
            return str == "\r" || str == "\n" || str == "\r\n";
        }

        public static string SpaceBlock(this string str, int spaceCount)
        {
            str = str.GetLines().ToMultiLineList(l => l.Prepend(" ".Repeat(spaceCount)));

            return str;
        }

        public static string TrimBlankLines(this string str)
        {
            str = str.GetLines().Where(l => l.Trim().Length > 0).ToMultiLineList();

            return str;
        }

        public static string GetLastLine(this StringBuilder builder)
        {
            try
            {
                var text = builder.ToString();
                var lineCount = text.Trim().GetLineCount();

                if (lineCount > 0)
                {
                    return text.GetLines().ElementAt(lineCount - 1);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Format("Error with GetLastLine: '{0}'", ex.ToString());
            }
        }

        public static int GetLineCount(this StringBuilder builder)
        {
            return builder.ToString().GetLineCount();
        }

        public static int GetLineCount(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return 0;
            }
            else
            {
                return str.Count(c => c == '\n') + 1;
            }
        }
        public static string SpaceIndentLine(this string str, int count)
        {
            return new string(' ', count) + str;
        }

        public static string SpaceIndentLines(this string str, int count, int lineSkip = 0)
        {
            var builder = new StringBuilder();
            var lines = str.GetLines();
            var lineCount = lines.Count();
            var x = 0;

            foreach (var line in lines)
            {
                if (x >= lineSkip)
                {
                    var newLine = new string(' ', count) + line;

                    if (x == lineCount - 1)
                    {
                        builder.Append(newLine);
                    }
                    else
                    {
                        builder.AppendLine(newLine);
                    }
                }
                else
                {
                    if (x == lineCount - 1)
                    {
                        builder.Append(line);
                    }
                    else
                    {
                        builder.AppendLine(line);
                    }
                }

                x++;
            }

            return builder.ToString();
        }

        public static int GetCountOfCharAtStart(this string str, char ch)
        {
            var charStr = ch.ToString();

            return str.RegexGet($"^{ charStr }*(?=[^{ charStr }])").Length;
        }

        public static string RemoveLines(this string str, params int[] line)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }
            else
            {
                var builder = new StringBuilder();

                using (var stringReader = new StringReader(str))
                {
                    using (var stringWriter = new StringWriter(builder))
                    {
                        string strLine;
                        var x = 1;

                        while ((strLine = stringReader.ReadLine()) != null)
                        {
                            if (!line.Contains(x))
                            {
                                stringWriter.WriteLine(strLine);
                            }

                            x++;
                        }
                    }
                }

                return builder.ToString();
            }
        }

        public static string RemoveLinesBetween(this string str, int startLine, int endLine)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }
            else
            {
                var builder = new StringBuilder();

                using (var stringReader = new StringReader(str))
                {
                    using (var stringWriter = new StringWriter(builder))
                    {
                        string strLine;
                        var x = 1;

                        while ((strLine = stringReader.ReadLine()) != null)
                        {
                            if (x < startLine || x > endLine)
                            {
                                stringWriter.WriteLine(strLine);
                            }

                            x++;
                        }
                    }
                }

                return builder.ToString();
            }
        }

        public static string GetLinesBetween(this string str, int startLine, int endLine)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }
            else
            {
                var builder = new StringBuilder();

                using (var stringReader = new StringReader(str))
                {
                    using (var stringWriter = new StringWriter(builder))
                    {
                        string strLine;
                        var x = 1;

                        while ((strLine = stringReader.ReadLine()) != null)
                        {
                            if (x >= startLine && x <= endLine)
                            {
                                stringWriter.WriteLine(strLine);
                            }

                            x++;
                        }
                    }
                }

                return builder.ToString();
            }
        }

        public static string GetLastLine(this string str)
        {
            var lineIndex = str.GetLineCount();

            return str.GetLine(lineIndex);
        }

        public static string GetLastNonCrlfLine(this string str)
        {
            var lineIndex = str.GetLineCount() - 1;

            return str.GetLine(lineIndex);
        }

        public static string GetLine(this string str, int line)
        {
            return str.GetLinesBetween(line, line);
        }

        public static string GetLinesStartingAt(this StringBuilder builder, int startLine)
        {
            return builder.ToString().GetLinesStartingAt(startLine);
        }

        public static string GetLinesStartingAt(this string str, int startLine)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }
            else
            {
                var builder = new StringBuilder();

                using (var stringReader = new StringReader(str))
                {
                    using (var stringWriter = new StringWriter(builder))
                    {
                        string strLine;
                        var x = 1;

                        while ((strLine = stringReader.ReadLine()) != null)
                        {
                            if (x >= startLine)
                            {
                                stringWriter.WriteLine(strLine);
                            }

                            x++;
                        }
                    }
                }

                return builder.ToString();
            }
        }

        public static int GetStartingTabCount(this string str)
        {
            var tabs = str.TakeWhile(c => c == '\t');

            return tabs.Count();
        }

        public static IEnumerable<string> GetLines(this StringBuilder builder, bool removeEmpty = false)
        {
            return builder.ToString().GetLines(removeEmpty);
        }

        public static string Join(this IEnumerable<string> lines)
        {
            return string.Join("\r\n", lines);
        }

        public static IEnumerable<string> GetLines(this string str, bool removeEmpty = false)
        {
            if (str.IsNullOrEmpty())
            {
                yield break;
            }
            else
            {
                using (var stringReader = new StringReader(str))
                {
                    string line;

                    while ((line = stringReader.ReadLine()) != null)
                    {
                        if (removeEmpty)
                        {
                            if (!line.Trim().IsNullOrEmpty())
                            {
                                yield return line;
                            }
                        }
                        else
                        {
                            yield return line;
                        }
                    }
                }
            }
        }

        public static IEnumerable<KeyValuePair<int, string>> GetLinesWithLineNumbers(this string str, bool removeEmpty = false)
        {
            if (str.IsNullOrEmpty())
            {
                yield break;
            }
            else
            {
                var x = 1;

                using (var stringReader = new StringReader(str))
                {
                    string line;

                    while ((line = stringReader.ReadLine()) != null)
                    {
                        if (removeEmpty)
                        {
                            if (!line.Trim().IsNullOrEmpty())
                            {
                                yield return new KeyValuePair<int, string>(x, line);
                            }
                        }
                        else
                        {
                            yield return new KeyValuePair<int, string>(x, line);
                        }

                        x++;
                    }
                }
            }
        }

        public static int GetCharCount(this string str, char ch)
        {
            if (str.IsNullOrEmpty())
            {
                return 0;
            }
            else
            {
                return str.Count(c => c == ch);
            }
        }

        public static int GetLeadingCharCount(this string str, char ch)
        {
            if (str.IsNullOrEmpty())
            {
                return 0;
            }
            else
            {
                return str.TakeWhile(c => c == ch).Count();
            }
        }

        public static string EmptyToNull(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string AddCrlfIfNone(this string str)
        {
            if (str.RegexIsMatch(@"\n$"))
            {
                return str;
            }
            else
            {
                return str + "\r\n";
            }
        }

        public static string IndentLines(this string str, int tabCount = 1)
        {
            return string.Join("\r\n", str.GetLines().Select(l => "\t".Repeat(tabCount) + l));
        }

        public static bool IsNullWhiteSpaceOrEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(str);
        }

        public static string GetRandomString(int length)
        {
            var provider = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            string base64;

            provider.GetBytes(bytes);

            base64 = Convert.ToBase64String(bytes);

            return base64.RemoveEnd(base64.Length - length);
        }

        public static string AsDisplayText(this object obj, bool quoteText = false)
        {
            if (obj == null)
            {
                return "null";
            }
            else if (obj is bool)
            {
                return ((bool)obj) ? "true" : "false";
            }
            else if (obj is string && quoteText)
            {
                return "'" + ((string)obj) + "'";
            }
            else
            {
                return obj.ToString();
            }
        }

        public static string AsDisplayText(this object obj, QuoteTextType quoteTextType)
        {
            if (obj == null)
            {
                return "null";
            }
            else if (obj is bool)
            {
                return ((bool)obj) ? "true" : "false";
            }
            else if (obj is string && quoteTextType == QuoteTextType.SingleQuote)
            {
                return "'" + ((string)obj) + "'";
            }
            else if (obj is string && quoteTextType == QuoteTextType.DoubleQuote)
            {
                return "\"" + ((string)obj) + "\"";
            }
            else
            {
                return obj.ToString();
            }
        }

        public static string AsDisplayText(this string str)
        {
            if (str == null)
            {
                return "null";
            }
            else
            {
                return str;
            }
        }

        public static string AsDisplayText(this DateTime? dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
            {
                return "null";
            }
            else
            {
                return dateTime.ToString();
            }
        }

        public static string AsDisplayText(this byte[] bytes)
        {
            if (bytes == null)
            {
                return "null";
            }
            else
            {
                string hex = BitConverter.ToString(bytes).Replace("-", ", ").SurroundWith("{ ", " }");

                return hex;
            }
        }

        public static string ToHexString(this byte b, bool leadingIdentifier = false)
        {
            if (leadingIdentifier)
            {
                return string.Format("0x{0:x2}", b);
            }
            else
            {
                return string.Format("{0:x2}", b);
            }
        }

        public static string ToHexString(this IntPtr ptr, bool leadingIdentifier = false)
        {
            if (leadingIdentifier)
            {
                return string.Format("0x{0:x8}", ptr.ToInt64());
            }
            else
            {
                return string.Format("{0:x8}", ptr.ToInt64());
            }
        }

        public static string ToHexString(this UIntPtr ptr, bool leadingIdentifier = false)
        {
            if (leadingIdentifier)
            {
                return string.Format("0x{0:x8}", ptr.ToUInt64());
            }
            else
            {
                return string.Format("{0:x8}", ptr.ToUInt64());
            }
        }

        public static string ToHexString(this uint n, bool leadingIdentifier = false)
        { 
            if (leadingIdentifier)
            {
                return string.Format("0x{0:x8}", n);
            }
            else
            {
                return string.Format("{0:x8}", n);
            }
        }

        public static string ToHexString(this ulong n, bool leadingIdentifier = false)
        {
            if (leadingIdentifier)
            {
                return string.Format("0x{0:x8}", n);
            }
            else
            {
                return string.Format("{0:x8}", n);
            }
        }

        public static string ToHexString(this long n, bool leadingIdentifier = false)
        {
            if (leadingIdentifier)
            {
                return string.Format("0x{0:x16}", n);
            }
            else
            {
                return string.Format("{0:x16}", n);
            }
        }

        public static string ToHexString(this short n, bool leadingIdentifier = false)
        {
            if (leadingIdentifier)
            {
                return string.Format("0x{0:x2}", n);
            }
            else
            {
                return string.Format("{0:x2}", n);
            }
        }

        public static string ToHexString(this int n, bool leadingIdentifier = false)
        {
            if (leadingIdentifier)
            {
                return string.Format("0x{0:x8}", n);
            }
            else
            {
                return string.Format("{0:x8}", n);
            }
        }

        public static long FromHexString(this string text, bool leadingIdentifier = false)
        {
            if (leadingIdentifier)
            {
                return long.Parse(text, System.Globalization.NumberStyles.HexNumber | NumberStyles.AllowHexSpecifier);
            }
            else
            {
                return long.Parse(text, System.Globalization.NumberStyles.HexNumber);
            }
        }

        public static string AsDisplayText(this byte[] bytes, int limit)
        {
            if (bytes == null)
            {
                return "null";
            }
            else
            {
                string hex = BitConverter.ToString(bytes, 0, Math.Min(limit, bytes.Length)).Replace("-", ", ").SurroundWith("{ ", " }");

                return hex;
            }
        }

        public static string GetNullTermString(this Encoding encoding, byte[] bytes)
        {
            return encoding.GetString(bytes, 0, bytes.IndexOf<byte>(0));
        }

        public static void AppendLineFormat(this StringBuilder builder, string format, params object[] args)
        {
            builder.AppendFormat(format + "\r\n", args);
        }

        public static void RemoveEnd(this StringBuilder builder, int length)
        {
            builder.Remove(builder.Length - length, length); 
        }

        public static void AppendLineFormatSpaceIndent(this StringBuilder builder, int count, string format, params object[] args)
        {
            builder.Append(" ".Repeat(count));
            builder.AppendFormat(format + "\r\n", args);
        }

        public static void AppendLineFormatTabIndent(this StringBuilder builder, int count, string format, params object[] args)
        {
            builder.Append('\t'.Repeat(count));
            builder.AppendFormat(format + "\r\n", args);
        }

        public static void AppendLineFormatTabIndent(this StringBuilder builder, string format, params object[] args)
        {
            builder.Append('\t'.Repeat(1));
            builder.AppendFormat(format + "\r\n", args);
        }

        public static string ReadNullTermString(this BinaryReader reader, int maxLength)
        {
            var builder = new StringBuilder();

            for (var x = 0; x < maxLength; x++)
            {
                var _byte = reader.ReadByte();

                if (_byte == 0)
                {
                    break;
                }

                builder.Append((char) _byte);
            }

            return builder.ToString();
        }

        public static string ReadNullTermStringLength(this BinaryReader reader, int maxLength)
        {
            var builder = new StringBuilder();

            for (var x = 0; x < maxLength; x++)
            {
                var _byte = reader.ReadByte();

                if (_byte != 0)
                {
                    builder.Append((char)_byte);
                }
            }

            return builder.ToString();
        }

        public static string ReadUnicodeString(this BinaryReader reader)
        {
            var length = reader.ReadInt16();
            var bytes = reader.ReadBytes(length * 2);

            return UnicodeEncoding.Unicode.GetString(bytes);
        }

        public static string ReadNullTermFourByteAlignedString(this BinaryReader reader)
        {
            var buffer = new List<char>();
            char nextChar;

            do
            {
                nextChar = reader.ReadChar();
                buffer.Add(nextChar);
            }
            while (nextChar != '\0' || reader.BaseStream.Position % 4 != 0);

            return new string(buffer.TakeWhile(b => !b.Equals('\0')).ToArray<char>());
        }

        public static string Capitalize(this string text)
        {
            if (text.Length > 1)
            {
                return char.ToUpper(text[0]) + text.Substring(1);
            }
            else
            {
                return text.ToUpper();
            }
        }

        public static string[] Split(this string text, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return text.Split(new string[] { separator }, options);
        }

        public static string CapitalizeWords(this string text)
        {
            if (text.Length > 1)
            {
                text = text.Replace("-", " ");

                return string.Join(" ", text.SplitToWords().Select(w => w.Capitalize()));
            }

            return text;
        }

        public static string NumberToWords(this int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public static IEnumerable<char> GetNonWordCharacters(this string text)
        {
            return text.Where(c => c != ' ' && !c.IsBetween('a', 'z') && !c.IsBetween('A', 'Z'));
        }

        public static bool HasNonWordCharacters(this string text)
        {
            return text.GetNonWordCharacters().Count() > 0;
        }

        public static bool IsNonWordCharacter(this char ch)
        {
            return ch != ' ' && !ch.IsBetween('a', 'z') && !ch.IsBetween('A', 'Z');
        }

        public static string RemoveNonWordCharacters(this string text)
        {
            if (text.Length > 1)
            {
                text = text.Replace("-", " ");

                return string.Join(string.Empty, text.Where(c => c == ' ' || c.IsBetween('a', 'z') || c.IsBetween('A', 'Z')));
            }

            return text;
        }

        public static bool IsUpperCase(this char ch)
        {
            return ch.IsBetween('A', 'Z');
        }

        public static string RemoveInsignificantWords(this string text, IEnumerable<string> exclusions = null)
        {
            if (text.Length > 1)
            {
                if (exclusions != null)
                {
                    return string.Join(" ", text.SplitToWords().Where(w => !w.IsInsignificantWord(exclusions)));
                }
                else
                {
                    return string.Join(" ", text.SplitToWords().Where(w => !w.IsInsignificantWord()));
                }
            }

            return text;
        }

        public static string RemoveInsignificantWords(this string text, int level, IEnumerable<string> exclusions = null)
        {
            if (text.Length > 1)
            {
                if (exclusions != null)
                {
                    return string.Join(" ", text.SplitToWords().Where(w => !w.IsInsignificantWord(level, exclusions)));
                }
                else
                {
                    return string.Join(" ", text.SplitToWords().Where(w => !w.IsInsignificantWord(level)));
                }
            }

            return text;
        }

        public static string RemoveText(this string text, string replaceWithEmpty)
        {
            if (text.Length > 1)
            {
                return text.Replace(replaceWithEmpty, string.Empty);
            }

            return text;
        }

        public static string LowerCase(this string text)
        {
            if (text.Length > 1)
            {
                return char.ToLower(text[0]) + text.Substring(1);
            }
            else
            {
                return text.ToLower();
            }
        }

        public static string AsFormat(this string format, params object[] parms)
        {
            return string.Format(format, parms);
        }

        public static int IndexOfFirstNonWhitespace(this string text)
        {
            var firstChar = text.ToArray<char>().FirstGreaterThan(c => c, ' ', true);

            return text.IndexOf(firstChar);
        }

        public static bool StartsWithAny(this CaselessString text, params string[] parts)
        {
            foreach (var part in parts)
            {
                if (text.StartsWith(part))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool StartsWithAny(this string text, params string[] parts)
        {
            foreach (var part in parts)
            {
                if (text.StartsWith(part))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool EndsWithAny(this string text, params string[] parts)
        { 
            foreach (var part in parts)
            {
                if (text.EndsWith(part))
                {
                    return true;
                }
            }

            return false;
        }

        public static string Expressionize(this string str)
        {
            var regex = new Regex(@"(?<expressionstart>\{)(?<symbol>@)(?<expressionend>[^}]*\})");
            var builder = new StringBuilder(str);
            var index = 0;
            var charIndex = 0;

            if (regex.IsMatch(str))
            {
                foreach (var match in regex.Matches(str).Cast<Match>())
                {
                    var symbol = match.Groups["symbol"];
                    var expressionStart = match.Groups["expressionstart"];
                    var expressionEnd = match.Groups["expressionend"];

                    charIndex = symbol.Index;

                    builder.Replace("@", index.ToString(), charIndex, 1);

                    charIndex += symbol.Length + expressionEnd.Length;
                    index++;
                }
            }

            return builder.ToString();
        }

        public static void AppendSpaceIndent(this StringBuilder builder, int count, string text)
        {
            builder.Append(" ".Repeat(count));
            builder.Append(text);
        }

        public static void AppendFormatSpaceIndent(this StringBuilder builder, int count, string format, params object[] args)
        {
            builder.Append(" ".Repeat(count));
            builder.AppendFormat(format, args);
        }

        public static void AppendFormatSpaceIndent(this StringBuilder builder, string leadIfLength, int count, string format, params object[] args)
        {
            if (builder.Length > 0)
            {
                builder.Append(leadIfLength);
            }

            builder.Append(" ".Repeat(count));
            builder.AppendFormat(format, args);
        }

        public static void AppendWithLeadingIfLength(this StringBuilder builder, string lead, string format, params object[] args)
        {
            if (builder.Length > 0)
            {
                builder.Append(lead);
            }

            builder.AppendFormat(format, args);
        }

        public static void AppendLineIfLength(this StringBuilder builder, string text)
        {
            if (builder.Length > 0)
            {
                builder.AppendLine(text);
            }
        }

        public static void AppendIfLength(this StringBuilder builder, string text)
        {
            if (builder.Length > 0)
            {
                builder.Append(text);
            }
        }

        public static void AppendLineWithLeadingIfLength(this StringBuilder builder, string lead, string format, params object[] args)
        {
            if (builder.Length > 0)
            {
                builder.Append(lead);
            }

            builder.AppendLineFormat(format, args);
        }

        public static void AppendTabIndent(this StringBuilder builder, int count, string text)
        {
            builder.Append('\t'.Repeat(count));
            builder.Append(text);
        }

        public static void AppendFormatTabIndent(this StringBuilder builder, int count, string format, params object[] args)
        {
            builder.Append('\t'.Repeat(count));
            builder.AppendFormat(format, args);
        }

        public static void AppendLineSpaceIndent(this StringBuilder builder, int count, string text)
        {
            builder.Append(" ".Repeat(count));
            builder.AppendLine(text);
        }

        public static void AppendLineTabIndent(this StringBuilder builder, int count, string text)
        {
            builder.Append('\t'.Repeat(count));
            builder.AppendLine(text);
        }

        public static void WriteSpaceIndent(this ILogWriter writer, int count, string text)
        {
            writer.Write(" ".Repeat(count));
            writer.Write(text);
        }

        public static void WriteFormatSpaceIndent(this ILogWriter writer, int count, string format, params object[] args)
        {
            writer.Write(" ".Repeat(count));
            writer.Write(format, args);
        }

        public static void WriteTabIndent(this ILogWriter writer, int count, string text)
        {
            writer.Write('\t'.Repeat(count));
            writer.Write(text);
        }

        public static void WriteFormatTabIndent(this ILogWriter writer, int count, string format, params object[] args)
        {
            writer.Write('\t'.Repeat(count));
            writer.Write(format, args);
        }

        public static void WriteLineSpaceIndent(this ILogWriter writer, int count, string text)
        {
            writer.Write(" ".Repeat(count));
            writer.WriteLine(text);
        }

        public static void WriteLineTabIndent(this ILogWriter writer, int count, string text)
        {
            writer.Write('\t'.Repeat(count));
            writer.WriteLine(text);
        }

        public static void PrependLine(this StringBuilder builder, string format, params object[] args)
        {
            builder.InsertLineFormat(0, format, args);
        }

        public static void PrependLineWithLeadingIfLength(this StringBuilder builder, string lead, string format, params object[] args)
        {
            var length = builder.Length;

            builder.InsertFormat(0, format, args);

            if (length > 0)
            {
                builder.Insert(0, lead);
            }
        }

        public static void InsertLineAtCharIndex(this StringBuilder builder, int index)
        {
            builder.Insert(index, "\r\n");
        }

        public static void InsertLineFormatTabIndent(this StringBuilder builder, int index, int count, string format, params object[] args)
        {
            var builderTemp = new StringBuilder();

            builderTemp.AppendLineFormatTabIndent(count, format, args);

            builder.Insert(index, builderTemp.ToString());
        }

        public static void InsertFormat(this StringBuilder builder, int index, string format, params object[] args)
        {
            string text;

            if (args.Length > 0)
            {
                text = string.Format(format, args);
            }
            else
            {
                text = format;
            }

            builder.Insert(index, text);
        }

        public static void InsertLineFormat(this StringBuilder builder, int index, string format, params object[] args)
        {
            string text;

            if (args.Length > 0)
            {
                text = string.Format(format, args);
            }
            else
            {
                text = format;
            }

            builder.Insert(index, text + "\r\n");
        }

        public static string Repeat(this string value, int count)
        {
            if (value.Length == 1)
            {
                return Repeat(value[0], count);
            }
            else
            {
                var builder = new StringBuilder();

                count.Countdown(n =>
                {
                    builder.Append(value);
                });

                return builder.ToString();
            }
        }

        public static string Repeat(this char value, int count)
        {
            return new string(value, count);
        }

        public static CaselessString AsCaseless(this string str)
        {
            return str;
        }

        public static string WordsToCamelCase(this string str)
        {
            return string.Join(" ", Regex.Split(str, @"[\s\n\-]").Where(f => f.Trim().Length > 0).Select(s => s.ToCamelCase()));
        }

        public static string WordsToTitleCase(this string str)
        {
            return string.Join(" ", Regex.Split(str, @"[\s\n\-]").Where(f => f.Trim().Length > 0).Select(s => s.ToTitleCase()));
        }

        public static string[] SplitToWords(this string str)
        {
            str = str.Replace("-", " ");

            return Regex.Split(str, @"[\s\n\-]").Where(f => f.Trim().Length > 0).ToArray();
        }

        public static string[] SplitTitleCaseIdentifierToWords(this string str)
        {
            str = str.Replace("-", " ");

            return Regex.Split(str, @"(?=[A-Z])").Where(f => f.Trim().Length > 0).ToArray();
        }

        public static bool EndsWith(this CaselessString str, string value)
        {
            return str.String.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public static CaselessString Replace(this CaselessString str, string oldValue, string newValue)
        {
            return str.String.Replace(oldValue, newValue);
        }

        public static bool StartsWith(this CaselessString str, string value)
        {
            return str.String.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        public static string HtmlEncode(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        public static string HtmlDecode(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        public static XDocument ParseAsXML(this string str)
        {
            return XDocument.Parse(str);
        }

        public static string RegexGet(this string str, string pattern, string groupName)
        {
            var regex = new Regex(pattern);

            if (regex.IsMatch(str))
            {
                var match = regex.Match(str);
                var value = match.Groups[groupName].Value;

                return value;
            }

            return null;
        }

        public static string RegexGet(this string str, string pattern)
        {
            var regex = new Regex(pattern);

            if (regex.IsMatch(str))
            {
                var match = regex.Match(str);

                return match.Value;
            }

            return null;
        }

        public static IEnumerable<Match> GetAttributeStringExpressionMatches(this string str)
        {
            return str.RegexGetMatches(@"\{\s*(?<expression>" + StringExtensions.REGEX_IDENTIFIER_MIDSTRING + @")\s*\}");
        }

        public static IEnumerable<Match> RegexGetMatches(this string str, string pattern)
        {
            var regex = new Regex(pattern);

            if (regex.IsMatch(str))
            {
                var matches = regex.Matches(str);

                return matches.Cast<Match>();
            }

            return null;
        }

        public static Match RegexGetMatch(this string str, string pattern)
        {
            var regex = new Regex(pattern);

            if (regex.IsMatch(str))
            {
                return regex.Match(str);
            }

            return null;
        }

        public static string[] RegexSplit(this string str, string pattern)
        {
            var parts = new List<string>();

            if (Regex.IsMatch(str, pattern))
            {
                string fragment;
                var matches = Regex.Matches(str, pattern);
                var lastIndex = 0;

                foreach (Match match in matches)
                {
                    if (match.Index > 0)
                    {
                        fragment = str.Substring(lastIndex, match.Index - lastIndex);
                        parts.Add(fragment);
                    }

                    lastIndex = match.Index;
                }

                if (lastIndex != 0)
                {
                    fragment = str.Substring(lastIndex, str.Length - lastIndex);
                    parts.Add(fragment);
                }
            }

            return parts.ToArray();
        }

        public static bool IsWildcardMatch(this string str, string pattern)
        {
            bool result;
            var regexPattern = Regex.Escape(pattern);

            regexPattern = regexPattern.Replace(@"\*", ".*?");
            regexPattern = regexPattern.Replace(@"\?", ".");

            result = Regex.IsMatch(str, regexPattern, RegexOptions.IgnoreCase);

            return result;
        }

        public static bool RegexIsMatch(this StringBuilder builder, string pattern)
        {
            return Regex.IsMatch(builder.ToString(), pattern);
        }

        public static bool RegexIsMatch(this string str, string pattern)
        {
            return Regex.IsMatch(str, pattern);
        }

        public static string RemoveTrailingNulls(this string str)
        {
            while (str[str.Length - 1] == '\0')
            {
                str = str.RemoveEnd(1);
            }

            return str;
        }

        public static string RemoveTrailingWhitespace(this string str)
        {
            while (str[str.Length - 1] == '\r' || str[str.Length - 1] == '\n')
            {
                str = str.RemoveEnd(1);
            }

            return str;
        }

        public static string RegexRemove(this string str, string pattern)
        {
            return Regex.Replace(str, pattern, string.Empty);
        }

        public static string RegexReplace(this string str, string pattern, string replace)
        {
            return Regex.Replace(str, pattern, replace);
        }

        public static string RegexInsertAfter(this string str, string pattern, string insert)
        {
            var regex = new Regex(pattern);

            if (regex.IsMatch(str))
            {
                var match = regex.Match(str);

                return str.Insert(match.Index + match.Length, insert);
            }

            return str;
        }

        public static string RemoveEnclosingTag(this string str, string tag)
        {
            return str.RegexRemove(@"\<" + tag + @"[^>]*\>")
                .RegexRemove(@"\</" + tag + @"\>");
        }

        public static string EncloseWithTag(this string str, string tag)
        {
            return str
                .Prepend("<" + tag + ">")
                .Append("</" + tag + ">");
        }

        public static string SurroundWith(this string str, string pre, string post)
        {
            return str
                .Prepend(pre)
                .Append(post);
        }

        public static string SurroundWithIfNotNullOrEmpty(this string str, string pre, string post)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            else
            {
                return str
                    .Prepend(pre)
                    .Append(post);
            }
        }

        public static string RemoveNewLineAndCarraigeReturns(this string str)
        {
            return str.RegexRemove(@"\n");
        }

        public static string ReplaceInnerQuotesToSingle(this string str)
        {
            return str.RegexReplace("(?<!^)\"(?!$)", "'");
        }

        public static string SurroundWithQuotes(this string str)
        {
            return str.SurroundWith("\"");
        }

        public static string SurroundWithQuotesIfNeeded(this string str)
        {
            if (!str.StartsWith("\""))
            {
                str = str.Prepend("\"");
            }

            if (!str.EndsWith("\""))
            {
                str = str.Append("\"");
            }

            return str;
        }

        public static string SurroundWith(this string str, string text)
        {
            return str
                .Prepend(text)
                .Append(text);
        }

        public static XDocument ParseAsXML(this string str, string enclosingTag)
        {
            return XDocument.Parse(str
                .Prepend("<" + enclosingTag + ">")
                .Append("</" + enclosingTag + ">"));
        }

        public static string Append(this string str, string suffix)
        {
            return str + suffix;
        }

        public static string SplitJoin(this string str, string separator, Func<string, string> select)
        {
            return string.Join(separator, str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries).Select(select));
        }

        public static string SplitJoin(this string str, string separator, string splitSeparator)
        {
            return string.Join(separator, str.Split(new string[] { splitSeparator }, StringSplitOptions.RemoveEmptyEntries));
        }

        public static string AppendIfMissing(this string str, string suffix)
        {
            if (!str.AsCaseless().EndsWith(suffix))
            {
                return str + suffix;
            }
            else
            {
                return str;
            }
        }

        public static string AppendIf(this string str, string suffix, bool condition)
        {
            if (condition)
            {
                return str + suffix;
            }
            else
            {
                return str;
            }
        }

        public static string AppendCrlf(this string str)
        {
            return str.Append("\r\n");
        }

        public static string AppendIfNotNullOrEmpty(this string str, string suffix)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str + suffix;
            }
            else
            {
                return str;
            }
        }

        public static string Append(this string str, string delimiter, string suffix)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str + suffix;
            }
            else
            {
                return str + delimiter + suffix;
            }
        }

        public static string Prepend(this string str, string prefix)
        {
            return prefix + str;
        }

        public static string PrependIfMissing(this string str, string prefix)
        {
            if (!str.AsCaseless().StartsWith(prefix))
            {
                return prefix + str;
            }
            else
            {
                return str;
            }
        }

        public static string PrependIf(this string str, string prefix, bool condition)
        {
            if (condition)
            {
                return prefix + str;
            }
            else
            {
                return str;
            }
        }

        public static string PrependIfNotNullOrEmpty(this string str, string prefix)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return prefix + str;
            }
            else
            {
                return str;
            }
        }

        public static string PrependIfPrefixNotNullOrEmpty(this string str, string prefix)
        {
            if (!prefix.IsNullOrEmpty())
            {
                return prefix + str;
            }
            else
            {
                return str;
            }
        }

        public static string GetString(string str, params object[] parms)
        {
            return str + ", Params:" + parms.ToCommaDelimitedList();
        }

        public static string GetString(string str)
        {
            return str;
        }

        internal static string GetObject(string p)
        {
            return p;
        }

        public static string RemoveQuotes(this string text)
        {
            if (text.StartsWith("\"") || text.StartsWith("'"))
            {
                text = text.Remove(0, 1);
            }

            if (text.EndsWith("\"") || text.EndsWith("'"))
            {
                text = text.Remove(text.Length - 1);
            }

            return text;
        }

        public static string MakeUserFriendly(this string text)
        {
            text = string.Join(string.Empty, text.Select(c =>
            {
                if (c > 64 && c < 90)
                {
                    return " " + c.ToString();
                }
                else
                {
                    return c.ToString();
                }
            }));

            return text;
        }

        public static string ReverseSlashes(this string text)
        {
            if (text.Contains("\\"))
            {
                text = text.Replace("\\", "/");
            }
            else if (text.Contains("/"))
            {
                text = text.Replace("/", "\\");
            }

            return text;
        }

        public static string ForwardSlashes(this string text)
        {
            if (text.Contains("\\"))
            {
                text = text.Replace("\\", "/");
            }

            return text;
        }

        public static string BackSlashes(this string text)
        {
            if (text.Contains("/"))
            {
                text = text.Replace("/", "\\");
            }

            return text;
        }

        public static string SlashesToPipes(this string text)
        {
            if (text.Contains("\\"))
            {
                text = text.Replace("\\", "|");
            }

            return text;
        }

        public static string PipesToSlashes(this string text)
        {
            if (text.Contains("|"))
            {
                text = text.Replace("|", "\\");
            }

            return text;
        }

        public static string RemoveSurroundingSlashes(this string text)
        {
            if (text.StartsWith("\\") || text.StartsWith("/"))
            {
                text = text.Remove(0, 1);
            }

            if (text.EndsWith("\\") || text.EndsWith("/"))
            {
                text = text.Remove(text.Length - 1);
            }

            return text;
        }

        public static string RemoveSurrounding(this string text, string surrounding)
        {
            if (text.StartsWith(surrounding))
            {
                text = text.Remove(0, 1);
            }

            if (text.EndsWith(surrounding))
            {
                text = text.Remove(text.Length - 1);
            }

            return text;
        }

        public static string RemoveSurrounding(this string text, string start, string end)
        {
            if (text.StartsWith(start))
            {
                text = text.Remove(0, 1);
            }

            if (text.EndsWith(end))
            {
                text = text.Remove(text.Length - 1);
            }

            return text;
        }

        public static string ToCamelCase(this string text)
        {
            return text.First().ToString().ToLower() + string.Join("", text.Skip(1));
        }

        public static string ToTitleCase(this string text)
        {
            return text.First().ToString().ToUpper() + string.Join("", text.Skip(1));
        }

        public static string RemoveEnd(this string text, int count)
        {
            return text.Remove(text.Length - count);
        }

        public static string RemoveBOM(this string text)
        {
            if (text.StartsWith(BytOrderMarkUtf8))
            {
                text = text.RemoveStart(BytOrderMarkUtf8.Length);
            }

            return text;
        }

        public static bool HasBOM(this string text)
        {
            return text.StartsWith(BytOrderMarkUtf8);
        }

        public static string RemoveEndAtLastChar(this string text, char lastChar)
        {
            if (text.IndexOf(lastChar) != -1)
            {
                return text.Crop(text.LastIndexOf(lastChar));
            }
            else
            {
                return text;
            }
        }

        public static string RemoveEndAtFirstChar(this string text, char firstChar)
        {
            if (text.IndexOf(firstChar) != -1)
            {
                return text.Crop(text.IndexOf(firstChar));
            }
            else
            {
                return text;
            }
        }

        public static string RemoveEndAfterFirstChar(this string text, char firstChar)
        {
            if (text.IndexOf(firstChar) != -1)
            {
                return text.Crop(text.IndexOf(firstChar) + 1);
            }
            else
            {
                return text;
            }
        }

        public static string Crop(this string text, string lengthOfString)
        {
            return text.Crop(lengthOfString.Length);
        }

        public static string Crop(this string text, int lengthToTruncateTo, bool useEllipsis = false)
        {
            if (text.Length > lengthToTruncateTo)
            {
                if (useEllipsis)
                {
                    return text.RemoveEnd(text.Length - lengthToTruncateTo + 3) + "...";
                }
                else
                {
                    return text.RemoveEnd(text.Length - lengthToTruncateTo);
                }
            } 
            else
            {
                return text;
            }
        }

        public static string Crop(this string text, string lengthOfString, bool useEllipsis = false)
        {
            return text.Crop(lengthOfString.Length, useEllipsis);
        }

        public static string Crop(this string text, int lengthToTruncateTo)
        {
            if (text.Length > lengthToTruncateTo)
            {
                return text.RemoveEnd(text.Length - lengthToTruncateTo);
            }
            else
            {
                return text;
            }
        }

        public static bool StartsWith(this string text, params string[] args)
        {
            foreach (var arg in args)
            {
                if (text.StartsWith(arg))
                {
                    return true;
                }
            }

            return false;
        }

        public static string Substitute(this string text, int insertionPoint, int length, string substitute)
        {
            var left = text.Left(insertionPoint);
            var right = text.Right(text.Length - (insertionPoint + length));

            return left + substitute + right;
        }

        public static string Left(this string text, int count)
        {
            if (text.Length > count)
            {
                return text.Substring(0, count);
            }
            else
            {
                return text;
            }
        }

        public static string Right(this string text, int count)
        {
            if (text.Length > count)
            {
                return text.Substring(text.Length - count, count);
            }
            else
            {
                return text;
            }
        }

        public static string RightMinusLengthOf(this string text, string right)
        {
            var count = text.Length - right.Length;

            if (text.Length > count)
            {
                return text.Right(count);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string RightAtLastIndexOf(this string text, char ch)
        {
            if (text.Contains(ch))
            {
                return text.Right(text.Length - text.LastIndexOf(ch) - 1);
            }
            else
            {
                return text;
            }
        }

        public static string RightAtIndexOf(this string text, char ch)
        {
            if (text.Contains(ch))
            {
                return text.Right(text.Length - text.IndexOf(ch) - 1);
            }
            else
            {
                return text;
            }
        }

        public static string RightAt(this string text, bool reverse, Func<int, char, bool> charProcessor)
        {
            if (reverse)
            {
                var length = text.Length;
                var x = text.Length;

                foreach (var ch in text.Reverse())
                {
                    if (charProcessor(x, ch))
                    {
                        return text.Right(length - x);
                    }

                    x--;
                }
            }
            else
            {
                var x = 0;

                foreach (var ch in text)
                {
                    if (charProcessor(x, ch))
                    {
                        return text.Right(x);
                    }

                    x++;
                }
            }

            return text;
        }

        public static string LeftUpToLastIndexOf(this string text, char ch)
        {
            if (text.Contains(ch))
            {
                return text.Left(text.LastIndexOf(ch));
            }
            else
            {
                return text;
            }
        }

        public static string LeftUpToIndexOf(this string text, char ch)
        {
            if (text.Contains(ch))
            {
                return text.Left(text.IndexOf(ch));
            }
            else
            {
                return text;
            }
        }

        public static string LeftUpToIndexOf(this string text, char ch, int count)
        {
            if (text.Contains(ch))
            {
                return text.Left(text.IndexOf(ch, 0, count));
            }
            else
            {
                return text;
            }
        }

        public static string RemoveStart(this string text, int count)
        {
            return text.Remove(0, count);
        }

        public static string RemoveStartEnd(this string text, int count)
        {
            return text.RemoveEnd(count).RemoveStart(count);
        }

        public static string RemoveEnd(this string text, string lengthOfString)
        {
            return text.Remove(text.Length - lengthOfString.Length);
        }

        public static string RemoveStartEnd(this string text, string lengthOfString)
        {
            var count = text.Length - lengthOfString.Length;

            return text.RemoveEnd(count).RemoveStart(count);
        }

        public static string RemoveStart(this string text, string lengthOfString)
        {
            return text.Remove(0, lengthOfString.Length);
        }

        public static string RemoveStartIfMatches(this string text, string startString)
        {
            if (text.StartsWith(startString))
            {
                return text.Remove(0, startString.Length);
            }
            else
            {
                return text;
            }
        }

        public static string RemoveEndIfMatches(this string text, string endString)
        {
            if (text.EndsWith(endString))
            {
                return text.Remove(text.Length - endString.Length);
            }
            else
            {
                return text;
            }
        }
    }
}
