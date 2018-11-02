﻿using System.Collections.Generic;
using LegendsViewer.Controls.HTML.Utilities;
using LegendsViewer.Legends.Enums;
using LegendsViewer.Legends.Parser;

namespace LegendsViewer.Legends
{
    public class MusicalForm : ArtForm
    {
        public static string Icon = "<i class=\"fa fa-fw fa-music\"></i>";

        public MusicalForm(List<Property> properties, World world)
            : base(properties, world)
        {
            FormType = FormType.Musical;
        }

        public override string ToLink(bool link = true, DwarfObject pov = null)
        {
            if (link)
            {
                string title = "Musical Form";
                title += "&#13";
                title += "Events: " + Events.Count;

                if (pov != this)
                {
                    return Icon + "<a href=\"musicalform#" + Id + "\" title=\"" + title + "\">" + Name + "</a>";
                }
                return Icon + "<a title=\"" + title + "\">" + HtmlStyleUtil.CurrentDwarfObject(Name) + "</a>";
            }
            return Name;
        }
    }
}
