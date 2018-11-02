﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Jdenticon;
using LegendsViewer.Controls;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class World : IDisposable
    {
        private readonly BackgroundWorker _worker;
        public static readonly Dictionary<string, Color> MainRaces = new Dictionary<string, Color>();

        public string Name;
        public readonly List<WorldRegion> Regions = new List<WorldRegion>();
        public readonly List<UndergroundRegion> UndergroundRegions = new List<UndergroundRegion>();
        public readonly List<Landmass> Landmasses = new List<Landmass>();
        public readonly List<MountainPeak> MountainPeaks = new List<MountainPeak>();
        public readonly List<Site> Sites = new List<Site>();
        public readonly List<HistoricalFigure> HistoricalFigures = new List<HistoricalFigure>();
        public readonly List<Entity> Entities = new List<Entity>();
        public List<War> Wars;
        public List<Battle> Battles;
        public List<BeastAttack> BeastAttacks;
        public readonly List<Era> Eras = new List<Era>();
        public readonly List<Artifact> Artifacts = new List<Artifact>();
        public readonly List<WorldConstruction> WorldConstructions = new List<WorldConstruction>();
        public readonly List<PoeticForm> PoeticForms = new List<PoeticForm>();
        public readonly List<MusicalForm> MusicalForms = new List<MusicalForm>();
        public readonly List<DanceForm> DanceForms = new List<DanceForm>();
        public readonly List<WrittenContent> WrittenContents = new List<WrittenContent>();
        public readonly List<Structure> Structures = new List<Structure>();
        public readonly List<WorldEvent> Events = new List<WorldEvent>();
        public readonly List<EventCollection> EventCollections = new List<EventCollection>();
        public readonly List<DwarfObject> PlayerRelatedObjects = new List<DwarfObject>();
        public readonly List<EntityPopulation> EntityPopulations = new List<EntityPopulation>();
        public readonly List<Population> SitePopulations = new List<Population>();
        public List<Population> CivilizedPopulations = new List<Population>();
        public List<Population> OutdoorPopulations = new List<Population>();
        public List<Population> UndergroundPopulations = new List<Population>();

        public readonly Dictionary<string, List<HistoricalFigure>> Breeds = new Dictionary<string, List<HistoricalFigure>>();

        public readonly StringBuilder Log;
        public readonly ParsingErrors ParsingErrors;

        public Bitmap Map, PageMiniMap, MiniMap;
        public readonly List<Era> TempEras = new List<Era>();
        public bool FilterBattles = true;

        private readonly List<HistoricalFigure> _hFtoHfLinkHFs = new List<HistoricalFigure>();
        private readonly List<Property> _hFtoHfLinks = new List<Property>();

        private readonly List<HistoricalFigure> _hFtoEntityLinkHFs = new List<HistoricalFigure>();
        private readonly List<Property> _hFtoEntityLinks = new List<Property>();

        private readonly List<HistoricalFigure> _hFtoSiteLinkHFs = new List<HistoricalFigure>();
        private readonly List<Property> _hFtoSiteLinks = new List<Property>();

        private readonly List<HistoricalFigure> _reputationHFs = new List<HistoricalFigure>();
        private readonly List<Property> _reputations = new List<Property>();

        private readonly List<Entity> _entityEntityLinkEntities = new List<Entity>();// legends_plus.xml
        private readonly List<Property> _entityEntityLinks = new List<Property>();// legends_plus.xml

        public World(BackgroundWorker worker, string xmlFile, string historyFile, string sitesAndPopulationsFile, string mapFile, string xmlPlusFile)
        {
            _worker = worker;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            MainRaces.Clear();
            ParsingErrors = new ParsingErrors();
            Log = new StringBuilder();

            CreateUnknowns();

            XmlParser xml = new XmlParser(worker, this, xmlFile);
            xml.Parse();

            HistoryParser history = new HistoryParser(worker, this, historyFile);
            Log.Append(history.Parse());
            SitesAndPopulationsParser sitesAndPopulations = new SitesAndPopulationsParser(worker, this, sitesAndPopulationsFile);
            sitesAndPopulations.Parse();

            _worker.ReportProgress(0, "\nResolving Links between...");
            ProcessHFtoEntityLinks();
            ResolveHfToEntityPopulation();
            ResolveStructureProperties();
            ResolveMountainPeakToRegionLinks();
            ResolveSiteToRegionLinks();
            ResolveArtifactProperties();
            ResolveArtformEventsProperties();

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

            _worker.ReportProgress(0, "\nGenerating Graphics...");
            GenerateCivIdenticons();
            GenerateMaps(mapFile);

            Log.AppendLine(ParsingErrors.Print());

            sw.Stop();
            Log.AppendLine($"Duration: {sw.Elapsed.Seconds + sw.Elapsed.Minutes * 60} secs, {sw.Elapsed.Milliseconds:D3} ms ");
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
            _worker.ReportProgress(0, "... Civilization Identicons");
            List<Entity> civs = Entities.Where(entity => entity.IsCiv).ToList();
            List<string> races = Entities.Where(entity => entity.IsCiv).GroupBy(entity => entity.Race).Select(entity => entity.Key).OrderBy(entity => entity).ToList();

            //Calculates color
            //Creates a variety of colors
            //Races 1 to 6 get a medium color
            //Races 7 to 12 get a light color
            //Races 13 to 18 get a dark color
            //19+ reduced color variance
            int maxHue = 300;
            int colorVariance;
            if (races.Count <= 1)
            {
                colorVariance = 0;
            }
            else if (races.Count <= 6)
            {
                colorVariance = Convert.ToInt32(Math.Floor(maxHue / Convert.ToDouble(races.Count - 1)));
            }
            else if (races.Count > 18)
            {
                colorVariance = Convert.ToInt32(Math.Floor(maxHue / (Math.Ceiling(races.Count / 3.0) - 1)));
            }
            else
            {
                colorVariance = 60;
            }

            foreach (Entity civ in civs)
            {
                int colorIndex = races.IndexOf(civ.Race);
                Color raceColor;
                if (colorIndex * colorVariance < 360)
                {
                    raceColor = Formatting.HsvToRgb(colorIndex * colorVariance, 1, 1.0);
                }
                else if (colorIndex * colorVariance < 720)
                {
                    raceColor = Formatting.HsvToRgb(colorIndex * colorVariance - 360, 0.4, 1);
                }
                else if (colorIndex * colorVariance < 1080)
                {
                    raceColor = Formatting.HsvToRgb(colorIndex * colorVariance - 720, 1, 0.4);
                }
                else
                {
                    raceColor = Color.Black;
                }

                var alpha = 176;

                if (!MainRaces.ContainsKey(civ.Race))
                {
                    MainRaces.Add(civ.Race, raceColor);
                }
                civ.LineColor = Color.FromArgb(alpha, raceColor);

                var iconStyle = new IdenticonStyle
                {
                    BackColor = Jdenticon.Rendering.Color.FromArgb(alpha, raceColor.R, raceColor.G, raceColor.B)
                };
                var identicon = Identicon.FromValue(civ.Name, 128);
                identicon.Style = iconStyle;
                civ.Identicon = identicon.ToBitmap();
                using (MemoryStream identiconStream = new MemoryStream())
                {
                    civ.Identicon.Save(identiconStream, ImageFormat.Png);
                    byte[] identiconBytes = identiconStream.GetBuffer();
                    civ.IdenticonString = Convert.ToBase64String(identiconBytes);
                }
                var small = Identicon.FromValue(civ.Name, 32);
                small.Style = iconStyle;
                var smallIdenticon = small.ToBitmap();
                using (MemoryStream smallIdenticonStream = new MemoryStream())
                {
                    smallIdenticon.Save(smallIdenticonStream, ImageFormat.Png);
                    byte[] smallIdenticonBytes = smallIdenticonStream.GetBuffer();
                    civ.SmallIdenticonString = Convert.ToBase64String(smallIdenticonBytes);
                }
                foreach (var childGroup in civ.Groups)
                {
                    childGroup.Identicon = civ.Identicon;
                    childGroup.LineColor = civ.LineColor;
                }
            }

            foreach (var entity in Entities.Where(entity => entity.Identicon == null))
            {
                var identicon = Identicon.FromValue(entity.Name, 128);
                entity.Identicon = identicon.ToBitmap();
            }
        }

        private void GenerateMaps(string mapFile)
        {
            _worker.ReportProgress(0, "... Maps");
            int biggestXCoordinate = 0;
            int biggestYCoordinates = 0;
            int[] worldSizes = { 17, 33, 65, 129, 257 };
            int worldSizeWidth = worldSizes[0];
            int worldSizeHeight = worldSizes[0];
            int tileSize = 16;
            foreach (Site site in Sites)
            {
                if (site.Coordinates.X > biggestXCoordinate)
                {
                    biggestXCoordinate = site.Coordinates.X;
                }

                if (site.Coordinates.Y > biggestYCoordinates)
                {
                    biggestYCoordinates = site.Coordinates.Y;
                }
            }

            for (int i = 0; i < worldSizes.Length - 1; i++)
            {
                if (biggestXCoordinate >= worldSizes[i])
                {
                    worldSizeWidth = worldSizes[i + 1];
                }

                if (biggestYCoordinates >= worldSizes[i])
                {
                    worldSizeHeight = worldSizes[i + 1];
                }
            }


            using (FileStream mapStream = new FileStream(mapFile, FileMode.Open))
            {
                Map = new Bitmap(mapStream);
            }

            Formatting.ResizeImage(Map, ref Map, worldSizeHeight * tileSize, worldSizeWidth * tileSize, false, false);
            Formatting.ResizeImage(Map, ref PageMiniMap, 250, 250, true);
            Formatting.ResizeImage(Map, ref MiniMap, 200, 200, true);
        }

        private void CreateUnknowns()
        {
            HistoricalFigure.Unknown = new HistoricalFigure();
        }

        #region GetWorldItemsFunctions

        public WorldRegion GetRegion(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < Regions.Count && Regions[id].Id == id)
            {
                return Regions[id];
            }
            return Regions.GetWorldObject(id);
        }
        public UndergroundRegion GetUndergroundRegion(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < UndergroundRegions.Count && UndergroundRegions[id].Id == id)
            {
                return UndergroundRegions[id];
            }
            return UndergroundRegions.GetWorldObject(id);
        }
        public HistoricalFigure GetHistoricalFigure(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < HistoricalFigures.Count && HistoricalFigures[id].Id == id)
            {
                return HistoricalFigures[id];
            }
            return HistoricalFigures.GetWorldObject(id) ?? HistoricalFigure.Unknown;
        }
        public Entity GetEntity(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < Entities.Count && Entities[id].Id == id)
            {
                return Entities[id];
            }
            return Entities.GetWorldObject(id);
        }

        public Artifact GetArtifact(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < Artifacts.Count && Artifacts[id].Id == id)
            {
                return Artifacts[id];
            }
            return Artifacts.GetWorldObject(id);
        }
        public PoeticForm GetPoeticForm(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < PoeticForms.Count && PoeticForms[id].Id == id)
            {
                return PoeticForms[id];
            }
            return PoeticForms.GetWorldObject(id);
        }
        public MusicalForm GetMusicalForm(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < MusicalForms.Count && MusicalForms[id].Id == id)
            {
                return MusicalForms[id];
            }
            return MusicalForms.GetWorldObject(id);
        }
        public DanceForm GetDanceForm(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < DanceForms.Count && DanceForms[id].Id == id)
            {
                return DanceForms[id];
            }
            return DanceForms.GetWorldObject(id);
        }
        public WrittenContent GetWrittenContent(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < WrittenContents.Count && WrittenContents[id].Id == id)
            {
                return WrittenContents[id];
            }
            return WrittenContents.GetWorldObject(id);
        }

        public EntityPopulation GetEntityPopulation(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < EntityPopulations.Count && EntityPopulations[id].Id == id)
            {
                return EntityPopulations[id];
            }
            return EntityPopulations.GetWorldObject(id);
        }

        public EventCollection GetEventCollection(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < EventCollections.Count && EventCollections[id].Id == id)
            {
                return EventCollections[id];
            }
            int min = 0;
            int max = EventCollections.Count - 1;
            while (min <= max)
            {
                int mid = min + (max - min) / 2;
                if (id > EventCollections[mid].Id)
                {
                    min = mid + 1;
                }
                else if (id < EventCollections[mid].Id)
                {
                    max = mid - 1;
                }
                else
                {
                    return EventCollections[mid];
                }
            }
            return null;
        }

        public WorldEvent GetEvent(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < Events.Count && Events[id].Id == id)
            {
                return Events[id];
            }
            int min = 0;
            int max = Events.Count - 1;
            while (min <= max)
            {
                int mid = min + (max - min) / 2;
                if (id > Events[mid].Id)
                {
                    min = mid + 1;
                }
                else if (id < Events[mid].Id)
                {
                    max = mid - 1;
                }
                else
                {
                    return Events[mid];
                }
            }
            return null;
        }

        public Structure GetStructure(int id)
        {
            if (id < 0)
            {
                return null;
            }

            if (id < Structures.Count && Structures[id].GlobalId == id)
            {
                return Structures[id];
            }
            int min = 0;
            int max = Structures.Count - 1;
            while (min <= max)
            {
                int mid = min + (max - min) / 2;
                if (id > Structures[mid].GlobalId)
                {
                    min = mid + 1;
                }
                else if (id < Structures[mid].GlobalId)
                {
                    max = mid - 1;
                }
                else
                {
                    return Structures[mid];
                }
            }
            return null;
        }

        public Site GetSite(int id)
        {
            // Sites start with id = 1 in xml instead of 0 like every other object
            if (id <= 0)
            {
                return null;
            }

            if (id <= Sites.Count && Sites[id - 1].Id == id)
            {
                return Sites[id - 1];
            }
            return Sites.GetWorldObject(id);
        }

        public WorldConstruction GetWorldConstruction(int id)
        {
            // WorldConstructions start with id = 1 in xml instead of 0 like every other object
            if (id <= 0)
            {
                return null;
            }

            if (id <= WorldConstructions.Count && WorldConstructions[id - 1].Id == id)
            {
                return WorldConstructions[id - 1];
            }
            return WorldConstructions.GetWorldObject(id);
        }

        public Era GetEra(int id)
        {
            return Eras.Find(era => era.Id == id);
        }
        #endregion

        #region AfterXMLSectionProcessing

        public void AddHFtoHfLink(HistoricalFigure hf, Property link)
        {
            _hFtoHfLinkHFs.Add(hf);
            _hFtoHfLinks.Add(link);
        }

        public void ProcessHFtoHfLinks()
        {
            for (int i = 0; i < _hFtoHfLinks.Count; i++)
            {
                Property link = _hFtoHfLinks[i];
                HistoricalFigure hf = _hFtoHfLinkHFs[i];
                HistoricalFigureLink relation = new HistoricalFigureLink(link.SubProperties, this);
                hf.RelatedHistoricalFigures.Add(relation);
            }

            _hFtoHfLinkHFs.Clear();
            _hFtoHfLinks.Clear();
        }

        public void AddHFtoEntityLink(HistoricalFigure hf, Property link)
        {
            _hFtoEntityLinkHFs.Add(hf);
            _hFtoEntityLinks.Add(link);
        }

        public void ProcessHFtoEntityLinks()
        {
            if (_hFtoEntityLinks.Any())
            {
                _worker.ReportProgress(0, "... Historical Figures and Entities");
            }
            for (int i = 0; i < _hFtoEntityLinks.Count; i++)
            {
                Property link = _hFtoEntityLinks[i];
                HistoricalFigure hf = _hFtoEntityLinkHFs[i];
                EntityLink relatedEntity = new EntityLink(link.SubProperties, this);
                if (relatedEntity.Entity != null)
                {
                    if (relatedEntity.Type != EntityLinkType.Enemy || relatedEntity.Type == EntityLinkType.Enemy && relatedEntity.Entity.IsCiv)
                    {
                        hf.RelatedEntities.Add(relatedEntity);
                    }
                }
            }

            _hFtoEntityLinkHFs.Clear();
            _hFtoEntityLinks.Clear();
        }

        public void AddHFtoSiteLink(HistoricalFigure hf, Property link)
        {
            _hFtoSiteLinkHFs.Add(hf);
            _hFtoSiteLinks.Add(link);
        }

        public void ProcessHFtoSiteLinks()
        {
            for (int i = 0; i < _hFtoSiteLinks.Count; i++)
            {
                Property link = _hFtoSiteLinks[i];
                HistoricalFigure hf = _hFtoSiteLinkHFs[i];
                SiteLink hfToSiteLink = new SiteLink(link.SubProperties, this);
                hf.RelatedSites.Add(hfToSiteLink);
                hfToSiteLink.Site?.RelatedHistoricalFigures.Add(hf);
            }

            _hFtoSiteLinkHFs.Clear();
            _hFtoSiteLinks.Clear();
        }

        public void AddEntityEntityLink(Entity entity, Property property)
        {
            _entityEntityLinkEntities.Add(entity);
            _entityEntityLinks.Add(property);
        }

        public void ProcessEntityEntityLinks()
        {
            for (int i = 0; i < _entityEntityLinkEntities.Count; i++)
            {
                Entity entity = _entityEntityLinkEntities[i];
                Property entityLink = _entityEntityLinks[i];
                entityLink.Known = true;
                var entityEntityLink = new EntityEntityLink(entityLink.SubProperties, this);
                entity.EntityLinks.Add(entityEntityLink);
            }
        }

        public void AddReputation(HistoricalFigure hf, Property link)
        {
            _reputationHFs.Add(hf);
            _reputations.Add(link);
        }

        public void ProcessReputations()
        {
            for (int i = 0; i < _reputations.Count; i++)
            {
                Property reputation = _reputations[i];
                HistoricalFigure hf = _reputationHFs[i];
                EntityReputation entityReputation = new EntityReputation(reputation.SubProperties, this);
                hf.Reputations.Add(entityReputation);
            }

            _reputationHFs.Clear();
            _reputations.Clear();
        }

        private void ResolveStructureProperties()
        {
            if (Structures.Count > 0)
            {
                _worker.ReportProgress(0, "... Sites and Structures");
            }
            foreach (Structure structure in Structures)
            {
                structure.Resolve(this);
            }
        }

        private void ResolveArtifactProperties()
        {
            if (Artifacts.Count > 0)
            {
                _worker.ReportProgress(0, "... Artifacts and Historical Figures");
            }
            foreach (var artifact in Artifacts)
            {
                artifact.Resolve(this);
            }
        }

        private void ResolveArtformEventsProperties()
        {
            _worker.ReportProgress(0, "... Artforms and Events");
            foreach (var formCreated in Events.OfType<DanceFormCreated>())
            {
                if (!string.IsNullOrWhiteSpace(formCreated.FormId))
                {
                    formCreated.ArtForm = GetDanceForm(Convert.ToInt32(formCreated.FormId));
                    formCreated.ArtForm.AddEvent(formCreated);
                }
            }
            foreach (var formCreated in Events.OfType<MusicalFormCreated>())
            {
                if (!string.IsNullOrWhiteSpace(formCreated.FormId))
                {
                    formCreated.ArtForm = GetMusicalForm(Convert.ToInt32(formCreated.FormId));
                    formCreated.ArtForm.AddEvent(formCreated);
                }
            }
            foreach (var formCreated in Events.OfType<PoeticFormCreated>())
            {
                if (!string.IsNullOrWhiteSpace(formCreated.FormId))
                {
                    formCreated.ArtForm = GetPoeticForm(Convert.ToInt32(formCreated.FormId));
                    formCreated.ArtForm.AddEvent(formCreated);
                }
            }
            foreach (var occasionEvent in Events.OfType<OccasionEvent>())
            {
                occasionEvent.ResolveArtForm();
            }
            foreach (var writtenContentComposed in Events.OfType<WrittenContentComposed>())
            {
                if (!string.IsNullOrWhiteSpace(writtenContentComposed.WrittenContentId))
                {
                    writtenContentComposed.WrittenContent = GetWrittenContent(Convert.ToInt32(writtenContentComposed.WrittenContentId));
                    writtenContentComposed.WrittenContent.AddEvent(writtenContentComposed);
                }
            }
        }

        private void ResolveMountainPeakToRegionLinks()
        {
            if (MountainPeaks.Count > 0)
            {
                _worker.ReportProgress(0, "... Mountain Peaks and Regions");
            }
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
            _worker.ReportProgress(0, "... Sites and Regions");
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

        private void ResolveHfToEntityPopulation()
        {
            if (EntityPopulations.Any(ep => ep.Entity != null))
            {
                _worker.ReportProgress(0, "... Historical Figures and Entity Populations");
                foreach (HistoricalFigure historicalFigure in HistoricalFigures.Where(hf => hf.EntityPopulationId != -1))
                {
                    historicalFigure.EntityPopulation = GetEntityPopulation(historicalFigure.EntityPopulationId);
                    if (historicalFigure.EntityPopulation != null)
                    {
                        if (historicalFigure.EntityPopulation.Member == null)
                        {
                            historicalFigure.EntityPopulation.Member = new List<HistoricalFigure>();
                        }
                        if (historicalFigure.EntityPopulation.EntityId != -1 && historicalFigure.EntityPopulation.Entity == null)
                        {
                            historicalFigure.EntityPopulation.Entity = GetEntity(historicalFigure.EntityPopulation.EntityId);
                        }
                        historicalFigure.EntityPopulation.Member.Add(historicalFigure);
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

        private void Dispose(bool disposing)
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
