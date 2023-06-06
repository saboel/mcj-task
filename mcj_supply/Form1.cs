using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace mcj_supply
{
    public partial class Form1 : Form
    {
        private string txtFilesPath = "./";
        private Dictionary<string, string> fileContentsCache = new Dictionary<string, string>();
        private string latestFilePath = string.Empty;
        private FileSystemWatcher fileWatcher;





        public Form1() //constructor 
        {
            InitializeComponent(); 
            InitializeFileWatcher();
            GetLatestTextFilePath();
        }
        private void Form1_Load(object sender, EventArgs e) // usually empty unless you want default values populated when the form window loads
        {

        }

        private void InitializeFileWatcher()
        {
            fileWatcher = new FileSystemWatcher
            {
                Path = txtFilesPath,
                Filter = "*.txt",
                EnableRaisingEvents = true
            };

            // Subscribe to the events
            fileWatcher.Created += FileWatcher_Created;
        }

        private void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            // A new text file was created
            string filePath = e.FullPath;

            if (string.IsNullOrEmpty(latestFilePath) || File.GetLastWriteTime(filePath) > File.GetLastWriteTime(latestFilePath))
            {
                latestFilePath = filePath;
                GetLatestTextFilePath();
            }
        }

     
        private void GetLatestTextFilePath()
        {
            DirectoryInfo directory = new DirectoryInfo(txtFilesPath);
            FileInfo[] textFiles = directory.GetFiles("*.txt");

            if (textFiles.Length > 0)
            {
                FileInfo latestFile = textFiles.OrderByDescending(f => f.LastWriteTime).First();
                //then first check if we have that latest file in the map! if we do then grab it from map
                latestFilePath = latestFile.FullName;
                if (!fileContentsCache.ContainsKey(latestFilePath))
                {
                    loadlatest(latestFilePath);
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string id = richTextBox1.Text;
            // Load the latest text file

            if (!string.IsNullOrEmpty(latestFilePath))
            {
                string target = GetTargetForLoadId(id);
                if (target != null)
                {
                    label1.Text = "Target: " + target;
                }
                else
                {
                    label1.Text = "Load ID not found";
                }
            }
            else
            {
                label1.Text = "No text files found";
            }

        }

        private void loadlatest(string filepath)
        {
                string Filecontents = File.ReadAllText(filepath);
                fileContentsCache[filepath] = Filecontents;
              
        }



        private string GetTargetForLoadId(string loadId)
        {
            if (!string.IsNullOrEmpty(latestFilePath) && fileContentsCache.ContainsKey(latestFilePath))
            {
                string contents = fileContentsCache[latestFilePath];
                string[] lines = contents.Split('\n');

                foreach (string line in lines)
                {
                    string[] data = line.Split(';');
                    if (data.Length >= 2 && data[0] == loadId)
                    {
                        return data[1]; // Return the target associated with the load ID
                    }
                }

            }

            return null; // Load ID not found
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



    }
}
