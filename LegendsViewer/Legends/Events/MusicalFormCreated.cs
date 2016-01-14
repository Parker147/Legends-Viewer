using System.Collections.Generic;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;
using System;

namespace LegendsViewer.Legends.Events
{
    public class MusicalFormCreated : FormCreatedEvent
    {
        public MusicalFormCreated(List<Property> properties, World world) : base(properties, world)
        {
            FormType = FormType.Musical;
            if (!string.IsNullOrWhiteSpace(FormId))
            {
                ArtForm = world.GetMusicalForm(Convert.ToInt32(FormId));
                ArtForm.AddEvent(this);
            }
        }
    }
}