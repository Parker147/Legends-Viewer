using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Controls.Map;
using LegendsViewer.Legends;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;
using System.IO;
using System.Drawing;
using System;

namespace LegendsViewer.Controls
{
    class SitePrinter : HTMLPrinter
    {
        Site Site;
        World World;

        public SitePrinter(Site site, World world)
        {
            Site = site;
            World = world;
        }

        public override string GetTitle()
        {
            return Site.Name;
        }

        public override string Print()
        {
            HTML = new StringBuilder();

            HTML.AppendLine("<div class=\"container-fluid\">");

            PrintTitle();
            PrintMaps();

            HTML.AppendLine("<div class=\"row\">");
            PrintPopulations(Site.Populations);
            HTML.AppendLine("</div>");

            PrintGeographyInfo();
            PrintStructures();
            PrintWarfareInfo();
            PrintOwnerHistory();
            PrintOfficials();
            PrintConnections();

            HTML.AppendLine("<div class=\"row\">");
            PrintBeastAttacks();
            PrintDeaths();
            HTML.AppendLine("</div>");

            PrintEventLog(Site.Events, Site.Filters, Site);
            HTML.AppendLine("</div>");

            return HTML.ToString();
        }

        private void PrintConnections()
        {
            if (Site.Connections.Count > 0)
            {
                HTML.AppendLine("<div class=\"row\">");
                HTML.AppendLine("<div class=\"col-md-12\">");
                HTML.AppendLine("<b>Connections</b></br>");
                HTML.AppendLine("<ol>");
                foreach (Site connection in Site.Connections)
                {
                    HTML.AppendLine("<li>" + connection.ToLink());
                }
                HTML.AppendLine("</ol>");
                HTML.AppendLine("</div>");
                HTML.AppendLine("</div>");
            }
        }

        private void PrintOfficials()
        {
            if (Site.Officials.Count > 0)
            {
                HTML.AppendLine("<div class=\"row\">");
                HTML.AppendLine("<div class=\"col-md-12\">");
                HTML.AppendLine("<b>Officials</b></br>");
                HTML.AppendLine("<ol>");
                foreach (Site.Official official in Site.Officials)
                {
                    HTML.AppendLine("<li>" + official.HistoricalFigure.ToLink() + ", " + official.Position);
                }
                HTML.AppendLine("</ol>");
                HTML.AppendLine("</div>");
                HTML.AppendLine("</div>");
            }
        }

        private void PrintOwnerHistory()
        {
            if (Site.OwnerHistory.Count > 0)
            {
                HTML.AppendLine("<div class=\"row\">");
                HTML.AppendLine("<div class=\"col-md-12\">");
                HTML.AppendLine("<b>Owner History</b><br />");
                HTML.AppendLine("<ol>");
                foreach (OwnerPeriod owner in Site.OwnerHistory)
                {
                    string ownerString = "UNKNOWN ENTITY";
                    if (owner.Owner != null)
                    {
                        if (owner.Owner is Entity)
                        {
                            ownerString = ((Entity)owner.Owner).PrintEntity();
                        }
                        else
                        {
                            ownerString = owner.Owner.ToLink(true, Site);
                        }
                    }
                    string startyear = owner.StartYear == -1 ? "a time before time" : owner.StartYear.ToString();
                    HTML.AppendLine("<li>" + ownerString + ", " + owner.StartCause + " " + Site.ToLink(true, Site) + " in " + startyear);
                    if (owner.EndYear >= 0)
                        HTML.Append(" and it was " + owner.EndCause + " in " + owner.EndYear);
                    if (owner.Ender != null)
                    {
                        if (owner.Ender is Entity)
                        {
                            HTML.Append(" by " + ((Entity)owner.Ender).PrintEntity());
                        }
                        else
                        {
                            HTML.Append(" by " + owner.Ender.ToLink(true, Site));
                        }
                    }
                    HTML.AppendLine(".");
                }
                HTML.AppendLine("</ol>");
                HTML.AppendLine("</div>");
                HTML.AppendLine("</div>");
            }
        }

        private void PrintWarfareInfo()
        {
            if (Site.Warfare.Count(battle => !World.FilterBattles || battle.Notable) > 0)
            {
                HTML.AppendLine("<div class=\"row\">");
                HTML.AppendLine("<div class=\"col-md-12\">");
                int warfareCount = 1;
                HTML.AppendLine("<b>Warfare</b> " + MakeLink("[Load]", LinkOption.LoadSiteBattles));
                if (World.FilterBattles) HTML.Append(" (Notable)");
                HTML.Append("<table border=\"0\">");
                foreach (EventCollection warfare in Site.Warfare.Where(battle => !World.FilterBattles || battle.Notable))
                {
                    HTML.AppendLine("<tr>");
                    HTML.AppendLine("<td width=\"20\"  align=\"right\">" + warfareCount + ".</td><td width=\"10\"></td>");
                    HTML.AppendLine("<td>" + warfare.StartYear + "</td>");
                    string warfareString = warfare.ToLink();
                    if (warfareString.Contains(" as a result of"))
                        warfareString = warfareString.Insert(warfareString.IndexOf(" as a result of"), "</br>");
                    HTML.AppendLine("<td>" + warfareString + "</td>");
                    HTML.AppendLine("<td>as part of</td>");
                    HTML.AppendLine("<td>" + ((warfare.ParentCollection == null) ? "UNKNOWN" : warfare.ParentCollection.ToLink()) + "</td>");
                    HTML.AppendLine("<td>by ");
                    if (warfare.GetType() == typeof(Battle))
                    {
                        Battle battle = warfare as Battle;
                        HTML.Append(battle.Attacker.PrintEntity() + "</td>");
                        if (battle.Victor == battle.Attacker) HTML.AppendLine("<td>(V)</td>");
                        else HTML.AppendLine("<td></td>");
                        HTML.AppendLine("<td>(Deaths: " + (battle.AttackerDeathCount + battle.DefenderDeathCount) + ")</td>");
                    }
                    if (warfare.GetType() == typeof(SiteConquered)) HTML.Append((warfare as SiteConquered).Attacker.PrintEntity() + "</td>");
                    HTML.AppendLine("</tr>");
                    warfareCount++;
                }
                HTML.AppendLine("</table></br>");

                if (World.FilterBattles && Site.Warfare.Count(battle => !battle.Notable) > 0)
                {
                    HTML.AppendLine("<b>Warfare</b> (Unnotable)</br>");
                    HTML.AppendLine("<ul>");
                    HTML.AppendLine("<li>Battles: " + Site.Warfare.OfType<Battle>().Where(battle => !battle.Notable).Count());
                    HTML.AppendLine("<li>Pillagings: " + Site.Warfare.OfType<SiteConquered>().Where(conquering => conquering.ConquerType == SiteConqueredType.Pillaging).Count());
                    HTML.AppendLine("</ul>");
                }

                HTML.AppendLine("</div>");
                HTML.AppendLine("</div>");
            }
        }

        private void PrintStructures()
        {
            if (Site.Structures.Any())
            {
                HTML.AppendLine("<div class=\"row\">");
                HTML.AppendLine("<div class=\"col-md-12\">");
                HTML.AppendLine("<b>Structures</b><br/>");
                HTML.AppendLine("<ul>");
                foreach (Structure structure in Site.Structures)
                {
                    HTML.AppendLine("<li>" + structure.ToLink() + ", ");
                    if (structure.DungeonType != DungeonType.Unknown)
                    {
                        HTML.AppendLine(structure.DungeonType.GetDescription());
                    }
                    else
                    {
                        HTML.AppendLine(structure.Type.GetDescription());
                    }
                    HTML.AppendLine("</li>");
                }
                HTML.AppendLine("</ul>");
                HTML.AppendLine("</div>");
                HTML.AppendLine("</div>");
            }
        }

        private void PrintGeographyInfo()
        {
            if (Site.Region != null || !Site.Rectangle.IsEmpty)
            {
                HTML.AppendLine("<div class=\"row\">");
                HTML.AppendLine("<div class=\"col-md-12\">");
                HTML.AppendLine("<b>Geography</b><br/>");
                HTML.AppendLine("<ul>");
                if (Site.Region != null)
                {
                    HTML.AppendLine("<li>Region: " + Site.Region.ToLink() + ", " + Site.Region.Type.GetDescription() + "</li>");
                }
                if (!Site.Rectangle.IsEmpty)
                {
                    HTML.AppendLine("<li>Position: X " + Site.Rectangle.X + " Y " + Site.Rectangle.Y + "</li>");
                    if (Site.Rectangle.Width != 0 && Site.Rectangle.Height != 0)
                    {
                        HTML.AppendLine("<li>Size: " + Site.Rectangle.Width + " x " + Site.Rectangle.Height + "</li>");
                    }
                }
                HTML.AppendLine("</ul>");
                HTML.AppendLine("</div>");
                HTML.AppendLine("</div>");
            }
        }

        private void PrintMaps()
        {
            HTML.AppendLine("<div class=\"row\">");
            HTML.AppendLine("<div class=\"col-md-12\">");
            List<Bitmap> maps = MapPanel.CreateBitmaps(World, Site);
            HTML.AppendLine("<table>");
            HTML.AppendLine("<tr>");
            PrintSiteMap();
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[0]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("<td>" + MakeLink(BitmapToHTML(maps[1]), LinkOption.LoadMap) + "</td>");
            HTML.AppendLine("</tr></table></br>");
            HTML.AppendLine("</div>");
            HTML.AppendLine("</div>");
        }

        private void PrintTitle()
        {
            HTML.AppendLine("<div class=\"row\">");
            HTML.AppendLine("<div class=\"col-md-12\">");
            if (!string.IsNullOrWhiteSpace(Site.Name))
            {
                HTML.AppendLine("<h1>" + Site.UntranslatedName + ", \"" + Site.Name + "\"</h1>");
                HTML.AppendLine("<b>" + Site.ToLink(false) + " is a " + Site.Type + "</b><br /><br />");
            }
            else
            {
                HTML.AppendLine("<h1>" + Site.Type + "</h1>");
            }
            HTML.AppendLine("</div>");
            HTML.AppendLine("</div>");
        }

        private void PrintDeaths()
        {
            int deathCount = Site.Events.OfType<HFDied>().Count();
            if (deathCount > 0 || Site.Warfare.OfType<Battle>().Any())
            {
                HTML.AppendLine("<div class=\"col-md-6 col-sm-12\">");
                var popInBattle =
                    Site.Warfare.OfType<Battle>()
                        .Sum(
                            battle =>
                                battle.AttackerSquads.Sum(squad => squad.Deaths) +
                                battle.DefenderSquads.Sum(squad => squad.Deaths));
                HTML.AppendLine("<b>Deaths</b> " + LineBreak);
                if (deathCount > 100)
                {
                    HTML.AppendLine("<ul>");
                    HTML.AppendLine("<li>Historical figures died at this site: " + deathCount);
                    if (popInBattle > 0)
                    {
                        HTML.AppendLine("<li>Population died in Battle: " + popInBattle);
                    }
                    HTML.AppendLine("</ul>");
                }
                else
                {
                    HTML.AppendLine("<ol>");
                    foreach (HFDied death in Site.Events.OfType<HFDied>())
                    {
                        HTML.AppendLine("<li>" + death.HistoricalFigure.ToLink() + ", in " + death.Year + " (" + death.Cause.GetDescription() + ")");
                    }
                    if (popInBattle > 0)
                    {
                        HTML.AppendLine("<li>Population in Battle: " + popInBattle);
                    }
                    HTML.AppendLine("</ol>");
                }
                HTML.AppendLine("</div>");
            }
        }

        private void PrintBeastAttacks()
        {
            if (Site.BeastAttacks == null || Site.BeastAttacks.Count == 0)
            {
                return;
            }
            HTML.AppendLine("<div class=\"col-md-6 col-sm-12\">");
            HTML.AppendLine("<b>Beast Attacks</b>");
            HTML.AppendLine("<ol>");
            foreach (BeastAttack attack in Site.BeastAttacks)
            {
                HTML.AppendLine("<li>" + attack.StartYear + ", " + attack.ToLink(true, Site));
                if (attack.GetSubEvents().OfType<HFDied>().Any()) HTML.Append(" (Deaths: " + attack.GetSubEvents().OfType<HFDied>().Count() + ")");
            }
            HTML.AppendLine("</ol>");
            HTML.AppendLine("</div>");
        }

        private void PrintSiteMap()
        {
            if (string.IsNullOrEmpty(FileLoader.SaveDirectory) || string.IsNullOrEmpty(FileLoader.RegionID))
            {
                return;
            }
            string sitemapPath = Path.Combine(FileLoader.SaveDirectory, FileLoader.RegionID + "-site_map-" + Site.ID);
            string sitemapPathFromProcessScript = Path.Combine(FileLoader.SaveDirectory, "site_maps\\" + FileLoader.RegionID + "-site_map-" + Site.ID);
            if (File.Exists(sitemapPath + ".bmp"))
            {
                CreateSitemapBitmap(sitemapPath + ".bmp");
            }
            else if (File.Exists(sitemapPath + ".png"))
            {
                CreateSitemapBitmap(sitemapPath + ".png");
            }
            else if (File.Exists(sitemapPath + ".jpg"))
            {
                CreateSitemapBitmap(sitemapPath + ".jpg");
            }
            else if (File.Exists(sitemapPath + ".jpeg"))
            {
                CreateSitemapBitmap(sitemapPath + ".jpeg");
            }
            else if (File.Exists(sitemapPathFromProcessScript + ".bmp"))
            {
                CreateSitemapBitmap(sitemapPathFromProcessScript + ".bmp");
            }
            else if (File.Exists(sitemapPathFromProcessScript + ".png"))
            {
                CreateSitemapBitmap(sitemapPathFromProcessScript + ".png");
            }
            else if (File.Exists(sitemapPathFromProcessScript + ".jpg"))
            {
                CreateSitemapBitmap(sitemapPathFromProcessScript + ".jpg");
            }
            else if (File.Exists(sitemapPathFromProcessScript + ".jpeg"))
            {
                CreateSitemapBitmap(sitemapPathFromProcessScript + ".jpeg");
            }
        }

        private void CreateSitemapBitmap(string sitemapPath)
        {
            Site.SiteMapPath = sitemapPath;
            Bitmap sitemap = null;
            Bitmap Map = null;
            using (FileStream mapStream = new FileStream(sitemapPath, FileMode.Open))
            {
                Map = new Bitmap(mapStream);
            }
            if (Map != null)
            {
                Formatting.ResizeImage(Map, ref sitemap, 250, 250, true, true);
            }
            if (sitemap != null)
            {
                string htmlImage = BitmapToHTML(sitemap);
                //string mapLink = MakeFileLink(htmlImage, sitemapPath);
                string mapLink = MakeLink(htmlImage, LinkOption.LoadSiteMap);
                HTML.AppendLine("<td>" + mapLink + "</td>");
            }
        }
    }
}
