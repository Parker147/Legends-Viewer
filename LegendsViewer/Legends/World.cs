using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;
using System.Diagnostics;
using LegendsViewer.Controls;
using Jdenticon;

namespace LegendsViewer.Legends
{
    public class World : IDisposable
    {
        public static Dictionary<string, Color> MainRaces = new Dictionary<string, Color>();

        public string Name;
        public StringBuilder Log;
        public List<WorldRegion> Regions = new List<WorldRegion>();
        public List<UndergroundRegion> UndergroundRegions = new List<UndergroundRegion>();
        public List<Landmass> Landmasses = new List<Landmass>();
        public List<MountainPeak> MountainPeaks = new List<MountainPeak>();
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
        public List<WorldConstruction> WorldConstructions = new List<WorldConstruction>();
        public List<PoeticForm> PoeticForms = new List<PoeticForm>();
        public List<MusicalForm> MusicalForms = new List<MusicalForm>();
        public List<DanceForm> DanceForms = new List<DanceForm>();
        public List<WrittenContent> WrittenContents = new List<WrittenContent>();
        public List<Structure> Structures = new List<Structure>();
        public List<WorldEvent> Events = new List<WorldEvent>();
        public List<EventCollection> EventCollections = new List<EventCollection>();
        public List<Population> CivilizedPopulations = new List<Population>();
        public List<Population> SitePopulations = new List<Population>();
        public List<Population> OutdoorPopulations = new List<Population>();
        public List<Population> UndergroundPopulations = new List<Population>();
        public List<DwarfObject> PlayerRelatedObjects = new List<DwarfObject>();

        public Dictionary<string, List<HistoricalFigure>> Breeds = new Dictionary<string, List<HistoricalFigure>>();

        public ParsingErrors ParsingErrors;

        public Bitmap Map, PageMiniMap, MiniMap;
        public List<Era> TempEras = new List<Era>();
        public bool FilterBattles = true;

        private List<HistoricalFigure> HFtoHFLinkHFs = new List<HistoricalFigure>();
        private List<Property> HFtoHFLinks = new List<Property>();

        private List<HistoricalFigure> HFtoEntityLinkHFs = new List<HistoricalFigure>();
        private List<Property> HFtoEntityLinks = new List<Property>();

        private List<HistoricalFigure> HFtoSiteLinkHFs = new List<HistoricalFigure>();
        private List<Property> HFtoSiteLinks = new List<Property>();

        private List<HistoricalFigure> ReputationHFs = new List<HistoricalFigure>();
        private List<Property> Reputations = new List<Property>();

        private List<HistoricalFigure> UsedIdentityHFs = new List<HistoricalFigure>();
        private List<int> UsedIdentityIDs = new List<int>();

        private List<HistoricalFigure> CurrentIdentityHFs = new List<HistoricalFigure>();
        private List<int> CurrentIdentityIDs = new List<int>();

        private List<Entity> EntityEntityLinkEntities = new List<Entity>();// legends_plus.xml
        private List<Property> EntityEntityLinks = new List<Property>();// legends_plus.xml

        public World(string xmlFile, string historyFile, string sitesAndPopulationsFile, string mapFile, string xmlPlusFile)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            MainRaces.Clear();
            ParsingErrors = new ParsingErrors();
            Log = new StringBuilder();
            //Log.AppendLine("Start: " + DateTime.Now.ToLongTimeString());
            //Log.AppendLine();

            CreateUnknowns();

            XMLParser xml = new XMLParser(this, xmlFile);
            xml.Parse();

            if (!string.IsNullOrEmpty(xmlPlusFile))
            {
                var xmlPlus = new XMLPlusParser(this, xmlPlusFile);
                xmlPlus.Parse();
            }

            HistoryParser history = new HistoryParser(this, historyFile);
            Log.Append(history.Parse());
            SitesAndPopulationsParser sitesAndPopulations = new SitesAndPopulationsParser(this, sitesAndPopulationsFile);
            sitesAndPopulations.Parse();

            ProcessHFtoEntityLinks();
            ResolveStructureProperties();
            ResolveMountainPeakToRegionLinks();
            ResolveSiteToRegionLinks();
            ResolveHFToEntityPopulation();

            HistoricalFigure.Filters = new List<string>();
            Site.Filters = new List<string>();
            WorldRegion.Filters = new List<string>();
            UndergroundRegion.Filters = new List<string>();
            Entity.Filters = new List<string>();
            War.Filters = new List<string>();
            Battle.Filters = new List<string>();
            SiteConquered.Filters = new List<string>();
            List<string> eraFilters = new List<string>();
            foreach (var eventInfo in AppHelpers.EventInfo)
            {
                eraFilters.Add(eventInfo[0]);
            }
            Era.Filters = eraFilters;
            BeastAttack.Filters = new List<string>();
            Artifact.Filters = new List<string>();
            WrittenContent.Filters = new List<string>();
            WorldConstruction.Filters = new List<string>();
            Structure.Filters = new List<string>();

            GenerateCivIdenticons();

            GenerateMaps(mapFile);

            Log.AppendLine(ParsingErrors.Print());
            //Log.AppendLine("Finish: " + DateTime.Now.ToLongTimeString());
            sw.Stop();
            Log.AppendLine("Duration: " + string.Format("{0} secs, {1:D3} ms ", sw.Elapsed.Seconds + (sw.Elapsed.Minutes * 60), sw.Elapsed.Milliseconds));
        }

        public void AddPlayerRelatedDwarfObjects(DwarfObject dwarfObject)
        {
            if (dwarfObject == null)
            {
                return;
            }
            if (PlayerRelatedObjects.Contains(dwarfObject))
            {
                return;
            }
            PlayerRelatedObjects.Add(dwarfObject);
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
            if (races.Count <= 1)
                colorVariance = 0;
            else if (races.Count <= 6) colorVariance = Convert.ToInt32(Math.Floor(maxHue / Convert.ToDouble(races.Count - 1)));
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

                int alpha;
                if (races.Count <= 12) alpha = 175;
                else alpha = 175;

                if (!MainRaces.ContainsKey(civ.Race))
                {
                    MainRaces.Add(civ.Race, raceColor);
                }
                civ.LineColor = Color.FromArgb(alpha, raceColor);

                var iconStyle = new IdenticonStyle
                {
                    BackColor = Jdenticon.Rendering.Color.FromArgb(alpha, raceColor.R, raceColor.G, raceColor.B)
                };
                var identicon = Identicon.FromValue(civ.Name, size: 128);
                identicon.Style = iconStyle;
                civ.Identicon = identicon.ToBitmap();
                using (MemoryStream identiconStream = new MemoryStream())
                {
                    civ.Identicon.Save(identiconStream, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] identiconBytes = identiconStream.GetBuffer();
                    civ.IdenticonString = Convert.ToBase64String(identiconBytes);
                }
                var small = Identicon.FromValue(civ.Name, size: 32);
                small.Style = iconStyle;
                var smallIdenticon = small.ToBitmap();
                using (MemoryStream smallIdenticonStream = new MemoryStream())
                {
                    smallIdenticon.Save(smallIdenticonStream, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] smallIdenticonBytes = smallIdenticonStream.GetBuffer();
                    civ.SmallIdenticonString = Convert.ToBase64String(smallIdenticonBytes);
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
            int biggestXCoordinate = 0;
            int biggestYCoordinates = 0;
            int[] worldSizes = { 17, 33, 65, 129, 257 };
            int worldSizeWidth = worldSizes[0];
            int worldSizeHeight = worldSizes[0];
            int tileSize = 16;
            foreach (Site site in Sites)
            {
                if (site.Coordinates.X > biggestXCoordinate)
                    biggestXCoordinate = site.Coordinates.X;
                if (site.Coordinates.Y > biggestYCoordinates)
                    biggestYCoordinates = site.Coordinates.Y;
            }

            for (int i = 0; i < worldSizes.Length - 1; i++)
            {
                if (biggestXCoordinate >= worldSizes[i])
                    worldSizeWidth = worldSizes[i + 1];
                if (biggestYCoordinates >= worldSizes[i])
                    worldSizeHeight = worldSizes[i + 1];
            }


            using (FileStream mapStream = new FileStream(mapFile, FileMode.Open))
                Map = new Bitmap(mapStream);

            Formatting.ResizeImage(Map, ref Map, worldSizeHeight * tileSize, worldSizeWidth * tileSize, false, false);
            Formatting.ResizeImage(Map, ref PageMiniMap, 250, 250, true, true);
            Formatting.ResizeImage(Map, ref MiniMap, 200, 200, true, true);
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

        public HistoricalFigure GetHistoricalFigure(string name)
        {
            name = Formatting.InitCaps(name.Replace("'", "`"));
            int min = 0;
            int max = HistoricalFigures.Count - 1;
            while (min <= max)
            {
                int mid = min + (max - min) / 2;
                if (string.Compare(HistoricalFiguresByName[mid].Name, name, true) < 0)
                    min = mid + 1;
                else if (string.Compare(HistoricalFiguresByName[mid].Name, name, true) > 0)
                    max = mid - 1;
                else if (mid == 0 && string.Compare(HistoricalFigures[mid + 1].Name, name, true) != 0)
                    return HistoricalFiguresByName[mid];
                else if (mid == (HistoricalFiguresByName.Count() - 1) && string.Compare(HistoricalFiguresByName[mid - 1].Name, name, true) != 0)
                    return HistoricalFiguresByName[mid];
                else if (string.Compare(HistoricalFiguresByName[mid - 1].Name, name, true) != 0 && string.Compare(HistoricalFiguresByName[mid + 1].Name, name, true) != 0) //checks duplicates
                    return HistoricalFiguresByName[mid];
                else
                    throw new Exception("Duplicate Historical Figure Name: " + name);
            }
            throw new Exception("Couldn't Find Historical Figure: " + name);
        }

        public Entity GetEntity(string name)
        {
            name = Formatting.InitCaps(name);
            int min = 0;
            int max = EntitiesByName.Count - 1;
            while (min <= max)
            {
                int mid = min + (max - min) / 2;
                if (string.Compare(EntitiesByName[mid].Name, name, true) < 0)
                    min = mid + 1;
                else if (string.Compare(EntitiesByName[mid].Name, name, true) > 0)
                    max = mid - 1;
                else if (mid == 0 && string.Compare(EntitiesByName[mid + 1].Name, name, true) != 0) return EntitiesByName[mid];
                else if (mid == (EntitiesByName.Count - 1) && string.Compare(EntitiesByName[mid - 1].Name, name, true) != 0) return EntitiesByName[mid];
                else if (string.Compare(EntitiesByName[mid - 1].Name, name, true) != 0 && string.Compare(EntitiesByName[mid + 1].Name, name, true) != 0)
                    return EntitiesByName[mid];
                else
                    throw new Exception("Duplicate Entity Name: " + name);
            }
            throw new Exception("Couldn't Find Entity: " + name);
        }


        public WorldRegion GetRegion(int id)
        {
            if (id < 0) return null;
            if (id < Regions.Count && Regions[id].ID == id)
            {
                return Regions[id];
            }
            return Regions.GetWorldObject(id);
        }
        public UndergroundRegion GetUndergroundRegion(int id)
        {
            if (id < 0) return null;
            if (id < UndergroundRegions.Count && UndergroundRegions[id].ID == id)
            {
                return UndergroundRegions[id];
            }
            return UndergroundRegions.GetWorldObject(id);
        }
        public HistoricalFigure GetHistoricalFigure(int id)
        {
            if (id < 0) return null;
            if (id < HistoricalFigures.Count && HistoricalFigures[id].ID == id)
            {
                return HistoricalFigures[id];
            }
            return HistoricalFigures.GetWorldObject(id) ?? HistoricalFigure.Unknown;
        }
        public Entity GetEntity(int id)
        {
            if (id < 0) return null;
            if (id < Entities.Count && Entities[id].ID == id)
            {
                return Entities[id];
            }
            return Entities.GetWorldObject(id);
        }

        public Artifact GetArtifact(int id)
        {
            if (id < 0) return null;
            if (id < Artifacts.Count && Artifacts[id].ID == id)
            {
                return Artifacts[id];
            }
            return Artifacts.GetWorldObject(id);
        }
        public PoeticForm GetPoeticForm(int id)
        {
            if (id < 0) return null;
            if (id < PoeticForms.Count && PoeticForms[id].ID == id)
            {
                return PoeticForms[id];
            }
            return PoeticForms.GetWorldObject(id);
        }
        public MusicalForm GetMusicalForm(int id)
        {
            if (id < 0) return null;
            if (id < MusicalForms.Count && MusicalForms[id].ID == id)
            {
                return MusicalForms[id];
            }
            return MusicalForms.GetWorldObject(id);
        }
        public DanceForm GetDanceForm(int id)
        {
            if (id < 0) return null;
            if (id < DanceForms.Count && DanceForms[id].ID == id)
            {
                return DanceForms[id];
            }
            return DanceForms.GetWorldObject(id);
        }
        public WrittenContent GetWrittenContent(int id)
        {
            if (id < 0) return null;
            if (id < WrittenContents.Count && WrittenContents[id].ID == id)
            {
                return WrittenContents[id];
            }
            return WrittenContents.GetWorldObject(id);
        }

        public EntityPopulation GetEntityPopulation(int id)
        {
            if (id < 0) return null;
            if (id < EntityPopulations.Count && EntityPopulations[id].ID == id)
            {
                return EntityPopulations[id];
            }
            return EntityPopulations.GetWorldObject(id);
        }

        public EventCollection GetEventCollection(int id)
        {
            if (id < 0) return null;
            if (id < EventCollections.Count && EventCollections[id].ID == id)
            {
                return EventCollections[id];
            }
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
            if (id < 0) return null;
            if (id < Events.Count && Events[id].ID == id)
            {
                return Events[id];
            }
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

        public Structure GetStructure(int id)
        {
            if (id < 0) return null;
            if (id < Structures.Count && Structures[id].GlobalID == id)
            {
                return Structures[id];
            }
            int min = 0;
            int max = Structures.Count - 1;
            while (min <= max)
            {
                int mid = min + (max - min) / 2;
                if (id > Structures[mid].GlobalID)
                    min = mid + 1;
                else if (id < Structures[mid].GlobalID)
                    max = mid - 1;
                else
                    return Structures[mid];
            }
            return null;
        }

        public Site GetSite(int id)
        {
            // Sites start with id = 1 in xml instead of 0 like every other object
            if (id <= 0) return null;
            if (id <= Sites.Count && Sites[id - 1].ID == id)
            {
                return Sites[id - 1];
            }
            return Sites.GetWorldObject(id);
        }

        public WorldConstruction GetWorldConstruction(int id)
        {
            // WorldConstructions start with id = 1 in xml instead of 0 like every other object
            if (id <= 0) return null;
            if (id <= WorldConstructions.Count && WorldConstructions[id - 1].ID == id)
            {
                return WorldConstructions[id - 1];
            }
            return WorldConstructions.GetWorldObject(id);
        }

        public Era GetEra(int id)
        {
            return Eras.Find(era => era.ID == id);
        }
        #endregion

        #region AfterXMLSectionProcessing

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

        public void AddHFtoEntityLink(HistoricalFigure hf, Property link)
        {
            HFtoEntityLinkHFs.Add(hf);
            HFtoEntityLinks.Add(link);
        }

        public void ProcessHFtoEntityLinks()
        {
            for (int i = 0; i < HFtoEntityLinks.Count; i++)
            {
                Property link = HFtoEntityLinks[i];
                HistoricalFigure hf = HFtoEntityLinkHFs[i];
                EntityLink relatedEntity = new EntityLink(link.SubProperties, this);
                if (relatedEntity.Entity != null)
                {
                    if (relatedEntity.Type != EntityLinkType.Enemy || (relatedEntity.Type == EntityLinkType.Enemy && relatedEntity.Entity.IsCiv))
                        hf.RelatedEntities.Add(relatedEntity);
                }
            }

            HFtoEntityLinkHFs.Clear();
            HFtoEntityLinks.Clear();
        }

        public void AddHFtoSiteLink(HistoricalFigure hf, Property link)
        {
            HFtoSiteLinkHFs.Add(hf);
            HFtoSiteLinks.Add(link);
        }

        public void ProcessHFtoSiteLinks()
        {
            for (int i = 0; i < HFtoSiteLinks.Count; i++)
            {
                Property link = HFtoSiteLinks[i];
                HistoricalFigure hf = HFtoSiteLinkHFs[i];
                SiteLink hfToSiteLink = new SiteLink(link.SubProperties, this);
                hf.RelatedSites.Add(hfToSiteLink);
                if (hfToSiteLink.Site != null)
                {
                    hfToSiteLink.Site.RelatedHistoricalFigures.Add(hf);
                }
            }

            HFtoSiteLinkHFs.Clear();
            HFtoSiteLinks.Clear();
        }

        public void AddEntityEntityLink(Entity entity, Property property)
        {
            EntityEntityLinkEntities.Add(entity);
            EntityEntityLinks.Add(property);
        }

        public void ProcessEntityEntityLinks()
        {
            for (int i = 0; i < EntityEntityLinkEntities.Count; i++)
            {
                Entity entity = EntityEntityLinkEntities[i];
                Property entityLink = EntityEntityLinks[i];
                entityLink.Known = true;
                var entityEntityLink = new EntityEntityLink(entityLink.SubProperties, this);
                entity.EntityLinks.Add(entityEntityLink);
            }
        }

        public void AddReputation(HistoricalFigure hf, Property link)
        {
            ReputationHFs.Add(hf);
            Reputations.Add(link);
        }

        public void ProcessReputations()
        {
            for (int i = 0; i < Reputations.Count; i++)
            {
                Property reputation = Reputations[i];
                HistoricalFigure hf = ReputationHFs[i];
                EntityReputation entityReputation = new EntityReputation(reputation.SubProperties, this);
                hf.Reputations.Add(entityReputation);
            }

            ReputationHFs.Clear();
            Reputations.Clear();
        }

        public void AddHFCurrentIdentity(HistoricalFigure hf, int identityID)
        {
            CurrentIdentityHFs.Add(hf);
            CurrentIdentityIDs.Add(identityID);
        }

        public void ProcessHFCurrentIdentities()
        {
            for (int i = 0; i < CurrentIdentityHFs.Count; i++)
            {
                HistoricalFigure hf = CurrentIdentityHFs[i];
                int id = CurrentIdentityIDs[i];
                HistoricalFigure currentIdentity = GetHistoricalFigure(id);
                if (hf.CurrentIdentity != null && hf.CurrentIdentity != currentIdentity)
                {
                    ParsingErrors.Report("|==> Historical Figure: " + hf.Name + " \nCurrentIdentity Conflict: " + hf.CurrentIdentity.Name + "|" + currentIdentity.Name);
                }
                hf.CurrentIdentity = currentIdentity;
            }
            CurrentIdentityHFs.Clear();
            CurrentIdentityIDs.Clear();
        }

        public void AddHFUsedIdentity(HistoricalFigure hf, int identityID)
        {
            UsedIdentityHFs.Add(hf);
            UsedIdentityIDs.Add(identityID);
        }

        public void ProcessHFUsedIdentities()
        {
            for (int i = 0; i < UsedIdentityHFs.Count; i++)
            {
                HistoricalFigure hf = UsedIdentityHFs[i];
                int id = UsedIdentityIDs[i];
                HistoricalFigure usedIdentity = GetHistoricalFigure(id);
                if (hf.UsedIdentity != null && hf.UsedIdentity != usedIdentity)
                {
                    ParsingErrors.Report("|==> Historical Figure: " + hf.Name + " \nUsedIdentity Conflict: " + hf.UsedIdentity.Name + "|" + usedIdentity.Name);
                }
                hf.UsedIdentity = usedIdentity;
            }
            UsedIdentityHFs.Clear();
            UsedIdentityIDs.Clear();
        }

        private void ResolveStructureProperties()
        {
            foreach (Structure structure in Structures)
            {
                structure.Resolve(this);
            }
        }

        private void ResolveMountainPeakToRegionLinks()
        {
            foreach (MountainPeak peak in MountainPeaks)
            {
                foreach (WorldRegion region in Regions)
                {
                    if (region.Coordinates.Contains(peak.Coordinates[0]))
                    {
                        peak.Region = region;
                        region.MountainPeaks.Add(peak);
                        break;
                    }
                }
            }
        }

        private void ResolveSiteToRegionLinks()
        {
            foreach (Site site in Sites)
            {
                foreach (WorldRegion region in Regions)
                {
                    if (region.Coordinates.Contains(site.Coordinates))
                    {
                        site.Region = region;
                        region.Sites.Add(site);
                        break;
                    }
                }
            }
        }

        private void ResolveHFToEntityPopulation()
        {
            if (EntityPopulations.Any(ep => ep.Entity != null))
            {
                for (int i = 0; i < HistoricalFigures.Count; i++)
                {
                    if (HistoricalFigures[i].EntityPopulationID != -1)
                    {
                        HistoricalFigures[i].EntityPopulation = GetEntityPopulation(HistoricalFigures[i].EntityPopulationID);
                        if (HistoricalFigures[i].EntityPopulation != null)
                        {
                            if (HistoricalFigures[i].EntityPopulation.Member == null)
                            {
                                HistoricalFigures[i].EntityPopulation.Member = new List<HistoricalFigure>();
                            }
                            if (HistoricalFigures[i].EntityPopulation.EntityID != -1 && HistoricalFigures[i].EntityPopulation.Entity == null)
                            {
                                HistoricalFigures[i].EntityPopulation.Entity =
                                    GetEntity(HistoricalFigures[i].EntityPopulation.EntityID);
                            }
                            HistoricalFigures[i].EntityPopulation.Member.Add(HistoricalFigures[i]);
                        }
                    }
                }
            }
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Map.Dispose();
                MiniMap.Dispose();
                PageMiniMap.Dispose();
            }
        }
    }
}
