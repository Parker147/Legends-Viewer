using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LegendsViewer.Legends
{
    public class WorldEvent : IComparable<WorldEvent>
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public int Seconds72 { get; set; }
        public string Type { get; set; }
        public EventCollection ParentCollection { get; set; }
        public World World;
        public WorldEvent(List<Property> properties, World world)
        {
            World = world;
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "id": this.ID = Convert.ToInt32(property.Value); property.Known = true; break;
                    case "year": this.Year = Convert.ToInt32(property.Value); property.Known = true; break;
                    case "seconds72": this.Seconds72 = Convert.ToInt32(property.Value); property.Known = true; break;
                    case "type": this.Type = String.Intern(property.Value); property.Known = true; break;
                    default: break;
                }
        }
        public WorldEvent() { ID = -1; Year = -1; Seconds72 = -1; Type = "INVALID"; }
        public virtual string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + this.Type;
        }

        public int Compare(WorldEvent worldEvent)
        {
            return this.ID.CompareTo(worldEvent.ID);
        }

        public virtual string GetYearTime()
        {
            if (this.Year == -1) return "In a time before time, ";
            string yearTime = "In " + this.Year + ", ";
            if (this.Seconds72 == -1)
                return yearTime;

            int month = this.Seconds72 % 100800;
            if (month <= 33600) yearTime += "early ";
            else if (month <= 67200) yearTime += "mid";
            else if (month <= 100800) yearTime += "late ";

            int season = this.Seconds72 % 403200;
            if (season < 100800) yearTime += "spring, ";
            else if (season < 201600) yearTime += "summer, ";
            else if (season < 302400) yearTime += "autumn, ";
            else if (season < 403200) yearTime += "winter, ";

            return yearTime;
        }
        public string PrintParentCollection(bool link = true, DwarfObject pov = null)
        {
            EventCollection parent = ParentCollection;
            string collectionString = "";
            while (parent != null)
            {
                if (collectionString.Length > 0) collectionString += " as part of ";
                collectionString += parent.ToLink(link, pov);
                parent = parent.ParentCollection;
            }

            if (collectionString.Length > 0)
                return "In " + collectionString + ". ";
            else
                return collectionString;
        }

        public int CompareTo(object obj)
        {
            return this.ID.CompareTo(obj);
        }

        public int CompareTo(WorldEvent other)
        {
            return this.ID.CompareTo(other.ID);
        }
    }

    public class AddHFEntityLink : WorldEvent
    {
        public Entity Entity;
        public HistoricalFigure HistoricalFigure;
        public string LinkType;
        public AddHFEntityLink(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "civ_id": Entity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                }
            Entity.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + " ";
            if (HistoricalFigure != null) eventString += HistoricalFigure.ToLink(link, pov);
            else eventString += "UNKNOWN HISTORICAL FIGURE";
            if (LinkType == "imprison") eventString += " was imprisoned by ";
            else if (LinkType == "enemy") eventString += " became an enemy of ";
            else eventString += " linked to ";
            eventString += Entity.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class AddHFHFLink : WorldEvent
    {
        public HistoricalFigure HistoricalFigure, HistoricalFigureTarget;
        public string LinkType;
        public AddHFHFLink(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "hfid_target": HistoricalFigureTarget = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            HistoricalFigureTarget.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime() + HistoricalFigure.ToLink(link, pov);
            if (LinkType == "imprison") eventString += " imprisoned ";
            else eventString += " linked (UNKNOWN) to ";
            if (HistoricalFigureTarget != null)
                eventString += HistoricalFigureTarget.ToLink(link, pov) + ". ";
            else
                eventString += " (UNKNOWN HISTORICAL FIGURE).";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class AttackedSite : WorldEvent
    {
        public Entity Attacker, Defender, SiteEntity;
        public Site Site;
        public HistoricalFigure AttackerGeneral, DefenderGeneral;
        public AttackedSite(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "attacker_civ_id": Attacker = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "defender_civ_id": Defender = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "attacker_general_hfid": AttackerGeneral = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "defender_general_hfid": DefenderGeneral = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                }
            Attacker.AddEvent(this);
            Defender.AddEvent(this);
            SiteEntity.AddEvent(this);
            Site.AddEvent(this);
            AttackerGeneral.AddEvent(this);
            DefenderGeneral.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Attacker.PrintEntity(true, pov) + " attacked ";
            if (SiteEntity != null) eventString += SiteEntity.PrintEntity(true, pov);
            else eventString += Defender.PrintEntity(true, pov);
            eventString += " at " + Site.ToLink(link, pov) + ". " + AttackerGeneral.ToLink(link, pov) + " led the attack";
            if (DefenderGeneral != null) eventString += ", and the defenders were led by " + DefenderGeneral.ToLink(link, pov) + ". ";
            else eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class BodyAbused : WorldEvent
    {
        public Entity Abuser;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public Location Coordinates;
        public BodyAbused(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + "UNKNOWN HISTORICAL FIGURE's body was abused by ";
            if (Abuser != null) eventString += Abuser.ToLink(link, pov);
            else eventString += "UNKNOWN ENTITY";
            if (Site != null)
                eventString += " in " + this.Site.ToLink(link, pov);
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class ChangeHFJob : WorldEvent
    {
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public ChangeHFJob(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " became a UNKNOWN JOB in " + Site.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }

    public enum HFState : byte
    {
        None,
        Settled,
        Wandering,
        Scouting,
        Snatcher,
        Refugee,
        Thief,
        Hunting,
        Unknown
    }
    public class ChangeHFState : WorldEvent
    {
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public Location Coordinates;
        public HFState State;
        private string UnknownState;
        public ChangeHFState(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "state": switch (property.Value)
                        {
                            case "settled": State = HFState.Settled; break;
                            case "wandering": State = HFState.Wandering; break;
                            case "scouting": State = HFState.Scouting; break;
                            case "snatcher": State = HFState.Snatcher; break;
                            case "refugee": State = HFState.Refugee; break;
                            case "thief": State = HFState.Thief; break;
                            case "hunting": State = HFState.Hunting; break;
                            default: State = HFState.Unknown; UnknownState = property.Value; world.Log.AppendLine("Unknown HF State: " + UnknownState); break;
                        } break;
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            if (HistoricalFigure != null)
            {
                HistoricalFigure.AddEvent(this);
                HistoricalFigure.States.Add(new HistoricalFigure.State(State, Year));
                HistoricalFigure.State lastState = HistoricalFigure.States.LastOrDefault();
                if (lastState != null) lastState.EndYear = Year;
                HistoricalFigure.CurrentState = State;
            }
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + HistoricalFigure.ToLink(link, pov);
            if (State == HFState.Settled) eventString += " settled in ";
            if (State == HFState.Refugee || State == HFState.Snatcher || State == HFState.Thief) eventString += " became a " + State.ToString().ToLower() + " in ";
            else if (State == HFState.Wandering) eventString += " began wandering ";
            else if (State == HFState.Scouting) eventString += " began scouting the area around ";
            else if (State == HFState.Hunting) eventString += " began hunting great beasts in ";
            else
            {
                eventString += " " + UnknownState + " in ";
            }
            if (Site != null) eventString += Site.ToLink(link, pov);
            else if (Region != null) eventString += Region.ToLink(link, pov);
            else if (UndergroundRegion != null) eventString += UndergroundRegion.ToLink(link, pov);
            else eventString += "the wilds";
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class ChangedCreatureType : WorldEvent
    {
        public HistoricalFigure Changee, Changer;
        public string OldRace, OldCaste, NewRace, NewCaste;
        public ChangedCreatureType(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "old_race": OldRace = Formatting.FormatRace(property.Value); break;
                    case "old_caste": OldCaste = property.Value; break;
                    case "new_race": NewRace = Formatting.FormatRace(property.Value); break;
                    case "new_caste": NewCaste = property.Value; break;
                    case "changee_hfid": Changee = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "changer_hfid": Changer = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                }
            Changee.AddEvent(this);
            Changer.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Changer.ToLink(link, pov) + " changed " + Changee.ToLink(link, pov) + " from a " + OldRace + " into a " + NewRace + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }

    public class CreateEntityPosition : WorldEvent
    {
        public CreateEntityPosition(List<Property> properties, World world)
            : base(properties, world)
        {
            
        }
    }

    public class CreatedSite : WorldEvent
    {
        public Entity Civ, SiteEntity;
        public Site Site;
        public CreatedSite(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            if (SiteEntity != null)
            {
                SiteEntity.Parent = Civ;
                new OwnerPeriod(Site, SiteEntity, this.Year, "founded");
            }
            else
                new OwnerPeriod(Site, Civ, this.Year, "founded");
            Site.AddEvent(this);
            SiteEntity.AddEvent(this);
            Civ.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (SiteEntity != null ) eventString += SiteEntity.ToLink(link, pov) + " of ";
            eventString += Civ.ToLink(link, pov) + " founded " + Site.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class CreatedWorldConstruction : WorldEvent
    {
        public Entity Civ, SiteEntity;
        public Site Site1, Site2;
        public int WorldConstructionID, MasterWorldConstructionID;
        public CreatedWorldConstruction(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id1": Site1 = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "site_id2": Site2 = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "wcid": WorldConstructionID = Convert.ToInt32(property.Value); break;
                    case "master_wcid": MasterWorldConstructionID = Convert.ToInt32(property.Value); break;
                }
            Civ.AddEvent(this);
            SiteEntity.AddEvent(this);
            Site1.AddEvent(this);
            Site2.AddEvent(this);

            Site1.AddConnection(Site2);
            Site2.AddConnection(Site1);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + SiteEntity.ToLink(link, pov) + " of " + Civ.ToLink(link, pov) + " finished the construction of UNKNOWN CONSTRUCTION connecting " + Site1.ToLink(link, pov) + " and " + Site2.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class CreatureDevoured : WorldEvent
    {
        public HistoricalFigure Eater, Victim;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public CreatureDevoured(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (Eater != null) eventString += Eater.ToLink(link, pov);
            else eventString += "UNKNOWN HISTORICAL FIGURE";
            eventString += " devoured UNKNOWN HISTORICAL FIGURE in ";
            if (Site != null) eventString += Site.ToLink(link, pov);
            else if (Region != null) eventString += Region.ToLink(link, pov);
            else if (UndergroundRegion != null) eventString += UndergroundRegion.ToLink(link, pov);
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }

    }
    public class DestroyedSite : WorldEvent
    {
        public Site Site;
        public Entity SiteEntity, Attacker, Defender;
        public DestroyedSite(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "attacker_civ_id": Attacker = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "defender_civ_id": Defender = world.GetEntity(Convert.ToInt32(property.Value)); break;
                }

            if (Site.OwnerHistory.Count == 0)
                if (SiteEntity != null && SiteEntity != Defender)
                {
                    SiteEntity.Parent = Defender;
                    new OwnerPeriod(Site, SiteEntity, 1, "UNKNOWN");
                }
                else
                    new OwnerPeriod(Site, Defender, 1, "UNKNOWN");

            Site.OwnerHistory.Last().EndCause = "destroyed";
            Site.OwnerHistory.Last().EndYear = this.Year;
            Site.OwnerHistory.Last().Ender = Attacker;

            Site.AddEvent(this);
            if (SiteEntity != Defender)
                SiteEntity.AddEvent(this);
            Attacker.AddEvent(this);
            Defender.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Attacker.ToLink(link, pov) + " defeated ";
            if (SiteEntity != null && SiteEntity != Defender) eventString += SiteEntity.ToLink(link, pov) + " of ";
            eventString += Defender.ToLink(link, pov) + " and destroyed " + Site.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class FieldBattle : WorldEvent
    {
        public Entity Attacker, Defender;
        public WorldRegion Region;
        public HistoricalFigure AttackerGeneral, DefenderGeneral;
        public UndergroundRegion UndergroundRegion;
        public Location Coordinates;
        public FieldBattle(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "attacker_civ_id": Attacker = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "defender_civ_id": Defender = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "attacker_general_hfid": AttackerGeneral = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "defender_general_hfid": DefenderGeneral = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            Attacker.AddEvent(this);
            Defender.AddEvent(this);
            AttackerGeneral.AddEvent(this);
            DefenderGeneral.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Attacker.ToLink(link, pov) + " attacked " + Defender.ToLink(link, pov) + " in " + Region.ToLink(link, pov) + ". " +
                AttackerGeneral.ToLink(link, pov) + " led the attack, and the defenders were led by " + DefenderGeneral.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class HFAbducted : WorldEvent
    {
        public HistoricalFigure Target { get; set; }
        public HistoricalFigure Snatcher { get; set; }
        public Site Site { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public HFAbducted(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "target_hfid": Target = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "snatcher_hfid": Snatcher = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            Target.AddEvent(this);
            Snatcher.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (Snatcher != null)
                eventString += Snatcher.ToLink(link, pov);
            else
                eventString += "(UNKNOWN HISTORICAL FIGURE)";
            eventString += " abducted " + Target.ToLink(link, pov) + " from " + Site.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }

    
    public class HFDied : WorldEvent
    {
        public HistoricalFigure Slayer { get; set; }
        public HistoricalFigure HistoricalFigure { get; set; }
        public DeathCause Cause { get; set; }
        private string UnknownCause { get; set; }
        public Site Site { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public int SlayerItemID { get; set; }
        public int SlayerShooterItemID { get; set; }
        public string SlayerRace { get; set; }
        public string SlayerCaste { get; set; }
        public HFDied(List<Property> properties, World world)
            : base(properties, world)
        {
            SlayerItemID = -1;
            SlayerShooterItemID = -1;
            SlayerRace = "UNKNOWN";
            SlayerCaste = "UNKNOWN";
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "slayer_item_id": SlayerItemID = Convert.ToInt32(property.Value); break;
                    case "slayer_shooter_item_id": SlayerShooterItemID = Convert.ToInt32(property.Value); break;
                    case "cause": switch (property.Value)
                        {
                            case "hunger": Cause = DeathCause.Starved; break;
                            case "struck": Cause = DeathCause.Struck; break;
                            case "murdered": Cause = DeathCause.Murdered; break;
                            case "old age": Cause = DeathCause.OldAge; break;
                            case "dragonfire": Cause = DeathCause.DragonsFire; break;
                            case "shot": Cause = DeathCause.Shot; break;
                            case "fire": Cause = DeathCause.Burned; break;
                            case "thirst": Cause = DeathCause.Thirst; break;
                            case "air": Cause = DeathCause.Suffocated; break;
                            case "blood": Cause = DeathCause.Bled; break;
                            case "cold": Cause = DeathCause.Cold; break;
                            case "crushed bridge": Cause = DeathCause.CrushedByABridge; break;
                            case "drown": Cause = DeathCause.Drowned; break;
                            case "infection": Cause = DeathCause.Infection; break;
                            case "obstacle": Cause = DeathCause.CollidedWithAnObstacle; break;
                            case "put to rest": Cause = DeathCause.PutToRest; break;
                            case "quitdead": Cause = DeathCause.StarvedQuit; break;
                            case "trap": Cause = DeathCause.Trap; break;
                            case "crushed": Cause = DeathCause.CaveIn; break;
                            case "cage blasted": Cause = DeathCause.InACage; break;
                            case "freezing water": Cause = DeathCause.FrozenInWater; break;
                            case "exec fed to beasts": Cause = DeathCause.ExecutedFedToBeasts; break;
                            case "exec burned alive": Cause = DeathCause.ExecutedBurnedAlive; break;
                            case "exec crucified": Cause = DeathCause.ExecutedCrucified; break;
                            case "exec drowned": Cause = DeathCause.ExecutedDrowned; break;
                            case "exec hacked to pieces": Cause = DeathCause.ExecutedHackedToPieces; break;
                            case "exec buried alive": Cause = DeathCause.ExecutedBuriedAlive; break;
                            case "exec beheaded": Cause = DeathCause.ExecutedBeheaded; break;
                            default: Cause = DeathCause.Unknown; UnknownCause = property.Value; world.Log.AppendLine("Unknown Death Cause: " + UnknownCause); break;
                        } break;
                    case "slayer_race": SlayerRace = Formatting.FormatRace(property.Value); break;
                    case "slayer_caste": SlayerCaste = property.Value; break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "slayer_hfid": Slayer = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            if (HistoricalFigure.DeathCause == DeathCause.None)
                HistoricalFigure.DeathCause = Cause;
            if (Slayer != null)
            {
                Slayer.AddEvent(this);
                Slayer.Kills.Add(this);
            }
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " ";
            string deathString = "";
            if (Cause == DeathCause.Thirst) deathString = "died of thirst";
            else if (Cause == DeathCause.OldAge) deathString = "died of old age";
            else if (Cause == DeathCause.Suffocated) deathString = "suffocated";
            else if (Cause == DeathCause.Bled) deathString = "bled to death";
            else if (Cause == DeathCause.Cold) deathString = "froze to death";
            else if (Cause == DeathCause.CrushedByABridge) deathString = "was crushed by a drawbridge";
            else if (Cause == DeathCause.Drowned) deathString = "drowned";
            else if (Cause == DeathCause.Starved) deathString = "starved to death";
            else if (Cause == DeathCause.Infection) deathString = "succumbed to infection";
            else if (Cause == DeathCause.CollidedWithAnObstacle) deathString = "died after colliding with an obstacle";
            else if (Cause == DeathCause.PutToRest) deathString = "was put to rest";
            else if (Cause == DeathCause.StarvedQuit) deathString = "starved";
            else if (Cause == DeathCause.Trap) deathString = "was killed by a trap";
            else if (Cause == DeathCause.CaveIn) deathString = "was crushed under a collapsing ceiling";
            else if (Cause == DeathCause.InACage) deathString = "died in a cage";
            else if (Cause == DeathCause.FrozenInWater) deathString = "was incased in ice";
            else if (Cause == DeathCause.Unknown) deathString = "died. (" + UnknownCause + ")";

            if (Slayer != null || SlayerRace != "UNKNOWN")
            {
                string slayerString;
                if (Slayer == null) slayerString = " a " + SlayerRace.ToLower();
                else slayerString = Slayer.ToLink(link, pov);

                if (Cause == DeathCause.DragonsFire) deathString = "burned up in " + slayerString + "'s dragon fire";
                else if (Cause == DeathCause.Burned) deathString = "was burned to death by " + slayerString + "'s fire";
                else if (Cause == DeathCause.Murdered) deathString = "was murdered by " + slayerString;
                else if (Cause == DeathCause.Shot) deathString = "was shot and killed by " + slayerString;
                else if (Cause == DeathCause.Struck) deathString = "was struck down by " + slayerString;
                else if (Cause == DeathCause.ExecutedBuriedAlive) deathString = "was buried alive by " + slayerString;
                else if (Cause == DeathCause.ExecutedBurnedAlive) deathString = "was burned alive by " + slayerString;
                else if (Cause == DeathCause.ExecutedCrucified) deathString = "was crucified by " + slayerString;
                else if (Cause == DeathCause.ExecutedDrowned) deathString = "was drowned by " + slayerString;
                else if (Cause == DeathCause.ExecutedFedToBeasts) deathString = "was fed to beasts by " + slayerString;
                else if (Cause == DeathCause.ExecutedHackedToPieces) deathString = "was hacked to pieces by " + slayerString;
                else if (Cause == DeathCause.ExecutedBeheaded) deathString = "was beheaded by " + slayerString;
                else deathString += ", slain by " + slayerString;
            }



            eventString += deathString;

            if (SlayerItemID >= 0) eventString += " with a (" + SlayerItemID + ")";
            else if (SlayerShooterItemID >= 0) eventString += " with a (shot) (" + SlayerShooterItemID + ")";

            if (Site != null) eventString += " in " + Site.ToLink(link, pov);
            else if (Region != null) eventString += " in " + Region.ToLink(link, pov);
            else if (UndergroundRegion != null) eventString += " in " + UndergroundRegion.ToLink(link, pov);
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class HFNewPet : WorldEvent
    {
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public Location Coordinates;
        public HFNewPet(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "group_hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " tamed UNKNOWN";
            if (Site != null) eventString += " at " + Site.ToLink(link, pov);
            else if (Region != null) eventString += " in " + Region.ToLink(link, pov);
            else if (UndergroundRegion != null) eventString += " in " + UndergroundRegion.ToLink(link, pov);
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class HFReunion : WorldEvent
    {
        public HistoricalFigure HistoricalFigure1, HistoricalFigure2;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public HFReunion(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "group_1_hfid": HistoricalFigure1 = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "group_2_hfid": HistoricalFigure2 = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure1.AddEvent(this);
            HistoricalFigure2.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + " " + HistoricalFigure1.ToLink(link, pov) + " was reunited with " + HistoricalFigure2.ToLink(link, pov);
            if (Site != null) eventString += " in " + Site.ToLink(link, pov);
            else if (Region != null) eventString += " in " + Region.ToLink(link, pov);
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }

    public enum HFSimpleBattleType : byte
    {
        [Description("2nd HF Lost After Giving Wounds")]
        HF2LostAfterGivingWounds,
        [Description("2nd HF Lost After Mutual Wounds")]
        HF2LostAfterMutualWounds,
        [Description("2nd HF Lost After Recieving Wounds")]
        HF2LostAfterReceivingWounds,
        Attacked,
        Scuffle,
        Confronted,
        Ambushed,
        [Description("Happened Upon")]
        HappenedUpon,
        Cornered,
        Surprised,
        Unknown
    }

    public class HFSimpleBattleEvent : WorldEvent
    {
        public HFSimpleBattleType SubType;
        public string UnknownSubType;
        public HistoricalFigure HistoricalFigure1, HistoricalFigure2;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public HFSimpleBattleEvent(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "subtype": switch (property.Value)
                        {
                            case "attacked": SubType = HFSimpleBattleType.Attacked; break;
                            case "scuffle": SubType = HFSimpleBattleType.Scuffle; break;
                            case "confront": SubType = HFSimpleBattleType.Confronted; break;
                            case "2 lost after receiving wounds": SubType = HFSimpleBattleType.HF2LostAfterReceivingWounds; break;
                            case "2 lost after giving wounds": SubType = HFSimpleBattleType.HF2LostAfterGivingWounds; break;
                            case "2 lost after mutual wounds": SubType = HFSimpleBattleType.HF2LostAfterMutualWounds; break;
                            case "happen upon": SubType = HFSimpleBattleType.HappenedUpon; break;
                            case "ambushed": SubType = HFSimpleBattleType.Ambushed; break;
                            case "corner": SubType = HFSimpleBattleType.Cornered; break;
                            case "surprised": SubType = HFSimpleBattleType.Surprised; break;
                            default: SubType = HFSimpleBattleType.Unknown; UnknownSubType = property.Value; world.Log.AppendLine("Unknown HF Battle SubType: " + UnknownSubType); break;
                        } break;
                    case "group_1_hfid": HistoricalFigure1 = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "group_2_hfid": HistoricalFigure2 = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure1.AddEvent(this);
            HistoricalFigure2.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + HistoricalFigure1.ToLink(link, pov);
            if (SubType == HFSimpleBattleType.HF2LostAfterGivingWounds) eventString = this.GetYearTime() + HistoricalFigure2.ToLink(link, pov) + " was forced to retreat from "
                + HistoricalFigure1.ToLink(link, pov) + " despite the latter's wounds";
            else if (SubType == HFSimpleBattleType.HF2LostAfterMutualWounds) eventString += " eventually prevailed and " + HistoricalFigure2.ToLink(link, pov)
                + " was forced to make a hasty escape";
            else if (SubType == HFSimpleBattleType.HF2LostAfterReceivingWounds) eventString = this.GetYearTime() + HistoricalFigure2.ToLink(link, pov) + " managed to escape from "
                + HistoricalFigure1.ToLink(link, pov) + "'s onslaught";
            else if (SubType == HFSimpleBattleType.Scuffle) eventString += " fought with " + HistoricalFigure2.ToLink(link, pov) + ". While defeated, the latter escaped unscathed";
            else if (SubType == HFSimpleBattleType.Attacked) eventString += " attacked " + HistoricalFigure2.ToLink(link, pov);
            else if (SubType == HFSimpleBattleType.Confronted) eventString += " confronted " + HistoricalFigure2.ToLink(link, pov);
            else if (SubType == HFSimpleBattleType.HappenedUpon) eventString += " happened upon " + HistoricalFigure2.ToLink();
            else if (SubType == HFSimpleBattleType.Ambushed) eventString += " ambushed " + HistoricalFigure2.ToLink();
            else if (SubType == HFSimpleBattleType.Cornered) eventString += " cornered " + HistoricalFigure2.ToLink();
            else if (SubType == HFSimpleBattleType.Surprised) eventString += " suprised " + HistoricalFigure2.ToLink();
            else eventString += " fought (" + UnknownSubType + ") " + HistoricalFigure2.ToLink(link, pov);
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }

    }
    public class HFTravel : WorldEvent
    {
        public Location Coordinates;
        public bool Escaped, Returned;
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public HFTravel(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "coords": Coordinates = Formatting.ConvertToLocation(property.Value); break;
                    case "escape": Escaped = true; property.Known = true; break;
                    case "return": Returned = true; property.Known = true; break;
                    case "group_hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + HistoricalFigure.ToLink(link, pov);
            if (Escaped) return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " escaped from the " + UndergroundRegion.ToLink(link, pov);
            else if (Returned) eventString += " returned to ";
            else eventString += " made a journey to ";

            if (UndergroundRegion != null) eventString += UndergroundRegion.ToLink(link, pov);
            else if (Site != null) eventString += Site.ToLink(link, pov);
            else if (Region != null) eventString += Region.ToLink(link, pov);

            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class HFWounded : WorldEvent
    {
        public HistoricalFigure Woundee, Wounder;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public HFWounded(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "woundee_hfid": Woundee = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "wounder_hfid": Wounder = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            Woundee.AddEvent(this);
            Wounder.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (Woundee != null)
                eventString += Woundee.ToLink(link, pov);
            else
                eventString += "(UNKNOWN HISTORICAL FIGURE)";
            eventString += " was wounded (UNKNOWN) by ";
            if (Wounder != null)
                eventString += Wounder.ToLink(link, pov);
            else
                eventString += "(UNKNOWN HISTORICAL FIGURE)";
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class ImpersonateHF : WorldEvent
    {
        public HistoricalFigure Trickster, Cover;
        public Entity Target;
        public ImpersonateHF(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "trickster_hfid": Trickster = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "cover_hfid": Cover = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "target_enid": Target = world.GetEntity(Convert.ToInt32(property.Value)); break;
                }
            Trickster.AddEvent(this);
            Cover.AddEvent(this);
            Target.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Trickster.ToLink(link, pov) + " fooled " + Target.ToLink(link, pov)
                + " into believing he/she was a manifestation of the diety " + Cover.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }

    public class ItemStolen : WorldEvent
    {
        public HistoricalFigure Thief;
        public Site Site, ReturnSite;
        public ItemStolen(List<Property> properties, World world)
            : base(properties, world)
        {
        }
        public override string Print(bool path = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();

            eventString += " a (UNKNOWN ITEM) was stolen from ";
            if (Site != null) eventString += Site.ToLink(path, pov);
            else eventString += " (UNKNOWN SITE)";

            eventString += " by ";
            if (Thief != null && Thief != null) eventString += Thief.ToLink(path, pov);
            else eventString += "(UNKNOWN HISTORICAL FIGURE)";

            if (ReturnSite != null) eventString += " and brought to " + ReturnSite.ToLink();

            eventString += ". ";
            eventString += PrintParentCollection(path, pov);
            return eventString;
        }
    }

    public class NewSiteLeader : WorldEvent
    {
        public Entity Attacker, Defender, SiteEntity, NewSiteEntity;
        public Site Site;
        public HistoricalFigure NewLeader;
        public NewSiteLeader(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "attacker_civ_id": Attacker = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "defender_civ_id": Defender = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "new_site_civ_id": NewSiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "new_leader_hfid": NewLeader = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                }

            if (Site.OwnerHistory.Count == 0)
                if (SiteEntity != null && SiteEntity != Defender)
                {
                    SiteEntity.Parent = Defender;
                    new OwnerPeriod(Site, SiteEntity, 1, "UNKNOWN");
                }
                else
                    new OwnerPeriod(Site, Defender, 1, "UNKNOWN");

            Site.OwnerHistory.Last().EndCause = "taken over";
            Site.OwnerHistory.Last().EndYear = this.Year;
            Site.OwnerHistory.Last().Ender = Attacker;
            NewSiteEntity.Parent = Attacker;
            new OwnerPeriod(Site, NewSiteEntity, this.Year, "took over");

            Attacker.AddEvent(this);
            Defender.AddEvent(this);
            if (SiteEntity != Defender)
                SiteEntity.AddEvent(this);
            Site.AddEvent(this);
            NewSiteEntity.AddEvent(this);
            NewLeader.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Attacker.ToLink(link, pov) + " defeated ";
            if (SiteEntity != null && SiteEntity != Defender) eventString += SiteEntity.ToLink(link, pov) + " of ";
            eventString += Defender.ToLink(link, pov) + " and placed " + NewLeader.ToLink(link, pov) + " in charge of " + Site.ToLink(link, pov) + ". The new government was called " + NewSiteEntity.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class PeaceAccepted : WorldEvent
    {
        public Site Site;
        public PeaceAccepted(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Site.AddEvent(this);
        }
    }
    public class PeaceRejected : WorldEvent
    {
        public Site Site;
        public PeaceRejected(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Site.AddEvent(this);
        }
    }
    public class PlunderedSite : WorldEvent
    {
        public Entity Attacker, Defender, SiteEntity;
        public Site Site;
        public PlunderedSite(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "attacker_civ_id": Attacker = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "defender_civ_id": Defender = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Attacker.AddEvent(this);
            Defender.AddEvent(this);
            if (Defender != SiteEntity) SiteEntity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Attacker.ToLink(link, pov) + " defeated ";
            if (SiteEntity != null && Defender != SiteEntity) eventString += SiteEntity.ToLink(link, pov) + " of ";
            eventString += Defender.ToLink(link, pov) + " and pillaged " + Site.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class ReclaimSite : WorldEvent
    {
        public Entity Civ, SiteEntity;
        public Site Site;
        public ReclaimSite(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            if (SiteEntity != null && SiteEntity != Civ)
                SiteEntity.Parent = Civ;

            //Make sure period was lost by an event, otherwise unknown loss
            if (Site.OwnerHistory.Count == 0)
                new OwnerPeriod(Site, null, 1, "UNKNOWN");
            if (Site.OwnerHistory.Last().EndYear == -1)
            {
                Site.OwnerHistory.Last().EndCause = "UNKNOWN";
                Site.OwnerHistory.Last().EndYear = this.Year - 1;
            }
            new OwnerPeriod(Site, SiteEntity, this.Year, "reclaimed");

            Civ.AddEvent(this);
            if (SiteEntity != Civ)
                SiteEntity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (SiteEntity != null && SiteEntity != Civ) eventString += SiteEntity.ToLink(link, pov) + " of ";
            eventString += Civ.ToLink(link, pov) + " launched an expedition to reclaim " + Site.ToLink(link, pov) + ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
    public class RemoveHFEntityLink : WorldEvent
    {
        public Entity Civ;
        public RemoveHFEntityLink(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                }
            Civ.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + " UNKNOWN HISTORICAL FIGURE removed link with " + Civ.ToLink(link, pov);
        }
    }


    //dwarf mode eventsList

    public class ArtifactCreated : WorldEvent
    {
        public int UnitID;
        public Artifact Artifact;
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public ArtifactCreated(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "unit_id": UnitID = Convert.ToInt32(property.Value); break;
                    case "artifact_id": Artifact = world.GetArtifact(Convert.ToInt32(property.Value)); break;
                    case "hist_figure_id": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string print = this.GetYearTime() + Artifact.Name;
            if (Site != null)
                print += " was created in " + Site.ToLink(link, pov);
            print += " by " + HistoricalFigure.ToLink(link, pov) + ". ";
            return print;
        }

    }

    public class DiplomatLost : WorldEvent
    {
        public Site Site;
        public DiplomatLost(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + "UNKNOWN ENTITY lost a diplomat at " + Site.ToLink(link, pov)
                + ". They suspected the involvement of UNKNOWN ENTITY.";
        }
    }

    public class EntityCreated : WorldEvent
    {
        public Entity Entity;
        public Site Site;
        public EntityCreated(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "entity_id": Entity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    
                    //Unhandled Events
                    case "structure_id": property.Known = true; break;
                }
            Entity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + Entity.ToLink(link, pov) + " formed in " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class HFRevived : WorldEvent
    {
        private string Ghost;
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public WorldRegion Region;
        public UndergroundRegion UndergroundRegion;
        public HFRevived(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "ghost": Ghost = property.Value; break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " came back from the dead as a " + Ghost + " in " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class MasterpieceArchDesign : WorldEvent
    {
        public int SkillAtTime;
        public HistoricalFigure HistoricalFigure;
        public Entity Civ;
        public Site Site;
        public MasterpieceArchDesign(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "skill_at_time": SkillAtTime = Convert.ToInt32(property.Value); break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "entity_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Civ.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " constructed a masterful (UNKNOWN) for " + Civ.ToLink(link, pov) +
                " at " + Site.ToLink(link, pov);
        }
    }

    public class MasterpieceArchConstructed : WorldEvent
    {
        public int SkillAtTime;
        public HistoricalFigure HistoricalFigure;
        public Entity Civ;
        public Site Site;
        public MasterpieceArchConstructed(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "skill_at_time": SkillAtTime = Convert.ToInt32(property.Value); break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "entity_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Civ.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " designed a masterful (UNKNOWN) for " + Civ.ToLink(link, pov) +
                " at " + Site.ToLink(link, pov) + ". ";
        }

    }

    public class MasterpieceEngraving : WorldEvent
    {
        private int SkillAtTime;
        public HistoricalFigure HistoricalFigure;
        public Entity Civ;
        public Site Site;
        public MasterpieceEngraving(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "skill_at_time": SkillAtTime = Convert.ToInt32(property.Value); break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "entity_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Civ.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + "created a masterful engraving for" + Civ.ToLink(link, pov) +
                " in " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class MasterpieceFood : WorldEvent
    {
        private int SkillAtTime;
        public HistoricalFigure HistoricalFigure;
        public Entity Civ;
        public Site Site;
        public MasterpieceFood(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "skill_at_time": SkillAtTime = Convert.ToInt32(property.Value); break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "entity_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Civ.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " prepared a masterful (UNKNOWN) for " + Civ.ToLink(link, pov) +
                " at " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class MasterpieceItem : WorldEvent
    {
        private int SkillAtTime;
        public HistoricalFigure HistoricalFigure;
        public Entity Civ;
        public Site Site;
        public MasterpieceItem(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "skill_at_time": SkillAtTime = Convert.ToInt32(property.Value); break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "entity_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Civ.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " created a masterful (UNKNOWN) for " + Civ.ToLink(link, pov) +
                " at " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class MasterpieceItemImprovement : WorldEvent
    {
        private int SkillAtTime;
        public HistoricalFigure HistoricalFigure;
        public Entity Civ;
        public Site Site;
        public MasterpieceItemImprovement(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "skill_at_time": SkillAtTime = Convert.ToInt32(property.Value); break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "entity_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Civ.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " added masterful (UNKNOWN) to a (UNKNOWN) for "
                + Civ.ToLink(link, pov) + " at " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class MasterpieceLost : WorldEvent
    {
        public MasterpieceLost(List<Property> properties, World world)
            : base(properties, world)
        {
            
        }
    }

    public class Merchant : WorldEvent
    {
        public Merchant(List<Property> properties, World world)
            : base(properties, world)
        {
        }
    }

    public class SiteAbandoned : WorldEvent
    {
        public Entity Civ, SiteEntity;
        public Site Site;
        public SiteAbandoned(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Site.OwnerHistory.Last().EndYear = this.Year;
            Site.OwnerHistory.Last().EndCause = "abandoned";
            if (SiteEntity != null)
            {
                SiteEntity.SiteHistory.Last(s => s.Site == Site).EndYear = this.Year;
                SiteEntity.SiteHistory.Last(s => s.Site == Site).EndCause = "abandoned";
            }
            Civ.SiteHistory.Last(s => s.Site == Site).EndYear = this.Year;
            Civ.SiteHistory.Last(s => s.Site == Site).EndCause = "abandoned";

            Civ.AddEvent(this);
            SiteEntity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (SiteEntity != null && SiteEntity != Civ) eventString += SiteEntity.ToLink(link, pov) + " of ";
            return eventString + Civ.ToLink(link, pov) + " abandoned the settlement at " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class SiteDied : WorldEvent
    {
        public Entity Civ, SiteEntity;
        public Site Site;
        public SiteDied(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Site.OwnerHistory.Last().EndYear = this.Year;
            Site.OwnerHistory.Last().EndCause = "withered";
            if (SiteEntity != null)
            {
                SiteEntity.SiteHistory.Last(s => s.Site == Site).EndYear = this.Year;
                SiteEntity.SiteHistory.Last(s => s.Site == Site).EndCause = "withered";
            }
            Civ.SiteHistory.Last(s => s.Site == Site).EndYear = this.Year;
            Civ.SiteHistory.Last(s => s.Site == Site).EndCause = "withered";

            Civ.AddEvent(this);
            SiteEntity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (SiteEntity != null && SiteEntity != Civ) eventString += SiteEntity.ToLink(link, pov) + " and ";
            return eventString + Civ.ToLink(link, pov) + " settlement of " + Site.ToLink(link, pov) + " withered.";
        }
    }


    //old eventsList
    public class AddHFSiteLink : WorldEvent
    {
        public Site Site;
        public AddHFSiteLink(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + "UNKNOWN HISTORICAL FIGURE linked to " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class CreatedStructure : WorldEvent
    {
        public int StructureID;
        public Entity Civ, SiteEntity;
        public Site Site;
        public CreatedStructure(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "structure_id": StructureID = Convert.ToInt32(property.Value); break;
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Civ.AddEvent(this);
            SiteEntity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (SiteEntity != null) eventString += SiteEntity.ToLink(link, pov) + " of ";
            eventString += Civ.ToLink(link, pov) + " constructed (" + StructureID + ") in " + Site.ToLink(link, pov) + ". ";
            return eventString;
        }
    }

    public class HFRazedStructure : WorldEvent
    {
        public int StructureID;
        public HistoricalFigure HistoricalFigure;
        public Site Site;
        public HFRazedStructure(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "structure_id": StructureID = Convert.ToInt32(property.Value); break;
                    case "hist_fig_id": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            HistoricalFigure.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + HistoricalFigure.ToLink(link, pov) + " razed a (" + StructureID + ") in " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class RemoveHFSiteLink : WorldEvent
    {
        public Site Site;
        public RemoveHFSiteLink(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            return this.GetYearTime() + "UNKNOWN HISTORICAL FIGURE removed link to " + Site.ToLink(link, pov) + ". ";
        }
    }

    public class ReplacedStructure : WorldEvent
    {
        public int OldABID, NewABID;
        public Entity Civ, SiteEntity;
        public Site Site;
        public ReplacedStructure(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "old_ab_id": OldABID = Convert.ToInt32(property.Value); break;
                    case "new_ab_id": NewABID = Convert.ToInt32(property.Value); break;
                    case "civ_id": Civ = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }
            Civ.AddEvent(this);
            SiteEntity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime();
            if (SiteEntity != null) eventString += SiteEntity.ToLink(link, pov) + " of ";
            eventString += Civ.ToLink(link, pov) + " replaced a (" + OldABID + ") in " + Site.ToLink(link, pov)
                + " with a (" + NewABID + ")";
            return eventString;
        }
    }

    public class SiteTakenOver : WorldEvent
    {
        public Entity Attacker, Defender, NewSiteEntity, SiteEntity;
        public Site Site;
        public SiteTakenOver(List<Property> properties, World world)
            : base(properties, world)
        {
            foreach(Property property in properties)
                switch(property.Name)
                {
                    case "attacker_civ_id": Attacker = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "defender_civ_id": Defender = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "new_site_civ_id": NewSiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_civ_id": SiteEntity = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                }

            if (Site.OwnerHistory.Count == 0)
                if (SiteEntity != null && SiteEntity != Defender)
                {
                    SiteEntity.Parent = Defender;
                    new OwnerPeriod(Site, SiteEntity, 1, "UNKNOWN");
                }
                else
                    new OwnerPeriod(Site, Defender, 1, "UNKNOWN");

            Site.OwnerHistory.Last().EndCause = "taken over";
            Site.OwnerHistory.Last().EndYear = this.Year;
            Site.OwnerHistory.Last().Ender = Attacker;
            NewSiteEntity.Parent = Attacker;
            new OwnerPeriod(Site, NewSiteEntity, this.Year, "took over");

            Attacker.AddEvent(this);
            Defender.AddEvent(this);
            NewSiteEntity.AddEvent(this);
            if (SiteEntity != Defender)
                SiteEntity.AddEvent(this);
            Site.AddEvent(this);
        }
        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = this.GetYearTime() + Attacker.ToLink(link, pov) + " defeated ";
            if (SiteEntity != null && SiteEntity != Defender) eventString += SiteEntity.ToLink(link, pov) + " of ";
            eventString += Defender.ToLink(link, pov) + " and took over " + Site.ToLink(link, pov) +
                ". The new government was called " + NewSiteEntity.ToLink(link, pov) + ". ";
            return eventString;
        }
    }
}
