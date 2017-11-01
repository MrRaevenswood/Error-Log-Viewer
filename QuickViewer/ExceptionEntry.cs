using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickViewer
{
    public class ExceptionEntry
    {
        public readonly string[] EntryLines;
        public readonly int? ErrorNo;
        public readonly DateTime? HeaderTimestamp;
        public readonly string ExceptionType;
        public readonly DateTime? GmtTimestamp;
        public readonly string Message;
        public readonly string Data;
        public readonly string AppDomainName;
        public readonly string WindowsIdentity;

        public ExceptionEntry(string[] entryLines)
        {
            EntryLines = entryLines;

            ErrorNo = ParseErrorNo();
            HeaderTimestamp = ParseHeaderTimestamp();
            ExceptionType = ParseType();
            GmtTimestamp = ParseGmtTimestamp();
            Message = ParseMessage();
            Data = ParseData();
            AppDomainName = ParseAppDomain();
            WindowsIdentity = ParseWindowsIdentity();
        }

        public static ExceptionEntry Parse(string[] allLines, int startLine, int endLine)
        {
            var entryLines = new string[1 + endLine - startLine];

            for (int i = startLine; i < endLine; i++)
            {
                entryLines[i - startLine] = allLines[i];
            }

            return new ExceptionEntry(entryLines);
        }

        private int? ParseErrorNo()
        {
            var line = (from l in EntryLines.ToList()
                        where l.StartsWith("Exceptions Error:")
                        select l).FirstOrDefault();

            if (line == null) return null;

            var parts = line.Split(":".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 1) return null;

            int result;

            if (int.TryParse(parts[1], out result)) return result;

            return null;
        }

        private DateTime? ParseHeaderTimestamp()
        {
            var line = (from l in EntryLines.ToList()
                        where l.Contains("Timestamp:")
                        select l).FirstOrDefault();

            if (line == null) return null;

            var p = line.IndexOf(" ", line.IndexOf("Timestamp:"));
            var dateTimeString = line.Substring(p).Trim();

            if (string.IsNullOrWhiteSpace(dateTimeString)) return null;

            DateTime result;

            if (DateTime.TryParse(dateTimeString, out result)) return result;

            return null;
        }

        private string ParseType()
        {
            var line = FindFirstMatchingLine("Type : ");

            if (string.IsNullOrWhiteSpace(line)) return null;

            var parts = line.Split(",".ToArray(), StringSplitOptions.None);

            if (parts.Length == 0) return null;

            return parts[0].Trim();
        }

        private DateTime? ParseGmtTimestamp()
        {
            DateTime testDate;
            var line = (from l in EntryLines
                        where DateTime.TryParse(l, out testDate)
                        select l).FirstOrDefault();

            if (line == null) return null;

            return DateTime.Parse(line);
        }

        private string ParseMessage()
        {
            return FindFirstMatchingLine("Message : ");
        }

        private string ParseData()
        {
            return FindFirstMatchingLine("Data : ");
        }

        private string ParseAppDomain()
        {
            return FindFirstMatchingLine("AppDomainName : ");
        }

        private string ParseWindowsIdentity()
        {
            return FindFirstMatchingLine("WindowsIdentity : ");
        }


        private string FindFirstMatchingLine(string searchLineStart)
        {
            var line = (from l in EntryLines
                        where l.StartsWith(searchLineStart)
                        select l).FirstOrDefault();

            if (line == null) return null;
            if (line.Length < searchLineStart.Length + 1) return null;
            return line.Substring(searchLineStart.Length).Trim();
        }
    }
}
