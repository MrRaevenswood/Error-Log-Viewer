using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Collections;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;

namespace PremLogViewer
{
    public static class Extensions
    {
        public static string HashFile(this FileInfo sourceFile)
        {
            //Declares and uses a new instance of the SHA1 class named sha
            using (var sha = new SHA1CryptoServiceProvider())
            {
                //Assigns to buffer the byte array of the sourcefile
                var buffer = File.ReadAllBytes(sourceFile.FullName);

                //Assigns to buffer the hashed result of buffer
                var hashBuffer = sha.ComputeHash(buffer);

                //Assigns to result the conversion to String that has all - removed from hashBuffer 
                var result = BitConverter.ToString(hashBuffer).Replace("-", "");

                //Returns the result
                return result;
            }
        }
    }
    public class Helper
    {
        public readonly string ConnectString;
        

        public Helper(string techName, string custName, string dataSource, string initialCatalog, string userId, string password, bool integratedSecurity)
        {
            ConnectString = BuildSqlConnectString(dataSource,initialCatalog,userId,password,integratedSecurity);
            CreateBatch(techName, custName);
        }

        public static string BuildSqlConnectString(string dataSource, string initialCatalog, string userId, string password, bool integratedSecurity)
        {
            var connectionBuilder = new SqlConnectionStringBuilder();
            connectionBuilder.DataSource = dataSource;
            connectionBuilder.InitialCatalog = initialCatalog;
            connectionBuilder.UserID = userId;
            connectionBuilder.Password = password;
            connectionBuilder.IntegratedSecurity = integratedSecurity;

            return connectionBuilder.ToString();
        }
        public static void ParseEntryStartAndEndLines(List<string> entryLines, out List<int> startOfBlock, out List<int> endOfBlock)
        {
            var beginBlock = new List<int>();
            var endBlock = new List<int>();

            var arrayLength = entryLines.Count;
            var counter = 0;

            while (counter <= arrayLength - 1)
            {
                if (entryLines[counter].ToString() == "----------------------------------------")
                {
                    if (beginBlock.Count == endBlock.Count)
                    {
                        beginBlock.Add(counter);
                    }
                    else
                    {
                        endBlock.Add(counter);
                    }
                }

                counter++;

            }

            startOfBlock = beginBlock;
            endOfBlock = endBlock;

        }

        public void CreateBatch(string techName, string custName)
        {
            using (var db = new SqlConnection(ConnectString))
            {
                db.Open();

                using (var cmd = new SqlCommand("CreateBatch", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TechName", techName);
                    cmd.Parameters.AddWithValue("@customerName", custName);

                    var rdr = cmd.ExecuteReader();
                }

            }
        }

        public int ProcessLogEntriesForFile(FileInfo sourceFile, List<ExceptionEntry> entries, string sourceOfFile)
        {
            var fileId = 0;

            //Initiates a new connection string to the Database
            using (var db = new SqlConnection(ConnectString))
            {
                //Opens connection to the database
                db.Open();

                //Opens an instance of SQLCommand to execute the CreateBatch stored procedure
                using (var cmd = new SqlCommand("LogHashCheck", db))
                {
                    //Sets the CommandType to Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Sets the parameters for the Stored Procedure
                    cmd.Parameters.AddWithValue("@sourceFileName", sourceFile.Name);
                    cmd.Parameters.AddWithValue("@sourceFileHash", sourceFile.HashFile());
                    cmd.Parameters.AddWithValue("@sourceFileType", sourceOfFile);

                    //Read output
                    using (var rdr = cmd.ExecuteReader())
                    {
                        //If there is a line in rdr, then run the if statement
                        if (rdr.Read())
                        {
                            //Assigns logID to the int value of the first procedure variable @logFileIdx
                            fileId = rdr.GetInt32(0);
                            //Assigns exists to the value of @preExists from the procedure
                            var exists = rdr.GetBoolean(1);

                            //Assigns batchId to the value of @batchIdx from the procedure
                            //batchId = rdr.GetInt32(2);

                            //If exists equals 1, return null and stop the method
                            if (exists) return 1;
                        }
                    }
                  }

                return 0;
            }
        }

        public void InsertEntry(int logIndex, ExceptionEntry entry)
        {
            var errorFileName = string.Empty;
            //Starts a new instance of SQLConnections called db
            using (var db = new SqlConnection(ConnectString))
            {
                //Open the db connection
                db.Open();

                //Run the InsertLogEntry stored procedure using the db connection
                using (var cmd = new SqlCommand("InsertLogEntry", db))
                {
                    //Sets the CommandType to Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Sets the parameters of the Stored Procedure to the Entries properties
                    cmd.Parameters.AddWithValue("@logFileIdx", logIndex);
                    cmd.Parameters.AddWithValue("@lineData", string.Join("\n\r", entry.EntryLines));
                    cmd.Parameters.AddWithValue("@errorNo", entry.ErrorNo);
                    cmd.Parameters.AddWithValue("@headerTimestamp", entry.HeaderTimestamp);
                    cmd.Parameters.AddWithValue("@exceptionType", entry.ExceptionType);
                    cmd.Parameters.AddWithValue("@gmtTimestamp", entry.GmtTimestamp);
                    cmd.Parameters.AddWithValue("@message", entry.Message);
                    cmd.Parameters.AddWithValue("@data", entry.Data);
                    cmd.Parameters.AddWithValue("@appDomainName", entry.AppDomainName);
                    cmd.Parameters.AddWithValue("@windowsIdentityName", entry.WindowsIdentity);

                    try
                    {
                        //Execute the query
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        if (e.Number == -2)
                        {
                            MessageBox.Show("Program failed due to failure to reach the SQL Server. This could be due to bad username/password or a network connection issue.");
                        }
                        else
                        {
                            using (var errorCmd = new SqlCommand())
                            {
                                errorCmd.CommandText = "Select SourceFileName From LogFile Where Idx =" + logIndex;
                                errorCmd.CommandType = CommandType.Text;
                                errorCmd.Connection = db;

                                using (var rdr = errorCmd.ExecuteReader())
                                {
                                    if (rdr.Read())
                                    {
                                        errorFileName = rdr.GetString(0);
                                    }

                                }


                            }
                            MessageBox.Show($"The following error message was shown for the file " + errorFileName +
                                $"{Environment.NewLine}{Environment.NewLine}" + e.Message);
                        }
                        
                    }

                }
            }
        }

        public List<string> GetIndividualEntries(string nameOfFile)
        {

            var entryData = new List<string>();

            using (var db = new SqlConnection(ConnectString))
            {
                db.Open();
                using (var cmdEntries = new SqlCommand("GetIndividualEntries", db))
                {
                    cmdEntries.CommandType = CommandType.StoredProcedure;

                    cmdEntries.Parameters.AddWithValue("@nameOfFile", nameOfFile);

                    using (var rdr = cmdEntries.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            entryData.Add(rdr.GetString(0));
                        }
                    }

                    return entryData;
                }
            }
        }

        public List<string> GetFileNames()
        {
            var fileNames = new List<string>();

            using (var db = new SqlConnection(ConnectString))
            {
                db.Open();
                using (var cmdEntries = new SqlCommand("GetFileNames", db))
                {
                    cmdEntries.CommandType = CommandType.StoredProcedure;

                    using (var rdr = cmdEntries.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            fileNames.Add(rdr.GetString(0));
                        }
                    }
                }
                db.Close();
            }
            
            return fileNames;
        }
    }
}
