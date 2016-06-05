using System.Text;
using LegendsViewer.Legends;
using LegendsViewer.Legends.Enums;
using System.Linq;
using LegendsViewer.Legends.Events;

namespace LegendsViewer.Controls
{
    public class WrittenContentPrinter : HTMLPrinter
    {
        WrittenContent WrittenContent;
        World World;

        public WrittenContentPrinter(WrittenContent writtenContent, World world)
        {
            WrittenContent = writtenContent;
            World = world;
        }

        public override string Print()
        {
            HTML = new StringBuilder();
            HTML.AppendLine("<h1>" + WrittenContent.Name + "</h1>");
            string type = null;
            if (WrittenContent.Type != WrittenContentType.Unknown)
            {
                type = WrittenContent.Type.GetDescription();
                string firstWord = WrittenContent.Styles.Count > 0 ? WrittenContent.Styles.First() : type;
                if (firstWord.StartsWith("A") || firstWord.StartsWith("E") || firstWord.StartsWith("I") || firstWord.StartsWith("O") || firstWord.StartsWith("U"))
                {
                    HTML.AppendLine("<b>An ");
                }
                else
                {
                    HTML.AppendLine("<b>A ");
                }
                for (int i = 0; i < WrittenContent.Styles.Count; i++)
                {
                    if (i != 0)
                    {
                        HTML.AppendLine(", ");
                    }
                    HTML.AppendLine(WrittenContent.Styles[i].ToLower());
                    if (i == WrittenContent.Styles.Count - 1)
                    {
                        HTML.AppendLine(" ");
                    }
                }
                HTML.AppendLine(type.ToLower() + " written by " + WrittenContent.Author.ToLink() + ".</b>");
                HTML.AppendLine("<br/>");
            }
            HTML.AppendLine("<br/>");

            PrintReferences();
            PrintEventLog(WrittenContent.Events, WrittenContent.Filters, WrittenContent);
            return HTML.ToString();
        }

        private void PrintReferences()
        {
            if (WrittenContent.References.Any())
            {
                HTML.AppendLine("<b>References</b><br />");
                HTML.AppendLine("<ul>");
                foreach (Reference reference in WrittenContent.References)
                {
                    if (reference.ID != -1)
                    {
                        WorldObject ReferencedObject = null;
                        switch (reference.Type)
                        {
                            case ReferenceType.WrittenContent:
                                ReferencedObject = World.GetWrittenContent(reference.ID);
                                break;
                            case ReferenceType.PoeticForm:
                                ReferencedObject = World.GetPoeticForm(reference.ID);
                                break;
                            case ReferenceType.MusicalForm:
                                ReferencedObject = World.GetMusicalForm(reference.ID);
                                break;
                            case ReferenceType.DanceForm:
                                ReferencedObject = World.GetDanceForm(reference.ID);
                                break;
                            case ReferenceType.Site:
                                ReferencedObject = World.GetSite(reference.ID);
                                break;
                            case ReferenceType.HistoricalEvent:
                                WorldEvent worldEvent = World.GetEvent(reference.ID);
                                if (worldEvent != null)
                                {
                                    HTML.AppendLine("<li>" + worldEvent.Print() + "</li>");
                                }
                                break;
                            case ReferenceType.Entity:
                                ReferencedObject = World.GetEntity(reference.ID);
                                break;
                            case ReferenceType.HistoricalFigure:
                                ReferencedObject = World.GetHistoricalFigure(reference.ID);
                                break;
                            case ReferenceType.ValueLevel:
                                HTML.AppendLine("<li>" + reference.Type + ": " + reference.ID + "</li>");
                                break;
                            case ReferenceType.KnowledgeScholarFlag:
                                HTML.AppendLine("<li>" + reference.Type + ": " + reference.ID + "</li>");
                                break;
                            case ReferenceType.Interaction:
                                HTML.AppendLine("<li>" + reference.Type + ": " + reference.ID + "</li>");
                                break;
                            case ReferenceType.Language:
                                HTML.AppendLine("<li>" + reference.Type + ": " + reference.ID + "</li>");
                                break;
                            case ReferenceType.Subregion:
                                ReferencedObject = World.GetUndergroundRegion(reference.ID);
                                break;
                            case ReferenceType.AbstractBuilding:
                                HTML.AppendLine("<li>" + reference.Type + ": " + reference.ID + "</li>");
                                break;
                            case ReferenceType.Artifact:
                                ReferencedObject = World.GetArtifact(reference.ID);
                                break;
                            case ReferenceType.Sphere:
                                HTML.AppendLine("<li>" + reference.Type + ": " + reference.ID + "</li>");
                                break;
                        }
                        if (ReferencedObject != null)
                        {
                            HTML.AppendLine("<li>" + ReferencedObject.ToLink() + "</li>");
                        }
                    }
                }
                HTML.AppendLine("</ul>");
                HTML.AppendLine("</br>");
            }
        }

        public override string GetTitle()
        {
            return WrittenContent.Name;
        }
    }
}
