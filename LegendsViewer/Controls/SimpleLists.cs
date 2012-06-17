using System.Collections.Generic;
using LegendsViewer.Legends;
using System.Linq;
using System.Text;

namespace LegendsViewer
{

        public class HistoricalFigureList
        {
            private World World;
            public bool deity, vampire, force, werebeast, ghost, alive, Leader, sortKills, sortEvents, sortFiltered, sortBattles;
            public string name, race, caste, type;
            public List<HistoricalFigure> BaseList;
            public HistoricalFigureList(World setWorld) { World = setWorld; BaseList = World.HistoricalFigures; }
            public IEnumerable<HistoricalFigure> GetList()
            {
                IEnumerable<HistoricalFigure> filtered = BaseList;
                if (name != "") filtered = filtered.Where(hf => hf.Name.ToLower().Contains(name.ToLower()));
                if (race != "All") filtered = filtered.Where(hf => hf.Race == race);
                if (caste != "All") filtered = filtered.Where(hf => hf.Caste == caste);
                if (type != "All") filtered = filtered.Where(hf => hf.AssociatedType == type);
                if (deity) filtered = filtered.Where(hf => hf.Deity);
                if (vampire ) filtered = filtered.Where(hf => hf.ActiveInteraction.Contains("VAMPIRE"));
                if (werebeast) filtered = filtered.Where(hf => hf.ActiveInteraction.Contains("WEREBEAST"));
                if (force) filtered = filtered.Where(hf => hf.Force);
                if (ghost) filtered = filtered.Where(hf => hf.Ghost);
                if (Leader) filtered = filtered.Where(hf => hf.Positions.Count > 0);
                if (alive) filtered = filtered.Where(hf => hf.DeathYear == -1);
                if (sortKills) filtered = filtered.OrderByDescending(hf => hf.NotableKills.Count);
                if (sortEvents) filtered = filtered.OrderByDescending(hf => hf.Events.Count);
                if (sortFiltered) filtered = filtered.OrderByDescending(hf => hf.Events.Count(ev => !HistoricalFigure.Filters.Contains(ev.Type)));
                if (sortBattles) filtered = filtered.OrderByDescending(hf => hf.Battles.Count(battle => !World.FilterBattles || battle.Notable));
                return filtered.Take(500);
            }
        }

        public class SitesList
        {
            private World World;
            public string name, type, PopulationType;
            public bool sortEvents, sortOwners, sortFiltered, sortWarfare, SortPopulation, SortConnections, SortDeaths, SortBeastAttacks;
            public List<Site> BaseList;
            public SitesList(World setWorld) { World = setWorld; BaseList = World.Sites; }
            public IEnumerable<Site> getList()
            {
                IEnumerable<Site> filtered = BaseList;
                if (name != "") filtered = filtered.Where(s => s.Name.ToLower().Contains(name.ToLower()) || s.UntranslatedName.ToLower().Contains(name.ToLower()));
                if (type != "All") filtered = filtered.Where(s => s.Type == type);
                if (sortOwners) filtered = filtered.OrderByDescending(s => s.OwnerHistory.Count());
                if (sortEvents) filtered = filtered.OrderByDescending(s => s.Events.Count);
                if (sortFiltered) filtered = filtered.OrderByDescending(s => s.Events.Count(ev => !Site.Filters.Contains(ev.Type)));
                if (sortWarfare) filtered = filtered.OrderByDescending(site => site.Warfare.OfType<Battle>().Count(battle => !World.FilterBattles || battle.Notable));
                if (SortPopulation)
                    if (PopulationType != "All") filtered = filtered.Where(site => site.Populations.Count(population => population.Race == PopulationType) > 0).OrderByDescending(site => site.Populations.Where(population => population.Race == PopulationType).Sum(population => population.Count));
                    else filtered = filtered.OrderByDescending(site => site.Populations.Sum(population => population.Count));
                if (SortConnections) filtered = filtered.OrderByDescending(site => site.Connections.Count);
                if (SortDeaths) filtered = filtered.OrderByDescending(site => site.Events.OfType<HFDied>().Count() + site.Warfare.OfType<Battle>().Sum(battle => battle.AttackerSquads.Sum(squad => squad.Deaths) + battle.DefenderSquads.Sum(squad => squad.Deaths)));
                if (SortBeastAttacks) filtered = filtered.OrderByDescending(site => site.BeastAttacks.Count);
                return filtered;
            }
        }

        public class RegionsList
        {
            private World World;
            public string name, type;
            public bool sortEvents, sortFiltered, sortBattles, SortDeaths;
            public List<WorldRegion> BaseList;
            public RegionsList(World setWorld) { World = setWorld; BaseList = World.Regions; }
            public IEnumerable<WorldRegion> getList()
            {
                IEnumerable<WorldRegion> filtered = BaseList;
                if (name != "") filtered = filtered.Where(r => r.Name.ToLower().Contains(name.ToLower()));
                if (type != "All") filtered = filtered.Where(r => r.Type == type);
                if (sortEvents) filtered = filtered.OrderByDescending(r => r.Events.Count);
                if (sortFiltered) filtered = filtered.OrderByDescending(r => r.Events.Count(ev => !WorldRegion.Filters.Contains(ev.Type)));
                if (sortBattles) filtered = filtered.OrderByDescending(region => region.Battles.Count(battle => !World.FilterBattles || battle.Notable));
                if (SortDeaths) filtered = filtered.OrderByDescending(region => region.Events.OfType<HFDied>().Count() + region.Battles.OfType<Battle>().Sum(battle => battle.AttackerSquads.Sum(squad => squad.Deaths) + battle.DefenderSquads.Sum(squad => squad.Deaths)));
                return filtered;
            }
        }

        public class UndergroundRegionsList
        {
            private World world;
            public string type;
            public bool sortEvents, sortFiltered;
            public List<UndergroundRegion> BaseList;
            public UndergroundRegionsList(World setWorld) { world = setWorld; BaseList = world.UndergroundRegions; }
            public IEnumerable<UndergroundRegion> getList()
            {
                IEnumerable<UndergroundRegion> filtered = BaseList;
                if (type != "All") filtered = filtered.Where(ur => ur.Type == type);
                if (sortEvents) filtered = filtered.OrderByDescending(ur => ur.Events.Count);
                if (sortFiltered) filtered = filtered.OrderByDescending(ur => ur.Events.Count(ev => !UndergroundRegion.Filters.Contains(ev.Type)));
                return filtered;
            }
        }

        public class EntitiesList
        {
            private World world;
            public string name, race, PopulationType;
            public bool sortEvents, sortSites, civs, sortFiltered, sortWars, SortPopulation;
            public List<Entity> BaseList;
            public EntitiesList(World setWorld) { world = setWorld; BaseList = world.Entities; }
            public IEnumerable<Entity> getList()
            {
                IEnumerable<Entity> filtered = BaseList.Where(entity => entity.Name != "");
                if (name != "") filtered = filtered.Where(e => e.Name.ToLower().Contains(name.ToLower()));
                if (race != "All") filtered = filtered.Where(e => e.Race == race);
                if (civs) filtered = filtered.Where(e => e.IsCiv);
                if (sortSites) filtered = filtered.OrderByDescending(civ => civ.SiteHistory.Count);
                if (sortEvents) filtered = filtered.OrderByDescending(e => e.Events.Count);
                if (sortFiltered) filtered = filtered.OrderByDescending(e => e.Events.Count(ev => !Entity.Filters.Contains(ev.Type)));
                if (sortWars) filtered = filtered.OrderByDescending(entity => entity.Wars.Count(war => !world.FilterBattles || war.Notable));
                if (SortPopulation)
                    if (PopulationType != "All") filtered = filtered.Where(entity => entity.Populations.Count(population => population.Race == PopulationType) > 0).OrderByDescending(civ => civ.Populations.Where(population => population.Race == PopulationType).Sum(population => population.Count));
                    else filtered = filtered.OrderByDescending(civ => civ.Populations.Sum(population => population.Count));

                return filtered;
            }
        }

        public class WarsList
        {
            private World World;
            public string Name;
            public bool SortEvents, SortFiltered, SortLength, SortDeaths, Ongoing, SortWarfare, SortConquerings;
            public List<War> BaseList;
            public WarsList(World world) { World = world; BaseList = world.EventCollections.OfType<War>().ToList(); }
            public IEnumerable<War> GetList()
            {
                IEnumerable<War> filtered = BaseList; // BaseList.Where(war => !World.FilterBattles || war.Notable);
                if (Name != "") filtered = filtered.Where(war => war.Name.ToLower().Contains(Name.ToLower()));
                if (Ongoing) filtered = filtered.Where(war => war.EndYear == -1);
                if (SortEvents) filtered = filtered.OrderByDescending(war => war.GetSubEvents().Count);
                if (SortFiltered) filtered = filtered.OrderByDescending(war => war.GetSubEvents().Count(ev => !War.Filters.Contains(ev.Type)));
                if (SortLength) filtered = filtered.OrderByDescending(war => war.Length);
                if (SortDeaths) filtered = filtered.OrderByDescending(war => war.DeathCount);
                if (SortWarfare) filtered = filtered.OrderByDescending(war => war.Collections.Count);
                if (SortConquerings) filtered = filtered.OrderByDescending(war => war.Collections.OfType<SiteConquered>().Count(conquering => conquering.ConquerType != SiteConqueredType.Pillaging));
                return filtered;
            }
        }

        public class BattlesList
        {
            private World World;
            public string Name;
            public bool SortEvents, SortFiltered, SortDeaths;
            public List<Battle> BaseList;
            public BattlesList(World world) { World = world; BaseList = world.EventCollections.OfType<Battle>().ToList(); }
            public IEnumerable<Battle> GetList()
            {
                IEnumerable<Battle> filtered = BaseList;// BaseList.Where(battle => !World.FilterBattles || battle.Notable);
                if (Name != "") filtered = filtered.Where(battle => battle.Name.ToLower().Contains(Name.ToLower()));
                if (SortEvents) filtered = filtered.OrderByDescending(battle => battle.GetSubEvents().Count);
                if (SortFiltered) filtered = filtered.OrderByDescending(battle => battle.GetSubEvents().Count(ev => !Battle.Filters.Contains(ev.Type)));
                if (SortDeaths) filtered = filtered.OrderByDescending(battle => battle.AttackerDeathCount + battle.DefenderDeathCount);
                return filtered;
            }
        }

        public class ConqueringsList
        {
            private World World;
            public string Name, Type;
            public bool SortEvents, SortFiltered, SortSite;
            public List<SiteConquered> BaseList;
            public ConqueringsList(World world) { World = world; BaseList = world.EventCollections.OfType<SiteConquered>().ToList(); }
            public IEnumerable<SiteConquered> GetList()
            {
                IEnumerable<SiteConquered> filtered = BaseList;// Where(conquering => !World.FilterBattles || conquering.Notable);
                if (Name != "") filtered = filtered.Where(pillaging => pillaging.Site.Name.ToLower().Contains(Name.ToLower()));
                if (Type != "All") filtered = filtered.Where(conquering => conquering.ConquerType.ToString() == Type);
                if (SortEvents) filtered = filtered.OrderByDescending(pillaging => pillaging.GetSubEvents().Count);
                if (SortFiltered) filtered = filtered.OrderByDescending(pillaging => pillaging.GetSubEvents().Count(ev => !SiteConquered.Filters.Contains(ev.Type)));
                if (SortSite) filtered = filtered.OrderBy(conquering => conquering.Site.ToString());
                return filtered;
            }
        }

        public class BeastAttackList
        {
            private World World;
            public string Name;
            public bool SortEvents, SortFiltered, SortDeaths;
            public List<BeastAttack> BaseList;
            public BeastAttackList(World world) { World = world; BaseList = world.EventCollections.OfType<BeastAttack>().ToList(); }
            public IEnumerable<BeastAttack> GetList()
            {
                IEnumerable<BeastAttack> filtered = BaseList;
                if (Name != "") filtered = filtered.Where(beastAttack => beastAttack.ToString().ToLower().Contains(Name.ToLower()));
                if (SortEvents) filtered = filtered.OrderByDescending(beastAttack => beastAttack.GetSubEvents().Count);
                if (SortFiltered) filtered = filtered.OrderByDescending(beastAttack => beastAttack.GetSubEvents().Count(ev => !BeastAttack.Filters.Contains(ev.Type)));
                if (SortDeaths) filtered = filtered.OrderByDescending(beastAttack => beastAttack.GetSubEvents().OfType<HFDied>().Count());
                return filtered;
            }
        }
    
}
