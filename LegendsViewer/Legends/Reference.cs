using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;
using System;
using System.Collections.Generic;

namespace LegendsViewer.Legends
{
    public class Reference
    {
        public int ID { get; set; } // legends_plus.xml
        public ReferenceType Type { get; set; } // legends_plus.xml

        public Reference(List<Property> properties, World world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "id": ID = Convert.ToInt32(property.Value); break;
                    case "type":
                        switch (property.Value)
                        {
                            case "WRITTEN_CONTENT": Type = ReferenceType.WrittenContent; break;
                            case "POETIC_FORM": Type = ReferenceType.PoeticForm; break;
                            case "MUSICAL_FORM": Type = ReferenceType.MusicalForm; break;
                            case "DANCE_FORM": Type = ReferenceType.DanceForm; break;
                            case "VALUE_LEVEL": Type = ReferenceType.ValueLevel; break;
                            case "KNOWLEDGE_SCHOLAR_FLAG": Type = ReferenceType.KnowledgeScholarFlag; break;
                            case "SITE": Type = ReferenceType.Site; break;
                            case "HISTORICAL_EVENT": Type = ReferenceType.HistoricalEvent; break;
                            case "INTERACTION": Type = ReferenceType.Interaction; break;
                            case "ENTITY": Type = ReferenceType.Entity; break;
                            case "HISTORICAL_FIGURE": Type = ReferenceType.HistoricalFigure; break;
                            case "LANGUAGE": Type = ReferenceType.Language; break;
                            case "SUBREGION": Type = ReferenceType.Subregion; break;
                            case "ABSTRACT_BUILDING": Type = ReferenceType.AbstractBuilding; break;
                            case "ARTIFACT": Type = ReferenceType.Artifact; break;
                            default:
                                world.ParsingErrors.Report("Unknown WrittenContent ReferenceType: " + property.Value);
                                break;
                        }
                        break;
                }
            }
        }
    }
}
