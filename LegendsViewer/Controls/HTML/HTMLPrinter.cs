using System;
using System.Text;
using LegendsViewer.Legends;
using System.Drawing;
using System.Drawing.Imaging;

namespace LegendsViewer.Controls
{
    public abstract class HTMLPrinter
    {
        protected StringBuilder HTML;
        protected const string LineBreak = "</br>";
        protected const string ListItem = "<li>";
        public abstract string GetTitle();
        public abstract string Print();

        public static string LegendsCSS;

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

            if (printType == typeof(string))
                return new StringPrinter(printObject as string);

            throw new Exception("No HTML Printer for type: " + printObject.GetType().ToString());
        }

        public string GetHTMLPage()
        {
            var htmlPage = new StringBuilder();
            htmlPage.Append("<!DOCTYPE html><html><head>");
            htmlPage.Append("<title>" + GetTitle() + "</title>");
            htmlPage.Append(GetStyle());
            htmlPage.Append("</head>");
            htmlPage.Append("<body>" + Print() + "</body>");
            htmlPage.Append("</html>");
            return htmlPage.ToString();
        }

        public string GetStyle()
        {
            if (string.IsNullOrWhiteSpace(LegendsCSS))
            {
                return "<style type=\"text/css\">"
                    + "body {font-size: 0.8em;}"
                    + "a:link {text-decoration: none; color: #000000}"
                    + "a:visited {text-decoration: none; color: #000000}"
                    + "a:hover {text-decoration: underline; color: #000000}"
                    + "td {font-size: 13px;}"
                    + "ol { margin-top: 0;}"
                    + "ul { margin-top: 0;}"
                    + "img {border:none;}"
                    + "h1 {font-size: 18px;font-weight: bold;margin-top: 0px;margin-bottom: 0px;}"
                    + "h2 {font-size: 16px;font-weight: normal;margin-top: 0px;margin-bottom: 0px;}"
                    + "h3 {font-size: 14px;font-weight: normal;margin-top: 0px;margin-bottom: 0px;margin-left: 5px;}"
                    + "</style>";
            }
            else
            {
                return "<style type=\"text/css\">"+ LegendsCSS + "</style>";
            }
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
            int imageSectionCount = 5;
            Size imageSectionSize = new Size(image.Width / imageSectionCount, image.Height / imageSectionCount);
            string html = "";
            for (int row = 0; row < imageSectionCount; row++)
            {
                for (int column = 0; column < imageSectionCount; column++)
                {
                    using (Bitmap section = new Bitmap(imageSectionSize.Width, imageSectionSize.Height))
                    {
                        using (Graphics drawSection = Graphics.FromImage(section))
                        {
                            drawSection.DrawImage(image, new Rectangle(new Point(0, 0), section.Size), new Rectangle(new Point(section.Size.Width * column, section.Size.Height * row), section.Size), GraphicsUnit.Pixel);
                            html += StringToImageHTML(BitmapToString(section));
                        }
                    }
                }
                html += "</br>";
            }
            image.Dispose();
            return html;
        }


        protected string BitmapToString(Bitmap image)
        {
            string imageString;
            using (System.IO.MemoryStream miniStream = new System.IO.MemoryStream())
            {
                image.Save(miniStream, ImageFormat.Bmp);
                byte[] miniMapBytes = miniStream.GetBuffer();
                imageString = Convert.ToBase64String(miniMapBytes);
            }

            return imageString;
        }

        protected string StringToImageHTML(string image)
        {
            string html = "<img src=\"data:image/gif;base64,";
            html += image;
            html += "\" align=absmiddle />";
            return html;
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
        LoadSearch
    }

    public enum ControlOption
    {
        HTML,
        Map,
        Chart,
        Search
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
