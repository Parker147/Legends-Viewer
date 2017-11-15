using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Interfaces;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class HfDied : WorldEvent, IFeatured
    {
        public HistoricalFigure Slayer { get; set; }
        public HistoricalFigure HistoricalFigure { get; set; }
        public DeathCause Cause { get; set; }
        private string UnknownCause { get; set; }
        public Site Site { get; set; }
        public WorldRegion Region { get; set; }
        public UndergroundRegion UndergroundRegion { get; set; }
        public int SlayerItemId { get; set; }
        public int SlayerShooterItemId { get; set; }
        public string SlayerRace { get; set; }
        public string SlayerCaste { get; set; }

        public int ItemId { get; set; }
        public string ItemType { get; set; }
        public string ItemSubType { get; set; }
        public string ItemMaterial { get; set; }
        public Artifact Artifact { get; set; }

        public int ShooterItemId { get; set; }
        public string ShooterItemType { get; set; }
        public string ShooterItemSubType { get; set; }
        public string ShooterItemMaterial { get; set; }
        public Artifact ShooterArtifact { get; set; }

        public HfDied(List<Property> properties, World world)
            : base(properties, world)
        {
            ItemId = -1;
            ShooterItemId = -1;
            SlayerItemId = -1;
            SlayerShooterItemId = -1;
            SlayerRace = "UNKNOWN";
            SlayerCaste = "UNKNOWN";
            Cause = DeathCause.Unknown;
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "slayer_item_id": SlayerItemId = Convert.ToInt32(property.Value); break;
                    case "slayer_shooter_item_id": SlayerShooterItemId = Convert.ToInt32(property.Value); break;
                    case "cause":
                        switch (property.Value)
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
                            case "blood drained": Cause = DeathCause.DrainedBlood; break;
                            case "collapsed": Cause = DeathCause.Collapsed; break;
                            case "scared to death": Cause = DeathCause.ScaredToDeath; break;
                            case "scuttled": Cause = DeathCause.Scuttled; break;
                            case "flying object": Cause = DeathCause.FlyingObject; break;
                            case "slaughtered": Cause = DeathCause.Slaughtered; break;
                            case "melt": Cause = DeathCause.Melted; break;
                            case "spikes": Cause = DeathCause.Spikes; break;
                            case "heat": Cause = DeathCause.Heat; break;
                            case "vanish": Cause = DeathCause.Vanish; break;
                            case "cooling magma": Cause = DeathCause.CoolingMagma; break;
                            default: Cause = DeathCause.Unknown; UnknownCause = property.Value; world.ParsingErrors.Report("|==> Events 'hf died'/ \nUnknown Death Cause: " + UnknownCause); break;
                        }
                        break;
                    case "slayer_race": SlayerRace = Formatting.FormatRace(property.Value); break;
                    case "slayer_caste": SlayerCaste = property.Value; break;
                    case "hfid": HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "slayer_hfid": Slayer = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "subregion_id": Region = world.GetRegion(Convert.ToInt32(property.Value)); break;
                    case "feature_layer_id": UndergroundRegion = world.GetUndergroundRegion(Convert.ToInt32(property.Value)); break;
                    case "victim_hf": if (HistoricalFigure == null) { HistoricalFigure = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); } else { property.Known = true; } break;
                    case "slayer_hf": if (Slayer == null) { Slayer = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); } else { property.Known = true; } break;
                    case "site": if (Site == null) { Site = world.GetSite(Convert.ToInt32(property.Value)); } else { property.Known = true; } break;
                    case "death_cause": property.Known = true; break;
                    case "item": ItemId = Convert.ToInt32(property.Value); break;
                    case "item_type": ItemType = property.Value; break;
                    case "item_subtype": ItemSubType = property.Value; break;
                    case "mat": ItemMaterial = property.Value; break;
                    case "artifact_id": Artifact = world.GetArtifact(Convert.ToInt32(property.Value)); break;
                    case "shooter_item": ShooterItemId = Convert.ToInt32(property.Value); break;
                    case "shooter_item_type": ShooterItemType = property.Value; break;
                    case "shooter_item_subtype": ShooterItemSubType = property.Value; break;
                    case "shooter_mat": ShooterItemMaterial = property.Value; break;
                    case "shooter_artifact_id": ShooterArtifact = world.GetArtifact(Convert.ToInt32(property.Value)); break;
                }
            }

            HistoricalFigure.AddEvent(this);
            if (HistoricalFigure.DeathCause == DeathCause.None)
            {
                HistoricalFigure.DeathCause = Cause;
            }

            if (Slayer != null)
            {
                Slayer.AddEvent(this);
                Slayer.NotableKills.Add(this);
            }
            Site.AddEvent(this);
            Region.AddEvent(this);
            UndergroundRegion.AddEvent(this);
            Artifact.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += GetDeathString(link, pov);
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
        
        public string PrintFeature(bool link = true, DwarfObject pov = null)
        {
            string eventString = "";
            eventString += GetDeathString(link, pov);
            eventString += " in ";
            eventString += Year;
            return eventString;
        }

        private string GetDeathString(bool link = true, DwarfObject pov = null)
        {
            string eventString = "";
            eventString += HistoricalFigure.ToLink(link, pov) + " ";
            string deathString = "";

            if (Slayer != null || SlayerRace != "UNKNOWN" && SlayerRace != "-1")
            {
                string slayerString;
                if (Slayer == null)
                {
                    slayerString = " a " + SlayerRace.ToLower();
                }
                else
                {
                    slayerString = Slayer.ToLink(link, pov);
                }

                if (Cause == DeathCause.DragonsFire)
                {
                    deathString = "burned up in " + slayerString + "'s dragon fire";
                }
                else if (Cause == DeathCause.Burned)
                {
                    deathString = "was burned to death by " + slayerString + "'s fire";
                }
                else if (Cause == DeathCause.Murdered)
                {
                    deathString = "was murdered by " + slayerString;
                }
                else if (Cause == DeathCause.Shot)
                {
                    deathString = "was shot and killed by " + slayerString;
                }
                else if (Cause == DeathCause.Struck)
                {
                    deathString = "was struck down by " + slayerString;
                }
                else if (Cause == DeathCause.ExecutedBuriedAlive)
                {
                    deathString = "was buried alive by " + slayerString;
                }
                else if (Cause == DeathCause.ExecutedBurnedAlive)
                {
                    deathString = "was burned alive by " + slayerString;
                }
                else if (Cause == DeathCause.ExecutedCrucified)
                {
                    deathString = "was crucified by " + slayerString;
                }
                else if (Cause == DeathCause.ExecutedDrowned)
                {
                    deathString = "was drowned by " + slayerString;
                }
                else if (Cause == DeathCause.ExecutedFedToBeasts)
                {
                    deathString = "was fed to beasts by " + slayerString;
                }
                else if (Cause == DeathCause.ExecutedHackedToPieces)
                {
                    deathString = "was hacked to pieces by " + slayerString;
                }
                else if (Cause == DeathCause.ExecutedBeheaded)
                {
                    deathString = "was beheaded by " + slayerString;
                }
                else if (Cause == DeathCause.DrainedBlood)
                {
                    deathString = "was drained of blood by " + slayerString;
                }
                else if (Cause == DeathCause.Collapsed)
                {
                    deathString = "collapsed, struck down by " + slayerString;
                }
                else if (Cause == DeathCause.ScaredToDeath)
                {
                    deathString = " was scared to death by " + slayerString;
                }
                else if (Cause == DeathCause.Bled)
                {
                    deathString = " bled to death, slain by " + slayerString;
                }
                else if (Cause == DeathCause.Spikes)
                {
                    deathString = " was impaled by " + slayerString;
                }
                else
                {
                    deathString += ", slain by " + slayerString;
                }
            }
            else
            {
                if (Cause == DeathCause.Thirst)
                {
                    deathString = "died of thirst";
                }
                else if (Cause == DeathCause.OldAge)
                {
                    deathString = "died of old age";
                }
                else if (Cause == DeathCause.Suffocated)
                {
                    deathString = "suffocated";
                }
                else if (Cause == DeathCause.Bled)
                {
                    deathString = "bled to death";
                }
                else if (Cause == DeathCause.Cold)
                {
                    deathString = "froze to death";
                }
                else if (Cause == DeathCause.CrushedByABridge)
                {
                    deathString = "was crushed by a drawbridge";
                }
                else if (Cause == DeathCause.Drowned)
                {
                    deathString = "drowned";
                }
                else if (Cause == DeathCause.Starved)
                {
                    deathString = "starved to death";
                }
                else if (Cause == DeathCause.Infection)
                {
                    deathString = "succumbed to infection";
                }
                else if (Cause == DeathCause.CollidedWithAnObstacle)
                {
                    deathString = "died after colliding with an obstacle";
                }
                else if (Cause == DeathCause.PutToRest)
                {
                    deathString = "was put to rest";
                }
                else if (Cause == DeathCause.StarvedQuit)
                {
                    deathString = "starved";
                }
                else if (Cause == DeathCause.Trap)
                {
                    deathString = "was killed by a trap";
                }
                else if (Cause == DeathCause.CaveIn)
                {
                    deathString = "was crushed under a collapsing ceiling";
                }
                else if (Cause == DeathCause.InACage)
                {
                    deathString = "died in a cage";
                }
                else if (Cause == DeathCause.FrozenInWater)
                {
                    deathString = "was incased in ice";
                }
                else if (Cause == DeathCause.Scuttled)
                {
                    deathString = "was scuttled";
                }
                else if (Cause == DeathCause.Slaughtered)
                {
                    deathString = "was slaughtered";
                }
                else if (Cause == DeathCause.FlyingObject)
                {
                    deathString = "was killed by a flying object";
                }
                else if (Cause == DeathCause.ExecutedBuriedAlive)
                {
                    deathString = "was buried alive";
                }
                else if (Cause == DeathCause.ExecutedBurnedAlive)
                {
                    deathString = "was burned alive";
                }
                else if (Cause == DeathCause.ExecutedCrucified)
                {
                    deathString = "was crucified";
                }
                else if (Cause == DeathCause.ExecutedDrowned)
                {
                    deathString = "was drowned";
                }
                else if (Cause == DeathCause.ExecutedFedToBeasts)
                {
                    deathString = "was fed to beasts";
                }
                else if (Cause == DeathCause.ExecutedHackedToPieces)
                {
                    deathString = "was hacked to pieces";
                }
                else if (Cause == DeathCause.ExecutedBeheaded)
                {
                    deathString = "was beheaded";
                }
                else if (Cause == DeathCause.Melted)
                {
                    deathString = "melted";
                }
                else if (Cause == DeathCause.Spikes)
                {
                    deathString = "was impaled";
                }
                else if (Cause == DeathCause.Heat)
                {
                    deathString = "died in the heat";
                }
                else if (Cause == DeathCause.Vanish)
                {
                    deathString = "vanished";
                }
                else if (Cause == DeathCause.CoolingMagma)
                {
                    deathString = "was encased in cooling magma";
                }
                else if (Cause == DeathCause.Unknown)
                {
                    deathString = "died (" + UnknownCause + ")";
                }
            }

            eventString += deathString;

            if (ItemId >= 0)
            {
                if (Artifact != null)
                {
                    eventString += " with " + Artifact.ToLink(link, pov);
                }
                else if (!string.IsNullOrWhiteSpace(ItemType) || !string.IsNullOrWhiteSpace(ItemSubType))
                {
                    eventString += " with a ";
                    eventString += !string.IsNullOrWhiteSpace(ItemMaterial) ? ItemMaterial + " " : " ";
                    eventString += !string.IsNullOrWhiteSpace(ItemSubType) ? ItemSubType : ItemType;
                }
            }
            else if (ShooterItemId >= 0)
            {
                if (ShooterArtifact != null)
                {
                    eventString += " (shot) with " + ShooterArtifact.ToLink(link, pov);
                }
                else if (!string.IsNullOrWhiteSpace(ShooterItemType) || !string.IsNullOrWhiteSpace(ShooterItemSubType))
                {
                    eventString += " (shot) with a ";
                    eventString += !string.IsNullOrWhiteSpace(ShooterItemMaterial) ? ShooterItemMaterial + " " : " ";
                    eventString += !string.IsNullOrWhiteSpace(ShooterItemSubType) ? ShooterItemSubType : ShooterItemType;
                }
            }
            else if (SlayerItemId >= 0)
            {
                eventString += " with a (" + SlayerItemId + ")";
            }
            else if (SlayerShooterItemId >= 0)
            {
                eventString += " (shot) with a (" + SlayerShooterItemId + ")";
            }

            if (Site != null)
            {
                eventString += " in " + Site.ToLink(link, pov);
            }
            else if (Region != null)
            {
                eventString += " in " + Region.ToLink(link, pov);
            }
            else if (UndergroundRegion != null)
            {
                eventString += " in " + UndergroundRegion.ToLink(link, pov);
            }

            return eventString;
        }
    }
}