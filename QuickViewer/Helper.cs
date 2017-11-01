using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace QuickViewer
{
    public static class Extensions
    {
        public static string HashFile(this FileInfo sourceFile)
        {
            using (var sha = new SHA1CryptoServiceProvider())
            {
                var buffer = File.ReadAllBytes(sourceFile.FullName);

                var hashBuffer = sha.ComputeHash(buffer);
                var result = BitConverter.ToString(hashBuffer).Replace("-", "");

                return result;
            }
        }
    }
    class Helper
    {
        public readonly string ConnectString;
        public Helper(string connectString)
        {
            ConnectString = connectString;
        }

        public int? ProcessLogEntriesForFile(string customerName, string sourceFilePath, List<ExceptionEntry> entries)
        {
            var fi = new FileInfo(sourceFilePath);
            var hash = fi.HashFile();
            int? batchId = null;
            using (var db = new SqlConnection(ConnectString))
            {
                db.Open();
                using (var cmd = new SqlCommand("CreateBatch", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerName", customerName);
                    cmd.Parameters.AddWithValue("@sourceFileName", fi.Name);
                    cmd.Parameters.AddWithValue("@sourceFileHash", hash);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            batchId = rdr.GetInt32(0);
                            var exists = rdr.GetBoolean(1);

                            if (exists) return null;
                        }
                    }
                }
            }

            if (batchId != null)
            {
                entries.ForEach(e => InsertEntry(batchId.Value, e));
            }
            return batchId;
        }

        private void InsertEntry(int logIndex, ExceptionEntry entry)
        {
            using (var db = new SqlConnection(ConnectString))
            {
                db.Open();
                using (var cmd = new SqlCommand("InsertLogEntry", db))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

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

                    cmd.ExecuteNonQuery();
                    //var result = (int)cmd.ExecuteScalar();

                    //return result;
                }
            }
        }
    }
}
