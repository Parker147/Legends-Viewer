using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public class HistoricalFigure : WorldObject
    {
        public static HistoricalFigure Unknown;
        public string Name { get; set; }
        public string Race { get; set; }
        public string Caste { get; set; }
        public string AssociatedType { get; set; }
        public HFState CurrentState { get; set; }
        public List<State> States { get; set; }
        public List<HistoricalFigureLink> RelatedHistoricalFigures { get; set; }
        public int Age { get; set; }
        public int Appeared { get; set; }
        public int BirthYear { get; set; }
        public int BirthSeconds72 { get; set; }
        public int DeathYear { get; set; }
        public int DeathSeconds72 { get; set; }
        public DeathCause DeathCause { get; set; }
        public string ActiveInteraction { get; set; }
        public string InteractionKnowledge { get; set; }
        public string Goal { get; set; }
        public string JourneyPet { get; set; }
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
        public List<HFDied> Kills { get; set; }
        public List<HistoricalFigure> HFKills { get { return Kills.Select(kill => kill.HistoricalFigure).ToList(); } set { } }
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

            foreach(Property property in properties)
                switch(property.Name)
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
                    case "hf_link":
                        property.Known = true;
                        HistoricalFigureLink relation = new HistoricalFigureLink(property.SubProperties, world);
                        //Children/Apprentices haven't been read into memory yet, so the child/apprentice relation will be built when the mother/father/master link is found
                        if (relation.Type != HistoricalFigureLinkType.Child && relation.Type != HistoricalFigureLinkType.Apprentice)
                            RelatedHistoricalFigures.Add(relation);
                        if ((relation.Type == HistoricalFigureLinkType.Mother || relation.Type == HistoricalFigureLinkType.Father || relation.Type == HistoricalFigureLinkType.Master) && relation.HistoricalFigure != null)
                                relation.HistoricalFigure.RelatedHistoricalFigures.Add(new HistoricalFigureLink(this, HistoricalFigureLinkType.Child));
  
                        if (relation.Type == HistoricalFigureLinkType.Spouse && relation.HistoricalFigure != null && relation.HistoricalFigure.ID < this.ID)
                            foreach(HistoricalFigureLink badLink in relation.HistoricalFigure.RelatedHistoricalFigures)
                                badLink.FixUnloadedHistoricalFigure(this);
                        break;
                    case "active_interaction": ActiveInteraction = property.Value; break;
                    case "interaction_knowledge": InteractionKnowledge = property.Value; break;
                    case "animated": Animated = true; property.Known = true; break;
                    case "animated_string": AnimatedType = property.Value; break;
                    case "journey_pet": JourneyPet = property.Value; break;
                    case "goal": Goal = property.Value; break;

                    //Unhandled Properties
                    case "sphere":
                    case "current_identity_id":
                    case "ent_pop_id":
                    case "holds_artifact":
                    case "used_identity_id": property.Known = true; break;
                    case "entity_link":
                        property.Known = true;
                        foreach(Property subProperty in property.SubProperties)
                            switch (subProperty.Name)
                            {
                                case "link_type":
                                case "link_strength":
                                case "entity_id": subProperty.Known = true; break;
                            }
                        break;
                    case "hf_skill":
                        property.Known = true;
                        foreach(Property subProperty in property.SubProperties)
                            switch (subProperty.Name)
                            {
                                case "skill":
                                case "total_ip": subProperty.Known = true; break;
                            }
                        break;
                    case "entity_former_position_link":
                    case "entity_position_link":
                        property.Known = true;
                        foreach(Property subProperty in property.SubProperties)
                            switch (subProperty.Name)
                            {
                                case "position_profile_id":
		                        case "entity_id":
		                        case "start_year":
                                case "end_year": subProperty.Known = true; break;
                            }
                        break;

                    case "site_link":
                        property.Known = true;
                        foreach(Property subProperty in property.SubProperties)
                            switch (subProperty.Name)
                            {
                                case "link_type":
                                case "site_id":
                                case "sub_id":
                                case "entity_id": subProperty.Known = true; break;
                            }
                        break;
                    case "entity_reputation":
                         property.Known = true;
                        foreach(Property subProperty in property.SubProperties)
                            switch (subProperty.Name)
                            {
                                case "entity_id":
                                case "unsolved_murders":
                                case "first_ageless_year":
                                case "first_ageless_season_count": subProperty.Known = true; break;
                            }
                        break;
                }
            if (Name == "") Name = "(Unnamed)";
        }

        private void Initialize()
        {
            Appeared = BirthYear = BirthSeconds72 = DeathYear = DeathSeconds72 = -1;
            Name = Race = Caste = AssociatedType = "";
            DeathCause = DeathCause.None;
            CurrentState = HFState.None;
            Kills = new List<HFDied>();
            Battles = new List<Battle>();
            Positions = new List<Position>();
            Spheres = new List<string>();
            BeastAttacks = new List<BeastAttack>();
            States = new List<State>();
            RelatedHistoricalFigures = new List<HistoricalFigureLink>();
            AnimatedType = "";
            Goal = "";
            ActiveInteraction = "";
            InteractionKnowledge = "";
            JourneyPet = "";
            
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (this == HistoricalFigure.Unknown) return this.Name;
            if (link)
                if ((pov == null || pov != this))
                {
                    string title = Caste + ", " + AssociatedType + " (" + BirthYear + " - ";
                    if (DeathYear == -1) title += "Present)";
                    else title += DeathYear + ")";
                    title += "&#13Events: " + Events.Count;
                    if (pov != null && pov.GetType() == typeof(BeastAttack) && (pov as BeastAttack).Beast == this) //Highlight Beast when printing Beast Attack Log
                        if (this.Name.IndexOf(" ") > 0)
                            return "<a href = \"hf#" + this.ID + "\" title=\"" + title + "\"><font color=#339900>" + this.Name.Substring(0, this.Name.IndexOf(" ")) + "</font></a>";
                        else return "<a href = \"hf#" + this.ID + "\" title=\"" + title + "\"><font color=#339900>" + this.Name + "</font></a>";
                    else
                        return "the " + this.Race.ToLower() + " " + "<a href = \"hf#" + this.ID + "\" title=\"" + title + "\">" + this.Name + "</a>";
                }
                else// (pov != null && pov.ID == this.ID)
                    if (this.Name.IndexOf(" ") > 0)
                        return "<font color=\"Blue\">" + this.Name.Substring(0, this.Name.IndexOf(" ")) + "</font>";
                    else
                        return "<font color=\"Blue\">" + this.Name + "</font>";
            else

                if ((pov == null || pov != this))
                    return this.Race + " " + this.Name;
                else //(pov != null && pov.ID == this.ID)
                    if (this.Name.IndexOf(" ") > 0)
                        return this.Name.Substring(0, this.Name.IndexOf(" ") + 1);
                    else
                        return this.Name;
        }

        public class Position
        {
            public Entity Entity { get; set; }
            public int Began { get; set; }
            public int Ended { get; set; }
            public string Title { get; set; }
            public int Length { get; set; }
            public Position(Entity civ, int began, int ended, string title) { Entity = civ; Began = began; Ended = ended; Title = title;}
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

        
        private string CasteNoun(string caste, bool owner = false)
        {
            if (caste.ToLower() == "male")
                if (owner) return "his";
                else return "he";
            else if (caste.ToLower() == "female")
                if (owner) return "her";
                else return "she";
            else
                if (owner) return "it's";
                else return "it";
        }

        public string RaceString()
        {
            string hfraceString = "";
            if (Ghost) hfraceString += "ghostly ";
            if (Skeleton) hfraceString += "skeletal ";
            if (Zombie) hfraceString += "zombie ";
            if (Caste == "MALE") hfraceString += "male ";
            else if (Caste == "FEMALE") hfraceString += "female ";
            return hfraceString + Race.ToLower();
        }

    }
}
