using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremLogViewer
{
    public class ExceptionEntry
    {
        public readonly List<string> EntryLines;
        public readonly int? ErrorNo;
        public readonly DateTime? HeaderTimestamp;
        public readonly string ExceptionType;
        public readonly DateTime? GmtTimestamp;
        public readonly string Message;
        public readonly string Data;
        public readonly string AppDomainName;
        public readonly string WindowsIdentity;
        
        public ExceptionEntry(List<string> entryLines)
        {
            //Assigns the results of the various class' functions
            //To properties
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

        public static ExceptionEntry Parse(List<string> allLines, int startLine, int endLine)
        {
            
            //Determines length of string array based on the amount of lines plus one
            var entryLines = new string[1 + endLine - startLine];

            /*
                Starts at the first line and assigns the entrylines array a value for the current index
                (calculated as i - startline) that is equal to the current index of allLines array
            */
            for (var i = startLine; i < endLine; i++)
            {
                entryLines[i - startLine] = allLines[i];
            }

            //Returns the entire array object
            return new ExceptionEntry(entryLines.ToList());
        }

        public static ExceptionEntry ResetExceptionEntry(List<string> allBlockLines)
        {
            return new ExceptionEntry(allBlockLines);
        }

        public int? ParseErrorNo()
        {
            /*
                Takes from the EntryLines Array transforms into a List
                Then selects all lines that starts with Exceptions Error:
                Will only select the first element as the result or a default value
                
            */
            var line = (from l in EntryLines.ToList()
                        where l.StartsWith("Exceptions Error:")
                        select l).FirstOrDefault();

            //If the result of line is null, the function stops and returns null
            if (line == null) return null;

            //Assigns to parts the value of line split by : with empty elements removed
            var parts = line.Split(":".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            //If parts is less than null, the function stops and returns null
            if (parts.Length < 1) return null;

            int result;

            /*  Converts the string of parts at index 1 to int, if it succeeds the result is returned. 
                If not then, null is returned.
            */
            if (int.TryParse(parts[1], out result)) return result;

            return null;
        }

        public DateTime? ParseHeaderTimestamp()
        {
            /*
                Assigns to line the result of the query that takes the first
                entry of the EntryLines array that is converted into a list and contains
                TimeStamp:
            */
            var line = (from l in EntryLines.ToList()
                        where l.Contains("Timestamp:")
                        select l).FirstOrDefault();

            //If line is null, function stops and returns null
            if (line == null) return null;

            /*
                Assigns to p, the result of the IndexOf method.
                The nested IndexOf statement finds the index of T in Line
                Then it uses that number to start the search at T for an empty space
                The index of the next empty space after Timestamp: is then assigned to 
                p.
            */
            var p = line.IndexOf(" ", line.IndexOf("Timestamp:"));

            /*
                Assigns the dateTimeString to DateTime entry in the log file.
                It does this by taking the entire line and extracting everything after index p
                Then it trims everything else and assigns the remainder to dateTimeString
            */
            var dateTimeString = line.Substring(p).Trim();

            //If dateTimeString is empty, null, or contains only WhiteSpace, it will return null
            if (string.IsNullOrWhiteSpace(dateTimeString)) return null;

            DateTime result;

            //Converts dateTimeString to DateTime and if it succeeds it will return the result
            if (DateTime.TryParse(dateTimeString, out result)) return result;
            //If not it returns null
            return null;
        }

        public string ParseType()
        {
            //Calls the FindFirstMatchingLine function with an argument of Type :
            //Assigns to line, the line that contains Type :
            var line = FindFirstMatchingLine("Type : ");

            //If line is null, empty, or only contains whitespace, return null
            if (string.IsNullOrWhiteSpace(line)) return null;

            //Assigns to parts the result of splitting line by comma
            var parts = line.Split(",".ToArray(), StringSplitOptions.None);

            //If parts has nothing return null
            if (parts.Length == 0) return null;

            //Returns the entry in the index 0 while trimming the trailing and leading whitespace characters
            return parts[0].Trim();
        }

        public DateTime? ParseGmtTimestamp()
        {
            //Finds the first date time line that can be parsed into a DATETIME
            //This should be the GMT Time
            DateTime testDate;
            var line = (from l in EntryLines //Missing .ToList()?
                        where DateTime.TryParse(l, out testDate)
                        select l).FirstOrDefault();
            
            //If there are no lines, null is returned.
            if (line == null) return null;

            //Else the line variable is parsed to a DateTime format
            return DateTime.Parse(line);
        }

        public string ParseMessage()
        {
            //Calls FindFirstMatchingLine method with a Message : argument
            //Then returns the output
            return FindFirstMatchingLine("Message : ");
        }

        public string ParseData()
        {
            //Calls FindFirstMatchingLine method with a Data : 
            //Then returns the output
            return FindFirstMatchingLine("Data : ");
        }

        public string ParseAppDomain()
        {
            //Calls FindFirstMatchingLine method with a AppDomainName : 
            //Then returns the output
            return FindFirstMatchingLine("AppDomainName : ");
        }

        public string ParseWindowsIdentity()
        {
            //Calls FindFirstMatchingLine method with a WindowsIdentity :
            //Then returns the output
            return FindFirstMatchingLine("WindowsIdentity : ");
        }


        public string FindFirstMatchingLine(string searchLineStart)
        {
            //Takes the searchLineStart parameter and searches for that string
            //Returns only one thing and assigns it to the line variable
            var line = (from l in EntryLines //Missing .ToList()?
                        where l.StartsWith(searchLineStart)
                        select l).FirstOrDefault();

            //If the line variable has nothing, then return null
            if (line == null) return null;

            //If the length of the line variable is less than the searchLineStart length plus 1
            //Return null
            if (line.Length < searchLineStart.Length + 1) return null;

            //Take the length as the starting index and extract the rest of the line
            //Starting at that index then trim the white space on either end then return it.
            return line.Substring(searchLineStart.Length).Trim();
        }
    }
}
