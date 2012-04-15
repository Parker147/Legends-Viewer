using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using LegendsViewer.Legends;
using SevenZip;

namespace LegendsViewer
{
    public class FileLoader
    {
        public bool Working;
        private frmLegendsViewer Form;
        private Button XMLButton, HistoryButton, SitesButton, MapButton;
        private TextBox XMLText, HistoryText, SitesText, MapText;
        private RichTextBox LogText;
        private Label StatusLabel;
        private bool _xmlReady, _historyReady, _sitesReady, _mapReady;
        private bool XMLReady 
        {
            get { return _xmlReady;}
            set {
                if (File.Exists(XMLText.Text))
                    _xmlReady = value;
                else
                    _xmlReady = false;
                if (_xmlReady)
                    XMLText.BackColor = Color.FromArgb(220, 255, 220);
                else
                {
                    XMLText.BackColor = SystemColors.MenuBar;
                    XMLText.Text = "Legends XML / Archive";
                }
            }
        }
        private bool HistoryReady
        {
            get { return _historyReady; }
            set
            {
                if (File.Exists(HistoryText.Text))
                    _historyReady = value;
                else
                    _historyReady = false;
                if (_historyReady)
                    HistoryText.BackColor = Color.FromArgb(220, 255, 220);
                else
                {
                    HistoryText.BackColor = SystemColors.MenuBar;
                    HistoryText.Text = "World History Text";
                }
            }
        }
        private bool SitesReady
        {
            get { return _sitesReady;}
            set {
                if (File.Exists(SitesText.Text))
                    _sitesReady = value;
                else
                    _sitesReady = false;
                if (_sitesReady)
                    SitesText.BackColor = Color.FromArgb(220, 255, 220);
                else
                {
                    SitesText.BackColor = SystemColors.MenuBar;
                    SitesText.Text = "Sites and Populations Text";
                }
            }
        }
        private bool MapReady
        {
            get { return _mapReady; }
            set
            {
                if (File.Exists(MapText.Text))
                    _mapReady = value;
                else
                    _mapReady = false;
                if (_mapReady)
                    MapText.BackColor = Color.FromArgb(220, 255, 220);
                else
                {
                    MapText.BackColor = SystemColors.MenuBar;
                    MapText.Text = "Map Image";
                }
            }
        }
        private List<string> ExtractedFiles;
        private Color ReadyColor;

        public FileLoader(frmLegendsViewer form, Button xmlButton, TextBox xmlText, Button historyButton, TextBox historyText, Button sitesButton, TextBox sitesText, Button mapButton, TextBox mapText, Label statusLabel, RichTextBox logText)
        {
            Form = form;
            XMLButton = xmlButton;
            XMLText = xmlText;
            HistoryButton = historyButton;
            HistoryText = historyText;
            SitesButton = sitesButton;
            SitesText = sitesText;
            MapButton = mapButton;
            MapText = mapText;
            StatusLabel = statusLabel;
            LogText = logText;

            XMLButton.Click += XMLClick;
            HistoryButton.Click += HistoryClick;
            SitesButton.Click += SitesClick;
            MapButton.Click += MapClick;

            ExtractedFiles = new List<string>();
        }

        private string GetFile(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = filter;
            if (dialog.ShowDialog() == DialogResult.OK)
                return dialog.FileName;
            else
                return "";
        }

        private void XMLClick(Object sender, EventArgs e)
        {
            AttemptLoadFrom(GetFile("Legends XML or Archive | *.*"));
        }

        public void AttemptLoadFrom(string file)
        {
            if (!file.EndsWith(".xml") && file != "")
                Extract(file);
            else if (file.EndsWith(".xml"))
            {
                XMLText.Text = file;
                XMLReady = true;
            }
            else
                XMLReady = false;

            LocateOtherFiles(file);
            Load();
        }

        private void HistoryClick(Object sender, EventArgs e)
        {
            string historyFile = GetFile("History Text | *.txt");
            if (historyFile != "")
            {
                HistoryText.Text = historyFile;
                HistoryReady = true;
            }
            else
            {
                HistoryReady = false;
            }
            Load();
        }

        private void SitesClick(Object sender, EventArgs e)
        {
            string sitesFile = GetFile("Sites and Pops Text | *.txt");
            if (sitesFile != "")
            {
                SitesText.Text = sitesFile;
                SitesReady = true;
            }
            else
                SitesReady = false;
            Load();
        }

        private void MapClick(Object sender, EventArgs e)
        {
            string mapFile = GetFile("Map Image | *.jpg;*.jpeg;*.bmp;*.png;|All Files|*.*");
            if (mapFile != "")
            {
                MapText.Text = mapFile;
                MapReady = true;
            }
            else
                MapReady = false;
            Load();
        }

        private void LocateOtherFiles(string xmlFile)
        {
            string region = "";
            if (xmlFile.LastIndexOf("-") > 0)
                region = xmlFile.Substring(xmlFile.LastIndexOf("\\") + 1, xmlFile.LastIndexOf("-") - xmlFile.LastIndexOf("\\") - 1);
            else
                return;
            string directory = xmlFile.Substring(0, xmlFile.LastIndexOf("\\") + 1);

            if (File.Exists(directory + region + "-world_history.txt"))
            {
                HistoryText.Text = directory + region + "-world_history.txt";
                HistoryReady = true;
            }
            if (File.Exists(directory + region + "-world_sites_and_pops.txt"))
            {
                SitesText.Text = directory + region + "-world_sites_and_pops.txt";
                SitesReady = true;
            }
            List<string> imageFiles = Directory.GetFiles(directory).Where(file => file.Contains("world") && file.Contains(region) && (file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"))).ToList();
            if (imageFiles.Count == 1)
            {
                MapText.Text = imageFiles.First();
                MapReady = true;
            }

        }

        private void Load()
        {
            if (!(XMLReady && HistoryReady && SitesReady && MapReady)) return;

            if (!(File.Exists(XMLText.Text) && File.Exists(HistoryText.Text) && File.Exists(SitesText.Text) && File.Exists(MapText.Text)))
                return;

            string[] files = new string[] { XMLText.Text, HistoryText.Text, SitesText.Text, MapText.Text };

            Working = true;
            StatusLabel.Text = "Loading...";
            StatusLabel.ForeColor = Color.Blue;

            BackgroundWorker load = new BackgroundWorker();
            load.DoWork += load_DoWork;
            load.RunWorkerCompleted += load_RunWorkerCompleted;

            load.RunWorkerAsync(files);
            while (load.IsBusy) Application.DoEvents();

            Working = false;
            XMLReady = HistoryReady = SitesReady = MapReady = false;
        }

        private void load_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = e.Argument as string[];
            e.Result = new World(files[0], files[1], files[2], files[3]);
        }

        private void load_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (string delete in ExtractedFiles)
                System.IO.File.Delete(delete);
            ExtractedFiles.Clear();

            if (e.Error != null)
            {
                LogText.AppendText(e.Error.Message + "\n" + e.Error.StackTrace);
                StatusLabel.Text = "ERROR!";
                StatusLabel.ForeColor = Color.Red;
            }
            else
                Form.AfterLoad(e.Result as World);

        }

        private void Extract(string file)
        {
            StatusLabel.Text = "Extracting...";
            StatusLabel.ForeColor = Color.Orange;

            BackgroundWorker extract = new BackgroundWorker();
            extract.DoWork += extract_DoWork;
            extract.RunWorkerCompleted += extract_RunWorkerCompleted;

            extract.RunWorkerAsync(file);
            while (extract.IsBusy) Application.DoEvents();
        }

        private void extract_DoWork(object sender, DoWorkEventArgs e)
        {
            using (SevenZipExtractor extractor = new SevenZipExtractor(e.Argument as String))
            {
                if (extractor.ArchiveFileNames.Count(file => file.EndsWith(".xml")) > 1 || extractor.ArchiveFileNames.Count(file => file.EndsWith(".xml")) == 0)
                    throw new Exception("Not enough or too many XML Files");
                if (extractor.ArchiveFileNames.Count(file => file.EndsWith(".world_history.txt")) > 1 || extractor.ArchiveFileNames.Count(file => file.EndsWith("-world_history.txt")) == 0)
                    throw new Exception("Not enough or too many World History Text Files");
                if (extractor.ArchiveFileNames.Count(file => file.EndsWith("-world_sites_and_pops.txt")) > 1 || extractor.ArchiveFileNames.Count(file => file.EndsWith("-world_sites_and_pops.txt")) == 0)
                    throw new Exception("Not enough or too many Site & Pops Text Files");
                if (extractor.ArchiveFileNames.Count(file => file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg")) == 0)
                    throw new Exception("No map image found.");

                string xml = extractor.ArchiveFileNames.Where(file => file.EndsWith(".xml")).Single();
                if (File.Exists(xml)) throw new Exception(xml + " already exists.");
                extractor.ExtractFiles(System.IO.Directory.GetCurrentDirectory(), xml);
                ExtractedFiles.Add(xml);

                string history = extractor.ArchiveFileNames.Where(file => file.EndsWith("-world_history.txt")).Single();
                if (File.Exists(history)) throw new Exception(history + " already exists.");
                extractor.ExtractFiles(System.IO.Directory.GetCurrentDirectory(), history);
                ExtractedFiles.Add(history);

                string sites = extractor.ArchiveFileNames.Where(file => file.EndsWith("-world_sites_and_pops.txt")).Single();
                if (File.Exists(sites)) throw new Exception(sites + " already exists.");
                extractor.ExtractFiles(System.IO.Directory.GetCurrentDirectory(), sites);
                ExtractedFiles.Add(sites);

                string map = "";

                if (extractor.ArchiveFileNames.Count(file => file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg")) == 1)
                    map = extractor.ArchiveFileNames.Single(file => file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"));
                else
                {
                    dlgFileSelect fileSelect = new dlgFileSelect(extractor.ArchiveFileNames.Where(file => file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg")).ToList());
                    fileSelect.Text = "Select Base Map";
                    fileSelect.ShowDialog();
                    if (fileSelect.SelectedFile == "") throw new Exception("No map file selected.");
                    if (File.Exists(fileSelect.SelectedFile)) { MessageBox.Show(fileSelect.SelectedFile + " already exists."); return; }
                    map = fileSelect.SelectedFile;
                }

                if (File.Exists(map)) throw new Exception(map + " already exists.");
                extractor.ExtractFiles(Directory.GetCurrentDirectory(), map);
                ExtractedFiles.Add(map);
            }
        }
        private void extract_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                StatusLabel.Text = "ERROR!";
                StatusLabel.ForeColor = Color.Red;
                LogText.Text = e.Error.Message;
                foreach (string delete in ExtractedFiles)
                    File.Delete(delete);
                ExtractedFiles.Clear();
                Working = false;
            }
            else
            {
                XMLText.Text = ExtractedFiles[0];
                XMLReady = true;
                HistoryText.Text = ExtractedFiles[1];
                HistoryReady = true;
                SitesText.Text = ExtractedFiles[2];
                SitesReady = true;
                MapText.Text = ExtractedFiles[3];
                MapReady = true;
            }
        }
    }
}
