using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;
using System;

namespace LegendsViewer.Legends.Events
{
    public class PoeticFormCreated : FormCreatedEvent
    {
        public PoeticFormCreated(List<Property> properties, World world) : base(properties, world)
        {
            FormType = FormType.Poetic;
            if (!string.IsNullOrWhiteSpace(FormId))
            {
                ArtForm = world.GetPoeticForm(Convert.ToInt32(FormId));
                ArtForm.AddEvent(this);
            }
        }
    }
}