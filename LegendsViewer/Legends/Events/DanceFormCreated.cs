using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class DanceFormCreated : FormCreatedEvent
    {
        public DanceFormCreated(List<Property> properties, World world) : base(properties, world)
        {
            FormType = FormType.Dance;
        }
    }
}