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
    }
}
