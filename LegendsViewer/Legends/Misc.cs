﻿using System.Collections.Generic;
using LegendsViewer.Legends.Events;

namespace LegendsViewer.Legends
{
    public static class Misc
    {
        public static void AddEvent(this WorldObject worldObject, WorldEvent worldEvent)
        {
            if (worldObject != null)
            {
                worldObject.Events.Add(worldEvent);
            }
        }

        public static T GetWorldObject<T>(this List<T> list, int id) where T : WorldObject
        {
            int min = 0;
            int max = list.Count - 1;
            while (min <= max)
            {
                int mid = min + (max - min) / 2;
                if (id > list[mid].Id)
                {
                    min = mid + 1;
                }
                else if (id < list[mid].Id)
                {
                    max = mid - 1;
                }
                else
                {
                    return list[mid];
                }
            }
            return null;
        }
    }
}