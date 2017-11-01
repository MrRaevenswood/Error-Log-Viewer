using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickViewer
{

    public partial class Form1 : Form
    {
        const string filename = @"server-exception.2016-09-27.log";
        const string filepath = @"C:\Logs";
        const string divider1 = @"----------------------------------------";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = new List<ExceptionEntry>();
            var lines = File.ReadAllLines(Path.Combine(filepath, filename)).ToArray();

            int? startLine = null;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == divider1)
                {
                    if (startLine == null)
                    {
                        startLine = i;
                    }
                    else
                    {
                        result.Add(ExceptionEntry.Parse(lines, startLine.Value, i));
                        startLine = null;
                    }
                }
            }

            var helper = new Helper("SERVER=Test;DATABASE=ExceptionLog;UID=sa;PWD=Test");
            helper.ProcessLogEntriesForFile("NYP", Path.Combine(filepath, filename), result);
            MessageBox.Show("DONE");
        }
    }
}
