/*
    ImportExceptionFiles Class includes the following properties
        ListOfExceptionFiles
        FileContent
        FileName
        ImportFilePath
        TypeOfFile
        MainFile
        Process
        SQLImport
        AmountOfFiles Import
    
     This class describes the files that are taken from entire folder directory
     that are exception log files for the program. This class is then designed to 
     contain the contents of each folder so that the program can loop through each folder as
     needed while it imports using the connection string given.    
        
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace PremLogViewer
{
    public class ImportExceptionFiles
    {
        private static string FileName = string.Empty;
        private static string ImportFilePath = string.Empty;

        private enum TypeOfFile : int
        {
            Client = 1,
            Scheduling = 2,
            Server = 3,
            PrintManager = 4

        }

        private static TypeOfFile FileType; 

        private List<string> ListOfExceptionFiles;
        private List<string> FileContent;
        private ExceptionEntry MainFile;
        private ExceptionEntry Process;
        private Helper SQLImport;
        private int Counter;
        private int AmountOfFilesCounter;
        private int HashResult = 0;
        private int FileId = 0;

        public ImportExceptionFiles(string importFileLocation, Helper sqlImport, List<string> fileContent)
        {
            FileContent = fileContent;
            MainFile = new ExceptionEntry(FileContent);
            Process = new ExceptionEntry(FileContent);
            ImportFilePath = importFileLocation;
            ListOfExceptionFiles = GetListOfExcepFiles();
            SQLImport = sqlImport;
            AmountOfFilesCounter = ListOfExceptionFiles.Count();

            Counter = CheckMultipleFiles();

        }
        public List<string> GetListOfExcepFiles()
        {
            var directoryName = Path.GetDirectoryName(ImportFilePath);
            var listOfExceptionFiles = Directory.EnumerateFiles(directoryName, "*exception*");
            return ListOfExceptionFiles = listOfExceptionFiles.ToList();
        }
        public void GetSourceType()
        {
            if (ListOfExceptionFiles[Counter].ToLower().Contains("client"))
            {
                FileType = TypeOfFile.Client;
            }
            else if (ListOfExceptionFiles[Counter].ToLower().Contains("scheduling"))
            {
                FileType = TypeOfFile.Scheduling;
            }
            else if (ListOfExceptionFiles[Counter].ToLower().Contains("server"))
            {
                FileType = TypeOfFile.Server;
            }
            else if (ListOfExceptionFiles[Counter].ToLower().Contains("printmanager"))
            {
                FileType = TypeOfFile.PrintManager;
            }


        }

        public int GetFileHashResults()
        {
            var processList = new List<ExceptionEntry>();
            var sourceFile = new FileInfo(ListOfExceptionFiles[Counter]);
            var hashResult = SQLImport.ProcessLogEntriesForFile(sourceFile, processList, FileType.ToString());

            return hashResult;
        }

        public int GetFileId()
        {
            var sourceFile = new FileInfo(ListOfExceptionFiles[Counter]);
            var fId = 0;
            using (var db = new SqlConnection(SQLImport.ConnectString))
            {
                db.Open();

                using (var cmd = new SqlCommand("GetFileId", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FileHash", sourceFile.HashFile());

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            fId = rdr.GetInt32(0);
                        }
                    }
                }

                db.Close();
            }
            return fId;
        }

        public String GetFileName(int fileID)
        {
            var fileName = "";
            using (var db = new SqlConnection(SQLImport.ConnectString))
            {
                db.Open();

                using (var cmd = new SqlCommand("GetFileName", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@fileId", fileID);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            fileName = rdr.GetString(0);
                        }
                    }
                }

                db.Close();
            }

            return fileName;
        }


        public int CheckMultipleFiles()
        {
            if (ListOfExceptionFiles.Count() > 1)
            {
                DialogResult multiFilesChoice = MessageBox.Show("You have multiple files in this directory that you can import. Would you like to import them?", "Import Multiple Files?", MessageBoxButtons.YesNo);
                if (multiFilesChoice == DialogResult.No)
                {
                    AmountOfFilesCounter = ListOfExceptionFiles.IndexOf(ImportFilePath) + 1;
                    Counter = AmountOfFilesCounter - 1;
                }
            }

            return Counter;
        }
        
        public void InsertAllFiles()
        {
            var counterBlock = 0;
            var startBlock = new List<int>();
            var endBlock = new List<int>();
            var entryBlock = new List<string>();

            Helper.ParseEntryStartAndEndLines(FileContent, out startBlock, out endBlock);

            var numOfEntriesStart = startBlock.Count;
            var numOfEntriesEnd = endBlock.Count;

            var entriesInsert = SQLImport;
            var counterStartEntries = 0;
            var counterEndEntries = 0;


            while (counterBlock <= numOfEntriesStart - 1)
            {
                counterStartEntries = startBlock[counterBlock];
                counterEndEntries = endBlock[counterBlock];

                while (counterStartEntries <= counterEndEntries)
                {
                    entryBlock.Add(MainFile.EntryLines[counterStartEntries]);
                    counterStartEntries++;

                }

                Process = ExceptionEntry.ResetExceptionEntry(entryBlock);


                entriesInsert.InsertEntry(FileId, Process);

                entryBlock.Clear();

                counterBlock++;

            }
        }
        
        public static void CheckFileHashAgainstExistingHashes(ImportExceptionFiles exceptionFile, Helper importJob)
        { 
            while (exceptionFile.Counter < exceptionFile.AmountOfFilesCounter)
            {
                exceptionFile.FileContent = File.ReadAllLines(exceptionFile.ListOfExceptionFiles[exceptionFile.Counter]).ToList();
                var lineCount = exceptionFile.FileContent.Count();

                exceptionFile.MainFile = ExceptionEntry.Parse(exceptionFile.FileContent, 0, lineCount);
                exceptionFile.Process = ExceptionEntry.Parse(exceptionFile.FileContent, 0, lineCount);

                exceptionFile.GetSourceType();
                exceptionFile.HashResult = exceptionFile.GetFileHashResults();
                exceptionFile.FileId = exceptionFile.GetFileId();
                var fileName = exceptionFile.GetFileName(exceptionFile.FileId);

                if (exceptionFile.HashResult == 1)
                {
                    MessageBox.Show($"This file has already been imported. Please import another file. The File ID is {exceptionFile.FileId} and the file name is {fileName}.");
                    exceptionFile.Counter++;
                    continue;
                }
                exceptionFile.InsertAllFiles();
                exceptionFile.Counter++; 
            }
        } 
      }
    }
