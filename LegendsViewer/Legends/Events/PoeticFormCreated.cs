using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends.Events
{
    public class PoeticFormCreated : FormCreatedEvent
    {
        public PoeticFormCreated(List<Property> properties, World world) : base(properties, world)
        {
            FormType = FormType.Poetic;
        }
    }
}