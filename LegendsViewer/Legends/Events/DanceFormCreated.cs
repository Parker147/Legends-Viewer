using System;
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
            if (!string.IsNullOrWhiteSpace(FormId))
            {
                ArtForm = world.GetDanceForm(Convert.ToInt32(FormId));
                ArtForm.AddEvent(this);
            }
        }
    }
}