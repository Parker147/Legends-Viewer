using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsViewer.Legends
{
    public class HistoricalFigureLink
    {
        public HistoricalFigure HistoricalFigure { get; set; }
        public int Strength { get; set; }
        public HistoricalFigureLinkType Type { get; set; }
        private int notYetLoadedHFID = -1;

        public HistoricalFigureLink(List<Property> properties, World world)
        {
            Strength = 0;
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "hfid":
                        int id = Convert.ToInt32(property.Value);
                        HistoricalFigure = world.GetHistoricalFigure(id);
                        if (HistoricalFigure == null)
                            notYetLoadedHFID = id;
                        break;
                    case "link_strength": Strength = Convert.ToInt32(property.Value); break;
                    case "link_type":
                        HistoricalFigureLinkType linkType = HistoricalFigureLinkType.Unknown;
                        if (!Enum.TryParse(Formatting.InitCaps(property.Value), out linkType))
                        {
                            Type = HistoricalFigureLinkType.Unknown;
                            world.Log.AppendLine("Unknown HF Link Type: " + property.Value);
                        }
                        else
                            Type = linkType;                              
                        break;
                }
            }

            //XML states that deity links, that should be 100, are 0.
            if (Type == HistoricalFigureLinkType.Deity && Strength == 0)
                Strength = 100;
        }

        public HistoricalFigureLink(HistoricalFigure hf, HistoricalFigureLinkType type)
        {
            HistoricalFigure = hf;
            Type = type;
        }

        public void FixUnloadedHistoricalFigure(HistoricalFigure hf)
        {
            if (hf.ID == notYetLoadedHFID)
                HistoricalFigure = hf;
        }

    }

    public enum HistoricalFigureLinkType
    {
        Apprentice,
        Child,
        Deity,
        Father,
        Master,
        Mother,
        Spouse,
        Unknown
    }

    class EntityLink
    {
    }

    class SiteLink
    {
    }
}
