using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Docuverse.Identicon;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class World
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
            MainRaces.Clear();
            ParsingErrors = new ParsingErrors();
            Log = new StringBuilder();
            Log.AppendLine("Start: " + DateTime.Now.ToLongTimeString());
            Log.AppendLine();

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


                int identiconCode;
                IdenticonRenderer identiconRenderer = new IdenticonRenderer();

                identiconCode = code.Next();
                civ.IdenticonCode = identiconCode;

                int alpha;
                if (races.Count <= 12) alpha = 175;
                else alpha = 175;

                if (!MainRaces.ContainsKey(civ.Race))
                {
                    MainRaces.Add(civ.Race, raceColor);
                }
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
            int biggestXCoordinate = 0;
            int biggestYCoordinates = 0;
            int[] worldSizes = { 17, 33, 65, 129, 257 };
            int worldSizeWidth = worldSizes[0];
            int worldSizeHeight = worldSizes[0];
            int tileSize = 16;
            foreach (Site site in this.Sites)
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
        public WorldConstruction GetWorldConstruction(int id)
        {
            if (id == -1) return null;
            else
            {
                int min = 0;
                int max = WorldConstructions.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (id > WorldConstructions[mid].ID)
                        min = mid + 1;
                    else if (id < WorldConstructions[mid].ID)
                        max = mid - 1;
                    else
                        return WorldConstructions[mid];
                }
                return null;
            }
        }
        public PoeticForm GetPoeticForm(int id)
        {
            if (id == -1) return null;
            else
            {
                int min = 0;
                int max = PoeticForms.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (id > PoeticForms[mid].ID)
                        min = mid + 1;
                    else if (id < PoeticForms[mid].ID)
                        max = mid - 1;
                    else
                        return PoeticForms[mid];
                }
                return null;
            }
        }
        public MusicalForm GetMusicalForm(int id)
        {
            if (id == -1) return null;
            else
            {
                int min = 0;
                int max = MusicalForms.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (id > MusicalForms[mid].ID)
                        min = mid + 1;
                    else if (id < MusicalForms[mid].ID)
                        max = mid - 1;
                    else
                        return MusicalForms[mid];
                }
                return null;
            }
        }
        public DanceForm GetDanceForm(int id)
        {
            if (id == -1) return null;
            else
            {
                int min = 0;
                int max = DanceForms.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (id > DanceForms[mid].ID)
                        min = mid + 1;
                    else if (id < DanceForms[mid].ID)
                        max = mid - 1;
                    else
                        return DanceForms[mid];
                }
                return null;
            }
        }
        public WrittenContent GetWrittenContent(int id)
        {
            if (id == -1) return null;
            else
            {
                int min = 0;
                int max = WrittenContents.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (id > WrittenContents[mid].ID)
                        min = mid + 1;
                    else if (id < WrittenContents[mid].ID)
                        max = mid - 1;
                    else
                        return WrittenContents[mid];
                }
                return null;
            }
        }
        public Structure GetStructure(int id)
        {
            if (id == -1) return null;
            else
            {
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

        public EntityPopulation GetEntityPopulation(int id)
        {
            if (id == -1) return null;
            else
            {
                int min = 0;
                int max = EntityPopulations.Count - 1;
                while (min <= max)
                {
                    int mid = min + (max - min) / 2;
                    if (id > EntityPopulations[mid].ID)
                        min = mid + 1;
                    else if (id < EntityPopulations[mid].ID)
                        max = mid - 1;
                    else
                        return EntityPopulations[mid];
                }
                return null;
            }
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
                SiteLink relatedSite = new SiteLink(link.SubProperties, this);
                hf.RelatedSites.Add(relatedSite);
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
                entity.EntityLinks.Add(new EntityEntityLink(entityLink.SubProperties, this));
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
                if (hf.CurrentIdentity != null)
                    throw new Exception("Current Identity already exists.");
                hf.CurrentIdentity = GetHistoricalFigure(id);
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
                if (hf.UsedIdentity != null)
                    throw new Exception("UsedIdentity already exists.");
                hf.UsedIdentity = GetHistoricalFigure(id);
            }
            UsedIdentityHFs.Clear();
            UsedIdentityIDs.Clear();
        }

        #endregion
    }


}
