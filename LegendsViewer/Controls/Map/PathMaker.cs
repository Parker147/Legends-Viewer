using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsViewer.Legends;
using System.Drawing;

namespace LegendsViewer.Controls
{
    public class PathMaker
    {
        public static List<List<Site>> Create(Entity civ, int year)
        {

            List<SiteNode> Sites = CreateSiteNodes(civ, year);
            //SetPathsDijkstra(Sites);
            SetPathsPrim(Sites);
            List<List<Site>> paths = new List<List<Site>>();
            foreach (SiteNode site in Sites)
                if (site.Previous != null)
                    paths.Add(new List<Site>() { site.Site, site.Previous.Site });
            return paths;

            //Straight Lines from first site
            /*List<List<Site>> paths = new List<List<Site>>();
            if (civ.SiteHistory.Count(period => period.StartYear <= year && (period.EndYear > year || period.EndYear == -1)) > 0)
            {               
                Site start = civ.SiteHistory.First(period => period.StartYear <= year && (period.EndYear > year || period.EndYear == -1)).Site;
                foreach (Entity.SitePeriod sitePeriod in civ.SiteHistory.Where((period => period.StartYear <= year && (period.EndYear > year || period.EndYear == -1) && period.Site != start)))
                {
                    List<Site> path = new List<Site>();
                    path.Insert(start);
                    path.Insert(sitePeriod.Site);
                    paths.Insert(path);
                }
            }
            return paths;*/

        }

        private class SiteNode
        {
            public double Distance;
            public SiteNode Previous;
            public Site Site;
            public List<SitePath> Paths = new List<SitePath>();
            public SiteNode(Site site) { Site = site; }
        }

        private class SitePath
        {
            public double Weight;
            public SiteNode SiteNode;
            public SitePath(double distance, SiteNode siteNode) { Weight = distance; SiteNode = siteNode; }
        }

        private static void SetPathsDijkstra(List<SiteNode> siteList)
        {
            List<SiteNode> Sites = new List<SiteNode>(siteList);
            foreach (SiteNode node in Sites)
            {
                node.Distance = double.MaxValue;
                node.Previous = null;
            }

            if (Sites.Count > 0)
                Sites.First().Distance = 0;
            while (Sites.Count > 0)
            {
                SiteNode current = Sites.First();
                foreach (SiteNode site in Sites)
                    if (site.Distance < current.Distance) current = site;
                if (current.Distance == double.MaxValue) break;
                Sites.Remove(current);


                foreach (SitePath path in current.Paths)
                {
                    double distance = current.Distance + path.Weight;
                    if (distance < path.SiteNode.Distance)
                    {
                        path.SiteNode.Distance = distance;
                        path.SiteNode.Previous = current;
                    }
                }
            }
        }

        private static void SetPathsPrim(List<SiteNode> siteList)
        {
            List<SiteNode> Sites = new List<SiteNode>(siteList);
            foreach (SiteNode node in Sites)
            {
                node.Distance = double.MaxValue;
                node.Previous = null;
            }

            SiteNode current = null;
            if (Sites.Count > 0)
            {
                Sites.First().Distance = 0;
                current = Sites.First();
            }
            while (Sites.Count > 0)
            {
                foreach (SitePath path in current.Paths)
                    if (path.Weight < path.SiteNode.Distance && Sites.Contains(path.SiteNode))
                    {
                        path.SiteNode.Distance = path.Weight;
                        path.SiteNode.Previous = current;
                    }
                Sites.Remove(current);
                current.Distance = double.MaxValue;
                foreach (SiteNode site in Sites)
                    if (site.Distance < current.Distance) current = site;
                if (current.Distance == double.MaxValue) break;
            }
        }


        private static List<SiteNode> CreateSiteNodes(Entity civ, int year)
        {
            List<SiteNode> Sites = new List<SiteNode>();
            foreach (OwnerPeriod sitePeriod in civ.SiteHistory.Where(site => ((site.StartYear == year && site.StartCause != "took over") || site.StartYear < year)
                        && (((site.EndYear >= year) || site.EndYear == -1))))
                Sites.Add(new SiteNode(sitePeriod.Site));
            foreach (SiteNode siteNode in Sites)
            {
                List<SitePath> quadrant1 = new List<SitePath>();
                List<SitePath> quadrant2 = new List<SitePath>();
                List<SitePath> quadrant3 = new List<SitePath>();
                List<SitePath> quadrant4 = new List<SitePath>();
                double quadrant1Distance, quadrant2Distance, quadrant3Distance, quadrant4Distance;
                quadrant1Distance = quadrant2Distance = quadrant3Distance = quadrant4Distance = double.MaxValue;
                foreach (SiteNode pathSite in Sites)
                {
                    Point quadrantPoint = new Point(pathSite.Site.Coordinates.X - siteNode.Site.Coordinates.X, siteNode.Site.Coordinates.Y - pathSite.Site.Coordinates.Y);
                    double distance = Math.Sqrt(Math.Pow(quadrantPoint.X, 2) + Math.Pow(quadrantPoint.Y, 2));

                    if (quadrantPoint.X > 0 && quadrantPoint.Y >= 0)
                    { //Quadrant1
                        if (distance < quadrant1Distance)
                        {
                            quadrant1.Clear(); quadrant1Distance = distance;
                        }
                        if (distance == quadrant1Distance) quadrant1.Add(new SitePath(distance, pathSite));
                    }
                    if (quadrantPoint.X <= 0 && quadrantPoint.Y > 0)
                    { //Quadrant2
                        if (distance < quadrant2Distance)
                        {
                            quadrant2.Clear(); quadrant2Distance = distance;
                        }
                        if (distance == quadrant2Distance) quadrant2.Add(new SitePath(distance, pathSite));
                    }
                    if (quadrantPoint.X < 0 && quadrantPoint.Y <= 0)
                    { //Quadrant3
                        if (distance < quadrant3Distance)
                        {
                            quadrant3.Clear(); quadrant3Distance = distance;
                        }
                        if (distance == quadrant3Distance) quadrant3.Add(new SitePath(distance, pathSite));
                    }
                    if (quadrantPoint.X >= 0 && quadrantPoint.Y < 0)
                    { //Quadrant4
                        if (distance < quadrant4Distance)
                        {
                            quadrant4.Clear(); quadrant4Distance = distance;
                        }
                        if (distance == quadrant4Distance) quadrant4.Add(new SitePath(distance, pathSite));
                    }
                }
                siteNode.Paths.AddRange(quadrant1);
                siteNode.Paths.AddRange(quadrant2);
                siteNode.Paths.AddRange(quadrant3);
                siteNode.Paths.AddRange(quadrant4);
            }

            return Sites;

        }
    }
}
