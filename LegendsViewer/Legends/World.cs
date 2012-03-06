using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Docuverse.Identicon;

namespace LegendsViewer.Legends
{

        public class World
        {
            public string Name;
            public StringBuilder Log;
            public List<WorldRegion> Regions = new List<WorldRegion>();
            public List<UndergroundRegion> UndergroundRegions = new List<UndergroundRegion>();
            public List<Site> Sites = new List<Site>();
            public List<HistoricalFigure> HistoricalFigures = new List<HistoricalFigure>();
            public List<HistoricalFigure> HistoricalFiguresByName;
            public List<EntityPopulation> EntityPopulations = new List<EntityPopulation>();
            public List<Entity> Entities = new List<Entity>();
            public List<Entity> EntitiesByName;
            public List<War> Wars;
            public List<Battle> Battles;
            public List<BeastAttack> BeastAttacks;
            public List<Era> Eras = new List<Era>();
            public List<Artifact> Artifacts = new List<Artifact>();
            public List<WorldEvent> Events = new List<WorldEvent>();
            public List<EventCollection> EventCollections = new List<EventCollection>();
            public List<Population> SitePopulations = new List<Population>(), OutdoorPopulations = new List<Population>(), UndergroundPopulations = new List<Population>();

            public ParsingErrors ParsingErrors;

            public System.Drawing.Bitmap Map, PageMiniMap, MiniMap;
            public List<Era> TempEras = new List<Era>();
            public bool FilterBattles = true;
            public static List<DeathCause> DeathCauses;

            private List<HistoricalFigure> HFtoHFLinkHFs = new List<HistoricalFigure>();
            private List<Property> HFtoHFLinks = new List<Property>();

            public World(string xmlFile, string historyFile, string sitesAndPopulationsFile, string mapFile)
            {
                ParsingErrors = new ParsingErrors();
                Log = new StringBuilder();
                Log.AppendLine("Start: " + DateTime.Now.ToLongTimeString());

                CreateUnknowns();

                XMLParser xml = new XMLParser(this, xmlFile);
                xml.Parse();
                HistoryParser history = new HistoryParser(this, historyFile);
                Log.Append(history.Parse());
                SitesAndPopulationsParser sitesAndPopulations = new SitesAndPopulationsParser(this, sitesAndPopulationsFile);
                sitesAndPopulations.Parse();

                HistoricalFigure.Filters = new List<string>();
                Site.Filters = new List<string>();
                WorldRegion.Filters = new List<string>();
                UndergroundRegion.Filters = new List<string>();
                Entity.Filters = new List<string>();
                War.Filters = new List<string>();
                Battle.Filters = new List<string>();
                SiteConquered.Filters = new List<string>();
                Era.Filters = new List<string>();
                BeastAttack.Filters = new List<string>();
                Artifact.Filters = new List<string>();

                GenerateCivIdenticons();

                GenerateMaps(mapFile);

                //DeathCauses = Events.OfType<LegendsViewer.HFDied>().Select(death => death.Cause).GroupBy(death => death).Select(death => death.Key).OrderBy(death => death.GetDescription()).ToList();
                Log.AppendLine(ParsingErrors.Print());
                Log.AppendLine("Finish: " + DateTime.Now.ToLongTimeString());
            }


            private void GenerateCivIdenticons()
            {
                List<Entity> civs = Entities.Where(entity => entity.IsCiv).ToList();
                List<string> races = Entities.Where(entity => entity.IsCiv).GroupBy(entity => entity.Race).Select(entity => entity.Key).OrderBy(entity => entity).ToList();

                int identiconSeed = Events.Count;
                Random code = new Random(identiconSeed);

                //Calculates color
                //Creates a variety of colors
                //Races 1 to 6 get a medium color
                //Races 7 to 12 get a light color
                //Races 13 to 18 get a dark color
                //19+ reduced color variance
                int maxHue = 300;
                int colorVariance;
                if (races.Count <= 6) colorVariance = Convert.ToInt32(Math.Floor(maxHue / Convert.ToDouble(races.Count - 1)));
                else if (races.Count > 18) colorVariance = Convert.ToInt32(Math.Floor(maxHue / (Math.Ceiling(races.Count / 3.0) - 1)));
                else colorVariance = 60;

                foreach (Entity civ in civs)
                {
                    int colorIndex = races.IndexOf(civ.Race);
                    Color raceColor;
                    if (colorIndex * colorVariance < 360) raceColor = Formatting.HsvToRgb(colorIndex * colorVariance, 1, 1.0);
                    else if (colorIndex * colorVariance < 720) raceColor = Formatting.HsvToRgb(colorIndex * colorVariance - 360, 0.4, 1);
                    else if (colorIndex * colorVariance < 1080) raceColor = Formatting.HsvToRgb(colorIndex * colorVariance - 720, 1, 0.4);
                    else raceColor = Color.Black;


                    int identiconCode;
                    IdenticonRenderer identiconRenderer = new IdenticonRenderer();

                    identiconCode = code.Next();
                    civ.IdenticonCode = identiconCode;

                    int alpha;
                    if (races.Count <= 12) alpha = 175;
                    else alpha = 175;
                    civ.IdenticonColor = Color.FromArgb(alpha, raceColor);
                    civ.LineColor = raceColor;

                    using (MemoryStream identiconStream = new MemoryStream())
                    {
                        using (Bitmap identicon = identiconRenderer.Render(identiconCode, 64, civ.IdenticonColor))
                        {
                            identicon.Save(identiconStream, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] identiconBytes = identiconStream.GetBuffer();
                            civ.Identicon = new Bitmap(identicon);
                            civ.IdenticonString = Convert.ToBase64String(identiconBytes);
                        }
                    }

                    using (MemoryStream smallIdenticonStream = new MemoryStream())
                    {
                        using (Bitmap smallIdenticon = identiconRenderer.Render(identiconCode, 24, civ.IdenticonColor))
                        {
                            smallIdenticon.Save(smallIdenticonStream, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] smallIdenticonBytes = smallIdenticonStream.GetBuffer();
                            civ.SmallIdenticonString = Convert.ToBase64String(smallIdenticonBytes);
                        }
                    }

                    foreach (Entity group in civ.Groups)
                    {
                        group.Identicon = civ.Identicon;
                        group.SmallIdenticonString = civ.SmallIdenticonString;
                    }
                }

                Bitmap nullIdenticon = new Bitmap(64, 64);
                using (Graphics nullGraphics = Graphics.FromImage(nullIdenticon))
                {
                    using (SolidBrush nullBrush = new SolidBrush(Color.FromArgb(150, Color.Red)))
                        nullGraphics.FillRectangle(nullBrush, new Rectangle(0, 0, 64, 64));
                    Entities.Where(entity => entity.Identicon == null).ToList().ForEach(entity => entity.Identicon = nullIdenticon);
                }
            }

            private void GenerateMaps(string mapFile)
            {
                using (FileStream mapStream = new FileStream(mapFile, FileMode.Open))
                    Map = new Bitmap(mapStream);

                Formatting.ResizeImage(Map, ref PageMiniMap, 250);
                Formatting.ResizeImage(Map, ref MiniMap, 200);
            }

            private void AddEvent(WorldEvent newEvent)
            {
                Events.Add(newEvent);
            }

            private void CreateUnknowns()
            {
                HistoricalFigure.Unknown = new HistoricalFigure();
            }



            #region GetWorldItemsFunctions
            public WorldRegion GetRegion(int id)
            {
                if (id == -1) return null;
                else
                {
                    int min = 0;
                    int max = Regions.Count - 1;
                    while (min <= max)
                    {
                        int mid = min + (max - min) / 2;
                        if (id > Regions[mid].ID)
                            min = mid + 1;
                        else if (id < Regions[mid].ID)
                            max = mid - 1;
                        else
                            return Regions[mid];
                    }
                    return null;
                }
            }
            public UndergroundRegion GetUndergroundRegion(int id)
            {
                if (id == -1) return null;
                else
                {
                    int min = 0;
                    int max = UndergroundRegions.Count - 1;
                    while (min <= max)
                    {
                        int mid = min + (max - min) / 2;
                        if (id > UndergroundRegions[mid].ID)
                            min = mid + 1;
                        else if (id < UndergroundRegions[mid].ID)
                            max = mid - 1;
                        else
                            return UndergroundRegions[mid];
                    }
                    return null;
                }
            }
            public Site GetSite(int id)
            {
                if (id == -1) return null;
                else
                {
                    int min = 0;
                    int max = Sites.Count - 1;
                    while (min <= max)
                    {
                        int mid = min + (max - min) / 2;
                        if (id > Sites[mid].ID)
                            min = mid + 1;
                        else if (id < Sites[mid].ID)
                            max = mid - 1;
                        else
                            return Sites[mid];
                    }
                    return null;
                }
            }
            public HistoricalFigure GetHistoricalFigure(int id)
            {
                //TODO: Make general binary search for WorldObjects?
                if (id == -1) return null;
                else
                {
                    int min = 0;
                    int max = HistoricalFigures.Count - 1;
                    while (min <= max)
                    {
                        int mid = min + (max - min) / 2;
                        if (id > HistoricalFigures[mid].ID)
                            min = mid + 1;
                        else if (id < HistoricalFigures[mid].ID)
                            max = mid - 1;
                        else
                            return HistoricalFigures[mid];
                    }
                    return HistoricalFigure.Unknown;
                }
            }
            public HistoricalFigure GetHistoricalFigure(string name)
            {
                int min = 0;
                int max = HistoricalFigures.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (String.Compare(HistoricalFiguresByName[mid].Name, name, true) < 0)
                        min = mid + 1;
                    else if (String.Compare(HistoricalFiguresByName[mid].Name, name, true) > 0)
                        max = mid - 1;
                    else if (mid == 0 && String.Compare(HistoricalFigures[mid + 1].Name, name, true) != 0)
                        return HistoricalFiguresByName[mid];
                    else if (mid == (HistoricalFiguresByName.Count() - 1) && String.Compare(HistoricalFiguresByName[mid - 1].Name, name, true) != 0)
                        return HistoricalFiguresByName[mid];
                    else if (String.Compare(HistoricalFiguresByName[mid - 1].Name, name, true) != 0 && String.Compare(HistoricalFiguresByName[mid + 1].Name, name, true) != 0) //checks duplicates
                        return HistoricalFiguresByName[mid];
                    else
                        throw new Exception("Duplicate Historical Figure Name: " + name);
                }
                throw new Exception("Couldn't Find Historical Figure: " + name);

            }
            public Entity GetEntity(int id)
            {
                if (id == -1) return null;
                else
                {
                    int min = 0;
                    int max = Entities.Count - 1;
                    while (min <= max)
                    {
                        int mid = min + (max - min) / 2;
                        if (id > Entities[mid].ID)
                            min = mid + 1;
                        else if (id < Entities[mid].ID)
                            max = mid - 1;
                        else
                            return Entities[mid];
                    }
                    return null;
                }
            }
            public Entity GetEntity(string name)
            {
                int min = 0;
                int max = EntitiesByName.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (String.Compare(EntitiesByName[mid].Name, name, true) < 0)
                        min = mid + 1;
                    else if (String.Compare(EntitiesByName[mid].Name, name, true) > 0)
                        max = mid - 1;
                    else if (mid == 0 && String.Compare(EntitiesByName[mid + 1].Name, name, true) != 0) return EntitiesByName[mid];
                    else if (mid == (EntitiesByName.Count - 1) && String.Compare(EntitiesByName[mid - 1].Name, name, true) != 0) return EntitiesByName[mid];
                    else if (String.Compare(EntitiesByName[mid - 1].Name, name, true) != 0 && String.Compare(EntitiesByName[mid + 1].Name, name, true) != 0)
                        return EntitiesByName[mid];
                    else
                        throw new Exception("Duplicate Entity Name: " + name);
                }
                throw new Exception("Couldn't Find Entity: " + name);

            }
            public Artifact GetArtifact(int id)
            {
                if (id == -1) return null;
                else
                {
                    int min = 0;
                    int max = Artifacts.Count - 1;
                    while (min <= max)
                    {
                        int mid = min + (max - min) / 2;
                        if (id > Artifacts[mid].ID)
                            min = mid + 1;
                        else if (id < Artifacts[mid].ID)
                            max = mid - 1;
                        else
                            return Artifacts[mid];
                    }
                    return null;
                }
            }
            public EventCollection GetEventCollection(int id)
            {
                int min = 0;
                int max = EventCollections.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (id > EventCollections[mid].ID)
                        min = mid + 1;
                    else if (id < EventCollections[mid].ID)
                        max = mid - 1;
                    else
                        return EventCollections[mid];
                }
                return null;

            }
            public WorldEvent GetEvent(int id)
            {
                int min = 0;
                int max = Events.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (id > Events[mid].ID)
                        min = mid + 1;
                    else if (id < Events[mid].ID)
                        max = mid - 1;
                    else
                        return Events[mid];
                }

                return null;
            }

            public Era GetEra(int id)
            {
                if (Eras.Count(era => era.ID == id) > 0) return Eras.First(era => era.ID == id);
                else throw new Exception("No Era with ID " + id);
            }
            #endregion

            public void AddHFtoHFLink(HistoricalFigure hf, Property link)
            {
                HFtoHFLinkHFs.Add(hf);
                HFtoHFLinks.Add(link);
            }

            public void ProcessHFtoHFLinks()
            {
                for (int i = 0; i < HFtoHFLinks.Count; i++)
                {
                    Property link = HFtoHFLinks[i];
                    HistoricalFigure HF = HFtoHFLinkHFs[i];
                    HistoricalFigureLink relation = new HistoricalFigureLink(link.SubProperties, this);
                    HF.RelatedHistoricalFigures.Add(relation);
                }

                HFtoHFLinkHFs.Clear();
                HFtoHFLinks.Clear();
            }
        }

                
    }
