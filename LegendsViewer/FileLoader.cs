using LegendsViewer.Controls;
using LegendsViewer.Legends;
using SevenZip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

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

        private const string FILE_IDENTIFIER_LEGENDS_XML = "-legends.xml";
        private const string FILE_IDENTIFIER_WORLD_HISTORY_TXT = "-world_history.txt";
        private const string FILE_IDENTIFIER_WORLD_MAP_BMP = "-world_map.bmp";
        private const string FILE_IDENTIFIER_WORLD_SITES_AND_POPS = "-world_sites_and_pops.txt";
        private const string FILE_IDENTIFIER_LEGENDS_PLUS_XML = "-legends_plus.xml";

        public static string SaveDirectory { get; set; }
        public static string RegionID { get; set; }

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
            {
                SevenZipBase.SetLibraryPath("7z64.dll");
            }
        }

        private string GetFile(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = filter
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return "";
            }
        }

        private void XMLClick(object sender, EventArgs e)
        {
            AttemptLoadFrom(GetFile("Legends XML or Archive | *.xml; *.zip; *.7z;  "));
        }

        public void AttemptLoadFrom(string file)
        {
            if (!file.EndsWith(".xml") && file != "")
            {
                Extract(file);
            }
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
            FileInfo fileInfo = new FileInfo(xmlFile);
            string directory = fileInfo.DirectoryName;
            string region = "";
            if (fileInfo.Name.Contains(FILE_IDENTIFIER_LEGENDS_XML))
            {
                region = fileInfo.Name.Replace(FILE_IDENTIFIER_LEGENDS_XML, "");
            }
            else if(fileInfo.Name.Contains(FILE_IDENTIFIER_LEGENDS_PLUS_XML))
            {
                region = fileInfo.Name.Replace(FILE_IDENTIFIER_LEGENDS_PLUS_XML, "");
            }
            else
            {
                return;
            }

            SaveDirectory = directory;
            RegionID = region;

            string path_legends_xml = Path.Combine(directory, region + FILE_IDENTIFIER_LEGENDS_XML);
            string path_world_history_txt = Path.Combine(directory, region + FILE_IDENTIFIER_WORLD_HISTORY_TXT);
            string path_world_sites_and_pops_txt = Path.Combine(directory, region + FILE_IDENTIFIER_WORLD_SITES_AND_POPS);
            string path_world_map_bmp = Path.Combine(directory, region + FILE_IDENTIFIER_WORLD_MAP_BMP);
            string path_legends_plus_xml = Path.Combine(directory, region + FILE_IDENTIFIER_LEGENDS_PLUS_XML);

            if (File.Exists(path_legends_xml))
            {
                XMLText.Text = path_legends_xml;
                XMLState = FileState.Ready;
            }
            else
            {
                XMLState = FileState.NotReady;
            }
            if (File.Exists(path_world_history_txt))
            {
                HistoryText.Text = path_world_history_txt;
                HistoryState = FileState.Ready;
            }
            else
            {
                HistoryState = FileState.NotReady;
            }
            if (File.Exists(path_world_sites_and_pops_txt))
            {
                SitesText.Text = path_world_sites_and_pops_txt;
                SitesState = FileState.Ready;
            }
            else
            {
                SitesState = FileState.NotReady;
            }
            if (File.Exists(path_world_map_bmp))
            {
                MapText.Text = path_world_map_bmp;
                MapState = FileState.Ready;
            }
            else
            {
                string imageFile = Directory.GetFiles(directory)
                    .FirstOrDefault(file => 
                        file.Contains(region) && 
                        (file.EndsWith(".bmp") || 
                         file.EndsWith(".png") || 
                         file.EndsWith(".jpg") || 
                         file.EndsWith(".jpeg")));
                if (!string.IsNullOrEmpty(imageFile))
                {
                    MapText.Text = imageFile;
                    MapState = FileState.Ready;
                }
                else
                {
                    MapState = FileState.NotReady;
                }
            }
            if (File.Exists(path_legends_plus_xml))
            {
                XMLPlusText.Text = path_legends_plus_xml;
                XMLPlusState = FileState.Ready;
            }
            else
            {
                XMLPlusState = FileState.NotReady;
            }
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

            BackgroundWorker load = new BackgroundWorker();
            load.DoWork += load_DoWork;
            load.RunWorkerCompleted += load_RunWorkerCompleted;

            load.RunWorkerAsync(files);
            while (load.IsBusy) Application.DoEvents();

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
                string repairedXMLFile = RepairXmlFile(files[0]);
                if (repairedXMLFile != null)
                {
                    if (repairedXMLFile != files[0])
                    {
                        ExtractedFiles.Add(repairedXMLFile);
                    }
                    e.Result = new World(repairedXMLFile, files[1], files[2], files[3], files[4]);
                }
                else
                {
                    e.Result = null;
                }
            }
        }

        public static string RepairXmlFile(string xmlFile)
        {
            DialogResult response =
                MessageBox.Show("There was an error loading this XML file! Do you wish to attempt a repair?",
                    "Error loading XML", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (response == DialogResult.Yes)
            {
                string currentLine = String.Empty;
                string safeFile = Path.GetTempFileName();
                using (StreamReader inputReader = new StreamReader(xmlFile))
                {
                    using (StreamWriter outputWriter = File.AppendText(safeFile))
                    {
                        while (null != (currentLine = inputReader.ReadLine()))
                        {
                            outputWriter.WriteLine(Regex.Replace(currentLine, "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]",
                                string.Empty));
                        }
                    }
                }
                DialogResult overwrite =
                    MessageBox.Show(
                        "Repair completed. Would you like to overwrite the original file with the repaired version? (Note: No effect if opened from an archive)",
                        "Repair Completed", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (overwrite == DialogResult.Yes)
                {
                    File.Delete(xmlFile);
                    File.Copy(safeFile, xmlFile);
                    return xmlFile;
                }
                return safeFile;
            }
            return null;
        }

        public event EventHandler<EventArgs<World>> AfterLoad;

        private void load_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

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
            foreach (string delete in ExtractedFiles)
            {
                File.Delete(delete);
            }
            ExtractedFiles.Clear();
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
                string file_name_legends_xml = extractor.ArchiveFileNames.FirstOrDefault(file => file.EndsWith(FILE_IDENTIFIER_LEGENDS_XML, StringComparison.OrdinalIgnoreCase));
                if (string.IsNullOrEmpty(file_name_legends_xml))
                {
                    throw new FileNotFoundException($"Could not find a 'region{FILE_IDENTIFIER_LEGENDS_XML}' file.");
                }
                string region = file_name_legends_xml.Replace(FILE_IDENTIFIER_LEGENDS_XML, "");

                string file_name_world_history_txt = extractor.ArchiveFileNames.FirstOrDefault(file => file.Equals(region + FILE_IDENTIFIER_WORLD_HISTORY_TXT));
                if (string.IsNullOrEmpty(file_name_world_history_txt))
                {
                    throw new FileNotFoundException($"Could not find a 'region{FILE_IDENTIFIER_WORLD_HISTORY_TXT}' file.");
                }
                string file_name_world_sites_and_pops_txt = extractor.ArchiveFileNames.FirstOrDefault(file => file.Equals(region + FILE_IDENTIFIER_WORLD_SITES_AND_POPS));
                if (string.IsNullOrEmpty(file_name_world_sites_and_pops_txt))
                {
                    throw new FileNotFoundException($"Could not find a 'region{FILE_IDENTIFIER_WORLD_SITES_AND_POPS}' file.");
                }
                string file_name_world_map_bmp = extractor.ArchiveFileNames.FirstOrDefault(file => file.Contains(region + FILE_IDENTIFIER_WORLD_MAP_BMP));
                if (string.IsNullOrEmpty(file_name_world_map_bmp))
                {
                    var extns = new[] { ".bmp", ".png", ".jpg", ".jpeg" };
                    file_name_world_map_bmp = extractor.ArchiveFileNames.FirstOrDefault(file => file.Contains(region) && extns.Contains(Path.GetExtension(file).ToLower()));
                }
                if (string.IsNullOrEmpty(file_name_world_map_bmp))
                {
                    throw new FileNotFoundException($"Could not find a 'region{FILE_IDENTIFIER_WORLD_MAP_BMP}' file.");
                }
                string file_name_legends_plus_xml = extractor.ArchiveFileNames.FirstOrDefault(file => file.Equals(region + FILE_IDENTIFIER_LEGENDS_PLUS_XML));

                string outputDirectory = new FileInfo(extractor.FileName).DirectoryName;

                extractFile(extractor, outputDirectory, file_name_legends_xml);
                extractFile(extractor, outputDirectory, file_name_world_history_txt);
                extractFile(extractor, outputDirectory, file_name_world_sites_and_pops_txt);
                extractFile(extractor, outputDirectory, file_name_world_map_bmp);
                if (!string.IsNullOrEmpty(file_name_legends_plus_xml))
                {
                    extractFile(extractor, outputDirectory, file_name_legends_plus_xml);
                }
            }
        }

        private void extractFile(SevenZipExtractor extractor, string outputDirectory, string fileName)
        {
            if (!File.Exists(Path.Combine(outputDirectory, fileName)))
            {
                extractor.ExtractFiles(outputDirectory, fileName);
            }
            ExtractedFiles.Add(Path.Combine(outputDirectory, fileName));
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
                if (ExtractedFiles.Count == 5)
                {
                    XMLPlusText.Text = ExtractedFiles[4];
                    XMLPlusState = FileState.Ready;
                }
                else
                {
                    XMLPlusState = FileState.NotReady;
                }
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