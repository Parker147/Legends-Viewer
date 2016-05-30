using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using LegendsViewer.Legends;
using SevenZip;
using System.Xml;
using System.Reflection;
using LegendsViewer.Controls;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer
{
    public class FileLoader
    {
        public bool Working;
        private Button XMLButton, HistoryButton, SitesButton, MapButton, XMLPlusButton;
        private TextBox XMLText, HistoryText, SitesText, MapText, XMLPlusText;
        private RichTextBox LogText;
        private Label StatusLabel;
        private Color ReadyColor = Color.FromArgb(220, 255, 220);
        private Color NotReadyColor = Color.FromArgb(255, 220, 220);
        private Color NotAvailableColor = Color.DarkGray;
        private Color DefaultColor = SystemColors.Control;
        private FileState _xmlState, _historyState, _sitesState, _mapState, _xmlPlusState;
        private string XMLTextDefault = "Legends XML / Archive";
        private const string HistoryTextDefault = "World History Text";
        private const string SitesTextDefault = "Sites and Populations Text";
        private const string MapTextDefault = "Map Image";
        private const string XMLPlusTextDefault = "Legends XML Plus";


        public static string SaveDirectory { get; set; }
        public static string SaveID { get; set; }

        private FileState XMLState
        {
            get { return _xmlState; }
            set
            {
                _xmlState = value;
                switch (value)
                {
                    case FileState.Default:
                        XMLText.Text = XMLTextDefault;
                        XMLText.BackColor = DefaultColor;
                        break;
                    case FileState.NotReady:
                        XMLText.Text = XMLTextDefault;
                        XMLText.BackColor = NotReadyColor;
                        break;
                    case FileState.Ready:
                        XMLText.BackColor = ReadyColor;
                        break;
                    default:
                        throw new Exception("Unhandled File Loader File State.");
                }
            }
        }
        private FileState HistoryState
        {
            get { return _historyState; }
            set
            {
                _historyState = value;
                switch (value)
                {
                    case FileState.Default:
                        HistoryText.Text = HistoryTextDefault;
                        HistoryText.BackColor = DefaultColor;
                        break;
                    case FileState.NotReady:
                        HistoryText.Text = HistoryTextDefault;
                        HistoryText.BackColor = NotReadyColor;
                        break;
                    case FileState.Ready:
                        HistoryText.BackColor = ReadyColor;
                        break;
                    default:
                        throw new Exception("Unhandled File Loader File State.");
                }
            }
        }
        private FileState SitesState
        {
            get { return _sitesState; }
            set
            {
                _sitesState = value;
                switch (value)
                {
                    case FileState.Default:
                        SitesText.Text = SitesTextDefault;
                        SitesText.BackColor = DefaultColor;
                        break;
                    case FileState.NotReady:
                        SitesText.Text = SitesTextDefault;
                        SitesText.BackColor = NotReadyColor;
                        break;
                    case FileState.Ready:
                        SitesText.BackColor = ReadyColor;
                        break;
                    default:
                        throw new Exception("Unhandled File Loader File State.");
                }
            }
        }
        private FileState MapState
        {
            get { return _mapState; }
            set
            {
                _mapState = value;
                switch (value)
                {
                    case FileState.Default:
                        MapText.Text = MapTextDefault;
                        MapText.BackColor = DefaultColor;
                        break;
                    case FileState.NotReady:
                        MapText.Text = MapTextDefault;
                        MapText.BackColor = NotReadyColor;
                        break;
                    case FileState.Ready:
                        MapText.BackColor = ReadyColor;
                        break;
                    default:
                        throw new Exception("Unhandled File Loader File State.");
                }
            }
        }
        private FileState XMLPlusState
        {
            get { return _xmlPlusState; }
            set
            {
                _xmlPlusState = value;
                switch (value)
                {
                    case FileState.Default:
                        XMLPlusText.Text = XMLPlusTextDefault;
                        XMLPlusText.BackColor = DefaultColor;
                        break;
                    case FileState.NotReady:
                        XMLPlusText.Text = XMLPlusTextDefault;
                        XMLPlusText.BackColor = NotAvailableColor;
                        break;
                    case FileState.Ready:
                        XMLPlusText.BackColor = ReadyColor;
                        break;
                    default:
                        throw new Exception("Unhandled File Loader File State.");
                }
            }
        }
        private List<string> ExtractedFiles;

        public FileLoader(
              Button xmlButton, TextBox xmlText
            , Button historyButton, TextBox historyText
            , Button sitesButton, TextBox sitesText
            , Button mapButton, TextBox mapText
            , Button xmlPlusButton, TextBox xmlPlusText
            , Label statusLabel, RichTextBox logText)
        {
            XMLButton = xmlButton;
            XMLText = xmlText;
            HistoryButton = historyButton;
            HistoryText = historyText;
            SitesButton = sitesButton;
            SitesText = sitesText;
            MapButton = mapButton;
            MapText = mapText;
            XMLPlusButton = xmlPlusButton;
            XMLPlusText = xmlPlusText;
            StatusLabel = statusLabel;
            LogText = logText;

            XMLState = HistoryState = SitesState = MapState = FileState.Default;

            XMLButton.Click += XMLClick;
            HistoryButton.Click += HistoryClick;
            SitesButton.Click += SitesClick;
            MapButton.Click += MapClick;
            XMLPlusButton.Click += XmlPlusClick;

            ExtractedFiles = new List<string>();

            if (Environment.Is64BitProcess)
                SevenZipBase.SetLibraryPath("7z64.dll");

            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "LegendsViewer.Controls.HTML.Styles.legends.css";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    Controls.HTMLPrinter.LegendsCSS = reader.ReadToEnd();
                }
                var chartjsName = "LegendsViewer.Controls.HTML.Scripts.Chart.min.js";
                using (Stream stream = assembly.GetManifestResourceStream(chartjsName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    Controls.HTMLPrinter.ChartJS = reader.ReadToEnd();
                }
                var cytoscapejsName = "LegendsViewer.Controls.HTML.Scripts.cytoscape.min.js";
                using (Stream stream = assembly.GetManifestResourceStream(cytoscapejsName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    Controls.HTMLPrinter.CytoscapeJS = reader.ReadToEnd();
                }
                var cytoscapejsDagreName = "LegendsViewer.Controls.HTML.Scripts.cytoscape-dagre.js";
                using (Stream stream = assembly.GetManifestResourceStream(cytoscapejsDagreName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    Controls.HTMLPrinter.CytoscapeJSDagre = reader.ReadToEnd();
                }
                var familygraphjsName = "LegendsViewer.Controls.HTML.Scripts.familygraph.js";
                using (Stream stream = assembly.GetManifestResourceStream(familygraphjsName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    Controls.HTMLPrinter.FamilyGraphJS = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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

        private void XMLClick(object sender, EventArgs e)
        {
            AttemptLoadFrom(GetFile("Legends XML or Archive | *.xml;*.7z;*.zip "));
        }

        public void AttemptLoadFrom(string file)
        {
            if (!file.EndsWith(".xml") && file != "")
                Extract(file);
            else if (file.EndsWith(".xml"))
            {
                LocateOtherFiles(file);
            }
            Load();
        }

        private void HistoryClick(object sender, EventArgs e)
        {
            string historyFile = GetFile("History Text | *.txt");
            if (historyFile != "")
            {
                HistoryText.Text = historyFile;
                HistoryState = FileState.Ready;
            }
            Load();
        }

        private void SitesClick(object sender, EventArgs e)
        {
            string sitesFile = GetFile("Sites and Pops Text | *.txt");
            if (sitesFile != "")
            {
                SitesText.Text = sitesFile;
                SitesState = FileState.Ready;
            }
            Load();
        }

        private void MapClick(object sender, EventArgs e)
        {
            string mapFile = GetFile("Map Image | *.jpg;*.jpeg;*.bmp;*.png;|All Files|*.*");
            if (mapFile != "")
            {
                MapText.Text = mapFile;
                MapState = FileState.Ready;
            }
            Load();
        }

        private void XmlPlusClick(object sender, EventArgs e)
        {
            string xmlFile = GetFile("Legends XML Plus|*legends_plus.xml;|Legends XML Plus|*.xml;|All Files|*.*");
            if (string.IsNullOrEmpty(xmlFile))
            {
                XMLPlusText.Text = xmlFile;
                XMLPlusState = FileState.Ready;
            }
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

            SaveDirectory = directory;
            SaveID = region;

            if (File.Exists(directory + region + "-legends.xml"))
            {
                XMLText.Text = directory + region + "-legends.xml";
                XMLState = FileState.Ready;
            }
            else
            {
                XMLState = FileState.NotReady;
            }
            if (File.Exists(directory + region + "-world_history.txt"))
            {
                HistoryText.Text = directory + region + "-world_history.txt";
                HistoryState = FileState.Ready;
            }
            else
            {
                HistoryState = FileState.NotReady;
            }
            if (File.Exists(directory + region + "-world_sites_and_pops.txt"))
            {
                SitesText.Text = directory + region + "-world_sites_and_pops.txt";
                SitesState = FileState.Ready;
            }
            else
            {
                SitesState = FileState.NotReady;
            }
            List<string> imageFiles = Directory.GetFiles(directory).Where(file => file.Contains("world") && file.Contains(region) && (file.EndsWith(".bmp") || file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg"))).ToList();
            if (imageFiles.Count == 1)
            {
                MapText.Text = imageFiles.First();
                MapState = FileState.Ready;
            }
            if (File.Exists(directory + region + "-legends_plus.xml"))
            {
                XMLPlusText.Text = directory + region + "-legends_plus.xml";
                XMLPlusState = FileState.Ready;
            }
            else
                XMLPlusState = FileState.NotReady;
        }

        private void Load()
        {
            if (XMLState != FileState.Ready || HistoryState != FileState.Ready || SitesState != FileState.Ready || MapState != FileState.Ready)
                return;

            if (!(File.Exists(XMLText.Text) && File.Exists(HistoryText.Text) && File.Exists(SitesText.Text) && File.Exists(MapText.Text)))
                return;

            string xmlPlusText = File.Exists(XMLPlusText.Text) ? XMLPlusText.Text : "";
            string[] files = new string[] { XMLText.Text, HistoryText.Text, SitesText.Text, MapText.Text, xmlPlusText };

            Working = true;
            XMLButton.Enabled = HistoryButton.Enabled = SitesButton.Enabled = MapButton.Enabled = XMLPlusButton.Enabled = false;
            LogText.Clear();
            StatusLabel.Text = "Loading...";
            StatusLabel.ForeColor = Color.Blue;

            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    var args = new DoWorkEventArgs(files);
            //    load_DoWork(this, args);
            //    load_RunWorkerCompleted(this, new RunWorkerCompletedEventArgs(args.Result, null, false));
            //}
            //else
            //{
                BackgroundWorker load = new BackgroundWorker();
                load.DoWork += load_DoWork;
                load.RunWorkerCompleted += load_RunWorkerCompleted;

                load.RunWorkerAsync(files);
                while (load.IsBusy) Application.DoEvents();
            //}

            Working = false;
            XMLButton.Enabled = HistoryButton.Enabled = SitesButton.Enabled = MapButton.Enabled = XMLPlusButton.Enabled = true;
            XMLState = HistoryState = SitesState = MapState = XMLPlusState = FileState.Default;
        }

        private void load_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = e.Argument as string[];
            try
            {
                e.Result = new World(files[0], files[1], files[2], files[3], files[4]);
            }
            catch (XmlException)
            {
                string safeXMLFile = XMLParser.SafeXMLFile(files[0]);
                if (safeXMLFile != null)
                {
                    if (safeXMLFile != files[0])
                    {
                        ExtractedFiles.Add(safeXMLFile);
                    }
                    e.Result = new World(safeXMLFile, files[1], files[2], files[3], files[4]);
                }
                else
                {
                    e.Result = null;
                }
            }
        }

        public event EventHandler<EventArgs<World>> AfterLoad;

        private void load_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (string delete in ExtractedFiles)
            {
                File.Delete(delete);
            }
            ExtractedFiles.Clear();

            if (e.Result != null)
            {
                if (e.Error != null)
                {
                    LogText.AppendText(e.Error.Message + "\n" + e.Error.StackTrace);
                    StatusLabel.Text = "ERROR!";
                    StatusLabel.ForeColor = Color.Red;
                }
                else
                {
                    AfterLoad?.Invoke(this, new EventArgs<World>(e.Result as World));
                }
            }
            else
            {
                LogText.AppendText("Repair of corrupt XML cancelled!");
                StatusLabel.Text = "ERROR!";
                StatusLabel.ForeColor = Color.Red;
            }

        }

        private void Extract(string file)
        {
            XMLButton.Enabled = HistoryButton.Enabled = SitesButton.Enabled = MapButton.Enabled = false;
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
                var noCase = StringComparison.OrdinalIgnoreCase;
                if (extractor.ArchiveFileNames.Count(file => file.EndsWith("legends.xml", noCase)) > 1 || extractor.ArchiveFileNames.Count(file => file.EndsWith("legends.xml", noCase)) == 0)
                    throw new Exception("Not enough or too many XML Files");
                if (extractor.ArchiveFileNames.Count(file => file.EndsWith(".world_history.txt", noCase)) > 1 || extractor.ArchiveFileNames.Count(file => file.EndsWith("-world_history.txt", noCase)) == 0)
                    throw new Exception("Not enough or too many World History Text Files");
                if (extractor.ArchiveFileNames.Count(file => file.EndsWith("-world_sites_and_pops.txt", noCase)) > 1 || extractor.ArchiveFileNames.Count(file => file.EndsWith("-world_sites_and_pops.txt", noCase)) == 0)
                    throw new Exception("Not enough or too many Site & Pops Text Files");

                var extns = new[] { ".bmp", ".png", ".jpg", ".jpeg" };
                if (extractor.ArchiveFileNames.Count(file => extns.Contains(Path.GetExtension(file).ToLower())) == 0)
                    throw new Exception("No map image found.");

                string outputDirectory = new FileInfo(extractor.FileName).Directory.FullName;

                string history = extractor.ArchiveFileNames.Single(file => file.EndsWith("-world_history.txt"));
                if (File.Exists(Path.Combine(outputDirectory, history))) throw new Exception(history + " already exists.");
                extractor.ExtractFiles(outputDirectory, history);
                ExtractedFiles.Add(Path.Combine(outputDirectory, history));

                string sites = extractor.ArchiveFileNames.Single(file => file.EndsWith("-world_sites_and_pops.txt"));
                if (File.Exists(Path.Combine(outputDirectory, sites))) throw new Exception(sites + " already exists.");
                extractor.ExtractFiles(outputDirectory, sites);
                ExtractedFiles.Add(Path.Combine(outputDirectory, sites));

                string map = "";

                var imgfiles = extractor.ArchiveFileNames.Where(file => extns.Contains(Path.GetExtension(file).ToLower())).ToList();
                if (imgfiles.Count() == 1)
                    map = imgfiles[0];
                else
                {
                    if (imgfiles.Count(file => Path.GetFileNameWithoutExtension(file).ToLower().EndsWith("world_map")) == 1)
                        map = imgfiles.Single(file => Path.GetFileNameWithoutExtension(file).ToLower().EndsWith("world_map"));
                    else
                    {
                        var fileSelect = new dlgFileSelect(imgfiles) { Text = "Select Base Map" };
                        fileSelect.ShowDialog();
                        if (fileSelect.SelectedFile == "") throw new Exception("No map file selected.");
                        if (File.Exists(fileSelect.SelectedFile)) { MessageBox.Show(fileSelect.SelectedFile + " already exists."); return; }
                        map = fileSelect.SelectedFile;
                    }
                }

                if (File.Exists(Path.Combine(outputDirectory, map))) throw new Exception(map + " already exists.");
                extractor.ExtractFiles(outputDirectory, map);
                ExtractedFiles.Add(Path.Combine(outputDirectory, map));

                string xmlPlus = extractor.ArchiveFileNames.FirstOrDefault(file => file.EndsWith("legends_plus.xml", noCase));
                if (!string.IsNullOrEmpty(xmlPlus))
                {
                    if (File.Exists(Path.Combine(outputDirectory, xmlPlus))) throw new Exception(xmlPlus + " already exists.");
                    extractor.ExtractFiles(outputDirectory, xmlPlus);
                    ExtractedFiles.Add(Path.Combine(outputDirectory, xmlPlus));
                }
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
                {
                    File.Delete(delete);
                }
                ExtractedFiles.Clear();
                Working = false;
            }
            else
            {
                XMLText.Text = ExtractedFiles[0];
                XMLState = FileState.Ready;
                HistoryText.Text = ExtractedFiles[1];
                HistoryState = FileState.Ready;
                SitesText.Text = ExtractedFiles[2];
                SitesState = FileState.Ready;
                MapText.Text = ExtractedFiles[3];
                MapState = FileState.Ready;
                XMLPlusText.Text = ExtractedFiles[4];
                XMLPlusState = FileState.Ready;
            }
        }

        private enum FileState
        {
            Default,
            Ready,
            NotReady
        }
    }
}
