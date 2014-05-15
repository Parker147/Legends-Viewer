﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace LegendsViewer.Legends
{
    class XMLParser
    {
        World World;
        XmlTextReader XML;
        string CurrentSectionName = "";
        Section CurrentSection = Section.Unknown;
        string CurrentItemName = "";

        public XMLParser(World world, string xmlFile)
        {
            World = world;
            XML = new XmlTextReader(new StreamReader(new MemoryStream(Encoding.ASCII.GetBytes(Regex.Replace(File.ReadAllText(xmlFile), "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]", string.Empty, RegexOptions.Compiled))),Encoding.ASCII));
            XML.WhitespaceHandling = WhitespaceHandling.Significant;
        }

        public void Parse()
        {
            while (!XML.EOF)
            {
                CurrentSection = GetSectionType(XML.Name);
                if (CurrentSection == Section.Junk)
                {
                    XML.Read();
                }
                else if (CurrentSection == Section.Unknown)
                    SkipSection();
                else
                    ParseSection();
            }
            XML.Close();
        }

        private void GetSectionStart()
        {
            CurrentSectionName = "";
            if (XML.NodeType == XmlNodeType.Element)
                CurrentSectionName = XML.Name;
            CurrentSection = GetSectionType(CurrentSectionName);
        }

        private Section GetSectionType(string sectionName)
        {
            switch (sectionName)
            {
                case "artifacts": return Section.Artifacts;
                case "entities": return Section.Entities;
                case "entity_populations": return Section.EntityPopulations;
                case "historical_eras": return Section.Eras;
                case "historical_event_collections": return Section.EventCollections;
                case "historical_events": return Section.Events;
                case "historical_figures": return Section.HistoricalFigures;
                case "regions": return Section.Regions;
                case "sites": return Section.Sites;
                case "underground_regions": return Section.UndergroundRegions;
                case "world_constructions": return Section.WorldConstructions;
                case "xml":
                case "":
                case "df_world": return Section.Junk;
                default: World.ParsingErrors.Report("Unknown XML Section: " + sectionName); return Section.Unknown;
            }
        }

        private void ParseSection()
        {
            XML.ReadStartElement();
            while (XML.NodeType != XmlNodeType.EndElement)
            {
                AddItemToWorld(ParseItem());
            }
            ProcessXMLSection(CurrentSection); //Done with section, do post processing
            XML.ReadEndElement();
        }

        private void SkipSection()
        {
            string currentSectionName = XML.Name;
            XML.ReadStartElement();
            while (!(XML.NodeType == XmlNodeType.EndElement && XML.Name == currentSectionName))
            {
                XML.Read();
            }
            XML.ReadEndElement();
        }

        public List<Property> ParseItem()
        {
            CurrentItemName = XML.Name;
            XML.ReadStartElement();
            List<Property> properties = new List<Property>();
            while (XML.NodeType != XmlNodeType.EndElement && XML.Name != CurrentItemName)
            {
                properties.Add(ParseProperty());
            }
            XML.ReadEndElement();
            return properties;
        }

        private Property ParseProperty()
        {
            Property property = new Property();

            if (XML.IsEmptyElement) //Need this for bugged XML properties that only have and end element like "</deity>" for historical figures.
            {
                property.Name = XML.Name;
                XML.ReadStartElement();
                return property;
            }

            property.Name = XML.Name;
            XML.ReadStartElement();

            if (XML.NodeType == XmlNodeType.Text)
            {
                property.Value = XML.Value;
                XML.Read();
            }
            else if (XML.NodeType == XmlNodeType.Element)
            {
                while (XML.NodeType != XmlNodeType.EndElement)
                {
                    property.SubProperties.Add(ParseProperty());
                }
            }

            XML.ReadEndElement();
            return property;

        }

        private void AddItemToWorld(List<Property> properties)
        {
            string eventType = "";
            if (CurrentSection == Section.Events || CurrentSection == Section.EventCollections)
                eventType = properties.First(property => property.Name == "type").Value;

            if (CurrentSection != Section.Events && CurrentSection != Section.EventCollections)
            {
                AddFromXMLSection(CurrentSection, properties);
            }
            else if (CurrentSection == Section.Events)
            {
                AddEvent(eventType, properties);
            }
            else if (CurrentSection == Section.EventCollections)
            {
                AddEventCollection(eventType, properties);
            }

            foreach (Property property in properties)
            {
                string section = "";
                if (CurrentSection == Section.Events || CurrentSection == Section.EventCollections)
                    section = eventType;
                else
                    section = CurrentSection.ToString();

                if (!property.Known && property.SubProperties.Count == 0)
                {
                    World.ParsingErrors.Report("Unknown " + section + " Property: " + property.Name, property.Value);
                }
                foreach (Property subProperty in property.SubProperties)
                {
                    if (!subProperty.Known)
                        World.ParsingErrors.Report("Unknown " + section + " Property: " + property.Name + " - " + subProperty.Name, subProperty.Value);
                }
            }
        }


        public void AddFromXMLSection(Section section, List<Property> properties)
        {
            switch (section)
            {
                case Section.Regions: World.Regions.Add(new WorldRegion(properties, World)); break;
                case Section.UndergroundRegions: World.UndergroundRegions.Add(new UndergroundRegion(properties, World)); break;
                case Section.Sites: World.Sites.Add(new Site(properties, World)); break;
                case Section.HistoricalFigures: World.HistoricalFigures.Add(new HistoricalFigure(properties, World)); break;
                case Section.EntityPopulations: World.EntityPopulations.Add(new EntityPopulation(properties, World)); break;
                case Section.Entities: World.Entities.Add(new Entity(properties, World)); break;
                case Section.Eras: World.Eras.Add(new Era(properties, World)); break;
                case Section.Artifacts: World.Artifacts.Add(new Artifact(properties, World)); break;
                case Section.WorldConstructions: break;
                default: World.ParsingErrors.Report("Unknown XML Section: " + section.ToString()); break;
            }
        }

        private void AddEvent(string type, List<Property> properties)
        {
            switch (type)
            {
                case "add hf entity link": World.Events.Add(new AddHFEntityLink(properties, World)); break;
                case "add hf hf link": World.Events.Add(new AddHFHFLink(properties, World)); break;
                case "attacked site": World.Events.Add(new AttackedSite(properties, World)); break;
                case "body abused": World.Events.Add(new BodyAbused(properties, World)); break;
                case "change hf job": World.Events.Add(new ChangeHFJob(properties, World)); break;
                case "change hf state": World.Events.Add(new ChangeHFState(properties, World)); break;
                case "changed creature type": World.Events.Add(new ChangedCreatureType(properties, World)); break;
                case "create entity position": World.Events.Add(new CreateEntityPosition(properties, World)); break;
                case "created site": World.Events.Add(new CreatedSite(properties, World)); break;
                case "created world construction": World.Events.Add(new CreatedWorldConstruction(properties, World)); break;
                case "creature devoured": World.Events.Add(new CreatureDevoured(properties, World)); break;
                case "destroyed site": World.Events.Add(new DestroyedSite(properties, World)); break;
                case "field battle": World.Events.Add(new FieldBattle(properties, World)); break;
                case "hf abducted": World.Events.Add(new HFAbducted(properties, World)); break;
                case "hf died": World.Events.Add(new HFDied(properties, World)); break;
                case "hf new pet": World.Events.Add(new HFNewPet(properties, World)); break;
                case "hf reunion": World.Events.Add(new HFReunion(properties, World)); break;
                case "hf simple battle event": World.Events.Add(new HFSimpleBattleEvent(properties, World)); break;
                case "hf travel": World.Events.Add(new HFTravel(properties, World)); break;
                case "hf wounded": World.Events.Add(new HFWounded(properties, World)); break;
                case "impersonate hf": World.Events.Add(new ImpersonateHF(properties, World)); break;
                case "item stolen": World.Events.Add(new ItemStolen(properties, World)); break;
                case "new site leader": World.Events.Add(new NewSiteLeader(properties, World)); break;
                case "peace accepted": World.Events.Add(new PeaceAccepted(properties, World)); break;
                case "peace rejected": World.Events.Add(new PeaceRejected(properties, World)); break;
                case "plundered site": World.Events.Add(new PlunderedSite(properties, World)); break;
                case "reclaim site": World.Events.Add(new ReclaimSite(properties, World)); break;
                case "remove hf entity link": World.Events.Add(new RemoveHFEntityLink(properties, World)); break;
                case "artifact created": World.Events.Add(new ArtifactCreated(properties, World)); break;
                case "diplomat lost": World.Events.Add(new DiplomatLost(properties, World)); break;
                case "entity created": World.Events.Add(new EntityCreated(properties, World)); break;
                case "hf revived": World.Events.Add(new HFRevived(properties, World)); break;
                case "masterpiece arch design": World.Events.Add(new MasterpieceArchDesign(properties, World)); break;
                case "masterpiece arch constructed": World.Events.Add(new MasterpieceArchConstructed(properties, World)); break;
                case "masterpiece engraving": World.Events.Add(new MasterpieceEngraving(properties, World)); break;
                case "masterpiece food": World.Events.Add(new MasterpieceFood(properties, World)); break;
                case "masterpiece lost": World.Events.Add(new MasterpieceLost(properties, World)); break;
                case "masterpiece item": World.Events.Add(new MasterpieceItem(properties, World)); break;
                case "masterpiece item improvement": World.Events.Add(new MasterpieceItemImprovement(properties, World)); break;
                case "merchant": World.Events.Add(new Merchant(properties, World)); break;
                case "site abandoned": World.Events.Add(new SiteAbandoned(properties, World)); break;
                case "site died": World.Events.Add(new SiteDied(properties, World)); break;
                case "add hf site link": World.Events.Add(new AddHFSiteLink(properties, World)); break;
                case "created structure": World.Events.Add(new CreatedStructure(properties, World)); break;
                case "hf razed structure": World.Events.Add(new HFRazedStructure(properties, World)); break;
                case "remove hf site link": World.Events.Add(new RemoveHFSiteLink(properties, World)); break;
                case "replaced structure": World.Events.Add(new ReplacedStructure(properties, World)); break;
                case "site taken over": World.Events.Add(new SiteTakenOver(properties, World)); break;
                case "entity relocate": World.Events.Add(new EntityRelocate(properties, World)); break;
	            case "hf gains secret goal": World.Events.Add(new HFGainsSecretGoal(properties, World)); break;
	            case "hf profaned structure": World.Events.Add(new HFProfanedStructure(properties, World)); break;
	            case "hf does interaction": World.Events.Add(new HFDoesInteraction(properties, World)); break;
	            case "entity primary criminals": World.Events.Add(new EntityPrimaryCriminals(properties, World)); break;
                case "hf confronted": World.Events.Add(new HFConfronted(properties, World)); break;
                case "assume identity": World.Events.Add(new AssumeIdentity(properties, World)); break;
	            case "entity law": World.Events.Add(new EntityLaw(properties, World)); break;
	            case "change hf body state": World.Events.Add(new ChangeHFBodyState(properties, World)); break;
                case "razed structure": World.Events.Add(new RazedStructure(properties, World)); break;
                case "hf learns secret": World.Events.Add(new HFLearnsSecret(properties, World)); break;
                case "artifact stored": World.Events.Add(new ArtifactStored(properties, World)); break;
                case "artifact possessed": World.Events.Add(new ArtifactPossessed(properties, World)); break;
                case "agreement made": World.Events.Add(new AgreementMade(properties, World)); break;
                case "artifact lost": World.Events.Add(new ArtifactLost(properties, World)); break;
                case "hf disturbed structure":
                default: World.ParsingErrors.Report("Unknown Event: " + type);
                    break;
            }


        }

        private void AddEventCollection(string type, List<Property> properties)
        {
            switch (type)
            {
                case "abduction": World.EventCollections.Add(new Abduction(properties, World)); break;
                case "battle": World.EventCollections.Add(new Battle(properties, World)); break;
                case "beast attack": World.EventCollections.Add(new BeastAttack(properties, World)); break;
                case "duel": World.EventCollections.Add(new Duel(properties, World)); break;
                case "journey": World.EventCollections.Add(new Journey(properties, World)); break;
                case "site conquered": World.EventCollections.Add(new SiteConquered(properties, World)); break;
                case "theft": World.EventCollections.Add(new Theft(properties, World)); break;
                case "war": World.EventCollections.Add(new War(properties, World)); break;
                default: World.ParsingErrors.Report("Unknown Event Collection: " + type); break;
            }
        }

        private void ProcessXMLSection(Section section)
        {
            if (section == Section.Events)
            {
                //Calculate Historical Figure Ages.
                int lastYear = World.Events.Last().Year;
                foreach (HistoricalFigure hf in World.HistoricalFigures)
                {
                    if (hf.DeathYear > 0)
                        hf.Age = hf.DeathYear - hf.BirthYear;
                    else
                        hf.Age = lastYear - hf.BirthYear;
                }
            }

            if (section == Section.EventCollections)
            {
                ProcessCollections();
            }

            //Create sorted Historical Figures so they can be binary searched by name, needed for parsing History file
            if (section == Section.HistoricalFigures)
            {
                World.HistoricalFiguresByName = new List<HistoricalFigure>(World.HistoricalFigures);
                World.HistoricalFiguresByName.Sort((a, b) => String.Compare(a.Name, b.Name));
                World.ProcessHFtoHFLinks();
                //World.ProcessHFCurrentIdentities();
                //World.ProcessHFUsedIdentities();
            }

            //Create sorted entities so they can be binary searched by name, needed for History/sites files
            if (section == Section.Entities)
            {
                World.EntitiesByName = new List<Entity>(World.Entities);
                World.EntitiesByName.Sort((a, b) => String.Compare(a.Name, b.Name));
                World.ProcessReputations();
                World.ProcessHFtoSiteLinks();
            }

            //Calculate end years for eras and add list of wars during era.
            if (section == Section.Eras)
            {
                World.Eras.Last().EndYear = World.Events.Last().Year;
                for (int i = 0; i < World.Eras.Count - 1; i++)
                    World.Eras[i].EndYear = World.Eras[i + 1].StartYear - 1;
                foreach (Era era in World.Eras)
                {
                    era.Events = World.Events.Where(events => events.Year >= era.StartYear && events.Year <= era.EndYear).OrderBy(events => events.Year).ToList();
                    era.Wars = World.EventCollections.OfType<War>().Where(war => (war.StartYear >= era.StartYear && war.EndYear <= era.EndYear && war.EndYear != -1) //entire war between
                                                                                                    || (war.StartYear >= era.StartYear && war.StartYear <= era.EndYear) //war started before & ended
                                                                                                    || (war.EndYear >= era.StartYear && war.EndYear <= era.EndYear && war.EndYear != -1) //war started during
                                                                                                    || (war.StartYear <= era.StartYear && war.EndYear >= era.EndYear) //war started before & ended after
                                                                                                    || (war.StartYear <= era.StartYear && war.EndYear == -1)).ToList();
                }
                
            }
        }

        private void ProcessCollections()
        {
            World.Wars = World.EventCollections.OfType<War>().ToList();
            World.Battles = World.EventCollections.OfType<Battle>().ToList();
            World.BeastAttacks = World.EventCollections.OfType<BeastAttack>().ToList();

            foreach (EventCollection eventCollection in World.EventCollections)
            {
                //Sub Event Collections aren't created until after the main collection
                //So only IDs are stored in the main collection until here now that all collections have been created
                //and can now be added to their Parent collection
                foreach (int collectionID in eventCollection.CollectionIDs)
                    eventCollection.Collections.Add(World.GetEventCollection(collectionID));
            }

            //Attempt at calculating beast historical figure for beast attacks.
            //Find beast by looking at eventsList and fill in some event properties from the beast attacks's properties
            //Calculated here so it can look in Duel collections contained in beast attacks
            foreach (BeastAttack beastAttack in World.EventCollections.OfType<BeastAttack>())
            {
                //Find Beast by looking at fights, Beast always engages the first fight in a Beast Attack?
                if (beastAttack.GetSubEvents().OfType<HFSimpleBattleEvent>().Count() > 0)
                {
                    beastAttack.Beast = beastAttack.GetSubEvents().OfType<HFSimpleBattleEvent>().First().HistoricalFigure1;
                    if (beastAttack.Beast.BeastAttacks == null) beastAttack.Beast.BeastAttacks = new List<BeastAttack>();
                    beastAttack.Beast.BeastAttacks.Add(beastAttack);
                }
                if (beastAttack.GetSubEvents().OfType<HFDied>().Count() > 1)
                {
                    var slayers = beastAttack.GetSubEvents().OfType<HFDied>().GroupBy(death => death.Slayer).Select(hf => new { HF = hf.Key, Count = hf.Count() });
                    if (slayers.Count(slayer => slayer.Count > 1) == 1)
                    {
                        HistoricalFigure beast = slayers.Single(slayer => slayer.Count > 1).HF;
                        beastAttack.Beast = beast;
                    }
                }

                //Fill in some various event info from collections.

                int insertIndex = 0;
                foreach (ItemStolen theft in beastAttack.Collection.OfType<ItemStolen>())
                {
                    theft.Site = beastAttack.Site;
                    theft.Thief = beastAttack.Beast;

                    insertIndex = beastAttack.Site.Events.BinarySearch(theft);
                    beastAttack.Site.Events.Insert(~insertIndex, theft);
                    if (beastAttack.Beast != null)
                    {
                        insertIndex = beastAttack.Beast.Events.BinarySearch(theft);
                        beastAttack.Beast.Events.Insert(~insertIndex, theft);
                    }
                }
                insertIndex = 0;
                foreach (CreatureDevoured devoured in beastAttack.Collection.OfType<CreatureDevoured>())
                {
                    if (beastAttack.Beast != null)
                    {
                        devoured.Eater = beastAttack.Beast;
                        insertIndex = beastAttack.Beast.Events.BinarySearch(devoured);
                        beastAttack.Beast.Events.Insert(~insertIndex, devoured);
                    }
                }

            }

            //Assign a Conquering Event its corresponding battle
            //Battle = first Battle prior to the conquering?
            foreach (SiteConquered conquer in World.EventCollections.OfType<SiteConquered>())
            {
                for (int i = conquer.ID - 1; i >= 0; i--)
                {
                    EventCollection collection = World.GetEventCollection(i);
                    if (collection == null) continue;
                    if (collection.GetType() == typeof(Battle))
                    {

                        conquer.Battle = collection as Battle;
                        conquer.Battle.Conquering = conquer;
                        break;
                    }
                }
            } 
        }
    }

    public class Property
    {
        public string Name = "";
        private string _value = "";
        public bool Known = false;
        public List<Property> SubProperties = new List<Property>();
        public string Value { get { Known = true; return _value; } set { _value = value; } }
    }

    public enum Section
    {
        Artifacts,
        Entities,
        EntityPopulations,
        Eras,
        EventCollections,
        Events,
        HistoricalFigures,
        Regions,
        Sites,
        UndergroundRegions,
        WorldConstructions,
        Unknown,
        Junk
    }
}
