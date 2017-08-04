using System;
using System.Text;
using LegendsViewer.Legends;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LegendsViewer.Controls.HTML;
using LegendsViewer.Controls.HTML.Utilities;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;

namespace LegendsViewer.Controls
{
    public abstract class HTMLPrinter : IDisposable
    {
        private bool disposed;
        protected StringBuilder HTML;
        protected const string LineBreak = "</br>";
        protected const string ListItem = "<li>";

        protected HTMLPrinter()
        {
            disposed = false;
        }

        public abstract string GetTitle();
        public abstract string Print();

        private readonly List<string> temporaryFiles = new List<string>();

        public static HTMLPrinter GetPrinter(object printObject, World world)
        {
            Type printType = printObject.GetType();
            if (printType == typeof(Battle))
                return new BattlePrinter(printObject as Battle, world);
            if (printType == typeof(BeastAttack))
                return new BeastAttackPrinter(printObject as BeastAttack, world);
            if (printType == typeof(Entity))
                return new EntityPrinter(printObject as Entity, world);
            if (printType == typeof(Era))
                return new EraPrinter(printObject as Era);
            if (printType == typeof(HistoricalFigure))
                return new HistoricalFigureHTMLPrinter(printObject as HistoricalFigure, world);
            if (printType == typeof(WorldRegion))
                return new RegionPrinter(printObject as WorldRegion, world);
            if (printType == typeof(SiteConquered))
                return new SiteConqueredPrinter(printObject as SiteConquered, world);
            if (printType == typeof(Site))
                return new SitePrinter(printObject as Site, world);
            if (printType == typeof(UndergroundRegion))
                return new UndergroundRegionPrinter(printObject as UndergroundRegion, world);
            if (printType == typeof(War))
                return new WarPrinter(printObject as War, world);
            if (printType == typeof(World))
                return new WorldStatsPrinter(world);
            if (printType == typeof(Artifact))
                return new ArtifactPrinter(printObject as Artifact);
            if (printType == typeof(WorldConstruction))
                return new WorldConstructionPrinter(printObject as WorldConstruction, world);
            if (printType == typeof(WrittenContent))
                return new WrittenContentPrinter(printObject as WrittenContent, world);
            if (printType == typeof(Structure))
                return new StructurePrinter(printObject as Structure, world);
            if (printType == typeof(Landmass))
                return new LandmassPrinter(printObject as Landmass, world);
            if (printType == typeof(MountainPeak))
                return new MountainPeakPrinter(printObject as MountainPeak, world);

            if (printType == typeof(string))
                return new StringPrinter(printObject as string);

            throw new Exception("No HTML Printer for type: " + printObject.GetType().ToString());
        }

        public string GetHTMLPage()
        {
            var htmlPage = new StringBuilder();
            htmlPage.Append("<!DOCTYPE html><html><head>");
            htmlPage.Append("<title>" + GetTitle() + "</title>");
            htmlPage.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
            htmlPage.AppendLine("<script type=\"text/javascript\" src=\"" + LocalFileProvider.LocalPrefix + "WebContent/scripts/jquery-3.1.1.min.js\"></script>");
            htmlPage.AppendLine("<script type=\"text/javascript\" src=\"" + LocalFileProvider.LocalPrefix + "WebContent/scripts/jquery.dataTables.min.js\"></script>");
            htmlPage.Append("<link rel=\"stylesheet\" href=\"" + LocalFileProvider.LocalPrefix + "WebContent/styles/bootstrap.min.css\">");
            htmlPage.Append("<link rel=\"stylesheet\" href=\"" + LocalFileProvider.LocalPrefix + "WebContent/styles/font-awesome.min.css\">");
            htmlPage.Append("<link rel=\"stylesheet\" href=\"" + LocalFileProvider.LocalPrefix + "WebContent/styles/legends.css\">");
            htmlPage.Append("<link rel=\"stylesheet\" href=\"" + LocalFileProvider.LocalPrefix + "WebContent/styles/jquery.dataTables.min.css\">");
            htmlPage.Append("</head>");
            htmlPage.Append("<body>");
            htmlPage.Append(Print());
            htmlPage.Append("</body>");
            htmlPage.Append("</html>");
            return htmlPage.ToString();
        }

        protected static string Bold(string text)
        {
            return "<b>" + text + "</b>";
        }

        protected static string Font(string text, string color)
        {
            return "<font color=\"" + color + "\">" + text + "</font>";
        }

        protected enum ListType
        {
            Unordered,
            Ordered
        }

        protected void StartList(ListType listType)
        {
            switch (listType)
            {
                case ListType.Ordered:
                    HTML.AppendLine("<ol>"); break;
                case ListType.Unordered:
                    HTML.AppendLine("<ul>"); break;
            }
        }

        protected void EndList(ListType listType)
        {
            switch (listType)
            {
                case ListType.Ordered:
                    HTML.AppendLine("</ol>"); break;
                case ListType.Unordered:
                    HTML.AppendLine("</ul>"); break;
            }
        }

        protected string MakeLink(string text, LinkOption option)
        {
            return "<a href=\"" + option.ToString() + "\">" + text + "</a>";
        }

        protected string MakeFileLink(string text, string filePath)
        {
            string htmlFilePath = filePath.Replace("\\", "/");
            return "<a title=\"" + htmlFilePath + "\" href=\"file:///" + htmlFilePath + "\" target=\"_blank\">" + text + "</a>";
        }

        protected string MakeLink(string text, DwarfObject dObject, ControlOption option = ControlOption.HTML)
        {
            //<a href=\"collection#" + attack.ID + "\">" + attack.GetOrdinal(attack.Ordinal)
            string objectType = "";
            int id = 0;
            if (dObject is EventCollection)
            {
                objectType = "collection";
                id = (dObject as EventCollection).ID;
            }
            else if (dObject.GetType() == typeof(HistoricalFigure))
            {
                objectType = "hf";
                id = (dObject as HistoricalFigure).ID;
            }
            else if (dObject.GetType() == typeof(Entity))
            {
                objectType = "entity";
                id = (dObject as Entity).ID;
            }
            else if (dObject.GetType() == typeof(WorldRegion))
            {
                objectType = "region";
                id = (dObject as WorldRegion).ID;
            }
            else if (dObject.GetType() == typeof(UndergroundRegion))
            {
                objectType = "uregion";
                id = (dObject as UndergroundRegion).ID;
            }
            else if (dObject.GetType() == typeof(Site))
            {
                objectType = "site";
                id = (dObject as Site).ID;
            }
            else throw new Exception("Unhandled make link for type: " + dObject.GetType());
            string optionString = "";
            if (option != ControlOption.HTML)
                optionString = "-" + option.ToString();
            return "<a href=\"" + objectType + "#" + id + optionString + "\">" + text + "</a>";
        }

        protected string BitmapToHTML(Bitmap image)
        {
            int imageSectionCount = 2;
            Size imageSectionSize = new Size(image.Width / imageSectionCount, image.Height / imageSectionCount);
            string html = "";
            using (Bitmap section = new Bitmap(imageSectionSize.Width, imageSectionSize.Height))
            {
                using (Graphics drawSection = Graphics.FromImage(section))
                {
                    for (int row = 0; row < imageSectionCount; row++)
                    {
                        for (int column = 0; column < imageSectionCount; column++)
                        {
                            drawSection.DrawImage(image, new Rectangle(new Point(0, 0), section.Size),
                                new Rectangle(new Point(section.Size.Width * column, section.Size.Height * row),
                                    section.Size), GraphicsUnit.Pixel);
                            string tempName = "";
                            while (true)
                            {
                                tempName = Path.Combine(LocalFileProvider.RootFolder, "temp",
                                    Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + GetTitle() + ".png");
                                if (!File.Exists(tempName))
                                {
                                    break;
                                }
                            }
                            if (!Directory.Exists(Path.GetDirectoryName(tempName)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(tempName));
                            }
                            section.Save(tempName);
                            html += ImageToHTML("temp/" + Path.GetFileName(tempName));
                            temporaryFiles.Add(tempName);
                        }
                        html += "</br>";
                    }
                }
            }
            image.Dispose();
            return html;
        }

        protected string BitmapToString(Bitmap image)
        {
            string imageString;
            using (MemoryStream miniStream = new MemoryStream())
            {
                image.Save(miniStream, ImageFormat.Bmp);
                byte[] miniMapBytes = miniStream.GetBuffer();
                imageString = Convert.ToBase64String(miniMapBytes);
            }

            return imageString;
        }

        protected string ImageToHTML(string image)
        {
            string html = "<img src=\"" + LocalFileProvider.LocalPrefix + image + "\" align=absmiddle />";
            return html;
        }

        protected string Base64ToHTML(string base64)
        {
            string html = "<img src=\"data:image/gif;base64," + base64 + "\" align=absmiddle />";
            return html;
        }

        protected string SkillToString(SkillDescription desc)
        {
            string subrank = desc.Rank.ToLower().Replace(" ", string.Empty).Substring(0, 5);

            return
                "<li class='" + desc.Category
                + " " + subrank
                + "' title='" + desc.Token
                + " | " + desc.Rank
                + " | " + desc.Points
                + "'>" + desc.Rank + " " + desc.Name + "</li>";
        }

        protected void PrintPopulations(List<Population> populations)
        {
            if (!populations.Any())
            {
                return;
            }
            var mainRacePops = new List<Population>();
            var animalPeoplePops = new List<Population>();
            var visitorsPops = new List<Population>();
            var outcastsPops = new List<Population>();
            var prisonersPops = new List<Population>();
            var slavesPops = new List<Population>();
            var otherRacePops = new List<Population>();
            for (int i = 0; i < populations.Count; i++)
            {
                if (populations[i].IsMainRace)
                {
                    mainRacePops.Add(populations[i]);
                }
                else if (populations[i].IsAnimalPeople)
                {
                    animalPeoplePops.Add(populations[i]);
                }
                else if (populations[i].IsVisitors)
                {
                    visitorsPops.Add(populations[i]);
                }
                else if (populations[i].IsOutcasts)
                {
                    outcastsPops.Add(populations[i]);
                }
                else if (populations[i].IsPrisoners)
                {
                    prisonersPops.Add(populations[i]);
                }
                else if (populations[i].IsSlaves)
                {
                    slavesPops.Add(populations[i]);
                }
                else
                {
                    otherRacePops.Add(populations[i]);
                }
            }
            if (mainRacePops.Any())
            {
                HTML.AppendLine("<b>Civilized Populations</b></br>");
                HTML.AppendLine("<ul>");
                foreach (Population population in mainRacePops)
                    HTML.AppendLine("<li>" + population.Count + " " + population.Race + "</li>");
                HTML.AppendLine("</ul>");
            }
            if (animalPeoplePops.Any())
            {
                HTML.AppendLine("<b>Animal People</b></br>");
                HTML.AppendLine("<ul>");
                foreach (Population population in animalPeoplePops)
                    HTML.AppendLine("<li>" + population.Count + " " + population.Race + "</li>");
                HTML.AppendLine("</ul>");
            }
            if (visitorsPops.Any())
            {
                HTML.AppendLine("<b>Visitors</b></br>");
                HTML.AppendLine("<ul>");
                foreach (Population population in visitorsPops)
                    HTML.AppendLine("<li>" + population.Count + " " + population.Race + "</li>");
                HTML.AppendLine("</ul>");
            }
            if (outcastsPops.Any())
            {
                HTML.AppendLine("<b>Outcasts</b></br>");
                HTML.AppendLine("<ul>");
                foreach (Population population in outcastsPops)
                    HTML.AppendLine("<li>" + population.Count + " " + population.Race + "</li>");
                HTML.AppendLine("</ul>");
            }
            if (prisonersPops.Any())
            {
                HTML.AppendLine("<b>Prisoners</b></br>");
                HTML.AppendLine("<ul>");
                foreach (Population population in prisonersPops)
                    HTML.AppendLine("<li>" + population.Count + " " + population.Race + "</li>");
                HTML.AppendLine("</ul>");
            }
            if (slavesPops.Any())
            {
                HTML.AppendLine("<b>Slaves</b></br>");
                HTML.AppendLine("<ul>");
                foreach (Population population in slavesPops)
                    HTML.AppendLine("<li>" + population.Count + " " + population.Race + "</li>");
                HTML.AppendLine("</ul>");
            }
            if (otherRacePops.Any())
            {
                HTML.AppendLine("<b>Other Populations</b></br>");
                HTML.AppendLine("<ul>");
                foreach (Population population in otherRacePops)
                    HTML.AppendLine("<li>" + population.Count + " " + population.Race + "</li>");
                HTML.AppendLine("</ul>");
            }
        }

        protected void PrintEventLog(List<WorldEvent> events, List<string> filters, DwarfObject dfo)
        {
            if (!events.Any())
            {
                return;
            }
            HTML.AppendLine("<b>Event Log</b> " + MakeLink(Font("[Chart]", "Maroon"), LinkOption.LoadChart) + "<br/><br/>");
            HTML.AppendLine("<table id=\"lv_eventtable\" class=\"display\" width=\"100 %\"></table>");
            HTML.AppendLine("<script>");
            HTML.AppendLine("$(document).ready(function() {");
            HTML.AppendLine("   var dataSet = [");
            foreach (var e in events)
            {
                if (filters == null || !filters.Contains(e.Type))
                {
                    HTML.AppendLine("['" + e.Date + "','" + e.Print(true, dfo).Replace("'", "`") + "','" + e.Type + "'],");
                }
            }
            HTML.AppendLine("   ];");
            HTML.AppendLine("   $('#lv_eventtable').dataTable({");
            HTML.AppendLine("       pageLength: 100,");
            HTML.AppendLine("       data: dataSet,");
            HTML.AppendLine("       columns: [");
            HTML.AppendLine("           { title: \"Date\", type: \"string\", width: \"60px\" },");
            HTML.AppendLine("           { title: \"Description\", type: \"html\" },");
            HTML.AppendLine("           { title: \"Type\", type: \"string\" }");
            HTML.AppendLine("       ]");
            HTML.AppendLine("   });");
            HTML.AppendLine("});");
            HTML.AppendLine("</script>");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            disposed &= !temporaryFiles.Any();
            if (!disposed)
            {
                if (disposing)
                {
                }
            }
            disposed = true;
        }

        public void DeleteTemporaryFiles()
        {
            foreach (string filename in temporaryFiles)
            {
                try
                {
                    File.Delete(filename);
                }
                catch
                {
                }
            }
            temporaryFiles.Clear();
        }
    }

    public enum LinkOption
    {
        LoadHFKills,
        LoadHFBattles,
        LoadSiteBattles,
        LoadSiteDeaths,
        LoadRegionBattles,
        LoadRegionDeaths,
        LoadEntityWars,
        LoadEntitySites,
        LoadEntityLeaders,
        LoadWarBattles,
        LoadBattleAttackers,
        LoadBattleDefenders,
        LoadMap,
        LoadChart,
        LoadSearch,
        LoadSiteMap
    }

    public enum ControlOption
    {
        HTML,
        Map,
        Chart,
        Search,
        ReadMe
    }

    public class TableMaker
    {
        StringBuilder HTML;
        bool Numbered;
        int count;
        public TableMaker(bool numbered = false, int width = 0)
        {
            HTML = new StringBuilder();
            string tableStart = "<table border=\"0\"";
            if (width > 0)
                tableStart += " width=\"" + width + "\"";
            tableStart += ">";
            HTML.AppendLine(tableStart);
            Numbered = numbered;
            count = 1;
        }

        public void StartRow()
        {
            HTML.AppendLine("<tr>");
            if (Numbered)
            {
                AddData(count.ToString(), 20, TableDataAlign.Right);
                AddData("", 10);
            }
        }

        public void EndRow()
        {
            HTML.AppendLine("</tr>");
            count++;
        }

        public void AddData(string data, int width = 0, TableDataAlign align = TableDataAlign.Left)
        {
            string dataHTML = "<td";
            if (width > 0)
                dataHTML += " width=\"" + width + "\"";
            if (align != TableDataAlign.Left)
            {
                dataHTML += " align=";
                switch (align)
                {
                    case TableDataAlign.Right:
                        dataHTML += "\"right\""; break;
                    case TableDataAlign.Center:
                        dataHTML += "\"center\""; break;
                }
            }
            dataHTML += ">";
            dataHTML += data + "</td>";
            HTML.AppendLine(dataHTML);
        }

        public string GetTable()
        {
            HTML.AppendLine("</table>");
            return HTML.ToString();
        }
    }


    public enum TableDataAlign
    {
        Left,
        Right,
        Center
    }
}
