using System;
using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class AgreementFormed : WorldEvent
    {
        private string AgreementId { get; set; }
        public HistoricalFigure Concluder { get; set; }
        private string AgreementSubjectId { get; set; }
        private AgreementReason Reason { get; set; }

        public AgreementFormed(List<Property> properties, World world) : base(properties, world)
        {
            foreach (Property property in properties)
            {
                switch (property.Name)
                {
                    case "agreement_id": AgreementId = property.Value; break;
                    case "concluder_hfid": Concluder = world.GetHistoricalFigure(Convert.ToInt32(property.Value)); break;
                    case "agreement_subject_id": AgreementSubjectId = property.Value; break;
                    case "reason":
                        switch (property.Value)
                        {
                            case "arrived at location": Reason = AgreementReason.ArrivedAtLocation; break;
                            case "violent disagreement": Reason = AgreementReason.ViolentDisagreement; break;
                            case "whim": Reason = AgreementReason.Whim; break;
                            default:
                                Reason = AgreementReason.Unknown;
                                world.ParsingErrors.Report("Unknown Agreement Reason: " + property.Value);
                                break;
                        }
                        break;
                }
            }
            Concluder.AddEvent(this);
        }

        public override string Print(bool link = true, DwarfObject pov = null)
        {
            string eventString = GetYearTime();
            eventString += Concluder != null ? Concluder.ToLink(link, pov) : "UNKNOWN HISTORICAL FIGURE";
            eventString += " formed an agreement";
            switch (Reason)
            {
                case AgreementReason.Whim:
                    eventString += " on a whim";
                    break;
                case AgreementReason.ViolentDisagreement:
                    eventString += " a violent disagreement";
                    break;
                case AgreementReason.ArrivedAtLocation:
                    eventString += " after arriving at the location";
                    break;
            }
            eventString += ". ";
            eventString += PrintParentCollection(link, pov);
            return eventString;
        }
    }
}