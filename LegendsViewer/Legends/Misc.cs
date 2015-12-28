using System.Collections.Generic;
using System.ComponentModel;

namespace LegendsViewer.Legends
{
    internal static class Misc
    {
        public static void AddEvent(this WorldObject worldObject, WorldEvent worldEvent)
        {
            if (worldObject != null)
                worldObject.Events.Add(worldEvent);
        }
    }

    public class Location : IEqualityComparer<Location>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public System.Drawing.Point ToPoint()
        {
            return new System.Drawing.Point(X, Y);
        }

        public static bool operator ==(Location a, Location b)
        {
            if ((object)a == null || (object)b == null)
                return false;
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Location a, Location b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Location b = obj as Location;
            if ((object)b == null)
            {
                return false;
            }

            return X == b.X && Y == b.Y;
        }

        public override int  GetHashCode()
        {
 	        return X ^ Y;
        }

        public bool Equals(Location b)
        {
            return X == b.X && Y == b.Y;
        }

        public bool Equals(Location a, Location b)
        {
            return a == b;
        }

        public int GetHashCode(Location location)
        {
            return location.X ^ location.Y;
        }
    }

    public enum DeathCause
    {
        None,
        Struck,
        [Description("Old Age")]
        OldAge,
        Thirst,
        Suffocated,
        Bled,
        Cold,
        [Description("Crushed by a Bridge")]
        CrushedByABridge,
        Drowned,
        Starved,
        [Description("In a Cage")]
        InACage,
        Infection,
        [Description("Collided With an Obstacle")]
        CollidedWithAnObstacle,
        [Description("Put to Rest")]
        PutToRest,
        [Description("Starved on Quit")]
        StarvedQuit,
        Trap,
        [Description("Dragon's Fire")]
        DragonsFire,
        Burned,
        Murdered,
        Shot,
        [Description("Cave In")]
        CaveIn,
        [Description("Frozen in Water")]
        FrozenInWater,
        [Description("Executed - Fed To Beasts")]
        ExecutedFedToBeasts,
        [Description("Executed - Burned Alive")]
        ExecutedBurnedAlive,
        [Description("Executed - Crucified")]
        ExecutedCrucified,
        [Description("Executed - Drowned")]
        ExecutedDrowned,
        [Description("Executed - Hacked To Pieces")]
        ExecutedHackedToPieces,
        [Description("Executed - Buried Alive")]
        ExecutedBuriedAlive,
        [Description("Executed - Beheaded")]
        ExecutedBeheaded,
        [Description("Drained of blood")]
        DrainedBlood,
        Collapsed,
        [Description("Scared to death")]
        ScaredToDeath,
        Scuttled,
        Unknown
    }
}
