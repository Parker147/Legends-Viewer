﻿using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class HfFreed : WorldEvent
    {
        public List<HistoricalFigure> RescuedHistoricalFigures { get; set; }
        public Entity FreeingCiv { get; set; }
        public Entity SiteCiv { get; set; }
        public Entity HoldingCiv { get; set; }
        public Site Site { get; set; }

        public HfFreed(List<Property> properties, World world)
            : base(properties, world)
        {
            RescuedHistoricalFigures = new List<HistoricalFigure>();
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "freeing_civ_id": FreeingCiv = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "site_id": Site = world.GetSite(Convert.ToInt32(property.Value)); break;
                    case "rescued_hfid": RescuedHistoricalFigures.Add(world.GetHistoricalFigure(Convert.ToInt32(property.Value))); break;
                    case "site_civ_id": SiteCiv = world.GetEntity(Convert.ToInt32(property.Value)); break;
                    case "holding_civ_id": HoldingCiv = world.GetEntity(Convert.ToInt32(property.Value)); break;
                }
            }
            foreach (var rescuedHistoricalFigure in RescuedHistoricalFigures)
            {
                rescuedHistoricalFigure.AddEvent(this);
            }
            FreeingCiv.AddEvent(this);
            Site.AddEvent(this);
            SiteCiv.AddEvent(this);
            HoldingCiv.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += "the forces of ";
            eventString += FreeingCiv?.ToLink(link, pov) ?? "an unknown civilization";
            eventString += " freed ";
            for (int i = 0; i < RescuedHistoricalFigures.Count; i++)
            {
                if (i > 0)
                {
                    eventString += " and ";
                }
                eventString += RescuedHistoricalFigures[i]?.ToLink(link, pov) ?? "an unknown creature";
            }
            if (Site != null)
            {
                eventString += " from " + Site.ToLink(link, pov);
            }
            if (SiteCiv != null)
            {
                eventString += " and " + SiteCiv.ToLink(link, pov);
            }
            if (HoldingCiv != null)
            {
                eventString += " of " + HoldingCiv.ToLink(link, pov);
            }
            eventString += PrintParentCollection(link, pov);
            eventString += ".";
            return eventString;
        }
    }
}