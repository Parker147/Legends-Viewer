using LegendsViewer.Controls.HTML.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.EventCollections;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class HistoricalFigure : WorldObject
    {
        public static string ForceNatureIcon = "<i class=\"glyphicon fa-fw glyphicon-leaf\"></i>";
        public static string DeityIcon = "<i class=\"fa fa-fw fa-sun-o\"></i>";
        public static string NeuterIcon = "<i class=\"fa fa-fw fa-neuter\"></i>";
        public static string FemaleIcon = "<i class=\"fa fa-fw fa-venus\"></i>";
        public static string MaleIcon = "<i class=\"fa fa-fw fa-mars\"></i>";

        public static HistoricalFigure Unknown;
        public string Name { get; set; }
        public string Race { get; set; }
        public string PreviousRace { get; set; }
        public string Caste { get; set; }
        public string AssociatedType { get; set; }
        public EntityPopulation EntityPopulation { get; set; }
        public HFState CurrentState { get; set; }
        public HistoricalFigure UsedIdentity { get; set; }
        public HistoricalFigure CurrentIdentity { get; set; }
        public List<Artifact> HoldingArtifacts { get; set; }
        public List<State> States { get; set; }
        public List<HistoricalFigureLink> RelatedHistoricalFigures { get; set; }
        public List<EntityLink> RelatedEntities { get; set; }
        public List<EntityReputation> Reputations { get; set; }
        public List<SiteLink> RelatedSites { get; set; }
        public List<Skill> Skills { get; set; }
        public int Age { get; set; }
        public int Appeared { get; set; }
        public int BirthYear { get; set; }
        public int BirthSeconds72 { get; set; }
        public int DeathYear { get; set; }
        public int DeathSeconds72 { get; set; }
        public DeathCause DeathCause { get; set; }
        public List<string> ActiveInteractions { get; set; }
        public List<string> InteractionKnowledge { get; set; }
        public string Goal { get; set; }
        public string Interaction { get; set; }

        public HistoricalFigure LineageCurseParent { get; set; }
        public List<HistoricalFigure> LineageCurseChilds { get; set; }

        public List<string> JourneyPets { get; set; }
        public string DeathCollectionType
        {
            get
            {
                HFDied death = Events.OfType<HFDied>().FirstOrDefault(death1 => death1.HistoricalFigure == this);
                if (death != null && death.ParentCollection != null)
                    return death.ParentCollection.GetCollectionParentString();
                else if (death != null)
                    return "None";
                else
                    return "Not Dead";
            }
            set { }
        }
        public List<HFDied> NotableKills { get; set; }
        public List<HistoricalFigure> HFKills { get { return NotableKills.Select(kill => kill.HistoricalFigure).ToList(); } set { } }
        public List<HistoricalFigure> Abductions { get { return Events.OfType<HFAbducted>().Where(abduction => abduction.Snatcher == this).Select(abduction => abduction.Target).ToList(); } set { } }
        public int Abducted { get { return Events.OfType<HFAbducted>().Count(abduction => abduction.Target == this); } set { } }
        public List<string> Spheres { get; set; }
        public List<Battle> Battles { get; set; }
        public List<Battle> BattlesAttacking { get { return Battles.Where(battle => battle.NotableAttackers.Contains(this)).ToList(); } set { } }
        public List<Battle> BattlesDefending { get { return Battles.Where(battle => battle.NotableDefenders.Contains(this)).ToList(); } set { } }
        public List<Battle> BattlesNonCombatant { get { return Battles.Where(battle => battle.NonCombatants.Contains(this)).ToList(); } set { } }
        public List<Position> Positions { get; set; }
        public Entity WorshippedBy { get; set; }
        public List<BeastAttack> BeastAttacks { get; set; }
        public bool Alive { get { if (DeathYear == -1) return true; else return false; } set { } }
        public bool Deity { get; set; }
        public bool Skeleton { get; set; }
        public bool Force { get; set; }
        public bool Zombie { get; set; }
        public bool Ghost { get; set; }
        public bool Beast { get; set; }
        public bool Animated { get; set; }
        public string AnimatedType { get; set; }
        public bool Adventurer { get; set; }
        public string BreedID { get; set; }
        public int Sex { get; set; }

        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }
        public HistoricalFigure()
        {
            Initialize();
            Name = "UNKNOWN HISTORICAL FIGURE";
            Race = "UNKNOWN";
            Caste = "UNKNOWN";
            AssociatedType = "UNKNOWN";
        }
        public override string ToString() { return this.Name; }
        public HistoricalFigure(List<Property> properties, World world)
            : base(properties, world)
        {
            Initialize();
            List<string> knownEntitySubProperties = new List<string>() { "entity_id", "link_strength", "link_type", "position_profile_id", "start_year", "end_year" };
            List<string> knownReputationSubProperties = new List<string>() { "entity_id", "unsolved_murders", "first_ageless_year", "first_ageless_season_count", "rep_enemy_fighter", "rep_trade_partner", "rep_killer", "rep_poet", "rep_bard", "rep_storyteller", "rep_dancer" };
            List<string> knownSiteLinkSubProperties = new List<string>() { "link_type", "site_id", "sub_id", "entity_id", "occupation_id" };
            List<string> knownEntitySquadLinkProperties = new List<string>() { "squad_id", "squad_position", "entity_id", "start_year", "end_year" };
            List<string> knownRelationshipProfileProperties = new List<string>() { "hf_id", "known_identity_id", "meet_count", "last_meet_year", "last_meet_seconds72", "rep_friendly", "rep_buddy", "rep_grudge", "rep_bonded", "rep_quarreler", "rep_trade_partner", "rep_psychopath", "rep_storyteller" };
            foreach (Property property in properties)
                switch (property.Name)
                {
                    case "appeared": Appeared = Convert.ToInt32(property.Value); break;
                    case "birth_year": BirthYear = Convert.ToInt32(property.Value); break;
                    case "birth_seconds72": BirthSeconds72 = Convert.ToInt32(property.Value); break;
                    case "death_year": DeathYear = Convert.ToInt32(property.Value); break;
                    case "death_seconds72": DeathSeconds72 = Convert.ToInt32(property.Value); break;
                    case "name": Name = String.Intern(Formatting.InitCaps(property.Value)); break;
                    case "race": Race = String.Intern(Formatting.FormatRace(property.Value)); break;
                    case "caste": Caste = Formatting.InitCaps(property.Value.ToLower().Replace('_', ' ')); break;
                    case "associated_type": AssociatedType = Formatting.InitCaps(property.Value.ToLower().Replace('_', ' ')); break;
                    case "deity": Deity = true; property.Known = true; break;
                    case "skeleton": Skeleton = true; property.Known = true; break;
                    case "force": Force = true; property.Known = true; break;
                    case "zombie": Zombie = true; property.Known = true; break;
                    case "ghost": Ghost = true; property.Known = true; break;
                    case "hf_link": //Will be processed after all HFs have been loaded
                        world.AddHFtoHFLink(this, property);
                        property.Known = true;
                        List<string> knownSubProperties = new List<string>() { "hfid", "link_strength", "link_type" };
                        foreach (string subPropertyName in knownSubProperties)
                        {
                            Property subProperty = property.SubProperties.FirstOrDefault(property1 => property1.Name == subPropertyName);
                            if (subProperty != null)
                                subProperty.Known = true;
                        }
                        break;
                    case "entity_link":
                    case "entity_former_position_link":
                    case "entity_position_link":
                        world.AddHFtoEntityLink(this, property);
                        foreach (string subPropertyName in knownEntitySubProperties)
                        {
                            Property subProperty = property.SubProperties.FirstOrDefault(property1 => property1.Name == subPropertyName);
                            if (subProperty != null)
                                subProperty.Known = true;
                        }
                        break;
                    case "entity_reputation":
                        world.AddReputation(this, property);
                        foreach (string subPropertyName in knownReputationSubProperties)
                        {
                            Property subProperty = property.SubProperties.FirstOrDefault(property1 => property1.Name == subPropertyName);
                            if (subProperty != null)
                                subProperty.Known = true;
                        }
                        break;
                    case "entity_squad_link":
                    case "entity_former_squad_link":
                        property.Known = true;
                        foreach (string subPropertyName in knownEntitySquadLinkProperties)
                        {
                            Property subProperty = property.SubProperties.FirstOrDefault(property1 => property1.Name == subPropertyName);
                            if (subProperty != null)
                                subProperty.Known = true;
                        }
                        break;
                    case "relationship_profile_hf":
                        property.Known = true;
                        foreach (string subPropertyName in knownRelationshipProfileProperties)
                        {
                            Property subProperty = property.SubProperties.FirstOrDefault(property1 => property1.Name == subPropertyName);
                            if (subProperty != null)
                                subProperty.Known = true;
                        }
                        break;
                    case "site_link":
                        world.AddHFtoSiteLink(this, property);
                        foreach (string subPropertyName in knownSiteLinkSubProperties)
                        {
                            Property subProperty = property.SubProperties.FirstOrDefault(property1 => property1.Name == subPropertyName);
                            if (subProperty != null)
                                subProperty.Known = true;
                        }
                        break;
                    case "hf_skill": Skills.Add(new Skill(property.SubProperties)); break;
                    case "active_interaction":
                        ActiveInteractions.Add(property.Value);
                        break;
                    case "interaction_knowledge":
                        InteractionKnowledge.Add(property.Value);
                        break;
                    case "animated": Animated = true; property.Known = true; break;
                    case "animated_string":
                        if (AnimatedType != "") throw new Exception("Animated Type already exists.");
                        AnimatedType = Formatting.InitCaps(property.Value); break;
                    case "journey_pet": JourneyPets.Add(Formatting.FormatRace(property.Value)); break;
                    case "goal": Goal = Formatting.InitCaps(property.Value); break;
                    case "sphere":
                        Spheres.Add(property.Value); break;
                    case "current_identity_id":
                        world.AddHFCurrentIdentity(this, Convert.ToInt32(property.Value));
                        break;
                    case "used_identity_id":
                        world.AddHFUsedIdentity(this, Convert.ToInt32(property.Value));
                        break;
                    case "ent_pop_id":
                        EntityPopulation = world.GetEntityPopulation(Convert.ToInt32(property.Value)); break;
                    case "holds_artifact":
                        HoldingArtifacts.Add(world.GetArtifact(Convert.ToInt32(property.Value))); break;
                    case "adventurer":
                        Adventurer = true;
                        property.Known = true;
                        break;
                    case "breed_id":
                        BreedID = property.Value;
                        if (!string.IsNullOrWhiteSpace(BreedID))
                        {
                            if (world.Breeds.ContainsKey(BreedID))
                            {
                                world.Breeds[BreedID].Add(this);
                            }
                            else
                            {
                                world.Breeds.Add(BreedID, new List<HistoricalFigure>() { this });
                            }
                        }
                        break;
                    case "sex": Convert.ToInt32(property.Value); break;
                }
            if (Name == "") Name = "(Unnamed)";
        }

        private void Initialize()
        {
            Appeared = BirthYear = BirthSeconds72 = DeathYear = DeathSeconds72 = -1;
            Name = Race = Caste = AssociatedType = "";
            DeathCause = DeathCause.None;
            CurrentState = HFState.None;
            NotableKills = new List<HFDied>();
            Battles = new List<Battle>();
            Positions = new List<Position>();
            Spheres = new List<string>();
            BeastAttacks = new List<BeastAttack>();
            States = new List<State>();
            RelatedHistoricalFigures = new List<HistoricalFigureLink>();
            RelatedEntities = new List<EntityLink>();
            Reputations = new List<EntityReputation>();
            RelatedSites = new List<SiteLink>();
            Skills = new List<Skill>();
            AnimatedType = "";
            Goal = "";
            ActiveInteractions = new List<string>();
            PreviousRace = "";
            InteractionKnowledge = new List<string>();
            JourneyPets = new List<string>();
            HoldingArtifacts = new List<Artifact>();
            LineageCurseChilds = new List<HistoricalFigure>();
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (this == Unknown)
                return Name;
            if (link)
            {
                string icon = getIcon();
                string title = getAnchorTitle();
                if ((pov == null || pov != this))
                {
                    if (pov != null && pov.GetType() == typeof(BeastAttack) && (pov as BeastAttack).Beast == this) //Highlight Beast when printing Beast Attack Log
                        return icon + "<a href=\"hf#" + ID + "\" title=\"" + title + "\"><font color=#339900>" + (Name.IndexOf(" ") > 0 ? Name.Substring(0, Name.IndexOf(" ")) : Name) + "</font></a>";
                    else
                        return "the " + GetRaceString() + " " + icon + "<a href=\"hf#" + ID + "\" title=\"" + title + "\">" + Name + "</a>";
                }
                else
                {
                    return "<a href=\"hf#" + ID + "\" title=\"" + title + "\">" + HTMLStyleUtil.CurrentDwarfObject(Name.IndexOf(" ") > 0 ? Name.Substring(0, Name.IndexOf(" ")) : Name) + "</a>";
                }
            }
            else if ((pov == null || pov != this))
            {
                return GetRaceString() + " " + Name;
            }
            else
            {
                return Name.IndexOf(" ") > 0 ? Name.Substring(0, Name.IndexOf(" ")) : Name;
            }
        }

        private string getIcon()
        {
            if (Force)
            {
                return ForceNatureIcon;
            }
            if (Deity)
            {
                return DeityIcon;
            }
            if (Caste == "Female")
            {
                return FemaleIcon;
            }
            if (Caste == "Male")
            {
                return MaleIcon;
            }
            if (Caste == "Default")
            {
                return NeuterIcon;
            }
            return "";
        }

        private string getAnchorTitle()
        {
            string title = Caste + " " + GetRaceString() + " " + (AssociatedType != "Standard" ? AssociatedType : "") + " ";
            if (!Deity)
            {
                title += "(" + BirthYear + " - " + (DeathYear == -1 ? "Present" : DeathYear.ToString()) + ")";
            }
            title += "&#13";
            title += "Events: " + Events.Count;
            return title;
        }

        public string ToTreeLeafLink(DwarfObject pov = null)
        {
            string dead = DeathYear != -1 ? "<br/>" + HTMLStyleUtil.SYMBOL_DEAD : "";
            if ((pov == null || pov != this))
            {
                return "<a " + (Deity ? "class=\"hf_deity\"" : "") + " href=\"hf#" + ID + "\" title=\"" + getAnchorTitle() + "\">" + Race + (Deity ? " Deity" : "") + "<br/>" + Name + dead + "</a>";
            }
            else
            {
                return "<a " + (Deity ? "class=\"hf_deity\"" : "") + " title=\"" + getAnchorTitle() + "\">" + Race + (Deity ? " Deity" : "") + "<br/>" + HTMLStyleUtil.CurrentDwarfObject(Name) + dead + "</a>";
            }
        }

        public class Position
        {
            public Entity Entity { get; set; }
            public int Began { get; set; }
            public int Ended { get; set; }
            public string Title { get; set; }
            public int Length { get; set; }
            public Position(Entity civ, int began, int ended, string title) { Entity = civ; Began = began; Ended = ended; Title = title; }
        }

        public class State
        {
            public HFState HFState { get; set; }
            public int StartYear { get; set; }
            public int EndYear { get; set; }

            public State(HFState state, int start)
            {
                HFState = state;
                StartYear = start;
                EndYear = -1;
            }
        }


        public string CasteNoun(bool owner = false)
        {
            if (this.Caste.ToLower() == "male")
                if (owner) return "his";
                else return "he";
            else if (this.Caste.ToLower() == "female")
                if (owner) return "her";
                else return "she";
            else
                if (owner) return "it's";
            else return "it";
        }

        public string GetRaceTitleString()
        {
            string hfraceString = "";
            if (Race == "Night Creature" && PreviousRace != "")
                return PreviousRace.ToLower() + " turned night creature";

            if (Ghost) hfraceString += "ghostly ";
            if (Skeleton) hfraceString += "skeletal ";
            if (Zombie) hfraceString += "zombie ";
            if (Caste.ToUpper() == "MALE") hfraceString += "male ";
            else if (Caste.ToUpper() == "FEMALE") hfraceString += "female ";

            hfraceString += Race.ToLower();

            if (ActiveInteractions.Any(it => it.Contains("VAMPIRE")))
                return hfraceString + " vampire";
            if (ActiveInteractions.Any(it => it.Contains("WEREBEAST")))
                return hfraceString + " werebeast";
            if (ActiveInteractions.Any(it => it.Contains("SECRET")))
                return hfraceString + " necromancer";

            return hfraceString;
        }

        public string GetRaceString()
        {
            if (Deity)
                return Race.ToLower() + " deity";
            if (Race == "Night Creature" && PreviousRace != "")
                return PreviousRace.ToLower() + " turned night creature";
            if (ActiveInteractions.Any(it => it.Contains("VAMPIRE")))
                return Race.ToLower() + " vampire";
            if (ActiveInteractions.Any(it => it.Contains("WEREBEAST")))
                return Race.ToLower() + " werebeast";
            if (ActiveInteractions.Any(it => it.Contains("SECRET")))
                return Race.ToLower() + " necromancer";
            return Race.ToLower();
        }
    }
}
