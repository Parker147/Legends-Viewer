﻿using System;
using System.Collections.Generic;
using System.Linq;
using LegendsViewer.Controls;
using LegendsViewer.Controls.HTML.Utilities;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Events;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class WrittenContent : WorldObject
    {
        public string Name { get; set; } // legends_plus.xml
        public int PageStart { get; set; } // legends_plus.xml
        public int PageEnd { get; set; } // legends_plus.xml
        public WrittenContentType Type { get; set; } // legends_plus.xml
        public HistoricalFigure Author { get; set; } // legends_plus.xml
        public List<string> Styles { get; set; } // legends_plus.xml
        public List<Reference> References { get; set; } // legends_plus.xml
        public string TypeAsString { get { return Type.GetDescription(); } set { } }
        public int PageCount { get { return PageEnd - PageStart + 1; } set { } }
        public int AuthorRoll { get; set; }
        public int FormId { get; set; }

        public static string Icon = "<i class=\"fa fa-fw fa-book\"></i>";

        public static List<string> Filters;
        public override List<WorldEvent> FilteredEvents
        {
            get { return Events.Where(dwarfEvent => !Filters.Contains(dwarfEvent.Type)).ToList(); }
        }

        public WrittenContent(List<Property> properties, World world)
            : base(properties, world)
        {
            Name = "Untitled";
            Styles = new List<string>();
            References = new List<Reference>();

            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "title": Name = Formatting.InitCaps(property.Value); break;
                    case "page_start": PageStart = Convert.ToInt32(property.Value); break;
                    case "page_end": PageEnd = Convert.ToInt32(property.Value); break;
                    case "reference":
                        property.Known = true;
                        if (property.SubProperties != null)
                        {
                            References.Add(new Reference(property.SubProperties, world));
                        }

                        break;
                    case "form":
                    case "type":
                        switch (property.Value)
                        {
                            case "Autobiography":
                            case "autobiography":
                                Type = WrittenContentType.Autobiography; break;
                            case "Biography":
                            case "biography":
                                Type = WrittenContentType.Biography; break;
                            case "Chronicle":
                            case "chronicle":
                                Type = WrittenContentType.Chronicle; break;
                            case "Dialog":
                            case "dialog":
                                Type = WrittenContentType.Dialog; break;
                            case "Essay":
                            case "essay":
                                Type = WrittenContentType.Essay; break;
                            case "Guide":
                            case "guide":
                                Type = WrittenContentType.Guide; break;
                            case "Letter":
                            case "letter":
                                Type = WrittenContentType.Letter; break;
                            case "Manual":
                            case "manual":
                                Type = WrittenContentType.Manual; break;
                            case "Novel":
                            case "novel":
                                Type = WrittenContentType.Novel; break;
                            case "Play":
                            case "play":
                                Type = WrittenContentType.Play; break;
                            case "Poem":
                            case "poem":
                                Type = WrittenContentType.Poem; break;
                            case "ShortStory":
                            case "short story":
                                Type = WrittenContentType.ShortStory; break;
                            case "MusicalComposition":
                            case "musical composition":
                                Type = WrittenContentType.MusicalComposition; break;
                            case "Choreography":
                            case "choreography":
                                Type = WrittenContentType.Choreography; break;
                            case "CulturalHistory":
                            case "cultural history":
                                Type = WrittenContentType.CulturalHistory; break;
                            case "StarChart":
                            case "star chart":
                                Type = WrittenContentType.StarChart; break;
                            case "ComparativeBiography":
                            case "comparative biography":
                                Type = WrittenContentType.ComparativeBiography; break;
                            case "CulturalComparison":
                            case "cultural comparison":
                                Type = WrittenContentType.CulturalComparison; break;
                            case "Atlas":
                            case "atlas":
                                Type = WrittenContentType.Atlas; break;
                            case "TreatiseOnTechnologicalEvolution":
                            case "treatise on technological evolution":
                                Type = WrittenContentType.TreatiseOnTechnologicalEvolution; break;
                            case "AlternateHistory":
                            case "alternate history":
                                Type = WrittenContentType.AlternateHistory; break;
                            case "StarCatalogue":
                            case "star catalogue":
                                Type = WrittenContentType.StarCatalogue; break;
                            case "Dictionary":
                            case "dictionary":
                                Type = WrittenContentType.Dictionary; break;
                            case "Genealogy":
                            case "genealogy":
                                Type = WrittenContentType.Genealogy; break;
                            case "Encyclopedia":
                            case "encyclopedia":
                                Type = WrittenContentType.Encyclopedia; break;
                            case "BiographicalDictionary":
                            case "biographical dictionary":
                                Type = WrittenContentType.BiographicalDictionary; break;
                            default:
                                Type = WrittenContentType.Unknown;
                                if (!int.TryParse(property.Value.Replace("unknown ", ""), out _))
                                {
                                    property.Known = false;
                                }
                                break;
                        }
                        break;
                    case "author":
                    case "author_hfid":
                        Author = world.GetHistoricalFigure(Convert.ToInt32(property.Value));
                        break;
                    case "style":
                        Styles.Add(property.Value.Contains(":")
                            ? string.Intern(property.Value.Substring(0,
                                property.Value.IndexOf(":", StringComparison.Ordinal)))
                            : string.Intern(property.Value));
                        break;
                    case "author_roll":
                        AuthorRoll = Convert.ToInt32(property.Value);
                        break;
                    case "form_id":
                        FormId = Convert.ToInt32(property.Value);
                        break;
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string type = null;
                if (Type != WrittenContentType.Unknown)
                {
                    type = Type.GetDescription();
                }
                string title = "Written Content";
                title += string.IsNullOrWhiteSpace(type) ? "" : ", " + type;
                title += "&#13";
                title += "Events: " + Events.Count;

                string linkedString = "";
                if (pov != this)
                {
                    linkedString = Icon + "<a href = \"writtencontent#" + Id + "\" title=\"" + title + "\">" + Name + "</a>";
                }
                else
                {
                    linkedString = Icon + "<a title=\"" + title + "\">" + HtmlStyleUtil.CurrentDwarfObject(Name) + "</a>";
                }
                return linkedString;
            }
            return Name;
        }
    }
}
